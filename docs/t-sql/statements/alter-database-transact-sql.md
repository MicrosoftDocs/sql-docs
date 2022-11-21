---
title: "ALTER DATABASE (Transact-SQL)"
description: ALTER DATABASE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: 10/10/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: references_regions
f1_keywords:
  - "ALTER_DATABASE_TSQL"
  - "ALTER DATABASE"
helpviewer_keywords:
  - "databases [SQL Server], modifying"
  - "ALTER DATABASE statement"
  - "databases [SQL Server], renaming"
  - "renaming databases"
  - "database modifications [SQL Server]"
  - "ALTER DATABASE statement, syntax described"
  - "database names [SQL Server], ALTER DATABASE"
  - "modifying databases"
  - "collations [SQL Server], modifying"
  - "database mirroring [SQL Server], Transact-SQL"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-current||=azuresqldb-mi-current||=azure-sqldw-latest||>=aps-pdw-2016"
---
# ALTER DATABASE (Transact-SQL)

Modifies certain configuration options of a database.

This article provides the syntax, arguments, remarks, permissions, and examples for whichever SQL product you choose.

For more information about the syntax conventions, see [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

[!INCLUDE [select-product](../includes/select-product.md)]

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

:::row:::
    :::column:::
        **_\* SQL Server \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: SQL Server

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this statement modifies a database, or the files and filegroups associated with the database. Adds or removes files and filegroups from a database, changes the attributes of a database or its files and filegroups, changes the database collation, and sets database options. Database snapshots cannot be modified. To modify database options associated with replication, use [sp_replicationdboption](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md).

Because of its length, the `ALTER DATABASE` syntax is separated into the multiple articles.

ALTER DATABASE   
The current article provides the syntax and related information for changing the name and the collation of a database.

[ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)   
Provides the syntax and related information for adding and removing files and filegroups from a database, and for changing the attributes of the files and filegroups.

[ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md)   
Provides the syntax and related information for changing the attributes of a database by using the SET options of ALTER DATABASE.

[ALTER DATABASE Database Mirroring](../../t-sql/statements/alter-database-transact-sql-database-mirroring.md)   
Provides the syntax and related information for the SET options of ALTER DATABASE that are related to database mirroring.

[ALTER DATABASE SET HADR](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)   
Provides the syntax and related information for the [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] options of ALTER DATABASE for configuring a secondary database on a secondary replica of an Always On availability group.

[ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md)   
Provides the syntax and related information for the SET options of ALTER DATABASE that are related to database compatibility levels.

[ALTER DATABASE SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)   
Provides the syntax related to database scoped configurations used for individual database level settings such as query optimization and query execution related behaviors.

## Syntax

```syntaxsql
-- SQL Server Syntax
ALTER DATABASE { database_name | CURRENT }
{
    MODIFY NAME = new_database_name
  | COLLATE collation_name
  | <file_and_filegroup_options>
  | SET <option_spec> [ ,...n ] [ WITH <termination> ]
}
[;]

<file_and_filegroup_options>::=
  <add_or_modify_files>::=
  <filespec>::=
  <add_or_modify_filegroups>::=
  <filegroup_updatability_option>::=

<option_spec>::=
{
  | <auto_option>
  | <change_tracking_option>
  | <cursor_option>
  | <database_mirroring_option>
  | <date_correlation_optimization_option>
  | <db_encryption_option>
  | <db_state_option>
  | <db_update_option>
  | <db_user_access_option><delayed_durability_option>
  | <external_access_option>
  | <FILESTREAM_options>
  | <HADR_options>
  | <parameterization_option>
  | <query_store_options>
  | <recovery_option>
  | <service_broker_option>
  | <snapshot_option>
  | <sql_option>
  | <termination>
  | <temporal_history_retention>
  | <data_retention_policy>
  | <compatibility_level>
      { 150 | 140 | 130 | 120 | 110 | 100 | 90 }
}
```

## Arguments

*database_name*
Is the name of the database to be modified.

> [!NOTE]
> This option is not available in a Contained Database.

CURRENT   
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.

Designates that the current database in use should be altered.

MODIFY NAME **=**_new_database_name_   
Renames the database with the name specified as *new_database_name*.

COLLATE *collation_name*   
Specifies the collation for the database. *collation_name* can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the collation of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

> [!NOTE]
> Collation cannot be changed after database has been created on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

When creating databases with other than the default collation, the data in the database always respects the specified collation. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], when creating a contained database, the internal catalog information is maintained using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default collation, **Latin1_General_100_CI_AS_WS_KS_SC**.

For more information about the Windows and SQL collation names, see [COLLATE](~/t-sql/statements/collations.md).

**\<delayed_durability_option> ::=**   
**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.

For more information, see [ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md) and [Control Transaction Durability](../../relational-databases/logs/control-transaction-durability.md).

**\<file_and_filegroup_options>::=**   
For more information, see [ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md).

## Remarks

To remove a database, use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).

To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

The `ALTER DATABASE` statement must run in auto-commit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.

The state of a database file (for example, online or offline), is maintained independently from the state of the database. For more information, see [File States](../../relational-databases/databases/file-states.md). The state of the files within a filegroup determines the availability of the whole filegroup. For a filegroup to be available, all files within the filegroup must be online. If a filegroup is offline, any try to access the filegroup by an SQL statement will fail with an error. When you build query plans for SELECT statements, the query optimizer avoids nonclustered indexes and indexed views that reside in offline filegroups. This enables these statements to succeed. However, if the offline filegroup contains the heap or clustered index of the target table, the SELECT statements fail. Additionally, any `INSERT`, `UPDATE`, or `DELETE` statement that modifies a table with any index in an offline filegroup will fail.

When a database is in the RESTORING state, most `ALTER DATABASE` statements will fail. The exception is setting database mirroring options. A database may be in the RESTORING state during an active restore operation or when a restore operation of a database or log file fails because of a corrupted backup file.

The plan cache for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is cleared by setting one of the following options.

- COLLATE
- MODIFY FILEGROUP DEFAULT
- MODIFY FILEGROUP READ_ONLY
- MODIFY FILEGROUP READ_WRITE
- MODIFY_NAME
- OFFLINE
- ONLINE
- PAGE_VERIFY
- READ_ONLY
- READ_WRITE

Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: `SQL Server has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations`. This message is logged every five minutes as long as the cache is flushed within that time interval.

The plan cache is also flushed in the following scenarios:

- A database has the `AUTO_CLOSE` database option set to ON. When no user connection references or uses the database, the background task tries to close and shut down the database automatically.
- You run several queries against a database that has default options. Then, the database is dropped.
- A database snapshot for a source database is dropped.
- You successfully rebuild the transaction log for a database.
- You restore a database backup.
- You detach a database.

## Changing the Database Collation

Before you apply a different collation to a database, make sure that the following conditions are in place:

- You are the only one currently using the database.
- No schema-bound object depends on the collation of the database.

If the following objects, which depend on the database collation, exist in the database, the ALTER DATABASE*database_name*COLLATE statement will fail. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return an error message for each object blocking the `ALTER` action:

- User-defined functions and views created with SCHEMABINDING
- Computed columns
- CHECK constraints
- Table-valued functions that return tables with character columns with collations inherited from the default database collation
  
Dependency information for non-schema-bound entities is automatically updated when the database collation is changed.

Changing the database collation does not create duplicates among any system names for the database objects. If duplicate names result from the changed collation, the following namespaces may cause the failure of a database collation change:

- Object names such as a procedure, table, trigger, or view
- Schema names
- Principals such as a group, role, or user
- Scalar-type names such as system and user-defined types
- Full-text catalog names
- Column or parameter names within an object
- Index names within a table

Duplicate names resulting from the new collation will cause the change action to fail, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return an error message specifying the namespace where the duplicate was found.

## Viewing Database Information

