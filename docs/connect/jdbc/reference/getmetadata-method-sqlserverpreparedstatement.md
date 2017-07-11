---
title: "getMetaData Method (SQLServerPreparedStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerPreparedStatement.getMetaData"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 5ed49a53-ed61-4e95-ad67-45957aaabb6a
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# getMetaData Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md) object that contains information about the columns of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object that will be returned when this [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object is run.  
  
## Syntax  
  
```  
  
public final java.sql.ResultSetMetaData getMetaData()  
```  
  
## Return Value  
 A ResultSetMetaData object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMetaData method is specified by the getMetaData method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  