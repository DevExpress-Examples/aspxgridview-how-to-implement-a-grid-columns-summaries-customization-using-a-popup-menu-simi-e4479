<!-- default file list -->
*Files to look at*:

* [SummaryFooterCell.cs](./CS/WebSite/App_Code/SummaryFooterCell.cs) (VB: [SummaryFooterCell.vb](./VB/WebSite/App_Code/SummaryFooterCell.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# ASPxGridView - How to implement a grid column's summaries customization using a popup menu similar to WinForms Grid


<p>Use the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewGridViewColumn_FooterTemplatetopic"><u>GridViewColumn.FooterTemplate</u></a> to place the ASPxLabel control to footer templates. Then, handle the label's client-side Click event to invoke a popup menu when the label is clicked. To pass the clicked label column's field name, use the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxEditorsASPxEditBase_JSPropertiestopic"><u>JSProperties</u></a> collection. When the menu item is clicked, send a callback to the server to add the column's summary. Pass summary items' values to the client using the grid's JSProperties collection and then assign them to the corresponding labels using their client-side Init event handlers.</p>

<br/>


