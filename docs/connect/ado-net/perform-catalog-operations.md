---
title: "Performing catalog operations"
description: Describes how to execute commands that modify database schema.
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
# Performing catalog operations

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

To execute a command to modify a database or catalog, such as the CREATE TABLE or CREATE PROCEDURE statement, create a **Command** object using the appropriate SQL statements and a **Connection** object. Execute the command with the <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteNonQuery%2A> method of the <xref:Microsoft.Data.SqlClient.SqlCommand> object.

## Example

The following code example creates a stored procedure in a Microsoft SQL Server database.

[!code-csharp[DataWorks SqlCommand.ExecuteNonQuery#3](~/../sqlclient/doc/samples/SqlCommand_ExecuteNonQuery_SP_DML.cs#3)]

## See also

- [Using commands to modify data](use-commands-to-modify-data.md)
- [Commands and parameters](commands-parameters.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
