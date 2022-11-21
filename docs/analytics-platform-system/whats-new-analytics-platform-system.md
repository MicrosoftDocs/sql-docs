---
title: "What's new"
description: "See what's new in Microsoft Analytics Platform System, a scale-out on-premises appliance that hosts MPP SQL Server Parallel Data Warehouse."
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: "martinle"
ms.date: "12/01/2021"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "conceptual"
ms.custom:
  - seo-dt-2019
  - intro-whats-new
---
# What's new in Analytics Platform System, a scale-out MPP data warehouse
See what's new in the latest Appliance Updates for Microsoft Analytics Platform System (APS). APS is a scale-out on-premises appliance that hosts MPP SQL Server Parallel Data Warehouse. 

::: moniker range=">= aps-pdw-2016-au7 "

<a name="h2-aps-cu7.8"></a>
## APS CU7.8
Release date - November 2021

### SCVMM2016

APS CU 7.8 software adds support for offline installation of SCVMM2016.

Patched VMM with latest SQL Version.

Release also includes additional security updates and bug fixes.

<a name="h2-aps-cu7.7"></a>
## APS CU7.7
Release date - November 2020

### SCVMM2016

APS CU7.7 software upgrades VMM VM to Windows Server 2016 and installs SCVMM2016. SCVMM 2012 R2 that is currently in use has an end of life date of July 2022. The newer SCVMM is needed to be supported making CU7.7 a mandatory upgrade. Customers are urged to upgrade to CU7.7 as soon as possible.

