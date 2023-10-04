---
title: "Connection pooling"
description: Learn about connection pooling, an optimization technique that ADO.NET uses to minimize the cost of opening connections to data sources.
author: David-Engel
ms.author: v-davidengel
ms.date: "11/13/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connection pooling

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Connecting to a data source can be time consuming. To minimize the cost of opening connections, ADO.NET uses an optimization technique called *connection pooling*, which minimizes the cost of repeatedly opening and closing connections.

## In this section  

[SQL Server Connection Pooling (ADO.NET)](sql-server-connection-pooling.md)  
Provides an overview of connection pooling and describes how connection pooling works in SQL Server.

## See also

- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
