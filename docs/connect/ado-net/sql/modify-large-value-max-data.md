---
title: "Modifying large-value (max) data in ADO.NET"
description: "Describes how to work with the large-value data types."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "08/15/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Modifying large-value (max) data in ADO.NET

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

Large object (LOB) data types are those that exceed the maximum row size of 8 kilobytes (KB). SQL Server provides a `max` specifier for `varchar`, `nvarchar`, and `varbinary` data types to allow storage of values as large as 2^32 bytes. Table columns and Transact-SQL variables may specify `varchar(max)`, `nvarchar(max)`, or `varbinary(max)` data types. In .NET, the `max` data types can be fetched by a `DataReader`, and can also be specified as both input and output parameter values without any special handling. For large `varchar` data types, data can be retrieved and updated incrementally.  
  
The `max` data types can be used for comparisons, as Transact-SQL variables, and for concatenation. They can also be used in the DISTINCT, ORDER BY, GROUP BY clauses of a SELECT statement as well as in aggregates, joins, and subqueries.

See [Using Large-Value Data Types](/previous-versions/sql/sql-server-2008/ms178158(v=sql.100)) from SQL Server Books Online more details on large-value data types.
  
## Large-value type restrictions  
The following restrictions apply to the `max` data types, which do not exist for smaller data types:  
  
- A `sql_variant` cannot contain a large `varchar` data type.  
  
- Large `varchar` columns cannot be specified as a key column in an index. They are allowed in an included column in a non-clustered index.  
  
- Large `varchar` columns cannot be used as partitioning key columns.  
  
## Working with large-value types in transact-SQL  
The Transact-SQL `OPENROWSET` function is a one-time method of connecting and accessing remote data. `OPENROWSET` can be referenced in the FROM clause of a query as though it were a table name. It can also be referenced as the target table of an INSERT, UPDATE, or DELETE statement.  
  
The `OPENROWSET` function includes the `BULK` rowset provider, which allows you to read data directly from a file without loading the data into a target table. This enables you to use `OPENROWSET` in a simple INSERT SELECT statement.  
  
The `OPENROWSET BULK` option arguments provide significant control over where to begin and end reading data, how to deal with errors, and how data is interpreted. For example, you can specify that the data file be read as a single-row, single-column rowset of type `varbinary`, `varchar`, or `nvarchar`. For the complete syntax and options, see SQL Server Books Online.  
  
The following example inserts a photo into the ProductPhoto table in the AdventureWorks sample database. When using the `BULK OPENROWSET` provider, you must supply the named list of columns even if you aren't inserting values into every column. The primary key in this case is defined as an identity column, and may be omitted from the column list. Note that you must also supply a correlation name at the end of the `OPENROWSET` statement, which in this case is ThumbnailPhoto. This correlates with the column in the `ProductPhoto` table into which the file is being loaded.  
  
```sql
INSERT Production.ProductPhoto (  
    ThumbnailPhoto,   
    ThumbnailPhotoFilePath,   
    LargePhoto,   
    LargePhotoFilePath)  
SELECT ThumbnailPhoto.*, null, null, N'tricycle_pink.gif'  
FROM OPENROWSET   
    (BULK 'c:\images\tricycle.jpg', SINGLE_BLOB) ThumbnailPhoto  
```  
  
## Updating data using UPDATE .WRITE  
The Transact-SQL UPDATE statement has new WRITE syntax for modifying the contents of `varchar(max)`, `nvarchar(max)`, or `varbinary(max)` columns. This allows you to perform partial updates of the data. The UPDATE .WRITE syntax is shown here in abbreviated form:  
  
UPDATE  
  
{ *\<object>* }  
  
SET  
  
