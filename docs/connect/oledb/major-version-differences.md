---
title: "MSOLEDBSQL Major Version Differences"
description: Learn about breaking changes between OLE DB Driver 19 and version 18, including encryption defaults, property type changes, and migration steps.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 08/26/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "MSOLEDBSQL, additional resources"
  - "MSOLEDBSQL19, additional resources"
  - "OLE DB Driver for SQL Server, additional resources"
  - "OLE DB Driver 19 for SQL Server, additional resources"
---
# Major version differences in MSOLEDBSQL

This article describes breaking changes between Microsoft OLE DB Driver 19 for SQL Server and earlier versions.

> [!TIP]  
> **MSOLEDBSQL19** (Microsoft OLE DB Driver 19 for SQL Server) is the current recommended OLE DB driver. It supports TDS 8.0 and modern security features. Version 19.2.0 and later versions also support TLS 1.3. Use `Provider=MSOLEDBSQL19` in your connection strings.

## Summary of changes

| Area | Version 18 and earlier versions | Version 19 and later versions |
| --- | --- | --- |
| Default encryption (`Encrypt`) setting | `no` (no encryption or the server can require encryption) | `Mandatory` (encryption required) |
| `Encrypt` property type | `VT_BOOL` | `VT_BSTR` |
| `Encrypt` valid values | `no`/`yes` | `no`/`yes`/`true`/`false`/`Optional`/`Mandatory`/`Strict` |
| Certificate validation | Skipped when client sets `Encrypt=no` | Always evaluated when encryption occurs |
| Driver name | `MSOLEDBSQL` | `MSOLEDBSQL19` |
| CLSID | `MSOLEDBSQL_CLSID` (legacy) | `MSOLEDBSQL_CLSID` (updated in header) |

> [!WARNING]  
> **ActiveDirectoryPassword authentication is deprecated**. If you're migrating from version 18 to 19 and using `Authentication=ActiveDirectoryPassword`, plan to migrate to a more secure authentication method. See [Deprecated authentication methods](#deprecated-authentication-methods) for alternatives.

## Encryption property changes

### Encrypt property type change

The driver property `SSPROP_INIT_ENCRYPT` changes from `VT_BOOL` to `VT_BSTR`.

| Connection string | Version 18 values | Version 19 values |
| --- | --- | --- |
| Provider: `Encrypt` | `no`/`yes` | `no`/`yes`/`true`/`false`/`Optional`/`Mandatory`/`Strict` |
| IDataInitialize: `Use Encryption for Data` | `true`/`false` | `no`/`yes`/`true`/`false`/`Optional`/`Mandatory`/`Strict` |

**Value mapping**:

| Mode | Equivalent values | Behavior |
| --- | --- | --- |
| `Optional` | `no`, `false` | Unencrypted unless server requires it |
| `Mandatory` (default) | `yes`, `true` | Encrypted connection required |
| `Strict` | *(no equivalent)* | TDS 8.0 encryption; requires SQL Server 2022 and later versions |

> [!TIP]  
> Starting with version 19.2.0, TDS 8.0 connections can use TLS 1.3 when connecting to SQL Server 2022 or later. The `ServerCertificate` property was also added in this version. For more information, see [TLS 1.3 support](../../relational-databases/security/networking/tls-1-3.md).

For backward compatibility, version 19 accepts all version 18 values (`yes`/`no`) in addition to the new values (`Optional`/`Mandatory`/`Strict`).

### Default encryption behavior

| Version | Default | Result |
| --- | --- | --- |
| 18 and earlier versions | `no` | Connections unencrypted by default |
| 19 and later versions | `Mandatory` | Connections encrypted by default |

To restore version 18 behavior, add one of these options to your connection string:

- Provider: `Encrypt=Optional;`
- IDataInitialize: `Use Encryption for Data=Optional;`

### Certificate validation behavior

| Scenario | Version 18 | Version 19 and later versions |
| --- | --- | --- |
| Client sets `Encrypt=no`, server doesn't force encryption | No validation | No validation |
| Client sets `Encrypt=no`, server forces encryption | `Trust Server Certificate` **ignored** | `Trust Server Certificate` **evaluated** |
| Client sets `Encrypt=yes` | `Trust Server Certificate` evaluated | `Trust Server Certificate` evaluated |

