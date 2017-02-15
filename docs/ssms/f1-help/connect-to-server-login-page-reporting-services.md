---
title: "Connect to Server (Login Page) Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.swb.connecttors.login.f1"
helpviewer_keywords: 
  - "Connect to Server dialog box, Reporting Services"
ms.assetid: d312c740-19d7-4931-84a2-88b805ec8439
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Connect to Server (Login Page) Reporting Services
Use this tab to view or specify the following options when connecting to [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion_md.md)].  
  
## Options  
**Server type**  
When registering a server from Object Explorer, select the type of server to connect to: [!INCLUDE[ssDE](../../includes/ssde_md.md)], Analysis Services, Reporting Services, or Integration Services. The rest of the dialog shows only the options that apply to the selected server type. When registering a server from Registered Servers, the **Server type** box is read-only, and matches the type of server displayed in the Registered Servers component. To register a different type of server, select [!INCLUDE[ssDE](../../includes/ssde_md.md)], Analysis Services, Reporting Services, or Integration Services from the Registered Servers toolbar before starting to register a new server.  
  
**Server name**  
The server mode of the report server instance you are connecting to determines the value you must enter.  
  
For a report server that runs in native mode, specify the report server instance to connect to. If you are using the default instance, the server name is typically the name of the computer. If you installed a named instance, append the instance name to the server name in this format: <servername>\\<InstanceName>. Reporting Services uses the backslash character to delimit the instance name.  
  
For a report server that runs in SharePoint integrated mode, you must specify a SharePoint site. You can specify any site in a site collection that is integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion_md.md)]. The URL that you provide must include the HTTP or HTTPS prefix. You must have permission to access the SharePoint site in order to connect to it in Management Studio. The permission level you are assigned to will determine which items you can view and manage. For more information, see [How to: Register and Connect to a Report Server](http://msdn.microsoft.com/en-us/c875ff87-ee7d-443a-a702-bdb4b6c27c6e).  
  
**Authentication**  
[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion_md.md)] can be configured to accept Windows Authentication requests or Forms authentication requests that are handled by a custom authentication extension that you provide. Select from one of the following authentication modes when connecting to Reporting Services:  
  
**Windows Authentication Mode (Windows Authentication)**  
Connect to the report server instance using your [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows credentials.  
  
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
[Configuring a Report Server Connection](http://msdn.microsoft.com/en-us/9759a9fb-35e9-4215-969b-a9f1fea18487)  
[How to: Register and Connect to a Report Server](http://msdn.microsoft.com/en-us/c875ff87-ee7d-443a-a702-bdb4b6c27c6e)  
[Configuring Authentication in Reporting Services](http://msdn.microsoft.com/en-us/753c2542-0e97-4d8f-a5dd-4b07a5cd10ab)  
  
