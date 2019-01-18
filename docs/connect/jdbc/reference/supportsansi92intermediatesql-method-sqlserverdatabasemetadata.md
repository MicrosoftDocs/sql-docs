---
title: "supportsANSI92IntermediateSQL Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsANSI92IntermediateSQL"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4d6e8301-0633-4565-91c6-a80910954461
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsANSI92IntermediateSQL Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the ANSI92 intermediate SQL grammar.  
  
## Syntax  
  
```  
  
public boolean supportsANSI92IntermediateSQL()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsANSI92IntermediateSQL method is specified by the supportsANSI92IntermediateSQL method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
