---
title: "sys.time_zone_info (Transact-SQL)"
description: sys.time_zone_info returns information about supported time zones.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/25/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.time_zone_info"
  - "sys.time_zone_info_TSQL"
  - "time_zone_info"
  - "time_zone_info_TSQL"
helpviewer_keywords:
  - "sys.time_zone_info system table"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# sys.time_zone_info (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-fabricse-fabricdw.md)]

`sys.time_zone_info` returns information about supported time zones.

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Name of the time zone in Windows standard format. For example, `Cen. Australia Standard Time` or `Central European Standard Time`. |
| `current_utc_offset` | **nvarchar(12)** | Current offset to UTC. For example, `+01:00` or `-07:00`. |
| `is_currently_dst` | **bit** | True if currently observing daylight saving time. |

## Remarks

All time zones installed on the computer are stored in the following registry hive:

- `KEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Time Zones`.

## Permissions

Any user with `CONNECT` permissions can access this system catalog view.

## Related content

- [GETUTCDATE (Transact-SQL)](../../t-sql/functions/getutcdate-transact-sql.md)
- [AT TIME ZONE (Transact-SQL)](../../t-sql/queries/at-time-zone-transact-sql.md)
- [Date and time data types and functions (Transact-SQL)](../../t-sql/functions/date-and-time-data-types-and-functions-transact-sql.md)
- [Server-wide Configuration Catalog Views (Transact-SQL)](server-wide-configuration-catalog-views-transact-sql.md)
