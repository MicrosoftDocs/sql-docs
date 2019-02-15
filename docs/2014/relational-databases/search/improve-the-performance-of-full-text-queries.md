---
title: "Improve the Performance of Full-Text Queries | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
ms.assetid: 0658dc74-25eb-4486-bbd6-e85c1f92c272
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Improve the Performance of Full-Text Queries
  The following is a list of recommendations that will help to improve the performance of full-text queries.  
  
 The performance of full-text queries is also influenced by hardware resources, such as memory, disk speed, CPU speed, and machine architecture.  
  
-   Defragment the index of the base table by using [ALTER INDEX REORGANIZE](/sql/t-sql/statements/alter-index-transact-sql).  
  
-   Reorganize the full-text catalog by using [ALTER FULLTEXT CATALOG REORGANIZE](/sql/t-sql/statements/alter-fulltext-catalog-transact-sql). Make sure that you do this before performance testing because running this statement causes a master merge of the full-text indexes in that catalog.  
  
-   Restrict your choice of full-text key columns to a small column. Although a 900-byte column is supported, we recommend using a smaller key column in a full-text index. `int` and `bigint` provide the best performance.  
  
-   Using an integer full-text key avoids a join with the **docid** mapping table. Therefore, an integer full-text key improves query performance by an order of magnitude and improves crawl performance. Additional performance benefits might result if the full-text key is also the clustered index key.  
  
-   Combine multiple [CONTAINS](/sql/t-sql/queries/contains-transact-sql) predicates into one CONTAINS predicate. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] you can specify a list of columns in the CONTAINS query.  
  
-   If you only require full-text key or rank information, use [CONTAINSTABLE](/sql/relational-databases/system-functions/containstable-transact-sql) or [FREETEXTTABLE](/sql/relational-databases/system-functions/freetexttable-transact-sql) instead of CONTAINS or FREETEXT, respectively.  
  
-   To limit results and increase performance, use the *top_n_by_rank* parameter of the FREETEXTTABLE and CONTAINSTABLE functions. *top_n_by_rank* allows you to recall only the most relevant hits. Use this parameter only if your business scenario does not require recalling all possible hits (that is, it does not require *total recall*).  
  
    > [!NOTE]  
    >  Total recall is typically necessary for legal scenarios but might be less important than performance for business scenarios such as an e-business.  
  
-   Check the full-text query plan to make sure that the appropriate join plan is chosen. Use a join hint or query hint if you have to. If a parameter is used in the full-text query, the first-time value of the parameter determines the query plan. You can use the OPTIMIZE FOR [query hint](/sql/t-sql/queries/hints-transact-sql-query) to force the query to compile with the value you want. This helps achieve a deterministic query plan and better performance.  
  
-   Too many full-text index fragments in the full-text index, can lead to substantial degradation in query performance. To reduce the number of fragments, reorganize the full-text catalog by using the REORGANIZE option of the [ALTER FULLTEXT CATALOG](/sql/t-sql/statements/alter-fulltext-catalog-transact-sql)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement. This statement essentially merges all the fragments into a single larger fragment and removes all obsolete entries from the full-text index.  
  
-   In  full-text search, logical operators specified in CONTAINSTABLE (AND, OR) can be implemented either as SQL joins or inside the full-text execution streaming table-valued functions (STVF). Typically, queries with only one type of logical operators are implemented purely by full-text execution, whereas queries that mix logical operators also possess SQL joins. Implementation of a logical operator inside the full-text execution STVF uses some special index properties that make it much faster than SQL joins. For this reason, we recommend that, where possible, you frame queries using only a single type of logical operator.  
  
-   For applications that contain selective-relation predications, queries that use selective relational predicates and unselective full-text predicates might perform best when they are written to use the query optimizer. This allows the query optimizer to decide whether it can exploit predicate or range pushdown to produce an effective query plan. This approach is simpler and often more efficient than indexing relational data as full-text data.  
  
## Related Resources  
 [SQL Server 2008 Full-Text Search: Internals and Enhancements](https://go.microsoft.com/fwlink/?LinkId=129544)  
  
## See Also  
 [sys.dm_fts_memory_buffers &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-memory-buffers-transact-sql)   
 [sys.dm_fts_memory_pools &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-memory-pools-transact-sql)  
  
  
