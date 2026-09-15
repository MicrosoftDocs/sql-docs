---
title: "sys.json_indexes (Transact-SQL)"
description: sys.json_indexes contains a row per json index.
author: uc-msft
ms.author: umajay
ms.reviewer: randolphwest
ms.date: 09/15/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.json_indexes"
  - "json_indexes"
  - "sys.json_indexes_TSQL"
  - "json_indexes_TSQL"
helpviewer_keywords:
  - "sys.json_indexes catalog view"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.json_indexes (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Contains a row per json index.

| Column name | Data type | Description |
| --- | --- | --- |
| **\<inherited columns>** | | Inherits columns from [sys.indexes](sys-indexes-transact-sql.md). |
| **optimize_for_array_search** | **bit** | Indicates that array search optimization is enabled for JSON index. 1 = Array search optimization is enabled for JSON index.<br />0 = Array search optimization isn't enabled for JSON indexes. Default is 0. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. JSON index without array search optimization

The following example returns JSON indexes for the table `dbo.Customers`. The JSON index is created without the array search optimization option enabled.

```sql
DROP TABLE IF EXISTS dbo.Customers;

CREATE TABLE dbo.Customers
(
    customer_id INT IDENTITY PRIMARY KEY,
    customer_info JSON NOT NULL
);

CREATE JSON INDEX CustomersJsonIndex
    ON dbo.Customers (customer_info);

INSERT INTO dbo.Customers (customer_info)
VALUES ('{"name":"customer1", "email": "customer1@example.com", "phone":["123-456-7890", "234-567-8901"]}');

SELECT object_id,
       index_id,
       optimize_for_array_search
FROM sys.json_indexes AS ji
WHERE object_id = OBJECT_ID('dbo.Customers');
```

### B. JSON index with array search optimization

The following example returns JSON indexes for the table `dbo.Customers`. The JSON index is created with the array search optimization option enabled.

```sql
DROP TABLE IF EXISTS dbo.Customers;

CREATE TABLE dbo.Customers
(
    customer_id INT IDENTITY PRIMARY KEY,
    customer_info JSON NOT NULL
);

CREATE JSON INDEX CustomersJsonIndex
    ON dbo.Customers (customer_info) WITH (OPTIMIZE_FOR_ARRAY_SEARCH = ON);

INSERT INTO dbo.Customers (customer_info)
VALUES ('{"name":"customer1", "email": "customer1@example.com", "phone":["123-456-7890", "234-567-8901"]}');

SELECT object_id,
       index_id,
       optimize_for_array_search
FROM sys.json_indexes AS ji
WHERE object_id = OBJECT_ID('dbo.Customers');
```

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.indexes (Transact-SQL)](sys-indexes-transact-sql.md)
- [sys.json_index_paths (Transact-SQL)](sys-json-index-paths-transact-sql.md)
- [CREATE JSON INDEX (Transact-SQL)](../../t-sql/statements/create-json-index-transact-sql.md)
