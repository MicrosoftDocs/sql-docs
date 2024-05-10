---
title: "Quickstart: Extended Events"
description: This quickstart helps you use Extended Events, a lightweight performance monitoring system, to collect data to monitor and troubleshoot problems.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: maghan, randolphwest
ms.date: 05/09/2024
ms.service: sql
ms.subservice: xevents
ms.topic: quickstart
ms.custom:
  - intro-quickstart
f1_keywords:
  - "sql11.ssms.XeNewEventSession.General.f1"
  - "sql11.ssms.XeNewEventSession.Events.f1"
  - "sql11.ssms.XeNewEventSession.Targets.f1"
  - "sql11.ssms.XeNewEventSession.Advanced.f1"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Quickstart: Extended Events

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Extended Events is a lightweight performance monitoring feature that enables users to collect data to monitor and troubleshoot problems. By using Extended Events, you can see details of the database engine internal operations that are relevant for performance monitoring and troubleshooting purposes. To learn more about Extended Events, see [Extended Events overview](extended-events.md).

This article aims to help SQL administrators, engineers, and developers who are new to Extended Events, and who want to start using it and see event data in just a few minutes.

Extended Events is also known as *XEvents*, and sometimes just *XE*.

After reading this article, you can:

- See how to create an event session in [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS), with example screenshots
- Correlate screenshots to equivalent Transact-SQL statements
- Understand in detail the terms and concepts behind the SSMS user interface and XEvents T-SQL statements
- Learn how to test your event session
- Understand session results, including:
  - Available options for result storage
  - Processed versus raw results
  - Tools for viewing the results in different ways and on different time scales
- See how you can search for and discover all the available events
- Understand the relationships among Extended Events system views

> [!TIP]  
> For more information about Extended Events in Azure SQL Database, including code samples, see [Extended Events in Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/xevent-db-diff-from-svr).

## Prerequisites

To get started, you need to:

1. [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md). We recommend using a recent version of SSMS with the latest improvements and fixes.
1. Ensure that your account has the `ALTER ANY EVENT SESSION` [server permission](../../t-sql/statements/grant-server-permissions-transact-sql.md).

