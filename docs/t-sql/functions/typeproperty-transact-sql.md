---
title: "TYPEPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "TYPEPROPERTY"
  - "TYPEPROPERTY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "status information [SQL Server], data types"
  - "data types [SQL Server], status information"
  - "TYPEPROPERTY function"
ms.assetid: bc311c80-bac5-46ab-a5c8-68b1c6bbf24a
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# TYPEPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns information about a data type.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
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
  
```  
SELECT TYPEPROPERTY(SCHEMA_NAME(schema_id) + '.' + name, 'OwnerId') AS owner_id, name, system_type_id, user_type_id, schema_id  
FROM sys.types;  
```  
  
### B. Returning the precision of the tinyint data type  
 The following example returns the precision or number of digits for the `tinyint` data type.  
  
```  
SELECT TYPEPROPERTY( 'tinyint', 'PRECISION');  
```  
  
## See Also  
 [TYPE_ID &#40;Transact-SQL&#41;](../../t-sql/functions/type-id-transact-sql.md)   
 [TYPE_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/type-name-transact-sql.md)   
 [COLUMNPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/columnproperty-transact-sql.md)   
 [Metadata Functions &#40;Transact-SQL&#41;](../../t-sql/functions/metadata-functions-transact-sql.md)   
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)   
 [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md)   
 [sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)  
  
  

