---
title: "LoadFromFile Method (ADO)"
description: "LoadFromFile Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "_Stream::raw_LoadFromFile"
helpviewer_keywords:
  - "LoadFromFile method [ADO]"
apitype: "COM"
---
# LoadFromFile Method (ADO)
Loads the contents of an existing file into a [Stream](./stream-object-ado.md).  
  
## Syntax  
  
```  
  
Stream.LoadFromFileFileName  
```  
  
#### Parameters  
 *FileName*  
 A **String** value that contains the name of a file to be loaded into the **Stream**. *FileName* can contain any valid path and name in UNC format. If the specified file does not exist, a run-time error occurs.  
  
## Remarks  
 This method can be used to load the contents of a local file into a **Stream** object. This can be used to upload the contents of a local file to a server.  
  
 The **Stream** object must be already open before calling **LoadFromFile**. This method does not change the binding of the **Stream** object; it will still be bound to the object specified by the URL or **Record** with which the **Stream** was originally opened.  
  
 **LoadFromFile** overwrites the current contents of the **Stream** object with data read from the file. Any existing bytes in the **Stream** are overwritten by the contents of the file. Any previously existing and remaining bytes following the [EOS](./eos-property.md) created by **LoadFromFile**, are truncated.  
  
 After a call to **LoadFromFile**, the current position is set to the beginning of the **Stream** ([Position](./position-property-ado.md) is 0).  
  
 Because 2 bytes may be added to the beginning of the stream for encoding, the size of the stream may not exactly match the size of the file from which it was loaded.  
  
## Applies To  
 [Stream Object (ADO)](./stream-object-ado.md)