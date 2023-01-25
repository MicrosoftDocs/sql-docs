---
title: Using table-valued parameters
description: Table-valued parameters provide an efficient way to send multiple rows of data from a client to SQL Server in a single parameterized command.
author: David-Engel
ms.author: v-davidengel
ms.date: 04/21/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using table-valued parameters

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Table-valued parameters provide an easy way to marshal multiple rows of data from a client application to SQL Server without requiring multiple round trips or special server-side logic for processing the data. You can use table-valued parameters to encapsulate rows of data in a client application and send the data to the server in a single parameterized command. The incoming data rows are stored in a table variable that can then be operated on by using Transact-SQL.  
  
Column values in table-valued parameters can be accessed using standard Transact-SQL SELECT statements. Table-valued parameters are strongly typed and their structure is automatically validated. The size of table-valued parameters is limited only by server memory.  
  
> [!NOTE]  
> Support for Table-Valued Parameters is available starting with  Microsoft JDBC Driver 6.0 for SQL Server.
>
> You can't return data in a table-valued parameter. Table-valued parameters are input-only; the OUTPUT keyword is not supported.  
  
 For more information about table-valued parameters, see the following resources.  
  
| Resource                                                                                                             | Description                                                                         |
| -------------------------------------------------------------------------------------------------------------------- | ----------------------------------------------------------------------------------- |
| [Table-Valued Parameters (Database Engine)](/previous-versions/sql/sql-server-2008/bb510489(v=sql.100)) in SQL Server Books Online | Describes how to create and use table-valued parameters                             |
| [User-Defined Table Types](/previous-versions/sql/sql-server-2008/bb522526(v=sql.100)) in SQL Server Books Online                  | Describes user-defined table types that are used to declare table-valued parameters |

## Passing multiple rows in previous versions of SQL Server  

Before table-valued parameters were introduced to SQL Server 2008, the options for passing multiple rows of data to a stored procedure or a parameterized SQL command were limited. A developer could choose from the following options for passing multiple rows to the server:  
  
- Use a series of individual parameters to represent the values in multiple columns and rows of data. The amount of data that can be passed by using this method is limited by the number of parameters allowed. SQL Server procedures can have, at most, 2100 parameters. Server-side logic is required to assemble these individual values into a table variable or a temporary table for processing.  
  
- Bundle multiple data values into delimited strings or XML documents and then pass those text values to a procedure or statement. This requires the procedure or statement to include the logic necessary for validating the data structures and unbundling the values.  
  
- Create a series of individual SQL statements for data modifications that affect multiple rows. Changes can be submitted to the server individually or batched into groups. However, even when submitted in batches that contain multiple statements, each statement is executed separately on the server.  
  
- Use the bcp utility program or [SQLServerBulkCopy](using-bulk-copy-with-the-jdbc-driver.md) to load many rows of data into a table. Although this technique is efficient, it doesn't support server-side processing unless the data is loaded into a temporary table or table variable.
  
## Creating table-valued parameter types  

Table-valued parameters are based on strongly typed table structures that are defined by using Transact-SQL `CREATE TYPE` statements. You have to create a table type and define the structure in SQL Server before you can use table-valued parameters in your client applications. For more information about creating table types, see [User-Defined Table Types](/previous-versions/sql/sql-server-2008/bb522526(v=sql.100)) in SQL Server Books Online.  

```sql
CREATE TYPE dbo.CategoryTableType AS TABLE  
    ( CategoryID int, CategoryName nvarchar(50) )  
```

After creating a table type, you can declare table-valued parameters based on that type. The following Transact-SQL fragment demonstrates how to declare a table-valued parameter in a stored procedure definition. The `READONLY` keyword is required for declaring a table-valued parameter.  

```sql
CREATE PROCEDURE usp_UpdateCategories
    (@tvpNewCategories dbo.CategoryTableType READONLY)  
```

## Modifying data with table-valued parameters (Transact-SQL)  

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
  
- You can't pass table-valued parameters to user defined functions.  
  
- Table-valued parameters can only be indexed to support UNIQUE or PRIMARY KEY constraints. SQL Server doesn't maintain statistics on table-valued parameters.  
  
- Table-valued parameters are read-only in Transact-SQL code. You can't update the column values in the rows of a table-valued parameter and you can't insert or delete rows. To modify the data that is passed to a stored procedure or parameterized statement in table-valued parameter, you must insert the data into a temporary table or into a table variable.  
  
- You can't use ALTER TABLE statements to modify the design of table-valued parameters.

- You can stream large objects in a table-valued parameter.  
  
## Configuring a table-valued parameter

Beginning with Microsoft JDBC Driver 6.0 for SQL Server, table-valued parameters are supported with a parameterized statement or a parameterized stored procedure. Table-valued parameters can be populated from a SQLServerDataTable, from a ResultSet or from a user provided implementation of the ISQLServerDataRecord interface. When setting a table-valued parameter for a prepared query, you must specify a type name, which must match the name of a compatible type previously created on the server.  
  
