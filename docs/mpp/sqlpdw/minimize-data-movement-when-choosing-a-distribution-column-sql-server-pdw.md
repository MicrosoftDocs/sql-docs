---
title: "Minimize Data Movement When Choosing a Distribution Column (SQL Server PDW)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2978efed-5b76-4955-b52d-5836455e093d
caps.latest.revision: 4
author: BarbKess
---
# Minimize Data Movement When Choosing a Distribution Column (SQL Server PDW)
This article describes best practices for choosing a distribution column that minimizes data movement for a SQL Server PDW distributed table. Choosing a good distribution column is an important aspect of achieving storage efficiency and highly performant queries. This is possibly the most important design choice you will make in SQL Server PDW.  
  
The best choice for a distribution column depends on the data characteristics and the queries you intend to process. The goal is to choose a distribution column that minimizes data skew among the compute nodes and minimizes data movement during query processing. These are both important and sometimes competing considerations.  
  
## Best Practices  
The following best practices describe how to choose a distribution column that minimizes data movement for your workloads.  
  
### 1. Choose distribution keys that will be join keys  
Choose a distribution column that is one of the join keys when queries will be joining multiple large distributed tables. This improves query performance by keeping the processing local to each distribution instead of shuffling rows amongst the distributions before processing the query. If possible, use sample queries to determine which join keys they contain.  
  
If you have a workload that is joining multiple large historical tables together (e.g. fact table to fact table joins), you will avoid data movement if you can join the distributed tables on their distribution keys.  
  
### 2. If possible, change an incompatible join to a compatible join by adding a predicate that uses the distribution key  
In some cases, the results of a query are not altered by adding another predicate that uses the distribution key. Although this predicate is not necessary logically, it is necessary for performance. If the distribution key is in the join condition, data shuffling is not needed and each distribution can perform its portion of the join locally.  
  
For example, imagine two tables, A and B, distributed on Postal Code, but joined together on a unique key, Address_ID. Adding the additional predicate A.Postal_Code = B.Postal_Code will not change the meaning of the query because postal code depends on the Address_ID. In other words, the following two predicates return the same results:  
  
-   WHERE ( A.Address_ID = B.Address_ID )  
  
-   WHERE ( A.Address_ID = B.Address_ID_ID AND A.Postal_Code = B.Postal_Code )  
  
However, the second predicate will perform more efficiently because it does not require data shuffling.  
  
### 3. Design your schema to encourage compatible aggregations  
The best performance for aggregations occurs when the distribution column is used in the GROUP BY clause (compatible aggregation). The GROUP BY operation is performed locally on all the distributions, without requiring a shuffle move among the distributions or a partition move so the Control node can perform aggregations.  
  
In contrast, the worst aggregation performance occurs when the distribution column is not used in the GROUP BY clause (incompatible aggregation). Each distribution moves all the rows that are part of the GROUP BY operation to the Control node. This leads to a bottleneck because the Control node must perform all the GROUP BY operations.  
  
### 4. Optimize for a key frequently used in Distinct Count  
For example, if `SELECT COUNT (DISTINCT Customer_ID) FROM T WHERE` ... is used frequently, there will be performance benefits if Customer_ID is the distribution key.  
  
### 5. Choose a column with static values  
Remember, that you cannot update values in the distribution column. If you need to update a value, you can delete the row and insert a new row with the updated value.  
  
