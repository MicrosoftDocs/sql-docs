---
title: Execute statements against multiple servers simultaneously
description: "Execute statements against multiple servers at the same time."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/21/2022
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "multiserver queries"
  - "executing queries against multiple servers"
  - "queries [SQL Server], multiserver"
---
# Execute statements against multiple servers simultaneously

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to query multiple servers at the same time in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], by creating a local server group, or a Central Management Server and one or more server groups, and one or more registered servers within the groups, and then querying the complete group.

The results returned by the query can be combined into a single results pane, or can be returned in separate results panes. The result set can include additional columns for the server name and the login used by the query on each server. Central Management Servers and subordinate servers can be registered by using only Windows Authentication. Servers in local server groups can be registered by using Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication.

Before you execute the following procedures, create a Central Management Server and server group. For more information, see [Create a Central Management Server and Server Group (SQL Server Management Studio)](./create-a-central-management-server-and-server-group.md).

## Permissions

Because the connections maintained by a Central Management Server execute in the context of the user, by using Windows Authentication, the effective permissions on the registered servers might vary. For example, the user might be a member of the **sysadmin** fixed server role on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] A, but have limited permissions on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] B.

## Execute statements against multiple configuration targets simultaneously

1. In SQL Server Management Studio, on the **View** menu, select **Registered Servers**.

1. Expand a Central Management Server, right-click a server group, point to **Connect**, and then select **New Query**.

1. In Query Editor, type and execute a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, such as the following:

    ```sql
    USE master
    GO
    SELECT * FROM sys.databases;
    GO
    ```

   By default, the results pane will combine the query results from all the servers in the server group.

#### Change the multiserver results options

1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], on the **Tools** menu, select **Options**.

1. Expand **Query Results**, expand **SQL Server**, and then select **Multiserver Results**.

1. On the **Multiserver Results** page, specify the option settings that you want, and then select **OK**.

## See also

- [Administer Multiple Servers Using Central Management Servers](../../relational-databases/administer-multiple-servers-using-central-management-servers.md)
