---
title: "View or Configure Remote Server Connection Options (SQL Server)"
description: Learn how to view or configure remote server connection options at the server level. You can use SQL Server Management Studio or Transact-SQL for this purpose.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/26/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "remote servers [SQL Server], connection options"
  - "servers [SQL Server], remote"
  - "connections [SQL Server], remote servers"
---
# View or configure remote server connection options (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to view or configure remote server connection options at the server level in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]

## Permissions

Executing `sp_serveroption` requires `ALTER ANY LINKED SERVER` permission on the server.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

### View or configure remote server connection options

1. In Object Explorer, right-click a server, and then select **Properties**.

1. In the **SQL Server Properties - \<**_server_name_**>** dialog box, select **Connections**.

1. On the **Connections** page, review the **Remote server connections** settings, and modify them if necessary.

1. Repeat steps 1 through 3 on the other server of the remote server pair.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

### View remote server connection options

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the **Standard** bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example uses [sp_helpserver](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md) to return information about all remote servers.

```sql
USE master;
GO

EXECUTE sp_helpserver;
```

### Configure remote server connection options

1. Connect to the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. From the **Standard** bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This example shows how to use [sp_serveroption](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md) to configure a remote server. The example configures a remote server corresponding to another instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], `SEATTLE3`, to be collation compatible with the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
USE master;

EXECUTE sp_serveroption 'SEATTLE3', 'collation compatible', 'true';
```

## Follow up: After you configure remote server connection options

The remote server must be stopped and restarted before the setting can take effect.

## Related content

- [Server configuration: remote access](configure-the-remote-access-server-configuration-option.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [Remote servers](remote-servers.md)
- [Linked servers (Database Engine)](../../relational-databases/linked-servers/linked-servers-database-engine.md)
- [sp_linkedservers (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-linkedservers-transact-sql.md)
- [sys.sp_helpserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helpserver-transact-sql.md)
- [sys.sp_serveroption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)
