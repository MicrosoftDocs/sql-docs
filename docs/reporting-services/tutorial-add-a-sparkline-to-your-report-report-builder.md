---
title: "Tutorial: Add a Sparkline to Your Report (Report Builder) | Microsoft Docs"
ms.date: 05/30/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: 18c90a36-48bf-4805-a960-2d1e8f00c2dc
author: maggiesMSFT
ms.author: maggies
---

# Tutorial: Add a Sparkline to Your Report (Report Builder)

In this tutorial in [!INCLUDE[ssRBnoversion_md](../includes/ssrbnoversion.md)], you create a basic table with a sparkline chart in a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated report.   
  
Sparklines and data bars are small, simple charts that convey a lot of information in a little space, often in tables and matrices in [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] reports. The following illustration shows a report similar to the one that you will create.  
  
![report-builder-sparkline-final](../reporting-services/media/report-builder-sparkline-final.png)  
     
Estimated time to complete this tutorial: 30 minutes.  
  
## Requirements  
For more information about requirements, see [Prerequisites for Tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="CreateTable"></a>1. Create a Report with a Table  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, click **Table or Matrix Wizard**.  
  
4.  On the **Choose a dataset** page, select **Create a dataset** > **Next**. The **Choose a connection to a data source** page opens.  
  
    > [!NOTE]  
    > This tutorial doesn't need specific data; it just needs a connection to a SQL Server database. If you already have a data source connection listed under **Data Source Connections**, you can select it and go to step 10. For more information, see [Alternative Ways to Get a Data Connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
5.  Click **New**. The **Data Source Properties** dialog box opens.  
  
6.  In **Name**, type **Product Sales**, a name for the data source.  
  
7.  In **Select a connection type**, verify that **Microsoft SQL Server** is selected.  
  
8.  In **Connection string**, type the following text:  
  
    `Data Source\=<servername>`  
  
    The expression `<servername>`, for example Report001, specifies a computer on which an instance of the SQL Server Database Engine is installed. Because the report data is not extracted from a SQL Server database, you need not include the name of a database. The default database on the specified server is used to parse the query.  
  
9. Click **Credentials**. Enter the credentials that you need to access the external data source.  
  
10. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
    You are back on the **Choose a connection to a data source** page.  
  
11. To verify that you can connect to the data source, click **Test Connection**.  
  
    The message "Connection created successfully" appears.  
  
12. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
13. Click **Next**.  
  
## <a name="Query"></a>2. Create a Query and Table Layout in the Table Wizard  
In a report, you can use a shared dataset that has a predefined query, or you can create an embedded dataset for use only in your report. In this tutorial, you will create an embedded dataset.  
  
> [!NOTE]  
> In this tutorial, the query contains the data values, so that it does not need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
### To create a query and table layout in the Table Wizard 
  
1.  On the **Design a query** page, the relational query designer is open. For this tutorial, you will use the text-based query designer.  
  
2.  Click **Edit As Text**. The text-based query designer displays a query pane and a results pane.  
  
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
  
4.  On the query designer toolbar, click Run  (**!**).  
  
    The query runs and displays the result set for the fields **SalesDate**, **Subcategory**, **Product**, **Sales**, and **Quantity**.  
  
5.  Click **Next**.  
  
6.  On the **Arrange fields** page, drag **Sales** to **Values**.  
  
    **Sales** is aggregated by the Sum function. The value is [Sum(Sales)].  
  
7.  Drag **Product** to **Row groups**.  
  
8.  Drag **SalesDate** to **Column groups**.  

    ![report-builder-sparkline-arrange-fields](../reporting-services/media/report-builder-sparkline-arrange-fields.png)
  
9. Click **Next**.  
  
10. On the **Choose the layout** page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
    The wizard Preview pane displays a table with three rows. When you run the report, each row will display in the following way:  
  
    *  The first row will appear once for the table to show column headings.  
  
    *  The second row will repeat once for each product and display the product name, total per day, and line total.  
  
    *  The third row will appear once for the table to display the grand totals.  
    
    ![report-builder-sparkline-choose-layout](../reporting-services/media/report-builder-sparkline-choose-layout.png)
  
11. Click **Next**.  
  
12. Click **Finish**.  
  
14. The table is added to the design surface. The table has three columns and three rows.  
  
    Look in the Grouping pane. If you can't see the Grouping pane, on the **View** menu, click **Grouping**. The Row Groups pane shows one row group: **Product**. The Column Groups pane shows one column group: **SalesDate**. Detail data is all the data that is retrieved by the dataset query.  
    
    ![report-builder-sparkline-grouping-pane](../reporting-services/media/report-builder-sparkline-grouping-pane.png)
  
15. Click **Run** to preview the report.  

### <a name="FormatCurrency"></a>2a. Format Data as Currency  
By default, the summary data for the **Sales** field displays a general number. Format it to display the number as currency. Toggle **Placeholder Styles** to display formatted text boxes and placeholder text as sample values.  
  
1.  Click **Design** to switch to design view.  
  
2.  Click the cell in the second row (under the column headings row) in the **SalesDate** column. Hold down the Ctrl key and select all cells that contain `[Sum(Sales)]`. 

    ![report-builder-select-sum-sales](../reporting-services/media/report-builder-select-sum-sales.png) 
  
3.  On the **Home** tab > **Number** group, click **Currency**. The cells change to show the formatted currency.  

    ![report-builder-placeholder-currency](../reporting-services/media/report-builder-placeholder-currency.png)
  
    If your regional setting is English (United States), the default sample text is [**$12,345.00**]. If you do not see an example currency value, in the **Numbers** group, click **Placeholder Styles** > **Sample Values**.  
    
    ![report-builder-placeholder-value-button](../reporting-services/media/report-builder-placeholder-value-button.png)
   
### <a name="FormatDates"></a>2b. (Optional) Format Data as Dates  
By default, the **SalesDate** field displays both date and time information. You can format them to display only the date.  
  
1.  Click the cell that contains `[SalesDate]`.  
  
3.  On the **Home** tab > **Number** group > **Date**.  
  
    The cell displays the example date **[1/31/2000]**.
     
4.  Click **Run** to preview the report.  
  
The **SalesDate** values display in the default date format, and the summary values for **Sales** display as currency.   
  
## <a name="Sparkline"></a>3. Add a Sparkline    
  
1.  Click **Design** to return to design view.  
  
2.  Select the Total column in your table.  
  
3.  Right-click, point to **Insert Column**, and then click **Left**.  

    ![report-builder-add-column-left](../reporting-services/media/report-builder-add-column-left.png)
  
4.  In the new column, right-click the cell in the `[Product]` row > **Insert** > **Sparkline**.  

    ![report-builder-insert-sparkline](../reporting-services/media/report-builder-insert-sparkline.png)
  
5.  In the **Select Sparkline Type** dialog box, make sure the first sparkline in the **Column** row is selected, then click **OK**.  
  
6.  Click the sparkline to show the Chart Data pane.  
  
7.  Click the plus (+) sign in the Values box, then click **Sales**. 

    ![report-builder-sparkline-values](../reporting-services/media/report-builder-sparkline-values.png) 
  
    The values in the **Sales** field are now the values for the sparkline.  
  
8.  Click the plus (+) sign in the Category Groups box, then click **SalesDate**.  
  
9. Click **Run** to preview your report.  
  
    Note that the bars in the sparkline charts don't line up with each other. There are only four bars in the second row of data, so the bars are wider than the bars in the first row, which has six. You can't compare values for each product per day. They need to line up.  
  
    Also, for each row the tallest bar is the height of the row. This is misleading, too, because the largest values for each row are not equal: the largest value for Budget Movie-Maker is $10,400, but for Slim Digital it's $26,576 - more than twice as large. And yet the largest bars in those two rows are about the same height. All the sparklines need to use the same scale.  
  
     ![report-builder-sparkline-misaligned](../reporting-services/media/report-builder-sparkline-misaligned.png)
  
## <a name="AlignSparklines"></a>4. Align the Sparklines Vertically and Horizontally  
Sparklines are hard to read when they don't all use the same measurements. Both the horizontal and vertical axes for each need to match the rest.  
   
1.  Click **Design** to return to design view.  
  
2.  Right-click the sparkline and click **Vertical Axis Properties**.  
  
3.  Check the **Align axes in** check box. Tablix1 is the only option in the list.  
  
     This sets the height of the bars in each sparkline relative to the others. 
  
4.  Click **OK**.  
  
5.  Right-click the sparkline and click **Horizontal Axis Properties**.  
  
6.  Check the **Align axes in** check box. Tablix1 is the only option in the list. 
  
    This sets the width of the bars in each sparkline relative to the others. If some sparklines have fewer bars than others, then those sparklines will have blank spaces for the missing data.  
  
7.  Click **OK**.  
  
8.  Click **Run** to preview your report again.  
  
Now all the bars in each sparkline align with the bars in the other sparklines, and the heights are relative.  
  
![report-builder-sparkline-aligned](../reporting-services/media/report-builder-sparkline-aligned.png)
  
## <a name="Width"></a>7. (Optional) Change Column Widths  
By default, each cell in a table contains a text box. A text box expands vertically to accommodate text when the page is rendered. In the rendered report, each row expands to the height of the tallest rendered text box in the row. The height of the row on the design surface has no affect on the height of the row in the rendered report.  
  
To reduce the amount of vertical space each row takes, expand the column width to accommodate the expected contents of the text boxes in the column on one line.  
  
### To change the width of columns  
  
1.  Click **Design** to return to design view.  
  
2.  Click the table so gray bars appear above and next to the table. Those are the column and row handles
  
3.  Point to the line between column handles so that the cursor changes into a double arrow. Drag the **Product** column so that the product name displays on one line.  
  
4.  Click **Run** to preview your report and see if you made it wide enough.  
  
## <a name="Title"></a>8. (Optional) Add a Report Title  
A report title appears at the top of the report. You can place the report title in a report header or if the report does not use one, in a text box at the top of the report body. In this tutorial, you will use the text box that is automatically placed at the top of the report body.  
  
The text can be further enhanced by applying different font styles, sizes, and colors to phrases and individual characters of the text. For more information, see [Format Text in a Text Box &#40;Report Builder and SSRS&#41;](../reporting-services/report-design/format-text-in-a-text-box-report-builder-and-ssrs.md).  
  
### To add a report title  
  
1.  On the design surface, click **Click to add title**.  
  
2.  Type **Sales by Date**, and then click outside the text box.  
  
3.  Select the text box that contains **Product Sales**.  
  
4.  On the Home tab > **Font** group > for **Color**, select **Teal**.  
  
7.  Select **Bold**.  
  
8.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## <a name="Save"></a>9. Save the Report  
Save the report to a report server or your computer. If you do not save the report to the report server, a number of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features such as report parts and subreports are not available.  
  
### To save the report on a report server  
  
1.  From the **Report Builder** button, click **Save As**.  
  
2.  Click **Recent Sites and Servers**.  
  
3.  Select or type the name of the report server where you have permission to save reports.  
  
    The message "Connecting to report server" appears. When the connection is complete, you see the contents of the report folder that the report server administrator specified as the default location for reports.  
  
4.  In **Name**, replace the default name with **Product Sales**.  
  
5.  Click **Save**.  
  
The report is saved to the report server. The name of report server that you are connected to appears in the status bar at the bottom of the window.  
  
### To save the report on your computer  
  
1.  From the **Report Builder** button, click **Save As**.  
  
2.  Click **Desktop**, **My Documents**, or **My computer**, and browse to the folder where you want to save the report.  
  
3.  In **Name**, replace the default name with **Product Sales**.  
  
4.  Click **Save**.  
  
## Next Steps  

This concludes the tutorial for creating a table report with sparkline charts. For more information about sparklines, see [Sparklines and Data Bars](../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md).  
  
[Report Builder Tutorials](../reporting-services/report-builder-tutorials.md) 
[Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
