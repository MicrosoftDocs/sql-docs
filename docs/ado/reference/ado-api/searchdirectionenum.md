---
title: "SearchDirectionEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "SearchDirectionEnum"
helpviewer_keywords: 
  - "SearchDirectionEnum enumeration [ADO]"
ms.assetid: 81272ae3-2165-4f4e-adfe-9ede0368cb17
author: MightyPen
ms.author: genemi
manager: craigg
---
# SearchDirectionEnum
Specifies the direction of a record search within a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSearchBackward**|-1|Searches backward, stopping at the beginning of the **Recordset**. If a match is not found, the record pointer is positioned at [BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md).|  
|**adSearchForward**|1|Searches forward, stopping at the end of the **Recordset**. If a match is not found, the record pointer is positioned at [EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md).|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.SearchDirection.BACKWARD|  
|AdoEnums.SearchDirection.FORWARD|  
  
## Applies To  
 [Find Method (ADO)](../../../ado/reference/ado-api/find-method-ado.md)
