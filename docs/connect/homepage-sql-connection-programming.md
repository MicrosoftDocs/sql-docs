---
title: Homepage for SQL client programming
description: Annotated links to downloads and documentation for different languages and operating systems, for connecting to SQL Server or to Azure SQL Database.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-daveng, randolphwest
ms.date: 09/15/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Homepage for client programming to Microsoft SQL Server

Welcome to our homepage about client programming to interact with Microsoft SQL Server, and with Azure SQL Database in the cloud. This article provides the following information:

- Lists and describes the available language and driver combinations.
  - Information is given for the operating systems of Linux (Ubuntu and others), macOS, and Windows.
- Provides links to the detailed documentation for each combination.
- Displays the areas and subareas of the hierarchical documentation for certain languages, where appropriate.

#### Azure SQL Database

In any given language, the code that connects to SQL Server is almost identical to the code for connecting to Azure SQL Database.

For details about the connection strings for connecting to Azure SQL Database, see:

- [Use .NET Core (C#) to query an Azure SQL database](/azure/sql-database/sql-database-connect-query-dotnet-core).
- Other Azure SQL Database articles that are nearby the preceding article in the table of contents, about other languages. For instance, see [Use PHP to query an Azure SQL database](/azure/sql-database/sql-database-connect-query-php).

#### Build-an-app webpages

Our *Build-an-app* webpages present code examples, along with configuration information, in an alternative format. For more information, see later in this article the [section labeled *Build-an-app website*](#build-an-app).

## Languages and drivers for client programs

In the following table, each language image is a link to detail about using the language with SQL Server. Each link jumps to a later section in this article.

:::row:::
    :::column:::
        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-csharp-c10c.png" alt-text="C-sharp logo" border="false":::](#c-sharp-ado-net)

        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-node-n30.png" alt-text="Node.js logo" border="false":::](#node-js)

        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-python-py72.png" alt-text="Python logo" border="false":::](#python)
    :::column-end:::
    :::column:::
        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-entity-framework-ef20d.png" alt-text="Entity Framework logo" border="false":::](#c-sharp-entity-framework)

        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-cpp-4point-p61f.png" alt-text="C++ logo" border="false":::](#cpp-odbc)

        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-ruby-un-r82.png" alt-text="Ruby logo" border="false":::](#ruby)
    :::column-end:::
    :::column:::
        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-java-j18c.png" alt-text="Java logo" border="false":::](#java-and-jdbc)

        [:::image type="content" source="media/homepage-sql-connection-drivers/gm-php-php60.png" alt-text="PHP logo" border="false":::](#php)
    :::column-end:::
:::row-end:::

#### Downloads and installs

The following article is devoted to the download and install various SQL connection drivers, for use by programming languages:

- [SQL Server Drivers](./sql-connection-libraries.md)

## <a id="c-sharp-ado-net"></a> C# using ADO.NET

The .NET managed languages, such as C# and Visual Basic, are the most common users of ADO.NET. *ADO.NET* is a casual name for a subset of .NET Framework classes.

#### Code examples

| Example | Description |
| :-- | :-- |
| [Proof of concept connecting to SQL using ADO.NET](./ado-net/step-3-connect-sql-ado-net.md) | A small code example focused on connecting and querying SQL Server. |
| [Connect resiliently to SQL with ADO.NET](./ado-net/step-4-connect-resiliently-sql-ado-net.md) | Retry logic in a code example, because connections can occasionally experience moments of connectivity loss.<br /><br />Retry logic applies well to connections maintained through the internet into any cloud database, such as to Azure SQL Database. |
| [Azure SQL Database: Demonstration of how to use .NET Core on Windows/Linux/macOS to create a C# program, to connect and query](/azure/sql-database/sql-database-connect-query-dotnet-core) | Azure SQL Database example. |
| [Build-an-app: C#, ADO.NET, Windows](https://www.microsoft.com/sql-server/developer-get-started/csharp/win/) | Configuration information, along with code examples. |

#### Documentation

| Area | Description |
| :-- | :-- |
| [C# using ADO.NET](./ado-net/microsoft-ado-net-sql-server.md)| Root of our documentation. |
| [Namespace: System.Data](/dotnet/api/system.data) | A set of classes used for ADO.NET. |
| [Namespace: Microsoft.Data.SqlClient](/dotnet/api/microsoft.data.SqlClient) | The set of classes used for Microsoft .NET Data Provider for SQL Server |

## <a id="c-sharp-entity-framework"></a> C# using Entity Framework

Entity Framework (EF) provides Object-Relational Mapping (ORM). ORM makes it easier for your Object-Oriented Programming (OOP) source code to manipulate data that was retrieved from a relational SQL database.

EF has direct or indirect relationships with the following technologies:

- .NET Framework
- [LINQ to SQL](/dotnet/framework/data/adonet/sql/linq/), or [LINQ to Entities](/dotnet/framework/data/adonet/ef/language-reference/linq-to-entities)
- Language syntax enhancements, such as the `=>` operator in C#.
- Handy programs that generate source code for classes, which map to the tables in your SQL database. For instance, [EdmGen.exe](/dotnet/framework/data/adonet/ef/edm-generator-edmgen-exe).

#### Original Entity Framework, and new Entity Framework

The [start page for Entity Framework](/ef/) introduces EF with a description similar to this:

- Entity Framework is an object-relational mapper (O/RM) that enables .NET developers to work with a database using .NET objects. It eliminates the need for most of the data-access source code that developers usually need to write.

*Entity Framework* is a name shared by two separate source code branches. One EF branch is older, and its source code can now be maintained by the public. The other EF is new. The two EFs are described next:

| Version | Description |
| :-- | :-- |
| [EF 6.x](/ef/ef6/) | Microsoft first released EF in August 2008. In March 2015, Microsoft announced that EF 6.x was the final version that Microsoft would develop. Microsoft released the source code into the public domain.<br /><br />Initially EF was part of .NET Framework. But EF 6.x was removed from .NET Framework.<br /><br />[EF 6.x source code on GitHub, in repository *aspnet/EntityFramework6*](https://github.com/aspnet/EntityFramework6) |
| [EF Core](/ef/core/) | Microsoft released the newly developed EF Core in June 2016. EF Core is designed for better flexibility and portability. EF Core can run on operating systems beyond just Microsoft Windows. And EF Core can interact with databases beyond just Microsoft SQL Server and other relational databases.<br /><br />**C# code examples:**<br />[Getting Started with Entity Framework Core](/ef/core/get-started/index)<br />[Getting started with EF Core on .NET Framework with an Existing Database](/ef/core/get-started/full-dotnet/existing-db) |

EF and related technologies are powerful, and are a lot to learn for the developer who wants to master the entire area.

## Java and JDBC

Microsoft provides a Java Database Connectivity (JDBC) driver for use with SQL Server (or with Azure SQL Database). It is a Type 4 JDBC driver, and it provides database connectivity through the standard JDBC application program interfaces (APIs).

#### Code examples

| Example | Description |
| :-- | :-- |
| [Code examples](./jdbc/sample-jdbc-driver-applications.md) | Code examples that teach about data types, result sets, and large data. |
| [Connection URL Sample](./jdbc/connection-url-sample.md) | Describes how to use a connection URL to connect to SQL Server. Then use it to use an SQL statement to retrieve data. |
| [Data Source Sample](./jdbc/data-source-sample.md) | Describes how to use a data source to connect to SQL Server. Then use a stored procedure to retrieve data. |
| [Use Java to query an Azure SQL database](/azure/sql-database/sql-database-connect-query-java) | Azure SQL Database example. |
| [Create Java apps using SQL Server on Ubuntu](https://www.microsoft.com/sql-server/developer-get-started/java/ubuntu/) | Configuration information, along with code examples. |

#### Documentation

The JDBC documentation includes the following major areas:

| Area | Description |
| :-- | :-- |
| [Java Database Connectivity (JDBC)](./jdbc/microsoft-jdbc-driver-for-sql-server.md) | Root of our JDBC documentation. |
| [Reference](./jdbc/reference/jdbc-driver-api-reference.md) | Interfaces, classes, and members. |
| [Programming Guide for JDBC SQL Driver](./jdbc/programming-guide-for-jdbc-sql-driver.md) | Configuration information, along with code examples. |

## <a id="node-js"></a> Node.js

With Node.js you can connect to SQL Server from Windows, Linux, or macOS. The root of our Node.js documentation is [here](./node-js/node-js-driver-for-sql-server.md).

The Node.js connection driver for SQL Server is implemented in JavaScript. The driver uses the TDS protocol, which is supported by all modern versions of SQL Server. The driver is an open-source project, [available on GitHub](https://tediousjs.github.io/tedious/).

#### Code examples

| Example | Description |
| :-- | :-- |
| [Proof of concept connecting to SQL using Node.js](./node-js/step-3-proof-of-concept-connecting-to-sql-using-node-js.md) | Bare bones source code for connecting to SQL Server, and executing a query. |
| [Azure SQL database: Use Node.js to query](/azure/sql-database/sql-database-connect-query-nodejs) | Example for Azure SQL Database in the cloud. |
| [Create Node.js apps to use SQL Server on macOS](https://www.microsoft.com/sql-server/developer-get-started/node/mac/) | Configuration information, along with code examples. |

## <a id="cpp-odbc"></a> ODBC for C++

Open database connectivity (ODBC) was developed in the 1990s, and it predates .NET Framework. ODBC is designed to be independent of any particular database system, and independent of operating system.

Over the years numerous ODBC drivers have been created and released by groups within and outside of Microsoft. The range of drivers involve several client programming languages. The list of data targets goes well beyond SQL Server.

Some other connectivity drivers use ODBC internally.

#### Code example

- [C++ code example, using ODBC](../odbc/reference/sample-odbc-program.md)

#### Documentation outline

The ODBC content in this section focuses on accessing either SQL Server or Azure SQL Database, from C++. The following table lists an approximate outline of the major documentation for ODBC.

| Area | Subarea | Description |
| :--- | :------ | :---------- |
| [ODBC for C++](./odbc/microsoft-odbc-driver-for-sql-server.md) | Root of our documentation. |
| [Linux-macOS](./odbc/linux-mac/system-requirements.md) | | Information about using ODBC on the Linux or macOS operating systems. |
| [Windows](./odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md) | | Information about using ODBC on the Windows operating system. |
| [Administration](../odbc/admin/odbc-data-source-administrator.md) | | The administrative tool for managing ODBC data sources. |
| [Microsoft](../odbc/microsoft/microsoft-supplied-odbc-drivers.md)  | | Various ODBC drivers that are created and provided by Microsoft. |
| [Conceptual and reference](../odbc/reference/introduction-to-odbc.md) | | Conceptual information about the ODBC interface, in addition to traditional reference. |
| Conceptual and reference | [Appendixes](../odbc/reference/appendixes/odbc-appendixes.md) | State transition tables, ODBC cursor library, and more. |
| Conceptual and reference | [Develop app](../odbc/reference/develop-app/checking-feature-support-and-variability.md)  | Functions, handles, and much more. |
| Conceptual and reference | [Develop driver](../odbc/reference/develop-driver/developing-an-odbc-driver.md) | How to develop your own ODBC driver, if you have a specialized data source. |
| Conceptual and reference | [Install](../odbc/reference/install/odbc-subkey.md) | ODBC installation, subkeys, and more. |
| Conceptual and reference | [Syntax](../odbc/reference/syntax/odbc-reference.md)   | APIs for setup, installer, translation, and data access. |

## PHP

You can use PHP to interact with SQL Server. The root of our PHP documentation is [here](./php/microsoft-php-driver-for-sql-server.md).

#### Code examples

| Example | Description |
| :-- | :-- |
| [Proof of concept connecting to SQL using PHP](./php/step-3-proof-of-concept-connecting-to-sql-using-php.md) | A small code example focused on connecting and querying SQL Server. |
| [Connect resiliently to SQL with PHP](./php/step-4-connect-resiliently-to-sql-with-php.md) | Retry logic in a code example, because connections through the Internet and the cloud can occasionally experience moments of connectivity loss. |
| [Azure SQL database: Use PHP to query](/azure/sql-database/sql-database-connect-query-php) | Azure SQL Database example. |
| [Create PHP apps to use SQL Server on RHEL](https://www.microsoft.com/sql-server/developer-get-started/php/rhel/) | Configuration information, along with code examples. |

## Python

You can use Python to interact with SQL Server.

#### Code examples

| Example | Description |
| :-- | :-- |
| [Proof of concept connecting to SQL with Python using pyodbc](./python/pyodbc/step-3-proof-of-concept-connecting-to-sql-using-pyodbc.md) | A small code example focused on connecting and querying SQL Server. |
| [Azure SQL database: Use Python to query](/azure/sql-database/sql-database-connect-query-python) | Azure SQL Database example. |
| [Create PHP apps to use SQL Server on SLES](https://www.microsoft.com/sql-server/developer-get-started/python/sles/) | Configuration information, along with code examples. |

#### Documentation

| Area | Description |
| :--- | :---------- |
| [Python to SQL Server](./python/python-driver-for-sql-server.md) | Root of our documentation. |
| [pymssql driver](./python/pymssql/python-sql-driver-pymssql.md) | Microsoft doesn't maintain or test the pymssql driver.<br /><br />The pymssql connection driver is a simple interface to SQL databases, for use in Python programs. Pymssql builds on top of FreeTDS to provide a Python DB-API (PEP-249) interface to Microsoft SQL Server. |
| [pyodbc driver](./python/pyodbc/python-sql-driver-pyodbc.md) | The pyodbc connection driver is an open-source Python module that makes accessing ODBC databases simple. It implements the DB API 2.0 specification, but is packed with even more Pythonic convenience. |

## Ruby

You can use Ruby to interact with SQL Server. The root of our Ruby documentation is [here](./ruby/ruby-driver-for-sql-server.md).

#### Code examples

| Example | Description |
| :-- | :-- |
| [Proof of concept connecting to SQL with Ruby](./ruby/step-3-proof-of-concept-connecting-to-sql-using-ruby.md) | A small code example focused on connecting and querying SQL Server. |
| [Azure SQL database: Use Ruby to query](/azure/sql-database/sql-database-connect-query-ruby) | Azure SQL Database example. |
| [Create Ruby apps to use SQL Server on macOS](https://www.microsoft.com/sql-server/developer-get-started/ruby/mac/) | Configuration information, along with code examples. |

## <a id="build-an-app"></a> Build-an-app website, for SQL client development

On our [*Build-an-app*](https://www.microsoft.com/sql-server/developer-get-started/) webpages, you can choose from a long list of programming languages for connecting to SQL Server. And your client program can run on various operating systems.

*Build-an-app* emphasizes simplicity and completeness for the developer who is just getting started. The steps explain the following tasks:

1. How to install Microsoft SQL Server
1. How to download and install tools and drivers.
1. How to make any necessary configurations, as appropriate for your chosen operating system.
1. How to compile the provided source code.
1. How to run the program.

Next are a couple approximate outlines of the detail provided on the website:

#### Java on Ubuntu

1. Set up your environment
   1. Install SQL Server
   1. Install Java
   1. Install the Java Development Kit (JDK)
   1. Install Maven
1. Create Java application with SQL Server
   1. Create a Java app that connects to SQL Server and executes queries
   1. Create a Java app that connects to SQL Server using the popular framework Hibernate
1. Make your Java app up to 100x faster
   1. Create a Java app to demonstrate Columnstore indexes

#### Python on Windows

1. Set up your environment
   1. Install SQL Server
   1. Install Python
   1. Install the ODBC Driver and SQL Command Line Utility for SQL Server
1. Create Python application with SQL Server
   1. Install the Python driver for SQL Server
   1. Create a database for your application
   1. Create a Python app that connects to SQL Server and executes queries
1. Make your Python app up to 100x faster
   1. Create a new table with 5 million using sqlcmd
   1. Create a Python app that queries this table and measures the time taken
   1. Measure how long it takes to run the query
   1. Add a columnstore index to your table
   1. Measure how long it takes to run the query with a columnstore index

The following screenshots give you an idea of what our SQL development documentation website looks like.

#### Choose a language

:::image type="content" source="./media/homepage-sql-connection-drivers/gm-aka-ms-sqldev-choose-language-g21.png" alt-text="Screenshot of SQL Developer website, getting started with Java.":::

#### Choose an operating system

:::image type="content" source="./media/homepage-sql-connection-drivers/gm-aka-ms-sqldev-java-ubuntu-c31.png" alt-text="Screenshot of SQL Developer website, with Java on Ubuntu.":::

## Other development

This section provides links about other development options. These include using these same languages for Azure development in general. The information goes beyond targeting just Azure SQL Database and Microsoft SQL Server.

#### Developer hub for Azure

- [Developer hub for Azure](/azure/)
- [Azure for .NET developers](/dotnet/azure/)
- [Azure for Java developers](/java/azure/)
- [Azure for Node.js developers](/nodejs/azure/)
- [Azure for Python developers](/python/azure/)
- [Create a PHP web app in Azure](/azure/app-service-web/app-service-web-get-started-php)

#### Other languages

- [Create Go apps using SQL Server on Windows](https://www.microsoft.com/sql-server/developer-get-started/go/windows/)
