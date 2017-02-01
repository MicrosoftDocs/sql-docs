---
title: "StringFormatEnum | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "StringFormatEnum"
helpviewer_keywords: 
  - "StringFormatEnum enumeration [ADO]"
ms.assetid: 28f7d1ec-092b-4323-a39d-d3f882c6c81a
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# StringFormatEnum
Specifies the format when retrieving a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) as a string.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adClipString**|2|Delimits rows by *RowDelimiter*, columns by *ColumnDelimiter*, and null values by *NullExpr*. These three parameters of the [GetString](../../../ado/reference/ado-api/getstring-method-ado.md) method are valid only with a *StringFormat* of **adClipString**.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.StringFormat.CLIPSTRING|  
  
## Applies To  
 [GetString Method (ADO)](../../../ado/reference/ado-api/getstring-method-ado.md)