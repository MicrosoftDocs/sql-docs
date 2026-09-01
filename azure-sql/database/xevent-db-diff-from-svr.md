---
title: Extended Events in Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & SQL database in Fabric
description: Describes extended events (XEvents) in Azure SQL
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest, dfurman
ms.date: 08/21/2026
ai-usage: ai-assisted
ms.service: azure-sql
ms.subservice: performance
ms.topic: concept-article
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi || =fabricsql"
---

# Extended Events in Azure SQL

[!INCLUDE [appliesto-sqldb-sqlmi-fabricsqldb](../includes/appliesto-sqldb-sqlmi-fabricsqldb.md)]

[!INCLUDE [sql-database-xevents-selectors-1-include](../includes/sql-database-xevents-selectors-1-include.md)]

For an introduction to Extended Events, see:

- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [Quick Start: Extended events](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)

The feature set, functionality, and usage scenarios for Extended Events in Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance are similar to what is available in SQL Server. The main differences are:

- In Azure SQL Database, SQL database in Fabric, and Azure SQL Managed Instance, the `event_file` target always uses blobs in Azure Storage, rather than files on disk.
   - In SQL Server, the `event_file` target can use either files on disk or blobs in Azure Storage.
- In Azure SQL Database and SQL database in Fabric, event sessions are always database-scoped. This means that:
   - An event session in one database can't collect events from another database.
   - An event must occur in the context of a user database to be included in a session.
- In Azure SQL Managed Instance, you can create both server-scoped and database-scoped event sessions. We recommend using server-scoped event sessions for most scenarios.

## Get started

There are two walkthrough examples to help you get started with Extended Events quickly:

