---
title: "Known Issues, Limitations, and Errors with CDC"
description: "Known issues and errors with change data capture (CDC) in SQL Server and Azure SQL Managed Instance"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: roblescarlos, bspendolini, randolphwest
ms.date: 10/07/2025
ms.service: sql
ms.topic: troubleshooting
helpviewer_keywords:
  - "Change data capture"
  - "Known issues"
  - "Limitations"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Known limitations, issues, and errors with CDC

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

This article explains known limitations, issues, and errors with change data capture (CDC) for **SQL Server** and **Azure SQL Managed Instance**. 

For Azure SQL Database, see [Known issues with CDC in Azure SQL Database](/azure/azure-sql/database/change-data-capture-overview#known-issues-and-limitations). 

<a id="modifying-metadata"></a>

## Modify metadata

For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([sys.database_principals](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

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

It's important to be aware of a situation where you have different collations between the database and the columns of a table configured for change data capture. CDC uses interim storage to populate side tables. If a table has **char** or **varchar** columns with collations that are different from the database collation, and if those columns store non-ASCII characters (such as double byte DBCS characters), CDC might not be able to persist the changed data consistent with the data in the base tables. This is because the interim storage variables can't have collations associated with them.

Consider one of the following approaches to ensure captured change data is consistent with base tables:

* Use **nchar** or **nvarchar** data type for columns containing non-ASCII data.  

* Or, Use the same collation for columns and for the database.

For example, if you have one database that uses a collation of SQL_Latin1_General_CP1_CI_AS, consider the following table:

```sql
CREATE TABLE T1( 
     C1 INT PRIMARY KEY, 
     C2 VARCHAR(10) collate Chinese_PRC_CI_AI)
```

CDC might fail to capture the binary data for column C2, because its collation is different (Chinese_PRC_CI_AI). Use **nvarchar** to avoid this problem:

```sql
CREATE TABLE T1( 
     C1 INT PRIMARY KEY, 
     C2 NVARCHAR(10) collate Chinese_PRC_CI_AI --Unicode data type, CDC works well with this data type
     )
```

## Accelerated database recovery (ADR) and change data capture (CDC)

Enabling both change data capture (CDC) and accelerated database recovery (ADR) for the same database isn't supported in [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]. Enabling both CDC and ADR is supported in later [!INCLUDE[ssnoversion-md](../../includes/ssnoversion-md.md)] versions starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] Cumulative Update 18.

When you enable CDC, the aggressive log truncation feature of ADR is disabled. This is because the CDC scan accesses the database transaction log. Active transactions continue to hold the transaction log truncation until the transaction commits and CDC scan catches up, or the transaction aborts. If you enable CDC on a database where ADR is enabled, you might observe higher transaction log utilization. Ensure that sufficient transaction log space is available for the needs of all your workloads.

<a id="enabling-cdc-fails-if-schema-or-user-named-cdc-already-exists"></a>

## Enable CDC fails if schema or user named `cdc` already exists

When you enable CDC on a database, it creates a new schema and user named `cdc`. So manually creating a custom schema or user named `cdc` isn't recommended, as it's reserved for system use.  

If you've manually defined a custom schema or user named `cdc` in your database that isn't related to CDC, the system stored procedure `sys.sp_cdc_enable_db` fails to enable CDC on the database with the following error message.

```output
The database <database_name> cannot be enabled for change data capture because a database user named 'cdc' or a schema named 'cdc' already exists in the current database. These objects are required exclusively by CDC. Drop or rename the user or schema and retry the operation.
```

To resolve this issue:

- Manually drop the empty `cdc` schema and `cdc` user. Then, CDC can be enabled successfully on the database.

## CDC fails after ALTER COLUMN

When the data type of a column on a CDC-enabled table is changed to an unsupported conversion, the CDC scan might result in errors after the update. 

The following are examples of `ALTER COLUMN` data type changes that aren't supported when CDC is enabled on a table:

- **bigint** to **int**
- **char(x)**, **nvarchar(x)**, or **nvarchar(x)** to **uniqueidentifier**, **DATE**, **Numeric** or **INT**

Changing the data type of a column in a CDC-enabled table can result in the following errors: 

- [Error 241](#error-241---conversion-failed-when-converting-date-andor-time-from-character-string) - Conversion failed when converting date and/or time from character string.
- [Error 245](#error-245---conversion-failed-when-converting-the-value-from-string-to-int) - Conversion failed when converting the value.
- [Error 8114](#error-8114---conversion-failed-when-converting-from-a-character-string-to-numeric-value) - Conversion failed when converting from a character string to numeric value.
- [Error 8169](#error-8169---conversion-failed-when-converting-from-a-character-string-to-uniqueidentifier) - Conversion failed when converting from a character string to uniqueidentifier.

Changing the size of columns of a CDC-enabled table using DDL statements can cause issues with the subsequent CDC capture process and result in the following errors: 
- [Error 2628](#error-2628---string-or-binary-data-would-be-truncated-in-table) - String or binary data would be truncated in table.
- [Error 8115](#error-8115---arithmetic-overflow-error-converting-data-type-from-bigint-to-int) - Arithmetic overflow error converting data type from bigint to int
- [Error 4922](#error-4922-and-error-5074---alter-table-alter-column-failed-because-of-dependent-objects) - ALTER TABLE ALTER COLUMN `<ColumnName>` failed because one or more objects access this column.
- [Error 5074](#error-4922-and-error-5074---alter-table-alter-column-failed-because-of-dependent-objects) - The statistics `<ColumnName>` is dependent on column `<ColumnName>`.

Data in CDC change tables are retained based on user-configured settings. Before making any changes to column size, you must assess whether the alteration is compatible with the existing data in CDC change tables.

If the `sys.dm_cdc_errors` indicate that scans are failing due to the [Error 2628](#error-2628---string-or-binary-data-would-be-truncated-in-table) or [Error 8115](#error-8115---arithmetic-overflow-error-converting-data-type-from-bigint-to-int) for change tables, you should first consume the change data in the affected change tables. After that, you need to [disable and then reenable CDC](enable-and-disable-change-data-capture-sql-server.md) on the table to resolve the problem effectively.

## Enabling CDC fails when CREATE OBJECT triggers exist

When you enable CDC, a `cdc user` is created to manage the CDC creation process. The `cdc user` runs a number of stored procedures to enable CDC, and some of these stored procedures create objects which fire existing `CREATE OBJECT` triggers. Since the `cdc user` does not have permission to write to the `master` database, these CDC stored procedures fail with error 22830.

Disable any `CREATE OBJECT` triggers before enabling CDC on a database. Reenable these triggers after CDC is configured.

## Import database using data-tier Import/Export and Extract/Publish operations

For CDC enabled SQL databases, when you use SqlPackage, SSDT, or other SQL tools to Import/Export or Extract/Publish, the `cdc` schema and user get excluded in the new database. Other CDC objects not included in Import/Export and Extract/Deploy operations include the tables marked as `is_ms_shipped=1` in `sys.objects`.

Even if CDC isn't enabled and you've defined a custom schema or user named `cdc` in your database that will also be excluded in Import/Export and Extract/Deploy operations to import/setup a new database.

## Partition switching with variables

Using variables with partition switching on databases or tables with change data capture (CDC) isn't supported for the `ALTER TABLE ... SWITCH TO ... PARTITION ...` statement. See [partition switching limitations](../replication/publish/replicate-partitioned-tables-and-indexes.md#replication-support-for-partition-switching) to learn more. 

## Online operations

### Online DDL statements are unsupported

In Azure SQL Managed Instance and versions of SQL Server before [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], [ALTER TABLE online DDL statements](../../t-sql/statements/alter-table-transact-sql.md#with--online--on--off-as-applies-to-altering-a-column) are unsupported when change data capture is enabled on a database. 

### Online index operations are unsupported

[Online index operations](../indexes/perform-index-operations-online.md) are unsupported when change data capture is enabled on a database. You can encounter Error 18773, "Could not locate text information records for the column "%.*ls", ID %d during command construction.". 

### Default constraints on added columns 

When CDC is enabled on a table and a non-nullable column with a default constraint is added, existing row data will have the value of the default constraint. However, CDC will use `NULL` instead of the default value for *existing* rows. This applies only to data present before the DDL was applied. As a workaround, issue non-changing `UPDATE` statements to existing rows, or, perform an `ALTER INDEX ... REBUILD` on the clustered index of the table. Use `ALTER TABLE ... REBUILD` on the heap if no clustered index is present.

<a id="troubleshooting-errors"></a>

## Troubleshooting

This section steps to troubleshoot errors associated with CDC on SQL Server, and Azure SQL Managed Instance. CDC-related errors might obstruct the proper functioning of the capture process and lead to the expansion of the database transaction log.

To examine these errors, you can query the dynamic management view [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md). If [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) dynamic management view returns any errors, review the following troubleshooting information.

> [!NOTE]
> For more information on a particular error code, see [Database Engine events and errors](../errors-events/database-engine-events-and-errors.md).  

These are the different troubleshooting categories included in this section:

| Category | Description |
|-------------|-------------|
| [Metadata modified](#metadata-modified) | Includes information on how to mitigate issues related with CDC when the tracked tabled has been modified or dropped. |
| [Database space management](#database-space-management) | Includes information on how to mitigate issues when the database space has been exhausted. |
| [CDC limitations](#cdc-limitation) | Includes information on how to mitigate issues caused by CDC limitations. |

### Metadata modified

#### Error 200/208 - Invalid object name

**Cause**: The error might occur when CDC metadata has been dropped. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions ([sys.database_principals](../system-catalog-views/sys-database-principals-transact-sql.md)) or rename the `cdc user`.

**Recommendation**: To address this problem, you need to disable and re-enable CDC for your database. When enabling change data capture for a database, it creates the cdc schema, cdc user, metadata tables, and other system objects for the database.

> [!NOTE]
> Objects found in the [sys.objects](../system-catalog-views/sys-objects-transact-sql.md) system catalog view with `is_ms_shipped=1 and schema_name='cdc'` should not be altered or dropped.

#### Error 1202 - Database principal doesn't exist, or user isn't a member

**Cause**: The error might occur when `cdc user` has been dropped. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions, or rename the `cdc user`.

**Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 15517 - Can't execute as the database principal because the principal doesn't exist

**Cause**: This type of principal can't be impersonated, or you don't have permission. The error might occur when CDC metadata has been dropped or it's no longer part of the `db_owner` role. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions, or rename the `cdc user`.

**Recommendation**: Ensure the `cdc` user exists in your database, and also has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 18807 - Can't find an object ID for the replication system table

**Cause**: This error happens when SQL Server can't find or access the replication system table '%s.' This could be because the table is missing or unreachable. For CDC to function properly, you shouldn't manually modify any CDC metadata such as `CDC schema`, change tables, CDC system stored procedures, default `cdc user` permissions, or rename the `cdc user`.

**Recommendation**: Verify that the system table exists and is accessible by querying the table directly. Query the [sys.objects](../system-catalog-views/sys-objects-transact-sql.md) system catalog, set predicate clause with `is_ms_shipped=1 and schema_name='cdc'` to list all CDC-related objects. If the query doesn't return any objects, you should disable and then re-enable CDC for your database. Enabling change data capture for a database creates the cdc schema, cdc user, metadata tables, and other system objects for the database.

#### Error 21050 - Only members of the sysadmin or db_owner fixed server role can perform this operation

**Cause**: The `cdc user` has been removed from the `db_owner` database role, or from the `sysadmin` server role.

**Recommendation**: Ensure the `cdc user` has the `db_owner` role assigned. To create the `cdc` user, see the example [Create cdc user and assign role](#create-user-and-assign-role).

#### Error 22830 - Could not update the metadata that indicates database `<database name>` is enabled for Change Data Capture. The failure occurred when executing the command `<CDC stored procedure name>`.

**Cause**: This error occurs when a `CREATE OBJECT` trigger exists in the database or on the server. When you enable CDC, a `cdc user` is created to manage the CDC creation process. The `cdc user` runs a number of stored procedures to enable CDC, and some of these stored procedures create objects which fire existing `CREATE OBJECT` triggers. Since the `cdc user` does not have permission to write to the `master` database, these CDC stored procedures fail with error 22830.

**Recommendation**: Before you enable CDC on a database, disable any `CREATE OBJECT` triggers. Reenable these triggers again after CDC is configured.

### Database space management

#### Error 1105 - Couldn't allocate space for object in database because the filegroup is full

**Cause**: This error occurs when the primary filegroup of a database runs out of space, and SQL Server is unable to allocate more space for an object (such as a table or index) within that filegroup.

**Recommendation**: To resolve this issue, delete any unnecessary data within your database to free up space. Identify unused tables, indexes, or other objects in the filegroup that can be safely removed. Monitor space utilization closely, for more information, see [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)

In case dropping unnecessary data/objects is **not an option**, consider allocating more space for your database transaction log. For more information about transaction log management, see [SQL Server transaction log architecture and management guide](../sql-server-transaction-log-architecture-and-management-guide.md)

<a id="cdc-limitation"></a>

### CDC limitations

#### Error 241 - Conversion failed when converting date and/or time from character string

**Cause**: This error occurs when the [ALTER COLUMN](../../t-sql/statements/alter-table-transact-sql.md#alter-column) is performed on a **date** data type and the table has CDC enabled. For example, if a table has an **nvarchar** column and you change the data type to **date** (for example, `ALTER TABLE table_name ALTER COLUMN [column_name] DATE NULL`), you might see this error in the [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) DMV. Error 241 is due to an unsupported data conversion in the change table, even though the `ALTER` command on the source table succeeds.

**Recommendation**: To resolve this issue, disable and re-enable CDC for your table after altering the column. Alternatively, disable CDC before altering the column, and then reenable CDC after the `ALTER COLUMN` change. 

#### Error 245 - Conversion failed when converting the value from string to int

**Cause**: This error occurs when the [ALTER COLUMN](../../t-sql/statements/alter-table-transact-sql.md#alter-column) command is issued to change the data type of a column when table has CDC enabled. For example, if a table has an **nvarchar** column and you change the data type to **int** (for example, `ALTER TABLE table_name ALTER COLUMN [column_name] INT NULL`), you might see this error in the [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) DMV. Error 245 is due to an unsupported data conversion in the change table, even though the `ALTER` command on the source table succeeds.

**Recommendation**: To resolve this issue, disable and re-enable CDC for your table after altering the column. Alternatively, disable CDC before altering the column, and then reenable CDC after the `ALTER COLUMN` change. 

#### Error 913 - CDC capture job fails when processing changes for a table with system CLR data type

**Cause**: This error occurs when enabling CDC on a table with system CLR data type, making DML changes, and then making DDL changes on the same table while the CDC capture job is processing changes related to other tables.

**Recommendation**: The recommended steps are to quiesce DML to the table, run a capture job to process changes, run DDL for the table, run a capture job to process DDL changes, and then re-enable DML processing. For more information, see [CDC capture job fails](/troubleshoot/sql/database-engine/replication/cdc-capture-job-fails-processing-changes-table) when processing changes for a table with system CLR data type (**geometry**, **geography**, or **hierarchyid**).

#### Error 2628 - string or binary data would be truncated in table

**Cause**: Changing the size of columns of a CDC-enabled table using DDL statements can cause issues with the subsequent CDC capture process. The [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) Dynamic Management View (DMV) is a useful for checking any CDC for any reported issues, like errors number 2628 and 8115.

**Recommendation**: Before making any changes to column size, you must assess whether the alteration is compatible with the existing data in CDC change tables. To address this problem, you need to disable and re-enable CDC for your database. For more information about enabling CDC for a database or a table, see [Enable CDC for a database](enable-and-disable-change-data-capture-sql-server.md#enable-for-a-database) and [Enable CDC for a table](enable-and-disable-change-data-capture-sql-server.md#enable-for-a-table).

#### Error 8114 - Conversion failed when converting from a character string to numeric value

**Cause**: This error occurs when an [ALTER COLUMN](../../t-sql/statements/alter-table-transact-sql.md#alter-column) command is issued to change the data type of a column when the table has CDC enabled. For example, if a table has a **char(x)**, **nvarchar(x)**, **nvarchar(x)** column and you change the data type to **numeric** (such as: `ALTER TABLE table_name ALTER COLUMN [column_name] numeric`), you might see this error in the [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) Dynamic Management View (DMV). Error 8114 indicates an unsupported data conversion in the change table, even though the ALTER command on the source table succeeds.

**Recommendation**: To resolve this issue, disable and re-enable CDC for your table after altering the column. Alternatively, disable CDC before running the `ALTER COLUMN` command, and then reenable CDC after the `ALTER COLUMN` change.

#### Error 8115 - Arithmetic overflow error converting data type from bigint to int

**Cause**: This error occurs when an [ALTER COLUMN](../../t-sql/statements/alter-table-transact-sql.md#alter-column) DDL is executed on a CDC-enabled table that results in a decrease in the precision of the column (such as changing the data type of the column from **bigint** to **int**). The decreased precision column is unable to hold the values present in the change table.


**Recommendation**: To resolve this issue, disable and re-enable CDC for your table after altering the column. Alternatively, disable CDC before running the `ALTER COLUMN` command, and then reenable CDC after the `ALTER COLUMN` change.

#### Error 4922 and Error 5074 - ALTER TABLE ALTER COLUMN failed because of dependent objects

You might encounter the following errors when you run `ALTER TABLE ... ALTER COLUMN` on a CDC-enabled table to change the data type or reduce the length or precision of a column:
- Error 4922: ALTER TABLE ALTER COLUMN `<ColumnName>` failed because one or more objects access this column.
- Error 5074: The statistics `<ColumnName>` is dependent on column `<ColumnName>`.

**Cause**: These errors occur together when you run `ALTER TABLE ... ALTER COLUMN` on a CDC-enabled table to change the data type or reduce the length or precision of a column. Even if you drop statistics on the base table, statistics can still exist on CDC system tables (change tables), which creates a dependency that prevents the `ALTER COLUMN` operation from completing. The following is an example scenario that leads to this error condition:
1. A CDC-enabled table has columns such as **char(x)**, **nvarchar(x)**, or **nvarchar(x)**, and create statistics on those columns.
1. Modify one of these columns by reducing its length or precision (for example: `ALTER TABLE table_name ALTER COLUMN column_name NVARCHAR(x)`).
1. Drop statistics on the modified column. However, statistics on the corresponding CDC system table might  still exist.


**Recommendation**: To resolve this issue, [disable CDC](enable-and-disable-change-data-capture-sql-server.md) on the table before running the `ALTER COLUMN` command, and then reenable CDC after the column change is complete.

#### Error 8169 - Conversion failed when converting from a character string to uniqueidentifier

**Cause**: This error occurs when an [ALTER COLUMN](../../t-sql/statements/alter-table-transact-sql.md#alter-column) command is issued to change the data type of a column when the table has CDC enabled. For example, if a table has a **char(x)**, **nvarchar(x)**, **nvarchar(x)** column and you change the data type to **uniqueidentifier** (such as: `ALTER TABLE table_name ALTER COLUMN [column_name] uniqueidentifier`), you might see this error in the [sys.dm_cdc_errors](../system-dynamic-management-views/change-data-capture-sys-dm-cdc-errors.md) Dynamic Management View (DMV). Error 8169 indicates an unsupported data conversion in the change table, even though the ALTER command on the source table succeeds.


**Recommendation**: To resolve this issue, disable and re-enable CDC for your table after altering the column. Alternatively, disable CDC before running the `ALTER COLUMN` command, and then reenable CDC after the `ALTER COLUMN` change.



## Create user and assign role

If the `cdc user` was removed, you can manually add the user back. 

Use the following T-SQL script, to create a user (`cdc`), and assign the proper role for the same (**db_owner**).

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

## Related content

- [What is change data capture (CDC)?](about-change-data-capture-sql-server.md)
- [CDC known issues and limitations with Azure SQL Database](/azure/azure-sql/database/change-data-capture-overview#known-issues-and-limitations)
- [Enable and disable change data capture](enable-and-disable-change-data-capture-sql-server.md)
