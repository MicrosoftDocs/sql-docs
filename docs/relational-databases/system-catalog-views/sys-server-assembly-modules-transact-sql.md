---
title: "sys.server_assembly_modules (Transact-SQL)"
description: sys.server_assembly_modules (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_assembly_modules_TSQL"
  - "sys.server_assembly_modules"
  - "server_assembly_modules"
  - "sys.server_assembly_modules_TSQL"
helpviewer_keywords:
  - "sys.server_assembly_modules catalog view"
dev_langs:
  - "TSQL"
ms.assetid: af799e38-2d16-49b2-bcf5-6f9199af899e
---
# sys.server_assembly_modules (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each assembly module for the server-level triggers of type TA. This view maps assembly triggers to the underlying CLR implementation. You can join this relation to **sys.server_triggers**. The assembly must be loaded into the **master** database. The tuple (object_id) is the key for the relation.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|This is a FOREIGN KEY reference back to the object upon which this assembly module is defined.|  
|**assembly_id**|**int**|ID of the assembly from which this module was created. The assembly must be loaded into the master database.|  
|**assembly_class**|**sysname**|Name of the class within assembly that defines this module.|  
|**assembly_method**|**sysname**|Name of the method within the class that defines this module. Is NULL for aggregate functions (AF).|  
|**execute_as_principal_id**|**int**|ID of the EXECUTE AS server principal.<br /><br /> NULL by default or if EXECUTE AS CALLER.<br /><br /> ID of the specified principal if EXECUTE AS SELF EXECUTE AS \<principal>.<br /><br /> -2 = EXECUTE AS OWNER.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)  
  
  
