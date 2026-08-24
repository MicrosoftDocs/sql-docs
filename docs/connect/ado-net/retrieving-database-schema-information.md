---
title: "Retrieving database schema information"
description: "Learn about using Microsoft SqlClient Data Provider for SQL Server to retrieve database schema information."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: "11/26/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---
# Retrieving database schema information

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Obtaining schema information from a database is accomplished with the process of schema discovery. Schema discovery allows applications to request that managed providers find and return information about the database schema, also known as *metadata*, of a given database. Different database schema elements such as tables, columns, and stored-procedures are exposed through schema collections. Each schema collection contains a variety of schema information specific to the provider being used.

The Microsoft SqlClient Data Provider for SQL Server implements the **GetSchema** method in the **SqlConnection** class, and the schema information that is returned from the **GetSchema** method comes in the form of a <xref:System.Data.DataTable>. The **GetSchema** method is an overloaded method that provides optional parameters for specifying the schema collection to return, and restricting the amount of information returned. The SqlClient data provider also provides a **GetSchemaTable** method that returns a DataTable describing the column metadata of the **SqlDataReader**.

## In this section

[GetSchema and schema collections](getschema-and-schema-collections.md)  
Describes the **GetSchema** method and how it can be used to retrieve and restrict schema information from a database.

[Schema restrictions](schema-restrictions.md)  
Describes schema restrictions that can be used with **GetSchema**. 

[Common schema collections](common-schema-collections.md)  
Describes all of the common schema collections supported by all of the .NET managed providers.  
  
[SQL Server schema collections](sql-server-schema-collections.md)  
Describes the additional schema collections supported by the Microsoft SqlClient Data Provider for SQL Server. 

## Related content

- <xref:System.Data.Common.DbConnection.GetSchema%2A>
- <xref:System.Data.Common.DbConnection>
- <xref:Microsoft.Data.SqlClient.SqlConnection.GetSchema%2A>
- <xref:Microsoft.Data.SqlClient.SqlConnection>
- <xref:System.Data.Common.DbDataReader.GetSchemaTable%2A>
- <xref:System.Data.Common.DbDataReader>
- <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSchemaTable%2A>
- <xref:Microsoft.Data.SqlClient.SqlDataReader>
- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