The following two code fragments demonstrate how to configure a table-valued parameter with a SQLServerPreparedStatement and with a SQLServerCallableStatement to insert data. Here sourceTVPObject can be a SQLServerDataTable, or a ResultSet or an ISQLServerDataRecord object. The examples assume connection is an active Connection object.  

```java
// Using table-valued parameter with a SQLServerPreparedStatement.  
SQLServerPreparedStatement pStmt =
    (SQLServerPreparedStatement) connection.prepareStatement("INSERT INTO dbo.Categories SELECT * FROM ?");  
pStmt.setStructured(1, "dbo.CategoryTableType", sourceTVPObject);  
pStmt.execute();  
```

```java
// Using table-valued parameter with a SQLServerCallableStatement.  
SQLServerCallableStatement pStmt =
    (SQLServerCallableStatement) connection.prepareCall("exec usp_InsertCategories ?");
pStmt.setStructured(1, "dbo.CategoryTableType", sourceTVPObject);;  
pStmt.execute();  
```

> [!NOTE]  
> See Section **Table-Valued Parameter API for the JDBC Driver** below for a complete list of APIs available for setting the table-valued parameter.  
  
## Passing a table-valued parameter as a SQLServerDataTable object  

Beginning with Microsoft JDBC Driver 6.0 for SQL Server, the SQLServerDataTable class represents an in-memory table of relational data. This example demonstrates how to construct a table-valued parameter from in-memory data using the SQLServerDataTable object. First, the code creates a SQLServerDataTable object, defines its schema, and populates the table with data. The code then configures a SQLServerPreparedStatement that passes this data table as a table-valued parameter to SQL Server.  

```java
/* Assumes connection is an active Connection object. */

// Create an in-memory data table.  
SQLServerDataTable sourceDataTable = new SQLServerDataTable();
  
// Define metadata for the data table.  
sourceDataTable.addColumnMetadata("CategoryID" ,java.sql.Types.INTEGER);
sourceDataTable.addColumnMetadata("CategoryName" ,java.sql.Types.NVARCHAR);
  
// Populate the data table.  
sourceDataTable.addRow(1, "CategoryNameValue1");
sourceDataTable.addRow(2, "CategoryNameValue2");
  
// Pass the data table as a table-valued parameter using a prepared statement.  
SQLServerPreparedStatement pStmt =
        (SQLServerPreparedStatement) connection.prepareStatement(  
            "INSERT INTO dbo.Categories SELECT * FROM ?;");  
pStmt.setStructured(1, "dbo.CategoryTableType", sourceDataTable);  
pStmt.execute();  
```

This example is similar to the previous one. The only difference is that it sets the TVP Name on the `SQLServerDataTable` instead of relying on casting `PreparedStatement` to a `SQLServerPreparedStatement` to use the `setStructured` method.

```java
/* Assumes connection is an active Connection object. */

// Create an in-memory data table.
SQLServerDataTable sourceDataTable = new SQLServerDataTable();
sourceDataTable.setTvpName("dbo.CategoryTableType");

// Define metadata for the data table.
sourceDataTable.addColumnMetadata("CategoryID" ,java.sql.Types.INTEGER);
sourceDataTable.addColumnMetadata("CategoryName" ,java.sql.Types.NVARCHAR);

// Populate the data table.
sourceDataTable.addRow(1, "CategoryNameValue1");
sourceDataTable.addRow(2, "CategoryNameValue2");

// Pass the data table as a table-valued parameter using a prepared statement.
PreparedStatement pStmt =
        connection.prepareStatement(
            "INSERT INTO dbo.Categories SELECT * FROM ?;");
pStmt.setObject(1, sourceDataTable);
pStmt.execute();
```

> [!NOTE]  
> See Section **Table-Valued Parameter API for the JDBC Driver** below for a complete list of APIs available for setting the table-valued parameter.  
  
## Passing a table-valued parameter as a ResultSet object  

This example demonstrates how to stream rows of data from a ResultSet to a table-valued parameter. First, the code retrieves data from a source table in a SQLServerDataTable object, defines its schema, and populates the table with data. The code then configures a SQLServerPreparedStatement that passes this data table as a table-valued parameter to SQL Server.  

```java
/* Assumes connection is an active Connection object. */

// Create the source ResultSet object. Here SourceCategories is a table defined with the same schema as Categories table.
ResultSet sourceResultSet = connection.createStatement().executeQuery("SELECT * FROM SourceCategories");  

// Pass the source result set as a table-valued parameter using a prepared statement.  
SQLServerPreparedStatement pStmt =
        (SQLServerPreparedStatement) connection.prepareStatement(  
                "INSERT INTO dbo.Categories SELECT * FROM ?;");  
pStmt.setStructured(1, "dbo.CategoryTableType", sourceResultSet);  
pStmt.execute();  
```

