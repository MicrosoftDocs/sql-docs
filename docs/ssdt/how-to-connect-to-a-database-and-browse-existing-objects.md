---
title: Connect to a Database and Browse Existing Objects
description: Learn how to use SQL Server Object Explorer in Visual Studio to connect to both on-premises and off-premises SQL Server instances.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.connectionpicker.f1"
ms.assetid: 9b331800-3806-4459-ac58-88cdc98124d3
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: Connect to a Database and Browse Existing Objects

A very common task for database administrators and developers is to connect to a live database, design or browse its schema and query against its objects. The SQL Server Object Explorer in Visual Studio now contains a dedicated **SQL Server** node, under which all connected SQL Server instances and their databases are grouped in an SSMS-like hierarchy. The connected SQL Server instances can be an on-premises one, such as running SQL Server 2008 or an off-premise SQL Azure instance.  
  
The following procedure assumes that you already have the AdventureWorks sample database installed. Use [GitHub](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks) to locate and install sample databases for different SQL Server versions. If you prefer, you can also follow the steps and use an existing database on your server.  
  
### To connect to a database instance  
  
1.  In Visual Studio, make sure that **SQL Server Object Explorer** is open. If it is not, click the **View** menu and select **SQL Server Object Explorer**.  
  
2.  Right-click the **SQL Server** node in **SQL Server Object Explorer** and select **Add SQL Server**.  
  
3.  In the **Connect to Server** dialog box, enter the **Server name** of the server instance you want to connect to, your credentials, and click **Connect**.  
  
4.  In **SQL Server Object Explorer**, expand the **Databases** node under your server instance. You will see all the databases residing in this server instance added under this **Databases** node.  
  
5.  Expand the **AdventureWorks** (or another database) node. You will notice that all the database entities are organized in a hierarchy similar to SQL Server Management Studio.  
  
