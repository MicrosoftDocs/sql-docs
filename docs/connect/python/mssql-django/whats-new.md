---
title: "What's New in mssql-django"
description: Learn about new features and changes in each version of the mssql-django Django backend for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 07/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---

# What's new in mssql-django

This article describes new features, improvements, and changes in each version of the `mssql-django` Django database backend.

## Version 1.7.4

**Release date**: July 2026

Version 1.7.4 is a backward-compatible patch release with two fixes for raw and annotated `GROUP BY` query handling.

### Bug fixes

- **`IndexError` on `GROUP BY` queries with escaped `%%` and real params**: Previously, any query with a `GROUP BY` clause passed through a placeholder-rewriting step that matched `%\w+` and replaced it with `{}`. That regex also matched escaped `%%` literals, which injected phantom placeholders and raised `IndexError: Replacement index N out of range` whenever a query combined a `%%`-escape with a real `%s` parameter. The narrowed regex now only touches `%%` (preserved verbatim) and `%s` (the real placeholder), which is the only pattern the compiler ever emits. The same fix also prevents an unrelated silent bug where an unescaped pattern such as `LIKE '%abc%'` in a no-param query was rewritten to `LIKE '{}%'` and returned the wrong rows.
- **`NotImplementedError` for `IntegerChoices` in raw `GROUP BY` queries**: Previously, passing an `IntegerChoices` value into a raw query that contained a `GROUP BY` clause raised `NotImplementedError: Not supported type <enum ...>`. The parameter typing helper used exact type checks (`typ == int`), and `type(IntegerChoices_value)` is the enum class rather than `int`, so the value fell through to the raise even though it subclasses `int`. Type checks now use `isinstance`, and the `bool` branch is evaluated before the `int` branch (because `bool` is itself an `int` subclass). Enum choices now bind correctly, `bool` still binds `BIT`, and plain `int` is unchanged.

## Version 1.7.3

**Release date**: June 2026

Version 1.7.3 is a backward-compatible patch release with two connection and runtime fixes.

### Bug fixes

- **`FA001` for `Authentication=` modes other than `ActiveDirectoryMsi`**: Previously, the backend skipped `Trusted_Connection=yes` only for `ActiveDirectoryMsi`. Other Entra modes that don't supply a `USER` value (for example, `ActiveDirectoryIntegrated`, `ActiveDirectoryDefault`, `ActiveDirectoryDeviceFlow`) still received `Trusted_Connection=yes`, which the ODBC driver rejected with `FA001` (`Cannot use Authentication option with Integrated Security option`). The fix detects any explicit `Authentication=` value with a boundary-aware, case-insensitive match and skips both `Trusted_Connection` and `Integrated Security=SSPI`. Password handling is unchanged: `SqlPassword`, `ActiveDirectoryPassword`, and `ActiveDirectoryServicePrincipal` continue to send `PWD`, while `ActiveDirectoryInteractive` continues to omit it.
- **`KeyError` on subclassed `DatabaseWrapper`**: The cached `sql_server_version` and `to_azure_sql_db` properties relied on `type(self).__dict__` introspection of `cached_property`, which raised `KeyError` the first time a `DatabaseWrapper` subclass accessed them (a regression introduced in 1.7.1). The fix uses explicit class-level dicts (`_known_versions`, `_known_azures`) accessed through `self.`, so the lookup resolves through the MRO and subclassed wrappers work correctly.

## Version 1.7.2

**Release date**: May 2026

Version 1.7.2 is a backward-compatible patch release with time zone and compatibility fixes.

### Bug fixes

- **`.explain()` compatibility for Django 4.0 and later**: Fixed compiler handling of Django's explain metadata so `.explain()` no longer fails with `AttributeError` on Django 4.0 and later. The backend now follows version-appropriate explain fields and correctly raises `NotSupportedError` when needed.
- **datetimeoffset time zone handling**: Fixed **datetimeoffset** parsing so time zone offsets are preserved instead of dropped. Returned datetimes are now time zone-aware when expected.
- **`Now()` with `USE_TZ=True`**: Updated SQL generation for `Now()` to use time zone-aware behavior when time zone support is enabled, preventing timestamp drift on non-UTC SQL Server hosts.

