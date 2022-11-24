---
title: "sysmergeschemaarticles (Transact-SQL)"
description: sysmergeschemaarticles (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sysmergeschemaarticles_TSQL"
  - "sysmergeschemaarticles"
helpviewer_keywords:
  - "sysmergeschemaarticles system table"
dev_langs:
  - "TSQL"
ms.assetid: b5085979-2f76-48e1-bf3b-765a84003dd9
---
# sysmergeschemaarticles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Tracks schema-only articles for merge replication. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|The name of the schema-only article in the merge publication.|  
|**type**|**tinyint**|Indicates the type of schema-only article, which can be one of the following:<br /><br /> **0x20** = Stored procedure schema-only article.<br /><br /> **0x40** = View schema-only article or indexed view schema-only article.|  
|**objid**|**int**|The object identifier of the article base object. Can be the object identifier of a procedure, view, indexed, view, or user-defined function.|  
|**artid**|**uniqueidentifier**|The article ID.|  
|**description**|**nvarchar(255)**|The description of the article.|  
|**pre_creation_command**|**tinyint**|Default action to take when the article is created in the subscription database:<br /><br /> **0 =** None - if the table already exists at the Subscriber, no action is taken.<br /><br /> **1** = Drop - drops the table before re-creating it.<br /><br /> **2** = Delete -issues a delete based on the WHERE clause in the subset filter.<br /><br /> **3** = Truncate -same as **2**, but deletes pages instead of rows. However, does not take a WHERE clause.|  
|**pubid**|**uniqueidentifier**|The unique identifier of the publication.|  
|**status**|**tinyint**|Indicates the status of the schema-only article, which can be one of the following:<br /><br /> **1** = Unsynced - the initial processing script to publish the table runs the next time the Snapshot Agent runs.<br /><br /> **2** = Active - the initial processing script to publish the table has been run.<br /><br /> **5** = New_inactive - to be added.<br /><br /> **6** = New_active - to be added.|  
|**creation_script**|**nvarchar(255)**|The path and name of an optional article schema pre-creation script used to create target table.|  
|**schema_option**|**binary(8)**|The bitmap of the schema generation option for the given schema-only article, which can be the bitwise logical OR the result of one or more of these values:<br /><br /> **0x00** = Disable scripting by the Snapshot Agent and uses the provided CreationScript.<br /><br /> **0x01** = Generate the object creation (CREATE TABLE, CREATE PROCEDURE, and so on).<br /><br /> **0x10** = Generate a corresponding clustered index.<br /><br /> **0x20** = Convert user-defined data types to base data types.<br /><br /> **0x40** = Generate corresponding nonclustered index or indexes.<br /><br /> **0x80** = Include declared referential integrity on the primary keys.<br /><br /> **0x100** = Replicate user triggers on a table article, if defined.<br /><br /> **0x200** = Replicate foreign key constraints. If the referenced table is not part of a publication, all foreign key constraints on a published table are not replicated.<br /><br /> **0x400** = Replicate check constraints.<br /><br /> **0x800** = Replicate defaults.<br /><br /> **0x1000** = Replicate column-level collation.<br /><br /> **0x2000** = Replicate extended properties associated with the published article source object.<br /><br /> **0x4000** = Replicate unique keys if defined on a table article.<br /><br /> **0x8000** = Replicate a primary key and unique keys on a table article as constraints using ALTER TABLE statements.<br /><br /> For more information on possible values for **schema_option**, see [sp_addmergearticle](../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md).|  
|**destination_object**|**sysname**|The name of the destination object in the subscription database. This value applies only to schema-only articles, such as stored procedures, views, and UDFs.|  
|**destination_owner**|**sysname**|The owner of the object in the subscription database, if it is not **dbo**.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
