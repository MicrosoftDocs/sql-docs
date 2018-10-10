---
title: "sys.all_parameters (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "all_parameters_TSQL"
  - "sys.all_parameters"
  - "all_parameters"
  - "sys.all_parameters_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.all_parameters catalog view"
ms.assetid: eecbb68e-9b4c-4243-94e2-8096a9cc7892
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.all_parameters (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

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
|**default_value**|**sql_variant**|If **has_default_value** is 1, the value of this column is the value of the default for the parameter; otherwise, NULL.|  
|**xml_collection_id**|**int**|Is the ID of the XML schema collection used to validate the parameter.<br /><br /> Nonzero if the data type of the parameter is **xml** and the XML is typed.<br /><br /> 0 = There is no XML schema collection, or the parameter is not XML.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [sys.parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameters-transact-sql.md)   
 [sys.system_parameters &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-system-parameters-transact-sql.md)  
  
  
