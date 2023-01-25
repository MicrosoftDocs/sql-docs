---
title: "Recompile a Stored Procedure"
description: Learn details about how to recompile a stored procedure by using Transact-SQL.
ms.custom: ""
ms.date: "12/01/2022"
ms.service: sql
ms.subservice: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "sp_recompile"
  - "WITH RECOMPILE clause"
  - "recompiling stored procedures"
  - "stored procedures [SQL Server], recompiling"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Recompile a Stored Procedure
[!INCLUDE[SQL Server Azure SQL Database PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

This article describes how to recompile a stored procedure in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../includes/tsql-md.md)]. There are three ways to do this: `WITH RECOMPILE` option in the procedure definition or when the procedure is called, the RECOMPILE query hint on individual statements, or by using the `sp_recompile` system stored procedure. 

##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   When a procedure is compiled for the first time or recompiled, the procedure's query plan is optimized for the current state of the database and its objects. If a database undergoes significant changes to its data or structure, recompiling a procedure updates and optimizes the procedure's query plan for those changes. This can improve the procedure's processing performance.  
  
-   There are times when procedure recompilation must be forced and other times when it occurs automatically. Automatic recompiling occurs whenever [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted. It also occurs if an underlying table referenced by the procedure has undergone physical design changes.  
  
-   Another reason to force a procedure to recompile is to counteract the "parameter sniffing" behavior of procedure compilation. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes procedures, any parameter values that are used by the procedure when it compiles are included as part of generating the query plan. If these values represent the typical ones with which the procedure is subsequently called, then the procedure benefits from the query plan every time that it compiles and executes. If parameter values on the procedure are frequently atypical, forcing a recompile of the procedure and a new plan based on different parameter values can improve performance.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features statement-level recompilation of procedures. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] recompiles stored procedures, only the statement that caused the recompilation is compiled, instead of the complete procedure.  
  
-   If certain queries in a procedure regularly use atypical or temporary values, procedure performance can be improved by using the RECOMPILE query hint inside those queries. Since only the queries using the query hint will be recompiled instead of the complete procedure, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]'s statement-level recompilation behavior is mimicked. But in addition to using the procedure's current parameter values, the RECOMPILE query hint also uses the values of any local variables inside the stored procedure when you compile the statement. For more information, see [Query Hint (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md).  
  
> [!NOTE]
> In Azure Synapse Analytics dedicated and serverless pools, stored procedures are not pre-compiled code, and so cannot be recompiled. For more information, see [Using stored procedures for dedicated SQL pools in Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-develop-stored-procedures).

###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  

#### WITH RECOMPILE option  

If this option is used when the procedure definition is created, it requires CREATE PROCEDURE permission in the database and ALTER permission on the schema in which the procedure is being created.  
  
If this option is used in an EXECUTE statement, it requires EXECUTE permissions on the procedure. Permissions are not required on the EXECUTE statement itself but execute permissions are required on the procedure referenced in the EXECUTE statement. For more information, see [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md).  
  
#### RECOMPILE query hint  

 This feature is used when the procedure is created and the hint is included in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements in the procedure. Therefore, it requires CREATE PROCEDURE permission in the database and ALTER permission on the schema in which the procedure is being created.  
  
#### sp_recompile system stored procedure  

 Requires ALTER permission on the specified procedure.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  

1. Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1. From the **Standard** bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**. This example creates the procedure definition.  

   ```sql
   USE AdventureWorks2012;  
   GO  
   IF OBJECT_ID ( 'dbo.uspProductByVendor', 'P' ) IS NOT NULL   
       DROP PROCEDURE dbo.uspProductByVendor;  
   GO  
   CREATE PROCEDURE dbo.uspProductByVendor @Name varchar(30) = '%'  
   WITH RECOMPILE  
   AS  
       SET NOCOUNT ON;  
       SELECT v.Name AS 'Vendor name', p.Name AS 'Product name'  
       FROM Purchasing.Vendor AS v   
       JOIN Purchasing.ProductVendor AS pv   
         ON v.BusinessEntityID = pv.BusinessEntityID   
       JOIN Production.Product AS p   
         ON pv.ProductID = p.ProductID  
       WHERE v.Name LIKE @Name;  
   ```  
  
### To recompile a stored procedure by using the WITH RECOMPILE option   
  
Select **New Query**, then copy and paste the following code example into the query window and select **Execute**. This executes the procedure and recompiles the procedure's query plan.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXECUTE HumanResources.uspProductByVendor WITH RECOMPILE;  
GO
```  
  
### To recompile a stored procedure by using sp_recompile  

Select **New Query**, then copy and paste the following example into the query window and select **Execute**. This does not execute the procedure but it does mark the procedure to be recompiled so that its query plan is updated the next time that the procedure is executed.  

```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_recompile N'dbo.uspProductByVendor';   
GO
```  
  
## Next steps

 [Create a Stored Procedure](../../relational-databases/stored-procedures/create-a-stored-procedure.md)   
 [Modify a Stored Procedure](../../relational-databases/stored-procedures/modify-a-stored-procedure.md)   
 [Rename a Stored Procedure](../../relational-databases/stored-procedures/rename-a-stored-procedure.md)   
 [View the Definition of a Stored Procedure](../../relational-databases/stored-procedures/view-the-definition-of-a-stored-procedure.md)   
 [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md)   
 [DROP PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-procedure-transact-sql.md)  
  
  
