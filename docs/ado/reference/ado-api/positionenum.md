---
title: "PositionEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "PositionEnum"
helpviewer_keywords: 
  - "PositionEnum enumeration"
ms.assetid: e69af0a5-3405-4b72-9c6e-6b188ff746fd
author: MightyPen
ms.author: genemi
manager: craigg
---
# PositionEnum
Specifies the current position of the record pointer within a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adPosBOF**|-2|Indicates that the current record pointer is at BOF (that is, the [BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) property is **True**).|  
|**adPosEOF**|-3|Indicates that the current record pointer is at EOF (that is, the [EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) property is **True**).|  
|**adPosUnknown**|-1|Indicates that the **Recordset** is empty, the current position is unknown, or the provider does not support the [AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md) or [AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md) property.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Position.BOF|  
|AdoEnums.Position.EOF|  
|AdoEnums.Position.UNKNOWN|  
  
## Applies To  
  
|||  
|-|-|  
|[AbsolutePage Property (ADO)](../../../ado/reference/ado-api/absolutepage-property-ado.md)|[AbsolutePosition Property (ADO)](../../../ado/reference/ado-api/absoluteposition-property-ado.md)|
