---
title: "Add, Change, or Delete a Map or Map Layer (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10526"
  - "sql12.rtp.rptdesigner.maptilelayerproperties.general.f1"
  - "10529"
  - "10525"
  - "10535"
  - "sql12.rtp.rptdesigner.mappolygonlayerproperties.general.f1"
  - "sql12.rtp.rptdesigner.shared.layervisibility.f1"
  - "sql12.rtp.rptdesigner.mappointlayerproperties.general.f1"
  - "sql12.rtp.rptdesigner.shared.layerfilters.f1"
  - "10524"
  - "sql12.rtp.rptdesigner.maplayerproperties.general.f1"
  - "sql12.rtp.rptdesigner.maplinelayerproperties.general.f1"
  - "10532"
  - "10528"
  - "10527"
  - "sql12.rtp.rptdesigner.maplayerproperties.analyticaldata.f1"
ms.assetid: 6e89815e-187e-45bf-bf63-3d5c4a246360
author: markingmyname
ms.author: maghan
manager: kfile
---
# Add, Change, or Delete a Map or Map Layer (Report Builder and SSRS)
  A map is a collection of layers. When you add a map to a report, you define the first layer. You can create additional layers by using the map layer wizard.  
  
 The easiest way to add, remove, or change options for a layer is to use the map layer wizard. You can also change options manually from the Map pane. To display the **Map** pane, click in the map on the report design surface. The following figure displays the parts of the pane:  
  
 ![rsMapLayerZone](../media/rsmaplayerzone.gif "rsMapLayerZone")  
  
 Map layers are drawn from bottom to top in the order that they appear in the Map pane. In the previous figure, the tile layer is drawn first and the polygon layer is drawn last. Layers that are drawn later might hide map elements on layers that are drawn earlier. You can change the order of layers by using the arrow keys on the Map pane toolbar. To show or hide layers, toggle the visibility icon. You can change the transparency of a layer on the `Visibility` page of the **Layer Data** properties dialog box.  
  
 The following table displays the toolbar icons for the **Map** pane.  
  