### SSIS destination adapter for SQL Server 2019 as target
New APS SSIS destination adapter that supports SQL Server 2019 as deployment target can be downloaded from [download site](https://www.microsoft.com/download/details.aspx?id=57472).

<a name="h2-aps-cu7.6"></a>
## APS CU7.6
Release date - April 2020

### Rename Column
After upgrading to CU7.6, customers will be able to rename a column of a user-created table. See [RENAME (Transact-SQL)](../t-sql/statements/rename-transact-sql.md) for syntax, examples, limitations and more information.

### Alter view
Customers will now be able to alter views. See [ALTER VIEW (Transact-SQL)](../t-sql/statements/alter-view-transact-sql.md) for more information.

<a name="h2-aps-cu7.5"></a>
## APS CU7.5
Release date - September 2019

### Alter External Data Source
Customers will be able to alter external data source definition with the CU7.5 update. Customers with Hadoop name node high availability can now alter the data source to change the arguments when a failover happens. For APS, only the LOCATION, RESOURCE_MANAGER_LOCATION and CREDENTIAL can be changed. See [alter external data source](../t-sql/statements/alter-external-data-source-transact-sql.md?view=sql-server-2017&preserve-view=true) for more information.

### CDH 5.15 and 5.16 support with PolyBase
PolyBase on APS with CU7.5 update now supports CDH 5.15 and 5.16 versions of Hadoop distribution from Cloudera. Use option 6 for CDH 5.x versions. 

### Try_Convert and Try_Cast support
CU7.5 APS now supports [TRY_CAST](../t-sql/functions/try-cast-transact-sql.md?view=sql-server-2017&preserve-view=true) and [TRY_CONVERT](../t-sql/functions/try-convert-transact-sql.md?view=sql-server-2017&preserve-view=true) tsql functions. Both of these functions returns a value converted to the specified data type if the convert succeeds; otherwise, returns null.

<a name="h2-aps-cu7.4"></a>
## APS CU7.4
Release date - May 2019

### Loading large rows with dwloader
Starting from APS CU7.4, customers will be able to use a new dwloader to load rows into tables that are larger than 32 KB (32,768 bytes). The new dwloader supports the -l switch that takes an integer value between 32768 and 33554432 (in bytes) to load rows larger than 32 KB. Only use this option when loading large rows (greater than 32 KB) as this switch will allocate more memory on the client and the server and may slow down loads. You can download the new dwloader from [download site](https://www.microsoft.com/download/details.aspx?id=57472).  

### HDP 3.0 and 3.1 support with PolyBase
PolyBase on APS now supports HDP 3.0 and 3.1 with this update. Use option 7 for HDP 3.x versions. For more information, see [PolyBase connectivity](../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md) page.

### UTF16 file support with PolyBase
PolyBase now support reading delimited text files that are in UTF16 (LE) encoding. See [create external file format](../t-sql/statements/create-external-file-format-transact-sql.md) for setup details. 

<a name="h2-aps-cu7.3"></a>
## APS CU7.3
Release date - December 2018

### Common subexpression elimination
APS CU7.3 improves query performance with common subexpression elimination in SQL query optimizer. The improvement improves queries in two ways. The first benefit is the ability to identify and eliminate such expressions help reduce SQL compilation time. The second and more important benefit is data movement operations for these redundant subexpressions are eliminated thus execution time for queries becomes faster. Detailed explanation of this feature can be found [here](common-sub-expression-elimination.md).

### APS Informatica connector for Informatica 10.2.0 published
We have released a new version of Informatica connectors for APS that works with Informatica version 10.2.0 and 10.2.0 Hotfix 1. The new connectors can be downloaded from [download site](https://www.microsoft.com/download/details.aspx?id=57472).
> [!NOTE]
> APS Informatica connector for Informatica 10.2.0 or 10.2.0 Hotfix 1 does not work on strict TLS1.2 and requires TLS1.0 and 1.1 to be fully functional.

#### Supported Versions

| APS Version | Informatica PowerCenter | Driver |
|:---|:---|:---|
| APS 2016 | 9.6.1 | SQL Server Native Client 11.x |
| APS 2016 and later | 10.2.0, 10.2.0 Hotfix 1 | SQL Server Native Client 11.x |

<a name="h2-aps-cu7.2"></a>
## APS CU7.2
Release date - October 2018

### Support for TLS 1.2
APS CU7.2 supports TLS 1.2. Client machine to APS and APS intra-node communication can now be set to communicate only over TLS1.2. Tools like SSDT, SSIS, and Dwloader installed on client machines that are set to communicate only over TLS 1.2 can now connect to APS using TLS 1.2. By default, APS will support all TLS (1.0, 1.1 and 1.2) versions for backward compatibility. If you wish to set your APS appliance to strictly use TLS 1.2, you can do so by changing registry settings. 

For more information, see [configuring TLS1.2 on APS](configure-tls12-aps.md).

### Hadoop encryption zone support for PolyBase
PolyBase now can communicate to Hadoop encryption zones. See APS configuration changes that are needed in [configure Hadoop security](polybase-configure-hadoop-security.md#encryptionzone).

### Insert-Select maxdop options
We have added a [feature switch](appliance-feature-switch.md) that allows you to pick maxdop settings greater than 1 for insert-select operations. You can now set the maxdop setting to 0, 1, 2, or 4. The default is 1.

> [!IMPORTANT]  
> Increasing maxdop may sometimes result in slower operations or deadlock errors. If that occurs, change the setting back to maxdop 1 and retry the operation.

### ColumnStore index health DMV
You can view columnstore index health information using **dm_pdw_nodes_db_column_store_row_group_physical_stats** dmv. Use the following view to determine fragmentation and decide when to rebuild or reorganize a columnstore index.

```sql
create view dbo.vCS_rg_physical_stats
as 
with cte
as
(
select   tb.[name]                    AS [logical_table_name]
,        rg.[row_group_id]            AS [row_group_id]
,        rg.[state]                   AS [state]
,        rg.[state_desc]              AS [state_desc]
,        rg.[total_rows]              AS [total_rows]
,        rg.[trim_reason_desc]        AS trim_reason_desc
,        mp.[physical_name]           AS physical_name
FROM    sys.[schemas] sm
JOIN    sys.[tables] tb               ON  sm.[schema_id]          = tb.[schema_id]                             
JOIN    sys.[pdw_table_mappings] mp   ON  tb.[object_id]          = mp.[object_id]
JOIN    sys.[pdw_nodes_tables] nt     ON  nt.[name]               = mp.[physical_name]
JOIN    sys.[dm_pdw_nodes_db_column_store_row_group_physical_stats] rg      ON  rg.[object_id]     = nt.[object_id]
                                                                            AND rg.[pdw_node_id]   = nt.[pdw_node_id]
                                        AND rg.[pdw_node_id]    = nt.[pdw_node_id]                                          
)
select *
from cte;
```

### PolyBase date range increase for ORC and Parquet files
Reading, importing and exporting date data types using PolyBase now supports dates before 1970-01-01 and after 2038-01-20 for ORC and Parquet file types.

### SSIS destination adapter for SQL Server 2017 as target
New APS SSIS destination adapter that supports SQL Server 2017 as deployment target can be downloaded from [download site](https://www.microsoft.com/download/details.aspx?id=57472).

<a name="h2-aps-cu7.1"></a>
## APS CU7.1
Release date - July 2018

### DBCC commands do not consume concurrency slots (behavior change)
APS supports a subset of the T-SQL [DBCC commands](../t-sql/database-console-commands/dbcc-transact-sql.md) such as [DBCC DROPCLEANBUFFERS](../t-sql/database-console-commands/dbcc-dropcleanbuffers-transact-sql.md). Previously, these commands would consume a [concurrency slot](./workload-management.md?view=aps-pdw-2016-au7&preserve-view=true&#concurrency-slots) reducing the number of user loads/queries that could be executed. The `DBCC` commands are now run in a local queue that do not consume a user concurrency slot improving overall query execution performance.

### Replaces some metadata calls with catalog objects
Using catalog objects for metadata calls instead of using SMO has shown performance improvement in APS. Starting from CU7.1, some of these metadata calls now use catalog objects by default. This behavior can be turned off by [feature switch](appliance-feature-switch.md) if customers using metadata queries run into any issues.

### Bug fixes
We have upgraded to SQL Server 2016 SP2 CU2 with APS CU7.1. The upgrade fixes some issues described below.

| Title | Description |
|:---|:---|
| **Potential tuple mover deadlock** |The upgrade fixes a long standing possibility of deadlock in a distributed transaction and tuple mover background thread. After installing CU7.1, customers who used TF634 to stop tuple mover as SQL Server startup parameter or global trace flag can safely remove it. | 
| **Certain lag/lead query fails** |Certain queries on CCI tables with nested lag/lead functions that would error is now fixed with this upgrade. | 


<a name="h2-aps-au7"></a>
## APS AU7
Release date - May 2018

APS 2016 is a prerequisite to upgrade to AU7. The following are new features in APS AU7:

### Auto-create and auto-update statistics
APS AU7 creates and updates statistics automatically, by default. To update statistics settings, administrators can use a new feature switch menu item in the [Configuration Manager](appliance-configuration.md#CMTasks). The [feature switch](appliance-feature-switch.md) controls the auto-create, auto-update, and asynchronous update behavior of statistics. You can also update statistics settings with the [ALTER DATABASE (Parallel Data Warehouse)](../t-sql/statements/alter-database-transact-sql.md?tabs=sqlpdw) statement.

### T-SQL
Select @var is now supported. For more information, see [select local variable](../t-sql/language-elements/select-local-variable-transact-sql.md) 

Query hints HASH and ORDER GROUP are now supported. For more information, see [Hints(Transact-SQL) - Query](../t-sql/queries/hints-transact-sql-query.md)

### Feature Switch
APS AU7 introduces Feature Switch in [Configuration Manager](launch-the-configuration-manager.md). AutoStatsEnabled and DmsProcessStopMessageTimeoutInSeconds are now configurable options that can be changed by Administrators.

### Known Issues
With APS AU7 software, an Intel BIOS update is provided which fixes a problem described as *speculative execution side-channel attacks*. The attacks aim to exploit what are called *Spectre and Meltdown vulnerabilities*. Although packaged together with APS, the BIOS update is installed manually, and not as part of the APS AU7 software install.

Microsoft advises all customers to install the BIOS updated. Microsoft has measured the effect of Kernel Virtual Address Shadowing (KVAS), Kernel Page Table Indirection (KPTI) and Indirect Branch Prediction mitigation (IBP) on various SQL workloads in various environments. The measurements found significant degradation on some workloads. Based on the results, the recommendation is that you test the performance effect of enabling BIOS update before you deploy them in a production environment. See SQL Server guidance [here](https://support.microsoft.com/help/4073225/guidance-protect-sql-server-against-spectre-meltdown).

::: moniker-end
::: moniker range=">= aps-pdw-2016 "
<a name="h2-aps-au6"></a>
## APS 2016
This section described the new features for APS 2016-AU6.

### SQL Server 2016

APS AU6 runs on the latest SQL Server 2016 release, and uses the default database compatibility level 130. SQL Server 2016 enables support for new features such as:

- Secondary indexes for clustered columnstore indexes.
- Kerberos for PolyBase.

### T-SQL
APS AU6 supports these T-SQL compatibility improvements.  These additional language elements make it easier to migrate from SQL Server and other data sources. 

- [Column-level SQL collations][] are now supported, in addition to Windows collations.
- [Nonclustered indexes on clustered columnstore indexes][] improve performance of queries that search for specific values in the clustered columnstore index. 
- [SELECT...INTO][] 
- [sp_spaceused()][] displays the disk space used or reserved in a table or database.
- [Wide tables][] support is the same as SQL Server 2016. The previous limit of 32 K for the row size no longer exists. 

**Data types**

- [VARCHAR(MAX)][], [NVARCHAR(MAX)][] and [VARBINARY(MAX)][]. These LOB data types have a maximum size of 2 GB. To load these objects use [bcp Utility][]. PolyBase and dwloader do not currently support these data types. 
- [SYSNAME][]
- [UNIQUEIDENTIFIER][]
- [NUMERIC][] and DECIMAL data types.

**Window functions**

- [ROWS or RANGE][] in the OVER clause of the SELECT statement.
- [FIRST_VALUE][]
- [LAST_VALUE][]
- [CUME_DIST][]
- [PERCENT_RANK][]

**Security functions**

- [CHECKSUM()][] and [BINARY_CHECKSUM()][]
- [HAS_PERMS_BY_NAME()][]

**Additional functions**

- [NEWID()][]
- [RAND()][]

### PolyBase/Hadoop enhancements

- Compatibility with Hortonworks HDP 2.4 and HDP 2.5
- Kerberos support via database scoped credentials
- Credential support with Azure Storage Blobs

### Install and upgrade enhancements

**Enterprise architecture updates**
Upgrading your existing appliance to APS AU6 installs the latest firmware and driver updates, which include security fixes. 

A new appliance from HPE or DELL includes all the latest updates plus:

- Latest generation processor support (Broadwell)
- Update to DDR4 DIMMs
- Improved DIMM throughput

**Integration**

- Fully Qualified Domain Name (FQDN) support makes it possible to setup a Domain trust to the appliance. 
- To use FQDN, you need to do a full upgrade and opt-in during the upgrade. 

**Reduced downtime**
Installing or upgrading to APS AU6 is faster and requires less downtime than previous releases. To reduce downtime, the install or upgrade: 

 - Streamlines applying WSUS updates by using an image that contains all the updates through June 2016
 - Applies security updates with the driver and firmware updates
 - Places the latest hotfixes and the appliance verification utility (PAV) on your appliance so they are ready to install with no need to download them.

::: moniker-end

<!--
Link references to other articles in this same GitHub repo.

The link format that starts with '/sql/what-ever/my-artlcle' is not appropriate for common links within the same repo (as most of these link are).  The first couple links have been edited to show the proper syntax, but all other links in this article need to be similarly edited.
The proper formats have at least two big advantages.  One big advantage is that the proper formats enable the OPS Build system to detect broken links at Pull Request build time, instead of only later during run time.
-->
[database compatibility level 130]: ../t-sql/statements/alter-database-transact-sql-compatibility-level.md
[Column-level SQL collations]: ~/relational-databases/collations/collation-and-unicode-support.md

[Nonclustered indexes on clustered columnstore indexes]:/sql/t-sql/statements/create-index-transact-sql
[VARCHAR(MAX)]:/sql/t-sql/data-types/char-and-varchar-transact-sql
[NVARCHAR(MAX)]:/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql
[VARBINARY(MAX)]:/sql/t-sql/data-types/binary-and-varbinary-transact-sql
[SYSNAME]:/sql/relational-databases/system-catalog-views/sys-types-transact-sql
[SELECT...INTO]:/sql/t-sql/queries/select-into-clause-transact-sql
[sp_spaceused()]:/sql/relational-databases/system-stored-procedures/sp-spaceused-transact-sql
[Wide tables]:/sql/sql-server/maximum-capacity-specifications-for-sql-server
[BULK INSERT]:/sql/t-sql/statements/bulk-insert-transact-sql
[bcp Utility]:/sql/tools/bcp-utility
[UNIQUEIDENTIFIER]:/sql/t-sql/data-types/uniqueidentifier-transact-sql
[NUMERIC]:/sql/t-sql/data-types/decimal-and-numeric-transact-sql
[ROWS or RANGE]:/sql/t-sql/queries/select-over-clause-transact-sql
[FIRST_VALUE]:/sql/t-sql/functions/first-value-transact-sql
[LAST_VALUE]:/sql/t-sql/functions/last-value-transact-sql
[CUME_DIST]:/sql/t-sql/functions/cume-dist-transact-sql
[PERCENT_RANK]:/sql/t-sql/functions/percent-rank-transact-sql
[CHECKSUM()]:/sql/t-sql/functions/checksum-transact-sql
[BINARY_CHECKSUM()]:/sql/t-sql/functions/binary-checksum-transact-sql
[HAS_PERMS_BY_NAME()]:/sql/t-sql/functions/has-perms-by-name-transact-sql
[NEWID()]:/sql/t-sql/functions/newid-transact-sql
[RAND()]:/sql/t-sql/functions/rand-transact-sql


  
