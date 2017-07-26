---
title: "Home page for SQL Connection Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Connection Drivers for Microsoft SQL Server

This article list the combinations of programming language and connection driver that you can use to interact with Microsoft SQL Server.


#### Azure SQL Database


In any given language, the code that connects to SQL Server is almost identical to the code for connecting to Azure SQL Database in the Microsoft cloud.

For details about the connection strings for connecting to Azure SQL Database, see:

- [Use .NET Core (C#) to query an Azure SQL database](https://docs.microsoft.com/azure/sql-database/sql-database-connect-query-dotnet-core); and
- Other Azure SQL Database that are nearby the preceding article in the table of contents, about other languages. For instance, see [Use PHP to query an Azure SQL database](https://docs.microsoft.com/azure/sql-database/sql-database-connect-query-php).


<a name="an-050-languages" />

## 1. Languages


This section lists the primary programming languages with which you can connect to SQL Server. It also lists the primary connection providers, or connection *drivers*, that are available for each language.

Each language link takes you to a more detailed section devoted to the language.


| Language | Description |
| :------- | :---------- |
| [ADO.NET](#an-110-ado-net-docu)         | System.Data.SqlClient classes. |
| [ADO for C++](#an-120-ado-cpp-docu)     | ActiveX Data Objects, distinct from .NET Framework. |
| [JDBC](#an-130-jdbc-docu)               | Java programs can connect to SQL Server. |
| [Node.js](#an-140-node-js-docu)         | JavaScript support through Node.js. |
| [ODBC for C/C++](#an-160-odbc-cpp-docu) | The primary native API for accessing data from SQL Server. ODBC drivers are available for operating systems beyond just Windows, such as for Linux and MacOS.<br /><br />There are ODBC drivers for data targets others than just SQL Server. There are ODBC drivers for other languages such as COBOL, Perl, PHP, and Python. For additional examples, see [ODBC for MySQL](https://dev.mysql.com/downloads/connector/odbc/). |
| [PHP](#an-170-php-docu)                 | PHP can access data in all Editions of SQL Server 2005 and later (including Express Editions), as well as in Azure SQL Database. |
| [Python](#an-180-python-docu)           | You can connect to SQL Server using Python on Windows, Linux, or MacOS. |
| [Ruby](#an-190-ruby-docu)               | You can connect to SQL Server using Ruby on Windows, Linux, or MacOS. |
| | <br /> |


> [!NOTE]
> The documentation links in the preceding table lead to code samples, among other things.
> 
> You can examine over 20 additional small sample programs at the following link:
> 
> [Build an app using SQL Server](https://sqlchoice.azurewebsites.net/en-us/sql-server/developer-get-started/).


<a name="an-110-ado-net-docu" />
## 2. [ADO.NET](/sql/connect/ado-net/index)


ADO.NET is a casual name for .NET Framework classes in the following namespaces:

- System.Data
- System.Data.SqlClient

&nbsp;


<a name="an-120-ado-cpp-docu" />

## 3. [ADO for C++](/sql/ado/index)


Microsoft ActiveX Data Objects (ADO) enables your client C++ application to access and manipulate data from a variety of sources. ADO is an OLE DB provider.

- *ADO* - stands for Microsoft ActiveX Data Objects.
- *OLE DB* - stands for Object Linking and Embedding for Databases. It is implemented in the pattern of the Component Object Model (COM).



| Area | Subarea | Description |
| :--- | :------ | :---------- |
| [Guide](/sql/ado/guide/index) | &nbsp; | &nbsp; |
| &nbsp; | [Appendixes](/sql/ado/guide/appendixes/index)             | Providers, errors, programming, and samples. |
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

## 4. [JDBC](/sql/connect/jdbc/index)

Microsoft provides a Java Database Connectivity (JDBC) driver for use with either SQL Server or Azure SQL Database. It is a Type 4 JDBC driver, and it provides database connectivity through the standard JDBC application program interfaces (APIs).

The documentation provides a JDBC programming guide, in addition to the documentation listed in the following table. 


| Area | Description |
| :--- | :---------- |
| [Code samples](/sql/connect/jdbc/code-samples/index) | Code samples that teach about data types, result sets, and large data. |
| [Reference](/sql/connect/jdbc/reference/index)       | Interfaces, classes, and members. |
| | <br /> |


<a name="an-150-odbc-cpp-docu" />

## 6. [ODBC for C++](/sql/connect/odbc/index)


Open database connectivity (ODBC) predates .NET Framework. Over the years numerous ODBC drivers have been created and released by groups within and outside of Microsoft. The range of drivers involve several client programming languages. The list of data targets goes well beyond SQL Server.

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


<a name="an-160-node-js-docu" />

## 7. [Node.js](/sql/connect/node-js/index)


With Node.js you can connect to SQL Server from Windows, Linux, or Mac.

The Node.js connection driver for SQL Server is implemented in JavaScript. The driver uses the TDS protocol, which is supported by all modern versions of SQL Server. The driver is an open source project, [available on Github](http://tediousjs.github.io/tedious/).

&nbsp;


<a name="an-170-php-docu" />

## 8. [PHP](/sql/connect/php/index)


You can use PHP to interact with SQL Server.

&nbsp;


<a name="an-180-python-docu" />

## 9. [Python](/sql/connect/python/index)


| Area | Description |
| :--- | :---------- |
| [pymssql driver](/sql/connect/python/pymssql/index) | The pymssql connection driver is a simple interface to SQL databases, for use in Python programs. Pymssql builds on top of FreeTDS to provide a Python DB-API (PEP-249) interface to Microsoft SQL Server. |
| [pyodbc driver](/sql/connect/python/pyodbc/index)   | The pyodbc connection driver is an open source Python module that makes accessing ODBC databases simple. It implements the DB API 2.0 specification, but is packed with even more Pythonic convenience. |
| | <br /> |


<a name="an-190-ruby-docu" />

## 10. [Ruby](/sql/connect/ruby/index)


You can use Ruby to interact with SQL Server.

&nbsp;

