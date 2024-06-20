---
title: "Tutorial: Create a basic table report (Report Builder)"
description: Create a basic table report from sample sales data by using the SSRS Report Builder, and organize, format, save, and export your report.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/20/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: concept-article
ms.custom: updatefrequency5

#customer intent: As a developer, I want to create a table report with Report Builder in SSRS, so I can organize, format, save, and export my SQL Server data.
---

# Tutorial: Create a basic table report (Report Builder)

You can use the Report Builder in SQL Server Reporting Services (SSRS) to create table reports for your SQL data. This tutorial shows you how to create a basic table report from sample sales data.

In this tutorial, you:

> [!div class="checklist"]
> * Follow wizard steps to create a table report
> * Identify an embedded source for the table data
> * Run a query to get the data values
> * Organize and format the table data, and add totals
> * Design and save the report
> * Review the exported report in Microsoft Excel

The following illustration shows the table report you create in this tutorial:

:::image type="content" source="../reporting-services/media/ssrs-tutorial-basic-table-report.png" border="false" alt-text="Screenshot of the sample table report prepared in this tutorial that shows product sales data." lightbox="../reporting-services/media/ssrs-tutorial-basic-table-report.png":::

The estimated time to complete this tutorial is 20 minutes.  
  
## Prerequisites  

