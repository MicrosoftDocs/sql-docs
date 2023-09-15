---
title: "Availability group wizard: Specify Availability Group Options"
description: "Describes the options found on the 'Specify Availability Group Name' page of the Availability Group Wizard within SQL Server Management Studio."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/01/2023
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.newagwizard.specifyagname.f1"
  - "sql13.swb.adddatabasewizard.specifyagname.f1"
---
# Specify Availability Group Options page for an Always On availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article describes the options of the **Specify Availability Group Options** page. This article is used by both the **[!INCLUDE [ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)]** and **[!INCLUDE [ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)]** of [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)].

## <a id="PageOptions"></a> Specify Availability Group Options

#### Availability group name

Specify the name of the availability group. For a new availability group, specify a valid [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] identifier that is unique across all availability groups in the Windows Server failover cluster (WSFC). The maximum length for an availability group name is 128 characters.

#### Cluster type dropdown list

Next, specify the cluster type. The possible cluster types depend on the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] version and operating system. Choose one from the following list:

- **Windows Server Failover Clustering**

  Use when the availability group is hosted on instances of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that belong to a Windows Server failover cluster for high availability and disaster recovery. Applies to all supported versions of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

- **EXTERNAL**

  Use when the availability group is hosted on an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that is managed by an external cluster technology for high availability and disaster recovery, for example Pacemaker on Linux. Applies to [!INCLUDE [sssql14](../../../includes/sssql17-md.md)] and later.

  > [!IMPORTANT]  
  > Don't choose `cluster type = EXTERNAL` on a Windows platform. Doing so results in the availability group going into a resolving state, and will prevent you from removing databases from the availability group.

- **NONE**

  Use when the availability group is hosted on an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that isn't managed by a cluster technology for read scale and load balancing. Applies to [!INCLUDE [sssql14](../../../includes/sssql17-md.md)] and later.

#### Database Level Health Detection checkbox

Check this box to enable database level health detection (DB_FAILOVER) option for the availability group. The database health detection notices when a database is no longer in the online status, when something goes wrong, and triggers the automatic failover of the availability group. See [SQL Server Always On Database Health Detection Failover Option](sql-server-always-on-database-health-detection-failover-option.md).

#### Per Database DTC Support checkbox

Check this box to enable distributed transactions for the databases in the availability group. In order to guarantee distributed transactions, the availability group must be configured to register databases as distributed transaction resource managers. See [Configure distributed transactions for an Always On availability group](configure-availability-group-for-distributed-transactions.md)

#### Contained checkbox

Check this box to create a [contained availability group](contained-availability-groups-overview.md). A contained availability group supports managing metadata objects (users, logins, permissions, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Agent jobs, etc.) at the availability group level in addition to the instance level.

#### Reuse System Databases checkbox

When using a contained availability group, check this box if you want to reuse existing system databases from a previously created contained availability group of the same name.

Select **Databases** page (**New Availability Group Wizard** and **Add Database Wizard**).

## Next steps

- [Use the New Availability Group Dialog Box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)
- [Add a database to an Always On availability group with the 'Availability Group Wizard'](availability-group-add-database-to-group-wizard.md)
- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
