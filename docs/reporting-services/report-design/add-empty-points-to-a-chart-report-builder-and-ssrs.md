---
title: "Add empty points to a chart in a paginated report"
description: Specify empty points on a chart in a paginated report. These points are calculated in Report Builder by taking the average of the previous and next data points that contain a value.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add empty points to a chart in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

Null values are shown on the chart as empty spaces or gaps between data points in a series. In paginated reports, empty points are data points that can be inserted in the empty space created by null values.  
  
 By default, empty points are calculated by taking the average of the previous and next data points that contain a value. You can change this configuration so that all empty points are inserted at zero.  
  
 For more information, see [Empty and null data points in charts &#40;Report Builder&#41;](../../reporting-services/report-design/empty-and-null-data-points-in-charts-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Specify empty points on a chart  
  
1.  Open the **Properties** pane.  
  
1.  On the design surface, right-click the series that contains null values. The properties for the series are displayed in the Properties pane.  
  
1.  Expand the **EmptyPoint** node.  
  
1.  Select a color value for the **Color** property.  
  
1.  In the **EmptyPoint** node, expand the **Marker** node.  
  
1.  Select a marker type for the **MarkerType** property.  
  
    > [!NOTE]  
    >  You must select a marker type to add empty points to a bar, column or scatter chart. However, for area, line and radar charts, selecting a marker type is optional because the chart fills in the empty space or gap without requiring a marker to be specified.  
  
1.  Set the value of the empty point.  
  
    1.  In the **Properties** pane, expand the **CustomAttributes** node.  
  
    1.  Set the **EmptyPointValue** property. To insert empty points at an average of the previous and next data points, select **Average**. To insert empty points at zero, select **Zero**.  
  
## Related content

- [Add dataset filters, data region filters, and group filters &#40;Report Builder&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)
- [Chart types &#40;Report Builder&#41;](../../reporting-services/report-design/chart-types-report-builder-and-ssrs.md)
- [Add scale breaks to a chart &#40;Report Builder&#41;](../../reporting-services/report-design/add-scale-breaks-to-a-chart-report-builder-and-ssrs.md)
- [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
