---
title: Creating Service Broker Applications
description: "This section provides a general overview of the structure of a Service Broker application, discusses some of the most common strategies for starting an application that uses Service Broker, and describes the basic steps to receive and process messages."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Creating Service Broker Applications

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This section provides a general overview of the structure of a Service Broker application, discusses some of the most common strategies for starting an application that uses Service Broker, and describes the basic steps to receive and process messages.

The application for an initiating service uses the BEGIN DIALOG statement to specify information about the services at each endpoint and the service contract that the application will use to communicate. The application uses the SEND statement to send the first message of the conversation to the target service. The application must be prepared to receive and process messages from Service Broker, even if the contract does not allow the target service to return messages. The initiating application is often implemented as two separate components. One component begins the conversation; the other component processes messages that arrive in the queue.

The application for a target service receives and processes messages from the initiating service. The application must also be prepared to receive and process messages from Service Broker.

Depending on the needs of the service, the part of the application that processes the queue can be started in several different ways. For more information about starting an application that uses Service Broker, see [Choosing a Startup Strategy](choosing-a-startup-strategy.md).

However the application starts, the application begins a transaction and uses the RECEIVE statement to dequeue a message. The application extracts the data from the messages and does any necessary processing. If necessary, the application uses the SEND statement to send messages to the other side of the conversation. The application then commits the transaction. For efficiency, the application may process multiple messages within the same transaction. Services that maintain state often use the GET CONVERSATION GROUP statement to lock a conversation group, retrieve state for the conversation group, and then process multiple messages for the conversation group.

The conversation continues, using SEND and RECEIVE statements to transmit messages between the endpoints. At any time, if necessary, either participant in the conversation may use BEGIN DIALOG to start a conversation with another service to get additional information. For example, an application that is processing an event notification may initiate another conversation with a service that provides personnel information in order to retrieve current contact information before sending out an alert.

When the conversation has achieved its purpose, the application at the appropriate endpoint will use an END CONVERSATION statement to end the conversation. The other participant receives the END CONVERSATION message and issues an END CONVERSATION message on its side as well. Once both participants have issued END CONVERSATION messages, the conversation ends.

If an error occurs, one participant in the conversation may indicate failure by ending the conversation using the WITH ERROR clause. Using the WITH ERROR clause ends the conversation and sends a Service Broker error message to the other participant in the conversation.

If Service Broker detects an error or the conversation lifetime expires, Service Broker ends the conversation and returns an error message to the active participants. If Service Broker cannot establish a conversation, the only active participant is the initiating application, and the error is delivered to the initiating service. If one participant has already ended the conversation, the error message is delivered to the other participant. Otherwise, the error is delivered to both participants in the conversation.

## In This Section

- [Choosing a Startup Strategy](choosing-a-startup-strategy.md)  
    Discusses some of the most common strategies for starting an application that uses Service Broker.

- [Service Broker Application Outline](service-broker-application-outline.md)  
    Describes the basic steps to receive and process messages.

## See also

- [Service Broker Activation](service-broker-activation.md)
- [Transactional Messaging](transactional-messaging.md)