#### Compatibility notes

Version 19 clients using default settings fail to connect when the server forces encryption and uses an untrusted certificate. Update your `Trust Server Certificate` setting or use a trusted certificate.

`TrustServerCertificate` was **not removed** in version 19. The option still works. Version 18 ignored this setting when `Encrypt` was set to `no`, even when the server forced encryption. Version 19 now evaluates `TrustServerCertificate` in all encrypted scenarios.

The version 19 driver, before 19.4.1, had an installer issue that could set the `TrustServerCertificate` registry option to `no` on systems that previously had v18 installed. When this problem occurred, the driver would use the more secure registry setting, which could make connection string options appear to have no effect. This issue was resolved in version 19.4.1. A fresh installation of v19 (without v18 present) always correctly defaulted the registry option to `yes`. For more information, see [Registry settings](features/registry-settings.md).

Keyword format differs by interface:

- Provider connection strings use no spaces: `TrustServerCertificate=yes;`
- IDataInitialize connection strings use spaces: `Trust Server Certificate=yes;`

For more information, see [Encryption and certificate validation in OLE DB](features/encryption-and-certificate-validation.md).

### Registry settings for Force Protocol Encryption

The **Force Protocol Encryption** registry setting uses numeric values that map to encryption modes:

| Registry value | Encryption mode | Description |
| --- | --- | --- |
| `0` | `Optional` | Encryption only if server requires it |
| `1` | `Mandatory` | Encryption required |
| `2` | `Strict` | TDS 8.0 encryption |

The driver uses the most secure option between the registry setting and the connection property. For registry key locations, see [Registry settings](features/registry-settings.md).

## Driver name changes

Version 19 supports side-by-side installation with version 18. The driver name includes the major version number for differentiation.

| Interface | Version 18 | Version 19 |
| --- | --- | --- |
| Provider keyword | `MSOLEDBSQL` | `MSOLEDBSQL19` |
| CLSID constant | `MSOLEDBSQL_CLSID` | `MSOLEDBSQL_CLSID` (updated in `msoledbsql.h`) |
| UI display name | Microsoft OLE DB Driver for SQL Server | Microsoft OLE DB Driver 19 for SQL Server |

### Migration steps

1. Include the updated `msoledbsql.h` header in your project.
1. For `IDBInitialize`: No changes needed (CLSID updated in header).
1. For `IDataInitialize`: Change `Provider=MSOLEDBSQL` to `Provider=MSOLEDBSQL19`.
1. For UI tools (SSMS, data link properties): Select **Microsoft OLE DB Driver 19 for SQL Server**.

### Connection string examples

Version 18 (before):

```text
Provider=MSOLEDBSQL;Server=myserver;Database=mydb;Trusted_Connection=yes;
```

Version 19 (after):

```text
Provider=MSOLEDBSQL19;Server=myserver;Database=mydb;Trusted_Connection=yes;
```

Version 19 with explicit encryption settings:

```text
Provider=MSOLEDBSQL19;Server=myserver;Database=mydb;Encrypt=Mandatory;TrustServerCertificate=no;
```

Version 19 with Strict encryption (TDS 8.0):

```text
Provider=MSOLEDBSQL19;Server=myserver;Database=mydb;Encrypt=Strict;ServerCertificate=C:\certs\server.cer;
```

## New version 19 properties

Version 19 introduces properties for enhanced certificate validation with `Strict` encryption mode.

### HostNameInCertificate (v19.0.0 and later versions)

Specifies the host name to validate against the server's TLS/SSL certificate. Use this property when the server name in the connection string differs from the certificate's Common Name (CN) or Subject Alternative Name (SAN).

| Interface | Property |
| --- | --- |
| Provider keyword | `HostNameInCertificate` |
| IDataInitialize keyword | `Host Name In Certificate` |
| OLE DB property | `SSPROP_INIT_HOST_NAME_CERTIFICATE` |

> [!NOTE]  
> This property is ignored when `Trust Server Certificate` is enabled. When `Encrypt=Strict`, the certificate is always validated.

