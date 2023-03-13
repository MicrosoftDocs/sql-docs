---
title: Certificates and Service Broker
description: "This topic describes how SQL Server uses certificates for Service Broker remote security."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Certificates and Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes how SQL Server uses certificates for Service Broker remote security. Service Broker remote security refers to operations that involve more than one SQL Server instance when those operations use either dialog security or transport security.

## Overview

Service Broker remote security maps an operation from outside an instance to a SQL Server database principal. The operation then proceeds in the security context of that database principal, with normal SQL Server permission checks. For example, when a message arrives for a conversation that uses dialog security, Service Broker uses information in the message to identify a database principal for the remote side of the conversation. SQL Server then verifies that the principal has permission to connect to the database that hosts the destination service, and permission to send a message to the destination service.

SQL Server uses certificates to verify the identity of a remote database, and to identify the local database principal for the operation. Therefore, installing a certificate in SQL Server constitutes a statement of trust in the database that holds the private key for the certificate. Carefully manage the certificates that you install and the remote service bindings that you create.

> [!NOTE]
> Only install certificates from trusted sources. Do not distribute private keys.

To verify the identity of a remote server, SQL Server must receive information that can be decrypted with the public key in a certificate owned by a local database principal. If SQL Server can successfully decrypt the information, it means that the remote database contains the private key that corresponds to the public key in the local certificate. Once SQL Server verifies the identity of a remote database, the remote database can act with the permissions of the local database principal.

For transport security, each database must trust the other database. Transport security can use either certificates or Windows Authentication. For more information on transport security, see [Service Broker Transport Security](service-broker-transport-security.md).

For dialog security, the initiator of the dialog must trust the target, and must be able to verify the identity of the target. However, the target may allow connections from initiators that do not provide identifying information. In this case, the initiators use the **public** role in the database that hosts the target service. Dialog security always uses certificates. For more information on dialog security, see [Service Broker Dialog Security](service-broker-dialog-security.md).

SQL Server does not provide automated methods for configuration of Service Broker security by means of certificates.

## Certificate Requirements

To be used for Service Broker security, a certificate must meet the following requirements:

- The key modulus must be less than 2048.

- The total certificate length must be less than 32 kilobytes (KB).

- A subject name must be specified.

- Validity dates must be specified.

- The key length must be a multiple of 64 bits.

A self-signed certificate created with the Transact-SQL statement CREATE CERTIFICATE meets the requirements in the preceding list. Certificates that are loaded from a file may not meet these requirements.

When the certificate is stored in SQL Server, the certificate must be encrypted with the master key for the database. Service Broker cannot use certificates that are only encrypted with a password. Also, the master key for the database must be encrypted with the service key for the instance. Otherwise, Service Broker cannot open the master key.

In order for SQL Server to use a certificate to begin a conversation, the certificate must be marked ACTIVE FOR BEGIN_DIALOG. Certificates are marked as active for begin dialog by default. However, you can choose to temporarily deactivate a certificate while updating the security configuration for a service. For more information, see [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md) and [ALTER CERTIFICATE (Transact-SQL)](../../t-sql/statements/alter-certificate-transact-sql.md).

## See also

- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)
- [Remote Service Bindings](remote-service-bindings.md)
- [Certificates for Dialog Security](certificates-for-dialog-security.md)
