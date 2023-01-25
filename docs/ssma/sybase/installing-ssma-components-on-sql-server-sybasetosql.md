---
description: "Installing SSMA components on SQL Server (SybaseToSQL)"
title: "Installing SSMA components on SQL Server (SybaseToSQL) | Microsoft Docs"
ms.custom:
  - intro-installation
ms.date: "04/29/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 5ad9e12c-2cdb-4dd2-8703-05a23242d19d
author: cpichuka 
ms.author: cpichuka 
---

# Installing SSMA components on SQL Server (SybaseToSQL)

In addition to installing SSMA, for using Server side data migration, you must also install components on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These components include the SSMA extension pack, which supports data migration, and Sybase providers to enable server-to-server connectivity.

## SSMA for Sybase extension pack

The SSMA extension pack adds the **sysdb** database to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The database contains the tables and stored procedures that are required to migrate data.

Also, when you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs when server side data migration engine is used for migrating the data.

### Prerequisites

Before you install the SSMA for Sybase server components on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the system meets the following requirements:

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is installed.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- The Sybase OLE DB/ADO.Net/ODBC provider and connectivity to the SAP ASE database server that contains the databases you want to migrate. You can install providers from the SAP ASE product media. For information about connectivity, see [Connecting to Sybase ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sybase-ase-sybasetosql.md).
- The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service must be running during installation. This is used to populate a list of the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the Setup wizard. You can disable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service after installation.

  > [!NOTE]
  > If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is running, but you still do not see a list of instances in Setup, you must unblock UDP port 1434. You can use Windows Firewall to temporarily unblock the port, or you can temporarily disable Windows Firewall. You might also have to temporarily disable antivirus software. Make sure to enable firewalls and antivirus software after installation.

### Installing the extension pack

You can install the extension pack any time before you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]
> To install the extension pack, you must be a member of the sysadmin server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

To install the extension pack:

1. Copy **SSMAforSybaseExtensionPack_*n*.msi**, where *n* is the build number, to the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
2. Double-click **SSMAforSybaseExtensionPack_*n*.msi**.
3. On the **Welcome** page, click **Next**.
4. On the **End-User License Agreement** page, read the license agreement. If you agree, select the **I accept the agreement** option, and then click **Next**.
5. On the **Choose Setup Type** page, click **Typical**.
6. On the **Ready to Install** page, click **Install**.
7. On the **Completed the First Step of Installation** page, click **Next**.

   A new dialog box will appear, in which you select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the Extension Pack installation.

8. Select the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you will be migrating SAP ASE databases, and then click **Next**.

   The default instance has the same name as the computer. Named instances will be followed by a backslash and the instance name.

9. On the connection page, select the authentication method and then click **Next**.

   Windows Authentication will use your Windows credentials to try to log on to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select Server Authentication, you must enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.

10. Next step requires you to set the password for a master key that will be used to encrypt any sensitive data stored in the extension pack database during server-side data migration. Provide a strong password and click **Next**.

11. On the next page, select **Install Utilities Database *n* and Install Extension Pack libraries**, where *n* is the version number. If you plan on using the Tester feature, select **Install Tester Database** check box, and then select **Next**.

    The **sysdb** database is created with the tables and stored procedures required for data migration (using server-side data migration engine) are created in this database.

12. Once installation is complete, a prompt will appear asking if you want to install Utilities Database on another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], select **Yes**, and then select **Next**, or to exit the wizard, select **No** and then select **Exit**.

### SQL Server database objects

After you install the extension pack, you will a see a **ssma_syb.bcp_migration_packages** table in the **sysdb** database. You will also see the following stored procedures:

- `bcp_clean_migration_data`
- `bcp_ensure_message_table`
- `bcp_insert_new_message`
- `bcp_post_process`
- `bcp_read_new_migration_messages`
- `bcp_save_migration_package`
- `bcp_smart_truncate`
- `bcp_start_migration_process`
- `get_jobstep_info`
- `stop_agent_process`

Every time that you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job. These jobs are named **ssma_syb data migration package {GUID}**, and are visible in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in the Jobs folder.  

## Sybase providers

When you use server-side data migration to move data from SAP ASE to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the data migrates directly between SAP ASE and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It does not go through SSMA because this would slow down the data migration.

### Installing the Sybase providers

The following instructions provide the basic installation steps for installing Sybase providers. The exact instructions will differ depending on the version of the Sybase Setup program.

> [!IMPORTANT]
> Before you run the Setup program, verify that you are not violating your licensing agreements.

1. Run the Sybase ASE Setup program.
2. Select custom setup.
3. On the feature selection page, select the ODBC, OLE DB and ADO.NET data providers.
4. Verify the selected features, and then click **Finish** to install the data provider.

## See also

- [Installing SSMA  for Sybase Client](../../ssma/sybase/installing-ssma-for-sybase-client-sybasetosql.md)
- [Migrating Sybase ASE Databases to SQL Server - Azure SQL Database](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)
