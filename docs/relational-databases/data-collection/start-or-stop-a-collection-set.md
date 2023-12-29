---
title: "Start or stop a collection set"
description: Start or stop a collection set with SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "collection sets [SQL Server], stopping"
  - "collection sets [SQL Server], starting"
---
# Start or stop a collection set

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to start or stop a collection set in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

## Limitations

Data collector stored procedures and catalog views are stored in the `msdb` database.

Unlike regular stored procedures, the parameters for data collector stored procedures are strictly typed and don't support automatic data type conversion. If these parameters aren't called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.

## Prerequisites

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent must be started.

## Recommendations

To obtain information about collection sets, query the [syscollector_collection_sets](../system-catalog-views/syscollector-collection-sets-transact-sql.md) catalog view.

## Permissions

Requires membership in the **dc_operator** fixed database role. If the collection set doesn't have a proxy account, membership in the **sysadmin** fixed server role is required.

## Use SQL Server Management Studio

### Start a collection set

1. In Object Explorer, expand the **Management** node, expand **Data Collection**, and then expand **System Data Collection Sets**.

1. Right-click the collection set that you want to start, and then select **Start Data Collection Set**.

   A message box displays the results of this action, and a green arrow on the icon for the collection set indicates that the collection set has started.

### Stop a collection set

1. In Object Explorer, expand the **Management** node, expand **Data Collection**, and then expand **System Data Collection Sets**.

1. Right-click the collection set that you want to stop, and then select **Stop Data Collection Set**.

   A message box displays the results of this action, and a red circle on the icon for the collection set indicates that the collection set has stopped.

## Use Transact-SQL

### Start a collection set

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses [sp_syscollector_start_collection_set](../system-stored-procedures/sp-syscollector-start-collection-set-transact-sql.md) to start the collection set that has the ID of `1`.

```sql
USE msdb;
GO
EXEC sp_syscollector_start_collection_set @collection_set_id = 1;
```

#### Stop a collection set

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses [sp_syscollector_stop_collection_set](../system-stored-procedures/sp-syscollector-stop-collection-set-transact-sql.md) to stop the collection set that has the ID of `1`.

```sql
USE msdb;
GO
EXEC sp_syscollector_stop_collection_set @collection_set_id = 1;
```

## Related content

- [Data Collector Views (Transact-SQL)](../system-catalog-views/data-collector-views-transact-sql.md)
- [Data collection](data-collection.md)
