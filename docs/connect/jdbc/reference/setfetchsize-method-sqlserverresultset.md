---
title: "setFetchSize Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.setFetchSize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 233bf4f8-4758-42d0-a80b-33e34fa78027
author: MightyPen
ms.author: genemi
manager: craigg
---
# setFetchSize Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gives the JDBC driver a hint as to the number of rows that should be fetched from the database when more rows are needed for this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public void setFetchSize(int rows)  
```  
  
#### Parameters  
 *rows*  
  
 An **int** indicating the number of rows to fetch.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setFetchSize method is specified by the setFetchSize method in the java.sql.ResultSet interface.  
  
 If the fetch size specified is zero, the JDBC driver ignores the value and estimates what the fetch size should be. The default value is set by the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that created the result set. The fetch size can be changed at any time.  
  
 This method changes the block fetch size for server cursors, and takes effect the next time the JDBC driver needs to call sp_cursorfetch. Setting the fetch size to zero restores the default fetch size for the cursor type that is currently in use  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
