---
title: "Run SQL Server With or Without a Network | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "verifying Server service has been started"
  - "net start [SQL Server]"
  - "command prompt [SQL Server], connections"
  - "SQL Server services, networks"
  - "status information [SQL Server], Server service"
  - "running SQL Server"
  - "networking [SQL Server], SQL Server with or without"
  - "services [SQL Server], networks"
  - "starting Server service"
  - "SQL Server, running"
ms.assetid: 54eac961-5c7a-4481-982d-f93a64b5c2f4
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Run SQL Server With or Without a Network
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can run on a network, or it can function without a network.  
  
## Running SQL Server on a Network  
 For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to communicate over a network, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service must be running. By default, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows automatically starts the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. To find out whether the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has been started, at the command prompt, type the following:  
  
 **net start**  
  
 If the services associated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] have been started, the following services will appear in the **net start** output:  
  
-   Analysis Services (MSSQLSERVER)  
  
-   SQL Server (MSSQLSERVER)  
  
-   SQL Server Agent (MSSQLSERVER)  
  
## Running SQL Server Without a Network  
 When running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without a network, you do not need to start the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. Because [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], SQL Server Configuration Manager, and the **net start** and **net stop** commands are functional even without a network, the procedures for starting and stopping an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are identical for a network or stand-alone operation.  
  
 When connecting to an instance of a stand-alone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a local client such as **sqlcmd**, you bypass the network and connect directly to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a local pipe. The difference between a local pipe and a network pipe is whether you are using a network. Both local and network pipes establish a connection with an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the standard pipe (\\\\.\pipe\sql\query), unless otherwise directed.  
  
 When you connect to an instance of a local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without specifying a server name, you are using a local pipe. When you connect to an instance of a local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and specify a server name explicitly, you are using either a network pipe or another network interprocess communication (IPC) mechanism, such as Internetwork Packet Exchange/Sequenced Packet Exchange (IPX/SPX) (assuming you have configured [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use multiple networks). Because a stand-alone [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support network pipes, you must omit the unnecessary **/**_<Server_name>_ argument when connecting to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a client. For example, to connect to a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from **osql**, type:  
  
 **osql /Usa /P** _\<saPassword>_  
  
  
