---
title: Connect to SQL Using Java
description: Find the complete Java and Maven quickstart for connecting to SQL, executing a parameterized query, and verifying the result.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 09/14/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: tutorial
ai-usage: ai-assisted
---
# Connect to SQL using Java

<a id="step-3-proof-of-concept-connecting-to-sql-using-java"></a>

The three-step getting-started guide is now a single [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md). Use its complete application instead of the previous proof-of-concept snippets.

## Connect

<a id="step-1-connect"></a>

Follow [Configure the connection](getting-started-with-the-jdbc-driver.md#configure-the-connection) to keep endpoint settings and credentials outside source code. For Azure SQL and SQL database in Fabric, the quickstart uses interactive Microsoft Entra authentication during local development and explains managed identity for Azure hosting.

## Execute a query

<a id="step-2-execute-a-query"></a>

Copy the [complete Java application](getting-started-with-the-jdbc-driver.md#add-the-java-application), then [run it and verify the result](getting-started-with-the-jdbc-driver.md#run-and-verify). The application binds a query parameter and uses try-with-resources to close the connection, statement, and result set.

## Insert a row

<a id="step-3-insert-a-row"></a>

The quickstart uses a read-only query, so it doesn't insert sample data. For a separate example of inserting a row and retrieving its identity value, see [Using auto-generated keys](using-auto-generated-keys.md).

## Also see

- [Java and Maven quickstart](getting-started-with-the-jdbc-driver.md)
- [Sample JDBC driver applications](sample-jdbc-driver-applications.md)
