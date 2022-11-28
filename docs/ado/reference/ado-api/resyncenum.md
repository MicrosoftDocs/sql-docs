---
title: "ResyncEnum"
description: "ResyncEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ResyncEnum"
helpviewer_keywords:
  - "ResyncEnum enumeration [ADO]"
apitype: "COM"
---
# ResyncEnum
Specifies whether underlying values are overwritten by a call to [Resync](./resync-method.md).  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adResyncAllValues**|2|Default. Overwrites data, and pending updates are canceled.|  
|**adResyncUnderlyingValues**|1|Does not overwrite data, and pending updates are not canceled.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Resync.ALLVALUES|  
|AdoEnums.Resync.UNDERLYINGVALUES|  
  
## Applies To  
 [Resync Method](./resync-method.md)