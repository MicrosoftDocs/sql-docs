---
title: "Tutorial: Creating Drillthrough and Main Reports (Report Builder) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: 7168c8d3-cef5-4c4a-a0bf-fff1ac5b8b71
author: maggiesMSFT
ms.author: maggies
---
# Tutorial: Creating Drillthrough and Main Reports (Report Builder)
This tutorial teaches you how to create two kinds of [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated reports: a drillthrough report and a main report. The sample sales data used in these reports is retrieved from an Analysis Services cube. 

The following illustration shows the reports you will create, and shows how the field value, Games and Toys, in the main report displays in the drillthrough report's title. The data in the drillthrough report pertains to the Games and Toys product category.  
  
![rs_DrillthroughCubeTutorial](../reporting-services/media/rs-drillthroughcubetutorial.gif "rs_DrillthroughCubeTutorial")  
   
Estimated time to complete this tutorial: 30 minutes.  
  
## Requirements  
This tutorial requires access to the Contoso Sales cube for both the drillthrough and the main reports. This dataset comprises the ContosoDW data warehouse and the Contoso_Retail online analytical processing (OLAP) database. The reports you will create in this tutorial retrieve report data from the Contoso Sales cube. The Contoso_Retail OLAP database can be downloaded from [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=191575). You need only download the file ContosoBIdemoABF.exe. It contains the OLAP database.  
  
The other file, ContosoBIdemoBAK.exe, is for the ContosoDW data warehouse, which is not used in this tutorial.  
  
The Web site includes instructions extracting and restoring the ContosoRetail.abf backup file to the Contoso_Retail OLAP database.  

You must have access to an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] on which to install the OLAP database.  
    
