---
title: "CopyRecordOptionsEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "CopyRecordOptionsEnum"
helpviewer_keywords: 
  - "CopyRecordOptionsEnum enumeration [ADO]"
ms.assetid: 2fa4eec5-d50b-4fd3-8ae7-40af441ba12b
author: MightyPen
ms.author: genemi
manager: craigg
---
# CopyRecordOptionsEnum
Specifies the behavior of the [CopyRecord](../../../ado/reference/ado-api/copyrecord-method-ado.md) method.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adCopyAllowEmulation**|4|Indicates that the *Source* provider attempts to simulate the copy using download and upload operations if this method fails due to *Destination*being on a different server or is serviced by a different provider than *Source*. Note that differing provider capabilities may hamper performance or lose data.|  
|**adCopyNonRecursive**|2|Copies the current directory, but none of its subdirectories, to the destination. The copy operation is not recursive.|  
|**adCopyOverWrite**|1|Overwrites the file or directory if the *Destination* points to an existing file or directory.|  
|**adCopyUnspecified**|-1|Default. Performs the default copy operation: The operation fails if the destination file or directory already exists, and the operation copies recursively.|  
  
## ADO/WFC Equivalent  
 These constants do not have ADO/WFC equivalents.  
  
## Applies To  
 [CopyRecord Method (ADO)](../../../ado/reference/ado-api/copyrecord-method-ado.md)
