---
title: MSSQLSERVER_10054
description: "MSSQLSERVER_10054"
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 02/29/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "10054 (Database Engine error)"
---
# MSSQLSERVER_10054

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 10054 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | |
| Message Text | A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: TCP Provider, error: 0 - An existing connection was forcibly closed by the remote host.) (Framework Microsoft SqlClient Data Provider). |

## Explanation

A connection to the server was attempted and established, but then before signing in, an error occurred.

This error can occur when trying to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using a lower level of encryption than required.

## User action

To find the cause of the error, review the configuration for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For information on how to encrypt connections for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

For more information related to troubleshooting, see [An existing connection was forcibly closed by the remote host (OS error 10054)](/troubleshoot/sql/database-engine/connect/tls-exist-connection-closed).

## Related content

- [Special cases for encrypting connections to SQL Server](../../database-engine/configure-windows/special-cases-for-encrypting-connections-sql-server.md)
- [Certificate requirements for SQL Server](../../database-engine/configure-windows/certificate-requirements.md)
- [TDS 8.0](../security/networking/tds-8.md)
