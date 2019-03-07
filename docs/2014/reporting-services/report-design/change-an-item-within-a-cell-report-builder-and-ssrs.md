---
title: "Change an Item Within a Cell (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 91a54778-8929-41f9-bb4c-826cec636be4
author: markingmyname
ms.author: maghan
manager: kfile
---
# Change an Item Within a Cell (Report Builder and SSRS)
  Only a non-container item, such as a text box, line, or image, can be replaced by a new report item. For example, you can drag a table into a text box to replace the text box with a table.  
  
 If the cell contains a container item such as a rectangle, list, table, or matrix, the new item is added to the containing item instead of replacing it. To replace a container item with a new item, delete the container. Deleting the container item replaces it with a text box, which you can then replace with another item.  
  
 By default, all cells in a table, matrix, or list data region contain a text box.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To change an item within a cell  
  
-   On the **Insert** tab, in the **Data Regions** or **Report Items** group, click the item that you want to add to the report, and then click the report. The item is added to the report.  
  
> [!NOTE]  
>  The **Image Properties** dialog box opens when you drag an image report item to a cell, where you can set properties such as the source of the image before the image is added to the cell.  
  
## See Also  
 [Images, Text Boxes, Rectangles, and Lines &#40;Report Builder and SSRS&#41;](rectangles-and-lines-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
