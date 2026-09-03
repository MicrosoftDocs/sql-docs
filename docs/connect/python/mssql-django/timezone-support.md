---
title: Time Zone Support in mssql-django
description: Configure time zone-aware datetime fields in Django applications using the mssql-django backend with SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Time zone support in mssql-django

This article explains how time zone-aware datetime fields work with SQL Server through the `mssql-django` backend and how to migrate existing data when you enable time zone support.

## How time zone support works

Django's `USE_TZ` setting in `settings.py` controls whether datetime fields are time zone-aware:

| Setting | Column type | Behavior |
| --- | --- | --- |
| `USE_TZ=False` | **datetime2** | Stores naive datetimes without time zone information. |
| `USE_TZ=True` | **datetimeoffset** | Stores time zone-aware datetimes with UTC offset. |

> [!NOTE]
> Django defaults to `USE_TZ=False` across all versions, including Django 5.x and 6.0. If your project needs time zone support, you must explicitly set `USE_TZ=True` in your `settings.py`. See [Time zone support in Django](https://docs.djangoproject.com/en/6.0/topics/i18n/timezones/) for more information. Note that **datetimeoffset** typically uses more storage than **datetime2** for the same timestamp precision, so on large tables you should validate storage and query plans after migration.

## Enable time zone support

Set `USE_TZ=True` in your `settings.py`:

```python
USE_TZ=True
TIME_ZONE = "UTC"
```

When `USE_TZ` is enabled, Django stores all datetimes in UTC and converts them to the local time zone for display.

Starting with `mssql-django` 1.7.2, the backend also aligns Django `Now()` behavior with time zone-aware SQL generation when `USE_TZ=True`.

## Migrate existing datetime columns

If your Django app had `DateTimeField` columns before enabling `USE_TZ=True`, you must manually migrate **datetime2** columns to **datetimeoffset** and convert local time to UTC.

The `mssql-django` backend uses **datetime2** as the underlying column type for `DateTimeField` when `USE_TZ=False`. Enabling `USE_TZ` doesn't automatically convert existing columns.

In the following steps, replace the placeholders:

- `<table-name>`: The table name containing the column.
- `<datetime-column>`: The column name to convert.
- `<offset>`: The time zone offset of your existing data as a string in `{+|-}HH:MM` format (for example, `'-05:00'` for US Eastern).

[!INCLUDE [article-uses-adventureworks](../../../includes/article-uses-adventureworks.md)]

### Step 1: Alter column types

Run the following SQL on each table that has **datetime2** columns. SQL Server implicitly converts the value and assigns a `+00:00` offset:

```sql
ALTER TABLE <table-name>
ALTER COLUMN <datetime-column> DATETIMEOFFSET;
```

### Step 2: Convert to UTC

After the column is **datetimeoffset**, re-tag each value with the original local time zone offset and then convert to UTC:

```sql
UPDATE <table-name>
SET <datetime-column> = TODATETIMEOFFSET(<datetime-column>, <offset>) AT TIME ZONE 'UTC';
```

`TODATETIMEOFFSET` replaces the offset portion of the **datetimeoffset** value so the timestamp reflects the original local time. `AT TIME ZONE 'UTC'` then converts the result to UTC.

> [!IMPORTANT]  
> A single fixed offset works only when all source values share the same offset. If your data spans daylight saving transitions, derive the offset by date or use a time zone name based conversion strategy instead of applying one constant offset to every row.

#### SQL Server time zone names

Use Windows time zone names when converting with `AT TIME ZONE`. Common examples:

| Region | SQL Server time zone name | DST transition dates (2026) |
|--------|-------------------------|---------------------------|
| US Eastern | `Eastern Standard Time` | Mar 8 – Nov 1 |
| US Central | `Central Standard Time` | Mar 8 – Nov 1 |
| US Pacific | `Pacific Standard Time` | Mar 8 – Nov 1 |
| UTC | `UTC` | None (no DST) |
| Europe/London | `GMT Standard Time` | Mar 29 – Oct 25 |

For a complete list, query SQL Server:

```sql
SELECT name, current_utc_offset, is_currently_dst
FROM sys.time_zone_info
ORDER BY name;
```

See [sys.time_zone_info](../../../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md) for full documentation.

#### Example: Convert US Eastern time to UTC (DST-aware)

This example uses an existing [!INCLUDE [sssampledbobject-md](../../../includes/sssampledbobject-md.md)] schema table and demonstrates correct DST handling during migration:

```sql
-- Test with a DST transition date (March 8, 2026)
SELECT TOP 10 SalesOrderID,
             CAST (OrderDate AS DATETIME2) AS OriginalDateTime2,
             (CAST (OrderDate AS DATETIME2) AT TIME ZONE 'Eastern Standard Time') AS EasternTime,
             (CAST (OrderDate AS DATETIME2) AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC') AS ConvertedToUtc
FROM Sales.SalesOrderHeader
WHERE MONTH(OrderDate) = 3 AND DAY(OrderDate) = 8
ORDER BY SalesOrderID;
```

> [!TIP]  
> Always test time zone conversions with data that spans DST transition dates. The previous query tests March 8 (spring forward), which is when Eastern time changes from EST (UTC-5) to EDT (UTC-4).

To migrate your application tables, apply the same `AT TIME ZONE` conversion pattern to your own `DateTimeField` columns. For each table, add a **datetimeoffset** column and populate it from the existing column:

```sql
ALTER TABLE [your_schema].[your_table] 
ADD [date_column_datetimeoffset] datetimeoffset NULL;

UPDATE [your_schema].[your_table]
SET [date_column_datetimeoffset] = CAST([old_date_column] AS DATETIME2) AT TIME ZONE 'Eastern Standard Time' AT TIME ZONE 'UTC';
```

After you verify the conversion results, drop the old column and rename the new one:

```sql
ALTER TABLE [your_schema].[your_table]
DROP COLUMN [old_date_column];

EXECUTE sp_rename '[your_schema].[your_table].[date_column_datetimeoffset]', 'date_column', 'COLUMN';
```

Use the SQL Server Windows time zone name that matches your source data region. For more information, see [sys.time_zone_info](../../../relational-databases/system-catalog-views/sys-time-zone-info-transact-sql.md).

### Step 3: Complete the Django migration

After converting the database columns, follow Django's documentation on [migrating a project started before time zone support was added](https://docs.djangoproject.com/en/stable/topics/i18n/timezones/#migration-guide).

> [!IMPORTANT]  
> Run this migration on all `DateTimeField` columns across all tables. Missing any column results in incorrect time zone handling for those fields.

## Limitations

- **Time zones and timedeltas**: Not all operations involving time zones and timedeltas are fully supported. For more information, see [Limitations and unsupported features in mssql-django](limitations.md).
- **Arithmetic with datetimes**: Righthand power and arithmetic with datetime values might not work as expected when time zone support is enabled.

## Related content

- [Django field to SQL Server type mappings](data-type-mappings.md)
- [mssql-django configuration reference](configuration-reference.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [Time zone support wiki](https://github.com/microsoft/mssql-django/wiki/Timezone-Support)
- [Django time zone documentation](https://docs.djangoproject.com/en/stable/topics/i18n/timezones)
