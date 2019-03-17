---
title: "Synonyms (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "conceptual"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "synonyms [SQL Server], about synonyms"
ms.assetid: 6210e1d5-075f-47e4-ac8d-f84bcf26fbc0
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Synonyms (Database Engine)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  A synonym is a database object that serves the following purposes:  
  
-   Provides an alternative name for another database object, referred to as the base object, that can exist on a local or remote server.  
  
-   Provides a layer of abstraction that protects a client application from changes made to the name or location of the base object.  
  
For example, consider the **Employee** table of [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)], located on a server named **Server1**. To reference this table from another server, **Server2**, a client application would have to use the four-part name **Server1.AdventureWorks.Person.Employee**. Also, if the location of the table were to change, for example, to another server, the client application would have to be modified to reflect that change.  
  
To address both these issues, you can create a synonym, **EmpTable**, on **Server2** for the **Employee** table on **Server1**. Now, the client application only has to use the single-part name, **EmpTable**, to reference the **Employee** table. Also, if the location of the **Employee** table changes, you will have to modify the synonym, **EmpTable**, to point to the new location of the **Employee** table. Because there is no ALTER SYNONYM statement, you first have to drop the synonym, **EmpTable**, and then re-create the synonym with the same name, but point the synonym to the new location of **Employee**.  
  
A synonym belongs to a schema, and like other objects in a schema, the name of a synonym must be unique. You can create synonyms for the following database objects:  
  
|||  
|-|-|  
|Assembly (CLR) stored procedure|Assembly (CLR) table-valued function|  
|Assembly (CLR) scalar function|Assembly (CLR) aggregate functions|  
|Replication-filter-procedure|Extended stored procedure|  
|SQL scalar function|SQL table-valued function|  
|SQL inline-tabled-valued function|SQL stored procedure|  
|View|Table* (User-defined)|  
  
 *Includes local and global temporary tables  
  
> [!NOTE]  
> Four-part names for function base objects are not supported.  
  
A synonym cannot be the base object for another synonym, and a synonym cannot reference a user-defined aggregate function.  
  
The binding between a synonym and its base object is by name only. All existence, type, and permissions checking on the base object is deferred until run time. Therefore, the base object can be modified, dropped, or dropped and replaced by another object that has the same name as the original base object. For example, consider a synonym, **MyContacts**, that references the **Person.Contact** table in [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)]. If the **Contact** table is dropped and replaced by a view named **Person.Contact**, **MyContacts** now references the **Person.Contact** view.  
  
References to synonyms are not schema-bound. Therefore, a synonym can be dropped at any time. However, by dropping a synonym, you run the risk of leaving dangling references to the synonym that was dropped. These references will only be found at run time.  
  
## Synonyms and Schemas  
If you have a default schema that you do not own and want to create a synonym, you must qualify the synonym name with the name of a schema that you do own. For example, if you own a schema **x**, but **y** is your default schema and you use the CREATE SYNONYM statement, you must prefix the name of the synonym with the schema **x**, instead of naming the synonym by using a single-part name. For more information about how to create synonyms, see [CREATE SYNONYM &#40;Transact-SQL&#41;](../../t-sql/statements/create-synonym-transact-sql.md).  
  
## Granting Permissions on a Synonym  
Only synonym owners, members of **db_owner**, or members of **db_ddladmin** can grant permission on a synonym.  
  
You can `GRANT`, `DENY`, and `REVOKE` all or any of the following permissions on a synonym:  
  
|||  
|-|-|  
|CONTROL|DELETE|  
|EXECUTE|INSERT|  
|SELECT|TAKE OWNERSHIP|  
|UPDATE|VIEW DEFINITION|  
  
## Using Synonyms  
 You can use synonyms in place of their referenced base object in several SQL statements and expression contexts. The following table contains a list of these statements and expression contexts:  
  
|||  
|-|-|  
|SELECT|INSERT|  
|UPDATE|DELETE|  
|EXECUTE|Sub-selects|  
  
 When you are working with synonyms in the contexts previously stated, the base object is affected. For example, if a synonym references a base object that is a table and you insert a row into the synonym, you are actually inserting a row into the referenced table.  
  
> [!NOTE]  
> You cannot reference a synonym that is located on a linked server.  
  
 You can use a synonym as the parameter for the OBJECT_ID function; however, the function returns the object ID of the synonym, not the base object.  
  
 You cannot reference a synonym in a DDL statement. For example, the following statements, which reference a synonym named `dbo.MyProduct`, generate errors:  
  
```sql  
ALTER TABLE dbo.MyProduct  
   ADD NewFlag int null;  
EXEC ('ALTER TABLE dbo.MyProduct  
   ADD NewFlag int null');  
```  
  
The following permission statements are associated only with the synonym and not the base object:  
  
|||  
|-|-|  
|GRANT|DENY|  
|REVOKE||  
  
Synonyms are not schema-bound and, therefore, cannot be referenced by the following schema-bound expression contexts:  
  
|||  
|-|-|  
|CHECK constraints|Computed columns|  
|Default expressions|Rule expressions|  
|Schema-bound views|Schema-bound functions|  
  
For more information about schema-bound functions, see [Create User-defined Functions &#40;Database Engine&#41;](../../relational-databases/user-defined-functions/create-user-defined-functions-database-engine.md).  
  
## Getting Information About Synonyms  
The `sys.synonyms` catalog view contains an entry for each synonym in a given database. This catalog view exposes synonym metadata such as the name of the synonym and the name of the base object. For more information, see [sys.synonyms &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-synonyms-transact-sql.md).  
  
By using extended properties, you can add descriptive or instructional text, input masks, and formatting rules as properties of a synonym. Because the property is stored in the database, all applications that read the property can evaluate the object in the same way. For more information, see [sp_addextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md).  
  
To find the base type of the base object of a synonym, use the OBJECTPROPERTYEX function. For more information, see [OBJECTPROPERTYEX &#40;Transact-SQL&#41;](../../t-sql/functions/objectpropertyex-transact-sql.md).  
  
### Examples  
The following example returns the base type of a synonym's base object that is a local object.  
  
```sql  
USE tempdb;  
GO  
CREATE SYNONYM MyEmployee   
FOR AdventureWorks2012.HumanResources.Employee;  
GO  
SELECT OBJECTPROPERTYEX(OBJECT_ID('MyEmployee'), 'BaseType') AS BaseType;  
```  
  
The following example returns the base type of a synonym's base object that is a remote object located on a server named `Server1`.  
  
```sql  
EXECUTE sp_addlinkedserver Server1;  
GO  
CREATE SYNONYM MyRemoteEmployee  
FOR Server1.AdventureWorks2012.HumanResources.Employee;  
GO  
SELECT OBJECTPROPERTYEX(OBJECT_ID('MyRemoteEmployee'), 'BaseType') AS BaseType;  
GO  
```  
  
## Related Content  
 [Create Synonyms](../../relational-databases/synonyms/create-synonyms.md)    
 [CREATE SYNONYM &#40;Transact-SQL&#41;](../../t-sql/statements/create-synonym-transact-sql.md)    
 [DROP SYNONYM &#40;Transact-SQL&#41;](../../t-sql/statements/drop-synonym-transact-sql.md)    
  
