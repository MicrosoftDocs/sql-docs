---
title: Support Lifecycle for mssql-python Driver
description: Learn about the support lifecycle, versioning policy, and compatibility for the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: lifecycle
ai-usage: ai-assisted
---

# Support lifecycle for mssql-python

The mssql-python driver is Microsoft's official Python driver for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. The driver is actively maintained on GitHub and released through PyPI. This article covers versioning, platform compatibility, and support timelines.

## Version support

Always use the latest release to get new features, performance improvements, and security fixes. New capabilities are only added to the current release.

### Current version

Version 1.13.0 is the current general availability (GA) release.

### Version history

| Version | Release date | Status | Key features |
| --------- | -------------- | -------- | -------------- |
| 1.13.0 | August 2026 | **Current** | Apache Arrow bulk copy, `token_provider` parameter, identity-aware connection pooling, required `mssql-python-odbc` companion package |
| 1.12.0 | July 2026 | Previous | Standalone `mssql-python-odbc` companion package, bulk copy connect-timeout fix, bulk copy CLR UDT fix |
| 1.11.0 | July 2026 | Previous | Context manager commit/rollback, Apple Silicon fix, service principal bulk copy fix |
| 1.10.0 | June 2026 | Previous | Bulk copy with ActiveDirectoryServicePrincipal, Arrow VARCHAR fixes |
| 1.9.0 | June 2026 | Previous | Bulk copy with Row objects, improved NULL parameter typing |
| 1.8.0 | May 2026 | Previous | Bulk copy with ActiveDirectoryMSI, Row string-key indexing |
| 1.7.1 | May 2026 | Previous | RHEL 8 wheels, broader concurrency and Unicode fixes |
| 1.6.0 | April 2026 | Previous | Parser-based connection string sanitization, connect/disconnect concurrency fixes |
| 1.5.0 | April 2026 | Previous | Arrow fetch, sql_variant, native UUID |
| 1.4.0 | February 2026 | Previous | Bulk copy support (BCP) |
| 1.3.0 | January 2026 | Previous | Settings class, module configuration |
| 1.2.0 | January 2026 | Previous | Schema discovery methods |
| 1.1.0 | December 2025 | Previous | Custom output converters |
| 1.0.0 | November 2025 | Previous | Initial GA release |

> [!IMPORTANT]
> Only the current version (1.13.0) receives new features and bug fixes. Previous versions remain functional but don't receive updates.

## Python version compatibility

The mssql-python driver requires Python 3.10 or later. If you're starting a new project, target the newest supported Python release in the compatibility matrix for the longest support window. If you're constrained to an older Python version (3.9 or earlier), use pyodbc instead.

| Python version | Support status |
| ---------------- | --------------- |
| Python 3.14 | Supported |
| Python 3.13 | Supported |
| Python 3.12 | Supported |
| Python 3.11 | Supported |
| Python 3.10 | Supported (minimum) |
| Python 3.9 and earlier | Not supported |

> [!IMPORTANT]
> Always use a supported Python version. Older Python versions don't receive security updates.

## SQL Server version compatibility

The mssql-python driver uses the TDS protocol and works with all actively supported SQL Server versions.

| SQL Server version | Support status |
| -------------------- | --------------- |
| All supported versions of Microsoft SQL Server | Fully supported |

### Azure SQL services

| Service | Support status |
| --------- | --------------- |
| Azure SQL Database | Fully supported |
| Azure SQL Managed Instance | Fully supported |
| Azure Synapse Analytics (dedicated pools) | Supported |

### Microsoft Fabric

| Endpoint | Support status |
| --------- | --------------- |
| SQL database in Fabric | Fully supported |
| Fabric Data Warehouse | Supported |
| SQL analytics endpoint (Lakehouse) | Supported |
| SQL analytics endpoint (mirrored database) | Supported |

## Operating system compatibility

The mssql-python driver uses DDBC (Direct Database Connectivity) and doesn't require the Microsoft ODBC Driver to be installed. Removing the external dependency simplifies deployment, especially in containers and CI/CD pipelines.

| Operating system | Architecture | Support status |
| ------------------ | -------------- | --------------- |
| Windows 11 | x64, ARM64 | Supported |
| Windows Server 2019, 2022, 2025 | x64 | Supported |
| Ubuntu 22.04, 24.04, 26.04 | x64, ARM64 | Supported |
| Debian 11, 12 | x64, ARM64 | Supported |
| Red Hat Enterprise Linux 8, 9 | x64, ARM64 | Supported |
| macOS 14, 15, 26 | Intel, Apple Silicon (ARM64) | Supported |
| Alpine Linux | x64 | Supported |

## Feature compatibility

The following tables list SQL Server features and their support status in the current version (1.13.0) of the mssql-python driver. If a feature you need isn't supported (such as Always Encrypted or MARS), consider using [pyodbc](/sql/connect/python/pyodbc/python-sql-driver-pyodbc) instead. For information about features added in earlier versions, see [What's new in mssql-python](whats-new.md).

