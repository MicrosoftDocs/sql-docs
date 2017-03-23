---
title: "Shared Memory Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "shared memory [SQL Server]"
ms.assetid: dc1704da-eacd-4d26-b529-c996f958ca4b
caps.latest.revision: 21
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Shared Memory Properties
  Use the **Protocol**page on the **Shared Memory Properties** dialog box to enable or disable the shared memory protocol. Shared memory is the simplest protocol to use and has no configurable settings. Because clients using the shared memory protocol can only connect to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance running on the same computer, it is not useful for most database activity. Use the shared memory protocol for troubleshooting when you suspect the other protocols are configured incorrectly.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be restarted to enable or disable the protocol.  
  
## Options  
 **Enabled**  
 Possible values are **Yes** and **No**. The shared memory protocol is enabled by default.  
  
## See Also  
 [Choosing a Network Protocol](http://msdn.microsoft.com/library/6565fb7d-b076-4447-be90-e10d0dec359a)   
 [Creating a Valid Connection String Using Shared Memory Protocol](../../tools/configuration-manager/creating-a-valid-connection-string-using-shared-memory-protocol.md)  
  
  