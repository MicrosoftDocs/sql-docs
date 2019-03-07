---
title: "getFetchDirection Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getFetchDirection"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 5ab385c2-e18c-4b75-ac2d-2402af5c52a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# getFetchDirection Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the fetch direction for this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public int getFetchDirection()  
```  
  
## Return Value  
 An **int** that indicates the current fetch direction.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFetchDirection method is specified by the getFetchDirection method in the java.sql.ResultSet interface.  
  
 This method returns FETCH_FORWARD for forward-only cursors, the last setting made by a call to the [setFetchDirection](../../../connect/jdbc/reference/setfetchdirection-method-sqlserverresultset.md) method for other cursor types, and will return FETCH_UNKNOWN these cursor types if the setFetchDirection method has never been called.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
