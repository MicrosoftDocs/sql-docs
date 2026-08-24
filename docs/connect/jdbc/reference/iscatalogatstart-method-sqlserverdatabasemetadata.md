---
title: "isCatalogAtStart Method (SQLServerDatabaseMetaData)"
description: "isCatalogAtStart Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.isCatalogAtStart"
apitype: "Assembly"
---
# isCatalogAtStart Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a catalog appears at the start of a fully qualified table name.  
  
## Syntax  
  
```  
  
public boolean isCatalogAtStart()  
```  
  
## Return Value  
 **true** if the catalog name is first. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isCatalogAtStart method is specified by the isCatalogAtStart method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
