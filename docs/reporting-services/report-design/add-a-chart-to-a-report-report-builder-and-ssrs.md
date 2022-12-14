---
title: "Add a chart to a paginated report | Microsoft Docs"
description: Learn how to add a chart to a paginated report when you want to summarize data in a visual format in Report Builder.
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: a6b595dc-f775-4a53-8554-74a0bf9335ec
author: maggiesMSFT
ms.author: maggies
---
# Add a chart to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

When you want to summarize data in a visual format in a paginated report, use a Chart data region. It is important to choose an appropriate chart type for the type of data that you are presenting. This affects how well the data can be interpreted when put in chart form. For more information, see [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md).  
  
 The simplest way to add a Chart data region to your report is to run the New Chart Wizard. The wizard offers column, line, pie, bar, and area charts. For these and other chart types, you can also add a chart manually.  
  
 After you add a Chart data region to the design surface, you can drag report dataset fields for numeric and non-numeric data to the Chart Data pane of the chart. Click the chart to display the Chart Data pane with its three areas: Series Groups, Category Groups, and Values.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To add a chart to a report by using the Chart Wizard  
  
1.  > [!NOTE]  
    >  The Chart Wizard is only available in Report Builder.  
  
     On the **Insert** tab, click **Chart**, and then click **Chart Wizard**.  
  
2.  Follow the steps in the **New** Chart wizard.  
  
3.  On the **Home** tab, click **Run** to see the rendered report.  
  
4.  On the **Run** tab, click **Design** to continue working on the report.  
  
## To add a chart to a report  
  
1.  Create a report and define a dataset. For more information, see [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md).  
  
2.  On the **Insert** tab, select **Chart**, and then select **Insert Chart**.  

  
3.  Select the design surface where you want the upper-left corner of the chart, and then drag to where you want the lower-right corner of the chart.  
  
     The **Select Chart Type** dialog box appears.  
  
4.  Select the type of chart you want to add. Select **OK**.
  
5.  Click the chart to display the **Chart Data** pane.  
  
6.  Add one or more fields to the **Values** area. This information will be plotted on the value axis.  
  
7.  Add a grouping field to the **Category Groups** area. When you add this field to the **Category Groups** area, a grouping field is automatically created for you. Each group represents a data point in your series.  
  
8.  To summarize the data by category, right-click the data field and click **Series Properties**. In the **Category** box, select the category field from the drop-down list. Select **OK**.
  
9. On the **Home** tab, click **Run** to see the rendered report.  
  
10. On the **Run** tab, click **Design** to continue working on the report.  
  
 On charts with axes, such as bar and column charts, the category axis may not display all the category labels. For more information about how to change the axis labels, see [Specify an Axis Interval &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specify-an-axis-interval-report-builder-and-ssrs.md).  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Chart Types &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-types-report-builder-and-ssrs.md)   
 [Empty and Null Data Points in Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/empty-and-null-data-points-in-charts-report-builder-and-ssrs.md)   
 [Tutorial: Adding a Bar Chart to Your Report (Report Builder)](../tutorial-add-a-bar-chart-to-your-report-report-builder.md)   
 [Tutorial: Adding a Bar Chart to a Report (Report Designer)](/previous-versions/sql/sql-server-2008-r2/cc281302(v=sql.105))   
 [Tutorial: Adding a Pie Chart to Your Report (Report Builder)](../tutorial-add-a-pie-chart-to-your-report-report-builder.md)   
 [Tutorial: Adding a Pie Chart to a Report (Report Designer)](/previous-versions/sql/sql-server-2008-r2/cc281304(v=sql.105))  
  
