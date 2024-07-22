---
title: "Change an item within a cell in a paginated report"
description: Replace a noncontainer item, such as a text box, line, or image, in paginated reports with a new report item in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Change an item within a cell in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In paginated reports, you can replace only a noncontainer item, such as a text box, line, or image, with a new report item. For example, you can drag a table into a text box to replace the text box with a table.  
  
 If the cell contains a container item such as a rectangle, list, table, or matrix, the new item is added to the containing item instead of replacing it. To replace a container item with a new item, delete the container. Deleting the container item replaces it with a text box, which you can then replace with another item.  
  
 By default, all cells in a table, matrix, or list data region contain a text box.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Change an item within a cell  
  
-   On the **Insert** tab, in the **Data Regions** or **Report Items** group, select the item that you want to add to the report, and then choose the report. The item is added to the report.  
  
> [!NOTE]  
>  The **Image Properties** dialog opens when you drag an image report item to a cell, where you can set properties such as the source of the image before the image is added to the cell.  
  
## Related content 
 [Images, text boxes, rectangles, and lines &#40;Report Builder&#41;](../../reporting-services/report-design/images-text-boxes-rectangles-and-lines-report-builder-and-ssrs.md)   
 [Tables, matrices, and lists &#40;Report Builder&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
