---
title: Deprecation of AMQP Protocol for Change Event Streaming
description: Learn about the deprecation of the AMQP protocol for change event streaming and how to migrate stream groups to the Kafka protocol.
author: nzagorac-ms
ms.author: nzagorac
ms.reviewer: mathoma
ms.date: 08/15/2026
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# Deprecation of AMQP protocol for change event streaming

This article describes the deprecation of the AMQP protocol for the [change event streaming (CES)](overview.md) feature in SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

The deprecation of the AMQP protocol introduces a breaking change to the `destination_type` configuration value when creating new stream groups. The rollout schedule differs by product. This article explains how these changes affect new and existing stream groups, the platform-specific differences, and how to migrate existing stream groups from the AMQP protocol to the Kafka protocol.

## What is AMQP protocol?

The AMQP protocol ("AMQP") was one of two protocols that CES supported when sending change events to Azure Event Hubs and Fabric Eventstream. It was a service-to-service implementation that published events to Azure Event Hubs and Fabric Eventstream.

Before this breaking change, you specified the protocol in the `destination_type` configuration value when [configuring](configure.md) a stream group, using one of the following supported values:

- `AzureEventHubsAMQP` - AMQP protocol
- `AzureEventHubsApacheKafka` - Kafka protocol

After this breaking change, the only supported `destination_type` value for newly created stream groups on Azure SQL Database and SQL database in Microsoft Fabric is `AzureEventHubs`, which uses the **Kafka** protocol. Azure SQL Managed Instance and SQL Server 2025 still accept `AzureEventHubsAMQP` and `AzureEventHubsApacheKafka`.

As of August 15, 2026, **newly created** stream groups on Azure SQL Database must use `AzureEventHubs` as the `destination_type`. Attempts to create a stream group using previous values fail. 

The consumers of messages published to Azure Event Hubs can choose between the AMQP or Kafka protocol regardless of the protocol that publishes the message.

## Platform support for destination_type values

The allowed `destination_type` values when creating a new stream group depend on the product and version:

| Product | Allowed `destination_type` values | Notes |
|---|---|---|
| **Azure SQL Database** | `AzureEventHubs` | The only supported value. `AzureEventHubsAMQP` and `AzureEventHubsApacheKafka` aren't accepted for new stream groups. |
| **Azure SQL Managed Instance** | `AzureEventHubsAMQP`, `AzureEventHubsApacheKafka` | These values are still accepted but are deprecated. Avoid `AzureEventHubsAMQP` for new stream groups.  |
| **SQL Server 2025** | `AzureEventHubsAMQP`, `AzureEventHubsApacheKafka` | These values are still accepted but are deprecated. Avoid `AzureEventHubsAMQP` for new stream groups. |
| **SQL database in Microsoft Fabric** | `AzureEventHubs` | The only supported value.|

For new stream groups, use `AzureEventHubs` when the platform supports it. For Azure SQL Managed Instance and SQL Server 2025, use `AzureEventHubsApacheKafka` (Kafka protocol). Avoid `AzureEventHubsAMQP` for new stream groups.

## How to migrate AMQP-configured stream groups to Kafka

Existing CES groups configured with `AzureEventHubsAMQP` continue to publish messages by using the AMQP protocol until April 2027. Recreate your existing AMQP-configured stream groups in SQL Server 2025, Azure SQL Database, Azure SQL Managed Instance, or SQL database in Microsoft Fabric by using `AzureEventHubs` as the `destination_type` as soon as possible.

Use the following query to identify all configured stream groups:

```sql
exec sp_help_change_event_stream_groups
```

Save the configuration values for the stream groups that have `AzureEventHubsAMQP` in the **streaming_dest_type** column. You need these values when creating the new stream groups.

Use the following query to identify the tables in each stream group that you need to migrate: 

```sql
exec sys.sp_help_change_event_stream_tables
```

To migrate an AMQP-configured stream group to Kafka, follow these steps:

1. Create a replacement stream by using the [sys.sp_create_event_stream_group](../../system-stored-procedures/sys-sp-create-event-stream-group-transact-sql.md) stored procedure. Specify `AzureEventHubs` as the `destination_type` parameter.

   The `destination_location` parameter expects port **9093**, such as the following example: `myEventHubsNamespace.servicebus.windows.net:9093/myEventHubsInstance`. 

   The Kafka protocol supports Microsoft Entra or service key authentication. SAS authentication is now unavailable for CES.

1. Remove each table, one by one, from the old stream group by using [sys.sp_remove_object_from_event_stream_group](../../system-stored-procedures/sys-sp-remove-object-from-event-stream-group-transact-sql.md): 

   ```sql
   exec sys.sp_remove_object_from_event_stream_group @stream_group_name = '<old_stream_group_name>', @object_name = '<schema.table_name>'
   ```

1. Add each table you removed from the old stream group to the new replacement stream group by using [sys.sp_add_object_to_event_stream_group](../../system-stored-procedures/sys-sp-add-object-to-event-stream-group-transact-sql.md):

   ```sql
   exec sys.sp_add_object_to_event_stream_group @stream_group_name = '<new_stream_group_name>', @object_name = '<schema.table_name>'
   ```

