---
title: "Tutorial: Create drillthrough and main reports (Report Builder)"
description: "Learn how to create two kinds of Reporting Services paginated reports: a drillthrough report and a main report."
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Tutorial: Create drillthrough and main reports (Report Builder)
This tutorial teaches you how to create two kinds of [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated reports: a drillthrough report and a main report. The sample sales data used in these reports is retrieved from an Analysis Services cube. 

The following illustration shows the reports you create in this tutorial, and shows how the field value, Games and Toys, in the main report displays in the drillthrough report's title. The data in the drillthrough report pertains to the Games and Toys product category.  

:::image type="content" source="../reporting-services/media/rs-drillthroughcubetutorial.gif" alt-text="Screenshot that shows how a drillthrough report relates to its origin report.":::

Estimated time to complete this tutorial: 30 minutes.  
  
## Requirements  
This tutorial requires access to the Contoso Sales cube for both the drillthrough and the main reports. This dataset comprises the ContosoDW data warehouse and the Contoso_Retail online analytical processing (OLAP) database. The reports you create in this tutorial retrieve report data from the Contoso Sales cube. The Contoso_Retail OLAP database can be downloaded from the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=18279). You need only download the file ContosoBIdemoABF.exe. It contains the OLAP database.  
  
The other file, ContosoBIdemoBAK.exe, is for the ContosoDW data warehouse, which isn't used in this tutorial.  
  
The Web site includes instructions extracting and restoring the ContosoRetail.abf backup file to the Contoso_Retail OLAP database.  

You must have access to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] on which to install the OLAP database.  
    
