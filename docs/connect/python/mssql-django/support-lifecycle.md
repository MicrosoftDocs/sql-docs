---
title: mssql-django Support and Lifecycle
description: Learn about the support lifecycle, version compatibility, and how to report issues for the mssql-django package.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sharmag, sumitsar
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---

# mssql-django support and lifecycle

The `mssql-django` package is the official Microsoft-supported Django database backend for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. It's actively maintained on [GitHub](https://github.com/microsoft/mssql-django) and released through [PyPI](https://pypi.org/project/mssql-django/). This page covers versioning, platform compatibility, and support policy.

## Version support

Always use the latest release to gain new features, performance improvements, and security fixes. New capabilities are only added to the current release.

### Current version

Version 2.0 is the current general availability (GA) release.

> [!IMPORTANT]
> Version 2.0 requires `mssql-python` 1.15.0 or later, even when a database alias uses `pyodbc`. The package installs only on platforms that have a compatible `mssql-python` distribution. Projects on other platforms stay on version 1.8.0.

### Support status definitions

Use these status values in the version table:

| Status | Meaning |
| --- | --- |
| **Current** | Receives new features, bug fixes, and security fixes. |
| **Previous** | Historical release. Remains available but doesn't receive updates. |

### Version history

| Version | Release date | Status | Django versions | Key features |
| --- | --- | --- | --- | --- |
| 2.0 | September 2026 | **Current** | 5.2 - 6.1 | Opt-in `mssql-python` driver, Python 3.10 - 3.14, `zoneinfo` replaces `pytz`, MARS and `inspectdb` fixes |
| 1.8.0 | August 2026 | Previous | 3.2 - 6.1 | Django 6.1 support, `quote_name` query compiler change, foreign key introspection returns the ON DELETE rule |
| 1.7.4 | July 2026 | Previous | 3.2 - 6.0 | `GROUP BY` fixes for escaped `%%` literals with real params and for `IntegerChoices` params in raw queries |
| 1.7.3 | June 2026 | Previous | 3.2 - 6.0 | `FA001` fix for `Authentication=` modes other than `ActiveDirectoryMsi`, subclassed `DatabaseWrapper` `KeyError` fix (regression from 1.7.1) |
| 1.7.2 | May 2026 | Previous | 3.2 - 6.0 | **datetimeoffset** time zone fix, `Now()` time zone fix, `.explain()` compatibility fix |
| 1.7.1 | April 2026 | Previous | 3.2 - 6.0 | SQL database in Fabric fix, descending index AlterField fix |
| 1.7 | March 2026 | Previous | 3.2 - 6.0 | Django 6.0 support, ODBC Driver 18 default, SQL Server 2025 support |
| 1.6 | August 2025 | Previous | 3.2 - 5.2 | Django 5.1 and 5.2 support, enhanced JSON functionality |
| 1.5 | April 2024 | Previous | 3.2 - 5.0 | `supports_comments` flag, `AutoField` fixes |
| 1.4 | January 2024 | Previous | 3.2 - 5.0 | Django 5.0 support, `db_comment` support |
| 1.3 | May 2023 | Previous | 3.2 - 4.2 | Django 4.2 support, case-sensitive `Replace` |
| 1.2 | December 2022 | Previous | 3.2 - 4.1 | Django 4.1 support, time zone support, `JSONField` on Azure SQL Managed Instance |
| 1.1 | July 2022 | Previous | 3.2 - 4.0 | Initial release with Django 3.2 and 4.0 support |

Versions prior to 1.1 were pre-release and aren't listed.

> [!IMPORTANT]
> Fixes and new features are only shipped in new releases. Older versions remain available on PyPI but aren't patched in place. To get bug fixes or security fixes, upgrade to the latest release.

