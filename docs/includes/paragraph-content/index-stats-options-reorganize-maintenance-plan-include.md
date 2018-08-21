

### Index stats options

<!--
This includes/paragraph-content/ file was created when processing vsts sqlbuvsts01 2999014 (5589131).  genemi  2017-07-21

Initially used in:
- relational-databases/maintenance-plans/rebuild-index-task-maintenance-plan.md
- relational-databases/maintenance-plans/reorganize-index-task-maintenance-plan.md
-->


In earlier versions of Microsoft SQL Server it could cause system slowdown to reorganize or rebuild a large index. SQL Server 2016 implemented major performance improvements for these index operations.

Also, in earlier versions the granularity of control was less refined. This caused the system to reorganize or rebuild some indexes even when the indexes were not much fragmented, which was wasteful. Newer controls on the Maintenance Plan user interface (UI) enable you to exclude indexes which do not need to be refreshed, based on index statistics criteria. For this the following dynamic management views (DMVs) of Transact-SQL are used internally:


- [sys.dm_db_index_usage_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md)
- [sys.dm_db_index_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)


 **Scan type**  
 The system must consume resources to gather index statistics. You can choose between consuming relatively less or more resources depending on how much precision you feel is needed for index statistics. The UI offers the following list of precision levels from which you must choose one:


- Fast
- Sampled
- Detailed


 **Optimize index only if:**  
 The UI offers the following tuneable filters that you can use to avoid refreshing indexes which do not yet strongly need refreshing:


- Fragmentation &gt; *(%)*
- Page Count &gt;
- Used in last *(days)*

