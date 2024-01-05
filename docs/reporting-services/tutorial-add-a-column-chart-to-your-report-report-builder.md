---
title: "Tutorial: Add a column chart to your report (Report Builder)"
description: Learn how to create a Reporting Services paginated report with a column chart displaying a series as a set of vertical bars grouped by category.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/02/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Tutorial: Add a column chart to your report (Report Builder)
In this tutorial, you create a [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] paginated report with a column chart displaying a series as a set of vertical bars grouped by category. 

Column charts are useful to:  
  
-   Show data changes over a period of time.  
-   Compare the relative value of multiple series.  
-   Display a moving average to show trends.  
  
The following illustration shows the column chart you create in this tutorial, with a moving average.  

:::image type="content" source="../reporting-services/media/report-builder-column-chart-tutorial.png" alt-text="Screenshot that shows a report builder column chart.":::

> [!NOTE]  
> In this tutorial, the steps for the wizard are consolidated into one procedure. For step-by-step instructions about how to browse to a report server, choose a data source, and create a dataset, see the first tutorial in this series: [Tutorial: Create a basic table report &#40;Report Builder&#41;](../reporting-services/tutorial-creating-a-basic-table-report-report-builder.md).  
  
Estimated time to complete this tutorial: 15 minutes.  
  
## Requirements  
For information about requirements, see [Prerequisites for tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="Chart"></a>1. Create a chart report from the Chart Wizard  
In this section, you go through the Chart Wizard to create an embedded dataset, choose a shared data source, and create a column chart.  
  
> [!NOTE]  
> The query in this tutorial contains the data values, so it doesn't need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
### Create a chart report  
  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, select **Chart Wizard**.  
  
4.  On the **Choose a dataset page**, select **Create a dataset**, and then choose **Next**.  
  
5.  On the **Choose a connection to a data source** page, select an existing data source. Or browse to the report server, and choose a data source. Then select **Next**. You might need to enter a user name and password.  
  
    > [!NOTE]  
    > The data source you choose is unimportant, as long as you have adequate permissions. You don't get data from the data source. For more information, see [Alternative ways to get a data connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
6.  On the **Design a query** page, select **Edit as Text**.  
  
7.  Paste the following query into the query pane:  
  
    ```  
    SELECT CAST('2015-01-01' AS date) AS SalesDate, CAST(54995.21 AS money) AS Sales  
    UNION SELECT CAST('2015-01-05' AS date) AS SalesDate, CAST(64499.04 AS money) AS Sales  
    UNION SELECT CAST('2015-02-11' AS date) AS SalesDate, CAST(37821.79 AS money) AS Sales  
    UNION SELECT CAST('2015-03-18' AS date) AS SalesDate, CAST(53633.08 AS money) AS Sales  
    UNION SELECT CAST('2015-04-23' AS date) AS SalesDate, CAST(24019.3 AS money) AS Sales  
    UNION SELECT CAST('2015-05-01' AS date) AS SalesDate, CAST(93245.5 AS money) AS Sales  
    UNION SELECT CAST('2015-06-06' AS date) AS SalesDate, CAST(55288.0 AS money) AS Sales  
    UNION SELECT CAST('2015-06-16' AS date) AS SalesDate, CAST(68733.5 AS money) AS Sales  
    UNION SELECT CAST('2015-07-16' AS date) AS SalesDate, CAST(24750.85 AS money) AS Sales  
    UNION SELECT CAST('2015-08-23' AS date) AS SalesDate, CAST(43452.3 AS money) AS Sales  
    UNION SELECT CAST('2015-09-24' AS date) AS SalesDate, CAST(58656. AS money) AS Sales  
    UNION SELECT CAST('2015-10-15' AS date) AS SalesDate, CAST(44583. AS money) AS Sales  
    UNION SELECT CAST('2015-11-21' AS date) AS SalesDate, CAST(81568. AS money) AS Sales  
    UNION SELECT CAST('2015-12-15' AS date) AS SalesDate, CAST(45973. AS money) AS Sales  
    UNION SELECT CAST('2015-12-26' AS date) AS SalesDate, CAST(96357. AS money) AS Sales  
    UNION SELECT CAST('2015-12-31' AS date) AS SalesDate, CAST(81946. AS money) AS Sales  
    ```  
  
8.  (Optional) Select the Run button (**!**) to see the data your chart is based on.  
  
9. Select **Next**.  
  
## <a name="ChartType"></a>2. Choose the chart type  
You can choose from several predefined chart types, and then modify the chart after you complete the wizard.  
  
### Add a column chart  
  
1.  On the **Choose a chart type** page, the column chart is the default chart type. Select **Next**.  
  
2.  On the **Arrange chart fields** page, drag the SalesDate field to **Categories**. Categories display on the horizontal axis.  
  
3.  Drag the Sales field to **Values**. The **Values** box displays Sum(Sales) because the sum of the sales total value is aggregated for each date. Values display on the vertical axis.  
  
4.  Select **Next**.  
 
6.  Select **Finish**.  
  
    The chart is added to the design surface. The new column chart just shows representational data. The legend reads Sales Date A through F, just to give an idea of what your report should like. 
    
    :::image type="content" source="../reporting-services/media/report-builder-column-chart-1-design-view.png" alt-text="Screenshot of a Report Builder column in the design view.":::
  
7.  Select the chart to display the chart handles. Drag the bottom-right corner of the chart to increase the size of the chart. The report design surface increases in size to accommodate the chart size.  
  
8.  Select **Run** to preview the report.  

    :::image type="content" source="../reporting-services/media/report-builder-column-chart-1-preview.png" alt-text="Screenshot that shows a preview of the Report Builder column chart.":::

The chart doesn't label every category on the horizontal axis. By default, only labels that fit next to the axis are included. 
  
## <a name="Horizontal"></a>3. Format a date on the horizontal axis  
By default, the horizontal axis displays values in a general format that is automatically scaled to fit the size of the chart.  
  
1.  Switch to report design view.  
  
2.  Right-click the horizontal axis > **Horizontal Axis Properties**.  
  
3.  On the **Number** tab, in **Category**, select **Date**.  
  
5.  In the **Type** box, select **31 Jan 2000**.  
  
6.  Select **OK**.
  
7.  On the Home tab, select **Run** to preview the report.  
  
The date displays in the date format that you selected. The chart still doesn't label every category on the horizontal axis. 

:::image type="content" source="../reporting-services/media/report-builder-column-chart-2-preview.png" alt-text="Screenshot of the Report Builder column chart displaying the date format.":::
  
You can customize the label display by rotating the labels and specifying the interval.  
  
## 4. Rotate the axis labels on the horizontal axis  
  
1.  Switch to report design view.  
  
2.  Right-click the horizontal axis title, then select **Show Axis Title** to remove the title. Because the horizontal axis displays dates, the title isn't needed.  
  
3.  Right-click the horizontal axis > **Horizontal Axis Properties**.  
  
5.  On the **Labels** tab, under **Change axis label auto-fit options**, select **Disable auto-fit**.  
  
7.  In **Label rotation angle**, select **-90**.  
  
8.  Select **OK**.
  
    The sample text for the horizontal axis rotates by 90 degrees.  
    
    :::image type="content" source="../reporting-services/media/report-builder-column-chart-rotate-x-axis.png" alt-text="Screenshot of the Report Builder column chart with rotated x-axis text in the design view.":::
  
9. Select **Run** to preview the report.  
  
On the chart, the labels are rotated.  

:::image type="content" source="../reporting-services/media/report-builder-column-chart-rotate-x-axis-preview.png" alt-text="Screenshot that shows a preview of the Report Builder column chart with rotated x-axis text.":::
  
## <a name="Legend"></a>5. Move the legend  
The legend is automatically created from category and series data. You can move the legend below the chart area of a column chart.  
  
1.  Switch to report design view.  
  
2.  Right-click the legend on the chart > **Legend Properties**.  
  
3.  Under **Layout and Position**, select a different position. For example, select the bottom middle option.  
  
    When the legend is placed at the top or bottom of a chart, the layout of the legend changes from vertical to horizontal. You can select a different layout in the **Layout** box.  
  
4.  Select **OK**.
  
5.  (Optional) Because there's only one category in this tutorial, the chart doesn't need a legend. To remove it, right-click the legend > **Delete Legend**.  
  
6.  Select **Run** to preview the report.  
  
## <a name="ChartTitle"></a>6. Title the chart  
    
1.  Switch to report design view.  
  
2.  Select the words **Chart Title** at the top of the chart, then enter **Store Sales Order Totals**.  
  
3.  Select **Run** to preview the report.  
  
## <a name="Vertical"></a>7. Format and label the vertical axis  
By default, the vertical axis displays values in a general format that is automatically scaled to fit the size of the chart.   
  
1.  Switch to report design view.  
  
2. Select the labels on the vertical axis on the left side of the chart to select them.  
  
3.  On the **Home** tab > **Number** group, select the **Currency** button. The axis labels change to show the currency format.  
  
4.  Select the **Decrease Decimal** button two times, to show the number rounded to the nearest dollar.  
  
5.  Right-click the vertical axis > **Vertical Axis Properties**.  
  
6.  On the **Number** tab, **Currency** is already selected in the **Category** box, and **Decimal places** is already **0** (zero).  
  
7.  Check **Show Values in**. **Thousands** is already selected.  
  
8.  Select **OK**.
  
9. Right-click the vertical axis > **Show Axis Title**. 

10. Right-click the vertical axis title > **Axis Title Properties**.  
  
10. Replace the text in the **Title text** field with **Sales Total (in Thousands)**. You can also specify various options related to how the title is formatted.  
  
11. Select **OK**.
  
12. Select **Run** to preview the report.  

    :::image type="content" source="../reporting-services/media/report-builder-column-chart-format-y-axis.png" alt-text="Screenshot that shows a preview of the Report Builder column chart format with the y-axis labeled correctly.":::
    
## 8. Show all the labels on the horizontal (x) axis

You notice that only some of the labels on the x axis are showing. In this section, you set a property in the Properties pane to show them all.

1.  Switch to report design view.  
  
2.  Select the chart, then choose the horizontal axis labels.

3. In the Properties pane, set LabelInterval to 1.

    :::image type="content" source="../reporting-services/media/report-builder-column-chart-set-label-interval.png" alt-text="Screenshot that shows how to set the label interval of the Report Builder column chart.":::

    The chart looks the same in design view. 
    
5.  Select **Run** to preview the report.

    :::image type="content" source="../reporting-services/media/report-builder-column-chart-label-interval-one-preview.png" alt-text="Screenshot that shows a preview of the Report Builder column chart with the label interval set to one.":::
    
    Now the chart displays all its labels.
  
## <a name="Average"></a>9. Add a moving average with a calculated series  

A moving average is an average of the data in your series, calculated over time. The moving average can identify trends.
  
1.  Switch to report design view.  
  
2.  Double-click the chart to display the **Chart Data** pane.  
  
3.  Right-click the **[Sum(Sales)]** field in the **Values** area, then select **Add Calculated Series**.  

    :::image type="content" source="../reporting-services/media/report-builder-column-chart-add-calculated-series.png" alt-text="Screenshot that shows how to add calculated series to the Report Builder column chart.":::
  
4.  In **Formula**, verify that **Moving average** is selected.  
  
5.  In **Set Formula Parameters**, for **Period**, select **4**.  
  
6.  On the **Border** tab, in **Line width**, select **3pt**.  
  
8.  Select **OK**.
  
9. Select **Run** to preview the report.  
  
The chart displays a line that shows the moving average for total sales by date, averaged over every four dates. For more information, see [Add a moving average to a chart in a paginated report (Report Builder)](../reporting-services/report-design/add-a-moving-average-to-a-chart-report-builder-and-ssrs.md).

:::image type="content" source="../reporting-services/media/report-builder-column-chart-moving-average.png" alt-text="Screenshot of a Report Builder column chart depicting a moving average.":::
  
## <a name="Title"></a>10. Add a report title  
  
1.  Switch to report design view.  
  
2.  On the design surface, select **Click to add title**.  
  
3.  Enter **Sales Chart**. Press ENTER, and then enter **January to December 2015**, so it looks like this:  
  
    **Sales Chart**  
  
    **January to December 2015**  
  
4.  Select **Sales Chart**, and on the **Home** tab > **Font** section > **Bold**.  
  
5.  Select **January to December 2015**, and on the **Home** tab > **Font** section >  set font size to **10**.  
  
6.  (Optional) You might need to make the **Title** text box taller to accommodate the two lines of text. Pull down on the double-headed arrows when you select in the middle of the bottom edge. And you might need to drag the top of the chart so the title doesn't overlap.  
  
    This title appears at the top of the report. When there's no page header defined, items at the top of the report body are the equivalent of a report header.  
  
7.  Select **Run** to preview the report.  
  
## <a name="Save"></a>11. Save the report
  
1.  Switch to report design view.  
  
2.  From the Report Builder button, select **Save As**.  

    You can save it either to your computer or to the report server.
  
3.  In **Name**, enter **Sales Order Column Chart**.  
  
4.  Select **Save**.  
  
## Next steps  
You successfully completed the Adding a Column Chart to Your Report tutorial. For more information about charts, see [Charts in a paginated report (Report Builder)](../reporting-services/report-design/charts-report-builder-and-ssrs.md) and [Sparklines and data bars in a paginated report (Report Builder)](../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md).  
  
## Related content

- [Report Builder tutorials](../reporting-services/report-builder-tutorials.md) 
- [Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)  
  

