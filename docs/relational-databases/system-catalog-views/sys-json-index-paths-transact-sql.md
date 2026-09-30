---
title: "sys.json_index_paths (Transact-SQL)"
description: "sys.json_index_paths contains the SQL/JSON paths for a JSON index."
author: uc-msft
ms.author: umajay
ms.reviewer: randolphwest
ms.date: 09/15/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.json_index_paths"
  - "json_index_paths"
  - "sys.json_index_paths_TSQL"
  - "json_index_paths_TSQL"
helpviewer_keywords:
  - "sys.json_index_paths catalog view"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# sys.json_index_paths (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

Contains the SQL/JSON paths for a JSON index. If the `CREATE JSON INDEX` statement doesn't define a `sql_json_path`, this catalog view contains one row with a root SQL/JSON path `S` for that index.

| Column name | Data type | Description |
| --- | --- | --- |
| **object_id** | **int** | ID of table with JSON column. |
| **index_id** | **int** | ID of JSON index. |
| **path** | **varchar(8000)** | SQL/JSON path. Collation of the path column is fixed to `Latin1_General_100_BIN2_UTF8`. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata visibility configuration](../security/metadata-visibility-configuration.md).

## Examples

### A. JSON index with no paths

The following example returns JSON indexes for the table `dbo.Customers`. The JSON index is created without specifying any SQL/JSON path.

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
       path
FROM sys.json_index_paths
WHERE object_id = OBJECT_ID('dbo.Customers');
```

### B. JSON index for a specific path

The following example returns JSON indexes for the table `dbo.Customers`. The JSON index is created for a specific SQL/JSON path `$.phone`.

```sql
DROP TABLE IF EXISTS dbo.Customers;

CREATE TABLE dbo.Customers
(
    customer_id INT IDENTITY PRIMARY KEY,
    customer_info JSON NOT NULL
);

CREATE JSON INDEX CustomersJsonIndex
    ON dbo.Customers (customer_info)
    FOR ('$.phone') WITH (OPTIMIZE_FOR_ARRAY_SEARCH = ON);

INSERT INTO dbo.Customers (customer_info)
VALUES ('{"name":"customer1", "email": "customer1@example.com", "phone":["123-456-7890", "234-567-8901"]}');
SELECT object_id,
       index_id,
       path
FROM sys.json_index_paths
WHERE object_id = OBJECT_ID('dbo.Customers');
```

### C. JSON index for multiple paths

The following example returns JSON indexes for the table `dbo.Customers`. The JSON index is created for multiple SQL/JSON paths `$.name` and `$.email`.

```sql
DROP TABLE IF EXISTS dbo.Customers;

CREATE TABLE dbo.Customers
(
    customer_id INT IDENTITY PRIMARY KEY,
    customer_info JSON NOT NULL
);

CREATE JSON INDEX CustomersJsonIndex
    ON dbo.Customers (customer_info)
    FOR ('$.name', '$.email');

INSERT INTO dbo.Customers (customer_info)
VALUES ('{"name":"customer1", "email": "customer1@example.com", "phone":["123-456-7890", "234-567-8901"]}');

SELECT object_id,
       index_id,
       path
FROM sys.json_index_paths
WHERE object_id = OBJECT_ID('dbo.Customers');
```

## Related content

- [Object catalog views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.indexes (Transact-SQL)](sys-indexes-transact-sql.md)
- [sys.json_indexes (Transact-SQL)](sys-json-indexes-transact-sql.md)
- [CREATE JSON INDEX (Transact-SQL)](../../t-sql/statements/create-json-index-transact-sql.md)
