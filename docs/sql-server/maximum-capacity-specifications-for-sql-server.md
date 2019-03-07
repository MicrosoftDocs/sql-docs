---
title: "Maximum Capacity Specifications for SQL Server | Microsoft Docs"
ms.date: 11/06/2017
ms.prod: sql
ms.reviewer: ""
ms.custom: ""
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
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: craigg
---
# Maximum Capacity Specifications for SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The following tables specify maximum sizes and numbers of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components. To navigate to the table for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] technology, click on its link:  
  
 [SQL Server Database Engine Objects](#Engine)  
  
 [SQL Server Utility Objects](#Utility)  
  
 [SQL Server Data-tier Application Objects](#DAC)  
  
 [SQL Server Replication Objects](#Replication)  
  
##  <a name="Engine"></a> [!INCLUDE[ssDE](../includes/ssde-md.md)] Objects  
 Maximum sizes and numbers of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases or referenced in [!INCLUDE[tsql](../includes/tsql-md.md)] statements.  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] object||Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|Additional Information|  
|---------------------------------------------------------|-|------------------------------------------------------------------|----------------------------|  
|Batch size||65,536 * Network Packet Size|Network Packet Size is the size of the tabular data stream (TDS) packets used to communicate between applications and the relational [!INCLUDE[ssDE](../includes/ssde-md.md)]. The default packet size is 4 KB, and is controlled by the network packet size configuration option.|  
|Bytes per short string column||8,000||  
|Bytes per GROUP BY, ORDER BY||8,060||  
|Bytes per index key||900 bytes for a clustered index. 1,700 for a nonclustered index.|The maximum number of bytes in a clustered index key cannot exceed 900 in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a nonclustered index key, the maximum is 1700 bytes.<br /><br /> You can define a key using variable-length columns whose maximum sizes add up to more than the limit. However, the combined sizes of the data in those columns can never exceed the limit.<br /><br /> In a nonclustered index, you can include extra non-key columns, and they do not count against the size limit of the key. The non-key columns might help some queries perform better.|  
|Bytes per index key for memory-optimized tables||2500 bytes for a nonclustered index. No limit for a hash index, as long as all index keys fit in-row.|On a memory-optimized table, a nonclustered index cannot have key columns whose maximum declared sizes exceed 2500 bytes. It is irrelevant whether the actual data in the key columns would be shorter than the maximum declared sizes.<br /><br /> For a hash index key there is no hard limit on size.<br /><br /> For indexes on memory-optimized tables, there is no concept of included columns, since all indexes inherently cover of all columns.<br /><br /> For a memory-optimized table, even though the row size is 8060 bytes, some variable-length columns can be physically stored outside those 8060 bytes. However, the maximum declared sizes of all key columns for all indexes on a table, plus any additional fixed-length columns in the table, must fit in the 8060 bytes.|  
|Bytes per foreign key||900||  
|Bytes per primary key||900||  
|Bytes per row||8,060|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports row-overflow storage which enables variable length columns to be pushed off-row. Only a 24-byte root is stored in the main record for variable length columns pushed out of row; because of this, the effective row limit is higher than in previous releases of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see the "Row-Overflow Data Exceeding 8 KB" topic in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Books Online.|  
|Bytes per row in memory-optimized tables||8,060|Starting [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] memory-optimized tables support off-row storage. Variable length columns are pushed off-row if the maximum sizes for all the columns in the table exceeds 8060 bytes; this is a compile-time decision. Only an 8-byte reference is stored in-row for columns stored off-row. For more information, see [Table and Row Size in Memory-Optimized Tables](../relational-databases/in-memory-oltp/table-and-row-size-in-memory-optimized-tables.md).|  
|Bytes in source text of a stored procedure||Lesser of batch size or 250 MB||  
|Bytes per **varchar(max)**, **varbinary(max)**, **xml**, **text**, or **image** column||2^31-1||  
|Characters per **ntext** or **nvarchar(max)** column||2^30-1||  
|Clustered indexes per table||1||  
|Columns in GROUP BY, ORDER BY||Limited only by number of bytes||  
|Columns or expressions in a GROUP BY WITH CUBE or WITH ROLLUP statement||10||  
|Columns per index key||32|If the table contains one or more XML indexes, the clustering key of the user table is limited to 31 columns because the XML column is added to the clustering key of the primary XML index. In [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can include nonkey columns in a nonclustered index to avoid the limitation of a maximum of 32 key columns. For more information, see [Create Indexes with Included Columns](../relational-databases/indexes/create-indexes-with-included-columns.md).|  
|Columns per foreign key||32||  
|Columns per primary key||32||  
|Columns per nonwide table||1,024||  
|Columns per wide table||30,000||  
|Columns per SELECT statement||4,096||  
|Columns per INSERT statement||4,096||  
|Connections per client||Maximum value of configured connections||  
|Database size||524,272 terabytes||  
|Databases per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]||32,767||  
|Filegroups per database||32,767||  
|Filegroups per database for memory-optimized data||1||  
|Files per database||32,767||  
|File size (data)||16 terabytes||  
|File size (log)||2 terabytes||  
|Data files for memory-optimized data per database||4,096 in [!INCLUDE[ssSQL14](../includes/ssSQL14-md.md)]. Later versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] do not impose such a strict limit.||  
|Delta file per data file for memory-optimized data||1||  
|Foreign key table references per table||Outgoing = 253. Incoming = 10,000.|For restrictions, see [Create Foreign Key Relationships](../relational-databases/tables/create-foreign-key-relationships.md).|  
|Identifier length (in characters)||128||  
|Instances per computer||50 instances on a stand-alone server.<br /><br /> 25 instances on a failover cluster when using a shared cluster disk as the stored option for you cluster installation [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports 50 instances on a failover cluster if you choose SMB file shares as the storage option for your cluster installation.||  
|Indexes per memory-optimized table||999 starting [!INCLUDE[ssSQL17](../includes/ssSQL17-md.md)] and in [!INCLUDE[ssSDSFull](../includes/ssSDSFull-md.md)]<br/>8 in [!INCLUDE[ssSQL14](../includes/ssSQL14-md.md)] and [!INCLUDE[ssSQL15](../includes/ssSQL15-md.md)]||  
|Length of a string containing SQL statements (batch size)||65,536 * Network packet size|Network Packet Size is the size of the tabular data stream (TDS) packets used to communicate between applications and the relational [!INCLUDE[ssDE](../includes/ssde-md.md)]. The default packet size is 4 KB, and is controlled by the network packet size configuration option.|  
|Locks per connection||Maximum locks per server||  
|Locks per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]||Limited only by memory|This value is for static lock allocation. Dynamic locks are limited only by memory.|  
|Nested stored procedure levels||32|If a stored procedure accesses more than 64  databases, or more than 2 databases in interleaving, you will receive an error.|  
|Nested subqueries||32||  
|Nested trigger levels||32||  
|Nonclustered indexes per table||999||  
|Number of distinct expressions in the GROUP BY clause when any of the following are present: CUBE, ROLLUP, GROUPING SETS, WITH CUBE, WITH ROLLUP||32||  
|Number of grouping sets generated by operators in the GROUP BY clause||4,096||  
|Parameters per stored procedure||2,100||  
|Parameters per user-defined function||2,100||  
|REFERENCES per table||253||  
|Rows per table||Limited by available storage||  
|Tables per database||Limited by number of objects in a database|Database objects include objects such as tables, views, stored procedures, user-defined functions, triggers, rules, defaults, and constraints. The sum of the number of all objects in a database cannot exceed 2,147,483,647.|  
|Partitions per partitioned table or index||15,000||  
|Statistics on non-indexed columns||30,000|| 
|Tables per SELECT statement||Limited only by available resources||  
|Triggers per table||Limited by number of objects in a database|Database objects include objects such as tables, views, stored procedures, user-defined functions, triggers, rules, defaults, and constraints. The sum of the number of all objects in a database cannot exceed 2,147,483,647.|  
|Columns per UPDATE statement (Wide Tables)||4096||  
|User connections||32,767||  
|XML indexes||249||  
  
##  <a name="Utility"></a> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility Objects  
 Maximum sizes and numbers of various objects that were tested in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility.  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility object||Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|  
|----------------------------------------------|-|------------------------------------------------------------------|  
|Computers (physical computers or virtual machines) per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility||100|  
|Instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] per computer||5|  
|Total number of instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility||200*|  
|User databases per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], including data-tier applications||50|  
|Total number of user databases per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility||1,000|  
|File groups per database||1|  
|Data files per file group||1|  
|Log files per database||1|  
|Volumes per computer||3|  
  
 *The maximum number of managed instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supported by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility may vary based on the hardware configuration of the server. For getting started information, see [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md). [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility control point is not available in every edition of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](https://msdn.microsoft.com/library/cc645993.aspx).    
  
##  <a name="DAC"></a> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data-tier Application Objects  
 Maximum sizes and numbers of various objects that were tested in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] data-tier applications (DAC).  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] DAC object||Maximum sizes/numbers [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] (64-bit)|  
