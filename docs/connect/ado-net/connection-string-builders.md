---
title: Connection string builders
description: Learn about the connection string builder classes used for different providers in ADO.NET. These classes all inherit from DbConnectionStringBuilder.
author: David-Engel
ms.author: v-davidengel
ms.date: 11/13/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Connection string builders

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

In earlier versions of ADO.NET, compile-time checking of connection strings with concatenated string values didn't occur, so that at run time, an incorrect keyword generated an <xref:System.ArgumentException>. The Microsoft SqlClient Data Provider for SQL Server includes the connection string builder class <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder?displayProperty=nameWithType> that inherits from <xref:System.Data.Common.DbConnectionStringBuilder>.

## Connection string injection attacks

A connection string injection attack can occur when dynamic string concatenation is used to build connection strings that are based on user input. If the string isn't validated and malicious text or characters not escaped, an attacker can potentially access sensitive data or other resources on the server. For example, an attacker could mount an attack by supplying a semicolon and appending another value. The connection string is parsed by using a "**last one wins**" algorithm, and the hostile input is replaced for a legitimate value.

The connection string builder classes are designed to eliminate guesswork and protect against syntax errors and security vulnerabilities. They provide methods and properties corresponding to the known key/value pairs permitted by the data provider. Each class maintains a fixed collection of synonyms and can translate from a synonym to the corresponding well-known key name. Checks are done for valid key/value pairs and an invalid pair throws an exception. Also, injected values are handled in a safe manner.

The following example demonstrates how the <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> handles an inserted extra value for the `Initial Catalog` setting.

[!code-csharp[SqlConnectionStringBuilder_InjectionAttack#1](~/../sqlclient/doc/samples/SqlConnectionStringBuilder_InjectionAttack.cs#1)]

The output shows that the <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder> handled it correctly by escaping the extra value in double quotation marks instead of appending it to the connection string as a new key/value pair.

```output
data source=(local);Integrated Security=True;
initial catalog="AdventureWorks;NewValue=Bad"
```

## Build connection strings from configuration files

If certain elements of a connection string are known beforehand, they can be stored in a configuration file and retrieved at run time to construct a complete connection string. For example, the name of the database might be known in advance, but not the name of the server. Or you might want a user to supply a name and password at run time without the ability to inject other values into the connection string.

One of the overloaded constructors for a connection string builder takes a <xref:System.String> as an argument, which enables you to supply a partial connection string that can then be completed from user input. The partial connection string can be stored in a configuration file and retrieved at run time.

> [!NOTE]
> The <xref:System.Configuration> namespace allows programmatic access to configuration files that use the <xref:System.Web.Configuration.WebConfigurationManager> for Web applications and the <xref:System.Configuration.ConfigurationManager> for Windows applications. For more information about working with connection strings and configuration files, see [Connection Strings and Configuration Files](connection-strings-and-configuration-files.md).

### Example

This example demonstrates retrieving a partial connection string from a configuration file and completing it by setting the <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.DataSource%2A>, <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.UserID%2A>, and <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder.Password%2A> properties of the <xref:Microsoft.Data.SqlClient.SqlConnectionStringBuilder>. The configuration file is defined as follows.

```xml
<connectionStrings>
  <clear/>
  <add name="partialConnectString"
    connectionString="Initial Catalog=Northwind;"
    providerName="Microsoft.Data.SqlClient" />
</connectionStrings>
```

> [!NOTE]
> You must set a reference to the `System.Configuration.dll` in your project for the code to run.

[!code-csharp[DataWorks SqlConnectionStringBuilder.UserNamePwd#1](~/../sqlclient/doc/samples/SqlConnectionStringBuilder_UserNamePwd.cs#1)]
  
## See also

- [Connection strings](connection-strings.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
