---
title: "Tutorial: Add a pie chart to your report (Report Builder)"
description: Learn how to create a pie chart in a Reporting Services paginated report, add percentages, and combine small slices into a single slice.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/15/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Tutorial: Add a pie chart to your report (Report Builder)
In this tutorial, you create pie chart in a Reporting Services paginated report. You add percentages and combine small slices into a single slice.

Pie and doughnut charts display data as a proportion of the whole. They have no axes. When you add a numeric field to a pie chart, the chart calculates the percentage of each value to the total.  

This illustration shows the pie chart you create in this tutorial. 

:::image type="content" source="../reporting-services/media/report-builder-pie-chart-final.png" alt-text="Screenshot of the report builder pie chart.":::
  
If there are too many data points on a pie chart, your data point labels might be too crowded to read. In that case, consider combining many small slices into one larger slice. Pie charts are more readable when you have aggregated your data into a few data points.  
 
> [!NOTE]  
> In this tutorial, the steps for the wizard are consolidated into two procedures. For step-by-step instructions about how to browse to a report server, add a data source, and add a dataset, see the first tutorial in this series: [Tutorial: Create a basic table report &#40;Report Builder&#41;](../reporting-services/tutorial-creating-a-basic-table-report-report-builder.md).  
  
Estimated time to complete this tutorial: 10 minutes  
  
## Requirements  
For information about requirements, see [Prerequisites for tutorials &#40;Report Builder&#41;](../reporting-services/prerequisites-for-tutorials-report-builder.md).  
  
## <a name="Chart"></a>1. Create a pie chart from the Chart Wizard  
In this section, you use the Chart Wizard to create an embedded dataset, choose a shared data source, and create a pie chart.  

  
1.  [Start Report Builder](../reporting-services/report-builder/start-report-builder.md) either from your computer, the [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] web portal, or SharePoint integrated mode.  
  
    The **New Report or Dataset** dialog box opens.  
  
    If you don't see the **New Report or Dataset** dialog box, on the **File** menu > **New**.  
  
2.  In the left pane, verify that **New Report** is selected.  
  
3.  In the right pane, choose **Chart Wizard**.  
  
4.  On the **Choose a dataset** page, select **Create a dataset**, and then choose **Next**.  
  
5.  On the **Choose a connection to a data source** page, select an existing data source or browse to the report server and choose a data source, and then select **Next**. You might need to enter a user name and password.  
  
    > [!NOTE]  
    > The data source you choose is unimportant, as long as you have adequate permissions. You will not be getting data from the data source. For more information, see [Alternative ways to get a data connection &#40;Report Builder&#41;](../reporting-services/alternative-ways-to-get-a-data-connection-report-builder.md).  
  
6.  On the **Design a Query** page, select **Edit as Text**.  
  
7.  Paste the following query into the query pane:  

    > [!NOTE]  
    > In this tutorial, the query contains the data values, so it does not need an external data source. This makes the query long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
    ```  
    SELECT 'Advanced Digital Camera' AS Product, CAST(254995.21 AS money) AS Sales  
    UNION SELECT 'Slim Digital Camera' AS Product, CAST(164499.04 AS money) AS Sales  
    UNION SELECT 'SLR Digital Camera' AS Product, CAST(782176.79 AS money) AS Sales  
    UNION SELECT 'Lens Adapter' AS Product, CAST(36333.08 AS money) AS Sales  
    UNION SELECT 'Macro Zoom Lens' AS Product, CAST(40199.3 AS money) AS Sales  
    UNION SELECT 'USB Cable' AS Product, CAST(53245.5 AS money) AS Sales  
    UNION SELECT 'Independent Filmmaker Camcorder' AS Product, CAST(452288.0 AS money) AS Sales  
    UNION SELECT 'Full Frame Digital Camera' AS Product, CAST(247250.85 AS money) AS Sales  
    ```  
  
8.  (Optional) Select the Run button (**!**) to see the data your chart is based on.  
  
9. Select **Next**.  
  
## <a name="ChartType"></a>2. Choose the chart type  
You can choose from various predefined chart types.  

  
1.  On the **Choose a chart type** page, select **Pie**, then choose **Next**. The **Arrange chart fields** page opens.  
  
    On the **Arrange chart fields** page, drag the Product field to the **Categories** pane. Categories define the number of slices in the pie chart. In this example, there are eight slices, one for each product.  
  
2.  Drag the Sales field to the **Values** pane. Sales represents the sales amount for the subcategory. The **Values** pane displays `[Sum(Sales)]` because the chart displays the aggregate for each product.  
  
3.  Select **Next** to see a preview.  
  
5.  Select **Finish**.  
  
    The chart is added to the design surface. You don't see the actual values of the pie chart. You see Product 1, Product 2, etc., to give an idea of how the chart should look.  

    :::image type="content" source="../reporting-services/media/report-builder-pie-chart-first-design.png" alt-text="Screenshot of the report builder pie chart in the design view.":::
  
6.  Select the chart to display the chart handles. Drag the bottom-right corner of the chart to make it bigger. The report design surface also gets bigger, to accommodate the chart size.  
  
7.  Select **Run** to preview the report.  
  
The report displays the pie chart with eight slices, one for each product. Now you see the actual products and the size of each slice represents the sales for that product. Three of the slices are thin.  

:::image type="content" source="../reporting-services/media/report-builder-pie-chart-first-preview.png" alt-text="Screenshot that shows a preview of the report builder pie chart.":::
  
## <a name="Percentages"></a>3. Display percentages in each slice  
On each slice of the pie, you can display a percentage for this slice compared to the whole pie.  

  
1.  Switch to report design view.  
  
2.  Right-click the pie chart and select **Show Data Labels**. The data labels appear on the chart.  
  
3.  Right-click a label, then select **Series Label Properties**.  
  
4.  In the **Label data** box, select **#PERCENT**.  
    
5.  (Optional) To specify how many decimal places the label shows, in the **Label data** box after **#PERCENT**, enter **{Pn}** where *n* is the number of decimal places to display. For example, to display no decimal places, enter **#PERCENT{P0}**.  

6.  To display values as percentages, the UseValueAsLabel property must be false. If you're prompted to set this value in the **Confirm Action** dialog, select **Yes**.  
  
    > [!NOTE]  
    > **Number Format** in the **Series Label Properties** dialog box has no effect when you format percentages. This formats the labels as percentages, but does not calculate the percentage of the pie that each slice represents.  
  
6.  Select **OK**.
  
7.  Select **Run** to preview the report.  
  
The report displays the percentage of the whole for each pie slice.  

:::image type="content" source="../reporting-services/media/report-builder-pie-chart-preview-percents.png" alt-text="Screenshot that shows a preview of the report builder pie chart with percentages in each slice.":::
  
## <a name="CombineSlices"></a>4. Combine small slices into one slice  
Three of the slices in the pie are small. You can combine multiple small slices into one larger "Other" slice that represents all three.  

1.  Switch to report design view.  
  
2.  If the Properties pane isn't showing, on the **View** tab > **Show/Hide** group > select **Properties**.  
  
3.  On the design surface, select on any slice of the pie chart. The properties for the series are displayed in the Properties pane.  
  
4.  In the **General** section, expand the **CustomAttributes** node.  
  
5.  Set the **CollectedStyle** property to **SingleSlice**.  

    :::image type="content" source="../reporting-services/media/report-builder-pie-chart-single-slice-property.png" alt-text="Screenshot that shows how to set a property of a single slice in the report builder pie chart.":::
 
6.  Verify that the **CollectedThreshold** property is set to 5.  
  
7.  Verify that the **CollectedThresholdUsePercent** property is set to **True**.  
  
8.  On the **Home** tab, select **Run** to preview the report.  
  
In the legend, you now see the category "Other". The new pie slice combines all the slices that were under 5% into one slice that is 6% of the whole pie.  

:::image type="content" source="../reporting-services/media/report-builder-pie-chart-start-at-90.png" alt-text="Screenshot that shows how the report builder pie chart starts at 90 degrees from the top of the chart.":::
 
## <a name="DrawingEffect"></a>5. Start pie chart values at the top 

By default in pie charts, the first value in the dataset starts at 90 degrees from the top of the pie. You see that in the pie chart in the previous sections.

In this section, you make the first value start at the top.

1.  Switch to report design view.  

2. Select the pie itself.

3. In the Properties pane, under **Custom Attributes**, change PieStartAngle from **0** to **270**.

4. Select **Run** to preview your report.

Now the pie chart slices are in alphabetical order, starting at the top, and ending with the "Other" slice.

:::image type="content" source="../reporting-services/media/report-builder-pie-chart-start-at-top.png" alt-text="Screenshot that shows how the report builder pie chart starts at the top.":::
  
## <a name="Title"></a>6. Add a report title  
  
Because the pie chart is the only visualization in the report, the chart doesn't need its own title. The report title is fine.
  
1.  In the chart, select the Chart Title box and press DELETE.

2. In the design surface, select **Click to add title**.  
  
2.  Enter **Camera and Camcorder Sales**, press ENTER, and then enter **As a Percentage of Total Sales**, so it looks like this:  
  
    **Camera and Camcorder Sales**  
  
    **As a Percentage of Total Sales**  
  
3.  Select **Camera and Camcorder Sales**, and on the **Home** tab > **Font** section > choose **Bold**.  
  
4.  Select **As a Percentage of Total Sales**, and on the **Home** tab > **Font** section > set the font size to **10**.  
  
5.  (Optional) You might need to make the Title text box taller to accommodate the two lines of text.  
  
    This title appears at the top of the report. When there's no page header defined, items at the top of the report body are the equivalent of a report header.  
  
6.  Select **Run** to preview the report.  
  
## <a name="Save"></a>7. Save the report  
  
### Save the report  
  
1.  Switch to report design view.  
  
2.  On the **File** menu, select **Save**.  
  
3.  In **Name**, enter **Sales Pie Chart**.  
  
4.  Select **Save**.  
  
Your report is saved on the report server.  
  
## Next steps
You successfully completed the Adding a Pie Chart to Your Report tutorial. For more information about charts, see [Charts in a paginated report (Report Builder)](../reporting-services/report-design/charts-report-builder-and-ssrs.md) and [Sparklines and data bars in a paginated report (Report Builder)](../reporting-services/report-design/sparklines-and-data-bars-report-builder-and-ssrs.md).  
  
## Related content

- [Report Builder tutorials](../reporting-services/report-builder-tutorials.md)  
- [Report Builder in SQL Server](../reporting-services/report-builder/report-builder-in-sql-server-2016.md)  
  

