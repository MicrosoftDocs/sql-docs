---
title: "SortOrder Property (ADOX)"
description: "SortOrder Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Column::SortOrder"
  - "_Column::PutSortOrder"
  - "_Column::put_SortOrder"
  - "_Column::get_SortOrder"
  - "_Column::GetSortOrder"
helpviewer_keywords:
  - "SortOrder property [ADOX]"
apitype: "COM"
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