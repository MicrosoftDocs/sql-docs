---
title: Connect to a Database and Browse Existing Objects
description: Learn how to use SQL Server Object Explorer in Visual Studio to connect to both on-premises and off-premises SQL Server instances.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
f1_keywords:
  - "sql.data.tools.connectionpicker.f1"
---

# How to: Connect to a database and browse existing objects

A common task for database administrators and developers is to connect to a live database, design, or browse its schema and query against its objects. The SQL Server Object Explorer in Visual Studio now contains a dedicated **SQL Server** node, under which all connected SQL Server instances and their databases are grouped in an SSMS-like hierarchy. The connected SQL Server instances can be an on-premises one, such as [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], or off-premises in Azure SQL.

The following procedure assumes that you already have the AdventureWorks sample database installed. Use [GitHub](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks) to locate and install sample databases for different SQL Server versions. If you prefer, you can also follow the steps and use an existing database on your server.

## Connect to a database instance

1. In Visual Studio, make sure that **SQL Server Object Explorer** is open. If it's not, select the **View** menu and select **SQL Server Object Explorer**.

1. Right-click the **SQL Server** node in **SQL Server Object Explorer** and select **Add SQL Server**.

1. In the **Connect to Server** dialog box, enter the **Server name** of the server instance you want to connect to, your credentials, and select **Connect**.

1. In **SQL Server Object Explorer**, expand the **Databases** node under your server instance. You see all the databases residing in this server instance added under this **Databases** node.

1. Expand the **AdventureWorks** (or another database) node. You notice that all the database entities are organized in a hierarchy similar to SQL Server Management Studio.
