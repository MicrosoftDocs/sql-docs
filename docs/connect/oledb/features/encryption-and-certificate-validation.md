---
title: Encryption and certificate validation
description: Learn about encryption and certificate validation for SQL Server connections. The OLE DB Driver for SQL Server supports encryption and certificate validation.
author: David-Engel
ms.author: v-davidengel
ms.date: 10/26/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "data access [OLE DB Driver for SQL Server], encryption"
  - "cryptography [OLE DB Driver for SQL Server]"
  - "MSOLEDBSQL, encryption"
  - "encryption [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server, encryption"
---
# Encryption and certificate validation

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]


[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] always encrypts network packets associated with logging in. If no certificate has been provisioned on the server when it starts up, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] generates a self-signed certificate, which is used to encrypt login packets.

Self-signed certificates don't guarantee security. The encrypted handshake is based on NT LAN Manager (NTLM). It's highly recommended you provision a verifiable certificate on SQL Server for secure connectivity. Transport Security Layer (TLS) can be made secure only with certificate validation.

Applications may also request encryption of all network traffic by using connection string keywords or connection properties. The keywords are "Encrypt" for OLE DB when using a provider string with **`IDbInitialize::Initialize`**, or "Use Encryption for Data" for ADO and OLE DB when using an initialization string with **`IDataInitialize`**. Encryption may also be configured on the client machine in the registry (See [Registry settings](./registry-settings.md#encryption-and-certificate-validation) for more details), using the **Force Protocol Encryption** option. By default, encryption of all network traffic for a connection requires a certificate being provisioned on the server. By setting your client to trust the certificate on the server, you might become vulnerable to man-in-the-middle attacks. If you deploy a verifiable certificate on the server, ensure you change the client settings about trusting the certificate to FALSE.

For information about connection string keywords, see [Using connection string keywords with OLE DB driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).

To enable encryption to be used when a certificate hasn't been provisioned on the server, the **`Force Protocol Encryption`** and the **`Trust Server Certificate`** client [registry settings](./registry-settings.md#encryption-and-certificate-validation) can be set. In this case, encryption will use a self-signed server certificate without validation if no verifiable certificate has been provisioned on the server.

## Encryption and certificate validation behavior

Application settings never reduce the level of security set in the registry (see [Registry settings](./registry-settings.md#encryption-and-certificate-validation) for more details), but may strengthen it. For example, if **`Force Protocol Encryption`** isn't set for the client, an application may request encryption itself. To guarantee encryption even when a server certificate hasn't been provisioned, an application may request encryption and enable `TrustServerCertificate`. However, if `TrustServerCertificate` isn't enabled in the client configuration, a provisioned server certificate is still required.

Version 19 of the OLE DB Driver for SQL Server introduces breaking changes in the encryption related APIs. For more information, see [Encryption property changes](../major-version-differences.md#encryption-property-changes).

### Major version 19

The following table describes the evaluation of the encryption settings:

| Force Protocol Encryption client setting | Connection string/connection attribute Encrypt/Use Encryption for Data | **Resulting encryption** |
|--|--|--|
| 0 | No/Optional | Optional |
| 0 | Yes/Mandatory (default) | Mandatory |
| 0 | Strict | Strict |
| 1 | No/Optional | Mandatory |
| 1 | Yes/Mandatory (default) | Mandatory |
| 1 | Strict | Strict |
| 2 | Ignored | Strict |

The following table describes the resulting encryption and validation:

| Encryption | Trust Server Certificate client setting | Connection string/connection attribute Trust Server Certificate | Result |
|--|--|--|--|
| Optional | N/A | N/A | Encryption only occurs for LOGIN packets. |
| Mandatory | 0 | Ignored | Encryption occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| Mandatory | 1 | No (default) | Encryption occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| Mandatory | 1 | Yes | Encryption always occurs, but may use a self-signed server certificate. |
| Strict | N/A | N/A | Encryption occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |

> [!CAUTION]
> The preceding table only provides a guide on the system behavior under different configurations. For secure connectivity, ensure that the client and server both require encryption (for server-side configuration, see [Configure Server for Forced Encryption](../../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md#configure-server)). Also ensure that the server has a verifiable certificate, and that the **`TrustServerCertificate`** setting on the client is set to FALSE.

> [!NOTE]
> Starting from version 19.2 of the OLE DB driver, TDS 8.0 connections can be configured to use TLS 1.3. For more details, see [TLS 1.3 support](../../../relational-databases/security/networking/tds-8-and-tls-1-3.md).

### Major version 18 with new authentication methods

For versions 18.x.x, to improve security, when the new **Authentication** or **Access Token** connection string keywords (or their corresponding properties) are used, the driver overrides the default encryption value by setting it to `yes`. Overriding happens at data source object initialization time. If encryption is set before initialization by any means, the value is respected and not overridden.

> [!NOTE]
> In ADO applications and in applications that obtain the `IDBInitialize` interface through `IDataInitialize::GetDataSource`, the Core Component implementing the interface explicitly sets encryption to its default value of `no`. As a result, the new authentication properties/keywords respect this setting and the encryption value **isn't** overridden. Therefore, it is **recommended** that these applications explicitly set `Use Encryption for Data=true` to override the default value.

To improve security, the new authentication methods respect the `TrustServerCertificate` setting (and its corresponding connection string keywords/properties) **independently of the client encryption setting**. As a result, server certificate is validated by default. The driver determines whether to validate the server certificate as follows:

| Trust Server Certificate client setting | Connection string/connection attribute Trust Server Certificate | **Certificate validation** |
|--|--|--|
| 0 | No (default) | Yes |
| 0 | Yes | Yes |
| 1 | No (default) | Yes |
| 1 | Yes | No |

The following table describes the evaluation of the encryption settings:

| Force Protocol Encryption client setting | Connection string/connection attribute Encrypt/Use Encryption for Data | **Resulting encryption** |
|--|--|--|
| 0 | No (default) | No |
| 0 | Yes | Yes |
| 1 | No (default) | Yes |
| 1 | Yes | Yes |

The following table describes the resulting encryption and validation:

| **Resulting encryption** | **Certificate validation** | Result |
|--|--|--|
| No | No | Encryption only occurs for LOGIN packets. |
| No | Yes | Encryption occurs for LOGIN packets only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| Yes | No | Encryption of all network traffic always occurs, but may use a self-signed server certificate. |
| Yes | Yes | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |

### Major version 18 with legacy authentication methods

The following table describes the encryption and validation outcome for legacy authentication methods:

| Force Protocol Encryption client setting | Trust Server Certificate client setting | Connection string/connection attribute Encrypt/Use Encryption for Data | Connection string/connection attribute Trust Server Certificate | Result |
|--|--|--|--|--|
| 0 | N/A | No (default) | N/A | Encryption only occurs for LOGIN packets. |
| 0 | N/A | Yes | No (default) | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| 0 | N/A | Yes | Yes | Encryption of all network traffic always occurs, but may use a self-signed server certificate. |
| 1 | 0 | Ignored | Ignored | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| 1 | 1 | No (default) | N/A | Encryption of all network traffic always occurs, but may use a self-signed server certificate. |
| 1 | 1 | Yes | No (default) | Encryption of all network traffic occurs only if there's a verifiable server certificate, otherwise the connection attempt fails. |
| 1 | 1 | Yes | Yes | Encryption of all network traffic always occurs, but may use a self-signed server certificate. |

## See also

[OLE DB driver for SQL server features](oledb-driver-for-sql-server-features.md)  
[Initialization and authorization properties](../ole-db-data-source-objects/initialization-and-authorization-properties.md)  
[Connection string keywords](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)  
[Major version differences](../major-version-differences.md)  
[Registry settings](./registry-settings.md)  
