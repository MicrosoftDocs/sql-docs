---
title: "Tutorial: Add a bar chart to your report (Report Builder)"
description: Learn how to use a wizard in Report Builder to create a bar chart in a Reporting Services paginated report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Tutorial: Add a bar chart to your report (Report Builder)
In this tutorial, you use a wizard in [!INCLUDE[ssRBnoversion_md](../includes/ssrbnoversion.md)] to create a bar chart in a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated report. Then you add a filter and enhance the chart. 

A bar chart displays category data horizontally. This visualization can help to:  
  
-   Improve readability of long category names.  
-   Improve understandability of times plotted as values.   
-   Compare the relative value of multiple series.  
  
The following illustration shows the bar chart that you create in this tutorial. It shows the sales for 2014 and 2015 for the top five salespeople, from most to least 2015 sales.  

:::image type="content" source="../reporting-services/media/report-builder-bar-chart.png" alt-text="Screenshot of a report builder bar chart.":::


  
 
> [!NOTE]  
> In this tutorial, the steps for the wizard are consolidated into one procedure. For step-by-step instructions about how to browse to a report server, create a dataset, and choose a data source, see the first tutorial in this series: [Tutorial: Create a basic table report &#40;Report Builder&#41;](../reporting-services/tutorial-creating-a-basic-table-report-report-builder.md).  
  
Estimated time to complete this tutorial: 15 minutes.  
  
## Requirements  
For more information about requirements, see [Prerequisites for tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="Chart"></a>1. Create a chart report from the Chart Wizard  
In which you create an embedded dataset, choose a shared data source, and create a bar chart by using the Chart Wizard.  
  
> [!NOTE]  
> In this tutorial, the query contains the data values so that it does not need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) from the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] web portal, from the report server in SharePoint integrated mode, or from your computer.  
  
     The **Getting Started** dialog box appears.  
  
    :::image type="content" source="media/tutorial-add-a-bar-chart-to-your-report-report-builder/report-builder-get-started.png" alt-text="Screenshot of Report Builder Get Started dialog box.":::
  
     If you don't see the **Getting Started** dialog box, select **File** >**New**. The **New Report or Dataset** dialog box has most of the same contents as the **Getting Started** dialog box. 
      
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, select **Chart Wizard**.  
  
4.  On the **Choose a dataset** page, select **Create a dataset**, and then choose **Next**.  
  
