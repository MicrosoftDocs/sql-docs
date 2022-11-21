---
title: Certificates for Dialog Security
description: "When a conversation begins, Service Broker uses remote service bindings to locate the certificate to use for the conversation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Certificates for Dialog Security

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

When a conversation begins, Service Broker uses remote service bindings to locate the certificate to use for the conversation. This topic describes how Service Broker determines the certificate to use for a conversation.

## Locating the Certificate for a Dialog

Service Broker first locates a remote service binding for the conversation, and then selects a certificate owned by the user specified in the binding.

To find a binding, Service Broker checks the database for a binding that specifies the target service name for the conversation.

To select a certificate, Service Broker finds the certificate with the latest expiration date that is owned by the user specified in the remote service binding. Service Broker does not consider certificates that are not yet valid, that have expired, or that are not marked as available for begin dialog. For more information on certificates, see [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md).

If no remote service binding exists, or the user for the remote service binding does not own a valid certificate that is available for begin dialog, Service Broker cannot encrypt messages for the conversation. If there is no binding, and the database contains a Broker Configuration Service, Service Broker requests a binding through that service. If there is no Broker Configuration Service in the database or the Broker Configuration Service does not provide a remote service binding, the conversation is delayed if encryption is ON, or continues without encryption if encryption is OFF. For more information, see [Determining the Dialog Security Type](determining-the-dialog-security-type.md).

## Remote Authorization and Dialog Security

Service Broker dialog security uses certificates for remote authorization. When a dialog uses dialog security, the first message sent by each participant in the conversation contains header information encrypted with the private key of the sending user, and header information encrypted with the public key of the receiving user. The content of the message is encrypted with a session key. The session key itself is encrypted, and can only be recovered using the private key for the receiving user.

If the recipient of the message can successfully decrypt the message, this means that the recipient possesses the corresponding keys, and this confirms the identity of each participant in the conversation. Installing a certificate in a database creates a trust relationship with the database that holds the private key.

In effect, encrypting a header with the private key for the local user asks the question, "Does the remote database trust the local database?" The remote database can only decrypt the header if the database contains the corresponding public key. Encrypting a header with the public key for a user in the remote database asks the question, "Does the local database trust the remote database?" The remote database can only decrypt the header if the database contains the corresponding private key. For security and efficiency, Service Broker asks both questions at the same time. However, the message is structured so that a recipient must be able to affirm both questions to respond to the message correctly.

The reasoning behind this strategy is simple. If the remote database can decrypt a header encrypted with a private key in the local database, the remote database contains the corresponding public key, and the remote database trusts the local database. If the remote database can decrypt a header encrypted with a public key in the local database, the remote database contains the corresponding private key, and the local database trusts the remote database. As long as the private keys remain secret, only the two databases involved in the conversation can successfully exchange messages for the conversation.

> [!NOTE]
> Only install certificates from trusted sources. Do not distribute private keys.

Full dialog security verifies identity in both directions during the first exchange of messages. For conversations that use anonymous dialog security, the initiator verifies that the target database contains the expected private key. However, with anonymous dialog security, the target database does not verify the identity of the initiator; instead, the database that hosts the target service must allow the **public** fixed database role to send messages to the service.

Messages must be exchanged both ways before the local database considers the identity of the remote database to be confirmed. Verification can only happen if the local database contains the correct public key for the certificate in the remote database.

For the first message sent by each side of the conversation, Service Broker includes the following headers:

- A service pair security header containing information on the certificates used for the message. The service pair security header is signed with the private key for the user who owns the service.

- A key exchange key that encrypts the 128-bit session key used to encrypt the body of the message. The key exchange key is encrypted with the public key for the remote user.

For dialogs that use anonymous security, the service pair security header remains unencrypted. The message itself is still encrypted, and the key exchange key is encrypted with the public key for the security principal in the target database. In this case, the first return message does not contain a key exchange key, service pair security header, or an encrypted session key.

When Service Broker itself generates a message in response to an incoming message (for example, an error or an acknowledgment), that message uses the session key of the incoming message, regardless of whether the dialog uses full security or anonymous security.

## See also

- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [ALTER REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/alter-remote-service-binding-transact-sql.md)
- [ALTER REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/alter-remote-service-binding-transact-sql.md)
- [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md)
- [ALTER CERTIFICATE (Transact-SQL)](../../t-sql/statements/alter-certificate-transact-sql.md)
- [DROP CERTIFICATE (Transact-SQL)](../../t-sql/statements/drop-certificate-transact-sql.md)
- [Certificates and Service Broker](certificates-and-service-broker.md)
- [Remote Service Bindings](remote-service-bindings.md)