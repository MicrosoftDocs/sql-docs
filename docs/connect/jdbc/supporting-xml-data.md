---
title: "Supporting XML Data | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 32b7217e-1f0c-473d-9a45-176daa81584e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supporting XML Data
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides an **xml** data type that lets you store XML documents and fragments in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The **xml** data type is a built-in data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and is in some ways similar to other built-in types, such as **int** and **varchar**. Like other built-in types, you can use the **xml** data type as: a variable type, a parameter type, a function-return type, or a column type when you create a table; or in [!INCLUDE[tsql](../../includes/tsql-md.md)] CAST and CONVERT functions. In the JDBC driver, the **xml** data type can be mapped as a String, byte array, stream, CLOB, BLOB, or SQLXML object. String is the default mapping.  
  
 The JDBC driver provides support for the JDBC 4.0 API, which introduces the SQLXML interface. The SQLXML interface defines methods to interact with and manipulate XML data. The **SQLXML** is a JDBC 4.0 data type and it maps to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**xml** data type. Therefore, in order to use the SQLXML data type in your applications, you must set the classpath to include the sqljdbc4.jar file. If the application tries to use the sqljdbc3.jar when accessing the SQLXML object and its methods, an exception is thrown.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always validates the XML data before storing it in the database column. Applications can use **SQLXML** data type, because the JDBC driver maps it to the **xml** data type automatically. The **SQLXML** support is available in sqljdbc4.jar. See [System Requirements for the JDBC Driver](../../connect/jdbc/system-requirements-for-the-jdbc-driver.md) for the list of JRE versions supported by the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)].  
  
 The topics in this section describe the SQLXML interface and how to program against the **SQLXML** data type by using the JDBC API methods.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[SQLXML Interface](../../connect/jdbc/sqlxml-interface.md)|Describes the SQLXML interface and its methods.|  
|[Programming with SQLXML](../../connect/jdbc/programming-with-sqlxml.md)|Describes how to use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] API methods to store and retrieve an XML data in and from a relational database with the **SQLXML** Java data type. Also contains information about the types of SQLXML objects and provides a list of important guidelines and limitations when using SQLXML objects.|  
  
## See Also  
 [Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)  
  
  
