---
title: "setFetchDirection Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.setFetchDirection"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4ee82290-508d-4bff-a5c5-8a56338deef8
author: MightyPen
ms.author: genemi
manager: craigg
---
# setFetchDirection Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gives a hint as to the direction in which the rows in this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object will be processed.  
  
> [!NOTE]  
>  This method is not currently supported by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. If you use this method, the JDBC driver remembers the setting, but currently does not act on it.  
  
## Syntax  
  
```  
  
public void setFetchDirection(int direction)  
```  
  
#### Parameters  
 *direction*  
  
 An **int** that indicates the suggested fetch direction. Can be one of the following values:  
  
 ResultSet.FETCH_FORWARD  
  
 ResultSet.FETCH_REVERSE  
  
 ResultSet.FETCH_UNKNOWN  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setFetchDirection method is specified by the setFetchDirection method in the java.sql.ResultSet interface.  
  
 The initial value of this method is determined by the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that produced this SQLServerResultSet object. The fetch direction can be changed at any time.  
  
> [!NOTE]  
>  Using this method when the cursor type is forward-only has no effect.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
