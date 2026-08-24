---
title: "getMaxCharLiteralLength Method (SQLServerDatabaseMetaData)"
description: "getMaxCharLiteralLength Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getMaxCharLiteralLength"
apitype: "Assembly"
---
# getMaxCharLiteralLength Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of characters that this database allows for a character literal.  
  
## Syntax  
  
```  
  
public int getMaxCharLiteralLength()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of characters allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxCharLiteralLength method is specified by the getMaxCharLiteralLength method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
