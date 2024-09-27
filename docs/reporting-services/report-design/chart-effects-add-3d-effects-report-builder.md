---
title: "Add 3D effects to a paginated report chart"
description: Provide depth and add visual impact to charts in your paginated report with three-dimensional effects in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add 3D effects to a paginated report chart (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Three-dimensional (3D) effects can be used to provide depth and add visual impact to charts in your paginated reports. For example, if you want to emphasize a particular slice of an exploded pie chart, you can rotate and change the perspective of the chart so that people notice that slice of the chart first. When 3D effects are applied to your chart, all gradient colors and hatching styles are disabled.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Apply 3D effects to a range, area, line, scatter, or polar chart  
  
1.  Right-click anywhere inside the chart area and select **3D Effects**. The **Chart Area Properties** dialog appears.  
  
1.  In **3D Options**, select the **Enable 3D** option.  
  
1.  (Optional) In **3D Options**, you can set various properties relating to 3D angles and scene options. For more information about these properties, see [3D, bevel, and other effects in a chart &#40;Report Builder&#41;](../../reporting-services/report-design/chart-effects-3d-bevel-and-other-report-builder.md).  
  
1.  Select **OK**.  
  
## Apply 3D effects to a funnel chart  
  
1.  Right-click anywhere inside the chart area and select **3D Effects**. The **Chart Area Properties** dialog appears.  
 
1.  In **3D Options**, select the **Enable 3D** option. Select **OK**.  
  
1.  (Optional) To customize the funnel chart visual appearance, you can go to the **Properties** pane and change properties specific to the funnel chart.  
  
    1.  Open the **Properties** pane.  
  
    1.  On the design surface, select anywhere on the funnel. The properties for the series of the funnel chart are displayed in the **Properties** pane.  
  
    1.  In the **General** section, expand the **CustomAttributes** node.  
  
    1.  For the **Funnel3DDrawingStyle** property, select whether the funnel is shown with as circular or square.  
  
    1.  For the **Funnel3DRotationAngle** property, set a value between -10 and 10. This setting determines the degree of tilt that is displayed on the 3D funnel chart.  
  
## Apply 3D effects to a pie chart  
  
1.  Right-click anywhere inside the chart area and select 3D Effects. The **Chart Area Properties** dialog appears.  
  
1.  In **3D Options**, select the **Enable 3D** option. Select **OK**.  
  
1.  (Optional) In **Rotation**, enter an integer value that represents the horizontal rotation of the pie chart.  
  
1.  (Optional) In **Inclination**, enter an integer value that represents the vertical tilt rotation of the pie chart.  
  
1.  Select **OK**.  
  
## Apply 3D effects to a bar or column chart  
  
1.  Right-click anywhere inside the chart area and select **3D Effects**. The **Chart Area Properties** dialog appears.  
  
1.  Select the **Enable 3D** option. Select **OK**.  
  
1.  (Optional) Select the **Enable series clustering** option. If your chart contains multiple bar or column chart series, this option displays the series as clustered. By default, multiple bar or column series are shown side-by-side.  
  
1.  Select **OK**.  
  
1.  (Optional) To add cylinder effects to a bar or column chart, follow these steps:  
  
    1.  Open the **Properties** pane.  
  
    1.  On the design surface, select any of the bars in the series. The properties for the series are displayed in the **Properties** pane.  
  
    1.  In the **General** section, expand the **CustomAttributes** node.  
  
    1.  For the **DrawingStyle** property, specify the **Cylinder** value.  
  
## Related content

- [3D, bevel, and other effects in a chart &#40;Report Builder&#41;](../../reporting-services/report-design/chart-effects-3d-bevel-and-other-report-builder.md)
- [Format a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)
- [Charts &#40;Report Builder&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
