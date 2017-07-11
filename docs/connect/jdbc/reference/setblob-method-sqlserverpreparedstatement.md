---
title: "setBlob Method (SQLServerPreparedStatement) | Microsoft Docs"
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
  - "SQLServerPreparedStatement.setBlob"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 218ff486-3f31-49e4-ad81-a423246a8307
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# setBlob Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given Blob object.  
  
## Syntax  
  
```  
  
public final void setBlob(int i,  
                          java.sql.Blob x)  
```  
  
#### Parameters  
 *i*  
  
 An **int** that indicates the parameter number.  
  
 *x*  
  
 A Blob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setBlob method is specified by the setBlob method in the java.sql.PreparedStatement interface.  
  
## See Also  
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  