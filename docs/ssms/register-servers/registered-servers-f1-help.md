---
title: "Registered Servers F1 Help | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Studio [SQL Server], Help"
  - "Registered Servers [SQL Server], help"
  - "SQL Server Management Studio Help [SQL Server], registered servers"
ms.assetid: 59f76b28-ba78-4a1a-b5d5-8b581f30114d
author: "markingmyname"
ms.author: "maghan"
manager: craigg
---
# Registered Servers F1 Help
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  This section contains the F1 Help for the Registered Servers component in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. It describes the various options.
  
 To learn about Registered Servers and get links to what to do with them, go to the [Register Servers](../../tools/sql-server-management-studio/register-servers.md) topic. 
 

 Click to save the Registered Servers settings. 
 
 ## Reporting Services New or Edit Server Registration (General Tab) 
  Use this tab to specify options when registering an instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 To access this page, in Registered Servers, click **Reporting Services** on the **Registered Servers** toolbar, right-click any registered servers group such as **Reporting Services**, point to **New**, and then click **Server Registration**.  
  
### Options  
 **Server type**  
 When a server is registered from Registered Servers, the **Server type** box is read-only, and it matches the type of server displayed in the **Registered Servers** pane. To register a different type of server, click the server you want on the **Registered Servers** toolbar before starting to register a new server.  
  
 **Server name**  
 Specify the report server instance to connect to. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], you can access a report server through its instance name. You can have one report server instance for each SQL Server instance that you install. If you are using the default instance, type the name of the SQL Server instance. If you are using a named instance, specify the named instance to connect to the report server in the format MSSQL$InstanceName.  
  
 **Authentication**  
 Authentication to a report server is made through Internet Information Services (IIS). Select from one of the following authentication modes when connecting to Reporting Services:  
  
 **Windows Authentication Mode (Windows Authentication)**  
 Connect to the report server instance using your [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows credentials.  
  
 **Basic Authentication**  
 Connect using **Basic Authentication** if your Reporting Services installation is configured to use Basic Authentication.  
  
 **Forms Authentication**  
 Connect using **Forms Authentication** if your Reporting Services installation is configured to use a custom authentication extension.  
  
 **User Name**  
 Enter the login name to use for the connection. This option is only available if you have selected **Basic** or **Forms Authentication**.  
  
 **Password**  
 Enter the password for the user name. This option is only editable if you have selected **Basic** or **Forms Authentication**.  
  
 **Remember password**  
 Store the password you have entered. This option is only available if you have clicked **Basic** or **Forms Authentication**.  
  
> **NOTE:** If you have stored the password and want to stop storing it, clear this check box and then click **Save**.  
  
 **Registered server name**  
 The name you want to appear in Registered Servers. This name does not have to match the name in the **Server name** box.  
  
 **Registered server description**  
 Enter an optional description of the server.  
  
 **Test**  
 Click to test the connection to the server selected in **Server name**.  
  
 
 ## Analysis Services - Multidimensional Data New or Edit Server Registration (General Tab)
 
  Use this tab to specify options when registering an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 To access this page, in Registered Servers click **Analysis Services** on the Registered Servers toolbar, right-click any registered servers group, such as **Analysis Services**, point to **New**, and then click **Server Registration**.  
  
### Options  
 **Server type**  
 When a server is registered from Registered Servers, the **Server type** box is read-only, and it matches the type of server displayed in the Registered Servers pane. To register a different type of server, click the server you want on the **Registered Servers** toolbar before starting to register a new server.  
  
 **Server name**  
 Select the server instance to connect to. The server instance last connected to is displayed by default.  
  
 **Authentication**  
 Windows Authentication allows a user to connect using their [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows credentials, as a Windows user or as a member of a Windows group.  
  
 **User name**  
 This option is not available in this release.  
  
 **Password**  
 This option is not available in this release.  
  
 **Remember password**  
 This option is not available in this release.  
  
 **Registered server name**  
 The name you want to appear in Registered Servers. This name does not have to match the **Server name** box.  
  
 **Registered server description**  
 Enter an optional description of the server.  
  
 **Test**  
 Click to test the connection to the server selected in **Server name**. 
 
 ## SSIS New or Edit Server Registration (General Tab) 
 
 Use this tab to specify options when registering [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
 To access this page, in Registered Servers, click **Integration Services** on the **Registered Servers** toolbar, right-click any registered servers group, point to **New**, and then click **Server Registration**.  
  
### Options  
 **Server type**  
 When a server is registered in Registered Servers, the **Server type** box is read-only, and it matches the type of server displayed in Registered Servers. To register a different type of server, click **Database Engine**, **Analysis Server**, **Reporting Services**, **SQL Server Compact** **Edition**, or **Integration Services** on the **Registered Servers** toolbar before starting to register a new server.  
  
 **Server name**  
 Select the server to which to connect. The server last connected to is displayed by default.  
  
 **Authentication**  
 Windows Authentication mode allows a user to connect through a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user account.  
  
 **User name**  
 This option is not available in this release.  
  
 **Password**  
 This option is not available in this release.  
  
 **Remember password**  
 This option is not available in this release.  
  
 **Registered server name**  
 The name you want to appear in **Registered Servers**. This name does not have to match the **Server name** box.  
  
 **Registered server description**  
 Enter an optional description of the server.  
  
 **Test**  
 Click to test the connection to the server selected in **Server name**. 
  

 
 
  
