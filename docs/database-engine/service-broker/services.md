---
title: Services
description: "A Service Broker service is a name for a specific business task or set of business tasks."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Services

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A Service Broker service is a name for a specific business task or set of business tasks. Conversations occur between services. Service Broker uses the name of the service to deliver messages to the correct queue within a database, to route messages, to enforce the contract for a conversation, and to determine the remote security for a new conversation.

Each service specifies a queue to hold incoming messages. The contracts associated with the service define the specific tasks that the service accepts new conversations for. Therefore, a target service specifies one or more contracts that conversations with the service must follow. A service that initiates conversations but does not receive new conversations from other services does not need to specify contracts. If the service can receive messages on the DEFAULT contract, the DEFAULT contract must be included in the service definition.

## See also

- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [ALTER SERVICE (Transact-SQL)](../../t-sql/statements/alter-service-transact-sql.md)
- [DROP SERVICE (Transact-SQL)](../../t-sql/statements/drop-service-transact-sql.md)
- [Service Broker Routing](service-broker-routing.md)
- [Understanding When Activation Occurs](understanding-when-activation-occurs.md)
- [Creating Service Broker Services](creating-service-broker-services.md)