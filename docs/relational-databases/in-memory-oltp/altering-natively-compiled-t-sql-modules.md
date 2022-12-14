---
title: "Altering Natively Compiled T-SQL Modules"
description: Learn how to perform ALTER operations on natively compiled stored procedures and natively compiled Transact-SQL modules in SQL Server and Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: 010318a0-6807-47c3-8ecc-bb7cb60513f0
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Altering Natively Compiled T-SQL Modules
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later) and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], you can perform `ALTER` operations on natively compiled stored procedures and other natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] modules such as scalar UDFs and triggers using the `ALTER` statement.  
  
When executing `ALTER` on a natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] module, the module is recompiled using a new definition. While recompilation is in progress, the old version of the module continues to be available for execution. Once compilation completes, module executions are drained, and the new version of the module is installed. When you alter a natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] module, you can modify the following options.  
  
-   Parameters  
-   EXECUTE AS  
-   TRANSACTION ISOLATION LEVEL  
-   LANGUAGE  
-   DATEFIRST  
-   DATEFORMAT  
-   DELAYED_DURABILITY  
  
> [!NOTE]  
> Natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] modules cannot be converted to non-natively compiled modules. Non-natively compiled T-SQL modules cannot be converted to natively compiled modules.  
  
For more information on `ALTER PROCEDURE` functionality and syntax, see [ALTER PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-procedure-transact-sql.md).  
  
You can execute [sp_recompile](../../relational-databases/system-stored-procedures/sp-recompile-transact-sql.md) on a natively compiled [!INCLUDE[tsql](../../includes/tsql-md.md)] modules, which causes the module to recompile on the next execution.  
  
## Example  
The following example creates a memory-optimized table (T1), and a natively compiled stored procedure (usp_1) that selects all the T1 columns. Then, usp_1 is altered to remove the `EXECUTE AS` clause, change the `LANGUAGE`, and select only one column (C1) from T1.  
  
```sql  
CREATE TABLE [dbo].[T1] (  
  [c1] [int] NOT NULL,  
  [c2] [float] NOT NULL,  
  CONSTRAINT [PK_T1] PRIMARY KEY NONCLUSTERED ([c1])  
  ) WITH ( MEMORY_OPTIMIZED = ON , DURABILITY = SCHEMA_AND_DATA )  
GO  
  
CREATE PROCEDURE [dbo].[usp_1]  
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
AS BEGIN ATOMIC WITH  
(  
 TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english'  
)  
   SELECT c1, c2 FROM dbo.T1  
END  
GO  
  
ALTER PROCEDURE [dbo].[usp_1]  
WITH NATIVE_COMPILATION, SCHEMABINDING  
AS BEGIN ATOMIC WITH  
(  
 TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'Dutch'  
)  
   SELECT c1 FROM dbo.T1  
END  
GO    
```   
  
## See Also  
 [Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md)
