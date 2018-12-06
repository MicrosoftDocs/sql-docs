---
title: "insertRow Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.insertRow"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 363d1008-1396-4fc0-8e27-c9ba2499e7f1
author: MightyPen
ms.author: genemi
manager: craigg
---
# insertRow Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Adds the contents of the insert row to this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object and to the database.  
  
## Syntax  
  
```  
  
public void insertRow()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This insertRow method is specified by the insertRow method in the java.sql.ResultSet interface.  
  
 The cursor must be on the insert row when this method is called. After this method is called, the cursor remains on the insert row and the result set remains in insert mode.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
