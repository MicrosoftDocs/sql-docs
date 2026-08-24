---
title: Use Vector Data Type
description: Learn about the vector data type in the JDBC driver, including FLOAT32 and FLOAT16 subtypes, and how it can be used to support various operations.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 03/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
---
# Use vector data type with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Starting with version 13.2.0, the JDBC driver supports the **vector** data type. Starting with version 13.4.0, the driver also supports the **vector(float16)** subtype. **Vector** is also supported with features such as Table-Valued Parameters and BulkCopy, with some limitations. This page shows various use cases of the **vector** data type with the JDBC Driver. For an overview of **vector** data types, see [Vector data type](../../t-sql/data-types/vector-data-type.md).

## Create a vector object

The microsoft.sql.Vector class is a custom implementation designed to represent vector data in the JDBC driver. It provides a structured way to handle high-dimensional data, including serialization and deserialization, while ensuring compatibility with SQL Server's **vector** data type.
The vector object should include the number of elements, type indicator and data.

```java
public enum VectorDimensionType {
        FLOAT32 // 32-bit (single precision) float
}

private VectorDimensionType vectorType; // The type of values in the data array
private int dimensionCount; // Number of dimensions in the vector
private Object[] data; // An array containing the vector's numerical values
```

The Microsoft JDBC Driver for SQL Server offers two methods to initialize a vector object, providing flexibility for different use cases and input data.

### Create a vector object with dimension count and vector type

Constructor:

```java
public Vector(int dimensionCount, VectorDimensionType vectorType, Object[] data);
```

Example:

```java
Vector vector = new Vector(3, Vector.VectorDimensionType.FLOAT32, new Float[]{1.0f, 2.0f, 3.0f});
```

### Create a vector object with precision and scale

Constructor:

```java
public Vector(int precision, int scale, Object[] data);
```

Where the parameters are:

- *precision*: The number of dimensions in the **vector**.
- *scale*:  The scale value, which represents the number of bytes per dimension. 4 bytes represents a FLOAT32.
- *data*: A float[] array containing the numerical values in the **vector**.

Example:

```java
Vector vector = new Vector(3, 4, new Float[]{1.0f, 2.0f, 3.0f});
```

## Populate and retrieve a table

Here are some code examples that populate and retrieve **vector** data from a database. The examples assume a table with a **vector** column, defined as follows:

```sql
CREATE TABLE sampleTable (vector_col vector(3))
```

Insert values with a plain INSERT statement:

```java
try (Statement stmt = connection.createStatement()){
    stmt.execute("insert into sampleTable values ('[0.1, 2.0, 3.0]')");
}
```

Insert values with a prepared statement and a **vector** parameter:

```java
Vector vector = new Vector(3, Vector.VectorDimensionType.FLOAT32, new Float[]{1.0f, 2.0f, 3.0f});
try (PreparedStatement preparedStatement = con.prepareStatement("insert into sampleTable values (?)")) {
    preparedStatement.setObject(1, vector, microsoft.sql.Types.VECTOR);
    preparedStatement.execute();
}
```

Insert a null value with a prepared statement and a parameter:

```java
Vector vector = new Vector(1, Vector.VectorDimensionType.FLOAT32, null);
try (PreparedStatement preparedStatement = con.prepareStatement("insert into sampleTable values (?)")) {
    preparedStatement.setObject(1, vector, microsoft.sql.Types.VECTOR);
    preparedStatement.execute();
}
```

```java
try (PreparedStatement preparedStatement = con.prepareStatement("insert into sampleTable values (?)")) {
    preparedStatement.setObject(1, null)
    preparedStatement.execute();
}
```

Read **vector** values from a table:

```java
try (SQLServerResultSet resultSet = (SQLServerResultSet) stmt.executeQuery("select * from sampleTable")) {
    assertTrue(resultSet.next(), "No result found for inserted vector.");
    ResultSetMetaData metaData = resultSet.getMetaData();
    int columnCount = metaData.getColumnCount();

    while (resultSet.next()) {
        for (int i = 1; i <= columnCount; i++) {
            String columnName = metaData.getColumnName(i);
            int columnType = metaData.getColumnType(i); // from java.sql.Types

            Object value = null;
            switch (columnType) {
                case Types.VARCHAR:
                case Types.NVARCHAR:
                    value = resultSet.getString(i);
                    break;
                case microsoft.sql.Types.VECTOR:
                    value = resultSet.getObject(i, microsoft.sql.Vector.class);
            }

            System.out.println(columnName + " = " + value + " (type: " + columnType + ")");
        }
    }
}
```

## Use stored procedures with vector

With the following stored procedure:

