---
title: "Logging In to SQL Server"
description: Find out about different ways of signing in to an instance of SQL Server. Learn which format to use for the server name in various environments.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server, logging in"
  - "services [SQL Server], logging in"
  - "TCP connection string"
  - "connecting to the Database Engine"
  - "logins [SQL Server], about logging in"
  - "named pipe connection string"
  - "log ins [SQL Server]"
  - "shared memory connection string"
  - "logging in [SQL Server]"
  - "logins [SQL Server]"
---
# Logging In to SQL Server

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can log in to an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using any of the graphical administration tools or from a command prompt.  
  
When you log in to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a graphical administration tool such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you are prompted to supply the server name, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, and a password, if necessary. If you log in to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Windows Authentication, you do not have to provide a SQL Server login each time you access an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses your [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows account to log you in automatically. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in mixed mode authentication ( [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication Mode), and you choose to log in using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must provide a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login and password. When possible, use Windows Authentication.  
  
> [!NOTE]  
> If you selected a case-sensitive collation when you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is also case sensitive.  
  
## Format for Specifying the Name of SQL Server

When connecting to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], you must specify the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the default instance (an unnamed instance), then specify the name of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, or the IP address of the computer. If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is a named instance (such as SQLEXPRESS), then specify the name of the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed, or the IP address of the computer, and add a slash and the instance name.  
  
 The following examples connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on a computer named APPHOST. When specifying a named instance, the examples use an instance name SQLEXPRESS.  
  
 **Examples:**  
  
|Type of Instance|Entry for the server name|  
|----------------------|-------------------------------|  
|Connection to a default instance using the default protocol.|APPHOST|  
|Connection to a named instance using the default protocol. |APPHOST\SQLEXPRESS|  
|Connection to a default instance on the same computer using a period to indicate that the instance is running on the local computer.|.|  
|Connection to a named instance on the same computer using a period to indicate that the instance is running on the local computer.|.\SQLEXPRESS|  
|Connection to a default instance on the same computer using localhost to indicate that the instance is running on the local computer.|localhost|  
|Connection to a named instance on the same computer using localhost to indicate that the instance is running on the local computer.|localhost\SQLEXPRESS|  
|Connection to a default instance on the same computer using (local) to indicate that the instance is running on the local computer.|(local)|  
|Connection to a named instance on the same computer using (local) to indicate that the instance is running on the local computer.|(local)\SQLEXPRESS|  
|Connection to a default instance on the same computer forcing a shared memory connection.|lpc:APPHOST|  
|Connection to a named instance on the same computer forcing a shared memory connection.|lpc:APPHOST\SQLEXPRESS|  
|Connection to a default instance listening on TCP address 192.168.17.28 using an IP address.|192.168.17.28|  
|Connection to a named instance listening on TCP address 192.168.17.28 using an IP address.|192.168.17.28\SQLEXPRESS|  
|Connection to a default instance that is not listening on the default TCP port, by specifying the port that is being used, in this case 2828. (Specifying a port number is not necessary if the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is listening on the default port (1433).)|APPHOST,2828|  
|Connection to a named instance on a designated TCP port, in this case 2828. (Specifying a port number is often necessary if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is not running on the host computer.)|APPHOST,2828|  
|Connection to a default instance that is not listening on the default TCP port, by specifying both the IP address and the TCP port that is being used, in this case 2828.|192.168.17.28,2828|  
|Connection to a named instance by specifying both the IP address and the TCP port that is being used, in this case 2828.|192.168.17.28\SQLEXPRESS,2828|  
|Connecting to default instance by name, forcing a TCP connection.|tcp:APPHOST|  
|Connecting to named instance by name, forcing a TCP connection.|tcp:APPHOST\SQLEXPRESS|  
|Connecting to a default instance by specifying a named pipe name.|\\\APPHOST\pipe\SQL\query|  
|Connecting to a named instance by specifying a named pipe name.|\\\APPHOST\pipe\MSSQL$SQLEXPRESS\SQL\query|  
|Connecting to default instance by name, forcing a named pipes connection.|np:APPHOST|  
|Connecting to named instance by name, forcing a named pipes connection.|np:APPHOST\SQLEXPRESS|  
  
## Verifying your Connection Protocol

When connected to the [!INCLUDE[ssDE](../../includes/ssde-md.md)], the following query will return the protocol  used for the current connection, along with the authentication method (NTLM or Kerberos), and will indicate if the connection is encrypted.  
  
```sql  
SELECT net_transport, auth_scheme, encrypt_option   
FROM sys.dm_exec_connections   
WHERE session_id = @@SPID;  
```  
  
## Related Tasks

[Log In to an Instance of SQL Server &#40;Command Prompt&#41;](../../database-engine/configure-windows/log-in-to-an-instance-of-sql-server-command-prompt.md)  
  
 The following resources can help you troubleshoot a connection problem.  
  
- [How to Troubleshoot Connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)  
  
- [Steps to troubleshoot SQL connectivity issues](/archive/blogs/sql_protocols/steps-to-troubleshoot-sql-connectivity-issues)  
  
## Related Content

- [Choose an Authentication Mode](../../relational-databases/security/choose-an-authentication-mode.md)
  
- [Use the sqlcmd Utility](../../ssms/scripting/sqlcmd-use-the-utility.md)
  
- [Creating a Login](../../t-sql/lesson-2-configuring-permissions-on-database-objects.md)