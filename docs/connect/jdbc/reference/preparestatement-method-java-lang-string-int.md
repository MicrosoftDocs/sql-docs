---
title: "prepareStatement Method (java.lang.String, int) | Microsoft Docs"
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
  - "SQLServerConnection.prepareStatement (java.lang.String, int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4504cd55-81fd-4783-bcb0-1efb1938fdd5
caps.latest.revision: 10
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# prepareStatement Method (java.lang.String, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object for sending parameterized SQL statements to the database, and has the capability to retrieve auto-generated keys.  
  
## Syntax  
  
```  
  
public java.sql.PreparedStatement prepareStatement(java.lang.String sql,  
                                                   int flag)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** containing an SQL statement.  
  
 *flag*  
  
 An **int** that indicates if auto-generated keys will be available.  
  
## Return Value  
 A PreparedStatement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This prepareStatement method is specified by the prepareStatement method in the java.sql.Connection interface.  
  
## See Also  
 [prepareStatement Method &#40;SQLServerConnection&#41;](../../../connect/jdbc/reference/preparestatement-method-sqlserverconnection.md)   
 [SQLServerConnection Members](../../../connect/jdbc/reference/sqlserverconnection-members.md)   
 [SQLServerConnection Class](../../../connect/jdbc/reference/sqlserverconnection-class.md)  
  
  