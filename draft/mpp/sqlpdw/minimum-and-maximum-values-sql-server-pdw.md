---
title: "Minimum and Maximum Values (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5243f018-2713-45e3-9b61-39b2a57401b9
caps.latest.revision: 70
author: BarbKess
---
# Minimum and Maximum Values (SQL Server PDW)
The following tables show boundaries and constraints on various SQL Server PDW objects and commands.  
  
## Contents  
  
-   [Minimum SQL Values](#MinSQL)  
  
-   [Maximum SQL Values](#MaxSQL)  
  
-   [Maximum System View Values](#SystemViews)  
  
-   [Password Age, Tools, and Processes Values](#MaxTools)  
  
## <a name="MinSQL"></a>Minimum SQL Values  
The following table specifies the minimum numbers and sizes of various objects defined in SQL Server PDW databases or referenced in SQL statements.  
  
||||  
|-|-|-|  
|**Object or Command**|**Boundary**|**Minimum Constraint**|  
|Database|Compatibility level|120<br /><br />All SQL Server PDW Appliance Update 5  user databases have database compatibility level 120, which is the compatibility level for SQL Server 2014. All databases from previous versions of PDW will have database compatibility level 120 after they have been upgraded or restored to SQL Server PDW Appliance Update 5 or a later appliance update.|  
|Database|Replicated size.|0.0625 GB<br /><br />The replicated size is specified as the amount allocated *per* Compute node. It is not the total amount allocated per appliance.|  
|Database|Distributed size.|0.0625 GB per Compute node times the number of Compute nodes in the appliance.<br /><br />0.0625 GB per Compute node = (2 drives per distribution) * (1 file per drive) \* (4 MB per file) \* ( 1 GB / 1024 MB) \* (8 distributions per Compute node).|  
|Database|Log size.|0.0625 GB per Compute node times the number of Compute nodes in the appliance.<br /><br />`--Create a database by using the minimum sizes`<br /><br />`--for an appliance with 2 Compute nodes.`<br /><br />`CREATE DATABASE TwoNodeMinSize`<br /><br />`WITH (`<br /><br />`AUTOGROW=ON,`<br /><br />`REPLICATED_SIZE = 0.0625 GB,`<br /><br />`DISTRIBUTED_SIZE = 0.125 GB,`<br /><br />`LOG_SIZE = 0.125 GB`<br /><br />`);`|  
|Index|Rows per CLOSED or COMPRESSED rowgroup in a clustered columnstore index.|102,400<br /><br />This is the minimum number of rows that can be added to the clustered columnstore index at the same time.|  
  
## <a name="MaxSQL"></a>Maximum SQL Values  
The following table specifies the maximum numbers and sizes of various objects defined in SQL Server PDW databases or referenced in SQL statements.  
  
||||  
|-|-|-|  
|**Object or Command**|**Boundary**|**Maximum Constraint**|  
|Query|Concurrent queries on user tables.|32|  
|Query|Queued queries on user tables.|1000|  
|Query|Concurrent queries on system views.|100|  
|Query|Queued queries on system views.|1000|  
|Batch|Maximum size.|65,536 * 4096 bytes.|  
|Query|Maximum parameters.|2098|  
|Session|Open sessions.|1024|  
|Session|Maximum memory for prepared statements.|20 MB|  
|Database|Databases per SQL Server PDW instance.|32,767|  
|Table|Tables per database.|2 billion|  
|Table|Columns per table.|1024 columns|  
|Table|Bytes per column|8000 bytes|  
|Table|Bytes per row, SQL Server defined size|8060 bytes<br /><br />This is a SQL Server requirement that the fixed size of each row must fit on one memory page. This limit is enforced in SQL Server PDW because SQL Server must be able to store each row. Bytes per row is computed the same as SQL Server with page compression ON.<br /><br />SQL Server PDW will give a compile error if the size of the row exceeds the SQL Server maximum definition size of 8060 bytes.<br /><br />To estimate the SQL Server row size, see the row size calculations in [Estimate the Size of a Clustered Index](https://msdn.microsoft.com/library/ms178085.aspx) on MSDN.<br /><br />For a list of PDW data type sizes, see [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md).|  
|Table|Bytes per row, DMS buffer defined size|32,768<br /><br />DMS stores rows in a different format than SQL Server. To improve performance, DMS pads all variable length data to the maximum SQL Server defined size when it loads a row into the DMS buffer. For example, the value 'hello' for an nvarchar(2000) NOT NULL will actually use 4002 bytes in the DMS buffer since nvarchar NOT NULL types require 2 bytes per character plus 2 bytes for the NULL terminator.<br /><br />For more information about the DMS buffer format, see [A. Compute the DMS buffer row size](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md#dmssize) in the Examples section.<br /><br />Data movement operations will fail when a row’s defined DMS buffer size exceeds the DMS buffer size of 32,768. Loading data with dwloader and performing distribution incompatible joins are some examples of operations that require DMS to move data between nodes; these operations will fail when DMS tries to move a row that has a DMS buffer size of more than 32,768 bytes.<br /><br />Both adding rows with the INSERT statement and loading data with Integration Services use a different code path than loading rows with dwloader. When using either of these to load, DMS moves the rows, but does not convert the row into the DMS buffer format. Therefore, it is possible to successfully load a row into PDW and then get a failure when DMS tries to move the row.<br /><br />For example, you might be able to use INSERT or Integration Services to load a row that has a SQL Server defined row-size of 40,000 bytes, provided the actual content of the row does not exceed 32,768 bytes, and then get an error when DMS tries to move the row for a distribution-incompatible join.<br /><br />For a detailed example, see [B. Exceeding the DMS buffer size](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md#dmsbuffer) in the Examples section.|  
|Table|Partitions per table.|15,000.<br /><br />For high performance, we recommend minimizing the number of partitions you need while still supporting your business requirements. As the number of partitions grows, the overhead for DDL and DML operations grows and causes slower performance.|  
|Table|Characters per partition boundary value.|4000|  
|Index|Non-clustered indexes per table.|999<br /><br />Applies to rowstore tables only.|  
|Index|Clustered indexes per table.|1<br /><br />Applies to both rowstore and columnstore tables.|  
|Index|Rows per rowgroup in a clustered columnstore index.|1,048,576<br /><br />This is the maximum number of rows that can be added to the clustered columnstore index at the same time. The number of rows per rowgroup has a minimum and a maximum.|  
|Index|Concurrent builds of clustered columnstore indexes.|32<br /><br />Applies when the clustered columnstore indexes are all being built on different tables. Only 1 clustered columnstore index build is allowed per table. Additional requests will wait in a queue.|  
|Index|Index key size.|900 bytes.<br /><br />Applies to rowstore indexes only.<br /><br />Indexes on **varchar** columns that exceed 900 bytes can be created if the existing data in the columns does not exceed 900 bytes when the index is created. However, later INSERT or UPDATE actions on the columns that cause the total size to exceed 900 bytes will fail.|  
|Index|Key columns per index.|16<br /><br />Applies to rowstore indexes only. Clustered columnstore indexes include all columns.|  
|Statistics|Size of the combined column values.|900 bytes.|  
|Statistics|Columns per statistics object.|32|  
|Statistics|Statistics created on columns per table.|30,000|  
|Stored Procedures|Maximum levels of nesting.|8|  
|View|Columns per view|1,024|  
|Logins|Logins per SQL Server PDW instance.|500,000|  
|SELECT results|Columns per row|4096<br /><br />You can never have more than 4096 columns per row in the SELECT result. There is no guarantee that you can always have 4096. If the query plan requires a temporary table, the 1024 columns per table maximum might apply.|  
|SELECT results|Bytes per row|>8060<br /><br />The number of bytes per row in the SELECT result can be more than the 8060 byte maximum that is allowed for table rows. If the query plan for the SELECT statement requires a temporary table, the 8060 byte table maximum might apply.|  
|SELECT results|Bytes per column|>8000<br /><br />The number of bytes per column in the SELECT result can be more than the 8000 byte maximum that is allowed for table columns. If the query plan for the SELECT statement requires a temporary table, the 8000 byte table maximum might apply.|  
|SELECT|Nested subqueries|32<br /><br />You can never have more than 32 nested subqueries in a SELECT statement. There is no guarantee that you can always have 32. For example, a JOIN can introduce a subquery into the query plan. The number of subqueries can also be limited by available memory.|  
|SELECT|Columns per JOIN|1024 columns<br /><br />You can never have more than 1024 columns in the JOIN. There is no guarantee that you can always have 1024. If the JOIN plan requires a temporary table with more columns than the JOIN result, the 1024 limit applies to the temporary table.|  
|SELECT|Bytes per GROUP BY columns.|8060<br /><br />The columns in the GROUP BY clause can have a maximum of 8060 bytes.|  
|SELECT|Bytes per ORDER BY columns|8060 bytes.<br /><br />The columns in the ORDER BY clause can have a maximum of 8060 bytes.|  
|INSERT|Bytes per column, fixed and variable width.|8000 bytes. Attempts to insert more bytes than defined for the column will result in an error.|  
|INSERT|Bytes per row, variable width columns.|>8060 bytes. Data in excess of 8060 bytes is placed in overflow storage area.|  
|UPDATE|Bytes per column, fixed and variable width.|8000 bytes. Attempts to update a column to a value requiring more bytes than defined for the column will result in an error.|  
|Identifiers and constants per statement|Number of referenced identifiers and constants.|65,535<br /><br />SQL Server limits the number of identifiers and constants that can be contained in a single expression of a query. This limit is 65,535. Exceeding this number results in SQL Server error 8632. For more information, see [Error message when you run a query in SQL Server 2005: "Internal error: An expression services limit has been reached"](http://support.microsoft.com/kb/913050).|  
  
## <a name="SystemViews"></a>Maximum System View Values  
  
|**System View**|**Maximum Rows**|  
|-------------------|--------------------|  
|sys.dm_pdw_component_health_alerts|10,000|  
|sys.dm_pdw_dms_cores|100|  
|sys.dm_pdw_dms_workers|Total number of DMS workers for the most recent 1000 SQL requests.|  
|sys.dm_pdw_dms_worker_pairs|10,000|  
|sys.dm_pdw_errors|10,000|  
|sys.dm_pdw_exec_requests|10,000|  
|sys.dm_pdw_exec_sessions|10,000|  
|sys.dm_pdw_request_steps|Total number of steps for the most recent 1000 SQL requests that are stored in sys.dm_pdw_exec_requests.|  
|sys.dm_pdw_os_event_logs|10,000|  
|sys.dm_pdw_sql_requests|The most recent 1000 SQL requests that are stored in sys.dm_pdw_exec_requests.|  
  
## <a name="MaxTools"></a>Password Age, Tools, and Processes Values  
  
||||  
|-|-|-|  
|**Tool or Process**|**Boundary**|**Maximum Constraint**|  
|Client Tool|SQL Server Compatibility|SQL Server 2008 SP2<br /><br />For SQL Server compatibility, SQL Server PDW reports the SQL Server product version by using a SQL Server 2008 Service Pack 2 version number. For example, the statement `SELECT @@VERSION` returns a version number incremented from 10.00.4000.00 which indicates SQL Server 2008 Service Pack 2 (SP2).<br /><br />For a client tool that is maintaining compatibility with SQL Server, the version number implies the client should only send statements that are supported by SQL Server 2008 SP2. The version number also implies that if a client tool works with SQL Server 2008 SP2, it will work with SQL Server PDW,  provided the client tool uses functionality that is available in PDW.For a list of differences between supported SQL Server and SQL Server PDW commands, see [SQL Server to SQL Server PDW Migration Guide](http://download.microsoft.com/download/4/2/6/42616D71-3488-46E2-89F0-E516C10F6576/SQL_Server_to_SQL_Server_PDW_Migration_Guide.pdf) at [http://upgradetopdw.com](http://upgradetopdw.com).<br /><br />Some tools recognize the client is SQL Server PDW rather than an instance of SMP SQL Server. Such tools are not trying to maintain SQL Server compatibility, and so they issue statements according to what SQL Server PDW supports. For example, SQL Server Data Tools sends clustered columnstore index commands and other commands to PDW that are beyond the scope of SQL Server 2008 SP2.|  
|Data Loads|Active loads per appliance.|10<br /><br />The combined total of loads with dwloader and Integration Services is 10 active loads per appliance. When this maximum is exceeded, the loads will wait in the load queue.<br /><br />For more information about how this maximum relates to the maximum number of destinations in an Integration Services PDW package, see the Limitations and Restrictions section in [Load Data With Integration Services &#40;SQL Server PDW&#41;](../sqlpdw/load-data-with-integration-services-sql-server-pdw.md).|  
|Data Loads|Queued loads per appliance.|40<br /><br />Loads are blocked from running if they cause the number of queued loads to exceed this maximum.|  
|Data Loads|Files per load.|10,000<br /><br />[dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md) can load a maximum of 10,000 files from the same directory.|  
|Database Backups|Active database backups per appliance.|1|  
|Database Backups|Concurrent Compute node backups per database backup.|4<br /><br />A user database backup requires a backup from each Compute node. SQL Server PDW allows 4 concurrent Compute node backups for each database backup.|  
|Domain Administrator Password|Days per password.|60 days.<br /><br />The domain administrator password for the appliance domain expires after 60 days.|  
|Password age|Days per password|Minimum 1 day to maximum 42 days.|  
|Minimum password length|Password characters|7 to 128 characters.<br /><br />For more information on password policies, see [CREATE LOGIN &#40;SQL Server PDW&#41;](../sqlpdw/create-login-sql-server-pdw.md).|  
  
## <a name="examples"></a>Examples  
  
### <a name="dmssize"></a>A. Compute the DMS buffer row size  
This example describes the DMS defined sizes of variable-length data and then calculates the DMS buffer size for a row.format.  
  
In the DMS buffer, variable-length data size is the sum of the following:  
  
-   Defined size for characters.  
  
-   NULL types use 8 bytes for the NULL indicator.  
  
-   ASCII types use 1 character for the NULL terminator.  
  
-   Unicode types use 2 characters for the NULL terminator.  
  
|Data Type|DMS buffer size|  
|-------------|-------------------|  
|char(1000) NULL|1009 bytes = 1000 +8 + 1|  
|char(1000) NOT NULL|1001 bytes = 1000 + 1|  
|nchar(1000 NULL|2010 bytes = 2000 + 8 + 2|  
|varchar(1000) NULL|1009 bytes = 1000 + 8 + 1|  
|varchar(1000) NOT NULL|1009 bytes = 1000  + 1|  
|nvarchar(1000) NULL|2010 bytes = 2000 + 8 + 2|  
|nvarchar(1000) NOT NULL|2010 bytes = 2000  + 2|  
  
In the DMS buffer, fixed-width columns use the SQL Server native size. If the type is nullable, DMS requires an extra 8 bytes. For the SQL Server size, see the max_length field in [sys.types &#40;SQL Server PDW&#41;](../sqlpdw/sys-types-sql-server-pdw.md).  
  
For example:  
  
|Fixed-Width Data Type|DMS buffer size|  
|--------------------------|-------------------|  
|int NULL|12 bytes = 4 + 8|  
|int NOT NULL|4 bytes = 4 + 0|  
  
Putting it altogether, the DMS buffer defined size for the following row is 31,134 bytes, which will fit in the DMS buffer.  
  
|Column|Data Type|Column Size|  
|----------|-------------|---------------|  
|col1|datetime2 (7) NULL|20 bytes = 8 + 8|  
|col2|float (4) NULL|12 bytes = 4 + 8|  
|col3|nchar (6) NULL|22 bytes = 12 + 8 + 2|  
|col4|char (7000) NULL|7009 bytes = 7000 + 8 + 1.|  
|col5|nvarchar (4000)|8002 bytes = 8000 + 2.|  
|col6|varchar (8000) NULL|8009 bytes = 8000 + 8 + 1|  
|col7|varbinary (8000)|8000 bytes|  
|col8|binary (60)|60 bytes|  
  
### <a name="dmsbuffer"></a>B. Exceeding the DMS buffer size  
This example shows how you could successfully insert a row into PDW, but then incur a DMS overflow error when DMS needs to move the row for a distribution incompatible join. The lesson to be learned is to create smaller rows that will fit into the DMS buffer.  
  
In the following example, we create table T1. The maximum possible size of the row when all the nvarchar variables have 4000 Unicode characters is more than 40,000 bytes, which exceeds the maximum DMS buffer size.  
  
Since the actual defined size of an nvarchar uses 26 bytes, the row definition is less than 8060 bytes and can fit on a SQL Server page. Therefore the CREATE TABLE statement succeeds, even though DMS will fail when it tries to load this row into the DMS buffer.  
  
```  
CREATE TABLE T1  
  (  
    c0 int NOT NULL,  
    CustomerKey int NOT NULL,  
    c1 nvarchar(4000),  
    c2 nvarchar(4000),  
    c3 nvarchar(4000),  
    c4 nvarchar(4000),  
    c5 nvarchar(4000)  
  )  
WITH ( DISTRIBUTION = HASH (c0) )  
;  
```  
  
This next step shows that we can successfully use INSERT to insert data into the table. This statement loads the data directly into SQL Server without using DMS, and so it does not incur a DMS buffer overflow failure. Integration Services will also load this row successfully.  
  
```  
--The INSERT operation succeeds because the row is inserted directly into SQL Server without requiring DMS to buffer the row.  
INSERT INTO T1  
VALUES (  
    25,  
    429817,  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.'  
  )  
```  
  
To prepare for demonstrating data movement, this example creates a second table with CustomerKey for the distribution column.  
  
```  
--This second table is distributed on CustomerKey.   
CREATE TABLE T2  
  (  
    c0 int NOT NULL,  
    CustomerKey int NOT NULL,  
    c1 nvarchar(4000),  
    c2 nvarchar(4000),  
    c3 nvarchar(4000),  
    c4 nvarchar(4000),  
    c5 nvarchar(4000)  
  )  
WITH ( DISTRIBUTION = HASH (CustomerKey) )  
;  
  
INSERT INTO T2  
VALUES (  
    32,  
    429817,  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.',  
    N'Each row must fit into the DMS buffer size of 32,768 bytes.'  
  )  
```  
  
Since both tables are not distributed on CustomerKey, a join between T1 and T2 on CustomerKey is distribution incompatible. DMS will need to load at least one row and copy it to a different distribution.  
  
```  
SELECT * FROM T1 JOIN T2 ON T1.CustomerKey = T2.CustomerKey;  
```  
  
As expected, DMS fails to perform the join because the row, when all the nvarchar columns are padded, is larger than the DMS buffer size of 32,768 bytes. The following error message occurs.  
  
```  
Msg 110802, Level 16, State 1, Line 126  
An internal DMS error occurred that caused this operation to fail. Details: Exception: Microsoft.SqlServer.DataWarehouse.DataMovement.Workers.DmsSqlNativeException, Message: SqlNativeBufferReader.ReadBuffer, error in OdbcReadBuffer: SqlState: , NativeError: 0, 'COdbcReadConnection::ReadBuffer: not enough buffer space for one row | Error calling: pReadConn->ReadBuffer(pBuffer, bufferOffset, bufferLength, pBytesRead, pRowsRead) | state: FFFF, number: 81, active connections: 8', Connection String: Driver={SQL Server Native Client 11.0};APP=DmsNativeReader:P13521-CMP02\sqldwdms (4556) - ODBC;Trusted_Connection=yes;AutoTranslate=no;Server=P13521-SQLCMP02,1500  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
