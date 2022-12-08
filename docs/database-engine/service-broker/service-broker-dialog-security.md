---
title: Service Broker Dialog Security
description: "Dialog security provides encryption, remote authentication, and remote authorization for a specific conversation."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Dialog Security

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Dialog security provides encryption, remote authentication, and remote authorization for a specific conversation. When a conversation uses dialog security, Service Broker encrypts all messages sent outside a SQL Server instance. Service Broker conversations use dialog security by default.

## Dialog Security Basics

Service Broker dialog security lets your application use authentication, authorization, or encryption for an individual dialog conversation (or dialog). By default, all dialog conversations use dialog security. When you begin a dialog, you can explicitly allow a dialog to proceed without dialog security by including the ENCRYPTION = OFF clause on the BEGIN DIALOG CONVERSATION statement. However, if a remote service binding exists for the service that the conversation targets, the dialog uses security even when ENCRYPTION = OFF.

For a dialog that uses security, Service Broker encrypts all messages sent outside a SQL Server instance. Messages that remain within a SQL Server instance are never encrypted. In dialog security, only the database that hosts the initiating service and the database that hosts the target service need to have access to the certificates used for security. That is, an instance that performs message forwarding is not required to have the capability to decrypt the messages that the instance forward.

Service Broker provides two types of dialog security, full security and anonymous security. For conversations that use dialog security, Service Broker provides remote authorization to map the remote side of the conversation to a local user.

Messages are encrypted on the network when the conversation uses either full security or anonymous security. However, the effective rights in the target database and the strategy used for message encryption differ slightly between the two approaches.

Whether the conversation uses full security or anonymous security, the message body is encrypted with a symmetric session key that is generated for the specific conversation. Only the keys are encrypted with private key encryption using the certificate supplied for Dialog Security. Service Broker also performs a message integrity check to help detect message corruption or tampering.

SQL Server creates a session key for a conversation that uses dialog security. To protect the session key while it is stored in the database, Service Broker encrypts the session key with the master key for the database. If a database master key is not available, messages for the conversation remain in the **transmission_status** with an error until a database master key is created, or until the conversation times out. Therefore, both databases that participate in the conversation must contain master keys, even when the databases are hosted in the same instance. If the initiating database does not contain a master key, the dialog will fail. If the target database does not contain a master key, messages remain in the transmission queue of the initiating database. The last transmission error for these messages indicates the reason that the messages cannot be delivered. Either use the ENCRYPTION = OFF parameter to create an unencrypted dialog, or use the following command to create a database master key:

```sql
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'
```

For convenience, Service Broker allows secure conversations that remain within a single database to proceed regardless of whether the database contains a master key. While two different databases can be moved to different SQL Server instances during the lifetime of a conversation, a conversation within a single database always remains within that database. Therefore, the conversation can proceed.

### Full Security

Full security helps to protect the initiating service from sending messages to an untrusted database and helps protect the target service from receiving messages from an untrusted database. Service Broker encrypts messages transmitted over the network when the conversation uses full security.

Full security provides identification for both the initiating service and the target service. Full security requires that the initiating service trust the target service and also requires that the target service trust the initiating service. For example, a service that orders parts from a vendor may require the ordering application to trust the vendor service and the vendor service to trust the ordering application.

Database administrators establish trust by exchanging certificates that contain public keys. For full dialog security, each side of the conversation contains a private key for a local user and a public key for a remote user. The database that hosts the initiating service contains a remote service binding. This remote service binding specifies the local user that owns the certificate that corresponds to the private key in the remote database. Therefore, operations on behalf of the initiating service run as the designated user in the target database.

The target database contains a user. This user owns a certificate that corresponds to a private key that is owned by the user that owns the initiating service. Therefore, operations on behalf of the target service run in the initiating database as the user who owns the initiating service.

For dialogs that use full security, each side of the conversation generates a session key. The first message in each direction contains the session key, encrypted with a key exchange key, as described in [Certificates and Service Broker](certificates-and-service-broker.md).

