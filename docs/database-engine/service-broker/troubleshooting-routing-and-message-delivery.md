---
title: Troubleshoot Routing and Message Delivery
description: "This section provides suggestions to correct common problems related to routing and message delivery."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/03/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
---

# Routing and message delivery troubleshooting

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This section provides suggestions to correct common problems related to routing and message delivery.

## Technique: Diagnosing message delivery

If messages aren't successfully delivered between two services, use the **ssbdiagnose** utility to generate a runtime report of a conversation. The runtime report displays any errors encountered by the conversation operations. If errors are encountered, **ssbdiagnose** also analyzes the configuration between the services and report any configuration problems it finds. For more information, see [ssbdiagnose utility (Service Broker)](../../tools/ssbdiagnose/ssbdiagnose-utility-service-broker.md).

## Problem: Messages remain in the transmission queue

Ensure that Service Broker message delivery is activated in the database. The `is_broker_enabled` column of `sys.databases` shows whether broker message delivery is activated, as shown in the following sample:

```sql
SELECT is_broker_enabled
FROM sys.databases
WHERE database_id = DB_ID();
```

You can deactivate Broker message delivery to prevent messages from being delivered to the wrong database. For more information about Service Broker message delivery, see [Manage Service Broker identities](managing-service-broker-identities.md). For more information about how to activate Service Broker message delivery, see [How to: Activate Service Broker message delivery in databases](how-to-activate-service-broker-message-delivery-in-databases-transact-sql.md).

If Service Broker message delivery is active, check the `transmission_status` column in the `sys.transmission_queue` catalog view for the messages. Common error messages include:

| Message | Description |
| --- | --- |
| No route for service. | Service Broker couldn't locate a route to the service specified. |
| The target Service Broker is unreachable. | Service Broker couldn't deliver the message to the target Service Broker. |
| Transport layer is unavailable. | No Service Broker endpoint exists in the instance, or the Service Broker endpoint didn't start successfully. |
| Target queue is disabled. | The queue that the destination service uses has the `STATUS` option set to `OFF`. Service Broker doesn't add new messages to a queue with a `STATUS` of `OFF`. |
| An error occurred while receiving data: '10054(An existing connection was forcibly closed by the remote host.)'. | The remote side of the conversation accepted the TCP/IP connection, but closed the connection before a message could be sent. |
| (None) | Service Broker hasn't yet tried to send the message. If the `enqueue_time` column shows the message has been in the queue a long time, Service Broker message delivery might not be activated in the database. |

## Problem: Route exists, transmission status shows no route for service

The most common causes for this problem are as follows:

- The `SEND` statement created the message when no route existed to successfully deliver the message.
- The route was then created, but Service Broker hasn't yet tried to resend the message.

For more information about retries, see [Service Broker routing and networking](service-broker-routing-and-networking.md).

Ensure that the service name specified in the message exactly matches the service name specified in the route. Service Broker uses a byte-by-byte binary comparison to match service names. If a route that specifies the service name exists, you can compare the names by running the following query:

```sql
SELECT tq.to_service_name AS N'No Exact Match'
FROM sys.transmission_queue AS tq
WHERE NOT EXISTS (SELECT remote_service_name
                  FROM sys.routes AS routes
                  WHERE tq.to_service_name = routes.remote_service_name);
```

> [!NOTE]  
> Some service names might appear in the result set even though they match a route. A route that doesn't specify a service name (`remote_service_name = NULL`) matches the service name used with any message. For more information about Service Broker routes, see [Service Broker routing](service-broker-routing.md).

If the message specifies a broker instance identifier, verify either that the route specifies the same broker instance identifier, or that the route doesn't specify a broker instance identifier at all.

Check that the route hasn't expired. The lifetime column of the `sys.routes` catalog view contains the expiration date and time for the route.

## Problem: Transmission status shows target service broker unreachable

The destination didn't accept the message. This can indicate that the service name specified doesn't match the name of a service that the destination SQL Server instance hosts. It might also indicate that the destination doesn't contain a route for the service. To troubleshoot this problem, check the routing and service configuration for the destination.

## Problem: Transmission status shows transport layer unavailable

Verify that a Service Broker endpoint exists. If no endpoint exists, create one. If an endpoint does exist, verify that the state of the endpoint is `STARTED`. For more information, see [Service Broker endpoints](service-broker-endpoints.md). For more information about how to create an endpoint, see [How to: Activate Service Broker networking](how-to-activate-service-broker-networking-transact-sql.md).

## Problem: Transmission status shows "An existing connection was forcibly closed by the remote host"

Transport security might be incorrectly configured, or the TCP/IP address for the route specifies a port that is being used by a service other than Service Broker.

> [!NOTE]  
> The port specified in the route must correspond to the port the Service Broker endpoint uses on the remote instance of the Database Engine. Service Broker uses the Service Broker communications protocols to transmit messages, not the Tabular Data Stream protocol that's used to transmit Transact-SQL batches and results. Therefore, the port that a Service Broker endpoint uses differs from the port that's used to transmit Transact-SQL. Check the Service Broker endpoint configuration to ensure that the two instances have compatible network security settings. If the Service Broker endpoint for one instance specifies `REQUIRED` or `ENABLED`, the Service Broker endpoint for the other instance can't specify `NONE`.

Check certificates, users, and permissions for Service Broker transport security. For more information, see [Service Broker transport security](service-broker-transport-security.md).

## Related content

- [How to: Activate Service Broker networking (Transact-SQL)](how-to-activate-service-broker-networking-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.routes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-routes-transact-sql.md)
- [sys.service_broker_endpoints (Transact-SQL)](../../relational-databases/system-catalog-views/sys-service-broker-endpoints-transact-sql.md)
- [Service Broker routing and networking](service-broker-routing-and-networking.md)
- [Service Broker endpoints](service-broker-endpoints.md)
- [Service Broker routing](service-broker-routing.md)
- [Start and stop the Service Broker queue](starting-and-stopping-the-queue.md)
