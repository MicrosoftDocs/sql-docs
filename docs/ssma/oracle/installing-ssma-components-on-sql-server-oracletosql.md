---
title: "Installing SSMA components on SQL Server (OracleToSQL) | Microsoft Docs"
ms.prod: sql
ms.custom: ""
ms.date: "10/01/2019"
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Installign the Extension Pack"
  - "SQL Server Database Objects"
ms.assetid: 33070e5f-4e39-4b70-ae81-b8af6e4983c5
author: "Shamikg"
ms.author: "Shamikg"
manager: shamikg
---
# Installing SSMA components on SQL Server (OracleToSQL)

In addition to installing SSMA, you must also install components on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These components include the SSMA extension pack, which supports data migration, and Oracle providers to enable server-to-server connectivity.  
  
## SSMA for Oracle extension pack

The SSMA extension pack adds the **sysdb** and **ssmatesterdb** databases to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The database **sysdb** contains the tables and stored procedures required to migrate data and the user-defined functions that emulate Oracle system functions. The **ssmatesterdb** database contains the tables and procedures required by the Tester component.  
  
Also, when you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs when the server-side data migration engine is used for migrating the data.  
  
### Prerequisites

Before you install the SSMA for Oracle server components on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the system meets the following requirements:  
  
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is installed. SSMA doesn't support SQL Server 2008 Express Edition.
  
- [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Installer 3.1 or a later version.  
  
- The Oracle Client Provider or the OLE DB provider for Oracle, and connectivity to the Oracle database that you want to migrate. You can install providers from the Oracle product media or Oracle Web site.  
  
- The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service must be running during installation. This is used to populate a list of the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the Setup wizard. You can disable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service after installation.  
  
    > [!NOTE]  
    > If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is running, but you still do not see a list of instances in Setup, you must unblock UDP port 1434. You can use Windows Firewall to temporarily unblock the port, or you can temporarily disable Windows Firewall. You might also have to temporarily disable antivirus software. Make sure to enable firewalls and antivirus software after installation.  
  
### Installing the extension pack

You can install the extension pack anytime before you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
> To install the extension pack, you must be a member of the **sysadmin** server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
**To install the extension pack**
  
1. If you haven't already, extract all files from the SSMA Zip file.  
  
    Depending on the version of WinZip you have, you can either double-click the file, or right-click the file and then select **Extract All** or **Open in WinZip**. Follow the instructions in the WinZip user interface to extract the files.  
  
2. Copy **SSMA for Oracle Extension Pack.*n*.Install.exe** (where *n* is the build number) to the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3. Double-click **SSMA for Oracle Extension Pack.*n*.Install.exe**.  
  
4. On the **Welcome** page, select **Next**.  
  
5. On the **End User License Agreement** page, read the license agreement. If you agree, select the **I accept the terms in the license agreement** check box, and then select **Next**.  
  
6. On the **Choose Setup Type** page, select **Typical**.  
  
7. On the **Ready to Install** page, select **Install**.  
  
8. On the **Completed the First Step of Installation** page, select **Next**.  
  
    A new dialog box will appear, in which you select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the extension pack installation.  
  
9. Select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to which you'll be migrating Oracle schemas, and then select **Next**.  
  
    The default instance has the same name as the computer. Named instances will be followed by a backslash and the instance name.  
  
10. On the connection page, select the authentication method and then select **Next**.  
  
    Windows Authentication will use your Windows credentials to try to sign in to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.  
  
11. On the next page, select **Install Utilities Database** *n*, where *n* is the version number, and then select **Next**.  
  
    The **sysdb** database is created and the user-defined functions and stored procedures are created in that database.  
  
    If **Install Tester Database** option is checked, the tester **ssmatesterdb** database will be created.  
  
12. To install the utilities to another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], select **Yes**, and then select **Next**, or to exit the wizard, select **No**.  
  
13. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using the sqlcmd utility, run the following script to enable CLR:  
  
    ```
    sp_configure 'clr enabled', 1  
    GO  
    RECONFIGURE  
    GO  
    ```

    If CLR isn't enabled, you'll receive the following error when SSMA connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
    SSMA could not retrieve the extension pack assembly version information. Reinstall the extension pack on the database server.  
  
### SQL Server database objects  

After you install the extension pack, an **ssma_oracle.bcp_migration_packages** table appears in the **sysdb** database.

Every time you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job. These jobs are named **ssma_oracle data migration package {GUID}**, and are visible in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in the Jobs folder.  
  
## See also

[Installing SSMA for Oracle Client &#40;OracleToSQL&#41;](../../ssma/oracle/installing-ssma-for-oracle-client-oracletosql.md)  
[Migrating Oracle Databases to SQL Server &#40;OracleToSQL&#41;](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)  
