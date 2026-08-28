---
title: Encryption and TLS with mssql-python
description: Configure encrypted connections and TLS certificate validation with the mssql-python driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---

# Encryption and TLS with mssql-python

The mssql-python driver supports encrypted connections through Transport Layer Security (TLS). Encryption protects data in transit between your application and SQL Server, and prevents eavesdropping and adversary-in-the-middle attacks.

## Connection encryption

### Enable encryption

Use the `Encrypt` keyword to control connection encryption:

```python
import mssql_python

# Require encryption (recommended)
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
)
```

### Encrypt keyword values

| Value | Description |
| ------- | ------------- |
| `yes` | Requires encryption and fails if the server doesn't support TLS. |
| `no` | Doesn't encrypt the connection. Not recommended for production. |
| `strict` | Uses TDS 8.0 strict encryption mode. Requires SQL Server 2022 or later. |

> [!IMPORTANT]
> Azure SQL Database requires `Encrypt=yes`. Connections without encryption are rejected.

## Certificate validation

### TrustServerCertificate

Controls whether the driver validates the server's TLS certificate:

```python
# Validate server certificate (recommended for production)
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Encrypt=yes;"
    "TrustServerCertificate=no;"
)

# Skip certificate validation (development only)
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "TrustServerCertificate=yes;"
)
```

| Value | Description |
| ------- | ------------- |
| `no` | Validates the certificate against trusted CAs. This value is the default and recommended setting. |
| `yes` | Trusts any certificate and skips validation. This setting is insecure. |

> [!CAUTION]
> Setting `TrustServerCertificate=yes` in production exposes your application to adversary-in-the-middle attacks. Only use this setting for development with self-signed certificates.

## TDS 8.0 strict encryption

SQL Server 2022 and later support TDS 8.0 with strict encryption mode:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Encrypt=strict;"
)
```

Strict mode provides the highest security level:

- The driver establishes TLS before any TDS handshake.
- Certificate validation is required, and you can't use `TrustServerCertificate=yes`.
- This mode provides the highest level of protection against interception.

## HostNameInCertificate

When the server's certificate name doesn't match the connection server name:

```python
conn = mssql_python.connect(
    "Server=192.168.1.100;"  # IP address
    "Database=<database>;"
    "Encrypt=yes;"
    "TrustServerCertificate=no;"
    "HostNameInCertificate=<server>.contoso.com;"  # Certificate CN
)
```

This keyword is useful when:

- Your application connects through an IP address instead of a hostname.
- The server is behind a load balancer.
- The certificate uses a different subject name than the connection server name.

## Recommended configurations

### Azure SQL Database

Azure SQL Database requires encryption:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "TrustServerCertificate=no;"
)
```

### On-premises SQL Server (production)

For on-premises SQL Server with a CA-signed certificate, keep `TrustServerCertificate=no` so the driver validates the server identity:

```python
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "TrustServerCertificate=no;"
)
```

### Development with self-signed certificates

During development with self-signed certificates, set `TrustServerCertificate=yes` to skip certificate validation:

```python
conn = mssql_python.connect(
    "Server=localhost;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "TrustServerCertificate=yes;"
)
```

### SQL Server 2022 and later versions with strict mode

Use `Encrypt=strict` for TDS 8.0, which wraps the entire connection in TLS from the start:

```python
conn = mssql_python.connect(
    "Server=<server>;"
    "Database=<database>;"
    "Encrypt=strict;"
)
```

## Troubleshooting

### "SSL Provider: Certificate validation failure"

The server certificate couldn't be validated. Run these checks:

- Verify the certificate is from a trusted CA.
- Check that the certificate expiration date is valid.
- Verify the server hostname matches the certificate CN or SAN.
- For development, use `TrustServerCertificate=yes`.

### "TCP Provider: An existing connection was forcibly closed"

The server might require encryption, but the client didn't request it. Try these steps:

- Add `Encrypt=yes` to your connection string.
- Check the SQL Server configuration for the **Force Encryption** setting.

### "The certificate chain was issued by an authority that is not trusted"

The certificate's root CA isn't in the system's trusted root store. Run these checks:

- Install the CA certificate in the trusted root store.
- For Azure SQL, ensure your system trusts Microsoft's CA certificates.
- For development, use `TrustServerCertificate=yes`.

### Connection fails with strict encryption

TDS 8.0 strict mode has these requirements:

- The server must run SQL Server 2022 or later.
- The server must have a valid, trusted certificate.
- You can't set `TrustServerCertificate=yes`.

If the server doesn't support TDS 8.0, use `Encrypt=yes` instead.

## Related content

- [Connection strings for mssql-python](connection-strings.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Troubleshoot mssql-python](troubleshooting.md)
- [Encrypt connections to SQL Server by importing a certificate](../../../database-engine/configure-windows/configure-sql-server-encryption.md)
