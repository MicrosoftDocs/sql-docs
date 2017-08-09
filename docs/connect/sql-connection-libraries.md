---
title: "Connection libraries for Microsoft SQL Databases | Microsoft Docs"
description: "Provides download links for modules which enable connection to Microsoft SQL Server and Azure SQL Database, from a variety of client programming languages."
author: MightyPen
ms.service: "sql-database"
ms.prod: "sql-server"
ms.custom: "develop apps"
ms.workload: "data-management"
ms.topic: article
ms.date: 08/08/2017
ms.author: genemi
---
# Connectiion modules for Microsoft SQL databases

This article provides download links to connection modules or *drivers* that your client programs can use for interacting with [Microsoft SQL Server](../index.md), and with its twin in the cloud [Azure SQL Database](http://docs.microsoft.com/azure/sql-database/). Drivers are available for a variety of programming languages, running on the following operating systems:

- Linux (Ubuntu)
- MacOS
- Windows


#### OOP-to-relational mismatch

*Relational*: Client programs written in an object oriented programming (OOP) language often use SQL drivers or frameworks which return queried data to them in a format that is more relational than object oriented. C# using ADO.NET is one example. The OOP-relational format mismatch sometimes makes the OOP code harder to write and understand. ADO.NET for C# is on example.

*ORM*: Other drivers or frameworks return queried data in the OOP format, avoiding the mismatch. These work by expecting that classes have been defined to match the data columns of particular SQL tables. The driver then performs the *object-relational mapping* (ORM) to return queried data as an instance of a class. Microsoft's Entity Framework (EF) for C#, and Hibernate for Java, are two examples.

The present article devotes separate sections to these two kinds of connection drivers.


<a name="anchor-20-drivers-relational-access" />

## Drivers for relational access


<!--
Each given Microsoft Download Center page should be enhanced
with a link to the next NEWER version page, on the day that the
original page is no longer the latest because the newer page is being added.
But this policy is not agreed on or observed,
putting the links in the following table at risk for being outdated.

PHP driver in Github.com also uses this FWLink:  http://go.microsoft.com/fwlink/?LinkID=518036 ,
although the FWLink is less precise than is http://github.com/Microsoft/msphpsql/tree/dev#install-unix .
-->


| Language | Download the SQL driver |
| :------- | :---------------------- |
| C#       | [ADO.NET](http://www.microsoft.com/net/download/)<br /><br />[.NET Core, for Linux-Ubuntu](https://www.microsoft.com/net/core#Ubuntu)<br />[.NET Core, for MacOS](https://www.microsoft.com/net/core#macos)<br />[.NET Core, for Windows](https://www.microsoft.com/net/core) |
| C++      | [ODBC 13.1](http://docs.microsoft.com/sql/connect/odbc/download-odbc-driver-for-sql-server) |
| Java     | [JDBC](http://go.microsoft.com/fwlink/?LinkId=245496)<br />[Download 6.2](http://www.microsoft.com/download/details.aspx?id=55539) |
| Node.js  | [Node.js driver, install instructions](http://docs.microsoft.com/sql/connect/node-js/step-1-configure-development-environment-for-node-js-development) |
| PHP      | *Operating system:*<br /><br />[Windows PHP driver](http://www.microsoft.com/download/details.aspx?id=20098)<br />[Ubuntu or MacOS PHP driver, from Github](http://github.com/Microsoft/msphpsql/tree/dev#install-unix) |
| Python   | [pyodbc, install instructions](http://docs.microsoft.com/sql/connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development)<br />[Download ODBC 13.1](http://docs.microsoft.com/sql/connect/odbc/download-odbc-driver-for-sql-server) |
| Ruby     | [Ruby driver, install instructions](https://docs.microsoft.com/sql/connect/ruby/step-1-configure-development-environment-for-ruby-development)<br />[Ruby download page](https://rubyinstaller.org/downloads/) |
| &nbsp; | <br /> |


<a name="anchor-40-drivers-orm-access" />

## Drivers for ORM access


The table below lists examples of Object Relational Mapping (ORM) frameworks, and web frameworks, that client applications can use to connect to Microsoft SQL databases.


| Language | ORM driver download |
| :------- | :------------------ |
| C# | [Entity Framework Core](http://docs.microsoft.com/ef/core/)<br />[Entity Framework (6.x or later)](http://docs.microsoft.com/ef/) |
| Java | [Hibernate ORM](http://hibernate.org/orm)|
| PHP | [Eloquent ORM, included in Laravel install](http://laravel.com/docs/) |
| Node.js | [Sequelize ORM](http://docs.sequelizejs.com) |
| Python | [Django](http://www.djangoproject.com/) |
| Ruby | [Ruby on Rails](http://rubyonrails.org/) |
| &nbsp; | <br /> |


<a name="anchor-60-build-an-app-webpages" />

## Build-an-app webpages


[http://aka.ms/sqldev](http://aka.ms/sqldev) takes you to a set of our *Build-an-app* webpages. The webpages provide information about numerous combinations of programming language, operating system, and SQL connection driver. Among the information provided by the Build-an-app webpages are the following items:

- Details about how to get started from the very beginning, for each combination of language + oper sys + driver.
    - Instructions for installing the latest SQL connection drivers.
- Code examples for each of the following items:
    - Object-relational code examples.
    - ORM code examples.
    - Columnstore index demonstrations for much faster performance.


#### First page, of Build-an-app webpages

![Build-an-app webpages, first page screenshot][image-ref-163-buildanapp-webpages-first-page]


#### Menu for Java - Ubuntu, of Build-an-app webpages

![Build-an-app webpages, menu Java Ubuntu][image-ref-167-buildanapp-webpages-menu-java-ubuntu]


&nbsp;


## Related links

- [Code examples for connecting to Azure SQL Database in the cloud, with Java and other languages](http://docs.microsoft.com/azure/sql-database/sql-database-connect-query-java).


<!-- Image references -->

[image-ref-163-buildanapp-webpages-first-page]: ./media/homepage-sql-connection-drivers/gm-aka-ms-sqldev-choose-language-g21.png
[image-ref-167-buildanapp-webpages-menu-java-ubuntu]: ./media/homepage-sql-connection-drivers/gm-aka-ms-sqldev-java-ubuntu-c31.png
