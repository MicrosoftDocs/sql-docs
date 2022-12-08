---
title: Change target recovery time of a database
description: Learn how to set or change the target recovery time of a SQL Server database in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 08/26/2022
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# Change the target recovery time of a database (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to set or change the target recovery time of a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. By default, the target recovery time is 60 seconds, and the database uses *indirect checkpoints*. The target recovery time establishes an upper-bound on recovery time for this database.

This setting takes effect immediately, and doesn't require a restart of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

> [!NOTE]  
> The upper-bound that is specified for a given database by its target recovery time setting could be exceeded if a long-running transaction causes excessive UNDO times.

## Limitations and restrictions

An online transactional workload on a database that is configured for [indirect checkpoints](database-checkpoints-sql-server.md#IndirectChkpt) could experience performance degradation. Indirect checkpoints make sure that the number of dirty pages are below a certain threshold so that the database recovery completes within the target recovery time. The recovery interval configuration option uses the number of transactions to determine the recovery time as opposed to indirect checkpoints that make use of the number of dirty pages.

When indirect checkpoints are enabled on a database receiving a large number of DML operations, the background writer can start aggressively flushing dirty buffers to disk to ensure that the time required to perform recovery is within the target recovery time set on the database. This can cause extra I/O activity on certain systems, which can contribute to a performance bottleneck if the disk subsystem is operating above or nearing the I/O threshold.

## Permissions

Requires ALTER permission on the database.

## Use SQL Server Management Studio

1. In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and expand that instance.

1. Expand the **Databases** container, then right-click the database you want to change, and select the **Properties** command.

1. In the **Database Properties** dialog box, select the **Options** page.

1. In the **Recovery** panel, in the **Target Recovery Time (Seconds)** field, specify the number of seconds that you want as the upper-bound of the recovery time for this database.

## Use Transact-SQL

1. Connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where the database resides.

1. Use the following [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql-set-options.md) statement as follows:

   TARGET_RECOVERY_TIME = *target_recovery_time* { SECONDS | MINUTES }

   *target_recovery_time*  
   Beginning with [!INCLUDE[ssSQL16_md](../../includes/sssql16-md.md)], the default value is 1 minute. When greater than 0 (the default for older versions), specifies the upper-bound on the recovery time for the specified database in the event of a crash.

   SECONDS  
   Indicates that *target_recovery_time* is expressed as the number of seconds.

   MINUTES  
   Indicates that *target_recovery_time* is expressed as the number of minutes.

   The following example sets the target recovery time of the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to `60` seconds.

   ```sql
   ALTER DATABASE AdventureWorks2019 SET TARGET_RECOVERY_TIME = 60 SECONDS;
   ```

## See also

- [Database Checkpoints &#40;SQL Server&#41;](../../relational-databases/logs/database-checkpoints-sql-server.md)
- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)
