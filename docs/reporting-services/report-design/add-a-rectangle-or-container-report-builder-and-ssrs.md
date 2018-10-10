---
title: "Add a Rectangle or Container (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
f1_keywords: 
  - "10061"
  - "sql13.rtp.rptdesigner.rectangleproperties.general.f1"
ms.assetid: f905c35f-754d-4d02-80f3-85e29ddda826
author: maggiesMSFT
ms.author: maggies
---
# Add a Rectangle or Container (Report Builder and SSRS)
  Add a rectangle to a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report when you want a graphical element to separate areas of the report, emphasize areas of a report, or provide a background for one or more report items. Rectangles are also used as containers to help control the way data regions render in a report. You can customize the appearance of a rectangle by editing rectangle properties such as the background and border colors. For more information about using a rectangle as a container, see [Rectangles and Lines &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md) and [Display the Same Data on a Matrix and a Chart &#40;Report Builder&#41;](../../reporting-services/report-design/display-the-same-data-on-a-matrix-and-a-chart-report-builder.md).    
   
## To add a rectangle    
    
1.  On the **Insert** tab, in the **Report Items** group, click **Rectangle.**    
    
2.  On the design surface, click the location where you want the upper left corner of the rectangle, and drag to where you want the lower-right corner.    
    
     Note that as you move the cursor, "snap lines" appear as the cursor lines up with other objects on the design surface. These help you if you want objects to be aligned.    
    
## To create a container    
    
1.  Add a rectangle report item to the report.    
    
2.  Drag other report items into the rectangle.    
    
    > [!NOTE]    
    >  A rectangle is only a container for items that you either create in the rectangle or drag into the rectangle. If you draw a rectangle around an item that already exists on the design surface, the rectangle will not act as its container.    
    
## To change rectangle properties such as color, style, or weight    
    
1.  Select the rectangle, and then click the line color, style, or weight options in the **Border** section of the Home tab.    
    
2.  Click the arrow next to the **Border** button to determine which sides of the rectangle to change.    
    
    > [!NOTE]    
    >  If you set the line style to **Double** and the line width is 1 1/2 pt or narrower, the line may not appear double when you run the report in Report Builder, Report Designer, or in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal. It appears double when you export the report to other formats such as Microsoft Word and Acrobat PDF.    
    
## See Also    
 [Rectangles and Lines &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md)     
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)    
    
  