```java
String sql = "CREATE PROCEDURE " + inputProc +
             " @p0 VECTOR(3) OUTPUT AS " +
             " SELECT TOP 1 @p0 = col_vector FROM sampleTable ";
```

Return a **vector** output parameter with the following example:

```java
try (CallableStatement callableStatement = con.prepareCall("{call " + inputProc + " (?) }")) {
    callableStatement.registerOutParameter(1, microsoft.sql.Types.VECTOR, 3, 4);
    callableStatement.execute();
}
```

## Use TVP with vector

```java
Vector vector = new Vector(3, Vector.VectorDimensionType.FLOAT32, new Float[]{1.0f, 2.0f, 3.0f});
SQLServerDataTable tvp = new SQLServerDataTable();
tvp.addColumnMetadata("vector_col", microsoft.sql.Types.VECTOR);
tvp.addRow(vector);

try (SQLServerPreparedStatement preparedStatement = (SQLServerPreparedStatement) con.prepareStatement( "insert into sampleTable select * from (?)")) {
    pstmt.setStructured(1, TVP_NAME, tvp);
    pstmt.execute();
}
```

## Use SQLServerBulkCopy from source table to destination table with vector

Vectors can be used in bulk copy commands:

```java
Vector vector = new Vector(3, Vector.VectorDimensionType.FLOAT32, new Float[]{1.0f, 2.0f, 3.0f});
try (Statement stmt = con.createStatement();
        SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(con)) {

        stmt.executeUpdate("create table destinationTable (vector_col vector(3))");

        bulkCopy.setDestinationTableName("destinationTable");
        bulkCopy.writeToServer(vector);
    }
```

## Use bulkCopy from csv file to destination table with vector

Vectors can be imported from CSV files.

Paste the following contents into a CSV file named `vectors.csv`:

```text
vector_col
"[1.0,2.0,3.0]"
"[4.0,5.0,6.0]"
```

```java
try (Statement stmt = con.createStatement();
    SQLServerBulkCopy bulkCopy = new SQLServerBulkCopy(con);
    SQLServerBulkCSVFileRecord fileRecord = new SQLServerBulkCSVFileRecord("vectors.csv", null, ",", true)) {

        stmt.executeUpdate("create table destinationTable (vector_col vector(3))");

        // Add column metadata for the CSV file
        fileRecord.addColumnMetadata(1, "vector_col", microsoft.sql.Types.VECTOR, 3, 4);
        fileRecord.setEscapeColumnDelimitersCSV(true);

        bulkCopy.setDestinationTableName("destinationTable");
        bulkCopy.writeToServer(fileRecord);
}
```

## Use vector FLOAT16 data type

FLOAT16 (IEEE 754 half-precision, 16-bit) vectors use 2 bytes per dimension instead of 4, halving the storage footprint and enabling up to 3,996 dimensions per vector (vs. 1,998 for FLOAT32) within the 8,000-byte TDS limit. This subtype is especially valuable for large-scale AI/ML workloads such as similarity search and embedding storage where memory efficiency matters.

FLOAT16 vector support is enabled through the existing vector feature extension (FE identifier 0x0E) with a version 2 handshake: the client requests v2 via the `vectorTypeSupport` connection property, and the server acknowledges FLOAT16 capability in the feature-ack response.

To use FLOAT16 vectors, follow the same patterns shown in the preceding examples and replace `FLOAT32` with `FLOAT16` and the scale value `4` with `2`. For example:

- Use `Vector.VectorDimensionType.FLOAT16` instead of `Vector.VectorDimensionType.FLOAT32`.
- Use a scale of `2` instead of `4` when constructing a vector with precision and scale.
- Define SQL columns as `vector(3, float16)` instead of `vector(3)`.

Here's an example of creating a FLOAT16 vector object:

```java
// Using dimension count and vector type
Vector vector = new Vector(3, Vector.VectorDimensionType.FLOAT16, new Float[]{1.0f, 2.0f, 3.0f});

// Using precision and scale (2 bytes = FLOAT16)
Vector vector = new Vector(3, 2, new Float[]{1.0f, 2.0f, 3.0f});
```

## Backward compatibility

If an application isn't updated to handle the **vector** data type, the driver provides backward compatibility by allowing **vector** data types to be read as backward compatible types. This behavior is controlled with the `vectorTypeSupport` connection string property.
Supported values are `off` (server sends **vector** types as string data in JSON format), `v1` (server sends **vector** types of FLOAT32 as vector data), and `v2` (to enable **vector** type support for FLOAT16). The default value is `v1`.

## Limitations of vector

For detailed limitations, see [Vector data type limitations](../../t-sql/data-types/vector-data-type.md#limitations).

## Related content

- [Understanding the JDBC driver data types](understanding-the-jdbc-driver-data-types.md)
