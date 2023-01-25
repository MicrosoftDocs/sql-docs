---
title: Conversation Groups
description: "A conversation group identifies a group of related conversations."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Conversation Groups

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

A conversation group identifies a group of related conversations. A conversation group allows an application to easily coordinate conversations involved in a specific business task.

Every conversation belongs to one conversation group. Every conversation group is associated with a specific service, and all conversations in the group are conversations to or from that service. A conversation group can contain any number of conversations.

SQL Server uses conversation groups to provide exactly-once-in-order (EOIO) access to messages that are related to a specific business task. When an application sends or receives a message, SQL Server locks the conversation group to which the message belongs. Thus, only one session at a time can receive messages for the conversation group. The conversation group lock guarantees that an application can process messages on each conversation exactly once in order (EOIO). Because a conversation group can contain more than one conversation, an application can use conversation groups to identify messages related to the same business task, and process those messages together.

A conversation group is not shared between participants in a conversation. Therefore, each participant in a conversation is free to group that conversation as appropriate. An application can manage complex interactions among services without requiring any special support from the services.

## Examples of Conversation Groups

A human resources application may have a **GetEmployeeInformation** service that combines information from a payroll service and information from a benefits service. The **GetEmployeeInformation** service begins a conversation with each service, and relates one conversation to the other in the same conversation group. Service Broker adds the conversation group identifier to each incoming message on these two conversations, regardless of whether the message arrives from the payroll service or the benefits service. Because the conversations are in the same conversation group, Service Broker provides all the information necessary for the **GetEmployeeInformation** service to match the benefits information to the payroll information, regardless of how many requests are in progress in the **GetEmployeeInformation** service.

The messages to the payroll service and the messages to the benefits service do not contain conversation group information for the conversation group created by **GetEmployeeInformation**. Each service operates independently, and only the **GetEmployeeInformation** service maintains information about the entire business task. Keeping services independent of one another helps make each service simple to code and easy to maintain. Another advantage to maintaining this independence is that if one service is unavailable, the other service can continue to operate.

## Organizing Application State

One benefit of the conversation group is that the conversation group identifier is a convenient key to identify and retrieve application state. The conversation group identifier makes it easy to maintain application state in the database. If accomplishing a task involves exchanging many messages over time, it is inefficient to keep an instance of the application running just to maintain the application state. An application scales better if, between messages, any data associated with the task is stored in the database and then is retrieved when the next message associated with that task is received. The conversation group identifier can be used as the primary key, in a state table provided by an application developer, to enable quick retrieval of the state associated with a particular task. For more information about using the conversation group identifier to maintain state, see [State Management](state-management.md).

Because SQL Server locks the conversation group each time an application sends or receives a message, an application does not need to explicitly prevent another program from updating the same state data at the same time. The application simply locks the conversation group, restores state, processes messages, updates state, and then commits the transaction.

For convenience, SQL Server allows an application to lock the next available conversation group without receiving a message. Using the GET CONVERSATION GROUP statement, an application can lock a conversation group and restore state before processing messages. See the [GET CONVERSATION GROUP (Transact-SQL)](../../t-sql/statements/get-conversation-group-transact-sql.md) statement for details.

## Conversation Group Lifetime

Service Broker manages the lifetime of the conversation group. You do not need to explicitly create or destroy a conversation group. Service Broker creates a new conversation group under the following circumstances:

- An application begins a new conversation that is not related to an existing conversation group. Service Broker creates a new conversation group and assigns a new identifier to the conversation group.

- An application begins a conversation related to a conversation group identifier that does not currently exist. In this case, Service Broker creates a new conversation group with the specified identifier. This means you can assign your own value to a conversation group identifier.

- Service Broker receives the first message on a new conversation started by another service. In this case, Service Broker uses the name of the service and the broker instance identifier (if one is present) to do the following:

   1. Locate the appropriate queue.

   2. Create a new conversation group and associate the conversation group with the queue.

   3. Create a new conversation handle and add the conversation handle to the new conversation group.

   4. Place the incoming message on the queue.

Service Broker adds the conversation group identifier to the metadata for the conversation that created the conversation group. Whenever Service Broker receives a message for any conversation associated with the conversation group, Service Broker adds the conversation group identifier to that message before entering the message in the queue.

A conversation group identifier is valid from the time Service Broker creates it until all conversations associated with the identifier end; that is, the conversation group identifier is guaranteed to be valid while any conversation in the group is active.

An application that uses the conversation group identifier to manage application state uses a state table provided by the developer. The application must delete that state from the state table when the application determines that the state is no longer necessary. In many cases, the application deletes state after the task completes successfully, or after errors indicate that the task cannot be completed. In these cases, the application typically includes the command to delete state within the transaction that sends the final response message and ends the conversation. This strategy ensures that the application state and the conversation group identifier have the same lifetime. If the send operation fails, the delete operation rolls back. Likewise, if the delete operation fails, the send operation rolls back and SQL Server does not send the message. In either case, the application state and the conversation group identifier remain valid. If both operations succeed, then the conversation group identifier lifetime ends at the same time that the program deletes the associated application state.

## See also

- [sys.conversation_endpoints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)
- [sys.conversation_groups (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-groups-transact-sql.md)
- [Conversation Group Locks](conversation-group-locks.md)