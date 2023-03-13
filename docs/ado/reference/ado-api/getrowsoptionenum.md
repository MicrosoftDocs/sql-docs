---
title: "GetRowsOptionEnum"
description: "GetRowsOptionEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "GetRowsOptionEnum"
helpviewer_keywords:
  - "GetRowsOptionEnum enumeration [ADO]"
apitype: "COM"
---
# GetRowsOptionEnum
Specifies how many records to retrieve from a [Recordset](./recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adGetRowsRest**|-1|Retrieves the rest of the records in the **Recordset**, from either the current position or a bookmark specified by the *Start* parameter of the [GetRows](./getrows-method-ado.md) method.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.GetRowsOption.REST|  
  
## Applies To  
 [GetRows Method (ADO)](./getrows-method-ado.md)