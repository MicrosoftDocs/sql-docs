---
title: "Change Map Legends, Color Scale, and Associated Rules (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.shared.maprulesdistribution.f1"
  - "10512"
  - "10539"
  - "10533"
  - "sql12.rtp.rptdesigner.mappointlayerproperties.typerules.f1"
  - "10534"
  - "sql12.rtp.rptdesigner.maplegendproperties.general.f1"
  - "sql12.rtp.rptdesigner.mapdistancescaleproperties.general.f1"
  - "10516"
  - "sql12.rtp.rptdesigner.mapcolorscaleproperties.labels.f1"
  - "sql12.rtp.rptdesigner.maplegendtitleproperties.general.f1"
  - "10514"
  - "sql12.rtp.rptdesigner.shared.mapruleslegend.f1"
  - "sql12.rtp.rptdesigner.mapcolorscaleproperties.general.f1"
  - "sql12.rtp.rptdesigner.shared.embeddedlabels.f1"
  - "10513"
  - "sql12.rtp.rptdesigner.mappointlayerproperties.sizerules.f1"
  - "sql12.rtp.rptdesigner.mapcolorscaletitleproperties.general.f1"
  - "10510"
  - "10509"
  - "10540"
  - "10517"
ms.assetid: a1d691b2-c5ae-420f-af60-b7c54a7385a4
author: markingmyname
ms.author: maghan
manager: kfile
---
# Change Map Legends, Color Scale, and Associated Rules (Report Builder and SSRS)
  A map can contain map legends, a color scale, and a distance scale. These parts of a map help users interpret the data visualization on the map.  
  
 Legends include the following parts of a map:  
  
-   **Map legend** Displays a guide to help interpret the analytical data that varies the display of a map elements on a map layer. A map can have multiple legends. For each map layer, you A specify which legend to use. A legend can provide a guide to more than one map layer.  
  
-   **Color scale** Displays a guide to help interpret colors on the map. A map has one color scale. Multiple layers can provide the data for the color scale.  
  
-   **Distance scale** Displays a guide to help interpret the scale of the map. A map has one distance scale. The current map viewport zoom value determines the distance scale.  
  
 ![rs_MapElements](../media/rs-mapelements.gif "rs_MapElements")  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="Viewport"></a> To change the position of a legend relative to the viewport  
  
#### To change the position of a legend relative to the viewport  
  
1.  In Design view, right-click the legend and open the _\<report item->_**Properties** page.  
  
2.  In **Position**, click the location that specifies where to display the legend relative to the viewport.  
  
3.  To display the legend outside the viewport, select **Show \<report item> outside viewport**.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
    > [!NOTE]  
    >  In preview, map legends and the color scale appear only when there are results from the rules related to that legend. If there are no items to display, the legend does not appear in the rendered report.  
  
  
  
##  <a name="MapLegend"></a> To change the layout of a map legend  
  
#### To change the layout of a map legend  
  
1.  In Design view, right-click the legend and open the **Legend Properties** page.  
  
2.  In **Legend layout**, click the table layout that you want to use for the legend. As you click different options, the layout on the design surface changes.  
  
3.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="MapLegendTitle"></a> To show or hide a map legend title  
  
#### To show or hide a map legend title  
  
-   Right-click the map legend on the design surface, and then click **Show Legend Title**.  
  
  
  
##  <a name="ColorScaleTitle"></a> To show or hide a color scale title  
  
#### To show or hide a color scale title  
  
-   Right-click the color scale on the design surface, and then click **Show Color Scale Title**.  
  
  
  
##  <a name="MoveItems"></a> To move items out of the first legend  
 Create as many additional legends as you need and then update the rules for each map layer specify which legend to display the rule results in.  
  
#### To create a new legend  
  
-   In Design view, right-click the map outside the map viewport, and then click **Add Legend**.  
  
     A new legend appears on the map.  
  
#### To display rule results in a legend  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want and then click _\<map element type\>_**Color Rule**.  
  
3.  Click **Legend**.  
  
4.  In the **Show in this legend** drop-down list, click the name of the legend to display the rule results in.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="TemplateStyle"></a> To vary map element colors based on a template style  
  
#### To vary map element colors based on a template style  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want and then click _\<map element type\>_**Color Rule**.  
  
3.  Click **Apply template style**.  
  
     A template style specifies font, border style, and color palette. Each map element is assigned a different color from the color palette for the theme that was specified in the Map Wizard or Map Layer Wizard. This is the only option that applies to layers that do not have associated analytical data.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="ColorPalette"></a> To vary map element colors based on color palette  
  
#### To vary map element colors based on color palette  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Color Rule**.  
  
3.  Click **Visualize data by using color palette**.  
  
     This option uses a built-in or custom palette that you specify. Based on related analytical data, each map element is assigned a different color or shade of color from the palette.  
  
4.  In **Data field**, type the name of the field that contains the analytical data that you want to visualize by color.  
  
5.  In **Palette**, from the drop-down list, select the name of the palette to use.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="ColorRanges"></a> To vary map element colors based on color ranges  
  
#### To vary map element colors based on color ranges  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Color Rule**.  
  
