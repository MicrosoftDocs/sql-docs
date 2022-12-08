---
title: Transactional Messaging
description: "The foundation of the Service Broker programming model is transactional messaging."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Transactional Messaging

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The foundation of the Service Broker programming model is transactional messaging. Any operation that involves Service Broker is part of the current transaction. Service Broker does not commit a messaging operation until the current transaction commits. If the transaction rolls back, the Database Engine guarantees that all messaging operations that are part of the transaction also roll back. An application manages messaging operations as part of managing SQL Server transactions.

For example, when a program sends a message within a transaction, Service Broker does not send the message over the network until the program commits the transaction. When a program receives a message within a transaction, the Database Engine does not permanently remove the message from the queue until the program commits the transaction.

Transactional messaging helps you write robust, scalable applications by ensuring that the state of the database remains consistent with the state of the queues. When an application makes a change to the database and sends or receives a message, the change to the database and the messaging operation are part of the same transaction. If the transaction rolls back, both the change to the database and the messaging operation roll back. Both operations succeed, or both operations fail. In the Service Broker model, an application uses transactional messaging to guarantee that the messages sent by the application reflect the current state of the database.

To take full advantage of transactional messaging, you write your applications so that messaging operations occur in the same transaction as the database operations that the messages represent. For example, an application that processes an order receives the message for the order and updates the database with the order in a single transaction.

If the application instead receives the message in one transaction and updates the database in a different transaction, a failure to update the database creates a situation where the message no longer exists but the change that the message requested has not taken place. In this case, the application does not take advantage of one of the main benefits that Service Broker provides. In particular, Service Broker guarantees that all messages are delivered exactly once, in order, or else the message sender is notified with a Service Broker error message. An application that removes a message from the queue permanently, but fails to process the message, as in this example, breaks this guarantee. Without this guarantee, the application must contain additional code to handle possible inconsistencies or run the risk of incorrect results.

When an application processes a message and makes no changes to the database, the guarantee holds. The message has been processed successfully. An application that uses Service Broker may choose to ignore a message, but the application must not inadvertently lose a message, even in cases where the application loses connectivity to the database or exits unexpectedly.

## See also

- [Controlling Transactions (ADO)](../../ado/guide/data/controlling-transactions-ado.md)
- [Broker System Messages](broker-system-messages.md)