---
title: "Map Viewport Properties Dialog Box, General | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.mapviewport.general.f1"
  - "10505"
ms.assetid: 6c9c773e-5c56-4571-95ed-8a157cfdfe52
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Map Viewport Properties Dialog Box, General
  Select **General** on the **Map Viewport Properties** dialog box to change the coordinate system, the projection, and the boundary options.  
  
## Options  
 **Coordinate system**  
 Indicate the type of coordinate system that the map data uses.  
  
-   **Planar** Choose this option when map data is in X and Y coordinates, for example, for building plans.  
  
-   **Geographic** Choose this option when map data is in longitude and latitude coordinates, for example, for city locations.  
  
 **Projection**  
 Specify the method to use to project geographic coordinates onto a two-dimensional surface. Choose a projection that is compatible with the data that you are visualizing. The four spatial properties that are affected by projection are area, shape, distance, and direction. For views of the earth, a good choice of projection depends on the center view, the map boundaries, and the zoom factor.  
  
 Each of the following projections preserves one or more of these spatial properties:  
  
-   **Equirectangular** Choose this option to use longitude and latitude as rectangular coordinates.  
  
-   **Mercator** Choose this popular projection for smaller areas, for less distortion around the equator, or when you want to add a map layer with an existing tile layer that uses the Mercator projection.  
  
-   **Robinson** Choose this option for less distortion of large areas that span from the equator to the poles. Developed by Arthur H. Robinson in 1963.  
  
-   **Fahey** Choose this option for less distortion of large areas that span from the equator to the poles. Developed by Lawrence Fahey in 1975.  
  
-   **Eckert1** Choose this option to use equally spaced parallels in latitude and straight lines for meridians in longitude.  
  
-   **Eckert3** Choose this option to use equally spaced parallels in latitude and curved lines for meridians in longitude.  
  
-   **HammerAitoff** Choose this option for polar maps or world maps.  
  
-   **Wagner3** Choose this option for world maps.  
  
-   **Bonne** Choose this option to view the world as it appears in an atlas.  
  
 **Page break options**  
 Select options to indicate how content is fitted to a report page.  
  
 **Boundary options**  
 Specify the lower and upper boundaries for coordinates to control which part of the map appears in the report.  
  
 **Minimum X**  
 Lowest X value. Available for **Planar** only.  
  
 **Maximum X**  
 Highest X value. Available for **Planar** only.  
  
 **Minimum Y**  
 Lowest Y value. Available for **Planar** only.  
  
 **Maximum Y**  
 Highest Y value. Available for **Planar** only.  
  
 **Minimum Longitude**  
 Lowest longitude value. Available for **Geographic** only.  
  
 **Maximum Longitude**  
 Highest longitude value. Available for **Geographic** only.  
  
 **Minimum Latitude**  
 Lowest latitude value. Available for **Geographic** only.  
  
 **Maximum Latitude**  
 Highest latitude value. Available for **Geographic** only.  
  
## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](report-design/maps-report-builder-and-ssrs.md)  
  
  
