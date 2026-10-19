---
title: Service Broker Configuration Notice Service
description: "You can create a Broker Configuration Notice (BCN) service on an initiating server which will automatically bind conversations to a specific user on a target server."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Service Broker Configuration Notice service

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

You can create a Broker Configuration Notice (BCN) service on an initiating server which automatically binds conversations to a specific user on a target server.

## Initiate conversations

When a BCN service is created, the initiating service sends a **MissingRemoteServiceBinding** message to the BCN service to ask if a user context is available for the conversation on the target server. When the BCN service responds that a user context is available, the user context is bound to the conversation, and all messages are added to the queue under the context of the user. If `ENCRYPTION = ON`, the dialog won't proceed until the BCN service confirms that a user context is available. If `ENCRYPTION = OFF`, the dialog proceeds after the BCN ends the **MissingRemoteServiceBinding** conversation.

> [!NOTE]  
> If a BCN service is created, the initiating service requests the user context regardless of the encryption status.

## Service Broker Configuration Notice service and dynamic routing

The BCN service also manages dynamic routing. For more information about creating a Broker Configuration Notice Service, see [Service Broker dynamic routing](service-broker-dynamic-routing.md).

## Related content

- [Service Broker dynamic routing](service-broker-dynamic-routing.md)
