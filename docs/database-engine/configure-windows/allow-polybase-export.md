---
title: "Server configuration: allow polybase export"
description: Set the configuration option to allow PolyBase export in SQL Server settings.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mikeray, randolphwest
ms.date: 07/18/2024
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
---

# Server configuration: allow polybase export

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

The `allow polybase export` server configuration option allows the export of data out of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. The functionality of this configuration option is different starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] compared to previous versions:

- In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, the [CREATE EXTERNAL TABLE AS SELECT](../../t-sql/statements/create-external-table-as-select-transact-sql.md) (CETAS) statement requires that you enable `allow polybase export` using `sp_configure`. This setting allows for data to be exported to a CSV or Parquet file. For examples, see [Use CREATE EXTERNAL TABLE AS SELECT exporting data as parquet](../../t-sql/statements/create-external-table-as-select-transact-sql.md#d-use-create-external-table-as-select-exporting-data-as-parquet).

- In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, enabling `allow polybase export` allows Hadoop to export data out of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to an external table. For more information, see [PolyBase connectors](../../relational-databases/polybase/polybase-guide.md#polybase-connectors) and [Export data](../../relational-databases/polybase/polybase-queries.md#export-data).

The possible values are described in the following table:

| Value | Meaning |
| --- | --- |
| `0` (default) | Disabled |
| `1` | Enabled |

This change takes effect immediately.

## Examples

The following example enables this setting.

```sql
EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
EXEC sp_configure 'allow polybase export', 1;
GO
RECONFIGURE;
GO
```

## Related content

- [Exporting data](../../relational-databases/polybase/polybase-configure-hadoop.md#exporting-data)
- [Data virtualization with PolyBase in SQL Server](../../relational-databases/polybase/polybase-guide.md)
- [CREATE EXTERNAL TABLE AS SELECT (CETAS) (Transact-SQL)](../../t-sql/statements/create-external-table-as-select-transact-sql.md)
