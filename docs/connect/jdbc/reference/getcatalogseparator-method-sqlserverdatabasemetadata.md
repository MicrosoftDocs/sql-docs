---
title: "getCatalogSeparator Method (SQLServerDatabaseMetaData)"
description: "getCatalogSeparator Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getCatalogSeparator"
apitype: "Assembly"
---
# getCatalogSeparator Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the **String** that this database uses as the separator between a catalog and table name.  
  
## Syntax  
  
```  
  
public java.lang.String getCatalogSeparator()  
```  
  
## Return Value  
 A **String** that contains the catalog separator.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCatalogSeparator method is specified by the getCatalogSeparator method in the java.sql.DatabaseMetaData interface.  
  
 When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method returns a period (".") as the catalog separator.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