1. Once you add all tables to the new stream group, use [sys.sp_drop_event_stream_group](../../system-stored-procedures/sys-sp-drop-event-stream-group-transact-sql.md) to remove the old stream group. For details, review [configure CES](configure.md).

   ```sql
   exec sys.sp_drop_event_stream_group @stream_group_name = '<old_stream_group_name>'
   ```

1. Verify the new stream group is active by running `sp_help_change_event_stream_groups` and confirming it shows `AzureEventHubs` as the **streaming_dest_type**.

Repeat this process for every AMQP-configured stream group in your environment.

## Moving tables to new stream groups (Kafka)

CES scans and publishes changes from the log file the moment they are created but publishing latency can cause events to lag.  When you remove a table from a stream group, pending changes in the log file don't get published. As such, carefully coordinate moving tables between stream groups to avoid missing events.

Before removing a table from a stream group, check the [sys.dm_change_feed_log_scan_sessions](../../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md) DMV to ensure there are no pending changes to publish on the tables. If there are pending changes, wait until the process publishes them before removing the table from the old stream group and adding it to the new stream group.

To mitigate the risk of missing events, use one of the following approaches:

- Move the tables between stream groups during a dedicated maintenance window when no writes are happening to the tables.
- Place an exclusive lock on the tables that are being streamed during a period of low activity to prevent new writes to those tables. Only release the exclusive lock after the tables are in the new stream group.

## Deprecation timelines

The deprecation of the AMQP protocol follows these two timelines:

| Stream group type | Effective date | Impact |
| --- | --- | --- |
| **New stream groups on Azure SQL Database and SQL database in Microsoft Fabric** | August 15, 2026 | Newly created stream groups must specify `AzureEventHubs` as the `destination_type`. Attempts to use either `AzureEventHubsAMQP` or `AzureEventHubsApacheKafka` fail. |
| **New stream groups on Azure SQL Managed Instance and SQL Server 2025** | Future update (TBD) | `AzureEventHubsAMQP` and `AzureEventHubsApacheKafka` are still accepted but deprecated. A future update adds `AzureEventHubs` as the required value. |
| **Existing stream groups using the AMQP protocol** | April 2027 | Stream groups already configured with `AzureEventHubsAMQP` continue to work normally until this date. They must be migrated to use the Kafka protocol before support for the AMQP protocol is removed. |

Consumers of published messages don't need to make any changes.

## Deprecation impact

This section describes the impact of AMQP protocol deprecation, such as:

- [Impact on consumers of published messages](#impact-on-consumers-of-published-messages)
- [Impact on existing AMQP stream groups](#impact-on-existing-amqp-stream-groups)
- [Impact on newly created stream groups](#impact-on-newly-created-stream-groups)
- [Impact on publisher network configuration](#impact-on-publisher-network-configuration)

### Impact on consumers of published messages

The protocol used to write to the destination (either Azure Event Hubs or Fabric Eventstream) is independent from the protocol used by message consumers. Therefore, switching from AMQP to Kafka protocol for message publishing doesn't impact message consumers. They can continue consuming messages using either protocol.

### Impact on existing AMQP stream groups

Existing stream groups configured with `AzureEventHubsAMQP` **continue working as-is** by using the AMQP protocol. There's no immediate interruption or configuration change required for these groups. However, you must migrate AMQP-configured stream groups to use the Kafka protocol before support for the AMQP protocol is removed in April 2027.

### Impact on newly created stream groups

Starting August 15, 2026, the only allowed `destination_type` value for newly created stream groups on Azure SQL Database and SQL database in Microsoft Fabric is `AzureEventHubs`. Attempts to create a stream group by using either previous value, `AzureEventHubsAMQP` or `AzureEventHubsApacheKafka`, fail with the following error message:

```text
Msg 23626, Level 16, State 2, Line 481, An error occurred. The error/state returned was 23618/5: 'The value provided for the argument '@destination_type' is invalid. Allowed values: AzureEventHubs.'
```

Update any automation scripts that create stream groups before this date. The `destination_type` parameter must use `AzureEventHubs` instead of `AzureEventHubsAMQP` or `AzureEventHubsApacheKafka`.

The Kafka protocol uses Microsoft Entra or service key authentication to connect to Azure Event Hubs or Fabric Eventstream. If your existing AMQP stream groups use SAS authentication, you need to switch to either Microsoft Entra or service key authentication when creating new stream groups. See [Configure change event streaming](configure.md) for details on how to configure authentication for stream groups.

### Impact on publisher network configuration

Depending on how you configure the network on the publishing side, you might need to reconfigure allowed outbound ports to use Kafka's port **9093**, instead of the **5671** and **5672** ports used by AMQP. The publisher must allow outbound traffic on port **9093**.

Only leave ports **5671** and **5672** open if you have existing stream groups that still use the AMQP protocol. These ports aren't required for newly created stream groups that use the Kafka protocol.

For more information, see [Azure Event Hubs firewall configuration per protocol](/azure/event-hubs/event-hubs-faq#what-ports-do-i-need-to-open-on-the-firewall).

## Related content

- [What is change event streaming (preview)?](overview.md)
- [Configure change event streaming (preview) to Azure Event Hubs](configure.md)
- [JSON message format - change event streaming](message-format.md)
- [Change event streaming FAQ](frequently-asked-questions-faq.yml)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [Azure Event Hubs firewall configuration per protocol](/azure/event-hubs/event-hubs-faq#what-ports-do-i-need-to-open-on-the-firewall)
