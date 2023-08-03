---
title: "SaveOptionsEnum"
description: "SaveOptionsEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "SaveOptionsEnum"
helpviewer_keywords:
  - "SaveOptionsEnum enumeration [ADO]"
apitype: "COM"
---
# SaveOptionsEnum
Specifies whether a file should be created or overwritten when saving from a [Stream](./stream-object-ado.md) object. The values can be **adSaveCreateNotExist** or **adSaveCreateOverWrite**..  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adSaveCreateNotExist**|1|Default. Creates a new file if the file specified by the *FileName* parameter does not already exist.|  
|**adSaveCreateOverWrite**|2|Overwrites the file with the data from the currently open **Stream** object, if the file specified by the *Filename* parameter already exists. If the file specified by the *Filename* parameter does not exist, a new file is created.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [SaveToFile Method](./savetofile-method.md)