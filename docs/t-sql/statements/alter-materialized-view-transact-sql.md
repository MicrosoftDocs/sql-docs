---
title: "ALTER MATERIALIZED VIEW (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "jrasnick"
ms.technology: data-warehouse
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_VIEW_TSQL"
  - "ALTER VIEW"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "indexed views [SQL Server], modifying"
  - "views [SQL Server], modifying"
  - "modifying views"
  - "ALTER VIEW statement"
author: XiaoyuL-Preview 
ms.author: xiaoyul
manager: craigg
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# ALTER MATERIALIZED VIEW (Transact-SQL)

[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

Modifies a previously created materialized view. ALTER VIEW does not affect dependent stored procedures or triggers and does not change permissions.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```
ALTER MATERIALIZED VIEW [ schema_name . ] view_name
{
      REBUILD | DISABLE
}
[;]
```  
  
## Arguments
 *schema_name*  
 Is the name of the schema to which the view belongs.  
  
 *view_name*  
 Is the view to change.  
  
> [!IMPORTANT]  
>  Column permissions are maintained only when columns have the same name before and after ALTER VIEW is performed.  
  
 ## Does any of the rest of this section apply?
 ENCRYPTION  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].  
  
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

If users want to suspend maintenance on the materialized view while maintaining metadata and permissions, they can disable the materialized view.  All queries against the materialized view while in a disabled state resolve against the underlying tables.  `Alter Materialized View REBUILD` resumes the materialized view.

##Does any of this section apply?
 For more information about ALTER VIEW, see Remarks in [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).  
  
> [!NOTE]  
>  If the previous view definition was created by using WITH ENCRYPTION or CHECK OPTION, these options are enabled only if they are included in ALTER VIEW.  
  
 If a view currently used is modified by using ALTER VIEW, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] takes an exclusive schema lock on the view. When the lock is granted, and there are no active users of the view, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] deletes all copies of the view from the procedure cache. Existing plans referencing the view remain in the cache but are recompiled when invoked.  
  
 ALTER VIEW can be applied to indexed views; however, ALTER VIEW unconditionally drops all indexes on the view.  
  
## Permissions

ALTER permission on the table or view is required.
  
## Examples

This example disables a materialized view and puts it in suspended mode.
  
```sql
ALTER MATERIALIZED VIEW My_Indexed_View DISABLE;  
```  
  
This example resumes materialized view by rebuilding it.  
  
```sql
ALTER MATERIALIZED VIEW My_Indexed_View REBUILD;  
```  
  
## See also

 [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [DROP VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/drop-view-transact-sql.md)   
 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md)  
