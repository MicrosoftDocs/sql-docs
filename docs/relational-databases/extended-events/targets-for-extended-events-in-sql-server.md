---
title: Extended Events Targets
description: This article explains different targets for Extended Events sessions. Learn about target abilities in gathering and reporting data.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman, randolphwest
ms.date: 05/28/2026
ms.service: sql
ms.subservice: xevents
ms.topic: concept-article
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017"
---

# Extended Events targets

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This article explains when and how to use Extended Events targets. For each target, the article describes:

- Its abilities in gathering and reporting event data
- Examples of event sessions using the target

The following table describes the availability of each target type in different SQL platforms.

| Target type | SQL Server | Azure SQL Database and SQL database in Fabric | Azure SQL Managed Instance |
| --- | --- | --- | --- |
| [event_file](#event_file-target) | Yes | Yes | Yes |
| [ring_buffer](#ring_buffer-target) | Yes | Yes | Yes |
| [event_stream](#event_stream-target) | Yes | Yes | Yes |
| [histogram](#histogram-target) | Yes | Yes | Yes |
| [event_counter](#event_counter-target) | Yes | Yes | Yes |
| [pair_matching](#pair_matching-target) | Yes | No | No |
| [etw_classic_sync_target](#etw_classic_sync_target-target) | Yes | No | No |

Unless noted differently, targets process the data they receive asynchronously.

To make the most use of this article, you should:

- Be familiar with the basics of Extended Events, as described in [Quickstart: Extended Events](quick-start-extended-events-in-sql-server.md).
- Use a recent version of [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms).

## event_file target

The `event_file` target writes event session output from memory buffers to a disk file or to a blob in Azure Storage.

- You specify the `filename` parameter in the `ADD TARGET` clause. The file extension must be `xel`.

- The file name you choose is used by the system as a prefix to which a date-time based numeric value is appended, followed by the `xel` extension.

- You can optionally specify the `MAX_FILE_SIZE` parameter. It defines the maximum size in megabytes (MB) to which the file can grow before a new file is created.

- You can optionally specify the `MAX_ROLLOVER_FILES` option to choose the maximum number of files to retain in the file system in addition to the current file. The default value is `UNLIMITED`. When `MAX_ROLLOVER_FILES` is evaluated, if the number of files exceeds the `MAX_ROLLOVER_FILES` setting, the older files are deleted.

> [!NOTE]
>
> Prior to May 2026, the `MAX_ROLLOVER_FILES` option had no effect for the `event_file` targets using blobs in Azure Storage.
>
> Starting from May 2026, the `MAX_ROLLOVER_FILES` option can be used to retain only the specified number of the most recent blobs. You must create a new event session and specify `MAX_ROLLOVER_FILES` in the `ADD TARGET` clause. For more information, see [Create an event session with event_file target in Azure Storage in T-SQL](#create-an-event-session-with-event_file-target-in-azure-storage-in-t-sql).
>
> Support for `MAX_ROLLOVER_FILES` with Azure Storage blobs is currently **in preview** in Azure SQL Database and SQL database in Fabric only.

> [!IMPORTANT]  
> Depending on the events added to a session, the files produced by the `event_file` target might contain sensitive data. Carefully review the file system and share permissions on the directory and individual `.xel` files, including inherited access, to avoid granting unnecessary read access. Follow the [principle of least privilege](/entra/identity-platform/secure-least-privileged-access). To reduce the risk of collecting sensitive data inadvertently, avoid long-running event sessions if they might collect sensitive data.

::: moniker range="=azuresqldb-current || =azuresqldb-mi-current"

> [!NOTE]  
> [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] only support blobs in [Azure Storage](/azure/storage/common/storage-account-create) as the value of the `filename` parameter. For an `event_file` code example for Azure SQL Database, SQL database in Fabric, or Azure SQL Managed Instance, see [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file).

::: moniker-end

### Create an event session with event_file target in the local file system

For a walkthrough for creating an event session using an `event_file` with local file storage, with SSMS or T-SQL, see [Quickstart: Extended Events](quick-start-extended-events-in-sql-server.md).

### Create an event session with event_file target in Azure Storage

For a detailed description of how to create a storage account in Azure Storage, see [Create a storage account](/azure/storage/common/storage-account-create). You can create a storage account using Azure portal, PowerShell, Azure SQL, an ARM template, or a Bicep template. Use an account that:

- Is a `Standard general-purpose v2` account.
- Uses the `Hot` [blob access tier](/azure/storage/blobs/access-tiers-overview).
- If using SQL Server in Azure Virtual Machine (Azure VM), the storage account should be in the same Azure region as your Azure VM.
- Doesn't have the [hierarchical namespace](/azure/storage/blobs/data-lake-storage-namespace) enabled.

Next, [create a container](/azure/storage/blobs/blob-containers-portal#create-a-container) in this storage account using Azure portal. You can also create a container [using PowerShell](/azure/storage/blobs/blob-containers-powershell#create-a-container), or [using Azure CLI](/azure/storage/blobs/blob-containers-cli#create-a-container).

Note the names of the *storage account* and *container* you created. You use them in the following steps.

To read and write event data, the database engine requires specific access. You grant this access differently depending on your choice of authentication type: [managed identity](#grant-access-using-managed-identity) or [secret-based authentication with a shared access signature (SAS) token](#grant-access-using-a-sas-token).

To authenticate to Azure Storage, the database engine requires a [server-scoped credential or database-scoped credential](../security/authentication-access/credentials-database-engine.md), which tells it what kind of authentication to use, and provides a secret for secret-based authentication. Creating this credential requires the `CONTROL` database permission.

For SQL Server and Azure SQL Managed Instance, this permission is required in the `master` database. By default, the permission is held by the members of the `db_owner` database role in `master`, and by the members of the `sysadmin` server role on the instance. For Azure SQL Database and SQL database in Fabric, this permission is held by the database owner (`dbo`), by the members of the `db_owner` database role, and by the administrator of the logical server.

Once a credential is created, the remaining steps to create an event session don't require the `CONTROL` permission. See [Permissions](extended-events.md#permissions) for the specific permissions needed.

#### Grant access using managed identity

If using managed identity with Microsoft Entra authentication, you assign the **Storage Blob Data Contributor** RBAC role for the container to the managed identity used by the database engine. For more information, see the following based on the SQL platform:

- The [managed identity of the Azure SQL Database logical server](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity?view=azuresql-db&preserve-view=true).
- The [managed identity of the Azure SQL managed instance](/azure/azure-sql/database/authentication-azure-ad-user-assigned-managed-identity?view=azuresql-mi&preserve-view=true).
- The [managed identity of the Azure VM hosting your SQL Server instance](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm?view=azuresql-vm&preserve-view=true). For more information, see [How managed identities for Azure resources work with Azure virtual machines](/entra/identity/managed-identities-azure-resources/how-managed-identities-work-vm).
- The [managed identity of the Arc-enabled SQL Server instance](../../sql-server/azure-arc/managed-identity.md).

Once the RBAC role assignment is in place, use the following steps:

1. Create a credential using T-SQL.

   Before executing the following T-SQL batch, make the following change:

   - In all three occurrences of `https://<storage-account-name>.blob.core.windows.net/<container-name>`, replace `<storage-account-name>` with the name of your storage account, and replace `<container-name>` with the name of your container. Make sure that there's no trailing slash at the end of the URL.

   **Create a server-scoped credential**: (Applies to SQL Server, Azure SQL Managed Instance)

   Using a client tool such as SSMS, open a new query window, connect to `master` database on the instance where you want to create the event session, and paste the following T-SQL batch.

   ```sql
   /* The name of the credential must match the URL of the blob container. */
   IF EXISTS (SELECT 1
              FROM sys.credentials
              WHERE name = 'https://<storage-account-name>.blob.core.windows.net/<container-name>')
       DROP CREDENTIAL
           [https://<storage-account-name>.blob.core.windows.net/<container-name>];

   /* When using managed identity, the credential does not contain a secret */
   CREATE CREDENTIAL
       [https://<storage-account-name>.blob.core.windows.net/<container-name>]
       WITH IDENTITY = 'MANAGED IDENTITY';
   ```

   **Create a database-scoped credential**: (Applies to Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric)

   Using a client tool such as SSMS, open a new query window, connect to user database where you create the event session, and paste the following T-SQL batch. Make sure you're connected to your user database, and not to the `master` database.

   ```sql
   /* The name of the credential must match the URL of the blob container. */
   IF EXISTS (SELECT 1
              FROM sys.database_credentials
              WHERE name = 'https://<storage-account-name>.blob.core.windows.net/<container-name>')
       DROP DATABASE SCOPED CREDENTIAL
           [https://<storage-account-name>.blob.core.windows.net/<container-name>];

   /* When using managed identity, the credential does not contain a secret */
   CREATE DATABASE SCOPED CREDENTIAL
       [https://<storage-account-name>.blob.core.windows.net/<container-name>]
       WITH IDENTITY = 'MANAGED IDENTITY';
   ```

1. Then, follow the steps to [create an event session in SSMS with event_file target in Azure Storage](#create-an-event-session-in-ssms-with-event_file-target-in-azure-storage).

<a id="grant-access-using-a-sas-token"></a>

#### Grant access using a shared access signature (SAS) token

If using **secret-based authentication**, you create a [shared access signature (SAS) token](/azure/storage/common/storage-sas-overview#sas-token) for the container. To use this authentication type, the **Allow storage account key access** option must be enabled for the storage account. For more information, see [Prevent Shared Key authorization for an Azure Storage account](/azure/storage/common/shared-key-authorization-prevent).

1. In the Azure portal, navigate to the storage account and container that you created. Select the container, and navigate to **Settings > Shared access tokens**.

   The SAS token must satisfy the following requirements:

   - **Permissions** set to `Read`, `Write`, `Delete`, `List`.
   - The **Start** time and **Expiry** time must encompass the lifetime of the event session. The SAS token you create only works within this time interval.
   - For Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric, the SAS token must have no IP address restrictions.

   Select the **Generate SAS token and URL** button. The SAS token is in the **Blob SAS token** box. You can copy it to use in the next step.

   > [!IMPORTANT]  
   > The SAS token provides read and write access to this container. Treat it as you would treat a password or any other secret.

   :::image type="content" source="media/targets-for-extended-events-in-sql-server/create-shared-access-signature-token.png" alt-text="Screenshot of the Shared Access Tokens screen for an Azure Storage container, with a generated SAS token for an example container." lightbox="media/targets-for-extended-events-in-sql-server/create-shared-access-signature-token.png":::

1. Create a credential to store the SAS token using T-SQL.

   Before executing the following T-SQL batch, make these changes:

   - If creating a server-scoped credential and using the `CREATE MASTER KEY` statement, replace `<password>` with an strong password that protects the master key. For more information, see [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md).

   - In all three occurrences of `https://<storage-account-name>.blob.core.windows.net/<container-name>`, replace `<storage-account-name>` with the name of your storage account, and replace `<container-name>` with the name of your container.

   - In the `SECRET` clause, replace `<sas-token>` with the SAS token you copied in the previous step.

   **Create a server-scoped credential**: (Applies to SQL Server, Azure SQL Managed Instance)

   Using a client tool such as SSMS, open a new query window, connect it to the `master` database on the instance where you create the event session, and paste the following T-SQL batch.

   ```sql
   /* Create a master key to protect the secret of the credential */
   IF NOT EXISTS (SELECT 1
                  FROM sys.symmetric_keys
                  WHERE name = '##MS_DatabaseMasterKey##')
       CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>'

   /* The name of the credential must match the URL of the blob container. */;
   IF EXISTS (SELECT 1
              FROM sys.credentials
              WHERE name = 'https://<storage-account-name>.blob.core.windows.net/<container-name>')
       DROP CREDENTIAL
           [https://<storage-account-name>.blob.core.windows.net/<container-name>];

   /* The secret is the SAS token for the container. */
   CREATE CREDENTIAL
       [https://<storage-account-name>.blob.core.windows.net/<container-name>]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
       SECRET = '<sas-token>';
   ```

   **Create a database-scoped credential**: (Applies to Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric)

   Using a client tool such as SSMS, open a new query window, connect to the database where you create the event session, and paste the following T-SQL batch. Make sure you're connected to your user database, and not to the `master` database.

   ```sql
   /* The name of the credential must match the URL of the blob container. */
   IF EXISTS (SELECT 1
              FROM sys.database_credentials
              WHERE name = 'https://<storage-account-name>.blob.core.windows.net/<container-name>')
       DROP DATABASE SCOPED CREDENTIAL
           [https://<storage-account-name>.blob.core.windows.net/<container-name>];

   /* The secret is the SAS token for the container. */
   CREATE DATABASE SCOPED CREDENTIAL
       [https://<storage-account-name>.blob.core.windows.net/<container-name>]
   WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
       SECRET = '<sas-token>';
   ```

1. Then, follow the steps in the next section to [create an event session in SSMS with event_file target in Azure Storage](#create-an-event-session-in-ssms-with-event_file-target-in-azure-storage).

<a id="create-event-session-with-event_file-target"></a>

#### Create an event session in SSMS with event_file target in Azure Storage

Once the credential providing access to the storage container is created, you can create the event session. Unlike creating the credential, creating an event session doesn't require the `CONTROL` permission. Once the credential is created, you can create event sessions even if you have more restricted permissions. See [Permissions](extended-events.md#permissions) for the specific permissions needed.

To create a new event session in SSMS:

1. For SQL Server and Azure SQL Managed Instance, expand the **Extended Events** node under the **Management** folder. For Azure SQL Database and SQL database in Fabric, expand the **Extended Events** node under the database.

1. Right-click on the **Sessions** folder, and select **New Session...**.

1. On the **General** page, enter a name for the session, which is `example-session` for the following code sample.

1. On the **Events** page, select one or more events to add to the session. For example, you can select the `sql_batch_starting` event.

1. On the **Data Storage** page, select `event_file` as the target type. Paste the URL of the storage container in the **Storage URL** box. Type a forward slash (`/`) at the end of this URL, followed by the file (blob) name. For example, `https://<storage-account-name>.blob.core.windows.net/<container-name>/example-session.xel`.

1. Now that the session is configured, you can optionally select the **Script** button to create a T-SQL script of the session, to save it for later.

1. Select **OK** to create the session.

1. In Object Explorer, expand the **Sessions** folder to see the event session you created. By default, the session isn't started when it's created. To start the session, right-click on the session name, and select **Start Session**. You can later stop it by selecting **Stop Session** once the session is running.

As T-SQL batches are executed, the session writes the `sql_batch_starting` events to the `example-session.xel` blob in the storage container.

> [!NOTE]  
> For SQL Managed Instance, instead of pasting the storage container URL on the **Data storage** page, use the **Script** button to create a T-SQL script of the session. Specify the container URL as the value for the `filename` argument and execute the script to create the session.

#### Create an event session with event_file target in Azure Storage in T-SQL

Here's an example of the `CREATE EVENT SESSION` with an `ADD TARGET` clause that adds an Azure Storage-based `event_file` target.

```sql
CREATE EVENT SESSION [example-session]
ON SERVER
ADD EVENT sqlserver.sql_batch_starting
ADD TARGET package0.event_file
(
    SET filename = N'https://<storage-account-name>.blob.core.windows.net/<container-name>/example-session.xel'
);
```

To use this example in Azure SQL Database or SQL database in Fabric, replace `ON SERVER` with `ON DATABASE`.

Here's a similar example for in Azure SQL Database or SQL database in Fabric that limits the size of each blob in Azure Storage to approximately 6 megabytes, and limits the number of the most recent files retained by the session to 3.

```sql
CREATE EVENT SESSION [example-session-with-rollover]
ON DATABASE
ADD EVENT sqlserver.lock_acquired
ADD TARGET package0.event_file
(
    SET
    filename = N'https://<storage-account-name>.blob.core.windows.net/<container-name>/example-session-with-rollover.xel',
    max_file_size = 6,
    max_rollover_files = 3
);
```

### Troubleshoot event sessions with event_file target in Azure Storage

[!INCLUDE [troubleshoot-event-sessions-with-event_file-target-azure-storage](../../includes/troubleshoot-event-sessions-with-event-file-target-azure-storage.md)]

### sys.fn_xe_file_target_read_file() function

The `event_file` target stores the data it receives in a binary format that's not human readable. The `sys.fn_xe_file_target_read_file` function lets you represent the contents of an `xel` file as a relational rowset. For more information, including usage examples, see [sys.fn_xe_file_target_read_file](../system-functions/sys-fn-xe-file-target-read-file-transact-sql.md).

## ring_buffer target

The `ring_buffer` target is useful quickly starting an event session and collecting event data in memory only. When the available memory in the ring buffer is used by events, the older events are discarded. When you stop the event session, all session output to the `ring_buffer` target is also discarded.

You consume data from a `ring_buffer` target by converting it to XML, as shown in the following example. During this conversion, any data that doesn't fit into a 4-MB XML document is omitted. Therefore, even if you capture more events in the ring buffer by using larger `MAX_MEMORY` values (or by leaving this parameter at its default value), you might not be able to consume all of them because of the 4-MB limit on the XML document size, considering the overhead of XML markup and Unicode strings.

You know that the contents of the ring buffer are omitted during conversion to XML if the `truncated` attribute in the XML document is set to `1`, for example:

```xml
<RingBufferTarget truncated="1" processingTime="0" totalEventsProcessed="284" eventCount="284" droppedCount="0" memoryUsed="64139">
```

> [!TIP]  
> When adding a `ring_buffer` target, set its `MAX_MEMORY` parameter to 1,024 KB or less. Using larger values might increase memory consumption unnecessarily.
>
> By default, `MAX_MEMORY` for a `ring_buffer` target isn't limited in SQL Server, and is limited to 32 MB in Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.

<a id="create-event-session-with-a-ring_buffer-target"></a>

### Create an event session with a ring_buffer target

Here's an example of creating an event session with a `ring_buffer` target to collect the `lock_acquired` events, limiting the total number of events in the ring buffer to 100. In this example, the `MAX_MEMORY` parameter appears twice: once to set the `ring_buffer` target memory to 1,024 KB, and once to set the event session buffer memory to 2 MB.

To use this example in Azure SQL Database or SQL database in Fabric, replace `ON SERVER` with `ON DATABASE`.

```sql
CREATE EVENT SESSION ring_buffer_lock_acquired
ON SERVER
ADD EVENT sqlserver.lock_acquired
ADD TARGET package0.ring_buffer
(
SET MAX_EVENTS_LIMIT = 100,
    MAX_MEMORY = 1024
)
WITH
(
    MAX_MEMORY = 2 MB,
    MAX_DISPATCH_LATENCY = 3 SECONDS
);
```

To start the event session, execute the following statement:

```sql
ALTER EVENT SESSION ring_buffer_lock_acquired
ON SERVER
STATE = START;
```

<a id="view-event-session-data-in-a-ring_buffer-target"></a>

To view the collected event data in the ring buffer in SSMS, expand the session node and select the `package0.ping_buffer` target. The data is displayed in XML.

To see event data from a `ring_buffer` target in a relational rowset while the session is active, you use [XQuery](../../xquery/xquery-language-reference-sql-server.md) expressions to convert XML to relational data. For example:

```sql
;WITH
/* An XML document representing memory buffer contents */
RingBuffer AS
(
    SELECT CAST (xst.target_data AS XML) AS TargetData
    FROM sys.dm_xe_session_targets AS xst
         INNER JOIN sys.dm_xe_sessions AS xs
             ON xst.event_session_address = xs.address
    WHERE xs.name = N'ring_buffer_lock_acquired'
),
/* A row for each event in the buffer, represented as an XML fragment */
EventNode AS
(
    SELECT CAST (NodeData.query('.') AS XML) AS EventInfo
    FROM RingBuffer AS rb
    CROSS APPLY rb.TargetData.nodes('/RingBufferTarget/event') AS n(NodeData)
)

/* A relational rowset formed by using the XQuery value method */
SELECT EventInfo.value('(event/@timestamp)[1]','datetimeoffset') AS timestamp,
       EventInfo.value('(event/@name)[1]','sysname') AS event_name,
       EventInfo.value('(event/data/value)[1]','nvarchar(max)') AS sql_batch_text
FROM EventNode
ORDER BY timestamp DESC;
```

## event_stream target

The `event_stream` target can only be used in .NET programs written in languages like C#. Developers can access an event stream through .NET Framework classes in the `Microsoft.SqlServer.XEvents.Linq` namespace. This target is implicitly present in any event session. It can't be added using T-SQL.

For more information, see [sys.fn_MSxe_read_event_stream](../system-functions/sys-fn-msxe-read-event-stream-transact-sql.md).

If you encounter error 25726, `The event data stream was disconnected because there were too many outstanding events. To avoid this error either remove events or actions from your session or add a more restrictive predicate filter to your session.` when reading from the `event_stream` target, it means that the event stream filled up with data faster than the client could consume the data. This causes the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] to disconnect from the event stream to avoid affecting [!INCLUDE [ssde-md](../../includes/ssde-md.md)] performance.

## histogram target

The `histogram` target counts the number of event occurrences for distinct values in a field or action. For each distinct value, a separate count bucket is used. The `histogram` target processes the data it receives synchronously.

The `SOURCE_TYPE` parameter controls the behavior of the `histogram` target:

- `SOURCE_TYPE = 0`: collect data for an *event field*.
- `SOURCE_TYPE = 1`: collect data for an *action*. This is the default.

The default value of the `SLOTS` parameter is 256. If you assign another value, the value is rounded up to the next power of 2. For example, `SLOTS = 59` would be rounded up to 64. The maximum number of histogram slots for a `histogram` target is 16,384.

When using `histogram` as the target, you might sometimes see unexpected results. Some events might not appear in the expected slots, while other slots might show a higher than expected count of events. This can happen if a hash collision occurs when assigning events to slots. While this is rare, if a hash collision occurs, an event that should be counted in one slot is counted in another. For this reason, care should be taken assuming that an event didn't occur just because the count in a particular slot shows as zero.

As an example, consider the following scenario:

- You set up an Extended Events session, using `histogram` as the target and bucketizing events by `object_id`, to collect stored procedure execution statistics.
- You execute stored procedure `A`. Then, you execute stored procedure `B`.

If the hash function returns the same value for the `object_id` of both stored procedures, the histogram shows `A` being executed twice while `B` doesn't appear.

To mitigate this problem when the number of distinct values is relatively small, set the number of histogram slots higher than the square of expected distinct values. For example, if the `histogram` target has its `SOURCE` set to the `table_name` event field, and there are 20 tables in the database, then 20*20 = 400. The next power of 2 greater than 400 is 512, which is the recommended number of slots in this example.

Each `histogram` target accepts data from a single source (an event field or an action) and contains only one histogram. It isn't possible to add more than one target of the same type per event session. It's also not possible to have more than one source type per `histogram` target. Therefore, a new event session is required to track different actions or event fields in a separate `histogram` target.

### Create an event session with a histogram target

Here's an example of creating an event session with a `histogram` target.

To use this example in Azure SQL Database or SQL database in Fabric, replace `ON SERVER` with `ON DATABASE`.

```sql
CREATE EVENT SESSION histogram_lock_acquired
ON SERVER
ADD EVENT sqlserver.lock_acquired
(
    ACTION (sqlos.system_thread_id)
)
ADD TARGET package0.histogram
(
    SET FILTERING_EVENT_NAME = N'sqlserver.lock_acquired',
        SLOTS = 16,
        SOURCE = N'sqlos.system_thread_id',
        SOURCE_TYPE = 1
);
```

In the `ADD TARGET ... (SET ...)` clause, the target parameter `SOURCE_TYPE` is set to `1`, which means that the `histogram` target tracks an action.

The `ADD EVENT ... (ACTION ...)` clause adds the `sqlos.system_thread_id` action to the event. The `SOURCE` parameter is set to `sqlos.system_thread_id` to use the system thread ID collected by this action as the source of data for the `histogram` target. The `histogram` target in this example counts the number of `lock_acquired` events for each system thread that acquires locks while the session is active.

To start the event session, execute the following statement:

```sql
ALTER EVENT SESSION histogram_lock_acquired
ON SERVER
STATE = START;
```

To view the collected histogram data in SSMS, expand the session node and select the `package0.histogram` target. The data is displayed in a two-column grid. Each row represents a bucket of distinct values and a count of occurrences.

Here's how the data captured by the `histogram` target in this example might look. The values in the `value` column are `system_thread_id` values. For instance, a total of 236 locks were acquired by system thread 6540.

```output
value   count
-----   -----
 6540     236
 9308      91
 9668      74
10144      49
 5244      44
 2396      28
```

Here's an example of reading the data from a `histogram` target with T-SQL:

```sql
WITH histogram_target
AS (SELECT TRY_CAST (st.target_data AS XML) AS target_data
    FROM sys.dm_xe_sessions AS s
         INNER JOIN sys.dm_xe_session_targets AS st
             ON s.address = st.event_session_address
    WHERE s.name = 'event-session-name-placeholder'),
 histogram
AS (SELECT hb.slot.value('(@count)[1]', 'bigint') AS slot_count,
           hb.slot.value('(value/text())[1]', 'nvarchar(max)') AS slot_value
    FROM histogram_target AS ht
CROSS APPLY ht.target_data.nodes('/HistogramTarget/Slot') AS hb(slot))
SELECT slot_value,
       slot_count
FROM histogram;
```

## event_counter target

The `event_counter` target counts how many times each specified event occurs.

The `event_counter` target has no parameters and processes the data it receives synchronously.

### Create an event session with an event_counter target

Here's an example of creating an event session with a `event_counter` target. The session counts the first four `checkpoint_begin` events and then stops counting because its predicate limits the number of events sent to targets to four. You can generate the `checkpoint_begin` event for this example by executing the `CHECKPOINT` command.

To use this example in Azure SQL Database or SQL database in Fabric, replace `ON SERVER` with `ON DATABASE`.

```sql
CREATE EVENT SESSION event_counter_checkpoint_begin
ON SERVER
ADD EVENT sqlserver.checkpoint_begin
(
    WHERE package0.counter <= 4
)
ADD TARGET package0.event_counter
WITH
(
    MAX_MEMORY = 4096 KB,
    MAX_DISPATCH_LATENCY = 3 SECONDS
);
```

To start the event session, execute the following statement:

```sql
ALTER EVENT SESSION event_counter_checkpoint_begin
ON SERVER
STATE = START;
```

To view the collected data in SSMS, expand the session node and select the `package0.event_counter` target. The data is displayed in a three-column grid. Each row represents an event with a count of its occurrences.

Here's how the data captured by the `event_counter` target in this example might look after four checkpoints occur.

```output
package_name   event_name         count
------------   ----------------   -----
sqlserver      checkpoint_begin   4
```

## pair_matching target

The `pair_matching` target enables you to detect start events that occur without a corresponding end event. For instance, you can find a `lock_acquired` event without a matching `lock_released` event, which might indicate that a long-running transaction is holding locks.

Extended Events doesn't automatically match the start and end events. Instead, you define the matching logic in the `pair_matching` target specification in the `CREATE EVENT SESSION` statement. When a start and end event are matched, the target discards the pair but retains unmatched start events.

### Create an event session with an pair_matching target

For this example, we create an example table named `T1`, insert three rows, and obtain the `object_id` value for this table. For simplicity, we create the table in the `tempdb` database in this example. If you use a different database, adjust the database name in the T-SQL example code that follows.

```sql
CREATE TABLE T1 (id INT PRIMARY KEY);

INSERT INTO T1 (id)
VALUES (1), (2), (3);

SELECT OBJECT_ID('T1') AS object_id;
-- object_id = 1029578706
```

The following event session collects two events, `lock_acquired` and `lock_released`. It also has two targets. One is the `event_counter` target that provides the number of occurrences for each event. The other is the `pair_matching` target that defines the logic to match the start `lock_acquired` event and the end `lock_released` event into pairs.

The sequence of comma-delimited fields assigned to `BEGIN_MATCHING_COLUMNS` and `END_MATCHING_COLUMNS` must be the same. No tabs or newlines are allowed between the fields mentioned in the comma-delimited value, although spaces are allowed.

In the event session definition, we use an event predicate to collect only those events in the `tempdb` database where the `object_id` in the event matches the object ID of table `T1`. Adjust the predicate in the `WHERE` clause to use the object ID of your table.

To use this example in Azure SQL Database or SQL database in Fabric, replace `ON SERVER` with `ON DATABASE`.

```sql
CREATE EVENT SESSION pair_matching_lock_acquired_released
ON SERVER
ADD EVENT sqlserver.lock_acquired
(
    SET COLLECT_DATABASE_NAME = 1,
        COLLECT_RESOURCE_DESCRIPTION = 1
    ACTION (sqlserver.transaction_id)
    WHERE (database_name = 'tempdb'
           AND object_id = 1029578706)
),
ADD EVENT sqlserver.lock_released
(
    SET COLLECT_DATABASE_NAME = 1,
        COLLECT_RESOURCE_DESCRIPTION = 1
    ACTION (sqlserver.transaction_id)
    WHERE (database_name = 'tempdb'
           AND object_id = 1029578706)
)
ADD TARGET package0.event_counter,
ADD TARGET package0.pair_matching
(
    SET BEGIN_EVENT = N'sqlserver.lock_acquired',
        BEGIN_MATCHING_COLUMNS = N'resource_0, resource_1, resource_2, transaction_id, database_id',
        END_EVENT = N'sqlserver.lock_released',
        END_MATCHING_COLUMNS = N'resource_0, resource_1, resource_2, transaction_id, database_id',
        RESPOND_TO_MEMORY_PRESSURE = 1
)
WITH
(
    MAX_MEMORY = 8192 KB,
    MAX_DISPATCH_LATENCY = 15 SECONDS
);
```

To start the event session, execute the following statement:

```sql
ALTER EVENT SESSION pair_matching_lock_acquired_released
ON SERVER
STATE = START;
```

Start a transaction that updates the `T1` table, but don't commit it or roll it back. This ensures that there are acquired but not released locks.

```sql
BEGIN TRANSACTION;
UPDATE T1
    SET id = id + 1;
```

Examine the output from each target of the `pair_matching_lock_acquired_released` event session in SSMS.

The `event_counter` target has the following output, showing that one lock remains not released. However, this target doesn't show any details this lock.

```output
package_name   event_name      count
------------   ----------      -----
sqlserver      lock_acquired   4
sqlserver      lock_released   3
```

The `pair_matching` target has the following output, truncated for brevity. As suggested by the `event_counter` output, we do indeed see the one row for the unpaired `lock_acquired` event, with more details about the event.

```output
package_name  event_name    timestamp                     associated_object_id  database_id  database_name
------------  ------------  ---------                     -------------         -----------  -------------
sqlserver    lock_acquired   2025-10-01 20:06:07.1890000  1029578706            2            tempdb
```

Roll back the transaction.

```sql
ROLLBACK;
```

If you add an action to an event collected by the `pair_matching` target, the action data is collected as well. For example, you can include the T-SQL text provided by the `sqlserver.sql_text` action with the event. In this example, it would collect the query that acquired the lock.

## etw_classic_sync_target target

In SQL Server, Extended Events can interoperate with Event Tracing for Windows (ETW) to monitor system activity. For more information, see:

- [Event Tracing for Windows target](event-tracing-for-windows-target.md)
- [Monitor System Activity Using Extended Events](monitor-system-activity-using-extended-events.md)

This ETW target processes the data it receives synchronously.

## Related content

- [Extended Events overview](extended-events.md)
- [Quickstart: Extended Events](quick-start-extended-events-in-sql-server.md)
- [Extended Events in Azure SQL](/azure/azure-sql/database/xevent-db-diff-from-svr)
