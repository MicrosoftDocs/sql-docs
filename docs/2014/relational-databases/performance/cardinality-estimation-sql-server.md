---
title: "Cardinality Estimation (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "11/24/2015"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "cardinality estimator"
  - "CE (cardinality estimator)"
  - "estimating cardinality"
ms.assetid: baa8a304-5713-4cfe-a699-345e819ce6df
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Cardinality Estimation (SQL Server)
  The cardinality estimation logic, called the cardinality estimator, is re-designed in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to improve the quality of query plans, and therefore to improve query performance. The new cardinality estimator incorporates assumptions and algorithms that work well on modern OLTP and data warehousing workloads. It is based on in-depth cardinality estimation research on modern workloads, and our learnings over the past 15 years of improving the SQL Server cardinality estimator. Feedback from customers shows that while most queries will benefit from the change or remain unchanged, a small number might show regressions compared to the previous cardinality estimator.  
  
> [!NOTE]  
>  Cardinality estimates are a prediction of the number of rows in the query result. The query optimizer uses these estimates to choose a plan for executing the query. The quality of the query plan has a direct impact on improving query performance.  
  
## Performance Testing and Tuning Recommendations  
 The new cardinality estimator is enabled for all new databases created in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]. However, upgrading to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] does not enable the new cardinality estimator on existing databases.  
  
 To ensure the best query performance, use these recommendations to test your workload with the new cardinality estimator before enabling it on your production system.  
  
1.  Upgrade all existing databases to use the new cardinality estimator. To do this, use [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) to set the database compatibility level to 120.  
  
2.  Run your test workload with the new cardinality estimator, and then troubleshoot any new performance issues in the same manner you currently troubleshoot performance issues.  
  
3.  Once your workload is running with the new cardinality estimator (database compatibility level 120 (SQL Server 2014)), and a specific query has regressed, you can run the query with trace flag 9481 to use the version of the cardinality estimator used in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and earlier. To run a query with a trace flag, see the KB article [Enable plan-affecting SQL Server query optimizer behavior that can be controlled by different trace flags on a specific-query level](https://support.microsoft.com/kb/2801413).  
  
4.  If you cannot change all of the databases at once to use the new cardinality estimator, you can use the former cardinality estimator for all databases by using [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level) to set the database compatibility level to 110.  
  
5.  If your workload is running with database compatibility level 110 and you want to test or run a specific query with the new cardinality estimator, you can run the query with trace flag 2312 to use the SQL Server 2014 version of the cardinality estimator.  To run a query with a trace flag, see the KB article [Enable plan-affecting SQL Server query optimizer behavior that can be controlled by different trace flags on a specific-query level](https://support.microsoft.com/kb/2801413).  
  
## New XEvents  
 There are two new query_optimizer_estimate_cardinality XEvents to support the new query plans.  
  
-   *query_optimizer_estimate_cardinality* occurs when the query optimizer estimates the cardinality on a relational expression.  
  
-   *query_optimizer_force_both_cardinality_estimation*_behaviors occurs when both traceflags 2312 and 9481 are enabled, attempting to force both old and new cardinality estimation behavior at the same time.  
  
## Examples  
 The following examples show some of the changes in the new cardinality estimates. The code for estimating cardinality has been rewritten. The logic is complex and it is not possible to provide an exhaustive list of all changes.  
  
> [!NOTE]  
>  These examples are provided as conceptual information. No action is required on your part to change the way you design databases and queries.  
  
### Example A. New cardinality estimates use an average cardinality for recently added ascending data  
 This example demonstrates how the new cardinality estimator can improve cardinality estimates for ascending data that exceeds the maximum value in the table during the most recent statistics update.  
  
```  
SELECT item, category, amount FROM dbo.Sales AS s WHERE Date = '2013-12-19';  
```  
  
 In this example, new rows are added to the Sales table each day, the query asks for sales that occurred on 12/19/2013, and statistics were last updated on 12/18/2013. The previous cardinality estimator assumes the 12/19/2013 values do not exist since the date exceeds the maximum date and statistics have not been updated to include the 12/19/2013 values. This situation, known as the ascending key problem, will occur if you load data during the day, and then run queries against the data before statistics are updated.  
  
 This behavior has changed. Now, even if statistics have not been updated for the most recent ascending data that is added since the last statistics update, the new cardinality estimator assumes the values exist and uses the average cardinality for each value in the column as the cardinality estimate.  
  
### Example B. New cardinality estimates assume filtered predicates on the same table have some correlation  
 For this example, assume the table Cars as 1000 rows, Make has 200 matches for 'Honda', Model has 50 matches for 'Civic', and that all of the Civics are Hondas. Therefore, 20% of the values in the Make column are 'Honda', 5% of the values in the Model column are 'Civic', and the actual number of Honda Civics is 50. The previous cardinality estimates assume the values in the Make and the Model columns are independent of each other. The previous query optimizer estimates there are 10 Honda Civics (.05 * .20 \* 1000 rows = 10 rows).  
  
```  
SELECT year, purchase_price FROM dbo.Cars WHERE Make = 'Honda' AND Model = 'Civic';  
```  
  
 This behavior has changed. Now, the new cardinality estimates assume the Make and Model columns have *some* correlation. The query optimizer estimates a higher cardinality by adding an exponential component to the estimation equation. The query optimizer now estimates that 22.36 rows ( .05 * SQRT(.20) \* 1000 rows = 22.36 rows ) match the predicate. For this scenario and specific data distribution, 22.36 rows is closer to the actual 50 rows that the query will return.  
  
 Note, the new cardinality estimator logic sorts the predicate selectivities and increases the exponent. For example, if the predicate selectivities were .05, .20, and .25, the cardinality estimate would be (.05 * SQRT(.20) \* SQRT(SQRT(.25)) ).  
  
### Example C. New cardinality estimates assume filtered predicates on different tables are independent  
 For this example, the previous cardinality estimator assumes that the predicate filters s.type and r.date are correlated. However, test results on modern workloads showed that predicate filters on columns in different tables are usually not correlated with each other.  
  
```  
SELECT s.ticket, s.customer, r.store FROM dbo.Sales AS s CROSS JOIN dbo.Returns AS r  
WHERE s.ticket = r.ticket AND s.type = 'toy' AND r.date = '2013-12-19';  
```  
  
 This behavior has changed. Now, the new cardinality estimator logic assumes that s.type is not correlated with r.date. In practical terms, the assumption is that toys are returned every day and not only on a specific day. In this case, the new cardinality estimates will be a smaller number than the previous cardinality estimates.  
  
## See Also  
 [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)  
  
  
