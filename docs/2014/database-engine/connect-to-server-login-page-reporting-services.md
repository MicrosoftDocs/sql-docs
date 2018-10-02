---
title: "Connect to Server (Login Page) Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.connecttors.login.f1"
helpviewer_keywords: 
  - "Connect to Server dialog box, Reporting Services"
ms.assetid: d312c740-19d7-4931-84a2-88b805ec8439
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Connect to Server (Login Page) Reporting Services
  Use this tab to view or specify the following options when connecting to [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
## Options  
 **Server type**  
 When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services. The rest of the dialog shows only the options that apply to the selected server type. When registering a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../includes/ssde-md.md)], Analysis Services, Reporting Services, or Integration Services from the Registered Servers toolbar before starting to register a new server.  
  
 **Server name**  
 The server mode of the report server instance you are connecting to determines the value you must enter.  
  
 For a report server that runs in native mode, specify the report server instance to connect to. If you are using the default instance, the server name is typically the name of the computer. If you installed a named instance, append the instance name to the server name in this format: \<servername>\\<InstanceName\>. Reporting Services uses the backslash character to delimit the instance name.  
  
 For a report server that runs in SharePoint integrated mode, you must specify a SharePoint site. You can specify any site in a site collection that is integrated with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. The URL that you provide must include the HTTP or HTTPS prefix. You must have permission to access the SharePoint site in order to connect to it in Management Studio. The permission level you are assigned to will determine which items you can view and manage. For more information, see [Connect to a Report Server in Management Studio](../reporting-services/tools/connect-to-a-report-server-in-management-studio.md).  
  
 **Authentication**  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] can be configured to accept Windows Authentication requests or Forms authentication requests that are handled by a custom authentication extension that you provide. Select from one of the following authentication modes when connecting to Reporting Services:  
  
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
 Store the password you have entered. This option is only displayed if you click **Options**, and it is only editable if you have selected to connect using **Basic** or **Forms Authentication**.  
  
 **Connect**  
 Connect to the selected server.  
  
 **Options**  
 Display additional server connection options, such as remembering the password.  
  
## See Also  
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
 [Connect to a Report Server in Management Studio](../reporting-services/tools/connect-to-a-report-server-in-management-studio.md)   
 [Authentication with the Report Server](../reporting-services/security/authentication-with-the-report-server.md)  
  
  
