---
title: "Altering Natively Compiled T-SQL Modules | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 010318a0-6807-47c3-8ecc-bb7cb60513f0
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "craigg"
---
# Altering Natively Compiled T-SQL Modules
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  In [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] (and later) and [!INCLUDE[ssSDS](../../includes/sssds-md.md)] you can perform ALTER operations on natively compiled stored procedures and other natively compiled T-SQL modules such as scalar UDFs and triggers using the ALTER statement.  
  
 When executing ALTER on a natively compiled T-SQL module, the module is recompiled using a new definition. While recompilation is in progress, the old version of the module continues to be available for execution. Once compilation completes, module executions are drained, and the new version of the module is installed. When you alter a natively compiled T-SQL module, you can modify the following options.  
  
-   Parameters  
  
-   EXECUTE AS  
  
-   TRANSACTION ISOLATION LEVEL  
  
-   LANGUAGE  
  
-   DATEFIRST  
  
-   DATEFORMAT  
  
-   DELAYED_DURABILITY  
  
> [!NOTE]  
>  Natively compiled T-SQL modules cannot be converted to non-natively compiled modules. Non-natively compiled T-SQL modules cannot be converted to natively compiled modules.  
  
 For more information on ALTER PROCEDURE functionality and syntax, see [ALTER PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-procedure-transact-sql.md)  
  
 You can execute sp_recompile on a natively compiled T-SQL modules, which causes the module to recompile on the next execution.  
  
## Example  
 The following example creates a memory-optimized table (T1), and a natively compiled stored procedure (SP1) that selects all the T1 columns. Then, SP1 is altered to remove the EXECUTE AS clause, change the LANGUAGE, and select only one column (C1) from T1.  
  
```  
CREATE TABLE [dbo].[T1]  
(  
[c1] [int] NOT NULL,  
[c2] [float] NOT NULL,  
CONSTRAINT [PK_T1] PRIMARY KEY NONCLUSTERED ([c1])  
)WITH ( MEMORY_OPTIMIZED = ON , DURABILITY = SCHEMA_AND_DATA )  
GO  
  
CREATE PROCEDURE [dbo].[usp_1]  
WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
AS BEGIN ATOMIC WITH  
(  
 TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english'  
)  
 SELECT c1, c2 from dbo.T1  
END  
GO  
  
ALTER PROCEDURE [dbo].[usp_1]  
WITH NATIVE_COMPILATION, SCHEMABINDING  
AS BEGIN ATOMIC WITH  
(  
 TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'Dutch'  
)  
 SELECT c1 from dbo.T1  
END  
GO  
  
```  
  
  