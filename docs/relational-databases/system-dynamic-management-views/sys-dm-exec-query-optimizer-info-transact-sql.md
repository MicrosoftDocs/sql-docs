---
title: "sys.dm_exec_query_optimizer_info (Transact-SQL)"
description: sys.dm_exec_query_optimizer_info (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_exec_query_optimizer_info_TSQL"
  - "dm_exec_query_optimizer_info"
  - "sys.dm_exec_query_optimizer_info_TSQL"
  - "sys.dm_exec_query_optimizer_info"
helpviewer_keywords:
  - "sys.dm_exec_query_optimizer_info dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 1d72cef1-22d8-4ae0-91db-6694fe918c9e
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_query_optimizer_info (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns detailed statistics about the operation of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer. You can use this view when tuning a workload to identify query optimization problems or improvements. For example, you can use the total number of optimizations, the elapsed time value, and the final cost value to compare the query optimizations of the current workload and any changes observed during the tuning process. Some counters provide data that is relevant only for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal diagnostic use. These counters are marked as "Internal only."  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_exec_query_optimizer_info`. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Name|Data type|Description|  
|----------|---------------|-----------------|  
|**counter**|**nvarchar(4000)**|Name of optimizer statistics event.|  
|**occurrence**|**bigint**|Number of occurrences of optimization event for this counter.|  
|**value**|**float**|Average property value per event occurrence.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
    
## Remarks  
 **sys.dm_exec_query_optimizer_info** contains the following properties (counters). All occurrence values are cumulative and are set to 0 at system restart. All values for value fields are set to NULL at system restart. All value-column values that specify an average use the occurrence value from the same row as the denominator in the calculation of the average. All query optimizations are measured when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] determines changes to **dm_exec_query_optimizer_info**, including both user- and system-generated queries. Execution of an already-cached plan does not change values in **dm_exec_query_optimizer_info**, only optimizations are significant.  
  
|Counter|Occurrence|Value|  
|-------------|----------------|-----------|  
|optimizations|Total number of optimizations.|Not applicable|  
|elapsed time|Total number of optimizations.|Average elapsed time per optimization of an individual statement (query), in seconds.|  
|final cost|Total number of optimizations.|Average estimated cost for an optimized plan in internal cost units.|  
|trivial plan|Internal only|Internal only|  
|tasks|Internal only|Internal only|  
|no plan|Internal only|Internal only|  
|search 0|Internal only|Internal only|  
|search 0 time|Internal only|Internal only|  
|search 0 tasks|Internal only|Internal only|  
|search 1|Internal only|Internal only|  
|search 1 time|Internal only|Internal only|  
|search 1 tasks|Internal only|Internal only|  
|search 2|Internal only|Internal only|  
|search 2 time|Internal only|Internal only|  
|search 2 tasks|Internal only|Internal only|  
|gain stage 0 to stage 1|Internal only|Internal only|  
|gain stage 1 to stage 2|Internal only|Internal only|  
|timeout|Internal only|Internal only|  
|memory limit exceeded|Internal only|Internal only|  
|insert stmt|Number of optimizations that are for INSERT statements.|Not applicable|  
|delete stmt|Number of optimizations that are for DELETE statements.|Not applicable|  
|update stmt|Number of optimizations that are for UPDATE statements.|Not applicable|  
|contains subquery|Number of optimizations for a query that contains at least one subquery.|Not applicable|  
|unnest failed|Internal only|Internal only|  
|tables|Total number of optimizations.|Average number of tables referenced per query optimized.|  
|hints|Number of times some hint was specified. Hints counted include: JOIN, GROUP, UNION and FORCE ORDER query hints, FORCE PLAN set option, and join hints.|Not applicable|  
|order hint|Number of times a force order hint was specified.|Not applicable|  
|join hint|Number of times the join algorithm was forced by a join hint.|Not applicable|  
|view reference|Number of times a view has been referenced in a query.|Not applicable|  
|remote query|Number of optimizations where the query referenced at least one remote data source, such as a table with a four-part name or an OPENROWSET result.|Not applicable|  
|maximum DOP|Total number of optimizations.|Average effective MAXDOP value for an optimized plan. By default, effective MAXDOP is determined by the **max degree of parallelism** server configuration option, and may be overridden for a specific query by the value of the MAXDOP query hint.|  
|maximum recursion level|Number of optimizations in which a MAXRECURSION level greater than 0 has been specified with the query hint.|Average MAXRECURSION level in optimizations where a maximum recursion level is specified with the query hint.|  
|indexed views loaded|Internal only|Internal only|  
|indexed views matched|Number of optimizations where one or more indexed views have been matched.|Average number of views matched.|  
|indexed views used|Number of optimizations where one or more indexed views are used in the output plan after being matched.|Average number of views used.|  
|indexed views updated|Number of optimizations of a DML statement that produce a plan that maintains one or more indexed views.|Average number of views maintained.|  
|dynamic cursor request|Number of optimizations in which a dynamic cursor request has been specified.|Not applicable|  
|fast forward cursor request|Number of optimizations in which a fast-forward cursor request has been specified.|Not applicable|  
|merge stmt|Number of optimizations that are for MERGE statements.|Not applicable|  
  
## Examples  
  
### A. Viewing statistics on optimizer execution  
 What are the current optimizer execution statistics for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]?  
  
```  
SELECT * FROM sys.dm_exec_query_optimizer_info;  
```  
  
### B. Viewing the total number of optimizations  
 How many optimizations are performed?  
  
```  
SELECT occurrence AS Optimizations FROM sys.dm_exec_query_optimizer_info  
WHERE counter = 'optimizations';  
```  
  
### C. Average elapsed time per optimization  
 What is the average elapsed time per optimization?  
  
```  
SELECT ISNULL(value,0.0) AS ElapsedTimePerOptimization  
FROM sys.dm_exec_query_optimizer_info WHERE counter = 'elapsed time';  
```  
  
### D. Fraction of optimizations that involve subqueries  
 What fraction of optimized queries contained a subquery?  
  
```  
SELECT (SELECT CAST (occurrence AS float) FROM sys.dm_exec_query_optimizer_info WHERE counter = 'contains subquery') /  
       (SELECT CAST (occurrence AS float)   
        FROM sys.dm_exec_query_optimizer_info WHERE counter = 'optimizations')  
        AS ContainsSubqueryFraction;  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Execution Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)  
  
