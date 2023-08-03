---
title: "AllowNullsEnum"
description: "AllowNullsEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "AllowNullsEnum"
helpviewer_keywords:
  - "AllowNullsEnum enumeration [ADOX]"
apitype: "COM"
---
# AllowNullsEnum
Specifies whether records with null values are indexed.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adIndexNullsAllow**|0|The index does allow entries in which the key columns are null. If a null value is entered in a key column, the entry is inserted into the index.|  
|**adIndexNullsDisallow**|1|Default. The index does not allow entries in which the key columns are null. If a null value is entered in a key column, an error will occur.|  
|**adIndexNullsIgnore**|2|The index does not insert entries containing null keys. If a null value is entered in a key column, the entry is ignored and no error occurs.|  
|**adIndexNullsIgnoreAny**|4|The index does not insert entries where some key column has a null value. For an index having a multi-column key, if a null value is entered in some column, the entry is ignored and no error occurs.|  
  
## Applies To  
 [IndexNulls Property (ADOX)](./indexnulls-property-adox.md)