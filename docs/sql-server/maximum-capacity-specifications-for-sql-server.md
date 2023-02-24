---
title: "Maximum capacity specifications for SQL Server"
description: This article shows maximum sizes and numbers of various objects defined in SQL Server components, along with additional information.
ms.service: sql
author: MikeRayMSFT
ms.author: mikeray
ms.date: 03/11/2022
ms.reviewer: randolphwest
ms.custom: ""
ms.subservice: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "objects [SQL Server]"
  - "number capacity specifications [SQL Server]"
  - "size [SQL Server], capacity specifications"
  - "number of objects"
  - "capacity specifications [SQL Server]"
  - "maximum capacity specifications [SQL Server]"
  - "size [SQL Server]"
  - "replication capacity specifications [SQL Server]"
  - "objects [SQL Server], capacity specifications"
  - "Database Engine [SQL Server], capacity specifications"
---
# Maximum capacity specifications for SQL Server

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

This article shows maximum sizes and numbers of various objects defined in [!INCLUDE[sssql16-md](../includes/sssql16-md.md)] and later. If you want to view edition limits, see [Compute capacity limits by edition of SQL Server](compute-capacity-limits-by-edition-of-sql-server.md).

For [!INCLUDE[ssSQL14](../includes/ssSQL14-md.md)], see [Maximum capacity specifications for SQL Server 2014](/previous-versions/sql/2014/sql-server/maximum-capacity-specifications-for-sql-server).

## [!INCLUDE[ssDE](../includes/ssde-md.md)] objects

Maximum values of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases, or referenced in [!INCLUDE[tsql](../includes/tsql-md.md)] statements.

|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] object|Maximum&nbsp;values&nbsp;for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|Additional Information|
|---|---|---|
|Batch size|65,536 * (network packet size)|Network packet size is the size of the tabular data stream (TDS) packets used to communicate between the relational [!INCLUDE[ssDE](../includes/ssde-md.md)] and applications. The default packet size is 4 KB, and is controlled by the network packet size configuration option.|
|Byte length of a string containing [!INCLUDE[tsql](../includes/tsql-md.md)] statements (batch size)|65,536 * (network packet size)|Network packet size is the size of the tabular data stream (TDS) packets used to communicate between the relational [!INCLUDE[ssDE](../includes/ssde-md.md)] and applications. The default packet size is 4 KB, and is controlled by the network packet size configuration option.|
|Bytes per short string column|8,000||
|Bytes per `GROUP BY`, `ORDER BY`|8,060||
|Bytes per index key|900 bytes for a clustered index. 1,700 bytes for a nonclustered index. For [!INCLUDE[ssSQL14](../includes/ssSQL14-md.md)] and earlier, all versions supported 900 bytes for all index types.|The maximum number of bytes in a clustered index key can't exceed 900. For a nonclustered index key, the maximum is 1,700 bytes.<br /><br />You can define a key using variable-length columns whose maximum sizes add up to more than the limit. However, the combined sizes of the data in those columns can never exceed the limit.<br /><br />In a nonclustered index, you can include extra non-key columns, and they don't count against the size limit of the key. The non-key columns might help some queries perform better.|
|Bytes per index key for memory-optimized tables|2,500 bytes for a nonclustered index. No limit for a hash index, as long as all index keys fit in-row.|On a memory-optimized table, a nonclustered index can't have key columns whose maximum declared sizes exceed 2,500 bytes. It doesn't matter if the actual data in the key columns would be shorter than the maximum declared sizes.<br /><br />For a hash index key, there's no hard limit on size.<br /><br />For indexes on memory-optimized tables, there's no concept of included columns, since all indexes inherently cover all columns.<br /><br />For a memory-optimized table, even though the row size is 8,060 bytes, some variable-length columns can be physically stored outside those 8,060 bytes. However, the maximum declared sizes of all key columns for all indexes on a table, plus any additional fixed-length columns in the table, must fit in the 8,060 bytes.|
|Bytes per foreign key|900||
|Bytes per primary key|900||
|Bytes per row|8,060|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports row-overflow storage, which enables variable length columns to be pushed off-row. Only a 24-byte root is stored in the main record for variable length columns pushed out of row. For more information, see [Large Row Support](../relational-databases/pages-and-extents-architecture-guide.md#large-row-support).|
|Bytes per row in memory-optimized tables|8,060|Memory-optimized tables on [!INCLUDE[sssql16-md](../includes/sssql16-md.md)] and later support off-row storage. Variable length columns are pushed off-row if the maximum sizes for all the columns in the table exceeds 8,060 bytes; this action is a compile-time decision. Only an 8-byte reference is stored in-row for columns stored off-row. For more information, see [Table and Row Size in Memory-Optimized Tables](../relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md).|
|Bytes in source text of a stored procedure|Lesser of batch size or 250 MB||
|Bytes per `varchar(max)`, `varbinary(max)`, `xml`, `text`, or `image` column|2^31-1||
|Characters per `ntext` or `nvarchar(max)` column|2^30-1||
|Clustered indexes per table|1||
|Columns in `GROUP BY`, `ORDER BY`|Limited only by number of bytes||
|Columns or expressions in a `GROUP BY WITH CUBE` or `GROUP BY WITH ROLLUP` statement|10||
|Columns per index key|32|If the table contains one or more XML indexes, the clustering key of the user table is limited to 31 columns because the XML column is added to the clustering key of the primary XML index. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] you can include non-key columns in a nonclustered index, to avoid the limitation of a maximum of 32 key columns. For more information, see [Create Indexes with Included Columns](../relational-databases/indexes/create-indexes-with-included-columns.md).|
|Columns per foreign key or primary key|32||
|Columns per `INSERT` statement|4,096||
|Columns per `SELECT` statement|4,096||
|Columns per table|1,024|Tables that include sparse column sets include up to 30,000 columns. See [sparse column sets](../relational-databases/tables/use-column-sets.md).|
|Columns per `UPDATE` statement|4,096|Different limits apply to [sparse column sets](../relational-databases/tables/use-column-sets.md).|
|Columns per view|1,024||
|Connections per client|Maximum value of configured connections||
|Database size|524,272 terabytes||
|Databases per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]|32,767||
|Filegroups per database|32,767||
|Filegroups per database for memory-optimized data|1||
|Files per database|32,767||
|File size (data)|16 terabytes||
|File size (log)|2 terabytes||
|Data files for memory-optimized data per database|4,096 in [!INCLUDE[ssSQL14](../includes/ssSQL14-md.md)]. The limit is less strict on [!INCLUDE[sssql16-md](../includes/sssql16-md.md)] and later.||
|Delta file per data file for memory-optimized data|1||
|Foreign key table references per table|Outgoing = 253.<br/>Incoming = 10,000.|For restrictions, see [Create Foreign Key Relationships](../relational-databases/tables/create-foreign-key-relationships.md).|
|Identifier length (in characters)|128||
|Instances per computer|50 instances on a stand-alone server.<br /><br />25 failover cluster instances when using a shared cluster drive as storage.<br/><br/>50 failover cluster instances with SMB file shares as the storage option.||
|Indexes per memory-optimized table|999 starting [!INCLUDE[ssSQL17](../includes/ssSQL17-md.md)] and in [!INCLUDE[ssSDSFull](../includes/ssSDSFull-md.md)].<br/><br/>8 in [!INCLUDE[sssql16-md](../includes/sssql16-md.md)] and [!INCLUDE[ssSQL14](../includes/ssSQL14-md.md)].||
|Locks per connection|Maximum locks per server||
|Locks per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]|Limited only by memory|This value is for static lock allocation. Dynamic locks are limited only by memory.|
|Nested stored procedure levels|32|If a stored procedure accesses more than 64 databases, or more than two databases in interleaving, you'll receive an error.|
|Nested subqueries|32||
|Nested transactions|4,294,967,296||
|Nested trigger levels|32||
|Nonclustered indexes per table|999||
|Number of distinct expressions in the `GROUP BY` clause when any of the following are present: `CUBE`, `ROLLUP`, `GROUPING SETS`, `WITH CUBE`, `WITH ROLLUP`|32||
|Number of grouping sets generated by operators in the `GROUP BY` clause|4,096||
|Parameters per stored procedure|2,100||
|Parameters per user-defined function|2,100||
|REFERENCES per table|253||
|Rows per table|Limited by available storage||
|Tables per database|Limited by total number of objects in a database|Objects include tables, views, stored procedures, user-defined functions, triggers, rules, defaults, and constraints. The sum of the number of all objects in a database can't exceed 2,147,483,647.|
|Partitions per partitioned table or index|15,000||
|Statistics on non-indexed columns|30,000||
|Tables per `SELECT` statement|Limited only by available resources||
|Triggers per table|Limited by number of objects in a database|Objects include tables, views, stored procedures, user-defined functions, triggers, rules, defaults, and constraints. The sum of the number of all objects in a database can't exceed 2,147,483,647.|
|User connections|32,767||
|XML indexes|249||

