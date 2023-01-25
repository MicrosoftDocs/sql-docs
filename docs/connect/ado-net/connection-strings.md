---
title: "Connection strings"
description: Learn about connection strings in the Microsoft SqlClient Data Provider for SQL Server, which contain initialization information passed as a parameter from a data provider to a data source.
author: David-Engel
ms.author: v-davidengel
ms.date: "11/13/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connection strings in ADO.NET

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

A connection string contains initialization information that is passed as a parameter from a data provider to a data source. The data provider receives the connection string as the value of the <xref:System.Data.Common.DbConnection.ConnectionString?displayProperty=nameWithType> property. The provider parses the connection string and ensures that the syntax is correct and that the keywords are supported. Then the <xref:System.Data.Common.DbConnection.Open?displayProperty=nameWithType> method passes the parsed connection parameters to the data source. The data source performs further validation and establishes a connection.

## Connection string syntax

A connection string is a semicolon-delimited list of key/value parameter pairs:

```csharp
keyword1=value; keyword2=value;
```

Keywords are not case-sensitive. Values, however, may be case-sensitive, depending on the data source. Both keywords and values may contain [whitespace characters](https://en.wikipedia.org/wiki/Whitespace_character#Unicode). Leading and trailing white space is ignored in keywords and unquoted values.

If a value contains the semicolon, [Unicode control characters](https://en.wikipedia.org/wiki/Unicode_control_characters), or leading or trailing white space, it must be enclosed in single or double quotation marks. For example:

```csharp
Keyword=" whitespace  ";
Keyword='special;character';
```

The enclosing character may not occur within the value it encloses. Therefore, a value containing single quotation marks can be enclosed only in double quotation marks, and vice versa:

```csharp
Keyword='double"quotation;mark';
Keyword="single'quotation;mark";
```

You can also escape the enclosing character by using two of them together:

```csharp
Keyword="double""quotation";
Keyword='single''quotation';
```

The quotation marks themselves, as well as the equals sign, do not require escaping, so the following connection strings are valid:

```csharp
Keyword=no "escaping" 'required';
Keyword=a=b=c
```

Since each value is read until the next semicolon or the end of string, the value in the latter example is `a=b=c`, and the final semicolon is optional.

All connection strings share the same basic syntax described above. The set of recognized keywords depends on the provider. The *Microsoft SqlClient* data provider for *SQL Server* supports many keywords from older APIs, but is generally more flexible and accepts synonyms for many of the common connection string keywords.

Typing mistakes can cause errors. For example, `Integrated Security=true` is valid, but `IntegratedSecurity=true` causes an error.

Connection strings constructed manually at run time from invalidated user input are vulnerable to string-injection attacks and jeopardize security at the data source. To address these problems, the <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> class has been created. This [connection string builder](connection-string-builders.md) class exposes parameters as strongly typed properties, and makes it possible to validate the connection string before it's sent to the data source.

## In this section

[Connection String Builder](connection-string-builders.md)\
Demonstrates how to use the `ConnectionStringBuilder` class to construct valid connection strings at run time.

[Connection Strings and Configuration Files](connection-strings-and-configuration-files.md)\
Demonstrates how to store and retrieve connection strings in configuration files.

[Connection String Syntax](connection-string-syntax.md)\
Describes how to configure provider-specific connection strings for `SqlClient`.

[Protecting Connection Information](protecting-connection-information.md)\
Demonstrates techniques for protecting information used to connect to a data source.

## See also

- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
