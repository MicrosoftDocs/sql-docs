---
title: "Implementing a CASE Expression in a Natively Compiled Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "11/21/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: 2f82db01-da7e-4a7d-8bc0-48b245e6f768
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Implementing a CASE Expression in a Natively Compiled Stored Procedure
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

**Applies to:** [!INCLUDE[ssSDSFull_md](../../includes/sssdsfull-md.md)] and SQL Server starting [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)]

CASE expressions are supported in natively compiled T-SQL modules. The following example demonstrates a way to use
the CASE expression in a query. 

``` 
-- Query using a CASE expression in a natively compiled stored procedure.
CREATE PROCEDURE dbo.usp_SOHOnlineOrderResult  
   WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
   AS BEGIN ATOMIC WITH  (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE=N'us_english')  
   SELECT   
      SalesOrderID,   
      CASE (OnlineOrderFlag)   
      WHEN 1 THEN N'Order placed online by customer'  
      ELSE N'Order placed by sales person'  
      END  
   FROM Sales.SalesOrderHeader_inmem
END  
GO  
  
EXEC dbo.usp_SOHOnlineOrderResult  
GO  
``` 

**Applies to:** [!INCLUDE[ssSQL14-md](../../includes/ssSQL14-md.md)] and SQL Server starting [!INCLUDE[ssSQL15-md](../../includes/ssSQL15-md.md)]

  CASE expressions are *not* supported in natively compiled T-SQL modules. The following sample shows a way to implement the functionality of a CASE expression in a natively compiled stored procedure.  
  
 The code samples uses a table variable to construct a single result set. This is suitable only when processing a limited number of rows, because it involves creating an additional copy of the data rows.  
  
 You should test the performance of this workaround.  
  
```  
-- original query  
SELECT   
   SalesOrderID,   
   CASE (OnlineOrderFlag)   
   WHEN 1 THEN N'Order placed online by customer'  
   ELSE N'Order placed by sales person'  
   END  
FROM Sales.SalesOrderHeader_inmem  
  
--  workaround for CASE in natively compiled stored procedures  
--  use a table for the single resultset  
CREATE TYPE dbo.SOHOnlineOrderResult AS TABLE  
(  
   SalesOrderID uniqueidentifier not null index ix_SalesOrderID,  
     OrderFlag nvarchar(100) not null  
) with (memory_optimized=on)  
go  
  
-- natively compiled stored procedure that includes the query  
CREATE PROCEDURE dbo.usp_SOHOnlineOrderResult  
   WITH NATIVE_COMPILATION, SCHEMABINDING, EXECUTE AS OWNER  
   AS BEGIN ATOMIC WITH  
      (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE=N'us_english')  
  
   -- table variable for creating the single resultset  
   DECLARE @result dbo.SOHOnlineOrderResult  
  
   -- CASE OnlineOrderFlag=1  
   INSERT @result   
   SELECT SalesOrderID, N'Order placed online by customer'  
      FROM Sales.SalesOrderHeader_inmem  
      WHERE OnlineOrderFlag=1  
  
   -- ELSE  
   INSERT @result   
   SELECT SalesOrderID, N'Order placed by sales person'  
      FROM Sales.SalesOrderHeader_inmem  
      WHERE OnlineOrderFlag!=1  
  
   -- return single resultset  
   SELECT SalesOrderID, OrderFlag FROM @result  
END  
GO  
  
EXEC dbo.usp_SOHOnlineOrderResult  
GO  
```  
  
## See Also  
 [Migration Issues for Natively Compiled Stored Procedures](../../relational-databases/in-memory-oltp/migration-issues-for-natively-compiled-stored-procedures.md)   
 [Transact-SQL Constructs Not Supported by In-Memory OLTP](../../relational-databases/in-memory-oltp/transact-sql-constructs-not-supported-by-in-memory-oltp.md)  
  
  
