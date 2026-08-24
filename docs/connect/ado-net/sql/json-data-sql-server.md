---
title: "JSON data type support in SqlClient"
description: "Describes how to work with JSON data retrieved from SQL Server."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 10/31/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---
# JSON datatype support in SqlClient

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

A native [JSON data type](../../../t-sql/data-types/json-data-type.md) is introduced in SQL Server 2025 and Azure SQL databases. This native JSON data type is available for structured JSON documents with automatic validation, efficient transport, and efficient storage.

The Microsoft.Data.SqlClient provider offers built-in support for JSON columns through:
- The common language runtime (CLR) `string` type for simple scenarios.
- `SqlJson` type for advanced use cases, such as client-side validation and explicit handling of JSON semantics.
- .NET 9 introduces a new SqlDbType.Json enumeration value for specifying JSON parameters. For earlier .NET versions, use `Microsoft.Data.SqlDbTypeExtensions`, which provides forward-compatible support for new SQL types.
- .NET 9 introduces a new `SqlDbType.Json` enumeration value for specifying JSON parameters.
- For earlier .NET versions, `Microsoft.Data.SqlDbTypeExtensions` is introduced as an alternative for `SqlDbType`.

## Example

The following example demonstrates the following scenarios to interact with JSON data.

- Insert a JSON value using CLR type `string`.
- Insert the JSON value using `SqlJson`. Using `SqlJson` is useful if you want to validate the JSON data before sending it to server.
- Insert a JSON file by streaming.
- Read JSON values using `SqlDataReader`.
- Use stored procedure parameters for JSON data.

[!code-csharp[SqlJsonExample#1](~/../sqlclient/doc/samples/SqlJsonExample.cs#1)]
  
## Related content

- [SQL Server and ADO.NET](index.md)
