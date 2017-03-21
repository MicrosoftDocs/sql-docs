---
title: "Cardinality Estimation (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "10/04/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "cardinality estimator"
  - "CE (cardinality estimator)"
  - "estimating cardinality"
ms.assetid: baa8a304-5713-4cfe-a699-345e819ce6df
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Cardinality Estimation (SQL Server)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  
This article illustrates how you can assess and choose the best cardinality estimation (CE) configuration for your SQL system. Most systems benefit from the latest CE because it is the most accurate. The CE predicts how many rows your query will likely return. The cardinality prediction is used by the query optimizer to generate the optimal query plan. The more accurate the CE, the more optimal the query plan, usually.  
  
Your application system could possibly have an important query whose plan is changed to a slower plan due to the new CE. Such a query might be like one of the following:  
  
- An OLTP query that runs so frequently that multiple instance of it often run concurrently.  
- A SELECT with substantial aggregation that runs during your OLTP business hours.  
  
You have techniques for identifying a query that performs slower with the new CE. And you have options for how to address the performance issue.  
  
  
## Versions of the CE  
  
 In 1998, a major update of the CE was part of Microsoft SQL Server 7.0, for which the compatibility level was 70. Subsequent updates came with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016, meaning compatibility levels 120 and 130. The CE updates for levels 120 and 130 incorporate assumptions and algorithms that work well on modern data warehousing workloads and on OLTP (online transaction processing).  
  
 **Compatibility level:** You can ensure your database is at a particular level by using the following Transact-SQL code for [COMPATIBILITY_LEVEL](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  

```tsql  
SELECT ServerProperty('ProductVersion');  
go  
  
ALTER DATABASE <yourDatabase>  
    SET COMPATIBILITY_LEVEL = 130;  
go  
  
SELECT    d.name, d.compatibility_level  
    FROM  sys.databases AS d  
    WHERE d.name = 'yourDatabase';  
go  
```  
  
 For a SQL Server database set at compatibility level 120, activation of the trace flag 9481 forces the system to use the CE for level 70.  
  
 **Legacy CE:** For a SQL Server database set at compatibility level 130, the level 70 CE can be can be activated by using the following Transact-SQL statement about [SCOPED CONFIGURATION](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md)
.
  
```tsql  
ALTER DATABASE
    SCOPED CONFIGURATION  
        SET LEGACY_CARDINALITY_ESTIMATION = ON;  
go  
  
SELECT  name, value  
    FROM  sys.database_scoped_configurations  
    WHERE name = 'LEGACY_CARDINALITY_ESTIMATION';  
```  
  
 **Query store:**Starting with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2016 the query store is a handy tool for examining the performance of your queries.  In SQL Server Management Studio (SSMS.exe), in the **Object Explorer** under your database node, a **Query Store** node is displayed  when the query store is ON.  
  
```tsql  
ALTER DATABASE <yourDatabase>  
    SET QUERY_STORE = ON;  
go  
  
SELECT  
        q.actual_state_desc    AS [actual_state_desc-ofQueryStore],  
        q.desired_state_desc,  
        q.query_capture_mode_desc  
    FROM  
        sys.database_query_store_options  AS q;  
go  
  
ALTER DATABASE <yourDatabase>  
    SET QUERY_STORE CLEAR;  
```  
  
 *Tip:* We recommend that each month you install the latest release of [(SSMS.exe)](http://msdn.microsoft.com/library/mt238290.aspx).  
  
 Another option for tracking the cardinality predictions of the CE is to use the extended event named **query_optimizer_estimate_cardinality**.  The following T-SQL code sample runs on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It writes a .xel file to C:\Temp\ (although you can change the path). When you open the .xel file in SSMS, its detailed information is displayed in a user friendly manner.  
  
```tsql  
DROP EVENT SESSION Test_the_CE_qoec_1 ON SERVER;  
go  
  
CREATE EVENT SESSION Test_the_CE_qoec_1  
    ON SERVER  
    ADD EVENT sqlserver.query_optimizer_estimate_cardinality  
    (  
        ACTION (sqlserver.sql_text)  
            WHERE (  
                sql_text LIKE '%yourTable%'  
                and sql_text LIKE '%SUM(%'  
            )  
    )  
    ADD TARGET package0.asynchronous_file_target   
        (SET  
            filename = 'c:\temp\xe_qoec_1.xel',  
            metadatafile = 'c:\temp\xe_qoec_1.xem'  
        );  
go  
  
ALTER EVENT SESSION Test_the_CE_qoec_1  
    ON SERVER  
    STATE = START;  --STOP;  
go  
```  
  
 For information about extended events as tailored for Azure SQL Database, see [Extended events in SQL Database](http://azure.microsoft.com/documentation/articles/sql-database-xevent-db-diff-from-svr/).  
  
  
## Steps to assess the CE version  
  
 Next are steps you can use to assess whether any of your most important queries perform less well under the latest CE. Some of the steps are performed by running a code sample presented in a preceding section.  
  
1.  Open SSMS. Ensure your  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]database is set to the highest available compatibility level.  
  
2.  Perform the following preliminary steps:  
  
    1.  Open SSMS.  
  
    2.  Run the T-SQL to ensure that your  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is set to the highest available compatibility level.  
  
    3.  Ensure that your database has its LEGACY_CARDINALITY_ESTIMATION configuration turned OFF.  
  
    4.  CLEAR your query store. Of course, ensure your query store is ON.  
  
    5.  Run the statement: \`SET NoCount OFF;`  
  
3.  Run the statement: \`SET STATISTICS XML ON;`  
  
4.  Run your important query.  
  
5.  In the results pane, on the **Messages** tab, note the actual number of rows affected.  
  
6.  In the results pane on the **Results** tab, double-click the cell that contains the statistics in XML format. A graphic query plan is displayed.  
  
7.  Right-click the first box in the graphic query plan, and then click **Properties**.  
  
8.  For later comparison with a different configuration, note the values for the following properties:  
  
    -   **CardinalityEstimationModelVersion**.  
  
    -   **Estimated Number of Rows**.  
  
    -   **Estimated I/O Cost**, and several similar *Estimated* properties that involve actual performance rather than row count predictions.  
  
    -   **Logical Operation** and **Physical Operation**. *Parallelism* is a good value.  
  
    -   **Actual Execution Mode**. *Batch* is a good value, better than *Row*.  
  
9. Compare the estimated number of rows to the actual number of rows. Is the CE inaccurate by 1% (high or low), or by 10%?  
  
10. Run: \`SET STATISTICS XML OFF;`  
  
11. Run the T-SQL to decrease the compatibility level of your database by one level (such as from 130 down to 120).  
  
12. Rerun all the non-preliminary steps.  
  
13. Compare the CE property values from the two runs.  
  
    - Is the inaccuracy percentage under the newest CE less than under the older CE?  
  
14. Finally, compare the various performance property values from the two runs.  
  
    -   Did your query use a different plan under the two differing CE estimations?  
  
    -   Did your query run slower under the latest CE?  
  
    -   Unless your query runs better and with a different plan under the older CE, you almost certainly want the latest CE.  
  
    -   However, if your query runs with a faster plan under the older CE, consider forcing the system to use the faster plan and to ignore the CE. This way you can have the latest CE on for everything, while keeping the faster plan in the one odd case.  
  
## How to activate the best query plan  
  
Suppose that with the new CE a slower query plan is generated for your query. Here are some options you have to activate the faster plan.  
  
You could set the compatibility level to a value lower than the latest available, for your whole database.  
  
- This activates the older CE, but it makes all queries subject to the older and less accurate CE.  
  
- Further the older level also loses excellent improvements in the query optimizer.  
  
You could use LEGACY_CARDINALITY_ESTIMATION to have the whole database use the older CE, while retaining the improvements in the query optimizer.  
  
The the finest control, you could *force* the SQL system to use the plan that was generated with the older CE during your testing. After you *pin* your preferred plan, you can set your whole database to use the latest compatibility level and CE. The option is elaborated next.  
  
### How to force a particular query plan  
  
The query store gives you different ways that you can force the system to use a particular query plan:  
  
- Execute **sp_query_store_force_plan**.  
  
- In the SSMS, expand your **Query Store** node, right-click **Top Resource Consuming Nodes**, and then click **View Top Resource Consuming Nodes**. The display shows buttons labeled **Force Plan** and **Unforce Plan**.  
  
 For more information about the query store, see [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md).  
  
  
## Descriptions of advance CE  
  
This section describes example queries that benefit from the enhancements implemented in the CE in recent releases. This is background information that does not call for specific action on your part.  
  
### Example A. CE understands maximum value might be higher than when statistics were last gathered  
  
Suppose statistics were last gathered for OrderTable on 2016-04-30, when the maximum OrderAddedDate was 2016-04-30. The CE for compatibility level 120 (and for higher levels) understands that columns in OrderTable which have *ascending* data might have values larger than the maximum recorded by the statistics. This understanding improves the query plan for SQL SELECTs such as the following.  
  
```tsql  
SELECT CustomerId, OrderAddedDate  
    FROM OrderTable  
    WHERE OrderAddedDate >= '2016-05-01';  
```  
  
### Example B. CE understands that filtered predicates on the same table are often correlated  
  
In the following SELECT we see filtered predicates on Make and Model. We intuitively understand that when Make is 'Honda' there is a chance the Model is 'Civic', given that Honda makes the Civic.  
  
The level 120 CE understands there might be a correlation between the two columns on the same table, Make and Model. The CE makes a more accurate estimation of how many rows will be returned by the query, and the query optimizer generates a more optimal plan.  
  
```tsql  
SELECT Model_Year, Purchase_Price  
    FROM dbo.Cars  
    WHERE  
        Make  = 'Honda'  AND  
        Model = 'Civic';  
```  
  
### Example C. CE no longer assumes any correlation between filtered predicates from different tables  
  
With extense new research on modern workloads and actual business data reveal that predicate filters from different tables usually do not correlate with each other. In the following query, the CE assumes there is no correlation between s.type and r.date. Therefore the CE makes a lower estimate of the number of rows returned.  
  
```tsql  
SELECT s.ticket, s.customer, r.store  
    FROM  
                   dbo.Sales    AS s  
        CROSS JOIN dbo.Returns  AS r  
    WHERE  
        s.ticket = r.ticket  AND  
        s.type   = 'toy'     AND  
        r.date   = '2016-05-11';  
```  
  
  
## See Also  
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)  
  

