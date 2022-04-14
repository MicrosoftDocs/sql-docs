---
title: Service Broker Configuration Notice Service
description: "You can create a Broker Configuration Notice (BCN) service on an initiating server which will automatically bind conversations to a specific user on a target server."
ms.prod: sql
ms.technology: configuration
ms.topic: conceptual
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
---

# Service Broker Configuration Notice Service

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

You can create a Broker Configuration Notice (BCN) service on an initiating server which will automatically bind conversations to a specific user on a target server.

## Initiating Conversations

When a BCN service is created, the initiating service will send a **MissingRemoteServiceBinding** message to the BCN service to ask if a user context is available for the conversation on the target server. When the BCN service responds that a user context is available, the user context is bound to the conversation, and all messages are added to the queue under the context of the user. If ENCRYPTION=ON, the dialog will not proceed until the BCN service confirms that a user context is available. If ENCRYPTION=OFF, the dialog will proceed after the BCN ends the **MissingRemoteServiceBinding** conversation.

> [!NOTE]
> If a BCN service is created, the initiating service will request the user context regardless of the encryption status.## Service Broker Configuration Notice Service and Dynamic Routing
The BCN service also manages dynamic routing. For more information about creating a Broker Configuration Notice Service, see [Service Broker Dynamic Routing](service-broker-dynamic-routing.md).

## See also

- [Service Broker Dynamic Routing](service-broker-dynamic-routing.md)
