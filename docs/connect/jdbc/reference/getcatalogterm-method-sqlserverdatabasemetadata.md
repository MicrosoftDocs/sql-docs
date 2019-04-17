---
title: "getCatalogTerm Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getCatalogTerm"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 0aa5d372-16aa-4790-a8f6-f8b742798f8f
author: MightyPen
ms.author: genemi
manager: craigg
---
# getCatalogTerm Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the database vendor's preferred term for "catalog".  
  
## Syntax  
  
```  
  
public java.lang.String getCatalogTerm()  
```  
  
## Return Value  
 A **String** that contains the catalog term.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCatalogTerm method is specified by the getCatalogTerm method in the java.sql.DatabaseMetaData interface.  
  
 When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method will return the term "database".  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
