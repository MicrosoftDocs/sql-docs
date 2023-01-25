---
title: Service Broker Applications
description: "Service Broker applications are made up of one or more programs and the database objects that those programs use."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Applications

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker applications are made up of one or more programs and the database objects that those programs use. Applications communicate by creating conversations between independent components called services and then exchanging messages within those conversations. Applications use Service Broker by executing Transact-SQL statements in a SQL Server database.

## Application Components

A Service Broker application is made up of:

- One or more programs that implement a task or related set of tasks. Outside SQL Server, applications can be written in any programming environment that can run Transact-SQL statements in SQL Server. Inside SQL Server, applications can be written as stored procedures using Transact-SQL or a common language runtime (CLR) compliant language.

- A service that exposes the tasks to other services. A service is a Service Broker object that provides an addressable name for a set of related tasks. Other services start conversations with this service to perform the tasks.

- A contract and message types that define the structure and direction of the messages that are used in communications between the services.

- A queue to hold messages for the service.

- Optionally, routes and remote service bindings. Routes associate a network address with the name of a remote service. Remote service bindings associate a service name with a local database principal. Service Broker uses the certificate associated with the specified principal to handle authorization for the remote service and encryption of the messages exchanged with the remote service. Service Broker permits the routes and remote service bindings to be configured while the application is in deployment without requiring changes to the application. This allows administrators to move services and change security credentials without changes to application code. For more information on configuring routes and remote service bindings, see [Administration (Service Broker)](administration.md).

## Service Broker DML

Usually, an application sets up the service definition objects at the time of installation. While running, the application sends and receives messages by using Service Broker Data Manipulation Language (DML). The DML statements fall into three broad categories: messages, conversations, and conversation groups:

### Messages

Service Broker provides the following operations to support working with messages:

- The SEND statement sends a message on a specific conversation.

- The RECEIVE statement receives one or more messages from a queue. All messages received belong to the same conversation group.

### Conversations

Service Broker provides the following operations to support working with conversations:

- The BEGIN DIALOG CONVERSATION statement begins a conversation between two services. Because the conversation involves exactly two services, the conversation is a dialog.

- The END CONVERSATION statement ends one side of a conversation.

- The BEGIN CONVERSATION TIMER statement delivers a dialog timer message to one side of a conversation at a specific time.

- The GET_TRANSMISSION_STATUS statement returns a description of the last transmission error for a conversation. If the last attempt to transmit a message for the conversation succeeded, the statement does not return a description.

### Conversation Groups

Service Broker provides two operations for working with conversation groups:

- The GET CONVERSATION GROUP statement returns the conversation group identifier for the next receivable message in a queue. The statement also locks the conversation group.

- The MOVE CONVERSATION statement moves a conversation from one conversation group to another. The statement locks both the original conversation group and the destination conversation group.

## See also

- [BEGIN CONVERSATION TIMER (Transact-SQL)](../../t-sql/statements/begin-conversation-timer-transact-sql.md)
- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [MOVE CONVERSATION (Transact-SQL)](../../t-sql/statements/move-conversation-transact-sql.md)
- [GET CONVERSATION GROUP (Transact-SQL)](../../t-sql/statements/get-conversation-group-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)
- [GET_TRANSMISSION_STATUS (Transact-SQL)](../../t-sql/statements/get-transmission-status-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [BEGIN CONVERSATION TIMER (Transact-SQL)](../../t-sql/statements/begin-conversation-timer-transact-sql.md)
- [Contracts](contracts.md)
- [Dialog Conversations](dialog-conversations.md)
- [Conversation Groups](conversation-groups.md)
- [Message Types](message-types.md)
- [Messages](messages.md)
- [Queues](queues.md)
- [Services](services.md)
- [Building Applications with Service Broker](building-applications-with-service-broker.md)
- [Remote Service Bindings](remote-service-bindings.md)
- [Routes](routes.md)
