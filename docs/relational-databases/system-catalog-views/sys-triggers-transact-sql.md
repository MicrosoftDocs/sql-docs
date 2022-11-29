---
title: "sys.triggers (Transact-SQL)"
description: sys.triggers (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "triggers"
  - "triggers_TSQL"
  - "sys.triggers"
  - "sys.triggers_TSQL"
helpviewer_keywords:
  - "sys.triggers catalog view"
dev_langs:
  - "TSQL"
ms.assetid: cefa4fc4-b8b9-4cd7-b124-eed5283acbfc
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.triggers (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Contains a row for each object that is a trigger, with a type of TR or TA. DML trigger names are schema-scoped and, therefore, are visible in **sys.objects**. DDL trigger names are scoped by the parent entity and are only visible in this view.  
  
 The **parent_class** and **name** columns uniquely identify the trigger in the database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Trigger name. DML trigger names are schema-scoped. DDL trigger names are scoped with respect to the parent entity.|  
|**object_id**|**int**|Object identification number. Is unique within a database.|  
|**parent_class**|**tinyint**|Class of the parent of the trigger.<br /><br /> 0 = Database, for the DDL triggers.<br /><br /> 1 = Object or column for the DML triggers.|  
|**parent_class_desc**|**nvarchar(60)**|Description of the parent class of the trigger.<br /><br /> DATABASE<br /><br /> OBJECT_OR_COLUMN|  
|**parent_id**|**int**|ID of the parent of the trigger, as follows:<br /><br /> 0 = Triggers that are database-parented triggers.<br /><br /> For DML triggers, this is the **object_id** of the table or view on which the DML trigger is defined.|  
|**type**|**char(2)**|Object type:<br /><br /> TA = Assembly (CLR) trigger<br /><br /> TR = SQL trigger|  
|**type_desc**|**nvarchar(60)**|Description of object type.<br /><br /> CLR_TRIGGER<br /><br /> SQL_TRIGGER|  
|**create_date**|**datetime**|Date the trigger was created.|  
|**modify_date**|**datetime**|Date the object was last modified by using an ALTER statement.|  
|**is_ms_shipped**|**bit**|Trigger created on behalf of the user by an internal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component.|  
|**is_disabled**|**bit**|Trigger is disabled.|  
|**is_not_for_replication**|**bit**|Trigger was created as NOT FOR REPLICATION.|  
|**is_instead_of_trigger**|**bit**|1 = INSTEAD OF triggers<br /><br /> 0 = AFTER triggers.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Security Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
