---
title: "Add Empty Points to the Chart (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 2b056119-439f-494f-83cf-ee0c05dc6487
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add Empty Points to the Chart (Report Builder and SSRS)
  Null values are shown on the chart as empty spaces or gaps between data points in a series. Empty points are data points that can be inserted in the empty space created by null values.  
  
 By default, empty points are calculated by taking the average of the previous and next data points that contain a value. You can change this so that all empty points are inserted at zero.  
  
 For more information, see [Empty and Null Data Points in Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To specify empty points on a chart  
  
1.  Open the Properties pane.  
  
2.  On the design surface, right-click the series that contains null values. The properties for the series are displayed in the Properties pane.  
  
3.  Expand the **EmptyPoint** node.  
  
4.  Select a color value for the Color property.  
  
5.  In the **EmptyPoint** node, expand the Marker node.  
  
6.  Select a marker type for the MarkerType property.  
  
    > [!NOTE]  
    >  You must select a marker type to add empty points to a bar, column or scatter chart. However, for area, line and radar charts, selecting a marker type is optional because the chart fills in the empty space or gap without requiring a marker to be specified.  
  
7.  Set the value of the empty point.  
  
    1.  In the Properties pane, expand the **CustomAttributes** node.  
  
    2.  Set the EmptyPointValue property. To insert empty points at an average of the previous and next data points, select **Average**. To insert empty points at zero, select **Zero**.  
  
## See Also  
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Chart Types &#40;Report Builder and SSRS&#41;](chart-types-report-builder-and-ssrs.md)   
 [Add Scale Breaks to a Chart &#40;Report Builder and SSRS&#41;](add-scale-breaks-to-a-chart-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)  
  
  
