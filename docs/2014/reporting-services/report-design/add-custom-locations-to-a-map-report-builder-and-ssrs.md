---
title: "Add Custom Locations to a Map (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "MICROSOFT.REPORTDESIGNER.MAPPOINT.POINTTEMPLATE"
ms.assetid: 7d36faae-5bcc-446a-9eba-f42349cafacb
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add Custom Locations to a Map (Report Builder and SSRS)
  After you add a map to a report, you can add your own point locations.  
  
 Display properties for all points on a layer are controlled by setting options for the point properties for the layer. For a selected embedded point, you can override the display properties.  
  
> [!NOTE]  
>  When you override the layer display properties for the embedded point, the changes that you make are not reversible.  
  
 For more information, see [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a point layer  
  
1.  On the report design surface, click the map to select it and display the Map pane.  
  
2.  On the toolbar, click **Add Layer**.  
  
3.  From the drop-down list, click **Add Point Layer**. A point layer with no points is added to the map. By default, the point layer is ready for embedded points.  
  
### To add a custom point  
  
1.  On the report design surface, click the map to select it and display the Map pane.  
  
2.  In the Map pane, right-click a point layer that has type **Embedded**, and then click **Add Point**. The cursor changes to crosshairs.  
  
3.  To add a point, click a location on the map. An embedded point is added to the selected layer at the location where you click.  
  
### To customize the display for an embedded point  
  
1.  Right-click the point, and then click **Point Properties**. The **Map Embedded Point Properties** dialog box opens.  
  
2.  Click **Override point options for this layer**. Multiple property pages appear in the left pane.  
  
3.  Click the pages and set the display properties that you want to apply to this point.  
  
## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](maps-report-builder-and-ssrs.md)   
 [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](vary-polygon-line-and-point-display-by-rules-and-analytical-data.md)  
  
  