## [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility objects

Maximum values of various objects that were tested in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility.

|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility object|Maximum&nbsp;values&nbsp;for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|Additional information|
|---|---|---|
|Computers (physical computers or virtual machines) per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility|100||
|Instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] per computer|5||
|Total number of instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility|200|The maximum number may vary based on the hardware configuration of the server. For getting started information, see [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md). [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility control point isn't available in every edition of SQL Server. You can read about the features supported by each edition of [SQL Server 2019](./editions-and-components-of-sql-server-2019.md), [SQL Server 2017](editions-and-components-of-sql-server-2017.md), and [SQL Server 2016](editions-and-components-of-sql-server-2016.md).|
|User databases per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance, including data-tier applications|50||
|Total number of user databases per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility|1,000||
|File groups per database|1||
|Data files per file group|1||
|Log files per database|1||
|Volumes per computer|3||

## [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data-tier application objects

Maximum values of various objects that were tested in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data-tier applications (DAC).

|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] DAC object|Maximum&nbsp;values&nbsp;for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|Additional information|
|---|---|---|
|Databases per DAC|1||
|Objects per DAC|Limited by the number of objects in a database, or available memory.|Types of objects included in the limit are users, tables, views, stored procedures, user-defined functions, user-defined data type, database roles, schemas, and user-defined table types.|

## Replication objects

Maximum values of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Replication.

|SQL&nbsp;Server&nbsp;Replication object|Maximum&nbsp;values&nbsp;for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]<br />(64-bit)|Additional information|
|---|---|---|
|Articles (merge publication)|2,048||
|Articles (snapshot or transactional publication)|32,767||
|Columns in a table (merge publication)|246|If row tracking is used for conflict detection (the default), the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.|
|Columns in a table ([!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] snapshot or transactional publication)|1,000|The base table can include the maximum number of columns allowable in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] publication database (1,024), but columns must be filtered from the article if they exceed the maximum specified for the publication type.|
|Columns in a table (Oracle snapshot or transactional publication)|995|The base table can include the maximum number of columns allowable in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] publication database (1,024), but columns must be filtered from the article if they exceed the maximum specified for the publication type.|
|Bytes for a column used in a row filter (merge publication)|1,024||
|Bytes for a column used in a row filter (snapshot or transactional publication)|8,000||

## See also

- [SQL Server utility features and tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md)
- [Hardware and software requirements for installing SQL Server](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
- [Check parameters for system configuration checker](../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)
- [Download SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads)