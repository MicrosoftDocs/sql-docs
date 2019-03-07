---
title: "Troubleshoot Reports: Map Reports (Report Builder and SSRS) | Microsoft Docs"
ms.date: 01/17/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: a690aec2-056b-40bc-8cab-c694bd2d6d62
author: markingmyname
ms.author: maghan
---
# Troubleshoot Reports: Map Reports (Report Builder and SSRS)
  Issues with maps in a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report might occur when you add a map or map layer to your report, when you customize an existing map or map layer in your report, when you preview a map in a report, or when you publish a report with a map. Use this topic to help troubleshoot these issues.  
    
   ## Need more help?  
   
  Try:  
 * [SQL Server Reporting Services](https://stackoverflow.com/questions/tagged/reporting-services) on Stack Overflow  
 * Log an issue or suggestion at [Microsoft SQL Server UserVoice](https://feedback.azure.com/forums/908035-sql-server).  

  
##  <a name="Embedded"></a> Report Definition Size Issues  
 Use this section to help solve issues that relate to report definition size.  
  
## How do I reduce the report definition size?  
 A map layer contains map elements that are created from spatial data. In some cases, map elements are embedded in the report definition. This happens in the following ways:  
  
-   If the source of spatial data is from a map in the Map Gallery or from an ESRI Shapefile on your local computer, map elements are automatically embedded in the report definition.  
  
     If you publish a report to the report server and there is a spatial data source reference to a local file, the spatial data cannot be retrieved at report processing time. To avoid this issue, the map data is embedded in the report definition.  
  
-   In the Map wizard or Layer wizard, if you select the option to embed spatial data, map elements based on the spatial data are embedded in the map layer in the report definition.  
  
-   In the Map pane, if you right-click the layer, and then click one of the **Embed Spatial Data** options, map elements based on the spatial data are embedded in the map layer in the report definition.  
  
-   To remove embedded data that is based on an ESRI Shapefile from a report definition, you must do the following:  
  
1.  Upload or publish the ESRI .shp and .dbf files to the report server.  
  
2.  In the report, in the Map pane in Design view, select the layer that has embedded data, and open the **Layer Data** properties. In **Use spatial data from**, select **Link to ESRI Shapefile**, and then browse to the folder on the report server that contains the ESRI Shapefiles, select it, and click OK.  
  
3.  Save your report. The embedded data for the layer that you changed has been removed from the report definition.  
  
 Map elements from a report in the Map Gallery will always be embedded in a map layer.  
  
##  <a name="Spatial"></a> Spatial Data Issues  
 Use this section to help solve issues that relate to spatial data.  
  
## On the design surface, I see sample spatial data  
 At design time, the design surface might show the message about sample spatial data for the following reasons:  
  
-   Spatial data comes from an ESRI .shp file, but the corresponding .dbf file is not available. ESRI Shapefiles usually include both a .shp file with spatial data and a support file .dbf. Verify that the .dbf file is in the same directory as the .shp file.  
  
-   Spatial data comes from a dataset and the data connection for the query is not valid or the current credentials are not valid.  
  
-   The map layer contains a property with an expression. Expressions are not evaluated until the report runs. To see the map, you must preview the report.  
  
-   Spatial data comes from a dataset that has a specific scope. For example, when a map is nested in a tablix data region, or the map uses the same dataset scope for analytical and for spatial data, the data scope is not calculated until the report runs.  
  
## When I set an offset for an individual map element, a cluster of map elements move  
 Spatial data defines the map elements that are displayed on each map layer. A map element can be based on spatial data that is a single point, a set of points, a single line, a set of lines, a single polygon, or a set of polygons. Each map element is a unit. If a map element contains multiple points, and you move the element, all points for that map element will move.  
  
 The data for each map element is determined by the format of the spatial data from the external source. For example, when a query specifies spatial data from a SQL Server database, each row in the result set can contain multiple sets of point or line or polygon coordinates. All map elements that are defined by a single row in the result set are treated as a unit. If you want to vary the display of specific sets of coordinates, you must do one of the following:  
  
-   Change the query to return the coordinate sets as separate rows in the result set.  
  
-   Select the map elements to vary and set the corresponding embedded point, line, or polygon properties by overriding the default display properties for the corresponding layer type.  
  
## My layer that uses spatial data from an ESRI Shapefile always has embedded data.  
 To ensure that reports with maps can run on a report server, ESRI Shapefiles must be available as resources on the report server. If you add a layer to a map and specify a Shapefile that is on your local file system, the spatial data is automatically embedded in the report.  
  
 To replace the embedded data with a link to the ESRI Shapefile, you must upload the .shp file and its matching .dbf file to the report server, and then change the source of spatial data for the layer.  
  
## I renamed a data source or dataset to a friendly name and now no data appears in my map.  
 The report definition is not automatically updated when you manually change the name of any report item.  
  
 When you change the name of a dataset, any data region or map layer that refers to that dataset must be manually updated. To rebind a tablix, chart, or gauge to a dataset, select the item on the design surface, open the data region properties, and select the name of the appropriate dataset. To rebind a map layer to a dataset, select the layer, open the layer properties, and select the name of the appropriate dataset.  
  
## My spatial data contains nulls and empty strings.  
 In spatial data for the map report item, nulls are set to zero (0) and empty strings are set to blank ("").  
  
 For spatial that comes from a SQL Server database, to change this behavior, you must change the query that returns the spatial data.  
  
## My map exceeds the maximum number of spatial elements  
 By default, a map can have 20,000 map elements or 1,000,000 points. If your map exceeds these limits, you can use one of the following approaches:  
  
-   Remove a layer.  
  
-   Decrease the map resolution.  
  
-   Decrease the map viewport coordinates to view a smaller area.  
  
-   If the spatial data comes from a report dataset, set a filter to limit the data from the dataset. The filter must be set on a field that is not a spatial data type.  
  
-   If the spatial data comes from a SQL Server database, change the query to use spatial functions to limit the data to a smaller area.  
  
##  <a name="Viewport"></a> Viewport Center and View Issues  
 Use this section to help solve issues that relate to viewport options.  
  
## I cannot set the center and view on an embedded map element.  
 To center a viewport on a specific map element, you must have associated the spatial data on a layer with analytical data.  
  
## I set the map center and view in my report. When I reopen the report, why isn't the map view the same?  
 If the user credentials that are needed to read spatial data are not available to the report when you open it, placeholder spatial data is used. Depending on the center and zoom options set for the map viewport, the map view might center on a different layer.  
  
 To reload the spatial data and use the map view center saved in the report, right-click the map viewport, and then click **Reload**. After you enter the credentials for the spatial data source, the layer loads the spatial data and the map view is restored.  
  
## The center and view for a map layer option does not work.  
 When the viewport is set to center on the spatial data for a specific layer, and the center of the view does not appear to be the center for the layer, there are probably small islands or areas that are included in the spatial data that are too small to be seen in the viewport. For example, spatial data for a country might include small islands or other small territories as part of the territory. The viewport uses all spatial data to calculate the center for the layer.  
  
 To override calculations for the layer, you can do one of the following:  
  
-   Specify a custom center for the viewport.  
  
-   Change the zoom level for the viewport to eliminate the locations that you do not want to include.  
  
-   Embed the spatial data in the report and delete the locations that you do not want to include.  
  
##  <a name="Layers"></a> Layer Issues  
 Use this section to help solve issues that relate to layer options.  
  
## I do not see one or more layers in my map.  
 Whether you see a map layer in a report depends on the availability of the spatial data, the relationship between the spatial data and the analytical data, the type of spatial data and the corresponding layer type, the visibility and transparency options on the layer, and the layer drawing order. If you do not see data from a layer, check the following options:  
  
-   **Layer type and spatial data type.** The layer type displays only spatial data that matches the layer type. For example, if the layer type is Point but the spatial data is Line, no data will appear.  
  
-   **Match field values.** The values in the fields that you specify to relate analytical data and spatial data must uniquely identify each map element. The fields must have the same data type. The values in the fields must be identical. For more information, see [Legend, Color Scale, and Distance Scale Issues](#Legend).  
  
-   **Layer order.** The order of layers in the Map pane is the order in which layers are drawn in the report renderer. Spatial data on layers which are drawn first might be overwritten by spatial data for layers that are drawn later. Layers that appear at the top of the list are drawn first. When you change the order layers in the list, you are changing the drawing order of the layers.  
  
-   **Transparency.** You can specify the transparency for each map layer independently. Default values for transparency differ depending on how you add a layer. A transparency of 0% means the layer is opaque and no other layer data will show through this one. To allow other data to show through an existing layer, adjust the value to a higher percentage that gives you the effect that you want.  
  
-   **Visibility.** Visibility for a layer is either **Visible**, **Hidden**, or **ZoomBased**, based on the zoom level of the map viewport. The maximum and minimum range for zoom level can also be specified. Visibility can be based on an expression that evaluates to one of these values.  
  
    > [!TIP]  
    >  You can toggle visibility for each layer in the Map pane. When you are designing each layer, toggle all other layers off to determine whether the issue is for an individual layer or is for transparency issues among layers.  
  
## I set a filter on the map layer and it has no effect.  
 To filter data for a layer, the data type in the filter expression must be specified. Verify that you have specified the correct underlying data type so that the filter equation correctly evaluates the specified condition. For more information, see [Filter Equation Examples &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-equation-examples-report-builder-and-ssrs.md).  
  
##  <a name="Legend"></a> Legend, Color Scale, and Rule Issues  
 Use this section to help solve issues that relate to rules, legend, and color scale options.  
  
## How do I control the values in the map legend?  
 Legend values are determined automatically from map element type rules that you specify for each map layer and by distribution rules that you specify for the legend.  
  
 By default, all items generated by all rules display in the first legend. Values for all polygon, line, and point rules for each layer contribute to the combined legend range. To display items in different legends, you must first create multiple legends and then, for each rule, specify in which legend to display the related items.  
  
 To associate a rule with a specific legend, open the rule properties, and on the Legend page, select the name of the legend to use. To remove items from a legend, in legend options, select the blank line for the name of the legend. If you rename legend elements in the report, you must manually associate each layer with the appropriate legend item.  
  
 To control the title and content for each legend, use the Legend properties for the rule. You can specify how many divisions to create, change the calculations that assign values to each division, set minimum and maximum range values, and change the format of the legend text.  
  
 For more information, see [Change Map Legends, Color Scale, and Associated Rules &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-map-legends-color-scale-and-associated-rules-report-builder-and-ssrs.md).  
  
## The rules that I set do not give the results that I expect.  
 Rules apply to the analytical data that is associated with map elements on a layer. Use the following list to help identify issues with all color rules, size rules, width rules, and marker type rules:  
  
-   The precedence for applying style to each map element (polygon, line, point) is, from lowest to highest precedence: layer properties; map element properties for all map elements on the layer; rules that you specify; and then, for embedded map elements that you select the override option for, the values that you specify. Once you select the override option for an embedded element, rules no longer apply, even if you later change values back to their original setting.  
  
-   Match field issues. Match fields enable data binding between map elements and analytical data. The spatial data and analytical data fields that correspond to match fields must have the same data type and the same format. If the match field does not exactly match the spatial data and analytical data, the rule has no effect. For example, if the match field for spatial data has extra blanks or extra punctuation compared to the match field for the analytical data, no match occurs.  
  
-   For more information, see [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
## What is the value NaN on the Color Scale?  
 **NaN** stands for Not a Number. Color scales values are expected to be numeric. Check the distribution settings and the legend text value for the rules that are associated with the color scale. If you created custom distribution ranges, verify that you specified the lower bound on the first range and the upper bound on the last range.  
  
## My color scale does not appear when I run the report.  
 The color scale displays information to the user when a map layer specifies color rules for polygons, lines, or points for the whole layer or for embedded map elements. If no map element specifies a color rule, or if the color rules specify by using a legend instead of the color map, then the color map does not appear in the rendered report.  
  
 To display the color scale, specify color rules for a layer or an embedded map element. For more information, see [Change Map Legends, Color Scale, and Associated Rules &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/change-map-legends-color-scale-and-associated-rules-report-builder-and-ssrs.md).  
  
##  <a name="Tile"></a> Tile Issues  
 Use this section to help solve issues that relate to tile background options.  
  
## I cannot see the Bing maps tile background.  
 The following settings affect whether a Bing maps tile background displays in local preview or on a report that runs from the report server:  
  
-   The map tile layer must exist. In the Map wizard or Layer wizard, select **Add a Bing Maps background for this map view**. This adds a tile layer for the current map viewport view center and zoom level. You can also add a tile layer from the Map pane toolbar.  
  
-   The map coordinate system for the viewport must be **Geographic**, not **Planar**.  
  
-   The map projection must be **Mercator**.  
  
-   For local preview, you must have internet access. For a report that runs from the report server, the report server must be configure to support tile background. For more information, see "Planning for Map Support" in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) in SQL Server Books Online.  
  
 For more information about adding a tile layer, see [Add, Change, or Delete a Map or Map Layer &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-change-or-delete-a-map-or-map-layer-report-builder-and-ssrs.md).  
  
## How do I control the text on a tile layer?  
 Both **Road** and **Hybrid** views include text. The text is part of the tiles that come from Bing Maps Web Services.  
  
 To include a tile layer without text, select **Aerial** view.  
  
##  <a name="Tooltip"></a> ToolTip and Label Issues  
 Use this section to help solve issues that relate to label or ToolTip options.  
  
## I get an expression error about dataset scope when I set a label or ToolTip to an expression.  
 When your spatial data comes from a map gallery or an ESRI Shapefile, the associated data is not part of a report dataset. You cannot use expression syntax for a dataset field reference to specify this data for a label or tooltip.  
  
 To specify data that is related to spatial data that is not part of a report dataset, you must use the symbol # followed by a label that specifies the name of the data.  
  
## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md)   
 [Troubleshoot Report Builder](https://msdn.microsoft.com/3806fc48-56f8-44d1-a3c1-df8c33cce0a3)  
  
  
