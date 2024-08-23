---
title: "sysmail_stop_sp (Transact-SQL)"
description: "Stops Database Mail by stopping the Service Broker objects that the external program uses."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_stop_sp_TSQL"
  - "sysmail_stop_sp"
helpviewer_keywords:
  - "sysmail_stop_sp"
dev_langs:
  - "TSQL"
---
# sysmail_stop_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stops Database Mail by stopping the [!INCLUDE [ssSB](../../includes/sssb-md.md)] objects that the external program uses.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_stop_sp
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Remarks

This stored procedure is in the `msdb` database.

`sysmail_stop_sp` stops the Database Mail queue that holds outgoing message requests and turns off [!INCLUDE [ssSB](../../includes/sssb-md.md)] activation for the external program.

When the queues are stopped, the Database Mail external program doesn't process messages. This stored procedure allows you to stop Database Mail for troubleshooting or maintenance purposes.

To start Database Mail, use `sysmail_start_sp`. `sp_send_dbmail` still accepts mail when the [!INCLUDE [ssSB](../../includes/sssb-md.md)] objects are stopped.

> [!NOTE]  
> `sysmail_stop_sp` only stops the queues for Database Mail. This stored procedure doesn't deactivate [!INCLUDE [ssSB](../../includes/sssb-md.md)] message delivery in the database. This stored procedure doesn't disable the Database Mail extended stored procedures to reduce the surface area. To disable the extended stored procedures, see the [Database Mail XPs (server configuration option)](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md) of the `sp_configure` system stored procedure.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example shows stopping Database Mail in the `msdb` database. The example assumes that Database Mail has been enabled.

```sql
USE msdb;
GO

EXECUTE dbo.sysmail_stop_sp;
GO
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [sysmail_start_sp (Transact-SQL)](sysmail-start-sp-transact-sql.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
