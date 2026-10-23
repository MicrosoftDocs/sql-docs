---
title: Developer Responsibilities for Service Broker
description: "The application developer is responsible for designing the Service Broker application and creating elements that require programming."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 08/29/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Developer responsibilities for Service Broker

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

The application developer is responsible for designing the Service Broker application and creating elements that require programming. The system administrator is responsible for configuring and managing Service Broker. Developers and administrators need to work together when planning the system, to ensure that it's developed and managed optimally for their particular environment and business purposes.

The tasks involved in creating an individual application depend on the needs of the application. The following tasks are a common sequence for developing a Service Broker application:

1. Plan the application. Create an outline of the tasks that the application must accomplish. Describe the conversations that take place during each task. Which endpoint needs to provide what information, in what order? What processing must take place? What priorities should be assigned to the conversations? All subsequent information depends on this outline.

1. Determine the format and structure of each message in each conversation. Plan the expected sequence of exchange for the messages, and how each participant in the conversation should respond to errors and messages that are sent in an unexpected order.

1. If the conversation uses XML messages, create a schema for each XML message. You use schemas during development, testing, and troubleshooting. When your service is in production, you might decide to remove schema validation from your message types, to improve performance.

1. Create a message type for each message in each conversation.

1. Create a contract for each conversation. The contract identifies the message types that each endpoint can use in the conversation.

1. Create a queue to store the messages that the application will receive.

1. Create a service to expose the functionality defined by the contract and implemented by the stored procedure that you created. When creating a service, you associate it with the queue you created in the previous step. This step tells Service Broker that all messages that arrive addressed to that service are to be placed in the identified queue.

1. Review the priority plans that you established in step 1. Create conversation priorities that cover all conversation endpoints that are designed to use priority levels other than the default. If the priority levels should be used when messages are transmitted from a database, ensure that the `HONOR_BROKER_PRIORITY` option in that database is set to `ON`.

1. Create an application that implements the expected message exchange pattern and processing requirements identified in step 1. If your application uses internal activation, the application is a stored procedure.

1. If your application uses internal activation, alter the queue to enable activation. Specify the stored procedure created in step 8 as the activation stored procedure.

1. Identify the services that use the service that you just created. If any of these services exist outside of the local SQL Server instance, create routes for them.

1. Review the remote services that you identified in the previous step and determine the security requirements for communications with them. If necessary, create certificates to enforce these requirements, then create database users for the certificates. Associate the certificates with these users. The administrators or developers of the other services must create remote service bindings to enable dialog security on traffic to this service.

1. During development and testing, it's often convenient for an application to work with the user names that the application uses in production. However, you should associate those user names with certificates that are used only in the development and test environment. When the application moves into production, use certificates created for the production environment. By using different certificates, you can reduce the effort involved in deploying the application while still maintaining security between the development environment and the production environment.

## Related content

- [Plan for Service Broker development](planning-for-service-broker-development.md)
