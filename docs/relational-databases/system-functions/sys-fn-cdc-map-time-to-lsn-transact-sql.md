---
title: "sys.fn_cdc_map_time_to_lsn (Transact-SQL)"
description: "sys.fn_cdc_map_time_to_lsn (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fn_cdc_map_time_to_lsn"
  - "fn_cdc_map_time_to_lsn_TSQL"
  - "sys.fn_cdc_map_time_to_lsn_TSQL"
  - "fn_cdc_map_time_to_lsn"
helpviewer_keywords:
  - "fn_cdc_map_time_to_lsn"
  - "sys.fn_cdc_map_time_to_lsn"
dev_langs:
  - "TSQL"
---
# sys.fn_cdc_map_time_to_lsn (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the log sequence number (LSN) value from the `start_lsn` column in the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) system table for the specified time. You can use this function to systematically map datetime ranges into the LSN-based range needed by the change data capture enumeration functions [cdc.fn_cdc_get_all_changes_<capture_instance>](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md) and [cdc.fn_cdc_get_net_changes_<capture_instance>](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md) to return data changes within that range.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.fn_cdc_map_time_to_lsn ( '<relational_operator>', tracking_time )

<relational_operator> ::=
    { largest less than
    | largest less than or equal
    | smallest greater than
    | smallest greater than or equal
    }
```

## Arguments

#### '<relational_operator>' { largest less than | largest less than or equal | smallest greater than | smallest greater than or equal }

Used to identify a distinct LSN value in within the `cdc.lsn_time_mapping` table with an associated `tran_end_time` that satisfies the relation when compared to the *tracking_time* value.

*relational_operator* is **nvarchar(30)**.

#### *tracking_time*  
Is the datetime value to match against. *tracking_time* is **datetime**.

## Return type

**binary(10)**

## Remarks

To understand how the `sys.fn_cdc_map_time_to_lsn` function can be used to map **datetime** ranges to LSN ranges, consider the following scenario.

Assume that a consumer wants to extract change data on a daily basis. That is, the consumer wants only changes for a given day up to and including midnight. The lower bound of the time range would be up to but not including midnight of the previous day. The upper bound would be up to and including midnight of the given day. The following example shows how the function `sys.fn_cdc_map_time_to_lsn` can be used to systematically map this time-based range into the LSN-based range needed by the change data capture enumeration functions to return all changes within that range.

```sql
DECLARE @begin_time DATETIME,
    @end_time DATETIME,
    @begin_lsn BINARY (10),
    @end_lsn BINARY (10);

SET @begin_time = '2007-01-01 12:00:00.000';
SET @end_time = '2007-01-02 12:00:00.000';

SELECT @begin_lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than', @begin_time);

SELECT @end_lsn = sys.fn_cdc_map_time_to_lsn('largest less than or equal', @end_time);

SELECT *
FROM cdc.fn_cdc_get_net_changes_HR_Department(@begin_lsn, @end_lsn, 'all` `');
```

The relational operator `smallest greater than` is used to restrict changes to those that occurred after midnight on the previous day. If multiple entries with different LSN values share the `tran_end_time` value identified as the lower bound in the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) table, the function will return the smallest LSN ensuring that all entries are included. For the upper bound, the relational operator `largest less than or equal to` is used to ensure that the range includes all entries for the day including those that have midnight as their `tran_end_time` value. If multiple entries with different LSN values share the `tran_end_time` value identified as the upper bound, the function will return the largest LSN ensuring that all entries are included.

## Permissions

Requires membership in the **public** role.

## Examples

The following example uses the `sys.fn_cdc_map_time_to_lsn` function to determine whether there are any rows in the [cdc.lsn_time_mapping](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md) table with a `tran_end_time` value that is greater than or equal to midnight. This query can be used to determine, for example, whether the capture process has already processed the changes committed through midnight of the previous day, so that the extraction of change data for that day can proceed.

```sql
DECLARE @extraction_time DATETIME,
    @lsn BINARY (10);

SET @extraction_time = '2007-01-01 12:00:00.000';

SELECT @lsn = sys.fn_cdc_map_time_to_lsn('smallest greater than or equal', @extraction_time);

IF @lsn IS NOT NULL
BEGIN
    <some action>
END
```

## See also

- [cdc.lsn_time_mapping &#40;Transact-SQL&#41;](../../relational-databases/system-tables/cdc-lsn-time-mapping-transact-sql.md)
- [sys.fn_cdc_map_lsn_to_time &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-cdc-map-lsn-to-time-transact-sql.md)
- [cdc.fn_cdc_get_net_changes_&#60;capture_instance&#62; &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-net-changes-capture-instance-transact-sql.md)
- [cdc.fn_cdc_get_all_changes_&#60;capture_instance&#62;  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/cdc-fn-cdc-get-all-changes-capture-instance-transact-sql.md)
