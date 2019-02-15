---
title: "Creating Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: e6b34010-cf62-4f65-bbdf-117f291cde7b
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Creating Natively Compiled Stored Procedures
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

Natively compiled stored procedures do not implement the full [!INCLUDE[tsql](../../includes/tsql-md.md)] programmability and query surface area. There are certain [!INCLUDE[tsql](../../includes/tsql-md.md)] constructs that cannot be used inside natively compiled stored procedures. For more information, see [Supported Features for Natively Compiled T-SQL Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).  
  
There are, however, several [!INCLUDE[tsql](../../includes/tsql-md.md)] features that are only supported for natively compiled stored procedures:  
  
-   Atomic blocks. For more information, see [Atomic Blocks](../../relational-databases/in-memory-oltp/atomic-blocks-in-native-procedures.md).  
  
-   **NOT NULL** constraints on parameters and variables. You cannot assign **NULL** values to parameters or variables declared as **NOT NULL**. For more information, see [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md).  
  
    -   CREATE PROCEDURE dbo.myproc (@myVarchar  varchar(32)  **not null**) ...  
  
    -   DECLARE @myVarchar  varchar(32)  **not null = "Hello"**; -- *(Must initialize to a value.)*  
  
    -   SET @myVarchar **= null**; -- *(Compiles, but fails during run time.)*  
  
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
|**BEGIN ATOMIC**|The natively compiled stored procedure body must consist of exactly one atomic block. Atomic blocks guarantee atomic execution of the stored procedure. If the procedure is invoked outside the context of an active transaction, it will start a new transaction, which commits at the end of the atomic block. Atomic blocks in natively compiled stored procedures have two required options:<br /><br /> **TRANSACTION ISOLATION LEVEL**. See [Transaction Isolation Levels for Memory-Optimized Tables](https://msdn.microsoft.com/library/8a6a82bf-273c-40ab-a101-46bd3615db8a) for supported isolation levels.<br /><br /> **LANGUAGE**. The language for the stored procedure must be set to one of the available languages or language aliases.|  
  
## See Also  
 [Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/natively-compiled-stored-procedures.md)  
  
  
