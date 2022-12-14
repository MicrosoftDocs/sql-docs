---
title: "Connecting to data source"
description: Learn about Connection objects, used to connect to data sources in ADO.NET. The Connection object you choose depends on the type of data source.
author: David-Engel
ms.author: v-davidengel
ms.date: "11/13/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connecting to a data source in ADO.NET

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

In the Microsoft SqlClient data provider, you use a **Connection** object to connect to a specific data source by supplying necessary authentication information in a connection string. The **Connection** object you use depends on the type of data source.

The Microsoft SqlClient Data Provider for SQL Server includes a <xref:Microsoft.Data.SqlClient.SqlConnection> type that is derived from a <xref:System.Data.Common.DbConnection> class.

## In this section  

[Establishing the Connection](establishing-connection.md)\
Describes how to use a **Connection** object to establish a connection to a data source.

[Connection Events](connection-events.md)\
Describes how to use an **InfoMessage** event to retrieve informational messages from a data source.

## See also

- [Connection strings](connection-strings.md)
- [Connection pooling](connection-pooling.md)
- [Commands and parameters](commands-parameters.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Transactions and concurrency](transactions-and-concurrency.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
