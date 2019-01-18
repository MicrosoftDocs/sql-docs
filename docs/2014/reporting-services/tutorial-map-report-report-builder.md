---
title: "Tutorial: Map Report (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 8d831356-7efa-40cc-ae95-383b3eecf833
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Tutorial: Map Report (Report Builder)
  This tutorial is designed to help you learn about the map features you can use to display report data against a geographic background.  
  
 Maps are based on spatial data that typically consists of points, lines, and polygons. For example, a polygon can represent the outline of a county, a line can represent a road, and a point can represent the location of a city. Each type of spatial data is displayed on a separate map layer as a set of map elements.  
  
 To vary the appearance of map elements, you specify a field that has values that match the map elements with analytical data from a dataset. You can also define rules that vary color, size, or other properties based on ranges of data.  
  
 In this tutorial, you will build a map report that displays store locations in New York state counties.  
  
##  <a name="BackToTop"></a> What You Will Learn  
 In this tutorial you will learn how to do the following:  
  
1.  [Create a Map with a Polygon Layer from the Map Wizard](#Map)  
  
2.  [Add a Map Point Layer to Display Store Locations](#PointLayer)  
  
3.  [Add a Map Line Layer to Display a Route](#LineLayer)  
  
4.  [Add a Bing Maps Tile Background](#TileLayer)  
  
5.  [Make a Layer Transparent](#Transparent)  
  
6.  [Vary County Color Based on Sales](#Vary)  
  
    1.  [Build a Relationship between Spatial and Analytical Data](#Relationship)  
  
    2.  [Specify Color Rules for Polygons](#ColorRules)  
  
    3.  [Format the Data in the Color Scale as Currency](#ColorScale)  
  
    4.  [Create a New Legend](#NewLegend)  
  
    5.  [Associate Legend and Color Rules](#Associate)  
  
    6.  [Clear Color from Counties with No Data](#NoData)  
  
7.  [Add a Custom Point](#CustomPoint)  
  
8.  [Center the Map View](#CenterView)  
  
9. [Add a Report Title](#Title)  
  
10. [Save the Report](#Save)  
  
> [!NOTE]  
>  In this tutorial, the steps for the wizard are consolidated into two procedures: one to create the dataset and one to create a table. For step-by-step instructions about how to browse to a report server, choose a data source, create a dataset, and run the wizard, see the first tutorial in this series: [Tutorial: Creating a Basic Table Report &#40;Report Builder&#41;](../reporting-services/tutorial-creating-a-basic-table-report-report-builder.md).  
  
 Estimated time to complete this tutorial: 30 minutes.  
  
## Requirements  
 For information about requirements, see [Prerequisites for Tutorials &#40;Report Builder&#41;](../reporting-services/report-builder-tutorials.md).  
  
##  <a name="Map"></a> 1. Create a Map with a Polygon Layer from the Map Wizard  
 Add a map to your report from the map gallery. The map has one layer that displays the counties in New York state. The shape of each county is a polygon based on spatial data that is embedded in the map from the map gallery.  
  
#### To add a map with the map wizard in a new report  
  
1.  Click **Start**, point to **Programs**, point to [!INCLUDE[ssCurrentUI](../includes/sscurrentui-md.md)]**Report Builder**, and then click **Report Builder**.  
  
     The Getting Started dialog box appears.  
  
    > [!NOTE]  
    >  If the Getting Started dialog box does not appear, from the Report Builder button, click **New**.  
  
2.  In the left pane, verify that **Report** is selected.  
  
3.  In the right pane, click **Map Wizard**.  
  
4.  Click **Create**.  
  
5.  **Choose a source of spatial data** page, verify that **Map gallery** is selected.  
  
6.  In the Map Gallery pane, expand **States by County** under **USA**, and click **New York**.  
  
     The Map Preview pane displays the New York county map.  
  
7.  Click **Next**.  
  
8.  On the **Choose spatial data and map view options** page, accept the defaults. By default, map elements from a map gallery will automatically be embedded in the report definition.  
  
9. Click **Next**.  
  
10. On the **Choose map visualization** page, verify **Basic Map** is selected, and click **Next**.  
  
11. On the **Choose color theme and data visualization** page, select the **Display labels** option.  
  
12. If it is selected, clear the **Single color map** option.  
  
13. From the **Data field** drop-down list, click #COUNTYNAME. The Map Preview pane in the wizard displays the following items:  
  
    -   A title with the text **Map Title**.  
  
    -   A map that displays counties in New York where each county is a different color and the county name appears wherever it fits over the county area.  
  
    -   A legend that contains a title and a list of items 1 through 5.  
  
    -   A color scale that contains values 0 to 160 and no color.  
  
    -   A distance scale that displays kilometers (km) and miles (mi).  
  
14. Click **Finish**.  
  
     The map is added to the design surface.  
  
15. Click the map to select it and display the **Map Layers Pane**. The **Map Layers Pane** shows one polygon layer of layer type **Embedded**. Each county is an embedded map element on this layer.  
  
    > [!NOTE]  
    >  If you do not see the **Map Layers** pane, it might be displayed outside your current view. Use the scroll bar at the bottom of the Design view window to change your view. Alternatively, in the **View** tab, clear the **Properties** or the **Report Data** option to provide more design surface area.  
  
16. Right-click the map title, and then click **Title Properties**.  
  
17. Replace the title text with **Sales by Store**.  
  
18. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
19. Preview the report.  
  
 The rendered report displays the map title, the map, and the distance scale. The counties are on a map polygon layer. Each county is a polygon that varies by color from a color palette, but the colors are not associated with any data. The distance scale displays distances in both kilometers and miles.  
  
 The map legend and color scale do not yet appear because there is no analytical data associated with each county. You will add analytical data later in this tutorial.  
  
##  <a name="PointLayer"></a> 2. Add a Map Point Layer to Display Store Locations  
 Use the map layer wizard to add a point layer that displays the locations of stores.  
  
> [!NOTE]  
>  In this tutorial, the query contains the data values, so that it does not need an external data source. This makes the query quite long. In a business environment, a query would not contain the data. This is for learning purposes only.  
  
#### To add a point layer based on a SQL Server spatial query  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane. On the toolbar, click the **New layer wizard** button ![rs_IconMapLayerWizard](../../2014/tutorials/media/rs-iconmaplayerwizard.gif "rs_IconMapLayerWizard").  
  
3.  On the **Choose a source of spatial data** page, select **SQL Server spatial query**, and click **Next**.  
  
4.  On the **Choose a dataset with SQL Server spatial data** page, click **Add a new dataset with SQL Server spatial data**, and then click **Next**.  
  
5.  On the **Choose a connection to a SQL Server spatial data source** page, select an existing data source or browse to the report server and select a data source.  
  
6.  Click **Next**.  
  
7.  On the Design a Query page, click **Edit as Text**.  
  
8.  Paste the following text in the query pane:  
  
    ```  
    Select 114 as StoreKey, 'Contoso Albany Store' as StoreName, 1125 as SellingArea, 'Albany' as City, 'Albany' as County,   
     CAST(1000000 as money) as Sales, CAST('POINT(-73.7472924218681 42.6564617079878)' as geography) AS SpatialLocation  
    UNION ALL SELECT 115 AS StoreKey, 'Contoso New York No.1 Store' AS  StoreName, 500 as SellingArea, 'New York' AS City, 'New York City' as County,  
     CAST('2000000' as money) as Sales, CAST('POINT(-73.9922069374483 40.7549638237402)' as geography) AS SpatialLocation  
    UNION ALL Select 116 as StoreKey, 'Contoso Rochester No.1 Store' as StoreName, 462 as SellingArea, 'Rochester' as City,  'Monroe' as County,    
     CAST(3000000 as money) as Sales, CAST('POINT(-77.624041566786 43.1547066024338)' as geography)  AS SpatialLocation  
    UNION ALL Select 117 as StoreKey, 'Contoso New York No.2 Store' as StoreName, 700 as SellingArea, 'New York' as City,'New York City' as County,    
      CAST(4000000 as money) as Sales, CAST('POINT(-73.9712488 40.7830603)' as geography) AS SpatialLocation  
    UNION ALL Select 118 as StoreKey, 'Contoso Syracuse Store' as StoreName, 680 as SellingArea, 'Syracuse' as City, 'Onondaga' as County,  
     CAST(5000000 as money) as Sales, CAST('POINT(-76.1349120532546 43.0610223535974)' as geography) AS SpatialLocation  
    UNION ALL Select 120 as StoreKey, 'Contoso Plattsburgh Store' as StoreName, 560 as SellingArea, 'Plattsburgh' as City,  'Clinton' as County,  
     CAST(6000000 as money) as Sales, CAST('POINT(-73.4728622833178 44.7028831413324)' as geography) AS SpatialLocation  
    UNION ALL Select 121 as StoreKey, 'Contoso Brooklyn Store' as StoreName, 1125 as SellingArea, 'Brooklyn' as City, 'New York City' as County,  
     CAST(7000000 as money) as Sales, CAST('POINT (-73.9638533447143 40.6785123489351)' as geography) AS SpatialLocation  
    UNION ALL Select 122 as StoreKey, 'Contoso Oswego Store' as StoreName, 500 as SellingArea, 'Oswego' as City, 'Oswego' as County,    
     CAST(8000000 as money) as Sales, CAST('POINT(-76.4602850815536 43.4353224527794)' as geography) AS SpatialLocation  
    UNION ALL Select 123 as StoreKey, 'Contoso Ithaca Store' as StoreName, 460 as SellingArea, 'Ithaca' as City, 'Tompkins' as County,  
     CAST(9000000 as money) as Sales, CAST('POINT(-76.5001866085881 42.4310489934743)' as geography) AS SpatialLocation  
    UNION ALL Select 124 as StoreKey, 'Contoso Rochester No.2 Store' as StoreName, 700 as SellingArea, 'Rochester' as City, 'Monroe' as County,    
     CAST(100000 as money) as Sales, CAST('POINT(-77.6240415667866 43.1547066024338)' as geography) AS SpatialLocation  
    UNION ALL Select 125 as StoreKey, 'Contoso Queens Store' as StoreName, 700 as SellingArea,'Queens' as City, 'New York City' as County,  
     CAST(500000 as money) as Sales, CAST('POINT(-73.7930979029883 40.7152781765927)' as geography) AS SpatialLocation  
    UNION ALL Select 126 as StoreKey, 'Contoso Elmira Store' as StoreName, 680 as SellingArea, 'Elmira' as City, 'Chemung' as County,  
     CAST(800000 as money) as Sales, CAST('POINT(-76.7397414783301 42.0736492742663)' as geography) AS SpatialLocation  
    UNION ALL Select 127 as StoreKey, 'Contoso Poestenkill Store' as StoreName, 455 as SellingArea, 'Poestenkill' as City, 'Rensselaer' as County,    
    CAST(1500000 as money) as Sales, CAST('POINT(-73.5626737425063 42.6940551238618)' as geography) AS SpatialLocation  
    ```  
  
9. On the query designer toolbar, click **Run** (**!**).  
  
     The result set displays seven columns: StoreKey, StoreName, SellingArea, City, County, Sales, and SpatialLocation. This data represents a set of stores in New York State that sell consumer goods. Each row in the result set contains a store identifier, store name, the area available for product display, the city and county where the store is located, the sales total, and the spatial location in longitude and latitude. The display area ranges from 455 square feet to 1125 square feet.  
  
10. Click **Next**.  
  
     The report dataset named DataSet1 is created for you. After you complete the wizard, you can use the Report Data to its field collection.  
  
11. On the **Choose a spatial data and map view options** page, verify that the **Spatial field** is `SpatialLocation` and that the **Layer type** is **Point**. Accept the other defaults on this page.  
  
     The map view displays circles that mark the location of each store.  
  
12. Click **Next**.  
  
13. Specify a map type that displays markers that vary by analytic data. On the Choose map visualization page, click **Analytical Marker Map**, and then click **Next**.  
  
14. On the **Choose the analytical dataset** page, click DataSet1. This dataset contains both analytical data and spatial data that will be displayed on the new point layer.  
  
15. Click **Next**.  
  
16. On the **Choose color theme and data visualization** page, clear the **Use marker colors to visualize data** option, and then select the option **Use marker types to visualize data**.  
  
17. In **Data field**, select `[Sum(SellingArea)]` to vary marker types by the size of the area a store sets aside to display the products.  
  
18. Click **Finish**.  
  
     The map layer is added to the report. The legend displays marker types based on SellingArea values.  
  
     Double-click the map to display the **Map Layer** pane. The **Map Layer** pane displays a new layer, PointLayer1, with spatial data source type **DataRegion**.  
  
19. Add a legend title. Right-click the legend title, and then click **Legend Title Properties**.  
  
20. Delete the title, and type **Display Area (Square Feet)**.  
  
21. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
22. View the default values that were set by the wizard. In the **Map Layers Pane**, right-click the point layer, and then click **Marker Type Rule**.  
  
     On the **General** tab, the markers are listed in the order in which they appear in the legend. On the **Distribution** tab, the number of subranges is 5. On the **Legend** tab, the legend text is set to display the start and end value in each range.  
  
23. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
24. Preview the report.  
  
 The map displays the locations of stores in New York state. The marker type for each store is based on the display area. Five ranges of display area were automatically calculated for you.  
  
##  <a name="LineLayer"></a> 3. Add a Map Line Layer to Display a Route  
 Use the map layer wizard to add a map layer that displays a route between two stores. In this tutorial, the path is created from three store locations. In a business application, the path might be the best route between stores.  
  
#### To add a line layer to map  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane. On the toolbar, click **New layer wizard**.  
  
3.  On the **Choose a source of spatial data** page, select **SQL Server spatial query** and click **Next**.  
  
4.  On the **Choose a dataset with SQL Server spatial data** page, click **Add a new dataset with SQL Server spatial data** and click **Next**.  
  
5.  On the **Choose a connection to a SQL Server spatial data source**, select DataSource1, the data source that you created in the first procedure.  
  
6.  Click **Next**.  
  
7.  On the **Design a Query** page, click **Edit as Text**. The query designer switches to text-based mode.  
  
8.  Paste the following text in the query pane:  
  
    ```  
    SELECT N'Path' AS Name, CAST('LINESTRING(  
       -76.5001866085881 42.4310489934743,  
       -76.4602850815536 43.4353224527794,  
       -73.4728622833178 44.7028831413324)' AS geography) as Route  
    ```  
  
9. Click **Next**.  
  
     A path appears on the map that connects three stores.  
  
10. On the **Choose spatial data and map view options** page, verify that the **Spatial field** is **Route** and that the **Layer type** is **Line**. Accept the other defaults.  
  
     The map view displays a path from a store in the northern part of New York state to a store in the southern part of New York state.  
  
11. Click **Next**.  
  
12. On the **Choose map visualization** page, click **Basic Line Map**, and then click **Next**.  
  
13. On the **Choose color theme and data visualization**, select the option **Single color map**. The path appears as a single color based on the selected theme.  
  
14. Click **Finish**.  
  
 The map displays a new line layer with spatial data source type **DataSet**. In this example, the spatial data comes from a dataset but no analytical data is associated with the line.  
  
##  <a name="TileLayer"></a> 4. Add a Bing Maps Tile Background  
 Add a map layer that displays a Bing Maps tile background.  
  
#### To add a Virtual Earth tile background  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane. On the toolbar, click **Add Layer**![rs_IconMapAddLayer](../../2014/tutorials/media/rs-iconmapaddlayer.gif "rs_IconMapAddLayer").  
  
3.  From the drop-down list, click **Tile Layer**.  
  
     The last layer in the **Map Layer** pane is TileLayer1. By default, the tile layer displays the road map style.  
  
    > [!NOTE]  
    >  In the wizard, you can also add a tile layer on the **Choose spatial data and map view options** page. To do this, select **Add a Bing Maps background for this map view**. In a rendered report, the tile background displays Bing Maps tiles for the current map viewport center and zoom level.  
  
4.  Click the down arrow on TileLayer1, and then click **Tile Properties**.  
  
5.  In **Type**, select **Aerial**. The aerial view does not contain text.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
##  <a name="Transparent"></a> 5. Make a Layer Transparent  
 To let the items on one layer show through another layer, you can adjust the order of layers and the transparency of each layer to get the effect that you want.  
  
#### To set the transparency of a layer  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane.  
  
3.  Click the down arrow on PolygonLayer1, and then click **Layer Data**. The **Map Polygon Layer Properties** dialog box opens.  
  
4.  Click **Visibility**.  
  
5.  In **Transparency (%)**, type **30**.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
 The design surface displays the counties as semi-transparent.  
  
##  <a name="Vary"></a> 6. Vary County Color Based on Sales  
 Each county on the polygon layer has a different color because the report processor automatically assigns a color value from the color palette based on the theme that you chose on the last page of the map wizard.  
  
 In the following steps, specify a color rule to associate specific colors with a range of store sales for each county. The colors red-yellow-green indicate relative high-middle-low sales. Format the color scale to show currency. Display the annual sales ranges in a new legend. For counties that do not contain stores, use no color to show that there is no associated data.  
  
###  <a name="Relationship"></a> 6a. Build a Relationship between Spatial and Analytical Data  
 To vary the county shapes by color based on analytical data, you first must associate the analytical data with the spatial data. In this tutorial, you will use the county name to match on.  
  
##### To build a relationship between spatial data and analytical data  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane.  
  
3.  Click the down arrow on PolygonLayer1, and then click **Layer Data**. The **Map Polygon Layer Properties** dialog box opens.  
  
4.  Click **Analytical data**.  
  
5.  From the drop-down list, select DataSet1. This dataset was created by the wizard when you specified the spatial data query for the counties.  
  
6.  In **Fields to match on**, click **Add**. A new row is added.  
  
7.  In **From spatial dataset**, from the drop-down list, click COUNTYNAME.  
  
8.  In **From analytical dataset**, from the drop-down list, click [County].  
  
9. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
10. Preview the report.  
  
 By specifying a match field from the spatial data source and from the analytical dataset, you enable the report processor to group analytical data based on the map elements. A data-bound map element has a successful match for the values that you specified.  
  
 Each county that contains a store has a color that is based on the color palette for the style that you chose in the wizard.  
  
###  <a name="ColorRules"></a> 6b. Specify Color Rules for Polygons  
 To create a rule that varies the color of each county based store sales, you must specify the range values, the number of divisions within that range that you want to display, and the colors to use.  
  
##### To specify color rules for all polygons that have associated data  
  
1.  Switch to Design view.  
  
2.  Click the down arrow on PolygonLayer1, and then click **Polygon Color Rule**. The **Map Color Rules Properties** dialog box opens. Notice that the color rule option **Visualize data by using color palette** is selected. This option was set by the wizard.  
  
3.  Select **Visualize data by using color ranges**. The palette option is replaced by start color, middle color, and end color options.  
  
4.  Define range values for sales per county. In **Data field**, from the drop-down list, select `[Sum(Sales)]`.  
  
5.  To change the format to display currency in thousands, change the expression to the following: `=Sum(Fields!Sales.Value)/1000`  
  
6.  Change **Start color** to **Red**.  
  
7.  Change **End color** to **Green**.  
  
     **Red** represents low sales values, **Yellow** represents middle sales values, and **Green** represents high sales values. The report processor calculates a range of colors based on these values and the options that you choose on the **Distribution** page.  
  
8.  Click **Distribution**.  
  
9. Verify that the distribution type is **Optimal**. For the expression from step 5, optimal distribution divides the values into subranges that balance the number of items in each range and the span for each range.  
  
10. Accept the default values for other options on this page. When you select the optimal distribution type, the number of subranges are calculated when the report runs.  
  
11. Click **Legend**.  
  
12. In **Color scale options**, verify that **Show in color scale** is selected.  
  
13. In **Show in this legend**, from the drop-down list, select the blank line. For now, you will show the color ranges only in the color scale.  
  
14. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
 The color scale displays five colors: red, orange, yellow, yellow green, and green. Each color represents a sales range that is automatically calculated based on the sales by county.  
  
###  <a name="ColorScale"></a> 6c. Format the Data in the Color Scale as Currency  
 By default, data has a general format. You can apply custom formats.  
  
##### To set the format for the color scale  
  
1.  Right-click the color scale, and then click **Color Scale Properties**.  
  
2.  Click **Number**.  
  
3.  In **Category**, click **Currency**.  
  
4.  In **Decimal places**, type **0**. This format specifies no decimal places for currency.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
6.  Preview the report.  
  
 The color scale displays annual sales in currency format for each range.  
  
###  <a name="NewLegend"></a> 6d. Create a New Legend  
 By default, all rules display in the first legend. To improve the display for a map, you can add legends.  
  
 To change the default display, there are two steps: create a new legend and then associate the rule results for a map layer with the new legend.  
  
##### To create a new legend  
  
1.  Switch to Design view.  
  
2.  Right-click the map outside the viewport, and then click **Add Legend**. A new legend is added to the map at a default location.  
  
3.  Right-click the legend, and then click **Legend Properties**.  
  
4.  In **Position options**, click the location that specifies where you want the legend to appear relative to the viewport. The map on the design surface changes to show the effect of your selections.  
  
5.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
6.  Click **Title** on the legend to select the legend title.  
  
7.  Click **Title** again to enter insert mode for the text. Replace **Title** by **Sales (Thousands)**, and then click outside the text.  
  
 The legend expands to display the title.  
  
###  <a name="Associate"></a> 6e. Associate Legend and Color Rules  
 Each legend can display one or more sets of rule results.  
  
##### To associate a legend with color rules  
  
1.  Double-click the map to display the **Map Layer** pane.  
  
2.  Click the down arrow on PolygonLayer1, and then click **Polygon Color Rule**. The **Map Color Rules Properties** dialog box opens.  
  
3.  Click **Legend**.  
  
4.  In **Color scale options**, clear **Show in color scale**.  
  
5.  In **Legend options**, from the drop-down list, select Legend2. The legend text option appears. By default, legend text is formatted with a general [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] format string. The 0 in N0 specifies no decimal digits.  
  
6.  In **Legend text**, use the following format to specify currency with no decimal digits: `#FROMVALUE {C0} - #TOVALUE {C0}`  
  
7.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
     On the design surface, the legend displays the color ranges with sample data formatted as currency.  
  
8.  Preview the report.  
  
 The counties that have associated stores and sales display according to the color rules. Counties that have no sales have no color.  
  
###  <a name="NoData"></a> 6f. Change Color for Counties with No Data  
 You can set the default display options for all map elements on a layer. Color rules take precedence over these display options.  
  
##### To set the display properties for all elements on a layer  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane.  
  
3.  Click the down arrow on PolygonLayer1, and then click **Polygon Properties**. The **Map Polygon Properties** dialog box opens. Display options set in this dialog box apply to all polygons on the layer before rule-based display options are applied.  
  
4.  Click **Fill**.  
  
5.  Verify that the fill style is **Solid.** Gradients and patterns apply to all colors.  
  
6.  In **Color**, click the down arrow, and then click **Light Steel Blue**.  
  
7.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
8.  Preview the report.  
  
 Counties that have no associated data display as blue. Only counties that have associated analytical data appear in the **Red** through **Green** colors from the color rules that you specified.  
  
##  <a name="CustomPoint"></a> 7. Add a Custom Point  
 To represent a new store that has not yet been built, specify a point and use the **PushPin** marker type.  
  
#### To add a custom point  
  
1.  Switch to Design view.  
  
2.  Double-click the map to display the **Map Layer** pane. On the toolbar, click **Add Layer**, and then click **Point Layer**.  
  
     A new point layer is added to the map. By default, the point layer has spatial data type **Embedded**.  
  
3.  Click the down arrow on PointLayer2, and then click **Add Point**.  
  
4.  Move the pointer over the map viewport. The cursor changes to crosshairs.  
  
5.  Click the location on the map where you want to add a point. In this tutorial, click a location in a county next to the start of the route. A point marked by a circle is added to the layer at the location where you clicked. By default, the point is selected.  
  
6.  Right-click the point you added, and then click **Embedded Point Properties**.  
  
7.  Select the option **Override point options for this layer**. Additional pages appear in the dialog box. Values that you set here take precedence over display options for the layer or for color rules.  
  
8.  Click **Marker**.  
  
9. For **Marker type**, select **Star**.  
  
10. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
11. Preview the report.  
  
 The new point you added appears as a **Star**.  
  
#### To add a label for the custom point  
  
1.  Switch to Design view.  
  
2.  Right-click the point you just added, and then click **Embedded Point Properties**.  
  
3.  Click **Labels**.  
  
4.  In **Label text**, type **New Store**.  
  
5.  In **Placement**, click **Top**.  
  
6.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
7.  Preview the report.  
  
 The label appears above the store location.  
  
##  <a name="CenterView"></a> Center the Map View  
 Change the map viewport center and zoom level.  
  
#### To change the viewport  
  
1.  Right-click the map viewport, and then click **Viewport Properties**.  
  
2.  Click **Center and Zoom**.  
  
3.  Verify that the option **Set a view center and zoom level** is selected.  
  
4.  [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
5.  Left-click the map viewport, and drag the center of the viewport to the location that you want.  
  
6.  Use the mouse wheel to change the zoom level of the viewport.  
  
7.  Preview the report.  
  
 In Design view, the map on the display surface and the view is based on sample data. In the rendered report, the map view is centered on the view that you specified.  
  
##  <a name="Title"></a> Add a Report Title  
  
#### To add a report title  
  
1.  On the design surface, click **Click to add title**.  
  
2.  Type **Sales in New York Stores** and then click outside the text box.  
  
 This title will appear at the top of the report. Items at the top of the report body when there is no page header defined are the equivalent of a report header.  
  
##  <a name="Save"></a> Save the Report  
  
#### To save the report  
  
1.  Switch to Design view.  
  
2.  From the Report Builder button, click **Save As**.  
  
3.  In **Name**, type **Store Sales in New York**.  
  
 Click **Save**.  
  
## Next Steps  
 This concludes the walkthrough for how to add a map to your report.  
  
 For more information, see [Maps &#40;Report Builder and SSRS&#41;](report-design/maps-report-builder-and-ssrs.md) and the blog entry [Cartographic Adjustment of Spatial Data for SQL Server Reporting Services](https://go.microsoft.com/fwlink/?LinkId=152771) on blogs.msdn.com.  
  
 For more tutorials, see [Tutorials &#40;Report Builder&#41;](report-builder-tutorials.md).  
  
## See Also  
 [Tutorials &#40;Report Builder&#41;](report-builder-tutorials.md)   
 [Report Builder in SQL Server 2014](report-builder/report-builder-in-sql-server-2016.md)   
 [Map Wizard and Map Layer Wizard &#40;Report Builder and SSRS&#41;](report-design/map-wizard-and-map-layer-wizard-report-builder-and-ssrs.md)   
 [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md)  
  
  
