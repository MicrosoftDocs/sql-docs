---
title: "Create a database snapshot (Transact-SQL)"
description: "Find out how to create a SQL Server database snapshot by using Transact-SQL. Learn about prerequisites and best practices for creating snapshots."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/21/2023
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "database snapshots [SQL Server], creating"
---
# Create a database snapshot (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The only way to create a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database snapshot is to use [!INCLUDE[tsql](../../includes/tsql-md.md)]. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] doesn't support the creation of database snapshots.

## Prerequisites

The source database, which can use any recovery model, must meet the following prerequisites:

- The server instance must be running an edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that supports database snapshot. For information about support for database snapshots in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).

- The source database must be online, unless the database is a mirror database within a database mirroring session.

- To create a database snapshot on a mirror database, the database must be in the synchronized [mirroring state](../../database-engine/database-mirroring/mirroring-states-sql-server.md).

- The source database can't be configured as a scalable shared database.

- Prior to SQL Server 2019, the source database could not contain a MEMORY_OPTIMIZED_DATA filegroup. Support for in-memory database snapshots was added in SQL Server 2019.

> [!IMPORTANT]  
> For information about other significant considerations, see [Database Snapshots (SQL Server)](database-snapshots-sql-server.md).

## Recommendations

This section discusses the following best practices:

- [Best practice: naming database snapshots](#naming)
- [Best practice: limiting the number of database snapshots](#limiting-number)
- [Best practice: client connections to a database snapshot](#client-connections)

#### <a id="naming"></a> Best practice: naming database snapshots

Before creating snapshots, it is important to consider how to name them. Each database snapshot requires a unique database name. For administrative ease, the name of a snapshot can incorporate information that identifies the database, such as:

- The name of the source database.

- An indication that the new name is for a snapshot.

- The creation date and time of the snapshot, a sequence number, or some other information, such as time of day, to distinguish sequential snapshots on a given database.

For example, consider a series of snapshots for the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. Three daily snapshots are created at 6-hour intervals between 6 A.M. and 6 P.M., based on a 24-hour clock. Each daily snapshot is kept for 24 hours before being dropped and replaced by a new snapshot of the same name. Each snapshot name indicates the hour, but not the day:

```output
AdventureWorks_snapshot_0600
AdventureWorks_snapshot_1200
AdventureWorks_snapshot_1800
```

Alternatively, if the creation time of these daily snapshots varies from day to day, a less precise naming convention might be preferable, for example:

```output
AdventureWorks_snapshot_morning
AdventureWorks_snapshot_noon
AdventureWorks_snapshot_evening
```

#### <a id="limiting-number"></a> Best practice: limiting the number of database snapshots

Creating a series of snapshots over time captures sequential snapshots of the source database. Each snapshot persists until it is explicitly dropped. Because each snapshot will continue to grow as original pages are updated, you may want to conserve disk space by deleting an older snapshot after creating a new snapshot.

> [!NOTE]  
> To revert to a database snapshot, you need to delete any other snapshots from that database.

#### <a id="client-connections"></a> Best practice: client connections to a database snapshot

To use a database snapshot, clients need to know where to find it. Users can read from one database snapshot while another is being created or deleted. However, when you substitute a new snapshot for an existing one, you need to redirect clients to the new snapshot. Users can manually connect to a database snapshot with [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or Azure Data Studio. However, to support a production environment, you should create a programmatic solution that transparently directs report-writing clients to the latest database snapshot of the database.

## Permissions

Any user who can create a database can create a database snapshot; however, to create a snapshot of a mirror database, you must be a member of the **sysadmin** fixed server role.

## Create a database snapshot using Transact-SQL

1. Based on the current size of the source database, ensure that you have sufficient disk space to hold the database snapshot. The maximum size of a database snapshot is the size of the source database at snapshot creation. For more information, see [View the Size of the Sparse File of a Database Snapshot (Transact-SQL)](view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md).

1. Issue a CREATE DATABASE statement on the files using the `AS SNAPSHOT OF`` clause. Creating a snapshot requires specifying the logical name of every database file of the source database. The syntax is as follows:

   ```syntaxsql
   CREATE DATABASE database_snapshot_name
   ON
   (
       NAME = logical_file_name
       , FILENAME = 'os_file_name'
   ) [ , ...n ]
  
   AS SNAPSHOT OF source_database_name
   [;]
   ```

   The arguments are as follows:

   | Argument | Description |
   | --- | --- |
   | *database_snapshot_name* | The name of the snapshot to which you want to revert the database. |
   | *logical_file_name* | The logical name of the source database used in SQL Server when referencing the file. |
   | '*os_file_name*' | The path and file name used by the operating system when you create the file. |
   | *source_database_name* | The source database. |

   For a full description of this syntax, see [CREATE DATABASE (SQL Server Transact-SQL)](../../t-sql/statements/create-database-transact-sql.md#as-snapshot-of-source_database_name).

   > [!NOTE]  
   > When you create a database snapshot, log files, offline files, restoring files, and defunct files are not allowed in the `CREATE DATABASE` statement.

## Examples

The `.ss` extension used in these examples is for convenience, and is not required.

#### A. Create a snapshot on the AdventureWorks database

This example creates a database snapshot on the `AdventureWorks` database. The snapshot name, `AdventureWorks_dbss_1800`, and the file name of its sparse file, `AdventureWorks_data_1800.ss`, indicate the creation time of 6 P.M. (1800 hours).

```sql
CREATE DATABASE AdventureWorks_dbss1800 ON (
    NAME = AdventureWorks,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Data\AdventureWorks_data_1800.ss'
    ) AS SNAPSHOT OF AdventureWorks;
GO
```

#### B. Create a snapshot on the Sales database

This example creates a database snapshot, `sales_snapshot1200`, on the `Sales` database, which is the same example database from [Create a database that has filegroups](../../t-sql/statements/create-database-transact-sql.md#d-create-a-database-that-has-filegroups) in [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md).

```sql
--Create sales_snapshot1200 as snapshot of the Sales database:
CREATE DATABASE sales_snapshot1200 ON (
    NAME = SPri1_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\data\SPri1dat_1200.ss'
    ),
    (
    NAME = SPri2_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\data\SPri2dt_1200.ss'
    ),
    (
    NAME = SGrp1Fi1_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\mssql\data\SG1Fi1dt_1200.ss'
    ),
    (
    NAME = SGrp1Fi2_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\data\SG1Fi2dt_1200.ss'
    ),
    (
    NAME = SGrp2Fi1_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\data\SG2Fi1dt_1200.ss'
    ),
    (
    NAME = SGrp2Fi2_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\data\SG2Fi2dt_1200.ss'
    ) AS SNAPSHOT OF Sales;
GO
```

## See also

- [CREATE DATABASE (Transact-SQL)](../../t-sql/statements/create-database-transact-sql.md)
- [Database Snapshots (SQL Server)](database-snapshots-sql-server.md)

## Next steps

- [View a Database Snapshot (SQL Server)](view-a-database-snapshot-sql-server.md)
- [Revert a Database to a Database Snapshot](revert-a-database-to-a-database-snapshot.md)
- [Drop a Database Snapshot (Transact-SQL)](drop-a-database-snapshot-transact-sql.md)
