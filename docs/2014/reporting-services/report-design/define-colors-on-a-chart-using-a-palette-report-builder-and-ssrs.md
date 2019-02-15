---
title: "Define Colors on a Chart Using a Palette (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d95efc22-5a32-43d4-9bd2-12753e7fd395
author: markingmyname
ms.author: maghan
manager: kfile
---
# Define Colors on a Chart Using a Palette (Report Builder and SSRS)
  You can change the color palette for a chart by selecting a pre-defined palette or defining a custom palette. Custom palettes are report-specific.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To change the colors on the chart using a built-in color palette  
  
1.  Open the Properties pane.  
  
2.  On the design surface, click the chart. The properties for the chart object are displayed in the Properties pane.  
  
     The object name (**Chart1** by default) appears in the drop-down list at the top of the Properties pane.  
  
3.  In the **Chart** section, for the Palette property, select a new palette from the drop-down list.  
  
    > [!NOTE]  
    >  You cannot change the colors or order in a pre-defined palette.  
  
### To define your own colors on the chart using a custom color palette  
  
1.  Open the Properties pane.  
  
2.  On the design surface, click the chart. The properties for the chart object are displayed in the Properties pane.  
  
3.  In the **Chart** section, for the `Palette` property, select **Custom**.  
  
4.  In the CustomPaletteColors property, click the Edit Collection (**...**) button. The **ReportColorExpression Collection Editor** opens.  
  
5.  Click **Add** to add a color. Select a color from the drop-down list or select Expression and specify a hex value for a specific color, such as ff6600 for "Orange".  
  
     For more information about hex values, see [Color Table](https://go.microsoft.com/fwlink/?linkid=9258) on MSDN.  
  
6.  Click **Add** to add more colors to the palette.  
  
7.  When you are done, click **OK**.  
  
 If you are using a custom palette, you can change the order of the colors to change the color of different series in the chart.  
  
## See Also  
 [Formatting Series Colors on a Chart &#40;Report Builder and SSRS&#41;](formatting-series-colors-on-a-chart-report-builder-and-ssrs.md)   
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)   
 [Specify Consistent Colors across Multiple Shape Charts &#40;Report Builder and SSRS&#41;](shape-charts-report-builder-and-ssrs.md)  
  
  
