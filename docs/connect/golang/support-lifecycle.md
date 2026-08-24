---
title: "go-mssqldb Support and Lifecycle"
description: "Go version support, SQL Server version compatibility, and support lifecycle for the go-mssqldb driver."
author: dlevy-msft
ms.author: dlevy
ms.date: 06/23/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# go-mssqldb support and lifecycle

This article describes the Go version requirements, SQL Server version compatibility, and support model for the `go-mssqldb` driver.

## Go version support

The `go-mssqldb` driver is tested against the two most recent stable Go releases. When a new Go version is released, the driver team validates compatibility and updates the continuous integration (CI) matrix.

| Driver version | Minimum Go version |
| --- | --- |
| v1.10.x | Go 1.25 |
| v1.9.x | Go 1.23 |
| v1.8.x | Go 1.18 |
| v1.7.x | Go 1.17 |

## SQL Server version compatibility

The driver works with the following Microsoft SQL platforms:

| Platform | Supported versions |
| --- | --- |
| SQL Server | All supported versions |
| Azure SQL Database | All tiers |
| Azure SQL Managed Instance | All tiers |
| SQL database in Microsoft Fabric | All tiers |
| Fabric Data Warehouse | All tiers |
| Azure Synapse Analytics | Serverless and dedicated SQL pools |

> [!NOTE]
> Older, out-of-support SQL Server versions might not support TLS 1.2 or later. To connect to these versions, set `encrypt=false` or upgrade to a supported SQL Server version. See [Limitations](known-limitations.md) for more information.

## Operating system support

The driver runs on any platform supported by Go:

| Operating system | Notes |
| --- | --- |
| Windows | Windows authentication (SSPI) is available natively. |
| Linux | Kerberos and NTLM authentication supported. See [Linux and macOS](linux-macos.md). |
| macOS | Kerberos and NTLM authentication supported. See [Linux and macOS](linux-macos.md). |

## Support model

The `go-mssqldb` driver is an open-source project hosted on [GitHub](https://github.com/microsoft/go-mssqldb). Microsoft maintains the driver and accepts community contributions.

- **Bug reports** - File an issue on the [GitHub issue tracker](https://github.com/microsoft/go-mssqldb/issues).
- **Feature requests** - File an issue with the `enhancement` label.
- **Security issues** - Follow the reporting instructions in the [SECURITY.md](https://github.com/microsoft/go-mssqldb/blob/main/SECURITY.md) file.
- **Commercial support** - For Azure SQL Database and Azure SQL Managed Instance, open a support ticket through the Azure portal.

## Related content

- [What's new in go-mssqldb](whats-new.md)
- [Install the go-mssqldb driver](installation.md)
- [go-mssqldb Limitations](known-limitations.md)
