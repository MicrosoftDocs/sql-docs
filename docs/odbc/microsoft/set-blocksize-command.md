---
description: "SET BLOCKSIZE Command"
title: "SET BLOCKSIZE Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "set blocksize command [ODBC]"
ms.assetid: 0c11580f-37f5-4a8e-99be-9fb9c44bb433
author: David-Engel
ms.author: v-davidengel
---
# SET BLOCKSIZE Command
Specifies how disk space is allocated for the storage of memo fields.  
  
## Syntax  
  
```  
  
SET BLOCKSIZE TO nBytes  
```  
  
## Arguments  
 *nBytes*  
 Specifies the block size in which disk space for memo fields is allocated. If *nBytes* is 0, disk space is allocated in single bytes (blocks of 1 byte). If *nBytes* is an integer between 1 and 32, disk space is allocated in blocks of *nBytes* bytes multiplied by 512. If *nBytes* is greater than 32, disk space is allocated in blocks of *nBytes* bytes. If you specify a block size value greater than 32, you can save substantial disk space.  
  
## Remarks  
 The default value for SET BLOCKSIZE is 64. To reset the block size to a different value after the file has been created, set it to a new value and then use COPY to create a new table. The new table has the specified block size.
