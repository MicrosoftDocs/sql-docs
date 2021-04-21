---
description: Learn about handling complex statements when using the JDBC driver. Different methods can be used in different scenarios.
title: Handling complex statements
ms.custom: ""
ms.date: 04/20/2021
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 6b807a45-a8b5-4b1c-8b7b-d8175c710ce0
author: David-Engel
ms.author: v-daenge
---
# Handling complex statements

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

When you use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], you might have to handle complex statements, including statements that are dynamically generated at runtime. Complex statements often perform different kinds of tasks, including updates, inserts, and deletes. These types of statements may also return multiple result sets and output parameters. In these situations, the Java code that runs the statements might not know in advance the types and number of objects and data returned.

To process complex statements, the JDBC driver provides many methods to query the objects and data that's returned so your application can correctly process them. The key to processing complex statements is the [execute](reference/execute-method-sqlserverstatement.md) method of the [SQLServerStatement](reference/sqlserverstatement-class.md) class. This method returns a **boolean** value. When the value is true, the first result returned from the statements is a result set. If the value is false, the first result returned is an update count.

When you know the type of object or data that's returned, you can use either the [getResultSet](reference/getresultset-method-sqlserverstatement.md) or the [getUpdateCount](reference/getupdatecount-method-sqlserverstatement.md) method to process that data. To continue to the next object or data that's returned from the complex statement, you can call the [getMoreResults](reference/getmoreresults-method.md) method.

In the following example, an open connection to the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal_md.md)] sample database is passed in to the function, a complex statement is constructed that combines a stored procedure call with a SQL statement, the statements are run, and then a `do` loop is used to process all the result sets and updated counts that are returned.

[!code[JDBC#HandlingComplexStatements1](codesnippet/Java/handling-complex-statements_1.java)]

## See also

[Using statements with the JDBC driver](using-statements-with-the-jdbc-driver.md)
