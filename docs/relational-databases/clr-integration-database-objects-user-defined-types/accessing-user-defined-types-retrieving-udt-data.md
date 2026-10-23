---
title: "Retrieving UDT Data"
description: This article describes how to access UDTs in a SQL Server database.
author: uc-msft
ms.author: umajay
ms.date: 03/19/2026
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "SqlDataReader object"
  - "ADO.NET [CLR integration]"
  - "binding UDTs [CLR integration]"
  - "UDTs [CLR integration], ADO.NET"
  - "assemblies [CLR integration], user-defined types"
  - "query parameters [CLR integration]"
  - "user-defined types [CLR integration], ADO.NET"
  - "bytes [CLR integration]"
dev_langs:
  - "VB"
  - "CSharp"
ms.custom: sfi-ropc-nochange
---
# Retrieve user-defined type (UDT) data in ADO.NET

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To create a user-defined type (UDT) on the client, the assembly registered as a UDT in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database must be available to the client application. The UDT assembly can be placed in the same directory with the application, or in the Global Assembly Cache (GAC). You can also set a reference to the assembly in your project.

## Requirements for using UDTs in ADO.NET

The assembly loaded in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the assembly on the client must be compatible for the UDT to be created on the client. For UDTs defined with the `Native` serialization format, the assemblies must be structurally compatible. For assemblies defined with the `UserDefined` format, the assembly must be available on the client.

You don't need a copy of the UDT assembly on the client to retrieve the raw data from a UDT column in a table.

> [!NOTE]  
> `SqlClient` might fail to load a UDT if UDT versions are mismatched or other problems occur. In this case, use regular troubleshooting mechanisms to determine why the assembly containing the UDT can't be found by the calling application. For more information, see [Diagnose Errors with Managed Debugging Assistants](/dotnet/framework/debug-trace-profile/diagnosing-errors-with-managed-debugging-assistants).

The code examples in this article use `Microsoft.Data.SqlClient`, which is available as a NuGet package. To add this dependency to your project, run the following command:

```dotnetcli
dotnet add package Microsoft.Data.SqlClient
```

## Access UDTs with a SqlDataReader

Use a `Microsoft.Data.SqlClient.SqlDataReader` from client code to retrieve a result set that contains a UDT column, which is exposed as an instance of the object.

### Example

This example shows how to use the `Main` method to create a new `SqlDataReader` object. The following actions occur within the code example:

1. The Main method creates a new `SqlDataReader` object and retrieves the values from the Points table, which has a UDT column named Point.

1. The `Point` UDT exposes X and Y coordinates defined as integers.

1. The UDT defines a `Distance` method and a `GetDistanceFromXY` method.

1. The sample code retrieves the values of the primary key and UDT columns to demonstrate the capabilities of the UDT.

1. The sample code calls the `Point.Distance` and `Point.GetDistanceFromXY` methods.

1. The results display in the console window.

> [!NOTE]  
> The application must already have a reference to the UDT assembly.

### [C#](#tab/csharp)

```csharp
using System;
using Microsoft.Data.SqlClient;

namespace Microsoft.Samples.SqlServer
{
    class ReadPoints
    {
        static void Main()
        {
            string connectionString = GetConnectionString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT ID, Pnt FROM dbo.Points", cnn);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Retrieve the value of the Primary Key column
                    int id = rdr.GetInt32(0);

                    // Retrieve the value of the UDT
                    Point pnt = (Point)rdr[1];

                    // You can also use GetSqlValue and GetValue
                    // Point pnt = (Point)rdr.GetSqlValue(1);
                    // Point pnt = (Point)rdr.GetValue(1);

                    Console.WriteLine(
                        "ID={0} Point={1} X={2} Y={3} DistanceFromXY={4} Distance={5}",
                        id, pnt, pnt.X, pnt.Y, pnt.DistanceFromXY(1, 9), pnt.Distance());
                }
                rdr.Close();
                Console.WriteLine("done");
            }
            static private string GetConnectionString()
            {
                // To avoid storing the connection string in your code,
                // you can retrieve it from a configuration file.
                return "Data Source=(local);Initial Catalog=AdventureWorks2022"
                       + "Integrated Security=SSPI";
            }
        }
    }
}
```

### [Visual Basic .NET](#tab/vb)

```vb
Option Explicit On
Option Strict On

Imports System
Imports Microsoft.Data.SqlClient

Module ReadPoints
    Sub Main()
        Dim connectionString As String = GetConnectionString()
        Using cnn As New SqlConnection(connectionString)
            cnn.Open()
            Dim cmd As New SqlCommand( _
             "SELECT ID, Pnt FROM dbo.Points", cnn)
            Dim rdr As SqlDataReader
            rdr = cmd.ExecuteReader

            While rdr.Read()
                ' Retrieve the value of the Primary Key column
                Dim id As Int32 = rdr.GetInt32(0)

                ' Retrieve the value of the UDT
                Dim pnt As Point = CType(rdr(1), Point)

             ' You can also use GetSqlValue and GetValue
             ' Dim pnt As Point = CType(rdr.GetSqlValue(1), Point)
             ' Dim pnt As Point = CType(rdr.GetValue(1), Point)

                ' Print values
                Console.WriteLine( _
                 "ID={0} Point={1} X={2} Y={3} DistanceFromXY={4} Distance={5}", _
                  id, pnt, pnt.X, pnt.Y, pnt.DistanceFromXY(1, 9), pnt.Distance())
            End While
            rdr.Close()
            Console.WriteLine("done")
        End Using
    End Sub
    Private Function GetConnectionString() As String
        ' To avoid storing the connection string in your code,
        ' you can retrieve it from a configuration file.
        Return "Data Source=(local);Initial Catalog=AdventureWorks2022" _
         & "Integrated Security=SSPI;"
    End Function
End Module
```