> [!NOTE]  
> See Section **Table-Valued Parameter API for the JDBC Driver** below for a complete list of APIs available for setting the table-valued parameter.  

## Passing a table-valued parameter as an ISQLServerDataRecord object  

Beginning with Microsoft JDBC Driver 6.0 for SQL Server, a new interface ISQLServerDataRecord is available for streaming data (depending on how the user provides the implementation for it) using a table-valued parameter. The following example demonstrates how to implement the ISQLServerDataRecord interface and how to pass it as a table-valued parameter. For simplicity, the following example passes just one row with hardcoded values to the table-valued parameter. Ideally, the user would implement this interface to stream rows from any source, for example from text files.  

```java
class MyRecords implements ISQLServerDataRecord  
{  
    int currentRow = 0;  
    Object[] row = new Object[2];  
  
    MyRecords(){  
        // Constructor. This implementation has just one row.
        row[0] = new Integer(1);  
        row[1] = "categoryName1";  
    }  
  
    public int getColumnCount(){  
        // Return the total number of columns, for this example it is 2.  
        return 2;  
    }  
  
    public SQLServerMetaData getColumnMetaData(int columnIndex) {  
        // Return the column metadata.  
        if (1 == columnIndex)  
            return new SQLServerMetaData("CategoryID", java.sql.Types.INTEGER);  
        else  
            return new SQLServerMetaData("CategoryName", java.sql.Types.NVARCHAR);  
    }  
  
    public Object[] getRowData(){  
        // Return the columns in the current row as an array of objects. This implementation has just one row.  
        return row;
    }  
  
    public boolean next(){  
        // Move to the next row. This implementation has just one row, after processing the first row, return false.  
        currentRow++;  
        if (1 == currentRow)  
            return true;  
        else  
            return false;  
    }
}

// Following code demonstrates how to pass MyRecords object as a table-valued parameter.  
MyRecords sourceRecords = new MyRecords();  
SQLServerPreparedStatement pStmt =
        (SQLServerPreparedStatement) connection.prepareStatement(  
                "INSERT INTO dbo.Categories SELECT * FROM ?;");  
pStmt.setStructured(1, "dbo.CategoryTableType", sourceRecords);  
pStmt.execute();  
```

> [!NOTE]  
> See Section **Table-valued parameter API for the JDBC driver** below for a complete list of APIs available for setting the table-valued parameter.

## Table-valued parameter API for the JDBC driver

### SQLServerMetaData

This class represents metadata for a column. It's used in the ISQLServerDataRecord interface to pass column metadata to the table-valued parameter. The methods in this class are:  

| Name                                                                                                                                                                             | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| public SQLServerMetaData(String columnName, int sqlType, int precision, int scale, boolean useServerDefault, boolean isUniqueKey, SQLServerSortOrder sortOrder, int sortOrdinal) | Initializes a new instance of SQLServerMetaData with the specified column name, sql type, precision, scale and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column. <br/><br/>useServerDefault - specifies if this column should use the default server value; Default value is false.<br>isUniqueKey - indicates if the column in the table-valued parameter is unique; Default value is false.<br>sortOrder  - indicates the sort order for a column; Default value is SQLServerSortOrder.Unspecified.<br>sortOrdinal - specifies ordinal of the sort column; sortOrdinal starts from 0; Default value is -1. |
| public SQLServerMetaData(String columnName, int sqlType)                                                                                                                        | Initializes a new instance of SQLServerMetaData using the column name and the sql type.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| public SQLServerMetaData(String columnName, int sqlType, int length)                                                                                                                        | Initializes a new instance of SQLServerMetaData using the column name, the sql type and the length (for String data). The length is used to differentiate large strings from strings with length less than 4000 characters. Introduced in the version 7.2 of the JDBC driver.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| public SQLServerMetaData(String columnName, int sqlType, int precision, int scale)                                                                                              | Initializes a new instance of SQLServerMetaData using the column name, sql type, precision and scale.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| Public SQLServerMetaData(SQLServerMetaData sqlServerMetaData)                                                                                                                    | Initializes a new instance of SQLServerMetaData from another SQLServerMetaData object.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
| public String getColumName()                                                                                                                                                     | Retrieves the column name.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| public int getSqlType()                                                                                                                                                          | Retrieves the java sql Type.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| public int getPrecision()                                                                                                                                                        | Retrieves the precision of the type passed to the column.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| public int getScale()                                                                                                                                                            | Retrieves the scale of the type passed to the column.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| public SQLServerSortOrder getSortOrder()                                                                                                                                         | Retrieves the sort order.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| public int getSortOrdinal()                                                                                                                                                      | Retrieves the sort ordinal.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| public boolean isUniqueKey()                                                                                                                                                     | Returns whether the column is unique.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| public boolean useServerDefault()                                                                                                                                                | Returns whether the column uses the default server value.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
  