Details about security and permissions related to Extended Events are available at the end of this article in the [Appendix](#appendix1).

## Extended Events in SSMS

SSMS provides a fully functional user interface (UI) for Extended Events. Many scenarios can be accomplished using this UI, without having to use T-SQL or dynamic management views (DMVs).

In the next section you can see the UI steps to create an Extended Events session, and to see the data it reports. After going through the steps hands-on or reviewing them in this article, you can read about the concepts involved in the steps for a deeper understanding.

## Create an event session in SSMS

When you create an Extended Events session, you tell the system:

- Which events you're interested in
- How you want the system to report the data to you

The demonstration opens the **New Session** dialog, shows how to use its four pages, named:

- General
- Events
- Data Storage
- Advanced

The text and supporting screenshots can be slightly different in your version of SSMS, but should still be relevant for the explanation of basic concepts.

1. Connect to a database engine instance. Extended Events are supported starting with [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], in Azure SQL Database, and Azure SQL Managed Instance.

1. In Object Explorer, select **Management > Extended Events**. In Azure SQL Database, event sessions are database-scoped, so the **Extended Events** option is found under each database, not under **Management**. Right-click on the **Sessions** folder and select **New Session**. The **New Session** dialog is preferable to **New Session Wizard**, although the two are similar.

1. Select the **General** page. Then type `YourSession`, or any name you like, into the **Session name** text box. Don't select **OK** yet, because you still need to enter some details on other pages.

   :::image type="content" source="media/xevents-session-newsessions-10-general-ssms-yoursessionnode.png" alt-text="Screenshot of New Session > General > Session name." lightbox="media/xevents-session-newsessions-10-general-ssms-yoursessionnode.png":::

1. Select the **Events** page.

   :::image type="content" source="media/xevents-session-newsessions-14-events-ssms-rightclick-not-wizard.png" alt-text="Screenshot of New Session > Events > Select > Event library, Selected events." lightbox="media/xevents-session-newsessions-14-events-ssms-rightclick-not-wizard.png":::

1. In the **Event library** area, in the dropdown list, choose **Event names only**.

   - Type `sql_statement_` into the text box. This filters the list to show only events with `sql_statement_` in the name.
   - Scroll and select the event named `sql_statement_completed`.
   - Select the right arrow button `>` to move the event to the **Selected events** box.

1. Staying on the **Events** page, select the **Configure** button. This opens the **Event configuration options** box for the selected events.

   :::image type="content" source="media/xevents-session-newsessions-20b-events-ssms-yoursessionnode.png" alt-text="Screenshot of New Session > Events > Configure > Filter (Predicate) > Field." lightbox="media/xevents-session-newsessions-20b-events-ssms-yoursessionnode.png":::

1. Select the **Filter (Predicate)** tab. Next, select **Select here to add a clause**. We configure this filter (also known as predicate) to capture all `SELECT` statements that have a `HAVING` clause.

1. In the **Field** dropdown list, choose `sqlserver.sql_text`.
   - For **Operator**, choose `like_i_sql_unicode_string`. Here, `i` in the name of operator means case-**i**nsensitive.
   - For **Value**, type `%SELECT%HAVING%`. Here, percent signs are wildcards standing for any character string.

   > [!NOTE]  
   > In the two-part name of the field, *sqlserver* is the package name and *sql_text* is the field name. The event we chose earlier, *sql_statement_completed*, must be in the same package as the field we choose.

1. Select the **Data Storage** page.

1. In the **Targets** area, select **Select here to add a target**.

   - In the **Type** dropdown list, choose `event_file`. This means the event data is stored in a file that we can open and view later. In Azure SQL Database and Azure SQL Managed Instance, event data is stored in Azure Storage blobs.

   > [!NOTE]  
   > Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], you can use Azure Blob Storage in an `event_file` target in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

   :::image type="content" source="media/xevents-session-newsessions-30-datastorage-ssms-yoursessionnode.png" alt-text="Screenshot of New Session > Data Storage > Targets > Type > event_file." lightbox="media/xevents-session-newsessions-30-datastorage-ssms-yoursessionnode.png":::

1. In the **Properties** area, type in the full path and file name into the **File name on server** text box. You can also use the **Browse** button. The file name extension must be `xel`. In our example, we used `C:\Temp\YourSession_Target.xel`

   :::image type="content" source="media/xevents-session-newsessions-40-advanced-ssms-yoursessionnode.png" alt-text="Screenshot of New Session > Advanced > Maximum dispatch latency > OK." lightbox="media/xevents-session-newsessions-40-advanced-ssms-yoursessionnode.png":::

1. Select the **Advanced** page. Reduce **Maximum dispatch latency** to 3 seconds.

1. Select the **OK** button at the bottom to create this event session.

1. Back in Object Explorer, open or refresh the **Sessions** folder, and see the new node for `YourSession`. The session isn't started yet. You start it later.

   :::image type="content" source="media/xevents-session-newsessions-50-objectexplorer-ssms-yoursessionnode.png" alt-text="Screenshot of Node for your new *event session* named YourSession, in the Object Explorer, under Management > Extended Events > Sessions.":::

### Edit an event session in SSMS

In the SSMS Object Explorer, you can edit your event session by right-clicking its node, and then selecting **Properties**. The same multi-page dialog is displayed.

## Create an event session using T-SQL

In the SSMS Extended Events UI, you can generate a T-SQL script to create your event session as follows:

- Right-click on the event session node, then select **Script Session as > CREATE to > Clipboard**.
- Paste into any text editor.

Here's the generated `CREATE EVENT SESSION` T-SQL statement for `YourSession`:

