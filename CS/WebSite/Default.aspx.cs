using System;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack)
            Grid.StartEdit(0);
    }

    protected void Grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        if (e.Column.FieldName == "CityID") {
            var combo = (ASPxComboBox)e.Editor;
            combo.Callback += new CallbackEventHandlerBase(combo_Callback);

            var grid = e.Column.Grid;
            if (!combo.IsCallback) {
                var countryID = -1;
                if (!grid.IsNewRowEditing)
                    countryID = (int)grid.GetRowValues(e.VisibleIndex, "CountryID");
                FillCitiesComboBox(combo, countryID);
            }
        }
    }

    private void combo_Callback(object sender, CallbackEventArgsBase e) {
        var countryID = -1;
        Int32.TryParse(e.Parameter, out countryID);
        FillCitiesComboBox(sender as ASPxComboBox, countryID);
    }

    protected void FillCitiesComboBox(ASPxComboBox combo, int countryID) {
        combo.DataSourceID = "Cities";
        Cities.SelectParameters["CountryID"].DefaultValue = countryID.ToString();
        combo.DataBindItems();

        combo.Items.Insert(0, new ListEditItem("", null)); // Null Item
    }
}