---
title: "getRow Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getRow"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: a266e3bc-05c2-44e2-9346-125ae6780216
author: MightyPen
ms.author: genemi
manager: craigg
---
# getRow Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the current row number.  
  
## Syntax  
  
```  
  
public int getRow()  
```  
  
## Return Value  
 An **int** that indicates the current row number, 0 if there is no row.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getRow method is specified by the getRow method in the java.sql.ResultSet interface.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
