---
title: "getConcurrency Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getConcurrency"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 207e25f4-769c-4ff3-913c-3517b06208e4
author: MightyPen
ms.author: genemi
manager: craigg
---
# getConcurrency Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the concurrency mode of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public int getConcurrency()  
```  
  
## Return Value  
 An **int** that indicates the concurrency type, which can be one of the following values:  
  
 ResultSet.CONCUR_READ_ONLY  
  
 ResultSet.CONCUR_UPDATABLE  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getConcurrency method is specified by the getConcurrency method in the java.sql.ResultSet interface.  
  
 The concurrency used is determined by the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that created the result set.  
  
 This method can be used to determine the actual concurrency. If the application selected CONCUR_READ_ONLY or CONCUR_UPDATABLE, these will be returned. If the application used default concurrency, CONCUR_READ_ONLY will be returned.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
