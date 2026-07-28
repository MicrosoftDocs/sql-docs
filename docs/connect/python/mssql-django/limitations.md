---
title: Limitations and Unsupported Features in mssql-django
description: Limitations and unsupported features of the mssql-django Django backend for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 07/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# Limitations and unsupported features in mssql-django

This article lists limitations of the `mssql-django` backend when used with SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

## Django feature limitations

The following Django features aren't supported or have limited support with the `mssql-django` backend:

| Feature | Status | Details |
| --- | --- | --- |
| `Avg` with `DurationField` | Not supported | Aggregate `Avg` doesn't work on `DurationField`. |
| `__regex` and `__iregex` lookups | Requires setup | Supported after installing the CLR assembly on SQL Server or Azure SQL Managed Instance. Azure SQL Database doesn't support CLR assemblies. See [Set up regex lookups](#set-up-regex-lookups). |
| `DISTINCT ON` | Not supported | SQL Server doesn't support `DISTINCT ON` clauses. Use `.values().distinct()` or subqueries. |
| `Subquery` in `ORDER BY` | Not supported | Ordering by subquery expressions might not work. |
| Database-level `CASCADE` | Limited | Some `SET NULL` and `SET DEFAULT` operations can require manual migration SQL. |
| `is_dst` in `Trunc`/`Extract` | Not supported | `is_dst` parameter (used to resolve ambiguous times during daylight saving transitions) in `Extract()` and `Trunc()` isn't supported. Use `AT TIME ZONE` in raw SQL for DST-aware queries. |
| Floating point annotate | Limited | Floating point `Avg` aggregates can lose precision compared to PostgreSQL due to SQL Server's **float** type behavior. For example, averaging 0.1 and 0.2 might yield 0.15000000000000000222 instead of exactly 0.15. Use `DecimalField` or `Cast(avg_expr, output_field=DecimalField())` for critical financial calculations. |
| Annotate/exists in `ORDER BY` | Not supported | Using annotate or exists expressions in `order_by` might not work. |
| Right-hand power and datetime arithmetic | Not supported | Right-hand power operations (for example, `F('value') ** 2` works but `2 ** F('value')` fails) and division with `timedelta` aren't supported. |
| Time zones and timedeltas | Limited | Time zones and timedeltas aren't fully supported. See [Time zone support in mssql-django](timezone-support.md). |
| `NthValue` window function | Not supported | SQL Server doesn't support `NTH_VALUE()`. Use `FIRST_VALUE`, `LAST_VALUE`, or a subquery. |
| `ignore_conflicts` in `bulk_create` | Not supported | `bulk_create(objs, ignore_conflicts=True)` isn't supported. SQL Server has no equivalent to PostgreSQL's `ON CONFLICT DO NOTHING`. |
| JSONField `contains` lookup | Not supported | Use key-path lookups instead (for example, `filter(metadata__color="blue")`). See [JSONField limitations](#jsonfield-limitations). |
| `select_for_update(of=(...))` | Not supported | SQL Server doesn't support locking specific tables. The backend raises `NotSupportedError`. See [Transaction management](transactions.md#differences-from-postgresql). |

## Migration limitations

| Limitation | Details |
| --- | --- |
| Alter `AutoField` | Can't change a field to or from `AutoField` (IDENTITY column). Requires creating a new table. |
| Rename with foreign keys | Renaming a column that has foreign key constraints can fail. Use `SeparateDatabaseAndState`. |
| `AddConstraint`/`RemoveConstraint` conflicts | Some constraint operations can conflict. Apply in separate migrations. |
| Date extract operations | `ExtractYear`, `ExtractMonth`, and similar operations have limited `tzinfo` support. |

## JSONField limitations

- `mssql-django` maps `JSONField` to **nvarchar(max)**. SQL Server 2025 introduced a native **json** type, but the Microsoft ODBC Driver for SQL Server doesn't expose it.
- The `contains` lookup isn't supported. Use key-path lookups instead (for example, `filter(metadata__color="blue")`).
- Quoted string values return with extra quotes (for example, `'"value"'` instead of `'value'`).
- Some nested lookups might behave differently than on PostgreSQL.
- For more information, see [JSONField with SQL Server](json-field.md).

## inspectdb limitations

- Composite primary keys aren't generated as `unique_together` automatically.
- Some SQL Server-specific column types might map to generic Django fields.
- Review and adjust generated models manually.
- For more information, see [Reverse-engineer models with inspectdb](inspectdb.md).

## SQL Server parameter limit

SQL Server limits each query to a maximum of 2,100 parameters. This limit affects Django operations that generate parameterized queries with large value lists:

| Operation | How it hits the limit |
| --- | --- |
| `filter(field__in=large_list)` | Each list item becomes a parameter. The backend auto-optimizes lists over 2,048 items into a temp table. |
| `prefetch_related()` | Each parent object ID becomes a parameter in the related query's `WHERE IN` clause. Auto-optimized like `filter(field__in=...)` when over 2,048 IDs. |
| `bulk_create()` | Each field of each object becomes a parameter. A model with 10 fields and 250 objects generates 2,500 parameters. |
| `bulk_update()` | Each field uses two parameters per object (one for the PK match, one for the value). |
| `Q()` with many conditions | Each value in chained `Q` objects becomes a parameter. |

Set `batch_size` on bulk operations and chunk large `IN` queries. See [Performance tuning](performance-tuning.md#work-within-the-2100-parameter-limit) for solutions.

## Bulk operations limitations

- `bulk_create` with `return_rows_bulk_insert=False` doesn't return IDs. Required for tables with triggers. See [Bulk operations with mssql-django](bulk-operations.md).

## Test framework limitations

`--keepdb` is required when using managed identity authentication (`ActiveDirectoryMsi`) because the test runner can't create or destroy databases with that auth method.

For more information, see [Test Django apps with SQL Server](testing.md).

## Version-specific notes

| mssql-django version | Notes |
| --- | --- |
| 1.7.4 | Fixed `IndexError` on `GROUP BY` queries that mix escaped `%%` literals with real params. Fixed `NotImplementedError` for `IntegerChoices` params in raw `GROUP BY` queries. |
| 1.7.3 | Fixed `FA001` for `Authentication=` modes other than `ActiveDirectoryMsi`. Fixed `KeyError` on subclassed `DatabaseWrapper` (regression from 1.7.1). |
| 1.7.2 | Fixed time zone handling for **datetimeoffset** and `Now()` with `USE_TZ=True`. Fixed `.explain()` compatibility for Django 4.0 and later. |
| 1.7.1 | SQL database in Fabric (EngineEdition 12) fix. Descending index `AlterField` fix. |
| 1.7 | ODBC Driver 18 is the default. Django 6.0, Python 3.14, SQL Server 2025 support added. |
| 1.6 | Django 5.1 and 5.2 support. Enhanced JSON functionality. |
| 1.5 | Bug fixes for AutoField, parameter formatting, and schema queries. |
| 1.4 | Django 5.0 support. `db_comment` support. |
| 1.3 | Django 4.2 support. |
| 1.2 | Django 4.1 support. Time zone support. `return_rows_bulk_insert` option. SQL Server 2022 support. |
| 1.1 | Django 3.2 and 4.0 support. |

## Django version-specific notes

| Django version | Notes |
| --- | --- |
| 5.1 | `inspectdb` can inspect tables with composite primary keys, but it doesn't generate complete model definitions for them. |
| 5.2 | `CompositePrimaryKey` support is partial. `inspectdb` still requires manual fixes, tuple comparison against subqueries requires Django 5.2.4 and later versions, and some migration plus JSONField bulk/CASE WHEN update paths still have test exclusions. For more information, see the [GitHub repository](https://github.com/microsoft/mssql-django). |
| 6.0 | Requires Python 3.12 and later versions. All 5.2 limitations apply. The backend handles all 6.0 API changes transparently. |

## Set up regex lookups

The `mssql-django` backend supports Django's `__regex` and `__iregex` lookups, but they require a one-time setup step. The backend ships a CLR assembly (`regex_clr.dll`) that provides a `dbo.REGEXP_LIKE` function to SQL Server.

### Prerequisites

- A SQL Server instance that supports CLR integration. On-premises SQL Server and Azure SQL Managed Instance support CLR. **Azure SQL Database doesn't support CLR assemblies**, so `__regex` and `__iregex` lookups aren't available on Azure SQL Database.
- The connecting user must have `sysadmin` or `ALTER SETTINGS` permission. The management command enables CLR automatically.
- The `mssql` app must be in `INSTALLED_APPS`.

### Install the CLR assembly

Run the management command, passing your database name:

```bash
python manage.py install_regex_clr <your-database-name>
```

This command performs the following steps:

1. Enables CLR on the server (`sp_configure 'clr enabled', 1`) if not already enabled.
1. Sets `clr strict security` to `0` (required for `SAFE` assemblies on SQL Server 2017+).
1. Creates the `regex_clr` assembly from the bundled DLL.
1. Creates the `dbo.REGEXP_LIKE` scalar function.

> [!CAUTION]  
> Setting `clr strict security` to `0` allows unsigned CLR assemblies to load. This is required because the bundled `regex_clr.dll` isn't signed. Discuss this change with your DBA before running the command on production servers. The setting applies server-wide, not per-database.

### Use regex lookups

After installing the assembly, use `__regex` and `__iregex` in querysets:

```python
# Case-sensitive regex
products = Product.objects.filter(name__regex=r"^Widget \d+$")

# Case-insensitive regex
products = Product.objects.filter(name__iregex=r"^widget \d+$")
```

The backend translates these lookups to `dbo.REGEXP_LIKE(column, pattern, case_flag) = 1`.

> [!NOTE]  
> You must run the `install_regex_clr` command once per database. If the database is dropped and recreated (for example, during testing), run the command again.

## Related content

- [Troubleshoot mssql-django](troubleshooting.md)
- [FAQ](faq.yml)
- [mssql-django support and lifecycle](support-lifecycle.md)
- [GitHub issues](https://github.com/microsoft/mssql-django/issues)
