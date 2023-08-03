---
title: "Executing a command"
description: Describes the Microsoft SqlClient Data Provider for SQL Server `Command` object and how to use it to execute queries and commands against a data source.
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
# Executing a command

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The Microsoft SqlClient Data Provider for SQL Server has <xref:Microsoft.Data.SqlClient.SqlCommand> object that inherits from <xref:System.Data.Common.DbCommand>. This object exposes methods for executing commands based on the type of command and desired return value, as described in the following table.

|Command|Return Value|  
|-------------|------------------|  
|`ExecuteReader`|Returns a `DataReader` object.|  
|`ExecuteScalar`|Returns a single scalar value.|  
|`ExecuteNonQuery`|Executes a command that does not return any rows.|  
|`ExecuteXMLReader`|Returns an <xref:System.Xml.XmlReader>. Available for a `SqlCommand` object only.|

Each strongly typed command object also supports a <xref:System.Data.CommandType> enumeration that specifies how a command string is interpreted, as described in the following table.

|CommandType|Description|
|-----------------|-----------------|  
|`Text`|An SQL command defining the statements to be executed at the data source.|  
|`StoredProcedure`|The name of the stored procedure. You can use the `Parameters` property of a command to access input and output parameters and return values, regardless of which `Execute` method is called.|  
|`TableDirect`|The name of a table.|

> [!IMPORTANT]
> When using `ExecuteReader`, return values and output parameters will not be accessible until the `DataReader` is closed.

## Example

The following code example demonstrates how to create a <xref:Microsoft.Data.SqlClient.SqlCommand> object to execute a stored procedure by setting its properties. A <xref:Microsoft.Data.SqlClient.SqlParameter> object is used to specify the input parameter to the stored procedure. The command is executed using the <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteReader%2A> method, and the output from the <xref:Microsoft.Data.SqlClient.SqlDataReader> is displayed in the console window.

[!code-csharp[DataWorks SqlClient.StoredProcedure#1](~/../sqlclient/doc/samples/SqlCommand_StoredProcedure.cs#1)]

### Troubleshooting commands

The Microsoft SqlClient Data Provider for SQL Server adds **diagnostic counters** to enable you to detect intermittent problems related to failed command executions. For more information, see [Diagnostic counters in SqlClient](diagnostic-counters.md).

## See also

- [Commands and parameters](commands-parameters.md)
- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
