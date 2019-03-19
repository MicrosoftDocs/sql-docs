---
title: "Connect to Server (Login Page) Database Engine | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.connecttosqlserver.login.f1"
ms.assetid: e08cfbc3-bed5-4401-a13b-1c66d902fe32
author: stevestein
ms.author: sstein
manager: craigg
---
# Connect to Server (Login Page) Database Engine
  Use this tab to view or specify options when connecting to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
> [!NOTE]  
>  To connect with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be configured in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Authentication mode. For more information about how to determine the authentication mode and to change the authentication mode, see [Change Server Authentication Mode](../../database-engine/configure-windows/change-server-authentication-mode.md).  
  
## Options  
 **Server type**  
 When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services. The rest of the dialog box shows only the options that apply to the selected server type. When registering a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services from the Registered Servers toolbar before starting to register a new server.  
  
 When connecting to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine through [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you must use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and specify a database in the **Connect to Server** dialog box, on the **Connection Properties** tab. Ensure that you select the **Encrypt connection** checkbox.  
  
 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to **master**. If you specify a user database, you will only see that database and its objects in Object Explorer. If you connect to **master**, you will be able to see all databases. For more information, see the [Windows Azure SQL Database Overview](https://social.technet.microsoft.com/wiki/contents/articles/1765.overview-of-azure-sql-database.aspx).  
  
 **Server name**  
 Select the server instance to connect to. The server instance last connected to is displayed by default.  
  
 **Authentication**  
 Two authentication modes are available when connecting to an instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
 When connecting to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine through [!INCLUDE[ssSDS](../../includes/sssds-md.md)], you must use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication and specify a database in the **Connect to Server** dialog box, on the **Connection Properties** tab. Ensure that you select the **Encrypt connection** checkbox.  
  
 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connects to **master**. If you specify a user database, you will only see that database and its objects in Object Explorer. If you connect to **master**, you will be able to see all databases. For more information, see the [Windows Azure SQL Database Overview](https://social.technet.microsoft.com/wiki/contents/articles/1765.overview-of-azure-sql-database.aspx).  
  
 **Windows Authentication Mode (Windows Authentication)**  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication mode allows a user to connect through a Windows user account.  
  
 **SQL Server Authentication**  
 When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs the authentication itself by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have a login account set, authentication fails, and the user receives an error message.  
  
> [!IMPORTANT]  
>  When possible, use Windows Authentication.  
  
 **User name**  
 Enter the user name to connect with. This option is only available if you have selected to connect using Windows Authentication.  
  
 **Login**  
 Enter the login to connect with. This option is only available if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Password**  
 Enter the password for the login. This option is only editable if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Remember password**  
 Click to have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] store the password you have entered. This option is only displayed if you have selected to connect using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 **Connect**  
 Click to connect to the server selected above.  
  
 **Options**  
 Click to change the dialog box and hide the additional server connection options, such as remembering the password.  
  
  
