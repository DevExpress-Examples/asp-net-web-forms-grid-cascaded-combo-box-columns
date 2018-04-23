Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If (Not IsPostBack) Then
			Grid.StartEdit(0)
		End If
	End Sub

	Protected Sub Grid_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		If e.Column.FieldName = "CityID" Then
			Dim combo = CType(e.Editor, ASPxComboBox)
			AddHandler combo.Callback, AddressOf combo_Callback

			Dim grid = e.Column.Grid
			If (Not combo.IsCallback) Then
				Dim countryID = -1
				If (Not grid.IsNewRowEditing) Then
					countryID = CInt(Fix(grid.GetRowValues(e.VisibleIndex, "CountryID")))
				End If
				FillCitiesComboBox(combo, countryID)
			End If
		End If
	End Sub

	Private Sub combo_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase)
		Dim countryID = -1
		Int32.TryParse(e.Parameter, countryID)
		FillCitiesComboBox(TryCast(sender, ASPxComboBox), countryID)
	End Sub

	Protected Sub FillCitiesComboBox(ByVal combo As ASPxComboBox, ByVal countryID As Integer)
		combo.DataSourceID = "Cities"
		Cities.SelectParameters("CountryID").DefaultValue = countryID.ToString()
		combo.DataBindItems()

		combo.Items.Insert(0, New ListEditItem("", Nothing)) ' Null Item
	End Sub
End Class