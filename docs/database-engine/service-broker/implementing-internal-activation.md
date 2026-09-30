---
title: Implement Internal Activation
description: "This tutorial is intended for users who are new to Service Broker, but are familiar with database concepts and Transact-SQL statements."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Implement internal activation in Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This tutorial is intended for users who are new to Service Broker, but are familiar with database concepts and Transact-SQL statements. It helps new users get started by showing them how to implement an internal activation stored procedure to process Service Broker messages.

## Concepts

This tutorial shows you how to create the database objects that are required to support a basic request-reply Service Broker conversation using an internal activation stored procedure. You then start a conversation and use it to transmit messages.

Each Service Broker conversation has two ends: the conversation initiator and target. In a request-reply conversation, a request message us sent from the initiator to the target, which returns a reply message. Service Broker internal activation can be used to run a stored procedure whenever there are messages to process. Service Broker can run multiple copies of the stored procedure if there are many messages being transmitted. This tutorial shows you how to create a stored procedure that receives the request messages at the target, and how to configure the target to use internal activation to run the stored procedure.

This lesson guides you to perform the following tasks:

- Create a service and queue for the target and a service and queue for the initiator.

- Create a request message type and a reply message type.

- Create a contract that specifies request messages go from the initiator to the target, and that reply messages go from the target to the initiator.

- Create a stored procedure that receives request messages from the target queue and sends reply messages to the initiator.

- Alter the target queue to enable internal activation of the stored procedure.

You then perform a basic conversation:

- Start the conversation.

- Send a request from the initiator to the target.

- Service Broker then activates the stored procedure. The stored procedure receives the request at the target, and sends a reply to the initiator.

- Receive the reply at the initiator.

- End the initiator side of the conversation.

- Service Broker then activates the stored procedure a second time, and the stored procedure ends the target side of the conversation.

Messages aren't transmitted across a network for conversations that have both ends in the same instance of the Database Engine. Database Engine security and permissions restrict access to authorized principles. Network encryption isn't needed for this scenario.

This tutorial is divided into the following lessons.

| Lesson | Description |
| --- | --- |
| [Lesson 1: Create the base conversation objects](lesson-1-creating-the-base-conversation-objects.md) | Create the message types, contract, services, and queues that are required to support a basic Service Broker conversation. |
| [Lesson 2: Create an internal activation procedure](lesson-2-creating-an-internal-activation-procedure.md) | Create the stored procedure that will receive messages from the target queue, then alter the target queue to specify internal activation. |
| [Lesson 3: Begin a conversation and transmit messages](lesson-3-beginning-a-conversation-and-transmitting-messages.md) | Complete a basic conversation by starting the conversation and transmitting a request message from the initiator to the target. The internal activation stored procedure receives the request message, and returns a reply message. You then end the initiator side of the conversation, and the stored procedure ends the target side of the conversation. |
| [Lesson 4: Drop the conversation objects](lesson-4-dropping-the-conversation-objects.md) | Drop the objects that were created to support the conversation. |

## Requirements

To complete this tutorial, you should be familiar with the Transact-SQL language and how to use the Database Engine Query Editor in SQL Server Management Studio. You must be a member of the **db_ddladmin** or **db_owner** fixed database roles for the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database, or the **sysadmin** fixed server role.

Your system must have the following installed:

- Any supported edition of SQL Server

- [SQL Server Management Studio](/ssms/)

- A supported internet browser

- The [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database, which you can download from the [Azure Data SQL Samples Repository](https://github.com/microsoft/sql-server-samples) GitHub repository

## Related content

- [Complete a conversation in a single database](completing-a-conversation-in-a-single-database.md)
- [Complete a conversation between databases](completing-a-conversation-between-databases.md)
- [Complete a conversation between instances](completing-a-conversation-between-instances.md)
