---
title: "Installing SSMA components on SQL Server (OracleToSQL) | Microsoft Docs"
description: Learn how to install the SSMA extension pack and Oracle providers on the computer that runs SQL Server to support Oracle database conversion.
ms.service: sql
ms.custom:
  - intro-installation
ms.date: "04/29/2021"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords:
  - "Installing the extension pack"
  - "SQL Server Database Objects"
ms.assetid: 33070e5f-4e39-4b70-ae81-b8af6e4983c5
author: cpichuka 
ms.author: cpichuka 
---

# Installing SSMA components on SQL Server (OracleToSQL)

In addition to installing SSMA, you must also install components on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These components include the SSMA extension pack, which supports data migration, and Oracle providers to enable server-to-server connectivity.

## SSMA for Oracle extension pack

The SSMA extension pack deploys extended stored procedures, and adds the **sysdb** database to the specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Extended stored procedures provide functionality required to emulate features and behavior of Oracle, while the **sysdb** database contains the tables and stored procedures required to migrate the data.

Also, when you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs when the server-side data migration engine is used for migrating the data.

### Prerequisites

Before you install the SSMA for Oracle server components on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that the system meets the following requirements:

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is installed.
- [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Installer 3.1 or a later version.
- The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 4.7.2 or a later version. You can obtain it from the [.NET Framework Developer Center](https://go.microsoft.com/fwlink/?LinkId=48882).
- The OLE DB provider for Oracle (if using OLE DB), and connectivity to the Oracle database that you want to migrate. You can install providers from the Oracle product media or Oracle Web site.
- The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service must be running during installation. This is used to populate a list of the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the Setup wizard. You can disable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service after installation.

  > [!NOTE]
  > If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is running, but you still do not see a list of instances in Setup, you must unblock UDP port 1434. You can use Windows Firewall to temporarily unblock the port, or you can temporarily disable Windows Firewall. You might also have to temporarily disable antivirus software. Make sure to enable firewalls and antivirus software after installation.

### Installing the extension pack

You can install the extension pack anytime before you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]
> To install the extension pack, you must be a member of the **sysadmin** server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

To install the extension pack:

1. Copy **SSMAforOracleExtensionPack_*n*.msi** (where *n* is the build number) to the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
2. Double-click **SSMAforOracleExtensionPack_*n*.msi**.
3. On the **Welcome** page, click **Next**.
4. On the **End-User License Agreement** page, read the license agreement. If you agree, select **I accept the agreement** option, and then click **Next**.
5. On the **Choose Setup Type** page, select **Typical**.
6. On the **Ready to Install** page, select **Install**.
7. On the **Completed the First Step of Installation** page, select **Next**.
  
   A new dialog box appears. Select the extension pack type.
  
8. Select the desired installation type and, click **Next**.

   > [!IMPORTANT]
   > Remote option should only be used when installing extension pack on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on Linux or when targeting [!INCLUDE[ssAzureMi](../../includes/ssazuremi_md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installations running on Windows should always have extension pack installed locally. [!INCLUDE[ssAzure](../../includes/ssazure_md.md)] and Azure Synapse Analytics do not support extension pack.

   If you are installing the extension pack on a local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, then next page will allow you to choose a local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to which you'll be migrating Oracle schemas. Choose an instance in the drop-down, and then select **Next**.

   The default instance has the same name as the computer. Named instances will be followed by a backslash and the instance name.

9. On the connection page, select the authentication method and then select **Next**.

   Windows Authentication will use your Windows credentials to try to sign in to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you select Server Authentication, you must enter a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login name and password.

10. Next step requires you to set the password for a master key that will be used to encrypt any sensitive data stored in the extension pack database during server-side data migration. Provide a strong password and click **Next**.

11. On the next page, select **Install Utilities Database *n* and Install Extension Pack libraries**, where *n* is the version number and click **Next**.

    The **sysdb** database is created with the tables and stored procedures required for data migration (using server-side data migration engine) are created in this database.

12. Once installation is complete, a prompt will appear asking if you want to install Utilities Database on another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], select **Yes**, and then select **Next**, or to exit the wizard, select **No** and then select **Exit**.

13. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or by using the `sqlcmd` utility, run the following script to enable CLR:

    ```sql
    sp_configure 'clr enabled', 1
    GO
    RECONFIGURE
    GO
    ```

    If CLR isn't enabled, you'll receive the following error when SSMA connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:

    > SSMA could not retrieve the extension pack assembly version information. Reinstall the extension pack on the database server.

### SQL Server database objects

After you install the extension pack, an **ssma_oracle.bcp_migration_packages** table appears in the **sysdb** database.

Every time you migrate data to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], SSMA creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job. These jobs are named **ssma_oracle data migration package {GUID}**, and are visible in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node of [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in the Jobs folder.

Also following extended stored procedures will be added to the **master** database:

- `xp_ora2ms_exec2`
- `xp_ora2ms_exec2_ex`
- `xp_ora2ms_versioninfo2`

## See also

- [Installing SSMA for Oracle Client](../../ssma/oracle/installing-ssma-for-oracle-client-oracletosql.md)
- [Migrating Oracle Databases to SQL Server](../../ssma/oracle/migrating-oracle-databases-to-sql-server-oracletosql.md)
