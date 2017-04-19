---
title: "ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "ALTER_DATABASE_SCOPED_CONFIGURATION"
  - "ALTER_DATABASE_SCOPED_CONFIGURATION_TSQL"
  - "DATABASE_SCOPED_CONFIGURATION_TSQL"
  - "SCOPED_CONFIGURATION_TSQL"
  - "SCOPED_TSQL"
  - "ALTER_DATABASE_SCOPED_TSQL"
  - "DATABASE_SCOPED_TSQL"
helpviewer_keywords: 
  - "ALTER DATABASE SCOPED CONFIGURATION statement"
  - "configuration [SQL Server], ALTER DATABASE SCOPED CONFIGURATION statement"
ms.assetid: 63373c2f-9a0b-431b-b9d2-6fa35641571a
caps.latest.revision: 32
author: "CarlRabeler"
ms.author: "carlrab"
manager: "jhubbard"
---
# ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  This statement enables several database configuration settings at the **individual database** level. This statement is available in both [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] and in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. Those settings are:  
  
- Clear procedure cache.  
  
- Set the MAXDOP parameter to an arbitrary value (1,2, ...) for the primary database based on what works best for that particular database and set a different value (e.g. 0) for all secondary database used (e.g. for reporting queries).  
  
- Set the query optimizer cardinality estimation model independent of the database to compatibility level.  
  
- Enable or disable parameter sniffing at the database level.  
  
- Enable or disable query optimization hotfixes at the database level.  

- Enable or disable the identity cache at the database level. **Applies to**: SQL Server 2017 (feature is in public preview) 
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER DATABASE SCOPED CONFIGURATION  
{        
     {  [ FOR SECONDARY] SET <set_options>  }    
}  
| CLEAR PROCEDURE_CACHE  
| IDENTITY_CACHE = { ON | OFF }
[;]    
  
< set_options > ::=    
{  
    MAXDOP = { <value> | PRIMARY}    
    | LEGACY_CARDINALITY_ESTIMATION = { ON | OFF | PRIMARY}    
    | PARAMETER_SNIFFING = { ON | OFF | PRIMARY}    
    | QUERY_OPTIMIZER_HOTFIXES = { ON | OFF | PRIMARY}    
}  
  
