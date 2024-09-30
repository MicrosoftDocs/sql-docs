---
title: "Show ToolTips on a series in a paginated report"
description: Learn how to add a ToolTip to each data point on the series of a chart in a paginated report to display related information in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Show ToolTips on a series in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can add a ToolTip to each data point on the series of a chart to display information related to the data point, such as the group name, the value of the data point, or the percentage of the data point relative to the series total. When users hover over the data point in a rendered paginated report, they'll see this information.  
  
 You cannot add a ToolTip to a calculated series.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To specify a ToolTip on each data point  
  
1.  Right-click on the series or right-click on the field in the **Values** area, and click **Series Properties**.  
  
2.  Click **Series Data** and, for the **ToolTip** property, type in a string or expression. You can use any chart-specific keyword to represent another element on the chart. For more information, see [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-data-points-on-a-chart-report-builder-and-ssrs.md).  
  
## Related content

- [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-data-points-on-a-chart-report-builder-and-ssrs.md)
- [Change the Text of a Legend Item &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-legend-change-item-text-report-builder.md)
- [Formatting Series Colors on a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-series-colors-on-a-chart-report-builder-and-ssrs.md)
- [Add a Drillthrough Action on a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-drillthrough-action-on-a-report-report-builder-and-ssrs.md)
