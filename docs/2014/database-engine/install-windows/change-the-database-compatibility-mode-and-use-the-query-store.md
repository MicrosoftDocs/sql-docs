---
title: "Migrate Query Plans | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "query plans [SQL Server], migrating"
  - "upgrading SQL Server, migrating query plans"
  - "plan guides [SQL Server], migrating query plans"
ms.assetid: 7e02a137-6867-4f6a-a45a-2b02674f7e65
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Migrate Query Plans
  In most cases, upgrading a database to the most recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will result in improved query performance. However, if you have mission-critical queries that have been carefully tuned for performance, you may want to preserve the query plans for these queries before upgrading by creating a plan guide for each query. If, after upgrading, the query optimizer chooses a less efficient plan for one or more of the queries, you can enable the plan guides and force the query optimizer to use the pre-upgrade plans.  
  
 To create plan guides before upgrading follow these steps:  
  
1.  Record the current plan for each mission critical query by using the [sp_create_plan_guide](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql) stored procedure and specifying the query plan in the USE PLAN query hint.  
  
2.  Verify that the plan guide is applied to the query.  
  
3.  Upgrade the database to the newer version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The plans are persisted in the upgraded database in the plan guides and serve as a fallback in case of plan regressions after the upgrade.  
  
     We recommend that you not enable the plan guides after the upgrade because you might miss opportunities for better plans in the new release or beneficial recompiles due to updated statistics.  
  
4.  If less efficient plans are chosen after the upgrade, activate all or a subset of the plan guides to override the new plans.  
  
## Example  
 The following example shows how to record a pre-upgrade plan for a query by creating a plan guide.  
  
### Step 1: Collect the Plan  
 The query plan recorded in the plan guide must be in XML format. XML-formatted query plans can be produced through the following ways:  
  
-   [SET SHOWPLAN_XML](/sql/t-sql/statements/set-showplan-xml-transact-sql)  
  
-   [SET STATISTICS XML](/sql/t-sql/statements/set-statistics-xml-transact-sql)  
  
-   Querying the query_plan column of the [sys.dm_exec_query_plan](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql) dynamic management function.  
  
-   The [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] [Showplan XML](../../relational-databases/event-classes/showplan-xml-event-class.md), [Showplan XML Statistics Profile](../../relational-databases/event-classes/showplan-xml-statistics-profile-event-class.md), and [Showplan XML For Query Compile](../../relational-databases/event-classes/showplan-xml-for-query-compile-event-class.md) event classes.  
  
 The following example collects the query plan for the statement `SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;` by querying dynamic management views.  
  
```  
USE AdventureWorks;  
GO  
SELECT query_plan  
    FROM sys.dm_exec_query_stats AS qs   
    CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st  
    CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, DEFAULT, DEFAULT) AS qp  
    WHERE st.text LIKE N'SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;%';  
GO  
```  
  
### Step 2: Create the Plan Guide to Force the Plan  
 Using the XML-formatted query plan (obtained by any of the methods previously described) in the plan guide, copy and paste the query plan as a string literal inside the USE PLAN query hint specified in the OPTION clause of sp_create_plan_guide.  
  
 Within the XML plan itself, escape quotation marks (') that appear in the plan with a second quotation mark before creating the plan guide. For example, a plan that contains `WHERE A.varchar = 'This is a string'` must be escaped by modifying the code to `WHERE A.varchar = ''This is a string''`.  
  
 The following example creates a plan guide for the query plan collected in step 1 and inserts the XML Showplan for the query in the `@hints` parameter. For brevity, only partial Showplan output is included in the example.  
  
```  
EXECUTE sp_create_plan_guide   
@name = N'Guide1',  
@stmt = N'SELECT City, StateProvinceID, PostalCode FROM Person.Address ORDER BY PostalCode DESC;',  
@type = N'SQL',  
@module_or_batch = NULL,  
@params = NULL,  
@hints = N'OPTION(USE PLAN N''<ShowPlanXML xmlns=''''https://schemas.microsoft.com/sqlserver/2004/07/showplan''''   
    Version=''''0.5'''' Build=''''9.00.1116''''>  
    <BatchSequence><Batch><Statements><StmtSimple>  
    ...  
    </StmtSimple></Statements></Batch>  
    </BatchSequence></ShowPlanXML>'')';  
GO  
```  
  
### Step 3: Verify That the Plan Guide Is Applied to the Query  
 Run the query again and examine the query plan that is produced. You should see that the plan matches the one that you specified in the plan guide.  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [Query Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-query)   
 [Plan Guides](../../relational-databases/performance/plan-guides.md)  
  
  
