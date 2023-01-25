---
title: Configuring parameters
description: Command objects use parameters to pass values to SQL statements or stored procedures, providing type checking and validation in ADO.NET.
author: David-Engel
ms.author: v-davidengel
ms.date: 11/25/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
ms.custom: event-tier1-build-2022
dev_langs:
  - "csharp"
---
# Configuring parameters

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

Command objects use parameters to pass values to SQL statements or stored procedures, providing type checking and validation. Unlike command text, parameter input is treated as a literal value, not as executable code. This behavior helps guard against "SQL injection" attacks, in which an attacker inserts a command that compromises security on the server into an SQL statement.

Parameterized commands can also improve query execution performance, because they help the database server accurately match the incoming command with a proper cached query plan. For more information, see [Execution Plan Caching and Reuse](../../relational-databases/query-processing-architecture-guide.md#execution-plan-caching-and-reuse) and [Parameters and Execution Plan Reuse](../../relational-databases/query-processing-architecture-guide.md#parameters-and-execution-plan-reuse). In addition to the security and performance benefits, parameterized commands provide a convenient method for organizing values passed to a data source.

A <xref:System.Data.Common.DbParameter> object can be created by using its constructor, or by adding it to the <xref:System.Data.Common.DbCommand.DbParameterCollection%2A> by calling the `Add` method of the <xref:System.Data.Common.DbParameterCollection> collection. The `Add` method will take as input either constructor arguments or an existing parameter object, depending on the data provider.

## Supply the ParameterDirection property

When adding parameters, you must supply a <xref:System.Data.ParameterDirection> property for parameters other than input parameters. The following table shows the `ParameterDirection` values that you can use with the <xref:System.Data.ParameterDirection> enumeration.

|Member name|Description|
|-----------------|-----------------|
|<xref:System.Data.ParameterDirection.Input>|The parameter is an input parameter. This value is the default.|
|<xref:System.Data.ParameterDirection.InputOutput>|The parameter can perform both input and output.|
|<xref:System.Data.ParameterDirection.Output>|The parameter is an output parameter.|
|<xref:System.Data.ParameterDirection.ReturnValue>|The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function.|

## Work with parameter placeholders

The syntax for parameter placeholders depends on the data source. The Microsoft SqlClient Data Provider for SQL Server handles naming and specifying parameters and parameter placeholders differently. The SqlClient data provider uses named parameters in the format `@parametername`.

## Specify parameter data types

The data type of a parameter is specific to the Microsoft SqlClient Data Provider for SQL Server. Specifying the type converts the value of the `Parameter` to the Microsoft SqlClient Data Provider for SQL Server type before passing the value to the data source. You may also specify the type of a `Parameter` in a generic manner by setting the `DbType` property of the `Parameter` object to a particular <xref:System.Data.DbType>.

The Microsoft SqlClient Data Provider for SQL Server type of a `Parameter` object is inferred from the .NET Framework type of the `Value` of the `Parameter` object, or from the `DbType` of the `Parameter` object. The following table shows the inferred `Parameter` type based on the object passed as the `Parameter` value or the specified `DbType`.

|.NET type|DbType|SqlDbType|
|-------------------------|------------|---------------|
|<xref:System.Boolean>|`Boolean`|`Bit`|
|<xref:System.Byte>|`Byte`|`TinyInt`|
|`byte[]`|`Binary`|`VarBinary`. This implicit conversion will fail if the byte array is larger than the maximum size of a `VarBinary`, which is 8000 bytes. For byte arrays larger than 8000 bytes, explicitly set the <xref:System.Data.SqlDbType>.|
|<xref:System.Char>| |Inferring a <xref:System.Data.SqlDbType> from char isn't supported.|
|<xref:System.DateTime>|`DateTime`|`DateTime`|
|<xref:System.DateTimeOffset>|`DateTimeOffset`|`DateTimeOffset` in SQL Server 2008. Inferring a <xref:System.Data.SqlDbType> from `DateTimeOffset` isn't supported in versions of SQL Server earlier than SQL Server 2008.|
|<xref:System.Decimal>|`Decimal`|`Decimal`|
|<xref:System.Double>|`Double`|`Float`|
|<xref:System.Single>|`Single`|`Real`|
|<xref:System.Guid>|`Guid`|`UniqueIdentifier`|
|<xref:System.Int16>|`Int16`|`SmallInt`|
|<xref:System.Int32>|`Int32`|`Int`|
|<xref:System.Int64>|`Int64`|`BigInt`|
|<xref:System.Object>|`Object`|`Variant`|
|<xref:System.String>|`String`|`NVarChar`. This implicit conversion will fail if the string is larger than the maximum size of an `NVarChar`, which is 4000 characters. For strings larger than 4000 characters, explicitly set the <xref:System.Data.SqlDbType>.|
|<xref:System.TimeSpan>|`Time`|`Time` in SQL Server 2008. Inferring a <xref:System.Data.SqlDbType> from `TimeSpan` isn't supported in versions of SQL Server earlier than SQL Server 2008.|
|<xref:System.UInt16>|`UInt16`|Inferring a <xref:System.Data.SqlDbType> from `UInt16` isn't supported.|
|<xref:System.UInt32>|`UInt32`|Inferring a <xref:System.Data.SqlDbType> from `UInt32` isn't supported.|
|<xref:System.UInt64>|`UInt64`|Inferring a <xref:System.Data.SqlDbType> from `UInt64` isn't supported.|
||`AnsiString`|`VarChar`|
||`AnsiStringFixedLength`|`Char`|
||`Currency`|`Money`|
||`Date`|`Date` in SQL Server 2008. Inferring a <xref:System.Data.SqlDbType> from `Date` isn't supported in versions of SQL Server earlier than SQL Server 2008.|
||`SByte`|Inferring a <xref:System.Data.SqlDbType> from `SByte` isn't supported.|
||`StringFixedLength`|`NChar`|
||`Time`|`Time` in SQL Server 2008. Inferring a <xref:System.Data.SqlDbType> from `Time` isn't supported in versions of SQL Server earlier than SQL Server 2008.|
||`VarNumeric`|Inferring a <xref:System.Data.SqlDbType> from `VarNumeric` isn't supported.|
|user-defined type (an object with <xref:Microsoft.SqlServer.Server.SqlUserDefinedAggregateAttribute>|SqlClient always returns an Object|`SqlDbType.Udt` if <xref:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute> is present, otherwise `Variant`|

> [!NOTE]
> Conversions from decimal to other types are narrowing conversions that round the decimal value to the nearest integer value toward zero. If the result of the conversion isn't representable in the destination type, an <xref:System.OverflowException> is thrown.

> [!NOTE]
> When you send a null parameter value to the server, you must specify <xref:System.DBNull>, not `null` (`Nothing` in Visual Basic). The null value in the system is an empty object that has no value. <xref:System.DBNull> is used to represent null values.

## Derive parameter information

Parameters can also be derived from a stored procedure using the `DbCommandBuilder` class. The `SqlCommandBuilder` class provides a static method, `DeriveParameters`, which automatically populates the parameters collection of a command object that uses parameter information from a stored procedure. `DeriveParameters` overwrites any existing parameter information for the command.

> [!NOTE]
> Deriving parameter information incurs a performance penalty because it requires an additional round trip to the data source to retrieve the information. If parameter information is known at design time, you can improve the performance of your application by setting the parameters explicitly.

For more information, see [Generating Commands with CommandBuilders](generate-commands-with-commandbuilders.md).

## Using parameters with a SqlCommand and a stored procedure

Stored procedures offer many advantages in data-driven applications. By using stored procedures, database operations can be encapsulated in a single command, optimized for best performance, and enhanced with extra security. Although a stored procedure can be called by passing the stored procedure name followed by parameter arguments as an SQL statement, by using the <xref:System.Data.Common.DbCommand.Parameters%2A> collection of the ADO.NET <xref:System.Data.Common.DbCommand> object enables you to more explicitly define stored procedure parameters, and to access output parameters and return values.

> [!NOTE]
> Parameterized statements are executed on the server by using `sp_executesql,` which allows for query plan reuse. Local cursors or variables in the `sp_executesql` batch are not visible to the batch that calls `sp_executesql`. Changes in database context last only to the end of the `sp_executesql` statement. For more information, see [sp_executesql (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md).

When using parameters with a <xref:Microsoft.Data.SqlClient.SqlCommand> to execute a SQL Server stored procedure, the names of the parameters added to the <xref:Microsoft.Data.SqlClient.SqlCommand.Parameters%2A> collection must match the names of the parameter markers in the stored procedure. The Microsoft SqlClient Data Provider for SQL Server doesn't support the question mark (?) placeholder for passing parameters to an SQL statement or a stored procedure. It treats parameters in the stored procedure as named parameters and searches for matching parameter markers. For example, the `CustOrderHist` stored procedure is defined by using a parameter named `@CustomerID`. When your code executes the stored procedure, it must also use a parameter named `@CustomerID`.

```sql
CREATE PROCEDURE dbo.CustOrderHist @CustomerID varchar(5)
```

### Example

This example demonstrates how to call a SQL Server stored procedure in the `Northwind` sample database. The name of the stored procedure is `dbo.SalesByCategory` and it has an input parameter named `@CategoryName` with a data type of `nvarchar(15)`. The code creates a new <xref:Microsoft.Data.SqlClient.SqlConnection> inside a using block so that the connection is disposed when the procedure ends. The <xref:Microsoft.Data.SqlClient.SqlCommand> and <xref:Microsoft.Data.SqlClient.SqlParameter> objects are created, and their properties set. A <xref:Microsoft.Data.SqlClient.SqlDataReader> executes the `SqlCommand` and returns the result set from the stored procedure, displaying the output in the console window.

> [!NOTE]
> Instead of creating `SqlCommand` and `SqlParameter` objects and then setting properties in separate statements, you can instead elect to use one of the overloaded constructors to set multiple properties in a single statement.

[!code-csharp[DataWorks SqlClient.StoredProcedure#1](~/../sqlclient/doc/samples/SqlCommand_StoredProcedure.cs#1)]

## See also

- [Commands and parameters](commands-parameters.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Data type mappings in ADO.NET](data-type-mappings-ado-net.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
