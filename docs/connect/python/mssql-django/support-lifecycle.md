---
title: mssql-django Support and Lifecycle
description: Learn about the support lifecycle, version compatibility, and how to report issues for the mssql-django package.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest
ms.date: 07/24/2026
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

Version 1.7.4 is the current general availability (GA) release.

### Support status definitions

Use these status values in the version table:

| Status | Meaning |
| --- | --- |
| **Current** | Receives new features, bug fixes, and security fixes. |
| **Previous** | Historical release. Remains available but doesn't receive updates. |

### Version history

| Version | Release date | Status | Django versions | Key features |
| --- | --- | --- | --- | --- |
| 1.7.4 | July 2026 | **Current** | 3.2 - 6.0 | `GROUP BY` fixes for escaped `%%` literals with real params and for `IntegerChoices` params in raw queries |
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

Each Django release supports specific Python versions. When choosing your versions, ensure compatibility between Django, Python, and `mssql-django`:

| Django version | Python versions |
| --- | --- |
| 6.0 | 3.12, 3.13, 3.14 |
| 5.2 | 3.10, 3.11, 3.12, 3.13 |
| 5.1 | 3.10, 3.11, 3.12, 3.13 |
| 5.0 | 3.10, 3.11, 3.12 |
| 4.2 | 3.8, 3.9, 3.10, 3.11, 3.12 |
| 4.1 | 3.8, 3.9, 3.10, 3.11 |
| 4.0 | 3.8, 3.9, 3.10 |
| 3.2 | 3.8, 3.9, 3.10 |

> [!IMPORTANT]  
> Always use a supported Python version. Older Python versions don't receive security updates.

## SQL Server version compatibility

The `mssql-django` backend works with all supported versions of SQL Server.

### Azure SQL services

| Service | Support status |
| --- | --- |
| Azure SQL Database | Fully supported |
| Azure SQL Managed Instance | Fully supported |

### Microsoft Fabric

| Endpoint | Support status |
| --- | --- |
| SQL database in Fabric | Fully supported |

## ODBC driver compatibility

The `mssql-django` backend requires an external ODBC driver, unlike the `mssql-python` driver which uses Direct Database Connectivity (DDBC).

As of `mssql-django` 1.7, the backend defaults to ODBC Driver 18 for SQL Server and automatically falls back to ODBC Driver 17 if version 18 isn't installed. You can override this by specifying the `driver` option in your database configuration.

| ODBC driver | Support status |
| --- | --- |
| Microsoft ODBC Driver 18 for SQL Server | Fully supported (default) |
| Microsoft ODBC Driver 17 for SQL Server | Fully supported (fallback) |
| FreeTDS ODBC driver | Supported |

For installation instructions, see [Download ODBC Driver for SQL Server](../../odbc/download-odbc-driver-for-sql-server.md).

## Operating system compatibility

The `mssql-django` backend runs anywhere Python and the ODBC driver are supported. The ODBC driver installation steps vary by operating system. See [Install mssql-django](installation.md) for platform-specific setup.

| Operating system | Architecture | Support status |
| --- | --- | --- |
| Windows 11 | x64, ARM64 | Supported |
| Windows Server 2019, 2022, 2025 | x64 | Supported |
| Ubuntu 22.04, 24.04 | x64, ARM64 | Supported |
| Debian 11, 12 | x64, ARM64 | Supported |
| Red Hat Enterprise Linux 8, 9 | x64, ARM64 | Supported |
| macOS 14, 15 | Intel, Apple Silicon (ARM64) | Supported |
| Alpine Linux | x64 | Supported (requires glibc compatibility layer or FreeTDS) |

## Feature compatibility

The following tables list Django and SQL Server features and their support status in the `mssql-django` backend. For more detail on unsupported features, see [Limitations and unsupported features in mssql-django](limitations.md).

### Django ORM features

| Feature | mssql-django support |
| --- | --- |
| Migrations | Yes |
| `QuerySet` API | Yes |
| `JSONField` | Yes (SQL Server 2016+) |
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
| Multiple Active Result Sets (MARS) | Yes (via `pyodbc`) |
| Stored procedures | Yes (via `cursor.execute`) |
| `SNAPSHOT` isolation | Yes (requires database-level config) |
| Read-only routing | Yes |

## Dependency requirements

The `mssql-django` package automatically installs the following dependencies:

| Dependency | Purpose | Required version |
| --- | --- | --- |
| Django | Web framework | >= 3.2 |
| `pyodbc` | ODBC database driver for Python | >= 3.0 |
| `pytz` | Time zone support (used by the backend for `datetime` conversion, regardless of `USE_TZ`) | Any |

The `mssql-django` backend also requires the Microsoft ODBC Driver for SQL Server to be installed on the host system. For more information, see [Install mssql-django](installation.md).

## Policy for versioning and breaking changes

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

When reporting an issue, include your Django version, Python version, SQL Server version, ODBC driver version, and a minimal reproduction of the problem.

### Contribute

Community contributions are welcome. For more information about the Contributor License Agreement (CLA) and submission process, see the [contributing guide](https://github.com/microsoft/mssql-django/blob/dev/CONTRIBUTING.md).

### Community

- Stack Overflow: Tag questions with `django` and `sql-server`.
- [Django documentation](https://docs.djangoproject.com/en/stable/)
- [Azure Python Developer Center](https://azure.microsoft.com/develop/python/)

## Related content

- [What's new in mssql-django](whats-new.md)
- [Install mssql-django](installation.md)
- [Limitations and unsupported features in mssql-django](limitations.md)
- [mssql-django configuration reference](configuration-reference.md)
- [mssql-django on GitHub](https://github.com/microsoft/mssql-django)
