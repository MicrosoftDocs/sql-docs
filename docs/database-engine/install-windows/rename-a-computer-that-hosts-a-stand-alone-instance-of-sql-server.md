---
title: "Rename a computer that hosts a stand-alone instance of SQL Server"
description: When you rename a computer that hosts an instance of SQL Server, update the system metadata stored in sys.servers.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/28/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "remote login errors [SQL Server]"
  - "standalone computer names [SQL Server]"
  - "names [SQL Server], standalone instances of SQL Server"
  - "renaming standalone instances of SQL Server"
  - "sysservers system table"
  - "removing remote logins"
  - "deleting remote logins"
  - "dropping remote logins"
monikerRange: ">=sql-server-2016"
---
# Rename a computer that hosts a stand-alone instance of SQL Server

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

When you change the name of the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the new name is recognized during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] startup. You don't have to run Setup again to reset the computer name. Instead, use the following steps to update system metadata that is stored in `sys.servers` and reported by the system function `@@SERVERNAME`. Update system metadata to reflect computer name changes for remote connections and applications that use `@@SERVERNAME`, or that query the server name from `sys.servers`.

The following steps can't be used to rename an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. They can be used only to rename the part of the instance name that corresponds to the computer name. For example, you can change a computer named MB1 that hosts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] named Instance1 to another name, such as MB2. However, the instance part of the name, Instance1, will remain unchanged. In this example, the `\\<ComputerName>\<InstanceName>` would be changed from `\\MB1\Instance1` to `\\MB2\Instance1`.

## Prerequisites

Before you begin the renaming process, review the following information:

- When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is part of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster, the computer renaming process differs from a computer that hosts a stand-alone instance. For more information, see [Rename a SQL Server Failover Cluster Instance](../../sql-server/failover-clusters/install/rename-a-sql-server-failover-cluster-instance.md).

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support renaming computers that are involved in replication, except when you use log shipping with replication. The secondary computer in log shipping can be renamed if the primary computer is permanently lost. For more information, see [Log Shipping and Replication (SQL Server)](../../database-engine/log-shipping/log-shipping-and-replication-sql-server.md).

- When you rename a computer that is configured to use [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] might not be available after the computer name change. For more information, see [Rename a Report Server Computer](../../reporting-services/report-server/rename-a-report-server-computer.md).

- When you rename a computer that is configured to use database mirroring, you must turn off database mirroring before the renaming operation. Then, re-establish database mirroring with the new computer name. Metadata for database mirroring won't be updated automatically to reflect the new computer name. Use the following steps to update system metadata.

- Users who connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through a Windows group that uses a hard-coded reference to the computer name might not be able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This can occur after the rename if the Windows group specifies the old computer name. To ensure that such Windows groups have [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connectivity following the renaming operation, update the Windows group to specify the new computer name.

You can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the new computer name after you have restarted [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To ensure that `@@SERVERNAME` returns the updated name of the local server instance, you should manually run the following procedure that applies to your scenario. The procedure you use depends on whether you are updating a computer that hosts a default or named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## Rename a computer that hosts a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

- For a renamed computer that hosts a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], run the following procedures:

  ```sql
  EXEC sp_dropserver '<old_name>';
  GO
  EXEC sp_addserver '<new_name>', local;
  GO
  ```

  Restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- For a renamed computer that hosts a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], run the following procedures:

  ```sql
  EXEC sp_dropserver '<old_name\instancename>';
  GO
  EXEC sp_addserver '<new_name\instancename>', local;
  GO
  ```

  Restart the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

## After the rename operation

After a computer has been renamed, any connections that used the old computer name must connect by using the new name.

## Verify rename operation

- Select information from either `@@SERVERNAME` or `sys.servers`. The `@@SERVERNAME` function will return the new name, and the `sys.servers` table will show the new name. The following example shows the use of `@@SERVERNAME`.

  ```sql
  SELECT @@SERVERNAME AS 'Server Name';
  ```

## Additional considerations

 **Remote logins** - If the computer has any remote logins, running `sp_dropserver` might generate an error similar to the following:

 `Server: Msg 15190, Level 16, State 1, Procedure sp_dropserver, Line 44 There are still remote logins for the server 'SERVER1'.`

 To resolve the error, you must drop remote logins for this server.

### Drop remote logins

- For a default instance, run the following procedure:

  ```sql
  EXEC sp_dropremotelogin old_name;
  GO
  ```

- For a named instance, run the following procedure:

  ```sql
  EXEC sp_dropremotelogin old_name\instancename;
  GO
  ```

 **Linked server configurations** - Linked server configurations will be affected by the computer renaming operation. Use `sp_addlinkedserver` or `sp_setnetname` to update computer name references. For more information, see the [sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md) or [sp_setnetname (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-setnetname-transact-sql.md).

 **Client alias names** - Client aliases that use named pipes will be affected by the computer renaming operation. For example, if an alias "PROD_SRVR" was created to point to SRVR1 and uses the named pipes protocol, the pipe name will look like `\\SRVR1\pipe\sql\query`. After the computer is renamed, the path of the named pipe will no longer be valid. For more information about named pipes, see the [Creating a Valid Connection String Using Named Pipes](/previous-versions/sql/sql-server-2008/ms189307(v=sql.100)).

## See also

- [Install SQL Server](../../database-engine/install-windows/install-sql-server.md)
