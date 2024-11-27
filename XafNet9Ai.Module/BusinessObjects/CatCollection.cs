using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafNet9Ai.Module.BusinessObjects
{
    [DefaultClassOptions]
    [Appearance("CatCollectionDetailsIsCat", TargetItems = "*", Criteria = "Processing = true",Enabled =false)]
    public class CatCollection : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public CatCollection(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://docs.devexpress.com/eXpressAppFramework/112834/getting-started/in-depth-tutorial-winforms-webforms/business-model-design/initialize-a-property-after-creating-an-object-xpo?v=22.1).
        }

        bool processing;
        int numberOfNotAnimals;
        int numberOfOtherAnimals;
        int numberOfWhiteCats;
        int numberOfBlackCats;
        string name;


        [Browsable(false)]
        public bool Processing
        {
            get => processing;
            set => SetPropertyValue(nameof(Processing), ref processing, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        public int NumberOfBlackCats
        {
            get => numberOfBlackCats;
            set => SetPropertyValue(nameof(NumberOfBlackCats), ref numberOfBlackCats, value);
        }

        public int NumberOfWhiteCats
        {
            get => numberOfWhiteCats;
            set => SetPropertyValue(nameof(NumberOfWhiteCats), ref numberOfWhiteCats, value);
        }

        public int NumberOfOtherAnimals
        {
            get => numberOfOtherAnimals;
            set => SetPropertyValue(nameof(NumberOfOtherAnimals), ref numberOfOtherAnimals, value);
        }

        
        public int NumberOfNotAnimals
        {
            get => numberOfNotAnimals;
            set => SetPropertyValue(nameof(NumberOfNotAnimals), ref numberOfNotAnimals, value);
        }

        [Association("CatCollection-CatCollectionDetails"),DevExpress.Xpo.Aggregated()]
        public XPCollection<CatCollectionDetail> CatCollectionDetails
        {
            get
            {
                return GetCollection<CatCollectionDetail>(nameof(CatCollectionDetails));
            }
        }
    }
}