- [Create an event session with an event_file target in Azure Storage](xevent-code-event-file.md). This example shows you how to capture event data in a file (blob) in Azure Storage using the `event_file` target, and includes [troubleshooting guidance](xevent-code-event-file.md#troubleshoot-event-sessions-with-an-event_file-target-in-azure-storage) for common errors. Use this if you need to persist captured event data, or if you want to use event viewer in SQL Server Management Studio (SSMS) to analyze captured data.
- [Create an event session with a ring_buffer target in memory](xevent-code-ring-buffer.md). This example shows you how to capture the latest events from an event session in memory using the `ring_buffer` target. Use this as a quick way to look at recent events during ad hoc investigations or troubleshooting, without having to store captured event data.

Extended Events can be used to monitor read-only replicas. For more information, see [Read queries on replicas](read-scale-out.md#monitor-read-only-replicas-with-extended-events).

## Best practices

Adopt the following best practices to use Extended Events securely, reliably, and without affecting database engine health and workload performance.

- If you use the `event_file` target:
  - Depending on the events added to a session, the files produced by the `event_file` target might contain sensitive data. Carefully review RBAC role assignments and the access control lists (ACL) on the storage account and container, including inherited access, to avoid granting unnecessary read access. Follow the [principle of least privilege](/entra/identity-platform/secure-least-privileged-access).
  - Use a storage account in the same Azure region as the database or managed instance where you create event sessions.
  - Align the redundancy of the storage account with the redundancy of the database, elastic pool, or managed instance. For [locally redundant](high-availability-sla-local-zone-redundancy.md#locally-redundant-availability) resources, use LRS, GRS, or RA-GRS. For [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) resources, use ZRS, GZRS, or RA-GZRS. See [Azure Storage redundancy](/azure/storage/common/storage-redundancy) for details.
  - Don't use any [blob access tier](/azure/storage/blobs/access-tiers-overview) other than `Hot`.
  - Don't enable the [hierarchical namespace](/azure/storage/blobs/data-lake-storage-namespace) for the storage account.
- If you want to create a continuously running event session that starts automatically after each [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] restart (for example, after a failover or a maintenance event), include the event session option of `STARTUP_STATE = ON` in your `CREATE EVENT SESSION`  or `ALTER EVENT SESSION` statements.
- Conversely, use `STARTUP_STATE = OFF` for short-term event sessions such as those used in ad hoc troubleshooting.
- In Azure SQL Database, do not read deadlock events from the built-in `dl` event session. If there is a large number of deadlock events collected, reading them with the [sys.fn_xe_file_target_read_file()](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql) function can cause an out-of-memory error in the `master` database. This might affect login processing and result in an application outage. For the recommended ways to monitor deadlocks, see [Collect deadlock graphs in Azure SQL Database with Extended Events](analyze-prevent-deadlocks.md#collect-deadlock-graphs-in-azure-sql-database-with-extended-events).

## Event session targets

For more information about Extended Events targets supported in Azure SQL Database, SQL database in Fabric, Azure SQL Managed Instance, and SQL Server, see [Targets for Extended Events](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server).

## Transact-SQL differences

When you execute the [CREATE EVENT SESSION](/sql/t-sql/statements/create-event-session-transact-sql), [ALTER EVENT SESSION](/sql/t-sql/statements/alter-event-session-transact-sql), and [DROP EVENT SESSION](/sql/t-sql/statements/drop-event-session-transact-sql) statements in SQL Server and in Azure SQL Managed Instance, you use the `ON SERVER` clause. In Azure SQL Database, you use the `ON DATABASE` clause instead, because in Azure SQL Database event sessions are database-scoped.

## Extended Events catalog views

Extended Events provides several [catalog views](/sql/relational-databases/system-catalog-views/catalog-views-transact-sql). Catalog views tell you about event session *metadata* or *definition*. These views don't return information about instances of active event sessions.

For list of catalog views for each platform, see [Extended Events Catalog Views](/sql/relational-databases/extended-events/extended-events#extended-events-catalog-views).

## Extended Events dynamic management views

Extended Events provides several [dynamic management views (DMVs)](/sql/relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views). DMVs return information about *started* event sessions.

For list of DMVs for each platform, see [Extended Events Dynamic Management Views](/sql/relational-databases/extended-events/extended-events#extended-events-dynamic-management-views).

### Common DMVs

There are additional Extended Events DMVs that are common to Azure SQL Database, Azure SQL Managed Instance, and SQL Server:

- [sys.dm_xe_map_values](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-map-values-transact-sql)
- [sys.dm_xe_object_columns](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-object-columns-transact-sql)
- [sys.dm_xe_objects](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql)
- [sys.dm_xe_packages](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-packages-transact-sql)

<a id="sqlfindseventsactionstargets"></a>

## Available events, actions, and targets

You can obtain available events, actions, and targets using this query:

```sql
SELECT o.object_type,
       p.name AS package_name,
       o.name AS db_object_name,
       o.description AS db_obj_description
FROM sys.dm_xe_objects AS o
INNER JOIN sys.dm_xe_packages AS p
ON p.guid = o.package_guid
WHERE o.object_type IN ('action','event','target')
ORDER BY o.object_type,
         p.name,
         o.name;
```

## Permissions

See [permissions](/sql/relational-databases/extended-events/extended-events#permissions) for detailed permissions by platform.

## Storage container authorization and control

When you use the `event_file` target with Azure Storage blobs, the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] running the event session must have specific access to the blob container. You can grant this access in one of the following ways:

- Assign the **Storage Blob Data Contributor** RBAC role to the [managed identity](authentication-azure-ad-user-assigned-managed-identity.md) of the Azure SQL logical server or Azure SQL managed instance on the container, and create a credential to instruct the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] to use managed identity for authentication.

  As an alternative to assigning the **Storage Blob Data Contributor** RBAC role, you can assign the following RBAC actions:

  | Namespace | Action |
  |:--|:--|
  | `Microsoft.Storage/storageAccounts/blobServices/containers/` |`read`|
  | `Microsoft.Storage/storageAccounts/blobServices/containers/blobs/` |`delete`|
  | `Microsoft.Storage/storageAccounts/blobServices/containers/blobs/` |`read`|
  | `Microsoft.Storage/storageAccounts/blobServices/containers/blobs/` |`write`|

- Create a [SAS token](/azure/storage/common/storage-sas-overview#sas-token) for the container, and store the token in a [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine).

  In Azure SQL Database, you must use a database-scoped credential. In Azure SQL Managed Instance and SQL Server, use a server-scoped credential.

  The SAS token you create for your Azure Storage container must satisfy the following requirements:

  - Have the `rwdl` (`Read`, `Write`, `Delete`, `List`) permissions.
  - Have the start time and expiry time that encompass the lifetime of the event session.
  - Have no IP address restrictions.

## Network security perimeter (preview)

[Network security perimeter](network-security-perimeter.md) (preview) puts a network access boundary around Azure SQL Database and other Azure platform as a service (PaaS) resources. When you associate a logical server with a network security perimeter (NSP), the outbound connections that Extended Events makes to Azure Storage are subject to the perimeter's access rules.

> [!NOTE]  
> Network security perimeter is available for Azure SQL Database only. This section doesn't apply to Azure SQL Managed Instance or SQL database in Fabric. As a preview feature, network security perimeter is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

### How Extended Events uses network access

Extended Events makes outbound connections from the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] to Azure Storage in two cases:

- **Writing event data.** When you start an event session with an `event_file` target that points to a blob, the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] checks outbound access before the session starts, and again each time it flushes event buffers to the blob.
- **Reading event data.** When you call [sys.fn_xe_file_target_read_file](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql) or [sys.fn_MSxe_read_event_stream](/sql/relational-databases/system-functions/sys-fn-msxe-read-event-stream-transact-sql) with a blob URL, the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] checks outbound access when the function initializes. SSMS calls `sys.fn_MSxe_read_event_stream` when you open captured event data in the event viewer.

Inbound TDS connections used to manage event sessions via T-SQL don't need any NSP configuration specific to Extended Events. The `CREATE EVENT SESSION`, `ALTER EVENT SESSION`, and `DROP EVENT SESSION` statements and the read functions all run over a normal client connection, so they follow the same inbound access rules as any other client connection to the database.

### Supported configurations

Behavior depends on the [access mode](/azure/private-link/network-security-perimeter-concepts#access-modes-in-network-security-perimeter) of the perimeter, on whether the storage account is in the same perimeter as the logical server, and on whether two different perimeters are linked to each other.

| SQL logical server NSP | Storage account NSP | Behavior |
| --- | --- | --- |
| No NSP | No NSP | The perimeter doesn't evaluate the connection. Extended Events connects to the storage account using the credential you configured and the storage account firewall rules. For more information, see [Storage container authorization and control](#storage-container-authorization-and-control). |
| No NSP | In an NSP | The perimeter doesn't evaluate outbound access from the logical server. Whether the connection succeeds depends on the inbound rules of the storage account's own perimeter. |
| In an NSP (Enforced) | Same NSP | Access is always allowed. You don't need an outbound rule. |
| In an NSP (Enforced) | Different but linked NSP | Access is allowed through cross-perimeter rules. You don't need an outbound FQDN rule. |
| In an NSP (Enforced) | Different unlinked NSP, or no NSP | Access is allowed when you use a managed identity, or when an outbound FQDN rule in the perimeter profile matches the host name of the storage account. If you use a SAS token and no rule matches, the event session fails to start with error 25602, and read functions might fail with error 25759. |
| In an NSP (Transition) | Any | The perimeter evaluates and logs rules but doesn't block traffic. |

### Configure outbound access to the storage account

When you configure a database to use Extended Events, you can choose between [managed identity](xevent-code-event-file.md#grant-access-using-managed-identity) and [SAS token](xevent-code-event-file.md#grant-access-using-a-sas-token) authentication. The authentication mechanism you choose determines whether you need an outbound access rule.

1. **Verify the perimeter association.** In the Azure portal, search for **Network Security Perimeter**, select your perimeter, and then select **Associated Resources** from the **Settings** menu to confirm that your server is listed. For more information, see [Network security perimeter](network-security-perimeter.md#get-started).
1. **Choose your authentication mechanism.** Use managed identity authentication. A managed identity token includes the claims that the perimeter needs, so you don't need to add an outbound rule and can skip the next step.
1. **Add an outbound access rule (SAS token only).** If you use a SAS token and the perimeter is in enforced mode, add an outbound access rule on the perimeter profile. Use a rule type of **Fully qualified domain names (FQDN)** and the host name of your storage account as the value, for example `myxedata.blob.core.windows.net`.

In this example, you can use `*.blob.core.windows.net` to allow every Azure Storage account, but that setting allows outbound connections to storage accounts you don't own. Use the specific host name where you can.

Keep the perimeter in transition mode until you confirm which outbound rules you need. In transition mode, the perimeter logs rule evaluations without blocking access, so you can find missing rules before they cause failures. Switch to enforced mode after the rules are in place.

### Limitations and behavior differences

- The [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] checks outbound access when a session starts and at every buffer flush. If you remove an outbound rule while a session is running, the session doesn't stop. Individual buffer writes start failing instead.
- Managed identities and SAS tokens aren't equivalent under a perimeter. A managed identity token carries perimeter claims, so it doesn't need an outbound rule. A SAS token doesn't carry those claims, so it needs a matching outbound rule in enforced mode.
- A blocked read function might not raise an error. When a perimeter blocks `sys.fn_xe_file_target_read_file` or `sys.fn_MSxe_read_event_stream`, the function might raise error 25759 or 25717, or return an empty result set with no error. If you expect data but get no rows and no error, check your outbound rules.

### Errors when a perimeter blocks access

Error 25602 means the `event_file` target couldn't initialize because the perimeter blocked the outbound connection to the storage account:

```output
The target, "<target_name>", encountered a configuration error during initialization. Object cannot be added to the event session.
For more information, see https://go.microsoft.com/fwlink/?linkid=2336061.
```

Error 25759 means a perimeter blocked a read function:

```output
Network Security Perimeter (NSP) blocked outbound access to the storage URL '<url>'.
The NSP configuration does not allow reading from the specified location.
```

Error 25717 means access was revoked while a read function was reading. Because the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] reads blob data in chunks rather than downloading whole files, this error can happen partway through a result set:

```output
The operating system returned error <error details> while reading from the file '<url>'.
```

To resolve any of these errors, switch to managed identity authentication, add an outbound FQDN rule that matches the host name of the storage account, or move the storage account into the same perimeter as the logical server.

For more diagnostic detail about target initialization and buffer write failures, query the Extended Events engine log:

```sql
SELECT CONVERT(xml, record) AS record_xml
FROM sys.dm_os_ring_buffers
WHERE ring_buffer_type = 'RING_BUFFER_XE_LOG';
```

Perimeter association changes and access mode changes appear in the Azure Activity Log for the logical server. Inbound and outbound rule evaluations appear in [network security perimeter diagnostic logs](/azure/private-link/network-security-perimeter-diagnostic-logs).

## Resource governance

In Azure SQL Database, memory consumption by extended event sessions is dynamically controlled by the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] to minimize resource contention.

There's a limit on memory available to event sessions:

- In a single database, total session memory is limited to 128 MB.
- In an elastic pool, individual databases are limited by the single database limits, and in total they can't exceed 512 MB.

If you receive an error message referencing a memory limit, the corrective actions you can take are:

- Run fewer concurrent event sessions.
- Using `CREATE` and `ALTER` statements for event sessions, reduce the amount of memory you specify in the `MAX_MEMORY` clause for the session.

> [!NOTE]
> In Extended Events, the `MAX_MEMORY` clause appears in two contexts: when creating or altering a session (at the session level), and when using the `ring_buffer` target (at the target level). The above limits apply to the session level memory.

There's a limit on the number of started event sessions in Azure SQL Database:

- In a single database, the limit is 100.
- In an elastic pool, the limit is 100 database-scoped sessions per pool.

In [dense elastic pools](elastic-pool-resource-management.md), starting a new extended event session might fail due to memory constraints even when the total number of started sessions is below 100.

To find the total memory consumed by an event session, execute the following query while connected to the database where the event session is started:

```sql
SELECT name AS session_name,
       total_buffer_size + total_target_memory AS total_session_memory
FROM sys.dm_xe_database_sessions;
```

To find the total event session memory for an elastic pool, this query needs to be executed in every database in the pool.

## Related content

- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [Quick Start: Extended events](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)
- [Network security perimeter for Azure SQL Database (preview)](network-security-perimeter.md)
