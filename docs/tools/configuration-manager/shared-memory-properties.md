---
title: "Shared Memory Properties"
description: Learn how to enable or disable the shared memory protocol, which clients can use to connect to an SQL Server instance running on the same computer.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "shared memory [SQL Server]"
ms.assetid: dc1704da-eacd-4d26-b529-c996f958ca4b
author: markingmyname
ms.author: maghan
monikerRange: ">=sql-server-2016"
---
# Shared Memory Properties
[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]
  Use the **Protocol** page on the **Shared Memory Properties** dialog box to enable or disable the shared memory protocol. Shared memory is the simplest protocol to use and has no configurable settings. Because clients using the shared memory protocol can only connect to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance running on the same computer, it is not useful for most database activity. Use the shared memory protocol for troubleshooting when you suspect the other protocols are configured incorrectly.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be restarted to enable or disable the protocol.  
  
## Options  
 **Enabled**  
 Possible values are **Yes** and **No**. The shared memory protocol is enabled by default.  
  
## See Also  
 [Choosing a Network Protocol](/previous-versions/sql/sql-server-2016/ms187892(v=sql.130))   
 [Creating a Valid Connection String Using Shared Memory Protocol](../../tools/configuration-manager/creating-a-valid-connection-string-using-shared-memory-protocol.md)  
  
