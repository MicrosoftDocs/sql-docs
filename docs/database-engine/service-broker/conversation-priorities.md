---
title: Conversation Priorities
description: "Conversation priorities are a set of user-defined rules, each of which specifies a priority level and the criteria for determining which Service Broker conversations to assign the priority level."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Conversation Priorities

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Conversation priorities are a set of user-defined rules, each of which specifies a priority level and the criteria for determining which Service Broker conversations to assign the priority level. Messages from conversations that have high priority levels are typically sent or received before messages from conversations that have low priority levels.

## Uses of Conversation Priorities

Conversation priorities can be used to do the following:

- Identify conversations that have precedence over others.

- Support different tiers of service, where messages from customers who pay higher rates are sent before messages from customers who pay lower rates.

- Favor customer requests over background tasks. For example, new customer registrations should have a higher priority than sending business transaction summaries to a data warehouse.

## Conversation Priorities and Conversation Endpoints

Conversation priorities are created in each database using the CREATE BROKER PRIORITY statement. Each conversation priority defines the following:

- A name for the conversation priority.

- A priority level to assign Service Broker conversations. The levels are specified as integers from 1 (lowest) to 10 (highest). The default is 5.

- The criteria that determine which conversations the priority level applies to the following:

  - A contract name, or ANY.

  - A local service name, or ANY.

  - A remote service name, or ANY.

Service Broker assigns the priority levels to conversation endpoints when the endpoints are created. Each conversation has two conversation endpoints:

- The initiator conversation endpoint associates one side of the conversation with the initiator service and initiator queue. The initiator conversation endpoint is created when the BEGIN DIALOG statement is run. The operations associated with the initiator conversation endpoint include the following:

  - Sends from the initiator service.

  - Receives from the initiator queue.

  - Getting the next conversation group from the initiator queue.

- The target conversation endpoint associates the other side of the conversation with the target service and queue. The target conversation endpoint is created when the first message from the initiator is put in the target queue. The operations associated with the target conversation endpoint include the following:

  - Receives from the target queue.

  - Sends from the target service.

  - Getting the next conversation group from the target queue.

Which service is evaluated as a local or remote service depends on the type of conversation endpoint:

- For the initiator conversation endpoint, the initiator service is the local service and the target service is the remote service.

- For the target conversation endpoint, the target service is the local service and the initiator service is the remote service.

## How Service Broker Assigns Priority Levels

Service Broker assigns conversation priority levels when conversation endpoints are created. The conversation endpoint retains the priority level until the conversation ends. New priorities or changes to existing priorities are not applied to existing conversations.

Service Broker assigns the conversation endpoint the priority level from the conversation priority whose contract and services criteria best match the endpoint properties. The following table shows the match precedence:

:::row:::
   :::column span="1":::
   **Endpoint Contract**
   :::column-end:::
   :::column span="1":::
   **Endpoint Local Service**
   :::column-end:::
   :::column span="1":::
   **Endpoint Remote Service**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Priority Contract

   :::column-end:::
   :::column span="1":::
   Priority Local Service

   :::column-end:::
   :::column span="1":::
   Priority Remote Service

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Priority Contract

   :::column-end:::
   :::column span="1":::
   Priority Local Service

   :::column-end:::
   :::column span="1":::
   ANY

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Priority Contract

   :::column-end:::
   :::column span="1":::
   ANY

   :::column-end:::
   :::column span="1":::
   Priority Remote Service

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Priority Contract

   :::column-end:::
   :::column span="1":::
   ANY

   :::column-end:::
   :::column span="1":::
   ANY

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   ANY

   :::column-end:::
   :::column span="1":::
   Priority Local Service

   :::column-end:::
   :::column span="1":::
   Priority Remote Service

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   ANY

   :::column-end:::
   :::column span="1":::
   Priority Local Service

   :::column-end:::
   :::column span="1":::
   ANY

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   ANY

   :::column-end:::
   :::column span="1":::
   ANY

   :::column-end:::
   :::column span="1":::
   Priority Remote Service

   :::column-end:::
:::row-end:::

:::row:::
  :::column span="1":::
  ANY

  :::column-end:::
  :::column span="1":::
  ANY

  :::column-end:::
  :::column span="1":::
  ANY

  :::column-end:::
