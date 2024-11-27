﻿using Microsoft.Extensions.AI;
using System;
using System.Linq;
using System.Threading.RateLimiting;

namespace XafSmartEditors.Razor.Middleware
{
    public static class UseRateLimitMiddleware
    {
        public static ChatClientBuilder UseRateLimitTaskDelay(this ChatClientBuilder chatClientBuilder, int rateLimit)
        {
            chatClientBuilder.Use(inner => new UseRateLimitClientTaskDelay(inner, rateLimit));
            return chatClientBuilder;


        }
        public static ChatClientBuilder UseRateLimitThreading(this ChatClientBuilder chatClientBuilder, TimeSpan window)
        {
            chatClientBuilder.Use(inner => new UseRateLimitClientWindow(inner, window));
            return chatClientBuilder;


        }

        private class UseRateLimitClientTaskDelay : DelegatingChatClient
        {
            private readonly int rateLimit;
            private DateTime lastRequest = DateTime.MinValue;
            public UseRateLimitClientTaskDelay(IChatClient chatClient, int rateLimit) : base(chatClient)
            {
                this.rateLimit = rateLimit;
            }
            public override async Task<ChatCompletion> CompleteAsync(IList<ChatMessage> chatMessages, ChatOptions options = null, CancellationToken cancellationToken = default)
            {
                var now = DateTime.Now;
                var timeSinceLastRequest = now - lastRequest;
                if (timeSinceLastRequest < TimeSpan.FromSeconds(rateLimit))
                {
                    await Task.Delay(TimeSpan.FromSeconds(rateLimit) - timeSinceLastRequest);
                }
                lastRequest = DateTime.Now;
                return await base.CompleteAsync(chatMessages, options, cancellationToken);
            }
        }

        private class UseRateLimitClientWindow : DelegatingChatClient
        {
            RateLimiter rateLimiter;
            public UseRateLimitClientWindow(IChatClient innerClient, TimeSpan window) : base(innerClient)
            {
                FixedWindowRateLimiterOptions options = new FixedWindowRateLimiterOptions { Window = window, QueueLimit = 1, PermitLimit = 1 };
                rateLimiter = new FixedWindowRateLimiter(options);
            }
            public async override Task<ChatCompletion> CompleteAsync(IList<ChatMessage> chatMessages, ChatOptions options = null, CancellationToken cancellationToken = default)
            {
                var Leas = rateLimiter.AttemptAcquire();
                if (!Leas.IsAcquired)
                {
                    return new ChatCompletion(new ChatMessage(ChatRole.Assistant, "Rate limit exceeded"));
                }
                return await base.CompleteAsync(chatMessages, options, cancellationToken);
            }

        }
    }
}
