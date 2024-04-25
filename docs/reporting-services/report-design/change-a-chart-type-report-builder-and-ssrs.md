---
title: "Change a chart type in a paginated report"
description: Learn how to change your paginated report chart type at any point in report design. Improve interpretation with characteristics appropriate for your data in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/03/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Change a chart type in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

When you first insert a chart into a paginated report, the **Select Chart Type** dialog appears. If you cancel this dialog, a Column chart type is added by default.  
  
 At any point when designing the report, you can change the chart type to something more suitable to the report. It's important to select the correct chart type for your data so that it can be interpreted correctly. You should understand each chart type's characteristics to determine what chart type is best suited for your data. For more information, see [Chart types &#40;Report Builder&#41;](../../reporting-services/report-design/chart-types-report-builder-and-ssrs.md).  
  
 When multiple series display on a chart, you might want to change the chart type of an individual series. You can only change the chart type of an individual series if the chart type is Area, Column, Line, or Scatter. For all other chart types, all series in your chart are changed to the selected chart type.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Change the chart type  
  
1.  In Design view, right-click the chart and then select **Change Chart Type**.  
  
    > [!NOTE]  
    >  When there are multiple series on a chart, you must right-click on the series, not the chart, which you want to change.  
  
1.  In the **SelectChart Type** dialog, select a chart type from the list.  
  
## Related content  
 [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md)   
 [Add a chart to a report &#40;Report Builder&#41;](../../reporting-services/report-design/add-a-chart-to-a-report-report-builder-and-ssrs.md)  
  
  
