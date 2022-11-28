---
title: "ADR Preallocation Factor configuration option"
description: "Explains the SQL Server instance configuration setting for ADR Preallocation Factor."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/01/2020"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "ADR Preallocation Factor"
---
# ADR Preallocation Factor configuration option

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Introduced in SQL Server 2019.

This configuration setting is required for [accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md).

Accelerated Database Recovery (ADR) maintains versions of data for recovery purposes. These versions are generated as part of various data manipulation language (DML) operations. Versions are stored in an internal table called persistent version store (PVS). 

## Remarks  

Performance can degrade if pages are allocated for the PVS table as part of foreground user DML operations. To address this, there is a background thread which pre-allocates pages and keeps them readily available for DML transactions. Performance is best when the background thread preallocates enough pages and the percentage of foreground PVS allocations is close to 0. The error log contains entires with the tag `PreallocatePVS` if the percentage goes high and is affecting performance.

The number of pages the background thread pre-allocates is based on various workload heuristics, but largely allocates pages in chunks of 512 pages. The ADR preallocation factor is a multiple of the chunk. By default, the factor is 4. This means it preallocates 2048 pages at once when required. 

While the background thread takes workload patterns into consideration, this factor can be increased if necessary to improve performance.

> [!CAUTION]
> If PVS preallocation is increased too much, it will contend with other allocations in the system and might actually reduce overall performance
>
> Before you modify this setting, test the overall performance of the system.

## Examples  

The following example sets the preallocation factor to 4.

```tsql
sp_configure 'show advanced options', 1;
RECONFIGURE;
GO 
sp_configure 'ADR Preallocation Factor', 4;
RECONFIGURE;
GO
```

## See Also  

- [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)
- [Accelerated database recovery](../../relational-databases/accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](../../relational-databases/accelerated-database-recovery-management.md)