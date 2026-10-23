---
title: Create a SQL Database for Java
description: Find database and authentication prerequisites for the Java and Maven quickstart for SQL Server, Azure SQL Database, and SQL database in Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 09/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: tutorial
ai-usage: ai-assisted
---
# Create a SQL database for Java

<a id="step-2-create-a-sql-database-for-java-development"></a>

The three-step getting-started guide is now a single [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md). Its query doesn't require AdventureWorks, sample tables, or permission to create or modify data.

## Azure SQL Database

Follow the quickstart's [database prerequisites](getting-started-with-the-jdbc-driver.md#prerequisites), including Microsoft Entra ID access and network connectivity. If you need a database, see [Create a single database in Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart).

## SQL Server

Use a SQL Server container or existing TCP endpoint and an account with permission to connect. Follow the quickstart's [database setup choices](getting-started-with-the-jdbc-driver.md#choose-your-database) and [SQL Server configuration](getting-started-with-the-jdbc-driver.md#sql-server-with-sql-authentication).

## SQL database in Fabric

Follow the quickstart's [database setup choices](getting-started-with-the-jdbc-driver.md#choose-your-database) and [Microsoft Entra configuration](getting-started-with-the-jdbc-driver.md#azure-sql-or-fabric-with-microsoft-entra-authentication). Use the server and database names from the SQL database's connection settings.

## Related content

- [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md)
