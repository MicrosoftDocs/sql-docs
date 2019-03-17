---
title: "Add 3D Effects to a Chart (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/03/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: ab9625d8-6557-4a4d-8123-eefa7c066ff5
author: markingmyname
ms.author: maghan
---
# Chart Effects - Add 3D Effects (Report Builder)
  Three-dimensional (3D) effects can be used to provide depth and add visual impact to charts in your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated reports. For example, if you want to emphasize a particular slice of an exploded pie chart, you can rotate and change the perspective of the chart so that people notice that slice first. When 3D effects are applied to your chart, all gradient colors and hatching styles are disabled.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To apply 3D effects to a Range, Area, Line, Scatter or Polar chart  
  
1.  Right-click anywhere inside the chart area and select **3D Effects**. The **Chart Area Properties** dialog box appears.  
  
2.  In **3D Options**, select the **Enable 3D** option.  
  
3.  (Optional) In **3D Options**, you can set a variety of properties relating to 3D angles and scene options. For more information about these properties, see [3D, Bevel, and Other Effects in a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-effects-3d-bevel-and-other-report-builder.md).  
  
4.  Click **OK**.  
  
## To apply 3D effects to a Funnel chart  
  
1.  Right-click anywhere inside the chart area and select **3D Effects**. The **Chart Area Properties** dialog box appears.  
  
2.  In **3D Options**, select the **Enable 3D** option. Click **OK**.  
  
3.  (Optional) To customize the funnel chart visual appearance, you can go to the Properties pane and change properties specific to the funnel chart.  
  
    1.  Open the Properties pane.  
  
    2.  On the design surface, click anywhere on the funnel. The properties for the series of the funnel chart are displayed in the Properties pane.  
  
    3.  In the **General** section, expand the **CustomAttributes** node.  
  
    4.  For the **Funnel3DDrawingStyle** property, select whether the funnel will be shown with as circular or square.  
  
    5.  For the **Funnel3DRotationAngle** property, set a value between -10 and 10. This will determine the degree of tilt that will be displayed on the 3D funnel chart.  
  
## To apply 3D effects to a Pie chart  
  
1.  Right-click anywhere inside the chart area and select 3D Effects. The **Chart Area Properties** dialog box appears.  
  
2.  In **3D Options**, select the **Enable 3D** option. Click **OK**.  
  
3.  (Optional) In **Rotation**, type an integer value that represents the horizontal rotation of the pie chart.  
  
4.  (Optional) In **Inclination**, type an integer value that represents the vertical tilt rotation of the pie chart.  
  
5.  Click **OK**.  
  
## To apply 3D effects to a Bar or Column chart  
  
1.  Right-click anywhere inside the chart area and select **3D Effects**. The **Chart Area Properties** dialog box appears.  
  
2.  Select the **Enable 3D** option. Click **OK**.  
  
3.  (Optional) Select the **Enable series clustering** option. If your chart contains multiple bar or column chart series, this option will display the series as clustered. By default, multiple bar or column series are shown side-by-side.  
  
4.  Click **OK**.  
  
5.  (Optional) To add cylinder effects to a bar or column chart, follow these steps:  
  
    1.  Open the Properties pane.  
  
    2.  On the design surface, click any of the bars in the series. The properties for the series are displayed in the Properties pane.  
  
    3.  In the **General** section, expand the **CustomAttributes** node.  
  
    4.  For the **DrawingStyle** property, specify the **Cylinder** value.  
  
## See Also  
 [3D, Bevel, and Other Effects in a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/chart-effects-3d-bevel-and-other-report-builder.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-a-chart-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
  