For more information about requirements, see [Prerequisites for tutorials (Report Builder)](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## Start the wizard to create a report  

You can create a table report by using the **Table or Matrix** wizard. The wizard has two design modes: _Report_ and _Shared dataset_. In Report design mode, you specify data in the **Report Data** pane and configure the report layout on the design surface. In Shared dataset design mode, you create dataset queries to share with others. In this tutorial, you use Report design mode.

Start the wizard and create a basic table report:

1. [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
   The **New Report or Dataset** dialog opens. If the dialog doesn't open, select **File** > **New**.  
  
1. Select the **New Report** tab, and select **Table or Matrix Wizard** on the right pane:

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-open-wizard.png" border="false" alt-text="Screenshot that shows how to select New Report in the Table or Matrix Wizard.":::
  
### Specify a data connection 

A data connection contains the information to connect to an external data source such as a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database. Usually, you get the connection information and the type of credentials to use from the data source owner. To specify a data connection, you can use a shared data source from the report server or create an embedded data source used only in this report.  
  
In this tutorial, you use an embedded data source. To learn more about using a shared data source, see [Alternative ways to get a data connection (Report Builder)](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
Create an embedded data source for the data connection:
  
1. On the **Choose a dataset** page, select the **Create a dataset** option, and then select **Next**. 
  
1. On the **Choose a connection to a data source** page, select **New**.
  
1. On the **Data Source Properties** dialog, set the following properties on the **General** tab:

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-data-source-general.png" border="false" alt-text="Screenshot that shows how to configure General options on the Data Source Properties dialog.":::

   1. Set the **Name** property of the data source to _Product\_Sales_.  
  
   1. For the **Select a connection type** property, confirm **Microsoft SQL Server** is selected.  
  
   1. For the **Connection string** property, enter the following value, where `\<servername>` is the name of an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
      ```sql
      Data Source=<servername>  
      ```

      Because you use a query that contains the data instead of retrieving the data from a database, the connection string doesn't include the database name. For more information, see [Prerequisites for tutorials (Report Builder)](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
1. Switch to the **Credentials** tab, and select your preferred access method for the data source. Enter credentials, as needed:

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-data-source-credentials.png" border="false" alt-text="Screenshot that shows how to configure Credentials options on the Data Source Properties dialog.":::
  
1. On the **General** tab, select **Test Connection** to verify you can connect to the data source.

   You should see a popup message: "Connection created successfully." Select **OK** to clear the popup message.

1. To complete the data source setup, select **OK**.

1. To continue in the wizard, select **Next**.
  
### Create a query

In a report, you can use a shared dataset that has a predefined query, or you can create an embedded dataset for use only in this specific report. In this tutorial, you create an embedded dataset.  
  
> [!NOTE]  
> In the tutorial example, the query contains the data values, so it doesn't need an external data source. This approach makes the query quite long, but it's useful for learning purposes. In a standard business environment, the query doesn't contain the data values.

Create a query by following these steps: 
  
1. On the **Design a query** page, the relational query designer is open. For this tutorial, you use the text-based query designer:

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-design-query.png" border="false" alt-text="Screenshot that shows the Design a query page in the Table or Matrix Wizard.":::

1. Select **Edit As Text**. The text-based query designer displays a query pane and a results pane.
  
1. Paste the following [!INCLUDE[tsql](../includes/tsql-md.md)] query into the top field:
  
   ```sql
   SELECT CAST('2009-01-05' AS date) as SalesDate, 'Accessories' as Subcategory,   
      'Carrying Case' as Product, CAST(9924.60 AS money) AS Sales, 68 as Quantity  
   UNION SELECT CAST('2009-01-06' AS date) as SalesDate, 'Accessories' as Subcategory,  
      'Tripod' as Product, CAST(1350.00 AS money) AS Sales, 18 as Quantity  
   UNION SELECT CAST('2009-01-11' AS date) as SalesDate, 'Accessories' as Subcategory,  
      'Lens Adapter' as Product, CAST(1147.50 AS money) AS Sales, 17 as Quantity  
   UNION SELECT CAST('2009-01-05' AS date) as SalesDate, 'Accessories' as Subcategory,  
      'Mini Battery Charger' as Product, CAST(1056.00 AS money) AS Sales, 44 as Quantity  
   UNION SELECT CAST('2009-01-06' AS date) as SalesDate,  'Accessories' as Subcategory,  
      'Telephoto Conversion Lens' as Product, CAST(1380.00 AS money) AS Sales, 18 as Quantity  
   UNION SELECT CAST('2009-01-06' AS date) as SalesDate,'Accessories' as Subcategory,   
      'USB Cable' as Product, CAST(780.00 AS money) AS Sales, 26 as Quantity  
   UNION SELECT CAST('2009-01-08' AS date) as SalesDate, 'Accessories' as Subcategory,   
      'Budget Movie-Maker' as Product, CAST(3798.00 AS money) AS Sales, 9 as Quantity  
   UNION SELECT CAST('2009-01-09' AS date) as SalesDate, 'Camcorders' as Subcategory,   
      'Business Videographer' as Product, CAST(10400.00 AS money) AS Sales, 13 as Quantity  
   UNION SELECT CAST('2009-01-10' AS date) as SalesDate, 'Camcorders' as Subcategory,   
      'Social Videographer' as Product, CAST(3000.00 AS money) AS Sales, 60 as Quantity  
   UNION SELECT CAST('2009-01-11' AS date) as SalesDate,  'Digital' as Subcategory,   
      'Advanced Digital' as Product, CAST(7234.50 AS money) AS Sales, 39 as Quantity  
   UNION SELECT CAST('2009-01-07' AS date) as SalesDate,  'Digital' as Subcategory,   
      'Compact Digital' as Product, CAST(10836.00 AS money) AS Sales, 84 as Quantity  
   UNION SELECT CAST('2009-01-08' AS date) as SalesDate,  'Digital' as Subcategory,   
      'Consumer Digital' as Product, CAST(2550.00 AS money) AS Sales, 17 as Quantity  
   UNION SELECT CAST('2009-01-05' AS date) as SalesDate, 'Digital' as Subcategory,   
      'Slim Digital' as Product, CAST(8357.80 AS money) AS Sales, 44 as Quantity  
   UNION SELECT CAST('2009-01-09' AS date) as SalesDate, 'Digital SLR' as Subcategory,   
      'SLR Camera 35mm' as Product, CAST(18530.00 AS money) AS Sales, 34 as Quantity  
   UNION SELECT CAST('2009-01-07' AS date) as SalesDate, 'Digital SLR' as Subcategory,   
      'SLR Camera' as Product, CAST(26576.00 AS money) AS Sales, 88 as Quantity
   ```
   
1. On the query designer toolbar, select **Run** (**!**):

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-run-query.png" border="false" alt-text="Screenshot that shows how to Run the query to check the result set.":::
  
   The query runs and displays the result set for the fields SalesDate, Subcategory, Product, Sales, and Quantity.  
  
   In the result set, the column headings are based on the names in the query. In the dataset, the column headings become the field names, and are saved in the report. After you complete the wizard, you can use the **Report Data** pane to view the collection of dataset fields.  
  
1. To continue in the wizard, select **Next**.

### Organize data into groups

When you select data fields to form groups, you design a table that has rows and columns that display detail data and aggregated data. In the following procedure, the first two steps specify the data values to display in the table and the last two steps organize the values. 
  
Organize table data into groups on the **Arrange fields** page: 
  
1. From the **Available fields** box, drag the Product field and then the Quantity field to the **Values** box. Position the Quantity field after the Product field.  
  
   The Sum function automatically aggregates the Quantity data, which is the default aggregate for numeric fields. The value is [Sum(Quantity)].  
  
   > [!TIP]
   > You can select the dropdown arrow next to the [Sum(Quantity)] aggregate to view the other aggregate functions. For this exercise, leave the aggregate function set to **Sum**.  
  
1. Drag the Sales field to the **Values** box and position it after the [Sum(Quantity)] aggregate.  
  
   The Sum function aggregates the Sales data. The value is [Sum(Sales)]. 
  
1. Drag the SalesDate field and then the Subcategory field to the **Row groups** box. Position the Subcategory field after the SalesDate field.

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-arrange-fields.png" border="false" alt-text="Screenshot that shows how to arrange the table data fields.":::

1. To continue in the wizard, select **Next**. 

### Add subtotal and total rows

After you create groups, you can add and format rows on which to display aggregate values for the fields. You can choose whether to show all the data or to allow the user to expand and collapse grouped data interactively.  
  
Follow these steps to add subtotals and totals for the table data:
  
1. On the **Choose the layout** page, under **Options**, configure the following options:

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-choose-layout.png" border="false" alt-text="Screenshot that shows how to choose the layout options for the table.":::

   1. Select the **Show subtotals and grand totals** option.  
  
   1. Select the **Blocked, subtotal below** option.

   1. Clear the **Expand/collapse groups** option.
   
      In this tutorial, the report you create doesn't use the drilldown feature that lets users expand a parent group hierarchy and display child group rows and detail rows. 
  
1. Review the table layout in the **Preview** pane. You should see five rows that demonstrate the table layout when you run the report:

   - The first row repeats once for the table to show the column headings.

   - The second row repeats once for each line item in the sales order to show the product name, order quantity, and line total.

   - The third row repeats once for each sales order category to show subtotals per category.

   - The fourth row repeats once for each order date to show the subtotals per day.

   - The fifth row repeats once for the table to show the grand totals.
  
1. Select **Next** to preview the table, and then select **Finish**.  
  
   Report Builder adds your table to the design surface. The table has five columns and five rows. The Row Groups pane shows three row groups: SalesDate, Subcategory, and Details. Detail data is all the data that the dataset query retrieves.  

   :::image type="content" source="../reporting-services/media/ssrs-tutorial-design-surface-new-table.png" border="false" alt-text="Screenshot of the new table report open on the design surface in Report Builder." lightbox="../reporting-services/media/ssrs-tutorial-design-surface-new-table.png":::

## Format data as currency

By default, the summary data for the Sales field displays a general number. Format it to display the number as currency.   
  
Format a currency field by following these steps:
  
1. To see formatted text boxes and placeholder text as sample values in Design View, on the **Home** tab, in the **Number** group, select the arrow next to the **Placeholder Styles** icon > **Sample Values**.  
  
1. Select the cell in the second row (under the column headings row) in the Sales column and drag down to select all cells that contain `[Sum(Sales)]`.  
  
1. On the **Home** tab, in the **Number** group, select the **Currency** button. The cells change to show the formatted currency.  
  
   If your regional setting is English (United States), the default sample text is [**$12,345.00**]. If you don't see an example currency value, on the **Home** tab, in the **Number** group, select the arrow next to the **Placeholder Styles** icon > **Sample Values**.  
  
1. Select **Run** to preview your report.  
  
The summary values for Sales display as currency.  
  
## Format data as date

By default, the SalesDate field displays both date and time. You can format them to display only the date.  
  
Format a date field as the default format:
  
1. Select **Design** to return to design view.  
  
1. Select the cell that contains `[SalesDate]`.  
  
1. On the Ribbon, on the **Home** tab, in the **Number** group, select the arrow and choose **Date**.  
  
   The cell displays the example date **[1/31/2000]**. If you don't see an example date, on the **Home** tab, in the **Number** group, select the arrow next to the **Placeholder Styles** icon > **Sample Values**.  
  
1. Select **Run** to preview the report.  
  
The SalesDate values display in the default date format.  
  
### Change the date format to a custom format  
  
You can also change the date format for the field to a custom format:

1. Select **Design** to return to design view.  
  
1. Select the cell that contains `[SalesDate]`.  
  
1. On the **Home** tab, in the **Number** group, select the arrow in the lower-right corner to open the dialog.  
  
   The **Text Box Properties** dialog opens.  
  
1. In the Category pane, verify that **Date** is selected.  
  
1. In the **Type** pane, select **January 31, 2000**.  
  
1. Select **OK**.
  
   The cell displays the example date **[January 31, 2000]**.  
  
1. Select **Run** to preview your report.  
  
The SalesDate value displays the name of the month instead of the number for the month.  
  
## Change column widths 

By default, each cell in a table contains a text box. A text box expands vertically to accommodate text when the page is rendered. In the rendered report, each row expands to the height of the tallest rendered text box in the row. The height of the row on the design surface has no effect on the height of the row in the rendered report.  
  
To reduce the amount of vertical space each row takes, expand the column width to accommodate the expected contents of the text boxes in the column on one line.  
  
Follow these steps to change the width of the table columns:
  
1. Select **Design** to return to design view.  
  
1. Select the table so that column and row handles appear above the table and next it  
  
   The gray bars along the top and side of the table are the column and row handles.  
  
1. Point to the line between column handles so that the cursor changes into a double arrow. Drag the columns to the width you want. For example, expand the column for Product so that the product name displays on one line.  
  
1. Select **Run** to preview your report.  
  
## Add a report title

A report title appears at the top of the report. You can place the report title in a report header or if the report doesn't use one, in a text box at the top of the report body. In this tutorial, you use the text box that is automatically placed at the top of the report body.  
  
The text can be further enhanced by applying different font styles, sizes, and colors to phrases and individual characters of the text. For more information, see [Format text in a text box in paginated reports (Report Builder)](../reporting-services/report-design/format-text-in-a-text-box-report-builder-and-ssrs.md).  
  
Add a report title:
  
1. On the design surface, select **Click to add title**.  
  
1. Enter **Product Sales**, and then select outside the text box.  
  
1. Right-click the text box that contains **Product Sales** and select **Text Box Properties**.  
  
1. In the **Text Box Properties** dialog, select **Font**.  
  
1. In the **Size** list, select **18pt**.  
  
1. In the **Color** list, select **Cornflower Blue**.  
  
1. Select **Bold**.  
  
1. Select **OK**.
  
## Save the report

Save the report to a report server or your computer. If you don't save the report to the report server, many [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features such as subreports aren't available.  
  
Follow these steps to save the report on a report server:
  
1. Select **File** > **Save As**.  
  
1. Select **Recent Sites and Servers**.  
  
1. Select or enter the name of the report server where you have permission to save reports.  
  
   The message "Connecting to report server" appears. When the connection is complete, you see the contents of the report folder that the report server administrator specified as the default location for reports.  
  
1. In **Name**, replace **Untitled** with **Product_Sales**.  
  
1. Select **Save**.  
  
The report is saved to the report server. The status bar at the bottom of the window shows the name of the report server that you're connected to.  
  
### Save the report on your computer

You can also save the report on your computer:
  
1. Select **File** > **Save As**.  
  
1. Select **Desktop**, **My Documents**, or **My computer**, and browse to the folder where you want to save the report.  
  
1. In **Name**, replace **Untitled** with **Product Sales**.  
  
1. Select **Save**.  
  
## Export the report

Reports can be exported to different formats such Microsoft Excel and comma separated value (CSV) files. For more information, see [Export paginated reports (Report Builder)](../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md).  
  
In this tutorial, you export the report to Excel and set a property on the report to provide a custom name for the workbook tab.  
  
Specify the workbook tab name:
  
1. Select **Design** to return to design view.  
  
1. Select anywhere on the design surface, outside the report.  
  
1. In the Properties pane, locate the InitialPageName property and enter **Product Sales Excel**.  
  
   > [!NOTE]  
   > If the Properties pane isn't visible, on the **View** tab, select **Properties**.  
   > If you don't see a property in the Properties pane, try selecting the **Alphabetical** button at the top of the pane to order all the properties alphabetically.   
  
Export a report to Excel by following these steps:
  
1. Select **Run** to preview the report.  
  
1. On the ribbon, select **Export** > **Excel**.
  
1. In the **Save As** dialog, browse to where you want to save the file.  
  
1. In the **File name** box, enter **Product_Sales_Excel**.  
  
1. Verify that the file type is **Excel (\*.xlsx)**.  
  
1. Select **Save**.  
  
### View the report in Excel

Now you're ready to view your report in Excel:
  
1. Open the folder where you save the workbook and double-click **Product_Sales_Excel.xlsx**.  
  
1. Verify that the name of the workbook tab is **Product Sales Excel**.  

## Related content

- [Tables, matrices, and lists in paginated reports (Report Builder)](../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
- [Report Builder tutorials](../reporting-services/report-builder-tutorials.md)  
- [Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)