### Anonymous Security

Anonymous security helps protect the initiating service against sending messages to an untrusted database. Service Broker encrypts messages transmitted over the network when the conversation uses anonymous security.

Anonymous security identifies the target service to the initiating service, but does not identify the initiating service to the target service.

For example, an application that submits work orders may need to guarantee that the recipient of the work order is the intended target, but the target database may not need to provide any special privileges for a service that submits work orders. In this case, the database that contains the initiating service must contain a remote service binding for the target service.

Because the target service cannot verify the identity of the initiating service, operations on behalf of the initiating service run as a member of the fixed database role **public** in the target database. The target database receives no information about the user that initiated the conversation. The target database does not need to contain a certificate for the user that initiates the conversation.

For dialogs that use anonymous security, both sides of the conversation use the session key generated by the initiating database. The target database does not return a session key to the initiating database.

## Security Contexts for Dialog Security

Service Broker remote authorization controls remote access to an individual service. Remote authorization determines the security context within which the incoming messages to a SQL Server instance are sent to a service.

Service Broker always uses remote authorization for a secure conversation that does not take place entirely within a SQL Server instance. The dialog security that is configured for the conversation determines the security context that Service Broker uses for remote authorization.

Service Broker does not use remote authorization when the conversation remains within a SQL Server instance, even if remote authorization is configured. For a conversation within an instance, the SQL Server security principals are already available to SQL Server, so there is no need to use remote authorization to determine the correct security context for Service Broker operations. However, as described earlier in this topic, Service Broker creates a session key so that the conversation can proceed if one of the databases is moved to another instance during the conversation.

For a conversation that uses anonymous security, the connection runs as a member of the fixed database role **public** in the target database. In this case, the fixed database role **public** must have permission to send a message to the service. However, the role needs no other permissions in the database.

For a conversation that uses full security, the connection on each side of the conversation acts with the permissions of the user that is specified in the remote service binding. For example, if a remote service binding associates the service name **InventoryService** with the user **InventoryServiceRemoteUser**, SQL Server uses the security context for **InventoryServiceRemoteUser** to put messages for the **InventoryService** application on the queue for the destination service.

For better security, the user that owns the private key for a service is typically a different user than the user specified for activation. A user that owns a private key only needs permission to add a message to the queue - that is, SEND permission for the service that uses the queue. In contrast, the user that is specified for activation has the permissions that are required to accomplish the requested work and send a response. In the preceding example, **InventoryServiceRemoteUser** does not require permissions to query the inventory table or send a return message. The user only needs permissions to send a message to the queue that **InventoryService** uses. Stored procedure activation occurs in a different session with the credentials that the queue specifies. No credentials need to be shared between the session that enqueues the message and the session that processes the message.

## Creating a Secure Dialog

When Service Broker establishes a dialog between two databases, the initiating service must establish a user context in the target database so it can put messages in the target queue. This user context determines whether the initiating service has permission to open a dialog to the target.

The most flexible way to do this is to create a certificate and remote service binding. For more information about creating a certificate, see [CREATE CERTIFICATE (Transact-SQL)](../../t-sql/statements/create-certificate-transact-sql.md). For more information about creating a remote service binding see, [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md).

> [!NOTE]
> If the security context is not set up correctly, messages sent on the dialog will stay in the sys.transmission_queue on the initiating service with the following error message in the transmission_status column: **The server principal `'%.*ls'` is not able to access the database `'%.*ls'` under the current security context.**

## See also

- [Broker:Corrupted Message Event Class](../../relational-databases/event-classes/broker-corrupted-message-event-class.md)
- [Audit Broker Login Event Class](../../relational-databases/event-classes/audit-broker-login-event-class.md)
- [Remote Service Bindings](remote-service-bindings.md)
- [Determining the Dialog Security Type](determining-the-dialog-security-type.md)