|Symbol|Description|When to use|  
|------------|-----------------|-----------------|  
|![rs_IconMapLayerWizard](../../tutorials/media/rs-iconmaplayerwizard.gif "rs_IconMapLayerWizard")|Map Layer Wizard|To add a layer by using a wizard, click **New layer wizard**.|  
|![rs_IconMapAddLayer](../../tutorials/media/rs-iconmapaddlayer.gif "rs_IconMapAddLayer")|Add Layer|To manually add a layer, click **Add Layer**, and then click the type of map layer to add.|  
|![rs_IconMapPolygonLayer](../media/rs-iconmappolygonlayer.gif "rs_IconMapPolygonLayer")|Polygon Layer|Add a map layer that displays areas or shapes that are based sets of polygon coordinates.|  
|![rs_IconMapLineLayer](../media/rs-iconmaplinelayer.gif "rs_IconMapLineLayer")|Line Layer|Add a map layer that displays paths or routes that are based on sets of line coordinates.|  
|![rs_IconMapPointLayer](../media/rs-iconmappointlayer.gif "rs_IconMapPointLayer")|Point Layer|Add a map layer that displays locations that are based on sets of point coordinates.|  
|![rs_IconMapTileLayer](../media/rs-iconmaptilelayer.gif "rs_IconMapTileLayer")|Tile Layer|Add a map layer that displays Bing Map tiles that correspond to the current map view area that is defined by the viewport.|  
  
 At the bottom of the Map pane is the Map view area. To change the center or zoom options for the map, use the arrow keys to adjust the view center and the slider to adjust the zoom level.  
  
 For more information about layers, see [Maps &#40;Report Builder and SSRS&#41;](maps-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="AddLayer"></a> To add a layer from the map layer wizard  
  
-   From the Ribbon, on the **Insert** menu, click **Map**, and then click **Map Wizard.** The wizard enables you to add a layer to the existing map. Most wizard pages are identical between the map wizard and the map layer wizard.  
  
     For more information, see [Map Wizard and Map Layer Wizard &#40;Report Builder and SSRS&#41;](map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md).  

##  <a name="ChangeLayer"></a> To change options for a layer by using the map layer wizard  
  
-   Run the map layer wizard. This wizard enables you to change options for a layer that you created by using the map layer wizard. In the Map pane, right-click the layer, and on the toolbar, click the layer wizard button (![rs_IconMapLayerWizard](../../tutorials/media/rs-iconmaplayerwizard.gif "rs_IconMapLayerWizard")).  
  
     For more information, see [Map Wizard and Map Layer Wizard &#40;Report Builder and SSRS&#41;](map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md).  

##  <a name="AddVectorLayer"></a> To add a point, line, or polygon layer from the Map pane toolbar  
  
1.  Click the map until the Map pane appears.  
  
2.  On the toolbar, click the **Add Layer** button, and from the drop-down list, click the type of layer that you want to add: **Point**, **Line**, or **Polygon**.  
  
    > [!NOTE]  
    >  Although you can add a map layer and configure it manually, we recommend that you use the map layer wizard to add new layers. To launch the wizard from the Map pane toolbar, click the layer wizard button (![rs_IconMapLayerWizard](../../tutorials/media/rs-iconmaplayerwizard.gif "rs_IconMapLayerWizard")).  
  
3.  Right-click the layer, and then click **Layer Data**.  
  
4.  In **Use spatial data from**, select the source of spatial data. Options vary based on your selection.  
  
     If you want to visualize analytical from your report on this layer, do the following:  
  
    1.  Click **Analytical data**.  
  
    2.  In **Analytical dataset**, click the name of the dataset that contains analytical data and the match fields to build a relationship between analytical and spatial data.  
  
    3.  Click **Add**.  
  
    4.  Type the name of the match field from the spatial dataset.  
  
    5.  Type the name of the match field from the analytical dataset.  
  
     For more information about linking spatial and analytical data, see [Customize the Data and Display of a Map or Map Layer &#40;Report Builder and SSRS&#41;](customize-the-data-and-display-of-a-map-or-map-layer-report-builder-and-ssrs.md).  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

##  <a name="FilterAnalyticalData"></a> To filter analytical data for the layer  
  
1.  Click the map until the Map pane appears.  
  
2.  Right-click the layer in the Map pane, and then click  **Layer Data**.  
  
3.  Click **Filters**.  
  
4.  Define a filter equation to limit the analytical data that is used in the map display. For more information, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](filter-equation-examples-report-builder-and-ssrs.md).  

##  <a name="PointProperties"></a> To control point properties for a point layer or for polygon center points  
  
1.  Select **General** on the **Map Point Properties** dialog box to change label, tooltip, and marker type options for the following map elements:  
  
    -   All dynamic or embedded points on a point layer. Color rules, size rules, and marker type rules for points override these options. To override options for a specific embedded point, use the [Map Embedded Point Properties Dialog Box, Marker](../map-embedded-point-properties-dialog-box-marker.md) page.  
  
    -   The center point for all dynamic or embedded polygons on a polygon layer. Color rules, size rules, and marker type rules for center points override these options. To override options for a specific center point, use the [Map Embedded Point Properties Dialog Box, Marker](../map-embedded-point-properties-dialog-box-marker.md) page.  

##  <a name="Embedded"></a> To specify embedded data as a source of spatial data  
  
1.  Click the map until the Map pane appears.  
  
2.  Right-click the layer, and then click **Layer Data**.  
  
3.  In **Use spatial data from**, select **Data embedded in report**.  
  
4.  To load map elements from an existing report or to create map elements based on an ESRI file, click **Browse**, point to the file, and then click **Open**. The map elements are embedded in this report definition. The spatial data that you point to must match the layer type. For example, for a point layer, you must point to spatial data that specifies sets of point coordinates.  
  
5.  In **Spatial field**, specify the name of the field that contains spatial data. You might need to determine this name from the source of spatial data.  
  
    > [!NOTE]  
    >  If you do not know the name of the field and you browsed to an ESRI Shapefile, use the **Link to ESRI shape file option** instead of this option.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

##  <a name="ESRI"></a> To specify an ESRI Shapefile as a source of spatial data  
  
1.  Click the map until the Map pane appears.  
  
2.  Right-click the layer, and then click **Layer Data**.  
  
3.  In **Use spatial data from**, select **Link to ESRI Shapefile**.  
  
4.  In **File name**, type the location of an ESRI Shapefile, or click **Browse** to select an ESRI Shapefile.  
  
    > [!NOTE]  
    >  If the Shapefile is on your local computer, the spatial data is embedded in the report definition. To retrieve the data dynamically when the report is processed, you must upload the ESRI .shp file and its .dbf support file to the report server. For more information, see " How to: Upload a File or Report (Report Manager)" in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in SQL Server Books Online.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

##  <a name="DatasetField"></a> To specify a report dataset field as a source of spatial data  
  
1.  Click the map until the Map pane appears.  
  
2.  Right-click the layer, and then click **Layer Data**.  
  
3.  In **Use spatial data from**, select **Spatial field in a dataset**.  
  
4.  In **Dataset name**, click the name of a dataset in the report that contains that spatial data that you want.  
  
5.  In **Spatial field name**, click the name of the field in the dataset that contains spatial data.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

##  <a name="TileLayer"></a> To add a tile layer  
  
1.  Click the map until the Map pane appears.  
  
2.  On the toolbar, click the **Add Layer** button, and from the drop-down list, click **Tile Layer**.  
  
    > [!NOTE]  
    >  For more information about the use of Bing map tiles in your report, see [Additional Terms of Use](https://go.microsoft.com/fwlink/?LinkId=151371) and [Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=151372).  
  
3.  Right-click the tile layer in the Map pane, and then click **Tile Properties**.  
  
4.  In **Tile options**, select a tile style. If the Bing map tiles are available, the layer on the design surface updates with the style that you select.  
  
    > [!NOTE]  
    >  A tile layer can also be added when you add a polygon, line, or point layer in the Map or Map Layer wizard. On the **Choose spatial data and map view options** page, select the option **Add a Bing Maps background for this map view**.  

##  <a name="DrawingOrder"></a> To change the drawing order of a layer  
  
1.  Click the map until the Map pane appears.  
  
2.  Click the layer in the Map pane to select it.  
  
3.  On the Map pane toolbar, click the up or down arrow to change the drawing order of each layer.  

##  <a name="Transparency"></a> To change the transparency of a polygon, line, or point layer  
  
1.  Click the map until the Map pane appears.  
  
2.  Right-click the layer, and then click **Layer Data**.  
  
3.  Click `Visibility`.  
  
4.  In **Transparency options**, type a value that represents the percentage transparency, for example, **40**. Zero (0) % transparency means that the layer is opaque. 100% transparency means that you will not see the layer in the report.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

##  <a name="TileTransparency"></a> To change the transparency of a tile layer  
  
1.  Click the map until the Map pane appears.  
  
2.  Right-click the layer, and then click **Tile Properties**.  
  
3.  Click `Visibility`.  
  
4.  In **Transparency options**, type a value that represents the percentage transparency, for example, **40**.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

##  <a name="Secure"></a> To specify a secure connection for a tile layer  
  
1.  Click the map until the Map pane appears.  
  
2.  In the Map pane, click the tile layer to select it. The Properties pane displays the tile layer properties.  
  
3.  In the Properties pane, set UseSecureConnection to **True**.  
  
 The connection for the Bing Maps Web service will use the HTTP SSL (Secure Sockets Layer) service to retrieve Bing map tiles for this layer.  

##  <a name="Language"></a> To specify the language for tile labels  
  
1.  By default, for tile styles that display labels, the language is determined from the default locale for Report Builder. You can customize the language setting for tile labels in the following ways.  
  
    -   Click the map outside the viewport to select the map. In the Properties pane, for the TileLanguage property, select a culture value from the drop-down list.  
  
    -   Click the report background to select the report. In the Properties pane, from for the Language property, select a culture value from the drop-down list.  
  
     The order of precedence for setting the tile label language is: report property Language, default locale for Report Builder, and map property TileLanguage.  

##  <a name="ConditionalHide"></a> To conditionally hide a layer based on viewport zoom level  
  
1.  Set `Visibility` options to control the display for a map layer.  
  
    -   In the Map Layers pane, right-click a layer to select it, and on the Map Layers toolbar, click Properties to open **Map Layer Properties**.  
  
    -   Click `Visibility`.  
  
    -   In Layer visibility, select **Show or hide based on zoom value**.  
  
    -   Enter minimum and maximum zoom values for when display the layer.  
  
    -   Optional. Enter a value for transparency.  
  
     You can also conditionally hide the layer. For more information, see [Hide an Item &#40;Report Builder and SSRS&#41;](../report-builder/hide-an-item-report-builder-and-ssrs.md).  

## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](maps-report-builder-and-ssrs.md)   
 [Troubleshoot Reports: Map Reports &#40;Report Builder and SSRS&#41;](troubleshoot-reports-map-reports-report-builder-and-ssrs.md)  
