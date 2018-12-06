---
title: "Sort Data in a Data Region (Report Builder and SSRS) | Microsoft Docs"
ms.date: 08/17/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 2fcb9be2-1daa-4c92-ad00-5f63cdf39f70
author: maggiesMSFT
ms.author: maggies
---
# Sort Data in a Data Region (Report Builder and SSRS)
  To change the sort order of data in a data region when a report first runs, you must set the sort expression on the data region or group. By default, the sort expression for a group is automatically set to the same value as the group expression.  
  
-   In a tablix data region, set the sort expression for the data region or for each group, including the details group. If you have only one details group in a tablix data region, you can define a sort expression in the query, on the data region, or on the details group and they all have the same effect.  
  
-   In a chart data region, set the sort expression for the Category and Series groups to control the sort order for each group. The order for colors in a chart legend is determined by the sort expression for the data points in the Category group.  
  
-   In a gauge data region, you do not typically need to sort data because the gauge displays a single value relative to a range. If you do need sort data in a gauge, you must first define a group, and then set the sort expression for the group.  
  
 For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md).  
  
 For a tablix data region, you can also add an interactive sort button to the top of a column header to provide the user with the ability to change the sort order of groups or detail rows. For more information, see [Interactive Sort &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/interactive-sort-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To sort data in a Tablix data region  
  
1.  On the design surface, right-click a row handle, and then click **Tablix Properties**.  
  
2.  Click **Sorting**.  
  
3.  For each sort expression, follow these steps:  
  
    1.  Click **Add**.  
  
    2.  Type or select an expression by which to sort the data.  
  
    3.  From the **Order** column drop-down list, choose the sort direction for each expression. **A-Z** sorts the expression in ascending order. **Z-A** sorts the expression in descending order.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To sort values in a group, including the details group, for a Tablix  
  
1.  On the design surface, click in the tablix data region to select it. The Grouping pane displays the row groups and column groups for the Tablix data region.  
  
2.  In the Row Groups pane, right-click the group name, and then click **Edit Group**.  
  
3.  In the **Tablix Group** dialog box, click **Sort**.  
  
4.  For each sort expression, follow these steps:  
  
    1.  Click **Add**.  
  
    2.  Type or select an expression by which to sort the data.  
  
    3.  From the **Order** column drop-down list, choose the sort direction for each expression. **A-Z** sorts the expression in ascending order. **Z-A** sorts the expression in descending order.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To sort x-axis labels in alphabetical order on a chart  
  
1.  Right-click a field in the Category Field drop-zone and click **Category GroupProperties**.  
  
2.  In the **Category Group Properties** dialog box, click **Sorting**.  
  
3.  For each sort expression, follow these steps:  
  
    1.  Click **Add**.  
  
    2.  Select the expression that matches your grouping field. You can verify the expression for the grouping field by clicking **Grouping**.  
  
    3.  From the **Order** column drop-down list, choose the sort direction for each expression. **A-Z** sorts the expression in ascending alphabetical order. **Z-A** sorts the expression in descending alphabetical order.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To sort the data points in ascending or descending order on a chart  
  
1.  Right-click a field in the Category Field drop zone and click **Category GroupProperties**.  
  
2.  In the **Category Group Properties** dialog box, click **Sorting**.  
  
3.  For each sort expression, follow these steps:  
  
    1.  Click **Add**.  
  
    2.  Select the expression that matches your data field. In most cases, this is an aggregated value, such as `=Sum(Fields!Quantity.Value)`.  
  
    3.  From the **Order** column drop-down list, choose the sort direction for each expression. **A-Z** sorts the expression in ascending order. **Z-A** sorts the expression in descending order.  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To sort data in ascending or descending order for display on a gauge  
  
1.  Right-click the gauge and click **Add Data Group**.  
  
2.  In the **Gauge Panel GroupProperties** dialog box, click **General** if necessary.  
  
3.  In **Group expressions**, click **Add**.  
  
4.  In **Group on**, type or select an expression by which to group the data.  
  
5.  Repeat steps 3 and 4 until you have added all the group expressions you want to use.  
  
6.  Click **Sorting**.  
  
7.  For each sort expression, follow these steps:  
  
    1.  Click **Add**.  
  
    2.  Select the expression that matches your grouping field. You can verify the expression for the grouping field by clicking **Grouping**.  
  
    3.  From the **Order** column drop-down list, choose the sort direction for each expression. **A-Z** sorts the expression in ascending order. **Z-A** sorts the expression in descending order.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 For more information about how data is grouped in a gauge, see [Gauges &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md).  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Specify Consistent Colors across Multiple Shape Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/specify-consistent-colors-across-multiple-shape-charts-report-builder-and-ssrs.md)  
  
  
