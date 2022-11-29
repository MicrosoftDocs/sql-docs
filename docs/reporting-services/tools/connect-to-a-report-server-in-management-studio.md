---
title: "Connect to a Report Server in Management Studio | Microsoft Docs"
description: Learn how to connect to any server in the SQL Server family and graphically browse its contents by using the Object Explorer in SQL Server Management Studio.
ms.date: 05/07/2019
ms.service: reporting-services
ms.subservice: tools


ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.connecttors.connectionproperties.f1"
  - "sql13.swb.connecttors.login.f1"
  - "sql13.swb.connection.login.reportserver.f1"
helpviewer_keywords: 
  - "report servers [Reporting Services], connections"
  - "connections [Reporting Services], report server"
  - "registering report servers"
  - "report servers [Reporting Services], registering"
  - "Connect to Server dialog box, Reporting Services"
ms.assetid: c875ff87-ee7d-443a-a702-bdb4b6c27c6e
author: maggiesMSFT
ms.author: maggies
---

# Connect to a Report Server in Management Studio

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] provides Object Explorer, which allows you to connect to any server in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] family and graphically browse its contents. For Reporting Services, you can use Object Explorer to do the following tasks:

- Enable report server features.

- Set server defaults and configure role definitions.

- Manage jobs that are running.

- Manage job schedules.

 You can connect to a native mode report server or a report server that runs in SharePoint integrated mode. Connection syntax and the types of operations that you can do depend on the server mode of the report server and your permissions. If you can't connect to the report server, or have problems doing specific tasks, you probably don't have sufficient permissions, or you specified the name of the report server incorrectly. For more information about permissions and connection syntax, see the table at the end of this article.

 You can't use Object Explorer to view or manage report server content. Content management is performed through the web portal if the report server runs in native mode or through a SharePoint site if the report server runs in SharePoint integrated mode.

 Object Explorer allows you to open connections to multiple server instances in the same workspace as along as the servers are registered in the same server group. Before you can connect to a report server instance in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], the server must be registered. If the report server is already registered, you can skip this step. Instructions for registering report servers are provided at the end of this article.

### To connect to a native mode report server

1. If Object Explorer isn't already open it, select it from the **View** menu.

2. Select **Connect** to view the list of server types, and then select **Reporting Services**.

3. In the **Connect to Server** dialog box, enter the name of the report server instance. Report server instance names are based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance names. By default, the instance name of a local report server instance is just the computer name. If you installed the report server as a named instance, use this syntax to specify the server: *\<servername>[\\<instancename\>]*.

4. Select the **Authentication type**. If you are using Windows Authentication, you connect using your credentials. If you select Basic authentication or Forms authentication, type the account and password.  
  
5. Select **Connect**. The report server appears in Object Explorer.  

6. Right-click the server node to set system properties and server defaults. For more information, see [Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md).

### To connect to a SharePoint integrated mode report server  

1. If Object Explorer isn't already open it, select it from the **View** menu.

2. Select **Connect** to view the list of server types, and then select **Reporting Services**.

3. In the **Connect to Server** dialog box, enter a URL to a SharePoint site. The following example illustrates the syntax: `https://<web server>/sites/<site>`.

4. Select the **Authentication type**. If you are using Windows Authentication, you must connect using your credentials. If you select Basic authentication or Forms authentication, type the account and password.

5. Select **Connect**. The report server appears in Object Explorer.

6. Right-click the server node to set system properties and server defaults. For more information, see [Set Report Server Properties &#40;Management Studio&#41;](../../reporting-services/tools/set-report-server-properties-management-studio.md).

### To register a report server

1. If you can't connect to a report server, you either don't have permission to access it, or the server isn't registered. To register the server, select **View** menu > **Registered Servers**.

2. Select the **Reporting Services** icon.

3. Right-click **Reporting Services**, > **New** > **Server Registration**. The **New Server Registration** dialog box is displayed.

4. For **Server name**, enter a value. You specify the value depending on the server mode:

    - For a native mode report server, type the name of the report server instance. Report server instance names are based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance names. By default, the instance name of a local report server instance is just the computer name. If you installed the report server as a named instance, use this syntax to specify the server: *\<servername>[\\<instancename\>]*.

    - For a report server that runs in SharePoint integrated mode, the server to connect to is the SharePoint site with which the report server is connected. Connect to the SharePoint site so that you can view the permission levels. The permissions control access to report server content and operations. You can specify any site in the site collection. The following example illustrates the syntax: `https://mysharepointsite`.

5. For **Authentication**, select the authentication mode that the report server is already using.

   - If you're using default security, choose **Windows Authentication**.
   - If you installed and deployed a custom security extension, choose **Forms Authentication**.
   - If you configured the report server to use Basic authentication, choose **Basic Authentication**.
   - If the report server is configured for SharePoint integrated mode, choose **Windows Authentication**.

6. Select **Test** to verify the connection.

7. When prompted, select **OK**, and then **Save**.

## Connection Syntax and Permissions

 The following table summarizes the connection syntax, steps, and permissions required to perform specific tasks.

 When you specify [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] as the Server Type in the **Connect to Server** dialog box, you can specify either a report server name or an endpoint to the Web service.

|Connect to|   Tasks   |   Permissions   |
|----------|-----------|-----------------|  
|Native mode report server, connected as the default, or named instance:<br /><br /> \<server name>\<_instance><br /><br /> The connection to the report server is made through the Report Server WMI provider.|View and set server properties and defaults.<br /><br /> View and cancel jobs.<br /><br /> Create and manage shared schedules.<br /><br /> Create, modify, or delete role definitions.|Assigned to the System Administrator role.|  
|Native mode report server, connected as the default, or named instance, through the endpoint to the Report Server Web service:<br /><br /> `https://<servername>/reportserver`<br /><br /> Specifying a URL to the report server provides an alternate way to connect to the report server.|View and set server properties and defaults.<br /><br /> View and cancel jobs.<br /><br /> Create and manage shared schedules.<br /><br /> Create, modify, or delete role definitions.|Assigned to the System Administrator role.|  
|SharePoint integrated mode report server, connected through the SharePoint site:<br /><br /> `https://<webserver>/<SharePointSite>`|View and set server properties and defaults.<br /><br /> View and cancel jobs.<br /><br /> Create and manage shared schedules defined for the site to which you're connected.<br /><br /> View the permission levels defined for the site to which you're connected.|Full Control level of permission on the SharePoint site to which you're connected.|
|SharePoint integrated mode report server, connected through the name of the report server instance:<br /><br /> \<server name>\<_instance>|View and set server properties and defaults.<br /><br /> View and cancel jobs.|Full Control level of permission on the SharePoint site that is integrated with the report server.<br /><br /> Notice that when you connect to the report server rather than the SharePoint site, the number of tasks that you can do is reduced. The reason is because the report server can only return application data that is stored or managed in the report server database, and not in the SharePoint configuration and content databases.|

## See also

 [Configure a Report Server Database Connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)  
 [Reporting Services in SQL Server Management Studio &#40;SSRS&#41;](../../reporting-services/tools/reporting-services-in-sql-server-management-studio-ssrs.md)
