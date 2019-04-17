---
title: "nullsAreSortedAtStart Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.nullsAreSortedAtStart Method (SQLServerDatabaseMetaData)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 372515da-3b0e-46f6-8c0b-01b1b45c5a2f
author: MightyPen
ms.author: genemi
manager: craigg
---
# nullsAreSortedAtStart Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether NULL values are sorted at the start, regardless of sort order.  
  
## Syntax  
  
```  
  
public boolean nullsAreSortedAtStart()  
```  
  
## Return Value  
 **true** if sorted at the start. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This nullsAreSortedAtStart method is specified by the nullsAreSortedAtStart method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
