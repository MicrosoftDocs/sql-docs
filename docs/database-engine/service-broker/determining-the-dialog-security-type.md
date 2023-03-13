---
title: Determining the Dialog Security Type
description: "The type of dialog security that is used for a conversation depends on the options in the BEGIN DIALOG CONVERSATION statement, the settings on the remote service binding for the service, and whether the owner of the initiating service owns a certificate."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Determining the Dialog Security Type

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The type of dialog security that is used for a conversation depends on the options in the BEGIN DIALOG CONVERSATION statement, the settings on the remote service binding for the service, and whether the owner of the initiating service owns a certificate. For each new dialog, SQL Server looks up the remote service binding for the target service in the **sys.remote_service_bindings** catalog view.

The following table lists the type of dialog security for each valid combination. Notice that if a remote service binding exists, the dialog uses encryption regardless of the settings on the BEGIN DIALOG CONVERSATION statement.

:::row:::
   :::column span="1":::
   ** **
   :::column-end:::
   :::column span="1":::
   ** **
   :::column-end:::
   :::column span="1":::
   **No remote service binding**
   :::column-end:::
   :::column span="1":::
   **Remote service binding with ANONYMOUS = ON**
   :::column-end:::
   :::column span="1":::
   **Remote service binding with ANONYMOUS = OFF**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Service owner has a certificate

   :::column-end:::
   :::column span="1":::
   ENCRYPTION = ON

   :::column-end:::
   :::column span="1":::
   Dialog fails

   :::column-end:::
   :::column span="1":::
   Anonymous security

   :::column-end:::
   :::column span="1":::
   Full security

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Service owner has a certificate

   :::column-end:::
   :::column span="1":::
   ENCRYPTION = OFF

   :::column-end:::
   :::column span="1":::
   No dialog security

   :::column-end:::
   :::column span="1":::
   Anonymous security

   :::column-end:::
   :::column span="1":::
   Full security

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Service owner does not have a certificate

   :::column-end:::
   :::column span="1":::
   ENCRYPTION = ON

   :::column-end:::
   :::column span="1":::
   Dialog fails

   :::column-end:::
   :::column span="1":::
   Anonymous security

   :::column-end:::
   :::column span="1":::
   Dialog fails
:::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Service owner does not have a certificate

   :::column-end:::
   :::column span="1":::
   ENCRYPTION = OFF

   :::column-end:::
   :::column span="1":::
   No dialog security

   :::column-end:::
   :::column span="1":::
   Anonymous security

   :::column-end:::
   :::column span="1":::
   Dialog fails

   :::column-end:::
:::row-end:::

- Dialog fails  
  SQL Server does not have the information required to provide the requested security. Service Broker ends the conversation and puts an error message on the queue for the initiating service.

- No dialog security  
  SQL Server does not provide dialog security for the dialog. Operations on behalf of the initiating service run as **public** in the target database. Messages are not encrypted for this dialog. Notice, however, that transport security may encrypt the message on the network.

- Anonymous security  
  SQL Server uses anonymous security. Messages outside of the instance are encrypted for this dialog. Because the target service cannot verify the identity of the initiating service, operations on behalf of the initiating service run as **public** in the target database.

- Full security  
  SQL Server uses full security. Messages outside of the instance are encrypted for this dialog. Operations on behalf of the initiating service run as the designated user in the target database.

## See also

- [How to: Configure Initiating Services for Anonymous Dialog Security (Transact-SQL)](how-to-configure-initiating-services-for-anonymous-dialog-security-transact-sql.md)
- [How to: Configure Initiating Services for Full Dialog Security (Transact-SQL)](how-to-configure-initiating-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure Target Services for Anonymous Dialog Security (Transact-SQL)](how-to-configure-target-services-for-anonymous-dialog-security-transact-sql.md)
- [How to: Configure Target Services for Full Dialog Security (Transact-SQL)](how-to-configure-target-services-for-full-dialog-security-transact-sql.md)
- [How to: Configure Permissions for a Local Service (Transact-SQL)](how-to-configure-permissions-for-a-local-service-transact-sql.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)