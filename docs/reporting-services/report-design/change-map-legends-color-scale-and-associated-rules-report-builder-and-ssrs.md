---
title: "Change map legends-color scale-associated rules in a paginated report"
description: Learn how to change map legends in a paginated report to help users interpret the data visualization on maps in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.mapcolorscaleproperties.labels.f1"
  - "sql13.rtp.rptdesigner.mappointlayerproperties.typerules.f1"
  - "sql13.rtp.rptdesigner.shared.maprulesdistribution.f1"
  - "10512"
  - "10539"
  - "10533"
  - "sql13.rtp.rptdesigner.maplegendtitleproperties.general.f1"
  - "10534"
  - "10516"
  - "sql13.rtp.rptdesigner.mapdistancescaleproperties.general.f1"
  - "sql13.rtp.rptdesigner.mapcolorscaleproperties.general.f1"
  - "sql13.rtp.rptdesigner.mapcolorscaletitleproperties.general.f1"
  - "10514"
  - "sql13.rtp.rptdesigner.mappointlayerproperties.sizerules.f1"
  - "10513"
  - "sql13.rtp.rptdesigner.shared.mapruleslegend.f1"
  - "sql13.rtp.rptdesigner.shared.embeddedlabels.f1"
  - "10510"
  - "10509"
  - "sql13.rtp.rptdesigner.maplegendproperties.general.f1"
  - "10540"
  - "10517"
---
# Change map legends, color scale, and associated rules in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In a paginated report, a map can contain map legends, a color scale, and a distance scale. These parts of a map help users interpret the data visualization on the map.  
  
 Legends include the following parts of a map:  
  
-   **Map legend**: Displays a guide to help interpret the analytical data that varies the display of map elements on a map layer. A map can have multiple legends. For each map layer, you A specify which legend to use. A legend can provide a guide to more than one map layer.  
  
-   **Color scale**: Displays a guide to help interpret colors on the map. A map has one color scale. Multiple layers can provide the data for the color scale.  
  
-   **Distance scale**: Displays a guide to help interpret the scale of the map. A map has one distance scale. The current map viewport zoom value determines the distance scale.  
  
 :::image type="content" source="../../reporting-services/report-design/media/rs-mapelements.gif" alt-text="Screenshot of the map elements, including map legend, color scale, and distance scale.":::
  
  
##  <a name="Viewport"></a> Change the position of a legend relative to the viewport  
  
#### Change the position of a legend relative to the viewport  
  
1.  In Design view, right-click the legend and open the `<report item>` **Properties** page.  
  
1.  In **Position**, choose the location that specifies where to display the legend relative to the viewport.  
  
1.  To display the legend outside the viewport, select **Show \<report item> outside viewport**.  
  
1.  Select **OK**.
  
    > [!NOTE]  
    >  In preview, map legends and the color scale appear only when there are results from the rules related to that legend. If there are no items to display, the legend doesn't appear in the rendered report.  
  
##  <a name="MapLegend"></a> Change the layout of a map legend  
  
### Change the layout of a map legend  
  
1.  In Design view, right-click the legend and open the **Legend Properties** page.  
  
1.  In **Legend layout**, select the table layout that you want to use for the legend. As you select different options, the layout on the design surface changes.  
  
1.  Select **OK**.
  
##  <a name="MapLegendTitle"></a> Show or hide a map legend title  
  
### Show or hide a map legend title  
  
-   Right-click the map legend on the design surface, and then select **Show Legend Title**.  
  
##  <a name="ColorScaleTitle"></a> Show or hide a color scale title  
  
### Show or hide a color scale title  
  
-   Right-click the color scale on the design surface, and then select **Show Color Scale Title**.  
  
##  <a name="MoveItems"></a> Move items out of the first legend  
 Create as many legends as you need and then update the rules for each map layer specify which legend to display the rule results in.  
  
### Create a new legend  
  
-   In Design view, right-click the map outside the map viewport, and then select **Add Legend**.  
  
     A new legend appears on the map.  
  
### Display rule results in a legend  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want and then select `<map element type> `**Color Rule**.  
  
1.  Select **Legend**.  
  
1.  In the **Show in this legend** list, select the name of the legend to display the rule results in.  
  
1.  Select **OK**.
  
##  <a name="TemplateStyle"></a> Vary map element colors based on a template style  
  
### Vary map element colors based on a template style  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want and then select `<map element type>` **Color Rule**.  
  
1.  Select **Apply template style**.  
  
     A template style specifies font, border style, and color palette. Each map element is assigned a different color from the color palette for the theme that was specified in the Map Wizard or Map Layer Wizard. This option is the only option that applies to layers that don't associate analytical data.  
  
1.  Select **OK**.
  
##  <a name="ColorPalette"></a> Vary map element colors based on color palette  
  
### Vary map element colors based on color palette  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Color Rule**.  
  
1.  Select **Visualize data by using color palette**.  
  
     This option uses a built-in or custom palette that you specify. Based on related analytical data, each map element is assigned a different color or shade of color from the palette.  
  
1.  In **Data field**, enter the name of the field that contains the analytical data that you want to visualize by color.  
  
1.  In **Palette**, from the list, select the name of the palette to use.  
  
1.  Select **OK**.
  
##  <a name="ColorRanges"></a> Vary map element colors based on color ranges  
  
1### Vary map element colors based on color ranges  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Color Rule**.  
  
