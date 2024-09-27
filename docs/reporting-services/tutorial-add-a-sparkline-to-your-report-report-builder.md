---
title: "Tutorial: Add a sparkline to your report (Report Builder)"
description: Learn how to use the Report Builder to create a basic table with a sparkline chart in a Reporting Services paginated report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---

# Tutorial: Add a sparkline to your report (Report Builder)

In this tutorial in [!INCLUDE[ssRBnoversion_md](../includes/ssrbnoversion.md)], you create a basic table with a sparkline chart in a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated report.   
  
Sparklines and data bars are small, simple charts that convey large amounts information in a little space, often in tables and matrices in [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] reports. The following illustration shows a report similar to the one that you create in this tutorial.  

:::image type="content" source="../reporting-services/media/report-builder-sparkline-final.png" alt-text="Screenshot that shows the Report Builder sparkline.":::

Estimated time to complete this tutorial: 30 minutes.  
  
## Requirements  
For more information about requirements, see [Prerequisites for tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="CreateTable"></a>1. Create a report with a table  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, select **Table or Matrix Wizard**.  
  
4.  On the **Choose a dataset** page, select **Create a dataset** > **Next**. The **Choose a connection to a data source** page opens.  
  
    > [!NOTE]  
    > This tutorial doesn't need specific data; it just needs a connection to a SQL Server database. If you already have a data source connection listed under **Data Source Connections**, you can select it and go to step 10. For more information, see [Alternative ways to get a data connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
5.  Select **New**. The **Data Source Properties** dialog box opens.  
  
6.  In **Name**, enter **Product Sales**, a name for the data source.  
  
7.  In **Select a connection type**, verify that **Microsoft SQL Server** is selected.  
  
8.  In **Connection string**, enter the following text:  
  
    `Data Source\=<servername>`  
  
    The expression `<servername>`, for example Report001, specifies a computer on which an instance of the SQL Server Database Engine is installed. Because the report data isn't extracted from a SQL Server database, you need not include the name of a database. The default database on the specified server is used to parse the query.  
  
9. Select **Credentials**. Enter the credentials that you need to access the external data source.  
  
10. Select **OK**.
  
    You're back on the **Choose a connection to a data source** page.  
  
11. To verify that you can connect to the data source, select **Test Connection**.  
  
    The message "Connection created successfully" appears.  
  
12. Select **OK**.
  
13. Select **Next**.  
  
## <a name="Query"></a>2. Create a query and table layout in the Table Wizard for your report
In a report, you can use a shared dataset that has a predefined query, or you can create an embedded dataset for use only in your report. In this tutorial, you create an embedded dataset.  
  
> [!NOTE]  
> In this tutorial, the query contains the data values, so that it does not need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
### Create a query and table layout in the Table Wizard
  
1.  On the **Design a query** page, the relational query designer is open. For this tutorial, you use the text-based query designer.  
  
2.  Select **Edit As Text**. The text-based query designer displays a query pane and a results pane.  
  
3.  Paste the following [!INCLUDE[tsql](../includes/tsql-md.md)] query into the **Query** box.  
  
    ```  
    SELECT CAST('2015-01-04' AS date) as SalesDate, 'Accessories' as Subcategory,   
       'Carrying Case' as Product, CAST(16996.60 AS money) AS Sales, 68 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate, 'Accessories' as Subcategory,  
       'Carrying Case' as Product, CAST(1350.00 AS money) AS Sales, 18 as Quantity  
    UNION SELECT CAST('2015-01-10' AS date) as SalesDate, 'Accessories' as Subcategory,  
       'Carrying Case' as Product, CAST(1147.50 AS money) AS Sales, 17 as Quantity  
    UNION SELECT CAST('2015-01-04' AS date) as SalesDate, 'Accessories' as Subcategory,  
       'Budget Movie-Maker' as Product, CAST(1056.00 AS money) AS Sales, 44 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate,  'Accessories' as Subcategory,  
       'Slim Digital' as Product, CAST(1380.00 AS money) AS Sales, 18 as Quantity  
    UNION SELECT CAST('2015-01-05' AS date) as SalesDate,'Accessories' as Subcategory,    
       'Budget Movie-Maker' as Product, CAST(780.00 AS money) AS Sales, 26 as Quantity  
    UNION SELECT CAST('2015-01-07' AS date) as SalesDate, 'Accessories' as Subcategory,   
       'Budget Movie-Maker' as Product, CAST(3798.00 AS money) AS Sales, 9 as Quantity  
    UNION SELECT CAST('2015-01-08' AS date) as SalesDate, 'Camcorders' as Subcategory,   
       'Budget Movie-Maker' as Product, CAST(10400.00 AS money) AS Sales, 13 as Quantity  
    UNION SELECT CAST('2015-01-09' AS date) as SalesDate, 'Camcorders' as Subcategory,   
       'Budget Movie-Maker' as Product, CAST(3000.00 AS money) AS Sales, 60 as Quantity  
    UNION SELECT CAST('2015-01-10' AS date) as SalesDate,  'Digital' as Subcategory,   
       'Budget Movie-Maker' as Product, CAST(7234.50 AS money) AS Sales, 39 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate,  'Digital' as Subcategory,   
       'Carrying Case' as Product, CAST(10836.00 AS money) AS Sales, 84 as Quantity  
    UNION SELECT CAST('2015-01-07' AS date) as SalesDate,  'Digital' as Subcategory,   
       'Slim Digital' as Product, CAST(2550.00 AS money) AS Sales, 17 as Quantity  
    UNION SELECT CAST('2015-01-04' AS date) as SalesDate, 'Digital' as Subcategory,   
       'Slim Digital' as Product, CAST(8357.80 AS money) AS Sales, 44 as Quantity  
    UNION SELECT CAST('2015-01-08' AS date) as SalesDate, 'Digital SLR' as Subcategory,   
       'Slim Digital' as Product, CAST(18530.00 AS money) AS Sales, 34 as Quantity  
    UNION SELECT CAST('2015-01-06' AS date) as SalesDate, 'Digital SLR' as Subcategory,   
       'Slim Digital' as Product, CAST(26576.00 AS money) AS Sales, 88 as Quantity  
    ```  
  
4.  On the query designer toolbar, select Run  (**!**).  
  
    The query runs and displays the result set for the fields **SalesDate**, **Subcategory**, **Product**, **Sales**, and **Quantity**.  
  
5.  Select **Next**.  
  
6.  On the **Arrange fields** page, drag **Sales** to **Values**.  
  
    **Sales** is aggregated by the Sum function. The value is [Sum(Sales)].  
  
7.  Drag **Product** to **Row groups**.  
  
8.  Drag **SalesDate** to **Column groups**.  

    :::image type="content" source="../reporting-services/media/report-builder-sparkline-arrange-fields.png" alt-text="Screenshot that shows how to arrange fields.":::
  
9. Select **Next**.  
  
10. On the **Choose the layout** page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
    The wizard Preview pane displays a table with three rows. When you run the report, each row displays in the following way:  
  
    *  The first row appears once for the table to show column headings.  
  
    *  The second row repeats once for each product and displays the product name, total per day, and line total.  
  
    *  The third row appears once for the table to display the grand totals.  

    :::image type="content" source="../reporting-services/media/report-builder-sparkline-choose-layout.png" alt-text="Screenshot that shows how to choose a layout.":::
  
11. Select **Next**.  
  
12. Select **Finish**.  
  
14. The table is added to the design surface. The table has three columns and three rows.  
  
    Look in the Grouping pane. If you can't see the Grouping pane on the **View** menu, select **Grouping**. The Row Groups pane shows one row group: **Product**. The Column Groups pane shows one column group: **SalesDate**. Detail data is all the data that the dataset query retrieves.  

    :::image type="content" source="../reporting-services/media/report-builder-sparkline-grouping-pane.png" alt-text="Screenshot that shows the Report Builder grouping pane.":::
  
15. Select **Run** to preview the report.  

### <a name="FormatCurrency"></a>2a. Format data as currency  
By default, the summary data for the **Sales** field displays a general number. Format it to display the number as currency. Toggle **Placeholder Styles** to display formatted text boxes and placeholder text as sample values.  
  
1.  Select **Design** to switch to design view.  
  
2.  Select the cell in the second row (under the column headings row) in the **SalesDate** column. Hold down the Ctrl key and choose all cells that contain `[Sum(Sales)]`. 

    :::image type="content" source="../reporting-services/media/report-builder-select-sum-sales.png" alt-text="Screenshot that shows how to select sum sales.":::
  
3.  On the **Home** tab > **Number** group, select **Currency**. The cells change to show the formatted currency.  

    :::image type="content" source="../reporting-services/media/report-builder-placeholder-currency.png" alt-text="Screenshot that shows the sales replaced by placeholder currency values.":::
  
    If your regional setting is English (United States), the default sample text is [**$12,345.00**]. If you don't see an example currency value in the **Numbers** group, select **Placeholder Styles** > **Sample Values**.  

    :::image type="content" source="../reporting-services/media/report-builder-placeholder-value-button.png" alt-text="Screenshot that shows the Sample Values option selected.":::
   
### <a name="FormatDates"></a>2b. (Optional) Format data as dates  
By default, the **SalesDate** field displays both date and time information. You can format them to display only the date.  
  
1.  Select the cell that contains `[SalesDate]`.  
  
3.  On the **Home** tab, go to **Number** group > **Date**.  
  
    The cell displays the example date **[1/31/2000]**.
     
4.  Select **Run** to preview the report.  
  
The **SalesDate** values display in the default date format, and the summary values for **Sales** display as currency.   
  
## <a name="Sparkline"></a>3. Add a sparkline    
  
1.  Select **Design** to return to design view.  
  
2.  Select the Total column in your table.  
  
3.  Right-click. Point to **Insert Column**, and then select **Left**.  

    :::image type="content" source="../reporting-services/media/report-builder-add-column-left.png" alt-text="Screenshot that shows how to insert a left column.":::
  
4.  In the new column, right-click the cell in the `[Product]` row > **Insert** > **Sparkline**.  

    :::image type="content" source="../reporting-services/media/report-builder-insert-sparkline.png" alt-text="Screenshot that shows how to insert a sparkline.":::
  
5.  In the **Select Sparkline Type** dialog box, make sure the first sparkline in the **Column** row is selected, then choose **OK**.  
  
6.  Select the sparkline to show the Chart Data pane.  
  
7.  Select the plus (+) sign in the Values box, then choose **Sales**. 

    :::image type="content" source="../reporting-services/media/report-builder-sparkline-values.png" alt-text="Screenshot that shows how to add values to the Report Builder sparkline.":::
  
    The values in the **Sales** field are now the values for the sparkline.  
  
8. Select the plus (+) sign in the Category Groups box, then choose **SalesDate**.  
  
9. Select **Run** to preview your report.  
  
    The bars in the sparkline charts don't line up with each other. There are only four bars in the second row of data, so the bars are wider than the bars in the first row, which has six. You can't compare values for each product per day. They need to line up.  
  
    Also, for each row, the tallest bar is the height of the row. This visual is misleading, too, because the largest values for each row aren't equal: the largest value for Budget Movie-Maker is $10,400, but for Slim Digital it's $26,576 - more than twice as large. And yet the largest bars in those two rows are about the same height. All the sparklines need to use the same scale.  

    :::image type="content" source="../reporting-services/media/report-builder-sparkline-misaligned.png" alt-text="Screenshot that shows a Report Builder sparkline that is misaligned.":::
  
## <a name="AlignSparklines"></a>4. Align the sparklines vertically and horizontally  
Sparklines are hard to read when they don't all use the same measurements. Both the horizontal and vertical axes for each need to match the rest.  
   
1.  Select **Design** to return to design view.  
  
2.  Right-click the sparkline and select **Vertical Axis Properties**.  
  
3.  Check the **Align axes in** check box. Tablix1 is the only option in the list.  
  
     This sets the height of the bars in each sparkline relative to the others. 
  
4.  Select **OK**.  
  
5.  Right-click the sparkline and select **Horizontal Axis Properties**.  
  
6.  Check the **Align axes in** check box. Tablix1 is the only option in the list. 
  
    This sets the width of the bars in each sparkline relative to the others. If some sparklines have fewer bars than others, then those sparklines have blank spaces for the missing data.  
  
7.  Select **OK**.  
  
8.  Select **Run** to preview your report again.  
  
Now all the bars in each sparkline align with the bars in the other sparklines, and the heights are relative.  

:::image type="content" source="../reporting-services/media/report-builder-sparkline-aligned.png" alt-text="Screenshot that shows a Report Builder sparkline that is aligned.":::
  
## <a name="Width"></a>7. (Optional) Change column widths  
By default, each cell in a table contains a text box. A text box expands vertically to accommodate text when the page is rendered. In the rendered report, each row expands to the height of the tallest rendered text box in the row. The height of the row on the design surface has no effect on the height of the row in the rendered report.  
  
To reduce the amount of vertical space each row takes, expand the column width to accommodate the expected contents of the text boxes in the column on one line.  
  
### Change the width of columns  
  
1.  Select **Design** to return to design view.  
  
2.  Select the table so gray bars appear above the table and next to it. Those bars are the column and row handles
  
3.  Point to the line between column handles so that the cursor changes into a double arrow. Drag the **Product** column so that the product name displays on one line.  
  
4.  Select **Run** to preview your report and see if you made it wide enough.  
  
## <a name="Title"></a>8. (Optional) Add a report title  
A report title appears at the top of the report. You can place the report title in a report header or if the report doesn't use one, in a text box at the top of the report body. In this tutorial, you use the text box that is automatically placed at the top of the report body.  
  
The text can be further enhanced by applying different font styles, sizes, and colors to phrases and individual characters of the text. For more information, see [Format text in a text box in paginated reports (Report Builder)](../reporting-services/report-design/format-text-in-a-text-box-report-builder-and-ssrs.md).  
  
### Add a report title  
  
1.  On the design surface, select **Click to add title**.  
  
2.  Enter **Sales by Date**, and then select outside the text box.  
  
3.  Select the text box that contains **Product Sales**.  
  
4.  On the Home tab > **Font** group > for **Color**, select **Teal**.  
  
7.  Select **Bold**.  
  
8.  Select **OK**.
  
## <a name="Save"></a>9. Save the report  
Save the report to a report server or your computer. If you don't save the report to the report server, many [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features such as subreports aren't available.  
  
### Save the report on a report server  
  
1.  From the **Report Builder** button, select **Save As**.  
  
2.  Select **Recent Sites and Servers**.  
  
3.  Select or enter the name of the report server where you have permission to save reports.  
  
    The message "Connecting to report server" appears. When the connection is complete, you see the contents of the report folder that the report server administrator specified as the default location for reports.  
  
4.  In **Name**, replace the default name with **Product Sales**.  
  
5.  Select **Save**.  
  
The report is saved to the report server. The name of report server that you're connected to appears in the status bar at the bottom of the window.  
  
### Save the report on your computer  
  
1.  From the **Report Builder** button, select **Save As**.  
  
2.  Select **Desktop**, **My Documents**, or **My computer**, and browse to the folder where you want to save the report.  
  
3.  In **Name**, replace the default name with **Product Sales**.  
  
4.  Select **Save**.  
  
## Related content

- [Report Builder Tutorials](../reporting-services/report-builder-tutorials.md)
- [Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)

## Next step

> [!div class="nextstepaction"]
> [Sparklines and Data Bars](../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md)
