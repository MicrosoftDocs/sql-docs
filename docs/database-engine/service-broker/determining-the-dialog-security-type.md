---
title: Determine the Dialog Security Type
description: "The type of dialog security that is used for a conversation depends on the options in the BEGIN DIALOG CONVERSATION statement, the settings on the remote service binding for the service, and whether the owner of the initiating service owns a certificate."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Determine the dialog security type

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The type of dialog security that's used for a conversation depends on the options in the `BEGIN DIALOG CONVERSATION` statement, the settings on the remote service binding for the service, and whether the owner of the initiating service owns a certificate. For each new dialog, SQL Server looks up the remote service binding for the target service in the `sys.remote_service_bindings` catalog view.

The following table lists the type of dialog security for each valid combination. If a remote service binding exists, the dialog uses encryption regardless of the settings on the `BEGIN DIALOG CONVERSATION` statement.

| Certificate | Encryption | No remote service binding | Remote service binding with `ANONYMOUS = ON` | Remote service binding with `ANONYMOUS = OFF` |
| --- | --- | --- | --- | --- |
| Service owner has a certificate | `ENCRYPTION = ON` | Dialog fails | Anonymous security | Full security |
| Service owner has a certificate | `ENCRYPTION = OFF` | No dialog security | Anonymous security | Full security |
| Service owner doesn't have a certificate | `ENCRYPTION = ON` | Dialog fails | Anonymous security | Dialog fails |
| Service owner doesn't have a certificate | `ENCRYPTION = OFF` | No dialog security | Anonymous security | Dialog fails |

- **Dialog fails**

  SQL Server doesn't have the information required to provide the requested security. Service Broker ends the conversation and puts an error message on the queue for the initiating service.

- **No dialog security**

  SQL Server doesn't provide dialog security for the dialog. Operations on behalf of the initiating service run as **public** in the target database. Messages aren't encrypted for this dialog. However, transport security might encrypt the message on the network.

- **Anonymous security**

  SQL Server uses anonymous security. Messages outside of the instance are encrypted for this dialog. Because the target service can't verify the identity of the initiating service, operations on behalf of the initiating service run as **public** in the target database.

- **Full security**

  SQL Server uses full security. Messages outside of the instance are encrypted for this dialog. Operations on behalf of the initiating service run as the designated user in the target database.

## Related content

- [How to: Configure initiating services for anonymous dialog security (Transact-SQL)](how-to-configure-initiating-services-for-anonymous-dialog-security-transact-sql.md)
- [How to: Configure initiating services for full dialog security (Transact-SQL)](how-to-configure-initiating-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure target services for anonymous dialog security (Transact-SQL)](how-to-configure-target-services-for-anonymous-dialog-security-transact-sql.md)
- [How to: Configure target services for full dialog security (Transact-SQL)](how-to-configure-target-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure permissions for a local service (Transact-SQL)](how-to-configure-permissions-for-a-local-service-transact-sql.md)
- [Service Broker communication protocols](service-broker-communication-protocols.md)
