---
title: "getCatalogTerm Method (SQLServerDatabaseMetaData)"
description: "getCatalogTerm Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getCatalogTerm"
apitype: "Assembly"
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
  
  