For detailed release notes, see [What's new in mssql-django](whats-new.md).

## Django and Python version compatibility

Each Django release supports specific Python versions. `mssql-django` 2.0 tests the following combinations:

| Django version | Python versions |
| --- | --- |
| 6.1 | 3.12, 3.13, 3.14 |
| 6.0 | 3.12, 3.13, 3.14 |
| 5.2 | 3.10, 3.11, 3.12, 3.13 |

Django 3.2 through 5.1 and Python 3.8 and 3.9 reached end of support and aren't tested. Projects on those versions stay on `mssql-django` 1.8.0.

> [!IMPORTANT]  
> Always use a supported Python version. Older Python versions don't receive security updates.

## SQL Server and Azure SQL compatibility

`mssql-django` 2.0 supports every supported version of Microsoft SQL. A newer SQL Server major version that the backend doesn't recognize connects by using the latest capability set the backend knows about, rather than failing version validation. 

| Product or service | Support status |
| --- | --- |
| SQL Server | Fully supported |
| SQL Server on Azure Virtual Machines | Fully supported |
| Azure SQL Database | Fully supported |
| Azure SQL Managed Instance | Fully supported |
| SQL database in Fabric | Fully supported |
| Microsoft Fabric Warehouse | Connections only. On the `pyodbc` path, set `MARS_Connection=no` in `extra_params`. Django migrations and other SQL Server features aren't supported. |
| Azure Synapse Analytics | Connections only. Django migrations and other SQL Server features aren't supported. |

## Database driver compatibility

`mssql-django` 2.0 connects through either of two Python database drivers, selected for each database alias. For more information, see [Select the database driver for mssql-django](select-database-driver.md).

| Python driver | Support status | Connectivity |
| --- | --- | --- |
| `pyodbc` | Fully supported (default) | Microsoft ODBC Driver 17 or 18 for SQL Server, installed separately |
| `mssql-python` | Fully supported (opt in with the `python_driver` option) | Built in, with no separate install |

### ODBC driver compatibility

On the `pyodbc` path, the backend defaults to ODBC Driver 18 for SQL Server and automatically falls back to ODBC Driver 17 if version 18 isn't installed. Override the default with the `driver` option in your database configuration. An explicit value doesn't fall back.

| ODBC driver | Support status |
| --- | --- |
| Microsoft ODBC Driver 18 for SQL Server | Fully supported (default) |
| Microsoft ODBC Driver 17 for SQL Server | Fully supported (fallback) |
| FreeTDS ODBC driver | Supported on the `pyodbc` path |

The `mssql-python` path ignores the `driver` option.

For installation instructions, see [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).

## Operating system compatibility

`mssql-django` 2.0 requires `mssql-python`, so the package installs only where a compatible `mssql-python` distribution exists. That requirement applies even when every database alias uses `pyodbc`.

| Operating system | Architecture | Support status |
| --- | --- | --- |
| Windows 11, Windows Server 2019, 2022, and 2025 | x64 | Supported |
| Windows 11, Windows Server 2022 and 2025 | ARM64 | Supported with Python 3.11 and later versions |
| macOS 15 and later versions | Intel, Apple silicon | Supported |
| Linux with glibc 2.28 or later, such as Ubuntu 22.04 and 24.04, Debian 11 and 12, and Red Hat Enterprise Linux 8 and 9 | x64, ARM64 | Supported |
| Linux with musl 1.2 or later, such as Alpine Linux | x64, ARM64 | Supported |
| SUSE Linux Enterprise Server | ARM64 | Not supported |

Projects on an unsupported platform stay on `mssql-django` 1.8.0, which doesn't require `mssql-python`.

On the `pyodbc` path, install the Microsoft ODBC Driver for SQL Server separately. The installation steps vary by operating system. See [Install mssql-django](installation.md) for platform-specific setup.

## Feature compatibility

The following tables list Django and SQL Server features and their support status in the `mssql-django` backend. For more detail on unsupported features, see [Limitations and unsupported features in mssql-django](limitations.md).

### Django ORM features

| Feature | mssql-django support |
| --- | --- |
| Migrations | Yes |
| `QuerySet` API | Yes |
| `JSONField` | Yes |
| `bulk_create` / `bulk_update` | Yes |
| Database transactions | Yes |
| `inspectdb` with `--schema` | Yes |
| `DISTINCT ON` | No |
| `__regex` / `__iregex` lookups | Partial (requires CLR assembly setup; unavailable on Azure SQL Database) |
| `SmallAutoField` | Yes |
| `select_for_update()` | Yes (`NOWAIT` and SKIP_LOCKED; `of` not supported) |
| Window functions | Yes |
| `GeneratedField` (computed columns) | Yes (Django 5.0 and later) |
| `CompositePrimaryKey` | Partial (Django 5.2 and later; see limitations) |
| `db_comment` | Yes (Django 4.2 and later) |
| Covering indexes (`include`) | Yes (Django 4.2 and later) |
| `NthValue` | No |

### SQL Server features

| Feature | mssql-django support |
| --- | --- |
| Encrypted connections (TLS) | Yes |
| Always Encrypted | Yes |
| Microsoft Entra authentication | Yes |
| Multiple Active Result Sets (MARS) | Yes on the `pyodbc` path. The backend enables MARS when the alias uses a Microsoft ODBC driver on Windows. The `mssql-python` path never enables MARS and rejects the `MARS_Connection` keyword. |
| Stored procedures | Yes (via `cursor.execute`) |
| `SNAPSHOT` isolation | Yes (requires database-level config) |
| Read-only routing | Yes |

## Dependency requirements

The `mssql-django` package automatically installs the following dependencies:

| Dependency | Purpose | Required version |
| --- | --- | --- |
| Django | Web framework | `>=5.2,<6.2` |
| `pyodbc` | Default ODBC database driver for Python | `>=3.0` |
| `mssql-python` | Python database driver. Installed unconditionally, and used only when an alias opts in. | `>=1.15.0` |
| `tzdata` | IANA time zone database for the standard library `zoneinfo` module, used where the operating system doesn't supply one | Any |

The `pyodbc` path also requires the Microsoft ODBC Driver for SQL Server on the host system. The `mssql-python` path has no separate ODBC driver install. For more information, see [Install mssql-django](installation.md).

## Policy for versioning and breaking changes

- **Major versions** (2.0): Can change the supported Python, Django, SQL Server, and platform matrix, and can add or remove dependencies. Breaking changes appear only in a major version.
- **Minor versions** (1.6, 1.7): Include new Django version support, new features, and bug fixes. Maintain backward compatibility.
- **Patch versions** (1.7.1, 1.7.2, 1.7.3, 1.7.4): Include bug fixes only.

The team documents breaking changes in release notes. See [What's new in mssql-django](whats-new.md) for version-specific notes.

## How to stay current

The `mssql-django` backend releases new versions to track Django releases. Check for updates when upgrading Django.

### Check installed version

Verify which version is currently installed:

```bash
pip show mssql-django
```

### Upgrade to latest version

Update to the latest release:

```bash
pip install --upgrade mssql-django
```

### Subscribe to updates

- Watch the [GitHub repository](https://github.com/microsoft/mssql-django) for release notifications.
- Check [PyPI](https://pypi.org/project/mssql-django/) for new releases.
- Review the [changelog](https://github.com/microsoft/mssql-django/releases) for each release.

## Get support

Microsoft supports `mssql-django` through GitHub and community channels.

### GitHub Issues

Report bugs and request features on GitHub:

- [Open an issue](https://github.com/microsoft/mssql-django/issues/new)
- [View known issues](https://github.com/microsoft/mssql-django/issues?q=is%3Aissue+is%3Aopen)

When you report an issue, include your Django version, Python version, SQL Server version, the Python database driver and its version, and a minimal reproduction of the problem.

### Contribute

Community contributions are welcome. For more information about the Contributor License Agreement (CLA) and submission process, see the [contributing guide](https://github.com/microsoft/mssql-django/blob/dev/CONTRIBUTING.md).

### Community

- Stack Overflow: Tag questions with `django` and `sql-server`.
- [Django documentation](https://docs.djangoproject.com/en/stable/)
- [Azure Python Developer Center](https://azure.microsoft.com/develop/python/)

## Related content

- [What's new in mssql-django](whats-new.md)
- [Install mssql-django](installation.md)
- [Select the database driver for mssql-django](select-database-driver.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [mssql-django configuration reference](configuration-reference.md)
- [mssql-django on GitHub](https://github.com/microsoft/mssql-django)