|------------------------------------------|-|------------------------------------------------------------------|  
|Databases per DAC||1|  
|Objects per DAC*||Limited by the number of objects in a database, or available memory.|  
  
 *The types of objects included in the limit are users, tables, views, stored procedures, user-defined functions, user-defined data type, database roles, schemas, and user-defined table types.  
  
##  <a name="Replication"></a> Replication Objects  
 Maximum sizes and numbers of various objects defined in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Replication.  
  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Replication object||Maximum sizes/numbers SQL Server (64-bit)|  
|--------------------------------------------------|-|---------------------------------------------------|  
|Articles (merge publication)||2048|  
|Articles (snapshot or transactional publication)||32,767|  
|Columns in a table* (merge publication)||246|  
|Columns in a table** ([!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] snapshot or transactional publication)||1,000|  
|Columns in a table** (Oracle snapshot or transactional publication)||995|  
|Bytes for a column used in a row filter (merge publication)||1,024|  
|Bytes for a column used in a row filter (snapshot or transactional publication)||8,000|  

 *If row tracking is used for conflict detection (the default), the base table can include a maximum of 1,024 columns, but columns must be filtered from the article so that a maximum of 246 columns is published. If column tracking is used, the base table can include a maximum of 246 columns.  
  
 **The base table can include the maximum number of columns allowable in the publication database (1,024 for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]), but columns must be filtered from the article if they exceed the maximum specified for the publication type.  
  
## See Also  
 [Hardware and Software Requirements for Installing SQL Server 2016](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)   
 [Check Parameters for the System Configuration Checker](../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)   
 [SQL Server Utility Features and Tasks](../relational-databases/manage/sql-server-utility-features-and-tasks.md)  
  
  
