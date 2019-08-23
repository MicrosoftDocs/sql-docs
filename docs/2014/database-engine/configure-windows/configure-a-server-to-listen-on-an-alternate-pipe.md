---
title: "Configure a Server to Listen on an Alternate Pipe (SQL Server Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "Named Pipes [SQL Server], configuring"
  - "listening [SQL Server], pipes"
  - "pipes [SQL Server], alternate"
  - "alternate pipes [SQL Server]"
ms.assetid: 914f7491-e2be-4b0d-b3aa-fe5409cdbafa
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure a Server to Listen on an Alternate Pipe (SQL Server Configuration Manager)
  This topic describes how to configure a server to listen on an alternate pipe in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using SQL Server Configuration Manager. By default, the default instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] listens on named pipe \\\\.\pipe\sql\query. Named instances of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and [!INCLUDE[ssEW](../../includes/ssew-md.md)] listen on other pipes.  
  
 There are three ways to connect to a specific named pipe with a client application:  
  
-   Run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service on the server.  
  
-   Create an alias on the client, specifying the named pipe.  
  
-   Program the client to connect using a custom connection string.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To configure the named pipe used by the SQL Server Database Engine  
  
1.  In SQL Server Configuration Manager, in the console pane, expand **SQL Server Network Configuration**, and then click expand **Protocols for** *\<instance name>*.  
  
2.  In the details pane, right-click **Named Pipes**, and then click **Properties**.  
  
3.  On the **Protocol** tab, in the **Pipe Name** box, type the pipe you want the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to listen on, and then click **OK**.  
  
4.  In the console pane, click **SQL Server Services**.  
  
5.  In the details pane, right-click **SQL Server (**\<instance name>**)** and then click **Restart**, to stop and restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is listening on an alternate pipe, there are three ways to connect to a specific named pipe with a client application:  
  
-   Run the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service on the server.  
  
-   Create an alias on the client, specifying the named pipe.  
  
-   Program the client to connect using a custom connection string.  
  
## See Also  
 [Create or Delete a Server Alias for Use by a Client &#40;SQL Server Configuration Manager&#41;](create-or-delete-a-server-alias-for-use-by-a-client.md)   
 [Server Network Configuration](server-network-configuration.md)  
  
  
