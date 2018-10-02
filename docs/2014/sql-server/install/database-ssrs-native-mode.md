---
title: "Database (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.databasesetup.F1"
ms.assetid: 8c9bb3b3-ea77-4a5b-ba35-7451ed11083d
author: markingmyname
ms.author: maghan
manager: craigg
---
# Database (SSRS Native Mode)
  Use the Database page to create and configure the report server databases that provide internal storage for one or more report server instances. If you are configuring a report server to use a remote report server database, you must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager to create the database.  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 Creating a report server database and configuring the connection is a multi-step process. To guide you through the steps, this page provides Wizards for each type of task. Permissions and logins are created or updated for you. You can monitor the status of each step in the Progress page. If an error occurs, you can click the error for information on how to resolve it.  
  
 A report server database must support a specific server mode. The default mode is native mode, but you can also create a report server database for SharePoint integrated mode if you are running a report server in a larger deployment of a SharePoint product or technology. For more information, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
 To open this page, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and click **Database** in the navigation pane. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
## Options  
 **SQL Server Name**  
 In Current Report Server Database, **SQL Server Name** specifies the name of the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] that runs the report server database. You can use a default or named instance on a local or remote computer.  
  
 **Database Name**  
 Specifies the name of the report server database that stores server data.  
  
 **Report Server Mode**  
 Indicates whether the report server database supports native mode or SharePoint integrated mode. For more information, see [Reporting Services Report Server](../../../2014/reporting-services/reporting-services-report-server.md).  
  
 **Change Database**  
 Start a wizard that guides you through all of the steps required for creating or selecting a report server database.  
  
 **Credential Type**  
 Specifies credentials that the report server uses to connect to the report server database. Credential types you can specify include the service account, a Windows domain user, Windows local user, or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database login. For more information about selecting credentials, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
 **User Name**  
 Specifies a domain user account if you are using Windows credentials, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login if you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credentials. If you are using Windows credentials, specify them in this format: *\<domain>\\<account\>*.  
  
 **Password**  
 Specifies the password for the account.  
  
 **Change Credentials**  
 Start a wizard that guides you through all of the steps required for selecting a different account or updating the password on the account that is used to connect to the report server database.  
  
## See Also  
 [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md)   
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)   
 [Report Server Database &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)   
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../../2014/sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)  
  
  
