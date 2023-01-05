---
description: "sys.sysdepends (Transact-SQL)"
title: "sys.sysdepends (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.sysdepends_TSQL"
  - "sysdepends"
  - "sysdepends_TSQL"
  - "sys.sysdepends"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysdepends system table"
  - "sys.sysdepends compatibility view"
ms.assetid: f9c182cb-386f-4e72-859f-9f1115b389f9
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysdepends (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains dependency information between objects (views, procedures, and triggers) in the database, and the objects (tables, views, and procedures) that are contained in their definition.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|Object ID.|  
|**depid**|**int**|Dependent object ID.|  
|**number**|**smallint**|Procedure number.|  
|**depnumber**|**smallint**|Dependent procedure number.|  
|**status**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**deptype**|**tinyint**|Identifies the dependent object type:<br /><br /> 0 = Object or column (non-schema-bound references only<br /><br /> 1 = Object or column (schema-bound references)|  
|**depdbid**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**depsiteid**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**selall**|**bit**|1 = Object is used in a SELECT * statement.<br /><br /> 0 = No.|  
|**resultobj**|**bit**|1 = Object is being updated.<br /><br /> 0 = No.|  
|**readobj**|**bit**|1 = The object is being read.<br /><br /> 0 = No.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)   
 [sp_depends &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-depends-transact-sql.md)   
 [sys.sql_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-dependencies-transact-sql.md)  
  
  
