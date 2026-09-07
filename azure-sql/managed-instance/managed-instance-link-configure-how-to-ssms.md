---
title: "Configure link with SSMS"
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure a link between SQL Server and Azure SQL Managed Instance with SQL Server Management Studio (SSMS).
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma, danil
ms.date: 03/31/2026
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023, build-2024
ms.topic: how-to
---
# Configure link with SSMS - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Learn how to configure a [link](managed-instance-link-feature-overview.md) between SQL Server and Azure SQL Managed Instance by using SQL Server Management Studio (SSMS). The link replicates databases from your initial primary to your secondary replica in near-real time.

After you create the link, you can fail over to your secondary replica for migration or disaster recovery.


> [!NOTE]
> - You can also configure the link by using [scripts](managed-instance-link-configure-how-to-scripts.md). 
> - Configuring Azure SQL Managed Instance as your initial primary is supported starting with [SQL Server 2022 CU10](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10). 

## Overview

Use the link feature to replicate databases from your initial primary to your secondary replica. For SQL Server 2022, the initial primary can be either SQL Server or Azure SQL Managed Instance. For SQL Server 2019 and earlier versions, the initial primary must be SQL Server. After you configure the link, the database from the initial primary replicates to the secondary replica. 

You can choose to leave the link in place for continuous data replication in a hybrid environment between the primary and secondary replica, or you can fail over the database to the secondary replica, to migrate to Azure or for disaster recovery. For SQL Server 2019 and earlier versions, failing over to Azure SQL Managed Instance breaks the link and fail back isn't supported. With SQL Server 2022 and SQL Server 2025, you have the option to maintain the link and fail back and forth between the two replicas.

