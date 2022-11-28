---
title: "ALTER EXTERNAL RESOURCE POOL (Transact-SQL)"
description: ALTER EXTERNAL RESOURCE POOL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/06/2020"
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: reference
f1_keywords:
  - "ALTER_EXTERNAL_RESOURCE_POOL_TSQL"
helpviewer_keywords:
  - "ALTER EXTERNAL RESOURCE POOL statement"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# ALTER EXTERNAL RESOURCE POOL (Transact-SQL)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Changes a Resource Governor external pool that specifies resources that can be used by external processes. 

::: moniker range="=sql-server-2016"
For [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] in [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], the external pool governs `rterm.exe`, `BxlServer.exe`, and other processes spawned by them.
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
For [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)], the external pool governs `rterm.exe`, `python.exe`, `BxlServer.exe`, and other processes spawned by them.
::: moniker-end

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
```syntaxsql
ALTER EXTERNAL RESOURCE POOL { pool_name | "default" }
[ WITH (
    [ MAX_CPU_PERCENT = value ]
    [ [ , ] MAX_MEMORY_PERCENT = value ]
    [ [ , ] MAX_PROCESSES = value ]
    )
]
[ ; ]
  
<CPU_range_spec> ::=
{ CPU_ID | CPU_ID  TO CPU_ID } [ ,...n ]
```  
::: moniker-end
::: moniker range="=sql-server-2016||=sql-server-2017"
 ```syntaxsql

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
::: moniker-end 

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

{ *pool_name* | "default" }  
Is the name of an existing user-defined external resource pool or the default external resource pool that is created when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.
"default" must be enclosed by quotation marks ("") or brackets ([]) when used with `ALTER EXTERNAL RESOURCE POOL` to avoid conflict with `DEFAULT`, which is a system reserved word.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
MAX_CPU_PERCENT =*value*  
Specifies the maximum average CPU bandwidth that all requests in the external resource pool can receive when there is CPU contention. *value* is an integer. The allowed range for *value* is from 1 through 100.

MAX_MEMORY_PERCENT =*value*  
Specifies the total server memory that can be used by requests in this external resource pool. *value* is an integer. The allowed range for *value* is from 1 through 100.

MAX_PROCESSES =*value*  
Specifies the maximum number of processes allowed for the external resource pool. Specify 0 to set an unlimited threshold for the pool, which is thereafter bound only by computer resources.
::: moniker-end

::: moniker range="=sql-server-2016||=sql-server-2017"
MAX_CPU_PERCENT =*value*  
Specifies the maximum average CPU bandwidth that all requests in the external resource pool can receive when there is CPU contention. *value* is an integer. The allowed range for *value* is from 1 through 100.

AFFINITY {CPU = AUTO | ( \<CPU_range_spec> ) | NUMANODE = (\<NUMA_node_range_spec>)}  
Attach the external resource pool to specific CPUs.

AFFINITY CPU = **(** \<CPU_range_spec> **)** maps the external resource pool to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPUs identified by the given CPU_IDs. When you use AFFINITY NUMANODE = **(** \<NUMA_node_range_spec> **)**, the external resource pool is affinitized to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] physical CPUs that correspond to the given NUMA node or range of nodes.

MAX_MEMORY_PERCENT =*value*  
Specifies the total server memory that can be used by requests in this external resource pool. *value* is an integer. The allowed range for *value* is from 1 through 100.

MAX_PROCESSES =*value*  
Specifies the maximum number of processes allowed for the external resource pool. Specify 0 to set an unlimited threshold for the pool, which is thereafter bound only by computer resources.
::: moniker-end
## Remarks

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] implements the resource pool when you execute the [ALTER RESOURCE GOVERNOR RECONFIGURE](../../t-sql/statements/alter-resource-governor-transact-sql.md) statement.

For general information about resource pools, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md), [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md), and [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md).  

For information specific to the use of external resource pools to govern machine learning jobs, see [Resource governance for machine learning in SQL Server](../../machine-learning/administration/resource-governor.md)...
## Permissions

Requires `CONTROL SERVER` permission.

## Examples

The following statement changes an external pool, restricting the CPU usage to 50 percent and the maximum memory to 25 percent of the available memory on the computer.
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
```sql
ALTER EXTERNAL RESOURCE POOL ep_1
WITH (
    MAX_CPU_PERCENT = 50
    , MAX_MEMORY_PERCENT = 25
);
GO
ALTER RESOURCE GOVERNOR RECONFIGURE;
GO
```
::: moniker-end

::: moniker range="=sql-server-2016||=sql-server-2017"
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
::: moniker-end

## See also

+ [Resource governance for machine learning in SQL Server](../../machine-learning/administration/resource-governor.md)
+ [external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)
+ [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
+ [DROP EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)
+ [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md)
+ [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)
+ [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
+ [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md) 