5.  On the **Choose a connection to a data source** page, select an existing data source. Or browse to the report server, and choose a data source. Then select **Next**. You might need to enter a user name and password.  
  
    > [!NOTE]  
    > The data source you choose is unimportant, as long as you have adequate permissions. You will not be getting data from the data source. For more information, see [Alternative ways to get a data connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
6.  On the **Design a query** page, select **Edit as Text**.  
  
7.  Paste the following query into the query pane:  
  
    ```  
    SELECT 'Luis' as FirstName, 'Alverca' as LastName, CAST(170000.00 AS money) AS SalesYear2015, CAST(150000. AS money) AS SalesYear2014  
    UNION SELECT 'Jeffrey' as FirstName, 'Zeng' as LastName, CAST(210000. AS money) AS SalesYear2015, CAST(190000. AS money) AS SalesYear2014  
    UNION SELECT 'Houman' as FirstName, 'Pournasseh' as LastName, CAST(150000. AS money) AS SalesYear2015, CAST(180000. AS money) AS SalesYear2014  
    UNION SELECT 'Robin' as FirstName, 'Wood' as LastName, CAST(75000. AS money) AS SalesYear2015, CAST(175000. AS money) AS SalesYear2014  
    UNION SELECT 'Daniela' as FirstName, 'Guaita' as LastName,  CAST(170000. AS money) AS SalesYear2015, CAST(175000. AS money) AS SalesYear2014  
    UNION SELECT 'John' as FirstName, 'Yokim' as LastName, CAST(160000. AS money) AS SalesYear2015, CAST(195000. AS money) AS SalesYear2014  
    UNION SELECT 'Delphine' as FirstName, 'Ribaute' as LastName, CAST(180000. AS money) AS SalesYear2015, CAST(205000. AS money) AS SalesYear2014  
    UNION SELECT 'Robert' as FirstName, 'Hernady' as LastName, CAST(140000. AS money) AS SalesYear2015, CAST(180000. AS money) AS SalesYear2014  
    UNION SELECT 'Tanja' as FirstName, 'Plate' as LastName, CAST(150000. AS money) AS SalesYear2015, CAST(160000. AS money) AS SalesYear2014  
    UNION SELECT 'David' as FirstName, 'Bradley' as LastName, CAST(210000. AS money) AS SalesYear2015, CAST(180000. AS money) AS SalesYear2014  
    UNION SELECT 'Michal' as FirstName, 'Jaworski' as LastName, CAST(175000. AS money) AS SalesYear2015, CAST(220000. AS money) AS SalesYear2014  
    UNION SELECT 'Chris' as FirstName, 'Ashton' as LastName, CAST(195000. AS money) AS SalesYear2015, CAST(205000. AS money) AS SalesYear2014  
    UNION SELECT 'Pongsiri' as FirstName, 'Hirunyanitiwatna' as LastName, CAST(175000. AS money) AS SalesYear2015, CAST(215000. AS money) AS SalesYear2014  
    UNION SELECT 'Brian' as FirstName, 'Burke' as LastName, CAST(187000. AS money) AS SalesYear2015, CAST(207000. AS money) AS SalesYear2014  
    ```  
  
8.  (Optional) Select the Run button (**!**) to see the data your chart is based on.  
  
9. Select **Next**.  
  
## <a name="ChartType"></a>2. Create a bar chart  
 
1.  On the **Choose a chart type** page, the column chart is the default chart type.  
  
2.  Select **Bar**, and then choose **Next**.  
  
    On the **Arrange chart fields** page, there are four fields in the **Available fields** pane: FirstName, LastName, SalesYear2015, and SalesYear2014.  
  
3.  Drag LastName to the Categories pane.  
  
4.  Drag SalesYear2015 to the Values pane. SalesYear2015 represents the sales amount for each salesperson for the year 2015. The Values pane displays `[Sum(SalesYear2015)]` because the chart displays the aggregate for each product.  
  
5.  Drag SalesYear2014 to the Values pane under SalesYear2015. SalesYear2014 represents the sales amount for each salesperson for the year 2014.  
  
6.  Select **Next**.  
  
7.  Select **Finish**.  
  
    The chart is added to the design surface. The new bar chart just shows representational data. The legend reads Last Name A, Last Name B, etc., rather than the people's names, just to give an idea of what your report should look like. 
  
9. Select the chart to display the chart handles. Drag the bottom-right corner of the chart to increase the size of the chart. Notice the design surface gets larger as you drag. 
  
10. Select **Run** to preview the report.  
  
The bar chart displays sales for each sales person for the years 2014 and 2015. The length of the bar corresponds to the sales total.  
  
## <a name="AllValues"></a>3. Display all the names on the vertical axis  
By default, only some of the values on the vertical axis appear. You can change the chart to display all categories.  
  
1.  Switch to report design view.  
  
2.  Right-click the vertical axis, then select **Vertical Axis Properties**.  
  
3.  Under **Axis range and interval**, in the **Interval** box, type **1**.  
  
4.  Select **OK**.
  
5.  Select **Run** to preview the report.  
  
> [!NOTE]  
> If you cannot read the salesperson names on the vertical axis, you can make your chart taller or change the formatting options for the axis labels.  
  
### <a name="CategoryExpression"></a>Display last name and first name on vertical axis  
You can change the category expression to include last name followed by first name of each sales person.  
  
1.  Switch to report design view.  
  
2.  Double-click the chart to display the **Chart Data** pane.  
  
3.  In the **Category Groups** area, right-click [LastName], and then select **Category Group Properties**.  
  
4.  In Label, select the expression (Fx) button.  
  
5.  Type the following expression: `=Fields!LastName.Value & ", " & Fields!FirstName.Value`  
  
    This expression concatenates the last name, a comma, and the first name.  
  
6.  Select **OK**.
  
7.  Select **OK**.
  
8.  Select **Run** to preview the report.  
  
If the first names don't appear when you run the report, you can refresh the data manually. While still in preview mode, on the **Run** tab in the **Navigation** group, select **Refresh**.  
  
> [!NOTE]  
> If you cannot read the salesperson names on the vertical axis, you can make your chart taller or change the formatting options for the axis labels.  
  
## <a name="Sort"></a>4. Change the sort order on the vertical axis  
When you sort the data on a chart, you're changing the order of values on the category axis.  
  
1.  Switch to report design view.  
  
2.  Double-click the chart to display the **Chart Data** pane.  
  
3.  In the **Category Groups** area, right-click [LastName], and then select **Category Group Properties**.  
  
4.  Select **Sorting**. The **Change sorting options** page displays a list of sort expressions. By default, this list has one sort expression that is the same as the original category group expression.  
  
5.  In **Sort by**, select **[SalesYear2015]**.  
  
6.  in the **Order** list, select **A to Z** so that the names appear in order from largest to smallest 2015 sales.
  
7.  Select **OK**.
  
10. Select **Run** to preview the report.  
  
The names on the horizontal axis are sorted from largest to smallest 2015 sales, with **Zeng** at the top.  
  
## <a name="Legend"></a>5. Move the legend  
To improve the readability of the chart values, you might want to move the chart legend. For example, in a bar chart where bars are shown horizontally, you can change the position of the legend so that it is above the chart or below the chart area. This positioning gives more horizontal space to the bars.  
  
#### Display the legend below the chart area of a bar chart  
  
1.  Switch to report design view.  
  
2.  Right-click the legend on the chart.  
  
3.  Select **Legend Properties**.  
  
4.  For **Legend position**, select a different position. For example, set the position to the middle bottom option.  
  
    When the legend is placed at the top or bottom of a chart, the layout of the legend changes from vertical to horizontal. You can select a different layout from the **Layout** drop-down list.  
  
5.  Select **OK**.
  
6.  Select **Run** to preview the report.  
  
## <a name="ChartTitle"></a>6. Title the chart  
  
1.  Switch to report design view.  
  
2.  Select the words **Chart Title** at the top of the chart, then enter: **Sales for 2014 and 2015**.  
  
3.  In the Properties pane, with the title selected, set **Color** to **Black** and **FontSize** to **12pt**. 
  
4.  Select **Run** to preview the report.  
  
## <a name="Horizontal"></a>7. Format and label the horizontal axis  
By default, the horizontal axis displays values in a general format that is automatically scaled to fit the size of the chart. You can change it to the currency format.  
   
1.  Switch to report design view.  
  
2.  Select the horizontal axis along the bottom of the chart to select it.  
  
3.  On the **Home** tab, go to **Number** group > **Currency**. The horizontal axis labels change to currency.  
  
3.  (Optional) Remove the decimal digits. Near the **Currency** button, select the **Decrease Decimal** button twice.  
  
4.  Right-click the horizontal axis, and select **Horizontal Axis Properties**.  
  
5.  On the **Number** tab, select **Show values in Thousands**.  
  
6.  Select **OK**.

8.  Right-click the horizontal axis, and select **Show Axis Title**.
  
7.  In the **Axis Title** box, enter **Sales in thousands** and press Enter.  

    > [!NOTE]  
    > While you're entering, the Axis Title box appears to be on the vertical axis. But when you press Enter, it goes to the horizontal axis.
  
9. Select **Run** to preview the report.  
  
The report displays the sales amount on the horizontal axis as currency in thousands, with no decimal digits.  
  
## <a name="Filter"></a>8. Add a filter to display the top five values  
You can add a filter to the chart to specify which data from the dataset to include or exclude in the chart.   
  
1.  Switch to report design view.  
  
2.  Double-click the chart to display the **Chart Data** pane.  
  
3.  In the **Category Groups** area, right-click the [LastName] field, and then select **Category Group Properties**.  
  
4.  Select **Filters**. The **Change filters** page can display a list of filter expressions. By default, this list is empty.  
  
5.  Select **Add**. A new blank filter appears.  
  
6.  In **Expression**, enter **[Sum(SalesYear2015)]**. This expression creates the underlying expression `=Sum(Fields!SalesYear2015.Value)`, which you can see if you select the **fx** button.  
  
7.  Verify that the data type is **Text**.  
  
8.  In **Operator**, select **Top N** from the drop-down list.  
  
9. In **Value**, enter the following expression: **=5**  
  
10. Select **OK**.
  
11. Select **Run** to preview the report.  
  
If the results aren't filtered when you run the report, you can refresh the data manually. On the **Run** tab in the **Navigation** group, select **Refresh**.  
  
The chart shows the top five salesperson names from the 2015 sales data.  
  
## <a name="Title"></a>9. Add a report title  
  
1.  On the design surface, select **Click to add title**.  
  
2.  Enter **Sales Bar Chart**. Press ENTER, and then enter **Top Five Sellers for 2015**, so it looks like this:  
  
    **Sales Bar Chart**  
  
    **Top Five Sellers for 2015**  
  
3.  Select **Sales Bar Chart**, and choose the **Bold** button.  
  
4.  Select **Top Five Sellers for 2015**, and in the **Font** section on the **Home** tab, set the font size to **10**.  
  
5.  (Optional) You might need to make the Title text box taller, and bring down the top of the bar chart, to accommodate the two lines of text.  
  
    This title appears at the top of the report. When there's no page header defined, items at the top of the report body are the equivalent of a report header.  
  
6.  Select **Run** to preview the report.  
  
## <a name="Save"></a>10. Save the report  
  
1.  Switch to report design view.  
  
2.  Select **File** > **Save As**.  
  
3.  In **Name**, enter **Sales Bar Chart**.  

    You can save it either to your computer or to the report server.
  
4.  Select **Save**.   
  
## Related content

- [Report Builder tutorials](../reporting-services/report-builder-tutorials.md)
- [Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)
- [Charts in a paginated report (Report Builder)](../reporting-services/report-design/charts-report-builder-and-ssrs.md)
- [Bar charts in a paginated report (Report Builder)](../reporting-services/report-design/bar-charts-report-builder-and-ssrs.md)
