using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafNet9Ai.Module.BusinessObjects
{

    public class EmbeddingDataValueConverter: ValueConverter
    {
     
   
        public EmbeddingDataValueConverter()
        {
            
        }

        public override Type StorageType => typeof(string);

        public override object ConvertFromStorageType(object value)
        {
           return FloatArrayConverter.StringToArray(value.ToString());
        }

        public override object ConvertToStorageType(object value)
        {
            return FloatArrayConverter.ArrayToString((float[])value);
        }
    }
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://docs.devexpress.com/eXpressAppFramework/112701/business-model-design-orm/data-annotations-in-data-model).
    public class XpoEmbedding : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public XpoEmbedding(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://docs.devexpress.com/eXpressAppFramework/112834/getting-started/in-depth-tutorial-winforms-webforms/business-model-design/initialize-a-property-after-creating-an-object-xpo?v=22.1).
        }

        string code;
        float[] data;
        string text;
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Code
        {
            get => code;
            set => SetPropertyValue(nameof(Code), ref code, value);
        }
        [Size(SizeAttribute.Unlimited)]
        public string Text
        {
            get => text;
            set => SetPropertyValue(nameof(Text), ref text, value);
        }
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(EmbeddingDataValueConverter))]
        public float[] Data
        {
            get => data;
            set => SetPropertyValue(nameof(Data), ref data, value);
        }

        public Embedding<float>  GetEmbedding()
        {
            return new Embedding<float>(Data);
        }
    }
}