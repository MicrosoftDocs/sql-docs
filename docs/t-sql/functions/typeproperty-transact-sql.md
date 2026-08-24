---
title: "TYPEPROPERTY (Transact-SQL)"
description: "TYPEPROPERTY (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "TYPEPROPERTY"
  - "TYPEPROPERTY_TSQL"
helpviewer_keywords:
  - "status information [SQL Server], data types"
  - "data types [SQL Server], status information"
  - "TYPEPROPERTY function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# TYPEPROPERTY (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Returns information about a data type.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
TYPEPROPERTY (type , property)  
```  
  
## Arguments
 *type*  
 Is the name of the data type.  
  
 *property*  
 Is the type of information to be returned for the data type. *property* can be one of the following values.  
  
|Property|Description|Value returned|  
|--------------|-----------------|--------------------|  
|**AllowsNull**|Data type allows for null values.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Data type not found.|  
|**OwnerId**|Owner of the type.<br /><br /> Note: The schema owner is not necessarily the type owner.|Nonnull = The database user ID of the type owner.<br /><br /> NULL = Unsupported type, or type ID is not valid.|  
|**Precision**|Precision for the data type.|The number of digits or characters.<br /><br /> -1 = **xml** or large value data type<br /><br /> NULL = Data type not found.|  
|**Scale**|Scale for the data type.|The number of decimal places for the data type.<br /><br /> NULL = Data type is not **numeric** or not found.|  
|**UsesAnsiTrim**|ANSI padding setting was ON when the data type was created.|1 = True<br /><br /> 0 = False<br /><br /> NULL = Data type not found, or it is not a binary or string data type.|  
  
## Return Types  
 **int**  
  
## Exceptions  
 Returns NULL on error or if a caller does not have permission to view the object.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as TYPEPROPERTY may return NULL if the user does not have any permission on the object. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Examples  
  
### A. Identifying the owner of a data type  
 The following example returns the owner of a data type.  
  
```sql
SELECT TYPEPROPERTY(SCHEMA_NAME(schema_id) + '.' + name, 'OwnerId') AS owner_id, name, system_type_id, user_type_id, schema_id  
FROM sys.types;  
```  
  
### B. Returning the precision of the tinyint data type  
 The following example returns the precision or number of digits for the `tinyint` data type.  
  
```sql
SELECT TYPEPROPERTY( 'tinyint', 'PRECISION');  
```  
  
## Related content

- [TYPE_ID (Transact-SQL)](type-id-transact-sql.md)
- [TYPE_NAME (Transact-SQL)](type-name-transact-sql.md)
- [COLUMNPROPERTY (Transact-SQL)](columnproperty-transact-sql.md)
- [Metadata functions (Transact-SQL)](metadata-functions-transact-sql.md)
- [OBJECTPROPERTY (Transact-SQL)](objectproperty-transact-sql.md)
- [ALTER AUTHORIZATION (Transact-SQL)](../statements/alter-authorization-transact-sql.md)
- [sys.types (Transact-SQL)](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)
