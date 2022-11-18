---
title: "sysschemaarticles (Transact-SQL)"
description: sysschemaarticles (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sysschemaarticles_TSQL"
  - "sysschemaarticles"
helpviewer_keywords:
  - "sysschemaarticles system table"
dev_langs:
  - "TSQL"
ms.assetid: 67a1c039-c283-4a9c-bacc-b9b3973590c3
---
# sysschemaarticles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Tracks schema-only articles for transactional and snapshot publications. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**artid**|**int**|The article ID.|  
|**creation_script**|**nvarchar(255)**|The path and name of an article schema script used to create the target table.|  
|**description**|**nvarchar(255)**|The descriptive entry for the article.|  
|**dest_object**|**sysname**|The name of the object in the subscription database if the article is a schema-only article, such as stored procedure, view, or UDF.|  
|**name**|**sysname**|The name of the schema-only article in a publication.|  
|**objid**|**int**|The object identifier of the article base object. It can be the object identifier of a procedure, view, indexed, view, or UDF.|  
|**pubid**|**int**|The ID for the publication.|  
|**pre_creation_cmd**|**tinyint**|Specifies what the system should do if it detects an existing object of the same name at the Subscriber when applying the snapshot for this article:<br /><br /> **0** = Nothing.<br /><br /> **1** = Delete destination table.<br /><br /> **2** = Drop destination table.<br /><br /> **3** = Truncate destination table.|  
|**status**|**int**|The bitmap used to indicate the status of the article.|  
|**type**|**tinyint**|The value indicating the type of schema-only article:<br /><br /> **32** = Stored procedure.<br /><br /> **64** = View or indexed view. <br /><br /> **96** = Aggregate function.<br /><br /> **128** = Function.|  
|**schema_option**|**binary(8)**|The bitmask of the schema generation option for the given article. It specifies the automatic creation of the stored procedure in the destination database for all CALL/MCALL/XCALL syntaxes, and can be the bitwise logical OR result of one or more of these values:<br /><br /> **0x00** = Disables scripting by the Snapshot Agent and uses *creation_script*.<br /><br /> **0x01** = Generates the object creation (CREATE TABLE, CREATE PROCEDURE, and so on). This value is the default for stored procedure articles.<br /><br /> **0x02** = Generates custom stored procedures for the article, if defined.<br /><br /> **0x10** = Generates a corresponding clustered index.<br /><br /> **0x20** = Converts user-defined data types to base data types.<br /><br /> **0x40**= Generates corresponding nonclustered index(es).<br /><br /> **0x80**= Includes declared referential integrity on the primary keys.<br /><br /> **0x73** = Generates the CREATE TABLE statement, creates clustered and nonclustered indexes, converts user-defined data types to base data types, and generates custom stored procedure scripts to be applied at the Subscriber. This value is the default for all articles except stored procedure articles.<br /><br /> **0x100**= Replicates user triggers on a table article, if defined.<br /><br /> **0x200**= Replicates foreign key constraints. If the referenced table is not part of a publication, all foreign key constraints on a published table will not be replicated.<br /><br /> **0x400**= Replicates check constraints.<br /><br /> **0x800**= Replicates defaults.<br /><br /> **0x1000**= Replicates column-level collation.<br /><br /> **0x2000**= Replicates extended properties associated with the published article source object.<br /><br /> **0x4000**= Replicates unique keys if defined on a table article.<br /><br /> **0x8000**= Replicates primary key and unique keys on a table article as constraints using ALTER TABLE statements.|  
|**dest_owner**|**sysname**|The owner of the table at the destination database.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
