---
title: "Change data capture (CDC) with Azure SQL Database"
description: "Learn about change data capture (CDC) in Azure SQL Database, which records insert, update, and delete activity that applies to a table."
author: croblesm
ms.author: roblescarlos
ms.reviewer: mathoma, randolphwest
ms.date: 01/04/2024
ms.service: sql-database
ms.subservice: replication
ms.topic: conceptual
ms.custom:
  - intro-overview
helpviewer_keywords:
  - "change data capture"
  - "change data capture for Azure SQL Database"
---

# Change data capture (CDC) with Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](change-data-capture-overview.md)
> - [SQL Server](/sql/relational-databases/track-changes/about-change-data-capture-sql-server)

In this article, learn how change data capture (CDC) is implemented in Azure SQL Database to record activity on a database when tables and rows have been modified. For details about the CDC feature, including how it's implemented in SQL Server and Azure SQL Managed Instance, see [CDC with SQL Server](/sql/relational-databases/track-changes/about-change-data-capture-sql-server).

## Overview

In Azure SQL Database, a change data capture scheduler replaces the SQL Server Agent jobs that capture and cleanup change data for the source tables. The scheduler runs the capture and cleanup processes automatically in the scope of the database, ensuring reliability and performance without external dependencies. Users retain the option to manually initiate capture and cleanup processes as needed.

A good example of a data consumer used by this technology is an extraction, transformation, and loading (ETL) application. An ETL application incrementally loads change data from SQL Server source tables to a data warehouse or data mart. Although the representation of the source tables within the data warehouse must reflect changes in the source tables, an end-to-end technology that refreshes a replica of the source isn't appropriate. Instead, you need a reliable stream of change data that is structured so that consumers can apply it to dissimilar target representations of the data. SQL Server change data capture provides this technology.

To learn more about change data capture in Azure SQL Database, refer to this Data Exposed episode:
> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Track-and-Record-Data-Changes-with-Change-Data-Capture-CDC-in-Azure-SQL/player?WT.mc_id=dataexposed-c9-niner]

## Data flow

The following illustration shows the principal data flow for change data capture with Azure SQL Database:

:::image type="content" source="media/change-data-capture-overview/change-data-capture-sql-db.png" alt-text="Diagram of a flow chart that depicts data flow for change data capture.":::

## Prerequisites

### Permissions

The `db_owner` role is required to enable change data capture for Azure SQL Database.

### Azure SQL Database compute requirements

You can enable CDC on Azure SQL Database for any service tier within the vCore-based purchasing model, for both [single databases](resource-limits-vcore-single-databases.md) and [elastic pools](resource-limits-vcore-elastic-pools.md).

For databases in the [DTU purchasing model](resource-limits-dtu-single-databases.md), CDC is supported for databases in the **S3** tier or higher. Subcore tiers (Basic, S0, S1, S2) aren't supported for CDC.

## Enable CDC for Azure SQL Database

Before you can create a capture instance for individual tables, you must enable CDC for your Azure SQL Database.

To enable CDC, connect to your Azure SQL Database through Azure Data Studio or SQL Server Management Studio (SSMS). Open a new query window, then enable CDC by running the following T-SQL:

```sql
EXEC sys.sp_cdc_enable_db;
GO
```

> [!NOTE]  
> To determine if a database is already enabled, query the `is_cdc_enabled` column in the `sys.databases` catalog view.

When change data capture is enabled for a database, the `cdc schema`, `cdc user`, metadata tables, and other system objects are created for the database. The `cdc schema` contains the change data capture metadata tables and, after cdc is enabled for the source tables, the individual change tables serve as a repository for change data. The `cdc schema` also contains associated system functions used to query for change data.

> [!IMPORTANT]  
> Change data capture requires exclusive use of the `cdc schema` and `cdc user`. If either a schema or a database user named `cdc` currently exists in a database, you can't enable cdc for the database until the schema and/or user is dropped or renamed.

## Enable CDC for a table

After enabling CDC for your Azure SQL Database, you can then enable CDC at the table level by selecting one or more tables to track data changes. Create a capture instance for individual source tables by using the stored procedure [sys.sp_cdc_enable_table](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql).

To enable CDC for a table, run the following T-SQL:

```sql
EXEC sys.sp_cdc_enable_table
    @source_schema = N'SchemaName',
    @source_name = N'TableName',
    @role_name = NULL;
GO
```

