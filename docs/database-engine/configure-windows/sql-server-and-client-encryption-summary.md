---
title: SQL Server and client encryption summary
description: Learn about the steps required to encrypt all connections to the SQL Server, enable encryption connections from specific clients and check if the encryption works.
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: randolphwest
ms.date: 12/08/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# SQL Server and client encryption summary

This article provides a summary of various scenarios and associated procedures for enabling encryption to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and also how to verify encryption is working.

## Encrypt all connections to the server (Server-side encryption)

| Type of certificate | Force encryption in server properties | Import server certificate on each client | Trust Server certificate setting | Encrypt property in the connection string | Comments |
| --- | --- | --- | --- | --- | --- |
| [Self-signed certificate - automatically created by SQL Server](special-cases-for-encrypting-connections-sql-server.md#scenario-1-you-want-to-encrypt-all-the-connections-to-sql-server) | Yes | Can't be done | Yes | Ignored | [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and earlier versions use the SHA1 algorithm. [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions use SHA256. For more information, see [Changes to hashing algorithm for self-signed certificate in SQL Server 2017](https://techcommunity.microsoft.com/t5/sql-server-support-blog/changes-to-hashing-algorithm-for-self-signed-certificate-in-sql/ba-p/319026). We don't recommend this approach for production use. |
| [Self-signed certificate created by using New-SelfSignedCertificate or makecert - Option 1](special-cases-for-encrypting-connections-sql-server.md#scenario1option1) | Yes | No | Yes | Ignored | We don't recommend this approach for production use. |
| [Self-signed certificate created by using New-SelfSignedCertificate or makecert - Option 2](special-cases-for-encrypting-connections-sql-server.md#scenario1option2) | Yes | Yes | Optional | Ignored | We don't recommend this approach for production use. |
| Company's certificate server or from a Certificate Authority (CA) that's not in the [List of Participants - Microsoft Trusted Root Program - Option 1](/security/trusted-root/participants-list) | Yes | No | Yes | Ignored | |
| Company's certificate server or from a Certificate Authority (CA) that's not in the [List of Participants - Microsoft Trusted Root Program - Option 2](/security/trusted-root/participants-list) | Yes | Yes | Optional | Ignored | |
| Trusted root authorities | Yes | No | Optional | Ignored | We recommend this approach. |

## Encrypt connections from specific client

| Type of certificate | Force encryption in server properties | Import server certificate on each client | Specify Trust Server certificate setting on the client | Manually specify encryption property to Yes/True on the client side | Comments |
| --- | --- | --- | --- | --- | --- |
| [Self-signed certificate - automatically created by SQL Server](special-cases-for-encrypting-connections-sql-server.md#scenario-2-only-some-clients-need-encrypted-connections-1) | Yes | Can't be done | Yes | Ignored | [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and earlier versions use the SHA1 algorithm. [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions use SHA256. For more information, see [Changes to hashing algorithm for self-signed certificate in SQL Server 2017](https://techcommunity.microsoft.com/t5/sql-server-support-blog/changes-to-hashing-algorithm-for-self-signed-certificate-in-sql/ba-p/319026). We don't recommend this approach for production use. |
| [Self-signed certificate created by using New-SelfSignedCertificate or makecert - Option 1](special-cases-for-encrypting-connections-sql-server.md#scenario2option1) | No | No | Yes | Yes | We don't recommend this approach for production use. |
| [Self-signed certificate created by using New-SelfSignedCertificate or makecert - Option 2](special-cases-for-encrypting-connections-sql-server.md#scenario2option2) | No | Yes | Optional | Yes | We don't recommend this approach for production use. |
| Company's certificate server or from a CA that's not in the [List of Participants - Microsoft Trusted Root Program](/security/trusted-root/participants-list) - Option 1 | No | No | Yes | Yes | |
| Company's certificate server or from a CA that's not in the [List of Participants - Microsoft Trusted Root Program](/security/trusted-root/participants-list) - Option 2 | No | Yes | Optional | Yes | |
| Trusted root authorities | No | No | Optional | Yes | We recommend this approach. |

## How to tell if encryption is working?

You can monitor communication using a tool such as Microsoft Network Monitor or a network sniffer and check the details of packets captured in the tool to confirm that the traffic is encrypted.

Alternatively, you can check the encryption status of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] connections using the Transact-SQL (T-SQL) commands. To do this, follow these steps:

1. Open a new query window in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Management Studio (SSMS) and connect to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.
1. Execute the following T-SQL command to check the value of `encrypt_option` column. For encrypted connections the value will be `TRUE`.

  ```sql
  SELECT * FROM sys.dm_exec_connections
  ```

## See also

- [TLS 1.2 support for Microsoft SQL Server](https://support.microsoft.com/help/3135244)
- [Securing SQL Server](../../relational-databases/security/securing-sql-server.md)

## Next steps

- [SQL Server Encryption](../../relational-databases/security/encryption/sql-server-encryption.md)
- [SQL Server fails to start with error 17182](/troubleshoot/sql/security/fails-start-error-17182)
- [Configuring SQL Server instance for certificates](configure-sql-server-encryption.md)
