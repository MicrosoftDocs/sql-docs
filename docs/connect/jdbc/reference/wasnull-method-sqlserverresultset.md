---
title: "wasNull Method (SQLServerResultSet) | Microsoft Docs"
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
  - "SQLServerResultSet.wasNull"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: d37f80ef-d72c-4429-ada3-1d685bdab6d7
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# wasNull Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Verifies if the last value read was a null value.  
  
## Syntax  
  
```  
  
public boolean wasNull()  
```  
  
## Return Value  
 **true** if the last value read was null. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This wasNull method is specified by the wasNull method in the java.sql.ResultSet interface.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  