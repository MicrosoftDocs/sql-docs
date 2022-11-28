---
description: "Multithreading"
title: "Multithreading | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC drivers [ODBC], thread-safe"
  - "thread-safe drivers [ODBC]"
  - "multithreaded applications [ODBC]"
ms.assetid: cdfebdf5-12ff-4e28-8055-41f49b77f664
author: David-Engel
ms.author: v-davidengel
---
# Multithreading
On multithread operating systems, drivers must be thread-safe. That is, it must be possible for applications to use the same handle on more than one thread. How this is achieved is driver-specific, and it is likely that drivers will serialize any attempts to concurrently use the same handle on two different threads.  
  
 Applications commonly use multiple threads instead of asynchronous processing. The application creates a separate thread, calls an ODBC function on it, and then continues processing on the main thread. Rather than having to continually poll the asynchronous function, as is the case when the SQL_ATTR_ASYNC_ENABLE statement attribute is used, the application can simply let the newly created thread finish.  
  
 Functions that accept a statement handle and are running on one thread can be canceled by calling **SQLCancel** with the same statement handle from another thread. Although drivers should not serialize the use of **SQLCancel** in this manner, there is no guarantee that calling **SQLCancel** will actually cancel the function running on the other thread.
