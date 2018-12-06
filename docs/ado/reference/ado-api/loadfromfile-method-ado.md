---
title: "LoadFromFile Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Stream::raw_LoadFromFile"
helpviewer_keywords: 
  - "LoadFromFile method [ADO]"
ms.assetid: b18d8d38-7354-4a94-b637-6ac035faa433
author: MightyPen
ms.author: genemi
manager: craigg
---
# LoadFromFile Method (ADO)
Loads the contents of an existing file into a [Stream](../../../ado/reference/ado-api/stream-object-ado.md).  
  
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
  
 **LoadFromFile** overwrites the current contents of the **Stream** object with data read from the file. Any existing bytes in the **Stream** are overwritten by the contents of the file. Any previously existing and remaining bytes following the [EOS](../../../ado/reference/ado-api/eos-property.md) created by **LoadFromFile**, are truncated.  
  
 After a call to **LoadFromFile**, the current position is set to the beginning of the **Stream** ([Position](../../../ado/reference/ado-api/position-property-ado.md) is 0).  
  
 Because 2 bytes may be added to the beginning of the stream for encoding, the size of the stream may not exactly match the size of the file from which it was loaded.  
  
## Applies To  
 [Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)
