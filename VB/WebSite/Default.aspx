<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.2, Version=11.2.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script type="text/javascript">
		function SummaryCell_Init(s, e) {
			var columnField = s.cpColumnField;
			var value = grid.cpSummaries[columnField];
			if (value === undefined) {
				s.SetText("(none)");                
			}
			else {
				s.SetText(value);
			}
		}

		var clickedColumnField;

		function SummaryCell_Click(s, e) {
			clickedColumnField = s.cpColumnField;
			popupMenu.ShowAtElement(s.GetMainElement());
		}

		function popupMenu_ItemClick(s, e) {
			switch (e.item.name) {
				case "add":
					return;
				default:
					grid.PerformCallback(e.item.name + '|' + clickedColumnField);
					break;
			}
			s.Hide();
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		(Left-click footer cells to invoke a popup menu)
		<dx:ASPxGridView ID="gvProducts" runat="server" AutoGenerateColumns="False" DataSourceID="dsProducts"
			KeyFieldName="ProductID" ClientInstanceName="grid" OnCustomCallback="gvProducts_CustomCallback"
			OnDataBound="gvProducts_DataBound" ondatabinding="gvProducts_DataBinding" 
			oninit="gvProducts_Init">
			<Columns>
				<dx:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="0"
					Visible="false">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="1">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="3">
					<PropertiesTextEdit DisplayFormatString="c2">
					</PropertiesTextEdit>
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="UnitsInStock" VisibleIndex="4">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn FieldName="UnitsOnOrder" VisibleIndex="5">
				</dx:GridViewDataTextColumn>
			</Columns>
			<Settings ShowFooter="True" />
		</dx:ASPxGridView>
		<asp:AccessDataSource ID="dsProducts" runat="server" DataFile="~/App_Data/nwind.mdb"
			SelectCommand="SELECT [ProductID], [ProductName], [UnitPrice], [UnitsInStock], [UnitsOnOrder] FROM [Products]">
		</asp:AccessDataSource>
		<dx:ASPxPopupMenu ID="pmAddSummary" runat="server" PopupAction="None" ClientInstanceName="popupMenu">
			<Items>
				<dx:MenuItem Name="add" Text="Set Total Summary">
					<Items>
						<dx:MenuItem Name="sum" Text="Sum">
						</dx:MenuItem>
						<dx:MenuItem Name="min" Text="Min">
						</dx:MenuItem>
						<dx:MenuItem Name="max" Text="Max">
						</dx:MenuItem>
						<dx:MenuItem Name="count" Text="Count">
						</dx:MenuItem>
						<dx:MenuItem Name="average" Text="Average">
						</dx:MenuItem>
					</Items>
				</dx:MenuItem>
				<dx:MenuItem Name="remove" Text="Remove Total Summary">
				</dx:MenuItem>
			</Items>
			<ClientSideEvents ItemClick="popupMenu_ItemClick" />
		</dx:ASPxPopupMenu>
	</div>
	</form>
</body>
</html>