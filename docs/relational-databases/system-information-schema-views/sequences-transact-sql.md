---
title: "SEQUENCES (Transact-SQL)"
description: SEQUENCES returns one row for each sequence that can be accessed by the current user in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/04/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SEQUENCES"
  - "SEQUENCES_TSQL"
helpviewer_keywords:
  - "SEQUENCES view"
  - "INFORMATION_SCHEMA.SEQUENCES view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# SEQUENCES (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns one row for each sequence that can be accessed by the current user in the current database.

To retrieve information from these views, specify the fully qualified name of `INFORMATION_SCHEMA.<view_name>`.

| Column name | Data type | Description |
| --- | --- | --- |
| `SEQUENCE_CATALOG` | **nvarchar(128)** | Sequence qualifier |
| `SEQUENCE_SCHEMA` | **nvarchar(128)** | Name of schema that contains the sequence |
| `SEQUENCE_NAME` | **nvarchar(128)** | Sequence name |
| `DATA_TYPE` | **nvarchar(128)** | The sequence data type |
| `NUMERIC_PRECISION` | **tinyint** | The precision of the sequence |
| `NUMERIC_PRECISION_RADIX` | **smallint** | Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, `NULL` is returned. |
| `NUMERIC_SCALE` | **int** | Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, `NULL` is returned. |
| `START_VALUE` | **int** | Specifies the first value returned by the sequence object. |
| `MINIMUM_VALUE` | **int** | The bounds for the sequence object. The default minimum value for a new sequence object is the minimum value of the data type of the sequence object. This is `0` for the **tinyint** data type, and a negative number for all other data types. |
| `MAXIMUM_VALUE` | **int** | The bounds for the sequence object. The default maximum value for a new sequence object is the maximum value of the data type of the sequence object. |
| `INCREMENT` | **int** | Value used to increment (or decrement if negative) the value of the sequence object for each call to the `NEXT VALUE FOR` function. If the increment is a negative value, the sequence object is descending; otherwise, it's ascending. The increment can't be 0. The default increment for a new sequence object is `1`. |
| `CYCLE_OPTION` | **int** | Property that specifies whether the sequence object should restart from the minimum value (or maximum for descending sequence objects), or throw an exception when its minimum or maximum value is exceeded. The default cycle option for new sequence objects is `NO CYCLE`. |
| `DECLARED_DATA_TYPE` | **int** | The data type for user-defined data type. |
| `DECLARED_DATA_PRECISION` | **int** | The precision for user-defined data type. |
| `DECLARED_NUMERIC_SCALE` | **int** | The numeric scale for user-defined data type. |

## Examples

The following example returns information about the schemas in the test database:

```sql
SELECT * FROM test.INFORMATION_SCHEMA.SEQUENCES;
```

## Related content

- [System Information Schema Views (Transact-SQL)](system-information-schema-views-transact-sql.md)
- [sys.sequences (Transact-SQL)](../system-catalog-views/sys-sequences-transact-sql.md)
