---
title: "Getting the driver version"
description: "Learn how and where to find the version of the Microsoft JDBC Driver for SQL Server."
ms.custom: ""
ms.date: "08/12/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 5e241d72-16da-4ada-ac67-e6308394108f
author: David-Engel
ms.author: v-daenge
---
# Getting the driver version
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The version of the installed [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] can be found in the following ways:  
  
-   Call the [SQLServerDatabaseMetaData](../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md) methods [getDriverMajorVersion](../../connect/jdbc/reference/getdrivermajorversion-method-sqlserverdatabasemetadata.md), [getDriverMinorVersion](../../connect/jdbc/reference/getdriverminorversion-method-sqlserverdatabasemetadata.md), or [getDriverVersion](../../connect/jdbc/reference/getdriverversion-method-sqlserverdatabasemetadata.md).  
  
-   The version is displayed in the readme.txt file of the product distribution.  
  
 In addition, the JDBC driver name can be returned from the [getDriverName](../../connect/jdbc/reference/getdrivername-method-sqlserverdatabasemetadata.md) method call on the SQLServerDatabaseMetaData class. It will return, for example, "Microsoft JDBC Driver 6.4 for SQL Server".  
  
 The following is an example of the output from calls to the methods of the SQLServerDatabaseMetaData class:  
  
 `getDriverName = Microsoft JDBC Driver 6.4 for SQL Server`  
  
 `getDriverMajorVersion = 6`  
  
 `getDriverMinorVersion = 4`  
  
 `getDriverVersion = 6.4.` *xxx.x*  
  
 Where "xxx.x" is the final version number.  
  
## See also  
 [Diagnosing problems with the JDBC driver](../../connect/jdbc/diagnosing-problems-with-the-jdbc-driver.md)  
  
  
