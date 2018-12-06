---
title: "New or Edit Server Registration (General Tab) (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.registerserver.general.reportserver.f1"
ms.assetid: 5f899a8e-52ef-46b5-b7a9-f200ccd9f724
author: mashamsft
ms.author: mathoma
manager: craigg
---
# New or Edit Server Registration (General Tab) (Reporting Services)
  Use this tab to specify options when registering an instance of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
 To access this page, in Registered Servers, click **Reporting Services** on the **Registered Servers** toolbar, right-click any registered servers group such as **Reporting Services**, point to **New**, and then click **Server Registration**.  
  
## Options  
 **Server type**  
 When a server is registered from Registered Servers, the **Server type** box is read-only, and it matches the type of server displayed in the **Registered Servers** pane. To register a different type of server, click the server you want on the **Registered Servers** toolbar before starting to register a new server.  
  
 **Server name**  
 Specify the report server instance to connect to. In [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], you can access a report server through its instance name. You can have one report server instance for each SQL Server instance that you install. If you are using the default instance, type the name of the SQL Server instance. If you are using a named instance, specify the named instance to connect to the report server in the format MSSQL$InstanceName.  
  
 **Authentication**  
 Authentication to a report server is made through Internet Information Services (IIS). Select from one of the following authentication modes when connecting to Reporting Services:  
  
 **Windows Authentication Mode (Windows Authentication)**  
 Connect to the report server instance using your [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows credentials.  
  
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
  
> [!NOTE]  
>  If you have stored the password and want to stop storing it, clear this check box and then click **Save**.  
  
 **Registered server name**  
 The name you want to appear in Registered Servers. This name does not have to match the name in the **Server name** box.  
  
 **Registered server description**  
 Enter an optional description of the server.  
  
 **Test**  
 Click to test the connection to the server selected in **Server name**.  
  
 **Save**  
 Click to save the registered server settings.  
  
  
