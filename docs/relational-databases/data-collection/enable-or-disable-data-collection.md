---
title: "Enable or disable data collection"
description: Enable or disable data collection with SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "data collector [SQL Server], disabling"
  - "data collector [SQL Server], enabling"
---
# Enable or disable data collection

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to enable or disable data collection in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Permissions

Requires membership in the **dc_admin** or **dc_operator** (with `EXECUTE` permission) fixed database role to execute this procedure.

## Use SQL Server Management Studio

#### Enable the data collector

1. In Object Explorer, expand the **Management** node.

1. Right-click **Data Collection**, and then select **Enable Data Collection**.

#### Disable the data collector

1. In Object Explorer, expand the **Management** node.

1. Right-click **Data Collection**, and then select **Disable Data Collection**.

## Use Transact-SQL

#### Enable the data collector

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses [sp_syscollector_enable_collector](../system-stored-procedures/sp-syscollector-enable-collector-transact-sql.md) to enable the data collector.

```sql
USE msdb;
GO
EXEC dbo.sp_syscollector_enable_collector;
```

#### Disable the data collector

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses [sp_syscollector_disable_collector](../system-stored-procedures/sp-syscollector-disable-collector-transact-sql.md) to disable the data collector.

```sql
USE msdb;
GO
EXEC dbo.sp_syscollector_disable_collector;
```

## Related content

- [Data collection](data-collection.md)
- [System stored procedures (Transact-SQL)](../system-stored-procedures/system-stored-procedures-transact-sql.md)
