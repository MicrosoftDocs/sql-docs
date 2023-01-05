---
title: "Track Data Changes"
description: "Enable applications to determine the DML changes (insert, update, and delete operations) that were made to user tables in a database, using track changes and change data capture."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 09/20/2022
ms.service: sql
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "change data capture [SQL Server], compared to change tracking"
  - "change data capture vs. change tracking"
  - "change data capture [SQL Server], data type behaviors"
  - "Sync Services for ADO.NET"
  - "change tracking [SQL Server], Sync Services for ADO.NET"
  - "change tracking [SQL Server]"
  - "change data capture [SQL Server], security"
  - "change data capture [SQL Server], other SQL Server features and"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Track data changes (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] provides two features that track changes to data in a database: [change data capture](#Capture) and [change tracking](#Tracking). These features enable applications to determine the DML changes (insert, update, and delete operations) that were made to user tables in a database. Change data capture and change tracking can be enabled on the same database; no special considerations are required. For the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that support change data capture and change tracking, see [Editions and supported features of SQL Server](../../sql-server/editions-and-components-of-sql-server-2019.md).

Change tracking is supported by [!INCLUDE[ssazure_md](../../includes/ssazure_md.md)]. Change data capture is only supported in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Azure SQL Managed Instance.

## Benefits of using change data capture or change tracking

 The ability to query for data that has changed in a database is an important requirement for some applications to be efficient. Typically, to determine data changes, application developers must implement a custom tracking method in their applications by using a combination of triggers, **timestamp** columns, and additional tables. Creating these applications usually involves a lot of work to implement, leads to schema updates, and often carries a high performance overhead.

 Using change data capture or change tracking in applications to track changes in a database, instead of developing a custom solution, has the following benefits:

- There is reduced development time. Because functionality is available in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you don't have to develop a custom solution.

- Schema changes aren't required. You don't have to add columns, add triggers, or create side table in which to track deleted rows or to store change tracking information if columns can't be added to the user tables.

- There is a built-in cleanup mechanism. Cleanup for change tracking is performed automatically in the background. Custom cleanup for data that is stored in a side table isn't required.

- Functions are provided to obtain change information.

- There is low overhead to DML operations. Synchronous change tracking will always have some overhead. However, using change tracking can help minimize the overhead. The overhead will frequently be less than that of using alternative solutions, especially solutions that require the use of triggers.

- Change tracking is based on committed transactions. The order of the changes is based on transaction commit time. This allows for reliable results to be obtained when there are long-running and overlapping transactions. Custom solutions that use **timestamp** values must be designed to handle these scenarios.

- Standard tools are available that you can use to configure and manage. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] provides standard DDL statements, [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], catalog views, and security permissions.

## Feature differences between change data capture and change tracking

The following table lists the feature differences between change data capture and change tracking. The tracking mechanism in change data capture involves an asynchronous capture of changes from the transaction log so that changes are available after the DML operation. In change tracking, the tracking mechanism involves synchronous tracking of changes in line with DML operations so that change information is available immediately.

|Feature|Change data capture|Change tracking|
|-------------|-------------------------|---------------------|
|**Tracked changes**|||
|DML changes|Yes|Yes|
|**Tracked information**|||
|Historical data|Yes|No|
|Whether column was changed|Yes|Yes|
|DML type|Yes|Yes|

## <a id="Capture"></a> Change data capture

Change data capture provides historical change information for a user table by capturing both the fact that DML changes were made and the actual data that was changed. Changes are captured by using an asynchronous process that reads the transaction log and has a low impact on the system.

As shown in the following illustration, the changes that were made to user tables are captured in corresponding change tables. These change tables provide a historical view of the changes over time. The [change data capture](../../relational-databases/system-functions/change-data-capture-functions-transact-sql.md) functions that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides enable the change data to be consumed easily and systematically.


:::image type="content" source="media/track-data-changes-sql-server/concept-change-data-capture.gif" alt-text="Diagram showing the concept of change data capture.":::

### Security model

This section describes the change data capture security model.

#### Configuration and administration

