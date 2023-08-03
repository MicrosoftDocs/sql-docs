---
title: "Allow PolyBase export configuration option"
description: Set `allow polybase export` configuration option in SQL Server settings
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mikeray
ms.date: 12/13/2022
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
---

# Set allow polybase export configuration option

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

The `allow polybase export` server configuration option allows the export of data out of SQL Server. The functionality of this configuration option is different starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] compared to previous versions:

- Introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the [CREATE EXTERNAL TABLE AS SELECT](../../t-sql/statements/create-external-table-as-select-transact-sql.md) (CETAS) statement requires the option `allow polybase export` to be enabled by using `sp_configure`. This allows for data to be exported to a CSV or Parquet file. For examples, see [Use CREATE EXTERNAL TABLE AS SELECT exporting data as parquet](../../t-sql/statements/create-external-table-as-select-transact-sql.md#d-use-create-external-table-as-select-exporting-data-as-parquet).
- In previous versions of SQL Server, enabling `allow polybase export` allows HADOOP to export data out of SQL Server to an external table. For more information, see [PolyBase connectors](../../relational-databases/polybase/polybase-guide.md#polybase-connectors) and [Export data](../../relational-databases/polybase/polybase-queries.md#export-data).

The possible values are described in the following table:

| Value | Meaning                                |
|-------|----------------------------------------|
| 0     | Disabled, the default setting.         |
| 1     | Enabled                                |

This change takes effect immediately.

## Example

The following example enables this setting.

```sql
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'allow polybase export', 1;
GO
RECONFIGURE;
GO
```

## Next steps

- [Exporting data](../../relational-databases/polybase/polybase-configure-hadoop.md#exporting-data)
- [Introducing data virtualization with PolyBase](../../relational-databases/polybase/polybase-guide.md)
- [CREATE EXTERNAL TABLE AS SELECT (Transact-SQL)](../../t-sql/statements/create-external-table-as-select-transact-sql.md)