---
title: "DbProviderFactories"
description: Describes the provider factory model and demonstrates how to use the base classes in the `System.Data.Common` namespace.
ms.date: "12/22/2020"
ms.assetid: 2a8e2640-3a49-42a1-a3a9-b43026907ae1
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-chmalh
---
# DbProviderFactories

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The <xref:System.Data.Common> namespace provides classes for creating <xref:System.Data.Common.DbProviderFactory> instances to work with specific data sources. When you create a <xref:System.Data.Common.DbProviderFactory> instance and pass it information about the data provider, the `DbProviderFactory` can determine the correct, strongly typed connection object to return based on the information it has been provided.  

The data provider <xref:Microsoft.Data.SqlClient> is no longer listed in machine.config file, but custom providers will continue to be listed there.  

## In this section  

[Obtain a SqlClientFactory](obtain-sqlclientfactory.md)  
Demonstrates how to obtain a `SqlClientFactory` from the `DbProviderFactories` class to work with specific data sources in .NET.  

## See also

- [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