For more about general requirements, see [Prerequisites for Tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="DMatrixAndDataset"></a>1. Create a Drillthrough Report from the Table or Matrix Wizard  
From the Getting Started dialog box, create a matrix report by using the **Table or Matrix Wizard**. There are two modes available in the wizard: report design and shared dataset design. In this tutorial, you will use the report design mode.  
  
#### To create a new report  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, verify that **Table or Matrix Wizard** is selected.  
  
## <a name="DConnection"></a>1a. Specify a Data Connection  
A data connection contains the information necessary to connect to an external data source such as an Analysis Services cube or a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database. To specify a data connection, you can use a shared data source from the report server or create an embedded data source that is used only in this report. In this tutorial, you will use an embedded data source. To learn more about using a shared data source, see [Alternative Ways to Get a Data Connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
#### To create an embedded data source  
  
1.  On the **Choose a dataset** page, select **Create a dataset**, and then click **Next**. The **Choose a connection to a data source** page opens.  
  
2.  Click **New**. The **Data Source Properties** dialog box opens.  
  
3.  In **Name**, type **Online and Reseller Sales Detail** as the name for the data source.  
  
4.  In **Select a connection type**, select **Microsoft SQL Server Analysis Services**, and then click **Build**.  
  
5.  In **Data source**, verify that the data source is **Microsoft SQL Server Analysis Services (AdomdClient)**.  
  
6.  In **Server name**, type the name of a server where an instance of Analysis Services is installed.  
  
7.  In **Select or enter a database name**, select the Contoso cube.  
  
8.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
9. Verify that **Connection string** contains the following syntax:  
  
    ```  
    Data Source=<servername>; Initial Catalog = Contoso  
    ```  
  
    The `<servername>` is the name of an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] with Analysis Services installed.  
  
10. Click **Credentials type**.  
  
    > [!NOTE]  
    > Depending on how permissions are configured on the data source, you might need to change the default authentication options. For more information, see [Security &#40;Report Builder&#41;](../reporting-services/report-builder/security-report-builder.md).  
  
11. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
    The **Choose a connection to a data source** page appears.  
  
12. To verify that you can connect to the data source, click **Test Connection**.  
  
    The message **Connection created successfully** appears.  
  
13. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
14. Click **Next**.  
  
## <a name="DMDXQuery"></a>1b. Create an MDX Query  
In a report, you can use a shared dataset that has a predefined query, or you can create an embedded dataset for use only in your report. In this tutorial, you will create an embedded dataset.  
  
#### To create query filters  
  
1.  On the **Design a query** page, in the Metadata pane, click the button **(...)**.  
  
2.  In the **Cube Selection** dialog box, click Sales, and then click **OK**.  
  
    > [!TIP]  
    > If you do not want to build the MDX query manually, click the ![Switch to Design mode](../reporting-services/media/rsqdicon-designmode.gif "Switch to Design mode") icon, toggle the query designer to Query mode, paste the completed MDX to the query designer, and then proceed to step 6 in [To create the dataset](#DSkip).  
  
    ```  
    SELECT NON EMPTY { [Measures].[Sales Amount], [Measures].[Sales Return Amount] } ON COLUMNS, NON EMPTY { ([Channel].[Channel Name].[Channel Name].ALLMEMBERS * [Product].[Product Category Name].[Product Category Name].ALLMEMBERS * [Product].[Product Subcategory Name].[Product Subcategory Name].ALLMEMBERS ) } DIMENSION PROPERTIES MEMBER_CAPTION, MEMBER_UNIQUE_NAME ON ROWS FROM ( SELECT ( { [Date].[Calendar Year].&[2009] } ) ON COLUMNS FROM ( SELECT ( { [Sales Territory].[Sales Territory Group].&[North America] } ) ON COLUMNS FROM ( SELECT ( STRTOSET(\@ProductProductCategoryName, CONSTRAINED) ) ON COLUMNS FROM ( SELECT ( { [Channel].[Channel Name].&[2], [Channel].[Channel Name].&[4] } ) ON COLUMNS FROM [Sales])))) WHERE ( [Sales Territory].[Sales Territory Group].&[North America], [Date].[Calendar Year].&[2009] ) CELL PROPERTIES VALUE, BACK_COLOR, FORE_COLOR, FORMATTED_VALUE, FORMAT_STRING, FONT_NAME, FONT_SIZE, FONT_FLAGS  
    ```  
  
3.  In the Measure Group pane, expand Channel, and then drag Channel Name to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Channel, is automatically added to the **Dimension** column. Do not change the **Dimension** or **Operator** columns.  
  
4.  To open the **Filter Expression** list, click the down arrow in the **Filter Expression** column.  
  
5.  In the filter expression list, expand **All Channel**, click **Online**, click **Reseller**, and then click **OK**.  
  
    The query now includes a filter to include only these channels: Online and Reseller.  
  
6.  Expand the Sales Territory dimension, and then drag Sales Territory Group to the **Hierarchy** column (below **Channel Name**).  
  
7.  Open the **Filter Expression** list, expand **All Sales Territory**, click **North America**, and then click **OK**.  
  
    The query now has a filter to include only sales in North America.  
  
8.  In the Measure Group pane, expand Date, and then drag Calendar Year to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Date, is automatically added to the **Dimension** column. Do not change the **Dimension** or **Operator** columns.  
  
9. To open the **Filter Expression** list, click the down arrow in the **Filter Expression** column.  
  
10. In the filter expression list, expand **All Date**, click **Year 2009**, and then click **OK**.  
  
    The query now includes a filter to include only the calendar year 2009.  
  
#### To create the parameter  
  
1.  Expand the Product dimension, and then drag the Product Category Name member to the **Hierarchy** column below **Calendar Year**.  
  
2.  Open the **Filter Expression** list, click **All Products**, and then click **OK**.  
  
3.  Click the **Parameter** checkbox. The query now includes the parameter ProductProductCategoryName.  
  
    > [!NOTE]  
    > The parameter contains the names of product categories. When you click a product category name in the main report, its name is passed to the drillthrough report by using this parameter.  
  
### <a name="DSkip"></a>To create the dataset  
  
1.  From the Channel dimension, drag Channel Name to the data pane.  
  
2.  From the Product dimension, drag Product Category Name to the data pane, and then place it to the right of Channel Name.  
  
3.  From the Product dimension, drag Product Subcategory Name to the data pane, and then place it to the right of Product Category Name.  
  
4.  In the Metadata pane, expand **Measure**, and then expand Sales.  
  
5.  Drag the Sales Amount measure to the data pane, and then place it to the right of Product Subcategory Name.  
  
6.  On the query designer toolbar, click **Run (!)**.  
  
7.  Click **Next**.  
  
## <a name="DLayout"></a>1c. Organize Data into Groups  
When you select the fields on which to group the data, you design a matrix with rows and columns that displays detail and aggregated data.  
  
#### To organize data into groups  
  
1.  To switch to design view, click **Design**.  
  
2.  On the **Arrange fields** page, drag Product_Subcategory_Name to **Row groups**.  
  
    > [!NOTE]  
    > The spaces in the names are replaced with underscores (_). For example Product Category Name is Product_Category_Name.  
  
3.  Drag Channel_Name to **Column groups**.  
  
4.  Drag Sales_Amount to **Values**.  
  
    Sales_Amount is automatically aggregated by the Sum function, the default aggregate for numeric fields. The value is `[Sum(Sales_Amount)]`.  
  
    To view the other aggregate functions available, open the drop-down list (do not change the aggregate function).  
  
5.  Drag Sales_Return_Amount to **Values**, and then place it below `[Sum(Sales_Amount)]`.  
  
    Steps 4 and 5 specify the data to display in the matrix.  
  
6.  Click **Next**.  
  
## <a name="DTotals"></a>1d. Add Subtotals and Totals  
After you create groups, you can add and format rows where the aggregate values for the fields will display. You can also choose whether to show all the data or to let a user expand and collapse grouped data interactively.  
  
#### To add subtotals and totals  
  
1.  On the **Choose the layout** page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
    The wizard Preview pane displays a matrix with four rows.  
  
2.  Click **Next**.  
  
2.  Click **Finish**.  
  
    The table is added to the design surface.  
  
3.  To preview the report, click **Run (!)**.  
  
## <a name="DFormat"></a>2. Format Data as Currency  
Apply currency formatting to the sales amount fields in the drillthrough report.  
  
#### To format data as currency  
  
1.  To switch to design view, click **Design**.  
  
2.  To select and format multiple cells at one time, press the Ctrl key, and then select the cells that contain the numeric sales data.  
  
3.  On the **Home** tab, in the **Number** group, click **Currency**.  
  
## <a name="DSparkline"></a>3. Add Columns to Show Sales Values in Sparklines  
Instead of showing sales and sales returns as currency values, the report shows the values in a sparkline.  
  
#### To add sparklines to columns  
  
1.  To switch to design view, click **Design**.  
  
2.  In the Total group of the matrix, right-click the **Sales Amount** column, click **Insert Column**, and then click **Right**.  
  
    An empty column is added to the right of **Sales Amount**.  
  
3.  On the ribbon, click **Rectangle**, and then click the empty cell to the right of the `[Sum(Sales_Amount)]` cell in the [Product_Subcategory] row group.  
  
4.  On the ribbon, click the **Sparkline** icon, and then click the cell where the rectangle was added.  
  
5.  In the **Select Sparkline Type** dialog box, verify that **Column** type is selected.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
7.  Right-click the sparkline.  
  
8.  In the Chart Data pane, click the **Add field** icon, and then click Sales_Amount.  
  
9. Right-click the `Sales_Return_Amount` column, and then add a column to the right of it.  
  
10. Repeat steps 2 through 6.  
  
11. Right-click the sparkline.  
  
12. In the Chart Data pane, click the **Add field** icon, and then click Sales_Return_Amount.  
  
13. To preview the report, click **Run**.  
  
## <a name="DReportTitle"></a>4. Add Report Title with Product Category Name  
A report title appears at the top of the report. You can place the report title in a report header or, if the report does not use one, in a text box at the top of the report body. In this tutorial, you will use the text box that is automatically placed at the top of the report body.  
  
#### To add a report title  
  
1.  To switch to design view, click **Design**.  
  
2.  On the design surface, click **Click to add title**.  
  
3.  Type **Sales and Returns for Category:**.  
  
4.  Right-click, and then click **Create Placeholder**.  
  
5.  Click the **(fx)** button to the right of the **Value** list.  
  
6.  In the **Expression** dialog box, in the Category pane, click **Dataset**, and then in the **Values** list double-click `First(Product_Category_Name)`.  
  
    The **Expression** box contains the following expression:  
  
    ```  
    =First(Fields!Product_Category_Name.Value, "DataSet1")  
    ```  
  
7.  To preview the report, click **Run**.  
  
The report title includes the name of the first product category. Later, after you run this report as a drillthrough report, the product category name will dynamically change to reflect the name of the product category that was clicked in the main report.  
  
## <a name="DParameter"></a>5. Update Parameter Properties  
By default parameters are visible, which is not appropriate for this report. You will update the parameter properties for the drillthrough report.  
  
#### To hide a parameter  
  
1.  In the Report Data pane, expand **Parameters**.  
  
2.  Right-click \@ProductProductCategoryName, and then click **Parameter Properties**.  
  
    > [!NOTE]  
    > The \@ character next to the name indicates that this is a parameter.  
  
3.  On the **General** tab, click **Hidden**.  
  
4.  In the **Prompt** box, type **Product Category**.  
  
    > [!NOTE]  
    > Because the parameter is hidden, this prompt is never used.  
  
5.  Optionally, click **Available Values** and **Default Values** and review their options. Do not change any options on these tabs.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## <a name="DSave"></a>6. Save the Report to a SharePoint Library  
You can save the report to a SharePoint library, report server, or your computer. If you save the report to your computer, a number of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features such as report parts and subreports are not available. In this tutorial, you will save the report to a SharePoint library.  
  
#### To save the report  
  
1.  From the Report Builder button, click **Save**. The **Save As Report** dialog box opens.  
  
    > [!NOTE]  
    > If you are resaving a report, it is automatically resaved to its previous location. To change the location, use the **Save As** option.  
  
2.  To show a list of recently used report servers and SharePoint sites, click **Recent Sites and Servers**.  
  
3.  Select or type the name of the SharePoint site where you have permission to save reports.  
  
    The URL of the SharePoint library has the following syntax:  
  
    ```  
    Http://<ServerName>/<Sites>/  
    ```  
  
4.  Click **Save**.  
  
    **Recent Sites and Servers** lists the libraries on the SharePoint site.  
  
5.  Navigate to the library where you will save the report.  
  
6.  In the **Name** box, replace the default name with **ResellerVSOnlineDrillthrough**.  
  
    > [!NOTE]  
    > You will save the main report to the same location. If you want to save the main and drillthrough reports to different sites or libraries, you must update the path of the **Go to report** action in the main report.  
  
7.  Click **Save**.  
  
## <a name="MMatrixAndDataset"></a>1. Create the Main Report from the Table or Matrix Wizard  
From the **Getting Started** dialog box, create a matrix report by using the **Table or Matrix Wizard**.  
  
#### To create the main report  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
 
2.  In the **Getting Started** dialog box, verify that **New Report** is selected, and then click **Table or Matrix Wizard**.  
  
## <a name="MConnection"></a>1a. Specify a Data Connection  
You will add an embedded data source to the main report.  
  
#### To create an embedded data source  
  
1.  On the **Choose a dataset** page, select **Create a dataset**, and then click **Next**.  
  
2.  Click **New**.  
  
3.  In **Name**, type **Online and Reseller Sales Main** as the name for the data source.  
  
4.  In **Select a connection type**, select **Microsoft SQL Server Analysis Services**, and then click **Build**.  
  
5.  In **Data source**, verify that the data source is **Microsoft SQL Server Analysis Services (AdomdClient)**.  
  
6.  In **Server name**, type the name of a server where an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)][!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] is installed.  
  
7.  In **Select or enter a database name**, select the Contoso cube.  
  
8.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
9. Verify that the **Connection string** contains the following syntax:  
  
    ```  
    Data Source=<servername>; Initial Catalog = Contoso  
    ```  
  
10. Click **Credentials type**.  
  
    Depending on how permissions are configured on the data source, you might need to change the default authentication.  
  
11. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
12. To verify that you can connect to the data source, click **Test Connection**.  
  
13. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
14. Click **Next**.  
  
## <a name="MMDXQuery"></a>1b. Create an MDX Query  
Next, create an embedded dataset. To do so, you will use the query designer to create filters, parameters, and calculated members as well as the dataset itself.  
  
#### To create query filters  
  
1.  On the **Design a query** page, in the Metadata pane, in the cube section, click the ellipsis **(...)**.  
  
2.  In the **Cube Selection** dialog box, click Sales, and then click **OK**.  
  
    > [!TIP]  
    > If you do not want to build the MDX query manually, click the ![Switch to Design mode](../reporting-services/media/rsqdicon-designmode.gif "Switch to Design mode") icon, toggle the query designer to Query mode, paste the completed MDX to the query designer, and then proceed to step 5 in [To create the dataset](#MSkip).  
  
    ```  
    WITH MEMBER [Measures].[Net QTY] AS [Measures].[Sales Quantity] -[Measures].[Sales Return Quantity] MEMBER [Measures].[Net Sales] AS [Measures].[Sales Amount] - [Measures].[Sales Return Amount] SELECT NON EMPTY { [Measures].[Net QTY], [Measures].[Net Sales] } ON COLUMNS, NON EMPTY { ([Channel].[Channel Name].[Channel Name].ALLMEMBERS * [Product].[Product Category Name].[Product Category Name].ALLMEMBERS ) } DIMENSION PROPERTIES MEMBER_CAPTION, MEMBER_UNIQUE_NAME ON ROWS FROM ( SELECT ( { [Date].[Calendar Year].&[2009] } ) ON COLUMNS FROM ( SELECT ( STRTOSET(\@ProductProductCategoryName, CONSTRAINED) ) ON COLUMNS FROM ( SELECT ( { [Sales Territory].[Sales Territory Group].&[North America] } ) ON COLUMNS FROM ( SELECT ( { [Channel].[Channel Name].&[2], [Channel].[Channel Name].&[4] } ) ON COLUMNS FROM [Sales])))) WHERE ( [Sales Territory].[Sales Territory Group].&[North America], [Date].[Calendar Year].&[2009] ) CELL PROPERTIES VALUE, BACK_COLOR, FORE_COLOR, FORMATTED_VALUE, FORMAT_STRING, FONT_NAME, FONT_SIZE, FONT_FLAGSQuery text: Code.  
    ```  
  
3.  In the Measure Group pane, expand Channel, and then drag Channel Name to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Channel, is automatically added to the **Dimension** column. Do not change the **Dimension** or **Operator** columns.  
  
4.  To open the **Filter Expression** list, click the down arrow in the **Filter Expression** column.  
  
5.  In the filter expression list, expand **All Channel**, click **Online** and **Reseller**, and then click **OK**.  
  
    The query now includes a filter to include only these channels: Online and Reseller.  
  
6.  Expand the Sales Territory dimension, and then drag Sales Territory Group to the **Hierarchy** column, below **Channel Name**.  
  
7.  Open the **Filter Expression** list, expand **All Sales Territory**, click **North America**, and then click **OK**.  
  
    The query now has a filter to include only sales in North America.  
  
8.  In the Measure Group pane, expand Date, and drag Calendar Year to the **Hierarchy** column in the filter pane.  
  
    The dimension name, Date, is automatically added to the **Dimension** column. Do not change the **Dimension** or **Operator** columns.  
  
9. To open the **Filter Expression** list, click the down arrow in the **Filter Expression** column.  
  
10. In the filter expression list, expand **All Date**, click **Year 2009**, and then click **OK**.  
  
    The query now includes a filter to include only the calendar year 2009.  
  
#### To create the parameter  
  
1.  Expand the Product dimension, and then drag the Product Category Name member to the **Hierarchy** column below **Sales Territory Group**.  
  
2.  Open the **Filter Expression** list, click **All Products**, and then click **OK**.  
  
3.  Click the **Parameter** checkbox. The query now includes the parameter ProductProductCategoryName.  
  
#### To create calculated members  
  
1.  Place the cursor inside the Calculated Members pane, right-click, and then click **New Calculated Member**.  
  
2.  In the Metadata pane, expand **Measures** and then expand Sales.  
  
3.  Drag the Sales Quantity measure to the **Expression** box, type the subtraction character (-), and then drag the Sales Return Quantity measure to the **Expression** box; place it after the subtraction character.  
  
    The following code shows the expression:  
  
    ```  
    [Measures].[Sales Quantity] - [Measures].[Sales Return Quantity]  
    ```  
  
4.  In the Name box, type **Net QTY**, and then click **OK**.  
  
    The Calculated Members pane lists the **Net QTY** calculated member.  
  
5.  Right-click **Calculated Members**, and then click **New Calculated Member**.  
  
6.  In the Metadata pane, expand **Measures**, and then expand Sales.  
  
7.  Drag the Sales Amount measure to the **Expression** box, type the subtraction character (-), and then drag the Sales Return Amount measure to the **Expression** box; place it after the subtraction character.  
  
    The following code shows the expression:  
  
    ```  
    [Measures].[Sales Amount] - [Measures].[Sales Return Amount]  
    ```  
  
8.  In the **Name** box, type  **Net Sales**, and then click **OK**.The Calculated Members pane lists the **Net Sales** calculated member.  
  
### <a name="MSkip"></a>To create the dataset  
  
1.  From the Channel dimension, drag Channel Name to the data pane.  
  
2.  From the Product dimension, drag Product Category Name to the data pane, and then place it to the right of Channel Name.  
  
3.  From **Calculated Members**, drag `Net QTY` to the data pane, and then place it to the right of Product Category Name.  
  
4.  From Calculated Members, drag Net Sales to the data pane, and then place it to the right of `Net QTY`.  
  
5.  On the query designer toolbar, click **Run (!)**.  
  
    Review the query result set.  
  
6.  Click **Next**.  
  
## <a name="MLayout"></a>1c. Organize Data into Groups  
When you select the fields on which to group data, you design a matrix with rows and columns that displays detail and aggregated data.  
  
#### To organize data into groups  
  
1.  On the **Arrange fields** page, drag Product_Category_Name to **Row groups**.  
  
2.  Drag Channel_Name to **Column groups**.  
  
3.  Drag `Net_QTY` to **Values**.  
  
    `Net_QTY` is automatically aggregated by the Sum function, the default aggregate for numeric fields. The value is `[Sum(Net_QTY)]`.  
  
    To view the other aggregate functions available, open the drop-down list. Do not change the aggregate function.  
  
4.  Drag `Net_Sales_Return` to **Values** and then place it below `[Sum(Net_QTY)]`.  
  
    Steps 3 and 4 specify the data to display in the matrix.  
  
## <a name="MTotals"></a>1d. Add Subtotals and Totals  
You can show subtotals and grand totals in reports. The data in the main report displays as an indicator; you will remove the grand total after you complete the wizard.  
  
#### To add subtotals and grand totals  
  
1.  On the **Choose the layout** page, under **Options**, verify that **Show subtotals and grand totals** is selected.  
  
    The wizard Preview pane displays a matrix with four rows.  When you run the report, each row will display in the following way: The first row is the column group, the second row contains the column headings, the third row contains the product category data (`[Sum(Net_ QTY)]` and `[Sum(Net_Sales)]`, and the fourth row contains the totals.  
  
2.  Click **Next**.  
  
3.  Click **Finish**.  
  
3.  To preview the report, click **Run**.  
  
## <a name="MGrandTotal"></a>2. Remove the Grand Total Row  
The data values are shown as indictor states, including the column group totals. Remove the row that displays the grand total.  
  
#### To remove the grand total row  
  
1.  To switch to design view, click **Design**.  
  
2.  Click the Total row (the last row in the matrix), right-click, and then click **Delete Rows**.  
  
3.  To preview the report, click **Run**.  
  
## <a name="MDrillthrough"></a>3. Configure Text Box Action for Drillthrough  
To enable the drillthrough, specify an action on a text box in the main report.  
  
#### To enable an action  
  
1.  To switch to design view, click **Design**.  
  
2.  Right-click the cell that contains Product_Category_Name, and then click **Text Box Properties**.  
  
3.  Click the **Action** tab.  
  
4.  Select **Go to report.**  
  
5.  In **Specify a report**, click **Browse**, and then locate the drillthrough report named ResellerVSOnlineDrillthrough.  
  
6.  To add a parameter to run the drillthrough report, click **Add**.  
  
7.  In the **Name** list, select ProductProductCategoryName.  
  
8.  In **Value**, type `[Product_Category_Name.UniqueName]`.  
  
    Product_Category_Name is a field in the dataset.  
  
    > [!IMPORTANT]  
    > You must include the **UniqueName** property because the drillthrough action requires a unique value.  
  
9. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
#### To format the drillthrough field  
  
1.  Right-click the cell that contains the `Product_Category_Name`, and then click **Text Box Properties**.  
  
2.  Click the **Font** tab.  
  
3.  In the **Effects** list, select **Underline**.  
  
4.  In the **Color** list, select **Blue**.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
6.  To preview your report, click **Run**.  
  
The product category names are in the common link format (blue and underlined).  
  
## <a name="MIndicators"></a>4. Replace Numeric Values with Indicators  
Use indicators to show the state of quantities and sales for Online and Reseller channels.  
  
#### To add an indicator for Net QTY values  
  
1.  To switch to design view, click **Design**.  
  
2.  On the ribbon, click the **Rectangle** icon, and then click in the `[Sum(Net QTY)]` cell in the `[Product_Category_Name]` row group in the `Channel_Name` column group.  
  
3.  On the ribbon, click the **Indicator** icon, and then click inside the rectangle. The **Select Indicator Type** dialog box opens with the **Directional** indicator selected.  
  
4.  Click the **3 Signs** type, and then click **OK**.  
  
5.  Right-click the indicator and in the Gauge Data pane, click the down arrow next to **(Unspecified)**. Select `Net_QTY`.  
  
6.  Repeat steps 2 through 5 for the `[Sum(Net QTY)]` cell in the `[Product_Category_Name]` row group within **Total**.  
  
#### To add an indicator for Net Sales values  
  
1.  On the ribbon, click the **Rectangle** icon, and then click inside the `[Sum(Net_Sales)]` cell in the `[Product_Category_Name]` row group in the `Channel_Name` column group.  
  
2.  On the ribbon, click the **Indicator** icon, and then click inside the rectangle.  
  
3.  Click the **3 Signs** type, and then click **OK**.  
  
4.  Right-click the indicator and in the Gauge Data pane, click the down arrow next to **(Unspecified)**. Select `Net_Sales`.  
  
5.  Repeat steps 1 through 4 for the `[Sum(Net_Sales)]` cell in the `[Product_Category_Name]` row group within **Total**.  
  
6.  To preview your report, click **Run**.  
  
## <a name="MParameter"></a>5. Update Parameter Properties  
By default, parameters are visible, which is not appropriate for this report. You will update the parameter properties to make the parameter internal.  
  
#### To make the parameter internal  
  
1.  In the Report Data pane, expand **Parameters**.  
  
2.  Right-click `@ProductProductCategoryName,` and then click **Parameter Properties**.  
  
3.  On the **General** tab, click **Internal**.  
  
4.  Optionally, click the **Available Values** and **Default Values** tabs and review their options. Do not change any options on these tabs.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## <a name="MTitle"></a>6. Add a Report Title  
Add a title to the main report.  
  
#### To add a report title  
  
1.  On the design surface, click **Click to add title**.  
  
2.  Type **2009 Product Category Sales: Online and Reseller Category:**.  
  
3.  Select the text you typed.  
  
4.  On the **Home** tab of the ribbon, in the Font group, select the **Times New Roman** font, **16pt** size, and the **Bold** and **Italic** styles.  
  
5.  To preview your report, click **Run**.  
  
## <a name="MSave"></a>7. Save the Main Report to a SharePoint Library  
Save the main report to a SharePoint library.  
  
#### To save the report  
  
1.  To switch to design view, click **Design**.  
  
2.  From the Report Builder button, click **Save**.  
  
3.  Optionally, to show a list of recently used report servers and SharePoint sites, click **Recent Sites and Servers**.  
  
4.  Select or type the name of the SharePoint site where you have permission to save reports. The URL of the SharePoint library has the following syntax:  
  
    ```  
    Http://<ServerName>/<Sites>/  
    ```  
  
5.  Navigate to the library where you want to save the report.  
  
6.  In **Name**, replace the default name with **ResellerVSOnlineMain**.  
  
    > [!IMPORTANT]  
    > Save the main report to the same location where you saved the drillthrough report. To save the main and drillthrough reports to different sites or libraries, confirm that the **Go to report** action in the main report, points to the correct location of the drillthrough report.  
  
7.  Click **Save**.  
  
## <a name="MRunReports"></a>8. Run the Main and Drillthrough Reports  
Run the main report, and then click values in the product category column to run the drillthrough report.  
  
#### To run the reports  
  
1.  Open the SharePoint library where the reports are saved.  
  
2.  Double-click ResellerVSOnlineMain.  
  
    The report runs and displays product category sales information.  
  
3.  Click the **Games and Toys** link in the column that contains product category names.  
  
    The drillthrough report runs, displaying only the values for the Games and Toys product category.  
  
4.  To return to the main report, click the Internet Explorer back button.  
  
5.  Optionally, explore other product categories by clicking their names.  
  
## See Also  
[Report Builder tutorials](../reporting-services/report-builder-tutorials.md)  
  
