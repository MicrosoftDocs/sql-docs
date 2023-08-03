---
title: "Obtaining a single value from a database"
description: Learn how to return a single value in ADO.NET. This example code returns the identity column value for an inserted record.
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
# Obtaining a single value from a database

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

You may need to return database information that is simply a single value rather than in the form of a table or data stream. For example, you may want to return the result of an aggregate function such as COUNT(\*), SUM(Price), or AVG(Quantity). The **Command** object provides the capability to return single values using the **ExecuteScalar** method. The **ExecuteScalar** method returns, as a scalar value, the value of the first column of the first row of the result set.

## Example

The following code example inserts a new value in the database using a <xref:Microsoft.Data.SqlClient.SqlCommand>. The <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteScalar%2A> method is used to return the identity column value for the inserted record.

[!code-csharp[DataWorks SqlCommand.ExecuteScalar#1](~/../sqlclient/doc/samples/SqlCommand_ExecuteScalar_Return_Id.cs#1)]

## See also

- [Commands and parameters](commands-parameters.md)
- [Executing a command](execute-command.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
