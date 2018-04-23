Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors

Public Class SummaryFooterCell
	Implements ITemplate
	Public Sub New()
	End Sub

	Public Sub InstantiateIn(ByVal container As Control) Implements ITemplate.InstantiateIn
		Dim gridContainer As GridViewFooterCellTemplateContainer = TryCast(container, GridViewFooterCellTemplateContainer)
		Dim label As New ASPxLabel()
		label.ID = "lbSummaryCell"
		gridContainer.Controls.Add(label)

		label.Width = Unit.Percentage(100)
		label.JSProperties("cpColumnField") = (TryCast(gridContainer.Column, GridViewDataColumn)).FieldName
		label.ClientSideEvents.Init = "SummaryCell_Init"
		label.ClientSideEvents.Click = "SummaryCell_Click"
	End Sub
End Class