---
title: "ALTER VIEW (Transact-SQL)"
description: ALTER VIEW (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/25/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_VIEW_TSQL"
  - "ALTER VIEW"
helpviewer_keywords:
  - "indexed views [SQL Server], modifying"
  - "views [SQL Server], modifying"
  - "modifying views"
  - "ALTER VIEW statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER VIEW (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Modifies a previously created view. This includes an indexed view. ALTER VIEW does not affect dependent stored procedures or triggers and does not change permissions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER VIEW [ schema_name . ] view_name [ ( column [ ,...n ] ) ]   
[ WITH <view_attribute> [ ,...n ] ]   
AS select_statement   
[ WITH CHECK OPTION ] [ ; ]  
  
<view_attribute> ::=   
{   
    [ ENCRYPTION ]  
    [ SCHEMABINDING ]  
    [ VIEW_METADATA ]       
}   
```

```syntaxsql
-- Syntax for Azure Synapse Analytics and Parallel Data Warehouse  
  
ALTER VIEW [ schema_name . ] view_name [  ( column_name [ ,...n ] ) ]   
AS <select_statement>   
[;]  
``` 
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *schema_name*  
 Is the name of the schema to which the view belongs.  
  
 *view_name*  
 Is the view to change.  
  
 *column*  
 Is the name of one or more columns, separated by commas, that are to be part of the specified view.  
  
> [!IMPORTANT]  
>  Column permissions are maintained only when columns have the same name before and after ALTER VIEW is performed.  
  
> [!NOTE]  
>  In the columns for the view, the permissions for a column name apply across a CREATE VIEW or ALTER VIEW statement, regardless of the source of the underlying data. For example, if permissions are granted on the **SalesOrderID** column in a CREATE VIEW statement, an ALTER VIEW statement can rename the **SalesOrderID** column, such as to **OrderRef**, and still have the permissions associated with the view using **SalesOrderID**.  
  
 ENCRYPTION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
 Encrypts the entries in [sys.syscomments](../../relational-databases/system-compatibility-views/sys-syscomments-transact-sql.md) that contain the text of the ALTER VIEW statement. WITH ENCRYPTION prevents the view from being published as part of SQL Server replication.  
  
 SCHEMABINDING  
 Binds the view to the schema of the underlying table or tables. When SCHEMABINDING is specified, the base tables cannot be modified in a way that would affect the view definition. The view definition itself must first be modified or dropped to remove dependencies on the table to be modified. When you use SCHEMABINDING, the _select\_statement_ must include the two-part names (_schema_**.**_object_) of tables, views, or user-defined functions that are referenced. All referenced objects must be in the same database.  
  
 Views or tables that participate in a view created with the SCHEMABINDING clause cannot be dropped, unless that view is dropped or changed so that it no longer has schema binding. Otherwise, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] raises an error. Also, executing ALTER TABLE statements on tables that participate in views that have schema binding fail if these statements affect the view definition.  
  
 VIEW_METADATA  
 Specifies that the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will return to the DB-Library, ODBC, and OLE DB APIs the metadata information about the view, instead of the base table or tables, when browse-mode metadata is being requested for a query that references the view. Browse-mode metadata is additional metadata that the instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] returns to the client-side DB-Library, ODBC, and OLE DB APIs. This metadata enables the client-side APIs to implement updatable client-side cursors. Browse-mode metadata includes information about the base table that the columns in the result set belong to.  
  
 For views created with VIEW_METADATA, the browse-mode metadata returns the view name and not the base table names when it describes columns from the view in the result set.  
  
 When a view is created by using WITH VIEW_METADATA, all its columns, except a **timestamp** column, are updatable if the view has INSERT or UPDATE INSTEAD OF triggers. For more information, see the Remarks section in [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).  
  
 AS  
 Are the actions the view is to take.  
  
 *select_statement*  
 Is the SELECT statement that defines the view.  
  
 WITH CHECK OPTION  
 Forces all data modification statements that are executed against the view to follow the criteria set within *select_statement*.  
  
## Remarks  
 For more information about ALTER VIEW, see Remarks in [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).  
  
> [!NOTE]  
>  If the previous view definition was created by using WITH ENCRYPTION or CHECK OPTION, these options are enabled only if they are included in ALTER VIEW.  
  
 If a view currently used is modified by using ALTER VIEW, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] takes an exclusive schema lock on the view. When the lock is granted, and there are no active users of the view, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] deletes all copies of the view from the procedure cache. Existing plans referencing the view remain in the cache but are recompiled when invoked.  
  
 ALTER VIEW can be applied to indexed views; however, ALTER VIEW unconditionally drops all indexes on the view.  
  
## Permissions  
 To execute ALTER VIEW, at a minimum, ALTER permission on OBJECT is required.  
  
## Examples  
 The following example creates a view that contains all employees and their hire dates called `EmployeeHireDate`. Permissions are granted to the view, but requirements are changed to select employees whose hire dates fall before a certain date. Then, `ALTER VIEW` is used to replace the view.  
  
```sql 
USE AdventureWorks2012 ;  
GO  
CREATE VIEW HumanResources.EmployeeHireDate  
AS  
SELECT p.FirstName, p.LastName, e.HireDate  
FROM HumanResources.Employee AS e JOIN Person.Person AS  p  
ON e.BusinessEntityID = p.BusinessEntityID ;  
GO  
```  
  
 The view must be changed to include only the employees that were hired before `2002`. If ALTER VIEW is not used, but instead the view is dropped and re-created, the previously used GRANT statement and any other statements that deal with permissions pertaining to this view must be re-entered.  
  
```sql  
ALTER VIEW HumanResources.EmployeeHireDate  
AS  
SELECT p.FirstName, p.LastName, e.HireDate  
FROM HumanResources.Employee AS e JOIN Person.Person AS p  
ON e.BusinessEntityID = p.BusinessEntityID  
WHERE HireDate < CONVERT(DATETIME,'20020101',101) ;  
GO  
```  
  
## See Also  
 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [DROP VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/drop-view-transact-sql.md)   
 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md)  
  
  