:::row-end:::

Service Broker first looks for a priority whose specified contract, local service, and remote service matches those used by the conversation endpoint. If one is not found, Service Broker then looks for a priority with a contract and local service that matches those used by the endpoint, and where the remote service was specified as ANY. This continues for all the variations listed in the precedence table. If no match is found, the endpoint is assigned the default priority of 5.

The Service Broker communication protocols do not transmit priority levels between conversation endpoints. Service Broker independently assigns a priority level to each endpoint. To have Service Broker assign priority levels to both the initiator and target conversation endpoints, you must ensure that both endpoints are covered by conversation priorities. If the initiator and target conversation endpoints are in separate databases, you must create conversation priorities in each database. If the initiator and target endpoints are in the same database:

- You can cover both conversation endpoints by using one conversation priority that specifies the contract name that is used by the conversation and ANY for both the local and remote service names.

- You can cover each conversation endpoint separately using two conversation priorities:

  - One conversation for the initiator endpoint that specifies the initiator service name for LOCAL_SERVICE_NAME and the target service name for REMOTE_SERVICE_NAME.

  - One conversation for the target endpoint that specifies the target service name for LOCAL_SERVICE_NAME and the initiator service name for REMOTE_SERVICE_NAME.

The same priority level is usually specified for both of the conversation endpoints for a conversation. While you can specify different priority levels for each endpoint, doing this does not mean messages are sent faster in one direction than the other. Messages are sent from one conversation endpoint and received at the other endpoint. Therefore, each message transmission is affected by the priority levels assigned to both endpoints. For example, you could configure a conversation so that the initiator conversation endpoint has priority level 10 and the target endpoint has priority level 1. In this case:

- Messages transmitted from the initiator service by using priority level 10 are received from the target queue using priority level 1.

- Messages transmitted from the target service by using priority level 1 are received from the initiator queue using priority level 10.

A conversation group is assigned the same priority level as the highest priority level assigned to any conversation where the following are true:

- The conversation is a member of the group.

- The conversation currently has messages in the service queue.

All conversation endpoints in a database are assigned default priorities of 5 if no conversation priorities have been created in the database.

Conversation priorities do not affect message forwarding, which always operates at the default priority level of 5.

## Conversation Priority Example

Consider a system with the following:

- An **InitiatorDB** containing an **InitiatorService** and **InitiatorQueue**.

- A **TargetDB** containing a **TargetService** and **TargetQueue**.

- A contract named **SimpleContract**, which specifies that **RequestMessages** are sent from the **InitiatorService** to the **TargetService**. It also specifies that **ReplyMessages** are sent from the **TargetService** to the **InitiatorService**.

This script specifies the priority level for the initiator conversation endpoint and its associated operations:

- The SEND of the **RequestMessage** from the **InitiatorService** to the **TargetQueue**.

- The RECEIVE of the **ReplyMessage** from the **InitiatorQueue**.

<!-- end list -->

```sql
    USE InitiatorDB;
    GO
    CREATE BROKER PRIORITY InitiatorToTargetPriority
        FOR CONVERSATION
        SET (CONTRACT_NAME = SimpleContract,
             LOCAL_SERVICE_NAME = InitiatorSerivce,
             REMOTE_SERVICE_NAME = N'TargetService',
             PRIORITY_LEVEL = 3);
    GO
```

This script specifies the priority level for the target conversation endpoint and its associated operations:

- The RECEIVE of the **RequestMessage** from the **TargetQueue**.

- The SEND of the **ReplyMessage** from the **TargetService** to the **InitiatorQueue**.

<!-- end list -->


```sql
    USE TargetDB;
    GO
    CREATE BROKER PRIORITY TargetToInitiatorPriority
        FOR CONVERSATION
        SET (CONTRACT_NAME = SimpleContract,
             LOCAL_SERVICE_NAME = TargetService,
             REMOTE_SERVICE_NAME = N'InitiatorService',
             PRIORITY_LEVEL = 3);
    GO
```

## How Priorities Operate

Typically, Service Broker sends and receives messages for high priority conversations before sending and receiving messages for low priority conversations. Messages from high priority conversations spend less time in queues than messages from low priority conversations.

### Reception Priority Levels

Priority levels are always applied to operations that receive messages or conversation group identifiers from a queue.

