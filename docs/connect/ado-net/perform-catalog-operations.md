---
title: "Performing catalog operations"
description: Describes how to execute commands that modify database schema.
ms.date: "11/25/2020"
dev_langs: 
  - "csharp"
ms.assetid: e60f542f-6271-495b-a9e4-48553481c2a3
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
author: David-Engel
ms.author: v-daenge
ms.reviewer: v-chmalh
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
