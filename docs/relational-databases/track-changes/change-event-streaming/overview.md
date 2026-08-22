---
title: "What is change event streaming (preview)"
description: "Provides an overview of change event streaming"
author: nzagorac-ms
ms.author: nzagorac
ms.reviewer: mathoma
ms.date: 07/29/2026
ms.service: sql
ms.topic: overview
ms.custom:
  - ignite-2025
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# What is change event streaming (preview)?
[!INCLUDE [sql25-sqldb-sqlmi-sqldbfabric](../../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

This article describes the change event streaming (CES) feature in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

To get started with the feature, see [Configure change event streaming](configure.md).

[!INCLUDE [change-event-streaming-preview](../../../includes/change-event-streaming-preview.md)]


## Overview 

CES is a modern data integration capability that streams SQL data changes directly into [Azure Event Hubs](/azure/event-hubs/event-hubs-about) or [Fabric Eventstream](/fabric/real-time-intelligence/event-streams/overview), which are high throughput data streaming services. CES captures and publishes incremental changes of data to the destination in near real-time. Captured changes include updates, inserts, and deletes (DML). Details of the data changes, such as the current schema, previous values, and new values, are sent to Azure Event Hubs or Fabric Eventstream in the form of a CloudEvent. The CloudEvent is serialized to JSON (native) or Avro Binary, and then streamed into an Azure Event Hubs or Fabric Eventstream destination.

[!INCLUDE [change-event-streaming-amqp-deprecation](../../../includes/change-event-streaming-amqp-deprecation.md)]

## Use cases

Use CES to:

- Build event-driven systems on top of your relational databases, with minimal overhead and easy data integration.
- Synchronize data across systems. More specifically, synchronize data between microservices or keep distributed systems synchronized.
- Implement real-time analytics on top of your relational data.
- Audit and monitor. Track changes of sensitive data or logging specific events.

The main advantages of using an event streaming service such as Azure Event Hubs with SQL Server's change event streaming are:

- **Scalability**:  Event streaming services are designed to handle high-throughput and can scale independently from a database.
- **Decoupling**: Systems downstream from a database and streaming service are loosely coupled, enabling greater flexibility and easier maintenance.
- **Multiconsumer support**:  Azure Event Hubs and Fabric Eventstream allow multiple consumers to process the same data stream, enabling varied use cases from a single source.
- **Real-time integration**: Enables seamless integration between OLTP systems and downstream systems for real-time data flow.

## Use change event streaming

To use CES, create a stream group that defines what tables you want to track, and how to access the streaming destination. The stream group designates the endpoint, provides authentication details, defines partitioning (if any), and determines which tables to track. After you configure CES, all data changes made by `INSERT`, `UPDATE`, and `DELETE` commands within the tables in the stream group are streamed as CloudEvents to the streaming destination.

In the context of CES, an object is the table that you're tracking. A stream group defines all the objects (that is, tables) that are tracked.

To get started with the feature, see [Configure change event streaming](configure.md). For frequently asked questions, see [Change event streaming FAQ](frequently-asked-questions-faq.yml).

## Consume CES events

You can consume CES events from Azure Event Hubs or Fabric Eventstream.

### Consume CES events from Azure Event Hubs

To learn how to consume change events from Azure Event Hubs with a .NET Core console application that receives events from an event hub using an event processor, review [Quickstart: Send or receive events using .NET](/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send#receive-events-from-the-event-hub).

### Consume CES events from Fabric Eventstream

If your SQL Database Engine is publishing to Fabric Eventstream, you can process this data in multiple ways in Fabric. To learn how to consume change events from Fabric Eventstream by using the SQL operator, see [Stream to Fabric Eventstream](/fabric/real-time-intelligence/event-streams/stream-sql-change-events-to-eventstream).

## Platform supportability

Change event streaming is currently in preview for the following products:
- SQL Server 2025
- Azure SQL Database
- Azure SQL Managed Instance
- SQL database in Microsoft Fabric

The following sections detail supportability differences between products:

### [SQL Server 2025](#tab/asql-2025)

To use CES in SQL Server 2025, consider the following: 
- Enable the [preview feature database scoped configuration](../../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).
- Microsoft Entra authentication is supported starting with Cumulative Update 3 (CU3) for instances [enabled by Azure Arc](../../../sql-server/azure-arc/connect.md) or running on an [Azure VM](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview).

### [Azure SQL Database](#tab/sql-db)

CES is available for all service tiers of Azure SQL Database (including Hyperscale). xEvent debugging isn't currently available in Azure SQL Database.

### [Azure SQL Managed Instance](#tab/sql-mi)

CES is available for all service tiers of Azure SQL Managed Instance. Your instance must be configured with the SQL Server 2025 or Always-up-to-date [update policy](/azure/azure-sql/managed-instance/update-policy). 

### [SQL database in Fabric](#tab/sql-db-fabric)

CES has the following limitations in SQL database in Fabric:
- xEvent debugging isn't currently available.
- Microsoft Entra authentication isn't currently available.

--- 

## Limitations

To learn more, review [limitations with the CES feature](configure.md#limitations).

## Related content

- [Configure change event streaming (preview) to Azure Event Hubs](configure.md)
- [CES frequently asked questions](frequently-asked-questions-faq.yml)
- [JSON message format - change event streaming](message-format.md)