You can use catalog views, system functions, and system stored procedures to return information about databases, files, and filegroups.

## Permissions

Requires `ALTER` permission on the database.

## Examples

### A. Changing the name of a database

The following example changes the name of the `AdventureWorks2012` database to `Northwind`.

```sql
USE master;
GO
ALTER DATABASE AdventureWorks2012
Modify Name = Northwind ;
GO
```

### B. Changing the collation of a database

The following example creates a database named `testdb` with the `SQL_Latin1_General_CP1_CI_AS` collation, and then changes the collation of the `testdb` database to `COLLATE French_CI_AI`.

**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later.

```sql
USE master;
GO

CREATE DATABASE testdb
COLLATE SQL_Latin1_General_CP1_CI_AS ;
GO

ALTER DATABASE testDB
COLLATE French_CI_AI ;
GO
```

## See also

- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.database_mirroring_witnesses](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)
- [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
- [System Databases](../../relational-databases/databases/system-databases.md)

::: moniker-end
::: moniker range="=azuresqldb-current"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Database \*_** &nbsp;
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: SQL Database

In Azure SQL Database, use this statement to modify a database. Use this statement to change the name of a database, change the edition and service objective of the database, join or remove the database to or from an elastic pool, set database options, add or remove the database as a secondary in a geo-replication relationship, and set the database compatibility level.

Because of its length, the `ALTER DATABASE` syntax is separated into the multiple articles.

ALTER DATABASE   
The current article provides the syntax and related information for changing the name and the collation of a database.

[ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md?view=azuresqldb-current&preserve-view=true)    
Provides the syntax and related information for changing the attributes of a database by using the SET options of ALTER DATABASE.

[ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md?view=azuresqldb-current&preserve-view=true)   
Provides the syntax and related information for the SET options of ALTER DATABASE that are related to database compatibility levels.

## Syntax

```syntaxsql
-- Azure SQL Database Syntax
ALTER DATABASE { database_name | CURRENT }
{
    MODIFY NAME = new_database_name
  | MODIFY ( <edition_options> [, ... n] )
  | MODIFY BACKUP_STORAGE_REDUNDANCY = { 'LOCAL' | 'ZONE' | 'GEO' }
  | SET { <option_spec> [ ,... n ] WITH <termination>}
  | ADD SECONDARY ON SERVER <partner_server_name>
    [WITH ( <add-secondary-option>::=[, ... n] ) ]
  | REMOVE SECONDARY ON SERVER <partner_server_name>
  | FAILOVER
  | FORCE_FAILOVER_ALLOW_DATA_LOSS
}
[;]

<edition_options> ::=
{

  MAXSIZE = { 100 MB | 250 MB | 500 MB | 1 ... 1024 ... 4096 GB }
  | EDITION = { 'Basic' | 'Standard' | 'Premium' | 'GeneralPurpose' | 'BusinessCritical' | 'Hyperscale'}
  | SERVICE_OBJECTIVE =
       { <service-objective>
       | { ELASTIC_POOL (name = <elastic_pool_name>) }
       }
}

<add-secondary-option> ::=
   {
      ALLOW_CONNECTIONS = { ALL | NO }
     | SERVICE_OBJECTIVE =
       { <service-objective>
       | { ELASTIC_POOL ( name = <elastic_pool_name>) }
       | DATABASE_NAME = <target_database_name>
       | SECONDARY_TYPE = { GEO | NAMED }
       }
   }

<service-objective> ::={ 'Basic' |'S0' | 'S1' | 'S2' | 'S3'| 'S4'| 'S6'| 'S7'| 'S9'| 'S12'
       | 'P1' | 'P2' | 'P4'| 'P6' | 'P11' | 'P15'
      | 'GP_Gen4_1' | 'GP_Gen4_2' | 'GP_Gen4_3' | 'GP_Gen4_4' | 'GP_Gen4_5' | 'GP_Gen4_6'
      | 'GP_Gen4_7' | 'GP_Gen4_8' | 'GP_Gen4_9' | 'GP_Gen4_10' | 'GP_Gen4_16' | 'GP_Gen4_24'
      | 'GP_Gen5_2' | 'GP_Gen5_4' | 'GP_Gen5_6' | 'GP_Gen5_8' | 'GP_Gen5_10' | 'GP_Gen5_12' | 'GP_Gen5_14'
      | 'GP_Gen5_16' | 'GP_Gen5_18' | 'GP_Gen5_20' | 'GP_Gen5_24' | 'GP_Gen5_32' | 'GP_Gen5_40' | 'GP_Gen5_80'
      | 'GP_Fsv2_8' | 'GP_Fsv2_10' | 'GP_Fsv2_12' | 'GP_Fsv2_14' | 'GP_Fsv2_16' | 'GP_Fsv2_18'
      | 'GP_Fsv2_20' | 'GP_Fsv2_24' | 'GP_Fsv2_32' | 'GP_Fsv2_36' | 'GP_Fsv2_72'
      | 'GP_S_Gen5_1' | 'GP_S_Gen5_2' | 'GP_S_Gen5_4' | 'GP_S_Gen5_6' | 'GP_S_Gen5_8'
      | 'GP_S_Gen5_10' | 'GP_S_Gen5_12' | 'GP_S_Gen5_14' | 'GP_S_Gen5_16'
      | 'GP_S_Gen5_18' | 'GP_S_Gen5_20' | 'GP_S_Gen5_24' | 'GP_S_Gen5_32' | 'GP_S_Gen5_40'
      | 'BC_Gen4_1' | 'BC_Gen4_2' | 'BC_Gen4_3' | 'BC_Gen4_4' | 'BC_Gen4_5' | 'BC_Gen4_6'
      | 'BC_Gen4_7' | 'BC_Gen4_8' | 'BC_Gen4_9' | 'BC_Gen4_10' | 'BC_Gen4_16' | 'BC_Gen4_24'
      | 'BC_Gen5_2' | 'BC_Gen5_4' | 'BC_Gen5_6' | 'BC_Gen5_8' | 'BC_Gen5_10' | 'BC_Gen5_12' | 'BC_Gen5_14'
      | 'BC_Gen5_16' | 'BC_Gen5_18' | 'BC_Gen5_20' | 'BC_Gen5_24' | 'BC_Gen5_32' | 'BC_Gen5_40' | 'BC_Gen5_80'
      | 'BC_M_8' | 'BC_M_10' | 'BC_M_12' | 'BC_M_14' | 'BC_M_16' | 'BC_M_18'
      | 'BC_M_20' | 'BC_M_24' | 'BC_M_32' | 'BC_M_64' | 'BC_M_128'
      | 'HS_GEN4_1' | 'HS_GEN4_2' | 'HS_GEN4_4' | 'HS_GEN4_8' | 'HS_GEN4_16' | 'HS_GEN4_24'
      | 'HS_GEN5_2' | 'HS_GEN5_4' | 'HS_GEN5_8' | 'HS_GEN5_16' | 'HS_GEN5_24' | 'HS_GEN5_32' | 'HS_GEN5_40' | 'HS_GEN5_80'
      | { ELASTIC_POOL(name = <elastic_pool_name>) }
      }

<option_spec> ::=
{
    <auto_option>
  | <change_tracking_option>
  | <cursor_option>
  | <db_encryption_option>
  | <db_update_option>
  | <db_user_access_option>
  | <delayed_durability_option>
  | <parameterization_option>
  | <query_store_options>
  | <snapshot_option>
  | <sql_option>
  | <target_recovery_time_option>
  | <termination>
  | <temporal_history_retention>
  | <compatibility_level>
    { 150 | 140 | 130 | 120 | 110 | 100 | 90 }

}
```

## Arguments

*database_name*   
Is the name of the database to be modified.

CURRENT   
Designates that the current database in use should be altered.

MODIFY NAME **=**_new_database_name_   
Renames the database with the name specified as *new_database_name*. The following example changes the name of a database `db1` to `db2`:

```sql
ALTER DATABASE db1
    MODIFY Name = db2 ;
```

MODIFY (EDITION **=** ['Basic' \| 'Standard' \| 'Premium' \|'GeneralPurpose' \| 'BusinessCritical' \| 'Hyperscale'])   
Changes the service tier of the database.

The following example changes edition to `Premium`:

```sql
ALTER DATABASE current
    MODIFY (EDITION = 'Premium');
```

> [!IMPORTANT]
> EDITION change fails if the MAXSIZE property for the database is set to a value outside the valid range supported by that edition.

MODIFY (BACKUP_STORAGE_REDUNDANCY **=** ['LOCAL' \| 'ZONE' \| 'GEO'])   
Changes the storage redundancy of point-in-time restore backups and long-term retention backups (if configured) of the database. The changes are applied to all the future backups taken. Existing backups continue to use the previous setting. 

> [!IMPORTANT]
> BACKUP_STORAGE_REDUNDANCY option for Azure SQL Database is available in public preview in Brazil South and generally available in Southeast Asia Azure region only.  

MODIFY (MAXSIZE **=** [100 MB \| 500 MB \| 1 \| 1024...4096] GB)   
Specifies the maximum size of the database. The maximum size must comply with the valid set of values for the EDITION property of the database. Changing the maximum size of the database may cause the database EDITION to be changed.

> [!NOTE]
> The **MAXSIZE** argument does not apply to single databases in the Hyperscale service tier. Hyperscale service tier databases grow as needed, up to 100 TB. The SQL Database service adds storage automatically - you do not need to set a maximum size.

**DTU model**

|**MAXSIZE**|**Basic**|**S0-S2**|**S3-S12**|**P1-P6**|**P11-P15**|
|-----------------|---------------|------------------|-----------------|-----------------|-----------------|-----------------|
|100 MB|√|√|√|√|√|
|250 MB|√|√|√|√|√|
|500 MB|√|√|√|√|√|
|1 GB|√|√|√|√|√|
|2 GB|√ (D)|√|√|√|√|
|5 GB|N/A|√|√|√|√|
|10 GB|N/A|√|√|√|√|
|20 GB|N/A|√|√|√|√|
|30 GB|N/A|√|√|√|√|
|40 GB|N/A|√|√|√|√|
|50 GB|N/A|√|√|√|√|
|100 GB|N/A|√|√|√|√|
|150 GB|N/A|√|√|√|√|
|200 GB|N/A|√|√|√|√|
|250 GB|N/A|√ (D)|√ (D)|√|√|
|300 GB|N/A|√|√|√|√|
|400 GB|N/A|√|√|√|√|
|500 GB|N/A|√|√|√ (D)|√|
|750 GB|N/A|√|√|√|√|
|1024 GB|N/A|√|√|√|√ (D)|
|From 1024 GB up to 4096 GB in increments of 256 GB*|N/A|N/A|N/A|N/A|√|

\* P11 and P15 allow MAXSIZE up to 4 TB with 1024 GB being the default size. P11 and P15 can use up to 4 TB of included storage at no additional charge. In the Premium tier, MAXSIZE greater than 1 TB is currently available in the following regions: US East2, West US, US Gov Virginia, West Europe, Germany Central, South East Asia, Japan East, Australia East, Canada Central, and Canada East. For more details regarding resource limitations for the DTU model, see [DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits).

The MAXSIZE value for the DTU model, if specified, has to be a valid value shown in the table above for the service tier specified.

**vCore model**

**General purpose - provisioned compute - Gen4 (part 1)**

|MAXSIZE|GP_Gen4_1|GP_Gen4_2|GP_Gen4_3|GP_Gen4_4|GP_Gen4_5|GP_Gen4_6|
|:----- | ------: |-------: |-------: |-------: |-------: |--------:|
|Max data size (GB)|1024|1024|1024|1536|1536|1536|

**General purpose - provisioned compute - Gen4 (part 2)**

|MAXSIZE|GP_Gen4_7|GP_Gen4_8|GP_Gen4_9|GP_Gen4_10|GP_Gen4_16|GP_Gen4_24
|:----- | ------: |-------: |-------: |-------: |-------: |--------:|
|Max data size (GB)|1536|3072|3072|3072|4096|4096|

**General purpose - provisioned compute - Gen5 (part 1)**

|MAXSIZE|GP_Gen5_2|GP_Gen5_4|GP_Gen5_6|GP_Gen5_8|GP_Gen5_10|GP_Gen5_12|GP_Gen5_14|
|:----- | ------: |-------: |-------: |-------: |--------: |---------:|--------: |
|Max data size (GB)|1024|1024|1024|1536|1536|1536|1536|

**General purpose - provisioned compute - Gen5 (part 2)**

|MAXSIZE|GP_Gen5_16|GP_Gen5_18|GP_Gen5_20|GP_Gen5_24|GP_Gen5_32|GP_Gen5_40|GP_Gen5_80|
|:----- | ------: |-------: |-------: |-------: |--------: |---------:|--------: |
|Max data size (GB)|3072|3072|3072|4096|4096|4096|4096|

**General purpose - provisioned compute - Fsv2-series (part 1)**

|MAXSIZE|GP_Fsv2_8|GP_Fsv2_10|GP_Fsv2_12|GP_Fsv2_14|GP_Fsv2_16|GP_Fsv2_18|
|:----- | ------: | ------: | ------: | ------: | ------: | ------: |
|Max data size (GB)|1024|1024|1024|1024|1536|1536|

**General purpose - provisioned compute - Fsv2-series (part 2)**

|MAXSIZE|GP_Fsv2_20|GP_Fsv2_24|GP_Fsv2_32|GP_Fsv2_36|GP_Fsv2_72|
|:----- | ------: | ------: | ------: | ------: | ------: |
|Max data size (GB)|1536|1536|3072|3072|4096|

**General purpose - serverless compute - Gen5 (part 1)**

|MAXSIZE|GP_S_Gen5_1|GP_S_Gen5_2|GP_S_Gen5_4|GP_S_Gen5_6|GP_S_Gen5_8|
|:----- | ------: |-------: |-------: |-------: |--------: |
|Max vCores|1|2|4|6|8|

**General purpose - serverless compute - Gen5 (part 2)**

|MAXSIZE|GP_S_Gen5_10|GP_S_Gen5_12|GP_S_Gen5_14|GP_S_Gen5_16|
|:----- | ------: |-------: |-------: |-------: |
|Max vCores|10|12|14|16|

**General purpose - serverless compute - Gen5 (part 3)**

|MAXSIZE|GP_S_Gen5_18|GP_S_Gen5_20|GP_S_Gen5_24|GP_S_Gen5_32|GP_S_Gen5_40|
|:----- | ------: |-------: |-------: |-------: |--------: |
|Max vCores|18|20|24|32|40|

**Business critical - provisioned compute - Gen4 (part 1)**

|Compute size (service objective)|BC_Gen4_1|BC_Gen4_2|BC_Gen4_3|BC_Gen4_4|BC_Gen4_5|BC_Gen4_6|
|:--------------- | ------: |-------: |-------: |-------: |-------: |-------: |
|Max data size (GB)|1024|1024|1024|1024|1024|1024|

**Business critical - provisioned compute - Gen4 (part 2)**

|Compute size (service objective)|BC_Gen4_7|BC_Gen4_8|BC_Gen4_9|BC_Gen4_10|BC_Gen4_16|BC_Gen4_24|
|:--------------- | ------: |-------: |-------: |--------: |--------: |--------: |
|Max data size (GB)|1024|1024|1024|1024|1024|1024|

**Business critical - provisioned compute - Gen5 (part 1)**

|MAXSIZE|BC_Gen5_2|BC_Gen5_4|BC_Gen5_6|BC_Gen5_8|BC_Gen5_10|BC_Gen5_12|BC_Gen5_14|
|:----- | ------: |-------: |-------: |-------: |---------: |--------:|--------: |
|Max data size (GB)|1024|1024|1024|1536|1536|1536|1536|

**Business critical - provisioned compute - Gen5 (part 2)**

|MAXSIZE|BC_Gen5_16|BC_Gen5_18|BC_Gen5_20|BC_Gen5_24|BC_Gen5_32|BC_Gen5_40|BC_Gen5_80|
|:----- | -------: |--------: |--------: |--------: |--------: |---------:|--------: |
|Max data size (GB)|3072|3072|3072|4096|4096|4096|4096|

**Business critical - provisioned compute - M-series (part 1)**

|MAXSIZE|BC_M_8|BC_M_10|BC_M_12|BC_M_14|BC_M_16|BC_M_18|
|:----- | -------: | -------: | -------: | -------: | -------: | -------: |
|Max data size (GB)|512|640|768|896|1024|1152|

**Business critical - provisioned compute - M-series (part 2)**

|MAXSIZE|BC_M_20|BC_M_24|BC_M_32|BC_M_64|BC_M_128|
|:----- | -------: | -------: | -------: | -------: | -------: |
|Max data size (GB)|1280|1536|2048|4096|4096|

If no `MAXSIZE`value is set when using the vCore model, the default is 32 GB. For more details regarding resource limitations for vCore model, see [vCore resource limits](/azure/sql-database/sql-database-dtu-resource-limits).

The following rules apply to MAXSIZE and EDITION arguments:

- If EDITION is specified but MAXSIZE is not specified, the default value for the edition is used. For example, is the EDITION is set to Standard, and the MAXSIZE is not specified, then the MAXSIZE is automatically set to 250 MB.
- If neither MAXSIZE nor EDITION is specified, the EDITION is set to General Purpose, and MAXSIZE is set to 32 GB.

MODIFY (SERVICE_OBJECTIVE = \<service-objective>)   
Specifies the compute size (service objective). The following example changes service objective of a Premium database to `P6`:

```sql
ALTER DATABASE current
    MODIFY (SERVICE_OBJECTIVE = 'P6');
```

SERVICE_OBJECTIVE   

- **For single and pooled databases**

  - Specifies the compute size (service objective). Available values for service objective are: `S0`, `S1`, `S2`, `S3`, `S4`, `S6`, `S7`, `S9`, `S12`, `P1`, `P2`, `P4`, `P6`, `P11`, `P15`, `GP_GEN4_1`, `GP_GEN4_2`, `GP_GEN4_3`, `GP_GEN4_4`, `GP_GEN4_5`, `GP_GEN4_6`, `GP_GEN4_7`, `GP_GEN4_8`, `GP_GEN4_7`, `GP_GEN4_8`, `GP_GEN4_9`, `GP_GEN4_10`, `GP_GEN4_16`, `GP_GEN4_24`, `BC_GEN4_1`, `BC_GEN4_2`, `BC_GEN4_3`, `BC_GEN4_4`, `BC_GEN4_5`, `BC_GEN4_6`, `BC_GEN4_7`, `BC_GEN4_8`, `BC_GEN4_9`, `BC_GEN4_10`, `BC_GEN4_16`, `BC_GEN4_24`, `GP_Gen5_2`, `GP_Gen5_4`, `GP_Gen5_6`, `GP_Gen5_8`, `GP_Gen5_10`, `GP_Gen5_12`, `GP_Gen5_14`, `GP_Gen5_16`, `GP_Gen5_18`, `GP_Gen5_20`, `GP_Gen5_24`, `GP_Gen5_32`, `GP_Gen5_40`, `GP_Gen5_80`, `GP_Fsv2_8`, `GP_Fsv2_10`, `GP_Fsv2_12`, `GP_Fsv2_14`, `GP_Fsv2_16`, `GP_Fsv2_18`, `GP_Fsv2_20`, `GP_Fsv2_24`, `GP_Fsv2_32`, `GP_Fsv2_36`, `GP_Fsv2_72`, `BC_Gen5_2`, `BC_Gen5_4`, `BC_Gen5_6`, `BC_Gen5_8`, `BC_Gen5_10`, `BC_Gen5_12`, `BC_Gen5_14`, `BC_Gen5_16`, `BC_Gen5_18`, `BC_Gen5_20`, `BC_Gen5_24`, `BC_Gen5_32`,`BC_Gen5_40`, `BC_Gen5_80`, `BC_M_8`, `BC_M_10`, `BC_M_12`, `BC_M_14`, `BC_M_16`, `BC_M_18`, `BC_M_20`, `BC_M_24`, `BC_M_32`, `BC_M_64`, `BC_M_128`.

- **For single databases in the Serverless compute tier**

  - Specifies the compute size (service objective). Available values for service objective are: `GP_S_Gen5_1`, `GP_S_Gen5_2`, `GP_S_Gen5_4`, `GP_S_Gen5_6`, `GP_S_Gen5_8`, `GP_S_Gen5_10`, `GP_S_Gen5_12`, `GP_S_Gen5_14`, `GP_S_Gen5_16`, `GP_S_Gen5_18`, `GP_S_Gen5_20`, `GP_S_Gen5_24`, `GP_S_Gen5_32`, `GP_S_Gen5_40`.

- **For single databases in the Hyperscale service tier**

  - Specifies the compute size (service objective). Available values for service objective are: `HS_GEN4_1` `HS_GEN4_2` `HS_GEN4_4` `HS_GEN4_8` `HS_GEN4_16`, `HS_GEN4_24`, `HS_Gen5_2`, `HS_Gen5_4`, `HS_Gen5_8`, `HS_Gen5_16`, `HS_Gen5_24`, `HS_Gen5_32`, `HS_Gen5_48`, `HS_Gen5_80`.

DATABASE_NAME
Only for Azure SQL Database Hyperscale. The database name that will be created. Only used by Azure SQL Database Hyperscale named replicas, when `SECONDARY_TYPE` = NAMED. For more information, see [Hyperscale Secondary Replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas).

SECONDARY_TYPE 
Only for Azure SQL Database Hyperscale. **GEO** specifies a geo-replica, **NAMED** specifies a named replica. Default is **GEO**. For more information, see [Hyperscale Secondary Replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas).

For service objective descriptions and more information about the size, editions, and the service objectives combinations, see [Azure SQL Database Service Tiers and Performance Levels](/azure/azure-sql/database/purchasing-models), [DTU resource limits](/azure/sql-database/sql-database-dtu-resource-limits) and [vCore resource limits](/azure/sql-database/sql-database-dtu-resource-limits). Support for PRS service objectives have been removed. For questions, use this e-mail alias: premium-rs@microsoft.com.

MODIFY (SERVICE_OBJECTIVE = ELASTIC\_POOL (name = \<elastic_pool_name>)   
To add an existing database to an elastic pool, set the SERVICE_OBJECTIVE of the database to ELASTIC_POOL and provide the name of the elastic pool. You can also use this option to change the database to a different elastic pool within the same server. For more information, see [Create and manage a SQL Database elastic pool](/azure/azure-sql/database/elastic-pool-overview). To remove a database from an elastic pool, use ALTER DATABASE to set the SERVICE_OBJECTIVE to a single database compute size (service objective).

> [!NOTE]
> Databases in the Hyperscale service tier cannot be added to an elastic pool.

ADD SECONDARY ON SERVER \<partner_server_name>   
Creates a geo-replication secondary database with the same name on a partner server, making the local database into a geo-replication primary, and begins asynchronously replicating data from the primary to the new secondary. If a database with the same name already exists on the secondary, the command fails. The command is executed on the `master` database on the server hosting the local database that becomes the primary.

> [!IMPORTANT]
> By default, the secondary database is created with the same backup storage redundancy as that of the primary or source database. Changing the backup storage redundancy while creating the secondary is not supported via T-SQL. 

WITH ALLOW_CONNECTIONS { **ALL** | NO }   
When ALLOW_CONNECTIONS is not specified, it is set to ALL by default. If it is set ALL, it is a read-only database that allows all logins with the appropriate permissions to connect.

WITH SERVICE_OBJECTIVE { `S0`, `S1`, `S2`, `S3`, `S4`, `S6`, `S7`, `S9`, `S12`, `P1`, `P2`, `P4`, `P6`, `P11`, `P15`, `GP_GEN4_1`, `GP_GEN4_2`, `GP_GEN4_3`, `GP_GEN4_4`, `GP_GEN4_5`, `GP_GEN4_6`, `GP_GEN4_7`, `GP_GEN4_8`, `GP_GEN4_7`, `GP_GEN4_8`, `GP_GEN4_9`, `GP_GEN4_10`, `GP_GEN4_16`, `GP_GEN4_24`, `BC_GEN4_1`, `BC_GEN4_2`, `BC_GEN4_3`, `BC_GEN4_4`, `BC_GEN4_5`, `BC_GEN4_6`, `BC_GEN4_7`, `BC_GEN4_8`, `BC_GEN4_9`, `BC_GEN4_10`, `BC_GEN4_16`, `BC_GEN4_24`, `GP_Gen5_2`, `GP_Gen5_4`, `GP_Gen5_6`, `GP_Gen5_8`, `GP_Gen5_10`, `GP_Gen5_12`, `GP_Gen5_14`, `GP_Gen5_16`, `GP_Gen5_18`, `GP_Gen5_20`, `GP_Gen5_24`, `GP_Gen5_32`, `GP_Gen5_40`, `GP_Gen5_80`, `GP_Fsv2_8`, `GP_Fsv2_10`, `GP_Fsv2_12`, `GP_Fsv2_14`, `GP_Fsv2_16`, `GP_Fsv2_18`, `GP_Fsv2_20`, `GP_Fsv2_24`, `GP_Fsv2_32`, `GP_Fsv2_36`, `GP_Fsv2_72`, `GP_S_Gen5_1`, `GP_S_Gen5_2`, `GP_S_Gen5_4`, `GP_S_Gen5_6`, `GP_S_Gen5_8`, `GP_S_Gen5_10`, `GP_S_Gen5_12`, `GP_S_Gen5_14`, `GP_S_Gen5_16`, `GP_S_Gen5_18`, `GP_S_Gen5_20`, `GP_S_Gen5_24`, `GP_S_Gen5_32`, `GP_S_Gen5_40`, `BC_Gen5_2`, `BC_Gen5_4`, `BC_Gen5_6`, `BC_Gen5_8`, `BC_Gen5_10`, `BC_Gen5_12`, `BC_Gen5_14`, `BC_Gen5_16`, `BC_Gen5_18`, `BC_Gen5_20`, `BC_Gen5_24`, `BC_Gen5_32`,`BC_Gen5_40`, `BC_Gen5_80`, `BC_M_8`, `BC_M_10`, `BC_M_12`, `BC_M_14`, `BC_M_16`, `BC_M_18`, `BC_M_20`, `BC_M_24`, `BC_M_32`, `BC_M_64`, `BC_M_128` }

When SERVICE_OBJECTIVE is not specified, the secondary database is created at the same service level as the primary database. When SERVICE_OBJECTIVE is specified, the secondary database is created at the specified level. This option supports creating geo-replicated secondaries with less expensive service levels. The SERVICE_OBJECTIVE specified must be within the same edition as the source. For example, you cannot specify S0 if the edition is premium.

ELASTIC_POOL (name = \<elastic_pool_name>)
When ELASTIC_POOL is not specified, the secondary database is not created in an elastic pool. When ELASTIC_POOL is specified, the secondary database is created in the specified pool.

> [!IMPORTANT]
> The user executing the ADD SECONDARY command must be DBManager on primary server, have db_owner membership in local database, and DBManager on secondary server. The client IP address must be added to the allowed list under firewall rules for both the primary and secondary servers. In case of different client IP addresses, the exact same client IP address that has been added on the primary server must also be added to the secondary. This is a required step to be done before running the ADD SECONDARY command to initiate geo-replication.

REMOVE SECONDARY ON SERVER \<partner_server_name>
Removes the specified geo-replicated secondary database on the specified server. The command is executed on the `master` database on the server hosting the primary database.

> [!IMPORTANT]
> The user executing the `REMOVE SECONDARY` command must be DBManager on the primary server.

FAILOVER
Promotes the secondary database in geo-replication partnership on which the command is executed to become the primary and demotes the current primary to become the new secondary. As part of this process, the geo-replication mode is temporarily switched from asynchronous mode to synchronous mode. During the failover process:

1. The primary stops taking new transactions.
2. All outstanding transactions are flushed to the secondary.
3. The secondary becomes the primary and begins asynchronous geo-replication with the old primary / the new secondary.

This sequence ensures that no data loss occurs. The period during which both databases are unavailable is on the order of 0-25 seconds while the roles are switched. The total operation should take no longer than about one minute. If the primary database is unavailable when this command is issued, the command fails with an error message indicating that the primary database is not available. If the failover process does not complete and appears stuck, you can use the force failover command and accept data loss - and then, if you need to recover the lost data, call devops (CSS) to recover the lost data.

> [!IMPORTANT]
> The user executing the FAILOVER command must be DBManager on both the primary server and the secondary server.

FORCE_FAILOVER_ALLOW_DATA_LOSS
Promotes the secondary database in geo-replication partnership on which the command is executed to become the primary and demotes the current primary to become the new secondary. Use this command only when the current primary is no longer available. It is designed for disaster recovery only, when restoring availability is critical, and some data loss is acceptable.

During a forced failover:

1. The specified secondary database immediately becomes the primary database and begins accepting new transactions.
2. When the original primary can reconnect with the new primary, an incremental backup is taken on the original primary, and the original primary becomes a new secondary.
3. To recover data from this incremental backup on the old primary, the user engages devops/CSS.
4. If there are additional secondaries, they are automatically reconfigured to become secondaries of the new primary. This process is asynchronous and there may be a delay until this process completes. Until the reconfiguration has completed, the secondaries continue to be secondaries of the old primary.

> [!IMPORTANT]
> The user executing the `FORCE_FAILOVER_ALLOW_DATA_LOSS` command must be belong to the `dbmanager` role on both the primary server and the secondary server.

## Remarks

To remove a database, use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).
To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

The `ALTER DATABASE` statement must run in auto-commit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.

Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: `SQL Server has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations`. This message is logged every five minutes as long as the cache is flushed within that time interval.

The procedure cache is also flushed in the following scenario: You run several queries against a database that has default options. Then, the database is dropped.

## Viewing Database Information

You can use catalog views, system functions, and system stored procedures to return information about databases, files, and filegroups.

## Permissions

To alter a database a login must be either the server admin login (created when the Azure SQL Database logical server was provisioned), the Azure AD admin of the server, a member of the dbmanager database role in `master`, a member of the db_owner database role in the current database, or `dbo` of the database.

To scale databases via T-SQL, ALTER DATABASE permissions are needed.  To scale databases via the Azure portal, PowerShell, Azure CLI, or REST API, Azure RBAC permissions are needed, specifically the Contributor, SQL DB Contributor role, or SQL Server Contributor Azure RBAC roles. For more information, visit [Azure RBAC built-in roles](/azure/role-based-access-control/built-in-roles).


## Examples

### A. Check the edition options and change them

Sets an edition and max size for database db1:

```sql
SELECT Edition = DATABASEPROPERTYEX('db1', 'EDITION'),
        ServiceObjective = DATABASEPROPERTYEX('db1', 'ServiceObjective'),
        MaxSizeInBytes =  DATABASEPROPERTYEX('db1', 'MaxSizeInBytes');

ALTER DATABASE [db1] MODIFY (EDITION = 'Premium', MAXSIZE = 1024 GB, SERVICE_OBJECTIVE = 'P15');
```

### B. Moving a database to a different elastic pool

Moves an existing database into a pool named pool1:

```sql
ALTER DATABASE db1
MODIFY ( SERVICE_OBJECTIVE = ELASTIC_POOL ( name = pool1 ) ) ;
```

### C. Add a Geo-Replication Secondary

Creates a readable secondary database db1 on server `secondaryserver` of the db1 on the local server.

```sql
ALTER DATABASE db1
ADD SECONDARY ON SERVER secondaryserver
WITH ( ALLOW_CONNECTIONS = ALL )
```

### D. Remove a Geo-Replication Secondary

Removes the secondary database db1 on server `secondaryserver`.

```sql
ALTER DATABASE db1
REMOVE SECONDARY ON SERVER testsecondaryserver
```

### E. Failover to a Geo-Replication Secondary

Promotes a secondary database db1 on server `secondaryserver` to become the new primary database when executed on server `secondaryserver`.

```sql
ALTER DATABASE db1 FAILOVER
```

### E. Force Failover to a Geo-Replication Secondary with data loss

Forces a secondary database db1 on server `secondaryserver` to become the new primary database when executed on server `secondaryserver`, in the event that the primary server becomes unavailable. This option may incur data loss.

```sql
ALTER DATABASE db1 FORCE_FAILOVER_ALLOW_DATA_LOSS
```

### G. Update a single database to service tier S0 (Standard edition, performance level 0)

Updates a single database to the Standard edition (service tier) with a compute size (service objective) of S0 and a maximum size of 250 GB.

```sql
ALTER DATABASE [db1] MODIFY (EDITION = 'Standard', MAXSIZE = 250 GB, SERVICE_OBJECTIVE = 'S0');
```

### H. Update the backup storage redundancy of a database

Updates the backup storage redundancy of a database to zone-redundant. All future backups of this database will use the new setting. This includes point-in-time restore backups and long-term retention backups (if configured). 

```sql
ALTER DATABASE db1 MODIFY BACKUP_STORAGE_REDUNDANCY = 'ZONE'
```

## See also

- [CREATE DATABASE - Azure SQL Database](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.database_mirroring_witnesses](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)
- [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
- [System Databases](../../relational-databases/databases/system-databases.md)

::: moniker-end
::: moniker range="=azuresqldb-mi-current"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* SQL Managed Instance \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::



&nbsp;

## Overview: Azure SQL Managed Instance

In [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], use this statement to set database options.

Because of its length, the `ALTER DATABASE` syntax is separated into the multiple articles.

ALTER DATABASE   
The current article provides the syntax and related information for setting file and filegroup options, for setting database options, and for setting the database compatibility level.  
  
[ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md?&tabs=sqldbmi)   
Provides the syntax and related information for adding and removing files and filegroups from a database, and for changing the attributes of the files and filegroups.  
  
[ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md?&tabs=sqldbmi)   
Provides the syntax and related information for changing the attributes of a database by using the SET options of ALTER DATABASE.  
  
[ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md?&tabs=sqldbmi)   
Provides the syntax and related information for the SET options of ALTER DATABASE that are related to database compatibility levels.  

## Syntax

```syntaxsql
-- Azure SQL Database Syntax  
ALTER DATABASE { database_name | CURRENT }  
{
    MODIFY NAME = new_database_name
  | COLLATE collation_name
  | <file_and_filegroup_options>  
  | SET <option_spec> [ ,...n ]  
}  
[;]

<file_and_filegroup_options>::=  
  <add_or_modify_files>::=  
  <filespec>::=
  <add_or_modify_filegroups>::=  
  <filegroup_updatability_option>::=  

<option_spec> ::=
{
    <auto_option>
  | <change_tracking_option>
  | <cursor_option>
  | <db_encryption_option>  
  | <db_update_option>
  | <db_user_access_option>
  | <delayed_durability_option>
  | <parameterization_option>
  | <query_store_options>
  | <snapshot_option>
  | <sql_option>
  | <target_recovery_time_option>
  | <temporal_history_retention>
  | <compatibility_level>
      { 150 | 140 | 130 | 120 | 110 | 100 | 90 }

}  

```

## Arguments

*database_name*   
Is the name of the database to be modified.

CURRENT   
Designates that the current database in use should be altered.

## Remarks

- To remove a database, use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).

- To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).

