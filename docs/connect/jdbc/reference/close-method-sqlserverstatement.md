---
title: "close Method (SQLServerStatement) | Microsoft Docs"
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
  - "SQLServerStatement.close"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 84a25d64-dd3e-4696-bb5f-4eaf391fab7e
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# close Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Releases this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object's database and JDBC resources immediately instead of waiting for them to be automatically released.  
  
## Syntax  
  
```  
  
public void close()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This close method is specified by the close method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  