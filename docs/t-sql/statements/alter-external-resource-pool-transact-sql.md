---
title: "ALTER EXTERNAL RESOURCE POOL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/13/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_EXTERNAL_RESOURCE_POOL_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER EXTERNAL RESOURCE POOL statement"
ms.assetid: 634c327d-971b-49ba-b8a2-e243a04040db
author: HeidiSteen
ms.author: heidist
manager: cgronlund
---
# ALTER EXTERNAL RESOURCE POOL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]
**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

Changes a Resource Governor external pool that specifies resources that can be used by external processes. 

+ For [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] in [!INCLUDE[sssql15-md](../../includes/sssql15-md.md)], the external pool governs `rterm.exe`, `BxlServer.exe`, and other processes spawned by them.

+ For [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)] in SQL Server 2017, the external pool governs the R processes listed for the previous version, as well as `python.exe`, `BxlServer.exe`, and other processes spawned by them.

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```sql
ALTER EXTERNAL RESOURCE POOL { pool_name | "default" }
[ WITH (
    [ MAX_CPU_PERCENT = value ]
    [ [ , ] AFFINITY CPU =
            {
                AUTO
              | ( <cpu_range_spec> )
              | NUMANODE = (( <NUMA_node_id> )
            } ]   
    [ [ , ] MAX_MEMORY_PERCENT = value ]
    [ [ , ] MAX_PROCESSES = value ]
    )
]
[ ; ]
  
<CPU_range_spec> ::=
{ CPU_ID | CPU_ID  TO CPU_ID } [ ,...n ]
```  
  
## Arguments

{ *pool_name* | "default" }  
Is the name of an existing user-defined external resource pool or the default external resource pool that is created when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.
"default" must be enclosed by quotation marks ("") or brackets ([]) when used with `ALTER EXTERNAL RESOURCE POOL` to avoid conflict with `DEFAULT`, which is a system reserved word.


MAX_CPU_PERCENT =*value*  
Specifies the maximum average CPU bandwidth that all requests in the external resource pool can receive when there is CPU contention. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.


AFFINITY {CPU = AUTO | ( \<CPU_range_spec> ) | NUMANODE = (\<NUMA_node_range_spec>)}  
Attach the external resource pool to specific CPUs. The default value is AUTO.

AFFINITY CPU = **(** \<CPU_range_spec> **)** maps the external resource pool to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPUs identified by the given CPU_IDs. When you use AFFINITY NUMANODE = **(** \<NUMA_node_range_spec> **)**, the external resource pool is affinitized to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] physical CPUs that correspond to the given NUMA node or range of nodes.


MAX_MEMORY_PERCENT =*value*  
Specifies the total server memory that can be used by requests in this external resource pool. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.


MAX_PROCESSES =*value*  
Specifies the maximum number of processes allowed for the external resource pool. Specify 0 to set an unlimited threshold for the pool, which is thereafter bound only by computer resources. The default is 0.

## Remarks

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] implements the resource pool when you execute the [ALTER RESOURCE GOVERNOR RECONFIGURE](../../t-sql/statements/alter-resource-governor-transact-sql.md) statement.

For general information about resource pools, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md), [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md), and [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md).  

For information specific to the use of external resource pools to govern machine learning jobs, see [Resource governance for machine learning in SQL Server](../../advanced-analytics/r/resource-governance-for-r-services.md)...
## Permissions

Requires `CONTROL SERVER` permission.

## Examples

The following statement changes an external pool, restricting the CPU usage to 50 percent and the maximum memory to 25 percent of the available memory on the computer.
  
```sql
ALTER EXTERNAL RESOURCE POOL ep_1
WITH (
    MAX_CPU_PERCENT = 50
    , AFFINITY CPU = AUTO
    , MAX_MEMORY_PERCENT = 25
);
GO
ALTER RESOURCE GOVERNOR RECONFIGURE;
GO
```

## See also

[Resource governance for machine learning in SQL Server](../../advanced-analytics/r/resource-governance-for-r-services.md)

[external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)

[CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)

[DROP EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)

[ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md)

[CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)

[Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)

[ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md) 