## Version 1.7.1

**Release date**: April 2026

Version 1.7.1 is a backward-compatible patch release with bug fixes.

### Bug fixes

- **`FieldDoesNotExist` when altering fields with descending index ordering**: Fixed `_alter_field()` in `schema.py` to use `index.fields_orders` instead of `index.fields` when resolving index field names. The previous code passed raw field-with-ordering strings (for example, `"-pub_date"`) to `model._meta.get_field()`, which raised `FieldDoesNotExist`. Now only the field name is extracted, and the ordering suffix is properly discarded.
- **SQL database in Microsoft Fabric support (EngineEdition 12)**: Recognized SQL database in Fabric (`EngineEdition=12`) as an Azure edition. Previously, Fabric's engine edition was unrecognized, causing `to_azure_sql_db` to return `False` and feature-gate checks to fail. The fix adds `EDITION_AZURE_SQL_FABRIC=12` to `_AZURE_EDITIONS` and maps Fabric to the latest supported SQL Server version. `JSONField`, hash functions, collation introspection, and test database teardown now work correctly on Fabric.

## Version 1.7

**Release date**: March 2026

### Highlights

- **Django 6.0 support**: Full compatibility with Django 6.0, which requires Python 3.12 or later. All 6.0 API changes are handled transparently by the backend.
- **Partial `CompositePrimaryKey` support**: The backend adds partial support for Django 5.2 `CompositePrimaryKey`. Tuple comparison against subqueries requires Django 5.2.4 or later, and some composite-key and `JSONField` edge cases remain. Django 5.2 itself was first supported in mssql-django 1.6.
- **SQL Server 2025 support**: Validated against SQL Server 2025.
- **ODBC Driver 18 default**: The backend now defaults to ODBC Driver 18 for SQL Server, with automatic fallback to ODBC Driver 17 if version 18 isn't installed.

### Version-specific notes

| Django version | Notes |
| --- | --- |
| Django 5.1 | `inspectdb` can inspect tables with composite primary keys, but it doesn't generate complete model definitions for them. |
| Django 5.2 | `CompositePrimaryKey` support is partial. Tuple comparison against subqueries requires Django 5.2.4 or later, and some migration plus `JSONField` edge cases remain. |
| Django 6.0 | Requires Python 3.12 or later. All 5.2 limitations apply. |

## Version 1.6

**Release date**: August 2025

- Added Django 5.1 and 5.2 support.
- Enhanced JSON functionality and backward compatibility.
- Improved pipeline infrastructure.

## Version 1.5

**Release date**: April 2024

- Added `supports_comments` feature flag for `db_comments`.
- Bug fixes for `AutoField`, parameter formatting, and schema queries.

## Version 1.4

**Release date**: January 2024

- Added Django 5.0 support.
- Added `db_comment` support.
- Bug fixes for date/time conversions and empty aggregates.

## Version 1.3

**Release date**: May 2023

- Added Django 4.2 support.
- Added case-sensitive `Replace` function support.
- Bug fixes for `OFFSET` handling and left padding.

## Version 1.2

**Release date**: December 2022

- Added Django 4.1 support.
- Added time zone support (**datetimeoffset** with `USE_TZ=True`).
- Added `return_rows_bulk_insert` option for bulk insert ID retrieval.
- Added SQL Server 2022 support.
- Added `JSONField` support for Azure SQL Managed Instance.

## Version 1.1

**Release date**: July 2022

- Django 3.2 and 4.0 support.
- SQL Server 2016 and later, and Azure SQL Database support.
- `pyodbc`-based connectivity.

## Related content

- [Install mssql-django](installation.md)
- [mssql-django support and lifecycle](support-lifecycle.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [mssql-django releases on GitHub](https://github.com/microsoft/mssql-django/releases)
