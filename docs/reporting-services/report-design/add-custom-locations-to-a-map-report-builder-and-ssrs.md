---
title: "Add custom locations to a map in a paginated report | Microsoft Docs"
description:  Learn how to add custom locations to a map you have added to a paginated report in Report Builder. 
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
f1_keywords: 
  - "MICROSOFT.REPORTDESIGNER.MAPPOINT.POINTTEMPLATE"
ms.assetid: 7d36faae-5bcc-446a-9eba-f42349cafacb
author: maggiesMSFT
ms.author: maggies
---
# Add custom locations to a map in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  After you add a map to a paginated report, you can add your own point locations.  
  
 Display properties for all points on a layer are controlled by setting options for the point properties for the layer. For a selected embedded point, you can override the display properties.  
  
> [!NOTE]  
>  When you override the layer display properties for the embedded point, the changes that you make are not reversible.  
  
 For more information, see [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To add a point layer  
  
1.  On the report design surface, click the map to select it and display the Map pane.  
  
2.  On the toolbar, click **Add Layer**.  
  
3.  From the drop-down list, click **Add Point Layer**. A point layer with no points is added to the map. By default, the point layer is ready for embedded points.  
  
## To add a custom point  
  
1.  On the report design surface, click the map to select it and display the Map pane.  
  
2.  In the Map pane, right-click a point layer that has type **Embedded**, and then click **Add Point**. The cursor changes to crosshairs.  
  
3.  To add a point, click a location on the map. An embedded point is added to the selected layer at the location where you click.  
  
## To customize the display for an embedded point  
  
1.  Right-click the point, and then click **Point Properties**. The **Map Embedded Point Properties** dialog box opens.  
  
2.  Click **Override point options for this layer**. Multiple property pages appear in the left pane.  
  
3.  Click the pages and set the display properties that you want to apply to this point.  
  
## See Also  
 [Maps &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/maps-report-builder-and-ssrs.md)   
 [Vary Polygon, Line, and Point Display by Rules and Analytical Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/vary-polygon-line-and-point-display-by-rules-and-analytical-data.md)  
  
  
