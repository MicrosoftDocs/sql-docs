---
title: "BASE64_DECODE (Transact-SQL)"
description: "Transact-SQL reference for the BASE64_DECODE function."
author: abledenthusiast
ms.author: aaronpitman
ms.reviewer: wiassaf
ms.date: 05/23/2023
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "BASE64_DECODE"
  - "BASE64_DECODE_TSQL"
helpviewer_keywords:
  - "base64 decode [SQL Server], base64 decode"
  - "BASE64_DECODE function"
  - "base64 decoding [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---

# BASE64_DECODE (Transact SQL)
[!INCLUDE [fabric-se-and-dw](../../includes/applies-to-version/fabric-se-and-dw.md)]

BASE64_DECODE converts a base64 encoded varchar into the corresponding varbinary.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
BASE64_DECODE(expression)
```

## Arguments

#### *expression*

An expression of type varchar (n | max).

## Return types

- **Varbinary(8000)**.
- **Varbinary(max)** if the input is varchar(max).
- If the input expression is null, the output is null.

## Remarks

The encoded string's alphabet must be that of [RFC 4648 Table 1](https://datatracker.ietf.org/doc/html/rfc4648#section-4) and may include padding, though padding is not required. The URL-safe alphabet specified within [RFC 4648 Table 2](https://datatracker.ietf.org/doc/html/rfc4648#section-5) is also accepted. This function ignores whitespace characters: `\n`, `\r`, `\t`, and ` `.

- When the input contains characters not contained within the standard or URL-safe alphabets specified by RFC 4648 the function returns error "Msg 9803, Level 16, State 20, Line 15, Invalid data for type "Base64Decode"". 
- If the data has valid characters, but incorrectly formatted, the function returns error Msg 9803, State 21. 
- If the input contains more than two padding characters or padding characters followed by extra valid input the function returns error Msg 9803, State 23.

## Examples

### A. Standard BASE64_DECODE

In the following example, the base64 encoded string is decoded back into varbinary.

```sql
SELECT BASE64_DECODE ('qQ==');
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
-------------
0xA9

(1 row affected)

```

### B. BASE64_DECODE a standard base64 string

In the following example, the string is base64 decoded. Note the string contains URL-unsafe characters `=` and `/`.

```sql
SELECT BASE64_DECODE('yv7K/g==')
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
------------  
0xCAFECAFE

(1 row affected)
```

### C. BASE64_DECODE varchar url_safe base64 string

In contrast to example B, this example base64 string was encoded using RFC 4648 Table 2 (url_safe), but can be decoded the same way as example B.

```sql
SELECT BASE64_DECODE('yv7K_g')
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
------------  
0xCAFECAFE
(1 row affected)
```

### D. BASE64_DECODE varchar contains characters not in the base64 alphabet

This example contains characters that aren't valid base64 characters.

```sql
SELECT BASE64_DECODE('qQ!!')
```

[!INCLUDE[ssResult_md](../../includes/ssresult-md.md)]

```output
Msg 9803, Level 16, State 20, Line 223
Invalid data for type "Base64Decode".
```

## Next steps

- [BASE64_ENCODE (Transact SQL)](base64-encode-transact-sql.md)