1.  Select **Visualize data by using color ranges**.  
  
     This option, combined with the start, middle, and end colors that you specify on this page and the options that you specify on the **Distribution** page, divide the related analytical data into ranges. The report processor assigns the appropriate color to each map element based on its associated data and the range that it falls into.  
  
1.  In **Data field**, enter the name of the field that contains the analytical data that you want to visualize by color.  
  
1.  In **Start color**, specify the color to use for the lowest range.  
  
1.  In **Middle color**, specify the color to use for the middle range.  
  
1.  In **End color**, specify the color to use for the highest range.  
  
1.  Select **OK**.
  
##  <a name="CustomColors"></a> Vary map element colors based on custom colors  
  
### Vary map element colors based on custom colors  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Color Rule**.  
  
1.  Select **Visualize data by using custom colors**.  
  
     This option uses the list of colors that you specify. Based on related analytical data, each map element is assigned a color from the list. If there are more map elements than colors, no color is assigned.  
  
1.  In **Data field**, enter the name of the field that contains the analytical data that you want to visualize by color.  
  
1.  In **Custom colors**, select **Add** to specify each custom color.  
  
1.  Select **OK**.
  
##  <a name="DistributionOptions"></a> Set distribution options for a legend  
  
### Set distribution options for a legend  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type> `**Color Rule**.  
  
1.  Select the **Visualize data by using** `<rule type>` option. To use distribution options, you must create ranges on the **Distribution** page based on analytical data that is associated with the layer.  
  
1.  Select **Distribution**.  
  
1.  Select one of the following distribution types:  
  
    -   **EqualInterval**: Specifies ranges that divide the data into equal range intervals.  
  
    -   **EqualDistribution**: Specifies ranges that divide that data so that each range has an equal number of items.  
  
    -   **Optimal**: Specifies ranges that automatically adjust distribution to create balanced subranges.  
  
    -   **Custom**: Specify your own number of ranges to control the distribution of values.  
  
     For more information about distribution options, see [Vary polygon, line, and point display by rules and analytical data &#40;Report Builder&#41;](../../reporting-services/report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
1.  In **Number of subranges**, enter the number of subranges to use. When the distribution type is **Optimal**, the number of subranges is automatically calculated.  
  
1.  In **Range start**, enter a minimum range value. All values less than this number are the same as the range minimum.  
  
1.  In **Range end**, enter a maximum range value. All values larger than this number are the same as the range maximum.  
  
1. Select **OK**.
  
##  <a name="RuleLegend"></a> Change the contents of a rule legend  
  
### Change the contents of a color, size, width, or marker type legend  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Rule**.  
  
1.  Verify that **Visualize data by using** `<rule type>` is selected.  
  
1.  In **Data field**, verify that the analytical data that you're visualizing on the layer is selected.  
  
    > [!NOTE]  
    >  If no fields appear in the list, right-click the layer, and then select **Layer Data** to open the **Map Layer Data Properties** dialog, **Analytical Data** page and verify that you have specified analytical data for this layer.  
  
1.  Select **Legend**.  
  
1.  In **Show in this legend**, select the map legend to use to display the rule results.  
  
1.  Select **OK**.
  
##  <a name="ColorScale"></a> Change the contents of the color scale  
  
### Change the contents of the color scale or a color legend  
  
1.  In Design view, select the map until the Map pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Color Rule**.  
  
1.  Select the color rule option to use. To display items in a map legend or color scale, you must select one of the **Visualize data by using** \<rule type> options.  
  
1.  In **Data field**, verify that the analytical data that you're visualizing on the layer is selected.  
  
    > [!NOTE]  
    >  If no fields appear in the drop-down list, right-click the layer, and then click **Layer Data** to open the Map Layer Data Properties Dialog Box, Analytical Data page and verify that you have specified analytical data for this layer.  
  
1.  Select **Legend**.  
  
1.  In **Color scale options**, select **Show in color scale** to display the rule results in the color scale. You can specify this option for more than one color rule.  
  
1.  Select **OK**.
  
##  <a name="HideItems"></a> Remove all items from a legend  
  
### Hide items based on a rule  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Rule**.  
  
1.  Select **Legend**.  
  
1.  Select **OK**.
  
##  <a name="ChangeFormatItems"></a> Change the format of content in a legend  
 Set legend options for the rule that is associated with the map legend.  
  
### Change the format of content in a legend  
  
1.  In Design view, select the map until the **Map** pane appears.  
  
1.  Right-click the layer that has the data that you want, and then select `<map element type>` **Rule**.  
  
1.  Select **Legend**.  
  
1.  **Legend text**: Displays keywords that specify which data appears in the legend. Use map keywords and custom formats to help control the format of legend text. For example, `#FROMVALUE {C2}` specifies a currency format with two decimal places. For more information, see [Vary polygon, line, and point display by rules and analytical data &#40;Report Builder&#41;](../../reporting-services/report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
1.  Select **OK**.
  
## Related content
 [Maps &#40;Report Builder&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md)   
 [Add, change, or delete a map or map layer &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-a-map-or-map-layer-report-builder-and-ssrs.md)   
 [Customize the data and display of a map or map layer &#40;Report Builder&#41;](../../reporting-services/report-design/customize-the-data-and-display-of-a-map-or-map-layer-report-builder-and-ssrs.md)   
 [Troubleshoot reports: Map reports &#40;Report Builder&#41;](../../reporting-services/report-design/troubleshoot-reports-map-reports-report-builder-and-ssrs.md)   
 [Map wizard and Map Layer wizard &#40;Report Builder&#41;](../../reporting-services/report-design/map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md)  
  
  
