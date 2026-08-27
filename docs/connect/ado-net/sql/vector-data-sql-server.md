---
title: "Vector datatype support in SqlClient"
description: "Describes how to work with vector datatype for SQL Server using SqlClient."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, paulmedynski, cmalhotra
ms.date: 10/31/2025
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
---
# Vector data type support in SqlClient

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Server 2025 and Azure SQL Database introduce a native [Vector data type](../../../t-sql/data-types/vector-data-type.md) for storing fixed-dimension numeric embeddings with automatic validation and efficient binary storage. For backward compatibility, vectors are exposed as JSON arrays. Each element is float32 by default, and dimensions must range from 1 to 1998.

In .NET, the `Microsoft.Data.SqlClient` provider adds native vector handling through the <xref:Microsoft.Data.SqlTypes.SqlVector`1> class (for example, `SqlVector<float>`), using optimized binary transport over the Tabular Data Stream (TDS) protocol. Older clients can still pass vectors as JSON strings transparently.

The vector type enables similarity search and AI-driven scenarios, with server-side vector functions for efficient computation.

In SqlClient, `vector` data is exchanged with SQL Server using the <xref:Microsoft.Data.SqlTypes.SqlVector`1> class.

In newer .NET versions starting with version 10, a new `SqlDbType` enumeration value `Vector` is available for representing SQL parameters of type `vector`.

For earlier versions, use `Microsoft.Data.SqlDbTypeExtensions` to declare SQL parameters of type `vector`.

## Example

The following code sample demonstrates the following scenarios for a vector column of three dimensions.

- Insert non-null vectors using SqlParameter.
- Insert null vectors for a given vector column.
- Read a vector column.
- Use a stored procedure with a vector data type parameter.
- Perform a bulk copy operation with vector data.

[!code-csharp[SqlVectorExample#1](~/../sqlclient/doc/samples/SqlVectorExample.cs#1)]

## Related content

- [SQL Server and ADO.NET](index.md)