### ServerCertificate (v19.2.0 and later versions)

Specifies the path to a certificate file (PEM, DER, or CER format) for exact certificate matching. The driver compares this certificate against the server's certificate during the TLS handshake.

| Interface | Property |
| --- | --- |
| Provider keyword | `ServerCertificate` |
| IDataInitialize keyword | `Server Certificate` |
| OLE DB property | `SSPROP_INIT_SERVER_CERTIFICATE` |

> [!IMPORTANT]  
> `ServerCertificate` can only be used when `Encrypt=Strict`. Attempting to use it with `Mandatory` or `Optional` encryption results in a connection error.

## Deprecated authentication methods

### ActiveDirectoryPassword

The `ActiveDirectoryPassword` authentication method (Microsoft Entra ID Password authentication) is deprecated. This authentication is based on the [OAuth 2.0 Resource Owner Password Credentials (ROPC) grant](/entra/identity-platform/v2-oauth-ropc), which is incompatible with multifactor authentication (MFA) and poses security risks.

> [!WARNING]  
> Microsoft is moving away from this high-risk authentication flow to protect users from malicious attacks. Plan to migrate to a more secure authentication method before this option is removed. For more information, see [Planning for mandatory multifactor authentication for Azure](/entra/identity/authentication/concept-mandatory-multifactor-authentication).

#### Recommended alternatives

| Scenario | Recommended authentication | Connection string keyword |
| --- | --- | --- |
| Interactive user context | Multifactor authentication | `Authentication=ActiveDirectoryInteractive` |
| App running on Azure | Managed Identity | `Authentication=ActiveDirectoryMSI` |
| Service/daemon without user | Service Principal | `Authentication=ActiveDirectoryServicePrincipal` |

For more information, see [Use Microsoft Entra ID](features/using-azure-active-directory.md).

## Troubleshooting

### Connection fails with certificate validation error

**Symptom**: Connection fails with a certificate validation error or untrusted certificate message.

**Cause**: Version 19 defaults to `Encrypt=Mandatory`, which requires a valid server certificate. Version 18 defaulted to `Encrypt=no` (unencrypted).

**Solutions**:

- **Recommended**: Install a trusted certificate on the server.
- **Development only**: Add `TrustServerCertificate=yes;` to your connection string (not recommended for production).
- **Fallback**: Add `Encrypt=Optional;` to restore version 18 behavior (reduces security).

### Connection fails with "Server Certificate can only be used with strict encryption"

**Symptom**: Connection fails when you use the `ServerCertificate` property.

**Cause**: The `ServerCertificate` property requires `Encrypt=Strict`.

**Solution**: Either remove `ServerCertificate` from your connection string, or change to `Encrypt=Strict;`.

### Application receives VT_BOOL error when setting Encrypt property

**Symptom**: Setting `SSPROP_INIT_ENCRYPT` with a boolean value fails.

**Cause**: Version 19 changed the property type from `VT_BOOL` to `VT_BSTR`.

**Solution**: Use string values (`"Mandatory"`, `"Optional"`, `"Strict"`, `"yes"`, `"no"`) instead of boolean values.

### Provider not found after upgrading

**Symptom**: Application fails with "Provider not found" or similar error.

**Cause**: Version 19 uses a different provider name (`MSOLEDBSQL19`).

**Solutions**:

- Update your connection string from `Provider=MSOLEDBSQL` to `Provider=MSOLEDBSQL19`.
- Include the updated `msoledbsql.h` header if using `IDBInitialize` with the CLSID.

## Related content

- [Microsoft OLE DB Driver for SQL Server](oledb-driver-for-sql-server.md)
- [Using connection string keywords with OLE DB Driver for SQL Server](applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Encryption and certificate validation in OLE DB](features/encryption-and-certificate-validation.md)
- [Universal Data Link (UDL) configuration](help-topics/data-link-pages.md)
- [SQL Server Login dialog box (OLE DB)](help-topics/sql-server-login-dialog.md)
- [Initialization and authorization properties](ole-db-data-source-objects/initialization-and-authorization-properties.md)
- [Registry settings](features/registry-settings.md)
