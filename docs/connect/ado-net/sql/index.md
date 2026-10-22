---
title: SQL Server and ADO.NET
description: Find Microsoft.Data.SqlClient guidance for secure connections, authentication, availability, local development, data types, and SQL Server features.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# SQL Server and ADO.NET

[!INCLUDE [Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Use this guide to find SQL Server-specific connection, data, and feature documentation for .NET applications.

## Microsoft.Data.SqlClient

<xref:Microsoft.Data.SqlClient> provides the ADO.NET driver for SQL Server, Azure SQL, and supported Microsoft Fabric SQL endpoints. It implements the Tabular Data Stream (TDS) protocol and exposes connections, commands, parameters, transactions, and data readers.

Start with [Connect and query](../get-started-sqlclient-driver.md) for a first application. Feature and authentication support depends on the target service; don't assume every SQL Server feature is available in a cloud endpoint.

## Secure and reliable connections

| Task | Guide |
| --- | --- |
| Require encryption and validate the server's identity. | [Encryption and certificate validation](../encryption-and-certificate-validation.md) |
| Choose Windows integrated or SQL password authentication. | [SQL Server and Windows authentication](authentication-sql-server.md) |
| Use managed identity, workload identity, user sign-in, or access tokens. | [Microsoft Entra authentication](azure-active-directory-authentication.md) |
| Connect through failover and configure read-only routing. | [High availability and disaster recovery](sqlclient-support-high-availability-disaster-recovery.md) |
| Develop against a per-user SQL Server engine on Windows. | [LocalDB and local development](sqlclient-support-localdb.md) |
| Review permissions, query construction, secrets, and logging. | [Security best practices](application-security-scenarios-sql-server.md) |

## In this section

| Topic | Description |
| --- | --- |
| [SQL Server security](sql-server-security.md) | Database security features and application security guidance. |
| [SQL Server data types and ADO.NET](sql-server-data-types.md) | SQL Server data types and their .NET representations. |
| [SQL Server binary and large-value data](sql-server-binary-large-value-data.md) | Binary, large-value, and FILESTREAM data. |
| [SQL Server data operations in ADO.NET](sql-server-data-operations.md) | Bulk copy, multiple active result sets, asynchronous operations, and table-valued parameters. |
| [SQL Server features and ADO.NET](sql-server-features-adonet.md) | Database features used by SqlClient applications. |

For engine configuration and administration, see [SQL Server documentation](../../../sql-server/index.yml).
