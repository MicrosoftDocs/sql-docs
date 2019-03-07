---
title: "Discontinued Database Engine Functionality in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "VIA protocol"
  - "unsupported features [SQL Server]"
  - "SQL Mail"
  - "discontinued functionality [SQL Server]"
  - "RESTORE WITH DBO_ONLY"
  - "BACKUP WITH PASSWORD"
  - "user instances enabled"
  - "BACKUP WITH MEDIAPASSWORD"
  - "AWE"
  - "SQL-DMO"
  - "*= and =*"
  - "80 compatibility levels"
  - "COMPUTE BY"
  - "user instance timeout"
  - "sp_dropalias"
  - "COMPUTE"
  - "WITH APPEND"
  - "sys.database_principal_aliases"
  - "sp_dboption"
  - "DATABASEPROPERTY"
  - "FASTFIRSTROW hint"
  - "SET DISABLE_DEF_CNST_CHK"
ms.assetid: d686cdf0-d11d-4dba-9ec8-de1a5f189f25
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Discontinued Database Engine Functionality in SQL Server 2014
  This topic describes the [!INCLUDE[ssDE](../includes/ssde-md.md)] features that are no longer available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
## Discontinued Features in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 The following table lists features that were removed in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
|Category|Discontinued feature|Replacement|  
|--------------|--------------------------|-----------------|  
|Compatibility level|90 compatibility level|Databases must be set to at least compatibility level 100. When a database with a compatibility level of less than 100 is upgraded to [!INCLUDE[ssSQL14](../includes/sssql14-md.md)], the compatibility level of the database is set to 100 during the upgrade operation.|  
  
## Discontinued Features in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
 The following table lists features that were removed in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
|Category|Discontinued feature|Replacement|  
|--------------|--------------------------|-----------------|  
|Backup and Restore|**BACKUP { DATABASE &#124; LOG } WITH PASSWORD** and **BACKUP { DATABASE &#124; LOG } WITH MEDIAPASSWORD** are discontinued. **RESTORE { DATABASE &#124; LOG } WITH [MEDIA]PASSWORD**continues to be deprecated.|None|  
|Backup and Restore|**RESTORE { DATABASE &#124; LOG } ... WITH DBO_ONLY**|**RESTORE { DATABASE &#124; LOG } ... ... WITH RESTRICTED_USER**|  
|Compatibility level|80 compatibility level|Databases must be set to at least compatibility level 90.|  
|Configuration Options|`sp_configure 'user instance timeout'` and `'user instances enabled'`|Use the Local Database feature. For more information, see [SqlLocalDB Utility](../tools/sqllocaldb-utility.md)|  
|Connection protocols|Support for the VIA protocol is discontinued.|Use TCP instead.|  
|Database objects|`WITH APPEND` clause on triggers|Re-create the whole trigger.|  
|Database options|`sp_dboption`|`ALTER DATABASE`|  
|Mail|SQL Mail|Use Database Mail. For more information, see [Database Mail](../relational-databases/database-mail/database-mail.md) and  [Use Database Mail Instead of SQL Mail](../relational-databases/policy-based-management/use-database-mail-instead-of-sql-mail.md).|  
|Memory Management|32-bit Address Windowing Extensions (AWE) and 32-bit Hot Add memory support.|Use a 64-bit operating system.|  
|Metadata|`DATABASEPROPERTY`|`DATABASEPROPERTYEX`|  
|Programmability|SQL Server Distributed Management Objects (SQL-DMO)|SQL Server Management Objects (SMO)|  
|Query hints|`FASTFIRSTROW` hint|`OPTION (FAST` *n* `)`.|  
|Remote servers|The ability for users to create new remote servers by using `sp_addserver` is discontinued. `sp_addserver` with the 'local' option remains available. Remote servers preserved during upgrade or created by replication can be used.|Replace remote servers by using linked servers.|  
|Security|`sp_dropalias`|Replace aliases with a combination of user accounts and database roles. Use `sp_dropalias` to remove aliases in upgraded databases.|  
|Security|The version parameter of **PWDCOMPARE** representing a value from a login earlier than [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2000 is discontinued.|None|  
|Service Broker programmability in SMO|The **Microsoft.SqlServer.Management.Smo.Broker.BrokerPriority** class no longer implements the **Microsoft.SqlServer.Management.Smo.IObjectPermission** interface.||  
|SET options|`SET DISABLE_DEF_CNST_CHK`|None.|  
|System tables|sys.database_principal_aliases|Use roles instead of aliases.|  
|Transact-SQL|`RAISERROR` in the format `RAISERROR integer 'string'` is discontinued.|Rewrite the statement using the current **RAISERROR(...)** syntax.|  
|Transact-SQL syntax|`COMPUTE / COMPUTE BY`|Use `ROLLUP`|  
|Transact-SQL syntax|Use of **\*=** and **=&#42;**|Use ANSI join syntax. For more information, see [FROM (Transact-SQL).](https://msdn.microsoft.com/library/ms177634\(SQL.105\).aspx)|  
|XEvents|databases_data_file_size_changed, databases_log_file_size_changed<br /><br /> eventdatabases_log_file_used_size_changed<br /><br /> locks_lock_timeouts_greater_than_0<br /><br /> locks_lock_timeouts|Replaced by database_file_size_change event, database_file_size_change<br /><br /> database_file_size_change event<br /><br /> lock_timeout_greater_than_0<br /><br /> lock_timeout|  
  
 **Additional XEvent changes**  
  
 **resource_monitor_ring_buffer_record**:  
  
-   Fields removed: single_pages_kb, multiple_pages_kb  
  
-   Fields added: target_kb, pages_kb  
  
 **memory_node_oom_ring_buffer_recorded**:  
  
-   Fields removed: single_pages_kb, multiple_pages_kb  
  
-   Fields added: target_kb, pages_kb  
  
## See Also  
 [Deprecated Database Engine Features in SQL Server 2014](deprecated-database-engine-features-in-sql-server-2016.md)  
  
  
