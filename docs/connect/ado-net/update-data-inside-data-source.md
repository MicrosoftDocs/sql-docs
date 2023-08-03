---
title: "Updating data in a data source"
description: Describes how to execute commands or stored procedures that modify data in a database.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/25/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Updating data in a data source

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

SQL statements that modify data (such as INSERT, UPDATE, or DELETE) do not return rows. Similarly, many stored procedures perform an action but do not return rows. To execute commands that do not return rows, create a **Command** object with the appropriate SQL command and a **Connection**, including any required **Parameters**. Execute the command with the <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteNonQuery%2A> method of the <xref:Microsoft.Data.SqlClient.SqlCommand> object.

> [!NOTE]
> The **ExecuteNonQuery** method returns an integer that represents the number of rows affected by the statement or stored procedure that was executed. If multiple statements are executed, the value returned is the sum of the records affected by all of the statements executed.

## Example

The following code example executes an INSERT statement to insert a record into a database using **ExecuteNonQuery**.
  
[!code-csharp[DataWorks SqlCommand.ExecuteNonQuery#1](~/../sqlclient/doc/samples/SqlCommand_ExecuteNonQuery_SP_DML.cs#1)]

The following code example executes the stored procedure created by the sample code in [Performing Catalog Operations](perform-catalog-operations.md). No rows are returned by the stored procedure, so the **ExecuteNonQuery** method is used, but the stored procedure does receive an input parameter and returns an output parameter and a return value.

[!code-csharp[DataWorks SqlCommand.ExecuteNonQuery#2](~/../sqlclient/doc/samples/SqlCommand_ExecuteNonQuery_SP_DML.cs#2)]

## See also

- [Using commands to modify data](use-commands-to-modify-data.md)
- [Update data sources with DataAdapters](update-data-sources-with-dataadapters.md)
- [Commands and parameters](commands-parameters.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
