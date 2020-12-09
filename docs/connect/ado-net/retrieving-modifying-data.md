---
title: "Retrieving and modifying data"
description: In .NET, Microsoft SqlClient Data Provider for SQL Server serves as a bridge between an application and a data source to read and update data.
ms.date: "11/30/2020"
ms.assetid: 722e7f87-3691-46c6-87e8-7d159722d675
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-chmalh
---
# Retrieving and modifying data in ADO.NET

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

A primary function of any database application is connecting to a data source and retrieving the data that it contains. The SqlClient data provider serves as a bridge between an application and a data source, allowing you to execute commands as well as to retrieve data by using a **DataReader** or a **DataAdapter**. A key function of any database application is the ability to update the data that is stored in the database. In the Microsoft SqlClient Data Provider for SQL Server, updating data involves using the **DataAdapter** and <xref:System.Data.DataSet>, and **Command** objects; and it may also involve using transactions.

## In this section

[Connecting to a data source](connecting-to-data-source.md)  
Describes how to establish a connection to a data source and how to work with connection events.

[Connection strings](connection-strings.md)  
Contains topics describing various aspects of using connection strings, including connection string keywords, security info, and storing and retrieving them.

[Connection pooling](connection-pooling.md)  
Describes connection pooling for the Microsoft SqlClient Data Provider for SQL Server.

[Commands and Parameters](commands-parameters.md)  
Contains topics describing how to create commands and command builders, configure parameters, and how to execute commands to retrieve and modify data.

[DataAdapters and DataReaders](dataadapters-datareaders.md)  
Contains topics describing DataReaders, DataAdapters, parameters, handling DataAdapter events and performing batch operations.

[Transactions and concurrency](transactions-and-concurrency.md)
Contains topics describing how to perform local transactions, distributed transactions, and work with optimistic concurrency.

[Retrieving database schema information](retrieving-database-schema-information.md)
Describes how to obtain available databases or catalogs, tables and views in a database, constraints that exist for tables, and other schema information from a data source.

## See also

- [Data type mappings in ADO.NET](data-type-mappings-ado-net.md)
- [SQL Server and ADO.NET](./sql/index.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
