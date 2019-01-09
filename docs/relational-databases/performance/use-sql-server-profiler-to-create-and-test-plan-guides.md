---
title: "Use SQL Server Profiler to Create and Test Plan Guides | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "checking plan guides"
  - "plan guides [SQL Server], testing"
  - "plan guides [SQL Server], SQL Server Profiler"
  - "matching queries to plan guides [SQL Server]"
  - "testing plan guides"
  - "SQL Server Profiler, plan guides"
  - "plan guides [SQL Server], creating"
  - "capturing query text [SQL Server]"
  - "verifying plan guides"
  - "Profiler [SQL Server Profiler], plan guides"
  - "query-to-plan guide matching [SQL Server]"
ms.assetid: 7018dbf0-1a1a-411a-88af-327bedf9cfbd
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Use SQL Server Profiler to Create and Test Plan Guides
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  When you are creating a plan guide, you can use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to capture the exact query text for use in the *statement_text* argument of the **sp_create_plan_guide** stored procedure. This helps make sure that the plan guide will be matched to the query at compile time. After the plan guide is created, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can also be used to test that the plan guide is, in fact, being matched to the query. Generally, you should test plan guides by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to verify that your query is being matched to your plan guide.  
  
## Capturing Query Text by Using SQL Server Profiler  
 If you run a query and capture the text exactly as it was submitted to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can create a plan guide of type SQL or TEMPLATE that will match the query text exactly. This makes sure that the plan guide is used by the query optimizer.  
  
 Consider the following query that is submitted by an application as a stand-alone batch:  
  
```  
SELECT COUNT(*) AS c  
FROM Sales.SalesOrderHeader AS h  
INNER JOIN Sales.SalesOrderDetail AS d  
  ON h.SalesOrderID = d.SalesOrderID  
WHERE h.OrderDate BETWEEN '20000101' and '20050101';  
```  
  
 Suppose you want this query to execute using a merge join operation, but SHOWPLAN indicates that the query is not using a merge join. You cannot change the query directly in the application, so instead you create a plan guide to specify that the MERGE JOIN query hint be appended to the query at compile time.  
  
 To capture the text of the query exactly as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] receives it, follow these steps:  
  
1.  Start a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace, making sure that the **SQL:BatchStarting** event type is selected.  
  
2.  Have the application run the query.  
  
3.  Pause the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] Trace.  
  
4.  Click the **SQL:BatchStarting** event that corresponds to the query.  
  
5.  Right-click and select **Extract Event Data**.  
  
    > [!IMPORTANT]  
    >  Do not try to copy the batch text by selecting it from the lower pane of the Profiler trace window. This might cause the plan guide that you create to not match the original batch.  
  
6.  Save the event data to a file. This is the batch text.  
  
7.  Open the batch text file in Notepad and copy the text to the copy and paste buffer.  
  
8.  Create the plan guide and paste the copied text inside the quotation marks (**''**) specified for the **@stmt** argument. You must escape any single quotation marks in the **@stmt** argument by preceding them with another single quotation mark. Be careful not to add or remove any other characters when you insert these single quotation marks. For example, the date literal **'**20000101**'** must be delimited as **''**20000101**''**.  
  
 Here is the plan guide:  
  
```  
EXEC sp_create_plan_guide   
    @name = N'MyGuide1',  
    @stmt = N'<paste the text copied from the batch text file here>',  
    @type = N'SQL',  
    @module_or_batch = NULL,  
    @params = NULL,  
    @hints = N'OPTION (MERGE JOIN)';  
```  
  
## Testing Plan Guides by Using SQL Server Profiler  
 To verify that a plan guide is being matched to a query, follow these steps:  
  
1.  Start a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace, making certain that the **Showplan XML** event type is selected (located under the **Performance** node).  
  
2.  Have the application run the query.  
  
3.  Pause the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] Trace.  
  
4.  Find the **Showplan XML** event for the affected query.  
  
    > [!NOTE]  
    >  The **Showplan XML for Query Compile** event cannot be used. **PlanGuideDB** does not exist in that event.  
  
5.  If the plan guide is of type OBJECT or SQL, verify that the **Showplan XML** event contains the **PlanGuideDB** and **PlanGuideName** attributes for the plan guide that you expected to match the query. Or, in the case of a TEMPLATE plan guide, verify that the **Showplan XML** event contains the **TemplatePlanGuideDB** and **TemplatePlanGuideName** attributes for the expected plan guide. This verifies that the plan guide is working. These attributes are contained under the **\<StmtSimple>** element of the plan.  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
  
  
