---
title: "getMaxBinaryLiteralLength Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getMaxBinaryLiteralLength"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 42e49ff9-8072-44e4-ad75-c864c3a4ad8c
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
