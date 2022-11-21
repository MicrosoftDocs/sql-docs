---
title: Connectivity libraries and frameworks
description: Lists the connectivity drivers that client apps can use from various languages to connect to Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and also to Azure SQL Database and Azure Synapse Analytics. 
author: VanMSFT 
ms.author: vanto
ms.date: 03/17/2017
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: 80efe5ff-09ba-48a0-ac93-a91d62cff47c
---
# Connectivity libraries and frameworks for Microsoft SQL Server

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Check out the [Getting Started Tutorials](https://aka.ms/sqldev) to quickly get started with programming languages such as C#, Java, Node.js, PHP, and Python and build an app using SQL Server on Linux or Windows or Docker on macOS.

The following table lists connectivity libraries or *drivers* that client applications can use from a variety of languages to connect to and use Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and also to Azure SQL Database and Azure Synapse Analytics. 

| Language | Platform | Additional resources | Download | Get Started |
| :-- | :-- | :-- | :-- | :-- |
| C# | Windows, Linux, macOS | [Microsoft ADO.NET for SQL Server](../connect/ado-net/microsoft-ado-net-sql-server.md) | [Download](https://msdn.microsoft.com/vstudio/aa496123.aspx) | [Get Started](https://www.microsoft.com/sql-server/developer-get-started/csharp/ubuntu)
| Java | Windows, Linux, macOS | [Microsoft JDBC Driver for SQL Server](../connect/jdbc/microsoft-jdbc-driver-for-sql-server.md) | [Download](https://go.microsoft.com/fwlink/?LinkId=245496) |  [Get Started](https://www.microsoft.com/sql-server/developer-get-started/java/ubuntu)
| PHP | Windows, Linux, macOS| [PHP SQL Driver for SQL Server](../connect/php/microsoft-php-driver-for-sql-server.md) | Operating System: <br/> \* [Windows](https://www.microsoft.com/download/details.aspx?id=20098) <br/> \* [Linux](https://github.com/Microsoft/msphpsql/tree/dev#install-unix) <br/> \* [macOS](https://github.com/Microsoft/msphpsql/tree/dev#install-unix) |  [Get Started](https://www.microsoft.com/sql-server/developer-get-started/php/ubuntu)
| Node.js | Windows, Linux, macOS | [Node.js Driver for SQL Server](../connect/node-js/node-js-driver-for-sql-server.md) |  [Get Started](https://www.microsoft.com/sql-server/developer-get-started/node/ubuntu)
| Python | Windows, Linux, macOS | [Python SQL Driver](../connect/python/python-driver-for-sql-server.md) <br/> \* [pyodbc](../connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development.md) |  [Get Started](https://www.microsoft.com/sql-server/developer-get-started/python/ubuntu)
| Ruby | Windows, Linux, macOS | [Ruby Driver for SQL Server](../connect/ruby/ruby-driver-for-sql-server.md) | [Get Started](https://www.microsoft.com/sql-server/developer-get-started/ruby/ubuntu)
| C++ | Windows, Linux, macOS | [Microsoft ODBC Driver for SQL Server](../connect/odbc/microsoft-odbc-driver-for-sql-server.md) | [Download](../connect/odbc/microsoft-odbc-driver-for-sql-server.md) |  

The following table lists a few examples of Object Relational Mapping (ORM) frameworks and web frameworks that client applications can use with Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and also to Azure SQL Database and Azure Synapse Analytics. 

| Language | Platform | ORM(s) |
| :-- | :-- | :-- |
| C# | Windows, Linux, macOS | [Entity Framework](/ef)<br />[Entity Framework Core](/ef/core/index) |
| Java | Windows, Linux, macOS |[Hibernate ORM](https://hibernate.org/orm)|
| PHP | Windows, Linux | [Laravel (Eloquent)](https://laravel.com/docs/5.0/eloquent) |
| Node.js | Windows, Linux, macOS | [Sequelize ORM](http://sequelize.org/) |
| Python | Windows, Linux, macOS |[Django](https://www.djangoproject.com/) |
| Ruby | Windows, Linux, macOS | [Ruby on Rails](https://rubyonrails.org/) |

## Related links
- [SQL Server Drivers](../connect/sql-connection-libraries.md) for connecting from client applications
