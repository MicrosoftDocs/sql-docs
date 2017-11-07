---
title: "Migrate MySQL Databases to SQL Server - Azure SQL DB | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: 8006f9a0-394d-4238-8dc5-44255134628b
caps.latest.revision: 12
author: "Shamikg"
ms.author: "Shamikg"
manager: "jhubbard"
---
# Migrating MySQL Databases to SQL Server - Azure SQL DB (MySQLToSql)
SQL Server Migration Assistant (SSMA) for MySQL is a comprehensive environment that helps you quickly migrate MySQL databases to SQL Server or SQL Azure. By using SSMA for MySQL, you can review database objects and data, assess databases for migration, migrate database objects to SQL Server or SQL Azure, and then migrate data to SQL Server or SQL Azure.  
  
## Recommended Migration Process  
To successfully migrate objects and data from MySQL databases to SQL Server or SQL Azure, use the following process:  
  
1.  [Working with SSMA Projects &#40;MySQLToSQL&#41;](../../ssma/mysql/working-with-ssma-projects-mysqltosql.md).  
  
    After you create the project, you can set project conversion, migration, and type mapping options. For more information about project settings, see [Setting Project Options &#40;MySQLToSQL&#41;](../../ssma/mysql/setting-project-options-mysqltosql.md). For information about how to customize data type mappings, see [Mapping MySQL and SQL Server Data Types &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-and-sql-server-data-types-mysqltosql.md)  
  
2.  [Connecting to MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-mysql-mysqltosql.md)  
  
3.  [Connecting to SQL Server &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-sql-server-mysqltosql.md)  
  
4.  [Mapping MySQL Databases to SQL Server Schemas &#40;MySQLToSQL&#41;](../../ssma/mysql/mapping-mysql-databases-to-sql-server-schemas-mysqltosql.md)  
  
5.  [Connecting to Azure SQL DB &#40;MySQLToSQL&#41;](../../ssma/mysql/connecting-to-azure-sql-db-mysqltosql.md)  
  
6.  Optionally, [Assessing MySQL Databases for Conversion &#40;MySQLToSQL&#41;](../../ssma/mysql/assessing-mysql-databases-for-conversion-mysqltosql.md) to assess database objects for conversion and estimate the conversion time.  
  
7.  [Converting MySQL Databases &#40;MySQLToSQL&#41;](../../ssma/mysql/converting-mysql-databases-mysqltosql.md)  
  
8.  [Synchronization](http://msdn.microsoft.com/en-us/ac993a6d-0283-4823-8793-6b217677dfa3)  
  
9. You can do this in one of the following ways:  
  
    -   Save a script and run it on SQL Server or SQL Azure.  
  
    -   Synchronize the database objects.  
  
10. [Migrating MySQL Data into SQL Server - Azure SQL DB &#40;MySQLToSQL&#41;](../../ssma/mysql/migrating-mysql-data-into-sql-server-azure-sql-db-mysqltosql.md)  
  
11. If necessary, update database applications.  
  
> [!NOTE]  
> You cannot migrate Information_schema and MySQL schemas.  
  
## See Also  
[Installing SSMA for MySQL &#40;MySqlToSql&#41;](../../ssma/mysql/installing-ssma-for-mysql-mysqltosql.md)  
[Getting Started with SSMA for MySQL &#40;MySQLToSQL&#41;](../../ssma/mysql/getting-started-with-ssma-for-mysql-mysqltosql.md)  
  
