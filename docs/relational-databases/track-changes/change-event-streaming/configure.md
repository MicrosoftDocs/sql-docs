---
title: Configure Change Event Streaming to Azure Event Hubs
description: Describes how to configure change event streaming.
author: nzagorac-ms
ms.author: nzagorac
ms.reviewer: mathoma, randolphwest
ms.date: 08/15/2026
ms.service: sql
ms.topic: how-to
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# Configure change event streaming (preview) to Azure Event Hubs

[!INCLUDE [sqlserver2025](../../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

This article describes how to configure the [change event streaming (CES)](overview.md) feature in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric to stream to Azure Event Hubs.

To configure CES to Fabric Eventstream, see [Stream to Fabric Eventstream](/fabric/real-time-intelligence/event-streams/stream-sql-change-events-to-eventstream).

[!INCLUDE [change-event-streaming-preview](../../../includes/change-event-streaming-preview.md)]

## Procedure for change event streaming

1. Use an existing or create a new [Azure Event Hubs](/azure/event-hubs/event-hubs-about) namespace and Event Hubs instance. The Event Hubs instance receives events.
1. Enable change event streaming for a user database.
1. Create a stream group. With this group, configure the destination, credentials, message size limits, and partitioning schema.
1. Add one or more tables to the stream group.

Each step is described in detail in the following sections of this article.

## Prerequisites

[!INCLUDE [change-event-streaming-amqp-deprecation](../../../includes/change-event-streaming-amqp-deprecation.md)]

To configure change event streaming, you need the following resources, permissions, and configuration:

- Azure Event Hubs namespace
- Azure Event Hubs instance
- Azure Event Hubs host name
- A login in the [db_owner](../../security/authentication-access/database-level-roles.md#fixed-database-roles) role or that has [CONTROL DATABASE](../../security/permissions-database-engine.md#permissions-database-engine) permission for the database where you intend to enable CES.
- For [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], enable the [preview feature database scoped configuration](../../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features). Azure SQL Database doesn't require this configuration.
- For Azure SQL Database configured to use [outbound firewall rules](/azure/azure-sql/database/outbound-firewall-rule-overview), and for Azure SQL Managed Instance [virtual network configuration](/azure/azure-sql/managed-instance/vnet-existing-add-subnet): [Firewall ports to open](/azure/event-hubs/event-hubs-faq#what-ports-do-i-need-to-open-on-the-firewall)
- For Azure SQL Database configured to use a [Network Security Perimeter](/azure/azure-sql/database/network-security-perimeter), allow access to the destination Azure Event Hubs:
  - [Firewall ports to open](/azure/event-hubs/event-hubs-faq#what-ports-do-i-need-to-open-on-the-firewall)
  - [Network Security Perimeter for Azure Event Hubs](/azure/event-hubs/network-security-perimeter).

When using change event streaming with Azure SQL Managed Instance, the instance must be configured with the SQL Server 2025 or Always-up-to-date [update policy](/azure/azure-sql/managed-instance/update-policy).

## Configure Azure Event Hubs

To learn how to create Azure Event Hubs, review [Create an event hub using the Azure portal](/azure/event-hubs/event-hubs-create).

## Azure Event Hubs access control

Configure access control for your SQL resource to Azure Event Hubs. Microsoft Entra authentication is the most secure method. CES supports Microsoft Entra authentication in Azure SQL Database and Azure SQL Managed Instance. For SQL Server 2025, Microsoft Entra authentication is supported starting with Cumulative Update 3 (CU3) for instances [enabled by Azure Arc](../../../sql-server/azure-arc/connect.md) or running on an [Azure VM](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview). While shared access policies are supported, use them only when Microsoft Entra authentication isn't an option.

### [Shared access policy based access control](#tab/sas-access-1)

[Shared access policies](/azure/event-hubs/authorize-access-shared-access-signature#shared-access-authorization-policies) provide authentication and authorization to Azure Event Hubs. Each shared access policy needs a name, an access level (`Manage`, `Send`, or `Listen`), and a resource binding (Event Hubs namespace or a specific Event Hub instance). Instance level policies offer more security by following the principle of least privilege. While SQL Database Engine products support shared access policies, use Microsoft Entra authentication whenever possible, as it provides better security.

If you use a shared access policy for authentication and authorization, clients sending data to an Event Hubs instance need to provide the name of the policy they want to use, along with the policy's **service key**.

To configure streaming to Azure Event Hubs, create or reuse a shared access policy with **Send** permission. You can authenticate using a **service key** (primary or secondary key value).

> [!NOTE]  
> For improved security, use Microsoft Entra based access control whenever possible. If Microsoft Entra based access control isn't possible and you're using shared access policies, the best practice is to rotate the service key periodically. Store all secrets securely by using Azure Key Vault or a similar service.

### Define a policy

You need a shared access policy with **Send** rights. You can either:

- Create a new policy

  Or

- Use an existing policy

> [!NOTE]  
> The policy must have **Send** rights.

Once the policy is determined, note the service key value. You use it, along with the policy name, when creating the credential in SQL before configuring CES.

### [Microsoft Entra based access control](#tab/entra-access-1)

Use Microsoft Entra [managed identities](/entra/identity/managed-identities-azure-resources/overview) to control access to Azure Event Hubs. It's the simplest and most secure way to grant access to Azure Event Hubs. CES supports Microsoft Entra authentication in Azure SQL Database and Azure SQL Managed Instance. For SQL Server 2025, Microsoft Entra authentication is supported starting with Cumulative Update 3 (CU3) for instances enabled by Azure Arc or running on an Azure VM. Microsoft Entra authentication isn't currently available for SQL database in Microsoft Fabric. 

> [!IMPORTANT]
> If your SQL Server instance isn't enabled by Azure Arc or running on an Azure VM, and you didn't install CU3 or later, Microsoft Entra authentication isn't available. You must use shared access policies instead.

To [allow managed identity write access](/azure/event-hubs/authenticate-managed-identity) to the Event Hubs instance from Azure SQL Database, Azure SQL Managed Instance, or SQL Server, follow these steps:

1. If you didn't already, configure a [managed identity](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity?view=azuresql-db&preserve-view=true) for your SQL Database Engine:
    1.  The [logical server](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity?view=azuresql-db&preserve-view=true) for Azure SQL Database
    1.  [Azure SQL Managed Instance](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity?view=azuresql-mi&preserve-view=true)
    1.  [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm?view=azuresql&preserve-view=true)
    1.  [SQL Server enabled by Azure Arc](../../../sql-server/azure-arc/microsoft-entra-authentication-with-managed-identity.md)


1. Add the `Azure Event Hubs Data Sender` role assignment to the managed identity of your logical server, SQL managed instance, or SQL Server for your Event Hubs instance. You can do this programmatically with any programming or scripting language, or on the **Access Control (IAM)** page for your Event Hubs instance in the Azure portal.

To follow the principle of least privilege, grant access to the specific Event Hubs instance that receives the change events. Granting write access to the entire Event Hubs namespace is technically allowed, but not recommended since it applies to any Event Hubs instance within the namespace.

---

## Enable and configure change event streaming

[!INCLUDE [change-event-streaming-amqp-deprecation](../../../includes/change-event-streaming-amqp-deprecation.md)]

To enable and configure change event streaming, change the database context to the user database and then follow these steps:

1. If it's not already configured, set the database to the [full recovery model](../../backup-restore/recovery-models-sql-server.md#recovery-model-overview).
1. Create a master key and a database scoped credential.
1. Enable event streaming.
1. Create the stream group.
1. Add one or more tables to the stream group.

The following examples demonstrate how to enable CES by platform:

- [Stream to Azure Event Hubs from Azure SQL Database](#example-stream-to-azure-event-hubs-from-azure-sql-database)
- [Stream to Azure Event Hubs from Azure SQL MI or SQL Server 2025](#example-stream-to-azure-event-hubs-from-azure-sql-mi-or-sql-server-2025)

The following table lists sample parameter values for the examples in this section:

| Parameter | Sample value | Notes |
| --- | --- | --- |
| `@stream_group_name` | `N'myStreamGroup'` | Name of the event stream group. |
| `@destination_location` | See Notes | The FQDN of the Azure Event Hubs namespace and instance name, including port 9093. Format: `<namespace>.servicebus.windows.net:9093/<instance>`. For Fabric Eventstream, use the [custom input endpoint](/fabric/real-time-intelligence/event-streams/stream-sql-change-events-to-eventstream#4-create-the-event-streaming-group). |
| `@partition_key_scheme` | `N'None'` | (Default) Partitions are chosen round robin. Other options are `StreamGroup`, `Table`, and `Column`. |
| `@max_message_size_kb` | `256` | 256 KB is the default maximum message size. Align this value with your destination limits. |

The examples also use the following values:

- *[optional, if shared access policies via service key are used]* Primary or secondary key value taken from the shared access policy: `Secret = 'BVFnT3baC/K6I8xNZzio4AeoFt6nHeK0i+ZErNGsxiw='`
- `EXEC sys.sp_add_object_to_event_stream_group N'myStreamGroup', N'dbo.myTable'`

### Example: Stream to Azure Event Hubs from Azure SQL Database

The following examples show how to stream change events to Azure Event Hubs from Azure SQL Database by using `AzureEventHubs` as the `destination_type`. This value is the only accepted value for Azure SQL Database and SQL database in Microsoft Fabric.



#### [Microsoft Entra authentication](#tab/entra-auth-1)

The example in this section uses Microsoft Entra authentication. This method is the most secure.

Replace the values in angle brackets (`<value>`) with values for your environment.

```sql
USE <database name>;

-- Create the Master Key with a password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Password>';

CREATE DATABASE SCOPED CREDENTIAL <CredentialName>
    WITH IDENTITY = 'Managed Identity'

EXEC sys.sp_enable_event_stream

EXEC sys.sp_create_event_stream_group
    @stream_group_name =      N'<EventStreamGroupName>',
    @destination_type =       N'AzureEventHubs',
    @destination_location =   N'<AzureEventHubsHostName>:9093/<EventHubsInstance>',
    @destination_credential = <CredentialName>,
    @max_message_size_kb =    <MaxMessageSize>,
    @partition_key_scheme =   N'<PartitionKeyScheme>'

EXEC sys.sp_add_object_to_event_stream_group
    N'<EventStreamGroupName>',
    N'<SchemaName>.<TableName>'
```

#### [Service key authentication](#tab/key-value-auth-1)

The example in this section uses a shared access policy key value. For improved security, use Microsoft Entra authentication whenever possible.

Replace the values in angle brackets (`<value>`) with values for your environment.

```sql
USE <database name>

-- Create the Master Key with a password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Password>'

CREATE DATABASE SCOPED CREDENTIAL <CredentialName>
    WITH IDENTITY = '<Azure Event Hubs SAS Policy name>',
    SECRET = '<Primary or Secondary key value>'

EXEC sys.sp_enable_event_stream

EXEC sys.sp_create_event_stream_group
    @stream_group_name =      N'<EventStreamGroupName>',
    @destination_type =       N'AzureEventHubs',
    @destination_location =   N'<AzureEventHubsHostName>:9093/<EventHubsInstance>',
    @destination_credential = <CredentialName>,
    @max_message_size_kb =    <MaxMessageSize>,
    @partition_key_scheme =   N'<PartitionKeyScheme>'

EXEC sys.sp_add_object_to_event_stream_group
    N'<EventStreamGroupName>',
    N'<SchemaName>.<TableName>'
```

---

### Example: Stream to Azure Event Hubs from Azure SQL MI or SQL Server 2025

The following examples show how to stream change events to Azure Event Hubs from Azure SQL Managed Instance or SQL Server 2025 by using `AzureEventHubsApacheKafka` as the `destination_type`. 

[!INCLUDE [change-event-streaming-amqp-deprecation](../../../includes/change-event-streaming-amqp-deprecation.md)]

#### [Microsoft Entra authentication](#tab/entra-auth-2)

The example in this section uses Microsoft Entra authentication to authenticate to your Azure Event Hubs instance through the Apache Kafka protocol. This method is the most secure.

Replace the values in angle brackets (`<value>`) with values for your environment.

```sql
USE <database name>

-- Create the Master Key with a password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Password>'

CREATE DATABASE SCOPED CREDENTIAL <CredentialName>
    WITH IDENTITY = 'Managed Identity'

EXEC sys.sp_enable_event_stream

EXEC sys.sp_create_event_stream_group
    @stream_group_name =      N'<EventStreamGroupName>',
    @destination_type =       N'AzureEventHubsApacheKafka',
    @destination_location =   N'<AzureEventHubsHostName>:<port>/<EventHubsInstance>', -- myEventHubsNamespace.servicebus.windows.net:9093/myEventHubsInstance
    @destination_credential = <CredentialName>,
    @max_message_size_kb =    <MaxMessageSize>,
    @partition_key_scheme =   N'<PartitionKeyScheme>'

EXEC sys.sp_add_object_to_event_stream_group
    N'<EventStreamGroupName>',
    N'<SchemaName>.<TableName>'
```

#### [Service key authentication](#tab/key-value-auth-2)

The example in this section uses a Shared Access policy key value to authenticate to your Azure Event Hubs instance through the Apache Kafka protocol. For improved security, use Microsoft Entra authentication whenever possible.

Replace the values in angle brackets (`<value>`) with values for your environment.

```sql
USE <database name>

-- Create the Master Key with a password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<Password>'

CREATE DATABASE SCOPED CREDENTIAL <CredentialName>
    WITH IDENTITY = '<Azure Event Hubs SAS Policy name>',
    SECRET = '<Primary or Secondary key value>' -- BVFnT3baC/K6I8xNZzio4AeoFt6nHeK0i+ZErNGsxiw=

EXEC sys.sp_enable_event_stream

EXEC sys.sp_create_event_stream_group
    @stream_group_name =      N'<EventStreamGroupName>',  -- myStreamGroup
    @destination_type =       N'AzureEventHubsApacheKafka',
    @destination_location =   N'<AzureEventHubsHostName>:<port>/<EventHubsInstance>', -- myEventHubsNamespace.servicebus.windows.net:9093/myEventHubsInstance
    @destination_credential = <CredentialName>,
    @max_message_size_kb =    <MaxMessageSize>,       -- 1024
    @partition_key_scheme =   N'<PartitionKeyScheme>'  -- N'None'

EXEC sys.sp_add_object_to_event_stream_group
    N'<EventStreamGroupName>',
    N'<SchemaName>.<TableName>' -- dbo.myTable
```

---

To confirm that streaming is enabled and to view the tables configured for a stream group, see [View CES configuration and function](#view-ces-configuration-and-function).

## Message size and column truncation

Azure Event Hubs and Fabric Eventstream limit the maximum size of each message they receive. CES uses the `@max_message_size_kb` setting to split a large outbound event into multiple message chunks that fit the configured destination. Set this value to align with the limits of your destination. For the JSON attributes that identify message chunks, see [JSON message format - change event streaming](message-format.md).

If one or more streamed column values are larger than 1 MB, CES truncates each affected column value to 1 MB before it forms the outbound event. The 1 MB limit applies to each column value separately. This truncation is separate from message splitting. After truncating oversized column values, CES forms the outbound event, splits it into chunks as needed according to `@max_message_size_kb`, and sends each chunk to the destination.

Set the [max text repl size](../../../database-engine/configure-windows/configure-the-max-text-repl-size-server-configuration-option.md) server configuration option to allow more than 65,536 bytes to be written to LOB columns when CES is enabled.

For example, if a row has five columns and the first three column values are each larger than 1 MB, CES:

1. Truncates the first column value to 1 MB.
1. Truncates the second column value to 1 MB.
1. Truncates the third column value to 1 MB.
1. Prepares the outbound event, splits it into the required number of chunks according to `@max_message_size_kb`, and sends each chunk to the destination.

> [!NOTE]
> Column data truncation is unconditional. CES truncates each streamed column value larger than 1 MB and doesn't log errors or warnings when this truncation occurs.

## View CES configuration and function

In [sys.databases](../../system-catalog-views/sys-databases-transact-sql.md), `is_event_stream_enabled = 1` indicates that change event streaming is enabled for the database.

The following query returns all databases with change event streaming enabled:

```sql
SELECT *
FROM sys.databases
WHERE is_event_stream_enabled = 1;
```

In [sys.tables](../../system-catalog-views/sys-tables-transact-sql.md), `is_replicated = 1` indicates a table is streamed, and [sp_help_change_feed_table](../../system-stored-procedures/sp-help-change-feed-table.md) provides information about the table group and table metadata for change event streaming.

The following query returns all tables with change event streaming enabled, and provides metadata information:

```sql
SELECT name,
       is_replicated
FROM sys.tables;

EXECUTE sp_help_change_feed_table
    @source_schema = '<schema name>',
    @source_name = '<table name>';
```

## CES stored procedures, system functions, and DMVs

The following table lists the stored procedures, system functions, and DMVs that you can use to configure, disable, and monitor change event streaming:

| System object | Description |
| --- | --- |
| <center>**Configure CES**</center> | |
| [sys.sp_enable_event_stream](../../system-stored-procedures/sys-sp-enable-event-stream-transact-sql.md) | Enables CES for the current user database. |
| [sys.sp_create_event_stream_group](../../system-stored-procedures/sys-sp-create-event-stream-group-transact-sql.md) | Creates a stream group, which is a streaming configuration for a group of tables. The stream group also defines the destination and related details (such as authentication, message size, partitioning). The stream_group_id is automatically generated and displayed for the end user when the procedure completes. |
| [sys.sp_add_object_to_event_stream_group](../../system-stored-procedures/sys-sp-add-object-to-event-stream-group-transact-sql.md) | Adds a table to the stream group. |
| <center>**Disable CES**</center> | |
| [sys.sp_remove_object_from_event_stream_group](../../system-stored-procedures/sys-sp-remove-object-from-event-stream-group-transact-sql.md) | Removes a table from the stream group. |
| [sys.sp_drop_event_stream_group](../../system-stored-procedures/sys-sp-drop-event-stream-group-transact-sql.md) | Drops the stream group. The stream group must not be in use. |
| [sys.sp_disable_event_stream](../../system-stored-procedures/sys-sp-disable-event-stream-transact-sql.md) | Disables CES for the current user database. |
| <center>**Monitor CES**</center> | |
| [sys.dm_change_feed_errors](../../system-dynamic-management-objects/sys-dm-change-feed-errors.md) | Returns delivery errors. |
| [sys.dm_change_feed_log_scan_sessions](../../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md) | Returns information about log scan activity. |
| [sys.sp_help_change_feed_settings](../../system-stored-procedures/sp-help-change-feed-settings.md) | Provides the status and information of configured change event streaming. |
| [sys.sp_help_change_feed](../../system-stored-procedures/sp-help-change-feed.md) | Monitors the current configuration of the change stream. |
| [sys.sp_help_change_feed_table_groups](../../system-stored-procedures/sp-help-change-feed-table-groups.md) | Returns metadata that is used to configure change event streaming groups. |
| [sys.sp_help_change_feed_table](../../system-stored-procedures/sp-help-change-feed-table.md) | Provides the status and information of the streaming group and table metadata for change event streaming. |

## Transaction log growth

Because message delivery is guaranteed, the transaction log for a database that has CES enabled can continue to grow. Log truncation is prevented as long as there are CES changes to stream from the log. Once the transaction log size reaches the max defined limit, writes to the database fail. 

To prevent this in Azure SQL Database and Azure SQL Managed Instance, when the transaction log size approaches the max defined limit, Microsoft might disable CES or kill long running transactions. For unmanaged SQL Server instances such as on-premises or on SQL Server on Azure VMs, you're responsible for monitoring the transaction log size and ensuring it doesn't reach the max defined limit, and manually disabling CES or killing long running transactions if needed.

Once CES is disabled, or a long running transaction is killed, the transaction log is truncated to free up space. You must manually enable CES again after it has been disabled, or retry any long running transactions that were killed. Data changes made while CES was disabled aren't captured. Only changes made after CES is restarted are streamed.

The following is a list of typical scenarios that can lead to transaction log growth with CES enabled:

- Persistent errors. CES retries to send a message that is rejected repeatedly and CES can't continue. Reasons for persistent errors that can lead to rejected messages include:
   - Network issues or misconfiguration.
   - Credential misconfiguration.
   - Misconfigured max message size that the destination rejects.
- The destination throttles the incoming events. For example, Azure Event Hubs rate limits based on its SKU.
- Long running transactions that generate a lot of log records and prevent log truncation.

To ensure smooth operations, monitor the size of the transaction log and [CES delivery errors](../../system-dynamic-management-objects/sys-dm-change-feed-errors.md) regularly.

## Performance

On SQL Server, Azure SQL Managed Instance and Azure SQL Database elastic pools, you can enable CES on multiple databases. Each CES-enabled database consumes server resources and competes with other server workload. Make sure that your server is adequately resourced for the expected workload and monitor the performance of your server and databases regularly.

## Limitations

Change event streaming (CES) has the following limitations:

- [Platform specific limitations](#platform-specific-limitations)
- [Server-level and general limitations](#server-level-and-general-limitations)
- [Database-level limitations](#database-level-limitations)
- [Table-level limitations](#table-level-limitations)
- [Column-level limitations](#column-level-limitations)
- [Permissions in the source database](#permissions-in-the-source-database-and-data-residency)
- [Networking and connectivity](#networking-and-connectivity)

### Platform specific limitations

On SQL products configured with a non-UTC time zone, the `committime` field in a [change event streaming message](message-format.md) incorrectly includes a **Z** suffix, even though this field shows the local time of the publishing database. When the database uses UTC, the value and suffix agree. This problem is known, and a fix is pending in a future release of the feature.

The following limitations apply to specific platforms for CES:

### [SQL Server 2025](#tab/sql-2025)

CES in SQL Server 2025 has the following limitations:
- You must enable the [preview feature database scoped configuration](../../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features).
- Microsoft Entra authentication is supported starting with Cumulative Update 3 (CU3) for instances [enabled by Azure Arc](../../../sql-server/azure-arc/connect.md) or running on an [Azure VM](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview).

### [Azure SQL Database](#tab/sql-db)

xEvent debugging isn't currently available in Azure SQL Database.

### [Azure SQL Managed Instance](#tab/sql-mi)

CES isn't available to instances configured with the SQL Server 2022 [update policy](/azure/azure-sql/managed-instance/update-policy).

### [SQL database in Fabric](#tab/sql-db-fabric)

CES has the following limitations in SQL database in Fabric:
- xEvent debugging isn't currently available.
- Microsoft Entra authentication isn't currently available.

---

### Server-level and general limitations


The following list describes server-level and general limitations:
- CES isn't supported on [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] on Linux.
- CES emits events only for data changes from `INSERT`, `UPDATE`, and `DELETE` DML statements.
- CES doesn't handle schema changes (DDL operations), which means it doesn't emit events for DDL operations. However, DDL operations aren't blocked, so if you execute them, the schema of subsequent DML events reflects the updated table structure. You're expected to gracefully handle events with the updated schema.
- Currently, CES doesn't stream data that exists in a table before CES is enabled. Existing data isn't seeded, or sent as a snapshot, when CES is enabled.
- If a message exceeds the Azure Event Hubs message size limit, the failure is currently only observable through Extended Events. CES xEvents are currently only available in SQL Server 2025, and not Azure SQL Database.
- Renaming tables and columns configured for CES isn't supported. Renaming a table or column fails. Database renames **are allowed**.
- CES isn't available to Azure SQL Managed Instance configured with the SQL Server 2022 [update policy](/azure/azure-sql/managed-instance/update-policy). It's only available to instances configured with the SQL Server 2025 or Always-up-to-date update policy.
- When using Kafka protocol, CES doesn't support SAS token authentication. The only available authentication methods are Microsoft Entra and Shared Access policy key value.

### Database-level limitations

The following list describes database-level limitations:
- CES only supports databases configured with the full recovery model.
- CES doesn't support databases configured with [Fabric Mirrored Databases for SQL Server](/fabric/database/mirrored-database/sql-server), [transactional replication](../../replication/transactional/transactional-replication.md), [change data capture](../about-change-data-capture-sql-server.md), or [Azure Synapse Link](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview). [Change tracking](../about-change-tracking-sql-server.md) is supported on databases configured with CES.
- CES can only stream from writable primary databases. Secondary databases that are part of Always On availability groups or that use the [Managed Instance link](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview) can't be configured as streaming sources.
- You can't enable CES on views or indexed views.
- You can configure up to 4,096 stream groups. Each stream group can include up to 40,000 tables.

### Table-level limitations

The following list describes table-level limitations:
- A table can belong to only one streaming group. You can't stream the same table to multiple destinations.
- You can only configure user tables for CES. CES doesn't support streaming system tables.
- While CES is enabled on a table, you can't add or drop a primary key constraint on that table.
- Table names that contain a period (`.`) are currently unsupported.
- `ALTER TABLE SWITCH PARTITION` isn't supported on tables configured for CES.
- `TRUNCATE TABLE` isn't supported on tables enabled for CES.
- CES doesn't support tables that use any of the following features:
  - Clustered columnstore indexes
  - Temporal history tables or ledger history tables
  - Always Encrypted
  - In-memory OLTP (memory-optimized tables)
  - Graph tables
  - External tables

> [!IMPORTANT]  
> [Online index operations](../../indexes/perform-index-operations-online.md) can generate substantial amounts of transaction log records. CES must process significantly more data, which can result in increased event latency.

### Column-level limitations

The following list describes column-level limitations:
- CES doesn't support the following data types. Streaming skips columns of these types:
  - **geography**
  - **geometry**
  - **image**
  - **json**
  - **rowversion** / **timestamp**
  - **sql_variant**
  - **text** / **ntext**
  - **vector**
  - **xml**
  - User-defined types (UDT)
- If one or more streamed column values are larger than 1 MB, CES truncates each affected column value to 1 MB before it forms the outbound event. This truncation is unconditional and doesn't log errors or warnings. Configure the [max text repl size](../../../database-engine/configure-windows/configure-the-max-text-repl-size-server-configuration-option.md) server configuration option to allow more than 65,536 bytes to be written to LOB columns when CES is enabled.

### Permissions in the source database and data residency

The following list describes permissions and data residency limitations:
- For row-level security, CES emits changes from all rows, regardless of user permissions.
- Dynamic data masking doesn't apply to data sent through CES. Data is streamed unmasked, even if masking is configured.
- CES doesn't emit events related to object-level permission changes (for example, granting permissions to specific columns).
- CES streams data to the configured destination if the network configuration allows it. If the destination is in a different region, CES streams the data across regions. Ensure this complies with your data residency and compliance requirements.

### Networking and connectivity

The following list describes a network and connectivity limitation:
- Currently, CES can only stream to Azure Event Hubs public endpoints. Service endpoints and private endpoints aren't currently supported.
- When using the AMQP protocol on Azure SQL Managed Instance or SQL Server 2025 (for existing stream groups not yet migrated), set the Azure Event Hubs [Minimum TLS version](/azure/event-hubs/transport-layer-security-enforce-minimum-version) configuration option to 1.2. CES doesn't work with TLS 1.3 over the AMQP protocol.

## Related content

- [What is change event streaming?](overview.md)
- [Frequently asked questions](frequently-asked-questions-faq.yml)
- [JSON message format - change event streaming](message-format.md)
- [Stream to Fabric Eventstream](/fabric/real-time-intelligence/event-streams/stream-sql-change-events-to-eventstream)