---

## Bind UDTs as bytes

In some situations, you might want to retrieve the raw data from the UDT column. Perhaps the type isn't available locally, or you don't want to instantiate an instance of the UDT. You can read the raw bytes into a byte array by using the `GetBytes` method of a `SqlDataReader`. This method reads a stream of bytes from the specified column offset into the buffer of an array starting at a specified buffer offset. Another option is to use one of the `GetSqlBytes` or `GetSqlBinary` methods and read all of the contents in a single operation. In either case, the UDT object is never instantiated, so you don't need to set a reference to the UDT in the client assembly.

### Example

This example shows how to retrieve the `Point` data as raw bytes into a byte array by using a `SqlDataReader`. The code uses a `System.Text.StringBuilder` to convert the raw bytes to a string representation to be displayed in the console window.

### [C#](#tab/csharp)

```csharp
using System;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

class GetRawBytes
{
    static void Main()
    {
        string connectionString = GetConnectionString();
        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ID, Pnt FROM dbo.Points", cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Retrieve the value of the Primary Key column
                int id = rdr.GetInt32(0);

                // Retrieve the raw bytes into a byte array
                byte[] buffer = new byte[32];
                long byteCount = rdr.GetBytes(1, 0, buffer, 0, 32);

                // Format and print bytes
                StringBuilder str = new StringBuilder();
                str.AppendFormat("ID={0} Point=", id);

                for (int i = 0; i < byteCount; i++)
                    str.AppendFormat("{0:x}", buffer[i]);
                Console.WriteLine(str.ToString());
            }
            rdr.Close();
            Console.WriteLine("done");
        }
    static private string GetConnectionString()
    {
        // To avoid storing the connection string in your code,
        // you can retrieve it from a configuration file.
        return "Data Source=(local);Initial Catalog=AdventureWorks2022"
            + "Integrated Security=SSPI";
    }
  }
}
```

### [Visual Basic .NET](#tab/vb)

```vb
Option Explicit On
Option Strict On

Imports System
Imports Microsoft.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Text

Module GetRawBytes
    Sub Main()
        Dim connectionString As String = GetConnectionString()
        Using cnn As New SqlConnection(connectionString)
            cnn.Open()
            Dim cmd As New SqlCommand( _
             "SELECT ID, Pnt FROM dbo.Points", cnn)
            Dim rdr As SqlDataReader
            rdr = cmd.ExecuteReader

            While rdr.Read()

                ' Retrieve the value of the Primary Key column
                Dim id As Int32 = rdr.GetInt32(0)

                ' Retrieve the raw bytes into a byte array
                Dim buffer(31) As Byte
                Dim byteCount As Integer = _
                 CInt(rdr.GetBytes(1, 0, buffer, 0, 31))

                ' Format and print bytes
                Dim str As New StringBuilder
                str.AppendFormat("ID={0} Point=", id)

                Dim i As Integer
                For i = 0 To (byteCount - 1)
                    str.AppendFormat("{0:x}", buffer(i))
                Next
                Console.WriteLine(str.ToString)

            End While
            rdr.Close()
            Console.WriteLine("done")
        End Using
    End Sub
    Private Function GetConnectionString() As String
        ' To avoid storing the connection string in your code,
        ' you can retrieve it from a configuration file.
        Return "Data Source=(local);Initial Catalog=AdventureWorks2022" _
           & "Integrated Security=SSPI;"
    End Function
End Module
```

---

### Example using GetSqlBytes

This example shows how to retrieve the `Point` data as raw bytes in a single operation by using the `GetSqlBytes` method. The code uses a `StringBuilder` to convert the raw bytes to a string representation to be displayed in the console window.

### [C#](#tab/csharp)

```csharp
using System;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

class GetRawBytes
{
    static void Main()
    {
         string connectionString = GetConnectionString();
        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT ID, Pnt FROM dbo.Points", cnn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Retrieve the value of the Primary Key column
                int id = rdr.GetInt32(0);

                // Use SqlBytes to retrieve raw bytes
                SqlBytes sb = rdr.GetSqlBytes(1);
                long byteCount = sb.Length;

                // Format and print bytes
                StringBuilder str = new StringBuilder();
                str.AppendFormat("ID={0} Point=", id);

                for (int i = 0; i < byteCount; i++)
                    str.AppendFormat("{0:x}", sb[i]);
                Console.WriteLine(str.ToString());
            }
            rdr.Close();
            Console.WriteLine("done");
        }
    static private string GetConnectionString()
    {
        // To avoid storing the connection string in your code,
        // you can retrieve it from a configuration file.
        return "Data Source=(local);Initial Catalog=AdventureWorks2022"
            + "Integrated Security=SSPI";
    }
  }
}
```

