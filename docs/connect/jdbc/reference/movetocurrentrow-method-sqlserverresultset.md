---
title: "moveToCurrentRow Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.moveToCurrentRow"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 9a7c754c-2d72-4207-b3bd-2afc6047fb3d
author: MightyPen
ms.author: genemi
manager: craigg
---
# moveToCurrentRow Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves the cursor to the remembered cursor position, which is usually the current row.  
  
## Syntax  
  
```  
  
public void moveToCurrentRow()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This moveToCurrentRow method is specified by the moveToCurrentRow method in the java.sql.ResultSet interface.  
  
 This method has no effect if the cursor is not on the insert row.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
