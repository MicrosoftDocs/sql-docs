---
title: Django Field to SQL Server Type Mappings
description: Review how Django model field types map to SQL Server data types in the mssql-django backend.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# Django field to SQL Server type mappings

This article documents how Django model field types map to SQL Server data types when using the `mssql-django` backend.

## Field type mapping table

| Django field | SQL Server type | Notes |
| --- | --- | --- |
| `AutoField` | **int** with `IDENTITY(1,1)` | Auto-incrementing primary key. |
| `BigAutoField` | **bigint** with `IDENTITY(1,1)` | 64-bit auto-incrementing primary key. |
| `SmallAutoField` | **smallint** with `IDENTITY(1,1)` | 16-bit auto-incrementing primary key. |
| `BooleanField` | **bit** | Stores `0` or `1`. |
| `CharField(max_length=N)` | **nvarchar(*N*)** | Unicode character data. |
| `DateField` | **date** | Date without time. |
| `DateTimeField` | **datetime2** | Date and time with fractional seconds. Uses **datetimeoffset** when `USE_TZ=True`. |
| `DecimalField(max_digits=M, decimal_places=D)` | **numeric(*M*, *D*)** | Fixed-precision decimal. |
| `DurationField` | **bigint** | Stored as microseconds. |
| `EmailField` | **nvarchar(254)** | CharField with email validation. |
| `FileField` | **nvarchar(100)** | Stores the file path. |
| `FilePathField` | **nvarchar(100)** | Stores the file system path. |
| `FloatField` | **float** | 64-bit floating point (**float(53)**). SQL Server also accepts the synonym `double precision`. |
| `IntegerField` | **int** | 32-bit signed integer. |
| `BigIntegerField` | **bigint** | 64-bit signed integer. |
| `SmallIntegerField` | **smallint** | 16-bit signed integer. |
| `PositiveIntegerField` | **int** | With a CHECK constraint `>= 0`. |
| `PositiveBigIntegerField` | **bigint** | With a CHECK constraint `>= 0`. |
| `PositiveSmallIntegerField` | **smallint** | With a CHECK constraint `>= 0`. |
| `GenericIPAddressField` | **nvarchar(39)** | IPv4 or IPv6 address. |
| `JSONField` | **nvarchar(max)** | With JSON check constraint. Requires SQL Server 2016+. |
| `SlugField` | **nvarchar(50)** | CharField with slug validation. |
| `TextField` | **nvarchar(max)** | Unlimited-length Unicode text. |
| `TimeField` | **time** | Time without date. |
| `URLField` | **nvarchar(200)** | CharField with URL validation. |
| `UUIDField` | **char(32)** | UUID stored as 32-character hex string. |
| `BinaryField` | **varbinary(*N*)** | Raw binary data. The backend uses `max_length` to emit **varbinary(*N*)**. |
| `ForeignKey` | Same as referenced field | Creates an index and FK constraint. |
| `OneToOneField` | Same as referenced field | Creates a unique index and FK constraint. |
| `ManyToManyField` | N/A | Creates an intermediate table. |

## SQL Server-specific behaviors

Some Django field types have platform-specific behavior when used with SQL Server.

### AutoField limitation

Altering a model field from or to `AutoField` at migration time isn't supported. If you need to change the primary key type, create a new field and migrate data manually.

### BooleanField and bit

SQL Server **bit** type stores `0` and `1`. Django maps `True`/`False` to these values. `NULL` is supported with `BooleanField(null=True)`.

### DateTimeField and time zone support

When `USE_TZ=True` in your Django settings, `DateTimeField` uses **datetimeoffset** to store time zone-aware datetimes. When `USE_TZ=False`, it uses **datetime2**.

If you enable `USE_TZ` after creating columns, you must manually migrate existing **datetime2** columns to **datetimeoffset**. For more information, see [Time zone support in mssql-django](timezone-support.md).

### TextField vs CharField

SQL Server maps both `TextField` and `CharField` to **nvarchar**. `TextField` uses **nvarchar(max)** while `CharField` uses **nvarchar(*N*)** where `N` is `max_length`.

### All string fields use nvarchar (Unicode)

The `mssql-django` backend maps all Django string fields (`CharField`, `TextField`, `EmailField`, `URLField`, `SlugField`, and others) to **nvarchar**, the Unicode string type. There's no built-in option to use **varchar** (non-Unicode) instead.

This is by design. Django's string handling is Unicode throughout, and **nvarchar** ensures that all characters are stored correctly regardless of language or encoding. Using **nvarchar** avoids data loss from character set mismatches.

**Trade-offs**:

- **nvarchar** uses 2 bytes per character, compared to 1 byte per character for **varchar** with single-byte collations.
- Index key size limits apply (900 bytes for nonclustered indexes). An **nvarchar(450)** column reaches the 900-byte limit (450 x 2 bytes), while a **varchar(900)** column reaches the same limit using single-byte characters.
- If your data is exclusively ASCII, **nvarchar** doubles storage compared to **varchar**.

**If you need varchar columns**:

For legacy databases or strict storage requirements, create a custom field that overrides `db_type`:

```python
from django.db import models

class VarcharField(models.CharField):
    def db_type(self, connection):
        return f"varchar({self.max_length})"

class LegacyProduct(models.Model):
    sku = VarcharField(max_length=50)  # Creates varchar(50) instead of nvarchar(50)

    class Meta:
        managed = False  # For existing tables
        db_table = "LegacyProduct"
```

> [!CAUTION]  
> Using **varchar** columns risks data loss if non-ASCII characters are written. Only use this approach when you're certain the column stores ASCII-only data, or when you need to match an existing database schema.

## Related content

- [JSONField with SQL Server](json-field.md)
- [Database migrations with mssql-django](migrations.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [Django model field reference](https://docs.djangoproject.com/en/stable/ref/models/fields)
