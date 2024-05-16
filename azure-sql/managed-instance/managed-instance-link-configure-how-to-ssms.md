---
title: "Configure link with SSMS"
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure a link between SQL Server and Azure SQL Managed Instance with SQL Server Management Studio (SSMS).
author: MariDjo
ms.author: dmarinkovic
ms.reviewer: mathoma, danil
ms.date: 11/14/2023
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---
# Configure link with SSMS - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to configure a [link](managed-instance-link-feature-overview.md) between SQL Server and Azure SQL Managed Instance by using SQL Server Management Studio (SSMS). With the link, databases from your initial primary are replicated to your secondary replica in near-real time.

After the link is created, you can then fail over to your secondary replica for the purpose of migration, or disaster recovery. 


> [!NOTE]
> - It's also possible to configure the link by using [scripts](managed-instance-link-configure-how-to-scripts.md). 
> - Configuring Azure SQL Managed Instance as your initial primary is currently in preview and only supported starting with [SQL Server 2022 CU10](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10). 


## Overview

Use the link feature to replicate databases from your initial primary to your secondary replica. For SQL Server 2022, the initial primary can be either SQL Server or Azure SQL Managed Instance. For SQL Server 2019 and earlier versions, the initial primary must be SQL Server. After the link is configured, databases from the initial primary are replicated to the secondary replica. 

You can choose to leave the link in place for continuous data replication in a hybrid environment between the primary and secondary replica, or you can fail over the database to the secondary replica, to migrate to Azure, or for disaster recovery. For SQL Server 2019 and earlier versions, failing over to Azure SQL Managed Instance breaks the link and fail back is unsupported. With SQL Server 2022, you have the option to maintain the link and fail back and forth between the two replicas - this feature is currently in preview.

