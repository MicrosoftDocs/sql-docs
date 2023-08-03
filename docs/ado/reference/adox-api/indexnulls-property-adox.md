---
title: "IndexNulls Property (ADOX)"
description: "IndexNulls Property (ADOX)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Index::get_IndexNulls"
  - "_Index::GetIndexNulls"
  - "_Index::put_IndexNulls"
  - "_Index::PutIndexNulls"
  - "_Index::IndexNulls"
helpviewer_keywords:
  - "IndexNulls property [ADOX]"
apitype: "COM"
---
# IndexNulls Property (ADOX)
Indicates whether records that have null values in their index fields have index entries.  
  
## Settings and Return Values  
 Sets and returns an [AllowNullsEnum](./allownullsenum.md) value. The default value is **adIndexNullsDisallow**.  
  
## Remarks  
 This property is read-only on [Index](./index-object-adox.md) objects already appended to a collection.  
  
## Applies To  
 [Index Object (ADOX)](./index-object-adox.md)  
  
## See Also  
 [IndexNulls Property Example (VB)](./indexnulls-property-example-vb.md)