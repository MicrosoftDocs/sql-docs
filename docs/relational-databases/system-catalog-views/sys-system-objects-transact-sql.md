---
title: "sys.system_objects (Transact-SQL)"
description: sys.system_objects (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.system_objects"
  - "system_objects"
  - "system_objects_TSQL"
  - "sys.system_objects_TSQL"
helpviewer_keywords:
  - "sys.system_objects catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 069e9045-97f2-4463-8e8f-c73855f3ea0a
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.system_objects (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains one row for all schema-scoped system objects that are included with [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. All system objects are contained in the schemas named sys or INFORMATION_SCHEMA.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|Object name.|  
|object_id|**int**|Object identification number. Is unique within a database.|  
|principal_id|**int**|ID of the individual owner if different from the schema owner. By default, schema-contained objects are owned by the schema owner. However, another owner can be specified by using the ALTER AUTHORIZATION statement to change ownership.<br /><br /> Is NULL if there is no other individual owner.<br /><br /> Is NULL if the object type is one of the following:<br /><br /> C = CHECK constraint<br /><br /> D = DEFAULT (constraint or stand-alone)<br /><br /> F = FOREIGN KEY constraint<br /><br /> PK = PRIMARY KEY constraint<br /><br /> R = Rule (old-style, stand-alone)<br /><br /> TA = Assembly (CLR) trigger<br /><br /> TR = SQL trigger<br /><br /> UQ = UNIQUE constraint|  
|schema_id|**int**|ID of the schema that the object is contained in.<br /><br /> For all schema-scoped system objects that included with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this value will always be in (schema_id('sys'), schema_id('INFORMATION_SCHEMA'))|  
|parent_object_id|**int**|ID of the object to which this object belongs.<br /><br /> 0 = Not a child object.|  
|type|**char(2)**|Object type:<br /><br /> AF = Aggregate function (CLR)<br /><br /> C = CHECK constraint<br /><br /> D = DEFAULT (constraint or stand-alone)<br /><br /> F = FOREIGN KEY constraint<br /><br /> FN = SQL scalar function<br /><br /> FS = Assembly (CLR) scalar-function<br /><br /> FT = Assembly (CLR) table-valued function<br /><br /> IF = SQL inline table-valued function<br /><br /> IT = Internal table<br /><br /> P = SQL Stored Procedure<br /><br /> PC = Assembly (CLR) stored-procedure<br /><br /> PG = Plan guide<br /><br /> PK = PRIMARY KEY constraint<br /><br /> R = Rule (old-style, stand-alone)<br /><br /> RF = Replication-filter-procedure<br /><br /> S = System base table<br /><br /> SN = Synonym<br /><br /> SQ = Service queue<br /><br /> TA = Assembly (CLR) DML trigger<br /><br /> TF = SQL table-valued-function<br /><br /> TR = SQL DML trigger<br /><br /> TT = Table type<br /><br /> U = Table (user-defined)<br /><br /> UQ = UNIQUE constraint<br /><br /> V = View<br /><br /> X = Extended stored procedure|  
|type_desc|**nvarchar(60)**|Description of the object type. AGGREGATE_FUNCTION<br /><br /> CHECK_CONSTRAINT<br /><br /> DEFAULT_CONSTRAINT<br /><br /> FOREIGN_KEY_CONSTRAINT<br /><br /> SQL_SCALAR_FUNCTION<br /><br /> CLR_SCALAR_FUNCTION<br /><br /> CLR_TABLE_VALUED_FUNCTION<br /><br /> SQL_INLINE_TABLE_VALUED_FUNCTION<br /><br /> INTERNAL_TABLE<br /><br /> SQL_STORED_PROCEDURE<br /><br /> CLR_STORED_PROCEDURE<br /><br /> PLAN_GUIDE<br /><br /> PRIMARY_KEY_CONSTRAINT<br /><br /> RULE<br /><br /> REPLICATION_FILTER_PROCEDURE<br /><br /> SYSTEM_TABLE<br /><br /> SYNONYM<br /><br /> SERVICE_QUEUE<br /><br /> CLR_TRIGGER<br /><br /> SQL_TABLE_VALUED_FUNCTION<br /><br /> SQL_TRIGGER<br /><br /> TABLE_TYPE<br /><br /> USER_TABLE<br /><br /> UNIQUE_CONSTRAINT<br /><br /> VIEW<br /><br /> EXTENDED_STORED_PROCEDURE|  
|create_date|**datetime**|Date the object was created.|  
|modify_date|**datetime**|Date the object was last modified by using an ALTER statement. If the object is a table or a view, modify_date also changes when a clustered index on the table or view is created or altered.|  
|is_ms_shipped|**bit**|Object is created by an internal [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component.|  
|is_published|**bit**|Object is published.|  
|is_schema_published|**bit**|Only the schema of the object is published.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)  
  
  
