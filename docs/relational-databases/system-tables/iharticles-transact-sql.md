---
title: "IHarticles (Transact-SQL)"
description: IHarticles (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "IHarticles"
  - "IHarticles_TSQL"
helpviewer_keywords:
  - "IHarticles system table"
dev_langs:
  - "TSQL"
ms.assetid: 773ef9b7-c993-4629-9516-70c47b9dcf65
---
# IHarticles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **IHarticles** system table contains one row for each article being replicated from a non-SQL Server Publisher using the current Distributor. This table is stored in the distribution database.  
  
## Definition  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**article_id**|**int**|The identity column that provides a unique ID number for the article.|  
|**name**|**sysname**|The name associated with the article, unique within the publication.|  
|**publication_id**|**smallint**|The ID of the publication to which the article belongs.|  
|**table_id**|**int**|The ID of the table being published from [IHpublishertables](../../relational-databases/system-tables/ihpublishertables-transact-sql.md).|  
|**publisher_id**|**smallint**|The ID of the Non-SQL Server Publisher.|  
|**creation_script**|**nvarchar(255)**|The schema script for the article.|  
|**del_cmd**|**nvarchar(255)**|The replication command type used when replicating deletes with table articles. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).|  
|**filter**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**filter_clause**|**ntext**|The WHERE clause of the article, used for horizontal filtering and written in an standard Transact-SQL that can be interpreted by the non-SQL Publisher.|  
|**ins_cmd**|**nvarchar(255)**|The replication command type used when replicating inserts with table articles. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).|  
|**pre_creation_cmd**|**tinyint**|The command to execute before the initial snapshot is applied when an object with the same name already exists at the Subscriber.<br /><br /> **0** = None - a command is not executed.<br /><br /> **1** = DROP - drop the destination table.<br /><br /> **2** = DELETE - delete data from the destination table.<br /><br /> **3** = TRUNCATE - truncate the destination table.|  
|**status**|**tinyint**|The bitmask of the article options and status, which can be the bitwise logical OR result of one or more of these values:<br /><br /> **0** = No additional properties.<br /><br /> **1** = Active.<br /><br /> **8** = Include the column name in INSERT statements.<br /><br /> **16** = Use parameterized statements.<br /><br /> For example, an active article using parameterized statements would have a value of 17 in this column. A value of 0 means that the article is inactive and no additional properties are defined.|  
|**type**|**tinyint**|The type of article:<br /><br /> **1** = Log-based article.|  
|**upd_cmd**|**nvarchar(255)**|The replication command type used when replicating updates with table articles. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../../relational-databases/replication/transactional/transactional-articles-specify-how-changes-are-propagated.md).|  
|**schema_option**|**binary(8)**|The bitmap of the schema generation option for the given article, which can be the bitwise logical OR result of one or more of these values:<br /><br /> **0x00** = Disable scripting by the Snapshot Agent and uses the provided CreationScript.<br /><br /> **0x01** = Generate the object creation (CREATE TABLE, CREATE PROCEDURE, and so on).<br /><br /> **0x10** = Generate a corresponding clustered index.<br /><br /> **0x40** = Generate corresponding nonclustered indexes.<br /><br /> **0x80** = Include declared referential integrity on the primary keys.<br /><br /> **0x1000** = Replicates column-level collation. Note: This option is set by default for Oracle Publishers to enable case-sensitive comparisons.<br /><br /> **0x4000** = Replicate unique keys if defined on a table article.<br /><br /> **0x8000** = Replicate a primary key and unique keys on a table article as constraints using ALTER TABLE statements.|  
|**dest_owner**|**sysname**|The owner of the table at the destination database.|  
|**dest_table**|**sysname**|The name of the destination table.|  
|**tablespace_name**|**nvarchar(255)**|Identifies the tablespace used by the logging table for the article.|  
|**objid**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**sync_objid**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**description**|**nvarchar(255)**|The descriptive entry for the article.|  
|**publisher_status**|**int**|Is used to indicate if the view that defines the published article has been defined by calling [sp_articleview](../../relational-databases/system-stored-procedures/sp-articleview-transact-sql.md).<br /><br /> **0** = [sp_articleview](../../relational-databases/system-stored-procedures/sp-articleview-transact-sql.md) has been called.<br /><br /> **1** = [sp_articleview](../../relational-databases/system-stored-procedures/sp-articleview-transact-sql.md) has not been called.|  
|**article_view_owner**|**nvarchar(255)**|The owner of the synchronization object on the Publisher used by the Log Reader Agent.|  
|**article_view**|**nvarchar(255)**|The synchronization object on the Publisher used by the Log Reader Agent.|  
|**ins_scripting_proc**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**del_scripting_proc**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**upd_scripting_proc**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**custom_script**|**int**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**fire_triggers_on_snapshot**|**bit**|This column is not used and is included only to make the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view of the **IHarticles** table compatible with the [sysarticles](../../relational-databases/system-views/sysarticles-system-view-transact-sql.md) view used for SQL Server articles ([sysarticles](../../relational-databases/system-tables/sysarticles-transact-sql.md)).|  
|**instance_id**|**int**|Identifies the current instance of the article log for the published table.|  
|**use_default_datatypes**|**bit**|Indicates whether the article uses default data type mappings; a value of **1** indicates that default data type mappings are used.|  
  
## See Also  
 [Heterogeneous Database Replication](../../relational-databases/replication/non-sql/heterogeneous-database-replication.md)   
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)   
 [sp_addarticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md)   
 [sp_changearticle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md)  
  
  
