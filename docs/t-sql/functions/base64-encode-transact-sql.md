---
title: "BASE64_ENCODE (Transact-SQL)"
description: "BASE64_ENCODE converts the value of a varbinary into a Base64 encoded varchar."
author: abledenthusiast
ms.author: aaronpitman
ms.reviewer: wiassaf
ms.date: 01/13/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "BASE64_ENCODE"
  - "BASE64_ENCODE_TSQL"
helpviewer_keywords:
  - "Base64 encode [SQL Server], Base64 encode"
  - "BASE64_ENCODE function"
  - "Base64 encoding [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =fabric || =fabric-sqldb"
---

# BASE64_ENCODE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

`BASE64_ENCODE` converts the value of a **varbinary** expression into a Base64-encoded **varchar** expression.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
BASE64_ENCODE (expression [ , url_safe ] )
```

## Arguments

#### *expression*

An expression of type **varbinary(*n*)** or **varbinary(max)**.

#### *url_safe*

Optional integer literal or expression, which specifies whether the output of the encode operation should be URL-safe. Any number other than `0` evaluates to true. The default value is `0`.

## Return types

- **varchar(8000)** if the input is **varbinary(*n*)** where `n` <= 6000.
- **varchar(max)** if the input is **varbinary(*n*)** where `n` > 6000.
- **varchar(max)** if the input is **varbinary(max)**.
- If the input expression is `NULL`, the output is `NULL`.

## Remarks

The encoded string uses the alphabet from [RFC 4648 Table 1](https://datatracker.ietf.org/doc/html/rfc4648#section-4) and might include padding. The URL-safe output uses the Base64URL alphabet from [RFC 4648 Table 2](https://datatracker.ietf.org/doc/html/rfc4648#section-5) and doesn't include padding. This function doesn't add any new line characters.

In each case, the database default collation is used. For more information on the supported collations in [!INCLUDE [fabric](../../includes/fabric.md)], see [Tables](/fabric/data-warehouse/tables#collation).

If you set `url_safe` to true, the generated Base64URL string isn't compatible with SQL Server's XML and JSON Base64 decoders.

## Examples

### A. Standard BASE64_ENCODE

The following example returns the Base64 encoded value for the `&copy;` symbol.

```sql
SELECT BASE64_ENCODE(0xA9) AS "Encoded &copy; symbol";
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
qQ==
```

### B. BASE64_ENCODE a string

In the following example, a string is Base64 encoded. You must first cast the string to a **varbinary**.

```sql
SELECT BASE64_ENCODE(CAST ('hello world' AS VARBINARY));
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
aGVsbG8gd29ybGQ=
```

### C. BASE64_ENCODE default vs url_safe

In the following example, the first `SELECT` statement doesn't specify `url_safe`; however, the second `SELECT` statement does specify `url_safe`.

```sql
SELECT BASE64_ENCODE(0xCAFECAFE);
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
yv7K/g==
```

The following example specifies that the output is URL-safe.

```sql
SELECT BASE64_ENCODE(0xCAFECAFE, 1);
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
yv7K_g
```

## Related content

- [BASE64_DECODE (Transact-SQL)](base64-decode-transact-sql.md)
