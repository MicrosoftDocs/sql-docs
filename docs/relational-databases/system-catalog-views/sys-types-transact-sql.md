---
title: "sys.types (Transact-SQL)"
description: "Contains a row for each system and user-defined type."
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "types"
  - "types_TSQL"
  - "sys.types_TSQL"
  - "sys.types"
helpviewer_keywords:
  - "sys.types catalog view"
  - "table-valued parameters,sys.types"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.types (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Contains a row for each system and user-defined type.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**name**|**sysname**|Name of the type. Is unique within the schema.|
|**system_type_id**|**tinyint**|ID of the internal system type.|
|**user_type_id**|**int**|ID of the type. Is unique within the database. For system data types, **user_type_id** = **system_type_id**.<br/><br/>CLR assembly types such as **hierarchyid**, **geometry** and **geography**, will have a different **system_type_id**, and can be identified using **is_assembly_type**. The **sysname** data type is an internal data type based on **nvarchar**.|
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

## See also

- [System Catalog Views (Transact-SQL)](catalog-views-transact-sql.md)
- [Scalar Types Catalog Views (Transact-SQL)](scalar-types-catalog-views-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../../t-sql/statements/alter-authorization-transact-sql.md)
- [OBJECTPROPERTY  (Transact-SQL)](../../t-sql/functions/objectproperty-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)
