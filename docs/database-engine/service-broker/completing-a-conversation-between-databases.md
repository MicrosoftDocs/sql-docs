---
title: Completing a Conversation Between Databases
description: "This tutorial is intended for users who are new to Service Broker, but are familiar with database concepts and Transact-SQL statements"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Completing a Conversation Between Databases

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This tutorial is intended for users who are new to Service Broker, but are familiar with database concepts and Transact-SQL statements. It will help new users get started by showing them how to build and run a basic conversation between two databases on the same instance of the Database Engine.

## What you learn

This tutorial builds on the tasks that you learned in the tutorial [Completing a Conversation in a Single Database](completing-a-conversation-in-a-single-database.md). In this tutorial you will learn how to configure the conversation so that it runs between two databases on the same instance of the Database Engine.

The steps that you follow in Lesson 2 are the same as those you followed in Lesson 1, with these exceptions:

- Create two databases: **InitiatorDB** and **TargetDB**. You need to create all the initiator service and queue in the **InitiatorDB** and the target service and queue in the **TargetDB**.

- Create two copies of the message types and contacts, one in the **InitiatorDB** and the other in **TargetDB**. Both sides of the conversation must have access to message type and contract definitions that are identical.

- Set the TRUSTWORTHY database property to ON in the **InitiatorDB**. This is the simplest mechanism for enabling conversations between two databases when they are on the same instance of the Database Engine.

- Learn which statements must be run in each database to complete a conversation, and the sequence in which they must be run.

Messages are not transmitted across a network for conversations that have both ends in the same instance of the Database Engine. Database Engine security and permissions restricts access to authorized principles. Network encryption is not needed for this scenario.

This tutorial is divided into four lessons:

- [Lesson 1: Creating the Databases](lesson-1-creating-the-databases.md)  
    In this lesson, you create the databases and enable the TRUSTWORTHY option in the initiator database.

- [Lesson 2: Creating the Target Conversation Objects](lesson-2-creating-the-target-conversation-objects.md)  
    In this lesson, you create the message types, contract, services, and queues in the target database.

- [Lesson 3: Creating the Initiator Conversation Objects](lesson-3-creating-the-initiator-conversation-objects.md)  
    In this lesson, you create the message types, contract, services, and queues in the initiator database.

- [Lesson 4: Beginning a Conversation and Transmitting Messages](lesson-4-beginning-a-conversation-and-transmitting-messages.md)  
    In this lesson, you complete a basic conversation by beginning the conversation and transmitting a request message from the initiator to the target. Then, you transmit a reply message back to the initiator and end the conversation.

## Requirements

To complete this tutorial, you should be familiar with the Transact-SQL language and using the Database Engine Query Editor in SQL Server Management Studio. You must have CREATE DATABASE, CREATE ANY DATABASE, or ALTER ANY DATABASE permissions to run this tutorial.

Your system must have the following installed:

- Any edition of SQL Server.

- Either SQL Server Management Studio or Management Studio Express.

- A supported internet browser.

## See also

- [Completing a Conversation Between Instances](completing-a-conversation-between-instances.md)