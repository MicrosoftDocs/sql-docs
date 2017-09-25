---
title: "ALTER DATABASE (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "09/25/2017"
ms.prod: 
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6fc5fd95-2045-4f20-a914-3598091bc7cc
caps.latest.revision: 37
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# ALTER DATABASE (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Modifies a  [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Changes the name of a database, the edition and service objective of a database, join an elastic pool, and sets database options.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Azure SQL Database Syntax  
ALTER DATABASE { database_name }  
{  
    MODIFY NAME = new_database_name  
  | MODIFY ( <edition_options> [, ... n] )   
  | SET { <option_spec> [ ,... n ] }   
  | ADD SECONDARY ON SERVER <partner_server_name>  
      [WITH ( <add-secondary-option>::= [, ... n] ) ]  
  | REMOVE SECONDARY ON SERVER <partner_server_name>  
  | FAILOVER  
  | FORCE_FAILOVER_ALLOW_DATA_LOSS  
}  
[;] 

<edition_options> ::=   
{  

      MAXSIZE = { 100 MB | 250 MB | 500 MB | 1 … 1024 … 4096 GB }    
    | EDITION = { 'basic' | 'standard' | 'premium' | 'premiumrs' }   
    | SERVICE_OBJECTIVE = 
                 {  <service-objective>
                 | { ELASTIC_POOL (name = <elastic_pool_name>) }   
                 }   
}  

<add-secondary-option> ::=  
   {  
      ALLOW_CONNECTIONS = { ALL | NO }  
     | SERVICE_OBJECTIVE =   
                 {  <service-objective> 
                 | { ELASTIC_POOL ( name = <elastic_pool_name>) }   
                 }   
   }  

<service-objective> ::=  'S0' | 'S1' | 'S2' | 'S3'| 'S4'| 'S6'| 'S7'| 'S9'| 'S12' |
                 | 'P1' | 'P2' | 'P4'| 'P6' | 'P11'  | 'P15' | 
                 | 'PRS1' | 'PRS2' | 'PRS4' | 'PRS6' |

```  
  
```
-- SET OPTIONS AVAILABLE FOR SQL Database  
-- Full descriptions of the set options are available in the topic   
-- ALTER DATABASE SET Options. The supported syntax is listed here.  

<optionspec> ::=   
{  
    <auto_option>   
  | <compatibility_level_option>  
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
}  
  
<auto_option> ::=   
{  
    AUTO_CREATE_STATISTICS { OFF | ON [ ( INCREMENTAL = { ON | OFF } ) ] }   
  | AUTO_SHRINK { ON | OFF }   
  | AUTO_UPDATE_STATISTICS { ON | OFF }   
  | AUTO_UPDATE_STATISTICS_ASYNC { ON | OFF }  
}  
  
<compatibility_level_option>::=  
COMPATIBILITY_LEVEL = { 130 | 120 | 110 | 100 }  
  
<cursor_option> ::=   
{  
    CURSOR_CLOSE_ON_COMMIT { ON | OFF }   
}  
  
<db_encryption_option> ::=  
    ENCRYPTION { ON | OFF }  
  
<db_update_option> ::=  
    { READ_ONLY | READ_WRITE }  
  
<db_user_access_option> ::=  
    { RESTRICTED_USER | MULTI_USER }  
  
<delayed_durability_option> ::=    DELAYED_DURABILITY = { DISABLED | ALLOWED | FORCED }  
  
<parameterization_option> ::=  
    PARAMETERIZATION { SIMPLE | FORCED }  
  
<query_store_options> ::=  
{  
    QUERY_STORE   
    {  
          = OFF   
        | = ON [ ( <query_store_option_list> [,... n] ) ]  
        | ( < query_store_option_list> [,... n] )  
        | CLEAR [ ALL ]  
    }  
}   
  
<query_store_option_list> ::=  
{  
      OPERATION_MODE = { READ_WRITE | READ_ONLY }   
    | CLEANUP_POLICY = ( STALE_QUERY_THRESHOLD_DAYS = number )  
    | DATA_FLUSH_INTERVAL_SECONDS = number   
    | MAX_STORAGE_SIZE_MB = number   
    | INTERVAL_LENGTH_MINUTES = number   
    | SIZE_BASED_CLEANUP_MODE = [ AUTO | OFF ]  
    | QUERY_CAPTURE_MODE = [ ALL | AUTO | NONE ]  
    | MAX_PLANS_PER_QUERY = number  
}  
  
