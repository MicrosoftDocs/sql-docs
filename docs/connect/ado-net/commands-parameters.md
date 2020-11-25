---
title: "Commands and Parameters"
description: Learn how to use Command objects for Microsoft SqlClient Data Provider for SQL Server to run commands and return results from a data source.
ms.date: "11/25/2020"
ms.assetid: b623f810-d871-49a5-b0f5-078cc3c34db6
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-chmalh
---
# Commands and Parameters

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

After establishing a connection to a data source, you can execute commands and return results from the data source using a <xref:System.Data.Common.DbCommand> object. You can create a command using one of the command constructors for the Microsoft SqlClient Data Provider for SQL Server. Constructors can take optional arguments, such as an SQL statement to execute at the data source, a <xref:System.Data.Common.DbConnection> object, or a <xref:System.Data.Common.DbTransaction> object. You can also configure those objects as properties of the command. You can also create a command for a particular connection using the <xref:System.Data.Common.DbConnection.CreateCommand%2A> method of a `DbConnection` object. The SQL statement being executed by the command can be configured using the <xref:System.Data.Common.DbCommand.CommandText%2A> property. The Microsoft SqlClient Data Provider for SQL Server has the <xref:Microsoft.Data.SqlClient.SqlCommand> object.

## In This Section

[Executing a Command](execute-command.md)
Describes the ADO.NET `Command` object and how to use it to execute queries and commands against a data source.

[Configuring Parameters and Parameter Data Types](configure-parameters-parameter-data-types.md)
Describes working with `Command` parameters, including direction, data types, and parameter syntax.

[Generating Commands with CommandBuilders](generate-commands-with-commandbuilders.md)  
Describes how to use command builders to automatically generate INSERT, UPDATE, and DELETE commands for a `DataAdapter` that has a single-table SELECT command.

[Obtaining a Single Value from a Database](obtain-single-value-from-database.md)
Describes how to use the `ExecuteScalar` method of a `Command` object to return a single value from a database query.

[Using Commands to Modify Data](use-commands-to-modify-data.md)
Describes how to use the Microsoft SqlClient data provider for SQL Server to execute stored procedures or data definition language (DDL) statements.

## See also

- [Connecting to a Data Source](connecting-to-data-source.md)
