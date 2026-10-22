---
title: "Server Configuration: Database Mail XPs"
description: "Learn about the Database Mail XPs option. View different ways of turning on this option so that you can use Database Mail in SQL Server."
author: adbadram
ms.author: adbadram
ms.date: 05/19/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "Database Mail XPs option"
  - "Database Mail [SQL Server], enabling"
---
# Server configuration: Database Mail XPs

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `Database Mail XPs` option to enable Database Mail on this server. The possible values are:

- `0` (default): Database Mail isn't available.
- `1`: Database Mail is available.

The setting takes effect immediately without a server stop and restart.

After enabling Database Mail, you must configure a Database Mail host database to use Database Mail.

When you configure Database Mail using the **Database Mail Configuration Wizard**, you enable the Database Mail extended stored procedures in the `msdb` database. If you use the **Database Mail Configuration Wizard**, you don't have to use the `sp_configure` example shown later in this article.

Setting the `Database Mail XPs` option to `0` prevents Database Mail from starting. If it's running when the option is set to `0`, it continues to run and send mail until it's idle for the time configured in the `DatabaseMailExeMinimumLifeTime` option.

## Examples

The following example enables the Database Mail extended stored procedures.

```sql
EXECUTE sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

EXECUTE sp_configure 'Database Mail XPs', 1;
GO
RECONFIGURE;
GO
```

The following example enables the Database Mail extended stored procedures if it isn't already enabled.

```sql
IF EXISTS (SELECT 1
           FROM sys.configurations
           WHERE NAME = 'Database Mail XPs'
                 AND VALUE = 0)
    BEGIN
        PRINT 'Enabling Database Mail XPs';
        EXECUTE sp_configure 'show advanced options', 1;
        RECONFIGURE;
        EXECUTE sp_configure 'Database Mail XPs', 1;
        RECONFIGURE;
    END
```

## Related content

- [Database Mail](../../relational-databases/database-mail/database-mail.md)
