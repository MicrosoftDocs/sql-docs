---
title: "CREATE REMOTE TABLE AS SELECT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9836df41-beb0-4bae-96f8-ab8935f89499
caps.latest.revision: 58
author: BarbKess
---
# CREATE REMOTE TABLE AS SELECT (SQL Server PDW)
Selects data from a SQL Server PDW database and copies that data to a new table in a SMP SQL Server database on a remote server. SQL Server PDW uses the appliance, with all the benefits of MPP query processing, to select the data for the remote copy. Use this for scenarios that require SQL Server functionality.  
  
To configure the remote server, see [Remote Table Copy &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/remote-table-copy-sql-server-pdw.md).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE REMOTE TABLE [ database_name . [ schema_name ] . | schema_name. ] table_name AT ('<connection_string>')  
    [ WITH ( BATCH_SIZE = batch_size ) ]  
    AS <select_statement>  
[;]  
  
<connection_string> ::=   
    Data Source = { IP_address | hostname } [, port ]; User ID = user_name ;Password = password;  
  
<select_statement> ::=  
    [ WITH <common_table_expression> [ ,...n ] ]  
    SELECT <select_criteria>  
```  
  
## Arguments  
*database_name*  
The database to create the remote table in. *database_name* is a SQL Server database. Default is the default database for the user login on the destination SQL Server instance.  
  
*schema_name*  
The schema for the new table. Default is the default schema for the user login on the destination SQL Server instance.  
  
*table_name*  
The name of the new table. For details on permitted table names, see [Object Naming Rules &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/object-naming-rules-sql-server-pdw.md).  
  
The remote table is created as a heap. It does not have check constraints or triggers. The collation of the remote table columns is the same as the collation of the source table columns. This applies to columns of type **char**, **nchar**, **varchar**, and **nvarchar**.  
  
*connection_string*  
A character string that specifies the `Data Source`, `User ID`, and `Password` parameters for connecting to the remote server and database.  
  
The connection string is a semicolon-delimited list of key and value pairs. Keywords are not case-sensitive. Spaces between key and value pairs are ignored. However, values may be case-sensitive, depending on the data source.  
  
*Data Source*  
The parameter that specifies the name or IP address, and TCP port number for the remote SMP SQL Server.  
  
*hostname* or *IP_address*  
Name of the remote server computer or the IPv4 address of the remote server. IPv6 addresses are not supported. You can specify a SQL Server named instance in the format **Computer_Name\Instance_Name** or **IP_address\Instance_Name**. The server must be remote and therefore cannot be specified as (local).  
  
TCP *port* number  
The TCP port number for the connection. You can specify a TCP port number from 0 to 65535 for an instance of SQL Server that is not listening on the default port 1433. For example: **ServerA,1450** or **10.192.14.27,1435**  
  
> [!NOTE]  
> We recommend connecting to a remote server by using the IP address. Depending on your network configuration, connecting by using the computer name might require additional steps to use your non-appliance DNS server to resolve the name to the correct server. This step is not necessary when connecting with an IP address. For more information, see [Use a DNS Forwarder to Resolve Non-Appliance DNS Names &#40;Analytics Platform System&#41;](../../mpp/management/use-a-dns-forwarder-to-resolve-non-appliance-dns-names-analytics-platform-system.md).  
  
*user_name*  
A valid SQL Server authentication login name. Maximum number of characters is 128.  
  
*password*  
The login password. Maximum number of characters is 128.  
  
*batch_size*  
The maximum number of rows per batch. SQL Server PDW sends rows in batches to the destination server. *Batch_size* is a positive integer >= 0. Default is 0.  
  
WITH *common_table_expression*  
Specifies a temporary named result set, known as a common table expression (CTE). For more information, see [WITH common_table_expression &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/with-common-table-expression-sql-server-pdw.md).  
  
SELECT <select_criteria>  
The query predicate that specifies which data will populate the new remote table. For information on the SELECT statement, see [SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/select-sql-server-pdw.md).  
  
## Permissions  
Requires:  
  
-   SELECT permission on each object in the SELECT clause.  
  
-   Requires CREATE TABLE permission on the destination SMP database.  
  
-   Requires ALTER, INSERT, and SELECT permissions on the destination SMP schema.  
  
## Error Handling  
If copying data to the remote database fails, SQL Server PDW will abort the operation, log an error, and attempt to delete the remote table. SQL Server PDW does not guarantee a successful cleanup of the new table.  
  
## Limitations and Restrictions  
**Remote Destination Server**:  
  
-   TCP is the default and only supported protocol for connecting to a remote server.  
  
-   The destination server must be a non-appliance server. CREATE REMOTE TABLE cannot be used to copy data from one appliance to another.  
  
-   The CREATE REMOTE TABLE statement only creates new tables. Therefore, the new table cannot already exist. The remote database and schema must already exist.  
  
-   The remote server must have space available to store the data that is transferred from the appliance to the SQL Server remote database.  
  
**SELECT statement**:  
  
-   The ORDER BY and TOP clauses are not supported in the select criteria.  
  
-   CREATE REMOTE TABLE cannot be run inside an active transaction or when the AUTOCOMMIT OFF setting is active for the session.  
  
[SET ROWCOUNT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/set-rowcount-sql-server-pdw.md) has no effect on this statement. To achieve a similar behavior, use [TOP &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/top-sql-server-pdw.md).  
  
## Locking Behavior  
After creating the remote table, the destination table is not locked until the copy starts. Therefore, it is possible for another process to delete the remote table after it is created and before the copy starts. When this occurs, SQL Server PDW will generate an error and the copy will fail.  
  
## Metadata  
Use [sys.dm_pdw_dms_workers &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-dms-workers-sql-server-pdw.md) to view the progress of copying the selected data to the remote SMP server. Rows with type PARALLEL_COPY_READER contain this information.  
  
## Security  
CREATE REMOTE TABLE uses SQL Server Authentication to connect to the remote SQL Server instance; it does not use Windows Authentication.  
  
The SQL Server PDW external facing network must be firewalled, with exception of SQL Server ports, administrative ports, and management ports.  
  
To help prevent accidental data loss or corruption, the user account that is used to copy from the appliance to the destination database should have only the minimum required permissions on the destination database.  
  
Connection settings allow you to connect to the SMP SQL Server instance with SSL protecting user name and password data, but with actual data being sent unencrypted in clear text. When this occurs, a malicious user could intercept the CREATE REMOTE TABLE statement text, which contains the SQL Server user name and password to log onto the SMP SQL Server instance. To avoid this risk, use data encryption on the connection to the SMP SQL Server instance.  
  
## <a name="Examples"></a>Examples  
  
### A. Creating a remote table  
This example creates a SQL Server SMP remote table called `MyOrdersTable` on database `OrderReporting` and schema `Orders`. The `OrderReporting` database is on a server named `SQLA` that listens on the default port 1433. The connection to the server uses the credentials of the user `David`, whose password is `e4n8@3`.  
  
```  
CREATE REMOTE TABLE OrderReporting.Orders.MyOrdersTable  
AT ( 'Data Source = SQLA, 1433; User ID = David; Password = e4n8@3;' )  
AS SELECT <select_criteria>;  
```  
  
### B. Querying the sys.dm_pdw_dms_workers DMV for remote table copy status  
This query shows how to view copy status for a remote table copy.  
  
```  
SELECT * FROM sys.dm_pdw_dms_workers   
WHERE type = 'PARALLEL_COPY_READER';  
```  
  
### C. Using a query join hint with CREATE REMOTE TABLE  
This query shows the basic syntax for using a query join hint with CREATE REMOTE TABLE. After the query is submitted to the Control node, SQL Server, running on the Compute nodes, will apply the hash join strategy when generating the SQL Server query plan. For more information on join hints and how to use the OPTION clause, see [OPTION &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/option-sql-server-pdw.md).  
  
```  
USE AdventureWorksPDW2012;  
CREATE REMOTE TABLE OrderReporting.Orders.MyOrdersTable  
AT ( 'Data Source = SQLA, 1433; User ID = David; Password = e4n8@3;' )  
    AS SELECT T1.* FROM OrderReporting.Orders.MyOrdersTable T1   
    JOIN OrderReporting.Order.Customer T2  
    ON T1.CustomerID=T2.CustomerID OPTION (HASH JOIN);  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
