---
title: "DataAdapters and DataReaders"
description: Learn about the Microsoft SqlClient Data Provider for SQL Server DataReader, which retrieves data from a database, and DataAdapter, which retrieves data from a data source and populates a DataSet.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# DataAdapters and DataReaders

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

You can use the Microsoft SqlClient Data Provider for SQL Server **DataReader** to retrieve a read-only, forward-only stream of data from a database. Results are returned as the query executes, and are stored in the network buffer on the client until you request them using the **Read** method of the **DataReader**. Using the **DataReader** can increase application performance both by retrieving data as soon as it is available, and (by default) storing only one row at a time in memory, reducing system overhead.

A <xref:System.Data.Common.DataAdapter> is used to retrieve data from a data source and populate tables within a <xref:System.Data.DataSet>. The `DataAdapter` also resolves changes made to the `DataSet` back to the data source. The `DataAdapter` uses the `Connection` object of the Microsoft SqlClient Data Provider for SQL Server to connect to a data source, and it uses `Command` objects to retrieve data from and resolve changes to the data source.

.NET has a <xref:System.Data.Common.DbDataReader> and a <xref:System.Data.Common.DbDataAdapter> object: the Microsoft SqlClient Data Provider for SQL Server includes a <xref:Microsoft.Data.SqlClient.SqlDataReader> and a <xref:Microsoft.Data.SqlClient.SqlDataAdapter> object.

## In this section

[Retrieve data by a DataReader](retrieve-data-by-datareader.md)  
Describes the ADO.NET **DataReader** object and how to use it to return a stream of results from a data source.

[Populate a DataSet from a DataAdapter](populate-dataset-from-dataadapter.md)  
Describes how to fill a `DataSet` with tables, columns, and rows by using a `DataAdapter`.

[DataAdapter parameters](dataadapter-parameters.md)  
Describes how to use parameters with the command properties of a `DataAdapter` including how to map the contents of a column in a `DataSet` to a command parameter.

[Add existing constraints to a DataSet](add-existing-constraints-to-dataset.md)  
Describes how to add existing constraints to a `DataSet`.

[DataAdapter, DataTable, and DataColumn mappings](dataadapter-datatable-datacolumn-mappings.md)  
Describes how to set up `DataTableMappings` and `ColumnMappings` for a `DataAdapter`.

[Paging through a query result](paging-through-query-result.md)  
Provides an example of viewing the results of a query as pages of data.

[Update data sources with DataAdapters](update-data-sources-with-dataadapters.md)  
Describes how to use a `DataAdapter` to resolve changes in a `DataSet` back to the database.

[Handle DataAdapter events](handle-dataadapter-events.md)  
Describes `DataAdapter` events and how to use them.

[Batch operations using DataAdapters](batch-operations-using-dataadapters.md)  
Describes enhancing application performance by reducing the number of round trips to SQL Server when applying updates from the `DataSet`.

## See also

- [Connecting to a data source](connecting-to-data-source.md)
- [Commands and parameters](commands-parameters.md)
- [Transactions and concurrency](transactions-and-concurrency.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
