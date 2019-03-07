---
title: "Maximum Capacity Specifications for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
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
ms.assetid: 13e95046-0e76-4604-b561-d1a74dd824d7
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Maximum Capacity Specifications for SQL Server
  The following tables specify the maximum sizes and numbers of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components. To navigate to the table for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] technology, click on its link:  
  
 [SQL Server Database Engine Objects](#Engine)  
  
 [SQL Server Utility Objects](#Utility)  
  
 [SQL Server Data-tier Application Objects](#DAC)  
  
 [SQL Server Replication Objects](#Replication)  
  
##  <a name="Engine"></a> [!INCLUDE[ssDE](../includes/ssde-md.md)] Objects  
 The following table specifies the maximum sizes and numbers of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases or referenced in [!INCLUDE[tsql](../includes/tsql-md.md)] statements.  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] object|Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (32-bit)|Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|  
|---------------------------------------------------------|------------------------------------------------------------------|------------------------------------------------------------------|  
|Batch size<br /><br /> Note: Network Packet Size is the size of the tabular data stream (TDS) packets used to communicate between applications and the relational [!INCLUDE[ssDE](../includes/ssde-md.md)]. The default packet size is 4 KB, and is controlled by the network packet size configuration option.|65,536 * Network Packet Size|65,536 * Network Packet Size|  
|Bytes per short string column|8,000|8,000|  
|Bytes per GROUP BY, ORDER BY|8,060|8,060|  
|Bytes per index key<br /><br /> Note: The maximum number of bytes in any index key cannot exceed 900 in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You can define a key using variable-length columns whose maximum sizes add up to more than 900, provided no row is ever inserted with more than 900 bytes of data in those columns. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can include nonkey columns in a nonclustered index to avoid the maximum index key size of 900 bytes.|900|900|  
|Bytes per foreign key|900|900|  
|Bytes per primary key|900|900|  
|Bytes per row<br /><br /> Note:<br />        [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports row-overflow storage which enables variable length columns to be pushed off-row. Only a 24-byte root is stored in the main record for variable length columns pushed out of row; because of this, the effective row limit is higher than in previous releases of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see the "Row-Overflow Data Exceeding 8 KB" topic in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online.|8,060|8,060|  
|Bytes per row in memory-optimized tables<br /><br /> Note:<br />        [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] In-Memory OLTP does not support row-overflow storage. Variable length columns are not pushed off row. This limits the maximum width of variable-length columns you can specify in a memory-optimized table to the maximum row size. For more information, see [Table and Row Size in Memory-Optimized Tables](../relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md).|Not supported|8,060|  
|Bytes in source text of a stored procedure|Lesser of batch size or 250 MB|Lesser of batch size or 250 MB|  
|Bytes per `varchar(max)`, `varbinary(max)`, `xml`, `text`, or `image` column|2^31-1|2^31-1|  
|Characters per `ntext` or `nvarchar(max)` column|2^30-1|2^30-1|  
|Clustered indexes per table|1|1|  
|Columns in GROUP BY, ORDER BY|Limited only by number of bytes|Limited only by number of bytes|  
|Columns or expressions in a GROUP BY WITH CUBE or WITH ROLLUP statement|10|10|  
|Columns per index key<br /><br /> Note: If the table contains one or more XML indexes, the clustering key of the user table is limited to 15 columns because the XML column is added to the clustering key of the primary XML index. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can include nonkey columns in a nonclustered index to avoid the limitation of a maximum of 16 key columns. For more information, see [Create Indexes with Included Columns](../relational-databases/indexes/create-indexes-with-included-columns.md).|16|16|  
|Columns per foreign key|16|16|  
|Columns per primary key|16|16|  
|Columns per nonwide table|1,024|1,024|  
|Columns per wide table|30,000|30,000|  
|Columns per SELECT statement|4,096|4,096|  
|Columns per INSERT statement|4096|4096|  
|Connections per client|Maximum value of configured connections|Maximum value of configured connections|  
|Database size|524,272 terabytes|524,272 terabytes|  
|Databases per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]|32,767|32,767|  
|Filegroups per database|32,767|32,767|  
|Filegroups per database for memory-optimized data|Not supported|1|  
|Files per database|32,767|32,767|  
|File size (data)|16 terabytes|16 terabytes|  
|File size (log)|2 terabytes|2 terabytes|  
|Data files for memory-optimized data per database|Not supported|4.096|  
|Delta file per data file for memory-optimized data|Not supported|1|  
|Foreign key table references per table<br /><br /> Note: Although a table can contain an unlimited number of FOREIGN KEY constraints, the recommended maximum is 253. Depending on the hardware configuration hosting [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], specifying additional FOREIGN KEY constraints may be expensive for the query optimizer to process.|253|253|  
|Identifier length (in characters)|128|128|  
|Instances per computer|50 instances on a stand-alone server for all [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions.<br /><br /> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports 25 instances on a failover cluster when using a shared cluster disk as the stored option for you cluster installation [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports 50 instances on a failover cluster if you choose SMB file shares as the storage option for your cluster installation For more information, see [Hardware and Software Requirements for Installing SQL Server 2014](install/hardware-and-software-requirements-for-installing-sql-server.md).|50 instances on a stand-alone server.<br /><br /> 25 instances on a failover cluster when using a shared cluster disk as the stored option for you cluster installation [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports 50 instances on a failover cluster if you choose SMB file shares as the storage option for your cluster installation.|  
|Indexes per memory-optimized table|Not supported|8|  
|Length of a string containing SQL statements (batch size)<br /><br /> Note: Network Packet Size is the size of the tabular data stream (TDS) packets used to communicate between applications and the relational [!INCLUDE[ssDE](../includes/ssde-md.md)]. The default packet size is 4 KB, and is controlled by the network packet size configuration option.|65,536 * Network packet size|65,536 * Network packet size|  
|Locks per connection|Maximum locks per server|Maximum locks per server|  
|Locks per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]<br /><br /> Note: This value is for static lock allocation. Dynamic locks are limited only by memory.|Up to 2,147,483,647|Limited only by memory|  
|Nested stored procedure levels<br /><br /> Note: If a stored procedure accesses more than 64  databases, or more than 2 databases in interleaving, you will receive an error.|32|32|  
|Nested subqueries|32|32|  
|Nested trigger levels|32|32|  
|Nonclustered indexes per table|999|999|  
|Number of distinct expressions in the GROUP BY clause when any of the following are present: CUBE, ROLLUP, GROUPING SETS, WITH CUBE, WITH ROLLUP|32|32|  
|Number of grouping sets generated by operators in the GROUP BY clause|4,096|4,096|  
|Parameters per stored procedure|2,100|2,100|  
|Parameters per user-defined function|2,100|2,100|  
|REFERENCES per table|253|253|  
|Rows per table|Limited by available storage|Limited by available storage|  
|Tables per database<br /><br /> Note: Database objects include objects such as tables, views, stored procedures, user-defined functions, triggers, rules, defaults, and constraints. The sum of the number of all objects in a database cannot exceed 2,147,483,647.|Limited by number of objects in a database|Limited by number of objects in a database|  
|Partitions per partitioned table or index|1,000<br /><br /> **\*\* Important \*\*** Creating a table or index with more than 1,000 partitions is possible on a 32-bit system, but is not supported.|15,000|  
|Statistics on non-indexed columns|30,000|30,000|  
|Tables per SELECT statement|Limited only by available resources|Limited only by available resources|  
|Triggers per table<br /><br /> Note: Database objects include objects such as tables, views, stored procedures, user-defined functions, triggers, rules, defaults, and constraints. The sum of the number of all objects in a database cannot exceed 2,147,483,647.|Limited by number of objects in a database|Limited by number of objects in a database|  
|Columns per UPDATE statement (Wide Tables)|4096|4096|  
|User connections|32,767|32,767|  
|XML indexes|249|249|  
  
##  <a name="Utility"></a> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility Objects  
 The following table specifies the maximum sizes and numbers of various objects that were tested in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility.  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility object|Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (32-bit)|Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|  
|----------------------------------------------|------------------------------------------------------------------|------------------------------------------------------------------|  
|Computers (physical computers or virtual machines) per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility|100|100|  
|Instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] per computer|5|5|  
|Total number of instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility|200*|200*|  
|User databases per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], including data-tier applications|50|50|  
|Total number of user databases per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility|1,000|1,000|  
|File groups per database|1|1|  
|Data files per file group|1|1|  
|Log files per database|1|1|  
|Volumes per computer|3|3|  
  
 *The maximum number of managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supported by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility might vary based on the hardware configuration of the server. For getting started information, see [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md). [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] utility control point is not available in every edition of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
##  <a name="DAC"></a> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data-tier Application Objects  
 The following table specifies the maximum sizes and numbers of various objects that were tested in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data-tier applications (DAC).  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] DAC object|Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (32-bit)|Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|  
|------------------------------------------|------------------------------------------------------------------|------------------------------------------------------------------|  
|Databases per DAC|1|1|  
|Objects per DAC*|Limited by the number of objects in a database, or available memory.|Limited by the number of objects in a database, or available memory.|  
  
 *The types of objects included in the limit are users, tables, views, stored procedures, user-defined functions, user-defined data type, database roles, schemas, and user-defined table types.  
  
##  <a name="Replication"></a> Replication Objects  
 The following table specifies the maximum sizes and numbers of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Replication.  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Replication object|Maximum sizes/numbers SQL Server (32-bit)|Maximum sizes/numbers SQL Server (64-bit)|  
|--------------------------------------------------|---------------------------------------------------|---------------------------------------------------|  
|Articles (merge publication)|256|256|  
|Articles (snapshot or transactional publication)|32,767|32,767|  
|Columns in a table* (merge publication)|246|246|  
|Columns in a table** ([!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] snapshot or transactional publication)|1,000|1,000|  
|Columns in a table** (Oracle snapshot or transactional publication)|995|995|  
|Bytes for a column used in a row filter (merge publication)|1,024|1,024|  
|Bytes for a column used in a row filter (snapshot or transactional publication)|8,000|8,000|  
  
 *If row tracking is used for conflict detection (the default), the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.  
  
 **The base table can include the maximum number of columns allowable in the publication database (1,024 for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]), but columns must be filtered from the article if they exceed the maximum specified for the publication type.  
  
## See Also  
 [Hardware and Software Requirements for Installing SQL Server 2014](install/hardware-and-software-requirements-for-installing-sql-server.md)   
 [Check Parameters for the System Configuration Checker](../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)   
 [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md)  
  
  
