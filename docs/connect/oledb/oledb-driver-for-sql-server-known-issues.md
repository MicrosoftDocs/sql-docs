---
title: Microsoft OLE DB Driver for SQL Server Known Issues
description: Find affected versions, fix release dates, upgrade guidance, and limitations for the Microsoft OLE DB Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, wiassaf, davidengel, sunilbs, vbeiranvand
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
---
# Microsoft OLE DB Driver for SQL Server known issues

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../includes/driver_oledb_download.md)]

Use this article to check whether a known issue affects your driver version and find the release that fixes it. For resolved issues, [download the latest driver](download-oledb-driver-for-sql-server.md) rather than installing an older release that first contained the fix. Review the [installation requirements](applications/installing-oledb-driver-for-sql-server.md) before upgrading.

For symptom-based checks of provider registration, authentication, certificates, networking, parameters, and timeouts, start with [Troubleshooting](troubleshooting.md).

## Known issues

Select an issue for its error text, cause, and resolution. Reported dates use the issue's creation date in the tracking system, in Coordinated Universal Time (UTC). The fix release date is when the documented fix became available, not the date the issue was closed.

| Issue | Affected driver version | Reported date | Status | First fixed version | Fix release date | Recommended action |
| --- | --- | --- | --- | --- | --- | --- |
| [Authentication library architecture mismatch](#authentication-library-architecture-mismatch) | 19.2.0 | November 16, 2022 | Resolved | [19.3.0](release-notes-for-oledb-driver-for-sql-server.md#1930) | February 14, 2023 | Upgrade to the [latest driver](download-oledb-driver-for-sql-server.md). |

### Authentication library architecture mismatch

The driver returns this error:

> Error loading adal.dll from system directory. Verify that Active Directory Authentication Library for SQL Server is properly installed

The 19.2.0 installer places a 64-bit Active Directory Authentication Library (ADAL) in the 32-bit system directory.

To resolve the issue, upgrade to the [latest driver](download-oledb-driver-for-sql-server.md). The 19.3.0 release corrects this installer defect and includes ADAL 3.6.1 with the correct architecture.

## Limitations

These entries describe configuration and support requirements, rather than defects with a fixed driver version or resolution date.

| Requirement | Applies to | Recommended action |
| --- | --- | --- |
| Linked-server encryption and certificate configuration. | Linked servers that use `MSOLEDBSQL19`. | Configure encryption and a trusted server certificate. For the supported encryption options and certificate requirements, see [SQL Server 2025 and MSOLEDBSQL version 19](../../relational-databases/linked-servers/linked-servers-database-engine.md#sql-server-2025-and-msoledbsql-version-19). |
| Supported SQL Server version for linked servers. | `MSOLEDBSQL19` is supported for linked servers on SQL Server 2022 and later versions. | Upgrade the SQL Server instance that hosts the linked server to a supported version. Updating the driver alone doesn't change the SQL Server version requirement. |

## Related content

- [Release notes for OLE DB Driver for SQL Server](release-notes-for-oledb-driver-for-sql-server.md)
- [Installing OLE DB Driver for SQL Server](applications/installing-oledb-driver-for-sql-server.md)
- [Support policies for OLE DB Driver for SQL Server](applications/support-policies-for-oledb-driver-for-sql-server.md)
