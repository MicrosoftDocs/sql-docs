---
title: "Transport Security: Availability Groups & Database Mirroring"
description: Learn how to secure messages between databases participating in an Always On availability group or in a database mirroring session hosted on SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/19/2026
ms.service: sql
ms.subservice: database-mirroring
ms.topic: concept-article
helpviewer_keywords:
  - "sessions [SQL Server], database mirroring"
  - "cryptography [SQL Server], database mirroring"
  - "certificates [SQL Server], database mirroring"
  - "encryption [SQL Server], database mirroring"
  - "Windows authentication [SQL Server]"
  - "authentication [SQL Server], database mirroring"
  - "transport security"
  - "database mirroring [SQL Server], security"
---
# Transport security in availability groups and database mirroring

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Transport security involves authentication and, optionally, encryption of messages exchanged between the databases. For database mirroring and [!INCLUDE [ssHADR](../../includes/sshadr-md.md)], authentication and encryption are configured on the database mirroring endpoint. For an introduction to database mirroring endpoints, see [The database mirroring endpoint (SQL Server)](the-database-mirroring-endpoint-sql-server.md).

## Authentication

Authentication is the process of verifying that a user is who the user claims to be. Connections between database mirroring endpoints require authentication. Connection requests from a partner or witness, if any, must be authenticated.

The type of authentication used by a server instance for database mirroring or [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] is a property of the database mirroring endpoint. Two types of transport security are available for database mirroring endpoints: Windows Authentication (the Security Support Provider Interface (SSPI)) and certificate-based authentication.

### Windows Authentication

Under Windows Authentication, each server instance logs in to the other side using the Windows credentials of the Windows user account under which the process is running. Windows Authentication might require some manual configuration of login accounts, as follows:

- If the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] run as services under the same domain account, no extra configuration is required.

- If the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] run as services under different domain accounts (in the same or trusted domains), the login of each account must be created in `master` on each of the other server instances, and that login must be granted `CONNECT` permissions on the endpoint.

- If the instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] run as the Network Service account, the login of each host computer account (`<domain-name>\<computer-name>$`) must be created in `master` on each of the other servers, and that login must be granted `CONNECT` permissions on the endpoint. This is because a server instance running under the Network Service account authenticates using the domain account of the host computer.

> [!NOTE]  
> For an example of setting up a database mirroring session using Windows Authentication, see [Example: Configure database mirroring using Windows authentication](example-setting-up-database-mirroring-using-windows-authentication-transact-sql.md).

### Certificates

In some situations, such as when server instances aren't in trusted domains or when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running as a local service, Windows Authentication is unavailable. In such cases, instead of user credentials, certificates are required to authenticate connection requests. The mirroring endpoint of each server instance must be configured with its own locally created certificate.

The encryption method is established when the certificate is created. For more information, see [Database Mirroring - Use Certificates for Outbound Connections](database-mirroring-use-certificates-for-outbound-connections.md). Carefully manage the certificates that you use.

A server instance uses the private key of its own certificate to establish its identity when setting up a connection. The server instance that receives the connection request uses the public key of the sender's certificate to authenticate the sender's identity. For example, consider two server instances, `Server_A` and `Server_B`. `Server_A` uses its private key to encrypt the connection header before sending a connection request to `Server_B`. `Server_B` uses the public key of `Server_A`'s certificate to decrypt the connection header. If the decrypted header is correct, `Server_B` knows that the header was encrypted by `Server_A`, and the connection is authenticated. If the decrypted header is incorrect, `Server_B` knows that the connection request is inauthentic and refuses the connection.

<a id="DataEncryption"></a>

## Data Encryption

By default, a database mirroring endpoint requires encryption of data sent over mirroring connections. In this case, the endpoint can connect only to endpoints that also use encryption. Unless you can guarantee that your network is secure, we recommend that you require encryption for your database mirroring connections. However, you can disable encryption or make it supported, but not required. If encryption is disabled, data is never encrypted and the endpoint can't connect to an endpoint that requires encryption. If encryption is supported, data is encrypted only if the opposite endpoint either supports or requires encryption.

> [!NOTE]  
> Mirroring endpoints created by [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] are created with encryption either required or disabled. To change the encryption setting to `SUPPORTED`, use the `ALTER ENDPOINT` [!INCLUDE [tsql](../../includes/tsql-md.md)] statement. For more information, see [ALTER ENDPOINT](../../t-sql/statements/alter-endpoint-transact-sql.md).

Optionally, you can control the encryption algorithms that can be used by an endpoint, by specifying one of the following values for the `ALGORITHM` option in a `CREATE ENDPOINT` statement or `ALTER ENDPOINT` statement:

| `ALGORITHM` value | Description |
| --- | --- |
| **RC4** (default) | Specifies that the endpoint must use the RC4 algorithm.<br /><br />**Warning**: The RC4 algorithm is deprecated. [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use AES. |
| **AES** (recommended) | Specifies that the endpoint must use the AES algorithm. |
| **AES RC4** | Specifies that the two endpoints negotiate for an encryption algorithm with this endpoint giving preference to the AES algorithm. |
| **RC4 AES** | Specifies that the two endpoints negotiate for an encryption algorithm with this endpoint giving preference to the RC4 algorithm. |

If connecting endpoints specify both algorithms but in different orders, the endpoint accepting the connection wins.

> [!CAUTION]  
> Though considerably faster than AES, RC4 is a relatively weak algorithm, while AES is a relatively strong algorithm. Therefore, you should use the AES algorithm.

The RC4 algorithm is only supported for backward compatibility. Data can only be encrypted using RC4 or RC4_128 when the database is in compatibility level 90 or 100 (Not recommended). Use a newer algorithm such as one of the AES algorithms instead. In [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and later versions, data encrypted using RC4 or RC4_128 can be decrypted in any compatibility level.  

For information about the [!INCLUDE [tsql](../../includes/tsql-md.md)] syntax for specifying encryption, see [CREATE ENDPOINT](../../t-sql/statements/create-endpoint-transact-sql.md).

## Configure transport security for database mirroring endpoints

- [Create a Database Mirroring Endpoint for Windows Authentication](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)
- [Establish Database Mirroring Session - Windows Authentication](establish-database-mirroring-session-windows-authentication.md)
- [Create a Database Mirroring Endpoint for Windows Authentication](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)
- [Database Mirroring - Use Certificates for Outbound Connections](database-mirroring-use-certificates-for-outbound-connections.md)

## Related content

- [Choose an encryption algorithm](../../relational-databases/security/encryption/choose-an-encryption-algorithm.md)
- [ALTER ENDPOINT (Transact-SQL)](../../t-sql/statements/alter-endpoint-transact-sql.md)
- [DROP ENDPOINT (Transact-SQL)](../../t-sql/statements/drop-endpoint-transact-sql.md)
- [Security for SQL Server Database Engine and Azure SQL Database](../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [Manage Metadata When Making a Database Available on Another Server](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md)
- [The database mirroring endpoint (SQL Server)](the-database-mirroring-endpoint-sql-server.md)
- [sys.database_mirroring_endpoints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)
- [sys.dm_db_mirroring_connections (Database mirroring)](../../relational-databases/system-dynamic-management-objects/database-mirroring-sys-dm-db-mirroring-connections.md)
- [Troubleshoot Database Mirroring Configuration (SQL Server)](troubleshoot-database-mirroring-configuration-sql-server.md)
- [Troubleshoot Always On Availability Groups Configuration (SQL Server)](../availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)
