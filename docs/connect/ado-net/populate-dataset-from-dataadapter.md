---
title: "Populate a DataSet from a DataAdapter"
description: Learn how to populate a DataSet from a DataAdapter in ADO.NET, which provides a consistent relational programming model independent of the data source.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Populate a DataSet from a DataAdapter

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The ADO.NET <xref:System.Data.DataSet> is a memory-resident representation of data that provides a consistent relational programming model independent of the data source. The `DataSet` represents a complete set of data that includes tables, constraints, and relationships among the tables. Because the `DataSet` is independent of the data source, a `DataSet` can include data local to the application, and data from multiple data sources. Interaction with existing data sources is controlled through the `DataAdapter`.

The `SelectCommand` property of the `DataAdapter` is a `Command` object that retrieves data from the data source. The `InsertCommand`, `UpdateCommand`, and `DeleteCommand` properties of the `DataAdapter` are `Command` objects that manage updates to the data in the data source according to modifications made to the data in the `DataSet`. These properties are covered in more detail in [Update Data Sources with DataAdapters](update-data-sources-with-dataadapters.md).

The `Fill` method of the `DataAdapter` is used to populate a `DataSet` with the results of the `SelectCommand` of the `DataAdapter`. `Fill` takes as its arguments a `DataSet` to be populated, and a `DataTable` object, or the name of the `DataTable` to be filled with the rows returned from the `SelectCommand`.

> [!NOTE]
> Using the `DataAdapter` to retrieve all of a table takes time, especially if there are many rows in the table. This is because accessing the database, locating and processing the data, and then transferring the data to the client is time-consuming. Pulling all of the table to the client also locks all of the rows on the server. To improve performance, you can use the `WHERE` clause to greatly reduce the number of rows returned to the client. You can also reduce the amount of data returned to the client by only explicitly listing required columns in the `SELECT` statement. Another good workaround is to retrieve the rows in batches (such as several hundred rows at a time) and only retrieve the next batch when the client is finished with the current batch.

The `Fill` method uses the `DataReader` object implicitly to return the column names and types that are used to create the tables in the `DataSet`, and the data to populate the rows of the tables in the `DataSet`. Tables and columns are only created if they do not already exist; otherwise `Fill` uses the existing `DataSet` schema. Column types are created as .NET Framework types according to the tables in [Data Type Mappings in ADO.NET](data-type-mappings-ado-net.md). Primary keys are not created unless they exist in the data source and `DataAdapter`**.**`MissingSchemaAction` is set to `MissingSchemaAction`**.**`AddWithKey`. If `Fill` finds that a primary key exists for a table, it will overwrite data in the `DataSet` with data from the data source for rows where the primary key column values match those of the row returned from the data source. If no primary key is found, the data is appended to the tables in the `DataSet`. `Fill` uses any mappings that may exist when you populate the `DataSet` (see [DataAdapter, DataTable, and DataColumn Mappings](dataadapter-datatable-datacolumn-mappings.md)).

> [!NOTE]
> If the `SelectCommand` returns the results of an OUTER JOIN, the `DataAdapter` does not set a `PrimaryKey` value for the resulting `DataTable`. You must define the `PrimaryKey` yourself to make sure that duplicate rows are resolved correctly.

The following code example creates an instance of a <xref:Microsoft.Data.SqlClient.SqlDataAdapter> that uses a <xref:Microsoft.Data.SqlClient.SqlConnection> to the Microsoft SQL Server `Northwind` database and populates a <xref:System.Data.DataTable> in a `DataSet` with the list of customers. The SQL statement and <xref:Microsoft.Data.SqlClient.SqlConnection> arguments passed to the <xref:Microsoft.Data.SqlClient.SqlDataAdapter> constructor are used to create the <xref:Microsoft.Data.SqlClient.SqlDataAdapter.SelectCommand%2A> property of the <xref:Microsoft.Data.SqlClient.SqlDataAdapter>.

## Example

[!code-csharp[SqlDataAdapter_FillDataSet#1](~/../sqlclient/doc/samples/SqlDataAdapter_FillDataSet.cs#1)]

> [!NOTE]
> The code shown in this example does not explicitly open and close the `Connection`. The `Fill` method implicitly opens the `Connection` that the `DataAdapter` is using if it finds that the connection is not already open. If `Fill` opened the connection, it also closes the connection when `Fill` is finished. This can simplify your code when you deal with a single operation such as a `Fill` or an `Update`. However, if you are performing multiple operations that require an open connection, you can improve the performance of your application by explicitly calling the `Open` method of the `Connection`, performing the operations against the data source, and then calling the `Close` method of the `Connection`. You should try to keep connections to the data source open as briefly as possible to free resources for use by other client applications.

## Multiple result sets

If the `DataAdapter` encounters multiple result sets, it creates multiple tables in the `DataSet`. The tables are given an incremental default name of Table*N*, starting with "Table" for Table0. If a table name is passed as an argument to the `Fill` method, the tables are given an incremental default name of TableName*N*, starting with "TableName" for TableName0.  
  
## Populate a DataSet from multiple DataAdapters  

Any number of `DataAdapter` objects can be used with a `DataSet`. Each `DataAdapter` can be used to fill one or more `DataTable` objects and resolve updates back to the relevant data source. `DataRelation` and `Constraint` objects can be added to the `DataSet` locally, which enables you to relate data from dissimilar data sources. For example, a `DataSet` can contain data from a Microsoft SQL Server database, an IBM DB2 database exposed through OLE DB, and a data source that streams XML. One or more `DataAdapter` objects can handle communication to each data source.  
  
### Example  

The following code example populates a list of customers from the `Northwind` database on Microsoft SQL Server, and a list of orders from the `Northwind` database stored in Microsoft Access. The filled tables are related with a `DataRelation`, and the list of customers is then displayed with the orders for that customer.

[!code-csharp[SqlDataAdapter_FillDataSet#2](~/../sqlclient/doc/samples/SqlDataAdapter_FillDataSet.cs#2)]

## SQL Server Decimal type

By default, the `DataSet` stores data by using .NET data types. For most applications, these provide a convenient representation of data source information. However, this representation may cause a problem when the data type in the data source is a SQL Server decimal or numeric data type. The .NET `decimal` data type allows a maximum of 28 significant digits, whereas the SQL Server `decimal` data type allows 38 significant digits. If the `SqlDataAdapter` determines during a `Fill` operation that the precision of a SQL Server `decimal` field is larger than 28 characters, the current row is not added to the `DataTable`. Instead the `FillError` event occurs, which enables you to determine whether a loss of precision will occur, and respond appropriately. For more information about the `FillError` event, see [Handle DataAdapter Events](handle-dataadapter-events.md). To get the SQL Server `decimal` value, you can also use a <xref:Microsoft.Data.SqlClient.SqlDataReader> object and call the <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlDecimal%2A> method.

ADO.NET also includes enhanced support for <xref:System.Data.SqlTypes> in the `DataSet`. For more information, see [SqlTypes and the DataSet](./sql/sqltypes-dataset.md).

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Data type mappings in ADO.NET](data-type-mappings-ado-net.md)
- [Multiple Active Result Sets (MARS)](./sql/multiple-active-result-sets-mars.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
