---
title: "Atomic Blocks | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 40e0e749-260c-4cfc-a848-444d30c09d85
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Atomic Blocks
  `BEGIN ATOMIC` is part of the ANSI SQL standard. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports atomic blocks only at the top-level of natively compiled stored procedures.  
  
-   Every natively compiled stored procedure contains exactly one block of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. This is an ATOMIC block.  
  
-   Non-native, interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures and ad hoc batches do not support atomic blocks.  
  
 Atomic blocks are executed (atomically) within the transaction. Either all statements in the block succeed or the entire block will be rolled back to the savepoint that was created at the start of the block. In addition, the session settings are fixed for the atomic block. Executing the same atomic block in sessions with different settings will result in the same behavior, independent of the settings of the current session.  
  
## Transactions and Error Handling  
 If a transaction already exists on a session (because a batch executed a `BEGIN TRANSACTION` statement and the transaction remains active), then starting an atomic block will create a savepoint in the transaction. If the block exits without an exception, the savepoint that was created for the block commits, but the transaction will not commit until the transaction at the session level commits. If the block throws an exception, the effects of the block are rolled back but the transaction at the session level will proceed, unless the exception is transaction-dooming. For example a write conflict is transaction-dooming, but not a type casting error.  
  
 If there is no active transaction on a session, `BEGIN ATOMIC` will start a new transaction. If no exception is thrown outside the scope of the block, the transaction will be committed at the end of the block. If the block throws an exception (that is, the exception is not caught and handled within the block), the transaction will be rolled back. For transactions that span a single atomic block (a single natively compiled stored procedure), you do not need to write explicit `BEGIN TRANSACTION` and `COMMIT` or `ROLLBACK` statements.  
  
 Natively compiled stored procedures support the `TRY`, `CATCH`, and `THROW` constructs for error handling. `RAISERROR` is not supported.  
  
 The following example illustrates the error handling behavior with atomic blocks and natively compiled stored procedures:  
  
```sql  
-- sample table  
CREATE TABLE dbo.t1 (  
  c1 int not null primary key nonclustered  
)  
WITH (MEMORY_OPTIMIZED=ON)  
GO  
  
-- sample proc that inserts 2 rows  
CREATE PROCEDURE dbo.usp_t1 @v1 bigint not null, @v2 bigint not null  
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
AS  
BEGIN ATOMIC  
WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english', DELAYED_DURABILITY = ON)  
  
  INSERT dbo.t1 VALUES (@v1)  
  INSERT dbo.t1 VALUES (@v2)  
  
END  
GO  
  
-- insert two rows  
EXEC dbo.usp_t1 1, 2  
GO  
  
-- verify we have no active transaction  
SELECT @@TRANCOUNT  
GO  
  
-- verify the rows 1 and 2 were committed  
SELECT c1 FROM dbo.t1  
GO  
  
-- execute proc with arithmetic overflow  
EXEC dbo.usp_t1 3, 4444444444444  
GO  
-- expected error message:  
-- Msg 8115, Level 16, State 0, Procedure usp_t1  
-- Arithmetic overflow error converting bigint to data type int.  
  
-- verify we have no active transaction  
SELECT @@TRANCOUNT  
GO  
  
-- verify rows 3 was not committed; usp_t1 has been rolled back  
SELECT c1 FROM dbo.t1  
GO  
  
-- start a new transaction  
BEGIN TRANSACTION  
  -- insert rows 3 and 4  
  EXEC dbo.usp_t1 3, 4  
  
  -- verify there is still an active transaction  
  SELECT @@TRANCOUNT  
  
  -- verify the rows 3 and 4 were inserted  
  SELECT c1 FROM dbo.t1 WITH (SNAPSHOT)   
  ORDER BY c1  
  
  -- catch the arithmetic overflow error  
  BEGIN TRY  
    EXEC dbo.usp_t1 5, 4444444444444  
  END TRY  
  BEGIN CATCH  
    PRINT N'Error occurred: ' + error_message()  
  END CATCH  
  
  -- verify there is still an active transaction  
  SELECT @@TRANCOUNT  
  
  -- verify rows 3 and 4 are still in the table, and row 5 has not been inserted  
  SELECT c1 FROM dbo.t1 WITH (SNAPSHOT)   
  ORDER BY c1  
  
COMMIT  
GO  
  
-- verify we have no active transaction  
SELECT @@TRANCOUNT  
GO  
  
-- verify rows 3 and 4 has been committed  
SELECT c1 FROM dbo.t1  
ORDER BY c1  
GO  
```  
  
 The following error messages specific to memory-optimized tables are transaction dooming. If they occur in the scope of an atomic block, they will cause the transaction to abort: 10772, 41301, 41302, 41305, 41325, 41332, and 41333.  
  
## Session Settings  
 The session settings in atomic blocks are fixed when the stored procedure is compiled. Some settings can be specified with `BEGIN ATOMIC` while other settings are always fixed to the same value.  
  
 The following options are required with `BEGIN ATOMIC`:  
  
|Required Setting|Description|  
|----------------------|-----------------|  
|`TRANSACTION ISOLATION LEVEL`|Supported values are `SNAPSHOT`, `REPEATABLEREAD`, and `SERIALIZABLE`.|  
|`LANGUAGE`|Determines date and time formats and system messages. All languages and aliases in [sys.syslanguages &#40;Transact-SQL&#41;](/sql/relational-databases/system-compatibility-views/sys-syslanguages-transact-sql) are supported.|  
  
 The following settings are optional:  
  
|Optional Setting|Description|  
|----------------------|-----------------|  
|`DATEFORMAT`|All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date formats are supported. When specified, `DATEFORMAT` overrides the default date format associated with `LANGUAGE`.|  
|`DATEFIRST`|When specified, `DATEFIRST` overrides the default associated with `LANGUAGE`.|  
|`DELAYED_DURABILITY`|Supported values are `OFF` and `ON`.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction commits can be either fully durable, the default, or delayed durable.For more information, see [Control Transaction Durability](../logs/control-transaction-durability.md).|  
  
 The following SET options have the same system default value for all atomic blocks in all natively compiled stored procedures:  
  
|Set Option|System Default for Atomic Blocks|  
|----------------|--------------------------------------|  
|ANSI_NULLS|ON|  
|ANSI_PADDING|ON|  
|ANSI_WARNING|ON|  
|ARITHABORT|ON|  
|ARITHIGNORE|OFF|  
|CONCAT_NULL_YIELDS_NULL|ON|  
|IDENTITY_INSERT|OFF|  
|NOCOUNT|ON|  
|NUMERIC_ROUNDABORT|OFF|  
|QUOTED_IDENTIFIER|ON|  
|ROWCOUNT|0|  
|TEXTSIZE|0|  
|XACT_ABORT|OFF<br /><br /> Uncaught exceptions cause the atomic block to roll back, but not cause the transaction to abort unless the error is transaction dooming.|  
  
## See Also  
 [Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md)  
  
  
