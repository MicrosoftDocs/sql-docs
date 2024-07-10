---
title: "Microsoft ADO.NET"
description: "Microsoft ADO.NET, for SQL Server and Azure SQL, is the core data access technology for .NET languages. Use the Microsoft.Data.SqlClient library to access SQL Server."


author: David-Engel
ms.author: davidengel
ms.reviewer: v-kaywon
ms.date: "05/06/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Microsoft ADO.NET for SQL Server and Azure SQL Database

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

ADO.NET is the core data access technology for .NET languages. Use the Microsoft.Data.SqlClient library or Entity Framework to access SQL Server, or providers from other suppliers to access their stores. Use System.Data.Odbc or System.Data.OleDb to access data from .NET languages using other data access technologies. Use System.Data.DataSet when you need an offline data cache in client applications. It also provides local persistence and XML capabilities that can be useful in web services.



::: moniker range=">=sql-server-2016||>=sql-server-linux-2017"

## Getting started (SQL Server)

* [Step 1: Configure development environment for ADO.NET development](step-1-configure-development-environment-ado-net-development.md)
* [Step 2: Create a SQL database for ADO.NET development](step-2-create-sql-database-ado-net-development.md)
* [Step 3: Proof of concept connecting to SQL using ADO.NET](step-3-connect-sql-ado-net.md)
* [Step 4: Connect resiliently to SQL with ADO.NET](step-4-connect-resiliently-sql-ado-net.md)

::: moniker-end

::: moniker range="=azuresqldb-current"

## Getting started (Azure SQL Database)

* [Step 1: Configure development environment for ADO.NET development](step-1-configure-development-environment-ado-net-development.md)
* [Step 2: Create a SQL database for ADO.NET development](/azure/azure-sql/database/single-database-create-quickstart)
* [Step 3: Proof of concept connecting to SQL using ADO.NET](step-3-connect-sql-ado-net.md)
* Step 4: Connect resiliently to SQL with ADO.NET
   * [Entity Framework Core with a passwordless connection](/azure/azure-sql/database/azure-sql-dotnet-entity-framework-core-quickstart)
   * [Microsoft.Data.SqlClient with a passwordless connection](/azure/azure-sql/database/azure-sql-dotnet-quickstart)
   * [Microsoft.Data.SqlClient with a password](step-4-connect-resiliently-sql-ado-net.md)


::: moniker-end

## Documentation

* [ADO.NET Overview](/dotnet/framework/data/adonet/)
* [Getting started with the SqlClient driver](get-started-sqlclient-driver.md)
* [Overview of the SqlClient driver](overview-sqlclient-driver.md)
* [Data type mappings in ADO.NET](data-type-mappings-ado-net.md)
* [Retrieving and modifying data in ADO.NET](retrieving-modifying-data.md)
* [SQL Server and ADO.NET](./sql/index.md)

## Community

* [ADO.NET Managed Providers Forum](https://social.msdn.microsoft.com/Forums/home?forum=adodotnetdataproviders)
* [ADO.NET DataSet Forum](https://social.msdn.microsoft.com/Forums/home?forum=adodotnetdataset)

## More samples

* [ADO.NET Code Examples](/dotnet/framework/data/adonet/ado-net-code-examples)