### SQLServerSortOrder

An Enum that defines the sort order. Possible values are Ascending, Descending and Unspecified.
  
### SQLServerDataTable

This class represents an in-memory data table to be used with table-valued parameters. The methods in this class are:  

| Name                                                          | Description                                          |
| ------------------------------------------------------------- | ---------------------------------------------------- |
| Public SQLServerDataTable()                                   | Initializes a new instance of SQLServerDataTable.    |
| public Iterator<Entry\<Integer, Object[]>> getIterator()      | Retrieves an iterator on the rows of the data table. |
| public void addColumnMetadata(String columnName, int sqlType) | Adds metadata for the specified column.              |
| public void addColumnMetadata(SQLServerDataColumn column)     | Adds metadata for the specified column.              |
| public void addRow(Object... values)                          | Adds one row of data to the data table.              |
| public Map\<Integer, SQLServerDataColumn> getColumnMetadata() | Retrieves column meta data of this data table.       |
| public void clear()                                           | Clears this data table.                              |

### SQLServerDataColumn

This class represents a column of the in-memory data table represented by SQLServerDataTable. The methods in this class are:  

| Name                                                       | Description                                                                      |
| ---------------------------------------------------------- | -------------------------------------------------------------------------------- |
| public SQLServerDataColumn(String columnName, int sqlType) | Initializes a new instance of SQLServerDataColumn with the column name and type. |
| public String getColumnName()                              | Retrieves the column name.                                                       |
| public int getColumnType()                                 | Retrieves the column type.                                                       |

### ISQLServerDataRecord

This class represents an interface that users can implement to stream data to a table-valued parameter. The methods in this interface are:  
  
| Name                                                    | Description                                                                                             |
| ------------------------------------------------------- | ------------------------------------------------------------------------------------------------------- |
| public SQLServerMetaData getColumnMetaData(int column); | Retrieves the column meta data of the given column index.                                               |
| public int getColumnCount();                            | Retrieves the total number of columns.                                                                  |
| public Object[] getRowData();                           | Retrieves the data for the current row as an array of Objects.                                          |
| public boolean next();                                  | Moves to the next row. Returns True if the move is successful and there's a next row, false otherwise. |

### SQLServerPreparedStatement

The following methods have been added to this class to support passing of table-valued parameters.  

| Name                                                                                                    | Description                                                                                                                                                                                                                                                                                                |
| ------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| public final void setStructured(int parameterIndex, String tvpName, SQLServerDataTable tvpDataTable)    | Populates a table-valued parameter with a data table. parameterIndex is the parameter index, tvpName is the name of the table-valued parameter, and tvpDataTable is the source data table object.                                                                                                          |
| public final void setStructured(int parameterIndex, String tvpName, ResultSet tvpResultSet)             | Populates a table-valued parameter with a ResultSet retrieved from another table. parameterIndex is the parameter index, tvpName is the name of the table-valued parameter, and tvpResultSet is the source result set object.                                                                               |
| public final void setStructured(int parameterIndex, String tvpName, ISQLServerDataRecord tvpDataRecord) | Populates a table-valued parameter with an ISQLServerDataRecord object. ISQLServerDataRecord is used for streaming data and the user decides how to use it. parameterIndex is the parameter index, tvpName is the name of the table-valued parameter, and tvpDataRecord is an ISQLServerDataRecord object. |
  
### SQLServerCallableStatement

The following methods have been added to this class to support passing of table-valued parameters.  
  
| Name                                                                                                        | Description                                                                                                                                                                                                                                                                                                                      |
| ----------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| public final void setStructured(String paratemeterName, String tvpName, SQLServerDataTable tvpDataTable)    | Populates a table-valued parameter passed to a stored procedure with a data table. paratemeterName is the name of the parameter, tvpName is the name of the type TVP, and tvpDataTable is the data table object.                                                                                                                 |
| public final void setStructured(String paratemeterName, String tvpName, ResultSet tvpResultSet)             | Populates a table-valued parameter passed to a stored procedure with a ResultSet retrieved from another table. paratemeterName is the name of the parameter, tvpName is the name of the type TVP, and tvpResultSet is the source result set object.                                                                              |
| public final void setStructured(String paratemeterName, String tvpName, ISQLServerDataRecord tvpDataRecord) | Populates a table-valued parameter passed to a stored procedure with an ISQLServerDataRecord object. ISQLServerDataRecord is used for streaming data and the user decides how to use it. paratemeterName is the name of the parameter, tvpName is the name of the type TVP, and tvpDataRecord is an ISQLServerDataRecord object. |

## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)
