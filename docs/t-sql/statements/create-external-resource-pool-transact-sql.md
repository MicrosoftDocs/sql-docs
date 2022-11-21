---
title: "CREATE EXTERNAL RESOURCE POOL (Transact-SQL)"
description: CREATE EXTERNAL RESOURCE POOL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/06/2020"
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: reference
f1_keywords:
  - "CREATE EXTERNAL RESOURCE POOL"
  - "CREATE EXTERNAL_RESOURCE_POOL_TSQL"
  - "EXTERNAL RESOURCE POOL"
  - "EXTERNAL_RESOURCE_POOL_TSQL"
  - "EXTERNAL RESOURCE"
  - "EXTERNAL_RESOURCE_TSQL"
helpviewer_keywords:
  - "CREATE EXTERNAL RESOURCE POOL statement"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# CREATE EXTERNAL RESOURCE POOL (Transact-SQL)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Creates an external pool to define resources for external processes. A resource pool represents a subset of the physical resources (memory and CPUs) of a Database Engine instance. A Resource Governor can distribute server resources among resource pools, up to a maximum of 64 pools.

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
CREATE EXTERNAL RESOURCE POOL pool_name  
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
CREATE EXTERNAL RESOURCE POOL pool_name  
[ WITH (  
    [ MAX_CPU_PERCENT = value ]  
    [ [ , ] AFFINITY CPU =    
            {  
                AUTO   
              | ( <cpu_range_spec> )   
              | NUMANODE = ( <NUMA_node_id> )   
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

*pool_name*  
Is the user-defined name for the external resource pool. *pool_name* is alphanumeric and can be up to 128 characters. This argument must be unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and must fulfill the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
MAX_CPU_PERCENT =*value*  
The maximum average CPU bandwidth for all requests that the external resource pool can receive when there's CPU contention. *value* is an integer. The allowed range for *value* is from 1 through 100.


MAX_MEMORY_PERCENT =*value*  
Specifies the total server memory that can be used by requests in this external resource pool. *value* is an integer. The allowed range for *value* is from 1 through 100.

MAX_PROCESSES =*value*  
The maximum number of processes allowed for the external resource pool. 0 = unlimited threshold for the pool, which is thereafter bound only by computer resources.
::: moniker-end

::: moniker range="=sql-server-2016||=sql-server-2017"
MAX_CPU_PERCENT =*value*  
The maximum average CPU bandwidth for all requests that the external resource pool can receive when there's CPU contention. *value* is an integer. The allowed range for *value* is from 1 through 100.

AFFINITY {CPU = AUTO | ( <CPU_range_spec>) | NUMANODE = (\<NUMA_node_range_spec>)}
Attach the external resource pool to specific CPUs.

AFFINITY CPU = **(** <CPU_range_spec> **)** maps the external resource pool to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CPUs identified by the given CPU_IDs.

When you use AFFINITY NUMANODE = **(\<NUMA_node_range_spec> **)**, the external resource pool is affinitized to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] physical CPUs that correspond to the given NUMA node or range of nodes. 

MAX_MEMORY_PERCENT =*value*  
Specifies the total server memory that can be used by requests in this external resource pool. *value* is an integer. The allowed range for *value* is from 1 through 100.

MAX_PROCESSES =*value*  
The maximum number of processes allowed for the external resource pool. 0 = unlimited threshold for the pool, which is thereafter bound only by computer resources.
::: moniker-end

## Remarks

The [!INCLUDE[ssDE](../../includes/ssde-md.md)] implements the resource pool when you execute the [ALTER RESOURCE GOVERNOR RECONFIGURE](../../t-sql/statements/alter-resource-governor-transact-sql.md) statement.

For general information about resource pools, see [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md), [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md), and [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md).

For information specific to managing external resource pools that are used for machine learning, see [Resource governance for machine learning in SQL Server](../../machine-learning/administration/resource-governor.md). 

## Permissions

Requires `CONTROL SERVER` permission.

## Examples

The external pool has restricted CPU usage to 75 percent. The maximum memory is 30 percent of the available memory on the computer.

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
```sql
CREATE EXTERNAL RESOURCE POOL ep_1
WITH (  
    MAX_CPU_PERCENT = 75
    , MAX_MEMORY_PERCENT = 30
);
GO
ALTER RESOURCE GOVERNOR RECONFIGURE;
GO
```
::: moniker-end

::: moniker range="=sql-server-2016||=sql-server-2017"
```sql
CREATE EXTERNAL RESOURCE POOL ep_1
WITH (  
    MAX_CPU_PERCENT = 75
    , AFFINITY CPU = AUTO
    , MAX_MEMORY_PERCENT = 30
);
GO
ALTER RESOURCE GOVERNOR RECONFIGURE;
GO
```
::: moniker-end

## See also

+ [external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)
+ [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md)
+ [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
+ [DROP EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-external-resource-pool-transact-sql.md)
+ [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md)
+ [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)
+ [Resource Governor Resource Pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
+ [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md)
+ [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)
+ [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)
