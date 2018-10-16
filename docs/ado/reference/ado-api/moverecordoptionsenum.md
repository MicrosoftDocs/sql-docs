---
title: "MoveRecordOptionsEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "MoveRecordOptionsEnum"
helpviewer_keywords: 
  - "MoveRecordOptionsEnum enumeration [ADO]"
ms.assetid: f53c2ce4-1021-4a45-92b8-775e8bebad99
author: MightyPen
ms.author: genemi
manager: craigg
---
# MoveRecordOptionsEnum
Specifies the behavior of the [Record](../../../ado/reference/ado-api/record-object-ado.md) object [MoveRecord](../../../ado/reference/ado-api/moverecord-method-ado.md) method.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adMoveUnspecified**|-1|Default. Performs the default move operation: The operation fails if the destination file or directory already exists, and the operation updates hypertext links.|  
|**adMoveOverWrite**|1|Overwrites the destination file or directory, even if it already exists.|  
|**adMoveDontUpdateLinks**|2|Modifies the default behavior of **MoveRecord** method by not updating the hypertext links of the source **Record**. The default behavior depends on the capabilities of the provider. Move operation updates links if the provider is capable. If the provider cannot fix links or if this value is not specified, then the move succeeds even when links have not been fixed.|  
|**adMoveAllowEmulation**|4|Requests that the provider attempt to simulate the move (using download, upload, and delete operations). If the attempt to move the **Record** fails because the destination URL is on a different server or serviced by a different provider than the source, this may cause increased latency or data loss, due to different provider capabilities when moving resources between providers.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [MoveRecord Method (ADO)](../../../ado/reference/ado-api/moverecord-method-ado.md)
