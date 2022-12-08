---
title: Uninstalling Service Broker Applications
description: "You must uninstall a Service Broker application only when the database continues to be hosted in the same instance, but no longer provides the service that the application implements."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Uninstalling Service Broker Applications

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

You must uninstall a Service Broker application only when the database continues to be hosted in the same instance, but no longer provides the service that the application implements. Dropping a database drops the Service Broker objects within that database. Moving a database from one instance to another also moves the services hosted within that database.

## Uninstalling an Initiating Service

To uninstall an initiating service, first stop the application from creating outgoing messages. The best way to prevent the application from sending new messages depends on the type of application. For an application that uses a stored procedure to initiate a conversation, you may need to drop that stored procedure. For an external program, you may need to make the program unavailable to users, or uninstall the program.

After you stop the service from creating new messages, make sure that the service processes each message that remains in the queue. You can write a simple procedure that receives each message on the queue, ends the conversation with an error, and removes the state for the conversation. Processing all the messages in the queue allows the target applications to end the conversation gracefully rather than wait for a response from the service you are shutting down.

Finally, drop the service definition to ensure that Service Broker no longer accepts messages for the service. Drop any routes for the service. Drop the contracts, message types, and queues for the service unless these objects are used by another service in the instance. If necessary, drop the activation stored procedure for the service.

## Uninstalling a Target Service

To uninstall a target service, first make sure that the service processes each message that remains in the queue. You can let the application process the messages, write a simple procedure that ends the conversations with an application-specific error, or simply drop the service to end the conversations with a Service Broker error. Whichever method you choose, ending the conversations allows the initiating applications to end gracefully rather than waiting for a response from the service you are shutting down.

In databases that host initiating services, drop each route that contains the network address for the service that you are uninstalling. Drop the routes for this service in the **msdb** database of instances that forward messages for this service. If the instance that hosts the service contains a route in **msdb** for the service, drop that route.

Finally, drop the service definition to ensure that Service Broker no longer accepts messages for the service. Drop the contracts, message types, and queues for the service unless these objects are used by another service in the instance. If necessary, drop the activation stored procedure for the service.

## See also

- [ALTER QUEUE (Transact-SQL)](../../t-sql/statements/alter-queue-transact-sql.md)
- [Starting and Stopping the Queue](starting-and-stopping-the-queue.md)
