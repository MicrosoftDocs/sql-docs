---
title: "Administer a report server database (native mode)"
description: Learn about administering a Reporting Services deployment, including backup and restore of report server databases and managing encryption keys.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "report servers [Reporting Services], databases"
  - "renaming databases"
  - "report server database"
  - "databases [Reporting Services], administering"
  - "reportservertempdb"
  - "reportserver database"
---
# Administer a report server database (SSRS native mode)
  A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment uses two [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases for internal storage. By default, the databases are named `ReportServer` and `ReportServerTempdb`. `ReportServerTempdb` is created with the primary report server database and is used to store temporary data, session information, and cached reports.  
  
 In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], database administration tasks include backing up and restoring the report server databases. The tasks also include managing the encryption keys that are used to encrypt and decrypt sensitive data.  
  
 To administer the report server databases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides various tools.  
  
-   You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], the [!INCLUDE[tsql](../../includes/tsql-md.md)] commands, or the database command prompt utilities to:
    - Back up or restore the report server database 
    - Move a report server database
    - Recover a report server database  
  
    For more information, see [Move the report server databases to another computer &#40;SSRS native mode&#41;](../../reporting-services/report-server/moving-the-report-server-databases-to-another-computer-ssrs-native-mode.md).  
  
-   To copy existing database content to another report server database, you can attach a copy of a report server database and use it with a different report server instance. Or, you can create and run a script that uses SOAP calls to recreate report server content in a new database. You can use the **rs** utility to run the script.  
  
-   You can use the **Database Setup** page in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to manage connections between the report server and report server database. You can also use it to find out which database is used for a particular report server instance. To learn more about the report server connection to the report server database, see [Configure a report server database connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
## SQL Server sign in and database permissions  
 The report server databases are used internally by the report server. Connections to either database are made by the Report Server service. You use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to configure the report server connection to the report server database.  
  
 Credentials for the report server connection to the database can be the service account, a Windows local or domain user account, or a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user. You must choose an existing account for the connection. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't create accounts for you.  
  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign-in to the report server database is created for you automatically for the account you specify.  
  
 Permissions to the database are also configured automatically. The Reporting Services Configuration tool assigns the account or database user to the **Public** and **RSExecRole** roles for the report server databases. The **RSExecRole** provides permissions for accessing the database tables and for executing stored procedures. The **RSExecRole** is created in the primary database and msdb when you create the report server database. The **RSExecRole** is a member of the **db_owner** role for the report server databases, allowing the report server to update its own schema in support of an autoupgrade process.  
  
## Naming conventions for the report server databases  
 When you create the primary database, the name of the database must follow the rules specified for [Database identifiers](../../relational-databases/databases/database-identifiers.md). The temporary database name always uses the same name as the primary report server database but with a Tempdb suffix. You can't choose a different name for the temporary database.  
  
 Renaming a report server database isn't supported because the report server databases are considered internal components. Renaming the report server databases causes errors to occur. Specifically, if you rename the primary database, an error message explains that the database names are out of sync. If you rename the **ReportServerTempdb** database, the following internal error occurs later when you run reports:  
  
 "An internal error occurred on the report server. For more information, see the error log. (rsInternalError)  
  
 Invalid object name `ReportServerTempDB.dbo.PersistedStream`."  
  
 This error occurs because the `ReportServerTempdb` name is stored internally and used by stored procedures to perform internal operations. Renaming the temporary database prevents the stored procedures from working properly.  
  
## Enable snapshot isolation on the report server database  
 You can't enable snapshot isolation on the report server database. If snapshot isolation is turned on, you encounter the following error: "The selected report isn't ready for viewing. The report is still being rendered or a report snapshot isn't available."  
  
 If you didn't purposely enable snapshot isolation, the attribute might be set by another application or the **model** database might have snapshot isolation enabled, causing all new databases to inherit the setting.  
  
 To turn off snapshot isolation on the report server database, start Management Studio, open a new query window, paste and then run the following script:  
  
```  
ALTER DATABASE ReportServer  
SET ALLOW_SNAPSHOT_ISOLATION OFF  
ALTER DATABASE ReportServerTempdb  
SET ALLOW_SNAPSHOT_ISOLATION OFF  
ALTER DATABASE ReportServer  
SET READ_COMMITTED_SNAPSHOT OFF  
ALTER DATABASE ReportServerTempDb  
SET READ_COMMITTED_SNAPSHOT OFF  
```  
  
## About database versions  
 In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], explicit information about the database version isn't available. However, because database versions are always synchronized to product versions, you can use product version information to tell when the database version changed. Product version information for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is indicated through file version information that appears in the log files, in the headers of all SOAP calls, and when you connect to the report server URL (for example, when you open a browser to `https://localhost/reportserver`).  
  
## Related content

- [Create a native mode report server database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md)
- [Configure the report server service account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Configure a report server database connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)
- [Create a report server database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)
- [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)
- [Backup and restore operations for Reporting Services](../../reporting-services/install-windows/backup-and-restore-operations-for-reporting-services.md)
- [Report server database &#40;SSRS native mode&#41;](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)
- [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)
- [Store encrypted report server data &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md)
- [Configure and manage encryption keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)
