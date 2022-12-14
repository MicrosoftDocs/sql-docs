---
title: "Access check cache Server Configuration Options"
description: "Learn about the access check result cache and the options that control the cache's behavior. See when to change these options in SQL Server."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 09/16/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "access check cache option"
  - "access check cache bucket count"
  - "access check cache quota"
---
# access check cache Server Configuration Options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When database objects are accessed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the access check is cached in an internal structure called the **access check result cache**. On an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that has a high rate of ad hoc query execution, you may notice many access check token entries that have a class of 65535 in the `sys.dm_os_memory_cache_entries` view. Access check token entries that have a class of 65535 represent special cache entries. These cache entries are used for cumulative permission checks for queries.

For example, you may run the following query: `select * from t1 join t2 join t3`. In this case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computes a cumulative permission check for this query. This check determines whether a user has SELECT permissions on `t1`, `t2`, and `t3`. These cumulative permission check results are embedded into an access check token entry and are inserted into the access check cache store with an ID of 65535. If the same user reuses or executes this query multiple times, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reuses the access check token cache entry one time.

To optimize the use of this cache, you should consider using various query parameterization techniques, or convert frequent query patterns to use stored procedures.

The **access check cache bucket count** option controls the number of hash buckets that are used for the access check result cache.

The **access check cache quota** option controls the number of entries that are stored in the access check result cache. When the maximum number of entries is reached, the oldest entries are removed from the access check result cache.

## Remarks

The default values of `0` indicate that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is managing these options. The default values translate to the following internal configurations.

#### [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later

| |Server architecture|Default number of entries|
|---|---|---|
|**Access check cache quota**|x64|1,024|
|**Access check cache bucket count**|x64|256|

#### [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] to [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)]

| &nbsp; |Server architecture|Default number of entries|
|---|---|---|
|**Access check cache quota**|x86|1,024|
| |x64 and IA-64|28,192,048|
|**Access check cache bucket count**|x86|256|
| |x64 and IA-64|2,048|

In rare circumstances, performance may be improved by changing these options. For example, you might want to reduce the size of the access check result cache if too much memory is used. Or, increase the size of the access check result cache if you experience high CPU usage when permissions are recalculated.

We recommend only changing these options when directed by Microsoft Customer Support Services. If you want to change the **access check cache bucket count** and **access check cache quota values**, use a ratio of 1:4. For example, if you change the **access check cache bucket count** value to `512`, you should change the **access check cache quota** value to `2048`.

## See also

- [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
