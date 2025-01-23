using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.PivotChart;
using DevExpress.ExpressApp.PivotChart.Win;
using DevExpress.ExpressApp.PivotGrid.Win;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Utils.Extensions;
using DevExpress.XtraPivotGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafNet9Ai.Module.BusinessObjects;

namespace XafNet9Ai.Win.Controllers
{

    public class MyViewController : ObjectViewController<DetailView,Analysis>
    {
        SimpleAction TestPivot;
        public MyViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            TestPivot = new SimpleAction(this, "TestPivot", "View");
            TestPivot.Execute += TestPivot_Execute;
           

        }
        private void AttachToPivotEditor(DetailView view)
        {
            IList<IAnalysisEditorWin> analysisEditors = view.GetItems<IAnalysisEditorWin>();
            if (analysisEditors.Count > 0)
            {
                //analysisEditor = analysisEditors[0];
                //if (analysisEditor.Control == null)
                //{
                //    analysisEditor.ControlCreated += new EventHandler<EventArgs>(analysisEditor_ControlCreated);
                //}
                //else
                //{
                //    AttachToPivotGrid(analysisEditor.Control.PivotGrid);
                //}
            }
        }

        private void TestPivot_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AttachToPivotEditor(this.View);
            Update(this.View.CurrentObject as Analysis);
            this.View.ObjectSpace.CommitChanges();

        }
        public void Update(Analysis analysis)
        {
            if (analysis != null && !PivotGridSettingsHelper.HasPivotGridSettings(analysis))
            {
                if (analysis.DataType == null)
                {
                    analysis.DataType = typeof(Invoice);
                }
                AnalysisControlWin control = this.View.GetItems<IAnalysisEditorWin>()[0].Control;
                //IAnalysisControl control= new AnalysisControlWin();
                control.DataSource = new AnalysisDataSource(analysis, this.View.ObjectSpace.GetObjects(typeof(Invoice)));
                control.FieldBuilder.RebuildFields();
                control.Fields["Date"].Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
                control.Fields["TotalAmount"].Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
                control.Fields["Number"].Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;


                //PivotGridSettingsHelper.SavePivotGridSettings(CreatePivotGridSettingsStore(control), analysis);
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
    //public class MyViewController : ViewController
    //{
    //    private SimpleAction createPivotGridAction;

    //    public MyViewController()
    //    {
    //        createPivotGridAction = new SimpleAction(this, "CreatePivotGrid", "View");
    //        createPivotGridAction.Execute += CreatePivotGridAction_Execute;
    //    }

    //    private void CreatePivotGridAction_Execute(object sender, SimpleActionExecuteEventArgs e)
    //    {
    //        // Create the pivot grid
    //        PivotGridListEditor pivotGridListEditor = new PivotGridListEditor(this.Application);
    //        PivotGridControl pivotGridControl = pivotGridListEditor.PivotGridControl;

    //        // Configure the pivot grid
    //        ConfigurePivotGrid(pivotGridControl);

    //        // Show the pivot grid in a new window
    //        e.ShowViewParameters.CreatedView = Application.CreateListView(ObjectSpace, typeof(YourDataType), pivotGridListEditor, true);
    //    }

    //    private void ConfigurePivotGrid(PivotGridControl pivotGridControl)
    //    {
    //        // Add fields to the pivot grid
    //        PivotGridField field1 = pivotGridControl.Fields.Add();
    //        field1.Area = PivotArea.RowArea;
    //        field1.FieldName = "YourRowField";

    //        PivotGridField field2 = pivotGridControl.Fields.Add();
    //        field2.Area = PivotArea.ColumnArea;
    //        field2.FieldName = "YourColumnField";

    //        PivotGridField field3 = pivotGridControl.Fields.Add();
    //        field3.Area = PivotArea.DataArea;
    //        field3.FieldName = "YourDataField";

    //        // Configure other pivot grid settings
    //        pivotGridControl.OptionsView.ShowColumnTotals = false;
    //        pivotGridControl.OptionsView.ShowRowTotals = false;
    //    }
    //}

}
