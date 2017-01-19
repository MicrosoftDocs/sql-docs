---
title: "Standard Database Access Architectures | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a9d41800-9068-4b76-895a-32b2853692dd
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Standard Database Access Architectures
In looking at the database access components described in the preceding section, it turns out that two of them — programming interfaces and data stream protocols — are good candidates for standardization. The other two components — IPC mechanism and network protocols — not only reside at too low a level but they are both highly dependent on the network and operating system. There is also a third approach — gateways — that provides possibilities for standardization.  
  
 This section contains the following topics.  
  
-   [Standard Programming Interface](../../odbc/reference/standard-programming-interface.md)  
  
-   [Standard Data Stream Protocol](../../odbc/reference/standard-data-stream-protocol.md)  
  
-   [Standard Gateway](../../odbc/reference/standard-gateway.md)