---
title: Service Broker Routing Examples
description: "This section presents examples of the Service Broker routing process."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Routing Examples

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This section presents examples of the Service Broker routing process. Each example contains sample routing tables for **AdventureWorks2008R2** and **msdb**, and describes how Service Broker uses those routing tables to choose a route for the message.

[!INCLUDE [SQL Server Service Broker AdventureWorks2008R2](../../includes/service-broker-adventureworks-2008-r2.md)]

The routing tables presented in this topic are simplified versions of the **sys.routes** catalog view. The route ID and the owner are not important for the routing process, and all routes are considered to have an indefinite lifetime.

A value of NULL in the **remote_service_name** column matches any service name. A value of NULL in the **broker_instance** column matches any Service Broker identifier.

The examples for outgoing messages do not use the routing table in **msdb**, and the examples for incoming messages and message forwarding do not use the routing table for **AdventureWorks2008R2**.

## Example 1: Default Configuration

This example describes the default configuration for Service Broker routing. By default, all databases except **master** contain the **AutoCreatedLocal** route. Therefore, the routing tables for **AdventureWorks2008R2** and **msdb** contain the following information.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

In this case, all dialogs created in the **AdventureWorks2008R2** database are delivered to a service in the current instance. In addition, all dialogs arriving from outside the instance are delivered to a service in the current instance.

For conversations created in **AdventureWorks2008R2**, **AutoCreatedLocal** is the only route in **AdventureWorks2008R2.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

The process is the same for conversations that arrive from outside of the instance. For conversations that arrive from outside of the instance, **AutoCreatedLocal** is the only route in **msdb.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

## Example 2: Network Route to a Specific Service

This example describes the typical routing configuration for services outside the current instance. To configure a route to an external service, create the route in the database that begins the conversation. In this example, **AdventureWorks2008R2** contains a route for the service **OrderParts**. The route contains a network address for the **OrderParts** service.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   OrderPartsRoute

   :::column-end:::
   :::column span="1":::
   OrderParts

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   TCP://host2.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

In this case, all dialogs created in the **AdventureWorks2008R2** database to the service **OrderParts** match the route **OrderPartsRoute**. Service Broker sends these messages to the network address **TCP://host2.Adventure-Works.com:4022/**. All other conversations are delivered to services in the same instance.

For conversations created in **AdventureWorks2008R2** with a target service of **OrderParts**, the set of matching routes contains **OrderPartsRoute**, since this route exactly matches the service name. **OrderPartsRoute** is the only route in the set of matching routes, so Service Broker chooses that route.

For conversations created in **AdventureWorks2008R2** to a different target service, the set of matching routes contains **AutoCreatedLocal**. Since this is the only route in the set of matching routes, Service Broker chooses that route. If the service for the message does not exist in the local instance, Service Broker marks the conversation DELAYED.

For conversations that arrive from outside of the instance, **AutoCreatedLocal** is the only route in **msdb.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

## Example 3: Network Route to a Mirrored Database

This example describes the typical routing configuration for a service hosted by a mirrored database outside the current instance. To configure a route to an external service, create the route in the database that begins the conversation. In this example, **AdventureWorks2008R2** contains a route for the service **OrderParts**. The route contains both a network address and a mirror address for the **OrderParts** service.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   OrderPartsRoute

   :::column-end:::
   :::column span="1":::
   OrderParts

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   TCP://partner1.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   TCP://partner2.Adventure-Works.com:4022/

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

For conversations created in **AdventureWorks2008R2** with a target service of **OrderParts**, the set of matching routes contains **OrderPartsRoute**, since this route exactly matches the service name. **OrderPartsRoute** is the only route in the set of matching routes, so Service Broker chooses that route. Service Broker checks both the address and the mirror address to determine which partner is the principal, and then sends the message to the principal.

For conversations created in **AdventureWorks2008R2** to a different target service, the set of matching routes contains **AutoCreatedLocal**. Since this is the only route in the set of matching routes, Service Broker chooses that route. If the service for the message does not exist in the local instance, Service Broker marks the conversation DELAYED.

For conversations that arrive from outside of the instance, **AutoCreatedLocal** is the only route in **msdb.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

## Example 4: Network Route to All External Services

This example sends messages from services in **AdventureWorks2008R2** to a different instance unless the service exists in the local instance. Notice that messages for any services that are not in the local instance go to the same network address. This configuration may be useful if the SQL Server instance at that network address performs message forwarding.

In this example, the **AdventureWorks2008R2** database contains the **AutoCreatedLocal** route as well as a route to the address **TCP://forwarding.Adventure-Works.com:4022/**.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   ExternalRoute

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   TCP://forwarding.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

For conversations created in **AdventureWorks2008R2**, the set of matching routes contains both **AutoCreatedLocal** and **ExternalRoute**, since the remote service name and broker instance is the same for both routes. Service Broker must choose between these two routes. Service Broker chooses routes with the address **'LOCAL'** before routes that specify a network address, so Service Broker first chooses **AutoCreatedLocal**. If the target service exists in the local instance, Service Broker uses this route and delivers the message to that service. However, if the target service does not exist in the local instance, Service Broker chooses **ExternalRoute**.

For conversations that arrive from outside of the instance, **AutoCreatedLocal** is the only route in **msdb.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

## Example 5: Network Route to Different Instances of a Service

This example shows a routing configuration where two different network addresses host different instances of the same service. This configuration may be useful for a load balancing configuration.

