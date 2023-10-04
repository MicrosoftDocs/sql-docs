---
title: "Diagnostic counters in SqlClient"
description: Use Microsoft SqlClient Data Provider for SQL Server diagnostic counters to monitor your application status and its connection resources.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-deshtehari
ms.date: "05/31/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Diagnostic counters in SqlClient

[!INCLUDE [appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

You can use <xref:Microsoft.Data.SqlClient> diagnostic counters in multiple target frameworks to monitor the status of your application and the connection resources that it uses. Use `performance counters` in .NET Framework, and `event counters` in .NET Core and .NET Standard.

> [!NOTE]
> When using Windows Authentication (integrated security), you must monitor either the pair `number-of-active-connection-pool-groups` and `number-of-active-connection-pools` event counters or the `NumberOfActiveConnectionPoolGroups` and `NumberOfActiveConnectionPools` performance counters. The reason is that connection pool groups map to unique connection strings. When integrated security is used, connection pools map to connection strings and additionally create separate pools for individual Windows identities. For example, if Fred and Julie, each within the same AppDomain, both use the connection string `"Data Source=MySqlServer;Integrated Security=true"`, a connection pool group is created for the connection string, and two additional pools are created, one for Fred and one for Julie. If John and Martha use a connection string with an identical SQL Server login, `"Data Source=MySqlServer;User Id=<myUserID>;Password=<myPassword>"`, then only a single pool is created for the **\<myUserID\>** identity.

## In this section

[Performance counters in SqlClient](performance-counters.md)  
Use Microsoft SqlClient Data Provider for SQL Server performance counters to monitor your application status and its connection resources by using Windows Performance Monitor or programmatically in `.NET Framework`.

[Event counters in SqlClient](event-counters.md)  
Use Microsoft SqlClient Data Provider for SQL Server event counters to monitor your application status and its connection resources in `.NET Core` and `.NET Standard`.

## See also

- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
