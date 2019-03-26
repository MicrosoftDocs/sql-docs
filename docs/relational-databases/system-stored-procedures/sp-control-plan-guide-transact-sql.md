---
title: "sp_control_plan_guide (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_control_plan_guide"
  - "sp_control_plan_guide_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_control_plan_guide"
ms.assetid: c96d43d5-6507-4d66-b3f5-f44c0617cb5c
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_control_plan_guide (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Drops, enables, or disables a plan guide.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_control_plan_guide [ @operation = ] N'<control_option>'  
  [ , [ @name = ] N'plan_guide_name' ]  
  
<control_option>::=  
{   
    DROP   
  | DROP ALL  
  | DISABLE  
  | DISABLE ALL  
  | ENABLE   
  | ENABLE ALL  
}  
```  
  
## Arguments  
 **N'** _plan_guide_name_ **'**  
 Specifies the plan guide that is being dropped, enabled, or disabled. *plan_guide_name* is resolved to the current database. If not specified, *plan_guide_name* defaults to NULL.  
  
 DROP  
 Drops the plan guide specified by *plan_guide_name*. After a plan guide is dropped, future executions of a query formerly matched by the plan guide are not influenced by the plan guide.  
  
 DROP ALL  
 Drops all plan guides in the current database. **N'**_plan_guide_name_ cannot be specified when DROP ALL is specified.  
  
 DISABLE  
 Disables the plan guide specified by *plan_guide_name*. After a plan guide is disabled, future executions of a query formerly matched by the plan guide are not influenced by the plan guide.  
  
 DISABLE ALL  
 Disables all plan guides in the current database. **N'**_plan_guide_name_ cannot be specified when DISABLE ALL is specified.  
  
 ENABLE  
 Enables the plan guide specified by *plan_guide_name*. A plan guide can be matched with an eligible query after it is enabled. By default, plan guides are enabled at the time they are created.  
  
 ENABLE ALL  
 Enables all plan guides in the current database. **N'**_plan_guide_name_**'**cannot be specified when ENABLE ALL is specified.  
  
## Remarks  
 Trying to drop or modify a function, stored procedure, or DML trigger that is referenced by a plan guide, either enabled or disabled, causes an error.  
  
 Disabling a disabled plan guide or enabling an enabled plan guide has no effect and runs without error.  
  
 Plans guides are not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md). However, you can execute **sp_control_plan_guide** with the DROP or DROP ALL option in any edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Permissions  
 To execute **sp_control_plan_guide** on a plan guide of type OBJECT (created specifying **@type ='**OBJECT**'** ) requires ALTER permission on the object that is referenced by the plan guide. All other plan guides require ALTER DATABASE permission.  
  
## Examples  
  
### A. Enabling, disabling and dropping a plan guide  
 The following example creates a plan guide, disables it, enables it, and drops it.  
  
```  
--Create a procedure on which to define the plan guide.  
IF OBJECT_ID(N'Sales.GetSalesOrderByCountry', N'P') IS NOT NULL  
    DROP PROCEDURE Sales.GetSalesOrderByCountry;  
GO  
CREATE PROCEDURE Sales.GetSalesOrderByCountry   
    (@Country nvarchar(60))  
AS  
BEGIN  
    SELECT *  
    FROM Sales.SalesOrderHeader AS h   
    INNER JOIN Sales.Customer AS c ON h.CustomerID = c.CustomerID  
    INNER JOIN Sales.SalesTerritory AS t ON c.TerritoryID = t.TerritoryID  
    WHERE t.CountryRegionCode = @Country;  
END  
GO  
--Create the plan guide.  
EXEC sp_create_plan_guide N'Guide3',  
    N'SELECT *  
    FROM Sales.SalesOrderHeader AS h   
    INNER JOIN Sales.Customer AS c ON h.CustomerID = c.CustomerID  
    INNER JOIN Sales.SalesTerritory AS t ON c.TerritoryID = t.TerritoryID  
    WHERE t.CountryRegionCode = @Country',  
    N'OBJECT',  
    N'Sales.GetSalesOrderByCountry',  
    NULL,  
    N'OPTION (OPTIMIZE FOR (@Country = N''US''))';  
GO  
--Disable the plan guide.  
EXEC sp_control_plan_guide N'DISABLE', N'Guide3';  
GO  
--Enable the plan guide.  
EXEC sp_control_plan_guide N'ENABLE', N'Guide3';  
GO  
--Drop the plan guide.  
EXEC sp_control_plan_guide N'DROP', N'Guide3';  
```  
  
### B. Disabling all plan guides in the current database  
 The following example disables all plan guides in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_control_plan_guide N'DISABLE ALL';  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)   
 [sys.plan_guides &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-plan-guides-transact-sql.md)   
 [Plan Guides](../../relational-databases/performance/plan-guides.md)  
  
  
