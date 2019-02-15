---
title: "RENAME (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/21/2018"
ms.service: sql-data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
ms.assetid: 0907cfd9-33a6-4fa6-91da-7d6679fee878
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# RENAME (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Renames a user-created table in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)]. Renames a user-created table or database in [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
> [!NOTE]  
>  To rename a database in [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], use [ALTER DATABASE (Azure SQL Data Warehouse](alter-database-transact-sql.md?&tabs=sqldw).  To rename a database in Azure SQL Database, use the [ALTER DATABASE (Azure SQL Database)](alter-database-transact-sql.md?&tabs=sqldbmi) statement. To rename a database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the stored procedure [sp_renamedb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-renamedb-transact-sql.md).
  
## Syntax  
  
```  
-- Syntax for Azure SQL Data Warehouse  
  
-- Rename a table.  
RENAME OBJECT [::] [ [ database_name .  [schema_name ] ] . ] | [schema_name . ] ] table_name TO new_table_name  
[;]  
  
```  
  
```  
-- Syntax for Parallel Data Warehouse  
  
-- Rename a table  
RENAME OBJECT [::] [ [ database_name . [ schema_name ] . ] | [ schema_name . ] ] table_name TO new_table_name  
[;]  
  
-- Rename a database  
RENAME DATABASE [::] database_name TO new_database_name  
[;]  
```  
  
## Arguments  
 RENAME OBJECT [::] [ [*database_name* . [ *schema_name* ] . ] | [ *schema_name* . ] ]*table_name* TO *new_table_name*  
 **APPLIES TO:**  [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
 Change the name of a user-defined table. Specify the table to be renamed with a one-, two-, or three-part name.    Specify the new table *new_table_name* as a one-part name.  
  
 RENAME DATABASE [::] [ *database_name* TO *new_database_name*  
 **APPLIES TO:**  [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
 Change the name of a user-defined database from *database_name* to *new_database_name*.  You can't rename a database to any of these [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]reserved database names:  
  
-   master  
  
-   model  
  
-   msdb  
  
-   tempdb  
  
-   pdwtempdb1  
  
-   pdwtempdb2  
  
-   DWConfiguration  
  
-   DWDiagnostics  
  
-   DWQueue  
  
## Permissions  
 To run this command, you need this permission:  
  
-   **ALTER** permission on the table  
   
  
## Limitations and Restrictions  
  
### Cannot rename an external table, indexes, or views
You can't rename an external table, indexes, or views. Instead of renaming, you can drop the external table, index, or view and then re-create it with the new name.

### Cannot rename a table in use  
 You can't rename a table or database while it is in use. Renaming a table requires an exclusive lock on the table. If the table is in use, you may need to terminate sessions that are using the table. To terminate a session, you can use the KILL command. Use KILL cautiously since when a session is terminated any uncommitted work will be rolled back. Sessions in SQL Data Warehouse are prefixed by 'SID'. Include 'SID' and the session number when invoking the KILL command. This example views a list of active or idle sessions and then terminates session 'SID1234'.  
  
### Views are not updated  
 When renaming a database, all views that use the former database name will become invalid. This behavior applies to views both inside and outside the database. For example, if the Sales database is renamed, a view that contains `SELECT * FROM Sales.dbo.table1` will become invalid. To resolve this issue, you can either avoid using three-part names in views, or update the views to reference the new database name.  
  
 When renaming a table, views are not updated to reference the new table name. Each view, inside or outside of the database, that references the former table name will become invalid. To resolve this issue, you can update each view to reference the new table name.  
  
## Locking  
 Renaming a table takes a shared lock on the DATABASE object, a shared lock on the SCHEMA object, and an exclusive lock on the table.  
  
## Examples  
  
### A. Rename a database  
 **APPLIES TO:**  [!INCLUDE[ssPDW](../../includes/sspdw-md.md)] only  
  
 This example renames the user-defined database AdWorks to AdWorks2.  
  
```  
-- Rename the user defined database AdWorks  
RENAME DATABASE AdWorks to AdWorks2;  
  
```  
  
 When renaming a table, all objects and properties associated with the table are updated to reference the new table name. For example, table definitions, indexes, constraints, and permissions are updated. Views are not updated.  
  
### B. Rename a table  
 **APPLIES TO:**  [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
 This example renames the Customer table to Customer1.  
  
```  
-- Rename the customer table  
RENAME OBJECT Customer TO Customer1;  
  
RENAME OBJECT mydb.dbo.Customer TO Customer1;  
```  
  
 When renaming a table, all objects and properties associated with the table are updated to reference the new table name. For example, table definitions, indexes, constraints, and permissions are updated. Views are not updated.  
   
  
### C. Move a table to a different schema  
 **APPLIES TO:**  [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
 If your intent is to move the object to a different schema, use [ALTER SCHEMA &#40;Transact-SQL&#41;](../../t-sql/statements/alter-schema-transact-sql.md). For example, the following statement moves the table item from the product schema to the dbo schema.  
  
```  
ALTER SCHEMA dbo TRANSFER OBJECT::product.item;  
```  
  
### D. Terminate sessions before renaming a table  
 **APPLIES TO:**  [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
 It is important to remember that you can't rename a table while it is in use. A rename of a table requires an exclusive lock on the table. If the table is in use, you may need to terminate the session using the table. To terminate a session, you can use the KILL command. Use KILL cautiously since when a session is terminated any uncommitted work will be rolled back. Sessions in SQL Data Warehouse are prefixed by 'SID'. You will need to include 'SID' and the session number when invoking the KILL command. This example views a list of active or idle sessions and then terminates session 'SID1234'.  
  
```  
-- View a list of the current sessions  
SELECT session_id, login_name, status   
FROM sys.dm_pdw_exec_sessions   
WHERE status='Active' OR status='Idle';  
  
-- Terminate a session using the session_id.  
KILL 'SID1234';  
```  
  
  
