Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Data
Imports System.Collections

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private gridSummaries As Dictionary(Of String, SummaryItemType)

	Protected Sub gvProducts_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)

		gridSummaries = TryCast(Session("GridSummaries"), Dictionary(Of String, SummaryItemType))
		If gridSummaries Is Nothing Then
			gridSummaries = New Dictionary(Of String, SummaryItemType)()
			Session("GridSummaries") = gridSummaries
		End If

		For Each col As GridViewColumn In gridView.Columns
			Dim dataCol As GridViewDataColumn = TryCast(col, GridViewDataColumn)
			If dataCol IsNot Nothing AndAlso dataCol.FieldName <> "ProductName" Then
				dataCol.FooterTemplate = New SummaryFooterCell()
			End If
		Next col
	End Sub

	Protected Sub gvProducts_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)

		gridView.TotalSummary.Clear()
		For Each fieldName As String In gridSummaries.Keys
			Dim type As SummaryItemType = gridSummaries(fieldName)
			gridView.TotalSummary.Add(type, fieldName)
		Next fieldName
	End Sub

	Protected Sub gvProducts_DataBound(ByVal sender As Object, ByVal e As EventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)

		Dim summaries As New Hashtable()

		For Each item As ASPxSummaryItem In gridView.TotalSummary
			summaries(item.FieldName) = item.GetTotalFooterDisplayText(gridView.Columns(item.FieldName), gridView.GetTotalSummaryValue(item))
		Next item

		gridView.JSProperties("cpSummaries") = summaries
	End Sub

	Protected Sub gvProducts_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)

		Dim parameters() As String = e.Parameters.Split("|"c)
		Dim operation As String = parameters(0)
		Dim fieldName As String = parameters(1)
		Select Case operation
			Case "remove"
				If gridSummaries.ContainsKey(fieldName) Then
					gridSummaries.Remove(fieldName)
				End If
			Case "sum"
				gridSummaries(fieldName) = SummaryItemType.Sum
			Case "min"
				gridSummaries(fieldName) = SummaryItemType.Min
			Case "max"
				gridSummaries(fieldName) = SummaryItemType.Max
			Case "count"
				gridSummaries(fieldName) = SummaryItemType.Count
			Case "average"
				gridSummaries(fieldName) = SummaryItemType.Average
		End Select
		gridView.DataBind()
	End Sub
End Class