---
title: "StreamOpenOptionsEnum"
description: "StreamOpenOptionsEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "StreamOpenOptionsEnum"
helpviewer_keywords:
  - "StreamOpenOptionsEnum enumeration [ADO]"
apitype: "COM"
---
# StreamOpenOptionsEnum
Specifies options for opening a [Stream](./stream-object-ado.md) object. The values can be combined with an OR operation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adOpenStreamAsync**|1|Opens the **Stream** object in asynchronous mode.|  
|**adOpenStreamFromRecord**|4|Identifies the contents of the *Source* parameter to be an already open [Record](./record-object-ado.md) object. The default behavior is to treat *Source* as a URL that points directly to a node in a tree structure. The default stream associated with that node is opened.|  
|**adOpenStreamUnspecified**|-1|Default. Specifies opening the **Stream** object with default options.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [Open Method (ADO Stream)](./open-method-ado-stream.md)