---
title: "Installing SSMA Components on SQL Server (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Installign the Extension Pack"
  - "SQL Server Database Objects"
ms.assetid: 33070e5f-4e39-4b70-ae81-b8af6e4983c5
author: "Shamikg"
ms.author: "Shamikg"
manager: "v-thobro"
---
# Installing SSMA Components on SQL Server (OracleToSQL)
In addition to installing SSMA, you must also install components on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These components include the SSMA extension pack, which supports data migration, and Oracle providers to enable server-to-server connectivity.  
  
## SSMA for Oracle Extension Pack  
The SSMA extension pack adds the databases, **sysdb** and **ssmatesterdb**, to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The database **sysdb** contains the tables and stored procedures that are required to migrate data, and the user-defined functions that emulate Oracle system functions. The **ssmatesterdb** database contains the tables and procedures that are required by the Tester component.  
  
Also, when you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs when server side data migration engine is used for migrating the data.  
  
### Prerequisites  
Before you install the SSMA for Oracle server components on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the system meets the following requirements:  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is installed. SSMA does not support SQL Server 2008 Express Edition.  
  
-   [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Installer 3.1 or a later version.  
  
-   The Oracle Client Provider or the OLE DB provider for Oracle, and connectivity to the Oracle database that you want to migrate. You can install providers from the Oracle product media or Oracle Web site.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service must be running during installation. This is used to populate a list of the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the Setup wizard. You can disable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service after installation.  
  
    > [!NOTE]  
    > If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is running, but you still do not see a list of instances in Setup, you must unblock UDP port 1434. You can use Windows Firewall to temporarily unblock the port, or you can temporarily disable Windows Firewall. You might also have to temporarily disable antivirus software. Make sure to enable firewalls and antivirus software after installation.  
  
### Installing the Extension Pack  
You can install the extension pack any time before you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
> To install the extension pack, you must be a member of the **sysadmin** server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**To install the extension pack**  
  
1.  If you have not already done this, extract all files from the SSMA Zip file.  
  
    Depending on the version of WinZip you have, you can either double-click the file, or right-click the file and select **Extract All** or **Open in WinZip**. Follow the instructions in the WinZip user interface to extract the files.  
  
2.  Copy SSMA for Oracle Extension Pack.*n*.Install.exe, where *n* is the build number, to the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3.  Double-click SSMA for Oracle Extension Pack.*n*.Install.exe.  
  
4.  On the Welcome page, click **Next**.  
  
5.  On the End User License Agreement page, read the license agreement. If you agree, select the **I accept the terms in the license agreement** check box, and then click **Next**.  
  
6.  On the Choose Setup Type page, click **Typical**.  
  
7.  On the Ready to Install page, click **Install**.  
  
8.  On the Completed the First Step of Installation page, click **Next**.  
  
    A new dialog box will appear, in which you select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the extension pack installation.  
  
9. Select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you will be migrating Oracle schemas, and then click **Next**.  
  
    The default instance has the same name as the computer. Named instances will be followed by a backslash and the instance name.  
  
10. On the connection page, select the authentication method and then click **Next**.  
  
    Windows Authentication will use your Windows credentials to try to log on to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.  
  
11. On the next page, select **Install Utilities Database** *n*, where *n* is the version number, and then click **Next**.  
  
    The **sysdb** database is created and the user-defined functions and stored procedures are created in that database.  
  
    If **Install Tester Database** option is checked the tester **ssmatesterdb** database will be created.  
  
12. To install the utilities to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], select **Yes**, and then click **Next**. Or, to exit the wizard, click **No**.  
  
13. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using the sqlcmd utility, run the following script to enable CLR:  
  
    ```  
    sp_configure 'clr enabled', 1  
    GO  
    RECONFIGURE  
    GO  
    ```  
    If CLR is not enabled, you will receive the following error when SSMA connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
    SSMA could not retrieve the extension pack assembly version information. Reinstall the extension pack on the database server.  
  
### SQL Server Database Objects  
After you install the extension pack, you will a see an **ssma_oracle.bcp_migration_packages** table, an **ssma_oracle.db_storage** table, and an **ssma_oracle.db_error_list** table in the **sysdb** database. You will also see many stored procedures and user-defined functions in the **ssma_oracle** schema.  
  
Every time that you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job. These jobs are named **ssma_oracle data migration package {GUID}**, and are visible in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in the Jobs folder.  
  
## See Also  
[Installing SSMA for Oracle Client &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-for-oracle-client-oracletosql.md)  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
  
