---
title: "Installing SSMA Components on SQL Server (MySQLToSql) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "SSMA extension pack, Installation"
ms.assetid: 6772d0c5-258f-4d7b-afb0-b5f810e71af1
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Installing SSMA Components on SQL Server (MySQLToSql)
In addition to installing SSMA, you must also install components on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These components include the SSMA extension pack, which supports data migration, and MySQL providers to enable server-to-server connectivity.  
  
## SSMA for MySQL Extension Pack  
The SSMA extension pack adds a database, **sysdb**, to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This database contains the tables and stored procedures that are required to migrate data.  
  
Also, when you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, when server side data migration engine is used for migrating the data.  
  
### Prerequisites  
Before you install the SSMA for MySQL server components on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the computer meets the following requirements:  
  
-   [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Installer 3.1 or a later version.  
  
-   The MySQL Client Provider, and connectivity to the MySQL database that you want to migrate. You can install providers from the MySQL product media or MySQL Web site.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service must be running during installation. This is used to populate a list of the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the Setup wizard. You can disable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service after installation.  
  
    > [!NOTE]  
    > If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is running, but you still do not see a list of instances in Setup, you must unblock UDP port 1434. You can use Windows Firewall to temporarily unblock the port, or you can temporarily disable Windows Firewall. You might also have to temporarily disable antivirus software. Make sure to enable firewalls and antivirus software after installation.  
  
### Installing the Extension Pack  
You can install the extension pack any time before you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
> To install the extension pack, you must be a member of the **sysadmin** server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**To install the extension pack**  
  
1.  Copy SSMA for MySQL Extension Pack.*n*.Install.exe, where *n* is the build number, to the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
2.  Double-click SSMA for MySQL Extension Pack.*n*.Install.exe.  
  
3.  On the Welcome dialog box, click **Next**.  
  
4.  On the End User License Agreement dialog box, read the license agreement. If you agree, select the **I accept the terms in the license agreement** check box, and then click **Next**.  
  
5.  On the Choose Setup Type dialog box, click **Typical**.  
  
6.  On the Ready to Install dialog box, click **Install**.  
  
7.  On the Completed the First Step of Installation dialog box, click **Next**.  
  
    A new dialog box will appear, in which you select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the extension pack installation.  
  
8.  Select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you will be migrating MySQL schemas, and then click **Next**.  
  
    The default instance has the same name as the computer. Named instances will be followed by a backslash and the instance name.  
  
9. On the connection dialog box, select the authentication method and then click **Next**.  
  
    Windows Authentication will use your Windows credentials to try to log on to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.  
  
10. On the next dialog box, select **Install Utilities Database** *n*, where *n* is the version number, and then click **Next**.  
  
    The **sysdb** database is created with the tables and stored procedures required for data migration (using server side data migration engine) are created in that database.  
  
11. To install the utilities to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], select **Yes**, and then click **Next**. Or, to exit the wizard, click **No**.  
  
## See Also  
[Installing SSMA for MySQL Client &#40;MySQLToSQL&#41;](../../ssma/mysql/installing-ssma-for-mysql-client-mysqltosql.md)  
[Migrating MySQL Databases to SQL Server - Azure SQL DB &#40;MySQLToSql&#41;](../../ssma/mysql/migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)  
  
