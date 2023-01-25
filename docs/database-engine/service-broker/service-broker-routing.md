---
title: Service Broker Routing
description: "This topic describes the details of how Service Broker routes messages."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Routing

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes the details of how Service Broker routes messages. For an overview, see [Routes](routes.md).

For most applications, a simple approach to Service Broker routing works well. In each database that contains a service, specify a route for the external services that the service communicates with. However, Service Broker provides a sophisticated routing system for handling cases where an application needs more complex behavior. For examples that illustrate the routing process, see [Service Broker Routing Examples](service-broker-routing-examples.md).

## Routing Process Description

SQL Server maintains two distinct levels of routing information. Each database contains a local routing table, **sys.routes**, for conversations begun in that database. For conversations that originate in the instance of SQL Server, SQL Server searches the routing table in the database that created the conversation. For conversations that arrive from outside the instance, SQL Server searches **msdb.sys.routes**.

The basic matching process is identical whether the conversation originates in the instance or outside the instance. The process ignores routes that have expired. The routing process consists of three distinct steps:

1.  **Finding matching routes**. Service Broker finds a set of possible routes by matching the service name and the Service Broker identifier.

2.  **Choosing a route**. Service Broker chooses a route from among the set of possible routes.

3.  **Locating the destination service**. When the route chosen specifies **'LOCAL'** as the network address, Service Broker locates the service in the instance. If the service does not exist in the instance, Service Broker might return to step 2 and choose another route.

When a message has been sent from the initiator to the target and the initiator receives an acknowledgment message from the target, the initiator uses the Service Broker identifier in the acknowledgment messages to route subsequent messages to the same target. Service Broker handles acknowledgment messages; the process is transparent to an application that uses Service Broker. For more information about acknowledgment messages, see [Service Broker Communication Protocols](service-broker-communication-protocols.md).

## Reply Messages From a Target Service

When a message arriving from outside the instance is from a target service, SQL Server checks to see whether the current instance contains the Service Broker identifier in the message. If so, then the message is delivered in the current instance as described in "Locating the Destination Service." Otherwise, SQL Server follows the standard matching process.

## Finding Matching Routes

The following procedure describes how SQL Server matches routes. At each step, if one or more routes match, the matching process ends, and Service Broker chooses one of the matching routes as follows:

1.  If the conversation specifies a Service Broker identifier, find a route with an exact match for both the service name and the Service Broker identifier.

2.  Find an exact match for the service name among routes that do not specify a Service Broker identifier.

3.  If the conversation does not specify a Service Broker identifier, find an exact match for the service name among routes that specify a Service Broker identifier. If the routing table contains routes that match the service name and have different Service Broker identifiers, arbitrarily pick a Service Broker identifier. Then, match only the routes that use that Service Broker identifier.

4.  If a route to a dynamic routing service is present, and no request for a route to the service is pending, mark the conversation delayed and request routing information from that service.

5.  Find a route that specifies neither the service name nor the Service Broker identifier.

6.  If the conversation specifies a Service Broker identifier and if the instance contains one or more databases that contain services with names that match the name that was specified in the conversation, route the conversation as if the routing table contained a route with the service name and the network address **'LOCAL'**.

7.  Mark the conversation delayed.

When a conversation is marked delayed, Service Broker performs the matching process again after a time-out period. Notice that failure to find a matching route is not considered an error.

## Choosing a Route

If the matching process finds more than one matching route, Service Broker chooses one route from among the matching routes. For this purpose, routes that have the same Service Broker identifier, service name, and network address are considered to be identical. Service Broker uses the following procedure to choose the exact route. At each step, the process continues at the next step if there are no routes that match the address specification for the step.

1.  Choose one route from among the routes that specify a mirror address.

2.  Choose one route from among the routes that specify **'LOCAL'** as the network address. If this instance of SQL Server does not contain a service that matches the name that was specified in the conversation, continue at step 3.

3.  Choose one route from among the routes that specify a network address.

4.  Choose one route from among the routes that specify **'TRANSPORT'** as the network address.

If broker forwarding is not active, Service Broker drops the message if the conversation does not originate in the current instance and the address of the route chosen is not **'LOCAL'**.

## Locating the Destination Service

As described earlier, Service Broker delivers messages to a service in the current instance when the matching route specifies **'LOCAL'** as the network address. For messages that originate outside the instance, the route must be in **msdb.sys.routes**. For messages that originate in the instance, the matching route must be in the **sys.routes** table for the database that initiates the conversation.

When Service Broker determines that the service for the message is in the current instance, Service Broker must locate the service in the instance. When a Service Broker identifier for the conversation exists in either the conversation or the route, Service Broker delivers messages to the database identified by the Service Broker identifier.

Otherwise, Service Broker locates the service by first searching for the service name in the database that contains the conversation. Then, it searches for the service name in the other databases in the instance. Service Broker delivers the message to the first service located. Notice, however, that the order in which Service Broker searches the other databases in an instance is unspecified, and is not guaranteed to be consistent from conversation to conversation. This means that if more than one copy of the target service exists in the instance, Service Broker randomly picks the service to target.

## Other Considerations

For improved reliability, Service Broker routing contains safeguards against routing loops. Service Broker routing is aware of database mirroring, and can transparently redirect conversations to the active partner of a mirrored database.

### Routing Loops

Service Broker message forwarding tracks the number of times that a message has been forwarded to protect against endless routing loops. For more information, see [Service Broker Message Forwarding](service-broker-message-forwarding.md).

If the matching route contains a network address that resolves to the current instance, SQL Server treats the conversation as if the conversation originated outside the instance. Service Broker routes messages for the conversation using the routes in **msdb.sys.routes**. Routing for these messages is identical to routing for messages from outside the instance. In particular, message forwarding must be active for Service Broker to forward the message to a network address other than **'LOCAL'**.

### Mirror Addresses

Routes with mirror addresses have the highest precedence when choosing a route from among the initial set of matching routes. However, Service Broker does not give special consideration to mirror addresses when finding matching routes for a conversation.

When Service Broker chooses a route that specifies a mirror address, and Service Broker has not previously delivered a message using the route, Service Broker sends a request to both addresses to determine which instance is currently the principal. When Service Broker identifies the principal, Service Broker sends all messages that use the route to the principal without contacting the mirror instance. If the principal is unreachable, or that instance indicates that it is no longer the principal, Service Broker sends messages to the other address for the pair if the instance of SQL Server at the other address indicates that it is the new principal.

In cases where Service Broker cannot reach the principal but the partner does not claim to be new principal, Service Broker does not send messages to the partner. Service Broker then retries the principal address and the partner address until either the principal is reachable, or the partner indicates that it is now the principal. By taking this approach, Service Broker reliably delivers messages when the principal and partner can communicate, but the instance sending the message cannot reach the principal.

## See also

- [sys.routes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md)
- [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md)
- [ALTER ROUTE (Transact-SQL)](../../t-sql/statements/alter-route-transact-sql.md)
- [DROP ROUTE (Transact-SQL)](../../t-sql/statements/drop-route-transact-sql.md)
- [Service Broker Message Forwarding](service-broker-message-forwarding.md)
- [Service Broker Communication Protocols](service-broker-communication-protocols.md)