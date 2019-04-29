---
title: "Map Viewport Properties Dialog Box, Center and Zoom | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rtp.rptdesigner.mapviewport.centerandzoom.f1"
  - "10506"
ms.assetid: 642a06f5-293f-48e0-97a6-1489dbefa719
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Map Viewport Properties Dialog Box, Center and Zoom
  Select **Center and Zoom** for the **Map Viewport Properties** dialog box to set the center view and zoom factor for a map. After you specify a map data source and the boundaries of the map that you want to include in your report, you can specify the view center and the zoom factor to further control the map display. Options change depending on which method you use to specify the center and zoom values. Click the **Expression** (*fx*) button to edit an expression that sets the value of the option.  
  
## Options  
 **Set a view center and zoom level**  
 Choose this option to specify custom values for the view center and the zoom level.  
  
 **Center map to show a map layer**  
 Choose this option to specify a layer and automatically center the view on its map data. For example, center the view on LineLayer1.  
  
 **Center map to show an embedded map element**  
 Choose this option to center the view on a specific data-bound map element. The spatial data must have a relationship with analytical data to specify this option.  
  
 For example, center the view on the map element where the name of the match field is [City] and the match value is "Seattle".  
  
 **Center map to show all data-bound map elements**  
 Choose this option to center the view on all map elements in the layer. The spatial data must have a relationship with analytical data to specify this option.  
  
 For example, center the view on the polygon bubble layer that shows cities and the bubble size is related to population. Only those cities with a matching population value are included when calculating the center for the viewport.  
  
 **Center and zoom options**  
 Select an option to specify the view center and zoom level.  
  
 **View center (X) %**  
 The horizontal coordinate. The default value, 50%. centers the view at the midpoint between the minimum and maximum horizontal values.  
  
 **View center (Y) %**  
 The vertical coordinate. The default value, 50%, centers the view at the midpoint between the minimum and maximum vertical values.  
  
 **Zoom level (%)**  
 The zoom factor. The default value, 100%, indicates no magnification.  
  
 **Center view on this layer**  
 Specify the name of the layer.  
  
 **Center view on the map element that matches this condition**  
 The read-only field that is displayed is used to match map data and analytical data. Specify the value that you want to match on. The view automatically centers on this map element. For example, NAME = TEXAS. By default, the data type for the condition is String and the match is case-sensitive.  
  
 To match on a field that has a different data type, you must write an expression that evaluates to that data type. For example, if the match field is a 5 digit postal code that is stored as an integer, then to specify the postal code 98053, you must use the expression =98053.  
  
## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](report-design/maps-report-builder-and-ssrs.md)  
  
  
