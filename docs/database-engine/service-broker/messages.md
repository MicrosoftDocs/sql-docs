---
title: Messages
description: "Messages are the information exchanged between applications that use Service Broker."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Messages

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Messages are the information exchanged between applications that use Service Broker.

Each message is part of a conversation. A message has a specific type, which is determined by the application that sends the message. Each message has a unique conversation identity, as well as a sequence number within the conversation. When receiving messages, Service Broker uses the conversation identity and the sequence number of the message to enforce message ordering.

The content of the message is determined by the application. When a message is received, Service Broker validates the content of the message to ensure that the content is valid for the message type. Regardless of the message type, SQL Server stores the content of the message as type varbinary(max). Therefore, a message can contain any data that can be converted to varbinary(max).

An application typically processes the content of a message based on the contract and the message type.

## See also

- [ Message Types](message-types.md)
- [Building Applications with Service Broker](building-applications-with-service-broker.md)