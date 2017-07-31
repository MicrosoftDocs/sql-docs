
???3 2017-07-31 15:01pm

---
title: "Home page for SQL Connection Drivers | Microsoft Docs"
description: "Hub page with annotated links to downloads and documentation for numerous combinations of languages and operating systems, for connecting to SQL Server or to Azure SQL Database."
author: "MightyPen"
manager: "craigg"

ms.date: "07/31/2017"
ms.prod: "sql-non-specified"
ms.technology: 
  - "drivers"
ms.topic: "article"
ms.reviewer: "andrela"
ms.author: "genemi"
---
# Connection Drivers for Microsoft SQL Server


Welcome to the homepage for the languages and connection drivers you can use to interact with Microsoft SQL Server. This article:

- Lists and describes the available language and driver combinations.

- Displays the areas and subareas of the hierarchical documentation for each combination.

- Provides links to the detailed documentation for each combination.


#### Azure SQL Database


In any given language, the code that connects to SQL Server is almost identical to the code for connecting to Azure SQL Database in the Microsoft cloud.

For details about the connection strings for connecting to Azure SQL Database, see:

- [Use .NET Core (C#) to query an Azure SQL database](https://docs.microsoft.com/azure/sql-database/sql-database-connect-query-dotnet-core); and
- Other Azure SQL Database that are nearby the preceding article in the table of contents, about other languages. For instance, see [Use PHP to query an Azure SQL database](https://docs.microsoft.com/azure/sql-database/sql-database-connect-query-php).


<a name="an-050-languages" />

## Languages


This section lists the primary programming languages with which you can connect to SQL Server. For each language, it also lists the primary connection libraries or providers, which we call *connection drivers*.

In most rows of the following table of links, the link takes you to a section that is devoted to the language. Each language section provides direct links to detailed subareas for the language and its related driver.


| Logo | Language | Description |
| :--  | :------- | :---------- |
| ![C# logo][image-ref-320-csharp]  |  [C# and Visual Basic, using ADO.NET](#an-110-ado-net-docu)  |  System.Data.SqlClient classes. |
| ![C++ logo][image-ref-322-cpp]  | [ADO for C++](#an-120-ado-cpp-docu)     | ActiveX Data Objects, distinct from .NET Framework. |
| . | [Entity Framework (EF)](https://docs.microsoft.com/en-us/ef/) | EF is an Object-relational mapper (ORM).<br /><br />Another set of EF documentation is available at: [Introduction to Entity Framework](https://msdn.microsoft.com/en-us/library/aa937723.aspx). |
| ![Java logo][image-ref-330-java] | [JDBC](#an-130-jdbc-docu)               | Java programs can connect to SQL Server. |
| . | [LINQ to Entites](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/linq-to-entities)| LINQ to Entities is is part of the ADO.NET Entity Framework. LINQ to Entities offers more features than does LINQ to SQL, but at the cost of some complexity.<br /><br /> LINQ to Entities requires you use a tool to generate a set of classes from your relational database tables schema. The group of generated classes is called an entity data model (EDM). |
| . | [LINQ to SQL](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/) | Language Integrated Query (LINQ) to SQL is an API for working with SQL relational databases. LINQ helps overcome the mismatch between object oriented programming (OOP) languages versus relationally organized data. LINQ provides object-relational mapping (ORM).<br /><br />The ORM of LINQ to SQL is not as powerful as that of LINQ to Entities. LINQ to SQL relies on anonymous ???3 objects which are declared at compile time, not earlier in your source code during design time. In C#, the **var** keyword is used in your source code for anonymous classes.<br /><br />LINQ is a set of *extensions to the .NET Framework* that encompass language-integrated query, set, and transform operations. LINQ extends C# and Visual Basic with *native language syntax* for queries and provides class libraries to take advantage of these capabilities. |
| ![Node.js logo][image-ref-340-node] | [Node.js](#an-140-node-js-docu)         | JavaScript support through Node.js. |
| ![ODBC logo][image-ref-350-odbc] | [ODBC for C/C++](#an-160-odbc-cpp-docu) | The primary native API for accessing data from SQL Server. ODBC drivers are available for operating systems beyond just Windows, such as for Linux and MacOS.<br /><br />There are ODBC drivers for data targets others than just SQL Server. There are ODBC drivers for other languages such as COBOL, Perl, PHP, and Python. For additional examples, see [ODBC for MySQL](https://dev.mysql.com/downloads/connector/odbc/). |
| ![PHP logo][image-ref-360-php] | [PHP](#an-170-php-docu)                 | PHP can access data in all Editions of SQL Server 2005 and later (including Express Editions), as well as in Azure SQL Database. |
| ![Python logo][image-ref-370-python] | [Python](#an-180-python-docu)           | You can connect to SQL Server using Python on Windows, Linux, or MacOS. |
| ![Ruby logo][image-ref-380-ruby] | [Ruby](#an-190-ruby-docu)               | You can connect to SQL Server using Ruby on Windows, Linux, or MacOS. |
| | | <br /> |


#### Code examples


The documentation links in the preceding table lead to code examples, among other things. You can examine over 20 additional small sample programs at the following link:

- [Build an app using SQL Server](https://sqlchoice.azurewebsites.net/en-us/sql-server/developer-get-started/).


#### Downloads and installs


We have the following articles devoted to the download and install various SQL connection drivers:

- [SQL Server Drivers](sql-server-drivers.md)

- [Connectivity libraries and frameworks for Microsoft SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-libraries)


<a name="an-110-ado-net-docu" />

## [ADO.NET for C# and VB](/sql/connect/ado-net/index)


![ADO.NET logo][image-ref-310-ado-net]

[ADO.NET](/sql/connect/ado-net/index) is a casual name for .NET Framework classes in the following namespaces:

- System.Data
- System.Data.SqlClient


The managed languages C# and Visual Basic are the most common users of ADO.NET.

&nbsp;


<a name="an-120-ado-cpp-docu" />

## [C++ using ADO](/sql/ado/index) ???3 MISMATCH with Languages table?!


![C++ logo][image-ref-322-cpp]

[Microsoft ActiveX Data Objects (ADO)](/sql/ado/index) enables your client C++ application to access and manipulate data from a variety of sources. ADO is an OLE DB provider.


- *ADO* - stands for Microsoft ActiveX Data Objects.

- *OLE DB* - stands for Object Linking and Embedding for Databases. It is implemented in the pattern of the Component Object Model (COM).
    - Some OLE DB providers use ODBC internally.


| Area | Subarea | Description |
| :--- | :------ | :---------- |
| [Guide](/sql/ado/guide/index) | &nbsp; | &nbsp; |
| &nbsp; | [Appendixes](/sql/ado/guide/appendixes/index)             | Providers, errors, programming, and examples. |
| &nbsp; | [Data](/sql/ado/guide/data/index)                         | The fundamentals of recordsets, transactions, and so on. |
| &nbsp; | [Extensions](/sql/ado/guide/extensions/index)             | Fundamentals, and providers. |
| &nbsp; | [Multidimensional](/sql/ado/guide/multidimensional/index) | Multidimensional data, with objects such as CubeDef and Cellset.<br /><br />Uses a multidimensional data provider (MDP). |
| &nbsp; | [Remote Data Services (RDS)](/sql/ado/guide/remote-data-services/index) | RDS is a feature of ADO. RDS enables you to do all the following tasks in a single round trip:<br /><br />1. Move data from a server to a client application, or to a Web page.<br />2. Manipulate the data on the client.<br />3. Return updates to the server. |
| [Reference](/sql/ado/reference/index) | &nbsp; | &nbsp; |
| &nbsp; | [ADO API](/sql/ado/reference/ado-api/index)    | Broad documentation of examples and API elements. |
| &nbsp; | [ADO Multidimensional API](/sql/ado/reference/ado-md-api/index) | Objects, and their properties and methods.<br /> Relations between properties.<br /> Code examples. |
| &nbsp; | [ADOX API](/sql/ado/reference/adox-api/index)  | ADO Extensions. For instance, the Index object represent an index on a table. |
| &nbsp; | [RDS API](/sql/ado/reference/rds-api/index)    | Remote Data Services API.<br /><br />Microsoft discourages new development with RDS. THe server portion of RDS are no longer installed, starting with Windows 8 and Windows Server 2012. The client portion will also be discontinued. |
| | | <br /> |


<a name="an-130-jdbc-docu" />

## [JDBC](/sql/connect/jdbc/index)


![Java logo][image-ref-330-java]

Microsoft provides a [Java Database Connectivity (JDBC)](/sql/connect/jdbc/index) driver for use with either SQL Server or Azure SQL Database. It is a Type 4 JDBC driver, and it provides database connectivity through the standard JDBC application program interfaces (APIs).

The documentation provides a JDBC programming guide, in addition to the documentation listed in the following table. 


| Area | Description |
| :--- | :---------- |
| [Code examples](/sql/connect/jdbc/code-samples/index) | Code examples that teach about data types, result sets, and large data. |
| [Reference](/sql/connect/jdbc/reference/index)       | Interfaces, classes, and members. |
| | <br /> |


<a name="an-140-node-js-docu" />

## [Node.js](/sql/connect/node-js/index)


![Node.js logo][image-ref-340-node]

With [Node.js](/sql/connect/node-js/index) you can connect to SQL Server from Windows, Linux, or Mac.

The Node.js connection driver for SQL Server is implemented in JavaScript. The driver uses the TDS protocol, which is supported by all modern versions of SQL Server. The driver is an open source project, [available on Github](http://tediousjs.github.io/tedious/).

&nbsp;


<a name="an-160-odbc-cpp-docu" />

## [ODBC for C++](/sql/connect/odbc/index)


![ODBC logo][image-ref-350-odbc]

[Open database connectivity (ODBC)](/sql/connect/odbc/index) predates .NET Framework. Over the years numerous ODBC drivers have been created and released by groups within and outside of Microsoft. The range of drivers involve several client programming languages. The list of data targets goes well beyond SQL Server.

The ODBC content in this section focuses on accessing either SQL Server or Azure SQL Database, from C++.


| Area | Subarea | Description |
| :--- | :------ | :---------- |
| [Linux-Mac](/sql/connect/odbc/linux-mac/index) | &nbsp; | Information about using ODBC on the Linux or MacOS operating systems. |
| [Windows](/sql/connect/odbc/windows/index)     | &nbsp; | Information about using ODBC on the Windows operating system. |
| [Administration](/sql/odbc/admin/index) | &nbsp; | The administrative tool for managing ODBC data sources. |
| [Microsoft](/sql/odbc/microsoft/index)  | &nbsp; | Various ODBC drivers that are created and provided by Microsoft. |
| [Conceptual and reference](/sql/odbc/reference/index) | &nbsp; | Conceptual information about the ODBC interface, in addition to traditional reference. |
| &nbsp; | [Appendixes](/sql/odbc/reference/appendixes/index)    | State transition tables, ODBC cursor library, and more. |
| &nbsp; | [Develop app](/sql/odbc/reference/develop-app/index)  | Functions, handles, and much more. |
| &nbsp; | [Develop driver](/sql/odbc/reference/develop-driver/index) | How to develop your own ODBC driver, if you have a specialized data source. |
| &nbsp; | [Install](/sql/odbc/reference/install/index) | ODBC installation, subkeys, and more. |
| &nbsp; | [Syntax](/sql/odbc/reference/syntax/index)   | APIs for setup, installer, translation, and of course data access. |
| | | <br /> |


<a name="an-170-php-docu" />

## [PHP](/sql/connect/php/index)


![PHP logo][image-ref-360-php]

You can use [PHP](/sql/connect/php/index) to interact with SQL Server.

&nbsp;


<a name="an-180-python-docu" />

## [Python](/sql/connect/python/index)


![Python logo][image-ref-370-python]

You can use [Python](/sql/connect/python/index) to interact with SQL Server.


| Area | Description |
| :--- | :---------- |
| [pymssql driver](/sql/connect/python/pymssql/index) | The pymssql connection driver is a simple interface to SQL databases, for use in Python programs. Pymssql builds on top of FreeTDS to provide a Python DB-API (PEP-249) interface to Microsoft SQL Server. |
| [pyodbc driver](/sql/connect/python/pyodbc/index)   | The pyodbc connection driver is an open source Python module that makes accessing ODBC databases simple. It implements the DB API 2.0 specification, but is packed with even more Pythonic convenience. |
| | <br /> |


<a name="an-190-ruby-docu" />

## [Ruby](/sql/connect/ruby/index)


![Ruby logo][image-ref-380-ruby]

You can use [Ruby](/sql/connect/ruby/index) to interact with SQL Server.

&nbsp;


<!-- Image references -->

[image-ref-310-ado-net]: ./media/homepage-sql-connection-drivers/gm-ado-net-an40.png
[image-ref-322-cpp]: ./media/homepage-sql-connection-drivers/gm-cpp-4point-p61c.png
[image-ref-320-csharp]: ./media/homepage-sql-connection-drivers/gm-csharp-c10.png
[image-ref-330-java]: ./media/homepage-sql-connection-drivers/gm-java-j18.png
[image-ref-340-node]: ./media/homepage-sql-connection-drivers/gm-node-n14.png
[image-ref-350-odbc]: ./media/homepage-sql-connection-drivers/gm-odbc-ic55826-o35.png
[image-ref-360-php]: ./media/homepage-sql-connection-drivers/gm-php-php51.png
[image-ref-370-python]: ./media/homepage-sql-connection-drivers/gm-python-py66.png
[image-ref-380-ruby]: ./media/homepage-sql-connection-drivers/gm-ruby-un-r73.png


???3 more images


