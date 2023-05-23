---
title: "BASE64_ENCODE (Transact-SQL)"
description: "Transact-SQL reference for the BASE64_ENCODE function."
author: abledenthusiast
ms.author: aaronpitman
ms.reviewer: wiassaf
ms.date: 05/23/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BASE64_ENCODE"
  - "BASE64_ENCODE_TSQL"
helpviewer_keywords:
  - "base64 encode [SQL Server], base64 encode"
  - "BASE64_ENCODE function"
  - "base64 encoding [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---

# BASE64_ENCODE (Transact SQL)
[!INCLUDE [fabric-se-and-dw](../../includes/applies-to-version/fabric-se-and-dw.md)]

BASE64_ENCODE converts the value of a varbinary into a base64 encoded varchar.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
BASE64_ENCODE (expression [, url_safe])
```

## Arguments

#### *expression*

An expression of type varbinary (n | max)
  
#### *url_safe*  

Optional integer literal or expression, which specifies whether the output of the encode operation should be URL-safe. Any number other than `0` evaluates to true. The default value is `0`.

## Return types

- **Varchar(8000)**
- **Varchar(max)** if the input is varbinary(max)
- **Varchar(max)** if the input is varchar(n) where n > 6000
- If the input expression is `null`, the output is `null`.

## Remarks

The encoded string alphabet is that of [RFC 4648 Table 1](https://datatracker.ietf.org/doc/html/rfc4648#section-4) and may add padding. The URL-safe output uses the base64url alphabet of [RFC 4648 Table 2](https://datatracker.ietf.org/doc/html/rfc4648#section-5) and doesn't add padding. This function doesn't add any new line characters.

In each case, the database default collation is used. For more information on the supported collations in [!INCLUDE [fabric](../../includes/fabric.md)], see [Tables](/fabric/data-warehouse/tables#collation).

If `url_safe` is true, the base64url string that is generated is incompatible with SQL Server's XML and JSON base64 decoders.

## Examples

### A. Standard BASE64_ENCODE

In the following example, simple varbinary is base64 encoded.

```sql
SELECT Base64_Encode(0xA9) as "Encoded &copy; symbol";
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
------------  
qQ==
(1 row affected)
```

### B. BASE64_ENCODE a string

In the following example, a string is base64 encoded. The string must first be casted to a varbinary.

```sql
SELECT BASE64_ENCODE (CAST ('hello world' as varbinary))
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
------------  
aGVsbG8gd29ybGQ=
(1 row affected)
```

### C. BASE64_ENCODE default vs url_safe

In the following example, the first select doesn't specify `url_safe`, however the second select does specify `url_safe`.

```sql
SELECT BASE64_ENCODE(0xCAFECAFE)
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
------------  
yv7K/g==
(1 row affected)
```

The following example specifies that the output is URL-safe.

```sql
SELECT BASE64_ENCODE(0xCAFECAFE, 1);
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
------------  
yv7K_g
(1 row affected)
```

## Next steps

- [BASE64_DECODE (Transact SQL)](base64-decode-transact-sql.md)