{ *column_name* = { .WRITE ( *expression* , @Offset , @Length ) }  
  
The WRITE method specifies that a section of the value of the *column_name* will be modified. The expression is the value that will be copied to the *column_name*, the `@Offset` is the beginning point at which the expression will be written, and the `@Length` argument is the length of the section in the column.  
  
|If|Then|  
|--------|----------|  
|The expression is set to NULL|`@Length` is ignored and the value in *column_name* is truncated at the specified `@Offset`.|  
|`@Offset` is NULL|The update operation appends the expression at the end of the existing *column_name* value and `@Length` is ignored.|  
|`@Offset` is greater than the length of the column_name value|SQL Server returns an error.|  
|`@Length` is NULL|The update operation removes all data from `@Offset` to the end of the `column_name` value.|  
  
> [!NOTE]
>  Neither `@Offset` nor `@Length` can be a negative number.  
  
## Example  
This Transact-SQL example updates a partial value in DocumentSummary, an `nvarchar(max)` column in the Document table in the AdventureWorks database. The word 'components' is replaced by the word 'features' by specifying the replacement word, the beginning location (offset) of the word to be replaced in the existing data, and the number of characters to be replaced (length). The example includes SELECT statements before and after the UPDATE statement to compare results.  
  
```sql
USE AdventureWorks;  
GO  
--View the existing value.  
SELECT DocumentSummary  
FROM Production.Document  
WHERE DocumentID = 3;  
GO  
-- The first sentence of the results will be:  
-- Reflectors are vital safety components of your bicycle.  
  
--Modify a single word in the DocumentSummary column  
UPDATE Production.Document  
SET DocumentSummary .WRITE (N'features',28,10)  
WHERE DocumentID = 3 ;  
GO   
--View the modified value.  
SELECT DocumentSummary  
FROM Production.Document  
WHERE DocumentID = 3;  
GO  
-- The first sentence of the results will be:  
-- Reflectors are vital safety features of your bicycle.  
```  
  
## Working with large-value types in ADO.NET  
You can work with large value types in ADO.NET by specifying large value types as <xref:Microsoft.Data.SqlClient.SqlParameter> objects in a <xref:Microsoft.Data.SqlClient.SqlDataReader> to return a result set, or by using a <xref:Microsoft.Data.SqlClient.SqlDataAdapter> to fill a `DataSet`/`DataTable`. There is no difference between the way you work with a large value type and its related, smaller value data type.  
  
### Using GetSqlBytes to Retrieve Data  
The `GetSqlBytes` method of the <xref:Microsoft.Data.SqlClient.SqlDataReader> can be used to retrieve the contents of a `varbinary(max)` column. The following code fragment assumes a <xref:Microsoft.Data.SqlClient.SqlCommand> object named `cmd` that selects `varbinary(max)` data from a table and a <xref:Microsoft.Data.SqlClient.SqlDataReader> object named `reader` that retrieves the data as <xref:System.Data.SqlTypes.SqlBytes>.  
  
```csharp  
reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);  
while (reader.Read())  
    {  
        SqlBytes bytes = reader.GetSqlBytes(0);  
    }  
```  
  
### Using GetSqlChars to retrieve data  
The `GetSqlChars` method of the <xref:Microsoft.Data.SqlClient.SqlDataReader> can be used to retrieve the contents of a `varchar(max)` or `nvarchar(max)` column. The following code fragment assumes a <xref:Microsoft.Data.SqlClient.SqlCommand> object named `cmd` that selects `nvarchar(max)` data from a table and a <xref:Microsoft.Data.SqlClient.SqlDataReader> object named `reader` that retrieves the data.   
  
```csharp  
reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);  
while (reader.Read())  
{  
    SqlChars buffer = reader.GetSqlChars(0);  
}  
```  
  
### Using GetSqlBinary to retrieve data  
The `GetSqlBinary` method of a <xref:Microsoft.Data.SqlClient.SqlDataReader> can be used to retrieve the contents of a `varbinary(max)` column. The following code fragment assumes a <xref:Microsoft.Data.SqlClient.SqlCommand> object named `cmd` that selects `varbinary(max)` data from a table and a <xref:Microsoft.Data.SqlClient.SqlDataReader> object named `reader` that retrieves the data as a <xref:System.Data.SqlTypes.SqlBinary> stream.  
  
```csharp  
reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);  
while (reader.Read())  
    {  
        SqlBinary binaryStream = reader.GetSqlBinary(0);  
    }  
```  
  
### Using GetBytes to retrieve data  
The `GetBytes` method of a <xref:Microsoft.Data.SqlClient.SqlDataReader> reads a stream of bytes from the specified column offset into a byte array starting at the specified array offset. The following code fragment assumes a <xref:Microsoft.Data.SqlClient.SqlDataReader> object named `reader` that retrieves bytes into a byte array. Note that, unlike `GetSqlBytes`, `GetBytes` requires a size for the array buffer.  
  
```csharp  
while (reader.Read())  
{  
    byte[] buffer = new byte[4000];  
    long byteCount = reader.GetBytes(1, 0, buffer, 0, 4000);  
}  
```  
  
### Using GetValue to retrieve data  
The `GetValue` method of a <xref:Microsoft.Data.SqlClient.SqlDataReader> reads the value from the specified column offset into an array. The following code fragment assumes a <xref:Microsoft.Data.SqlClient.SqlDataReader> object named `reader` that retrieves binary data from the first column offset, and then string data from the second column offset.  
  
```csharp  
while (reader.Read())  
{  
    // Read the data from varbinary(max) column  
    byte[] binaryData = (byte[])reader.GetValue(0);  
  
    // Read the data from varchar(max) or nvarchar(max) column  
    String stringData = (String)reader.GetValue(1);  
}  
```  
  
## Converting from large value types to CLR types  
You can convert the contents of a `varchar(max)` or `nvarchar(max)` column using any of the string conversion methods, such as `ToString`. The following code fragment assumes a <xref:Microsoft.Data.SqlClient.SqlDataReader> object named `reader` that retrieves the data.  
  
```csharp  
while (reader.Read())  
{  
     string str = reader[0].ToString();  
     Console.WriteLine(str);  
}  
```  
  
### Example  
The following code retrieves the name and the `LargePhoto` object from the `ProductPhoto` table in the `AdventureWorks` database and saves it to a file. The assembly needs to be compiled with a reference to the <xref:System.Drawing> namespace.  The <xref:Microsoft.Data.SqlClient.SqlDataReader.GetSqlBytes%2A> method of the <xref:Microsoft.Data.SqlClient.SqlDataReader> returns a <xref:System.Data.SqlTypes.SqlBytes> object that exposes a `Stream` property. The code uses this to create a new `Bitmap` object, and then saves it in the Gif `ImageFormat`.  
  
[!code-csharp[DataWorks SqlBytes_Stream#1](~/../sqlclient/doc/samples/SqlBytes_Stream.cs#1)]
  
## Using large value type parameters  
Large value types can be used in <xref:Microsoft.Data.SqlClient.SqlParameter> objects the same way you use smaller value types in <xref:Microsoft.Data.SqlClient.SqlParameter> objects. You can retrieve large value types as <xref:Microsoft.Data.SqlClient.SqlParameter> values, as shown in the following example. The code assumes that the following GetDocumentSummary stored procedure exists in the AdventureWorks sample database. The stored procedure takes an input parameter named @DocumentID and returns the contents of the DocumentSummary column in the @DocumentSummary output parameter.  
  
```sql
CREATE PROCEDURE GetDocumentSummary   
(  
    @DocumentID int,  
    @DocumentSummary nvarchar(MAX) OUTPUT  
)  
AS  
SET NOCOUNT ON  
SELECT  @DocumentSummary=Convert(nvarchar(MAX), DocumentSummary)  
FROM    Production.Document  
WHERE   DocumentID=@DocumentID  
```  
  
### Example  
The ADO.NET code creates <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand> objects to execute the GetDocumentSummary stored procedure and retrieve the document summary, which is stored as a large value type. The code passes a value for the @DocumentID input parameter, and displays the results passed back in the @DocumentSummary output parameter in the Console window.  
  
[!code-csharp[DataWorks SqlParameter_Value#1](~/../sqlclient/doc/samples/SqlParameter_Value.cs#1)]
  
## Next steps
- [SQL Server binary and large-value data](sql-server-binary-large-value-data.md)
- [SQL Server data operations in ADO.NET](sql-server-data-operations.md)