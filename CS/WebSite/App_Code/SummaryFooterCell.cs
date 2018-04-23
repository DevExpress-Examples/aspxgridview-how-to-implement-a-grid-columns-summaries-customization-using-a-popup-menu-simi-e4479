using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public class SummaryFooterCell: ITemplate
{
	public SummaryFooterCell()
	{
	}

    public void InstantiateIn(Control container) {
        GridViewFooterCellTemplateContainer gridContainer = container as GridViewFooterCellTemplateContainer;
        ASPxLabel label = new ASPxLabel();
        label.ID = "lbSummaryCell";
        gridContainer.Controls.Add(label);

        label.Width = Unit.Percentage(100);
        label.JSProperties["cpColumnField"] = (gridContainer.Column as GridViewDataColumn).FieldName;
        label.ClientSideEvents.Init = "SummaryCell_Init";
        label.ClientSideEvents.Click = "SummaryCell_Click";
    }
}