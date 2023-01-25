---
title: "Plan and Test the Database Engine Upgrade Plan"
description: This article describes planning before you begin your SQL Server upgrade, including a planning checklist and developing and testing an upgrade plan.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# Plan and test the database engine upgrade plan

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

To perform a successful [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] upgrade, regardless of approach, appropriate planning is required.

## Release notes and known upgrade issues

Before upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], review:

- [SQL Server 2019 Release Notes](../../sql-server/sql-server-2019-release-notes.md)
- [SQL Server 2017 Release Notes](../../sql-server/sql-server-2017-release-notes.md)
- [SQL Server 2016 Release Notes](../../sql-server/sql-server-2016-release-notes.md)
- [SQL Server Database Engine Backward Compatibility](../discontinued-database-engine-functionality-in-sql-server.md) article.

## Pre-upgrade planning checklist

Before upgrading the [!INCLUDE[ssDE](../../includes/ssde-md.md)], review the following checklist and the associated articles. These articles apply to all upgrades, regardless of upgrade method and help you determine the most appropriate upgrade method: Rolling upgrade, new installation upgrade, or in-place upgrade. For example, you may not be able to perform an upgrade in-place or a rolling upgrade if you are upgrading the operating system, upgrading from SQL Server 2005, or upgrading from a 32-bit version of SQL Server. For a decision tree, see [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md).

- **Hardware and software requirements:** Review the hardware and software requirements to for installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These requirements can be found at: [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md). A part of any upgrade planning cycle is to consider upgrading hardware (newer hardware is faster and may reduce licensing either due to fewer processors or due to database and server consolidation) and upgrading the operating system. These types of hardware and software changes affect the type of upgrade method you choose.

- **Current environment:** Research your current environment to understand the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components that are being used and the clients that connect to your environment.

  - **Client providers:** While upgrading does not require you to update the provider for each of your clients, you may choose to do so. If you are upgrading from [!INCLUDE[sql14](../../includes/sssql14-md.md)] or older, the following [!INCLUDE[sql15](../../includes/sssql16-md.md)] features either require an updated provider for each client or an updated provider to provide additional functionality:

  - [Always Encrypted &#40;Database Engine&#41;](../../relational-databases/security/encryption/always-encrypted-database-engine.md)

  - [Stretch Database](../../sql-server/stretch-database/stretch-database.md)

  > [!IMPORTANT]  
  > Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

  - [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)

  - TLS Security update

- **Third-party components:** Determine the compatibility of third-party components, such as integrated backup.

- **Target environment:** Verify that your target environment meets the hardware and software requirements and that it can support the original system's requirements. For example, your upgrade may involve the consolidation of multiple SQL Server instances to a single, new [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] instance, or the virtualization of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environment to a private or public cloud.

- **Edition:** Determine the appropriate edition of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] for your upgrade and determine the valid upgrade paths for the upgrade. For detailed information, see [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md). Before you upgrade from one edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another, verify that the functionality that you are currently using is supported in the edition to which you are upgrading.

  > [!NOTE]  
  > When you upgrade [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] from a prior version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise edition, choose between Enterprise Edition: Core-based Licensing and Enterprise Edition. These Enterprise editions differ only with respect to the licensing modes. For more information, see [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).

- **Backward compatibility:** Review the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] database engine  backward compatibility article to review changes in behavior between [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version from which you are upgrading. See [SQL Server Database Engine Backward Compatibility](../discontinued-database-engine-functionality-in-sql-server.md).

- **Data Migration Assistant:** Run the Data Migration Assistant to assist in diagnosing issues that might either block the upgrade process or require modification to existing scripts or applications due to a breaking change.

  You can download the Data Migration Assistant [here](https://aka.ms/get-dma).

- **System configuration checker:** Run the [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] System Configuration Checker (SCC) to determine if the SQL Server setup program detects any blocking issues before you schedule the upgrade. For more information, see [Check Parameters for the System Configuration Checker](../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md).

- **Upgrading memory-optimized tables:** When upgrading a SQL Server 2014 database instance containing memory-optimized tables to SQL Server 2016, the upgrade process requires additional time to convert the memory-optimized tables to the new on-disk format (and the database is offline while these steps are happening.   The amount of time is dependent upon the size of the memory-optimized tables and the speed of the I/O subsystem. The upgrade requires three sizes of data operations for in-place and new installation upgrades (step 1 is not required for rolling upgrades, but steps 2 and 3 are required):

  1. Run database recovery using the old on-disk format (including loading all data in memory-optimized tables into memory from disk)

  1. Serialize the data to disk in the new on-disk format

  1. Run database recovery using the new format (including loading all data in memory-optimized tables into memory from disk)

     Additionally, insufficient space on disk during this process causes recovery to fail. Ensure there is sufficient space on disk to store the existing database plus additional storage equal to the current size of the containers in the MEMORY_OPTIMIZED_DATA filegroup in the database to perform an in-place upgrade or when attaching a SQL Server 2014 database to a SQL Server 2016 instance. Use the following query to determine the disk space currently required for the MEMORY_OPTIMIZED_DATA filegroup, and consequently also the amount of free disk space required for upgrade to succeed:

    ```sql
    SELECT CAST(SUM(size) AS FLOAT) * 8 / 1024 / 1024 AS [size in GB]
    FROM sys.database_files
    WHERE data_space_id IN
    (
        SELECT data_space_id
        FROM sys.filegroups
        WHERE type = N'FX'
    );
    ```

## Develop and test the upgrade plan

The best approach is to treat your upgrade like you would any IT project. Organize an upgrade team that has the database administration, network, extraction, transformation, and loading (ETL), and other skills required for the upgrade. The team needs to:

- **Choose the upgrade method:** See [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md).

- **Develop a rollback plan:** Executing this plan enables you to restore your original environment if you need to roll back.

- **Determine acceptance criteria:** Verify that the upgrade is successful before you cut over users to the upgraded environment.

- **Test the upgrade plan:** To test performance using your actual workload, use the Microsoft SQL Server Distributed Replay Utility. This utility can use multiple computers to replay trace data, simulating a mission-critical workload. By performing a replay on a test server before and after a SQL Server upgrade, you can measure performance differences and look for any incompatibilities your application may have with the upgrade. For more information, see [SQL Server Distributed Replay](../../tools/distributed-replay/sql-server-distributed-replay.md) and [Administration Tool Command-line Options &#40;Distributed Replay Utility&#41;](../../tools/distributed-replay/administration-tool-command-line-options-distributed-replay-utility.md).

## Next steps

- [Upgrade Database Engine](../../database-engine/install-windows/upgrade-database-engine.md)
- [Database Migration Guide](/data-migration/)
