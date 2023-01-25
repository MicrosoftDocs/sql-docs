---
title: "StringFormatEnum"
description: "StringFormatEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "StringFormatEnum"
helpviewer_keywords:
  - "StringFormatEnum enumeration [ADO]"
apitype: "COM"
---
# StringFormatEnum
Specifies the format when retrieving a [Recordset](./recordset-object-ado.md) as a string.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adClipString**|2|Delimits rows by *RowDelimiter*, columns by *ColumnDelimiter*, and null values by *NullExpr*. These three parameters of the [GetString](./getstring-method-ado.md) method are valid only with a *StringFormat* of **adClipString**.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.StringFormat.CLIPSTRING|  
  
## Applies To  
 [GetString Method (ADO)](./getstring-method-ado.md)