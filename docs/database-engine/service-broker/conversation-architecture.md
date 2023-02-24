---
title: Conversation Architecture
description: "All Service Broker applications communicate through conversations."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Conversation Architecture

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

All Service Broker applications communicate through conversations. Conversations are reliable, long-running, asynchronous exchanges of messages. The following table shows the objects that Service Broker uses for conversations:

## In This Section

- [Messages](messages.md)  
    Messages are the data that is exchanged between services. Each message belongs to one conversation, and has a specific message type.

- [Dialog Conversations](dialog-conversations.md)  
    Dialogs are conversations between two Service Broker services. Dialogs let Service Broker provide exactly-once-in-order (EOIO) message delivery. Each dialog belongs to one conversation group, and follows a specific contract.

- [Conversation Groups](conversation-groups.md)  
    Conversation groups identify conversations that work together to complete the same task. Service Broker uses conversation groups to manage message locking. Application developers use conversation groups to manage concurrency, and to help with state management.

- [Conversation Priorities](conversation-priorities.md)  
    Conversation priorities identify the relative importance of conversations. Messages from high priority conversations are processes before messages from low priority conversations. This helps ensure important work is not blocked during heavy processing loads. It also enables systems to offer different levels of service to different customers.

## See also

- [Logical Architecture](logical-architecture.md)