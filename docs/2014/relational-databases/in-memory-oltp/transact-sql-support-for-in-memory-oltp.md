---
title: "Transact-SQL Support for In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: b1cc7c30-1747-4c21-88ac-e95a5e58baac
author: MightyPen
ms.author: genemi
manager: craigg
---
# Transact-SQL Support for In-Memory OLTP
  You can access memory-optimized tables using any Transact-SQL query or DML statement (SELECT, INSERT, UPDATE, or DELETE), ad hoc statement, and SQL module such as stored procedures, table-value functions, scalar functions, triggers, and views. For more information see [Accessing Memory-Optimized Tables Using Interpreted Transact-SQL](accessing-memory-optimized-tables-using-interpreted-transact-sql.md).  
  
 Stored procedures that only reference memory-optimized tables can be natively compiled into machine code and typically provide significant performance gains over interpreted (disk-based) stored procedures. For optimized access to memory-optimized tables use natively compiled stored procedures. For more information, see [Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md).  
  
 When creating and modifying database objects (DDL statements), the following statements have been modified:  
  
-   [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-file-and-filegroup-options) (see `MEMORY_OPTIMIZED_DATA`)  
  
-   [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql) (see `MEMORY_OPTIMIZED_DATA`)  
  
-   [CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql) (see `NATIVE_COMPILATION`, `SCHEMABINDING`, `EXECUTE AS`, and `BEGIN ATOMIC`)  
  
-   [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql) (see `MEMORY_OPTIMIZED`, `DURABILITY`, `BUCKET_COUNT`, `INDEX`, and `HASH`)  
  
-   [CREATE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-type-transact-sql) (see `MEMORY_OPTIMIZED`, `BUCKET_COUNT`, `INDEX`, and `HASH`)  
  
-   [DECLARE @local_variable &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/declare-local-variable-transact-sql) (see `NULL` | `NOT NULL`)  
  
 Memory-optimized tables support `PRIMARY KEY` and `NOT NULL` constraints. For information on implementing unsupported constraints, see [Migrating Check and Foreign Key Constraints](../../database-engine/migrating-check-and-foreign-key-constraints.md).  
  
 For information on unsupported features, see [Transact-SQL Constructs Not Supported by In-Memory OLTP](transact-sql-constructs-not-supported-by-in-memory-oltp.md).  
  
## In This Section  
  
-   [Supported Data Types](supported-data-types-for-in-memory-oltp.md)  
  
-   [Accessing Memory-Optimized Tables Using Interpreted Transact-SQL](accessing-memory-optimized-tables-using-interpreted-transact-sql.md)  
  
-   [System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP](../../database-engine/system-views-stored-procedures-dmvs-and-wait-types-for-in-memory-oltp.md)  
  
## See Also  
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](in-memory-oltp-in-memory-optimization.md)   
 [Migration Issues for Natively Compiled Stored Procedures](migration-issues-for-natively-compiled-stored-procedures.md)   
 [Supported SQL Server Features](unsupported-sql-server-features-for-in-memory-oltp.md)   
 [Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md)  
  
  
