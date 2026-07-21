---
title: Improve the Performance of Full-Text Queries
description: Improve the Performance of Full-Text Queries
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: best-practice
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Improve the performance of full-text queries

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following list of recommendations can help you to improve the performance of full-text queries.  

Hardware resources such as memory, disk speed, CPU speed, and machine architecture can influence the performance of full-text queries.  

- Defragment the index of the base table by using [ALTER INDEX REORGANIZE](../../t-sql/statements/alter-index-transact-sql.md).

- Reorganize the full-text catalog by using [ALTER FULLTEXT CATALOG REORGANIZE](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md). Too many full-text index fragments can lead to substantial degradation in query performance. This statement merges all the fragments into a single larger fragment per index, removing all stale occurrence information. Make sure that you run this statement before performance testing because running this statement causes a master merge of the full-text indexes in that catalog.

- Restrict your choice of full-text key columns to a small column. Although a 900-byte column is supported, use a smaller key column in a full-text index. **int** and **bigint** provide the best performance.

- Using an integer full-text key avoids a join with the **docid** mapping table, which improves query and crawl performance. Additional performance improvements are possible if the full-text key is also the clustered index key.

- Combine multiple [CONTAINS](../../t-sql/queries/contains-transact-sql.md) predicates into one `CONTAINS` predicate. In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can specify a list of columns in the `CONTAINS` query.

- If you only require full-text key or rank information, use [CONTAINSTABLE](../system-functions/containstable-transact-sql.md) or [FREETEXTTABLE](../system-functions/freetexttable-transact-sql.md) instead of `CONTAINS` or `FREETEXT`, respectively.

- To limit results and increase performance, use the *top_n_by_rank* parameter of the `FREETEXTTABLE` and `CONTAINSTABLE` functions. *top_n_by_rank* allows you to recall only the most relevant hits. Use this parameter only if your business scenario doesn't require recalling all possible hits (that is, it doesn't require *total recall*).

  > [!NOTE]  
  > Total recall is typically necessary for legal scenarios, but might be less important than performance for business scenarios such as an e-business.

- Check the full-text query plan to make sure that the appropriate join plan is chosen. Use a join hint or query hint if you have to. If a parameter is used in the full-text query, the first-time value of the parameter determines the query plan. You can use the `OPTIMIZE FOR` [query hint](../../t-sql/queries/hints-transact-sql-query.md) to force the query to compile with the value you want. This helps achieve a deterministic query plan and better performance.

- In full-text search, logical operators specified in `CONTAINSTABLE (AND, OR)` can be implemented either as Transact-SQL joins or inside the full-text execution streaming table-valued functions (STVF). Typically, queries with only one type of logical operator are implemented purely by full-text execution, whereas queries that mix logical operators also possess SQL joins. Implementation of a logical operator inside the full-text execution STVF uses some special index properties that make it much faster than SQL joins. For this reason, where possible, frame your queries using only a single type of logical operator.

- For applications that contain selective relational predicates, queries that use selective relational predicates and unselective full-text predicates might perform best when they're written to use the query optimizer. This approach allows the query optimizer to decide whether it can exploit predicate or range pushdown to produce an effective query plan. This approach is simpler and often more efficient than indexing relational data as full-text data.

## Related resources

[SQL Server 2008 Full-Text Search: Internals and Enhancements](/previous-versions/sql/sql-server-2008/cc721269(v=sql.100))

## Related content

- [sys.dm_fts_memory_buffers (Transact-SQL)](../system-dynamic-management-objects/sys-dm-fts-memory-buffers-transact-sql.md)
- [sys.dm_fts_memory_pools (Transact-SQL)](../system-dynamic-management-objects/sys-dm-fts-memory-pools-transact-sql.md)
