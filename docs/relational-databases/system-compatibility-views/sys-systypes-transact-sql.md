---
description: "sys.systypes (Transact-SQL)"
title: "sys.systypes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.systypes_TSQL"
  - "systypes_TSQL"
  - "sys.systypes"
  - "systypes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.systypes compatibility view"
  - "systypes system table"
ms.assetid: 1b0b1d0c-5f7b-470b-bd52-8bfa922d7889
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.systypes (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns one row for each system-supplied and each user-defined data type defined in the database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Data type name.|  
|**xtype**|**tinyint**|Physical storage type.|  
|**status**|**tinyint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**xusertype**|**smallint**|Extended user type. Overflows or returns NULL if the number of data types exceeds 32,767.|  
|**length**|**smallint**|Physical length of the data type.|  
|**xprec**|**tinyint**|Internal precision, as used by the server. Not to be used in queries.|  
|**xscale**|**tinyint**|Internal scale, as used by the server. Not to be used in queries.|  
|**tdefault**|**int**|ID of the stored procedure that contains integrity checks for this data type.|  
|**domain**|**int**|ID of the stored procedure that contains integrity checks for this data type.|  
|**uid**|**smallint**|Schema ID of the owner of the type.<br /><br /> For databases upgraded from an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the schema ID is equal to the user ID of the owner.<br /><br /> **\*\* Important \*\*** If you use any of the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DDL statements, you must use the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view instead of **sys.systypes**.<br /><br /> ALTER AUTHORIZATION ON TYPE<br /><br /> CREATE TYPE<br /><br /> Overflows or returns NULL if the number of users and roles exceeds 32,767.|  
|**reserved**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**collationid**|**int**|If character based, **collationid** is the id of the collation of the current database; otherwise, it is NULL.|  
|**usertype**|**smallint**|User type ID. Overflows or returns NULL if the number of data types exceeds 32,767.|  
|**variable**|**bit**|Variable-length data type.<br /><br /> 1 = True<br /><br /> 0 = False|  
|**allownulls**|**bit**|Indicates the default nullability for this data type. This default value is overridden by if nullability is specified by using [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) or [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md).|  
|**type**|**tinyint**|Physical storage data type.|  
|**printfmt**|**varchar(255)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**prec**|**smallint**|Level of precision for this data type.<br /><br /> -1 = **xml** or large value types.|  
|**scale**|**tinyint**|Scale for this data type, based on precision.<br /><br /> NULL = Data type is nonnumeric.|  
|**collation**|**sysname**|If character based, **collation** is the collation of the current database; otherwise, it is NULL.|  
  
## See Also  
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)   
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)  
  
  
