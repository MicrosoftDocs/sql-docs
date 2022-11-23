---
title: "Creating Natively Compiled Stored Procedures"
description: Learn about Transact-SQL features supported only for natively compiled stored procedures. See how to create natively compiled stored procedures in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: e6b34010-cf62-4f65-bbdf-117f291cde7b
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating Natively Compiled Stored Procedures
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Natively compiled stored procedures do not implement the full [!INCLUDE[tsql](../../includes/tsql-md.md)] programmability and query surface area. There are certain [!INCLUDE[tsql](../../includes/tsql-md.md)] constructs that cannot be used inside natively compiled stored procedures. For more information, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).  
  
The following [!INCLUDE[tsql](../../includes/tsql-md.md)] features are only supported for natively compiled stored procedures:  
  
-   Atomic blocks. For more information, see [Atomic Blocks](../../relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures.md).  
  
-   `NOT NULL` constraints on parameters and variables. You cannot assign **NULL** values to parameters or variables declared as **NOT NULL**. For more information, see [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md).  
  
    -   `CREATE PROCEDURE dbo.myproc (@myVarchar VARCHAR(32) NOT NULL) AS (...)`  
  
    -   `DECLARE @myVarchar VARCHAR(32) NOT NULL = "Hello"; -- Must initialize to a value.`  
  
    -   `SET @myVarchar = NULL; -- Compiles, but fails during run time.`  
  
-   Schema binding of natively compiled stored procedures.  
  
Natively compiled stored procedures are created using [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md). The following example shows a memory-optimized table and a natively compiled stored procedure used for inserting rows into the table.  
  
```sql  
CREATE TABLE [dbo].[T2] (  
  [c1] [int] NOT NULL, 
  [c2] [datetime] NOT NULL,
  [c3] nvarchar(5) NOT NULL, 
  CONSTRAINT [PK_T1] PRIMARY KEY NONCLUSTERED ([c1])  
  ) WITH ( MEMORY_OPTIMIZED = ON , DURABILITY = SCHEMA_AND_DATA )  
GO  
  
CREATE PROCEDURE [dbo].[usp_2] (@c1 int, @c3 nvarchar(5)) 
WITH NATIVE_COMPILATION, SCHEMABINDING  
AS BEGIN ATOMIC WITH  
(  
 TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english'  
)  
  DECLARE @c2 datetime = GETDATE();  
  INSERT INTO [dbo].[T2] (c1, c2, c3) values (@c1, @c2, @c3);  
END  
GO  
```  
 
In the code sample, **NATIVE_COMPILATION** indicates that this [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure is a natively compiled stored procedure. The following options are required:  
  
|Option|Description|  
|------------|-----------------|  
|**SCHEMABINDING**|A natively compiled stored procedure must be bound to the schema of the objects it references. This means that tables referenced by the procedure cannot be dropped. Tables referenced in the procedure must include their schema name, and wildcards (\*) are not allowed in queries (meaning no `SELECT * from...`). **SCHEMABINDING** is only supported for natively compiled stored procedures in this version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**BEGIN ATOMIC**|The natively compiled stored procedure body must consist of exactly one atomic block. Atomic blocks guarantee atomic execution of the stored procedure. If the procedure is invoked outside the context of an active transaction, it will start a new transaction, which commits at the end of the atomic block. Atomic blocks in natively compiled stored procedures have two required options:<br /><br /> **TRANSACTION ISOLATION LEVEL**. See [Transaction Isolation Levels for Memory-Optimized Tables](/previous-versions/sql/sql-server-2016/dn133175(v=sql.130)) for supported isolation levels.<br /><br /> **LANGUAGE**. The language for the stored procedure must be set to one of the available languages or language aliases.|  
  
## See Also  
 [Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md)  
  
