---
title: "Display data point labels outside a pie chart in a paginated report | Microsoft Docs"
description: Discover how displaying the labels outside a pie chart in a paginated report might create more room for longer data labels in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 959b7574-cf43-470b-b592-4944d8a9948f
author: maggiesMSFT
ms.author: maggies
---
# Display data point labels outside a pie chart in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In paginated reports, pie chart labeling is optimized to display labels on only several slices of data. Labels may overlap if the pie chart contains too many slices. One solution is to display the labels outside the pie chart, which may create more room for longer data labels. If you find that your labels still overlap, you can create more space for them by enabling 3D. This reduces the diameter of the pie chart, creating more space around the chart.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To display data point labels inside a pie chart  
  
1.  Add a pie chart to your report. For more information, see [Add a Chart to a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-chart-to-a-report-report-builder-and-ssrs.md).  
  
2.  On the design surface, right-click on the chart and select **Show Data Labels**.  
  
### To display data point labels outside a pie chart  
  
1.  Create a pie chart and display the data labels.  
  
2.  Open the Properties pane.  
  
3.  On the design surface, click on the pie itself to display the **Category** properties in the Properties pane.  
  
4.  Expand the **CustomAttributes** node. A list of attributes for the pie chart is displayed.  
  
5.  Set the **PieLabelStyle** property to **Outside**.  
  
6.  Set the **PieLineColor** property to **Black**. The PieLineColor property defines callout lines for each data point label.  
  
### To prevent overlapping labels displayed outside a pie chart  
  
1.  Create a pie chart with external labels.  
  
2.  On the design surface, right-click outside the pie chart but inside the chart borders and select **Chart Area Properties**.The **Chart AreaProperties** dialog box appears.  
  
3.  On the **3D Options** tab, select **Enable 3D**.  
  
4.  If you want the chart to have more room for labels but still appear two-dimensional, set the **Rotation** and **Inclination** properties to **0**.  
  
## See Also  
 [Pie Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/pie-charts-report-builder-and-ssrs.md)   
 [Collect Small Slices on a Pie Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/collect-small-slices-on-a-pie-chart-report-builder-and-ssrs.md)   
 [Display Percentage Values on a Pie Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/display-percentage-values-on-a-pie-chart-report-builder-and-ssrs.md)  
  
  
