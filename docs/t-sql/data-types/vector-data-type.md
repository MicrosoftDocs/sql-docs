---
title: Vector Data Type
description: The vector data type stores vector data optimized for machine learning applications and similarity search operations.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: pookam, randolphwest
ms.date: 09/22/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: quickstart
ms.collection:
  - ce-skilling-ai-copilot
ms.update-cycle: 180-days
ms.custom:
  - ignite-2025
f1_keywords:
  - "VECTOR"
  - "VECTOR_TSQL"
helpviewer_keywords:
  - "VECTOR data type"
  - "vector, data type"
dev_langs:
  - TSQL
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =fabric-sqldb || =azuresqldb-mi-current"
---

# Vector data type

[!INCLUDE [sqlserver2025-asdb-asmi-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

The **vector** data type stores vector data optimized for operations such as similarity search and machine learning applications. Vectors are stored in an optimized binary format but are exposed as JSON arrays for convenience. Each element of the vector is stored as a single-precision (4-byte) floating-point value.

To provide a familiar experience for developers, the **vector** data type is created and displayed as a JSON array. For example, a vector with three dimensions can be represented as `'[0.1, 2, 30]'`. You can implicitly and explicitly convert between the **vector** type and **varchar**, **nvarchar**, and **json** types.

For more information on working with vector data, see:

- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Intelligent applications](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications#vector-search)

## Sample syntax

The usage syntax for the **vector** type is similar to all other SQL Server data types in a table.

```syntaxsql
column_name VECTOR ( { <dimensions> } ) [ NOT NULL | NULL ]
```

By default, the base type is **float32**. To use **half-precision**, specify **float16** explicitly.

```syntaxsql
column_name VECTOR ( <dimensions> [ , <base_type> ] ) [ NOT NULL | NULL ]
```

### Dimensions

A vector must have at least one dimension. The maximum number of dimensions depends on the vector base type:

- **float32** supports up to 1,998 dimensions.
- **float16** supports up to 3,996 dimensions.

## Feature availability

The **vector** data type is available under all database compatibility levels. If you don't specify a base type, the vector uses **float32**.

Vector features are available in Azure SQL Managed Instance configured with the [Always-up-to-date](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy) policy.

### Half-precision float16 vectors

- Half-precision (**float16**) vectors are generally available in Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. No preview configuration is required.

- In SQL Server 2025 (17.x), half-precision (**float16**) vectors are available in preview. To use **float16** in SQL Server 2025, you must enable the `PREVIEW_FEATURES` database scoped configuration option. For more information, see [Enable preview features in SQL Server 2025](#enable-preview-features-in-sql-server-2025).

For more information about half-precision vectors, see [Half-precision float support in the vector data type](vector-data-type-half-precision-float.md).

### Enable preview features in SQL Server 2025

For some preview features in SQL Server 2025, you must enable the `PREVIEW_FEATURES` database scoped configuration option.

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION
   SET PREVIEW_FEATURES = ON;
   GO
   ```

For more information, see [PREVIEW_FEATURES](../statements/alter-database-scoped-configuration-transact-sql.md#preview_features---on--off-).

## Examples

### A. Column definition

Use the **vector** type in column definitions within a `CREATE TABLE` statement. For example:

The following example creates a table with a vector column and inserts data into it.

Define a **vector** column in a table by using either the default base type **float32** or **float16** for half-precision storage.

```sql
CREATE TABLE dbo.vectors
(
    id INT PRIMARY KEY,
    v VECTOR(3) NOT NULL -- Uses default base type (**float32**)
);

CREATE TABLE dbo.vectors_fp16
(
    id INT PRIMARY KEY,
    v VECTOR(3, float16) -- Uses float16 for reduced storage and precision
);

INSERT INTO dbo.vectors (id, v)
VALUES (1, '[0.1, 2, 30]'),
       (2, '[-100.2, 0.123, 9.876]'),
       (3, JSON_ARRAY(1.0, 2.0, 3.0)); -- Using JSON_ARRAY to create a vector

SELECT *
FROM dbo.vectors;
```

### B. Usage in variables

The following example declares vectors by using the new **vector** data type and calculates distances by using the `VECTOR_DISTANCE` function.

Use the **vector** type with variables:

```sql
DECLARE @v AS VECTOR(3) = '[0.1, 2, 30]';
SELECT @v;
GO
DECLARE @v AS VECTOR(3, float16) = '[0.1, 2, 30]';
SELECT @v;
```

### C. Usage in stored procedures or functions

You can use the **vector** data type as a parameter in stored procedures or functions. For example:

```sql
CREATE PROCEDURE dbo.SampleStoredProcedure
@V VECTOR(3),
@V2 VECTOR(3) OUTPUT
AS
BEGIN
    SELECT @V;
    SET @V2 = @V;
END
```

## Conversions

- You can't use the **vector** type with the **sql_variant** type or assign it to a **sql_variant** variable or column. This restriction is similar to **varchar(max)**, **varbinary(max)**, **nvarchar(max)**, **xml**, **json**, and CLR-based data types.

## Compatibility

### Enhancements to TDS protocol

SQL Server stores vectors in an optimized binary format but exposes them as JSON arrays for convenience.

**Supported** drivers use enhancements to the TDS protocol to transmit vector data more efficiently in binary format and present them to applications as native vector types. This approach reduces payload size, eliminates the overhead of JSON parsing, and preserves full floating-point precision. As a result, it improves both performance and accuracy when working with high-dimensional vectors in AI and machine learning scenarios.

#### Native Driver Support

Applications using **TDS version 7.4 or higher** and updated drivers can natively read, write, stream, and bulk copy vector data.

These capabilities require versions of the following drivers. Ensure you're using the correct version to enable native vector support.

- **Microsoft.Data.SqlClient**: Version **6.1.0** introduces the `SqlVector` type, extending `System.Data.SqlDbTypes`.
- **Microsoft JDBC Driver for SQL Server**: Version **13.1.0 Preview** introduces the `microsoft.sql.Types.VECTOR` type and `microsoft.sql.Vector` class.

> [!NOTE]
> Native binary transport for **float16** vectors is supported in [Microsoft JDBC Driver 13.4 for SQL Server](https://github.com/microsoft/mssql-jdbc/releases/tag/v13.4.0) and [Microsoft ODBC Driver 18.7.1 for SQL Server](../../connect/odbc/download-odbc-driver-for-sql-server.md). These drivers serialize and deserialize half-precision vector values using the native wire format, reducing the network payload compared to the JSON representation.
> Use these driver versions or later for native **float16** transport. Drivers that don't support native **float16** transport can continue to work with vectors represented as `varchar(max)` JSON arrays.
>
> [!NOTE]  
> For clients that don't support the updated TDS protocol, SQL Server continues to expose vector data as **varchar(max)** types to ensure backward compatibility. Client applications can work with vector data as if it were a JSON array. The SQL Database Engine automatically converts vectors to and from a JSON array, making the new type transparent for the client. Hence drivers and all languages are automatically compatible with the new type.

You can start using the new **vector** type right away. The following examples show different languages and driver configurations.

### [.NET](#tab/csharp)

> [!IMPORTANT]  
> Requires **Microsoft.Data.SqlClient 6.1.0** or later for native vector support.

```csharp
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

namespace VectorSampleApp
{
    class Program
    {
        // Set your environment variable or fallback to local server
        private static readonly string connectionString =
            Environment.GetEnvironmentVariable("CONNECTION_STR")
            ?? "Server=tcp:localhost,1433;Database=Demo2;Integrated Security=True;TrustServerCertificate=True";

        private const int VectorDimensions = 3;
        private const string TableName = "dbo.Vectors";

        static void Main()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            SetupTables(connection, TableName, VectorDimensions);
            InsertVectorData(connection, TableName);
            ReadVectorData(connection, TableName);
        }

        private static void SetupTables(SqlConnection connection, string tableName, int vectorDimensionCount)
        {
            using var command = connection.CreateCommand();

            command.CommandText = $@"
                IF OBJECT_ID('{tableName}', 'U') IS NOT NULL DROP TABLE {tableName};
                IF OBJECT_ID('{tableName}Copy', 'U') IS NOT NULL DROP TABLE {tableName}Copy;";
            command.ExecuteNonQuery();

            command.CommandText = $@"
                CREATE TABLE {tableName} (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    VectorData VECTOR({vectorDimensionCount})
                );

                CREATE TABLE {tableName}Copy (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    VectorData VECTOR({vectorDimensionCount})
                );";
            command.ExecuteNonQuery();
        }

        private static void InsertVectorData(SqlConnection connection, string tableName)
        {
            using var command = new SqlCommand($"INSERT INTO {tableName} (VectorData) VALUES (@VectorData)", connection);
            var param = command.Parameters.Add("@VectorData", SqlDbTypeExtensions.Vector);

            // Insert null using DBNull.Value
            param.Value = DBNull.Value;
            command.ExecuteNonQuery();

            // Insert non-null vector
            param.Value = new SqlVector<float>(new float[] { 3.14159f, 1.61803f, 1.41421f });
            command.ExecuteNonQuery();

            // Insert typed null vector
            param.Value = SqlVector<float>.CreateNull(VectorDimensions);
            command.ExecuteNonQuery();

            // Prepare once and reuse for loop
            command.Prepare();
            for (int i = 0; i < 10; i++)
            {
                param.Value = new SqlVector<float>(new float[]
                {
                    i + 0.1f,
                    i + 0.2f,
                    i + 0.3f
                });
                command.ExecuteNonQuery();
            }
        }

        private static void ReadVectorData(SqlConnection connection, string tableName)
        {
            using var command = new SqlCommand($"SELECT VectorData FROM {tableName}", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var sqlVector = reader.GetSqlVector<float>(0);

                Console.WriteLine($"Type: {sqlVector.GetType()}, IsNull: {sqlVector.IsNull}, Length: {sqlVector.Length}");

                if (!sqlVector.IsNull)
                {
                    float[] values = sqlVector.Memory.ToArray();
                    Console.WriteLine("VectorData: " + string.Join(", ", values));
                }
                else
                {
                    Console.WriteLine("VectorData: NULL");
                }
            }
        }
    }
}
```

> [!NOTE]  
> If you don't use the latest .NET drivers, you can still work with vector data in [!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)] by serializing and deserializing it as a JSON string by using the `JsonSerializer` class. This approach ensures compatibility with the `varchar(max)` representation of vectors that SQL Server exposes for older clients.

```csharp
using Microsoft.Data.SqlClient;
using Dapper;
using DotNetEnv;
using System.Text.Json;

namespace DotNetSqlClient;

class Program
{
    static void Main(string[] args)
    {
        Env.Load();

        var v1 = new float[] { 1.0f, 2.0f, 3.0f };

        using var conn = new SqlConnection(Env.GetString("MSSQL"));
        conn.Execute("INSERT INTO dbo.vectors VALUES(100, @v)", param: new {@v = JsonSerializer.Serialize(v1)});

        var r = conn.ExecuteScalar<string>("SELECT v FROM dbo.vectors") ?? "[]";
        var v2 = JsonSerializer.Deserialize<float[]>(r);
        Console.WriteLine(JsonSerializer.Serialize(v2));
    }
}
```

### [JDBC](#tab/jdbc)

**Applies to:** Microsoft JDBC Driver for SQL Server 13.1.0 and later versions.

Example of inserting **vector** data into a table:

```java
@Test
public void getVectorData() throws SQLException {
    String query = "SELECT v FROM " + AbstractSQLGenerator.escapeIdentifier(tableName);
    try (PreparedStatement stmt = connection.prepareStatement(query)) {
        try (ResultSet rs = stmt.executeQuery()) {
            assertTrue(rs.next(), "No result found for inserted vector.");
            ResultSetMetaData meta = rs.getMetaData();
            int columnCount = meta.getColumnCount();

            while (rs.next()) {
                for (int i = 1; i <= columnCount; i++) {
                    String columnName = meta.getColumnName(i);
                    int columnType = meta.getColumnType(i); // from java.sql.Types

                    Object value = null;
                    switch (columnType) {
                        case Types.VARCHAR:
                        case Types.NVARCHAR:
                            value = rs.getString(i);
                            break;
                        case microsoft.sql.Types.VECTOR:
                            value = rs.getObject(i, microsoft.sql.Vector.class);
                    }

                    System.out.println(columnName + " = " + value + " (type: " + columnType + ")");
                }
                System.out.println("---");
            }
        }
    }
}
```

Example of selecting **vector** data from a table:

```java
@Test
    public void getVectorData() throws SQLException {
        String query = "SELECT v FROM " + AbstractSQLGenerator.escapeIdentifier(tableName);
        try (PreparedStatement stmt = connection.prepareStatement(query)) {
            try (ResultSet rs = stmt.executeQuery()) {
                assertTrue(rs.next(), "No result found for inserted vector.");
                ResultSetMetaData meta = rs.getMetaData();
                int columnCount = meta.getColumnCount();

                while (rs.next()) {
                    for (int i = 1; i <= columnCount; i++) {
                        String columnName = meta.getColumnName(i);
                        int columnType = meta.getColumnType(i); // from java.sql.Types

                        Object value = null;
                        switch (columnType) {
                            case Types.VARCHAR:
                            case Types.NVARCHAR:
                                value = rs.getString(i);
                                break;
                            case microsoft.sql.Types.VECTOR:
                                value = rs.getObject(i, microsoft.sql.Vector.class);
                        }

                        System.out.println(columnName + " = " + value + " (type: " + columnType + ")");
                    }
                    System.out.println("---");
                }
            }
        }
    }
```

### [Python](#tab/python)

This sample uses Python with the [mssql-python driver](../../connect/python/mssql-python/python-sql-driver-mssql-python.md). Applications can write and read vector data by using `json.loads` and `json.dumps`:

```python
import json
import os, json
import mssql_python

connection_string = os.environ["MSSQL"]
conn = mssql_python.connect(connection_string)

cursor = conn.cursor()
query = "INSERT INTO dbo.vectors VALUES(100, ?)"
cursor.execute(query, json.dumps([1,2,3]))
cursor.close()

cursor = conn.cursor()
query = "SELECT v FROM dbo.vectors"
cursor.execute(query)
rows = cursor.fetchall()
for row in rows:
    print(json.loads(row[0]))
cursor.close()

conn.close()
```

---

## Tools

The following tools support the **vector** data type:

- [SQL Server Management Studio](https://aka.ms/ssms) version 21 and later versions
- [DacFX and SqlPackage](../../tools/sqlpackage/release-notes-sqlpackage.md) version 162.5 (November 2024) and later versions
- The [SQL Server extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md) version 1.32 (May 2025) and later versions
- [Microsoft.Build.Sql](../../tools/sql-database-projects/sql-database-projects.md) version 1.0.0 (March 2025) and later versions
- [SQL Server Data Tools (Visual Studio 2022)](https://visualstudio.microsoft.com/downloads/) version 17.13 and later versions

## Limitations

The **vector** type has the following limitations:

### Tables

- Column-level constraints aren't supported, except for `NULL` and `NOT NULL` constraints.

  - `DEFAULT` and `CHECK` constraints aren't supported for **vector** columns.

  - Key constraints, such as `PRIMARY KEY` or `FOREIGN KEY`, aren't supported for **vector** columns. Equality, uniqueness, joins using vector columns as keys, and sort orders don't apply to **vector** data types.

  - There's no notion of uniqueness for vectors, so unique constraints aren't applicable.

  - Checking the range of values within a vector isn't applicable.

- Vectors don't support comparison, addition, subtraction, multiplication, division, concatenation, or any other mathematical, logical, and compound assignment operators.

- You can't use **vector** columns in memory-optimized tables.

### Indexes

- You can't use B-tree indexes or columnstore indexes on **vector** columns. However, you can include a **vector** column as an included column in an index definition.
- [Vector indexes](../statements/create-vector-index-transact-sql.md) create an approximate index on a vector column to improve the performance of nearest neighbors search. To learn more about how vector indexing and vector search works, and the differences between exact and approximate search, see [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md).

### Table schema metadata

- The [sp_describe_first_result_set](../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md) system stored procedure doesn't correctly return the **vector** data type. As a result, many data access clients and drivers see a **varchar** or **nvarchar** data type.

### Ledger tables

- The stored procedure `sp_verify_database_ledger` generates an error if the database contains a table with a **vector** column.

### User-defined types

- You can't create an alias type using `CREATE TYPE` for the **vector** type. This restriction is similar to the behavior of the **xml** and **json** data types.

### Always Encrypted

- The **vector** type isn't supported with the Always Encrypted feature.

## Known issues

- Data Masking currently shows **vector** data as **varbinary** data type in the Azure portal.

## Related content

- [Half-precision float support in vector data type](vector-data-type-half-precision-float.md)
- [Vector search and vector indexes in the SQL Database Engine](../../sql-server/ai/vectors.md)
- [Intelligent applications](/azure/azure-sql/database/ai-artificial-intelligence-intelligent-applications?view=azuresql&preserve-view=true#vector-search)
- [Vector functions](../functions/vector-functions-transact-sql.md)
