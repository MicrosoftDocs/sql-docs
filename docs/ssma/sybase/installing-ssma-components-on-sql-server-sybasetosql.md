---
title: "Install SSMA components on SQL Server (SybaseToSQL)"
description: Install components on the server that runs SQL Server to support SAP ASE database conversion with SSMA, including the SSMA extension pack and Sybase providers.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: "04/24/2026"
ms.service: sql
ms.subservice: ssma
ms.topic: install-set-up-deploy
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-installation
helpviewer_keywords:
  - "SSMA extension pack, Installation"
---

# Install SSMA components on SQL Server (SybaseToSQL)

In addition to installing SQL Server Migration Assistant (SSMA), you must also install components on the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. These components include the SSMA extension pack, which supports data migration, and Sybase providers to enable server-to-server connectivity.

## SSMA for Sybase extension pack

The SSMA extension pack adds a database, `sysdb`, to the specified instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This database contains the tables and stored procedures that are required to migrate data.

Also, when you migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs, when server side data migration engine is used for migrating the data.

### Prerequisites

Before you install the SSMA for Sybase server components on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the computer meets the following requirements:

- Windows 11 or later versions, or Windows Server 2022 or later versions.
- The [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. [Download .NET Framework](https://dotnet.microsoft.com/download/dotnet-framework).
- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is installed.
- The Sybase OLE DB, ADO.NET, and ODBC providers, and connectivity to the SAP ASE database server that contains the databases you want to migrate. You can install providers from the SAP ASE product media. For information about connectivity, see [Connecting to Sybase ASE &#40;SybaseToSQL&#41;](../../ssma/sybase/connecting-to-sybase-ase-sybasetosql.md).
- The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service must be running during installation. This service populates a list of the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in the Setup wizard. You can disable the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service after installation.

  > [!NOTE]  
  > If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is running, but you still don't see a list of instances in Setup, you must unblock UDP port 1434. You can use Windows Firewall to temporarily unblock the port, or you can temporarily disable Windows Firewall. You might also have to temporarily disable antivirus software. Make sure to enable firewalls and antivirus software after installation.

### Install the extension pack

You can install the extension pack anytime before you migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> To install the extension pack, you must be a member of the **sysadmin** server role on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

To install the extension pack:

1. Copy **SSMAforSybaseExtensionPack_*n*.msi**, where *n* is the build number, to the computer that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Double-click **SSMAforSybaseExtensionPack_*n*.msi**.

1. On the **Welcome** dialog box, select **Next**.

1. On the **End-User License Agreement** dialog box, read the license agreement. If you agree, select the **I accept the agreement** option, and then select **Next**.

1. On the **Choose Setup Type** dialog box, select **Typical**.

1. On the **Ready to Install** dialog box, select **Install**.

1. On the **Completed the First Step of Installation** dialog box, select **Next**.

   A new dialog box appears, in which you select the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for the Extension Pack installation.

1. Select the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where you are migrating SAP ASE databases, and then select **Next**.

   The default instance has the same name as the computer. Named instances are followed by a backslash and the instance name.

1. On the connection page, select the authentication method and then select **Next**.

   Windows Authentication uses your Windows credentials to try to sign in to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you select Server Authentication, you must enter a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.

1. Set the password for a master key to encrypt any sensitive data stored in the extension pack database during server-side data migration. Provide a strong password and select **Next**.

1. On the next dialog box, select **Install Utilities Database *n* and Install Extension Pack libraries**, where *n* is the version number. If you plan on using the Tester feature, select the **Install Tester Database** check box, and then select **Next**.

   The `sysdb` database is created with the tables and stored procedures required for data migration (using server-side data migration engine) in this database.

1. To install the utilities to another instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], select **Yes**, and then select **Next**. Or, to exit the wizard, select **No** and then select **Exit**.

### SQL Server database objects

After you install the extension pack, you see a `ssma_syb.bcp_migration_packages` table in the `sysdb` database. You also see the following stored procedures:

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

Every time that you migrate data to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job. These jobs are named `ssma_syb data migration package {GUID}`, and are visible in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent node of [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in the Jobs folder.

## Sybase providers

When you use server-side data migration to move data from SAP ASE to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the data migrates directly between SAP ASE and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. It doesn't go through SSMA because this would slow down the data migration.

### Install the Sybase providers

The following instructions provide the basic installation steps for installing Sybase providers. The exact instructions differ depending on the version of the Sybase Setup program.

> [!IMPORTANT]  
> Before you run the Setup program, verify that you're not violating your licensing agreements.

1. Run the Sybase ASE Setup program.

1. Select custom setup.

1. On the feature selection page, select the ODBC, OLE DB, and ADO.NET data providers.

1. Verify the selected features, and then select **Finish** to install the data provider.

## Related content

- [Install SSMA for Sybase client](installing-ssma-for-sybase-client-sybasetosql.md)
- [Migrating SAP ASE databases to SQL Server - Azure SQL Database](migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)
