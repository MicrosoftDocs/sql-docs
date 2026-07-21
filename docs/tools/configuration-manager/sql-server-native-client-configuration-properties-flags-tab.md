---
title: "SQL Server Native Client Configuration Properties (Flags Tab)"
description: Find out about the options on the Flags tab of the SQL Server Native Client Configuration Properties dialog box.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# SQL Server Native Client Configuration Properties (Flags tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] clients on this machine, communicate with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] servers using the protocols provided in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Native Client library file. This page configures the client computer to request an encrypted connection using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL). If an encrypted connection can't be established, the connection fails.

The sign-in process is always encrypted. The options in this article apply only to encrypting data.

For more information about how [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] encrypts communication and for instructions on how to configure the client to trust the root authority of the server certificate, see:

- [Transport Layer Security and digital certificates](../../database-engine/configure-windows/certificate-overview.md)
- [SQL Server and client encryption summary](../../database-engine/configure-windows/sql-server-and-client-encryption-summary.md)

> [!IMPORTANT]  
> [!INCLUDE [snac-removed-oledb-and-odbc](../../includes/snac-removed-oledb-and-odbc.md)]

## Options

#### Force protocol encryption

Request a connection using TLS.

#### Trust Server Certificate

When set to **No**, the client process attempts to validate the server certificate. The client and server must each have a certificate issued from a public certification authority. If the certificate isn't present on the client computer, or if the validation of the certificate fails, the connection is terminated.

When set to **Yes**, the client doesn't validate the server certificate, enabling the use of a self-signed certificate.

**Trust Server Certificate** is only available if **Force protocol encryption** is set to **Yes**.
