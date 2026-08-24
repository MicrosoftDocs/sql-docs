---
title: "getMaxBinaryLiteralLength Method (SQLServerDatabaseMetaData)"
description: "getMaxBinaryLiteralLength Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getMaxBinaryLiteralLength"
apitype: "Assembly"
---
# getMaxBinaryLiteralLength Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of hex characters this database allows in an inline binary literal.  
  
## Syntax  
  
```  
  
public int getMaxBinaryLiteralLength()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of hexadecimal characters.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxBinaryLiteralLength method is specified by the getMaxBinaryLiteralLength method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
