---
title: "Understanding Compatible and Incompatible Joins (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 512590ed-f800-4997-880f-0a49405644fd
caps.latest.revision: 11
author: BarbKess
---
# Understanding Compatible and Incompatible Joins (SQL Server PDW)
Describes which SQL Server PDW joins require data movement by explaining which joins are compatible and incompatible. SQL Server PDW can achieve high performance by performing joins in parallel on the Compute nodes and distributions, and then combining the results on the Control node. To improve query performance, tables and queries need to be designed so that, whenever possible, it is not necessary to move data prior to the join.  
  
## Contents  
  
-   [Background](#Background)  
  
-   [Compatible Joins](#CompatibleJoins)  
  
-   [Incompatible Joins](#IncompatibleJoins)  
  
## <a name="Background"></a>Background  
As part of the shared nothing architecture, every Compute node and distribution must be able to perform its parallel join operations by using local data. Before running parallel joins, SQL Server PDW copies data as necessary among the Compute nodes to ensure that each distribution can perform its portion of the join with local data. Moving data takes time and usually has a negative impact on query performance. Therefore, it is important to design tables and queries to minimize data movement for joins.  
  
## <a name="CompatibleJoins"></a>Compatible Joins  
A compatible join in SQL Server PDW, is a join that does not require data movement before each Compute node or distribution performs its portion of the join. To improve query performance, one design goal is to minimize data movement by using compatible joins when possible.  
  
|Join type|Left Table|Right Table|Compatibility|  
|-------------|--------------|---------------|-----------------|  
|All join types|Replicated|Replicated|Compatible - no data movement required.|  
|Inner Join<br /><br />Right Outer Join<br /><br />Cross Join|Replicated|Distributed|Compatible – no data movement required.|  
|Inner Join<br /><br />Left Outer Join<br /><br />Cross Join|Distributed|Replicated|Compatible – no data movement required.|  
|All join types, except cross joins, can be compatible. See the Compatibility column for details.|Distributed|Distributed|Compatible – no data movement required if the join predicate is an equality join and if the predicate joins two distributed columns that have matching data types.<br /><br />For example, if table A is distributed on column a and table B is distributed on column b, and both a and b have matching data types, the following join is compatible: `SELECT * FROM A JOIN B ON(A.a=B.b)`<br /><br />SQL Server PDW analyzes the logic for some conjunctive predicates to see if they are compatible. For example, the following join is compatible: `SELECT * FROM A JOIN B ON(A.a=B.b AND A.a = B.b+1)`<br /><br />Cross Joins are always incompatible.|  
  
The following example shows the query plan for a compatible inner join between two distributed tables, FactSurveyResponse and DimCustomer. Both tables have `CustomerKey( int, NOT NULL )` as the distribution column, and the join is performed on the `CustomerKey` column.  
  
```  
USE AdventureWorksPDW2012;  
EXPLAIN  
SELECT *  
FROM dbo.FactSurveyResponse AS s JOIN dbo.DimCustomer AS c  
    ON (s.CustomerKey = c.CustomerKey);  
```  
  
## <a name="IncompatibleJoins"></a>Incompatible Joins  
An incompatible join is a join that requires data movement before each Compute node or distribution can perform its portion of the parallel join. SQL Server PDW automates the data movement by incorporating the data movement operations into the query plan.  
  
Data movement operations take extra time and storage, and can negatively impact query performance. Therefore, one of the design goals for SQL Server PDW databases and queries is to minimize data movement whenever possible by using compatible joins, and by avoiding incompatible joins.  
  
|Join type|Left Table|Right Table|Compatibility|  
|-------------|--------------|---------------|-----------------|  
|Left Outer Join<br /><br />Full Outer Join|Replicated|Distributed|Incompatible – requires data movement before the join.|  
|Right Outer Join<br /><br />Full Outer Join|Distributed|Replicated|Incompatible – requires data movement before the join.|  
|See Compatibility column for details.|Distributed|Distributed|Incompatible – requires data movement unless the join predicate is an equality join and the join predicate joins two distributed columns that have matching data types.<br /><br />Cross joins are always incompatible.|  
  
The following example shows the query plan for an incompatible join between two distributed tables, DimCustomer and ProspectiveBuyer. The join is performed on BirthDate, which is not a distribution column in either table. Thus, a SHUFFLE_MOVE is required.  
  
```  
USE AdventureWorksPDW2012;  
EXPLAIN  
SELECT c.BirthDate, c.CustomerKey, c.FirstName, c.LastName, p.ProspectiveBuyerKey, p.FirstName, p.LastName  
FROM dbo.DimCustomer AS c JOIN dbo.ProspectiveBuyer AS p  
    ON (c.BirthDate = p.BirthDate)  
where c.LastName = p.LastName  
order by c.BirthDate;  
```  
  
In the resulting query plan, there is a SHUFFLE_MOVE operation which indicates that data movement is required before each distribution can run the join.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
