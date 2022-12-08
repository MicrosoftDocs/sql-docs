---
title: Typical Uses of Service Broker
description: "Service Broker can be useful for any application that needs to perform processing asynchronously, or that needs to distribute processing across a number of computers."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Typical Uses of Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Service Broker can be useful for any application that needs to perform processing asynchronously, or that needs to distribute processing across a number of computers. Typical uses of Service Broker include:

- Asynchronous triggers

- Reliable query processing

- Reliable data collection

- Distributed server-side processing for client applications

- Data consolidation for client applications

- Large-scale batch processing

## Asynchronous Triggers

Many applications that use triggers, such as online transaction processing (OLTP) systems, can benefit from Service Broker. A trigger queues a message that requests work from a Service Broker service. The trigger does not actually perform the requested work. Instead, the trigger creates a message that contains information about the work to be done and sends this message to a service that performs the work. The trigger then returns.

When the original transaction commits, Service Broker delivers the message to the destination service. The program that implements the service performs the work in a separate transaction. By performing this work in a separate transaction, the original transaction can commit immediately. The application avoids system slowdowns that result from keeping the original transaction open while performing the work.

## Reliable Query Processing

Some applications must reliably process queries, without regard to computer failures, power outages, or similar problems. An application that needs reliable query processing can submit queries by sending messages to a Service Broker service. The application that implements the service reads the message, runs the query, and returns the results. All three of these operations take place in the same transaction. If a failure occurs before the transaction commits, the entire transaction rolls back and the message returns to the queue. When the computer recovers, the application restarts and processes the message again.

## Reliable Data Collection

Applications that collect data from a large set of sources can take advantage of Service Broker to reliably collect data. For instance, a retail application with multiple sites can use Service Broker to send transaction information to a central data store. Because Service Broker provides reliable, asynchronous message delivery, each site can continue to process transactions even if the site temporarily loses connectivity to the central data store. Service Broker security helps to ensure that messages are not misdirected, and helps to protect the data in transit.

## Distributed Server-Side Processing for Client Applications

Large applications that access multiple SQL Server databases can benefit from Service Broker. For example, a Web application for ordering books could use Service Broker on the server side to exchange information between the different databases that contain data on ordering, customer, inventory, and credit. Because Service Broker provides message queuing and reliable message delivery, the application can continue to accept orders even when one of the databases is unavailable or heavily loaded. In this scenario, Service Broker functions as a framework for a distributed OLTP system.

## Data Consolidation for Client Applications

Applications that must use or display information simultaneously from multiple databases can take advantage of Service Broker. For example, a customer service application that consolidates data from multiple locations onto one screen can use Service Broker to run these multiple requests in parallel, rather than sequentially, and in so doing significantly shorten application response time. The customer service application sends requests to different services in parallel; as the services respond to the requests, the customer service application collects the responses and displays the results.

## Large-Scale Batch Processing

Applications that must perform large-scale batch processing can take advantage of the queuing and parallel processing offered by Service Broker to handle large volumes of work quickly and efficiently. The application stores data to be processed in a Service Broker queue. A program periodically reads from the queue and processes the data. An application can take advantage of the reliable messaging provided by Service Broker to perform batch processing on a computer other than the computer from which the request originates.

## See also

- [Logical Architecture](logical-architecture.md)
- [Benefits of Programming with Service Broker](benefits-of-programming-with-service-broker.md)