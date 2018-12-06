---
title: "Add a Filter (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 10ae54e7-0e8a-4dff-995d-05516c51d076
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add a Filter (Report Builder and SSRS)
  Add a filter to a dataset, data region, or group when you want to include or exclude specific values for calculations or display. Filters are applied at run time first on the dataset, and then on the data region, and then on the group, in top-down order for group hierarchies. In a table, matrix, or list, filters for row groups, column groups, and adjacent groups are applied independently. In a chart, filters for category groups and series groups are applied independently.  
  
 To add a filter, you must specify one or more filter equations. A filter equation consists of an expression that identifies the data that you want to filter, an operator, and the value to compare to. The data types of the filtered data and the value must match. Filtering on aggregate values for a dataset or data region is not supported.  
  
 To filter data points in a chart, you can set a filter on a category group or a series group. By default, the chart uses the built-in function Sum to aggregate values that belong to the same group into an individual data point in the series. If you change the aggregate function of a series, you must change the aggregate function in the filter expression.  
  
 For more information about filtering embedded and shared datasets, see [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](../report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To set a filter on a data region  
  
1.  Open a report in **Design** view.  
  
2.  Select the data region on the design surface, and then right-click _\<data region>_**Properties**. For a gauge, select **Gauge Panel Properties**. The _\<data region>_**Properties** dialog box opens.  
  
    > [!NOTE]  
    >  On a Tablix data region, right-click the corner cell or a row or column handle, and then click **Tablix Properties**.  
  
3.  Click **Filters**. This displays the current list of filter equations. By default, the list is empty.  
  
4.  Click **Add**. A new blank filter equation appears.  
  
5.  In **Expression**, type or select the expression for the field to filter. To edit the expression, click the expression (*fx*) button.  
  
6.  From the drop-down box, select the data type that matches the type of data in the expression you created in step 5.  
  
7.  In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
8.  In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md).  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To set a filter on a Tablix row or column group  
  
1.  Open a report in **Design** view.  
  
2.  Right-click the table, matrix, or list data region on the design surface to select it. The Grouping pane displays the groups for the selected item.  
  
3.  In the Grouping pane, right-click the group, and then click **Edit Group**. The **Tablix Group** dialog box opens.  
  
4.  Click **Filters**. This displays the current list of filter equations. By default, the list is empty.  
  
5.  Click **Add**. A new blank filter equation appears.  
  
6.  In **Expression**, type or select the expression for the field to filter. To edit the expression, click the expression (*fx*) button.  
  
7.  From the drop-down box, select the data type that matches the type of data in the expression you created in step 5.  
  
8.  In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
9. In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md).  
  
10. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To set a filter on a Chart category group  
  
1.  Open a report in **Design** view.  
  
2.  On the design surface, click the chart twice to bring up data, series and category field drop zones.  
  
3.  Right-click on a field contained in the category field drop zone and select **Category Group Properties**.  
  
4.  Click **Filters**. This displays the current list of filter equations. By default, the list is empty.  
  
5.  Click **Add**. A new blank filter equation appears.  
  
6.  In **Expression**, type or select the expression for the field to filter. To edit the expression, click the expression (*fx*) button.  
  
7.  From the drop-down box, select the data type that matches the type of data in the expression you created in step 5.  
  
8.  In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
9. In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md).  
  
10. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To set a filter on a Chart series group  
  
1.  Open a report in **Design** view.  
  
2.  On the design surface, click the chart twice to bring up data, series and category field drop zones.  
  
3.  Right-click on a field contained in the series field drop zone and select **Series Group Properties**.  
  
4.  Click **Filters**. This displays the current list of filter equations. By default, the list is empty.  
  
5.  Click **Add**. A new blank filter equation appears.  
  
6.  In **Expression**, type or select the expression for the field to filter. To edit the expression, click the expression (*fx*) button.  
  
7.  From the drop-down box, select the data type that matches the type of data in the expression you created in step 5.  
  
8.  In the **Operator** box, select the operator that you want the filter to use to compare the values in the **Expression** box and the **Value** box. The operator you choose determines the number of values that are used from the next step.  
  
9. In the **Value** box, type the expression or value against which you want the filter to evaluate the value in **Expression**.  
  
     For examples of filter equations, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md).  
  
10. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](gauges-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)  
  
  
