---
title: Creating a Remote Service Binding
description: "To exchange messages with Service Broker, you must create the appropriate user security context."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Creating a Remote Service Binding

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To exchange messages with Service Broker, you must create the appropriate user security context. The most flexible way to do this is with a remote service binding. A remote service binding establishes a relationship between a local database user, the certificate for the user, and the name of a remote service. Service Broker uses the remote service binding to provide dialog security for conversations that target the remote service. This binding defines the security credentials to use to initiate a conversation with a remote service.

When a conversation is initiated, Service Broker checks to see whether a remote service binding is available for the target service. If a remote service binding is not available, Service Broker checks the routing table for the Broker Configuration Notice (BCN) service; the service name is **SQL/ServiceBroker/BrokerConfiguration**. If a BCN route exists, Service Broker creates a new conversation with the BCN and sends a message on that conversation that requests creation of a remote service binding. The application defined for the BCN queue must then respond to the request.

> [!NOTE]
> The behavior is comparable to [Service Broker Dynamic Routing](service-broker-dynamic-routing.md).

## Requesting Remote Service Binding

Requests for remote service bindings use the message type `https://schemas.microsoft.com/SQL/ServiceBroker/BrokerConfigurationNotice/MissingRemoteServiceBinding`. The message is in XML format, and contains the name of the service for which remote service binding information should be available.

For example, the following message is a request for a remote service binding to the service **http://Adventure-Works.com/Elsewhere**:

```xml
    <MissingRemoteServiceBinding xmlns="https://schemas.microsoft.com/SQL/ServiceBroker/BrokerConfigurationNotice/MissingRemoteServiceBinding">
      <SERVICE_NAME>http://Adventure-Works.com/Elsewhere</SERVICE_NAME>
    </MissingRemoteServiceBinding>
```

The application that is defined in the BCN queue reads a **MissingRemoteServiceBinding** message from the queue. If the application can determine the appropriate user context for the target service, the application creates a remote service binding for the service and then ends the conversation. If the application cannot determine the remote service binding for the service, the application ends the conversation with an error.

If the application ended the conversation with an error, Service Broker stores this response for 10 minutes. Service Broker will not send a **MissingRemoteServiceBinding** message for dialogs to the BCN service during this time. If a conversation is started after 10 minutes, Service Broker will send another **MissingRemoteServiceBinding** message to check whether a remote service binding has been created.

In either case, the application ends the conversation. Service Broker sends one **MissingRemoteServiceBinding** message at a time for a specific service regardless of the number of conversations to the service. Further, Service Broker uses the longest possible time out for requests to the BCN service. Therefore if the BCN service does not end the conversation, Service Broker does not create a new request to the service. If the BCN service did not create a remote service binding, secure messages (ENCRYPTION=ON) remain delayed until the conversation lifetime expires. However, non-secure messages (ENCRYPTION=OFF) will continue after the BCN service ends the conversation.

## See also

- [CREATE REMOTE SERVICE BINDING (Transact-SQL)](../../t-sql/statements/create-remote-service-binding-transact-sql.md)
- [Service Broker Configuration Notice Service](service-broker-configuration-notice-service.md)
