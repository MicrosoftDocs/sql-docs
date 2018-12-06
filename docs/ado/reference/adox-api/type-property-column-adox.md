---
title: "Type Property (Column) (ADOX) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Column::Type"
  - "_Column::GetType"
  - "_Column::get_Type"
  - "_Column::put_Type"
  - "_Column::PutType"
helpviewer_keywords: 
  - "Type property [ADOX]"
ms.assetid: 5c6718b6-f728-478a-8afb-5d17b0a91d1f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Type Property (Column) (ADOX)
Indicates the data type of a column.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that can be one of the [DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md) constants. The default value is **adVarWChar**.  
  
## Remarks  
 This property is read/write until the [Column](../../../ado/reference/adox-api/column-object-adox.md) object is appended to a collection or to another object, after which it is read-only.  
  
## Applies To  
 [Column Object (ADOX)](../../../ado/reference/adox-api/column-object-adox.md)  
  
## See Also  
 [ParentCatalog Property Example (VB)](../../../ado/reference/adox-api/parentcatalog-property-example-vb.md)   
 [Type Property (Key) (ADOX)](../../../ado/reference/adox-api/type-property-key-adox.md)   
 [Type Property (Table) (ADOX)](../../../ado/reference/adox-api/type-property-table-adox.md)