To either enable or disable change data capture for a database, the caller of [sys.sp_cdc_enable_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-db-transact-sql.md) or [sys.sp_cdc_disable_db (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql.md) must be a member of the fixed server **sysadmin** role. Enabling and disabling change data capture at the table level requires the caller of [sys.sp_cdc_enable_table (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql.md) and [sys.sp_cdc_disable_table (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-table-transact-sql.md) to either be a member of the sysadmin role or a member of the database **database db_owner** role.

Use of the stored procedures to support the administration of change data capture jobs is restricted to members of the server **sysadmin** role and members of the **database db_owner** role.

#### Change enumeration and metadata queries

To gain access to the change data that is associated with a capture instance, the user must be granted SELECT access to all the captured columns of the associated source table. In addition, if a gating role is specified when the capture instance is created, the caller must also be a member of the specified gating role, and the change data capture schema (`cdc`) must have SELECT access to the gating role.

Other general change data capture functions for accessing metadata will be accessible to all database users through the public role, although access to the returned metadata will also typically be gated by using SELECT access to the underlying source tables, and by membership in any defined gating roles.

#### DDL operations to change data capture-enabled source tables

When a table is enabled for change data capture, DDL operations can only be applied to the table by a member of the fixed server role **sysadmin**, a member of the **database role db_owner**, or a member of the **database role db_ddladmin**. Users who have explicit grants to perform DDL operations on the table will receive error 22914 if they try these operations.

### Data type considerations for change data capture

All base column types are supported by change data capture. The following table lists the behavior and limitations for several column types.

|Type of Column|Changes Captured in Change Tables|Limitations|
|--------------------|---------------------------------------|-----------------|
|Sparse columns|Yes|Doesn't support capturing changes when using a columnset.|
|Computed columns|No|Changes to computed columns aren't tracked. The column will appear in the change table with the appropriate type, but will have a value of NULL.|
|XML|Yes|Changes to individual XML elements aren't tracked.|
|Timestamp|Yes|The data type in the change table is converted to binary.|
|BLOB data types|Yes|The previous image of the BLOB column is stored only if the column itself is changed.|

### Change data capture and other SQL Server features

This section describes how the following features interact with change data capture:

- Database mirroring
- Transactional replication
- Database restore or attach

#### Database mirroring

A database that is enabled for change data capture can be mirrored. To ensure that capture and cleanup happen automatically on the mirror, follow these steps:

1. Ensure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is running on the mirror.

1. Create the capture job and cleanup job on the mirror after the principal has failed over to the mirror. To create the jobs, use the stored procedure [sys.sp_cdc_add_job (Transact-SQL)](../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md).

For more information about database mirroring, see [Database Mirroring (SQL Server)](../../database-engine/database-mirroring/database-mirroring-sql-server.md).

#### Transactional replication

Change data capture and transactional replication can coexist in the same database, but population of the change tables is handled differently when both features are enabled. Change data capture and transactional replication always use the same procedure, [sp_replcmds](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md), to read changes from the transaction log. When change data capture is enabled on its own, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job calls `sp_replcmds`. When both features are enabled on the same database, the Log Reader Agent calls `sp_replcmds`. This agent populates both the change tables and the `distribution` database tables. For more information, see [Replication Log Reader Agent](../../relational-databases/replication/agents/replication-log-reader-agent.md).

Consider a scenario in which change data capture is enabled on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, and two tables are enabled for capture. To populate the change tables, the capture job calls `sp_replcmds`. The database is enabled for transactional replication, and a publication is created. Now, the Log Reader Agent is created for the database and the capture job is deleted. The Log Reader Agent continues to scan the log from the last log sequence number that was committed to the change table. This ensures data consistency in the change tables. If transactional replication is disabled in this database, the Log Reader Agent is removed, and the capture job is re-created.

> [!NOTE]  
> When the Log Reader Agent is used for both change data capture and transactional replication, replicated changes are first written to the `distribution` database. Then, captured changes are written to the change tables. Both operations are committed together. If there is any latency in writing to the `distribution` database, there will be a corresponding latency before changes appear in the change tables.

#### Restore or attach a database enabled for change data capture

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the following logic to determine if change data capture remains enabled after a database is restored or attached:

- If a database is restored to the same server with the same database name, change data capture remains enabled.

- If a database is restored to another server, by default change data capture is disabled, and all related metadata is deleted.

  To retain change data capture, use the `KEEP_CDC` option when restoring the database. For more information about this option, see [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md).

- If a database is detached and attached to the same server or another server, change data capture remains enabled.

- If a database is attached or restored with the `KEEP_CDC` option to any edition other than Standard or Enterprise, the operation is blocked because change data capture requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard or Enterprise editions. Error message 932 is displayed:

  ```output
  SQL Server cannot load database '%.*ls' because change data capture is enabled. The currently installed edition of SQL Server does not support change data capture. Either disable change data capture in the database by using a supported edition of SQL Server, or upgrade the instance to one that supports change data capture.
  ```

You can use [sys.sp_cdc_disable_db](../../relational-databases/system-stored-procedures/sys-sp-cdc-disable-db-transact-sql.md) to remove change data capture from a restored or attached database.

## <a id="Tracking"></a> Change tracking

 Change tracking captures the fact that rows in a table were changed, but doesn't capture the data that was changed. This enables applications to determine the rows that have changed with the latest row data being obtained directly from the user tables. Therefore, change tracking is more limited in the historical questions it can answer compared to change data capture. However, for those applications that don't require the historical information, there is far less storage overhead because of the changed data not being captured. A synchronous tracking mechanism is used to track the changes. This has been designed to have minimal overhead to the DML operations.

 The following illustration shows a synchronization scenario that would benefit by using change tracking. In the scenario, an application requires the following information: all the rows in the table that were changed since the last time that the table was synchronized, and only the current row data. Because a synchronous mechanism is used to track the changes, an application can perform two-way synchronization and reliably detect any conflicts that might have occurred.

 :::image type="content" source="media/track-data-changes-sql-server/concept-change-tracking.gif" alt-text="Diagram showing the concept of change tracking.":::

### Change tracking and Sync Services for ADO.NET

[!INCLUDE[sql_sync_long](../../includes/sql-sync-long-md.md)] enables synchronization between databases, providing an intuitive and flexible API that enables you to build applications that target offline and collaboration scenarios. [!INCLUDE[sql_sync_long](../../includes/sql-sync-long-md.md)] provides an API to synchronize changes, but it doesn't actually track changes in the server or peer database. You can create a custom change tracking system, but this typically introduces significant complexity and performance overhead. To track changes in a server or peer database, we recommend that you use change tracking in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] because it is easy to configure and provides high performance tracking.

For more information about change tracking and [!INCLUDE[sql_sync_long](../../includes/sql-sync-long-md.md)], use the following links:

- [About Change Tracking (SQL Server)](../../relational-databases/track-changes/about-change-tracking-sql-server.md)

  Describes change tracking, provides a high-level overview of how change tracking works, and describes how change tracking interacts with other [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] features.

- [Microsoft Sync Framework Developer Center](/previous-versions/sql/synchronization/mt490616(v=msdn.10))

  Provides complete documentation for [!INCLUDE[ssSyncFrameLong](../../includes/sssyncframelong-md.md)] and [!INCLUDE[sql_sync_short](../../includes/sql-sync-short-md.md)]. In the documentation for [!INCLUDE[sql_sync_short](../../includes/sql-sync-short-md.md)], the topic "How to: Use SQL Server Change Tracking" contains detailed information and code examples.

## Next steps

|Task|Article|
|-|-|
|Provides an overview of change data capture.|[About Change Data Capture (SQL Server)](../../relational-databases/track-changes/about-change-data-capture-sql-server.md)|
|Describes how to enable and disable change data capture on a database or table.|[Enable and Disable Change Data Capture (SQL Server)](../../relational-databases/track-changes/enable-and-disable-change-data-capture-sql-server.md)|
|Describes how to administer and monitor change data capture.|[Administer and Monitor Change Data Capture (SQL Server)](../../relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server.md)|
|Describes how to work with the change data that is available to change data capture consumers. This topic covers validating LSN boundaries, the query functions, and query function scenarios.|[Work with Change Data (SQL Server)](../../relational-databases/track-changes/work-with-change-data-sql-server.md)|
|Provides an overview of change tracking.|[About Change Tracking (SQL Server)](../../relational-databases/track-changes/about-change-tracking-sql-server.md)|
|Describes how to enable and disable change tracking on a database or table.|[Enable and Disable Change Tracking (SQL Server)](../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)|
|Describes how to manage change tracking, configure security, and determine the effects on storage and performance when change tracking is used.|[Manage Change Tracking (SQL Server)](../../relational-databases/track-changes/manage-change-tracking-sql-server.md)|
|Describes how applications that use change tracking can obtain tracked changes, apply these changes to another data store, and update the source database. This topic also describes the role change tracking plays when a failover occurs and a database must be restored from a backup.|[Work with Change Tracking (SQL Server)](../../relational-databases/track-changes/work-with-change-tracking-sql-server.md)|

## See also

- [Change Data Capture Functions (Transact-SQL)](../../relational-databases/system-functions/change-data-capture-functions-transact-sql.md)
- [Change Tracking Functions (Transact-SQL)](../../relational-databases/system-functions/change-tracking-functions-transact-sql.md)
- [Change Data Capture Stored Procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/change-data-capture-stored-procedures-transact-sql.md)
- [Change Data Capture Tables (Transact-SQL)](../../relational-databases/system-tables/change-data-capture-tables-transact-sql.md)
- [Change Data Capture Related Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/system-dynamic-management-views.md)
