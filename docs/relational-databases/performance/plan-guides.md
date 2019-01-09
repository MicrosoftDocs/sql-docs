---
title: "Plan Guides | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "TEMPLATE plan guide"
  - "SQL plan guides"
  - "OPTIMIZE FOR query hint"
  - "RECOMPILE query hint"
  - "OBJECT plan guide"
  - "plan guides [SQL Server], about plan guides"
  - "OPTION clause"
  - "plan guides [SQL Server]"
  - "USE PLAN query hint"
ms.assetid: bfc97632-c14c-4768-9dc5-a9c512f6b2bd
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Plan Guides
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Plan guides let you optimize the performance of queries when you cannot or do not want to directly change the text of the actual query in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Plan guides influence the optimization of queries by attaching query hints or a fixed query plan to them. Plan guides can be useful when a small subset of queries in a database application provided by a third-party vendor are not performing as expected. In the plan guide, you specify the Transact-SQL statement that you want optimized and either an OPTION clause that contains the query hints you want to use or a specific query plan you want to use to optimize the query. When the query executes, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] matches the Transact-SQL statement to the plan guide and attaches the OPTION clause to the query at run time or uses the specified query plan.  
  
 The total number of plan guides you can create is limited only by available system resources. Nevertheless, plan guides should be limited to mission-critical queries that are targeted for improved or stabilized performance. Plan guides should not be used to influence most of the query load of a deployed application.  
  