<snapshot_option> ::=  
{  
    ALLOW_SNAPSHOT_ISOLATION { ON | OFF }  
  | READ_COMMITTED_SNAPSHOT {ON | OFF }  
  | MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT {ON | OFF }  
}  
<sql_option> ::=   
{  
    ANSI_NULL_DEFAULT { ON | OFF }   
  | ANSI_NULLS { ON | OFF }   
  | ANSI_PADDING { ON | OFF }   
  | ANSI_WARNINGS { ON | OFF }   
  | ARITHABORT { ON | OFF }   
  | COMPATIBILITY_LEVEL = { 90 | 100 | 110 | 120}  
  | CONCAT_NULL_YIELDS_NULL { ON | OFF }   
  | NUMERIC_ROUNDABORT { ON | OFF }   
  | QUOTED_IDENTIFIER { ON | OFF }   
  | RECURSIVE_TRIGGERS { ON | OFF }   
}  
  
<termination>  ::=   
{  
    ROLLBACK AFTER integer [ SECONDS ]   
  | ROLLBACK IMMEDIATE   
  | NO_WAIT  
}  

<temporal_history_retention>  ::=  TEMPORAL_HISTORY_RETENTION { ON | OFF }
```  
  
 For full descriptions of the set options, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md) and [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
## Arguments  
 *database_name*  
 Is the name of the database to be modified.  
  
 CURRENT  
 Designates that the current database in use should be altered.  
  
 MODIFY NAME **=***new_database_name*  
 Renames the database with the name specified as *new_database_name*. The following example changes the name of a database `db1` to `db2`:   

```  
ALTER DATABASE db1  
    MODIFY Name = db2 ;  
```    

 MODIFY (EDITION **=** ['basic' | 'standard' | 'premium' | 'premiumrs'] )    
 Changes the service tier of the database. The following example changes edition to `premium`:
  
```  
ALTER DATABASE current 
    MODIFY (EDITION = 'premium');
``` 

EDITION change fails if the MAXSIZE property for the database is set to a value outside the valid range supported by that edition.  

 MODIFY (MAXSIZE **=** [100 MB | 500 MB | 1 | 1024…4096] GB)  
 Specifies the maximum size of the database. The maximum size must comply with the valid set of values for the EDITION property of the database. Changing the maximum size of the database may cause the database EDITION to be changed. Following table lists the supported MAXSIZE values and the defaults (D) for the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] service tiers.  
  
|**MAXSIZE**|**Basic**|**S0-S2**|**S3-S12**|**P1-P6 and PRS1-PRS6**|**P11-P15**|  
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
|From 1024 GB up to 4096 GB in increments of 256 GB*|N/A|N/A|N/A|N/A|√|√|  
  
 \* P11 and P15 allow MAXSIZE up to 4 TB with 1024 GB being the default size.  P11 and P15 can use up to 4 TB of included storage at no additional charge. In the Premium tier, MAXSIZE greater than 1 TB is currently available in the following regions: US East2, West US, US Gov Virginia, West Europe, Germany Central, South East Asia, Japan East, Australia East, Canada Central, and Canada East. For current limitations, see [Single databases](https://docs.microsoft.com/azure/sql-database-single-database-resources).  

  
 The following rules apply to MAXSIZE and EDITION arguments:  
  
-   The MAXSIZE value, if specified, has to be a valid value shown in the previous table.  
  
-   If EDITION is specified but MAXSIZE is not specified, the default value for the edition is used. For example, is the EDITION is set to Standard, and the MAXSIZE is not specified, then the MAXSIZE is automatically set to 500 MB.  
  
-   If neither MAXSIZE nor EDITION is specified, the EDITION is set to Standard (S0), and MAXSIZE is set to 250 GB.  

 MODIFY (SERVICE_OBJECTIVE = \<service-objective>)  
 Specifies the performance level. The following example changes service objectove of a premium database to `P6`:
 
```  
ALTER DATABASE current 
    MODIFY (SERVICE_OBJECTIVE = 'P6');