```  
  
## Arguments  
 
FOR SECONDARY  
 
Specifies the settings for secondary databases (all secondary databases must have the identical values).  
  
MAXDOP **=** {\<value> | PRIMARY }  
**\<value>**  
  
Specifies the default MAXDOP setting that should be used for statements. 0 is the default value and indicates that the server configuration will be used instead. The MAXDOP at the database scope overrides (unless it is set to 0) the **max degree of parallelism** set at the server level by sp_configure. Query hints can still override the DB scoped MAXDOP in order to tune specific queries that need different setting. All these settings are limited by the MAXDOP set for the Workload Group.   

You can use the max degree of parallelism option to limit the number of processors to use in parallel plan execution. SQL Server considers parallel execution plans for queries, index data definition language (DDL) operations, parallel insert, online alter column, parallel stats collectiion, and static and keyset-driven cursor population.
 
To set this option at the instance level, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). To accomplish this at the query level, add the **QUERYTRACEON** [query hint](https://msdn.microsoft.com/library/ms181714.aspx)  
  
PRIMARY  
  
Can only be set for the secondaries and indicates that the configuration will be the one set for the primary. If the configuration for the primary changes, secondary will also adjust to the same value. **PRIMARY** is the default setting for the secondaries  
  
LEGACY_CARDINALITY_ESTIMATION **=** { ON | **OFF** | PRIMARY }  

Enables you to set the query optimizer cardinality estimation model to the SQL Server 2012 and earlier version independent of the compatibility level of the database. The default is **OFF**, which sets the query optimizer cardinality estimation model based on the compatibility level of the database. Setting this to **ON** is equivalent to [Trace Flag 9481](https://support.microsoft.com/en-us/kb/2801413). To set this at the instance level, see [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md). To accomplish this at the query level, add the **QUERYTRACEON** [query hint](https://msdn.microsoft.com/library/ms181714.aspx).  
  
PRIMARY  
  
This value is only valid on secondaries and specifies that the query optimizer cardinality estimation model setting on all secondaries will be the value set for the primary. If the configuration on the primary for  the query optimizer cardinality estimation model changes, the value on the secondaries will change accordingly. **PRIMARY** is the default setting for the secondaries.  
  
PARAMETER_SNIFFING **=** { **ON** | OFF | PRIMARY}  

Enables or disables parameter sniffing. The default is ON. This is equivalent to [Trace Flag 4136](https://support.microsoft.com/en-us/kb/980653). To set  this at the instance level, see [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md). To set this at the query level, see the **OPTIMIZE FOR UNKNOWN** [query hint](https://msdn.microsoft.com/library/ms181714.aspx).  
  
PRIMARY  
  
This value is only valid on secondaries and specifies that the value for this setting on all secondaries will be the vale set for the primary. If the configuration for the primary changes, the value on the secondaries will change accordingly. This is the default setting for the secondaries.  
  
QUERY_OPTIMIZER_HOTFIXES **=** { ON | **OFF** | PRIMARY }  

Enables or disables query optimization hotfixes regardless of the compatibility level of the database. The default is **OFF**. This is equivalent to [Trace Flag 4199](https://support.microsoft.com/en-us/kb/974006).   

To set this at the instance level, see [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md). To accomplish this at the query level, add the **QUERYTRACEON** [query hint](https://msdn.microsoft.com/library/ms181714.aspx).  
  
PRIMARY  
  
This value is only valid on secondaries and specifies that the value for this setting on all secondaries will be the vale set for the primary. If the configuration for the primary changes, the value on the secondaries will change accordingly. This is the default setting for the secondaries.  
  
CLEAR PROCEDURE_CACHE  

Clears the procedure cache for the database. This can be executed both on the primary and the secondaries.  

IDENTITY_CACHE = { **ON** | OFF }  

**Applies to**: SQL Server 2017 (feature is in public preview) 

Enables or disables identity cache at the database level. The default is **ON**. Identity caching is used to improve INSERT performance on tables with Identity columns. To avoid gaps in the values of the Identity column in cases where the server restarts unexpectedly or fails over to a secondary server, disable the IDENTITY_CACHE option. This option is similar to the existing SQL Server Trace Flag 272, except that it can be set at the database level rather than only at the server level.   

> [!NOTE] 
> This option can only be set for the PRIMARY. For more information, see [Identity columns](create-table-transact-sql-identity-property.md).  
>

##  <a name="Permissions"></a> Permissions  
 Requires ALTER ANY DATABASE SCOPE CONFIGURATION   
on the database. This permission can be granted by a user with CONTROL permission on a database  
  
## General Remarks  
 While you can configure secondary databases to have different scoped configuration settings from their primary,  all secondary databases will use the same configuration. Different settings cannot be configured for individual secondaries.  
  
 Executing this statement will clear the procedure cache in the current database, which means that all queries will have to recompile.  
  
 For 3-part name queries, the settings for the current database connection for the query will be honored, other than for SQL modules (e.g. procedures, functions, and triggers) that are compiled in the current database context and therefore will use the options of the database in which they reside.  
  
 The ALTER_DATABASE_SCOPED_CONFIGURATION event is added as a DDL event that can be used to fire a DDL trigger. This is a child of the ALTER_DATABASE_EVENTS trigger group.  
  
## Limitations and Restrictions  
 **MAXDOP**  
  
 The granular settings can override the global ones and that resource governor can cap all other MAXDOP settings.  The logic for MAXDOP setting is the following:  
  
-   Query hint overrides both the sp_configure and the database scoped setting. If the resource group MAXDOP is set for the workload group:  
  
    -   If the query hint is set to 0 it is overridden by the resource governor setting.  
  
    -   If the query hint is not 0, it is capped by the resource governor setting.  
  
-   The DB scoped setting (unless it’s 0) overrides the sp_configure setting unless there is a query hint and is capped by the resource governor setting.  
  
-   The sp_configure setting is overridden by the resource governor setting.  
  
 **QUERY_OPTIMIZER_HOTFIXES**  
  
 When QUERYTRACEON hint is used to enable the legacy query optimizer or query optimizer hotfixes, it would be an OR condition between the query hint and the database scoped configuration setting, meaning if either is enabled, the options will apply.  
  
 **GeoDR**  
  
 Readable secondary databases,  e.g. Always On Availability Groups and GeoReplication, use the secondary value by checking the state of the database. Even though we don’t recompile on failover and technically the new primary has queries that are using the secondary settings, the idea is that the setting between primary and secondary will only vary when the workload is different and therefore the cached queries are using the optimal settings, whereas new queries will pick the new settings which are appropriate for them.  
  
 **DacFx**  
  
 Since ALTER DATABASE SCOPED CONFIGURATION is a new feature in Azure SQL Database and SQL Server 2016 that affects the database schema, exports of the schema (with or without data)  will not be able to be imported into an older version of SQL Server e.g. SQL Server 2012 or SQL Server 2014.   For example, an export to a [DACPAC](https://msdn.microsoft.com/library/ee210546.aspx#Anchor_3) or a [BACPAC](https://msdn.microsoft.com/library/ee210546.aspx#Anchor_4) from an Azure SQL Database or SQL Server 2016 database that used this new feature would not be able to be imported into a down-level server.  
  
## Metadata  

The [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md) system view provides information about scoped configurations within a database. Database-scoped configuration options only show up in sys.database_scoped_configurations as they are overrides to server-wide default settings. The [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md) system view only shows server-wide settings.  
  
## Examples  
These examples demonstrate the use of ALTER DATABASE SCOPED CONFIGURATION  
  
### A. Grant Permission  

This example grant permission required to execute ALTER DATABASE SCOPED CONFIGURATION     
to user [Joe].  
  
```tsql  
GRANT ALTER ANY DATABASE SCOPED CONFIGURATION to [Joe] ;  
```  
  
### B. Set MAXDOP  

This example sets MAXDOP = 1 for a primary database and MAXDOP = 4 for a secondary database in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 1 ;  
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP=4 ;  
```  
  
