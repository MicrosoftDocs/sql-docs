---
title: "Working with result sets | Microsoft Docs"
ms.custom: ""
ms.date: "08/12/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4fc4b1c6-3075-4ad7-9244-865d9ede7ae6
author: MightyPen
ms.author: genemi
---

# Working with result sets

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

When you work with the data contained in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, one method of manipulating the data is to use a result set. The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] supports the use of result sets through the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object. By using the SQLServerResultSet object, you can retrieve the data returned from an SQL statement or stored procedure, update the data as needed, and then persist that data back to the database.  
  
In addition, the SQLServerResultSet object provides methods for navigating through its rows of data, getting or setting the data that it contains, and for establishing various levels of sensitivity to changes in the underlying database.  
  
> [!NOTE]  
> For more information about managing result sets, including their sensitivity to changes, see [Managing Result Sets with the JDBC driver](../../../connect/jdbc/managing-result-sets-with-the-jdbc-driver.md).  
  
The topics in this section describe different ways that you can use a result set to manipulate the data contained in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
## In this section  
  
| Topic                                                                                           | Description                                                                                                                                                                                             |
| ----------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| [Retrieving result set data sample](../../../connect/jdbc/code-samples/retrieving-result-set-data-sample.md) | Describes how to use a result set to retrieve data from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database and display it.                                                         |
| [Modifying result set data sample](../../../connect/jdbc/code-samples/modifying-result-set-data-sample.md)   | Describes how to use a result set to insert, retrieve, and modify data in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.                                                      |
| [Caching result set data sample](../../../connect/jdbc/code-samples/caching-result-set-data-sample.md)       | Describes how to use a result set to retrieve large amounts of data from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, and to control how that data is cached on the client. |
  
## See also  

[Sample JDBC driver applications](../../../connect/jdbc/code-samples/sample-jdbc-driver-applications.md)  
  
