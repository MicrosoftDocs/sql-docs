---
title: AT TIME ZONE (Transact-SQL)
description: AT TIME ZONE Converts an input date to the corresponding datetimeoffset value in the target time zone.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 06/12/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "AT TIME ZONE"
  - "AT_TIME_ZONE_TSQL"
helpviewer_keywords:
  - "AT TIME ZONE function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =fabric"
---
# AT TIME ZONE (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Converts an *inputdate* to the corresponding *datetimeoffset* value in the target time zone. When *inputdate* is provided without offset information, the function applies the offset of the time zone assuming that *inputdate* is in the target time zone. If *inputdate* is provided as a *datetimeoffset* value, then `AT TIME ZONE` clause converts it into the target time zone using the time zone conversion rules.

`AT TIME ZONE` implementation relies on a Windows mechanism to convert **datetime** values across time zones.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
inputdate AT TIME ZONE timezone
```

## Arguments

#### *inputdate*

An expression that can be resolved to a **smalldatetime**, **datetime**, **datetime2**, or **datetimeoffset** value.

#### *timezone*

Name of the destination time zone. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] relies on time zones that are stored in the Windows Registry. Time zones installed on the computer are stored in the following registry hive: `KEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones`. A list of installed time zones is also exposed through the [sys.time_zone_info](../../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md) view.

For more information about time zones for SQL Server on Linux, see [Configure the time zone for SQL Server 2022 on Linux](../../linux/sql-server-linux-configure-time-zone.md).

## Return types

Returns the data type of **datetimeoffset**.

## Return value

The **datetimeoffset** value in the target time zone.

## Remarks

`AT TIME ZONE` applies specific rules for converting input values in **smalldatetime**, **datetime**, and **datetime2** data types that fall into an interval affected by a DST change:

- When the clock's set ahead, there's a gap in local time equal to the duration of the clock adjustment. This duration is usually 1 hour, but it can be 30 or 45 minutes, depending on time zone. Points in time that are in this gap are converted with the offset *after* DST change.

  ```sql
  /*
    Moving to DST in "Central European Standard Time" zone:
    offset changes from +01:00 -> +02:00
    Change occurred on March 27th, 2022 at 02:00:00.
    Adjusted local time became 2022-03-27 03:00:00.
  */

  --Time before DST change has standard time offset (+01:00)
  SELECT CONVERT(DATETIME2(0), '2022-03-27T01:01:00', 126)
  AT TIME ZONE 'Central European Standard Time';
  --Result: 2022-03-27 01:01:00 +01:00

  /*
    Adjusted time from the "gap interval" (between 02:00 and 03:00)
    is moved 1 hour ahead and presented with the summer time offset
    (after the DST change)
  */
  SELECT CONVERT(DATETIME2(0), '2022-03-27T02:01:00', 126)
  AT TIME ZONE 'Central European Standard Time';
  --Result: 2022-03-27 03:01:00 +02:00
  --Time after 03:00 is presented with the summer time offset (+02:00)
  SELECT CONVERT(DATETIME2(0), '2022-03-27T03:01:00', 126)
  AT TIME ZONE 'Central European Standard Time';
  --Result: 2022-03-27 03:01:00 +02:00
  ```

- When the clock is set back, then 2 hours of local time are overlapped onto one hour. In that case, points in time that belong to the overlapped interval are presented with the offset *before* the clock change:

    ```sql
    /*
        Moving back from DST to standard time in
        "Central European Standard Time" zone:
        offset changes from +02:00 -> +01:00.
        Change occurred on October 30th, 2022 at 03:00:00.
        Adjusted local time became 2022-10-30 02:00:00
    */

    --Time before the change has DST offset (+02:00)
    SELECT CONVERT(DATETIME2(0), '2022-10-30T01:01:00', 126)
    AT TIME ZONE 'Central European Standard Time';
    --Result: 2022-10-30 01:01:00 +02:00

    /*
      Time from the "overlapped interval" is presented with DST offset (before the change)
    */
    SELECT CONVERT(DATETIME2(0), '2022-10-30T02:00:00', 126)
    AT TIME ZONE 'Central European Standard Time';
    --Result: 2022-10-30 02:00:00 +02:00

    --Time after 03:00 is regularly presented with the standard time offset (+01:00)
    SELECT CONVERT(DATETIME2(0), '2022-10-30T03:01:00', 126)
    AT TIME ZONE 'Central European Standard Time';
    --Result: 2022-10-30 03:01:00 +01:00
    ```

Since some information (such as timezone rules) is maintained outside of [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)] and are subject to occasional change, the `AT TIME ZONE` function is classed as nondeterministic.

While **datetimeoffset** isn't supported in data warehousing in [!INCLUDE [fabric](../../includes/fabric.md)], `AT TIME ZONE` can still be used with **datetime2**, as in the following example.

## Examples

### A. Add target time zone offset to datetime without offset information

Use `AT TIME ZONE` to add offset based on time zone rules when you know that the original **datetime** values are provided in the same time zone:

```sql
USE AdventureWorks2022;
GO

SELECT SalesOrderID, OrderDate,
    OrderDate AT TIME ZONE 'Pacific Standard Time' AS OrderDate_TimeZonePST
FROM Sales.SalesOrderHeader;
```

### B. Convert values between different time zones

The following example converts values between different time zones. The `OrderDate` values are **datetime** and aren't stored with an offset, but are known to be Pacific Standard Time. The first step is to assign the known offset and then convert to the new time zone:

```sql
USE AdventureWorks2022;
GO

SELECT SalesOrderID, OrderDate,
    --Assign the known offset only
    OrderDate AT TIME ZONE 'Pacific Standard Time' AS OrderDate_TimeZonePST,
    --Assign the known offset, then convert to another time zone
    OrderDate AT TIME ZONE 'Pacific Standard Time' AT TIME ZONE 'Central European Standard Time' AS OrderDate_TimeZoneCET
FROM Sales.SalesOrderHeader;
```

You can also substitute in a local variable containing the time zone:

```sql
USE AdventureWorks2022;
GO

DECLARE @CustomerTimeZone nvarchar(128) = 'Central European Standard Time';

SELECT SalesOrderID, OrderDate,
    --Assign the known offset only
    OrderDate AT TIME ZONE 'Pacific Standard Time' AS OrderDate_TimeZonePST,
    --Assign the known offset, then convert to another time zone
    OrderDate AT TIME ZONE 'Pacific Standard Time' AT TIME ZONE @CustomerTimeZone AS OrderDate_TimeZoneCustomer
FROM Sales.SalesOrderHeader;
```

### C. Query temporal tables using a specific time zone

The following example selects data from a temporal table using Pacific Standard Time.

```sql
USE AdventureWorks2022;
GO

DECLARE @ASOF DATETIMEOFFSET;

SET @ASOF = DATEADD(MONTH, -1, GETDATE()) AT TIME ZONE 'UTC';

-- Query state of the table a month ago projecting period
-- columns as Pacific Standard Time
SELECT BusinessEntityID,
    PersonType,
    NameStyle,
    Title,
    FirstName,
    MiddleName,
    ValidFrom AT TIME ZONE 'Pacific Standard Time'
FROM Person.Person_Temporal
FOR SYSTEM_TIME AS OF @ASOF;
```

## Related content

- [Date and time types](../data-types/date-and-time-types.md)
- [Date and time data types and functions (Transact-SQL)](../functions/date-and-time-data-types-and-functions-transact-sql.md)
