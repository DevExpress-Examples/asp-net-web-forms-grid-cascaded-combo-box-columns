<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>How to implement cascaded combobox columns in ASPxGridView without using templates
    </title>
    <style type="text/css">
        .grid {
            margin: 0 auto;
        }
    </style>
    <script type="text/javascript">
        function CountriesCombo_SelectedIndexChanged(s, e) {
            grid.GetEditor("CityID").PerformCallback(s.GetValue());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="false" DataSourceID="Customers"
            Width="800" CssClass="grid" KeyFieldName="CustomerID" ClientInstanceName="grid"
            OnCellEditorInitialize="Grid_CellEditorInitialize">
            <Columns>
                <dx:GridViewCommandColumn Width="100" ShowNewButton="true" ShowEditButton="true"/>
                <dx:GridViewDataColumn FieldName="CustomerID" Visible="false" SortOrder="Descending" />
                <dx:GridViewDataTextColumn FieldName="CustomerName" Width="200">
                    <PropertiesTextEdit>
                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn Caption="Country" FieldName="CountryID" Width="200">
                    <PropertiesComboBox DataSourceID="Countries" ValueField="CountryID" ValueType="System.Int32"
                        TextField="CountryName" EnableSynchronization="False" IncrementalFilteringMode="StartsWith">
                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                        <ClientSideEvents SelectedIndexChanged="CountriesCombo_SelectedIndexChanged" />
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn Caption="City" FieldName="CityID" Width="200">
                    <PropertiesComboBox DataSourceID="AllCities" ValueField="CityID" ValueType="System.Int32"
                        TextField="CityName" EnableSynchronization="False" IncrementalFilteringMode="StartsWith">
                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <Settings ShowGroupPanel="true" />
            <SettingsEditing Mode="Inline" />
        </dx:ASPxGridView>
        <asp:ObjectDataSource ID="Customers" runat="server" TypeName="DataProvider" SelectMethod="GetCustomers"
            InsertMethod="InsertCustomer" UpdateMethod="UpdateCustomer">
            <InsertParameters>
                <asp:Parameter Name="CustomerName" Type="String" />
                <asp:Parameter Name="CountryID" Type="Int32" />
                <asp:Parameter Name="CityID" Type="Int32" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="CustomerID" Type="Int32" />
                <asp:Parameter Name="CustomerName" Type="String" />
                <asp:Parameter Name="CountryID" Type="Int32" />
                <asp:Parameter Name="CityID" Type="Int32" />
            </UpdateParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="Countries" runat="server" TypeName="DataProvider" SelectMethod="GetCountries" />
        <asp:ObjectDataSource ID="AllCities" runat="server" TypeName="DataProvider" SelectMethod="GetCities" />
        <asp:ObjectDataSource ID="Cities" runat="server" TypeName="DataProvider" SelectMethod="GetCities">
            <SelectParameters>
                <asp:Parameter Name="CountryID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
