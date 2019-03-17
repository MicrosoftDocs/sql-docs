---
title: "Rectangles and Lines (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d6226b0c-0398-4185-8565-96099876fc21
author: markingmyname
ms.author: maghan
manager: kfile
---
# Rectangles and Lines (Report Builder and SSRS)
  Rectangles and lines can create visual effects within a report. You can set display properties on these report items from the Border section of the Home tab, and set other properties by using the Properties pane. You can add features like a background color or image, a tooltip, or a bookmark to a rectangle.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="RectanglesLinesReportParts"></a> Rectangles and Lines as Report Parts  
 You can publish rectangles with the items that they contain separately from the report as report parts. [!INCLUDE[ssRBrptparts](../../includes/ssrbrptparts-md.md)]  
  
 You cannot publish the report items within the rectangle as report parts. When people add the rectangle to a report, they get the rectangle and the items it contains.  
  

  
##  <a name="RectangleAsContainer"></a> Using a Rectangle as a Container  
 You can use a rectangle as a container for other items. When you move the rectangle, the items that are contained within the rectangle move along with it. An item within the rectangle shows the name of the rectangle in its **Parent** property. For more information about using a rectangle as a container, see [Add a Rectangle or Container &#40;Report Builder and SSRS&#41;](add-a-rectangle-or-container-report-builder-and-ssrs.md) and [Display the Same Data on a Matrix and a Chart &#40;Report Builder&#41;](display-the-same-data-on-a-matrix-and-a-chart-report-builder.md).  
  
> [!NOTE]  
>  A rectangle is only a container for items that you either create in the rectangle or drag into the rectangle. If you draw a rectangle around an item that already exists on the design surface, the rectangle will not act as its container. The rectangle will not be listed in the item's Parent property.  
  
 When using rectangles to contain report items, consider how the items will be affected as a whole during report rendering. Report items that contain repeated rows of data (for example, tables) will expand to accommodate the data that is returned by a query, and this affects the positioning of other items in the rectangle. A table will push items down if they are positioned below the data region. To anchor an item in place, you can place the report item inside of a rectangle that has an upper edge above the lower edge of the table. For more information, see [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](rendering-behaviors-report-builder-and-ssrs.md).  
  

  
##  <a name="ReportBorder"></a> Adding a Report Border  
 You can add a border to a report by adding borders to the headers, footers, and report body themselves, without adding lines or rectangles. For more information, see [Add a Border to a Report &#40;Report Builder and SSRS&#41;](add-a-border-to-a-report-report-builder-and-ssrs.md).  
  

  
##  <a name="HowTo"></a> How-To Topics  
 [Add a Border to a Report &#40;Report Builder and SSRS&#41;](add-a-border-to-a-report-report-builder-and-ssrs.md)  
  
 [Add a Rectangle or Container &#40;Report Builder and SSRS&#41;](add-a-rectangle-or-container-report-builder-and-ssrs.md)  
  
 [Add and Modify a Line &#40;Report Builder and SSRS&#41;](add-and-modify-a-line-report-builder-and-ssrs.md)  
  
## See Also  
 [Add a Rectangle or Container &#40;Report Builder and SSRS&#41;](add-a-rectangle-or-container-report-builder-and-ssrs.md)  
  
  
