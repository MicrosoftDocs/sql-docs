---
title: Disaster recovery with Managed Instance link
titleSuffix: Azure SQL Managed Instance
description: Learn how to use the Managed Instance link to recover your SQL Server data to Azure SQL Managed Instance in the event of a disaster.
author: danimir
ms.author: danil
ms.reviewer: mathoma
ms.date: 11/14/2023
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---

# Disaster recovery with Managed Instance link - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to configure a hybrid disaster recovery solution between SQL Server hosted anywhere and Azure SQL Managed Instance by using the [Managed Instance link](managed-instance-link-feature-overview.md). 

## Overview

The Managed Instance link enables [disaster recovery](managed-instance-link-disaster-recovery.md), where, in the event of a disaster, you can manually fail over your workload from your primary to your secondary. 

With SQL Server 2016, and SQL Server 2019, the primary is always SQL Server and failover to the secondary managed instance is one-directional. Reversing roles by failing back to SQL Server and making SQL Managed Instance primary isn't supported. However, it's possible to recover your data to SQL Server using data movement options such as [transactional replication](replication-transactional-overview.md) or [exporting a bacpac](../database/database-export.md). 

With SQL Server 2022, either SQL Server or Azure SQL Managed Instance can be the primary and you can establish the link initially from either SQL Server or SQL Managed Instance. You can fail over between SQL Server and Azure SQL Managed Instance in either direction, as needed.

When failing back to SQL Server 2022, you can choose to fail back: 
-  _online_ by using the Managed Instance link directly. This option is currently in a preview. 
-  _offline_ by taking a backup of your database from SQL Managed Instance and [restoring it to your SQL Server 2022 instance](restore-database-to-sql-server.md). This option is generally available.

:::image type="content" source="media/managed-instance-link-feature-overview/disaster-recovery-scenario.png" alt-text="Diagram showing the disaster recovery scenario.":::


> [!IMPORTANT]
> After successful fail over to SQL Managed Instance, manually repoint your application(s) connection string to the SQL managed instance FQDN to complete the fail over process and continue running in Azure.

## Prerequisites 

To use the link with Azure SQL Managed Instance for disaster recovery, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites)) with the required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- A configured [Managed Instance link](managed-instance-link-configure-how-to-ssms.md) between SQL Server and Azure SQL Managed Instance. 

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


## One-way failover (SQL Server 2016 - 2022)

For SQL Server 2016 and SQL Server 2019, failover to Azure SQL Managed Instance from SQL Server is one way. Failing back, or restoring your database to SQL Server isn't possible. However, you can recover your data back to SQL Server by using data movement options such as [transactional replication](replication-transactional-overview.md) or [exporting a bacpac](../database/database-export.md). Failing over to Azure SQL Managed Instance breaks the link and drops the distributed availability group. 

With SQL Server 2022, you can choose to perform a one-way failover, such as for migration, by breaking the link in the process of failover. Be sure to choose the appropriate option for your business when you fail over your SQL Server 2022 database. 

You can manually fail over one-way to Azure SQL Managed Instance by using [SQL Server Management Studio (SSMS)](managed-instance-link-configure-how-to-ssms.md#fail-over-a-database) or [scripts](managed-instance-link-configure-how-to-scripts.md#stop-workload). 

## Online fail back (SQL Server 2022)

SQL Server 2022 introduces online failover with fail back, which allows you to seamlessly failover to Azure SQL Managed Instance and then fail back online to SQL Server by using the Managed Instance link, with minimal down time. 

The option to fail back online to SQL Server from SQL Managed Instance is currently in preview. 

You can manually fail over between replicas by using [SQL Server Management Studio (SSMS)](managed-instance-link-configure-how-to-ssms.md#fail-over-a-database) or [scripts](managed-instance-link-configure-how-to-scripts.md#stop-workload). 

## Offline fail back (SQL Server 2022)

With SQL Server 2022, after the disaster is mitigated, you can choose to fail back to SQL Server from SQL Managed Instance offline by taking a backup of your database on your managed instance, and then restoring it to SQL Server. This option is generally available. 

To get started, review [Restore database to SQL Server 2022](restore-database-to-sql-server.md). 

## License-free passive DR replica 

You can save on licensing costs by activating the [Hybrid failover benefit](business-continuity-high-availability-disaster-recover-hadr-overview.md#license-free-dr-replicas) for your passive secondary SQL managed instance when it's used only for disaster recovery.  The **Hybrid failover benefit** can be activated for new and existing instances. 

> [!NOTE]
> The **Hybrid failover benefit** is only applicable when you configure a secondary instance as a passive _in a hybrid environment between SQL Server and SQL Managed Instance_. For failover benefits between two instances in a _failover group_, use the [failover benefit](failover-group-standby-replica-how-to-configure.md) instead. 


### New instances 

To activate the **Hybrid failover benefit** for a new instance, follow these steps: 

1. Go to the **SQL managed instances** page in the [Azure portal](https://portal.azure.com). 
1. Select **+ Create** to open the **Create Azure SQL Managed Instance** page. 
1. On the **Basics** tab, select **Configure Managed Instance** under **Compute + Storage** to open the **Compute + Storage** page:   

   :::image type="content" source="media/managed-instance-link-disaster-recovery/create-new-managed-instance.png" alt-text="Screenshot of creating a new managed instance in the Azure portal with configure managed instance selected. ":::

1. Choose **Hybrid failover rights** under **SQL Server License**. 
1. Check the box to confirm that you'll use this instance as a passive replica. 
1. Select **Apply** to save your changes. 

### Existing instances

To activate the **Hybrid failover benefit** for an existing instance, follow these steps: 

1. Go to your **SQL managed instance** in the [Azure portal](https://portal.azure.com). 
1. Select **Compute + storage** under **Settings** in the resource menu. 
1. Choose **Hybrid failover rights** under **SQL Server License** and then check the box to confirm that you'll use this instance as a passive replica: 

   :::image type="content" source="media/managed-instance-link-disaster-recovery/update-license-existing-instance.png" alt-text="Screenshot of the compute and storage page for your managed instance in the Azure portal with hybrid failover rights highlighted. ":::

1. Select **Apply** to save your changes. 

## Related content

For more information on the link feature, see the following resources:

- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Prepare your environment for a Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