3.  Click **Visualize data by using color ranges**.  
  
     This option, combined with the start, middle, and end colors that you specify on this page and the options that you specify on the **Distribution** page, divide the related analytical data into ranges. The report processor assigns the appropriate color to each map element based on the its associated data and the range that it falls into.  
  
4.  In **Data field**, type the name of the field that contains the analytical data that you want to visualize by color.  
  
5.  In **Start color**, specify the color to use for the lowest range.  
  
6.  In **Middle color**, specify the color to use for the middle range.  
  
7.  In **End color**, specify the color to use for the highest range.  
  
8.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="CustomColors"></a> To vary map element colors based on custom colors  
  
#### To vary map element colors based on custom colors  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Color Rule**.  
  
3.  Click **Visualize data by using custom colors**.  
  
     This option uses the list of colors that you specify. Based on related analytical data, each map element is assigned a color from the list. If there are more map elements than colors, no color is assigned.  
  
4.  In **Data field**, type the name of the field that contains the analytical data that you want to visualize by color.  
  
5.  In **Custom colors**, click **Add** to specify each custom color.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="DistributionOptions"></a> To set distribution options for a legend  
  
#### To set distribution options for a legend  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Color Rule**.  
  
3.  Select the **Visualize data by using** \<rule type\> option. To use distribution options, you must create ranges on the **Distribution** page based on analytical data that is associated with the layer.  
  
4.  Click **Distribution**.  
  
5.  Select one of the following distribution types:  
  
    -   **EqualInterval**. Specifies ranges that divide the data into equal range intervals.  
  
    -   **EqualDistribution**. Specifies ranges that divide that data so that each range has an equal number of items.  
  
    -   **Optimal**. Specifies ranges that automatically adjust distribution to create balanced subranges.  
  
    -   **Custom**. Specify your own number of ranges to control the distribution of values.  
  
     For more information about distribution options, see [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
6.  In **Number of subranges**, type the number of subranges to use. When the distribution type is **Optimal**, the number of subranges is automatically calculated.  
  
7.  In **Range start**, type a minimum range value. All values less than this number are the same as the range minimum.  
  
8.  In **Range end**, type a maximum range value. All values larger than this number are the same as the range maximum.  
  
9. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="RuleLegend"></a> To change the contents of a rule legend  
  
#### To change the contents of a color, size, width, or marker type legend  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Rule**.  
  
3.  Verify that **Visualize data by using** \<*rule type*> is selected.  
  
4.  In **Data field**, verify that the analytical data that you are visualizing on the layer is selected.  
  
    > [!NOTE]  
    >  If no fields appear in the drop-down list, right-click the layer, and then click **Layer Data** to open the Map Layer Data Properties Dialog Box, Analytical Data page and verify that you have specified analytical data for this layer.  
  
5.  Click **Legend**.  
  
6.  In **Show in this legend**, select the map legend to use to display the rule results.  
  
7.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="ColorScale"></a> To change the contents of the color scale  
  
#### To change the contents of the color scale or a color legend  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Color Rule**.  
  
3.  Select the color rule option to use. To display items in a map legend or color scale, you must select one of the **Visualize data by using** \<rule type> options.  
  
4.  In **Data field**, verify that the analytical data that you are visualizing on the layer is selected.  
  
    > [!NOTE]  
    >  If no fields appear in the drop-down list, right-click the layer, and then click **Layer Data** to open the Map Layer Data Properties Dialog Box, Analytical Data page and verify that you have specified analytical data for this layer.  
  
5.  Click **Legend**.  
  
6.  In **Color scale options**, select **Show in color scale** to display the rule results in the color scale. You can specify this option for more than one color rule.  
  
7.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="HideItems"></a> To remove all items from a legend  
  
#### To hide items based on a rule  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Rule**.  
  
3.  Click **Legend**.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
##  <a name="ChangeFormatItems"></a> To change the format of content in a legend  
 Set legend options for the rule that is associated with the map legend.  
  
#### To change the format of content in a legend  
  
1.  In Design view, click the map until the Map pane appears.  
  
2.  Right-click the layer that has the data that you want, and then click _\<map element type\>_**Rule**.  
  
3.  Click **Legend**.  
  
4.  **Legend text** displays keywords that specify which data appears in the legend. Use map keywords and custom formats to help control the format of legend text. For example, #FROMVALUE {C2} specifies a currency format with two decimal places. For more information, see [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
  
  
## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](maps-report-builder-and-ssrs.md)   
 [Add, Change, or Delete a Map or Map Layer &#40;Report Builder and SSRS&#41;](add-change-or-delete-a-map-or-map-layer-report-builder-and-ssrs.md)   
 [Customize the Data and Display of a Map or Map Layer &#40;Report Builder and SSRS&#41;](customize-the-data-and-display-of-a-map-or-map-layer-report-builder-and-ssrs.md)   
 [Troubleshoot Reports: Map Reports &#40;Report Builder and SSRS&#41;](troubleshoot-reports-map-reports-report-builder-and-ssrs.md)   
 [Map Wizard and Map Layer Wizard &#40;Report Builder and SSRS&#41;](map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md)  
  
  