### SQL Server features

| Feature | mssql-python support |
| --------- | --------------------- |
| Encrypted connections (TLS) | Supported |
| Always Encrypted | Not supported |
| Microsoft Entra authentication | Supported |
| Multiple Active Result Sets (MARS) | Not supported |
| Bulk copy (BCP) | Supported |
| Apache Arrow fetch | Supported |
| sql_variant columns | Supported |
| Native UUID | Supported |
| Connection resiliency | Supported |
| Read-only routing | Supported |

### DB-API 2.0 compliance

| Feature | Support |
| --------- | --------- |
| Module interface | Compliant |
| Connection interface | Compliant |
| Cursor interface | Mostly compliant |
| Type objects | Compliant |
| Exceptions | Compliant |
| `callproc()` | Not implemented |

> [!NOTE]
> The `callproc()` method isn't implemented. Call stored procedures by using `EXECUTE` statements with standard query methods.

## Dependency requirements

The mssql-python driver has minimal dependencies:

| Dependency | Purpose | Version |
| ------------ | --------- | --------- |
| azure-identity | Microsoft Entra ID authentication | Required (>= 1.12.0) |
| mssql-python-odbc | Companion package that ships the ODBC driver binaries | Required (== 18.6.2.1) as of mssql-python 1.13.0 |

The `pip install mssql-python` command installs the `mssql-python-odbc` companion package automatically. That package supplies the ODBC driver binaries that the driver loads at startup. You don't need to install the Microsoft ODBC Driver for SQL Server separately.

In mssql-python 1.13.0, the companion package is required at runtime. The driver no longer falls back to binaries inside the `mssql-python` wheel. Install `mssql-python-odbc==18.6.2.1` explicitly in these conditions:

- You install with `--no-deps`.
- You install from a private index that doesn't mirror the companion package.

## Migration from pyodbc

The mssql-python driver simplifies deployment (no ODBC driver required) and provides built-in connection pooling. The main differences to plan for when migrating:

| Consideration | Notes |
| --------------- | ------- |
| Parameter style | pyodbc uses `qmark` positional parameters. mssql-python defaults to `pyformat` named parameters and also supports `qmark` for compatibility. |
| Row access | Attribute access is supported in both. |
| Stored procedures | Use `EXECUTE` (no `callproc`). |
| Connection pooling | Built-in pooling in the mssql-python driver. |

See [Migrate from pyodbc](migrate-from-pyodbc.md) for detailed guidance.

## Migration from pymssql

The pymssql driver is no longer actively maintained. The mssql-python driver offers a supported alternative with similar connection syntax but modern features like built-in connection pooling and Microsoft Entra authentication.

| Consideration | Notes |
| --------------- | ------- |
| FreeTDS dependency | Not required. The mssql-python driver uses DDBC. |
| Connection parameters | Similar style, but different parameter names. |
| Row access | Both support dictionary-style and attribute access patterns. |
| Stored procedures | Use `EXECUTE` (no `callproc`). |

See [Migrate from pymssql](migrate-from-pymssql.md) for detailed guidance.

## Breaking changes policy

- **Major versions** (2.0, 3.0): Major version changes signify breaking API changes.
- **Minor versions** (1.4, 1.5): Include new features and maintain backward compatibility.
- **Patch versions** (1.4.1, 1.4.2): Include bug fixes only.

The team documents breaking changes in release notes and provides migration guides.

## How to stay current

The mssql-python driver releases minor versions frequently. Check for updates regularly, especially before production deployments.

### Check installed version

Verify which version of the driver is currently installed in your environment:

```python
import mssql_python
print(mssql_python.__version__)
```

### Upgrade to latest version

Update to the latest release to get the newest features and security fixes.

```bash
pip install --upgrade mssql-python
```

### Subscribe to updates

- Watch the [GitHub repository](https://github.com/microsoft/mssql-python).
- Check PyPI for new releases.
- Review the changelog for each release.

## Getting support

### GitHub Issues

Report bugs and request features on GitHub:

- [Open an issue](https://github.com/microsoft/mssql-python/issues)
- [View known issues](https://github.com/microsoft/mssql-python/issues?q=is%3Aissue+is%3Aopen)

### Documentation

- [Official documentation](python-sql-driver-mssql-python.md)
- [GitHub repository](https://github.com/microsoft/mssql-python)

### Community

- Stack Overflow: Tag questions with `mssql-python`.
- GitHub Discussions: Use for general questions and discussions.

## Related content

- [Microsoft Python Driver for SQL Server - mssql-python](python-sql-driver-mssql-python.md)
- [What's new in mssql-python](whats-new.md)
- [Install mssql-python](installation.md)
- [GitHub repository](https://github.com/microsoft/mssql-python)
