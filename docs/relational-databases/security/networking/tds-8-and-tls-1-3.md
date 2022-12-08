---
title: TDS 8.0 and TLS 1.3 support
description: This article discusses TDS 8.0 and TLS 1.3 support with SQL Server 2022
ms.service: sql
ms.subservice: security
ms.topic: conceptual
ms.custom:
- event-tier1-build-2022
author: VanMSFT
ms.author: vanto
ms.date: "05/24/2022"
monikerRange: ">= sql-server-ver16||>= sql-server-linux-ver16"
---

# TDS 8.0 and TLS 1.3 support

[!INCLUDE [SQL Server 2022](../../../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] supports Tabular Data Stream (TDS) 8.0, and Transport Layer Security (TLS) 1.3 when TDS 8.0 is used.

The [Tabular Data Stream (TDS)](/openspecs/windows_protocols/ms-tds/b46a581a-39de-4745-b076-ec4dbb7d13ec) protocol is an application layer protocol used by clients to connect to SQL Server, while SQL Server uses Transport Layer Security (TLS) to encrypt data that is transmitted across a network between an instance of SQL Server and a client application.

TDS is a secure protocol, but in previous versions of SQL Server, encryption could be turned off or not enabled. To meet the standards of mandatory encryption while using SQL Server, an iteration of the TDS protocol was introduced: TDS 8.0

The TLS handshake now precedes any TDS messages, wrapping the TDS session in TLS to enforce encryption, making TDS 8.0 aligned with HTTPS and other web protocols. This significantly contributes to TDS traffic manageability as standard network appliances are now able to filter and securely passthrough SQL queries.

Another benefit to TDS 8.0 is greater compatibility with TLS 1.3, and TLS standards to come. TDS 8.0 is also fully compatible with TLS 1.2 and previous TLS versions.

## How TDS works

The Tabular Data Stream (TDS) protocol is an application-level protocol used for the transfer of requests and responses between clients and database server systems. In such systems, the client will typically establish a long-lived connection with the server. Once the connection is established using a transport-level protocol, TDS messages are used to communicate between the client and the server.  

During the TDS session lifespan, there are three phases:

- Initialization
- Authentication
- Data exchange

Encryption is negotiated during the initial phase, but TDS negotiation happens over an unencrypted connection. The SQL Server connection will look like this for prior versions to TDS 8.0:

TCP handshake :arrow_right: TDS prelogin (cleartext) and response (cleartext) :arrow_right: TLS handshake :arrow_right: authentication (encrypted) :arrow_right: data exchange (could be encrypted or unencrypted)

With the introduction of TDS 8.0, the SQL Server connections are as follows:

TCP handshake :arrow_right: TLS handshake :arrow_right: TDS prelogin (encrypted) and response (encrypted) :arrow_right:  authentication (encrypted) :arrow_right: data exchange (encrypted)

## Differences between TLS 1.2 and TLS 1.3

TLS 1.3 reduces the number of round trips from two to one during the handshake phase, making it faster and more secure than TLS 1.2. The server hello packet containing server certificate is encrypted and one Round Trip Time (1-RTT) resumption is discontinued and replaced with 0-RTT resumption based on client key share. Added security of TLS 1.3 comes from discontinuing certain cyphers and algorithms.

Here's a list of algorithms and ciphers removed in TLS 1.3:

- RC4 Stream Cipher 
- RSA Key Exchange 
- SHA-1 Hash Function 
- CBC (Block) Mode Ciphers 
- MD5 Algorithm 
- Various non-ephemeral Diffie-Hellman groups 
- EXPORT-strength ciphers 
- DES 
- 3DES 

## Strict connection encryption

To leverage TDS 8.0, [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] added `strict` as an additional connection encryption type to SQL Server drivers (`Encrypt=strict`). Download the latest version of the ODBC or OLE DB drivers to use the `strict` connection encryption type.

- [ODBC Driver for SQL Server](../../../connect/odbc/download-odbc-driver-for-sql-server.md) version 18.1.2.1 or higher
- [OLE DB Driver for SQL Server](../../../connect/oledb/download-oledb-driver-for-sql-server.md) version 19.2.0 or higher

In order to prevent a man-in-the-middle attack with `strict` connection encryption, users won't be able to set the `TrustServerCertificate` option to **true** and trust any certificate the server provided. Instead, users would use the `HostNameInCertificate` option to specify the certificate that should be trusted. The certificate supplied by the server would need to pass the certificate validation.

## Additional changes to connection string encryption properties

The following additions are added to connection strings for encryption:

| Keyword | Default | Description |
|---|---|---|
| **Encrypt** | *false* | **Existing behavior** <br/> When `true`, SQL Server uses TLS encryption for all data sent between the client and server if the server has a certificate installed. Recognized values are `true`, `false`, `yes`, and `no`. For more information, see [Connection String Syntax](/dotnet/framework/data/adonet/connection-string-syntax).<br/><br/>**Change of behavior**<br/> When set to `strict`, SQL Server uses TDS 8.0 for all data sent between the client and server. <br/><br/>When set to `mandatory`, `true`, or `yes`, SQL Server uses TDS 7.x with TLS/SSL encryption for all data sent between the client and server if the server has a certificate installed. <br/><br/> When set to `optional`, `false`, or `no`, the connection uses TDS 7.x and would be encrypted only if required by the SQL Server. |
| **TrustServerCertificate** | *false* | **Existing behavior** <br/> Set to `true` to specify that the driver doesn't validate the server TLS/SSL certificate. If `true`, the server TLS/SSL certificate is automatically trusted when the communication layer is encrypted using TLS. <br/><br/> If `false`, the driver validates the server TLS/SSL certificate. If the server certificate validation fails, the driver raises an error and closes the connection. The default value is `false`. Make sure the value passed to `serverName` exactly matches the `Common Name (CN)` or DNS name in the `Subject Alternate Name` in the server certificate for a TLS/SSL connection to succeed. <br/><br/>**Change of behavior**<br/> If **Encrypt** is set to `strict`, this setting specifies the location of the certificate to be used for server certificate validation (exact match). The driver will support PEM, DER, and CER file extensions. <br/><br/> If Encrypt is set to `true` or `false`, and the `TrustServerCertificate` property is unspecified or set to `null`, `true` or `false`, the driver uses the `ServerName` property value on the connection URL as the host name to validate the SQL Server TLS/SSL certificate. |
| **HostNameInCertificate** | *null* | The host name to be used in validating the SQL Server TLS/SSL certificate. If the **HostNameInCertificate** property is unspecified or set to `null`, the driver uses the `ServerName` property value as the host name to validate the SQL Server TLS/SSL certificate. |

## Next steps

- [Connect to SQL Server with strict encryption](connect-with-strict-encryption.md)
