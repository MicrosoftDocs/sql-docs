---
title: Complete a Conversation in a Single Database
description: "This tutorial is intended for users who are new to Service Broker but are familiar with database concepts and Transact-SQL statements."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Complete a conversation in a single database

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This tutorial is intended for users who are new to Service Broker, but are familiar with database concepts and Transact-SQL statements. It helps new users get started by showing them how to build and run a simple conversation in a single database.

## What you learn

This tutorial shows you how to create the database objects that are required to support a simple request-reply Service Broker conversation. You then start a conversation and use it to transmit messages.

Each Service Broker conversation has two ends: the conversation initiator and target. You perform the following tasks:

- Create a service and queue for the target and a service and queue for the initiator.

- Create a request message type and a reply message type.

- Create a contract that specifies request messages go from the initiator to the target, and that reply messages go from the target to the initiator.

Perform a simple conversation:

- Start the conversation.

- Send a request from the initiator to the target.

- Receive the request at the target and send a reply to the initiator.

- Receive the reply at the initiator.

- End the conversation.

Messages aren't transmitted across a network for conversations that have both ends in the same instance of the Database Engine. Database Engine security and permissions restricts access to authorized principles. Network encryption isn't needed for this scenario.

This tutorial is divided into three lessons:

| Lesson | Description |
| --- | --- |
| [Lesson 1: Create the conversation objects](lesson-1-creating-the-conversation-objects.md) | In this lesson, you create the message types, contract, services, and queues that are required to support a basic Service Broker conversation. |
| [Lesson 2: Begin a conversation and transmit messages](lesson-2-beginning-a-conversation-and-transmitting-messages.md) | In this lesson, you complete a basic conversation by starting the conversation and transmitting a request message from the initiator to the target. You then transmit a reply message back to the initiator and end the conversation. |
| [Lesson 3: Drop the conversation objects](lesson-3-dropping-the-conversation-objects.md) | In this lesson, you drop the objects that were created to support the conversation. |

## Requirements

To complete this tutorial, you should be familiar with the Transact-SQL language and how to use the Database Engine Query Editor in SQL Server Management Studio. You must be a member of the **db_ddladmin** or **db_owner** fixed database roles for the AdventureWorks2008R2 sample database, or the **sysadmin** fixed server role.

[!INCLUDE [SQL server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

Your system must have the following installed:

- Any edition of SQL Server.
- Either SQL Server Management Studio or Management Studio Express.
- A supported internet browser.

- The AdventureWorks2008R2 sample database. For more information about how to install the sample databases, see [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

## Related content

- [Complete a conversation between databases](completing-a-conversation-between-databases.md)
- [Complete a conversation between instances](completing-a-conversation-between-instances.md)
