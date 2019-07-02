---
title: "ALTER MATERIALIZED VIEW (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: jrasnick
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
# ALTER MATERIALIZED VIEW (Transact-SQL) (preview)

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
 Is the materialized view to change.  
  
*REBUILD*   
Resumes the materialized view.

*DISABLE*   
Suspends maintenance on the materialized view while maintaining metadata and permissions.  All queries against the materialized view while in a disabled state resolve against the underlying tables.
  
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
