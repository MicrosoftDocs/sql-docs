---
title: Create a New Registered Server
description: "An overview of how to create a new registered server in SQL Server Management Studio."
author: markingmyname
ms.author: maghan
ms.date: 09/27/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.registerserver.general.sqlce.f1"
  - "sql13.swb.registerserver.general.sqlserver.f1"
helpviewer_keywords:
  - "Registered Servers [SQL Server], creating new registered servers"
---

# Create a New Registered Server (SQL Server Management Studio)

[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article describes how to save the connection information for servers that you access frequently, by registering the server in the Registered Servers component of SQL Server Management Studio in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. A server can be registered before connecting, or when connecting from Object Explorer. There's a special menu option to register the server instances on the local computer.  
  
There are two kinds of registered servers:  
  
- Local server groups  
  
  Use local server groups to easily connect to servers that you frequently manage. Both local and nonlocal servers are registered into local server groups. Local server groups are unique to each user. For information about how to share registered server information, see [Export Registered Server Information &#40;SQL Server Management Studio&#41;](./export-registered-server-information-sql-server-management-studio.md) and [Import Registered Server Information &#40;SQL Server Management Studio&#41;](./import-registered-server-information-sql-server-management-studio.md).  
  
  > [!NOTE]  
  >  We recommend that you use Windows Authentication whenever possible.  
  
- Central Management Servers  
  
  Central Management Servers store server registrations in the Central Management Server instead of on the file system. Central Management Servers and subordinate registered servers can be registered only by using Windows Authentication. After a Central Management Server has been registered, its associated registered servers will be automatically displayed. For more information about Central Management Servers, see [Administer Multiple Servers Using Central Management Servers](../../relational-databases/administer-multiple-servers-using-central-management-servers.md). Versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are earlier than [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] can't be designated as a Central Management Server.  
  
## <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
### To create a new registered server
  
1. If Registered Servers isn't visible in SQL Server Management Studio, on the **View** menu, select **Registered Servers**.  
  
     **Server type**  
     When a server is registered from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers pane. To register a different type of server, select **Database Engine**, **Analysis Server**, **Reporting Services**, or **Integration Services** on the **Registered Servers** toolbar before starting to register a new server.  
  
     **Server name**  
     Select the server instance to register in the format: *\<servername>*[\\*\<instancename>*].  
  
     **Authentication**  
     Two authentication modes are available when connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     **Windows Authentication**  
     Windows Authentication mode allows a user to connect through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user account.  
  
     **SQL Server Authentication**  
     When a user connects with a specified login name and password from a nontrusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication itself by checking whether a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and whether the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't have a login account set, authentication fails, and the user receives an error message.  
  
    > [!IMPORTANT]  
    >  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)] For more information, see [Choose an Authentication Mode](../../relational-databases/security/choose-an-authentication-mode.md).  
  
     **User name**  
     Shows the current user name you're connecting with. This read-only option is only available if you have selected to connect using Windows Authentication. To change **User names**, sign into the computer as a different user.  
  
     **Login**  
     Enter the login to connect with. This option is available only if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
     **Password**  
     Enter the password for the login. This option can be edited only if you have selected to connect by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
     **Remember password**  
     Select to have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encrypt and store the password you have entered. This option is displayed only if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
    > [!NOTE]  
    >  If you have stored the password and want to stop storing it, clear this check box, and then click **Save**.  
  
     **Registered server name**  
     The name you want to appear in Registered Servers. This name doesn't have to match the **Server name** box.  
  
     **Registered server description**  
     Enter an optional description of the server.  
  
     **Test**  
     Select to test the connection to the server selected in **Server name**.  
  
     **Save**  
     Select to save the registered server settings.  
  
## Multiserver Queries

 The Query Editor window in SQL Server Management Studio can connect to and query multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the same time. The results that are returned by the query can be merged into a single results pane, or they can be returned in separate results panes. As an option, Query Editor can include columns that provide the name of the server that produced each row, and also the login that was used to connect to the server that provided each row. For more information about how to execute multiserver queries, see [Execute Statements Against Multiple Servers Simultaneously &#40;SQL Server Management Studio&#41;](./execute-statements-against-multiple-servers-simultaneously.md).  
  
 To execute queries against all the servers in a local server group, right-click the server group, select **Connect**, and then select **New Query**. When queries are executed in the new Query Editor window, they'll execute against all servers in the group, using the stored connection information including the user authentication context. Servers registered by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication but not saving the password will fail to connect.  
  
 To execute queries against all the servers that are registered with a Central Management Server, expand the Central Management Server, right-click the server group, select **Connect**, and then select **New Query**. When queries are executed in the new Query Editor window, they'll execute against all of the servers in the server group, using the stored connection information and using the Windows Authentication context of the user.  
  
## See also

- [Hide System Objects in Object Explorer](../object/hide-system-objects-in-object-explorer.md)
- [Export Registered Server Information &#40;SQL Server Management Studio&#41;](./export-registered-server-information-sql-server-management-studio.md)
- [Import Registered Server Information &#40;SQL Server Management Studio&#41;](./import-registered-server-information-sql-server-management-studio.md)  
- [Administer multiple servers using Central Management Servers](../../relational-databases/administer-multiple-servers-using-central-management-servers.md)  