> [!NOTE]
>  Plan guides cannot be used in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md). Plan guides are visible in any edition. You can also attach a database that contains plan guides to any edition. Plan guides remain intact when you restore or attach a database to an upgraded version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Types of Plan Guides  
 The following types of plan guides can be created.  
  
 ### OBJECT plan guide  
 An OBJECT plan guide matches queries that execute in the context of [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures, scalar user-defined functions, multi-statement table-valued user-defined functions, and DML triggers.  
  
 Suppose the following stored procedure, which takes the `@Country_region` parameter, is in a database application that is deployed against the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:  
  
```sql  
CREATE PROCEDURE Sales.GetSalesOrderByCountry (@Country_region nvarchar(60))  
AS  
BEGIN  
    SELECT *  
    FROM Sales.SalesOrderHeader AS h, Sales.Customer AS c,   
        Sales.SalesTerritory AS t  
    WHERE h.CustomerID = c.CustomerID  
        AND c.TerritoryID = t.TerritoryID  
        AND CountryRegionCode = @Country_region  
END;  
```  
  
 Assume that this stored procedure has been compiled and optimized for `@Country_region = N'AU'` (Australia). However, because there are relatively few sales orders that originate from Australia, performance decreases when the query executes using parameter values of countries with more sales orders. Because the most sales orders originate in the United States, a query plan that is generated for `@Country_region = N'US'` would likely perform better for all possible values of the `@Country_region` parameter.  
  
 You could address this problem by modifying the stored procedure to add the `OPTIMIZE FOR` query hint to the query. However, because the stored procedure is in a deployed application, you cannot directly modify the application code. Instead, you can create the following plan guide in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```sql  
sp_create_plan_guide   
@name = N'Guide1',  
@stmt = N'SELECT *FROM Sales.SalesOrderHeader AS h,  
        Sales.Customer AS c,  
        Sales.SalesTerritory AS t  
        WHERE h.CustomerID = c.CustomerID   
            AND c.TerritoryID = t.TerritoryID  
            AND CountryRegionCode = @Country_region',  
@type = N'OBJECT',  
@module_or_batch = N'Sales.GetSalesOrderByCountry',  
@params = NULL,  
@hints = N'OPTION (OPTIMIZE FOR (@Country_region = N''US''))';  
```  
  
 When the query specified in the `sp_create_plan_guide` statement executes, the query is modified before optimization to include the `OPTIMIZE FOR (@Country = N''US'')` clause.  
  
 ### SQL plan guide  
 An SQL plan guide matches queries that execute in the context of stand-alone [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and batches that are not part of a database object. SQL-based plan guides can also be used to match queries that parameterize to a specified form. SQL plan guides apply to stand-alone [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and batches. Frequently, these statements are submitted by an application by using the [sp_executesql](../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md) system stored procedure. For example, consider the following stand-alone batch:  
  
```sql  
SELECT TOP 1 * FROM Sales.SalesOrderHeader ORDER BY OrderDate DESC;  
```  
  
 To prevent a parallel execution plan from being generated on this query, create the following plan guide and set the `MAXDOP` query hint to `1` in the `@hints` parameter.  
  
```sql  
sp_create_plan_guide   
@name = N'Guide2',   
@stmt = N'SELECT TOP 1 * FROM Sales.SalesOrderHeader ORDER BY OrderDate DESC',  
@type = N'SQL',  
@module_or_batch = NULL,   
@params = NULL,   
@hints = N'OPTION (MAXDOP 1)';  
```  
  
> [!IMPORTANT]  
>  The values that are supplied for the `@module_or_batch` and `@params` arguments of the `sp_create_plan guide` statement must match the corresponding text submitted in the actual query. For more information, see [sp_create_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md) and [Use SQL Server Profiler to Create and Test Plan Guides](../../relational-databases/performance/use-sql-server-profiler-to-create-and-test-plan-guides.md).  
  
 SQL plan guides can also be created on queries that parameterize to the same form when the PARAMETERIZATION database option is SET to FORCED, or when a TEMPLATE plan guide is created specifying that a parameterized class of queries.  
  
 ### TEMPLATE plan guide  
 A TEMPLATE plan guide matches stand-alone queries that parameterize to a specified form. These plan guides are used to override the current PARAMETERIZATION database SET option of a database for a class of queries.  
  
 You can create a TEMPLATE plan guide in either of the following situations:  
  
-   The PARAMETERIZATION database option is SET to FORCED, but there are queries you want compiled according to the rules of [Simple Parameterization](../../relational-databases/query-processing-architecture-guide.md#SimpleParam).  
  
-   The PARAMETERIZATION database option is SET to SIMPLE (the default setting), but you want [Forced Parameterization](../../relational-databases/query-processing-architecture-guide.md#ForcedParam) to be tried on a class of queries.  
  
## Plan Guide Matching Requirements  
 Plan guides are scoped to the database in which they are created. Therefore, only plan guides that are in the database that is current when a query executes can be matched to the query. For example, if [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] is the current database and the following query executes:  
  
 ```sql
 SELECT FirstName, LastName FROM Person.Person;
 ```  
  
 Only plan guides in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database are eligible to be matched to this query. However, if [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] is the current database and the following statements are run:  
  
 ```sql
 USE DB1; 
 SELECT FirstName, LastName FROM Person.Person;
 ```  
  
 Only plan guides in `DB1` are eligible to be matched to the query because the query is executing in the context of `DB1`.  
  
 For SQL- or TEMPLATE-based plan guides, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] matches the values for the @module_or_batch and @params arguments to a query by comparing the two values character by character. This means you must provide the text exactly as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives it in the actual batch.  
  
 When @type = 'SQL' and @module_or_batch is set to NULL, the value of @module_or_batch is set to the value of @stmt. This means that the value for *statement_text* must be provided in the identical format, character-for-character, as it is submitted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. No internal conversion is performed to facilitate this match.  
  
 When both a regular (SQL or OBJECT) plan guide and a TEMPLATE plan guide can apply to a statement, only the regular plan guide will be used.  
  
> [!NOTE]  
>  The batch that contains the statement on which you want to create a plan guide cannot contain a USE *database* statement.  
  
## Plan Guide Effect on the Plan Cache  
 Creating a plan guide on a module removes the query plan for that module from the plan cache. Creating a plan guide of type OBJECT or SQL on a batch removes the query plan for a batch that has the same hash value. Creating a plan guide of type TEMPLATE removes all single-statement batches from the plan cache within that database.  
  
## Related Tasks  
  
|Task|Topic|  
|----------|-----------|  
|Describes how to create a plan guide.|[Create a New Plan Guide](../../relational-databases/performance/create-a-new-plan-guide.md)|  
|Describes how to create a plan guide for parameterized queries.|[Create a Plan Guide for Parameterized Queries](../../relational-databases/performance/create-a-plan-guide-for-parameterized-queries.md)|  
|Describes how to control query parameterization behavior by using plan guides.|[Specify Query Parameterization Behavior by Using Plan Guides](../../relational-databases/performance/specify-query-parameterization-behavior-by-using-plan-guides.md)|  
|Describes how to include a fixed query plan in a plan guide.|[Apply a Fixed Query Plan to a Plan Guide](../../relational-databases/performance/apply-a-fixed-query-plan-to-a-plan-guide.md)|  
|Describes how to specify query hints in a plan guide.|[Attach Query Hints to a Plan Guide](../../relational-databases/performance/attach-query-hints-to-a-plan-guide.md)|  
|Describes how to view plan guide properties.|[View Plan Guide Properties](../../relational-databases/performance/view-plan-guide-properties.md)|  
|Describes how to use SQL Server Profiler to create and test plan guides.|[Use SQL Server Profiler to Create and Test Plan Guides](../../relational-databases/performance/use-sql-server-profiler-to-create-and-test-plan-guides.md)|  
|Describes how to validate plan guides.|[Validate Plan Guides After Upgrade](../../relational-databases/performance/validate-plan-guides-after-upgrade.md)|  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)   
 [sp_control_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-control-plan-guide-transact-sql.md)   
 [sys.plan_guides &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-plan-guides-transact-sql.md)   
 [sys.fn_validate_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-validate-plan-guide-transact-sql.md)  
  
  
