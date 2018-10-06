---
title: "sys.database_mirroring_witnesses (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.database_mirroring_witnesses"
  - "sys.database_mirroring_witnesses_TSQL"
  - "database_mirroring_witnesses"
  - "database_mirroring_witnesses_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database mirroring [SQL Server], catalog views"
  - "sys.database_mirroring_witnesses catalog view"
  - "witness [SQL Server], sys.database_mirroring_witnesses catalog view"
ms.assetid: 0dd5b794-733b-4a3c-b5a4-62f9f1f0f22d
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Database Mirroring Witness Catalog Views - sys.database_mirroring_witnesses
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Contains a row for every witness role that a server plays in a database mirroring partnership. 
  
  In a database mirroring session, automatic failover requires a witness server. Ideally, the witness resides on a separate computer from both the principal and mirror servers. The witness does not serve the database. Instead, it monitors the status of the principal and mirror servers. If the principal server fails, the witness may initiate automatic failover to the mirror server. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_name**|**sysname**|Name of the two copies of the database in the database mirroring session.|  
|**principal_server_name**|**sysname**|Name of partner server whose copy of the database is currently the principal database.|  
|**mirror_server_name**|**sysname**|Name of the partner server whose copy of the database is currently the mirror database.|  
|**safety_level**|**tinyint**|Transaction safety setting for updates on the mirror database:<br /><br /> 0 = Unknown state<br /><br /> 1 = Off (asynchronous)<br /><br /> 2 = Full (synchronous)<br /><br /> Using a witness for automatic failover requires full transaction safety, which is the default.|  
|**safety_level_desc**|**nvarchar(60)**|Description of safety guarantee of updates on the mirror database:<br /><br /> UNKNOWN<br /><br /> OFF<br /><br /> FULL|  
|**safety_sequence_number**|**int**|Update sequence number for changes to **safety_level**.|  
|**role_sequence_number**|**int**|Update sequence number for changes to principal/mirror roles played by the mirroring partners.|  
|**mirroring_guid**|**uniqueidentifier**|Identifier of the mirroring partnership.|  
|**family_guid**|**uniqueidentifier**|Identifier of the backup family for the database. Used for detecting matching restore states.|  
|**is_suspended**|**bit**|Database mirroring is suspended.|  
|**is_suspended_sequence_number**|**int**|Sequence number for setting **is_suspended**.|  
|**partner_sync_state**|**tinyint**|Synchronization state of the mirroring session:<br /><br /> 5 = The partners are synchronized. Failover is potentially possible. For information about the requirements for failover see, [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).<br /><br /> 6 = The partners are not synchronized. Failover is not possible now.|  
|**partner_sync_state_desc**|**nvarchar(60)**|Description of the synchronization state of the mirroring session:<br /><br /> SYNCHRONIZED<br /><br /> UNSYNCHRONIZED|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md)   
 [sys.database_mirroring &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md)   
 [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)  
  
  