This example sets MAXDOP for a secondary database as it is for its primary database in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP=PRIMARY  
```  
  
### C. Set LEGACY_CARDINALITY_ESTIMATION  

This example sets LEGACY_CARDINALITY_ESTIMATION to ON for a secondary database in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY  
SET LEGACY_CARDINALITY_ESTIMATION=ON ;  
```  
  
This example sets LEGACY_CARDINALITY_ESTIMATION for a secondary database as it is for its primary database in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY      
SET LEGACY_CARDINALITY_ESTIMATION=PRIMARY ;  
```  
  
### D. Set PARAMETER_SNIFFING  

This example sets PARAMETER_SNIFFING to OFF for a primary database in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING =OFF ;  
```  
  
This example sets PARAMETER_SNIFFING to OFF for a primary database in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY      
SET PARAMETER_SNIFFING=OFF  ;  
```  
  
This example sets PARAMETER_SNIFFING for secondary database as it is on primary database   
in a geo-replication scenario.  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY      
SET PARAMETER_SNIFFING =PRIMARY  ;  
```  
  
### E. Set QUERY_OPTIMIZER_HOTFIXES  

Set QUERY_OPTIMIZER_HOTFIXES to ON for a primary database   
in a geo-replication scenario.  

```tsql  
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES=ON ;  
```  
  
### F. Clear Procedure Cache  

This example clears the procedure cache (possible only for a primary database).  
  
```tsql  
ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE ;  
```  

### G. Set IDENTITY_CACHE

**Applies to**: SQL Server 2017 (feature is in public preview) 

This example disables the identity cache.

```tsql 
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE=OFF ; 
```

## Additional Resources

### MAXDOP Resources 
* [Recommendations and guidelines for the "max degree of parallelism" configuration option in SQL Server ](https://support.microsoft.com/en-us/kb/2806535) 

### LEGACY_CARDINALITY_ESTIMATION Resources    
* [Cardinality Estimation (SQL Server)](https://msdn.microsoft.com/library/dn600374.aspx)
* [Optimizing Your Query Plans with the SQL Server 2014 Cardinality Estimator](https://msdn.microsoft.com/library/dn673537.aspx)

### PARAMETER_SNIFFING Resources    
* ["I smell a parameter!"](https://blogs.msdn.microsoft.com/queryoptteam/2006/03/31/i-smell-a-parameter/)

### QUERY_OPTIMIZER_HOTFIXES Resources    
* [SQL Server query optimizer hotfix trace flag 4199 servicing model](https://support.microsoft.com/en-us/kb/974006)

## More information  
 [sys.database_scoped_configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-scoped-configurations-transact-sql.md)   
 [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)   
 [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)   
 [sys.configurations &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)  
  
  