Priority level is one of the factors determining the set of messages retrieved by a RECEIVE and the sequence in which the messages are retrieved:

- Each RECEIVE statement always retrieves messages from one conversation group:

  - A RECEIVE that has no WHERE clause retrieves messages that belong to the highest-priority unlocked conversation group that has messages in the queue.

  - A RECEIVE that has a WHERE clause retrieves the messages for the conversation group specified in the WHERE clause.

- Within a conversation group, RECEIVE retrieves messages depending on the priority level of the conversations in the group. All of the messages from the conversation with the highest priority level are retrieved first, then the messages for the conversation with the next highest priority level, and so on.

- Within a conversation, the messages are retrieved in the same sequence as they were sent.

GET CONVERSATION GROUP returns the group with the highest priority level from the set of unlocked groups that have messages in the queue.

### Transmission Priority Levels

Messages in the transmission queues for an instance are transmitted in sequence based on:

- The priority level of their associated conversation endpoint.

- Within priority level, their send sequence in the conversation.

Service Broker coordinates priority levels across all of the transmission queues in an instance of the Database Engine. Service Broker first transmits messages from the priority 10 conversations in all of the transmission queues, then messages from priority 9 conversations, and so on.

The relative difference in message performance increases with the difference in priority levels. In a system using two adjacent priority levels, such as 9 and 10, the messages with the higher priority level will have a small performance advantage. In a system using two widely separated priority levels, such as 1 and 10, the messages with the higher priority level have a larger performance advantage. In systems using multiple priority levels, most of the processing is allocated to the top 2 or 3 priority levels.

Priority levels specified in conversation priorities are only applied to messages in the transmission queue if the HONOR_BROKER_PRIORITY database option is set to ON. If HONOR_BROKER_PRIORITY is set to OFF, all messages placed in the transmission queue for that database are sent using the default priority level of 5. When viewed using **sys.transmission_queue**, the message still displays the priority level it received from the endpoint, but the default priority level is used to transmit the message.

Because priority levels are applied to messages in the transmission queue, they typically do not affect messages sent between services in the same instance of the Database Engine. Messages that are sent to a service in the same instance are placed directly into the service's queue without going through a transmission queue. Some conditions might cause local messages to be placed in the transmission queue, such as some types of errors or the destination queue being inactive. If the message is stored in the transmission queue, the relevant priority level is applied.

Messages and message fragments may be sent out of priority order:

- Service Broker sends messages between instances of the Database Engine using blocks of message fragments. If there are several message fragments with different priorities ready to send to one instance, Service Broker may send all of the fragments in one block. Some of the fragments at the end of the block may have a lower priority level than message fragments waiting for transmission to another instance.

- Service Broker includes a starvation prevention mechanism to help keep large numbers of high priority messages from blocking low priority messages. A low priority message that has been waiting for a long time can be sent even if there are higher priority messages in the queue.

While individual messages or message fragments may be sent out of priority order, the effects should be small when considered across many message sends.

## See also

- [ALTER BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/alter-broker-priority-transact-sql.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [BEGIN DIALOG CONVERSATION (Transact-SQL)](../../t-sql/statements/begin-dialog-conversation-transact-sql.md)
- [CREATE BROKER PRIORITY (Transact-SQL)](../../t-sql/statements/create-broker-priority-transact-sql.md)
- [CREATE QUEUE (Transact-SQL)](../../t-sql/statements/create-queue-transact-sql.md)
- [CREATE SERVICE (Transact-SQL)](../../t-sql/statements/create-service-transact-sql.md)
- [GET CONVERSATION GROUP (Transact-SQL)](../../t-sql/statements/get-conversation-group-transact-sql.md)
- [RECEIVE (Transact-SQL)](../../t-sql/statements/receive-transact-sql.md)
- [SEND (Transact-SQL)](../../t-sql/statements/send-transact-sql.md)
- [sys.conversation_priorities (Transact-SQL)](../../relational-databases/system-catalog-views/sys-conversation-priorities-transact-sql.md)
- [sys.transmission_queue (Transact-SQL)](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md)
- [Conversation Architecture](conversation-architecture.md)
- [Service Architecture](service-architecture.md)
- [Service Broker Routing and Networking](service-broker-routing-and-networking.md)