If you plan to use your secondary managed instance for only disaster recovery, you can save on licensing costs by activating the [hybrid failover benefit](managed-instance-link-disaster-recovery.md#license-free-passive-dr-replica). 

Use the instructions in this article to manually set up the link between SQL Server and Azure SQL Managed Instance. After the link is created, your source database gets a read-only copy on your target secondary replica. 


## Prerequisites 

> [!NOTE]
> Some functionality of the link is generally available, while some is currently in preview. Review [version supportability](managed-instance-link-feature-overview.md#prerequisites) to learn more. 

To replicate your databases to your secondary replica through the link, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- [SQL Server Management Studio v19.2 or later](/sql/ssms/download-sql-server-management-studio-ssms).
- A properly [prepared environment](managed-instance-link-preparation.md).

Consider the following:

- The link feature supports one database per link. To replicate multiple databases from an instance, create a link for each individual database. For example, to replicate 10 databases to SQL Managed Instance, create 10 individual links.
- Collation between SQL Server and SQL Managed Instance should be the same. A mismatch in collation could cause a mismatch in server name casing and prevent a successful connection from SQL Server to SQL Managed Instance.
- Error 1475 on your initial SQL Server primary indicates that you need to start a new backup chain by creating a full backup without the `COPY ONLY` option.
- To establish a link, or fail over, from SQL Managed Instance to SQL Server 2022, your managed instance must be configured with the [SQL Server 2022 update policy](update-policy.md#sql-server-2022-update-policy). Data replication and failover from SQL Managed Instance to SQL Server 2022 is not supported by instances configured with the Always-up-to-date update policy. 
- While you can establish a link from SQL Server 2022 to a SQL managed instance configured with the Always-up-to-date update policy, after fail over to SQL Managed Instance, you will no longer be able to replicate data or fail back to SQL Server 2022. 

## Permissions

For SQL Server, you should have **sysadmin** permissions. 

For Azure SQL Managed Instance, you should be a member of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor), or have the following custom role permissions: 

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

Use SSMS to back up your database on SQL Server. Follow these steps:

1. Connect to your SQL Server in SQL Server Management Studio (SSMS). 
1. In **Object Explorer**, right-click the database, hover over **Tasks** and then choose **Back up**. 
1. Choose **Full** for backup type. 
1. Ensure the **Back up to** option has the backup path to a disk with sufficient free storage space available. 
1. Select **OK** to complete the full backup. 

For more information, see [Create a Full Database Backup](/sql/relational-databases/backup-restore/create-a-full-database-backup-sql-server).

> [!NOTE]
> The link supports replicating user databases only. Replication of system databases is not supported. To replicate instance-level objects (stored in `master` or `msdb`), script them out and run T-SQL scripts on the destination instance.

## Create link to replicate database

In the following steps, use the **New Managed Instance link** wizard in SSMS to create a link between your initial primary and your secondary replica. 

After you create the link, your source database gets a read-only copy on your target secondary replica.

1. Open SSMS and connect to your initial primary.
1. In **Object Explorer**, right-click the database you want to link to the secondary, hover over **Azure SQL Managed Instance link**, and select **New...** to open the **New Managed Instance link** wizard. If your SQL Server version isn't supported, the option isn't available in the context menu.

    :::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/new-link.png" alt-text="Screenshot that shows a database's context menu option to create a new link.":::

1. On the **Introduction** page of the wizard, select **Next**.
1. On the **Specify Link Options** page, provide a name for your link - if you select multiple databases, then the database name is automatically appended to the end of the name you provide so you don't have to include it yourself. Check the boxes if you want to enable connectivity troubleshooting and, for SQL Server 2022, if you plan to use the link for two-way disaster recovery. Select **Next**. 
1. On the **Requirements** page, the wizard validates requirements to establish a link to your secondary. Select **Next** after all the requirements are validated, or resolve any requirements that aren't met and then select **Re-run Validation**. 
1. On the **Select Databases** page, choose the database you want to replicate to your secondary replica via the link. Selecting multiple databases create multiple distributed availability groups, one for each link. Select **Next**. 
1. On the **Specify Secondary Replica** page, select **Add secondary replica** add your secondary replica. If your initial primary is SQL Server, this opens the **Sign In** to Azure window. If your initial primary is SQL Managed Instance, this opens the **Connect to server** dialog box.   
   
   1. For a SQL Server initial primary, sign in to Azure, choose the subscription, resource group, and secondary SQL Server managed instance from the dropdown. Select **Login** to open the **Connect to Server** dialog box and then connect to the SQL Managed Instance you want to replicate your database to. When you see **Login successful** on the **Sign in** window, select **OK** to close window and go back to the **New Managed Instance link** wizard. 
   1. For a SQL Managed Instance initial primary, connect to the SQL Server instance you want to replicate your database to. 

1. After adding your secondary replica, use the tabs in the wizard to modify **Endpoint** settings if you need to, and review information about backups and the link endpoint in the remaining tabs. Select **Next** when you're ready to proceed. 
1. If SQL Managed Instance is your initial primary, the next page in the wizard is the **Login to Azure** page. Sign in again if you need to, and then select **Next**. This page isn't available when SQL Server is your initial primary. 
1. On the **Validation** page, make sure all validations are successful. If any fail, resolve them, and then rerun the validation. Select **Next** when ready. 
1. On the **Summary** page, review your configuration once more. Optionally, select **Script** to generate a script so you can easily recreate the same link in the future. Select **Finish** when you're ready to create the link. 
1. The **Executing actions** page displays the progress of each action.  
1. After all steps finish, the **Results** page shows check marks next to the successfully completed actions. You can now close the window. 


## View a replicated database

After the link is created, your database is replicated to the secondary replica. Depending on database size and network speed, the database might initially be in a **Restoring** state on the secondary replica. After initial seeding finishes, the database is restored to the secondary replica and ready for read-only workloads. 

On either replica, use **Object Explorer** in SSMS to view the **Synchronized** state of the replicated database. Expand **Always On High Availability** and **Availability Groups** to view the distributed availability group created for each link. 

:::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/check-replicated-database-on-sql-server.png" alt-text="Screenshot that shows the state of the SQL Server database and distributed availability group in S S M S."::: :::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/check-replicated-database-on-sql-managed-instance.png" alt-text="Screenshot that shows the state of the SQL Managed Instance database and distributed availability group.":::

Regardless of which instance is primary, you can also right-click the linked distributed availability group on SQL Server and select **Show Dashboard** to view the dashboard for the distributed availability group, which shows the status of the linked database in the distributed availability group.

## Stop workload

If you're ready to migrate or fail over your database to the secondary replica, first stop any application workloads on the primary replica during your maintenance hours. This enables database replication to catch up on the secondary so you can fail over or migrate to the secondary without data loss. Ensure your applications aren't committing transactions to the primary before failing over. 


## Fail over a database

Use the **Failover between SQL Server and Managed Instance** wizard in SSMS to fail over your database from your primary to your secondary replica. 

You can perform a planned failover from either the primary or the secondary replica. To do a forced failover, connect to the secondary replica.

> [!CAUTION]
> - Before failing over, stop the workload on the source database to allow the replicated database to completely catch up and fail over without data loss. If you're performing a forced failover, you could lose data. 
> - Failing over a database in SQL Server 2019 and earlier versions breaks and removes the link between the two replicas. You can't fail back to the initial primary.
> - Failing over a database while maintaining the link with SQL Server 2022 is currently in preview. 

To fail over your database, follow these steps: 

1. Open SSMS and connect to either replica.  
1. In **Object Explorer**, right-click your replicated database, hover over **Azure SQL Managed Instance link**, and select **Failover...** to open the **Failover between SQL Server and Managed Instance** wizard.  If you have multiple links from the same database, expand **Availability Groups** under **Always On availability groups** in **Object Explorer** and right-click the distributed availability group for the link you want to fail over. Select **Failover...** to open the **Failover between SQL Server and Managed Instance** wizard for that specific link.

   :::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/link-failover-ssms-database-context-failover-database.png" alt-text="Screenshot that shows a database's context menu option for failover.":::

1. On the **Introduction** page, select **Next**.
1. The **Choose failover type** page shows you details about each replica, the role of the database you selected, and the supported failover types. You can initiate failover from any replica. If you choose a forced failover you must check the box to indicate you understand there could be potential data loss. Select **Next**. 
1. On the **Log in to Azure and Remote Instance** page, select **Sign-in** to provide your credentials and sign in to your Azure account. Select **Login** to sign into either your SQL Server or SQL Managed Instance secondary replica, if prompted to do so. 
1. On the **Post-Failover Operations** page, options differ between SQL Server 2022 and earlier versions. 
 
    1. For SQL Server 2022, you can choose to stop replication between replicas and drop the link and distributed availability group after failover completes. If you want to maintain the link and continue replicating data between replicas, leave the box unchecked. If you choose to drop the link, you can also check the box to drop the availability group if you created it solely for the purpose of replicating your database to Azure and you no longer need it. Check the boxes that fit your scenario, and then select **Next**. 
    1. For SQL Server 2019 and earlier versions, the option to **Remove the link** is checked by default, and you can't uncheck it since failing over to SQL Managed Instance stops replication, breaks the link, and drops the distributed availability group. Check the box to indicate you understand the link will be dropped, and then select **Next**. 

1. On the **Summary** page, review the actions. Optionally, select **Script** to generate a script so you can easily fail over the database using the same link in the future. Select **Finish** when you're ready to fail over the database. 
1. After all steps finish, the **Results** page shows check marks next to the successfully completed actions. You can now close the window. 

If you chose to maintain the link for SQL Server 2022, the secondary becomes the new primary, the link is still active and you can fail back to the secondary. 

If you're on SQL Server 2019 and earlier versions, or if you chose to drop the link for SQL Server 2022, the link is dropped and no longer exists after failover completes. The source database and target database on each replica can both execute a read/write workload. They're completely independent. 

> [!IMPORTANT]
> After successful fail over to SQL Managed Instance, manually repoint your application(s) connection string to the SQL managed instance FQDN to complete the migration or fail over process and continue running in Azure.

## View database after failover

For SQL Server 2022, if you chose to maintain the link, you can check the database in **Object Explorer** on either SQL Server or SQL Managed Instance. The status of the database is **Synchronized** and the distributed availability group under **Availability Groups** exists. 

If you dropped the link during failover, you can use **Object Explorer** to confirm the distributed availability group no longer exists, but if you chose to keep the availability group, the database will still be **Synchronized**. 

## Troubleshoot 

The section provides guidance to address issues with configuring and using the link. 

### Errors 

If you encounter an error message when you create the link or fail over a database, select the error to open a window with additional details about the error. 

If you encounter an error when working with the link, the SSMS wizard stops execution at the step that failed, and can't be restarted again. Address the issue, and, if necessary, clean up the environment to revert back to the original state by removing the distributed availability group and availability group if it was created while setting up the link. Then launch the wizard again to start over.

### Inconsistent state after forced failover 

Using forced failover can result in an inconsistent state between the primary and secondary replicas, causing a split brain scenario from both replicas being in the same role. Data replication fails in this state until the user resolves the situation by manually designating one replica as primary and the other replica as secondary. 



## Related content

For more information on the link feature, review the following resources:

- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Prepare your environment for Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
