---
title: "sysmail_start_sp (Transact-SQL)"
description: "Starts Database Mail by starting the Service Broker objects that the external program uses."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysmail_start_sp"
  - "sysmail_start_sp_TSQL"
helpviewer_keywords:
  - "sysmail_start_sp"
dev_langs:
  - "TSQL"
---
# sysmail_start_sp (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Starts Database Mail by starting the [!INCLUDE [ssSB](../../includes/sssb-md.md)] objects that the external program uses.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sysmail_start_sp
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Database Mail isn't enabled or installed upon [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation. Use the Database Mail Configuration wizard to enable and install the Database Mail objects.

This stored procedure is in the `msdb` database. This stored procedure starts the Database Mail queue that holds outgoing message requests and enables the [!INCLUDE [ssSB](../../includes/sssb-md.md)] activation for the external program.

When the queues are started, the Database Mail external program can process messages. This procedure allows you to restart the queues after the queues have been stopped with the `sysmail_stop_sp` stored procedure.

> [!NOTE]  
> This stored procedure only starts the queues for Database Mail. This stored procedure doesn't activate [!INCLUDE [ssSB](../../includes/sssb-md.md)] message delivery in the database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

## Examples

The following example shows starting Database Mail in the `msdb` database. The example assumes that Database Mail has been enabled.

```sql
USE msdb;
GO

EXECUTE dbo.sysmail_start_sp;
GO
```

## Related content

- [Database Mail](../database-mail/database-mail.md)
- [Database Mail XPs (server configuration option)](../../database-engine/configure-windows/database-mail-xps-server-configuration-option.md)
- [sysmail_stop_sp (Transact-SQL)](sysmail-stop-sp-transact-sql.md)
- [Database Mail stored procedures (Transact-SQL)](database-mail-stored-procedures-transact-sql.md)