- The `ALTER DATABASE` statement must run in auto-commit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.

- The plan cache for the Managed Instance is cleared by setting one of the following options.
    - COLLATE
    - MODIFY FILEGROUP DEFAULT
    - MODIFY FILEGROUP READ_ONLY
    - MODIFY FILEGROUP READ_WRITE
    - MODIFY NAME

    Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: " [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.
The plan cache is also flushed when several queries are executed against a database that has default options. Then, the database is dropped.

- Some `ALTER DATABASE` statements require exclusive lock on a database to be executed. This is why they might fail when another active process is holding a lock on the database. Error that is reported in a case like this is `Msg 5061, Level 16, State 1, Line 38` with message `ALTER DATABASE failed because a lock could not be placed on database '<database name>'. Try again later`. This is typically a transient failure and to resolve it, once all locks on the database are released, retry the `ALTER DATABASE` statement that failed. System view `sys.dm_tran_locks` holds information on active locks. To check if there are shared or exclusive locks on a database use following query.
  
    ```sql
    SELECT
        resource_type, resource_database_id, request_mode, request_type, request_status, request_session_id 
    FROM 
        sys.dm_tran_locks
    WHERE
        resource_database_id = DB_ID('testdb')
    ```
  
## Viewing database information

You can use catalog views, system functions, and system stored procedures to return information about databases, files, and filegroups.

