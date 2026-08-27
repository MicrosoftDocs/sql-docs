---
title: "Define colors on a paginated report chart using a palette"
description: Change the color palette for a paginated report chart by selecting a pre-defined palette or by defining a custom palette.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom:
  - updatefrequency5
---
# Define colors on a paginated report chart using a palette (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can change the color palette for a paginated report chart by selecting a pre-defined palette or defining a custom palette. Custom palettes are chart-specific.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To change the colors on the chart using a built-in color palette  
  
1.  Open the Properties pane.  
  
2.  On the design surface, click the chart. The properties for the chart object are displayed in the Properties pane.  
  
     The object name (**Chart1** by default) appears in the dropdown list at the top of the Properties pane.  
  
3.  In the **Chart** section, for the Palette property, select a new palette from the dropdown list.  
  
    > [!NOTE]  
    >  You cannot change the colors or order in a pre-defined palette.  
  
### To define your own colors on the chart using a custom color palette  
  
1.  Open the Properties pane.  
  
2.  On the design surface, click the chart. The properties for the chart object are displayed in the Properties pane.  
  
3.  In the **Chart** section, for the **Palette** property, select **Custom**.  
  
4.  In the CustomPaletteColors property, click the Edit Collection (**...**) button. The **ReportColorExpression Collection Editor** opens.  
  
5.  Click **Add** to add a color. Select a color from the dropdown list or select Expression and specify a hex value for a specific color, such as ff6600 for "Orange".  
  
     For more information about hex values, see [Color Table](https://go.microsoft.com/fwlink/?linkid=9258) on MSDN.  
  
6.  Click **Add** to add more colors to the palette.  
  
7.  When you are done, click **OK**.  
  
 If you are using a custom palette, you can change the order of the colors to change the color of different series in the chart.  
  
## Related content

- [Formatting series colors on a paginated report chart (Report Builder)](formatting-series-colors-on-a-chart-report-builder-and-ssrs.md)
- [Charts in a paginated report (Report Builder)](charts-report-builder-and-ssrs.md)
- [Specify consistent colors in multiple shape charts in a paginated report (Report Builder)](specify-consistent-colors-across-multiple-shape-charts-report-builder-and-ssrs.md)
