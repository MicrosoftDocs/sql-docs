---
title: "Add, change, or delete a map or map layer in a paginated report"
description: Learn how to add, remove, or change map options manually or by using the map layer wizard in a paginated report in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/24/2018
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rtp.rptdesigner.maplayerproperties.general.f1"
  - "10526"
  - "sql13.rtp.rptdesigner.mappolygonlayerproperties.general.f1"
  - "10529"
  - "10525"
  - "10535"
  - "sql13.rtp.rptdesigner.mappointlayerproperties.general.f1"
  - "sql13.rtp.rptdesigner.shared.layerfilters.f1"
  - "sql13.rtp.rptdesigner.maplinelayerproperties.general.f1"
  - "10524"
  - "sql13.rtp.rptdesigner.maptilelayerproperties.general.f1"
  - "10532"
  - "sql13.rtp.rptdesigner.maplayerproperties.analyticaldata.f1"
  - "10528"
  - "10527"
  - "sql13.rtp.rptdesigner.shared.layervisibility.f1"
---
# Add, change, or delete a map or map layer in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  A map is a collection of layers. When you add a map to a paginated report, you define the first layer. You can create more layers by using the map layer wizard.  
  
 The easiest way to add, remove, or change options for a layer is to use the map layer wizard. You can also change options manually from the Map pane. To display the **Map** pane, select the map on the report design surface. The following figure displays the parts of the pane:  
  
 :::image type="content" source="../../reporting-services/report-design/media/rsmaplayerzone.gif" alt-text="Screenshot of the Map Layers section that points out the Layer Toolbar.":::
  
 Map layers are drawn from bottom to top in the order that they appear in the Map pane. In the previous figure, the tile layer is drawn first and the polygon layer is drawn last. Layers that are drawn later might hide map elements on layers that are drawn earlier. You can change the order of layers by using the arrow keys on the Map pane toolbar. To show or hide layers, toggle the visibility icon. You can change the transparency of a layer on the **Visibility** page of the **Layer Data** properties dialog.  
  
 The following table displays the toolbar icons for the **Map** pane.  
  
