---
title: "Sign in to an Instance of SQL Server (Command Prompt)"
description: "Learn about the sqlcmd utility. See how to use it in a command prompt to test connectivity to an instance of SQL Server."
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "logins [SQL Server], named instance of SQL Server"
  - "log ins [SQL Server]"
  - "logins [SQL Server], default instance of SQL Server"
  - "command prompt [SQL Server], logins"
  - "logging in [SQL Server]"
---
# Sign in to an instance of SQL Server (Command Prompt)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to test connectivity to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], using the **sqlcmd** utility.

## Sign in to the default instance of SQL Server

From a command prompt, enter the following command to connect by using Windows Authentication:

```console
sqlcmd [/E] [/S servername]
```

## Sign in to a named instance of SQL Server

From a command prompt, enter the following command to connect by using Windows Authentication:

```console
sqlcmd [/E] /S servername\instancename
```

## Related content

- [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md)
- [osql Utility](../../tools/osql-utility.md)
