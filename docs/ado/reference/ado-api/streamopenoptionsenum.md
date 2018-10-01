---
title: "StreamOpenOptionsEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "StreamOpenOptionsEnum"
helpviewer_keywords: 
  - "StreamOpenOptionsEnum enumeration [ADO]"
ms.assetid: 85b6c57f-47ed-46ba-bd92-07882ae9e9d2
author: MightyPen
ms.author: genemi
manager: craigg
---
# StreamOpenOptionsEnum
Specifies options for opening a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object. The values can be combined with an OR operation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adOpenStreamAsync**|1|Opens the **Stream** object in asynchronous mode.|  
|**adOpenStreamFromRecord**|4|Identifies the contents of the *Source* parameter to be an already open [Record](../../../ado/reference/ado-api/record-object-ado.md) object. The default behavior is to treat *Source* as a URL that points directly to a node in a tree structure. The default stream associated with that node is opened.|  
|**adOpenStreamUnspecified**|-1|Default. Specifies opening the **Stream** object with default options.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [Open Method (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)
