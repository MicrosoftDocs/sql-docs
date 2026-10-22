---
title: Encryption and Certificate Validation in Microsoft.Data.SqlClient
description: Configure TLS encryption, validate SQL Server certificates, and choose Mandatory or Strict encryption with Microsoft.Data.SqlClient.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 09/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Encryption and certificate validation in Microsoft.Data.SqlClient

[!INCLUDE [Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Use Transport Layer Security (TLS) to encrypt traffic between your application and SQL Server. Keep server certificate validation enabled to verify the server's identity. Encryption without identity validation doesn't protect against an *adversary-in-the-middle* that impersonates the server.

Microsoft.Data.SqlClient defaults to `Encrypt=Mandatory` and `TrustServerCertificate=false`. `Encrypt=true` is a synonym for `Mandatory`. For a remote connection, explicitly request these settings and provision a server certificate that the client can validate:

```text
Server=tcp:<server>,1433;Database=<database>;Integrated Security=true;Encrypt=true;TrustServerCertificate=false;MultiSubnetFailover=true;
```

Replace the placeholders and use an [authentication method](sql/authentication-sql-server.md) supported by your server and application environment. For Azure SQL or SQL database in Microsoft Fabric, see [Microsoft Entra authentication](sql/azure-active-directory-authentication.md).

## Choose an encryption mode

`Encrypt` controls whether the client requires encryption. A server can also require encryption. This table describes current behavior without certificate pinning:

| Client setting | Server requirement | Certificate validation | Result |
| --- | --- | --- | --- |
| `Encrypt=Optional` or `false` | Doesn't force encryption. | None, regardless of `TrustServerCertificate`. | Only login packets are encrypted. Subsequent traffic isn't encrypted. |
| `Encrypt=Optional;TrustServerCertificate=false` | Forces encryption. | Trust chain, validity, and server name. | All traffic is encrypted. Invalid certificates fail the connection. |
| `Encrypt=Optional;TrustServerCertificate=true` | Forces encryption. | Bypassed. | All traffic is encrypted, but the server's identity isn't verified. |
| `Encrypt=Mandatory;TrustServerCertificate=false` | Either setting. | Trust chain, validity, and server name. | All traffic is encrypted. Invalid certificates fail the connection. |
| `Encrypt=Mandatory;TrustServerCertificate=true` | Either setting. | Bypassed. | All traffic is encrypted, but the server's identity isn't verified. |
| `Encrypt=Strict` | Supports Tabular Data Stream (TDS) 8.0. | Required. `TrustServerCertificate` can't bypass it. | TLS starts before TDS messages. An unsupported server or invalid certificate fails the connection. |

Use `Mandatory` for encrypted connections to servers that don't support TDS 8.0. Use `Strict` when your endpoint supports [TDS 8.0](../../relational-databases/security/networking/tds-8.md), including SQL Server 2022 (16.x) and later versions. TDS 8.0 supports TLS 1.2 and TLS 1.3; it doesn't require TLS 1.3. The negotiated TLS version also depends on the client, server, and operating system configuration.

`Optional` isn't a remedy for certificate errors. It can leave application data unencrypted when the server doesn't require encryption.

## Configure a verifiable server certificate

For normal certificate validation:

1. Provision a certificate that meets the [SQL Server certificate requirements](../../database-engine/configure-windows/certificate-requirements.md).
1. Include the name clients use to connect in the certificate's subject alternative name (SAN).
1. Make the issuing certificate authority and any required intermediate certificates trusted on every client host or container.
1. Configure SQL Server to use the certificate, and keep `TrustServerCertificate=false` in clients.
1. Plan certificate renewal before expiration, including any changes to names or issuing authorities.

A certificate from a public or enterprise certificate authority can satisfy these requirements. An enterprise certificate isn't automatically trusted inside a Linux container or on a separate developer computer.

### Connect through an alias

If the connection uses a Domain Name System (DNS) alias that isn't in the certificate, first consider issuing a certificate that includes the alias. Alternatively, set `HostNameInCertificate` to the expected name in the certificate:

```text
Server=tcp:sql-alias.contoso.com,1433;Database=<database>;Integrated Security=true;Encrypt=true;TrustServerCertificate=false;HostNameInCertificate=sql-server.contoso.com;MultiSubnetFailover=true;
```

`HostNameInCertificate` changes the name SqlClient expects during certificate validation. It doesn't change the network destination or bypass trust-chain and expiration checks. Leave it unset when the server name already matches the certificate.

### Pin a specific server certificate

`ServerCertificate` supplies a local certificate file for exact comparison with the server's certificate. Supported formats are Privacy-Enhanced Mail (PEM) and Distinguished Encoding Rules (DER), including `.cer` certificate files. Use it with `Encrypt=Mandatory` or `Encrypt=Strict`, and keep `TrustServerCertificate=false`.

```text
Server=tcp:<server>,1433;Database=<database>;Integrated Security=true;Encrypt=Strict;TrustServerCertificate=false;ServerCertificate=C:\certificates\sql-server.cer;MultiSubnetFailover=true;
```

Distribute the expected certificate through a trusted channel and protect the file from replacement. An exact certificate match is an alternative to normal chain and name validation, not an additional check on top of it. The certificate bytes must match; the same subject name or public key alone isn't enough. A missing, unreadable, invalid, or mismatched pin file fails validation.

Pinning couples client deployment to certificate rotation. Update the pin when the server certificate changes, including renewal. Don't obtain a pin by accepting an unverified certificate from the network.

SqlClient doesn't expose a public callback for arbitrary TLS server-certificate validation. `AccessTokenCallback` controls authentication token acquisition, not TLS validation.

## Development certificates and troubleshooting

Use a trusted development certificate with the correct server name. If you temporarily use `TrustServerCertificate=true` with `Encrypt=true` in an isolated development environment, the connection is encrypted but the client doesn't authenticate the server certificate.

> [!CAUTION]
> Don't deploy `TrustServerCertificate=true` as a production fix for certificate errors. It disables server identity validation with `Mandatory` and has no effect with `Strict`.

| Failure | Check |
| --- | --- |
| Certificate chain isn't trusted. | Install the appropriate trusted root and intermediates on the client. Verify the certificate SQL Server actually presents. |
| Certificate name doesn't match. | Compare `Server` with the certificate SAN. Correct the name, certificate, or intentional `HostNameInCertificate` override. |
| Certificate is expired. | Renew the server certificate and check the client clock. |
| Pinned certificate doesn't match or can't be loaded. | Check the file path, permissions, format, and deployed certificate. Update the pin through your trusted distribution process. |
| Strict encryption can't connect. | Confirm the endpoint supports TDS 8.0 and that its TLS configuration is compatible with the client. |

For server-side configuration, see [Certificate overview](../../database-engine/configure-windows/certificate-overview.md).

## Changes in encryption and certificate validation behavior

These compatibility notes explain behavior changes when upgrading older applications. New applications should use the current settings described in this article.

<a id="version-40"></a>
<a id="version-20"></a>
<a id="version-10"></a>

| Driver release | Change |
| --- | --- |
| 1.0 | `Encrypt=false` is the default. When the client doesn't request encryption, server-forced encryption doesn't trigger certificate validation. |
| 2.0 | Server-forced encryption honors `TrustServerCertificate` even when `Encrypt=false`. |
| 4.0 | Encryption becomes enabled by default. Upgrades can expose previously unnoticed certificate configuration errors. |
| 5.0 | Adds `Optional`, `Mandatory`, `Strict`, and `HostNameInCertificate`. |
| 5.1 | Adds `ServerCertificate` for certificate pinning. |
| 7.0.3 | Fixes managed-networking certificate pin validation to fail closed for missing, invalid, or mismatched files, even when normal platform validation succeeds. |

## Related content

- [Connection strings in ADO.NET](connection-strings.md)
- [Connection string syntax](connection-string-syntax.md)
- [Security best practices](sql/application-security-scenarios-sql-server.md)
