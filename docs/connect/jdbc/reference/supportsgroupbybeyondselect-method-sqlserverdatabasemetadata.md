---
title: "supportsGroupByBeyondSelect Method (SQLServerDatabaseMetaData) | Microsoft Docs"
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
  - "SQLServerDatabaseMetaData.supportsGroupByBeyondSelect"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: eadd2c37-d9ec-4b47-a91e-ed90b1eaf4b4
caps.latest.revision: 7
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# supportsGroupByBeyondSelect Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports using columns that are not included in the SELECT statement in a GROUP BY clause, provided that all the columns in the SELECT statement are included in the GROUP BY clause.  
  
## Syntax  
  
```  
  
public boolean supportsGroupByBeyondSelect()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsGroupByBeyondSelect method is specified by the supportsGroupByBeyondSelect method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  