---
title: "How FOR JSON escapes special characters and control characters"
description: This article describes how the FOR JSON clause of a SQL Server SELECT statement escapes special characters and represents control characters in the JSON output.
author: jovanpop-msft
ms.author: jovanpop
ms.reviewer: jroth, randolphwest
ms.date: 07/12/2023
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "FOR JSON, special characters"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# How FOR JSON escapes special characters and control characters (SQL Server)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-serverless-pool-only](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-serverless-pool-only.md)]

This article describes how the `FOR JSON` clause of a SQL Server `SELECT` statement escapes special characters and represents control characters in the JSON output.

> [!IMPORTANT]  
> This article describes the built-in support for JSON in Microsoft SQL Server. For general information about escaping and encoding in JSON, see Section 2.5 of the [JSON RFC](https://www.ietf.org/rfc/rfc4627.txt).

## Escape of special characters

If the source data contains special characters, the `FOR JSON` clause escapes them in the JSON output with `\`, as shown in the following table. This escaping occurs both in the names of properties and in their values.

| Special character | Escaped output |
| --- | --- |
| Quotation mark (`"`) | `\"` |
| Backslash (`\`) | `\\`|
| Slash (`/`) | `\/` |
| Backspace | `\b` |
| Form feed | `\f` |
| New line | `\n` |
| Carriage return | `\r` |
| Horizontal tab | `\t` |

## Control characters

If the source data contains control characters, the `FOR JSON` clause encodes them in the JSON output in `\u<code>` format, as shown in the following table.

| **Control character** | **Encoded output** |
| --- | --- |
| **CHAR(0)** | `\u0000` |
| **CHAR(1)** | `\u0001` |
| ... | ... |
| **CHAR(31)** | `\u001f` |

## Example

Here's an example of the `FOR JSON` output for source data that includes both special characters and control characters.

Query:

```sql
SELECT 'VALUE\    /
  "' AS [KEY\/"],
    CHAR(0) AS '0',
    CHAR(1) AS '1',
    CHAR(31) AS '31'
FOR JSON PATH;
```

Result:

```json
[
    {
        "KEY\\\/\"": "VALUE\\    \/\r\n  \"",
        "0": "\u0000",
        "1": "\u0001",
        "31": "\u001f"
    }
]
```

## Next steps

- [Format Query Results as JSON with FOR JSON (SQL Server)](format-query-results-as-json-with-for-json-sql-server.md)
- [SELECT - FOR Clause (Transact-SQL)](../../t-sql/queries/select-for-clause-transact-sql.md)
- [JSON as a bridge between NoSQL and relational worlds](https://channel9.msdn.com/events/DataDriven-SQLServer2016/JSON-as-bridge-betwen-NoSQL-relational-worlds)
