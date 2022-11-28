---
title: "SeekEnum"
description: "SeekEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "SeekEnum"
helpviewer_keywords:
  - "SeekEnum enumeration [ADO]"
apitype: "COM"
---
# SeekEnum
Specifies the type of [Seek](./seek-method.md) to execute.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSeekFirstEQ**|1|Seeks the first key equal to *KeyValues*.|  
|**adSeekLastEQ**|2|Seeks the last key equal to *KeyValues*.|  
|**adSeekAfterEQ**|4|Seeks either a key equal to *KeyValues* or just after where that match would have occurred.|  
|**adSeekAfter**|8|Seeks a key just after where a match with *KeyValues* would have occurred.|  
|**adSeekBeforeEQ**|16|Seeks either a key equal to *KeyValues*or just before where that match would have occurred.|  
|**adSeekBefore**|32|Seeks a key just before where a match with *KeyValues* would have occurred.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.Seek.FIRSTEQ|  
|AdoEnums.Seek.LASTEQ|  
|AdoEnums.Seek.AFTEREQ|  
|AdoEnums.Seek.AFTER|  
|AdoEnums.Seek.BEFOREEQ|  
|AdoEnums.Seek.BEFORE|  
  
## Applies To  
 [Seek Method](./seek-method.md)