```  
 Available values for service objective are:  `S0`, `S1`, `S2`, `S3`, `S4`, `S6`, `S7`, `S9`, `S12`, `P1`, `P2`, `P4`, `P6`, `P11`, `P15`, `PRS1`, `PRS2`, `PRS4`, and `PRS6`. For service objective descriptions and more information about the size, editions, and the service objectives combinations, see [Azure SQL Database Service Tiers and Performance Levels](http://msdn.microsoft.com/library/azure/dn741336.aspx). If the specified SERVICE_OBJECTIVE is not supported by the EDITION, you receive an error. To change the SERVICE_OBJECTIVE value from one tier to another (for example from S1 to P1), you must also change the EDITION value.  
  
 MODIFY (SERVICE_OBJECTIVE = ELASTIC\_POOL (name = \<elastic_pool_name>)  
 To add an existing database to an elastic pool, set the SERVICE_OBJECTIVE of the database to ELASTIC_POOL and provide the name of the elastic pool. You can also use this option to change the database to a different elastic pool within the same server. For more information, see [Create and manage a SQL Database elastic pool](https://azure.microsoft.com/documentation/articles/sql-database-elastic-pool-portal/). To remove a database from an elastic pool, use ALTER DATABASE to set the SERVICE_OBJECTIVE to a single database performance level.  

 ADD SECONDARY ON SERVER \<partner_server_name>  
 Creates a geo-replication secondary database with the same name  on a partner server, making the local database into a geo-replication primary, and begins asynchronously replicating data from the primary to the new secondary. If a database with the same name already exists on the secondary, the command fails. The command is executed on the master database on the server hosting the local database that becomes the primary.  
  
 WITH ALLOW_CONNECTIONS { ALL | **NO** }  
 When ALLOW_CONNECTIONS is not specified, it is set to NO by default. If it is set ALL, it is a read-only database that allows all logins with the appropriate permissions to connect.  
  
 WITH SERVICE_OBJECTIVE {  'S0' | 'S1' | 'S2' | 'S3" | 'S4'| 'S6'| 'S7'| 'S9'| 'S12' | 'P1' | 'P2' | 'P4'| 'P6' | 'P11' | 'P15' | 'PRS1' | 'PRS2' | 'PRS4' | 'PRS6' }  
 When SERVICE_OBJECTIVE is not specified, the secondary database is created at the same service level as the primary database. When SERVICE_OBJECTIVE is  specified, the secondary database is created at the specified level. This option supports creating geo-replicated secondaries with less expensive service levels. The SERVICE_OBJECTIVE specified must be within the same edition as the source. For example, you cannot specify S0 if the edition is premium.  
  
 ELASTIC_POOL (name = \<elastic_pool_name)  
 When ELASTIC_POOL is not specified, the secondary database is not created in an elastic pool. When ELASTIC_POOL is specified, the secondary database is created in the specified pool.  
  
> [!IMPORTANT]  
>  The user executing the ADD SECONDARY command must be DBManager on primary server, have db_owner membership in local database, and DBManager on secondary server.  
  
 REMOVE SECONDARY ON SERVER  \<partner_server_name>  
 Removes the specified geo-replicated secondary database on the specified server. The command is executed on the master database on the server hosting the primary database.  
  
> [!IMPORTANT]  
>  The user executing the REMOVE SECONDARY command must be DBManager on the primary server.  
  
 FAILOVER  
 Promotes the secondary database in geo-replication partnership on which the command is executed to become the primary and demotes the current primary to become the new secondary. As part of this process, the geo-replication mode is temporarily switched from asynchronous mode to synchronous mode. During the failover process:  
  
1.  The primary stops taking new transactions.  
  
2.  All outstanding transactions are flushed to the secondary.  
  
3.  The secondary becomes the primary and begins asynchronous geo-replication with the old primary / the new secondary.  
  
 This sequence ensures that no data loss occurs. The period during which both databases are unavailable is on the order of 0-25 seconds while the roles are switched. The total operation should take no longer than about one minute. If the primary database is unavailable when this command is issued, the command fails with an error message indicating that the primary database is not available. If the failover process does not complete and appears stuck, you can use the force failover command and accept data loss - and then, if you need to recover the lost data, call devops (CSS) to recover the lost data.  
  
> [!IMPORTANT]  
>  The user executing the FAILOVER command must be DBManager on both the primary server and the secondary server.  
  
 FORCE_FAILOVER_ALLOW_DATA_LOSS  
 Promotes the secondary database in geo-replication partnership on which the command is executed to become the primary and demotes the current primary to become the new secondary. Use this command only when the current primary is no longer available. It is designed for disaster recovery only, when restoring availability is critical, and some data loss is acceptable.  
  
 During a forced failover:  
  
1.  The specified secondary database immediately becomes the primary database and begins accepting new transactions.  
  
2.  When the original primary can reconnect with the new primary, an incremental backup is taken on the original primary, and the original primary becomes a new secondary.  
  
3.  To recover data from this incremental backup on the old primary, the user engages devops/CSS.  
  
4.  If there are additional secondaries, they are automatically reconfigured to become secondaries of the new primary. This process is asynchronous and there may be a delay until this process completes. Until the reconfiguration has completed, the secondaries continue to be secondaries of the old primary.  
  
> [!IMPORTANT]  
>  The user executing the FORCE_FAILOVER_ALLOW_DATA_LOSS command must be DBManager on both the primary server and the secondary server.  
  
## Remarks  
 To remove a database, use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md).  
  
 To decrease the size of a database, use [DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md).  
  
 The ALTER DATABASE statement must run in autocommit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.  
  
 Clearing the plan cache causes a recompilation of all subsequent execution plans and can cause a sudden, temporary decrease in query performance. For each cleared cachestore in the plan cache, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains the following informational message: "[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations". This message is logged every five minutes as long as the cache is flushed within that time interval.  
  
 The procedure cache is also flushed in the following scenarios:  
  
-   A database has the AUTO_CLOSE database option set to ON. When no user connection references or uses the database, the background task tries to close and shut down the database automatically.  
  
-   You run several queries against a database that has default options. Then, the database is dropped.  
  
-   You successfully rebuild the transaction log for a database.  
  
-   You restore a database backup.  
  
-   You detach a database.  
  
## Viewing Database Information  
 You can use catalog views, system functions, and system stored procedures to return information about databases, files, and filegroups.  
  
## Permissions  
 Only the server-level principal login (created by the provisioning process) or members of the `dbmanager` database role can alter a database.  
  
> [!IMPORTANT]  
>  The owner of the database cannot alter the database unless they are a member of the `dbmanager` role.  
  
## Examples  
  
### A. Check the edition options and change them:

```
SELECT Edition = DATABASEPROPERTYEX('db1', 'EDITION'),
        ServiceObjective = DATABASEPROPERTYEX('db1', 'ServiceObjective'),
        MaxSizeInBytes =  DATABASEPROPERTYEX('db1', 'MaxSizeInBytes');

