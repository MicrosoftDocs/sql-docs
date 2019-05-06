---
title: "Map Wizard and Map Layer Wizard (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: reference
f1_keywords: 
  - "sql13.rtp.rptdesigner.mapandlayerwizard.f1"
  - "10542"
  - "MICROSOFT.REPORTDESIGNER.MAPLAYER.NAME"
ms.assetid: 48cbe18b-1290-4107-8a1c-ec6acd71f73b
author: maggiesMSFT
ms.author: maggies
---
# Map Wizard and Map Layer Wizard (Report Builder and SSRS)
 In [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated reports, the Map Wizard and Map Layer Wizard automate the task of creating a map, adding a map layer, or changing map layer options on an existing layer.  
  
 Before you add a map to a report or a map layer to a map, gather the following information:  
  
-   **Spatial data source.** The location or connection to a source that provides spatial data, for example, the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and a database that contains spatial data, or the name of an Environmental Systems Research Institute, Inc. (ESRI) Shapefile.  
  
-   **Spatial data.** From the spatial data source, a field that contains sets of coordinates that specify locations.  
  
-   **Analytical data.** Analytical data to use to vary the map display, for example, annual store sales.  
  
-   **Match fields.** Match fields that define the relationship between spatial data and analytical data, for example, the name of a region and city to uniquely identify each city.  
  
 The following sections provide information about options that you specify when in the Map and Map Layer Wizards.  
  
-   When you first open Report Builder, click the **Map** wizard icon in the center of the design surface.  
  
-   On the **Insert** tab, click **Map**, and then click **Map Wizard**.  
  
 To open the Map Layer wizard, do the following action:  
  
-   Click the map to display the Map pane and on the toolbar, click the **New layer wizard** button.  
  
 Click the title of the wizard page for the corresponding help content. The pages that you see vary depending on your choices for the type of map, the source of spatial data, and the source of analytical data.  
  
1.  [Choose a source of spatial data](#SpatialDataSource). Spatial data can come from the map gallery, an Environmental Systems Research Institute, Inc. (ESRI) Shapefile, or from spatial data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database.  
  
    -   [What is spatial data?](#SpatialData)  
  
    -   [What is the Map gallery?](#MapGallery)  
  
    -   [What is an ESRI Shapefile?](#Shapefile)  
  
    -   [Where can I get ESRI shapefiles?](#GetShapefiles)  
  
    -   [What is a SQL Server spatial query?](#SqlServerSpatial)  
  
2.  [Choose spatial data and map view options](#MapView). Set the map view, the map resolution, specify whether to embed spatial data in the report, and specify whether to include a tile background of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Bing map tiles.  
  
    -   [What is the map view or viewport?](#Viewport)  
  
    -   [What is map resolution or optimization?](#Resolution)  
  
    -   [What does embedding spatial data do?](#Embed)  
  
    -   [What is a Bing maps tile background?](#Tiles)  
  
3.  [Choose map visualization](#Visualization). Choose the type of map to create.  
  
    -   [What is the difference between a Basic Map, a Bubble Map, and an Analytical Map?](#MapType)  
  
    -   Choose map visualization: Polygons  
  
    -   Choose map visualization: Lines  
  
    -   Choose map visualization: Points  
  
4.  Choose a connection to a data source Choose map visualization: Points. Choose a data source connection or create one to an external data source that contains analytical data to display on the map.  
  
5.  Design a query. Build a query that specifies the analytical data.  
  
6.  [Choose the analytical dataset](#AnalyticalData). Specify a data source for the analytical data.  
  
    -   [What is the difference between spatial data and analytical data?](#Diff)  
  
7.  [Specify the match fields for spatial and analytical data](#SpecifyMatchFields). Build a relationship between the spatial data and analytical data so that the appearance of map elements can vary based on data.  
  
    -   [What is a match field?](#MatchFields)  
  
8.  [Choose color theme and data visualization](#ThemeandVisualization). To specify how to visualize your data against the map background, specify the map theme, the fields to visualize, and what to vary: color, size, and/or marker type.  
  
    -   [What does the theme do?](#Theme)  
  
    -   [What are the legends and scales in Map Preview for?](#Legends)  
  
    -   [What are rules?](#Rules)  
  
 After you add a map or map layer and preview the report, you can change map and map layer options that you set in the wizards. For more information, see [Customize the Data and Display of a Map or Map Layer &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/customize-the-data-and-display-of-a-map-or-map-layer-report-builder-and-ssrs.md).  
  
 For more information about maps, see [Maps &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md). For step-by-step instructions to add a map to a report, see [Tutorial: Map Report &#40;Report Builder&#41;](../../reporting-services/tutorial-map-report-report-builder.md).  
  
##  <a name="SpatialDataSource"></a> Choose a source of spatial data  
 On this page, specify the spatial data source and which spatial data to include. Spatial data can come from the map gallery, an ESRI Shapefile, or a dataset query that specifies [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] spatial data from a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] or later version database.  
  
 You can use the same source or a different source of spatial data for each layer, but you must specify the source every time you add a layer. When the spatial data is from the map gallery or an ESRI Shapefile, the spatial data source is not a separate report item. It does not appear in the Report Data pane.  
  
###  <a name="SpatialData"></a> What is spatial data?  
 Spatial data contains coordinates that define geographic or geometric elements. In a map, spatial data defines *map elements*: polygons that define areas or shapes, lines that define routes or paths, and points that define markers or pushpins. Spatial data is stored in binary format on the data source and is specified as sets of coordinates. For example, a point is an X and Y coordinate (X Y), a line is two sets of coordinates ((X1 Y1), (X2 Y2)), a polygon is four or more sets of coordinates where the first and last set of coordinates are the same ((X1 Y1), (X2 Y2), (X3 Y3), (X1 Y1)).  
  
 For more information, see the documentation for the type of spatial data that you use.  
  
###  <a name="MapGallery"></a> What is the map gallery?  
 The map gallery contains maps from reports that are located in the map gallery folder for the report authoring environment. Maps from the gallery provide a quick start to add a map to your report. The predefined maps in the gallery are provided by a map provider.  
  
> [!NOTE]  
>  This [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] mapping feature uses data from TIGER/Line Shapefiles provided courtesy of the U.S. Census Bureau ([https://www.census.gov/](https://www.census.gov/)). TIGER/Line Shapefiles are an extract of selected geographic and cartographic information from the Census MAF/TIGER database. TIGER/Line Shapefiles are available without charge from the U.S. Census Bureau. To obtain more information about the TIGER/Line Shapefiles go to [https://www.census.gov/geo/www/tiger](https://www.census.gov/geo/www/tiger). The boundary information in the TIGER/Line Shapefiles are for statistical data collection and tabulation purposes only; their depiction and designation for statistical purposes does not constitute a determination of jurisdictional authority or rights of ownership or entitlement and they are not legal land descriptions. Census TIGER and TIGER/Line are registered trademarks of the U.S. Bureau of the Census.  
  
 To extend the map gallery, you can add or remove reports from the map gallery directory, and add folders to organize the maps. For more information, see [Maps &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md).  
  
###  <a name="Shapefile"></a> What is an ESRI shapefile?  
 An ESRI Shapefile is a set of files with data that conforms to the Environmental Systems Research Institute, Inc. (ESRI) shapefile spatial data format. The set of files typically includes the *\<filename>*.shp file that contains the spatial data and a support file, *\<filename>*.dbf.  
  
 When you specify a shape file as a spatial data source and it is located on your local computer, the spatial data is automatically embedded in the report. To use spatial data from an ESRI file dynamically, you must do the following:  
  
 In Report Builder, upload both the .shp file and the .dbf file to the same folder on a report server, and then link to the .shp file as the spatial data source.  
  
 In Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], add both the .shp file and the .dbf file to the report project, and then specify the name of the .shp file as the spatial data source.  
  
###  <a name="GetShapefiles"></a> Where can I get ESRI shapefiles?  
 ESRI shapefiles are available on the Web. For more information, see [Finding ESRI Shapefiles for a Map](https://go.microsoft.com/fwlink/?linkid=178814).  
  
###  <a name="SqlServerSpatial"></a> What is a SQL Server spatial query?  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] spatial query is a dataset query that specifies data that is either a SQLGeometry or a SQLGeography data type from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database.  
  
> [!NOTE]  
>  When you define a data source in the wizard, you will see different query designers in the Design a Query page, depending on what type of data source you are connecting to. For more information, see [Query Designers &#40;Report Builder&#41;](https://msdn.microsoft.com/library/553f0d4e-8b1d-4148-9321-8b41a1e8e1b9).  
  
 When you run the query in the query designer, the result set displays a column with spatial data that appears as text. For example, one row might contain spatial data that is a single point and the next row might contain spatial data that defines a set of points. Each row becomes one map element. You can vary the display of each map element as an indivisible unit.  
  
 For more information, see [Spatial Data Types](../../relational-databases/spatial/spatial-data-types-overview.md).  
  
##  <a name="MapView"></a> Choose spatial data and map view options  
 On this page you can set the following options:  
  
-   Set the view center and zoom level for the spatial data that you selected in the previous wizard page. The view that you set applies to the whole map.  
  
-   Set the map resolution.  
  
-   Specify whether to embed the spatial data in the report.  
  
-   For embedded data, specify whether to include all data or just the data in the current view.  
  
-   Specify whether to include a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Bing map tiles background.  
  
###  <a name="Viewport"></a> What is the map view or viewport?  
 The map viewport defines the map area to display for all layers in your report.  
  
 By default, color scale and distance scale appear inside the viewport, and the map legend appears outside the viewport. You can change these options for the viewport after you complete the wizard.  
  
###  <a name="Resolution"></a> What is map resolution and optimization?  
 When you change the resolution for spatial data that represents lines or polygons, you are specifying how detailed you want the map to be drawn. For example, for aerial views of a region, do you need granularity down to a hundred meters of surface area on the earth, or is one mile resolution enough?  
  
 When spatial data is embedded in the report, a higher resolution increases the number of elements that are needed to draw details at that resolution. When the spatial data is not embedded in the report, a higher resolution increases the amount of time required by the report processor to calculate the lines for the map at that resolution every time you view the report.  
  
 When you lower the resolution, the report processor applies an algorithm to reduce the number of points needed to draw the map elements. The lower the resolution, the less data is required to display the map elements, which can lead to better display performance.  
  
 As you adjust the slider, the preview data in the wizard pane updates to give you an indication of the effect. After you add the map to the report, you can adjust this value by changing map viewport options.  
  
###  <a name="Embed"></a> What does embedding spatial data do?  
 When you embed map elements or Bing map tiles in a report, the spatial data is stored in the report definition.  
  
 A report with a map can use spatial data or Bing map tiles that are retrieved dynamically either when the report is processed or at design time and then embedded in the report definition. Embedded map elements can significantly increase the size of the report definition, but reduce the time it takes to view the map in the report. Dynamic map elements reduce the report definition size but increase the time it takes to process and view the map.  
  
 Good report design requires that you assess the trade-offs for static or dynamic map data and find the balance that works for your circumstances. In general, more data means the report definition and the compiled report requires more storage on the report server and longer processing times. It is always a best practice to crop spatial data, as well as limit other report data, to include just what is needed for the report.  
  
###  <a name="Tiles"></a> What is a Bing map tile background?  
 To add a geographic image background to your map, select the Bing map tile background option. The report processor downloads tiles from Bing Maps Web Services for the map area and resolution that you specify on this wizard page. You can specify one of the following tile types:  
  
-   **Road.** Display a road map style with white background.  
  
-   **Aerial.** Display an aerial view only. No text is displayed in this mode.  
  
-   **Hybrid.** Display the combination of **Road** and **Aerial** views.  
  
 For more information about tiles, see [Bing Maps Tiles System](https://go.microsoft.com/fwlink/?LinkId=147315). For more information about the use of Bing map tiles in your report, see [Additional Terms of Use](https://go.microsoft.com/fwlink/?LinkId=151371).  
  
 To see a tile background in Design view, you must have Internet access. To see the tile background in preview from a report on a report server, the report server must be configured to support Bing map tiles. For more information, see [Troubleshoot Reports: Map Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/troubleshoot-reports-map-reports-report-builder-and-ssrs.md) and [Plan a Map Report](../../reporting-services/report-design/plan-a-map-report-report-builder-and-ssrs.md).  
  
 For more information on other ways to customize a tile layer, see [Add, Change, or Delete a Map or Map Layer &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-change-or-delete-a-map-or-map-layer-report-builder-and-ssrs.md).  
  
##  <a name="Visualization"></a> Choose map visualization  
 On this page, choose the type of map or map layer to add to your report. The first time you run the wizard, you are adding the map and the first map layer to the report. A map can contain multiple map layers. Each map layer displays a specific type of spatial data: polygons, lines, or points.  
  
 The type of map that you choose depends on the purpose of the map and the data that you have available.  
  
###  <a name="MapType"></a> What is the difference among a Basic Map, a Bubble Map, and an Analytical Map?  
 A **Basic Map** displays locations only. You can vary the colors of the areas on the map by shade, but the color does not represent analytical data values.  
  
 A **Bubble Map** conveys the relative value for a single analytical data aggregate as bubble size, for example, store sales. You can create bubble maps for either polygons or points. For polygons, set the polygon center point properties; for points, set the marker properties.  
  
 An **Analytical Map** conveys the relative value of one or more analytical data aggregates for each map element. For example, store sales as marker size, profit range for product categories as marker color, and top selling product as marker type.  
  
 For more information, see [Plan a Map Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/plan-a-map-report-report-builder-and-ssrs.md).  
  
##  <a name="AnalyticalData"></a> Choose the analytical dataset  
 On this page, specify where to get the analytical data to display on this map layer.  
  
 To display your report data or any analytical data against the map background, you must specify where the data is and how it is related to the spatial data. Your data can come from an existing report dataset, or from a new dataset that you build a query for. Existing analytical data might be included in the ESRI Shapefile that contains the spatial data.  
  
###  <a name="Diff"></a> What is the difference between spatial data and analytical data?  
 Spatial data consists of sets of coordinates that specify points, lines, and polygons. Map elements are based on spatial data.  
  
 Analytical data is numeric or categorical data that you want use to vary the appearance of the map. Analytical can come from a report dataset or might be included with spatial data from a map from the map gallery or from an ESRI shape file.  
  
##  <a name="SpecifyMatchFields"></a> Specify the match fields  
 On this page, build a relationship between the spatial data and analytical data.  
  
###  <a name="MatchFields"></a> What are match fields?  
 Match fields enable the report processor to build a relationship between the analytical data and the spatial data. Match fields specify unique values within the analytical data. For example, the store name might not be unique within the data, so you could specify both a city and the store name.  
  
##  <a name="ThemeandVisualization"></a> Choose color theme and data visualization  
 On this page, specify how to visualize your data against the map background, the map theme, the fields to visualize, and what to vary: color, size, and/or marker type.  
  
###  <a name="Theme"></a> What does the theme do?  
 The theme that you choose sets default values for color, border, and font. You can change these options after you complete the wizard.  
  
###  <a name="Legends"></a> What are the legends and scales in Map Preview for?  
 Legends help a user interpret the data that is displayed on a map. A map provides a color range, a distance scale, and a legend.  
  
-   **Color range.** The color range displays a color bar with a scale that provides a guide to the data intervals that are determined by the report processor based on the rules that you specify for the layer.  
  
-   **Distance scale.** The distance scale provides a guide to the distance units on the map. Distance units are determined automatically based on the map projection and zoom level.  
  
-   **Legend.** The legend provides a guide to help interpret the meaning of colors, sizes, and marker types on a map. By default, all the rules for all the layers display data intervals in the first legend. You can customize this legend and add legends after you add the map to the report.  
  
###  <a name="Rules"></a> What are rules?  
 Rules are calculations that the report processor uses to divide analytical data into ranges. You can specify different rules for each layer. The type of rules you can specify depend on the type of spatial data on the layer:  
  
-   **Polygons.** You can specify color rules.  
  
-   **Center points for polygons.** You can specify color, size, and marker type rules.  
  
-   **Lines.** You can specify color and width rules.  
  
-   **Points.** You can specify color, size, and marker type rules.  
  
 The report processor applies the rules that you set and automatically determines the list of items to display in a legend. By default, the results of all rules for all layers display in the first legend. You can adjust this after you complete the wizard. For more information, see [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
## See Also  
 [Troubleshoot Reports: Map Reports &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/troubleshoot-reports-map-reports-report-builder-and-ssrs.md)   
 [Plan a Map Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/plan-a-map-report-report-builder-and-ssrs.md)   
 [Maps &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md)  
  
  