In this example, the **AdventureWorks2008R2** database contains the **AutoCreatedLocal** route as well as routes to the service **BalancedService**.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   BalancedRouteOne

   :::column-end:::
   :::column span="1":::
   BalancedService

   :::column-end:::
   :::column span="1":::
   5fb8d92b-ed69-4c80-afbb-2aa6a7d3cb2d

   :::column-end:::
   :::column span="1":::
   TCP://server1.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   BalancedRouteTwo

   :::column-end:::
   :::column span="1":::
   BalancedService

   :::column-end:::
   :::column span="1":::
   81b1d3d0-288e-4d2c-b1d3-456cbb944b4f

   :::column-end:::
   :::column span="1":::
   TCP://server2.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

For conversations created in the **AdventureWorks2008R2** database to the service **BalancedService** that do not specify a Service Broker identifier, the set of matching routes contains either **BalancedRouteOne** and **BalancedRouteTwo**. Since the routes contain different Service Broker identifiers, the matching process arbitrarily selects a Service Broker identifier and matches that route. Since only one route matches, Service Broker chooses that route for the conversation. The result is that some conversations route to **TCP://server1.Adventure-Works.com:4022/**, and other conversations route to **TCP://server2.Adventure-Works.com:4022/**. However, once Service Broker receives an acknowledgment for a message in a conversation, Service Broker uses the Service Broker identifier contained in the acknowledgment for other messages in the conversation. Once the first acknowledgment is received, all future messages on the conversation are routed using the Service Broker identifier in the acknowledgment.

For conversations created in the **AdventureWorks2008R2** database to the service **BalancedService** that specify one of the Service Broker identifiers in the routing table, the set of matching routes contains the route that matches the Service Broker identifier. The conversation routes to the address in the route with that Service Broker identifier.

For conversations created in **AdventureWorks2008R2** to a different target service, the set of matching routes contains only **AutoCreatedLocal**. Service Broker chooses that route. If the service for the message does not exist in the local instance, Service Broker marks the conversation DELAYED.

For conversations that arrive from outside of the instance, **AutoCreatedLocal** is the only route in **msdb.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

## Example 6: Message Forwarding for a Specific Service

This example forward messages from outside the local instance to the service **ElsewhereService** to the network address **TCP://elsewhere.Adventure-Works.com:4022/**. For all other services, Service Broker delivers the messages to a service in the local instance or marks the conversation DELAYED if the service does not exist in the local instance.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   ForwardingRoute

   :::column-end:::
   :::column span="1":::
   ElsewhereService

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   TCP://elsewhere.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

For conversations created in **AdventureWorks2008R2**, **AutoCreatedLocal** is the only route in **AdventureWorks2008R2.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, Service Broker marks the conversation DELAYED. Notice that a conversation created in **AdventureWorks2008R2** to the service **ElsewhereService** does not route to **TCP://elsewhere.Adventure-Works.com:4022/**.

For conversations that arrive from outside of the instance to the service **ElsewhereService**, the route **ForwardingRoute** exactly matches the service name. Therefore, **ForwardingRoute** is the only route in the set of matching routes, and Service Broker chooses that route when message forwarding is on. Service Broker chooses this route even if the local instance contains the service **ElsewhereService**. If message forwarding is off, Service Broker drops the message.

For conversations that arrive from outside of the instance to all other services, **AutoCreatedLocal** is the only matching route in **msdb.sys.routes**. Service Broker chooses that route. If the service for the message does not exist in the local instance, the message is dropped.

## Example 7: Message Forwarding for All Services Not in the Instance

This example sends messages from outside the local instance to a different instance unless the service exists in the local instance. Notice that messages for all external services go to the same network address. This configuration may be useful for message forwarding.

**AdventureWorks2008R2.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

**msdb.sys.routes**

:::row:::
   :::column span="1":::
   **name**
   :::column-end:::
   :::column span="1":::
   **remote_service_name**
   :::column-end:::
   :::column span="1":::
   **broker_instance**
   :::column-end:::
   :::column span="1":::
   **address**
   :::column-end:::
   :::column span="1":::
   **mirror_address**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   AutoCreatedLocal

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   LOCAL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   ForwardingRoute

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
   :::column span="1":::
   TCP://forwarding.Adventure-Works.com:4022/

   :::column-end:::
   :::column span="1":::
   NULL

   :::column-end:::
:::row-end:::

For conversations created in **AdventureWorks2008R2**, **AutoCreatedLocal** is the only route in **AdventureWorks2008R2.sys.routes**. That route is the only route in the set of matching routes, and Service Broker chooses that route. If the service for the message does not exist in the local instance, Service Broker marks the conversation DELAYED.

For conversations that arrive from outside the database, the set of matching routes contains both **AutoCreatedLocal** and **ForwardingRoute**, since both routes specify the same remote service name and Service Broker identifier. Service Broker must choose between these two routes. Service Broker chooses routes with the address **'LOCAL'** before routes that specify a network address, so Service Broker first chooses **AutoCreatedLocal**. If the target service exists in the local instance, Service Broker uses this route and delivers the message to that service. However, if the target service does not exist in the local instance, and message forwarding is on, Service Broker chooses **ForwardingRoute**. If message forwarding is not on, Service Broker drops the message if the target service does not exist in the local instance.

## See also

- [CREATE ROUTE (Transact-SQL)](../../t-sql/statements/create-route-transact-sql.md)
- [sys.routes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md)
- [Routes](routes.md)
- [Service Broker Routing](service-broker-routing.md)