ALTER DATABASE [db1] MODIFY (EDITION = 'Premium', MAXSIZE = 1024 GB, SERVICE_OBJECTIVE = 'P15');
```

### B. Moving a database to a different elastic pool  
 Moves an existing database into a pool named pool1:  
  
```  
ALTER DATABASE db1   
MODIFY ( SERVICE_OBJECTIVE = ELASTIC_POOL ( name = pool1 ) ) ;  
```  
  
### B. Add a Geo-Replication Secondary  
 Creates a non-readable secondary database db1 on server secondaryserver of the db1 on the local server.  
  
```  
ALTER DATABASE db1   
ADD SECONDARY ON SERVER secondaryserver   
WITH ( ALLOW_CONNECTIONS = NO )  
```  
  
### D. Remove a Geo-Replication Secondary  
 Removes the secondary database db1 on server secondaryserver.  
  
```  
ALTER DATABASE db1   
REMOVE SECONDARY ON SERVER testsecondaryserver   
```  
  
### E. Failover to a Geo-Replication Secondary  
 Promotes a secondary database db1 on server secondaryserver to become the new primary database when executed on server secondaryserver.  
  
```  
ALTER DATABASE db1 FAILOVER  
```  
  
## See also  
 [CREATE DATABASE &#40;Azure SQL Database&#41;](../../t-sql/statements/create-database-azure-sql-database.md)   
 [DATABASEPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/databasepropertyex-transact-sql.md)   
 [DROP DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-transact-sql.md)   
 [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)   
 [sp_spaceused &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.database_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)   
 [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)   
 [sys.data_spaces &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-data-spaces-transact-sql.md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)   
 [sys.master_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)   
 [System Databases](../../relational-databases/databases/system-databases.md)  
  
  
