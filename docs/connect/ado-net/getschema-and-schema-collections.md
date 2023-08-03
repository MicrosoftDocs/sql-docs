---
title: "Get schema and schema collections"
description: "Describes using GetSchema method to retrieve and restrict schema information from a database."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/26/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Get schema and schema collections

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The **SqlConnection** classes in the Microsoft SqlClient Data Provider for SQL Server implements a **GetSchema** method which is used to retrieve schema information about the database that is currently connected, and the schema information returned from the **GetSchema** method comes in the form of a <xref:System.Data.DataTable>. The **GetSchema** method is an overloaded method that provides optional parameters for specifying the schema collection to return, and restricting the amount of information returned.

## Specifying the schema collections

The first optional parameter of the **GetSchema** method is the collection name which is specified as a string. There are two types of schema collections: common schema collections that are common to all providers, and specific schema collections which are specific to each provider.  

You can query the Microsoft SqlClient Data Provider for SQL Server to determine the list of supported schema collections by calling the **GetSchema** method with no arguments, or with the schema collection name "MetaDataCollections". This will return a <xref:System.Data.DataTable> with a list of the supported schema collections, the number of restrictions that they each support, and the number of identifier parts that they use.  

### Retrieving schema collections example

The following examples demonstrate how to use the <xref:Microsoft.Data.SqlClient.SqlConnection.GetSchema%2A> method of the Microsoft SqlClient Data Provider for SQL Server <xref:Microsoft.Data.SqlClient.SqlConnection> class to retrieve schema information about all of the tables contained in the **AdventureWorks** sample database:  

[!code-csharp[SqlClient GetSchema#1](~/../sqlclient/doc/samples/SqlConnection_GetSchema_Tables.cs#1)]  

## See also

- [Retrieving database schema information](retrieving-database-schema-information.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
