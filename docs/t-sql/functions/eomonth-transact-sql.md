---
title: "EOMONTH (Transact-SQL)"
description: Th EOMONTH function returns the last day of the month containing a specified date, with an optional offset.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/09/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "EOMONTH"
  - "EOMONTH_TSQL"
helpviewer_keywords:
  - "EOMONTH function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# EOMONTH (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns the last day of the month containing a specified date, with an optional offset.

> [!TIP]  
> You can use [DATETRUNC](datetrunc-transact-sql.md) to calculate the start of the month.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EOMONTH ( start_date [ , month_to_add ] )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *start_date*

A date expression that specifies the date for which to return the last day of the month.

#### *month_to_add*

An optional integer expression that specifies the number of months to add to *start_date*.

If the *month_to_add* argument has a value, then `EOMONTH` adds the specified number of months to *start_date*, and then returns the last day of the month for the resulting date. If this addition overflows the valid range of dates, then `EOMONTH` raises an error.

## Return types

**date**

## Remarks

The `EOMONTH` function can remote to instances running [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions. It can't remote to instances with a version before [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)].

## Examples

### A. EOMONTH with explicit datetime type

```sql
DECLARE @date DATETIME = '12/1/2024';

SELECT EOMONTH(@date) AS Result;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result
------------
2024-12-31
```

### B. EOMONTH with string parameter and implicit conversion

```sql
DECLARE @date VARCHAR(255) = '12/1/2024';

SELECT EOMONTH(@date) AS Result;
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Result
------------
2024-12-31
```

### C. EOMONTH with and without the month_to_add parameter

The values shown in these result sets reflect an execution date between and including `12/01/2024` and `12/31/2024`.

```sql
DECLARE @date DATETIME = '2024-12-31';

SELECT EOMONTH(@date) AS 'This Month';
SELECT EOMONTH(@date, 1) AS 'Next Month';
SELECT EOMONTH(@date, -1) AS 'Last Month';
GO
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
This Month
-----------------------
2024-12-31

Next Month
-----------------------
2025-01-31

Last Month
-----------------------
2024-11-30
```

## Related content

- [What are the SQL database functions?](functions.md)
- [SYSDATETIME (Transact-SQL)](sysdatetime-transact-sql.md)
- [SYSDATETIMEOFFSET (Transact-SQL)](sysdatetimeoffset-transact-sql.md)
