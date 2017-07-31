---
title: "CREATE DATABASE (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "07/31/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "SERVICE_OBJECTIVE"
  - "SERVICE_OBJECTIVE_TSQL"
  - "ELASTIC_POOL"
  - "ELASTIC_POOL_TSQL"
  - "EDITION"
  - "EDITION_TSQL"
  - "MAXSIZE"
  - "MAXSIZE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "SERVICE_OBJECTIVE"
  - "ELASTIC_POOL"
  - "EDITION SQL Database"
  - "MAXSIZE SQL Database"
ms.assetid: 22b167f7-ae86-490b-adb3-ec02ca1c1508
caps.latest.revision: 62
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CREATE DATABASE (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Creates a new database. You must be connected to the master database to create a new database.  
  
## Syntax  
  
```  
  
CREATE DATABASE database_name [ COLLATE collation_name ]  
{  
   (<edition_options> [, ...n])   
}  

[ WITH CATALOG_COLLATION = { DATABASE_DEFAULT | SQL_Latin1_General_CP1_CI_AS }  ]
  
<edition_options> ::=   
{  
      MAXSIZE = { 100 MB | 500 MB | 1 | 5 | 10 | 20 | 30 … 150…500 } GB    
    | ( EDITION = {  'basic' | 'standard' | 'premium' | 'premiumrs'}   
    | SERVICE_OBJECTIVE =   
          {  'basic' | 'S0' | 'S1' | 'S2' | 'S3'   
            | 'P1' | 'P2' | 'P3' | 'P4'| 'P6' | 'P11'  | 'P15'  
            | 'PRS1' | 'PRS2' | 'PRS4' | 'PRS6' 
            | { ELASTIC_POOL(name = <elastic_pool_name>) } }  ) 
}  
[;]  
  
```
  
## Arguments  
 This syntax diagram demonstrates the supported arguments in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 *database_name*  
 The name of the new database. This name must be unique on the SQL server, which can host both [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] databases and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] databases, and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](http://go.microsoft.com/fwlink/p/?LinkId=180386).  
  
 *Collation_name*  
 Specifies the default collation for the database. Collation name can be either a Windows collation name or a SQL collation name. If not specified, the database is assigned the default collation, which is  SQL_Latin1_General_CP1_CI_AS.  
  
 For more information about the Windows and SQL collation names, [COLLATE (Transact-SQL)](http://msdn.microsoft.com/library/ms184391.aspx).  
  
 *CATALOG_COLLATION*  
Specifies the default collation for the metadata catalog. *DATABASE_DEFAULT* specifies that the metadata catalog used for system views and system tables be collated to match the default collation for the database.  This is the behavior found in SQL Server. 

*SQL_Latin1_General_CP1_CI_AS* specifies that the metadata catalog used for system views and tables be collated to a fixed SQL_Latin1_General_CP1_CI_AS collation.  This is the default setting on Azure SQL Database if unspecified.

 *EDITION*  
 Specifies the service tier of the database. The available values are: 'basic', 'standard', 'premium', and 'premiumrs'.  
  
 When EDITION is specified but MAXSIZE is not specified, MAXSIZE will be set to the most restrictive size that the edition supports.  
  
 *MAXSIZE*  
 Specifies the maximum size of the database. MAXSIZE must be valid for the specified EDITION (service tier) Following are the supported MAXSIZE values and defaults (D) for the service tiers.  
  
|**MAXSIZE**|**Basic**|**Standard**|**Premium**| **Premium RS** 
|-----------------|---------------|------------------|-----------------|-----------------|  
|100 MB|√|√|√|√|  
|500 MB|√|√|√|√|  
|1 GB|√|√|√|√|  
|2 GB|√ (D)|√|√|√|  
|5 GB||√|√|√|  
|10 GB||√|√|√|  
|20 GB||√|√|√|  
|30 GB||√|√|√|  
|40 GB||√|√|√|  
|50 GB||√|√|√|  
|100 GB||√|√|√|  
|150 GB||√|√|√|  
|200 GB||√|√|√|  
|250 GB||√ (D)|√|√|  
|300 GB|||√|√|  
|400 GB|||√|√|  
|500 GB|||√ (D) \* |√|  
  
 \* Premium P11 and P15 allow a larger MAXSIZE of up to 4 TB, with 1024 GB being the default size. Customers using P11 and P15 performance levels can use up to 4 TB of included storage at no additional charge. This 4 TB option is currently in public preview in the following regions: US East2, West US, West Europe, South East Asia, Japan East, Australia East, Canada Central, and Canada East. For current limitations, see [Current 4 TB limitations](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-service-tiers#current-limitations-of-p11-and-p15-databases-with-4-tb-maxsize).  
  
 The following rules apply to MAXSIZE and EDITION arguments:  
  
-   The MAXSIZE value, if specified, has to be a valid value shown in the table above.  
  
-   If EDITION is specified but MAXSIZE is not specified, the default value for the edition is used. For example, is the EDITION is set to Standard, and the MAXSIZE is not specified, then the MAXSIZE is automatically set to 500 MB.  
  
-   If neither MAXSIZE nor EDITION is specified, the EDITION is set to Standard (S0), and MAXSIZE is set to 250 GB.  
  
 SERVICE_OBJECTIVE  
 Specifies the performance level. For service objective descriptions and more information about the size, editions, and the service objectives combinations, see [Azure SQL Database Service Tiers and Performance Levels](https://azure.microsoft.com/documentation/articles/sql-database-service-tiers/). If the specified SERVICE_OBJECTIVE is not supported by the EDITION you receive an error.  
  
 ELASTIC_POOL (name = <elastic_pool_name>)  
 To create a new database in an elastic database pool, set the SERVICE_OBJECTIVE of the database to ELASTIC_POOL and provide the name of the pool. For more information, see [Create and manage a SQL Database elastic database pool (preview)](https://azure.microsoft.com/documentation/articles/sql-database-elastic-pool-portal/).  
  
 *AS COPY OF [source_server_name.]source_database_name*  
 For copying a database to the same or a different [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server.  
  
 *source_server_name*  
 The name of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server where the source database is located. This parameter is optional when the source database and the destination database are to be located on the same [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server.  
  
 Note: The `AS COPY OF` argument does not support the fully qualified unique domain names. In other words, if your server's fully qualified domain name is `serverName.database.windows.net`, use only `serverName` during database copy.  
  
 *source_database_name*  
 The name of the database that is to be copied.  
  
 [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] does not support the following arguments and options when using the `CREATE DATABASE` statement:  
  
-   Parameters related to the physical placement of file, such as \<filespec> and \<filegroup>  
  
-   External access options, such as DB_CHAINING and TRUSTWORTHY  
  
-   Attaching a database  
  
-   Service broker options, such as ENABLE_BROKER, NEW_BROKER, and ERROR_BROKER_CONVERSATIONS  
  
-   Database snapshot  
  
 For more information about the arguments and the `CREATE DATABASE` statement, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md).  
  
## Remarks  
 Databases in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] have several default settings that are set when the database is created. For more information about these default settings, see the list of values in [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md).  
  
 MAXSIZE provides the ability to limit the size of the database. If the size of the database reaches its MAXSIZE you will receive error code 40544. When this occurs, you cannot insert or update data, or create new objects (such as tables, stored procedures, views, and functions). However, you can still read and delete data, truncate tables, drop tables and indexes, and rebuild indexes. You can then update MAXSIZE to a value larger than your current database size or delete some data to free storage space. There may be as much as a fifteen-minute delay before you can insert new data.  
  
> [!IMPORTANT]  
>  The `CREATE DATABASE` statement must be the only statement in a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch. You must be connected to the **master** database when executing the `CREATE DATABASE` statement.  
  
 To change the size, edition, or service objective values later, use [ALTER DATABASE &#40;Azure SQL Database&#41;](../../t-sql/statements/alter-database-azure-sql-database.md).  

The CATALOG_COLLATION argument is only available during database creation. 
  
## Database Copies  
 Copying a database using the `CREATE DATABASE` statement is an asynchronous operation. Therefore, a connection to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server is not needed for the full duration of the copy process. The `CREATE DATABASE` statement will return control to the user after the entry in sys.databases is created but before the database copy operation is complete. In other words, the `CREATE DATABASE` statement returns successfully when the database copy is still in progress.  
  
-   Monitoring the copy process on an [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] server: Query the `percentage_complete` or `replication_state_desc` columns in the [dm_database_copies](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md) or the `state` column in the **sys.databases** view. The [sys.dm_operation_status](../../relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database.md) view can be used as well as it returns the status of database operations including database copy.  
  
 At the time the copy process completes successfully, the destination database is transactionally consistent with the source database.  
  
 The following syntax and semantic rules apply to your use of the `AS COPY OF` argument:  
  
-   The source server name and the server name for the copy target may be the same or different. When they are the same, this parameter is optional and the server context of the current session will be used by default.  
  
-   The source and destination database names must be specified, unique, and comply with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rules for identifiers. For more information, see [Identifiers](http://go.microsoft.com/fwlink/p/?LinkId=180386).  
  
-   The `CREATE DATABASE` statement must be executed within the context of the master database of the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server where the new database will be created.  
  
-   After the copying completes, the destination database must be managed as an independent database. You can execute the `ALTER DATABASE` and `DROP DATABASE` statements against the new database independently of the source database. You can also copy the new database to another new database.  
  
-   The source database may continue to be accessed while the database copy is in progress.  
  
 For more information, see [Create a copy of an Azure SQL database using Transact-SQL](https://azure.microsoft.com/documentation/articles/sql-database-copy-transact-sql/).  
  
## Permissions  
 To create a database a login must be one of the following:  
  
-   The server-level principal login  
  
-   The Azure AD administrator for the local Azure SQL Server  
  
-   A login that is a member of the `dbmanager` database role  
  
 **Additional requirements for using `CREATE DATABASE ... AS COPY OF` syntax:** The login executing the statement on the local server must also be at least the `db_owner` on the source server. If the login  is based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, the login executing the statement on the local server must have a matching login on the source [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server, with an identical name and password.  
  
## Examples  
 All examples must be executed while connected to the **master** database. For more information, see [How to connect to an Azure SQL database with SSMS](https://azure.microsoft.com/documentation/articles/sql-database-connect-to-database/).  
  
### Simple Example  
 A simple example for creating a database.  
  
```tsql  
CREATE DATABASE TestDB1;  
```  
  
### Simple Example with Edition  
 A simple example for creating a standard database.  
  
```tsql  
CREATE DATABASE TestDB2  
( EDITION = 'standard' );  
```  
  
### Example with Additional Options  
 An example using multiple options.  
  
```tsql  
CREATE DATABASE hito   
COLLATE Japanese_Bushu_Kakusu_100_CS_AS_KS_WS   
( MAXSIZE = 500 MB, EDITION = 'standard', SERVICE_OBJECTIVE = 'S1' ) ;  
```  
  
### Creating a Copy  
 An example creating a copy of a database.  
  
```tsql  
CREATE DATABASE escuela   
AS COPY OF school;  
```  
  
### Creating a Database in an Elastic Pool  
 Creates new database in pool named S3M100:  
  
```tsql  
CREATE DATABASE db1 ( SERVICE_OBJECTIVE = ELASTIC_POOL ( name = S3M100 ) ) ;  
```  
  
### Creating a Copy of a Database on Another Server  
 The following example creates a copy of the db_original database, named db_copy in the P2 performance level for a single database.  This is true regardless of whether db_original is in an elastic pool or a performance level for a single database.  
  
```tsql  
CREATE DATABASE db_copy   
    AS COPY OF ozabzw7545.db_original ( SERVICE_OBJECTIVE = 'P2' )  ;  
```  
  
 The following example creates a copy of the db_original database, named db_copy in an elastic pool named ep1.  This is true regardless of whether db_original is in an elastic pool or a performance level for a single database.  If db_original is in an elastic pool with a different name, then db_copy is still created in ep1.  
  
```tsql  
CREATE DATABASE db_copy   
    AS COPY OF ozabzw7545.db_original   
    (SERVICE_OBJECTIVE = ELASTIC_POOL( name = ep1 ) ) ;  
```  

### Create database with specified catalog collation value

The following example sets the catalog collation to DATABASE_DEFAULT during database creation, which sets the catalog collation to be the same as the database collation.

```tsql
CREATE DATABASE TestDB3 COLLATE Japanese_XJIS_140  (MAXSIZE = 100 MB, EDITION = ‘basic’)  
      WITH CATALOG_COLLATION = DATABASE_DEFAULT 
```
  
## See Also  

-  [sys.dm_database_copies &#40;Azure SQL Database&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-database-copies-azure-sql-database.md)

-   [ALTER DATABASE &#40;Azure SQL Database&#41;](https://msdn.microsoft.com/library/mt574871.aspx)   
    
  
