---
title: "BASE64_DECODE (Transact-SQL)"
description: "BASE64_DECODE converts a Base64 encoded varchar into the corresponding varbinary."
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
  - "BASE64_DECODE"
  - "BASE64_DECODE_TSQL"
helpviewer_keywords:
  - "Base64 decode [SQL Server], Base64 decode"
  - "BASE64_DECODE function"
  - "Base64 decoding [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =fabric || =fabric-sqldb"
---

# BASE64_DECODE (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricse-fabricdw-fabricsqldb.md)]

`BASE64_DECODE` converts a Base64-encoded **varchar** expression into the corresponding **varbinary** expression.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
BASE64_DECODE ( expression )
```

## Arguments

#### *expression*

An expression of type **varchar(*n*)** or **varchar(max)**.

## Return types

- **varbinary(8000)** if the input is **varchar(*n*)**.
- **varbinary(max)** if the input is **varchar(max)**.
- If the input expression is `NULL`, the output is `NULL`.

## Remarks

The encoded string's alphabet must be that of [RFC 4648 Table 1](https://datatracker.ietf.org/doc/html/rfc4648#section-4) and might include padding, though padding isn't required. The URL-safe alphabet specified within [RFC 4648 Table 2](https://datatracker.ietf.org/doc/html/rfc4648#section-5) is also accepted. This function ignores whitespace characters: `\n`, `\r`, `\t`, and ` `.

- When the input contains characters not contained within the standard or URL-safe alphabets specified by RFC 4648, the function returns the following error:

  ```output
  Msg 9803, Level 16, State 20, Line 15, Invalid data for type "Base64Decode"
  ```

- If the data has valid characters but is incorrectly formatted, the function returns error `Msg 9803, State 21`.

- If the input contains more than two padding characters or padding characters followed by extra valid input, the function returns error `Msg 9803, State 23`.

## Examples

### A. Standard BASE64_DECODE

In the following example, the Base64 encoded string is decoded back into **varbinary**.

```sql
SELECT BASE64_DECODE('qQ==');
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
0xA9
```

### B. BASE64_DECODE a standard Base64 string

In the following example, the string is Base64 decoded. Note the string contains URL-unsafe characters `=` and `/`.

```sql
SELECT BASE64_DECODE('yv7K/g==');
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
0xCAFECAFE
```

### C. BASE64_DECODE varchar url_safe Base64 string

Unlike example B, this example uses RFC 4648 Table 2 (`url_safe`) to encode the Base64 string. However, you can decode it the same way as example B.

```sql
SELECT BASE64_DECODE('yv7K_g');
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
0xCAFECAFE
```

### D. BASE64_DECODE varchar contains characters not in the Base64 alphabet

This example contains characters that aren't valid Base64 characters.

```sql
SELECT BASE64_DECODE('qQ!!');
```

[!INCLUDE [ssResult_md](../../includes/ssresult-md.md)]

```output
Msg 9803, Level 16, State 20, Line 223
Invalid data for type "Base64Decode".
```

## Related content

- [BASE64_ENCODE (Transact-SQL)](base64-encode-transact-sql.md)
