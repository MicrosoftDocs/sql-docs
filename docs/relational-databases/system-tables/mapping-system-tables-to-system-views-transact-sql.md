---
title: "Mapping System Tables to System Views (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "catalog views [SQL Server], mapping system tables to"
  - "mapping system tables to system views [SQL Server]"
  - "system tables [SQL Server], mapping to catalog views"
ms.assetid: a616fce9-b4c1-49da-87a7-9d6f74911d8f
author: stevestein
ms.author: sstein
manager: craigg
---
# Mapping System Tables to System Views (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This topic shows the mapping between the system tables and functions and system views and functions.  
  
 The following table maps the system tables to their corresponding system views or functions in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
|System table|System views or functions|Type of view or function|  
|------------------|-------------------------------|------------------------------|  
|sysaltfiles|[sys.master_files](../../relational-databases/system-catalog-views/sys-master-files-transact-sql.md)|Catalog view|  
|syscacheobjects|[sys.dm_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)<br /><br /> [sys.dm_exec_plan_attributes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-plan-attributes-transact-sql.md)<br /><br /> [sys.dm_exec_sql_text](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sql-text-transact-sql.md)<br /><br /> [sys.dm_exec_cached_plan_dependent_objects](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plan-dependent-objects-transact-sql.md)|Dynamic management view<br /><br /> Dynamic management view<br /><br /> Dynamic management view<br /><br /> Dynamic management view|  
|syscharsets|[sys.syscharsets](../../relational-databases/system-compatibility-views/sys-syscharsets-transact-sql.md)|Compatibility view|  
|sysconfigures|[sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)|Catalog view|  
|syscurconfigs|[sys.configurations](../../relational-databases/system-catalog-views/sys-configurations-transact-sql.md)|Catalog view|  
|sysdatabases|[sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)|Catalog view|  
|sysdevices|[sys.backup_devices](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md)|Catalog view|  
|syslanguages|[sys.syslanguages](../../relational-databases/system-compatibility-views/sys-syslanguages-transact-sql.md)|Compatibility view|  
|syslockinfo|[sys.dm_tran_locks](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)|Dynamic management view|  
|syslocks|[sys.dm_tran_locks](../../relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql.md)|Dynamic management view|  
|syslogins|[sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)<br /><br /> [sys.sql_logins](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md)|Catalog view|  
|sysmessages|[sys.messages](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)|Catalog view|  
|sysoledbusers|[sys.linked_logins](../../relational-databases/system-catalog-views/sys-linked-logins-transact-sql.md)|Catalog view|  
|sysopentapes|[sys.dm_io_backup_tapes](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md)|Dynamic management view|  
|sysperfinfo|[sys.dm_os_performance_counters](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)|Dynamic management view|  
|sysprocesses|[sys.dm_exec_connections](../../relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql.md)<br /><br /> [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)<br /><br /> [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)|Dynamic management view<br /><br /> Dynamic management view<br /><br /> Dynamic management view|  
|sysremotelogins|[sys.remote_logins](../../relational-databases/system-catalog-views/sys-remote-logins-transact-sql.md)|Catalog view|  
|sysservers|[sys.servers](../../relational-databases/system-catalog-views/sys-servers-transact-sql.md)|Catalog view|  
  
 The following table maps the system tables or functions that are in every database in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] to their corresponding system views or functions in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
|System table or function|System view or function|Type of view or function|  
|------------------------------|-----------------------------|------------------------------|  
|fn_virtualfilestats|[sys.dm_io_virtual_file_stats](../../relational-databases/system-dynamic-management-views/sys-dm-io-virtual-file-stats-transact-sql.md)|Dynamic management view|  
|syscolumns|[sys.columns](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)|Catalog view|  
|syscomments|[sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)|Catalog view|  
|sysconstraints|[sys.check_constraints](../../relational-databases/system-catalog-views/sys-check-constraints-transact-sql.md)<br /><br /> [sys.default_constraints](../../relational-databases/system-catalog-views/sys-default-constraints-transact-sql.md)<br /><br /> [sys.key_constraints](../../relational-databases/system-catalog-views/sys-key-constraints-transact-sql.md)<br /><br /> [sys.foreign_keys](../../relational-databases/system-catalog-views/sys-foreign-keys-transact-sql.md)|Catalog view<br /><br /> Catalog view<br /><br /> Catalog view<br /><br /> Catalog view|  
|sysdepends|[sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)|Catalog view|  
|sysfilegroups|[sys.filegroups](../../relational-databases/system-catalog-views/sys-filegroups-transact-sql.md)|Catalog view|  
|sysfiles|[sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)|Catalog view|  
|sysforeignkeys|[sys.foreign_key_columns](../../relational-databases/system-catalog-views/sys-foreign-key-columns-transact-sql.md)|Catalog view|  
|sysindexes|[sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)<br /><br /> [sys.partitions](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)<br /><br /> [sys.allocation_units](../../relational-databases/system-catalog-views/sys-allocation-units-transact-sql.md)<br /><br /> [sys.dm_db_partition_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-partition-stats-transact-sql.md)|Catalog view<br /><br /> Catalog view<br /><br /> Catalog view<br /><br /> Dynamic management view|  
|sysindexkeys|[sys.index_columns](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)|Catalog view|  
|sysmembers|[sys.database_role_members](../../relational-databases/system-catalog-views/sys-database-role-members-transact-sql.md)|Catalog view|  
|sysobjects|[sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)|Catalog view|  
|syspermissions|[sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)<br /><br /> [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)|Catalog view<br /><br /> Catalog view|  
|sysprotects|[sys.database_permissions](../../relational-databases/system-catalog-views/sys-database-permissions-transact-sql.md)<br /><br /> [sys.server_permissions](../../relational-databases/system-catalog-views/sys-server-permissions-transact-sql.md)|Catalog view<br /><br /> Catalog view|  
|sysreferences|[sys.foreign_keys](../../relational-databases/system-catalog-views/sys-foreign-keys-transact-sql.md)|Catalog view|  
|systypes|[sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)|Catalog view|  
|sysusers|[sys.database_principals](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)|Catalog view|  
|sysfulltextcatalogs|[sys.fulltext_catalogs](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)|Catalog view|  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
