---
title: "Paging through a query result"
description: Provides an example of viewing the results of a query as pages of data.
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
# Paging through a query result

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Paging through a query result is the process of returning the results of a query in smaller subsets of data, or pages. This is a common practice for displaying results to a user in small, easy-to-manage chunks.

The <xref:Microsoft.Data.SqlClient.SqlDataAdapter> provides a facility for returning only a page of data, through overloads of the <xref:System.Data.Common.DbDataAdapter.Fill%2A> method. However, this might not be the best choice for paging through large query results because, although the **DataAdapter** fills the target <xref:System.Data.DataTable> or <xref:System.Data.DataSet> with only the requested records, the resources to return the entire query are still used.

To return a page of data from a data source without using the resources to return the entire query, specify additional criteria for your query that reduce the rows returned to only those required.

To use the <xref:System.Data.Common.DbDataAdapter.Fill%2A> method to return a page of data, specify a **startRecord** parameter, for the first record in the page of data, and a **maxRecords** parameter, for the number of records in the page of data.

## Example

The following code example shows how to use the <xref:System.Data.Common.DbDataAdapter.Fill%2A> method to return the first page of a query result where the page size is five records.

[!code-csharp[SqlDataAdapter_Paging#1](~/../sqlclient/doc/samples/SqlDataAdapter_Paging.cs#1)]

In the previous example, the **DataSet** is only filled with five records, but the entire **Orders** table is returned. To fill the **DataSet** with those same five records, but only return five records, use the `TOP` and `WHERE` clauses in your SQL statement, as in the following code example.

[!code-csharp[SqlDataAdapter_Paging#2](~/../sqlclient/doc/samples/SqlDataAdapter_Paging.cs#2)]

> [!NOTE]
> When paging through the query results in this way, you must preserve the `unique identifier` that orders the rows, in order to pass the unique ID to the command to return the next page of records, as shown in the following code example.

[!code-csharp[SqlDataAdapter_Paging#4](~/../sqlclient/doc/samples/SqlDataAdapter_Paging.cs#4)]

To return the `next page of records` using the overload of the **Fill** method that takes the **startRecord** and **maxRecords** parameters, increment the current record index by the page size and fill the table.

> [!NOTE]
> Remember that the database server returns the entire query results even though only one page of records is added to the **DataSet**.

In the following code example, the table rows are cleared before they are filled with the next page of data. You might want to preserve a certain number of returned rows in a local cache to reduce trips to the database server.

[!code-csharp[SqlDataAdapter_Paging#3](~/../sqlclient/doc/samples/SqlDataAdapter_Paging.cs#3)]

To return the next page of records without having the database server return the entire query, specify restrictive criteria to the SELECT statement. Because the preceding example preserved the last record returned, you can use it in the WHERE clause to specify a starting point for the query, as shown in the following code example.

[!code-csharp[SqlDataAdapter_Paging#5](~/../sqlclient/doc/samples/SqlDataAdapter_Paging.cs#5)]

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
