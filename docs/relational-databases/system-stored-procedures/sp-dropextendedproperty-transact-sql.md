---
title: "sp_dropextendedproperty (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dropextendedproperty_TSQL"
  - "sp_dropextendedproperty"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dropextendedproperty"
ms.assetid: 4851865a-86ca-4823-991a-182dd1934075
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_dropextendedproperty (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops an existing extended property.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropextendedproperty   
    [ @name = ] { 'property_name' }  
      [ , [ @level0type = ] { 'level0_object_type' }   
        , [ @level0name = ] { 'level0_object_name' }   
            [ , [ @level1type = ] { 'level1_object_type' }   
              , [ @level1name = ] { 'level1_object_name' }   
                [ , [ @level2type = ] { 'level2_object_type' }   
                  , [ @level2name = ] { 'level2_object_name' }   
                ]   
            ]   
        ]   
    ]   
```  
  
## Arguments  
 [ @name= ]{ '*property_name*'}  
 Is the name of the property to be dropped. *property_name* is **sysname** and cannot be NULL.  
  
 [ @level0type= ]{ '*level0_object_type*'}  
 Is the name of the level 0 object type specified. *level0_object_type* is **varchar(128)**, with a default of NULL.  
  
 Valid inputs are ASSEMBLY, CONTRACT, EVENT NOTIFICATION, FILEGROUP, MESSAGE TYPE, PARTITION FUNCTION, PARTITION SCHEME, REMOTE SERVICE BINDING, ROUTE, SCHEMA, SERVICE, USER, TRIGGER, TYPE, and NULL.  
  
> [!IMPORTANT]  
>  USER and TYPE as level-0 types will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features. Use SCHEMA as the level 0 type instead of USER. For TYPE, use SCHEMA as the level 0 type and TYPE as the level 1 type.  
  
 [ @level0name= ]{ '*level0_object_name*'}  
 Is the name of the level 0 object type specified. *level0_object_name* is **sysname** with a default of NULL.  
  
 [ @level1type= ]{ '*level1_object_type*'}  
 Is the type of level 1 object. *level1_object_type* is **varchar(128)** with a default of NULL. Valid inputs are AGGREGATE, DEFAULT, FUNCTION, LOGICAL FILE NAME, PROCEDURE, QUEUE, RULE, SYNONYM, TABLE, TABLE_TYPE, TYPE, VIEW, XML SCHEMA COLLECTION, and NULL.  
  
 [ @level1name= ]{ '*level1_object_name*'}  
 Is the name of the level 1 object type specified. *level1_object_name* is **sysname** with a default of NULL.  
  
 [ @level2type= ]{ '*level2_object_type*'}  
 Is the type of level 2 object. *level2_object_type* is **varchar(128)** with a default of NULL. Valid inputs are COLUMN, CONSTRAINT, EVENT NOTIFICATION, INDEX, PARAMETER, TRIGGER, and NULL.  
  
 [ @level2name= ]{ '*level2_object_name*'}  
 Is the name of the level 2 object type specified. *level2_object_name* is **sysname** with a default of NULL.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Remarks  
 For the purpose of specifying extended properties, the objects in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database are classified into three levels: 0, 1, and 2. Level 0 is the highest level and is defined as objects contained at the database scope. Level 1 objects are contained in a schema or user scope, and level 2 objects are contained by level 1 objects. Extended properties can be defined for objects at any of these levels. References to an object in one level must be qualified with the types and names of all higher level objects.  
  
 Given a valid *property_name*, if all object types and names are null and a property exists on the current database, that property is deleted. See example B that follows later in this topic.  
  
## Permissions  
 Members of the db_owner and db_ddladmin fixed database roles may drop extended properties of any object with the following exception: db_ddladmin may not add properties to the database itself, or to users or roles.  
  
 Users may drop extended properties to objects they own or on which they have ALTER or CONTROL permissions.  
  
## Examples  
  
### A. Dropping an extended property on a column  
 The following example removes the property `caption` from column `id` in table `T1` contained in the schema `dbo`.  
  
```  
CREATE TABLE T1 (id int , name char (20));  
GO  
EXEC sp_addextendedproperty   
     @name = 'caption'   
    ,@value = 'Employee ID'   
    ,@level0type = 'schema'   
    ,@level0name = dbo  
    ,@level1type = 'table'  
    ,@level1name = 'T1'  
    ,@level2type = 'column'  
    ,@level2name = id;  
GO  
EXEC sp_dropextendedproperty   
     @name = 'caption'   
    ,@level0type = 'schema'   
    ,@level0name = dbo  
    ,@level1type = 'table'  
    ,@level1name = 'T1'  
    ,@level2type = 'column'  
    ,@level2name = id;  
GO  
DROP TABLE T1;  
GO  
```  
  
### B. Dropping an extended property on a database  
 The following example removes the property named `MS_Description` from the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. Because the property is on the database itself, no object types and names are specified.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_dropextendedproperty   
@name = N'MS_Description';  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [sys.fn_listextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql.md)   
 [sp_addextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md)   
 [sp_updateextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-updateextendedproperty-transact-sql.md)   
 [sys.extended_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-properties-catalog-views-sys-extended-properties.md)  
  
  
