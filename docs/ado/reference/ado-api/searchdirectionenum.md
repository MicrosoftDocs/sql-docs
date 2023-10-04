---
title: "SearchDirectionEnum"
description: "SearchDirectionEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "SearchDirectionEnum"
helpviewer_keywords:
  - "SearchDirectionEnum enumeration [ADO]"
apitype: "COM"
---
# SearchDirectionEnum
Specifies the direction of a record search within a [Recordset](./recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSearchBackward**|-1|Searches backward, stopping at the beginning of the **Recordset**. If a match is not found, the record pointer is positioned at [BOF](./bof-eof-properties-ado.md).|  
|**adSearchForward**|1|Searches forward, stopping at the end of the **Recordset**. If a match is not found, the record pointer is positioned at [EOF](./bof-eof-properties-ado.md).|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.SearchDirection.BACKWARD|  
|AdoEnums.SearchDirection.FORWARD|  
  
## Applies To  
 [Find Method (ADO)](./find-method-ado.md)