---
title: "3D, Bevel, and Other Effects in a Chart (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10156"
ms.assetid: 18ef2119-2931-43ae-9078-f39b460462dd
author: markingmyname
ms.author: maghan
manager: kfile
---
# 3D, Bevel, and Other Effects in a Chart (Report Builder and SSRS)
  Three-dimensional (3D) effects can be used to provide depth and add visual impact to your chart. For example, if you want to emphasize a particular slice of an exploded pie chart, you can rotate and change the perspective of the chart so that people notice that slice first. When 3D effects are applied to your chart, all gradient colors and hatching styles are disabled.  
  
 Three-dimensional effects can be applied to individual charts, and it is possible to display both two-dimensional and three-dimensional charts on the same report.  
  
 For all chart types, you can add three-dimensional effects to a chart area in the **Chart Area Properties** dialog box by selecting **Enable 3D**. For more information, see [Add 3D Effects to a Chart &#40;Report Builder and SSRS&#41;](chart-effects-add-3d-effects-report-builder.md).  
  
 Another way to add visual impact to charts is by adding bevel, emboss and texture styles in bar, column, pie and doughnut charts. For more information, see [Add Bevel, Emboss, and Texture Styles to a Chart &#40;Report Builder and SSRS&#41;](chart-effects-add-bevel-emboss-or-texture-report-builder.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Coordinate-Based Three-Dimensional Charts  
 When working with coordinate-based chart types (Column, Bar, Area, Point, Line and Range), three-dimensional effects display the chart with a third axis, known as the "z-axis". The introduction of this third axis allows you to apply a variety of visual enhancements to your chart.  
  
### Changing the White Space in a 3D Chart  
 When you display a chart area in three-dimensional mode, each series is shown in a separate row along the z-axis of the chart. To change the amount of space between each series, modify the chart's point gap depth by changing the **Point Gap Depth** property in the 3D Effects dialog box.  
  
### Changing the Projection of a 3D Chart  
 There are two types of 3D projections: oblique and perspective. An oblique projection to the chart adds a depth dimension to a two-dimensional chart. The z-axis is drawn at equal angles from the horizontal and vertical axes, which remain perpendicular to each other just as in a two-dimensional chart.  
  
 Perspective projection transforms the chart by estimating a view plane and re-drawing the chart as if it were being viewed from that point. The **Rotation** value shifts the view vertically from "ground level" at 0 to overhead at 90. The **Inclination** value shifts the viewing angle to the left or right. A value of 0 is equivalent to a two-dimensional view of the chart. The **Perspective** value defines the percentage of distortion that will be used when displaying the projection. This type of projection maintains the proportions of your chart, but the chart's appearance becomes distorted, so it is most effective to use a lower degree of perspective.  
  
> [!NOTE]  
>  The oblique and perspective projections are separate types of projections, so they cannot be used together on the same chart.  
  
### Clustering Data  
 In 2D charts, multiple series of data appear side-by-side. Clustering shows individual series in separate rows on a 3D chart. For example, if you have a chart that contains three series of data points, clustering will display each of the three series on a separate row along the z-axis. By default, all chart types shown in 3D are clustered.  
  
 Clustering can be disabled for bar and column charts. When clustering is disabled, multiple bar and column series are displayed side-by-side in one row.  
  
## Shape-Based Three-Dimensional Charts  
 Shape-based chart types (Pie, Doughnut, Funnel, Pyramid) have fewer three-dimensional effects available. When working with shape-based chart types, you can change the rotation and inclination values only.  
  
## Rotations  
 Charts can be rotated horizontally and vertically from -90 to 90 degrees. A positive horizontal angle will rotate the chart counter-clockwise around the x-axis, while a positive vertical angle will rotate the chart clockwise around the y-axis.  
  
## Highlighting 3D Effects  
 You can add highlighting styles to a 3D chart via the **Shading** property, which appears under Area3DStyle in the Properties pane when the chart area is selected. A simple lighting style applies the same hue to the chart area elements. A realistic style changes the hues of the chart area elements depending on a specified lighting angle.  
  
## See Also  
 [Formatting Axis Labels on a Chart &#40;Report Builder and SSRS&#41;](formatting-axis-labels-on-a-chart-report-builder-and-ssrs.md)   
 [Formatting a Chart &#40;Report Builder and SSRS&#41;](formatting-a-chart-report-builder-and-ssrs.md)   
 [Add 3D Effects to a Chart &#40;Report Builder and SSRS&#41;](chart-effects-add-3d-effects-report-builder.md)  
  
  
