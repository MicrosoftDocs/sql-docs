---
title: Managing result sets
description: Learn how to manage the results of a query by using result sets in the JDBC driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Managing result sets with the JDBC driver

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

The result set is an object that represents a set of data returned from a data source, usually as the result of a query. The result set contains rows and columns to hold the requested data elements, and it is navigated with a cursor. A result set can be updatable, meaning that it can be modified and have those modifications pushed to the original data source. A result set can also have various levels of sensitivity to changes in the underlying data source.  
  
The type of result set is determined when a statement is created, which is when a call to the [createStatement](reference/createstatement-method-sqlserverconnection.md) method of the [SQLServerConnection](reference/sqlserverconnection-class.md) class is made. The fundamental role of a result set is to provide Java applications with a usable representation of the database data. This task is typically done with the typed getter and setter methods on the result set data elements.  
  
In the following example, which is based on the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database, a result set is created by calling the [executeQuery](reference/executequery-method-sqlserverstatement.md) method of the [SQLServerStatement](reference/sqlserverstatement-class.md) class. Data from the result set is then displayed by using the [getString](reference/getstring-method-sqlserverresultset.md) method of the [SQLServerResultSet](reference/sqlserverresultset-class.md) class.  
  
[!code[JDBC#ManagingResultSets1](codesnippet/Java/managing-result-sets-with-t_1.java)]  
  
The articles in this section describe various aspects of result set usage, including cursor types, concurrency, and row locking.  
  
## In this section  
  
|Article|Description|  
|-----------|-----------------|  
|[Understanding cursor types](understanding-cursor-types.md)|Describes the different cursor types that the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports.|  
|[Understanding concurrency control](understanding-concurrency-control.md)|Describes how the JDBC driver supports concurrency control.|  
|[Understanding row locking](understanding-row-locking.md)|Describes how the JDBC driver supports row locking.|  
  
## See also

[Overview of the JDBC driver](overview-of-the-jdbc-driver.md)  