```sql
CREATE EVENT SESSION [YourSession]
    ON SERVER -- For SQL Server and Azure SQL Managed Instance
    -- ON DATABASE -- For Azure SQL Database
    ADD EVENT sqlserver.sql_statement_completed
    (
        ACTION(sqlserver.sql_text)
        WHERE
        ( [sqlserver].[like_i_sql_unicode_string]([sqlserver].[sql_text], N'%SELECT%HAVING%')
        )
    )
    ADD TARGET package0.event_file
    (SET
        filename = N'C:\Temp\YourSession_Target.xel',
        max_file_size = (2),
        max_rollover_files = (2)
    )
    WITH (
        MAX_MEMORY = 2048 KB,
        EVENT_RETENTION_MODE = ALLOW_MULTIPLE_EVENT_LOSS,
        MAX_DISPATCH_LATENCY = 3 SECONDS,
        MAX_EVENT_SIZE = 0 KB,
        MEMORY_PARTITION_MODE = NONE,
        TRACK_CAUSALITY = OFF,
        STARTUP_STATE = OFF
    );
GO
```

### Conditional DROP of the event session

Before the `CREATE EVENT SESSION` statement, you can conditionally execute a `DROP EVENT SESSION` statement, in case a session with the same name already exists. This deletes the existing session. Without this, attempting to create a session with the same name causes an error.

```sql
IF EXISTS (SELECT *
      FROM sys.server_event_sessions
      WHERE name = 'YourSession')
BEGIN
    DROP EVENT SESSION YourSession
          ON SERVER;
END
GO
```

### Start and stop the event session using T-SQL

When you create an event session, the default is for it to not start running automatically. You can start or stop your event session anytime by using the following `ALTER EVENT SESSION` T-SQL statement.

```sql
ALTER EVENT SESSION [YourSession]
      ON SERVER
    STATE = START; -- STOP;
```

You have the option of configuring the event session to automatically start when the database engine instance is started. See the `STARTUP STATE = ON` keyword in `CREATE EVENT SESSION`.

SSMS UI offers a corresponding checkbox, **Start the event session at server startup**, on the **New Session > General** page.

## Test an event session

Test your event session with these steps:

1. In Object Explorer, right-click your event session node, and then select **Start Session**.
1. While connected to the same server (or the same database in Azure SQL Database) where you created the event session, run the following `SELECT...HAVING` statement a couple of times. Consider changing the value in the `HAVING` clause for each run, toggling between 2 and 3. This enables you to see the differences in the results.
1. Right-click your session node, and select **Stop Session**.
1. Read the next subsection about [how to SELECT and view the results](#select-the-full-results-xml-37).

```sql
SELECT c.name,
    COUNT(*) AS [Count-Per-Column-Repeated-Name]
FROM sys.syscolumns AS c
INNER JOIN sys.sysobjects AS o
    ON o.id = c.id
WHERE o.type = 'V'
    AND c.name LIKE '%event%'
GROUP BY c.name
HAVING Count(*) >= 3 --2     -- Try both values during session.
ORDER BY c.name;
```

For completeness, here's the example output from the preceding `SELECT...HAVING`.

```output
/* Approximate output, 6 rows, all HAVING Count >= 3:
name                   Count-Per-Column-Repeated-Name
---------------------  ------------------------------
event_group_type       4
event_group_type_desc  4
event_session_address  5
event_session_id       5
is_trigger_event       4
trace_event_id         3
*/
```

### <a id="select-the-full-results-xml-37"></a> View event session data as XML

In a query window in SSMS, run the following `SELECT` statement to see the event data captured by your session. Each row represents one event occurrence. The `CAST(... AS xml)` changes the data type of the column from **nvarchar** to **xml**. This lets you select the column value, to open it in a new window for easier reading.

> [!NOTE]  
> The `event_file` target always inserts a numeric part in the `xel` file name. Before you can run the following query, you must copy the actual full name of the `xel` file that includes this numeric part, and paste it into the `SELECT` statement. In the following example, the numeric part is `_0_131085363367310000`.

```sql
SELECT object_name,
    file_name,
    file_offset,
    event_data,
    'CLICK_NEXT_CELL_TO_BROWSE_XML RESULTS!' AS [CLICK_NEXT_CELL_TO_BROWSE_XML_RESULTS],
    CAST(event_data AS XML) AS [event_data_XML]
-- TODO: In the SSMS results grid, click this XML cell
FROM sys.fn_xe_file_target_read_file(
    'C:\Temp\YourSession_Target_0_131085363367310000.xel', NULL, NULL, NULL
);

```

This query provides two ways to view the full results of any given event row:

- Run the SELECT in SSMS, and then select a cell in the `event_data_XML` column.

- Copy the XML string from a cell in the `event_data` column. Paste into any text editor like Notepad, and save the file with extension `xml`. Then open the file in a browser or an editor capable of displaying XML data.

#### Event data in XML

Next we see part of the results, which are in XML format. The following XML is edited for brevity. `<data name="row_count">` displays a value of `6`, which matches our six result rows displayed earlier. And we can see the whole `SELECT` statement.

```xml
<event name="sql_statement_completed" package="sqlserver" timestamp="2016-05-24T04:06:08.997Z">
  <data name="duration">
    <value>111021</value>
  </data>
  <data name="cpu_time">
    <value>109000</value>
  </data>
  <data name="physical_reads">
    <value>0</value>
  </data>
  <data name="last_row_count">
    <value>6</value>
  </data>
  <data name="offset">
    <value>0</value>
  </data>
  <data name="offset_end">
    <value>584</value>
  </data>
  <data name="statement">
    <value>SELECT c.name,
            COUNT(*) AS [Count-Per-Column-Repeated-Name]
        FROM sys.syscolumns AS c
        INNER JOIN sys.sysobjects AS o
            ON o.id = c.id
        WHERE o.type = 'V'
            AND c.name LIKE '%event%'
        GROUP BY c.name
        HAVING Count(*) >= 3 --2     -- Try both values during session.
    ORDER BY c.name;</value>
      </data>
</event>
```

## Display event session data in SSMS

There are several advanced features in the SSMS UI you can use to view the data captured by an event session. For more information, see [View event data in SQL Server Management Studio](advanced-viewing-of-target-data-from-extended-events-in-sql-server.md).

You start with context menu options labeled **View Target Data** and **Watch Live Data**.

### View target data

In the SSMS Object Explorer, you can right-click the target node that is under your event session node. In the context menu, select **View Target Data**. SSMS displays the data.

The display isn't updated as new events occur in a session. But you can select **View Target Data** again.

:::image type="content" source="media/xevents-viewtargetdata-ssms-targetnode-61.png" alt-text="Screenshot of View Target Data, in SSMS, Management > Extended Events > Sessions > YourSession > package0.event_file, right-click." lightbox="media/xevents-viewtargetdata-ssms-targetnode-61.png":::

### Watch live data

In the SSMS Object Explorer, you can right-click your event session node. In the context menu, select **Watch Live Data**. SSMS displays incoming data as it continues to arrive in real time.

:::image type="content" source="media/xevents-watchlivedata-ssms-yoursessionnode-63.png" alt-text="Screenshot of Watch Live Data, in SSMS, Management > Extended Events > Sessions > YourSession, right-click." lightbox="media/xevents-watchlivedata-ssms-yoursessionnode-63.png":::

## Terms and concepts in Extended Events

The following table lists the terms used for Extended Events, and describes their meanings.

| Term | Description |
| :--- | :--- |
| `event session` | A construct centered around one or more events, plus supporting items like actions are targets. The `CREATE EVENT SESSION` statement creates each event session. You can `ALTER` an event session to start and stop it at will.<br /><br />An event session is sometimes referred to as just a *session*, when the context clarifies it means *event session*.<br /><br />Further details about event sessions are described in: [Extended Events sessions](sql-server-extended-events-sessions.md). |
| `event` | A specific occurrence in the system that is watched for by an active event session.<br /><br />For example, the `sql_statement_completed` event represents the moment that any given T-SQL statement completes. The event can report its duration and other data. |
| `target` | An item that receives the output data from a captured event. The target displays the data to you.<br /><br />Examples include the `event_file` target used earlier in this quick start, and the `ring_buffer` target that keeps the most recent events in memory.<br /><br />Any kind of target can be used for any event session. For details, see [Targets for Extended Events](targets-for-extended-events-in-sql-server.md). |
| `action` | A field known to the event. Data from the field is sent to the target. The action field is closely related to the *predicate filter*. |
| `predicate`, or filter | A test of data in an event field, used so that only an interesting subset of event occurrences are sent to the target.<br /><br />For example, a filter could include only those `sql_statement_completed` event occurrences where the T-SQL statement contained the string `HAVING`. |
| `package` | A name qualifier attached to each item in a set of items that centers around a core of events.<br /><br />For example, a package can have events about T-SQL text. One event could be about all the T-SQL in a batch. Meanwhile another narrower event is about individual T-SQL statements. Further, for any one T-SQL statement, there are `started` and `completed` events.<br /><br />Fields appropriate for the events are also in the package with the events. Most targets are in `package0` and are used with events from many other packages. |

## Extended Event scenarios and usage details

There are numerous scenarios for using Extended Events to monitor and troubleshoot the database engine and query workloads. The following articles provide examples using lock-related scenarios:

- [Find the Objects That Have the Most Locks Taken on Them](find-the-objects-that-have-the-most-locks-taken-on-them.md)
  - This scenario uses the [histogram](targets-for-extended-events-in-sql-server.md#histogram-target) target, which processes the raw event data before displaying it to you in a summarized (bucketized) form.
- [Determine Which Queries Are Holding Locks](determine-which-queries-are-holding-locks.md)
  - This scenario uses the [pair_matching](targets-for-extended-events-in-sql-server.md#pair_matching-target) target, where the pair of events is `sqlserver.lock_acquire` and `sqlserver.lock_release`.

### How to discover events available in packages

The following query returns a row for each available event whose name contains the three character string `sql`. You can edit the `LIKE` clause to search for different event names. The result set also identifies the package that contains the event.

```sql
SELECT -- Find an event you want.
    p.name AS [Package-Name],
    o.object_type,
    o.name AS [Object-Name],
    o.description AS [Object-Descr],
    p.guid AS [Package-Guid]
FROM sys.dm_xe_packages AS p
INNER JOIN sys.dm_xe_objects AS o
    ON p.guid = o.package_guid
WHERE o.object_type = 'event' --'action'  --'target'
    AND p.name LIKE '%'
    AND o.name LIKE '%sql%'
ORDER BY p.name,
    o.object_type,
    o.name;
```

The following result example shows the returned row, pivoted here into the format of `column name = value`. The data is from the `sql_statement_completed` event that was used in the preceding example steps. The description of the object (an event, in this example) serves as a documentation string.

```output
Package-Name = sqlserver
object_type  = event
Object-Name  = sql_statement_completed
Object-Descr = Occurs when a Transact-SQL statement has completed.
Package-Guid = 655FD93F-3364-40D5-B2BA-330F7FFB6491
```

### Find events using SSMS UI

Another option for finding events by name is to use the **New Session > Events > Event library** dialog that is shown in a preceding screenshot. You can type a partial event name and find all matching event names.

### SQL Trace event classes

A description of using Extended Events with SQL Trace event classes and columns is available at: [View the Extended Events Equivalents to SQL Trace Event Classes](view-the-extended-events-equivalents-to-sql-trace-event-classes.md).

### Event Tracing for Windows (ETW)

Descriptions of using Extended Events with Event Tracing for Windows (ETW) are available at:

- [Event Tracing for Windows target](event-tracing-for-windows-target.md)
- [Monitor System Activity Using Extended Events](monitor-system-activity-using-extended-events.md)

### System event sessions

In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and Azure SQL Managed Instance, several system event sessions are created by default and configured to start when the database engine is started. Like most event sessions, they consume a small amount of resources and don't materially affect workload performance. Microsoft recommends that these sessions remain enabled and running. The health sessions, particularly the [system_health](use-the-system-health-session.md) session, are often useful for monitoring and troubleshooting.

You can see these event sessions in the SSMS Object Explorer under **Management > Extended Events > Sessions**. For example, in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], these system event sessions are:

- `AlwaysOn_health`
- `system_health`
- `telemetry_events`

### PowerShell provider

You can manage Extended Events by using the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] PowerShell provider. For more information, see [Use the PowerShell Provider for Extended Events](use-the-powershell-provider-for-extended-events.md).

### System views

The system views for Extended Events include:

- *Catalog views:* for information about event sessions defined by `CREATE EVENT SESSION`.
- *Dynamic management views (DMVs):* for information about active (started) event sessions.

[SELECTs and JOINs From System Views for Extended Events in SQL Server](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md) provides information about:

- How to join the views
- Several useful queries based on these views
- The correlation between:
  - View columns
  - `CREATE EVENT SESSION` clauses
  - The SSMS UI

## <a id="appendix1"></a> Appendix: Queries to find Extended Event permission holders

The permissions mentioned in this article are:

- `ALTER ANY EVENT SESSION`
- `VIEW SERVER STATE`
- `CONTROL SERVER`

The following `SELECT...UNION ALL` statement returns rows that show who has the necessary permissions for creating event sessions and querying the system catalog views for Extended Events.

```sql
-- Ascertain who has the permissions listed in the ON clause.
-- 'CONTROL SERVER' permission includes the permissions
-- 'ALTER ANY EVENT SESSION' and 'VIEW SERVER STATE'.
SELECT 'Owner-is-Principal' AS [Type-That-Owns-Permission],
    NULL AS [Role-Name],
    prin.name AS [Owner-Name],
    PERM.permission_name COLLATE Latin1_General_CI_AS_KS_WS AS [Permission-Name]
FROM sys.server_permissions AS PERM
INNER JOIN sys.server_principals AS prin
    ON prin.principal_id = PERM.grantee_principal_id
WHERE PERM.permission_name IN (
    'ALTER ANY EVENT SESSION',
    'VIEW SERVER STATE',
    'CONTROL SERVER'
)
UNION ALL
-- Plus check for members of the 'sysadmin' fixed server role,
-- because 'sysadmin' includes the 'CONTROL SERVER' permission.
SELECT 'Owner-is-Role',
    prin.name, -- [Role-Name]
    CAST((IsNull(pri2.name, N'No members')) AS NVARCHAR(128)),
    NULL
FROM sys.server_role_members AS rolm
RIGHT JOIN sys.server_principals AS prin
    ON prin.principal_id = rolm.role_principal_id
LEFT JOIN sys.server_principals AS pri2
    ON rolm.member_principal_id = pri2.principal_id
WHERE prin.name = 'sysadmin';
```

### HAS_PERMS_BY_NAME function

The following `SELECT` statement reports your permissions. It relies on the built-in function [HAS_PERMS_BY_NAME](../../t-sql/functions/has-perms-by-name-transact-sql.md).

Further, if you have the authority to temporarily *impersonate* other logins, you can uncomment the [EXECUTE AS LOGIN](../../t-sql/statements/execute-as-transact-sql.md) and `REVERT` statements, to see if other logins hold the `ALTER ANY EVENT SESSION` permission.

```sql
--EXECUTE AS LOGIN = 'LoginNameHere';
SELECT HAS_PERMS_BY_NAME(NULL, NULL, 'ALTER ANY EVENT SESSION');
--REVERT;
```

## Related content

- [Extended Events overview](extended-events.md)
- [Extended Events sessions](sql-server-extended-events-sessions.md)
- [Targets for Extended Events](targets-for-extended-events-in-sql-server.md)