> [!TIP]  
> The previous example doesn't use an explicit *@role_name* by setting the parameter to `NULL`, but you can use a gating role to limit access to the change data.

### Columns in the source table to be captured

By default, all of the columns in the source table are identified as captured columns. If only a subset of columns needs to be tracked, such as for privacy or performance reasons, use the *@captured_column_list* parameter to specify the subset of columns.

To enable CDC for a specific list of columns in a table, run the following T-SQL:

```sql
EXEC sys.sp_cdc_enable_table
    @source_schema = N'SchemaName',
    @source_name = N'TableName',
    @role_name = NULL,
    @captured_column_list = N'Column1, Column2, Column3';
GO
```

> [!TIP]  
> Notice the previous example doesn't use an explicit *@role_name* and by setting the parameter to `NULL`, but you can use a gating role to limit access to the change data.

### A role to control access to a change table

The purpose of the named role is to control access to the change data. The specified role can be an existing fixed server role or a database role. If the specified role doesn't already exist, a database role of that name is created automatically. Users must have SELECT permission on all the captured columns of the source table. In addition, when a role is specified, users who aren't members of either the **sysadmin** or **db_owner** role must also be members of the specified role.

To enable CDC for table specifying a gating role, run the following T-SQL:

```sql
EXEC sys.sp_cdc_enable_table
    @source_schema = N'SchemaName',
    @source_name = N'TableName',
    @role_name = N'RoleName'
GO
```

If you don't want to use a gating role, explicitly set the *@role_name* parameter to `NULL`.

### A function to query for net changes

A capture instance always includes a table valued function to return all change table entries that occurred within a defined interval. This function is named by appending the capture instance name to `cdc.fn_cdc_get_all_changes_`. For more information, see [cdc.fn_cdc_get_all_changes](/sql/relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql).

If the parameter *@supports_net_changes* is set to 1, a net changes function is also generated for the capture instance. This function returns only one change for each distinct row changed in the interval specified in the call. For more information, see [cdc.fn_cdc_get_net_changes](/sql/relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql).

To support net changes queries, the source table must have a primary key or unique index to uniquely identify rows. If a unique index is used, the name of the index must be specified using the *@index_name* parameter. The columns defined in the primary key or unique index must be included in the list of source columns to be captured.

To enable CDC for a table with support for net changes, run the following T-SQL:

```sql
EXEC sys.sp_cdc_enable_table
    @source_schema = N'SchemaName',
    @source_name = N'TableName',
    @role_name = NULL,
    @supports_net_changes = 1
GO
```

If change data capture is enabled on a table with an existing primary key, and the *@index_name* parameter isn't used to identify an alternative unique index, the change data capture feature uses the primary key. Subsequent changes to the primary key aren't allowed without first disabling change data capture for the table. This is true regardless of whether support for net changes queries was requested when change data capture was configured.

If there's no primary key on a table at the time it's enabled for change data capture, the subsequent addition of a primary key is ignored by change data capture. Because change data capture doesn't use a primary key that is created after the table was enabled, the key and key columns can be removed without restrictions.

For more information about the `sys.sp_cdc_enable_table` stored procedure arguments, see [sys.sp_cdc_enable_table (Transact-SQL)](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-enable-table-transact-sql).

> [!TIP]  
> To determine whether a source table has already been enabled for change data capture, examine the `is_tracked_by_cdc` column in the `sys.tables` catalog view.

## Disable CDC for Azure SQL Database

Disabling CDC for your Azure SQL Database removes all associated change data capture metadata, including the `cdc user`, `cdc schema`, and the external scheduler capture and cleanup processes. However, any gating roles created by change data capture aren't removed automatically, and must be explicitly deleted.

> [!NOTE]  
> To determine if a database has cdc enabled, query the `is_cdc_enabled` column in the `sys.databases` catalog view.

It's not necessary to disable CDC for individual tables before you disable CDC at the database level.

To disable CDC at the database level, run the following T-SQL:

```sql
EXEC sys.sp_cdc_disable_db;
GO
```

> [!TIP]  
> After you disable CDC at the database level, you'll need to [enable CDC for individual tables](#enable-cdc-for-a-table) again if you want to use the CDC feature once more.

## Manage CDC

In Azure SQL Database, CDC is a crucial feature to track and manage changes in your database tables. Unlike traditional SQL Server environments, Azure SQL Database employs a change data capture scheduler to handle CDC tasks instead of relying on SQL Server Agent jobs. This scheduler automatically initiates periodic capture and cleanup processes for CDC tables within your database, ensuring reliability and performance without external dependencies.

