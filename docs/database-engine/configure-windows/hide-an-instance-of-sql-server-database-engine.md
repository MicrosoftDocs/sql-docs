---
title: Hide an Instance of SQL Server Database Engine
description: Find out how to hide an instance of the SQL Server Database Engine. Client computers can't use the SQL Server Browser service to locate hidden instances.
author: adbadram
ms.author: adbadram
ms.date: 10/20/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "Database Engine [SQL Server], hiding instances"
  - "hiding instances of Database Engine"
---
# Hide an instance of SQL Server Database Engine

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to hide an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using SQL Server Configuration Manager. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser service to enumerate instances of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] installed on the computer.

This option enables client applications to browse for a server, and helps clients distinguish between multiple instances of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] on the same computer. You can use the following procedure to prevent the SQL Server Browser service from exposing an instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to client computers that try to locate the instance by using the **Browse** button.

<a id="SSMSProcedure"></a>

## Use SQL Server Configuration Manager

1. In **SQL Server Configuration Manager**, expand **SQL Server Network Configuration**, right-click **Protocols for** *\<server instance>*, and then select **Properties**.

1. On the **Flags** tab, in the **HideInstance** box, select **Yes**, and then select **OK** to close the dialog box. The change takes effect immediately for new connections.

## Remarks

If you hide a named instance, you need to provide the port number in the connection string to connect to the hidden instance, even if the browser service is running. We recommend that you use a static port instead of a dynamic port for the named hidden instance.

For more information, see [Configure SQL Server to listen on a specific TCP port](configure-a-server-to-listen-on-a-specific-tcp-port.md).

### Clustering

If you hide a clustered instance or availability group name, cluster service might not be able to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This causes the cluster instance **IsAlive** check to fail and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] goes offline.

To avoid this scenario, create an alias in all the nodes of the clustered instance or all instances that host availability group replicas to reflect the static port that you configured for the instance. For example, on an availability group with two replicas, on node-one, create an alias for the node-two instance, like `node-two\instancename`. On node-two, create an alias called `node-one\instancename`. The aliases are required for successful failover.

For more information, see [Create or delete a server alias for use by a client](create-or-delete-a-server-alias-for-use-by-a-client.md).

If you hide a clustered named instance, cluster service might not be able to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] if the `LastConnect` registry key (`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSSQLServer\Client\SNI11.0\LastConnect`) has a different port than the port that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is listening on.

If the cluster service is unable to make a connection to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you might see an error similar to:

```output
Event ID: 1001: Event Name: Failover clustering resource deadlock.
```

## Related content

- [Server network configuration](server-network-configuration.md)
- [How to: Enable Kerberos Authentication on a SQL Server Failover Cluster](/previous-versions/sql/sql-server-2008-r2/ms189585(v=sql.105))
