---
title: "Mapping CLR Parameter Data"
description: This article lists Microsoft SQL Server data types, equivalents in the CLR for SQL Server, and native CLR equivalents in the .NET Framework.
author: uc-msft
ms.author: umajay
ms.date: 07/15/2025
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "SqlBinary data type"
  - "SqlInt16 data type"
  - "SqlMoney data type"
  - "SqlString data type"
  - "SqlSingle data type"
  - "data types [CLR integration]"
  - "SqlInt64 data type"
  - "SqlDateTime data type"
  - "SqlXml data type"
  - "SqlBoolean data type"
  - "SqlDecimal data type"
  - "SqlBytes data type"
  - "mapping data types [CLR integration]"
  - "SqlChars data type"
  - "SqlInt32 data type"
---
# Map CLR parameter data

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The following table lists [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types, their equivalents in the common language runtime (CLR) for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in the `System.Data.SqlTypes` namespace, and their native CLR equivalents in the .NET Framework.

| SQL Server data type | Type (in `System.Data.SqlTypes` or `Microsoft.SqlServer.Types`) | CLR data type (.NET Framework) |
| --- | --- | --- |
| **bigint** | `SqlInt64` | `Int64`, `Nullable<Int64>` |
| **binary** | `SqlBytes`, `SqlBinary` | `Byte[]` |
| **bit** | `SqlBoolean` | `Boolean`, `Nullable<Boolean>` |
| **char** | None | None |
| **cursor** | None | None |
| **date** | `SqlDateTime` | `DateTime`, `Nullable<DateTime>` |
| **datetime** | `SqlDateTime` | `DateTime`, `Nullable<DateTime>` |
| **datetime2** | None | `DateTime`, `Nullable<DateTime>` |
| **datetimeoffset** | `None` | `DateTimeOffset`, `Nullable<DateTimeOffset>` |
| **decimal** | `SqlDecimal` | `Decimal`, `Nullable<Decimal>` |
| **float** | `SqlDouble` | `Double`, `Nullable<Double>` |
| **geography** | `SqlGeography` <sup>1</sup> | None |
| **geometry** | `SqlGeometry` <sup>1</sup> | None |
| **hierarchyid** | `SqlHierarchyId` <sup>1</sup> | None |
| **image** | None | None |
| **int** | `SqlInt32` | `Int32`, `Nullable<Int32>` |
| **money** | `SqlMoney` | `Decimal`, `Nullable<Decimal>` |
| **nchar** | `SqlChars`, `SqlString` | `String`, `Char[]` |
| **ntext** | None | None |
| **numeric** | `SqlDecimal` | `Decimal`, `Nullable<Decimal>` |
| **nvarchar** | `SqlChars`, `SqlString`<br /><br />`SQLChars` is a better match for data transfer and access, and `SQLString` is a better match for performing `String` operations. | `String`, `Char[]` |
| **nvarchar(1)**, **nchar(1)** | `SqlChars`, `SqlString` | `Char`, `String`, `Char[]`, `Nullable<char>` |
| **real** | `SqlSingle` (however, the range of `SqlSingle` is larger than **real**) | `Single`, `Nullable<Single>` |
| **rowversion** | None | `Byte[]` |
| **smallint** | `SqlInt16` | `Int16`, `Nullable<Int16>` |
| **smallmoney** | `SqlMoney` | `Decimal`, `Nullable<Decimal>` |
| **sql_variant** | None | `Object` |
| **table** | None | None |
| **text** | None | None |
| **time** | None | `TimeSpan`, `Nullable<TimeSpan>` |
| **timestamp** | None | None |
| **tinyint** | `SqlByte` | `Byte`, `Nullable<Byte>` |
| **uniqueidentifier** | `SqlGuid` | `Guid`, `Nullable<Guid>` |
| User-defined type (UDT) | None | The same class that is bound to the user-defined type in the same assembly or a dependent assembly. |
| **varbinary** | `SqlBytes`, `SqlBinary` | `Byte[]` |
| **varbinary(1)**, **binary(1)** | `SqlBytes`, `SqlBinary` | `byte`, `Byte[]`, `Nullable<byte>` |
| **varchar** | None | None |
| **xml** | `SqlXml` | None |

<sup>1</sup> Defined in `Microsoft.SqlServer.Types.dll`, which is installed with SQL Server and can be downloaded from the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] [feature pack](https://www.microsoft.com/download/details.aspx?id=100430).

## Automatic data type conversion with output parameters

A CLR method can return information to the calling code or program by marking an input parameter with the `out` modifier ([!INCLUDE [c-sharp-md](../../includes/c-sharp-md.md)]) or `<Out()> ByRef` ([!INCLUDE [visual-basic-md](../../includes/visual-basic-md.md)] .NET). If the input parameter is a CLR data type in the `System.Data.SqlTypes` namespace, and the calling program specifies its equivalent [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type as the input parameter, a type conversion occurs automatically when the CLR method returns the data type.

For example, the following CLR stored procedure has an input parameter of `SqlInt32` CLR data type that is marked with `out` (C#) or `<Out()> ByRef` (Visual Basic):

### [C#](#tab/csharp)

```csharp
[Microsoft.SqlServer.Server.SqlProcedure]
public static void PriceSum(out SqlInt32 value)
{ ... }
```

### [Visual Basic .NET](#tab/vb)

```vb
<Microsoft.SqlServer.Server.SqlProcedure> _
Public Shared Sub PriceSum(<Out()> ByRef value As SqlInt32)
...
End Sub
```

---

After the assembly is built and created in the database, the stored procedure is created in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with the following Transact-SQL, which specifies a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type of **int** as an OUTPUT parameter:

```sql
CREATE PROCEDURE PriceSum
(@sum INT OUTPUT)
AS EXTERNAL NAME TestStoredProc.StoredProcedures.PriceSum;
```

When the CLR stored procedure is called, the `SqlInt32` data type is automatically converted to an **int** data type, and returned to the calling program.

Not all CLR data types can be automatically converted to their equivalent [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types through an `out` parameter, however. The following table lists these exceptions.

| CLR data type (SQL Server) | SQL Server data type |
| --- | --- |
| `Decimal` | **smallmoney** |
| `SqlMoney` | **smallmoney** |
| `Decimal` | **money** |
| `DateTime` | **smalldatetime** |
| `SQLDateTime` | **smalldatetime** |

## Related content

- [SQL Server data types in the .NET Framework](sql-server-data-types-in-the-net-framework.md)