### Automatic CDC capture and cleanup

The CDC capture job in Azure SQL Database operates seamlessly, running every 20 seconds to track changes efficiently. Simultaneously, the cleanup job runs every hour, ensuring your CDC tables remain optimized. Users can rest assured that CDC management occurs automatically without manual intervention.

> [!IMPORTANT]  
> If a serverless database has CDC enabled and is in a paused state, CDC doesn't run. The CDC scan will not affect the autopause feature.

### Manual CDC control

While CDC runs automatically, users maintain the flexibility to perform manual CDC operations on demand. The [sp_cdc_scan](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-scan-transact-sql) and [sp_cdc_cleanup_change_tables](/sql/relational-databases/system-stored-procedures/sys-sp-cdc-cleanup-change-table-transact-sql) procedures allow you to trigger capture and cleanup tasks as needed.

### Monitor CDC

Azure SQL Database provides valuable tools to monitor CDC activities. Two dynamic management views, [sys.dm_cdc_log_scan_sessions](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-log-scan-sessions) and [sys.dm_cdc_errors](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors), offer insights into CDC processes, ensuring you have full visibility into your data changes.

### CDC Customization

While Azure SQL Database streamlines CDC management, some limitations exist:

- The frequency of CDC capture and cleanup jobs can't be customized.
- The `pollinginterval` and `continuous` values for capture and cleanup jobs aren't applicable in Azure SQL Database.
- Removing the capture job entry from the `cdc.cdc_jobs` table doesn't halt the background capture job.
- Dropping the cleanup job entry stops the cleanup job.
- The `cdc.cdc_jobs` table resides in the `cdc` schema, not `msdb`.

Despite these limitations, you can still customize the following options:

- Query the `cdc.cdc_jobs` table for current configuration details.
- Adjust the `maxtrans` and `maxscans` options using the `sp_cdc_change_job` stored procedure.
- Manage jobs by employing `sp_cdc_drop_job` and `sp_cdc_add_job` as needed.

## Performance considerations and recommendations

Enabling change data capture for Azure SQL Database has a performance effect comparable to enabling CDC for SQL Server or Azure SQL Managed Instance. However, several factors influence the performance effect when enabling CDC, including:

- The number of CDC-enabled tables in your Azure SQL Database.

- Frequency of changes in the tracked tables, or volume of transactions. Active transactions prevent log truncation until the transaction commits and CDC scan catches up, or the transaction aborts. This might result in the transaction log filling up more than usual and should be monitored so that the transaction log doesn't fill.

- Make sure there's free space available in the source database, as CDC artifacts (for example, CT tables, cdc_jobs etc.) are stored in the same database.

- Whether you have a single database or it's part of an elastic pool.

- Databases in an elastic pool share resource among them (such as disk space), therefore enabling CDC on multiple databases runs the risk of reaching the maximum size of the elastic pool disk size. Monitor resources such as CPU, memory, and log throughput. For more information, see [Resource management in dense elastic pools](elastic-pool-resource-management.md).

- When dealing with databases in elastic pools, it's crucial to consider the count of CDC-enabled  tables and the number of databases that those tables belong to. We suggest assessing your workload and taking required measures, such as scaling the elastic pool. For more information, see [Scale elastic pool resources in Azure SQL Database](elastic-pool-scale.md).

> [!IMPORTANT]  
> These considerations are general guidance. For precise guidance to optimize performance for a specific workload, reach out to [Microsoft support](/sql/sql-server/sql-server-get-help).

Consider the following best practices when you use CDC with Azure SQL Database:

