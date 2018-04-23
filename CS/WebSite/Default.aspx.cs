using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using System.Collections;

public partial class _Default : System.Web.UI.Page {

    Dictionary<string, SummaryItemType> gridSummaries;

    protected void gvProducts_Init(object sender, EventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;

        gridSummaries = Session["GridSummaries"] as Dictionary<string, SummaryItemType>;
        if (gridSummaries == null) {
            gridSummaries = new Dictionary<string, SummaryItemType>();
            Session["GridSummaries"] = gridSummaries;
        }

        foreach (GridViewColumn col in gridView.Columns) {
            GridViewDataColumn dataCol = col as GridViewDataColumn;
            if (dataCol != null && dataCol.FieldName != "ProductName") {
                dataCol.FooterTemplate = new SummaryFooterCell();
            }
        }
    }

    protected void gvProducts_DataBinding(object sender, EventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;

        gridView.TotalSummary.Clear();
        foreach (string fieldName in gridSummaries.Keys) {
            SummaryItemType type = gridSummaries[fieldName];
            gridView.TotalSummary.Add(type, fieldName);
        }
    }

    protected void gvProducts_DataBound(object sender, EventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;

        Hashtable summaries = new Hashtable();

        foreach (ASPxSummaryItem item in gridView.TotalSummary) {
            summaries[item.FieldName] = item.GetTotalFooterDisplayText(gridView.Columns[item.FieldName], gridView.GetTotalSummaryValue(item));
        }

        gridView.JSProperties["cpSummaries"] = summaries;
    }  
       
    protected void gvProducts_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;

        string[] parameters = e.Parameters.Split('|');
        string operation = parameters[0];
        string fieldName = parameters[1];
        switch (operation) {
            case "remove":
                if (gridSummaries.ContainsKey(fieldName)) {
                    gridSummaries.Remove(fieldName);
                }                
                break;
            case "sum":
                gridSummaries[fieldName] = SummaryItemType.Sum;
                break;
            case "min":
                gridSummaries[fieldName] = SummaryItemType.Min;
                break;
            case "max":
                gridSummaries[fieldName] = SummaryItemType.Max;
                break;
            case "count":
                gridSummaries[fieldName] = SummaryItemType.Count;
                break;
            case "average":
                gridSummaries[fieldName] = SummaryItemType.Average;
                break;
        }
        gridView.DataBind();
    }    
}