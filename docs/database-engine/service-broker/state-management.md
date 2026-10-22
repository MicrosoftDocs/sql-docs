---
title: State Management
description: "An application that maintains state typically stores that state in database tables."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# State management

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

An application that maintains state typically stores that state in database tables. Because each conversation group has a unique identifier, that identifier is typically used as a key for the state table. Service Broker also provides message retention for applications that must preserve the precise messages sent and received.

Many applications don't require state. In general, an application maintains state if the task involves more than one message, and there's information about the task that can't be stored in the existing tables for the database.

For example, an application that looks up and returns customer information doesn't require state, and doesn't use a state table. On the other hand, an application that manages order fulfillment generates requests to several other services. A program that coordinates requests to other services often uses a state table to track the requests. The application updates the data tables and clears the state table when all of the requests have completed successfully. If a request returns an error, the application resends the request, or uses the state table to send a compensating request.

An application might also use a state table for auditing or logging purposes. The application saves the important information about each request to the state table. In this case, the application doesn't delete information from the state table when a conversation completes.

Some applications might require a precise record of the messages sent and received while the conversation is active. For this scenario, Service Broker provides message retention.

## Related content

- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [Service Broker application outline](service-broker-application-outline.md)
