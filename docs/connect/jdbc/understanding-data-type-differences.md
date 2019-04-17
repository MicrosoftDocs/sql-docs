---
title: "Understanding Data Type Differences | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: ab8fa00f-cb16-47e2-94b8-3a76f56c2b84
author: MightyPen
ms.author: genemi
manager: craigg
---
# Understanding Data Type Differences

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

There are a number of differences between the Java programming language data types and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types. The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] helps to facilitate those differences through various types of conversions.  

## Character Types

The JDBC character string data types are **CHAR**, **VARCHAR**, and **LONGVARCHAR**. The JDBC driver provides support for the JDBC 4.0 API. In the JDBC 4.0, the JDBC character string data types can also be **NCHAR**, **NVARCHAR**, and **LONGNVARCHAR**. These new character string types maintain Java native character types in Unicode format and remove the need to perform any ANSI-to-Unicode or Unicode-to-ANSI conversion.  
  
| Type            | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| --------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Fixed-length    | The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **char** and **nchar** data types map directly to the JDBC **CHAR** and **NCHAR** types. These are fixed-length types with padding provided by the server in the case where the column has `SET ANSI_PADDING ON`. Padding is always turned on for **nchar**, but for **char**, in the case where the server char columns are not padded, the JDBC driver adds the padding.                                                                                                                                                                                                                                                                                                                                                                                      |
| Variable-length | The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **varchar** and **nvarchar** types map directly to the JDBC **VARCHAR** and **NVARCHAR** types, respectively.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
| Long            | The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **text** and **ntext** types map to the JDBC **LONGVARCHAR** and **LONGNVARCHAR** type, respectively. These are deprecated types beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], so you should use large value types, **varchar(max)** or **nvarchar(max)**, instead.<br /><br /> Using the update\<Numeric Type> and [updateObject (int, java.lang.Object)](../../connect/jdbc/reference/updateobject-method-int-java-lang-object.md) methods will fail against **text** and **ntext** server columns. However, using the [setObject](../../connect/jdbc/reference/setobject-method-sqlserverpreparedstatement.md) method with a specified character conversion type is supported against **text** and **ntext** server columns. |
  
## Binary String Types

The JDBC binary-string types are **BINARY**, **VARBINARY**, and **LONGVARBINARY**.  
  
| Type            | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| --------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Fixed-length    | The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **binary** type maps directly to the JDBC **BINARY** type. This is a fixed-length type with padding provided by the server in the case where the column has SET ANSI_PADDING ON. When the server char columns are not padded, the JDBC driver adds the padding.<br /><br /> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **timestamp** type is a JDBC **BINARY** type with the fixed length of 8 bytes. |
| Variable-length | The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **varbinary** type maps to the JDBC **VARBINARY** type.<br /><br /> The **udt** type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maps to JDBC as a **VARBINARY** type.                                                                                                                                                                                                                                 |
| Long            | The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **image** type maps to the JDBC **LONGVARBINARY** type. This type is deprecated beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], so you should use a large value type, **varbinary(max)** instead.                                                                                                                                                                                           |
  
## Exact Numeric Types

The JDBC exact numeric types map directly to their corresponding SQL Server types.  
  
| Type     | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| BIT      | The JDBC **BIT** type represents a single bit that can be 0 or 1. This maps to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **bit** type.                                                                                                                                                                                                                                                                                                                                       |
| TINYINT  | The JDBC **TINYINT** type represents a single byte. This maps to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **tinyint** type.                                                                                                                                                                                                                                                                                                                                                 |
| SMALLINT | The JDBC **SMALLINT** type represents a signed 16-bit integer. This maps to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **smallint** type.                                                                                                                                                                                                                                                                                                                                     |
| INTEGER  | The JDBC **INTEGER** type represents a signed 32-bit integer. This maps to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **int** type.                                                                                                                                                                                                                                                                                                                                           |
| BIGINT   | The JDBC **BIGINT** type represents a signed 64-bit integer. This maps to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **bigint** type.                                                                                                                                                                                                                                                                                                                                         |
| NUMERIC  | The JDBC **NUMERIC** type represents a fixed-precision decimal value that holds values of identical precision. The **NUMERIC** type maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **numeric** type.                                                                                                                                                                                                                                                                   |
| DECIMAL  | The JDBC **DECIMAL** type represents a fixed-precision decimal value that holds values of at least the specified precision. The **DECIMAL** type maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **decimal** type.<br /><br /> The JDBC **DECIMAL** type also maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **money** and **smallmoney** types, which are specific fixed-precision decimal types that are stored in 8 and 4 bytes, respectively. |
  
## Approximate Numeric Types

The JDBC approximate numeric types are **REAL**, **DOUBLE**, and **FLOAT**.  
  
| Type   | Description                                                                                                                                                                                                                                                                                                   |
| ------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| REAL   | The JDBC **REAL** type has seven digits of precision (single precision) and maps directly to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **real** type.                                                                                                                                     |
| DOUBLE | The JDBC **DOUBLE** type has 15 digits of precision (double precision) and maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **float** type. The JDBC **FLOAT** type is a synonym of **DOUBLE**. Because there can be confusion between **FLOAT** and **DOUBLE**, **DOUBLE** is preferred. |
  
## Datetime Types

The JDBC **TIMESTAMP** type maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** and **smalldatetime** types. The **datetime** type is stored in two 4-byte integers. The **smalldatetime** type holds the same information (date and time), but with less accuracy, in two 2-byte small integers.  
  
> [!NOTE]  
> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **timestamp** type is a fixed-length binary-string type. It does not map to any of the JDBC time types: **DATE**, **TIME**, or **TIMESTAMP**.  
  
## Custom Type Mapping

The custom type mapping feature of JDBC that uses the SQLData interfaces for the JDBC advanced types (UDTs, Struct, and so on). is not implemented in the JDBC driver.  
  
## See Also

[Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)  
