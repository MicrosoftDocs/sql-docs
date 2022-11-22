---
description: "sys.sp_xtp_bind_db_resource_pool (Transact-SQL)"
title: "sys.sp_xtp_bind_db_resource_pool (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_xtp_bind_db_resource_pool_TSQL"
  - "sp_xtp_bind_db_resource_pool"
  - "sys.sp_xtp_bind_db_resource_pool_TSQL"
  - "sys.sp_xtp_bind_db_resource_pool"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_xtp_bind_db_resource_pool"
  - "sys.sp_xtp_bind_db_resource_pool"
ms.assetid: c2a78073-626b-4159-996e-1808f6bfb6d2
author: markingmyname
ms.author: maghan
---
# sys.sp_xtp_bind_db_resource_pool (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Binds the specified [!INCLUDE[inmemory](../../includes/inmemory-md.md)] database to the specified resource pool. Both the database and the resource pool must exist prior to executing `sys.sp_xtp_bind_db_resource_pool`.  
  
 This system procedure creates a binding between the Resource Governor pool identified by resource_pool_name, and the database identified by database_name. It is not required that the database has any memory-optimized objects at the time of binding. In the absence of memory-optimized objects, there is no memory taken from the resource pool. This binding will be used by Resource Governor to manage memory allocated by [!INCLUDE[inmemory](../../includes/inmemory-md.md)] allocators as described below.  
  
 If there is already a binding in place for a given database, the procedure returns an error.  In no event may a database have more than one active binding.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
  
## Syntax  
  
```sql  
sys.sp_xtp_bind_db_resource_pool 'database_name', 'resource_pool_name'  
```  
  
## Arguments  
 database_name  
 The name of an existing [!INCLUDE[inmemory](../../includes/inmemory-md.md)] enabled database.  
  
 resource_pool_name  
 The name of an existing resource pool.  
  
## Messages  
 When an error occurs `sp_xtp_bind_db_resource_pool` returns one of these messages.  
  
 **Database does not exist**  
 Database_name must refer to an existing database. If there is no database with the specified ID, the following message is returned:   
*Database ID %d does not exist.  Please use a valid database ID for this binding.*  
  
```  
Msg 911, Level 16, State 18, Procedure sp_xtp_bind_db_resource_pool_internal, Line 51  
Database 'Hekaton_DB213' does not exist. Make sure that the name is entered correctly.  
```  
  
**Database is a system database**  
 [!INCLUDE[inmemory](../../includes/inmemory-md.md)] tables cannot be created in system databases.  Thus it is invalid to create a binding of [!INCLUDE[inmemory](../../includes/inmemory-md.md)] memory for such a database.  The following error is returned:  
*Database_name %s refers to a system database.  Resource pools may only be bound to a user database.*  
  
```  
Msg 41371, Level 16, State 1, Procedure sp_xtp_bind_db_resource_pool_internal, Line 51  
Binding to a resource pool is not supported for system database 'master'. This operation can only be performed on a user database.  
```  
  
**Resource Pool does not exist**  
 The resource pool identified by resource_pool_name must exist prior to executing `sp_xtp_bind_db_resource_pool`.  If there is no pool with the specified ID, the following error is returned:  
*Resource Pool %s does not exist.  Please enter a valid resource pool name.*  
  
```  
Msg 41370, Level 16, State 1, Procedure sp_xtp_bind_db_resource_pool_internal, Line 51  
Resource pool 'Pool_Hekaton' does not exist or resource governor has not been reconfigured.  
```  
  
**Pool_name refers to a reserved system pool**  
 The pool names "INTERNAL" and "DEFAULT" are reserved for system pools.  It is not valid to explicitly bind a database to either of these.  If a system pool name is entered, the following error is returned:  
*Resource Pool %s is a system resource pool.  System resource pools may not be explicitly bound to a database using this procedure.*  
  
```  
Msg 41373, Level 16, State 1, Procedure sp_xtp_bind_db_resource_pool_internal, Line 51  
Database 'Hekaton_DB' cannot be explicitly bound to the resource pool 'internal'. A database can only be bound only to a user resource pool.  
```  
  
**Database is already bound to another Resource Pool**  
 A database can be bound to only one resource pool at any time. Database bindings to resource pools must be explicitly removed before they can be bound to another pool. See [sys.sp_xtp_unbind_db_resource_pool &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-xtp-unbind-db-resource-pool-transact-sql.md).  
*Database %s is already bound to resource pool %s.  You must unbind before you can create a new binding.*  
  
```  
Msg 41372, Level 16, State 1, Procedure sp_xtp_bind_db_resource_pool_internal, Line 54  
Database 'Hekaton_DB' is currently bound to a resource pool. A database must be unbound before creating a new binding.  
```  
  
 When successful, `sp_xtp_bind_db_resource_pool` returns the following message.  
  
**Successful Binding**  
 When successful, the function returns the following success message, which is logged in the SQL ERRORLOG  
*A resource binding has been successfully created between the database with ID %d and the resource pool with ID %d.*  
  
## Examples  
A.  The following code example binds the database Hekaton_DB to the resource pool Pool_Hekaton.  
  
```sql  
sys.sp_xtp_bind_db_resource_pool N'Hekaton_DB', N'Pool_Hekaton'  
```  
 
 The binding takes effect the next time the database is brought online.  
 
 B. Expanded example of above example which includes some basic checks.  Execute the following [!INCLUDE[tsql](../../includes/tsql-md.md)] in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]\:
 
```sql
DECLARE @resourcePool sysname = N'Pool_Hekaton';
DECLARE @database sysname = N'Hekaton_DB';

-- Check whether resource pool exists
IF NOT EXISTS (
	SELECT * FROM sys.resource_governor_resource_pools WHERE name = @resourcePool
	)
BEGIN
	SELECT N'Resource pool "' + @resourcePool + N'" does not exist or resource governor has not been reconfigured.';
END
-- Check whether database is already bound to a resource pool
ELSE IF EXISTS (
	SELECT p.name
	FROM sys.databases d
	JOIN sys.resource_governor_resource_pools p
	ON d.resource_pool_id = p.pool_id
	WHERE d.name = @database
	)
BEGIN
	SELECT N'Database "' + @database + N'" is currently bound to resource pool "' + @resourcePool  + N'". A database must be unbound before creating a new binding.';
END
-- Bind resource pool to database.
ELSE BEGIN
	EXEC sp_xtp_bind_db_resource_pool @database, @resourcePool; 
END 
``` 
  
## Requirements  
  
-   Both the database specified by `database_name` and the resource pool specified by `resource_pool_name` must exist prior to binding them.  
  
-   Requires CONTROL SERVER permission.  
  
## See Also  
 [Bind a Database with Memory-Optimized Tables to a Resource Pool](../../relational-databases/in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md)   
 [sys.sp_xtp_unbind_db_resource_pool &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-xtp-unbind-db-resource-pool-transact-sql.md)  
  
  
