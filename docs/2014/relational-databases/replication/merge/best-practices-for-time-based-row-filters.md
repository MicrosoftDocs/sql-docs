---
title: "Best Practices for Time-Based Row Filters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "best practices"
ms.assetid: 773c5c62-fd44-44ab-9c6b-4257dbf8ffdb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Best Practices for Time-Based Row Filters
  Users of applications often require a time-based subset of data from a table. For instance, a salesperson might require data for orders in the last week, or an event planner might require data for events in the upcoming week. In many cases, applications use queries containing the `GETDATE()` function to accomplish this. Consider the following row filter statement:  
  
```  
WHERE SalesPersonID = CONVERT(INT,HOST_NAME()) AND OrderDate >= (GETDATE()-6)  
```  
  
 With a filter of this type, it is usually assumed that two things always occur when the Merge Agent runs: rows that satisfy this filter are replicated to Subscribers; and rows that no longer satisfy this filter are cleaned up at Subscribers. (For more information about filtering with `HOST_NAME()`, see [Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md).) However, merge replication only replicates and cleans up data that has changed since the last synchronization, regardless of how you define a row filter for that data.  
  
 For merge replication to process a row, the data in the row must satisfy the row filter, and it must have changed since the last synchronization. In the case of the **SalesOrderHeader** table, **OrderDate** is entered when a row is inserted. Rows are replicated to the Subscriber as expected because the insert is a data change. However, if there are rows at the Subscriber that no longer satisfy the filter (they are for orders older than seven days), they are not removed from the Subscriber unless they were updated for some other reason.  
  
 The case of the event planner further highlights the issue with this type of filtering. Consider the following filter for an **Events** table:  
  
```  
WHERE EventCoordID = CONVERT(INT,HOST_NAME()) AND EventDate <= (GETDATE()+6)  
```  
  
 For a table that contains events, inserts might be made well ahead of the event date. If the insert for an event in the coming week was made a month ago and the row was not updated for another reason, the row is not replicated to the Subscriber even if it satisfies the row filter.  
  
 In addition, depending on how the publication is configured, merge replication evaluates filters at different times:  
  
-   If a publication uses precomputed partitions (the default), filters are evaluated when a row is inserted or updated.  
  
-   If the publication does not use precomputed partitions, filters are evaluated when the Merge Agent runs.  
  
 For more information about precomputed partitions, see [Optimize Parameterized Filter Performance with Precomputed Partitions](parameterized-filters-optimize-for-precomputed-partitions.md). The time at which the filter is evaluated affects what data satisfies the filter. For example, if a publication uses precomputed partitions, and you synchronize data every two days, the subset of data for the salesperson could include rows up to two days older than expected.  
  
## Recommendations for Using Time-Based Row Filters  
 The following method provides a robust and straightforward approach to filtering based on time:  
  
-   Add a column to the table of data type `bit`. This column is used to indicate whether a row should be replicated.  
  
-   Use a row filter that references the new column rather than a time-based column.  
  
-   Create a SQL Server Agent job (or a job scheduled through another mechanism) that updates the column before the Merge Agent is scheduled to run.  
  
 This approach addresses the shortcomings of using `GETDATE()` or another time-based method and avoids the problem of having to determine when filters are evaluated for partitions. Consider the following example of an **Events** table:  
  
|**EventID**|**EventName**|**EventCoordID**|**EventDate**|**Replicate**|  
|-----------------|-------------------|----------------------|-------------------|-------------------|  
|1|Reception|112|2006-10-04|1|  
|2|Dinner|112|2006-10-10|0|  
|3|Party|112|2006-10-11|0|  
|4|Wedding|112|2006-10-12|0|  
  
 The row filter for this table would then look like this:  
  
```  
WHERE EventCoordID = CONVERT(INT,HOST_NAME()) AND Replicate = 1  
```  
  
 The SQL Server Agent job could execute [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements similar to the following before each Merge Agent run:  
  
```  
UPDATE Events SET Replicate = 0 WHERE Replicate = 1  
GO  
UPDATE Events SET Replicate = 1 WHERE EventDate <= GETDATE()+6  
GO  
```  
  
 The first line resets the **Replicate** column to **0**, and the second line sets the column to **1** for events that occur in the next seven days. If this [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement runs on 10/07/2006, the table is updated to:  
  
|**EventID**|**EventName**|**EventCoordID**|**EventDate**|**Replicate**|  
|-----------------|-------------------|----------------------|-------------------|-------------------|  
|1|Reception|112|2006-10-04|0|  
|2|Dinner|112|2006-10-10|1|  
|3|Party|112|2006-10-11|1|  
|4|Wedding|112|2006-10-12|1|  
  
 The events for the next week are now flagged as being ready to replicate. The next time the Merge Agent runs for the subscription that event coordinator 112 uses, rows 2, 3, and 4 will be downloaded to the Subscriber and row 1 will be removed from the Subscriber.  
  
## See Also  
 [GETDATE &#40;Transact-SQL&#41;](/sql/t-sql/functions/getdate-transact-sql)   
 [Implement Jobs](../../../ssms/agent/implement-jobs.md)   
 [Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md)  
  
  
