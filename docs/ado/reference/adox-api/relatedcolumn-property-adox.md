---
description: "RelatedColumn Property (ADOX)"
title: "RelatedColumn Property (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Column::GetRelatedColumn"
  - "_Column::PutRelatedColumn"
  - "_Column::RelatedColumn"
  - "_Column::put_RelatedColumn"
  - "_Column::get_RelatedColumn"
helpviewer_keywords: 
  - "RelatedColumn property [ADOX]"
ms.assetid: 2f2ca019-c785-4c08-beb1-3a2d3b47823e
author: rothja
ms.author: jroth
---
# RelatedColumn Property (ADOX)
Indicates the name of the related [Column Object (ADOX)](./column-object-adox.md) in the related table (key columns only).  
  
## Settings and Return Values  
 Sets and returns a **String** value that is the name of the related column in the related table.  
  
## Remarks  
 The default value is an empty string ("").  
  
 This property is read-only for [Column](./column-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Column Object (ADOX)](./column-object-adox.md)  
  
## See Also  
 [Keys Append Method, Key Type, RelatedColumn, RelatedTable and UpdateRule Properties Example (VB)](./keys-append-method-key-type-relatedcolumn-relatedtable-example-vb.md)   
 [Key Object (ADOX)](./key-object-adox.md)