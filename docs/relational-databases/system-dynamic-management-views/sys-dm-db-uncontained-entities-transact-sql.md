---
title: "sys.dm_db_uncontained_entities (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_db_uncontained_entities"
  - "dm_db_uncontained_entities_TSQL"
  - "sys.dm_db_uncontained_entities_TSQL"
  - "dm_db_uncontained_entities"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_uncontained_entities dynamic management view"
ms.assetid: f417efd4-8c71-4f81-bc9c-af13bb4b88ad
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_db_uncontained_entities (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Shows any uncontained objects used in the database. Uncontained objects are objects that cross the database boundary in a contained database. This view is accessible from both a contained database and a non-contained database. If sys.dm_db_uncontained_entities is empty, your database does not use any uncontained entities.  
  
 If a module crosses the database boundary more than once, only the first discovered crossing is reported.  
  
||||  
|-|-|-|  
|**Column name**|**Type**|**Description**|  
|*class*|**int**|1 = Object or column (includes modules, XPs, views, synonyms, and tables).<br /><br /> 4 = Database Principal<br /><br /> 5 = Assembly<br /><br /> 6 = Type<br /><br /> 7 = Index (Full-text Index)<br /><br /> 12 = Database DDL Trigger<br /><br /> 19 = Route<br /><br /> 30 = Audit Specification|  
|*class_desc*|**nvarchar(120)**|Description of class of the entity. One of the following to match the class:<br /><br /> **OBJECT_OR_COLUMN**<br /><br /> **DATABASE_PRINCIPAL**<br /><br /> **ASSEMBLY**<br /><br /> **TYPE**<br /><br /> **INDEX**<br /><br /> **DATABASE_DDL_TRIGGER**<br /><br /> **ROUTE**<br /><br /> **AUDIT_SPECIFICATION**|  
|*major_id*|**int**|ID of the entity.<br /><br /> If *class* = 1, then object_id<br /><br /> If *class* = 4, then sys.database_principals.principal_id.<br /><br /> If *class* = 5, then sys.assemblies.assembly_id.<br /><br /> If *class* = 6, then sys.types.user_type_id.<br /><br /> If *class* = 7, then sys.indexes.index_id.<br /><br /> If *class* = 12, then sys.triggers.object_id.<br /><br /> If *class* = 19, then sys.routes.route_id.<br /><br /> If *class* = 30, then sys. database_audit_specifications.databse_specification_id.|  
|*statement_line_number*|**int**|If the class is a module, returns the line number on which the uncontained use is located.  Otherwise the value is null.|  
|*statement_ offset_begin*|**int**|If the class is a module, indicates, in bytes, beginning with 0, the starting position where uncontained use begins. Otherwise the return value is null.|  
|*statement_ offset_end*|**int**|If the class is a module, indicates, in bytes, starting with 0, the ending position of the uncontained use. A value of -1 indicates the end of the module. Otherwise the return value is null.|  
|*statement_type*|**nvarchar(512)**|The type of statement.|  
|*feature_ name*|**nvarchar(256)**|Returns the external name of the object.|  
|*feature_type_name*|**nvarchar(256)**|Returns the type of feature.|  
  
## Remarks  
 sys.dm_db_uncontained_entities shows those entities which can potentially cross the database boundary. It will return any user entities that have the potential to use objects outside of the database.  
  
 The following feature types are reported.  
  
-   Unknown containment behavior (dynamic SQL or deferred name resolution)  
  
-   DBCC command  
  
-   System stored procedure  
  
-   System scalar function  
  
-   System table valued function  
  
-   System built-in function  
  
## Security  
  
### Permissions  
 sys.dm_db_uncontained_entities only returns objects for which the user has some type of permission. To fully evaluate the containment of the database this function should be used by a high privileged user such as a member of the **sysadmin** fixed server role or the **db_owner** role.  
  
## Examples  
 The following example creates a procedure named P1, and then queries `sys.dm_db_uncontained_entities`. The query reports that P1 uses **sys.endpoints** which is outside of the database.  
  
```sql  
CREATE DATABASE Test;  
GO  
  
USE Test;  
GO  
CREATE PROC P1  
AS   
SELECT * FROM sys.endpoints ;  
GO  
SELECT SO.name, UE.* FROM sys.dm_db_uncontained_entities AS UE  
LEFT JOIN sys.objects AS SO  
    ON UE.major_id = SO.object_id;  
```  
  
## See Also  
 [Contained Databases](../../relational-databases/databases/contained-databases.md)  
  
  
