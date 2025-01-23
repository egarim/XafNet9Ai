using System;
using System.Linq;

namespace XafNet9Ai.Win.Controllers
{
    using System;
    using System.Linq;
    // Helper class to find types by name
    public static class ReflectionHelper
    {
        public static Type FindType(string fullName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == fullName);
        }
    }
}
