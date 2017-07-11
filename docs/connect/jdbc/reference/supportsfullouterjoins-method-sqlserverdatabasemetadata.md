---
title: "supportsFullOuterJoins Method (SQLServerDatabaseMetaData) | Microsoft Docs"
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
  - "SQLServerDatabaseMetaData.supportsFullOuterJoins"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 836f1f45-59ed-4a34-9809-2000d3062576
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# supportsFullOuterJoins Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports full nested outer joins.  
  
## Syntax  
  
```  
  
public boolean supportsFullOuterJoins()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsFullOuterJoins method is specified by the supportsFullOuterJoins method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  