---
title: "Known issues, limitations and errors with CDC"
description: "Known issues and errors with change data capture (CDC) in SQL Server and Azure SQL Managed Instance"
author: croblesm
ms.author: roblescarlos
ms.reviewer: "mathoma"
ms.date: 10/23/2024
ms.service: sql
ms.topic: troubleshooting
helpviewer_keywords:
  - "Change data capture"
  - "Known issues"
  - "Limitations"
---

# Known limitations, issues and errors with CDC
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

This article explains known limitations, issues and errors with change data capture (CDC) for **SQL Server** and **Azure SQL Managed Instance**. 

For Azure SQL Database, see [Known issues with CDC in Azure SQL Database](/azure/azure-sql/database/change-data-capture-overview#known-issues-and-limitations). 

## Modifying metadata

For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([`sys.database_principals`](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

Any objects in [sys.objects](../system-catalog-views/sys-objects-transact-sql.md) with `is_ms_shipped` property set to `1` shouldn't be modified.

```sql
SELECT    name AS object_name   
        ,SCHEMA_NAME(schema_id) AS schema_name  
        ,type_desc  
        ,is_ms_shipped  
FROM sys.objects 
WHERE is_ms_shipped= 1 AND SCHEMA_NAME(schema_id) = 'cdc'
```

## Collation differences

It's important to be aware of a situation where you have different collations between the database and the columns of a table configured for change data capture. CDC uses interim storage to populate side tables. If a table has CHAR or VARCHAR columns with collations that are different from the database collation and if those columns store non-ASCII characters (such as double byte DBCS characters), CDC might not be able to persist the changed data consistent with the data in the base tables. This is because the interim storage variables can't have collations associated with them.

Consider one of the following approaches to ensure captured change data is consistent with base tables:

* Use NCHAR or NVARCHAR data type for columns containing non-ASCII data.  

* Or, Use the same collation for columns and for the database.

For example, if you have one database that uses a collation of  SQL_Latin1_General_CP1_CI_AS, consider the following table:

```sql
CREATE TABLE T1( 
     C1 INT PRIMARY KEY, 
     C2 VARCHAR(10) collate Chinese_PRC_CI_AI)
```

CDC might fail to capture the binary data for column C2, because its collation is different (Chinese_PRC_CI_AI). Use NVARCHAR to avoid this problem:

```sql
CREATE TABLE T1( 
     C1 INT PRIMARY KEY, 
     C2 NVARCHAR(10) collate Chinese_PRC_CI_AI --Unicode data type, CDC works well with this data type
     )
```

## Accelerated Database Recovery (ADR) and change data capture (CDC)

Currently, enabling both change data capture (CDC) and Accelerated Database Recovery (ADR) is not supported. When you enable change data capture (CDC) on SQL Server, the aggressive log truncation feature of ADR is disabled. This is because the CDC scan accesses the database transaction log. Active transactions continue to hold the transaction log truncation until the transaction commits and CDC scan catches up, or transaction aborts. This can cause various issues including the transaction log filling up more than usual or data operations recorded in the side table being abnormal.

When enabling CDC, we recommend using the Resumable index option. Resumable index doesn't require to keep open a long-running transaction to create or rebuild an index, allowing log truncation during this operation and better log space management. For more information, see [Guidelines for online index operations - Resumable Index considerations](../../relational-databases/indexes/guidelines-for-online-index-operations.md#resumable-index-considerations). 

## Online DDL statements are unsupported

[Online DDL statements](../../t-sql/statements/alter-table-transact-sql.md#with--online--on--off-as-applies-to-altering-a-column) are unsupported when change change data capture is enabled on a database. 


## Enabling CDC fails if schema or user named `cdc` already exists

When you enable CDC on a database, it creates a new schema and user named `cdc`. So manually creating a custom schema or user named `cdc` is not recommended, as it's reserved for system use.  

If you've manually defined a custom schema or user named `cdc` in your database that isn't related to CDC, the system stored procedure `sys.sp_cdc_enable_db` fails to enable CDC on the database with below error message.

> The database `<database_name>` cannot be enabled for change data capture because a database user named 'cdc' or a schema named 'cdc' already exists in the current database. These objects are required exclusively by CDC. Drop or rename the user or schema and retry the operation.

To resolve this issue:

- Manually drop the empty `cdc` schema and `cdc` user. Then, CDC can be enabled successfully on the database.

## CDC fails after ALTER COLUMN to VARCHAR and VARBINARY

When the datatype of a column on a CDC-enabled table is changed from `TEXT` to `VARCHAR` or `IMAGE` to `VARBINARY` and an existing row is updated to an off-row value. After the update, the CDC scan will result in errors.

## DDL changes to source tables

Changing the size of columns of a CDC-enabled table using DDL statements can cause issues with the subsequent CDC capture process, resulting in **error 2628** or **error 8115**. Remember that data in CDC change tables are retained based on user-configured settings. So, before making any changes to column size, you must assess whether the alteration is compatible with the existing data in CDC change tables.

If the `sys.dm_cdc_errors` indicate that scans are failing due to the **error 2628** or **error 8115** for change tables, you should first consume the change data in the affected change tables. After that, you need to [disable and then reenable CDC](enable-and-disable-change-data-capture-sql-server.md) on the table to resolve the problem effectively.

## Import database using data-tier Import/Export and Extract/Publish operations

For CDC enabled SQL databases, when you use SqlPackage, SSDT, or other SQL tools to Import/Export or Extract/Publish, the `cdc` schema and user get excluded in the new database. Other CDC objects not included in Import/Export and Extract/Deploy operations include the tables marked as `is_ms_shipped=1` in sys.objects.

Even if CDC isn't enabled and you've defined a custom schema or user named `cdc` in your database that will also be excluded in Import/Export and Extract/Deploy operations to import/setup a new database.



## Partition switching with variables

Using variables with partition switching on databases or tables with change data capture (CDC) isn't supported for the `ALTER TABLE ... SWITCH TO ... PARTITION ...` statement. See [partition switching limitations](../replication/publish/replicate-partitioned-tables-and-indexes.md#replication-support-for-partition-switching) to learn more. 

## Troubleshooting errors

This section steps to troubleshoot errors associated with CDC on SQL Server, and Azure SQL Managed Instance. CDC-related errors may obstruct the proper functioning of the capture process and lead to the expansion of the database transaction log.

To examine these errors, you can query the dynamic management view [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md). If [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) dynamic management view returns any errors,  refer to the following section to understand the mitigation steps.

> [!NOTE]
> For more information on a particular error code, see [Database Engine events and errors](../errors-events/database-engine-events-and-errors.md).  

These are the different troubleshooting categories included in this section:

| Category | Description |
|-------------|-------------|
| [Metadata Modified](#metadata-modified) | Includes information on how to mitigate issues related with CDC when the tracked tabled has been modified or dropped. |
| [Database Space Management](#database-space-management) | Includes information on how to mitigate issues when the database space has been exhausted.  |
| [CDC Limitation](#cdc-limitation) | Includes information on how to mitigate issues caused by CDC limitations. |

### Metadata modified  

#### Error 200/208 - Invalid object name  

* **Cause**: The error might occur when CDC metadata has been dropped. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([`sys.database_principals`](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

* **Recommendation**: To address this problem, you need to disable and re-enable CDC for your database. When enabling change data capture for a database, it creates the cdc schema, cdc user, metadata tables, and other system objects for the database.

> [!NOTE]
> Objects found in the [**sys.objects**](../system-catalog-views/sys-objects-transact-sql.md) system catalog view with **is_ms_shipped=1 and schema_name='cdc'** should not be altered or dropped.

#### Error 1202 - Database principal doesn't exist, or user isn't a member

* **Cause**: The error might occur when CDC user has been dropped. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([`sys.database_principals`](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

* **Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 15517 - Can't execute as the database principal because the principal doesn't exist

* **Cause**: This type of principal can't be impersonated, or you don't have permission. The error might occur when CDC metadata has been dropped or it's no longer part of the `db_owner` role. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([`sys.database_principals`](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

* **Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 18807 - Can't find an object ID for the replication system table

* **Cause**: This error happens when SQL Server can't find or access the replication system table '%s.' This could be because the table is missing or unreachable. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([`sys.database_principals`](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

* **Recommendation**: Verify that the system table exists and is accessible by querying the table directly. Query the [**sys.objects**](../system-catalog-views/sys-objects-transact-sql.md) system catalog, set predicate clause with **is_ms_shipped=1 and schema_name='cdc'** to list all CDC-related objects. If the query doesn't return any objects, you should disable and then re-enable CDC for your database. Enabling change data capture for a database creates the cdc schema, cdc user, metadata tables, and other system objects for the database.

#### Error 21050 - Only members of the sysadmin or db_owner fixed server role can perform this operation

* **Cause**: The `cdc` user has been removed from the `db_owner` database role, or from the `sysadmin` server role.

* **Recommendation**: Ensure the `cdc` user has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

### Database Space Management

#### Error 1105 - Couldn't allocate space for object in database because the filegroup is full

* **Cause**: This error occurs when the primary filegroup of a database runs out of space, and SQL Server is unable to allocate more space for an object (such as a table or index) within that filegroup.

* **Recommendation**: To resolve this issue, delete any unnecessary data within your database to free up space. Identify unused tables, indexes, or other objects in the filegroup that can be safely removed. Monitor space utilization closely, for more information, see [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)

    In case dropping unnecessary data/objects is **not an option**, consider allocating more space for your database transaction log. For more information about transaction log management, see [SQL Server transaction log architecture and management guide](../sql-server-transaction-log-architecture-and-management-guide.md)

### CDC limitation

#### Error 2628 - string or binary data would be truncated in table

* **Cause**: Changing the size of columns of a CDC-enabled table using DDL statements can cause issues with the subsequent CDC capture process. The 'sys.dm_cdc_errors' Dynamic Management View (DMV) is a useful for checking any CDC for any reported issues, like errors number 2628 and 8115.

* **Recommendation**: Before making any changes to column size, you must assess whether the alteration is compatible with the existing data in CDC change tables.  To address this problem, you need to disable and re-enable CDC for your database. For more information about enabling CDC for a database or a table, see [**Enable CDC for a database**](enable-and-disable-change-data-capture-sql-server.md#enable-for-a-database) and [**Enable CDC for a table**](enable-and-disable-change-data-capture-sql-server.md#enable-for-a-table).

#### Error 913 - CDC capture job fails when processing changes for a table with system CLR datatype

* **Cause**: This error occurs when enabling CDC on a table with system CLR datatype, making DML changes, and then making DDL changes on the same table while the CDC capture job is processing changes related to other tables.

* **Recommendation**: The recommended steps are to quiesce DML to the table, run a capture job to process changes, run DDL for the table, run a capture job to process DDL changes, and then re-enable DML processing. For more information, see [CDC capture job fails when processing changes](/troubleshoot/sql/database-engine/replication/cdc-capture-job-fails-processing-changes-table).


## Create user and assign role

If the `cdc user` was removed, you can manually add the user back. 

Use the following T-SQL script, to create a user (`cdc`), and assign the proper role for the same (`db_owner`).

```sql
IF NOT EXISTS 
(
    SELECT * 
    FROM sys.database_principals 
    WHERE NAME = 'cdc'
)
BEGIN
    CREATE USER [cdc] 
    WITHOUT LOGIN WITH DEFAULT_SCHEMA = [cdc];
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

## Next steps

- For an overview of CDC for SQL Server, see [What is change data capture (CDC)?](about-change-data-capture-sql-server.md)
- For more information about known issues and limitations for CDC with Azure SQL Database, see [CDC known issues and limitations with Azure SQL Database](/azure/azure-sql/database/change-data-capture-overview#known-issues-and-limitations)
- For more information on how to enable and disable CDC, see [Enable and disable change data capture](enable-and-disable-change-data-capture-sql-server.md)
