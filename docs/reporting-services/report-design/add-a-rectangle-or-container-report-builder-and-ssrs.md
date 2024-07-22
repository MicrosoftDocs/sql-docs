---
title: "Add a rectangle or container to a paginated report"
description: Separate or emphasize areas of a report or provide a background for one or more report items using a customized rectangle in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
f1_keywords:
  - "10061"
  - "sql13.rtp.rptdesigner.rectangleproperties.general.f1"
---
# Add a rectangle or container to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can add a rectangle to a paginated report when you want a graphical element to separate areas of the report. You can also add a rectangle to emphasize areas of a report or provide a background for one or more report items. Rectangles are also used as containers to help control the way data regions render in a report. You can customize the appearance of a rectangle by editing rectangle properties such as the background and border colors. For more information about using a rectangle as a container, see [Rectangles and lines &#40;Report Builder&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md) and [Display the same data on a matrix and a chart &#40;Report Builder&#41;](../../reporting-services/report-design/display-the-same-data-on-a-matrix-and-a-chart-report-builder.md).    
   
## Add a rectangle    
    
1.  On the **Insert** tab, in the **Report Items** group, select **Rectangle.**    
    
1.  On the design surface, choose the location where you want the upper left corner of the rectangle, and drag to where you want the lower-right corner.    
    
     As you move the cursor, "snap lines" appear as the cursor lines up with other objects on the design surface. These lines help you if you want objects to be aligned.    
    
## Create a container    
    
1.  Add a rectangle report item to the report.    
    
1.  Drag other report items into the rectangle.    
    
    > [!NOTE]    
    >  A rectangle is only a container for items that you either create in the rectangle or drag into the rectangle. If you draw a rectangle around an item that already exists on the design surface, the rectangle doesn't act as its container.    
    
## Change rectangle properties such as color, style, or weight    
    
1.  Select the rectangle, and then choose the line color, style, or weight options in the **Border** section of the **Home** tab.    
    
1.  Select the arrow next to the **Border** button to determine which sides of the rectangle to change.    
    
    > [!NOTE]    
    >  If you set the line style to **Double** and the line width is 1 1/2 pt or narrower, the line might not appear double when you run the report in Report Builder, Report Designer, or in the [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] web portal. It appears double when you export the report to other formats such as Microsoft Word and Acrobat PDF.    
    
## Related content  
 [Rectangles and lines &#40;Report Builder&#41;](../../reporting-services/report-design/rectangles-and-lines-report-builder-and-ssrs.md)     
 [Renderer behaviors &#40;Report Builder&#41;](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)    
    
  
