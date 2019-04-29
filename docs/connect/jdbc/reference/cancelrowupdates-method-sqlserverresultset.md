---
title: "cancelRowUpdates Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.cancelRowUpdates"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 2ecacca4-f7bc-4f5d-886a-da7747fdccae
author: MightyPen
ms.author: genemi
manager: craigg
---
# cancelRowUpdates Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Cancels the updates made to the current row in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public void cancelRowUpdates()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This cancelRowUpdates method is specified by the cancelRowUpdates method in the java.sql.ResultSet interface.  
  
 This method can be called after calling an updater method and before calling the [updateRow](../../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md) method to roll back the updates that were made to a row. If no updates have been made or updateRow has already been called, this method has no effect.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
