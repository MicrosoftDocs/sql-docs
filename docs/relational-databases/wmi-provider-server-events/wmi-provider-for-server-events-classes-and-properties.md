---
title: "WMI Provider for Server Events classes and properties"
description: "WMI Provider for Server Events classes and properties."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/30/2023
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "event classes [WMI]"
  - "WMI Provider for Server Events, events listed"
  - "classes [WMI]"
---
# WMI Provider for Server Events classes and properties

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

There are two main categories of events that make up the programming model for the WMI Provider for Server Events, which can be queried by issuing WQL queries against the provider. These are data definition language (DDL) events, and trace events. The `QUEUE_ACTIVATION` and `BROKER_QUEUE_DISABLED` service broker events can also be queried.

## Events and event groups

To get a full list of server events, query the `sys.event_notification_event_types` catalog view with the following Transact-SQL script.

```sql
; WITH EventsCTE (Child, Level, Hierarchy)
AS (
    SELECT t.[type],
        0,
        CAST(t.[type_name] AS NVARCHAR(MAX))
    FROM sys.event_notification_event_types t
    WHERE t.parent_type IS NULL

    UNION ALL

    SELECT t.[type],
        Level + 1,
        CAST(rc.Hierarchy + '/' + cast(t.[type_name] AS NVARCHAR(MAX)) AS NVARCHAR(MAX))
    FROM sys.event_notification_event_types t
    INNER JOIN EventsCTE rc
        ON t.parent_type = rc.Child
    )
SELECT Level, Hierarchy
FROM EventsCTE
WHERE Level > 0
ORDER BY Hierarchy;
```

## Remarks

The `DDL_ASSEMBLY_EVENTS` event, for example, includes any `ALTER_ASSEMBLY`, `CREATE_ASSEMBLY`, and `DROP_ASSEMBLY` event. Similarly, the `TRC_FULL_TEXT` event includes any `FT_CRAWL_ABORTED`, `FT_CRAWL_STARTED`, and `FT_CRAWL_STOPPED` event. `ALL_EVENTS` covers all DDL events, trace events, `QUEUE_ACTIVATION`, and `BROKER_QUEUE_DISABLED`.

To learn which properties can be queried from an event or event group, refer to the event schema. By default, the event schema is installed in the following directory:

> [!INCLUDE [ssInstallPath](../../includes/ssinstallpath-md.md)]Tools\Binn\schemas\sqlserver\2006\11\events\events.xsd

For example, by referring to the `ALTER_DATABASE` event, its parent event is `DDL_SERVER_LEVEL_EVENTS` and its properties are `TSQLCommand` and `DatabaseName`. The event also inherits the properties `SQLInstance`, `PostTime`, `ComputerName`, `SPID`, and `LoginName`. The event has no children events.

> [!NOTE]  
> System stored procedures that perform DDL-like operations can also fire event notifications. Test your event notifications to determine their responses to system stored procedures that are run. For example, the `CREATE TYPE` statement and `sp_addtype` stored procedure will both fire an event notification that is created on a `CREATE_TYPE` event. For more information, see [DDL Events](../triggers/ddl-events.md).

## Related content

- [WMI Provider for Server Events concepts](wmi-provider-for-server-events-concepts.md)
- [Use WQL with the WMI Provider for Server Events](using-wql-with-the-wmi-provider-for-server-events.md)
