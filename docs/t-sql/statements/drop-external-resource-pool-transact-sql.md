---
title: "DROP EXTERNAL RESOURCE POOL (Transact-SQL)"
description: DROP EXTERNAL RESOURCE POOL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "08/06/2020"
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: reference
f1_keywords:
  - "DROP EXTERNAL RESOURCE POOL"
  - "DROP_EXTERNAL_RESOURCE_POOL_TSQL"
helpviewer_keywords:
  - "DROP EXTERNAL RESOURCE POOL statement"
dev_langs:
  - "TSQL"
---
# DROP EXTERNAL RESOURCE POOL (Transact-SQL)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Deletes a Resource Governor external resource pool used to define resources for external processes. 

::: moniker range="=sql-server-2016||>=sql-server-linux-ver15"
For [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] in [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)], the external pool governs `rterm.exe`, `BxlServer.exe`, and other processes spawned by them.
::: moniker-end

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
For [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)], the external pool governs `rterm.exe`, `python.exe`, `BxlServer.exe`, and other processes spawned by them.
::: moniker-end

External resource pools are created by using [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md) and modified by using [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md).  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```syntaxsql
DROP EXTERNAL RESOURCE POOL pool_name  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*pool_name*  
The name of the external resource pool to be deleted.  
  
## Remarks

You cannot drop an external resource pool if it contains workload groups.  

You cannot drop the Resource Governor default or internal pools.  

When you are executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  

## Permissions

Requires `CONTROL SERVER` permission.  

## Examples

The following example drops the external resource pool named `ex_pool`.  

```sql
DROP EXTERNAL RESOURCE POOL ex_pool;  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  

## See Also

+ [external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)
+ [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
+ [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
+ [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-group-transact-sql.md)
+ [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)
