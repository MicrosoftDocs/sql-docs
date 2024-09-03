---
title: "DATETIMEFROMPARTS (Transact-SQL)"
description: DATETIMEFROMPARTS returns a datetime value for the specified date and time arguments.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/21/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATETIMEFROMPARTS_TSQL"
  - "DATETIMEFROMPARTS"
helpviewer_keywords:
  - "DATETIMEFROMPARTS function"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# DATETIMEFROMPARTS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This function returns a **datetime** value for the specified date and time arguments. For more information about valid ranges, see [datetime](../data-types/datetime-transact-sql.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DATETIMEFROMPARTS ( year , month , day , hour , minute , seconds , milliseconds )
```

## Arguments

#### *year*

An integer expression that specifies a year.

#### *month*

An integer expression that specifies a month.

#### *day*

An integer expression that specifies a day.

#### *hour*

An integer expression that specifies hours.

#### *minute*

An integer expression that specifies minutes.

#### *seconds*

An integer expression that specifies seconds.

#### *milliseconds*

An integer expression that specifies milliseconds.

## Return types

**datetime**

## Remarks

`DATETIMEFROMPARTS` returns a fully initialized **datetime** value. `DATETIMEFROMPARTS` raises an error if at least one required argument has an invalid value. `DATETIMEFROMPARTS` returns `NULL` if at least one required argument has a `NULL` value.

This function is capable of being remoted to [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] servers and later versions. It isn't remoted to servers running [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and earlier versions.

## Examples

```sql
SELECT DATETIMEFROMPARTS ( 2010, 12, 31, 23, 59, 59, 0 ) AS Result;
```

[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
2010-12-31 23:59:59.000
```

## Related content

- [datetime (Transact-SQL)](../data-types/datetime-transact-sql.md)