## Permissions

Only the server-level principal login (created by the provisioning process) or members of the `dbcreator` database role can alter a database.

> [!IMPORTANT]
> The owner of the database cannot alter the database unless they are a member of the `dbcreator` role.

## Examples

The following examples show you how to set automatic tuning and how to add a file in a managed instance.

```sql
ALTER DATABASE WideWorldImporters
  SET AUTOMATIC_TUNING ( FORCE_LAST_GOOD_PLAN = ON);

ALTER DATABASE WideWorldImporters
  ADD FILE (NAME = 'data_17');
```

## See also

- [CREATE DATABASE - Azure SQL Database](../../t-sql/statements/create-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
- [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)
- [EVENTDATA](../../t-sql/functions/eventdata-transact-sql.md)
- [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)
- [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.database_mirroring_witnesses](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)
- [sys.data_spaces](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)
- [sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)
- [sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)
- [System Databases](../../relational-databases/databases/system-databases.md)

::: moniker-end
::: moniker range="=azure-sqldw-latest"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Azure Synapse<br />Analytics \*_** &nbsp;
    :::column-end:::
    :::column:::
        [Analytics Platform<br />System (PDW)](alter-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: Azure Synapse Analytics

In Azure Synapse, `ALTER DATABASE` modifies certain configuration options of a dedicated SQL pool.

Because of its length, the `ALTER DATABASE` syntax is separated into the multiple articles. 

[ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md) provides the syntax and related information for changing the attributes of a database by using the SET options of `ALTER DATABASE`.

## Syntax

### [Dedicated SQL pool](#tab/sqlpool)
```syntaxsql
ALTER DATABASE { database_name | CURRENT }
{
  MODIFY NAME = new_database_name
| MODIFY ( <edition_option> [, ... n] )
| SET <option_spec> [ ,...n ] [ WITH <termination> ]
}
[;]

<edition_option> ::=
      MAXSIZE = {
            250 | 500 | 750 | 1024 | 5120 | 10240 | 20480
          | 30720 | 40960 | 51200 | 61440 | 71680 | 81920
          | 92160 | 102400 | 153600 | 204800 | 245760
      } GB
      | SERVICE_OBJECTIVE = {
            'DW100' | 'DW200' | 'DW300' | 'DW400' | 'DW500'
          | 'DW600' | 'DW1000' | 'DW1200' | 'DW1500' | 'DW2000'
          | 'DW3000' | 'DW6000' | 'DW500c' | 'DW1000c' | 'DW1500c'
          | 'DW2000c' | 'DW2500c' | 'DW3000c' | 'DW5000c' | 'DW6000c'
          | 'DW7500c' | 'DW10000c' | 'DW15000c' | 'DW30000c'
      }
```
### [Serverless SQL pool](#tab/sqlod)
```syntaxsql
ALTER DATABASE { database_name | Current } 
{ 
    COLLATE collation_name 
  | SET { <optionspec> [ ,...n ] } 
} 
[;] 

<optionspec> ::= 
{ 
    <auto_option> 
  | <sql_option> 
}  

<auto_option> ::= 
{ 
    AUTO_CREATE_STATISTICS { OFF | ON [ ( INCREMENTAL = { ON | OFF } ) ] } 
  | AUTO_UPDATE_STATISTICS { ON | OFF } 
  | AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF } 
} 

<sql_option> ::= 
{ 
    ANSI_NULL_DEFAULT { ON | OFF } 
  | ANSI_NULLS { ON | OFF } 
  | ANSI_PADDING { ON | OFF } 
  | ANSI_WARNINGS { ON | OFF } 
  | ARITHABORT { ON | OFF } 
  | COMPATIBILITY_LEVEL = { 140 | 130 | 120 | 110 | 100 } 
  | CONCAT_NULL_YIELDS_NULL { ON | OFF } 
  | NUMERIC_ROUNDABORT { ON | OFF } 
  | QUOTED_IDENTIFIER { ON | OFF } 
} 
```
---

## Arguments

#### *database_name*   
Specifies the name of the database to be modified.

#### MODIFY NAME = *new_database_name*   
Renames the database with the name specified as *new_database_name*. 
 
The 'MODIFY NAME' option has some support limitations in Azure Synapse:
 - Unsupported with Azure Synapse serverless pools
 - Unsupported with dedicated SQL pools created in your Azure Synapse Workspace
 - Supported with dedicated SQL pools (formerly SQL DW) created via the [Azure portal](https://portal.azure.com/#create/Microsoft.SQLDataWarehouse), including those with a [connected workspace](/azure/synapse-analytics/sql-data-warehouse/workspace-connected-create)


#### MAXSIZE   
The default is 245,760 GB (240 TB).

**Applies to:** Optimized for Compute Gen1

The maximum allowable size for the database. The database cannot grow beyond MAXSIZE.

**Applies to:** Optimized for Compute Gen2

The maximum allowable size for rowstore data in the database. Data stored in rowstore tables, a columnstore index's deltastore, or a nonclustered index on a clustered columnstore index cannot grow beyond MAXSIZE. Data compressed into columnstore format does not have a size limit and is not constrained by MAXSIZE.

#### SERVICE_OBJECTIVE   
Specifies the compute size (service objective). For more information about service objectives for Azure Synapse, see [Data Warehouse Units (DWUs)](/azure/sql-data-warehouse/what-is-a-data-warehouse-unit-dwu-cdwu).

## Permissions

Requires these permissions:

- Server-level principal login (the one created by the provisioning process), or
- Member of the `dbmanager` database role.

The owner of the database cannot alter the database unless the owner is a member of the `dbmanager` role.

## General Remarks

The current database must be a different database than the one you are altering, therefore ALTER must be run while connected to the `master` database.

COMPATIBILITY_LEVEL in SQL Analytics is set to 130 by default and cannot be changed. For more details, see [Improved Query Performance with Compatibility Level 130 in Azure SQL Database](./alter-database-transact-sql-compatibility-level.md).

> [!NOTE]
> COMPATIBILITY_LEVEL applies to provisioned resources (pools) only.

## Limitations and Restrictions

To run `ALTER DATABASE`, the database must be online and cannot be in a paused state.

The `ALTER DATABASE` statement must run in auto-commit mode, which is the default transaction management mode. This is set in the connection settings.

The `ALTER DATABASE` statement cannot be part of a user-defined transaction.

You cannot change the database collation.

## Examples

Before you run these examples, make sure the database you are altering is not the current database. The current database must be a different database than the one you are altering, therefore ALTER must be run while connected to the `master` database.

### A. Change the name of the database

```sql
ALTER DATABASE AdventureWorks2012
MODIFY NAME = Northwind;
```

### B. Change max size for the database

```sql
ALTER DATABASE dw1 MODIFY ( MAXSIZE=10240 GB );
```

### C. Change the compute size (service objective)

```sql
ALTER DATABASE dw1 MODIFY ( SERVICE_OBJECTIVE= 'DW1200' );
```

### D. Change the max size and the compute size (service objective)

```sql
ALTER DATABASE dw1 MODIFY ( MAXSIZE=10240 GB, SERVICE_OBJECTIVE= 'DW1200' );
```

## See also

- [CREATE DATABASE (Azure Synapse Analytics)](../../t-sql/statements/create-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
- [Azure Synapse Analytics list of reference articles](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-reference-tsql-language-elements)

::: moniker-end
::: moniker range=">=aps-pdw-2016"

:::row:::
    :::column:::
        [SQL Server](alter-database-transact-sql.md?view=sql-server-ver15&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Database](alter-database-transact-sql.md?view=azuresqldb-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [SQL Managed Instance](alter-database-transact-sql.md?view=azuresqldb-mi-current&preserve-view=true)
    :::column-end:::
    :::column:::
        [Azure Synapse<br />Analytics](alter-database-transact-sql.md?view=azure-sqldw-latest&preserve-view=true)
    :::column-end:::
    :::column:::
        **_\* Analytics<br />Platform System (PDW) \*_** &nbsp;
    :::column-end:::
:::row-end:::

&nbsp;

## Overview: Analytics Platform System

Modifies the maximum database size options for replicated tables, distributed tables, and the transaction log in PDW. Use this statement to manage disk space allocations for a database as it grows or shrinks in size. The article also describes syntax related to setting database options in PDW.

## Syntax

```syntaxsql
-- Analytics Platform System
ALTER DATABASE database_name
    SET ( <set_database_options> | <db_encryption_option> )
[;]

<set_database_options> ::=
{
    AUTOGROW = { ON | OFF }
    | REPLICATED_SIZE = size [GB]
    | DISTRIBUTED_SIZE = size [GB]
    | LOG_SIZE = size [GB]
    | SET AUTO_CREATE_STATISTICS { ON | OFF }
    | SET AUTO_UPDATE_STATISTICS { ON | OFF }
    | SET AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }
}

<db_encryption_option> ::=
    ENCRYPTION { ON | OFF }
```

## Arguments

*database_name*   
The name of the database to be modified. To display a list of databases on the appliance, use [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).

AUTOGROW = { ON | OFF }   
Updates the AUTOGROW option. When AUTOGROW is ON, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] automatically increases the allocated space for replicated tables, distributed tables, and the transaction log as necessary to accommodate growth in storage requirements. When AUTOGROW is OFF, [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] returns an error if replicated tables, distributed tables, or the transaction log exceeds the maximum size setting.

REPLICATED_SIZE = *size* [GB]   
Specifies the new maximum gigabytes per Compute node for storing all of the replicated tables in the database being altered. If you are planning for appliance storage space, you will need to multiply REPLICATED_SIZE by the number of Compute nodes in the appliance.

DISTRIBUTED_SIZE = *size* [GB]   
Specifies the new maximum gigabytes per database for storing all of the distributed tables in the database being altered. The size is distributed across all of the Compute nodes in the appliance.

LOG_SIZE = *size* [GB]   
Specifies the new maximum gigabytes per database for storing all of the transaction logs in the database being altered. The size is distributed across all of the Compute nodes in the appliance.

ENCRYPTION { ON | OFF }   
Sets the database to be encrypted (ON) or not encrypted (OFF). Encryption can only be configured for [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] when [sp_pdw_database_encryption](../../relational-databases/system-stored-procedures/sp-pdw-database-encryption-sql-data-warehouse.md) has been set to **1**. A database encryption key must be created before transparent data encryption can be configured. For more information about database encryption, see [Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md).

SET AUTO_CREATE_STATISTICS { ON | OFF }   
When the automatic create statistics option, AUTO_CREATE_STATISTICS, is ON, the Query Optimizer creates statistics on individual columns in the query predicate, as necessary, to improve cardinality estimates for the query plan. These single-column statistics are created on columns that do not already have a histogram in an existing statistics object.

Default is ON for new databases created after upgrading to AU7. The default is OFF for databases created prior to the upgrade.

For more information about statistics, see [Statistics](../../relational-databases/statistics/statistics.md)

SET AUTO_UPDATE_STATISTICS { ON | OFF }   
When the automatic update statistics option, AUTO_UPDATE_STATISTICS, is ON, the query optimizer determines when statistics might be out-of-date and then updates them when they are used by a query. Statistics become out-of-date after operations insert, update, delete, or merge change the data distribution in the table or indexed view. The query optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.

Default is ON for new databases created after upgrading to AU7. The default is OFF for databases created prior to the upgrade.

For more information about statistics, see [Statistics](../../relational-databases/statistics/statistics.md).

SET AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }   
The asynchronous statistics update option, AUTO_UPDATE_STATISTICS_ASYNC, determines whether the Query Optimizer uses synchronous or asynchronous statistics updates. The AUTO_UPDATE_STATISTICS_ASYNC option applies to statistics objects created for indexes, single columns in query predicates, and statistics created with the `CREATE STATISTICS` statement.

Default is ON for new databases created after upgrading to AU7. The default is OFF for databases created prior to the upgrade.

For more information about statistics, see [Statistics](../../relational-databases/statistics/statistics.md).

## Permissions

Requires the `ALTER` permission on the database.

## Error Messages

If auto-stats is disabled and you try to alter the statistics settings, PDW outputs the error `This option is not supported in PDW`. The system administrator can enable auto-stats by enabling the feature switch [AutoStatsEnabled](../../analytics-platform-system/appliance-feature-switch.md).

## General Remarks

The values for `REPLICATED_SIZE`, `DISTRIBUTED_SIZE`, and `LOG_SIZE` can be greater than, equal to, or less than the current values for the database.

## Limitations and Restrictions

Grow and shrink operations are approximate. The resulting actual sizes can vary from the size parameters.

[!INCLUDE[ssPDW](../../includes/sspdw-md.md)] does not perform the `ALTER DATABASE` statement as an atomic operation. If the statement is aborted during execution, changes that have already occurred will remain.

The statistics settings only work if the administrator has enable auto-stats. If you are an administrator, use the feature switch [AutoStatsEnabled](../../analytics-platform-system/appliance-feature-switch.md) to enable or disable auto-stats.

## Locking Behavior

Takes a shared lock on the DATABASE object. You cannot alter a database that is in use by another user for reading or writing. This includes sessions that have issued a [USE](../language-elements/use-transact-sql.md) statement on the database.

## Performance

Shrinking a database can take a large amount of time and system resources, depending on the size of the actual data within the database, and the amount of fragmentation on disk. For example, shrinking a database could take several hours or more.

## Determining Encryption Progress

Use the following query to determine progress of database transparent data encryption as a percent:

```sql
WITH
database_dek AS (
    SELECT ISNULL(db_map.database_id, dek.database_id) AS database_id,
        dek.encryption_state, dek.percent_complete,
        dek.key_algorithm, dek.key_length, dek.encryptor_thumbprint,
        type
    FROM sys.dm_pdw_nodes_database_encryption_keys AS dek
    INNER JOIN sys.pdw_nodes_pdw_physical_databases AS node_db_map
        ON dek.database_id = node_db_map.database_id
        AND dek.pdw_node_id = node_db_map.pdw_node_id
    LEFT JOIN sys.pdw_database_mappings AS db_map
        ON node_db_map .physical_name = db_map.physical_name
    INNER JOIN sys.dm_pdw_nodes nodes
        ON nodes.pdw_node_id = dek.pdw_node_id
    WHERE dek.encryptor_thumbprint <> 0x
),
dek_percent_complete AS (
    SELECT database_dek.database_id, AVG(database_dek.percent_complete) AS percent_complete
    FROM database_dek
    WHERE type = 'COMPUTE'
    GROUP BY database_dek.database_id
)
SELECT DB_NAME( database_dek.database_id ) AS name,
    database_dek.database_id,
    ISNULL(
       (SELECT TOP 1 dek_encryption_state.encryption_state
        FROM database_dek AS dek_encryption_state
        WHERE dek_encryption_state.database_id = database_dek.database_id
        ORDER BY (CASE encryption_state
            WHEN 3 THEN -1
            ELSE encryption_state
            END) DESC), 0)
        AS encryption_state,
dek_percent_complete.percent_complete,
database_dek.key_algorithm, database_dek.key_length, database_dek.encryptor_thumbprint
FROM database_dek
INNER JOIN dek_percent_complete
    ON dek_percent_complete.database_id = database_dek.database_id
WHERE type = 'CONTROL';
```

For a comprehensive example demonstrating all the steps in implementing TDE, see [Transparent Data Encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md).

## Examples: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]

### A. Altering the AUTOGROW setting

Set AUTOGROW to ON for database `CustomerSales`.

```sql
ALTER DATABASE CustomerSales
    SET ( AUTOGROW = ON );
```

### B. Altering the maximum storage for replicated tables

The following example sets the replicated table storage limit to 1 GB for the database `CustomerSales`. This is the storage limit per Compute node.

```sql
ALTER DATABASE CustomerSales
    SET ( REPLICATED_SIZE = 1 GB );
```

### C. Altering the maximum storage for distributed tables

 The following example sets the distributed table storage limit to 1000 GB (one terabyte) for the database `CustomerSales`. This is the combined storage limit across the appliance for all of the Compute nodes, not the storage limit per Compute node.

```sql
ALTER DATABASE CustomerSales
    SET ( DISTRIBUTED_SIZE = 1000 GB );
```

### D. Altering the maximum storage for the transaction log

 The following example updates the database `CustomerSales` to have a maximum [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log size of 10 GB for the appliance.

```sql
ALTER DATABASE CustomerSales
    SET ( LOG_SIZE = 10 GB );
```

### E. Check for current statistics values

The following query returns the current statistics values for all databases. The value 1 means the feature is on, and a 0 means the feature is off.

```sql
SELECT NAME,
    is_auto_create_stats_on,
    is_auto_update_stats_on,
    is_auto_update_stats_async_on
FROM sys.databases;
```

### F. Enable auto-create and auto-update stats for a database

Use the following statement to enable create and update statistics automatically and asynchronously for database, CustomerSales. This creates and updates single-column statistics as necessary to create high-quality query plans.

```sql
ALTER DATABASE CustomerSales
    SET AUTO_CREATE_STATISTICS ON;
ALTER DATABASE CustomerSales
    SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE
    SET AUTO_UPDATE_STATISTICS_ASYNC ON;
```

## See also

- [CREATE DATABASE - Analytics Platform System](../../t-sql/statements/create-database-transact-sql.md?view=aps-pdw-2016-au7&preserve-view=true)
- [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md)

::: moniker-end
