---
title: "ADR cleaner retry timeout (min) configuration option"
description: "Explains the SQL Server instance configuration setting for ADR cleaner retry timeout."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/01/2020"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "ADR cleaner retry timeout (min)"
---
# ADR cleaner retry timeout (min) configuration option

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Introduced in SQL Server 2019.

This configuration setting is required for [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md). The cleaner is the asynchronous process that wakes up periodically and cleans page versions that are not needed.

Occasionally the cleaner runs into issues while acquiring object level locks due to conflicts with user workload during its sweep. It tracks such pages in a separate list. The ADR cleaner retry timeout (default value of 15) controls the amount of time the cleaner would spend exclusively retrying object lock acquisition and cleanup of page before abandoning the sweep. Completion of a sweep with 100% success is essential to keep the growth of aborted transactions in the aborted transactions map. If the separate list cannot be cleaned up in the prescribed timeout, then the current sweep will be abandoned and the next sweep will start.

## Remarks  

The cleaner is single threaded in SQL Server 2019 and so one SQL Server instance can work on one database at a time. If the instance has more than one user database with ADR enabled, then do not increase the timeout to a large value as that could delay cleanup on one database while the retry is happening on another database.

## Examples

The following examples sets the cleaner retry timeout.

```tsql
sp_configure 'show advanced options', 1;  
RECONFIGURE;
GO 
sp_configure 'ADR cleaner retry timeout', 15;  
RECONFIGURE;  
GO  
```  

## See Also  

- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md)