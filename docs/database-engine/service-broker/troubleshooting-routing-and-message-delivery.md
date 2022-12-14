---
title: Troubleshooting Routing and Message Delivery
description: "This section provides suggestions to correct common problems related to routing and message delivery."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Troubleshooting Routing and Message Delivery

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This section provides suggestions to correct common problems related to routing and message delivery.

## Technique: Diagnosing Message Delivery

If messages are not successfully delivered between two services, use the **ssbdiagnose** utility to generate a runtime report of a conversation. The runtime report will display any errors encountered by the conversation operations. If errors are encountered, **ssbdiagnose** will also analyze the configuration between the services and report any configuration problems it finds. For more information, see [Ssbdiagnose Utility](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md).

## Problem: Messages Remain in the Transmission Queue

Ensure that Service Broker message delivery is activated in the database. The **is_broker_enabled** column of **sys.databases** shows whether broker message delivery is activated, as shown in the following sample:

```sql
    SELECT is_broker_enabled FROM sys.databases
    WHERE database_id = DB_ID() ;
```

Broker message delivery can be deactivated to prevent messages from being delivered to the wrong database. For more information about Service Broker message delivery, see [Managing Service Broker Identities](managing-service-broker-identities.md). For more information about how to activate Service Broker message delivery, see [How to: Activate Service Broker Message Delivery in Databases (Transact-SQL)](how-to-activate-service-broker-message-delivery-in-databases-transact-sql.md).

If Service Broker message delivery is active, check the **transmission_status** column in the **sys.transmission_queue** catalog view for the messages. Common error messages include the following:

:::row:::
   :::column span="1":::
   **Message**
   :::column-end:::
   :::column span="1":::
   **Description**
   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   No route for service.

   :::column-end:::
   :::column span="1":::

   Service Broker couldn't locate a route to the service specified.

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   The target Service Broker is unreachable.

   :::column-end:::
   :::column span="1":::
   Service Broker could not deliver the message to the target Service Broker.

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Transport layer is unavailable.

   :::column-end:::
   :::column span="1":::
   No Service Broker endpoint exists in the instance, or the Service Broker endpoint did not start successfully.

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   Target queue is disabled.

   :::column-end:::
   :::column span="1":::
   The queue that the destination service uses has the STATUS option set to OFF. Service Broker does not add new messages to a queue with a STATUS of OFF.

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   An error occurred while receiving data: '10054(An existing connection was forcibly closed by the remote host.)'.

   :::column-end:::
   :::column span="1":::
   The remote side of the conversation accepted the TCP/IP connection, but closed the connection before a message could be sent.

   :::column-end:::
:::row-end:::

:::row:::
   :::column span="1":::
   (None)

   :::column-end:::
   :::column span="1":::
   Service Broker has not yet tried to send the message. If the **enqueue_time** column shows the message has been in the queue a long time, Service Broker message delivery might not be activated in the database.

   :::column-end:::
:::row-end:::

## Problem: Route Exists, Transmission Status Shows No Route For Service

The most common causes for this problem are the following:

- The SEND statement created the message when no route existed to successfully deliver the message.

- The route was then created, but Service Broker has not yet tried to resend the message.

For more information about retries, see [Service Broker Routing and Networking](service-broker-routing-and-networking.md).

Ensure that the service name specified in the message exactly matches the service name specified in the route. Service Broker uses a byte-by-byte binary comparison to match service names. If a route that specifies the service name exists, you can compare the names by running the following query:

```sql
    SELECT N'No Exact Match' = tq.to_service_name
    FROM sys.transmission_queue AS tq
    WHERE NOT EXISTS
        (SELECT remote_service_name
         FROM sys.routes AS routes
         WHERE tq.to_service_name = routes.remote_service_name) ;
```

> [!NOTE]
> Some service names might appear in the result set even though they do match a route. A route that does not specify a service name (remote_service_name = NULL) will match the service name used with any message.For more information about Service Broker routes, see [Service Broker Routing](service-broker-routing.md).

If the message specifies a broker instance identifier, verify either that the route specifies the same broker instance identifier, or that the route does not specify a broker instance identifier at all.

Check that the route has not expired. The lifetime column of the **sys.routes** catalog view contains the expiration date and time for the route.

## Problem: Transmission Status Shows Target Service Broker Unreachable

The destination did not accept the message. This can indicate that the service name specified does not match the name of a service that the destination SQL Server instance hosts. It might also indicate that the destination does not contain a route for the service. To troubleshoot this problem, check the routing and service configuration for the destination.

## Problem: Transmission Status Shows Transport Layer Unavailable

Verify that a Service Broker endpoint exists. If no endpoint exists, create one. If an endpoint does exist, verify that the state of the endpoint is STARTED. For more information, see [Service Broker Endpoints](service-broker-endpoints.md). For more information about how to create an endpoint, see [How to: Activate Service Broker Networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md).

## Problem: Transmission Status Shows "An Existing Connection Was Forcibly Closed By the Remote Host"

Transport security might be incorrectly configured, or the TCP/IP address for the route specifies a port that is being used by a service other than Service Broker.

> [!NOTE]
> The port specified in the route must correspond to the port that is used by the Service Broker endpoint on the remote instance of the Database Engine. Service Broker uses the Service Broker communications protocols to transmit messages, not the Tabular Data Stream protocol that is used to transmit Transact-SQL batches and results. Therefore, the port that is used by a Service Broker endpoint differs from the port that is used to transmit Transact-SQL.Check the Service Broker endpoint configuration to ensure that the two instances have compatible network security settings. If the Service Broker endpoint for one instance specifies REQUIRED or ENABLED, the Service Broker endpoint for the other instance cannot specify NONE.

Check certificates, users, and permissions for Service Broker transport security. For more information, see [Service Broker Transport Security](service-broker-transport-security.md).

## See also

- [How to: Activate Service Broker Networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)[sys.routes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md)
- [sys.service_broker_endpoints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-service-broker-endpoints-transact-sql.md)
- [Service Broker Routing and Networking](service-broker-routing-and-networking.md)
- [Service Broker Endpoints](service-broker-endpoints.md)
- [Service Broker Routing](service-broker-routing.md)
- [Starting and Stopping the Queue](starting-and-stopping-the-queue.md)
