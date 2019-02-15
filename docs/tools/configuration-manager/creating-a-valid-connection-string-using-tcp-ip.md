---
title: "Creating a Valid Connection String Using TCP IP | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "connection strings [Database Engine]"
  - "ports [SQL Server], connecting to"
  - "TCP/IP [SQL Server], connection strings"
  - "connection strings [Database Engine], TCP/IP"
  - "aliases [SQL Server], TCP/IP"
ms.assetid: ee5dbc2c-1fc6-42bd-bdf5-efa792557934
author: "stevestein"
ms.author: "sstein"
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Creating a Valid Connection String Using TCP IP
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  To create a valid connection string using TCP/IP, you must:  
  
-   Specify an **Alias Name**.  
  
-   For the **Server**, enter either a server name to which you can connect using the **PING** utility, or an IP address to which you can connect using the **PING** utility. For a named instance append the instance name.  
  
-   Specify **TCP/IP** for the **Protocol**.  
  
-   Optionally, enter a port number for the **Port No**. The default is 1433, which is the port number of the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on a server. To connect to a named instance or a default instance that is not listening on port 1433, you must provide the port number, or start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service. For information on configuring the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service, see [SQL Server Browser Service](../../tools/configuration-manager/sql-server-browser-service.md).  
  
 At the time of connection, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client component reads the server, protocol, and port values from the registry for the specified alias name, and creates a connection string in the format `tcp:<servername>[\<instancename>],<port>` or `tcp:<IPAddress>[\<instancename>],<port>`.  
  
> [!NOTE]
>  The [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Firewall closes port 1433 by default. Because [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] communicates over port 1433, you must reopen the port if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to listen for incoming client connections using TCP/IP. For information on configuring a firewall, see "How to: Configure a Firewall for SQL Server Access" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online or review your firewall documentation.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client fully support both Internet Protocol version 4 (IPv4) and Internet Protocol version 6 (IPv6). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager accepts both IPv4 and IPv6 formats for IP addresses. For information on IPv6, see "Connecting Using IPv6" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Connecting to the Local Server  
 When connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on the same computer as the client, you can use `(local)` as the server name. This is not encouraged as it leads to ambiguity, however it can be useful when the client is known to be running on the intended computer. For instance, when creating an application for mobile disconnected users, such as a sales force, where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will run on laptop computers and store project data, a client connecting to `(local)` would always connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on the laptop. The word `localhost` or a period (**.**) can be used in place of `(local)`.  
  
## Verifying Your Connection Protocol  
 The following query returns the protocol used for the current connection.  
  
```  
SELECT net_transport   
FROM sys.dm_exec_connections   
WHERE session_id = @@SPID;  
  
```  
  
## Examples  
 Connecting by server name:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             <servername>  
  
```  
  
 Connecting by server name to a named instance:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             <servername>\<instancename>  
  
```  
  
 Connecting by server name to a specified port:  
  
```  
Alias Name         <serveralias>  
Port No            <port>  
Protocol           TCP/IP  
Server             <servername>  
  
```  
  
 Connecting by IP address:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             <IPAddress>  
  
```  
  
 Connecting by IP address to a named instance:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             <IPAddress>\<instancename>  
  
```  
  
 Connecting by IP address to a specified port:  
  
```  
Alias Name         <serveralias>  
Port No            <port number>  
Protocol           TCP/IP  
Server             <IPAddress>  
  
```  
  
 Connecting to the local computer using `(local)`:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             (local)  
  
```  
  
 Connecting to the local computer using `localhost`:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             localhost  
  
```  
  
 Connecting to a named instance on the local computer `localhost`:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             localhost\<instancename>  
  
```  
  
 Connecting to the local computer using a period:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             .  
  
```  
  
 Connecting to a named instance on the local computer using a period:  
  
```  
Alias Name         <serveralias>  
Port No            <blank>  
Protocol           TCP/IP  
Server             .\<instancename>  
  
```  
  
> [!NOTE]  
>  For information on specifying the network protocol as a **sqlcmd** parameter, see "How to: Connect to the Database Engine Using sqlcmd.exe" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Creating a Valid Connection String Using Shared Memory Protocol](../../tools/configuration-manager/creating-a-valid-connection-string-using-shared-memory-protocol.md)   
 [Creating a Valid Connection String Using Named Pipes](https://msdn.microsoft.com/library/90930ff2-143b-4651-8ae3-297103600e4f)   
 [Choosing a Network Protocol](https://msdn.microsoft.com/library/6565fb7d-b076-4447-be90-e10d0dec359a)  
  
  
