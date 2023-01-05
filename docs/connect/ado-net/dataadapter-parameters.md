---
title: "DataAdapter parameters"
description: Learn about the properties of DbDataAdapter that return data from a data source and manage changes to the data source.
author: David-Engel
ms.author: v-davidengel
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# DataAdapter parameters

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The <xref:System.Data.Common.DbDataAdapter> has four properties that are used to retrieve data from and update data to the data source: the <xref:System.Data.Common.DbDataAdapter.SelectCommand%2A> property returns data from the data source; and the <xref:System.Data.Common.DbDataAdapter.InsertCommand%2A> , <xref:System.Data.Common.DbDataAdapter.UpdateCommand%2A>, and <xref:System.Data.Common.DbDataAdapter.DeleteCommand%2A> properties are used to manage changes at the data source.

> [!NOTE]
> The `SelectCommand` property must be set before you call the `Fill` method of the `DataAdapter`. The `InsertCommand`, `UpdateCommand`, or `DeleteCommand` properties must be set before the `Update` method of the `DataAdapter` is called, depending on what changes were made to the data in the <xref:System.Data.DataTable>. For example, if rows have been added, the `InsertCommand` must be set before you call `Update`. When `Update` is processing an inserted, updated, or deleted row, the `DataAdapter` uses the respective `Command` property to process the action. Current information about the modified row is passed to the `Command` object through the `Parameters` collection.

When you update a row at the data source, you call the UPDATE statement, which uses a unique identifier to identify the row in the table to be updated. The unique identifier is typically the value of a primary key field. The UPDATE statement uses parameters that contain both the unique identifier and the columns and values to be updated, as shown in the following Transact-SQL statement.

```sql
UPDATE Customers SET CompanyName = @CompanyName
  WHERE CustomerID = @CustomerID  
```  

> [!NOTE]
> The syntax for parameter placeholders depends on the data source. This example shows placeholders for a SQL Server data source.

In this example, the `CompanyName` field is updated with the value of the `@CompanyName` parameter for the row where `CustomerID` equals the value of the `@CustomerID` parameter. The parameters retrieve information from the modified row using the <xref:Microsoft.Data.SqlClient.SqlParameter.SourceColumn%2A> property of the <xref:Microsoft.Data.SqlClient.SqlParameter> object. The following are the parameters for the previous sample UPDATE statement. The code assumes that the variable `adapter` represents a valid <xref:Microsoft.Data.SqlClient.SqlDataAdapter> object.

[!code-csharp[Classic WebData SqlDataAdapter.SqlDataAdapter#2](~/../sqlclient/doc/samples/SqlDataAdapter_SqlDataAdapter.cs#2)]

The `Add` method of the `Parameters` collection takes the name of the parameter, the data type, the size (if applicable to the type), and the name of the <xref:System.Data.Common.DbParameter.SourceColumn%2A> from the `DataTable`. Notice that the <xref:System.Data.Common.DbParameter.SourceVersion%2A> of the `@CustomerID` parameter is set to `Original`. This guarantees that the existing row in the data source is updated if the value of the identifying column or columns has been changed in the modified <xref:System.Data.DataRow>. In that case, the `Original` row value would match the current value at the data source, and the `Current` row value would contain the updated value. The `SourceVersion` for the `@CompanyName` parameter is not set and uses the default, `Current` row value.

> [!NOTE]
> For both the `Fill` operations of the `DataAdapter` and the `Get` methods of the `DataReader`, the .NET type is inferred from the type returned from the Microsoft SqlClient Data Provider for SQL Server. The inferred .NET types and accessor methods for Microsoft SQL Server data types are described in [Data Type Mappings in ADO.NET](data-type-mappings-ado-net.md).

## Parameter.SourceColumn, Parameter.SourceVersion

The `SourceColumn` and `SourceVersion` may be passed as arguments to the `Parameter` constructor, or set as properties of an existing `Parameter`. The `SourceColumn` is the name of the <xref:System.Data.DataColumn> from the <xref:System.Data.DataRow> where the value of the `Parameter` will be retrieved. The `SourceVersion` specifies the `DataRow` version that the `DataAdapter` uses to retrieve the value.

The following table shows the <xref:System.Data.DataRowVersion> enumeration values available for use with `SourceVersion`.

|DataRowVersion Enumeration|Description|  
|--------------------------------|-----------------|  
|`Current`|The parameter uses the current value of the column. This is the default.|  
|`Default`|The parameter uses the `DefaultValue` of the column.|  
|`Original`|The parameter uses the original value of the column.|  
|`Proposed`|The parameter uses a proposed value.|  

The `SqlClient` code example in the next section defines a parameter for an <xref:System.Data.Common.DbDataAdapter.UpdateCommand%2A> in which the `CustomerID` column is used as a `SourceColumn` for two parameters: `@CustomerID` (`SET CustomerID = @CustomerID`), and `@OldCustomerID` (`WHERE CustomerID = @OldCustomerID`). The `@CustomerID` parameter is used to update the **CustomerID** column to the current value in the `DataRow`. As a result, the `CustomerID` `SourceColumn` with a `SourceVersion` of `Current` is used. The `@OldCustomerID` parameter is used to identify the current row in the data source. Because the matching column value is found in the `Original` version of the row, the same `SourceColumn` (`CustomerID`) with a `SourceVersion` of `Original` is used.

## Work with SqlClient parameters

The following example demonstrates how to create a <xref:Microsoft.Data.SqlClient.SqlDataAdapter> and set the <xref:System.Data.Common.DataAdapter.MissingSchemaAction%2A> to <xref:System.Data.MissingSchemaAction.AddWithKey> in order to retrieve additional schema information from the database. The <xref:Microsoft.Data.SqlClient.SqlDataAdapter.SelectCommand%2A>, <xref:Microsoft.Data.SqlClient.SqlDataAdapter.InsertCommand%2A>, <xref:Microsoft.Data.SqlClient.SqlDataAdapter.UpdateCommand%2A>, and <xref:Microsoft.Data.SqlClient.SqlDataAdapter.DeleteCommand%2A> properties set and their corresponding <xref:Microsoft.Data.SqlClient.SqlParameter> objects added to the <xref:Microsoft.Data.SqlClient.SqlCommand.Parameters%2A> collection. The method returns a `SqlDataAdapter` object.

[!code-csharp[Classic WebData SqlDataAdapter.SqlDataAdapter#1](~/../sqlclient/doc/samples/SqlDataAdapter_SqlDataAdapter.cs#1)]

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Commands and parameters](commands-parameters.md)
- [Update data sources with DataAdapters](update-data-sources-with-dataadapters.md)
- [Modify data with stored procedures](modify-data-with-stored-procedures.md)
- [Data type mappings in ADO.NET](data-type-mappings-ado-net.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
