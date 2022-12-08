---
description: "sp_updateextendedproperty (Transact-SQL)"
title: "sp_updateextendedproperty (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/12/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_updateextendedproperty_TSQL"
  - "sp_updateextendedproperty"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_updateextendedproperty"
ms.assetid: 7f02360f-cb9e-48b4-b75f-29b4bc9ea304
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_updateextendedproperty (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Updates the value of an existing extended property.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_updateextendedproperty  
    [ @name = ]{ 'property_name' }   
    [ , [ @value = ]{ 'value' }  
        [, [ @level0type = ]{ 'level0_object_type' }  
         , [ @level0name = ]{ 'level0_object_name' }  
              [, [ @level1type = ]{ 'level1_object_type' }  
               , [ @level1name = ]{ 'level1_object_name' }  
                     [, [ @level2type = ]{ 'level2_object_type' }  
                      , [ @level2name = ]{ 'level2_object_name' }  
                     ]  
              ]  
        ]  
    ]  
```  
  
## Arguments  
 [ @name= ]{ '*property_name*'}  
 Is the name of the property to be updated. *property_name* is **sysname**, and cannot be NULL.  
  
 [ @value= ]{ '*value*'}  
 Is the value associated with the property. *value* is **sql_variant**, with a default of NULL. The size of *value* may not be more than 7,500 bytes.  
  
 [ @level0type= ]{ '*level0_object_type*'}  
 Is the user or user-defined type. *level0_object_type* is **varchar(128)**, with a default of NULL. Valid inputs are ASSEMBLY, CONTRACT, EVENT NOTIFICATION, FILEGROUP, MESSAGE TYPE, PARTITION FUNCTION, PARTITION SCHEME, PLAN GUIDE, REMOTE SERVICE BINDING, ROUTE, SCHEMA, SERVICE, USER, TRIGGER, TYPE, and NULL.  
  
> [!IMPORTANT]  
>  USER and TYPE as level-0 types will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features. Use SCHEMA as the level 0 type instead of USER. For TYPE, use SCHEMA as the level 0 type and TYPE as the level 1 type.  
  
 [ @level0name= ]{ '*level0_object_name*'}  
 Is the name of the level 1 object type specified. *level0_object_name* is **sysname** with a default of NULL.  
  
 [ @level1type= ]{ '*level1_object_type*'}  
 Is the type of level 1 object. *level1_object_type* is **varchar(128)** with a default of NULL. Valid inputs are AGGREGATE, DEFAULT, FUNCTION, LOGICAL FILE NAME, PROCEDURE, QUEUE, RULE, SYNONYM, TABLE, TABLE_TYPE, TYPE, VIEW, XML SCHEMA COLLECTION, and NULL.  
  
 [ @level1name= ]{ '*level1_object_name*'}  
 Is the name of the level 1 object type specified. *level1_object_name* is **sysname** with a default of NULL.  
  
 [ @level2type= ]{ '*level2_object_type*'}  
 Is the type of level 2 object. *level2_object_type* is **varchar(128)** with a default of NULL. Valid inputs are COLUMN, CONSTRAINT, EVENT NOTIFICATION, INDEX, PARAMETER, TRIGGER, and NULL.  
  
 [ @level2name= ]{ '*level2_object_name*'}  
 Is the name of the level 2 object type specified. *level2_object_name* is **sysname**, with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 For the purpose of specifying extended properties, the objects in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database are classified into three levels (0, 1, and 2). Level 0 is the highest level and is defined as objects contained at the database scope. Level 1 objects are contained in a schema or user scope, and level 2 objects are contained by level 1 objects. Extended properties can be defined for objects at any of these levels. References to an object in one level must be qualified with the names of the higher level objects that own or contain them.  
  
 Given a valid *property_name* and *value*, if all object types and names are null, the property updated belongs to the current database.  
  
## Permissions  
 Members of the db_owner and db_ddladmin fixed database roles may update the extended properties of any object with the following exception: db_ddladmin may not add properties to the database itself, or to users or roles.  
  
 Users may update extended properties to objects they own, or on which they have ALTER or CONTROL permissions.  
  
## Examples  
  
### A. Updating an extended property on a column  
 The following example updates the value of property `Caption` on column `ID` in table `T1`.  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE T1 (id int , name char (20));  
GO  
EXEC sp_addextendedproperty   
    @name = N'Caption'  
    ,@value = N'Employee ID'  
    ,@level0type = N'Schema', @level0name = dbo  
    ,@level1type = N'Table',  @level1name = T1  
    ,@level2type = N'Column', @level2name = id;  
GO  
--Update the extended property.  
EXEC sp_updateextendedproperty   
    @name = N'Caption'  
    ,@value = 'Employee ID must be unique.'  
    ,@level0type = N'Schema', @level0name = dbo  
    ,@level1type = N'Table',  @level1name = T1  
    ,@level2type = N'Column', @level2name = id;  
GO  
```  
  
### B. Updating an extended property on a database  
 The following example first creates an extended property on the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database and then updates the value of that property.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_addextendedproperty   
@name = N'NewCaption', @value = 'AdventureWorks2012 Sample OLTP Database';  
GO  
USE AdventureWorks2012;  
GO  
EXEC sp_updateextendedproperty   
@name = N'NewCaption', @value = 'AdventureWorks2012 Sample Database';  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sys.fn_listextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql.md)   
 [sp_addextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md)   
 [sp_dropextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropextendedproperty-transact-sql.md)   
 [sys.extended_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-properties-catalog-views-sys-extended-properties.md)  
  
  
