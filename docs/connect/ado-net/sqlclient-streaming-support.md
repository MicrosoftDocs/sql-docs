---
title: SqlClient streaming support
description: Stream large binary, text, XML, and JSON values between a SQL database and an application without buffering the complete value.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, cmalhotra, davidengel, paulmedynski
ms.date: 10/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
dev_langs:
  - csharp
ai-usage: ai-assisted
---

# SqlClient streaming support

Use Microsoft.Data.SqlClient streaming methods to process large values without first loading the complete value into application memory. Streaming is useful for binary files, large text, XML, JSON, and server-to-server transfer.

Streaming doesn't make the value seekable or make concurrent operations on a reader safe. The connection, command, reader, source stream, and destination object must remain alive until the transfer finishes.

The examples in this article assume that an open <xref:Microsoft.Data.SqlClient.SqlConnection> named `connection` targets the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

The C# examples require `using Microsoft.Data.SqlClient;`. Examples that use `SqlDbType` or `CommandBehavior` also require `using System.Data;`.

<a id="streaming-support-from-sql-server"></a>

## Stream data from a SQL database

Execute the command with `CommandBehavior.SequentialAccess` and read columns in ordinal order. With sequential access, after you advance past a column, don't read an earlier column in the same row.

Use these <xref:Microsoft.Data.SqlClient.SqlDataReader> methods:

| Method | Result | Typical SQL data |
| --- | --- | --- |
| <xref:Microsoft.Data.SqlClient.SqlDataReader.GetStream%2A> | <xref:System.IO.Stream> | **binary**, **varbinary**, **image**, and user-defined type (UDT) data |
| <xref:Microsoft.Data.SqlClient.SqlDataReader.GetTextReader%2A> | <xref:System.IO.TextReader> | Character and JSON data |
| <xref:Microsoft.Data.SqlClient.SqlDataReader.GetXmlReader%2A> | <xref:System.Xml.XmlReader> | **xml** |

Call `IsDBNull` before requesting a stream for a nullable column. A streaming getter used with an incompatible SQL type throws. Consume or dispose the current stream before reading another column, advancing the row, or moving to the next result.

`GetStream` isn't supported for encrypted columns. `GetTextReader` doesn't support sequential access to an encrypted column.

`CommandTimeout` applies while Microsoft.Data.SqlClient waits for network packets during execution and reading. Time that your application spends processing already received data isn't counted. For cancellation and timeouts, see [Asynchronous programming](asynchronous-programming.md).

<a id="example"></a>
<a id="sample--streaming-from-sql-server"></a>

### Stream binary data

AdventureWorks stores file content in the nullable **varbinary(max)** `Production.Document.Document` column. This example copies one value to a file with a bounded buffer:

```csharp
using var command = new SqlCommand("""
    SELECT TOP (1) Document
    FROM Production.Document
    WHERE Document IS NOT NULL
    ORDER BY ModifiedDate DESC;
    """, connection);

using SqlDataReader reader = command.ExecuteReader(
    CommandBehavior.SequentialAccess);
if (reader.Read())
{
    using Stream source = reader.GetStream(0);
    using FileStream destination = File.Create("document.bin");
    source.CopyTo(destination);
}
```

For text, use `GetTextReader` and copy to a <xref:System.IO.TextWriter>. For XML, use `GetXmlReader` and process XML nodes without converting the complete value to string.

<a id="streaming-support-to-sql-server"></a>

## Stream data to a SQL database

Set <xref:Microsoft.Data.SqlClient.SqlParameter.Value%2A> to a streaming object and choose a compatible SQL type:

| Value type | Compatible parameter types |
| --- | --- |
| <xref:System.IO.Stream> | `Binary`, `VarBinary`, and the legacy `Image` type |
| <xref:System.IO.TextReader> | `Char`, `NChar`, `VarChar`, `NVarChar`, the legacy `Text` and `NText` types, `Xml`, and `Json` |
| <xref:System.Xml.XmlReader> | `Char`, `NChar`, `VarChar`, `NVarChar`, the legacy `Text` and `NText` types, `Xml`, and `Json` |

Set <xref:Microsoft.Data.SqlClient.SqlParameter.Size%2A> explicitly. `Size` limits the amount transferred. Use `-1` for a `(max)` parameter when you want to send the complete source.

For bounded-memory transfer, use `VarBinary`, `VarChar`, or `NVarChar` with `Size = -1`, or use `Xml` or `Json`. Microsoft.Data.SqlClient buffers the source into a byte array or string before execution for non-`(max)` character and binary types and the legacy `Image`, `Text`, and `NText` types.

<a id="sample--streaming-to-sql-server"></a>

### Stream a parameter value

This example sends a file through a **varbinary(max)** parameter and returns the number of bytes received. It doesn't store the value:

```csharp
using FileStream input = File.OpenRead("document.bin");
using var command = new SqlCommand(
    "SELECT DATALENGTH(@content);",
    connection);
command.Parameters.Add(
    new SqlParameter("@content", SqlDbType.VarBinary, -1)
    {
        Value = input
    });

long length = (long)command.ExecuteScalar()!;
Console.WriteLine($"{length} bytes");
```

Keep the input object open until command execution completes. Pass a cancellation token to an asynchronous execution method or call `Cancel` to request cancellation of the streaming operation. Cancellation is best effort. The operation might finish before cancellation takes effect.

An exception from the source stream or reader faults command execution. Don't retry from the same object unless you can establish its position and the database operation is safe to repeat.

<a id="sample--streaming-from-one-sql-server-to-another-sql-server"></a>

## Stream data from one SQL database to another

This example passes an AdventureWorks document stream directly to a parameter on another open `SqlConnection` named `destinationConnection`. The sample command reports the received length. Replace it with the destination database's parameterized insert or update command:

```csharp
using var readSource = new SqlCommand("""
    SELECT TOP (1) Document
    FROM Production.Document
    WHERE Document IS NOT NULL
    ORDER BY ModifiedDate DESC;
    """, connection);

using SqlDataReader sourceReader = readSource.ExecuteReader(
    CommandBehavior.SequentialAccess);

if (sourceReader.Read())
{
    using Stream content = sourceReader.GetStream(0);
    using var write = new SqlCommand(
        "SELECT DATALENGTH(@content);",
        destinationConnection);
    write.Parameters.Add(
        new SqlParameter("@content", SqlDbType.VarBinary, -1)
        {
            Value = content
        });

    Console.WriteLine(write.ExecuteScalar());
}
```

The source and destination commands don't share a transaction. If your application requires coordinated atomic work across servers, design and test that transaction boundary separately.

## Related content

- [Retrieve data with SqlDataReader](retrieve-data-by-datareader.md)
- [Configure streaming parameters](configure-parameters.md#configure-streaming-values)
- [Asynchronous programming](asynchronous-programming.md)
- [Binary and large-value data](sql/sql-server-binary-large-value-data.md)
