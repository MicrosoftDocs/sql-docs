---
title: Completing a Conversation Between Instances
description: "It will help new users get started by showing them how to build and run a simple conversation between two databases on separate instances of the Database Engine."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Completing a Conversation Between Instances

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This tutorial is intended for users who are new to Service Broker, but are familiar with database concepts and Transact-SQL statements. It will help new users get started by showing them how to build and run a simple conversation between two databases on separate instances of the Database Engine.

## What you learn

This tutorial builds on the tasks that you learned in [Completing a Conversation Between Databases](completing-a-conversation-between-databases.md). In this tutorial, you will learn how to configure a conversation so that it runs between two instances of the Database Engine.

The steps that you follow in this tutorial are the same as those you followed in the Completing a Conversation Between Databases tutorial, with these exceptions:

- The two databases are on separate instances of the Database Engine.

- You can learn how to create Service Broker endpoints and routes to establish network connections between two instances.

- The previous tutorials did not transmit messages on the network. Therefore, they used Database Engine permissions to help protect against unauthorized access to messages. In Lesson 3, you will learn how to create certificates and remote service bindings to encrypt messages on the network.

In this tutorial, the instance of the Database Engine that contains the initiator database is referred to as the initiator instance. The instance that contains the target database is referred to as the target instance.

This tutorial is divided into six lessons:

- [Lesson 1: Creating the Target Database](lesson-1-creating-the-target-database.md)  
    In this lesson, you create the target database and all the objects that do not have dependencies on the initiator database. This includes the endpoint, master key, certificate, users, message types, contract, service, and queue.

- [Lesson 2: Creating the Initiator Database](lesson-2-creating-the-initiator-database.md)  
    In this lesson, you create the initiator database and its endpoint, master key, certificate, users, routes, remote service bindings, message types, contract, service, and queue.

- [Lesson 3: Completing the Target Conversation Objects](lesson-3-completing-the-target-conversation-objects.md)  
    In this lesson, you create the target objects that have dependencies on the initiator database. This includes certificates, users, routes, and remote service bindings.

- [Lesson 4: Beginning the Conversation](lesson-4-beginning-the-conversation.md)  
    In this lesson, you start the conversation and send a request message from the initiator to the target.

- [Lesson 5: Receiving a Request and Sending a Reply](lesson-5-receiving-a-request-and-sending-a-reply.md)  
    In this lesson, you receive the request message at the target service and send a reply message back to the initiator.

- [Lesson 6: Receiving the Reply and Ending the Conversation](lesson-6-receiving-the-reply-and-ending-the-conversation.md)  
    In this lesson, you receive the reply message at the initiator service and end the conversation.

## Requirements

To complete this tutorial, you should be familiar with the Transact-SQL language and how to use the Database Engine Query Editor in SQL Server Management Studio.

You must have two instances of the Database Engine installed. If the two instances are on separate computers, always connect to each instance from a copy of Management Studio on the same computer. For example, do not connect to the initiator instance from a copy of Management Studio on the target computer.

You must have a single login authorized in both instances. In both instances, the login must be either a member of the **sysadmin** fixed server role, or have the following permissions to run this tutorial:

- ALTER ANY LINKED SERVER.

- CREATE ENDPOINT.

- At least one of the CREATE DATABASE, CREATE ANY DATABASE, or ALTER ANY DATABASE permissions.

Both instances of the Database Engine must be running under a Windows account that is a valid login in the other instance.

Both systems must have the following installed:

- Any edition of SQL Server, but only one of the instances can be SQL Server Express Edition.

- Either SQL Server Management Studio or Management Studio Express.

- A supported internet browser.

The firewalls for both systems must be configured to enable connections to UDP port 1434 and TCP ports 1433 and 4022. Use the Configuration Manager tool to ensure that both instances allow TCP/IP connections, and that the SQL Server Browser service is running on both computers.

## See also

- [Completing a Conversation in a Single Database](completing-a-conversation-in-a-single-database.md)
- [Completing a Conversation Between Databases](completing-a-conversation-between-databases.md)