### [Visual Basic .NET](#tab/vb)

```vb
Option Explicit On
Option Strict On

Imports System
Imports Microsoft.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.Text

Module GetRawBytes
    Sub Main()
        Dim connectionString As String = GetConnectionString()
        Using cnn As New SqlConnection(connectionString)
            cnn.Open()
            Dim cmd As New SqlCommand( _
             "SELECT ID, Pnt FROM dbo.Points", cnn)
            Dim rdr As SqlDataReader
            rdr = cmd.ExecuteReader

            While rdr.Read()
                ' Retrieve the value of the Primary Key column
                Dim id As Int32 = rdr.GetInt32(0)

                ' Use SqlBytes to retrieve raw bytes
                Dim sb As SqlBytes = rdr.GetSqlBytes(1)
                Dim byteCount As Long = sb.Length

                ' Format and print bytes
                Dim str As New StringBuilder
                str.AppendFormat("ID={0} Point=", id)

                Dim i As Integer
                For i = 0 To (byteCount - 1)
                    str.AppendFormat("{0:x}", sb(i))
                Next
                Console.WriteLine(str.ToString)

            End While
            rdr.Close()
            Console.WriteLine("done")
        End Using
    End Sub
    Private Function GetConnectionString() As String
        ' To avoid storing the connection string in your code,
        ' you can retrieve it from a configuration file.
        Return "Data Source=(local);Initial Catalog=AdventureWorks2022" _
           & "Integrated Security=SSPI;"
    End Function
End Module
```

---

## Work with UDT parameters

You can use UDTs as both input and output parameters in your ADO.NET code.

## Use UDTs in query parameters

You can use UDTs as parameter values when you set up a `SqlParameter` for a `Microsoft.Data.SqlClient.SqlCommand` object. The `SqlDbType.Udt` enumeration of a `SqlParameter` object indicates that the parameter is a UDT when calling the `Add` method to the `Parameters` collection. The `UdtTypeName` property of a `SqlCommand` object specifies the fully qualified name of the UDT in the database by using the `<database>.<schema_name>.<object_name>` syntax. Use the fully qualified name to avoid ambiguity in your code.

A local copy of the UDT assembly must be available to the client project.

### Example

The code in this example creates `SqlCommand` and `SqlParameter` objects to insert data into a UDT column in a table. The code uses the `SqlDbType.Udt` enumeration to specify the data type, and the `UdtTypeName` property of the `SqlParameter` object to specify the fully qualified name of the UDT in the database.

### [C#](#tab/csharp)

```csharp
using System;
using System.Data;
using Microsoft.Data.SqlClient;

class Class1
{
static void Main()
{
  string ConnectionString = GetConnectionString();
     using (SqlConnection cnn = new SqlConnection(ConnectionString))
     {
       SqlCommand cmd = cnn.CreateCommand();
       cmd.CommandText =
         "INSERT INTO dbo.Points (Pnt) VALUES (@Point)";
       cmd.CommandType = CommandType.Text;

       SqlParameter param = new SqlParameter("@Point", SqlDbType.Udt);       param.UdtTypeName = "TestPoint.dbo.Point";       param.Direction = ParameterDirection.Input;       param.Value = new Point(5, 6);       cmd.Parameters.Add(param);

       cnn.Open();
       cmd.ExecuteNonQuery();
       Console.WriteLine("done");
     }
    static private string GetConnectionString()
    {
        // To avoid storing the connection string in your code,
        // you can retrieve it from a configuration file.
        return "Data Source=(local);Initial Catalog=AdventureWorks2022"
            + "Integrated Security=SSPI";
    }
  }
}
```

### [Visual Basic .NET](#tab/vb)

```vb
Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports Microsoft.Data.SqlClient

Module Module1

  Sub Main()
    Dim ConnectionString As String = GetConnectionString()
    Dim cnn As New SqlConnection(ConnectionString)
    Using cnn
      Dim cmd As SqlCommand = cnn.CreateCommand()
      cmd.CommandText = "INSERT INTO dbo.Points (Pnt) VALUES (@Point)"
      cmd.CommandType = CommandType.Text

      Dim param As New SqlParameter("@Point", SqlDbType.Udt)
      param.UdtTypeName = "TestPoint.dbo.Point"
      param.Direction = ParameterDirection.Input
      param.Value = New Point(5, 6)
      cmd.Parameters.Add(param)

      cnn.Open()
      cmd.ExecuteNonQuery()
      Console.WriteLine("done")
    End Using
  End Sub
    Private Function GetConnectionString() As String
        ' To avoid storing the connection string in your code,
        ' you can retrieve it from a configuration file.
        Return "Data Source=(local);Initial Catalog=AdventureWorks2022;" _
           & "Integrated Security=SSPI;"
    End Function
End Module
```

---

## Related content

- [Access user-defined types in ADO.NET](accessing-user-defined-types-in-ado-net.md)
