---
title: "StreamReadEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "StreamReadEnum"
helpviewer_keywords: 
  - "StreamReadEnum enumeration [ADO]"
ms.assetid: cfa1b416-003a-436f-a21b-bd2397e54db3
author: MightyPen
ms.author: genemi
manager: craigg
---
# StreamReadEnum
Specifies whether the whole stream or the next line should be read from a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adReadAll**|-1|Default. Reads all bytes from the stream, from the current position onwards to the [EOS](../../../ado/reference/ado-api/eos-property.md) marker. This is the only valid **StreamReadEnum** value with binary streams ([Type](../../../ado/reference/ado-api/type-property-ado-stream.md) is **adTypeBinary**).|  
|**adReadLine**|-2|Reads the next line from the stream (designated by the [LineSeparator](../../../ado/reference/ado-api/lineseparator-property-ado.md) property).|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
  
|||  
|-|-|  
|[Read Method](../../../ado/reference/ado-api/read-method.md)|[ReadText Method](../../../ado/reference/ado-api/readtext-method.md)|
