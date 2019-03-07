---
title: "sys.fn_listextendedproperty (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_listextendedproperty"
  - "fn_listextendedproperty_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_listextendedproperty function"
  - "displaying extended properties"
  - "database extended properties [SQL Server]"
  - "viewing extended properties"
  - "column extended properties [SQL Server]"
  - "sys.fn_listextendedproperties function"
  - "database objects [SQL Server], extended properties"
  - "extended properties [SQL Server], columns"
  - "table extended properties [SQL Server]"
ms.assetid: 59bbb91f-a277-4a35-803e-dcb91e847a49
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fn_listextendedproperty (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns extended property values of database objects.  
 
 
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_listextendedproperty (   
    { default | 'property_name' | NULL }   
  , { default | 'level0_object_type' | NULL }   
  , { default | 'level0_object_name' | NULL }   
  , { default | 'level1_object_type' | NULL }   
  , { default | 'level1_object_name' | NULL }   
  , { default | 'level2_object_type' | NULL }   
  , { default | 'level2_object_name' | NULL }   
  )   
```  
  
## Arguments  
 { default | '*property_name*' | NULL}  
 Is the name of the property. *property_name* is **sysname**. Valid inputs are default, NULL, or a property name.  
  
 { default | '*level0_object_type*' | NULL}  
 Is the user or user-defined type. *level0_object_type* is **varchar(128)**, with a default of NULL. Valid inputs are ASSEMBLY, CONTRACT, EVENT NOTIFICATION, FILEGROUP, MESSAGE TYPE, PARTITION FUNCTION, PARTITION SCHEME, REMOTE SERVICE BINDING, ROUTE, SCHEMA, SERVICE, TRIGGER, TYPE, USER, and NULL.  
  
> [!IMPORTANT]  
>  USER and TYPE as level-0 types will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features. Use SCHEMA as the level 0 type instead of USER. For TYPE, use SCHEMA as the level 0 type and TYPE as the level 1 type.  
  
 { default | '*level0_object_name*' | NULL }  
 Is the name of the level 0 object type specified. *level0_object_name* is **sysname** with a default of NULL. Valid inputs are default, NULL, or an object name.  
  
 { default | '*level1_object_type*' | NULL }  
 Is the type of level 1 object. *level1_object_type* is **varchar(128)** with a default of NULL. Valid inputs are AGGREGATE, DEFAULT, FUNCTION, LOGICAL FILE NAME, PROCEDURE, QUEUE, RULE, SYNONYM, TABLE, TYPE, VIEW, XML SCHEMA COLLECTION, and NULL.  
  
> [!NOTE]  
>  Default maps to NULL and 'default' maps to the object type DEFAULT.  
  
 {default | '*level1_object_name*' |NULL }  
 Is the name of the level 1 object type specified. *level1_object_name* is **sysname** with a default of NULL. Valid inputs are default, NULL, or an object name.  
  
 { default | '*level2_object_type*' |NULL }  
 Is the type of level 2 object. *level2_object_type* is **varchar(128)** with a default of NULL. Valid inputs are DEFAULT, default (maps to NULL), and NULL. Valid inputs for *level2_object_type* are COLUMN, CONSTRAINT, EVENT NOTIFICATION, INDEX, PARAMETER, TRIGGER, and NULL.  
  
 { default | '*level2_object_name*' |NULL }  
 Is the name of the level 2 object type specified. *level2_object_name* is **sysname** with a default of NULL. Valid inputs are default, NULL, or an object name.  
  
## Tables Returned  
 This is the format of the tables returned by fn_listextendedproperty.  
  
|Column name|Data type|  
|-----------------|---------------|  
|objtype|**sysname**|  
|objname|**sysname**|  
|name|**sysname**|  
|value|**sql_variant**|  
  
 If the table returned is empty, either the object does not have extended properties or the user does not have permissions to list the extended properties on the object. When returning extended properties on the database itself, the objtype and objname columns will be NULL.  
  
## Remarks  
 If the value for *property_name* is NULL or default, fn_listextendedproperty returns all the properties for the specified object.  
  
 When the object type is specified and the value of the corresponding object name is NULL or default, fn_listextendedproperty returns all extended properties for all objects of the type specified.  
  
 The objects are distinguished according to levels, with level 0 as the highest and level 2 the lowest. If a lower-level object, level 1 or 2, type and name are specified, the parent object type and name should be given values that are not NULL or default. Otherwise, the function returns an empty result set.  
  
 **objname** is fixed as Latin1_General_CI_AI. However you can workaround this by overriding collation in comparison.  
  
```  
SELECT o.[object_id] AS 'table_id', o.[name] 'table_name',  
0 AS 'column_order', NULL AS 'column_name', NULL AS 'column_datatype',  
NULL AS 'column_length', Cast(e.value AS varchar(500)) AS 'column_description'  
FROM AdventureWorks.sys.objects AS o  
LEFT JOIN sys.fn_listextendedproperty(N'MS_Description', N'user',N'HumanResources',N'table', N'Employee', null, default) AS e  
    ON o.name = e.objname COLLATE SQL_Latin1_General_CP1_CI_AS  
WHERE o.name = 'Employee';  
```  
  
## Permissions  
 Permissions to list extended properties of objects vary by object type.  
  
## Examples  
  
### A. Displaying extended properties on a database  
 The following example displays all extended properties set on the database object itself.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT objtype, objname, name, value  
FROM fn_listextendedproperty(default, default, default, default, default, default, default);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `objtype    objname     name            value`  
  
 `---------  ---------   -----------     ----------------------------`  
  
 `NULL       NULL        MS_Description  AdventureWorks2008 Sample OLTP Database`  
  
 `(1 row(s) affected)`  
  
### B. Displaying extended properties on all columns in a table  
 The following example lists extended properties for columns in the `ScrapReason` table. This is contained in the schema `Production`.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT objtype, objname, name, value  
FROM fn_listextendedproperty (NULL, 'schema', 'Production', 'table', 'ScrapReason', 'column', default);  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `objtype objname      name            value`  
  
 `------- -----------  -------------   ------------------------`  
  
 `COLUMN ScrapReasonID MS_Description  Primary key for ScrapReason records.`  
  
 `COLUMN Name          MS_Description  Failure description.`  
  
 `COLUMN ModifiedDate  MS_Description  Date the record was last updated.`  
  
 `(3 row(s) affected)`  
  
### C. Displaying extended properties on all tables in a schema  
 The following example lists extended properties for all tables contained in the `Sales` schema.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT objtype, objname, name, value  
FROM fn_listextendedproperty (NULL, 'schema', 'Sales', 'table', default, NULL, NULL);  
GO  
```  
  
## See Also  
 [sp_addextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md)   
 [sp_dropextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropextendedproperty-transact-sql.md)   
 [sp_updateextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-updateextendedproperty-transact-sql.md)   
 [sys.extended_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-properties-catalog-views-sys-extended-properties.md)  
  
  
