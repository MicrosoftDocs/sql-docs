---
title: Connection Libraries for Microsoft SQL Database
description: Provides download links for modules, which enable connection to Microsoft SQL Server and Azure SQL Database, from various client programming languages.
author: David-Engel
ms.author: davidengel
ms.reviewer: vanto, randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ms.custom:
  - ignite-2025
---
# Connection modules for Microsoft SQL Database

This article provides download links to connection modules or *drivers* that your client programs can use for interacting with [databases in SQL Server](../relational-databases/databases/databases.md), [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview?view=azuresql-db&preserve-view=true), [SQL database in Microsoft Fabric](/fabric/database/sql/overview), and [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview?view=azuresql-mi&preserve-view=true).

Drivers are available for a variety of programming languages, running on the following operating systems:

- Linux
- macOS
- Windows

**OOP-to-relational mismatch:**

*Relational*: Client programs that are written in an object-oriented programming (OOP) language often use SQL drivers, which return queried data in a format that is more relational than object oriented. C# using ADO.NET is one example. The OOP-relational format mismatch sometimes makes the OOP code harder to write and understand.

*ORM*: Other drivers or frameworks return queried data in the OOP format, avoiding the mismatch. These drivers work by expecting that classes have been defined to match the data columns of particular SQL tables. The driver then performs the *object-relational mapping* (ORM) to return queried data as an instance of a class. Microsoft's Entity Framework (EF) for C#, and Hibernate for Java, are two examples.

The present article devotes separate sections to these two kinds of connection drivers.

<a id="anchor-20-drivers-relational-access"></a>

## Drivers for relational access

| Language | Download the SQL driver |
| --- | --- |
| C# | [ADO.NET](https://dotnet.microsoft.com/download)<br />[Microsoft.Data.SqlClient](https://www.nuget.org/packages/Microsoft.Data.SqlClient/)<br />[.NET for: Linux-Ubuntu, macOS, Windows](https://dotnet.microsoft.com/download) |
| C++ | [ODBC](odbc/download-odbc-driver-for-sql-server.md)<br /><br />[OLE DB](oledb/download-oledb-driver-for-sql-server.md) |
| Go | [go-mssqldb driver](golang/microsoft-go-mssqldb-driver.md)<br />[Installation and system requirements](golang/installation.md)<br />[Go download page](https://go.dev/dl/) |
| Java | [JDBC](jdbc/download-microsoft-jdbc-driver-for-sql-server.md) |
| Node.js | [Node.js driver, install instructions](node-js/step-1-configure-development-environment-for-node-js-development.md) |
| PHP | [PHP](php/download-drivers-php-sql-server.md) |
| Python | [mssql-python](python/mssql-python/python-sql-driver-mssql-python.md) |
| Ruby | [Ruby driver install instructions](ruby/step-1-configure-development-environment-for-ruby-development.md)<br />[Ruby download page](https://rubyinstaller.org/downloads/) |

<a id="anchor-40-drivers-orm-access"></a>

## Drivers for ORM access

The following table lists examples of Object Relational Mapping (ORM) frameworks that client applications use to connect to Microsoft SQL Database.

| Language | ORM driver download |
| --- | --- |
| C# | [Entity Framework Core](/ef/core/providers/sql-server)<br />[Entity Framework (6.x or later)](/ef/ef6/fundamentals/install) |
| Go | [GORM](https://gorm.io/) |
| Java | [Hibernate ORM](https://hibernate.org/orm) |
| PHP | [Eloquent ORM, included in Laravel install](https://laravel.com/docs/11.x) |
| Node.js | [Sequelize ORM](https://sequelize.org/)<br />[Prisma](https://www.prisma.io/) |
| Python | [Django](https://www.djangoproject.com/)<br />[mssql-django - SQL Server backend for Django](python/mssql-django/python-sql-driver-mssql-django.md) |
| Ruby | [Ruby on Rails](https://rubyonrails.org/) |

## Related content

- [Code examples for connecting to Azure SQL Database in the cloud, with Java and other languages](/azure/sql-database/sql-database-connect-query-java)
