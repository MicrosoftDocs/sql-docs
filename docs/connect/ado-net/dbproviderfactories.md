---
title: "DbProviderFactories"
description: Describes the provider factory model and demonstrates how to use the base classes in the `System.Data.Common` namespace.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: "12/22/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---
# DbProviderFactories

[!INCLUDE [dotnet-all](../../includes/products/applies-full/dotnet-all.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The <xref:System.Data.Common> namespace provides classes for creating <xref:System.Data.Common.DbProviderFactory> instances to work with specific data sources. When you create a <xref:System.Data.Common.DbProviderFactory> instance and pass it information about the data provider, the `DbProviderFactory` can determine the correct, strongly typed connection object to return based on the information it has been provided.  

The data provider <xref:Microsoft.Data.SqlClient> is no longer listed in machine.config file, but custom providers will continue to be listed there.  

## In this section  

[Obtain a SqlClientFactory](obtain-sqlclientfactory.md)  
Demonstrates how to obtain a `SqlClientFactory` from the `DbProviderFactories` class to work with specific data sources in .NET.  

## Related content

- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft.Data.SqlClient for SQL Server](microsoft-ado-net-sql-server.md)
