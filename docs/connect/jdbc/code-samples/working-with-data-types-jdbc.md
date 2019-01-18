---
title: "Working with Data Types (JDBC) | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: b39f44d0-3710-4bc6-880c-35bd8c10a734
author: MightyPen
ms.author: genemi
manager: craigg
---
# Working with Data Types (JDBC)

[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

The primary function of the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] is to allow Java developers to access data contained in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] databases. To accomplish this, the JDBC driver mediates the conversion between [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types and Java language types and objects.  
  
> [!NOTE]  
> For a detailed discussion of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and JDBC driver data types, including their differences and how they are converted to Java language data types, see [Understanding the JDBC Driver Data Types](../../../connect/jdbc/understanding-the-jdbc-driver-data-types.md).  
  
In order to work with SQL Server data types, the JDBC driver provides get\<Type> and set\<Type> methods for the [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) and [SQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md) classes, and it provides get\<Type> and update\<Type> methods for the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) class. Which method you use depends on the type of data that you are working with, and whether you are using result sets or queries.  
  
The topics in this section describe how to use the JDBC driver data types to access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data in your Java applications.  
  
## In This Section  
  
| Topic                                                                         | Description                                                                                                                                                                                                                                                  |
| ----------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| [Basic Data Types Sample](../../../connect/jdbc/code-samples/basic-data-types-sample.md)   | Describes how to use result set getter methods to retrieve basic [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type values, and how to use result set update methods to update those values.                                             |
| [SQLXML Data Type Sample](../../../connect/jdbc/code-samples/sqlxml-data-type-sample.md)   | Describes how to store an XML data in a relational database, how to retrieve an XML data from a database, and how to parse an XML data with the **SQLXML** Java data type.                                                                                   |
| [Spatial Data Types Sample](../../../connect/jdbc/code-samples/spatial-data-types-sample.md) | Describes how to store Spatial Datatypes in SQL Server and how to retrieve these types back from SQL Server. Also discusses how to use newly defined classes **Geometry** and **Geography** from the driver, for managing Java reference of these datatypes. |
  
## See Also

[Sample JDBC Driver Applications](../../../connect/jdbc/code-samples/sample-jdbc-driver-applications.md)  