- Test your workload thoroughly before enabling CDC on databases in production to help you determine the appropriate SLO fit for your workload. For more information about Azure SQL Database compute sizes, see [Service tiers](sql-database-paas-overview.md#service-tiers).

- Consider scaling the number of vCores or transitioning to a higher database tier, such as Hyperscale, to maintain the previous performance level once CDC has been enabled on your Azure SQL Database. For more information, see [vCore purchasing model - Azure SQL Database](service-tiers-sql-database-vcore.md) and [Hyperscale service tier](service-tier-hyperscale.md).

- Monitor space utilization closely. For more information, see [Manage file space for databases in Azure SQL Database](file-space-manage.md).

- Monitor log generation rate, for more information, see [Resource consumption by user workloads and internal processes](resource-limits-logical-server.md#resource-consumption-by-user-workloads-and-internal-processes).

- The CDC scan and cleanup processes are part of your regular database workload (also consuming resources). Depending on the volume of transactions, performance degradation can be substantial due to the scan and cleanup processes not keeping up with the workload as entire rows are added to change tables and for update operations, preimage is also included. We suggest assessing your workload and taking the required measures according to the previous recommendations. For more information, see the [CDC management](#manage-cdc) section in this article.

> [!IMPORTANT]  
> The scheduler runs capture and cleanup automatically within the SQL Database. **The CDC capture job runs every 20 seconds, and the cleanup job runs every hour**.

- To prevent an increase in latency, ensure the number of CDC-enabled databases doesn't surpass the count of vCores allocated to an elastic pool. To learn more, see [Resource management in dense elastic pools](elastic-pool-resource-management.md).

- Based on your workload and performance level, consider changing the CDC retention period to a smaller number than the default of three days to ensure that the cleanup process can keep pace with changes in the change table. Maintaining a lower retention period while monitoring the database size is a good practice.

- **No Service Level Agreement (SLA)** is provided for when changes are populated to the change tables. Subsecond latency is also not supported.

## Known issues and limitations

### Aggressive log truncation

When you enable change data capture (CDC) on Azure SQL Database, the aggressive log truncation feature of Accelerated Database Recovery (ADR) is disabled. This is because the CDC scan accesses the database transaction log. Active transactions prevent transaction log truncation until the transaction commits and CDC scan catches up, or the transaction aborts. This might result in the transaction log filling up more than usual and should be monitored so that the transaction log doesn't fill.

When enabling CDC, we recommend using the resumable index option when you create or rebuild an index. Resumable indexes don't keep a long-running transaction open, and allow log truncation during the operation for better log space management. For more information, see [Guidelines for online index operations - Resumable Index considerations](/sql/relational-databases/indexes/guidelines-for-online-index-operations#resumable-index-considerations).

### Azure SQL Database service tier

While CDC is supported for [databases](resource-limits-vcore-single-databases.md) and [elastic pools](resource-limits-vcore-elastic-pools.md) in *any service tier* within the vCore-based purchasing model, databases lower than **S3** (such as Basic, S0, S1, S2) aren't supported in the [DTU purchasing model](resource-limits-dtu-single-databases.md).

### Azure SQL Database log limits

Accelerated Database Recovery and CDC aren't compatible in Azure SQL Database. This is because the CDC scan actively accesses and interacts with the database transaction log, which can conflict with the aggressive log truncation behavior of ADR.

To prevent scalability and space management issues, closely monitoring your Azure SQL Database and consider scaling to a higher database tier, and allow your transaction log to grow according to your workload needs.

> [!TIP]  
> If your workload demands higher overall performance due to higher transaction log throughput and faster transaction commit times, use the [Hyperscale service tier](service-tier-hyperscale.md).

### Capture and cleanup customization

Configuring the frequency of the capture and the cleanup processes for CDC in Azure SQL Databases isn't possible. The scheduler runs capture and cleanup automatically.

### Failover in Azure SQL Database

In the event of local or GeoDR failover scenarios, if your database has CDC enabled, the process of capturing and cleaning up data changes will occur automatically on the new primary database after the failover takes place.

### Microsoft Entra ID

[!INCLUDE [entra-id](../includes/entra-id.md)]

If you create a database in Azure SQL Database as a Microsoft Entra user and enable CDC on it, a SQL user (for example, even one in the `sysadmin` role) isn't able to disable/make changes to CDC artifacts. However, another Microsoft Entra user is able to enable/disable CDC on the same database.

Similarly, if you create a database as a SQL user, enabling/disabling change data capture as a Microsoft Entra user doesn't work.

Enabling CDC fails if you create a database in Azure SQL Database as a Microsoft Entra user, don't enable CDC, and then try enabling CDC after restoring the database.

To resolve this issue, connect to your database with your Microsoft Entra admin account, and run the following T-SQL:

```sql
ALTER AUTHORIZATION ON DATABASE::[<restored_db_name>] TO [<azuread_admin_login_name>];

EXEC sys.sp_cdc_enable_db;
```

### Point-in-time restore (PITR)

If you enabled CDC on your Azure SQL Database as SQL user, point-in-time-restore (PITR) retains the CDC in the restored database, unless it's restored to a subcore SLO. If restored to subcore SLO, CDC artifacts aren't available.

If you enable CDC on your database as a Microsoft Entra user, it's not possible to Point-in-time restore (PITR) to a subcore SLO. Restore the database to the same or higher SLO as the source, and then disable CDC if necessary.

## Troubleshooting

This section provides guidance and troubleshooting steps associated with CDC on Azure SQL Database. CDC-related errors might obstruct the proper functioning of the capture process and lead to the expansion of the database transaction log.

To examine these errors, you can query the dynamic management view [sys.dm_cdc_errors](/sql/relational-databases/system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors). If the `sys.dm_cdc_errors` dynamic management view returns any errors,  refer to the following section to understand the mitigation steps.

> [!NOTE]  
> For more information on a particular error code, see [Database Engine events and errors](/sql/relational-databases/errors-events/database-engine-events-and-errors).

These are the different troubleshooting categories included in this section:

| Category | Description |
| --- | --- |
| [Metadata modified](#metadata-modified) | Includes information on how to mitigate issues related with CDC when the tracked tabled has been modified or dropped. |
| [Database space management](#database-space-management) | Includes information on how to mitigate issues when the database space has been exhausted. |
| [CDC limitation](#cdc-limitation) | Includes information on how to mitigate issues caused by CDC limitations. |

### Metadata modified

#### Error 200/208 - Invalid object name

- **Cause**: The error might occur when CDC metadata has been dropped. For CDC to function properly, you shouldn't manually modify any CDC metadata such as the `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions (`sys.database_principals`) or rename the `cdc user`.

- **Recommendation**: To address this problem, you need to disable and re-enable CDC for your database. When enabling change data capture for a database, it creates the cdc schema, cdc user, metadata tables, and other system objects for the database. You'll need to manually re-enable [CDC for individual tables](#enable-cdc-for-a-table) after CDC is enabled for the database.

> [!NOTE]  
> Objects found in the [sys.objects](/sql/relational-databases/system-catalog-views/sys-objects-transact-sql) system catalog view with `is_ms_shipped=1` and `schema_name=cdc` shouldn't be altered or dropped.

#### Error 1202 - Database principal doesn't exist, or user is not a member

- **Cause**: The error might occur when CDC user has been dropped. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions (`sys.database_principals`) or rename `cdc user`.

- **Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 15517 - Can't execute as the database principal because the principal doesn't exist

- **Cause**: This type of principal can't be impersonated, or you don't have permission. The error might occur when CDC metadata has been dropped or it's no longer part of the `db_owner` role. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions (`sys.database_principals`) or rename the `cdc user`.

- **Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 18807 - Can't find an object ID for the replication system table

- **Cause**: This error happens when SQL Server can't find or access the replication system table '%s.' This could be because the table is missing or unreachable. CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions (`sys.database_principals`) or rename the `cdc user`.

- **Recommendation**: Verify that the system table exists and is accessible by querying the table directly. Query the [sys.objects](/sql/relational-databases/system-catalog-views/sys-objects-transact-sql) system catalog, set predicate clause with `is_ms_shipped=1` and `schema_name=cdc` to list all CDC-related objects. If the query doesn't return any objects, you should disable and then re-enable CDC for your database. Enabling change data capture for a database creates the `cdc schema`, `cdc user`, metadata tables, and other system objects for the database. You'll need to manually re-enable [CDC for individual tables](#enable-cdc-for-a-table) after CDC is enabled for the database.

#### Error 21050 - Only members of the sysadmin or db_owner fixed server role can perform this operation

- **Cause**: The `cdc` user has been removed from the `db_owner` database role, or from the `sysadmin` server role.

- **Recommendation**: Ensure the `cdc` user has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

### Database Space Management

#### Error 1105 - Couldn't allocate space for object in database because the filegroup is full

- **Cause**: This error occurs when the primary filegroup of a database runs out of space, and SQL Database is unable to allocate more space for an object (such as a table or index) within that filegroup.

- **Recommendation**: To resolve this issue, delete any unnecessary data within your database to free up space. Identify unused tables, indexes, or other objects in the filegroup that can be safely removed. Monitor space utilization closely, for more information, see [Manage file space for databases in Azure SQL Database](file-space-manage.md).

  In case dropping unnecessary data/objects is **not an option**, consider scaling to a higher database tier.

> [!IMPORTANT]  
> For detailed information about Azure SQL Database (single database) compute sizes (SLO) see [Resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md) and [Resource limits for single databases using the DTU purchasing model - Azure SQL Database](resource-limits-dtu-single-databases.md).

#### Error 1132 - The elastic pool has reached its storage limit

- **Cause**: This error occurs when the storage usage in your elastic pool has exceeded the allocated limit.

- **Recommendation**: To resolve this issue, implement data archiving and purging strategies to keep only the necessary data in the databases that are part of the elastic pool. Monitor space utilization closely. For more information, see [Manage file space for databases in Azure SQL Database](file-space-manage.md).

  In case archiving data or dropping unnecessary data/objects isn't an option, consider scaling to a higher database tier.

> [!IMPORTANT]  
> For detailed information about Azure SQL Database (single database) compute sizes (SLO) see [Resource limits for elastic pools using the vCore purchasing model](resource-limits-vcore-elastic-pools.md) and [Resource limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md).

#### CDC limitation

#### Error 2628 - string or binary data would be truncated in table

- **Cause**: Changing the size of columns of a CDC-enabled table using DDL statements can cause issues with the subsequent CDC capture process. The `sys.dm_cdc_errors` Dynamic Management View (DMV) is a useful for checking any CDC for any reported issues, like errors number 2628 and 8115.

- **Recommendation**: Before making any changes to column size, you must assess whether the alteration is compatible with the existing data in CDC change tables. To address this problem, you need to disable and re-enable CDC for your database. For more information about enabling CDC for a database or a table, see [Enable CDC for Azure SQL Database](#enable-cdc-for-azure-sql-database) and [Enable CDC for a table](#enable-cdc-for-a-table) sections in this article.

#### Error 22830 - Built-in function 'SUSER_SNAME' in impersonation context is not supported in this version of SQL Server

- **Cause**: This error occurs during enabling CDC if a user trigger exists on the database, which has a call to `SUSER_SNAME()` on `create_table`. You can list triggers with the following Transact-SQL script. This command gives the details of the object trigger and the corresponding `object_id`:

  ```sql
  SELECT name,
      object_id
  FROM sys.triggers
  WHERE parent_class_desc = 'DATABASE'
      AND is_disabled = 0;
  ```

  Once you get the trigger definitions, you can look for calls being made to `SYSTEM_USER` with the following script:

  ```sql
  SELECT OBJECT_DEFINITION(object_id) AS trigger_definition;
  ```

- **Recommendation**: To resolve this issue, follow these steps for each user trigger obtained from the previous script.

  - Disable the trigger
  - Enable CDC
  - Re-enable the trigger

For more information, see [DISABLE TRIGGER (Transact-SQL)](/sql/t-sql/statements/disable-trigger-transact-sql).

#### Error 913 - CDC capture job fails when processing changes for a table with system CLR data type

- **Cause**: This error occurs when enabling CDC on a table with system CLR data type, making DML changes, and then making DDL changes on the same table while the CDC capture job is processing changes related to other tables.

- **Recommendation**: The recommended steps are to quiesce DML to the table, run a capture job to process changes, run DDL for the table, run a capture job to process DDL changes, and then re-enable DML processing. For more information, see [CDC capture job fails when processing changes](/troubleshoot/sql/database-engine/replication/cdc-capture-job-fails-processing-changes-table).

## Create user and assign role

If the `cdc user` was removed, you can manually add the user back.

Use the following T-SQL script, to create a user (`cdc`), and assign the proper role (`db_owner`).

```sql
IF NOT EXISTS (
    SELECT *
    FROM sys.database_principals
    WHERE NAME = 'cdc'
)
BEGIN
    CREATE USER [cdc] WITHOUT LOGIN
    WITH DEFAULT_SCHEMA = [cdc];
END

EXEC sp_addrolemember 'db_owner', 'cdc';
```

## Check and add role membership

To verify if `cdc` user belongs to either the `sysadmin` or `db_owner` role, run the following T-SQL query:

```sql
EXECUTE AS USER = 'cdc';

SELECT is_srvrolemember('sysadmin'), is_member('db_owner');
```

If the `cdc` user doesn't belong to either role, execute the following T-SQL query to add `db_owner` role to the `cdc` user.

```sql
EXEC sp_addrolemember 'db_owner' , 'cdc';
```

## Related content

- [Work with Change Data](/sql/relational-databases/track-changes/work-with-change-data-sql-server)
- [Track Data Changes](/sql/relational-databases/track-changes/track-data-changes-sql-server)
- [Administer and Monitor change data capture](/sql/relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server)
- [Temporal Tables](/sql/relational-databases/tables/temporal-tables)