For more about general requirements, see [Prerequisites for tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="DMatrixAndDataset"></a>1. Create a drillthrough report from the table or Matrix Wizard  
From the Getting Started dialog box, create a matrix report by using the **Table or Matrix Wizard**. There are two modes available in the wizard: report design and shared dataset design. In this tutorial, you use the report design mode.  
  
### Create a new report  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, verify that **Table or Matrix Wizard** is selected.  
  
## <a name="DConnection"></a>1a. Specify a data connection  
A data connection contains the information necessary to connect to an external data source such as an Analysis Services cube or a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database. To specify a data connection, you can use a shared data source from the report server or create an embedded data source that is used only in this report. In this tutorial, you use an embedded data source. To learn more about using a shared data source, see [Alternative ways to get a data connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
### Create an embedded data source  
  
1.  On the **Choose a dataset** page, select **Create a dataset**, and then choose **Next**. The **Choose a connection to a data source** page opens.  
  
2.  Select **New**. The **Data Source Properties** dialog box opens.  
  
3.  In **Name**, enter **Online and Reseller Sales Detail** as the name for the data source.  
  
4.  In **Select a connection type**, select **Microsoft SQL Server Analysis Services**, and then choose **Build**.  
  
5.  In **Data source**, verify that the data source is **Microsoft SQL Server Analysis Services (AdomdClient)**.  
  
6.  In **Server name**, enter the name of a server where an instance of Analysis Services is installed.  
  
7.  In **Select or enter a database name**, select the Contoso cube.  
  
8.  Select **OK**.
  
9. Verify that **Connection string** contains the following syntax:  
  
    ```  
    Data Source=<servername>; Initial Catalog = Contoso  
    ```  
  
    The `<servername>` is the name of an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with Analysis Services installed.  
  
10. Select **Credentials type**.  
  
    > [!NOTE]  
    > Depending on how permissions are configured on the data source, you might need to change the default authentication options. For more information, see [Security &#40;Report Builder&#41;](../reporting-services/report-builder/security-report-builder.md).  
  
11. Select **OK**.
  
    The **Choose a connection to a data source** page appears.  
  
12. To verify that you can connect to the data source, select **Test Connection**.  
  
    The message **Connection created successfully** appears.  
  
13. Select **OK**.
  
14. Select **Next**.  
  
## <a name="DMDXQuery"></a>1b. Create an MDX query  
In a report, you can use a shared dataset that has a predefined query, or you can create an embedded dataset for use only in your report. In this tutorial, you create an embedded dataset.  
  
### Create query filters  
  
1.  On the **Design a query** page, in the Metadata pane, select the button **(...)**.  
  
2.  In the **Cube Selection** dialog box, select Sales, and then choose **OK**.  
  
    > [!TIP]  
    > If you do not want to build the MDX query manually, select the Switch to Design mode :::image type="icon" source="../reporting-services/media/rsqdicon-designmode.gif"::: icon, toggle the query designer to Query mode, paste the completed MDX to the query designer, and then proceed to step 6 in [Create the dataset](#DSkip).  
  
    ```  
    SELECT NON EMPTY { [Measures].[Sales Amount], [Measures].[Sales Return Amount] } ON COLUMNS, NON EMPTY { ([Channel].[Channel Name].[Channel Name].ALLMEMBERS * [Product].[Product Category Name].[Product Category Name].ALLMEMBERS * [Product].[Product Subcategory Name].[Product Subcategory Name].ALLMEMBERS ) } DIMENSION PROPERTIES MEMBER_CAPTION, MEMBER_UNIQUE_NAME ON ROWS FROM ( SELECT ( { [Date].[Calendar Year].&[2009] } ) ON COLUMNS FROM ( SELECT ( { [Sales Territory].[Sales Territory Group].&[North America] } ) ON COLUMNS FROM ( SELECT ( STRTOSET(\@ProductProductCategoryName, CONSTRAINED) ) ON COLUMNS FROM ( SELECT ( { [Channel].[Channel Name].&[2], [Channel].[Channel Name].&[4] } ) ON COLUMNS FROM [Sales])))) WHERE ( [Sales Territory].[Sales Territory Group].&[North America], [Date].[Calendar Year].&[2009] ) CELL PROPERTIES VALUE, BACK_COLOR, FORE_COLOR, FORMATTED_VALUE, FORMAT_STRING, FONT_NAME, FONT_SIZE, FONT_FLAGS  
    ```  
  
3.  In the Measure Group pane, expand Channel, and then drag Channel Name to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Channel, is automatically added to the **Dimension** column. Don't change the **Dimension** or **Operator** columns.  
  
4.  To open the **Filter Expression** list, select the down arrow in the **Filter Expression** column.  
  
5.  In the filter expression list, expand **All Channel**, select **Online**, choose **Reseller**, and then select **OK**.  
  
    The query now includes a filter to include only these channels: Online and Reseller.  
  
6.  Expand the Sales Territory dimension, and then drag Sales Territory Group to the **Hierarchy** column (below **Channel Name**).  
  
7.  Open the **Filter Expression** list, expand **All Sales Territory**, select **North America**, and then choose **OK**.  
  
    The query now has a filter to include only sales in North America.  
  
8.  In the Measure Group pane, expand Date, and then drag Calendar Year to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Date, is automatically added to the **Dimension** column. Don't change the **Dimension** or **Operator** columns.  
  
9. To open the **Filter Expression** list, select the down arrow in the **Filter Expression** column.  
  
10. In the filter expression list, expand **All Date**, select **Year 2009**, and then choose **OK**.  
  
    The query now includes a filter to include only the calendar year 2009.  
  
### Create the parameter  
  
1.  Expand the Product dimension, and then drag the Product Category Name member to the **Hierarchy** column below **Calendar Year**.  
  
2.  Open the **Filter Expression** list, select **All Products**, and then choose **OK**.  
  
3.  Select the **Parameter** checkbox. The query now includes the parameter ProductProductCategoryName.  
  
    > [!NOTE]  
    > The parameter contains the names of product categories. When you select a product category name in the main report, its name is passed to the drillthrough report by using this parameter.  
  
### <a name="DSkip"></a>Create the dataset  
  
1.  From the Channel dimension, drag Channel Name to the data pane.  
  
2.  From the Product dimension, drag Product Category Name to the data pane, and then place it to the right of Channel Name.  
  
3.  From the Product dimension, drag Product Subcategory Name to the data pane, and then place it to the right of Product Category Name.  
  
4.  In the Metadata pane, expand **Measure**, and then expand Sales.  
  
5.  Drag the Sales Amount measure to the data pane, and then place it to the right of Product Subcategory Name.  
  
6.  On the query designer toolbar, select **Run (!)**.  
  
7.  Select **Next**.  
  
## <a name="DLayout"></a>1c. Organize drillthrough report data into groups  
When you select the fields on which to group the data, you design a matrix with rows and columns that displays detail and aggregated data.  
  
### Organize data into groups  
  
1.  To switch to design view, select **Design**.  
  
2.  On the **Arrange fields** page, drag Product_Subcategory_Name to **Row groups**.  
  
    > [!NOTE]  
    > The spaces in the names are replaced with underscores (_). For example Product Category Name is Product_Category_Name.  
  
3.  Drag Channel_Name to **Column groups**.  
  
4.  Drag Sales_Amount to **Values**.  
  
    The Sum function automatically aggregates Sales_Amount, the default aggregate for numeric fields. The value is `[Sum(Sales_Amount)]`.  
  
    To view the other aggregate functions available, open the drop-down list (don't change the aggregate function).  
  
5.  Drag Sales_Return_Amount to **Values**, and then place it after `[Sum(Sales_Amount)]`.  
  
    Steps 4 and 5 specify the data to display in the matrix.  
  
6.  Select **Next**.  
  
## <a name="DTotals"></a>1d. Add drillthrough report subtotals and totals  
After you create groups, you can add and format rows where the aggregate values for the fields will display. You can also choose whether to show all the data or to let a user expand and collapse grouped data interactively.  
  
### Add subtotals and totals  
  
1.  On the **Choose the layout** page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
    The wizard Preview pane displays a matrix with four rows.  
  
2.  Select **Next**.  
  
2.  Select **Finish**.  
  
    The table is added to the design surface.  
  
3.  To preview the report, select **Run (!)**.  
  
## <a name="DFormat"></a>2. Format data as currency  
Apply currency formatting to the sales amount fields in the drillthrough report.  
  
### Format data as currency  
  
1.  To switch to design view, select **Design**.  
  
2.  To select and format multiple cells at one time, press the Ctrl key, and then select the cells that contain the numeric sales data.  
  
3.  On the **Home** tab, in the **Number** group, select **Currency**.  
  
## <a name="DSparkline"></a>3. Add columns to show sales values in sparklines  
Instead of showing sales and sales returns as currency values, the report shows the values in a sparkline.  
  
### Add sparklines to columns  
  
1.  To switch to design view, select **Design**.  
  
2.  In the Total group of the matrix, right-click the **Sales Amount** column, select **Insert Column**, and then choose **Right**.  
  
    An empty column is added to the right of **Sales Amount**.  
  
3.  On the ribbon, select **Rectangle**, and then choose the empty cell to the right of the `[Sum(Sales_Amount)]` cell in the [Product_Subcategory] row group.  
  
4.  On the ribbon, select the **Sparkline** icon, and then choose the cell where the rectangle was added.  
  
5.  In the **Select Sparkline Type** dialog box, verify that **Column** type is selected.  
  
6.  Select **OK**.
  
7.  Right-click the sparkline.  
  
8.  In the Chart Data pane, select the **Add field** icon, and then choose Sales_Amount.  
  
9. Right-click the `Sales_Return_Amount` column, and then add a column to the right of it.  
  
10. Repeat steps 2 through 6.  
  
11. Right-click the sparkline.  
  
12. In the Chart Data pane, select the **Add field** icon, and then choose Sales_Return_Amount.  
  
13. To preview the report, select **Run**.  
  
## <a name="DReportTitle"></a>4. Add a report title with product category name  
A report title appears at the top of the report. You can place the report title in a report header or, if the report doesn't use one, in a text box at the top of the report body. In this tutorial, you use the text box that is automatically placed at the top of the report body.  
  
### Add a report title  
  
1.  To switch to design view, select **Design**.  
  
2.  On the design surface, select **Click to add title**.  
  
3.  Enter **Sales and Returns for Category:**.  
  
4.  Right-click, and then select **Create Placeholder**.  
  
5.  Select the **(fx)** button to the right of the **Value** list.  
  
6.  In the **Expression** dialog box, in the Category pane, select **Dataset**, and then in the **Values** list double-click `First(Product_Category_Name)`.  
  
    The **Expression** box contains the following expression:  
  
    ```  
    =First(Fields!Product_Category_Name.Value, "DataSet1")  
    ```  
  
7.  To preview the report, select **Run**.  
  
The report title includes the name of the first product category. Later, after you run this report as a drillthrough report, the product category name will dynamically change to reflect the name of the product category that was selected in the main report.  
  
## <a name="DParameter"></a>5. Set a hidden parameter property  
By default parameters are visible, which isn't appropriate for this report. You update the parameter properties for the drillthrough report later.
  
### Hide a parameter  
  
1.  In the Report Data pane, expand **Parameters**.  
  
2.  Right-click \@ProductProductCategoryName, and then select **Parameter Properties**.  
  
    > [!NOTE]  
    > The \@ character next to the name indicates that this is a parameter.  
  
3.  On the **General** tab, select **Hidden**.  
  
4.  In the **Prompt** box, enter **Product Category**.  
  
    > [!NOTE]  
    > Because the parameter is hidden, this prompt is never used.  
  
5.  Optionally, select **Available Values** and **Default Values** and review their options. Don't change any options on these tabs.  
  
6.  Select **OK**.
  
## <a name="DSave"></a>6. Save the report to a SharePoint library  
You can save the report to a SharePoint library, report server, or your computer. If you save the report to your computer, many [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features such as subreports aren't available. In this tutorial, you save the report to a SharePoint library.  
  
### Save the report  
  
1.  From the Report Builder button, select **Save**. The **Save As Report** dialog box opens.  
  
    > [!NOTE]  
    > If you are resaving a report, it is automatically resaved to its previous location. To change the location, use the **Save As** option.  
  
2.  To show a list of recently used report servers and SharePoint sites, select **Recent Sites and Servers**.  
  
3.  Select or enter the name of the SharePoint site where you have permission to save reports.  
  
    The URL of the SharePoint library has the following syntax:  
  
    ```  
    Http://<ServerName>/<Sites>/  
    ```  
  
4.  Select **Save**.  
  
    **Recent Sites and Servers** lists the libraries on the SharePoint site.  
  
5.  Navigate to the library where you want to save the report.  
  
6.  In the **Name** box, replace the default name with **ResellerVSOnlineDrillthrough**.  
  
    > [!NOTE]  
    > You will save the main report to the same location. If you want to save the main and drillthrough reports to different sites or libraries, you must update the path of the **Go to report** action in the main report.  
  
7.  Select **Save**.  
  
## <a name="MMatrixAndDataset"></a>1. Create the main report from the table or Matrix Wizard  
From the **Getting Started** dialog box, create a matrix report by using the **Table or Matrix Wizard**.  
  
### Create the main report  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
 
2.  In the **Getting Started** dialog box, verify that **New Report** is selected, and then select **Table or Matrix Wizard**.  
  
## <a name="MConnection"></a>1a. Add an embedded data source  
In this section, you add an embedded data source to the main report.  
  
### Create an embedded data source  
  
1.  On the **Choose a dataset** page, select **Create a dataset**, and then select **Next**.  
  
2.  Select **New**.  
  
3.  In **Name**, enter **Online and Reseller Sales Main** as the name for the data source.  
  
4.  In **Select a connection type**, select **Microsoft SQL Server Analysis Services**, and then select **Build**.  
  
5.  In **Data source**, verify that the data source is **Microsoft SQL Server Analysis Services (AdomdClient)**.  
  
6.  In **Server name**, enter the name of a server where an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] is installed.  
  
7.  In **Select or enter a database name**, select the Contoso cube.  
  
8.  Select **OK**.
  
9. Verify that the **Connection string** contains the following syntax:  
  
    ```  
    Data Source=<servername>; Initial Catalog = Contoso  
    ```  
  
10. Select **Credentials type**.  
  
    Depending on how permissions are configured on the data source, you might need to change the default authentication.  
  
11. Select **OK**.
  
12. To verify that you can connect to the data source, select **Test Connection**.  
  
13. Select **OK**.
  
14. Select **Next**.  
  
## <a name="MMDXQuery"></a>1b. Create an embedded dataset  
Next, create an embedded dataset. To do so, you use the query designer to create filters, parameters, and calculated members and the dataset itself.  
  
### Create query filters  
  
1.  On the **Design a query** page, in the Metadata pane, in the cube section, select the ellipsis **(...)**.  
  
2.  In the **Cube Selection** dialog box, select Sales, and then select **OK**.  
  
    > [!TIP]  
    > If you do not want to build the MDX query manually, select the Switch to Design mode :::image type="icon" source="../reporting-services/media/rsqdicon-designmode.gif"::: icon, toggle the query designer to Query mode, paste the completed MDX to the query designer, and then proceed to step 5 in [Create the dataset](#MSkip).  
  
    ```  
    WITH MEMBER [Measures].[Net QTY] AS [Measures].[Sales Quantity] -[Measures].[Sales Return Quantity] MEMBER [Measures].[Net Sales] AS [Measures].[Sales Amount] - [Measures].[Sales Return Amount] SELECT NON EMPTY { [Measures].[Net QTY], [Measures].[Net Sales] } ON COLUMNS, NON EMPTY { ([Channel].[Channel Name].[Channel Name].ALLMEMBERS * [Product].[Product Category Name].[Product Category Name].ALLMEMBERS ) } DIMENSION PROPERTIES MEMBER_CAPTION, MEMBER_UNIQUE_NAME ON ROWS FROM ( SELECT ( { [Date].[Calendar Year].&[2009] } ) ON COLUMNS FROM ( SELECT ( STRTOSET(\@ProductProductCategoryName, CONSTRAINED) ) ON COLUMNS FROM ( SELECT ( { [Sales Territory].[Sales Territory Group].&[North America] } ) ON COLUMNS FROM ( SELECT ( { [Channel].[Channel Name].&[2], [Channel].[Channel Name].&[4] } ) ON COLUMNS FROM [Sales])))) WHERE ( [Sales Territory].[Sales Territory Group].&[North America], [Date].[Calendar Year].&[2009] ) CELL PROPERTIES VALUE, BACK_COLOR, FORE_COLOR, FORMATTED_VALUE, FORMAT_STRING, FONT_NAME, FONT_SIZE, FONT_FLAGSQuery text: Code.  
    ```  
  
3.  In the Measure Group pane, expand Channel, and then drag Channel Name to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Channel, is automatically added to the **Dimension** column. Don't change the **Dimension** or **Operator** columns.  
  
4.  To open the **Filter Expression** list, select the down arrow in the **Filter Expression** column.  
  
5.  In the filter expression list, expand **All Channel**, select **Online** and **Reseller**, and then choose **OK**.  
  
    The query now includes a filter to include only these channels: Online and Reseller.  
  
6.  Expand the Sales Territory dimension, and then drag Sales Territory Group to the **Hierarchy** column, below **Channel Name**.  
  
7.  Open the **Filter Expression** list, expand **All Sales Territory**, select **North America**, and then choose **OK**.  
  
    The query now has a filter to include only sales in North America.  
  
8.  In the Measure Group pane, expand Date, and drag Calendar Year to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Date, is automatically added to the **Dimension** column. Don't change the **Dimension** or **Operator** columns.  
  
9. To open the **Filter Expression** list, select the down arrow in the **Filter Expression** column.  
  
10. In the filter expression list, expand **All Date**, select **Year 2009**, and then choose **OK**.  
  
    The query now includes a filter to include only the calendar year 2009.  
  
### Create the parameter  
  
1.  Expand the Product dimension, and then drag the Product Category Name member to the **Hierarchy** column below **Sales Territory Group**.  
  
2.  Open the **Filter Expression** list, select **All Products**, and then choose **OK**.  
  
3.  Select the **Parameter** checkbox. The query now includes the parameter ProductProductCategoryName.  
  
### Create calculated members  
  
1.  Place the cursor inside the Calculated Members pane, right-click, and then select **New Calculated Member**.  
  
2.  In the Metadata pane, expand **Measures** and then expand Sales.  
  
3.  Drag the Sales Quantity measure to the **Expression** box, enter the subtraction character (-), and then drag the Sales Return Quantity measure to the **Expression** box; place it after the subtraction character.  
  
    The following code shows the expression:  
  
    ```  
    [Measures].[Sales Quantity] - [Measures].[Sales Return Quantity]  
    ```  
  
4.  In the Name box, enter **Net QTY**, and then select **OK**.  
  
    The Calculated Members pane lists the **Net QTY** calculated member.  
  
5.  Right-click **Calculated Members**, and then select **New Calculated Member**.  
  
6.  In the Metadata pane, expand **Measures**, and then expand Sales.  
  
7.  Drag the Sales Amount measure to the **Expression** box, enter the subtraction character (-), and then drag the Sales Return Amount measure to the **Expression** box; place it after the subtraction character.  
  
    The following code shows the expression:  
  
    ```  
    [Measures].[Sales Amount] - [Measures].[Sales Return Amount]  
    ```  
  
8.  In the **Name** box, enter  **Net Sales**, and then select **OK**.The Calculated Members pane lists the **Net Sales** calculated member.  
  
### <a name="MSkip"></a>Create the dataset  
  
1.  From the Channel dimension, drag Channel Name to the data pane.  
  
2.  From the Product dimension, drag Product Category Name to the data pane, and then place it to the right of Channel Name.  
  
3.  From **Calculated Members**, drag `Net QTY` to the data pane, and then place it to the right of Product Category Name.  
  
4.  From Calculated Members, drag Net Sales to the data pane, and then place it to the right of `Net QTY`.  
  
5.  On the query designer toolbar, select **Run (!)**.  
  
    Review the query result set.  
  
6.  Select **Next**.  
  
## <a name="MLayout"></a>1c. Organize main report data into groups  
When you select the fields on which to group data, you design a matrix with rows and columns that displays detail and aggregated data.  
  
### Organize data into groups  
  
1.  On the **Arrange fields** page, drag Product_Category_Name to **Row groups**.  
  
2.  Drag Channel_Name to **Column groups**.  
  
3.  Drag `Net_QTY` to **Values**.  
  
    The Sum function automatically aggregates `Net_QTY`, the default aggregate for numeric fields. The value is `[Sum(Net_QTY)]`.  
  
    To view the other aggregate functions available, open the drop-down list. Don't change the aggregate function.  
  
4.  Drag `Net_Sales_Return` to **Values** and then place it after `[Sum(Net_QTY)]`.  
  
    Steps 3 and 4 specify the data to display in the matrix.  
  
## <a name="MTotals"></a>1d. Add main report subtotals and totals  
You can show subtotals and grand totals in reports. The data in the main report displays as an indicator. You next remove the grand total after you complete the wizard.  
  
### Add subtotals and grand totals  
  
1.  On the **Choose the layout** page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
    The wizard Preview pane displays a matrix with four rows.  When you run the report, each row displays in the following way: The first row is the column group, the second row contains the column headings, the third row contains the product category data (`[Sum(Net_ QTY)]` and `[Sum(Net_Sales)]`, and the fourth row contains the totals.  
  
2.  Select **Next**.  
  
3.  Select **Finish**.  
  
3.  To preview the report, select **Run**.  
  
## <a name="MGrandTotal"></a>2. Remove the grand total row from your report
The data values are shown as indictor states, including the column group totals. Remove the row that displays the grand total.  
  
### Remove the grand total row  
  
1.  To switch to design view, select **Design**.  
  
2.  Select the Total row (the last row in the matrix), right-click, and then select **Delete Rows**.  
  
3.  To preview the report, select **Run**.  
  
## <a name="MDrillthrough"></a>3. Configure text box action for drillthrough  
To enable the drillthrough, specify an action on a text box in the main report.  
  
### Enable an action  
  
1.  To switch to design view, select **Design**.  
  
2.  Right-click the cell that contains Product_Category_Name, and then select **Text Box Properties**.  
  
3.  Select the **Action** tab.  
  
4.  Select **Go to report.**  
  
5.  In **Specify a report**, select **Browse**, and then locate the drillthrough report named ResellerVSOnlineDrillthrough.  
  
6.  To add a parameter to run the drillthrough report, select **Add**.  
  
7.  In the **Name** list, select ProductProductCategoryName.  
  
8.  In **Value**, enter `[Product_Category_Name.UniqueName]`.  
  
    Product_Category_Name is a field in the dataset.  
  
    > [!IMPORTANT]  
    > You must include the **UniqueName** property because the drillthrough action requires a unique value.  
  
9. Select **OK**.
  
### Format the drillthrough field  
  
1.  Right-click the cell that contains the `Product_Category_Name`, and then select **Text Box Properties**.  
  
2.  Select the **Font** tab.  
  
3.  In the **Effects** list, select **Underline**.  
  
4.  In the **Color** list, select **Blue**.  
  
5.  Select **OK**.
  
6.  To preview your report, select **Run**.  
  
The product category names are in the common link format (blue and underlined).  
  
## <a name="MIndicators"></a>4. Replace numeric values with indicators  
Use indicators to show the state of quantities and sales for Online and Reseller channels.  
  
### Add an indicator for Net QTY values  
  
1.  To switch to design view, select **Design**.  
  
2.  On the ribbon, select the **Rectangle** icon, and then choose in the `[Sum(Net QTY)]` cell in the `[Product_Category_Name]` row group in the `Channel_Name` column group.  
  
3.  On the ribbon, select the **Indicator** icon, and then choose inside the rectangle. The **Select Indicator Type** dialog box opens with the **Directional** indicator selected.  
  
4.  Select the **3 Signs** enter, and then choose **OK**.  
  
5.  Right-click the indicator and in the Gauge Data pane, select the down arrow next to **(Unspecified)**. Select `Net_QTY`.  
  
6.  Repeat steps 2 through 5 for the `[Sum(Net QTY)]` cell in the `[Product_Category_Name]` row group within **Total**.  
  
### Add an indicator for Net Sales values  
  
1.  On the ribbon, select the **Rectangle** icon, and then select inside the `[Sum(Net_Sales)]` cell in the `[Product_Category_Name]` row group in the `Channel_Name` column group.  
  
2.  On the ribbon, select the **Indicator** icon, and then select inside the rectangle.  
  
3.  Select the **3 Signs** type, and then select **OK**.  
  
4.  Right-click the indicator and in the Gauge Data pane, select the down arrow next to **(Unspecified)**. Select `Net_Sales`.  
  
5.  Repeat steps 1 through 4 for the `[Sum(Net_Sales)]` cell in the `[Product_Category_Name]` row group within **Total**.  
  
6.  To preview your report, select **Run**.  
  
## <a name="MParameter"></a>5. Set internal parameter property  
By default, parameters are visible, which isn't appropriate for this report. You next update the parameter properties to make the parameter internal.  
  
### Make the parameter internal  
  
1.  In the Report Data pane, expand **Parameters**.  
  
2.  Right-click `@ProductProductCategoryName,` and then select **Parameter Properties**.  
  
3.  On the **General** tab, select **Internal**.  
  
4.  Optionally, select the **Available Values** and **Default Values** tabs and review their options. Don't change any options on these tabs.  
  
5.  Select **OK**.
  
## <a name="MTitle"></a>6. Add a report title  
Add a title to the main report.  
  
### Add a report title  
  
1.  On the design surface, select **Click to add title**.  
  
2.  Enter **2009 Product Category Sales: Online and Reseller Category:**.  
  
3.  Select the text you entered.  
  
4.  On the **Home** tab of the ribbon, in the Font group, select the **Times New Roman** font, **16pt** size, and the **Bold** and **Italic** styles.  
  
5.  To preview your report, select **Run**.  
  
## <a name="MSave"></a>7. Save the main report to a SharePoint library  
Save the main report to a SharePoint library.  
  
### Save the report  
  
1.  To switch to design view, select **Design**.  
  
2.  From the Report Builder button, select **Save**.  
  
3.  Optionally, to show a list of recently used report servers and SharePoint sites, select **Recent Sites and Servers**.  
  
4.  Select or enter the name of the SharePoint site where you have permission to save reports. The URL of the SharePoint library has the following syntax:  
  
    ```  
    Http://<ServerName>/<Sites>/  
    ```  
  
5.  Navigate to the library where you want to save the report.  
  
6.  In **Name**, replace the default name with **ResellerVSOnlineMain**.  
  
    > [!IMPORTANT]  
    > Save the main report to the same location where you saved the drillthrough report. To save the main and drillthrough reports to different sites or libraries, confirm that the **Go to report** action in the main report, points to the correct location of the drillthrough report.  
  
7.  Select **Save**.  
  
## <a name="MRunReports"></a>8. Run the main and drillthrough reports  
Run the main report, and then select values in the product category column to run the drillthrough report.  
  
### Run the reports  
  
1.  Open the SharePoint library where the reports are saved.  
  
2.  Double-click ResellerVSOnlineMain.  
  
    The report runs and displays product category sales information.  
  
3.  Select the **Games and Toys** link in the column that contains product category names.  
  
    The drillthrough report runs, displaying only the values for the Games and Toys product category.  
  
4.  To return to the main report, select the Internet Explorer back button.  
  
5.  Optionally, explore other product categories by selecting their names.  
  
## Related content

- [Report Builder tutorials](../reporting-services/report-builder-tutorials.md)
