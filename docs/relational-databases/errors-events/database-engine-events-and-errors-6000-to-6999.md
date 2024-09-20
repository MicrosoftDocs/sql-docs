---
title: "Database Engine events and errors (6000 to 6999)"
description: "Consult this SQL Server error code list (between 6000 and 6999) to find explanations for error messages for SQL Server database engine events."
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: supportability
ms.topic: reference
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi || >=aps-pdw-2016-au7 || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Database Engine events and errors (6000 to 6999)

This article contains error message numbers (between the range 6000 and 6999) and their description, which is the text of the error message from the `sys.messages` catalog view. Where applicable, the error number is a link to further information.

For the full range of error numbers, see the list on [Database Engine events and errors](database-engine-events-and-errors.md#errors-and-events).

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
This article shows events and errors (between the range 6000 and 6999) for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2022](?view=sql-server-ver16&preserve-view=true)
- [SQL Server 2019](?view=sql-server-ver15&preserve-view=true)
- [SQL Server 2017](?view=sql-server-2017&preserve-view=true)
:::moniker-end

::: moniker range="=sql-server-2017 || =sql-server-linux-2017"
This article shows events and errors (between the range 6000 and 6999) for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2022](?view=sql-server-ver16&preserve-view=true)
- [SQL Server 2019](?view=sql-server-ver15&preserve-view=true)
- [SQL Server 2016](?view=sql-server-2016&preserve-view=true)
:::moniker-end

::: moniker range="=sql-server-ver15 || =sql-server-linux-ver15"
This article shows events and errors (between the range 6000 and 6999) for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2022](?view=sql-server-ver16&preserve-view=true)
- [SQL Server 2017](?view=sql-server-2017&preserve-view=true)
- [SQL Server 2016](?view=sql-server-2016&preserve-view=true)
:::moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresql || =azuresql-db || =azuresql-mi || >=aps-pdw-2016-au7"
This article shows events and errors (between the range 6000 and 6999) for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. If you want to view events and errors for other versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see:

- [SQL Server 2019](?view=sql-server-ver15&preserve-view=true)
- [SQL Server 2017](?view=sql-server-2017&preserve-view=true)
- [SQL Server 2016](?view=sql-server-2016&preserve-view=true)
:::moniker-end

## Errors and events (6000 to 6999)

::: moniker range="=sql-server-2016"
[!INCLUDE [sql-server-2016-database-engine-events-and-errors-6000-6999](includes/sql-server-2016-database-engine-events-and-errors-6000-6999.md)]
:::moniker-end

::: moniker range="=sql-server-2017 || =sql-server-linux-2017"
[!INCLUDE [sql-server-2017-database-engine-events-and-errors-6000-6999](includes/sql-server-2017-database-engine-events-and-errors-6000-6999.md)]
:::moniker-end

::: moniker range="=sql-server-ver15 || =sql-server-linux-ver15"
[!INCLUDE [sql-server-2019-database-engine-events-and-errors-6000-6999](includes/sql-server-2019-database-engine-events-and-errors-6000-6999.md)]
:::moniker-end

::: moniker range=">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresql || =azuresql-db || =azuresql-mi || >=aps-pdw-2016-au7"
[!INCLUDE [sql-server-2022-database-engine-events-and-errors-6000-6999](includes/sql-server-2022-database-engine-events-and-errors-6000-6999.md)]
:::moniker-end

## Related content

- [Database Engine events and errors](database-engine-events-and-errors.md)
- [Understanding Database Engine errors](../../relational-databases/errors-events/understanding-database-engine-errors.md)
- [Cause and resolution of Database Engine errors](/previous-versions/sql/sql-server-2016/ms365262(v=sql.130))
