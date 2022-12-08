---
title: "sys.all_parameters (Transact-SQL)"
description: sys.all_parameters (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/30/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "all_parameters_TSQL"
  - "sys.all_parameters"
  - "all_parameters"
  - "sys.all_parameters_TSQL"
helpviewer_keywords:
  - "sys.all_parameters catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.all_parameters (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Shows the union of all parameters that belong to user-defined or system objects.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object to which this parameter belongs.|  
|**name**|**sysname**|Name of parameter. Is unique within the object. If the object is a scalar function, the parameter name is an empty string in the row representing the return value.|  
|**parameter_id**|**int**|ID of parameter. Is unique within the object. If the object is a scalar function, **parameter_id** = 0 represents the return value.|  
|**system_type_id**|**tinyint**|ID of the system type of the parameter.|  
|**user_type_id**|**int**|ID of the type of the parameter as defined by the user.<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|**max_length**|**smallint**|Maximum length of the parameter, in bytes.<br /><br /> -1 = Column data type is **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, or **xml**.|  
|**precision**|**tinyint**|Precision of the parameter if it is numeric-based; otherwise, 0.|  
|**scale**|**tinyint**|Scale of the parameter if it is numeric-based; otherwise, 0.|  
|**is_output**|**bit**|1 = Parameter is output (or return); otherwise, 0.|  
|**is_cursor_ref**|**bit**|1 = Parameter is a cursor reference parameter.|  
|**has_default_value**|**bit**|1 = Parameter has a default value.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only maintains default values for CLR objects in this catalog view; therefore, this column will always have a value of 0 for [!INCLUDE[tsql](../../includes/tsql-md.md)] objects. To view the default value of a parameter in a [!INCLUDE[tsql](../../includes/tsql-md.md)] object, query the **definition** column of the [sys.sql_modules](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) catalog view, or use the [OBJECT_DEFINITION](../../t-sql/functions/object-definition-transact-sql.md) system function.|  
|**is_xml_document**|**bit**|1 = Content is a complete XML document.<br /><br /> 0 = Content is a document fragment or the data type of the column is not **xml**.|  
|**default_value**|**sql_variant**|If **has_default_value** is 1, the value of this column is the value of the default for the parameter; otherwise `NULL`.|  
|**xml_collection_id**|**int**|Is the ID of the XML schema collection used to validate the parameter.<br /><br /> Nonzero if the data type of the parameter is **xml** and the XML is typed.<br /><br /> 0 = There is no XML schema collection, or the parameter is not XML.|  
|**is_readonly**|**bit**|1 =  Parameter is READONLY; otherwise, 0.|  
|**is_nullable**|**bit**|1 = Parameter is nullable. (the default).<br /><br /> 0 = Parameter is not nullable, for more efficient execution of natively-compiled stored procedures.|  
|**encryption_type**|**int**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Encryption type:<br /><br /> 1 = Deterministic encryption<br /><br /> 2 = Randomized encryption|  
|**encryption_type_desc**|**nvarchar(64)**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Encryption type description:<br /><br /> RANDOMIZED<br /><br /> DETERMINISTIC|  
|**encryption_algorithm_name**|**sysname**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> Name of encryption algorithm.<br /><br /> Only AEAD_AES_256_CBC_HMAC_SHA_512 is supported.|  
|**column_encryption_key_id**|**int**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)].<br /><br /> ID of the CEK.|  
|**column_encryption_key_database_name**|**sysname**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later, [!INCLUDE[ssSDW_md](../../includes/sssds-md.md)].<br /><br /> The name of the database where the column encryption key exists if different than the database of the column. Is `NULL` if the key exists in the same database as the column.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)   
 [sys.parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md)   
 [sys.system_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-parameters-transact-sql.md)  
  
  
