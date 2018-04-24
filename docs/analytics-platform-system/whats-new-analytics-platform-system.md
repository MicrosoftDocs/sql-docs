---
title: "What's new in Analytics Platform System – a scale-out data warehouse"
description: "See what’s new in Microsoft® Analytics Platform System, a scale-out on-premises appliance that hosts MPP SQL Server Parallel Data Warehouse."
author: "mzaman1"
manager: "craigg"
ms.prod: "sql"
ms.technology: "data-warehouse"
ms.topic: "conceptual"
ms.date: "04/24/2018"
ms.author: "murshedz"
ms.reviewer: "martinle"
---

# What's new in Analytics Platform System, a scale-out MPP data warehouse
See what’s new in the latest Appliance Updates for Microsoft® Analytics Platform System (APS). APS is a scale-out on-premises appliance that hosts MPP SQL Server Parallel Data Warehouse. 


## APS AU7
These are the new features for APS AU7:

### Auto-create and auto-update statistics
APS AU7 creates and updates statistics automatically, by default. To update statistics settings, administrators can use a new feature switch menu item in the [Configuration Manager](appliance-configuration.md#CMTasks). The [feature switch](appliance-feature-switch.md) controls the auto-create, auto-update, and asynchronous update behavior of statistics. You can also update statistics settings with the [ALTER DATABASE (Parallel Data Warehouse)](/sql/t-sql/statements/alter-database-parallel-data-warehouse) statement.

### T-SQL
Select @var is now supported. For more information, see [select local variable] (/sql/t-sql/language-elements/select-local-variable-transact-sql) 

Query hints HASH and ORDER GROUP are now supported. For more information, see [Hints(Transact-SQL) - Query ] (/sql/t-sql/language-elements/hints-transact-sql-query)

### Feature Switch
APS AU7 introduces Feature Switch in [Configuration Manager](launch-the-configuration-manager.md). AutoStatsEnabled and DmsProcessStopMessageTimeoutInSeconds are now configurable options that can be changed by Administrators.

### Known Issues
With APS AU7 software, we are packaging and providing the Intel BIOS update that fixes “speculative execution side-channel attacks” (aka. Spectre and Meltdown vulnerabilities). Though packaged together, the BIOS update is installed manually and not part of the APS AU7 software install. Microsoft advises all customers to install the BIOS updated. Microsoft has measured the effect of Kernel Virtual Address Shadowing (KVAS), Kernel Page Table Indirection (KPTI) and Indirect Branch Prediction mitigation (IBP) on various SQL workloads in various environments and found some workloads with significant degradation. We recommend that you test the performance effect of enabling BIOS update before you deploy them in a production environment. If the performance effect of enabling these features is too high for an existing application, you can consider whether isolating your APS Appliance from untrusted code running is a better mitigation for your application. See SQL Server guidance [here](https://support.microsoft.com/en-us/help/4073225/guidance-protect-sql-server-against-spectre-meltdown).

## APS 2016
These are the new features for APS 2016:

### SQL Server 2016

APS 2016 runs on the latest SQL Server 2016 release and uses the default database compatibility level 130.  SQL Server 2016 makes it possible to support some of the  new features such as secondary indexes for clustered columnstore indexes and Kerberos for PolyBase. 


### T-SQL
APS 2016 supports these T-SQL compatibility improvements.  These additional language elements make it easier to migrate from SQL Server and other data sources. 

- [Column-level SQL collations][] are now supported in addition to Windows collations.
- [Nonclustered indexes on clustered columnstore indexes][] improve performance of queries that search for specific values in the clustered columnstore index. 
- [SELECT...INTO][] 
- [sp_spaceused()][] displays the disk space used or reserved in a table or database.
- [Wide tables][] support is the same as SQL Server 2016. The previous limit of 32K for the row size no longer exists. 

**Data types**

- [VARCHAR(MAX)][], [NVARCHAR(MAX)][] and [VARBINARY(MAX)][]. These LOB data types have a maximum size of 2 GB. To load these objects use [bcp Utility][]. Polybase and dwloader do not currently support these data types. 
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
Upgrading your existing appliance to APS 2016 installs the latest firmware and driver updates, which include security fixes. 

A new appliance from HPE or DELL includes all the latest updates plus:

- Latest generation processor support (Broadwell)
- Update to DDR4 DIMMs
- Improved DIMM throughput

**Integration**

- Fully Qualified Domain Name (FQDN) support makes it possible to setup a Domain trust to the appliance. 
- To use FQDN, you need to do a full upgrade and opt-in during the upgrade. 

**Reduced downtime**
Installing or upgrading to APS 2016 is faster and requires less downtime than previous releases. To reduce downtime, the install or upgrade: 

 - Streamlines applying WSUS updates by using an image that contains all the updates through June 2016
 - Applies security updates with the driver and firmware updates
 - Places the latest hotfixes and the appliance verification utility (PAV) on your appliance so they are ready to install with no need to download them.


<!--MSDN references-->
[database compatibility level 130]:/sql/t-sql/statements/alter-database-transact-sql-compatibility-level
[Column-level SQL collations]:/sql/relational-databases/collations/collation-and-unicode-support
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


  

  