If you plan to use your secondary managed instance for only disaster recovery, you can save on licensing costs by activating the [hybrid failover benefit](managed-instance-link-disaster-recovery.md#license-free-passive-dr-replica). 

Use the instructions in this article to manually set up the link between SQL Server and Azure SQL Managed Instance. After you create the link, your source database gets a read-only copy on your target secondary replica. 

## Prerequisites 

To replicate your databases to your secondary replica through the link, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- [SQL Server Management Studio v19.2 or later](/ssms/sql-server-management-studio-ssms).
- A properly [prepared environment](managed-instance-link-preparation.md).

Consider the following:

- The link feature supports one database per link. To replicate multiple databases from an instance, create a link for each individual database. For example, to replicate 10 databases to SQL Managed Instance, create 10 individual links.
- Collation between SQL Server and SQL Managed Instance should be the same. A mismatch in collation can cause a mismatch in server name casing and prevent a successful connection from SQL Server to SQL Managed Instance.
- Error 1475 on your initial SQL Server primary indicates that you need to start a new backup chain by creating a full backup without the `COPY ONLY` option.
- To establish a link, or fail over, *from* SQL Managed Instance to SQL Server 2025, you must configure your SQL managed instance with the [SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy). Data replication and failover *from* SQL Managed Instance to SQL Server 2025 isn't supported by instances configured with a mismatched update policy.
- To establish a link, or fail over, *from* SQL Managed Instance to SQL Server 2022, you must configure your SQL managed instance with the [SQL Server 2022 update policy](update-policy.md#sql-server-2022-update-policy). Data replication and failover *from* SQL Managed Instance to SQL Server 2022 isn't supported by instances configured with a mismatched update policy.
- While you can establish a link from a supported version of SQL Server to a SQL managed instance configured with the **Always-up-to-date** update policy, after failover to SQL Managed Instance, you can't replicate data or fail back to your SQL Server instance. 

## Permissions

For SQL Server, you need **sysadmin** permissions. 

For Azure SQL Managed Instance, you need to be a member of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor) role, or have the following custom role permissions: 

|Microsoft.Sql/ resource|Necessary permissions| 
|---- | ---- | 
|Microsoft.Sql/managedInstances| /read, /write|
|Microsoft.Sql/managedInstances/hybridCertificate | /action |
|Microsoft.Sql/managedInstances/databases| /read, /delete, /write, /completeRestore/action, /readBackups/action, /restoreDetails/read| 
|Microsoft.Sql/managedInstances/distributedAvailabilityGroups| /read, /write, /delete, /setRole/action| 
|Microsoft.Sql/managedInstances/endpointCertificates| /read|
|Microsoft.Sql/managedInstances/hybridLink| /read, /write, /delete|
|Microsoft.Sql/managedInstances/serverTrustCertificates | /write, /delete, /read | 

## Prepare databases

If SQL Server is your initial primary, you need to create a backup of your database. Since Azure SQL Managed Instance takes backups automatically, skip this step if SQL Managed Instance is your initial primary. 

When you create a link, the initial seeding between the primary and secondary replicas happens by taking a full backup of the database on the primary replica, transferring it to the secondary replica, and restoring it there. When you take the full backup, we recommend that you use the `WITH CHECKSUM` option to ensure that the backup is valid and doesn't have any corruption. For more information, see [BACKUP (Transact-SQL)](/sql/t-sql/statements/backup-transact-sql).

Use SSMS to back up your database on SQL Server. Follow these steps:

1. Connect to your SQL Server in SQL Server Management Studio (SSMS). 
1. In **Object Explorer**, right-click the database, hover over **Tasks**, and then choose **Back up**. 
1. Choose **Full** for backup type. 
1. Ensure the **Back up to** option has the backup path to a disk with sufficient free storage space available. 
1. (Optional but recommended) On the **Media Options** tab, check the box for **Perform checksum before writing to media** to have SQL Server verify the integrity of the backup after it's created.
1. Select **OK** to complete the full backup. 

For more information, see [Create a Full Database Backup](/sql/relational-databases/backup-restore/create-a-full-database-backup-sql-server).

> [!NOTE]
> The link supports replicating user databases only. Replication of system databases isn't supported. To replicate instance-level objects stored in `master` or `msdb`, script them out and run T-SQL scripts on the destination instance.

## Create link to replicate database

In the following steps, use the **New Managed Instance link** wizard in SSMS to create a link between your initial primary and your secondary replica. 

After you create the link, your source database gets a read-only copy on your target secondary replica.

1. Open SSMS and connect to your initial primary.
1. In **Object Explorer**, right-click the database you want to link to the secondary, hover over **Azure SQL Managed Instance link**, and select **New...** to open the **New Managed Instance link** wizard. If your SQL Server version isn't supported, the option isn't available in the context menu.

    :::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/new-link.png" alt-text="Screenshot that shows a database's context menu option to create a new link.":::

1. On the **Introduction** page of the wizard, select **Next**.
1. On the **Specify Link Options** page, provide a name for your link. If you select multiple databases, the wizard automatically appends the database name to the end of the name you provide so you don't have to include it yourself. Check the boxes if you want to enable connectivity troubleshooting and, for SQL Server 2022 or SQL Server 2025, if you plan to use the link for two-way disaster recovery. Select **Next**.

   > [!NOTE]
   > Starting with SSMS v22.7.0, you can preview adding multiple databases when creating a link in SSMS. This feature is temporarily disabled unless you request access. For more information, see the [access request form](https://aka.ms/milink-multidb-prpr).

1. On the **Requirements** page, the wizard validates requirements to establish a link to your secondary. Select **Next** after all the requirements are validated, or resolve any requirements that aren't met and then select **Re-run Validation**. 
1. On the **Select Databases** page, choose the database you want to replicate to your secondary replica via the link. Selecting multiple databases creates multiple distributed availability groups, one for each link. Select **Next**. 
1. On the **Specify Secondary Replica** page, select **Add secondary replica** to add your secondary replica. If your initial primary is SQL Server, this action opens the **Sign In** to Azure window. If your initial primary is SQL Managed Instance, this action opens the **Connect to server** dialog box.   
   
   1. For a SQL Server initial primary, sign in to Azure, choose the subscription, resource group, and secondary SQL managed instance from the dropdown list. Select **Login** to open the **Connect to Server** dialog box and then connect to the SQL Managed Instance you want to replicate your database to. When you see **Login successful** on the **Sign in** window, select **OK** to close window and go back to the **New Managed Instance link** wizard. 
   1. For a SQL Managed Instance initial primary, connect to the SQL Server instance you want to replicate your database to. 
   
   > [!NOTE]
   > To establish a link to an availability group that already exists, provide the IP address of the existing listener in the **Endpoint URL** field on the **Endpoints** tab of the **Specify Secondary Replica** page.

1. After adding your secondary replica, use the tabs in the wizard to modify **Endpoint** settings if you need to, and review information about backups and the link endpoint in the remaining tabs. Select **Next** when you're ready to proceed. 
1. If SQL Managed Instance is your initial primary, the next page in the wizard is the **Login to Azure** page. Sign in again if you need to, and then select **Next**. This page isn't available when SQL Server is your initial primary. 
1. On the **Validation** page, make sure all validations are successful. If any fail, resolve them, and then rerun the validation. Select **Next** when ready. 
1. On the **Summary** page, review your configuration once more. Optionally, select **Script** to generate a script so you can easily recreate the same link in the future. Select **Finish** when you're ready to create the link. 
1. The **Executing actions** page displays the progress of each action.  
1. After all steps finish, the **Results** page shows check marks next to the successfully completed actions. You can now close the window. 


## View a replicated database

After you create the link, the database replicates to the secondary replica. Depending on database size and network speed, the database might initially be in a **Restoring** state on the secondary replica. After initial seeding finishes, the database is restored to the secondary replica and ready for read-only workloads. 

On either replica, use **Object Explorer** in SSMS to view the **Synchronized** state of the replicated database. 

:::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/check-replicated-database-on-sql-server.png" alt-text="Screenshot that shows the state of the SQL Server database and distributed availability group in SSMS.":::

Expand **Always On High Availability** and **Availability Groups** to view the distributed availability group created for each link. 

:::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/check-replicated-database-on-sql-managed-instance.png" alt-text="Screenshot that shows the state of the SQL Managed Instance database and distributed availability group.":::

Regardless of which instance is primary, you can also right-click the linked distributed availability group on SQL Server and select **Show Dashboard** to view the dashboard for the distributed availability group, which shows the status of the linked database in the distributed availability group.

## Take first transaction log backup

If SQL Server is your initial primary, take the first [transaction log backup](/sql/relational-databases/backup-restore/back-up-a-transaction-log-sql-server) on SQL Server *after* initial seeding finishes. At that point, the database is no longer in the **Restoring...** state on Azure SQL Managed Instance. Then, take [SQL Server transaction log backups regularly](managed-instance-link-best-practices.md#take-log-backups-regularly) to minimize excessive log growth while SQL Server is in the primary role.

If SQL Managed Instance is your primary, you don't need to take any action as Azure SQL Managed Instance takes log backups automatically.

## Drop a link 

If you want to drop the link, either because it's no longer needed or because it's in an irreparable state and needs to be recreated, you can do so by using SQL Server Management Studio (SSMS). 

You can delete the link from the following menu options in **Object Explorer** of SSMS, after connecting to your instance: 

- **Always On Availability Groups** > **Availability Groups** > Right-click the distributed availability group name associated with the link > **Delete...**
- **Databases** > Right-click the database associated with the link > **Azure SQL Managed Instance link** > **Delete...**


## Troubleshoot 

If you encounter an error message when you create the link, select the error message to open a window with additional details about the error. 

If you encounter an error when working with the link, the SSMS wizard stops execution at the step that failed, and you can't restart it. Address the issue. If necessary, clean up the environment to revert back to the original state by removing the distributed availability group and availability group if you created them while setting up the link. Then launch the wizard again to start over.

For more information, see [troubleshoot issues with the link](managed-instance-link-troubleshoot-how-to.md). 


## Related content

To use the link, see: 
- [Prepare environment for the Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Fail over the link](managed-instance-link-failover-how-to.md)
- [Migrate with the link](managed-instance-link-migrate.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
- [Troubleshoot issues with the link](managed-instance-link-troubleshoot-how-to.md).

To learn more about the link, see: 
- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md).

For other replication and migration scenarios, consider:
- [Transactional replication with SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md).
