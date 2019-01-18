---
title: "sys.types (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "types"
  - "types_TSQL"
  - "sys.types_TSQL"
  - "sys.types"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.types catalog view"
  - "table-valued parameters,sys.types"
ms.assetid: a5dbc842-71a0-4f62-b5e0-f560a99b7f8c
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.types (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Contains a row for each system and user-defined type.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the type. Is unique within the schema.|  
|**system_type_id**|**tinyint**|ID of the internal system-type of the type.|  
|**user_type_id**|**int**|ID of the type. Is unique within the database. For system data types, **user_type_id** = **system_type_id**.|  
|**schema_id**|**int**|ID of the schema to which the type belongs.|  
|**principal_id**|**int**|ID of the individual owner if different from schema owner. By default, schema-contained objects are owned by the schema owner. However, an alternate owner can be specified by using the ALTER AUTHORIZATION statement to change ownership.<br /><br /> NULL if there is no alternate individual owner.|  
|**max_length**|**smallint**|Maximum length (in bytes) of the type.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.<br /><br /> For **text** columns, the **max_length** value will be 16.|  
|**precision**|**tinyint**|Max precision of the type if it is numeric-based; otherwise, 0.|  
|**scale**|**tinyint**|Max scale of the type if it is numeric-based; otherwise, 0.|  
|**collation_name**|**sysname**|Name of the collation of the type if it is character-based; other wise, NULL.|  
|**is_nullable**|**bit**|Type is nullable.|  
|**is_user_defined**|**bit**|1 = User-defined type.<br /><br /> 0 = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type.|  
|**is_assembly_type**|**bit**|1 = Implementation of the type is defined in a CLR assembly.<br /><br /> 0 = Type is based on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system data type.|  
|**default_object_id**|**int**|ID of the stand-alone default that is bound to the type by using [sp_bindefault](../../relational-databases/system-stored-procedures/sp-bindefault-transact-sql.md).<br /><br /> 0 = No default exists.|  
|**rule_object_id**|**int**|ID of the stand-alone rule that is bound to the type by using [sp_bindrule](../../relational-databases/system-stored-procedures/sp-bindrule-transact-sql.md).<br /><br /> 0 = No rule exists.|  
|**is_table_type**|**bit**|Indicates the type is a table.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Scalar Types Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/scalar-types-catalog-views-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)  
  
  
