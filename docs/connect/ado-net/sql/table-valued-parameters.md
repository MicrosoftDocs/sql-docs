---
title: Table-valued parameters
description: Learn how to work with table-valued parameters in ADO.NET by using Microsoft.Data.SqlClient.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Table-valued parameters

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Table-valued parameters provide an easy way to marshal multiple rows of data from a client application to SQL Server.  They don't require multiple round trips or special server-side logic for processing the data. You can use table-valued parameters to encapsulate rows of data in a client application and send the data to the server in a single parameterized command. The incoming data rows are stored in a table variable that can then be operated on by using Transact-SQL.

Column values in table-valued parameters can be accessed using standard Transact-SQL SELECT statements. Table-valued parameters are strongly typed and their structure is automatically validated. The size of table-valued parameters is limited only by server memory.

> [!NOTE]
> You cannot return data in a table-valued parameter. Table-valued parameters are input-only; the OUTPUT keyword is not supported.

For more information about table-valued parameters, see the following resources.

|Resource|Description|
|--------------|-----------------|
|[Use Table-Valued Parameters (Database Engine)](../../../relational-databases/tables/use-table-valued-parameters-database-engine.md)|Describes how to create and use table-valued parameters.|
|[Creating a user-defined table type](../../../t-sql/statements/create-type-transact-sql.md#c-creating-a-user-defined-table-type)|Describes user-defined table types that are used to declare table-valued parameters.|
|[User-Defined Table Types](/previous-versions/sql/sql-server-2008/bb522526(v=sql.100))|Describes user-defined table types that are used to declare table-valued parameters.|

## Passing multiple rows in previous versions of SQL Server

Before table-valued parameters were introduced, the options for passing multiple rows of data to a stored procedure or a parameterized SQL command were limited. A developer could choose from the following options for passing multiple rows to the server:

- Use a series of individual parameters to represent the values in multiple columns and rows of data. The amount of data that can be passed by using this method is limited by the number of parameters allowed. SQL Server procedures can have, at most, 2100 parameters. Server-side logic is required to assemble these individual values into a table variable or a temporary table for processing.

- Bundle multiple data values into delimited strings or XML documents and then pass those text values to a procedure or statement. This method requires the procedure or statement to include logic for validating the data structures and unbundling the values.

- Create a series of individual SQL statements for data modifications that affect multiple rows, such as those created by calling the `Update` method of a <xref:Microsoft.Data.SqlClient.SqlDataAdapter>. Changes can be submitted to the server individually or batched into groups. However, even when submitted in batches that contain multiple statements, each statement is executed separately on the server.

- Use the `bcp` utility program or the <xref:Microsoft.Data.SqlClient.SqlBulkCopy> object to load many rows of data into a table. Although this technique is efficient, it doesn't support server-side processing unless the data is loaded into a temporary table or table variable.

## Creating table-valued parameter types

Table-valued parameters are based on strongly typed table structures that are defined by using Transact-SQL CREATE TYPE statements. You must create a table type and define the structure in SQL Server before you can use table-valued parameters in your client applications. For more information about creating table types, see [Use Table-Valued Parameters (Database Engine)](../../../relational-databases/tables/use-table-valued-parameters-database-engine.md).

The following statement creates a table type named CategoryTableType that consists of CategoryID and CategoryName columns:

```sql
CREATE TYPE dbo.CategoryTableType AS TABLE
    ( CategoryID int, CategoryName nvarchar(50) )
```

After you create a table type, you can declare table-valued parameters based on that type. The following Transact-SQL fragment demonstrates how to declare a table-valued parameter in a stored procedure definition. The READONLY keyword is required for declaring a table-valued parameter.

```sql
CREATE PROCEDURE usp_UpdateCategories
    (@tvpNewCategories dbo.CategoryTableType READONLY)
```

## Modifying data with table-valued parameters (transact-SQL)

Table-valued parameters can be used in set-based data modifications that affect multiple rows by executing a single statement. For example, you can select all the rows in a table-valued parameter and insert them into a database table, or you can create an update statement by joining a table-valued parameter to the table you want to update.

The following Transact-SQL UPDATE statement demonstrates how to use a table-valued parameter by joining it to the Categories table. When you use a table-valued parameter with a JOIN in a FROM clause, you must also alias it, as shown here, where the table-valued parameter is aliased as "ec":

```sql
UPDATE dbo.Categories
    SET Categories.CategoryName = ec.CategoryName
    FROM dbo.Categories INNER JOIN @tvpEditedCategories AS ec
    ON dbo.Categories.CategoryID = ec.CategoryID;
```

This Transact-SQL example demonstrates how to select rows from a table-valued parameter to perform an INSERT in a single set-based operation.

```sql
INSERT INTO dbo.Categories (CategoryID, CategoryName)
    SELECT nc.CategoryID, nc.CategoryName FROM @tvpNewCategories AS nc;
```

## Limitations of table-valued parameters

There are several limitations to table-valued parameters:

- You can't pass table-valued parameters to [CLR user-defined functions](../../../relational-databases/clr-integration-database-objects-user-defined-functions/clr-user-defined-functions.md).

- Table-valued parameters can only be indexed to support UNIQUE or PRIMARY KEY constraints. SQL Server doesn't maintain statistics on table-valued parameters.

- Table-valued parameters are read-only in Transact-SQL code. You can't update the column values in the rows of a table-valued parameter and you can't insert or delete rows. To modify data passed to a stored procedure or parameterized statement in a table-valued parameter, you must insert the data into a temporary table or into a table variable.

- You can't use ALTER TABLE statements to modify the design of table-valued parameters.

## Configuring a SqlParameter example

<xref:Microsoft.Data.SqlClient> supports populating table-valued parameters from <xref:System.Data.DataTable>, <xref:System.Data.Common.DbDataReader>, or <xref:System.Collections.Generic.IEnumerable%601> \ <xref:Microsoft.Data.SqlClient.Server.SqlDataRecord> objects. Specify a type name for the table-valued parameter by using the <xref:Microsoft.Data.SqlClient.SqlParameter.TypeName%2A> property of a <xref:Microsoft.Data.SqlClient.SqlParameter>. The `TypeName` must match the name of a compatible type previously created on the server. The following code fragment demonstrates how to configure <xref:Microsoft.Data.SqlClient.SqlParameter> to insert data.

In the following example, the `addedCategories` variable contains a <xref:System.Data.DataTable>. To see how the variable is populated, see the examples in the next section, [Passing a Table-Valued Parameter to a Stored Procedure](#passing).

```csharp
// Configure the command and parameter.
SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);
SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@tvpNewCategories", addedCategories);
tvpParam.SqlDbType = SqlDbType.Structured;
tvpParam.TypeName = "dbo.CategoryTableType";
```

You can also use any object derived from <xref:System.Data.Common.DbDataReader> to stream rows of data to a table-valued parameter, as shown in this fragment:

```csharp
// Configure the SqlCommand and table-valued parameter.
SqlCommand insertCommand = new SqlCommand("usp_InsertCategories", connection);
insertCommand.CommandType = CommandType.StoredProcedure;
SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@tvpNewCategories", dataReader);
tvpParam.SqlDbType = SqlDbType.Structured;
```

## <a name="passing"></a> Passing a table-valued parameter to a stored procedure

This example demonstrates how to pass table-valued parameter data to a stored procedure. The code extracts added rows into a new <xref:System.Data.DataTable> by using the <xref:System.Data.DataTable.GetChanges%2A> method. The code then defines a <xref:Microsoft.Data.SqlClient.SqlCommand>, setting the <xref:Microsoft.Data.SqlClient.SqlCommand.CommandType%2A> property to <xref:System.Data.CommandType.StoredProcedure>. The <xref:Microsoft.Data.SqlClient.SqlParameter> is populated by using the <xref:Microsoft.Data.SqlClient.SqlParameterCollection.AddWithValue%2A> method and the <xref:Microsoft.Data.SqlClient.SqlParameter.SqlDbType%2A> is set to `Structured`. The <xref:Microsoft.Data.SqlClient.SqlCommand> is then executed by using the <xref:Microsoft.Data.SqlClient.SqlCommand.ExecuteNonQuery%2A> method.

```csharp
// Assumes connection is an open SqlConnection object.
using (connection)
{
    // Create a DataTable with the modified rows.
    DataTable addedCategories = CategoriesDataTable.GetChanges(DataRowState.Added);

    // Configure the SqlCommand and SqlParameter.
    SqlCommand insertCommand = new SqlCommand("usp_InsertCategories", connection);
    insertCommand.CommandType = CommandType.StoredProcedure;
    SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@tvpNewCategories", addedCategories);
    tvpParam.SqlDbType = SqlDbType.Structured;

    // Execute the command.
    insertCommand.ExecuteNonQuery();
}
```

### Passing a table-valued parameter to a parameterized SQL statement

The following example demonstrates how to insert data into the dbo.Categories table by using an INSERT statement with a SELECT subquery that has a table-valued parameter as the data source. When passing a table-valued parameter to a parameterized SQL statement, you must specify a type name for the table-valued parameter by using the new <xref:Microsoft.Data.SqlClient.SqlParameter.TypeName%2A> property of a <xref:Microsoft.Data.SqlClient.SqlParameter>. This `TypeName` must match the name of a compatible type previously created on the server. The code in this example uses the `TypeName` property to reference the type structure defined in dbo.CategoryTableType.

> [!NOTE]
> If you supply a value for an identity column in a table-valued parameter, you must issue the SET IDENTITY_INSERT statement for the session.

```csharp
// Assumes connection is an open SqlConnection.
using (connection)
{
    // Create a DataTable with the modified rows.
    DataTable addedCategories = CategoriesDataTable.GetChanges(DataRowState.Added);

    // Define the INSERT-SELECT statement.
    string sqlInsert =
        "INSERT INTO dbo.Categories (CategoryID, CategoryName)"
        + " SELECT nc.CategoryID, nc.CategoryName"
        + " FROM @tvpNewCategories AS nc;"

    // Configure the command and parameter.
    SqlCommand insertCommand = new SqlCommand(sqlInsert, connection);
    SqlParameter tvpParam = insertCommand.Parameters.AddWithValue("@tvpNewCategories", addedCategories);
    tvpParam.SqlDbType = SqlDbType.Structured;
    tvpParam.TypeName = "dbo.CategoryTableType";

    // Execute the command.
    insertCommand.ExecuteNonQuery();
}
```

## Streaming rows with a DataReader

You can also use any object derived from <xref:System.Data.Common.DbDataReader> to stream rows of data to a table-valued parameter. The following code fragment demonstrates retrieving data from an Oracle database by using an <xref:System.Data.OracleClient.OracleCommand> and an <xref:System.Data.OracleClient.OracleDataReader>. The code then configures a <xref:Microsoft.Data.SqlClient.SqlCommand> to invoke a stored procedure with a single input parameter. The <xref:Microsoft.Data.SqlClient.SqlParameter.SqlDbType%2A> property of the <xref:Microsoft.Data.SqlClient.SqlParameter> is set to `Structured`. The <xref:Microsoft.Data.SqlClient.SqlParameterCollection.AddWithValue%2A> passes the `OracleDataReader` result set to the stored procedure as a table-valued parameter.

```csharp
// Assumes connection is an open SqlConnection.
// Retrieve data from Oracle.
OracleCommand selectCommand = new OracleCommand(
    "Select CategoryID, CategoryName FROM Categories;",
    oracleConnection);
OracleDataReader oracleReader = selectCommand.ExecuteReader(
    CommandBehavior.CloseConnection);

// Configure the SqlCommand and table-valued parameter.
SqlCommand insertCommand = new SqlCommand(
    "usp_InsertCategories", connection);
insertCommand.CommandType = CommandType.StoredProcedure;
SqlParameter tvpParam =
    insertCommand.Parameters.AddWithValue(
    "@tvpNewCategories", oracleReader);
tvpParam.SqlDbType = SqlDbType.Structured;

// Execute the command.
insertCommand.ExecuteNonQuery();
```

## Next steps

- [SQL Server data operations in ADO.NET](sql-server-data-operations.md)
