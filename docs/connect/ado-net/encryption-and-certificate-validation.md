---
title: Encryption and certificate validation
description: Learn about encryption and certificate validation for SQL Server connections. Encrypt and trust server certificate settings affect the security of your connections.
author: David-Engel
ms.author: davidengel
ms.date: 04/25/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Encryption and certificate validation in Microsoft.Data.SqlClient

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always encrypts network packets associated with logging in. If no certificate has been provisioned on the server when it starts up, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generates a self-signed certificate, which is used to encrypt login packets.

At a high level, encryption between a client and server ensures data is only readable by the client and server. An important part of the encryption process is server certificate validation. Server certificate validation allows the client to ensure the server is who it says it is. The certificate is validated for things like expiry, trust chain, and that the name in the certificate matches the name of the server the client is connecting to. For more information, see [Transport Layer Security and digital certificates](../../database-engine/configure-windows/certificate-overview.md).

It's highly recommended you provision a verifiable certificate on SQL Server for secure connectivity. Transport Security Layer (TLS) can be made secure only with certificate validation.

Applications may request encryption of all network traffic by using the `Encrypt` connection string keyword or connection property. By default, encryption of all network traffic for a connection requires a certificate being provisioned on the server. By setting your client to trust the certificate on the server, you might become vulnerable to man-in-the-middle attacks. If you deploy a verifiable certificate on the server, ensure client `Encrypt` settings are `True` and `Trust Server Certificate` settings are `False`.

To enable encryption to be used when a certificate hasn't been provisioned on the server, the `Encrypt` and `Trust Server Certificate` client settings can be used. In this case, encryption uses a self-signed server certificate without validation by the client. This configuration encrypts the connection but doesn't prevent devices in between the client and server from intercepting the connection and proxying the encryption.

## Changes in encryption and certificate validation behavior

Version 4.0 of Microsoft.Data.SqlClient introduces breaking changes in the encryption settings. `Encrypt` now defaults to `True`.

Version 2.0 of Microsoft.Data.SqlClient introduces breaking changes in the behavior of the `Trust Server Certificate` setting. Previously, if `Encrypt` was set to `False`, the server certificate wouldn't be validated, regardless of the `Trust Server Certificate` setting. Now, the server certificate is validated based on the `Trust Server Certificate` setting if the server forces encryption, even if `Encrypt` is set to `False`.

### Version 4.0

The following table describes the encryption and validation outcome for encryption and certificate settings:

| `Encrypt` client setting | `Trust Server Certificate` client setting | `Force encryption` server setting | Result |
|--|--|--|--|
| False | False (default) | No | Encryption only occurs for LOGIN packets. Certificate isn't validated. |
| False | False (default) | Yes | **(Behavior change from version 1.0 to 2.0)** Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| False | True | Yes | Encryption of all network traffic occurs, and the certificate isn't validated. |
| True **(new default)** | False (default) | N/A | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| True **(new default)** | True | N/A | Encryption of all network traffic occurs, but the certificate isn't validated. |
| Strict (added in version 5.0) | N/A | N/A | Encryption of all network traffic occurs using TDS 8 only if there's a verifiable server certificate, otherwise the connection attempt fails. |

> [!CAUTION]
> The preceding table only provides a guide on the system behavior under different configurations. For secure connectivity, ensure that the client and server both require encryption. Also ensure that the server has a verifiable certificate, and that the `TrustServerCertificate` setting on the client is set to `False`.

Starting in version 5.0 of Microsoft.Data.SqlClient, `HostNameInCertificate` is a new connection option. Server certificate validation ensures that the Common Name (CN) or Subject Alternate Name (SAN) in the certificate matches the server name being connected to. In some cases, like DNS aliases, the server name might not match the CN or SAN. The `HostNameInCertificate` value can be used to specify a different, expected CN or SAN in the server certificate.

### Version 2.0

Starting in version 2.0, when the server forces encryption, the client validates the server certificate based on the `Trust Server Certificate` setting, regardless of the `Encrypt` setting.

The following table describes the encryption and validation outcome for encryption and certificate settings:

| `Encrypt` client setting | `Trust Server Certificate` client setting | `Force encryption` server setting | Result |
|--|--|--|--|
| False (default) | False (default) | No | Encryption only occurs for LOGIN packets. Certificate isn't validated. |
| False (default) | False (default) | Yes | **(Behavior change)** Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| False (default) | True | Yes | Encryption of all network traffic occurs, and the certificate isn't validated. |
| True | False (default) | N/A | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| True | True | N/A | Encryption of all network traffic occurs, but the certificate isn't validated. |

> [!CAUTION]
> The preceding table only provides a guide on the system behavior under different configurations. For secure connectivity, ensure that the client and server both require encryption. Also ensure that the server has a verifiable certificate, and that the `TrustServerCertificate` setting on the client is set to `False`.

### Version 1.0

The following table describes the encryption and validation outcome for encryption and certificate settings:

| `Encrypt` client setting | `Trust Server Certificate` client setting | `Force encryption` server setting | Result |
|--|--|--|--|
| False (default) | False (default) | No | Encryption only occurs for LOGIN packets. Certificate isn't validated. |
| False (default) | False (default) | Yes | Encryption of all network traffic occurs, but the certificate isn't validated. |
| False (default) | True | Yes | Encryption of all network traffic occurs, and the certificate isn't validated. |
| True | False (default) | N/A | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| True | True | N/A | Encryption of all network traffic occurs, but the certificate isn't validated. |

## See also

[Connection strings](connection-strings.md)  
[Connection string syntax](connection-string-syntax.md)  
