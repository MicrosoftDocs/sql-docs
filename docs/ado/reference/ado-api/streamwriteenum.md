---
title: "StreamWriteEnum"
description: "StreamWriteEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "StreamWriteEnum"
helpviewer_keywords:
  - "StreamWriteEnum enumeration [ADO]"
apitype: "COM"
---
# StreamWriteEnum
Specifies whether a line separator is appended to the string written to a [Stream](./stream-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adWriteChar**|0|Default. Writes the specified text string (specified by the *Data* parameter) to the **Stream** object.|  
|**adWriteLine**|1|Writes a text string and a line separator character to a **Stream** object. If the [LineSeparator](./lineseparator-property-ado.md) property is not defined, then this returns a run-time error.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [WriteText Method](./writetext-method.md)