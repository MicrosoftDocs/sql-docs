---
title: Conversation Architecture
description: "All Service Broker applications communicate through conversations."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: reference
---

# Conversation architecture

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

All Service Broker applications communicate through conversations. Conversations are reliable, long-running, asynchronous exchanges of messages. The following list shows the objects that Service Broker uses for conversations:

## In this section

| Article | Description |
| --- | --- |
| [Messages](messages.md) | Messages are the data that is exchanged between services. Each message belongs to one conversation, and has a specific message type. |
| [Dialog conversations](dialog-conversations.md) | Dialogs are conversations between two Service Broker services. Dialogs let Service Broker provide exactly-once-in-order (EOIO) message delivery. Each dialog belongs to one conversation group, and follows a specific contract. |
| [Conversation groups](conversation-groups.md) | Conversation groups identify conversations that work together to complete the same task. Service Broker uses conversation groups to manage message locking. Application developers use conversation groups to manage concurrency, and to help with state management. |
| [Conversation priorities](conversation-priorities.md) | Conversation priorities identify the relative importance of conversations. Messages from high-priority conversations are processed before messages from low-priority conversations. This helps ensure important work isn't blocked during heavy processing loads. It also enables systems to offer different levels of service to different customers. |

## Related content

- [Logical architecture (Service Broker)](logical-architecture.md)
