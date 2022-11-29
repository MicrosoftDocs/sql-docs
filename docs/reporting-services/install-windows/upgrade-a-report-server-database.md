---
description: "Upgrade a Report Server Database"
title: "Upgrade a Report Server Database | Microsoft Docs"
ms.date: 08/17/2018
ms.service: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading databases"
  - "report server database"
  - "upgrading Reporting Services"
ms.assetid: 4091cf87-9d97-4048-a393-67f1f9207401
author: maggiesMSFT
ms.author: maggies
---

# Upgrade a Report Server Database

The report server database provides storage for one or more report server instances. Because the report server database schema can change with each new release of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], it is required that the database version match the version of the report server instance you are using. In most cases, a report server database can be upgraded automatically with no specific action on your part.  
  
 **Native Mode:** In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode, the report server database actually comprises two databases that have default names of ReportServer and ReportServerTempDB.  

::: moniker range="=sql-server-2016"
  
 **SharePoint mode:** In SQL Server 2016 Reporting Services SharePoint mode, the report server database is actually a collection of databases that is created for each instance of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  

::: moniker-end

## Ways to Upgrade a Native Mode Report Server Database

 The following list identifies the conditions under which a report server database is upgraded:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup upgrades a single instance of a report server. The report server database schema is automatically upgraded after service startup and the report server determines that the database schema version does not match the server version.  
  
     At service startup, the report server checks the database schema version to verify that it matches the server version. If the database schema version is an older version, it is automatically upgraded to the schema version that is required by the report server. Automatic upgrade is especially useful if you restored or attached an older report server database. A message is entered in the report server trace log file indicating that the database schema version was upgraded.  
  
-   The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager upgrades a local or remote report server database when you select an older version to use with a newer report server instance. In this case, you must confirm the upgrade action before it happens.  
  
     The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager no longer provides a separate Upgrade button or upgrade script. Those features are obsolete starting with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] due to the automatic upgrade feature of the Report Server service.  
  
 After the schema is updated, you cannot roll back the upgrade to an earlier version. Always back up the report server database in case you need to recreate a previous installation.  
  
## How the Schema, Metadata, and Report Server Content is Updated  
 The report server database is upgraded in three stages:  
  
1.  The schema is upgraded automatically after setup and service startup, or when you select a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native mode report server database in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager that is an older version. In addition, the Report Server service checks the database version at startup. If the report server is connected to a database that is an earlier version, the report server will update the database during startup.  
  
2.  Security descriptors are upgraded on first use of the report server database after the schema is updated.  
  
3.  Published reports and compiled report snapshots are updated on first use. For more information, see [Upgrade Reports](../../reporting-services/install-windows/upgrade-reports.md).  
  
 In addition to the report server database, a report server also uses a temporary database. The temporary database is upgraded automatically when you upgrade the report server database.  
  
## Permissions required to upgrade a Report Server Database  
 If you are upgrading a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation that includes a report server database, you may see an error message if the database upgrade is performed with insufficient permissions. By default, Setup uses the security token of the user who is running the Setup program to connect to the remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and update the schema. If you have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **sysadmin** permissions on the database server that hosts the report server databases, the database upgrade will succeed. Similarly, if you run Setup from the command prompt and specify the RSUPGRADEDATABASEACCOUNT and RSUPGRADEPASSWORD arguments for an account that has **sysadmin** permission to modify the schema on the remote computer, the database upgrade will succeed.  
  
 However, if you do not have **sysadmin** permission to the database on the remote computer, the connection will be refused with the following error:  
  
 `"Setup was not able to upgrade the report server database schema. You must update the database schema manually after setup is finished. To update the schema, run the Report Server Configuration Manager, open the Database Setup page, re-select the database, and click Apply. The database will be upgraded automatically."`  
  
 At this point, the report server program files will be upgraded, but the report server database will be in the format of the previous version. The report server will be unavailable until you finish the upgrade process by upgrading the database manually.  
  
#### To upgrade a Native Mode database With Scripts  
 You can use WMI scripts to upgrade a report server database. For more information, see [GenerateDatabaseUpgradeScript Method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-generatedatabaseupgradescript.md)  
  
## Next steps

[Report Server Configuration Manager](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
[Create a Report Server Database](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)  
[Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md)   
[Migrate a Reporting Services Installation](../../reporting-services/install-windows/migrate-a-reporting-services-installation-native-mode.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
