---
title: Database Engine events and errors
description: Consult this MSSQL error code list to find explanations for error messages for SQL Server database engine events.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: supportability
ms.topic: reference
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi || >=aps-pdw-2016-au7 || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Database Engine events and errors

This section contains error message numbers and their descriptions, which are taken from the text of the error message in the `sys.messages` catalog view. Where applicable, the error number is a link to further information.

You can query the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] to see a full list of all errors, by running the following query against the `sys.messages` catalog view:

```sql
SELECT message_id AS Error,
    severity AS Severity,
    [Event Logged] = CASE is_event_logged
        WHEN 0 THEN 'No' ELSE 'Yes'
        END,
    [text] AS [Description]
FROM sys.messages
WHERE language_id = 1040 /* replace 1040 with the desired language ID, such as 1033 for US English */
ORDER BY message_id;
```

## SQL Server version

::: moniker range="=sql-server-2016"
This article shows events and errors for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2022](?view=sql-server-ver16&preserve-view=true)
- [SQL Server 2019](?view=sql-server-ver15&preserve-view=true)
- [SQL Server 2017](?view=sql-server-2017&preserve-view=true)
:::moniker-end

::: moniker range="=sql-server-2017 || =sql-server-linux-2017"
This article shows events and errors for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2022](?view=sql-server-ver16&preserve-view=true)
- [SQL Server 2019](?view=sql-server-ver15&preserve-view=true)
- [SQL Server 2016](?view=sql-server-2016&preserve-view=true)
:::moniker-end

::: moniker range="=sql-server-ver15 || =sql-server-linux-ver15"
This article shows events and errors for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2022](?view=sql-server-ver16&preserve-view=true)
- [SQL Server 2017](?view=sql-server-2017&preserve-view=true)
- [SQL Server 2016](?view=sql-server-2016&preserve-view=true)
:::moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresql || =azuresql-db || =azuresql-mi || >=aps-pdw-2016-au7"
This article shows events and errors for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2019](?view=sql-server-ver15&preserve-view=true)
- [SQL Server 2017](?view=sql-server-2017&preserve-view=true)
- [SQL Server 2016](?view=sql-server-2016&preserve-view=true)
:::moniker-end

## Errors and events

Visit the following sections for more detail about error and event codes:

- [Errors 0 to 999](database-engine-events-and-errors-0-to-999.md)
- [Errors 1000 to 1999](database-engine-events-and-errors-1000-to-1999.md)
- [Errors 2000 to 2999](database-engine-events-and-errors-2000-to-2999.md)
- [Errors 3000 to 3999](database-engine-events-and-errors-3000-to-3999.md)
- [Errors 4000 to 4999](database-engine-events-and-errors-4000-to-4999.md)
- [Errors 5000 to 5999](database-engine-events-and-errors-5000-to-5999.md)
- [Errors 6000 to 6999](database-engine-events-and-errors-6000-to-6999.md)
- [Errors 7000 to 7999](database-engine-events-and-errors-7000-to-7999.md)
- [Errors 8000 to 8999](database-engine-events-and-errors-8000-to-8999.md)
- [Errors 9000 to 9999](database-engine-events-and-errors-9000-to-9999.md)
- [Errors 10000 to 10999](database-engine-events-and-errors-10000-to-10999.md)
- [Errors 11000 to 12999](database-engine-events-and-errors-11000-to-12999.md)
- [Errors 13000 to 13999](database-engine-events-and-errors-13000-to-13999.md)
- [Errors 14000 to 14999](database-engine-events-and-errors-14000-to-14999.md)
- [Errors 15000 to 15999](database-engine-events-and-errors-15000-to-15999.md)
- [Errors 16000 to 17999](database-engine-events-and-errors-16000-to-17999.md)
- [Errors 18000 to 18999](database-engine-events-and-errors-18000-to-18999.md)
- [Errors 19000 to 20999](database-engine-events-and-errors-19000-to-20999.md)
- [Errors 21000 to 21999](database-engine-events-and-errors-21000-to-21999.md)
- [Errors 22000 to 22999](database-engine-events-and-errors-22000-to-22999.md)
- [Errors 23000 to 25999](database-engine-events-and-errors-23000-to-25999.md)
- [Errors 26000 to 27999](database-engine-events-and-errors-26000-to-27999.md)
- [Errors 28000 to 30999](database-engine-events-and-errors-28000-to-30999.md)
- [Errors 31000 to 41399](database-engine-events-and-errors-31000-to-41399.md)
- [Errors 41400 to 42999](database-engine-events-and-errors-41400-to-42999.md)

## Related content

- [Understanding Database Engine errors](../../relational-databases/errors-events/understanding-database-engine-errors.md)
- [Cause and resolution of Database Engine errors](/previous-versions/sql/sql-server-2016/ms365262(v=sql.130))
