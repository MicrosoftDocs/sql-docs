---
title: "Upgrade Data Quality Services"
description: This article provides information on how to upgrade your existing installation of SQL Server Data Quality Services (DQS).
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: upgrade-and-migration-article
monikerRange: ">=sql-server-2017"
---
# Upgrade Data Quality Services

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This article provides information on how to upgrade your existing installation of [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Data Quality Services (DQS). As part of upgrading your [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Data Quality Server, you must also upgrade the DQS databases schema.

[!INCLUDE [support-notice](../../data-quality-services/includes/support-notice.md)]

## Before you upgrade

You must back up your DQS databases before upgrading DQS to prevent any accidental data loss during the schema upgrade. For information about backing up DQS databases, see [Backing Up and Restoring DQS Databases](../../data-quality-services/backing-up-and-restoring-dqs-databases.md).

You can connect to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Data Quality Server by using the current or an earlier version of Data Quality Client or the [DQS Cleansing Transformation](../../integration-services/data-flow/transformations/dqs-cleansing-transformation.md) in Integration Services to perform your data quality tasks.

After upgrading Data Quality Services (DQS) and Master Data Services (MDS), any earlier version of the MDS add-in for Excel no longer works. You can download the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of MDS add-in for Excel from [Master Data Services Installation and Configuration](../../master-data-services/master-data-services-installation-and-configuration.md).

## Prerequisites

You must be logged on as a member of the Administrators group on the [!INCLUDE [ssDQSServer](../../includes/ssdqsserver-md.md)] computer.

Your Windows user account must be a member of the **sysadmin** fixed server role in the SQL Server instance where [!INCLUDE [ssDQSServer](../../includes/ssdqsserver-md.md)] is installed.

<a id="Upgrade"></a>

## Upgrade DQS

To upgrade DQS:

1. Back up your DQS databases before you start the upgrade process. For information about backing up DQS databases, see [Backing Up and Restoring DQS Databases](../../data-quality-services/backing-up-and-restoring-dqs-databases.md).

1. Upgrade the SQL Server instance where DQS is installed.

   1. Run the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] Setup wizard.

   1. In the left pane, select **Installation**.

   1. In the right pane, select **Upgrade from...** a previous version of SQL Server.

   1. Complete the Setup wizard.

      This step upgrades your SQL Server instance to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] and also install the latest Data Quality Client, if Data Quality Client was previously installed on this computer. If you have Data Quality Client installed on other computers, you must run the sub steps in Step 2 on those computers to install the current version of Data Quality Client. The Setup wizard installs the current version of Data Quality Client alongside the existing version of Data Quality Client.

      After you upgrade the DQS databases schema, you can connect to the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version of Data Quality Server by using the current or an earlier version of Data Quality Client.

1. Upgrade the DQS databases schema.

   1. Start Command Prompt as an administrator.

   1. At the command prompt, change your directory to the location where DQSInstaller.exe is available. For the default instance of SQL Server, the DQSInstaller.exe file is available at C:\Program Files\Microsoft SQL Server\MSSQL[nn].MSSQLSERVER\MSSQL\Binn:

      In the folder path, replace [nn] with the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] version number.

      - For SQL Server 2016: 13
      - For SQL Server 2017: 14

        ```csharp
        cd C:\Program Files\Microsoft SQL Server\MSSQL[nn].MSSQLSERVER\MSSQL\Binn
        ```

   1. At the command prompt, type the following command, and press ENTER:

      ```cmd
      dqsinstaller.exe -upgrade
      ```

   1. The installer prompts you for backing up the DQS databases before proceeding. If you have already backed up your DQS databases, type **Y** or **Yes**, and then press ENTER to continue with the upgrade.

   1. A completion message is displayed after successful upgrade of the DQS databases schema.

<a id="Verify"></a>

## Verify the DQS Databases schema upgrade

To verify that DQS databases schemas successfully upgraded, you can check the current version in the `DQS_MAIN` and `DQS_PROJECTS` databases by querying the `A_DB_VERSION` table in each database. To do so:

1. Start SQL Server Management Studio, and connect to the SQL Server instance that contains the upgraded DQS databases schema.

1. Run the following query:

   ```sql
   SELECT *
   FROM DQS_MAIN.dbo.A_DB_VERSION
   WHERE STATUS = 2;

   SELECT *
   FROM DQS_PROJECTS.dbo.A_DB_VERSION
   WHERE STATUS = 2;
   ```

1. The output displays an entry for each upgrade along with the date of the upgrade. The maximum VERSION_ID and ASSEMBLY_VERSION on the latest date is the current version. A value of 2 in the STATUS column indicates success. If an error occurs, it's listed in the ERROR column. A sample output:

   | ID | UPGRADE_DATE | VERSION_ID | ASSEMBLY_VERSION | USER_NAME | STATUS | ERROR |
   | --- | --- | --- | --- | --- | --- | --- |
   | 1000 | 2013-08-11 05:26:39.567 | 1200 | 11.0.3000.0 | `<DOMAIN\UserName>` | 2 | |
   | 1001 | 2013-09-19 15:09:37.750 | 1600 | 12.0.xxxx.0 | `<DOMAIN\UserName>` | 2 | |

## Related content

- [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)
- [Remove Data Quality Server Objects](../../sql-server/install/remove-data-quality-server-objects.md)
- [Upgrade SQL Server](upgrade-sql-server.md)
