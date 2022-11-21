---
title: Conversation Group Locks
description: "Service Broker uses conversation group locks to guarantee that only one queue reader can work with a related set of messages at any given time."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Conversation Group Locks

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker uses conversation group locks to guarantee that only one queue reader can work with a related set of messages at any given time. Service Broker uses conversation group locks to guarantee that messages are processed exactly once, in order.

All conversations belong to a conversation group. By default, each conversation belongs to a different conversation group, and so has a different conversation group identifier. The MOVE CONVERSATION statement changes the conversation group for a conversation. The BEGIN DIALOG CONVERSATION statement contains options for associating a new conversation with an existing conversation group. For more information on conversation groups, see [Conversation Groups](conversation-groups.md).

A conversation group lock is, in effect, an exclusive lock on a set of messages that share the same conversation group identifier. Conversation group locks are designed for simplicity, efficiency, and correctness. There is no explicit command or hint for acquiring or releasing a conversation group lock. Instead, every Service Broker command that affects a dialog or conversation group acquires the appropriate conversation group lock automatically. For example, a BEGIN DIALOG statement locks the conversation group that the new dialog belongs to, whereas a RECEIVE statement locks the conversation group that the received messages belong to.

A session holds a conversation group lock for the duration of the transaction within which the session acquired the lock. A session cannot hold a conversation group lock across transactions; when a transaction ends, all conversation group locks acquired during the transaction are released.

Locking occurs for the conversation group rather than for the conversation ID. Therefore, the lock only applies to one side of the conversation, even when both the initiator and the target are in the same database. A lock acquired by the target service does not block the initiating service, and vice versa. Further, the Database Engine does not enforce locking when adding incoming messages to a queue. The Database Engine adds messages to the queue even when an application has a conversation group lock on the conversation group that the messages belong to.

In practical terms, this means that an application that uses only identifiers retrieved from Service Broker does not need to wait to acquire locks on Service Broker resources. Most Service Broker applications are designed to take advantage of the locking provided by Service Broker. That is, most Service Broker applications only use conversation group identifiers and conversation handles that have been obtained from a Service Broker statement within the same transaction.

For example, an application typically gets a conversation group identifier from Service Broker, retrieves state from a state table, and then processes messages for conversations in that conversation group. Once the application gets the conversation group identifier, the application has a lock on the conversation group: no other instance of the application can acquire the lock. However, the conversation group lock does not prevent other instances of the application from receiving messages for other conversation groups, and does not prevent incoming messages from arriving on the queue.

With this locking strategy, Service Broker can guarantee in-order message processing. Because only one queue reader can process messages for a given conversation group, there is no risk of two queue readers receiving messages in the same conversation group at the same time. For a specific conversation, the RECEIVE statement returns messages in the order in which the messages were sent, so multiple queue readers can process messages from the queue without having to explicitly coordinate ordering.

Because locking operates on a conversation group rather than an individual conversation, a queue reader that does not specify a particular conversation in the RECEIVE statement may receive messages from different conversations that belong to the same conversation group. Further, the RECEIVE statement returns the next available message in the queue, regardless of whether that message is part of a conversation group that is currently unlocked or is locked in the current transaction. To receive messages from a particular conversation, specify the conversation handle in the RECEIVE statement. To receive messages from a particular conversation group, specify the conversation group identifier in the RECEIVE statement.

As a result of this locking strategy, your application should acquire a conversation group lock before updating the state table for the application. Most of the time, this happens automatically when your application receives a message or gets a conversation group. When handling errors, however, an application may need to reacquire the conversation group lock before updating the state table to indicate the error. For more information on error handling, see [Error Handling for Service Broker](error-handling-for-service-broker.md).

The following statements acquire conversation group locks:

- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)

- [BEGIN CONVERSATION TIMER (Transact-SQL)](../../t-sql/statements/begin-conversation-timer-transact-sql.md)

- [GET CONVERSATION GROUP (Transact-SQL)](../../t-sql/statements/get-conversation-group-transact-sql.md)

- [END CONVERSATION (Transact-SQL)](../../t-sql/statements/end-conversation-transact-sql.md)

- [MOVE CONVERSATION (Transact-SQL)](../../t-sql/statements/move-conversation-transact-sql.md)

- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)

- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)

## See also

- [sys.conversation_endpoints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-endpoints-transact-sql.md)
- [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md)
