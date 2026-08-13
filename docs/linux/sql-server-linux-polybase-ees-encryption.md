---
title: PolyBase EES Encryption on Linux
description: Learn how PolyBase External Execution Service (EES) communication is encrypted on SQL Server for Linux in SQL Server 2025 CU 8 and later versions.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei, amitkh, atsingh
ms.date: 08/13/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17"
---

# PolyBase EES encryption (SQL Server on Linux)

[!INCLUDE [sqlserver2025-linux](../includes/applies-to-version/sqlserver2025-linux.md)]

This article describes how PolyBase External Execution Service (EES) communication is encrypted on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for Linux, starting with [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] Cumulative Update (CU) 8.

## Overview

PolyBase on Linux uses EES, which runs locally on the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] host, to run the ODBC drivers required for external connectivity. By default, a certificate encrypts communication between [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] services and EES.

EES uses a self-signed certificate that's generated automatically each time EES restarts. The generated certificate is valid for 365 days.

## Certificate details

| Item | Value |
| --- | --- |
| Certificate path | `/var/opt/mssql/polybase-ees` |
| Certificate file name | `ca.crt` |
| Certificate SAN | `subjectAltName=IP:127.0.0.1,DNS:localhost` |

## Bring your own certificate

To use your own certificate for EES, place your certificate file at the same location and with the same name that EES expects.

1. Copy your certificate to `/var/opt/mssql/polybase-ees/ca.crt`.

1. Restart the EES service to apply the certificate.

1. Validate that PolyBase external access works as expected.

## When to restart EES

Restart EES to roll back to the default self-signed certificate, to force regeneration, or to refresh a certificate that's nearing its 365-day expiration.

To restart EES on Linux, run the following command:

```bash
sudo systemctl restart mssql-ees.service
```

## Fallback behavior

If the certificate file is missing or invalid, PolyBase falls back to unencrypted communication between [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] services and EES.

## Related content

- [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md)
- [Connect to ODBC data sources with PolyBase on SQL Server on Linux](sql-server-linux-polybase.md)
- [Configure PolyBase to access external data with ODBC generic types](../relational-databases/polybase/polybase-configure-odbc-generic.md)