|Symbol|Description|When to use|  
|------------|-----------------|-----------------|  
|:::image type="icon" source="../../reporting-services/media/rs-iconmaplayerwizard.gif":::|Map Layer Wizard|To add a layer by using a wizard, select **New layer wizard**.|  
|:::image type="icon" source="../../reporting-services/media/rs-iconmapaddlayer.gif":::|Add Layer|To manually add a layer, select **Add Layer**, and then select the type of map layer to add.|  
|:::image type="icon" source="../../reporting-services/report-design/media/rs-iconmappolygonlayer.gif":::|Polygon Layer|Add a map layer that displays areas or shapes that are based sets of polygon coordinates.|  
|:::image type="icon" source="../../reporting-services/report-design/media/rs-iconmaplinelayer.gif":::|Line Layer|Add a map layer that displays paths or routes that are based on sets of line coordinates.|  
|:::image type="icon" source="../../reporting-services/report-design/media/rs-iconmappointlayer.gif":::|Point Layer|Add a map layer that displays locations that are based on sets of point coordinates.|  
|:::image type="icon" source="../../reporting-services/report-design/media/rs-iconmaptilelayer.gif":::|Tile Layer|Add a map layer that displays Bing Map tiles that correspond to the current map view area that the viewport defines.|  
  
 The Map view area is at the bottom of the **Map** pane. To change the center or zoom options for the map, use the arrow keys to adjust the view center and the slider to adjust the zoom level.  
  
 For more information about layers, see [Maps &#40;Report Builder&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="AddLayer"></a> Add a layer from the map layer wizard  
  
-   From the Ribbon, on the **Insert** menu, select **Map**, and then choose **Map Wizard.** The wizard enables you to add a layer to the existing map. Most wizard pages are identical between the map wizard and the map layer wizard.  
  
     For more information, see [Map wizard and map layer wizard &#40;Report Builder&#41;](../../reporting-services/report-design/map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md).  
  
##  <a name="ChangeLayer"></a> Change options for a layer by using the map layer wizard  
  
-   Run the map layer wizard. This wizard enables you to change options for a layer that you created by using the map layer wizard. In the **Map** pane, right-click the layer, and on the toolbar, select the layer wizard button (:::image type="icon" source="../../reporting-services/media/rs-iconmaplayerwizard.gif":::).  
  
     For more information, see [Map wizard and map layer wizard &#40;Report Builder&#41;](../../reporting-services/report-design/map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md).  
  
##  <a name="AddVectorLayer"></a> Add a point, line, or polygon layer from the Map pane toolbar  
  
1.  Select the map so that the **Map** pane appears.  
  
1.  On the toolbar, select the **Add Layer** button, and from the list, choose the type of layer that you want to add: **Point**, **Line**, or **Polygon**.  
  
    > [!NOTE]  
    >  Although you can add a map layer and configure it manually, you should use the map layer wizard to add new layers. To launch the wizard from the **Map** pane toolbar, select the layer wizard button (:::image type="icon" source="../../reporting-services/media/rs-iconmaplayerwizard.gif":::).  
  
1.  Right-click the layer, and then select **Layer Data**.  
  
1.  In **Use spatial data from**, select the source of spatial data. Options vary based on your selection.  
  
     If you want to visualize analytical from your report on this layer, do the following actions:  
  
    1.  Select **Analytical data**.  
  
    1.  In **Analytical dataset**, choose the name of the dataset that contains analytical data and the match fields to build a relationship between analytical and spatial data.  
  
    1.  Select **Add**.  
  
    1.  Enter the name of the match field from the spatial dataset.  
  
    1.  Enter the name of the match field from the analytical dataset.  
  
     For more information about linking spatial and analytical data, see [Customize the data and display of a map or map layer &#40;Report Builder&#41;](../../reporting-services/report-design/customize-the-data-and-display-of-a-map-or-map-layer-report-builder-and-ssrs.md).  
  
1.  Select **OK**.
  
##  <a name="FilterAnalyticalData"></a> Filter analytical data for the layer  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Right-click the layer in the **Map** pane, and then choose  **Layer Data**.  
  
1.  Select **Filters**.  
  
1.  Define a filter equation to limit the analytical data that is used in the map display. For more information, see [Filter equation examples &#40;Report Builder&#41;](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
##  <a name="PointProperties"></a> Control point properties for a point layer or for polygon center points  
  
1.  Select **General** on the **Map Point Properties** dialog to change label, tooltip, and marker type options for the following map elements:  
  
    -   All dynamic or embedded points on a point layer. Color rules, size rules, and marker type rules for points override these options. To override options for a specific embedded point, use the [Map embedded point Properties dialog, marker](./maps-report-builder-and-ssrs.md) page.  
  
    -   The center point for all dynamic or embedded polygons on a polygon layer. Color rules, size rules, and marker type rules for center points override these options. To override options for a specific center point, use the [Map embedded point Properties dialog, marker](./maps-report-builder-and-ssrs.md) page.  
  
##  <a name="Embedded"></a> Specify embedded data as a source of spatial data  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Right-click the layer, and then select **Layer Data**.  
  
1.  In **Use spatial data from**, select **Data embedded in report**.  
  
1.  To load map elements from an existing report or to create map elements based on an ESRI file, select **Browse**, point to the file, and then select **Open**. The map elements are embedded in this report definition. The spatial data that you point to must match the layer type. For example, for a point layer, you must point to spatial data that specifies sets of point coordinates.  
  
1.  In **Spatial field**, specify the name of the field that contains spatial data. You might need to determine this name from the source of spatial data.  
  
    > [!NOTE]  
    >  If you don't know the name of the field and you browsed to an ESRI Shapefile, use the **Link to ESRI shape file option** instead of this option.  
  
1.  Select **OK**.
  
##  <a name="ESRI"></a> Specify an ESRI Shapefile as a source of spatial data  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Right-click the layer, and then select **Layer Data**.  
  
1.  In **Use spatial data from**, select **Link to ESRI Shapefile**.  
  
1.  In **File name**, enter the location of an ESRI Shapefile, or select **Browse** to choose an ESRI Shapefile.  
  
    > [!NOTE]  
    >  If the Shapefile is on your local computer, the spatial data is embedded in the report definition. To retrieve the data dynamically when the report is processed, you must upload the ESRI .shp file and its .dbf support file to the report server. For more information, see [Upload a file or report](../reports/upload-a-file-or-report-report-manager.md).  
  
1.  Select **OK**.
 
##  <a name="DatasetField"></a> Specify a report dataset field as a source of spatial data  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Right-click the layer, and then select **Layer Data**.  
  
1.  In **Use spatial data from**, select **Spatial field in a dataset**.  
  
1.  In **Dataset name**, select the name of a dataset in the report that contains that spatial data that you want.  
  
1.  In **Spatial field name**, select the name of the field in the dataset that contains spatial data.  
  
1.  Select **OK**.
  
##  <a name="TileLayer"></a> Add a tile layer  
  
1.  Select the map so the **Map** pane appears.  
  
1.  On the toolbar, select the **Add Layer** button, and from the list, choose **Tile Layer**.  
  
    > [!NOTE]  
    >  For more information about the use of Bing map tiles in your report, see [Terms of use](https://go.microsoft.com/fwlink/?LinkId=151371).  
  
1.  Right-click the tile layer in the **Map** pane, and then choose **Tile Properties**.  
  
1.  In **Tile options**, select a tile style. If the Bing map tiles are available, the layer on the design surface updates with the style that you select.  
  
    > [!NOTE]  
    >  A tile layer can also be added when you add a polygon, line, or point layer in the Map or Map Layer wizard. On the **Choose spatial data and map view options** page, select the option **Add a Bing Maps background for this map view**.  
  
##  <a name="DrawingOrder"></a> Change the drawing order of a layer  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Select the layer in the **Map** pane to select it.  
  
1.  On the **Map** pane toolbar, select the up or down arrow to change the drawing order of each layer.  
  
##  <a name="Transparency"></a> Change the transparency of a polygon, line, or point layer  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Right-click the layer, and then choose **Layer Data**.  
  
1.  Select **Visibility**.  
  
1.  In **Transparency options**, enter a value that represents the percentage transparency, for example, **40**. Zero (0) % transparency means that the layer is opaque. 100% transparency means that you don't see the layer in the report.  
  
1.  Select **OK**.
  
##  <a name="TileTransparency"></a> Change the transparency of a tile layer  
  
1.  Select the map so the **Map** pane appears.  
  
1.  Right-click the layer, and then choose **Tile Properties**.  
  
1.  Select **Visibility**.  
  
1.  In **Transparency options**, enter a value that represents the percentage transparency, for example, **40**.  
  
1.  Select **OK**.
  
##  <a name="Secure"></a> Specify a secure connection for a tile layer  
  
1.  Select the map so the **Map** pane appears.  
  
1.  In the **Map** pane, choose the tile layer to select it. The **Properties** pane displays the tile layer properties.  
  
1.  In the **Properties** pane, set **UseSecureConnection** to **True**.  
  
 The connection for the Bing Maps Web service uses the HTTP SSL service to retrieve Bing map tiles for this layer. Transport Layer Security (TLS) was previously known as Secure Sockets Layer (SSL).
  
##  <a name="Language"></a> Specify the language for tile labels  
  
1.  By default, for tile styles that display labels, the language is determined from the default locale for Report Builder. You can customize the language setting for tile labels in the following ways.  
  
    -   To choose the map, select the map outside the viewport. In the **Properties** pane, for the `TileLanguage` property, select a culture value from the list.  
  
    -   To choose the report, select the report background. In the **Properties** pane, from for the `Language` property, select a culture value from the list.  
  
     The order of precedence for setting the tile label language is: report property `Language`, default locale for Report Builder, and map property `TileLanguage`.  
  
##  <a name="ConditionalHide"></a> Conditionally hide a layer based on viewport zoom level  
  
1.  Set **Visibility** options to control the display for a map layer.  
  
    -   In the **Map Layers** pane, right-click a layer to select it. On the **Map Layers** toolbar, select **Properties** to open **Map Layer Properties**.  
  
    -   Select **Visibility**.  
  
    -   In Layer visibility, select **Show or hide based on zoom value**.  
  
    -   Enter minimum and maximum zoom values for when to display the layer.  
  
    -   Optional. Enter a value for transparency.  
  
     You can also conditionally hide the layer. For more information, see [Hide an item &#40;Report Builder&#41;](../../reporting-services/report-builder/hide-an-item-report-builder-and-ssrs.md).  
  
## Related content  
 [Maps &#40;Report Builder&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md)   
 [Troubleshoot reports: Map reports &#40;Report Builder&#41;](../../reporting-services/report-design/troubleshoot-reports-map-reports-report-builder-and-ssrs.md)  
  
