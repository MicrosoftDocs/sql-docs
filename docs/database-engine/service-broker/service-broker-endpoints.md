---
title: Service Broker Endpoints
description: "SQL Server uses Service Broker endpoints for Service Broker communication outside of the SQL Server instance."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray, maghan
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Service Broker Endpoints

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

SQL Server uses Service Broker endpoints for Service Broker communication outside of the SQL Server instance.

An endpoint is a SQL Server object that represents the capability for SQL Server to communicate over the network. Each endpoint supports a specific type of communication. For example, an HTTP endpoint lets SQL Server process specific SOAP requests. A Service Broker endpoint configures SQL Server to send and receive Service Broker messages over the network.

Service Broker endpoints provide options for transport security and message forwarding. A Service Broker endpoint listens on a specific TCP port number.

By default, an instance of SQL Server does not contain a Service Broker endpoint. Thus, Service Broker does not send or receive messages over the network by default. You must create a Service Broker endpoint to send or receive messages outside the SQL Server instance. For more information on creating Service Broker endpoints, see [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md). An instance may contain only one Service Broker endpoint.

> [!NOTE]
> When you create a Service Broker endpoint, SQL Server accepts TCP/IP connections on the port that is specified in the endpoint. Service Broker transport security requires authorization for connections to the port. If the computer on which SQL Server runs has a firewall enabled, the firewall configuration on the computer must allow both incoming and outgoing connections for the port that is specified in the endpoint. For more information on Service Broker transport security, see [Service Broker Transport Security](service-broker-transport-security.md).

## See also

- [CREATE ENDPOINT (Transact-SQL)](../../t-sql/statements/create-endpoint-transact-sql.md)
- [Service Broker Transport Security](service-broker-transport-security.md)
