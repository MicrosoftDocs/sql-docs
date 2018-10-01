---
title: "sys.database_mirroring (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.database_mirroring"
  - "database_mirroring"
  - "sys.database_mirroring_TSQL"
  - "database_mirroring_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.database_mirroring catalog view"
ms.assetid: 480de2b0-2c16-497d-a6a3-bf7f52a7c9a0
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sys.database_mirroring (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each database in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the database is not ONLINE or database mirroring is not enabled, the values of all columns except database_id will be NULL.  
  
 To see the row for a database other than master or tempdb, you must either be the database owner or have at least ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission or CREATE DATABASE permission in the master database. To see non-NULL values on a mirror database, you must be a member of the **sysadmin** fixed server role.  
  
> [!NOTE]  
>  If a database does not participate in mirroring, all columns prefixed with "mirroring_" are NULL.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|ID of the database. Is unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**mirroring_guid**|**uniqueidentifier**|ID of the mirroring partnership.<br /><br /> NULL= Database is inaccessible or is not mirrored.<br /><br /> Note: If the database does not participate in mirroring, all columns prefixed with "mirroring_" are NULL.|  
|**mirroring_state**|**tinyint**|State of the mirror database and of the database mirroring session.<br /><br /> 0 = Suspended<br /><br /> 1 = Disconnected from the other partner<br /><br /> 2 = Synchronizing<br /><br /> 3 = Pending Failover<br /><br /> 4 = Synchronized<br /><br /> 5 = The partners are not synchronized. Failover is not possible now.<br /><br /> 6 = The partners are synchronized. Failover is potentially possible. For information about the requirements for failover see, [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_state_desc**|**nvarchar(60)**|Description of the state of the mirror database and of the database mirroring session, can be one of:<br /><br /> DISCONNECTED<br /><br /> SYNCHRONIZED<br /><br /> SYNCHRONIZING<br /><br /> PENDING_FAILOVER<br /><br /> SUSPENDED<br /><br /> UNSYNCHRONIZED<br /><br /> SYNCHRONIZED<br /><br /> NULL<br /><br /> For more information, see [Mirroring States &#40;SQL Server&#41;](../../database-engine/database-mirroring/mirroring-states-sql-server.md).|  
|**mirroring_role**|**tinyint**|Current role of the local database plays in the database mirroring session.<br /><br /> 1 = Principal<br /><br /> 2 = Mirror<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_role_desc**|**nvarchar(60)**|Description of the role the local database plays in mirroring, can be one of:<br /><br /> PRINCIPAL<br /><br /> MIRROR|  
|**mirroring_role_sequence**|**int**|The number of times that mirroring partners have switched the principal and mirror roles due to a failover or forced service.<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_safety_level**|**tinyint**|Safety setting for updates on the mirror database:<br /><br /> 0 = Unknown state<br /><br /> 1 = Off [asynchronous]<br /><br /> 2 = Full [synchronous]<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_safety_level_desc**|**nvarchar(60)**|Transaction safety setting for the updates on the mirror database, can be one of:<br /><br /> UNKNOWN<br /><br /> OFF<br /><br /> FULL<br /><br /> NULL|  
|**mirroring_safety_sequence**|**int**|Update the sequence number for changes to transaction safety level.<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_partner_name**|**nvarchar(128)**|Server name of the database mirroring partner.<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_partner_instance**|**nvarchar(128)**|The instance name and computer name for the other partner. Clients require this information to connect to the partner if it becomes the principal server.<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_witness_name**|**nvarchar(128)**|Server name of the database mirroring witness.<br /><br /> NULL = No witness exists.|  
|mirroring_witness_state|**tinyint**|State of the witness in the database mirroring session of the database, can be one of:<br /><br /> 0 = Unknown<br /><br /> 1 = Connected<br /><br /> 2 = Disconnected<br /><br /> NULL = No witness exists, the database is not online, or the database is not mirrored.|  
|**mirroring_witness_state_desc**|**nvarchar(60)**|Description of state, can be one of:<br /><br /> UNKNOWN<br /><br /> CONNECTED<br /><br /> DISCONNECTED<br /><br /> NULL|  
|**mirroring_failover_lsn**|**numeric(25,0)**|Log sequence number (LSN) of the latest transaction log record that is guaranteed to be hardened to disk on both partners. After a failover, the **mirroring_failover_lsn** is used by the partners as the point of reconciliation at which the new mirror server begins to synchronize the new mirror database with the new principal database.|  
|**mirroring_connection_timeout**|**int**|Mirroring connection time out in seconds. This is the number of seconds to wait for a reply from a partner or witness before considering them unavailable. The default time-out value is 10 seconds.<br /><br /> NULL = Database is inaccessible or is not mirrored.|  
|**mirroring_redo_queue**|**int**|Maximum amount of log to be redone on the mirror. If mirroring_redo_queue_type is set to UNLIMITED, which is the default setting, this column is NULL. If the database is not online, this column is also NULL.<br /><br /> Otherwise, this column contains the maximum amount of log in megabytes. When the maximum is reached, the log is temporarily stalled on the principal as the mirror server catches up. This feature limits failover time.<br /><br /> For more information, see [Estimate the Interruption of Service During Role Switching &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/estimate-the-interruption-of-service-during-role-switching-database-mirroring.md).|  
|**mirroring_redo_queue_type**|**nvarchar(60)**|UNLIMITED indicates that mirroring will not inhibit the redo queue. This is the default setting.<br /><br /> MB for maximum size of the redo queue in mega bytes. Note that if the queue size was specified as kilobytes or gigabytes, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] converts the value into megabytes.<br /><br /> If the database is not online, this column is NULL.|  
|**mirroring_end_of_log_lsn**|**numeric(25,0)**|The local end-of-log that has been flushed to disk. This is comparable to the hardened LSN from the mirror server (see the **mirroring_failover_lsn** column).|  
|**mirroring_replication_lsn**|**numeric(25,0)**|The maximum LSN that replication can send.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sys.database_mirroring_witnesses &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/database-mirroring-witness-catalog-views-sys-database-mirroring-witnesses.md)   
 [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)   
 [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)  
  
  
