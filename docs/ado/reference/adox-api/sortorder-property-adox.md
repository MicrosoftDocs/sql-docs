---
description: "SortOrder Property (ADOX)"
title: "SortOrder Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Column::SortOrder"
  - "_Column::PutSortOrder"
  - "_Column::put_SortOrder"
  - "_Column::get_SortOrder"
  - "_Column::GetSortOrder"
helpviewer_keywords: 
  - "SortOrder property [ADOX]"
ms.assetid: 04510b19-9cb2-4895-b23b-f7790123eb04
author: rothja
ms.author: jroth
---
# SortOrder Property (ADOX)
Indicates the sort sequence for the column (index columns only).  
  
## Settings and Return Values  
 Sets and returns a **Long** value that can be one of the [SortOrderEnum](./sortorderenum.md) constants. The default value is **adSortAscending**.  
  
## Remarks  
 This property only applies to [Column](./column-object-adox.md) objects in the [Columns](./columns-collection-adox.md) collection of an [Index](./index-object-adox.md).  
  
## Applies To  
 [Column Object (ADOX)](./column-object-adox.md)  
  
## See Also  
 [SortOrder Property Example (VB)](./sortorder-property-example-vb.md)   
 [Columns Collection (ADOX)](./columns-collection-adox.md)   
 [Index Object (ADOX)](./index-object-adox.md)