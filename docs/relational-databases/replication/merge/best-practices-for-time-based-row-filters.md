---
title: Best Practices for Time-Based Row Filters
description: Time-based row filters in Merge Replication often miss rows because GETDATE() filters only process changed data. Learn a reliable bit-column approach to fix it.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: best-practice
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "best practices"
---
# Best practices for time-based row filters
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Users of applications often need a time-based subset of data from a table. For example, a salesperson might need data for orders in the last week, or an event planner might need data for events in the upcoming week. In many cases, applications use queries containing the **GETDATE()** function to accomplish this task. Consider the following row filter statement:  
  
```  
WHERE SalesPersonID = CONVERT(INT,HOST_NAME()) AND OrderDate >= (GETDATE()-6)  
```  
  
 When you use a filter of this type, assume that two things always occur when the Merge Agent runs: the process replicates rows that satisfy this filter to Subscribers, and it cleans up rows that no longer satisfy this filter at Subscribers. (For more information about filtering with **HOST_NAME()**, see [Parameterized Row Filters](../../../relational-databases/replication/merge/parameterized-filters-parameterized-row-filters.md).) However, Merge Replication only replicates and cleans up data that changed since the last synchronization, regardless of how you define a row filter for that data.  
  
 For Merge Replication to process a row, the data in the row must satisfy the row filter, and it must have changed since the last synchronization. In the case of the **SalesOrderHeader** table, you enter **OrderDate** when you insert a row. The process replicates rows to the Subscriber as expected because the insert is a data change. However, if there are rows at the Subscriber that no longer satisfy the filter (they're for orders older than seven days), the process doesn't remove them from the Subscriber unless you update them for some other reason.  
  
 The case of the event planner further highlights the issue with this type of filtering. Consider the following filter for an **Events** table:  
  
```  
WHERE EventCoordID = CONVERT(INT,HOST_NAME()) AND EventDate <= (GETDATE()+6)  
```  
  
 For a table that contains events, you might make inserts well ahead of the event date. If you made the insert for an event in the coming week a month ago and didn't update the row for another reason, the process doesn't replicate the row to the Subscriber even if it satisfies the row filter.  
  
 In addition, depending on how you configure the publication, Merge Replication evaluates filters at different times:  
  
-   If a publication uses precomputed partitions (the default), the process evaluates filters when you insert or update a row.  
  
-   If the publication doesn't use precomputed partitions, the process evaluates filters when the Merge Agent runs.  
  
 For more information about precomputed partitions, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md). The time at which the process evaluates the filter affects what data satisfies the filter. For example, if a publication uses precomputed partitions, and you synchronize data every two days, the subset of data for the salesperson could include rows up to two days older than expected.  
  
## Recommendations for using time-based row filters  
 The following method provides a robust and straightforward approach to filtering based on time:  
  
-   Add a column to the table with the data type **bit**. Use this column to indicate whether a row should be replicated.  
  
-   Use a row filter that references the new column rather than a time-based column.  
  
-   Create a SQL Server Agent job (or a job scheduled through another mechanism) that updates the column before the Merge Agent is scheduled to run.  
  
 This approach addresses the shortcomings of using **GETDATE()** or another time-based method and avoids the problem of having to determine when filters are evaluated for partitions. Consider the following example of an **Events** table:  
  
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
  
 The events for the next week are now flagged as being ready to replicate. The next time the Merge Agent runs for the subscription that event coordinator 112 uses, rows 2, 3, and 4 are downloaded to the Subscriber and row 1 is removed from the Subscriber.  
  
## Related content

- [GETDATE (Transact-SQL)](../../../t-sql/functions/getdate-transact-sql.md)
- [Implement Jobs](/ssms/agent/implement-jobs)
- [Parameterized Filters - Parameterized Row Filters](parameterized-filters-parameterized-row-filters.md)
