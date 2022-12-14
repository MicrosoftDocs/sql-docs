---
title: "Data type mappings"
description: "Describes the data types are used by Microsoft SqlClient Data Provider for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "11/13/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Data type mappings in ADO.NET

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The ADO.NET is based on the common type system, which defines how types are declared, used, and managed in the runtime. It consists of both value types and reference types, which all derive from the <xref:System.Object> base type. When working with a data source, the data type is inferred from the data provider if it is not explicitly specified. For example, a <xref:System.Data.DataSet> object is independent of any specific data source. Data in a `DataSet` is retrieved from a data source, and changes are persisted back to the data source by using a `DataAdapter`. This program flow means that when a `DataAdapter` fills a <xref:System.Data.DataTable> in a `DataSet` with values from a data source, the resulting data types of the columns in the `DataTable` are .NET Framework types, instead of types specific to the Microsoft SqlClient Data Provider for SQL Server that is used to connect to the data source.

Likewise, when a `DataReader` returns a value from a data source, the resulting value is stored in a local variable that has a .NET Framework type. For both the `Fill` operations of the `DataAdapter` and the `Get` methods of the `DataReader`, the .NET Framework type is inferred from the value returned from the Microsoft SqlClient Data Provider for SQL Server.

Instead of relying on the inferred data type, you can use the typed accessor methods of the `DataReader` when you know the specific type of the value being returned. Typed accessor methods give you better performance by returning a value as a specific .NET Framework type, which eliminates the need for additional type conversion.

> [!NOTE]
> Null values for Microsoft SqlClient Data Provider for SQL Server data types are represented by `DBNull.Value`.

## In This Section

[SQL Server Data Type Mappings](sql-server-data-type-mappings.md)
Lists inferred data type mappings and data accessor methods for <xref:Microsoft.Data.SqlClient>.

[Floating-Point Numbers](floating-point-numbers.md)
Describes issues that developers frequently encounter when working with floating-point numbers.

## See also

- [SQL Server data types and ADO.NET](./sql/sql-server-data-types.md)
- [Configuring parameters](configure-parameters.md)
- [Retrieving database schema information](retrieving-database-schema-information.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
