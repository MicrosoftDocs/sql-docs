---
title: "CopyRecordOptionsEnum"
description: "CopyRecordOptionsEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "CopyRecordOptionsEnum"
helpviewer_keywords:
  - "CopyRecordOptionsEnum enumeration [ADO]"
apitype: "COM"
---
# CopyRecordOptionsEnum
Specifies the behavior of the [CopyRecord](./copyrecord-method-ado.md) method.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCopyAllowEmulation**|4|Indicates that the *Source* provider attempts to simulate the copy using download and upload operations if this method fails due to *Destination*being on a different server or is serviced by a different provider than *Source*. Note that differing provider capabilities may hamper performance or lose data.|  
|**adCopyNonRecursive**|2|Copies the current directory, but none of its subdirectories, to the destination. The copy operation is not recursive.|  
|**adCopyOverWrite**|1|Overwrites the file or directory if the *Destination* points to an existing file or directory.|  
|**adCopyUnspecified**|-1|Default. Performs the default copy operation: The operation fails if the destination file or directory already exists, and the operation copies recursively.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [CopyRecord Method (ADO)](./copyrecord-method-ado.md)