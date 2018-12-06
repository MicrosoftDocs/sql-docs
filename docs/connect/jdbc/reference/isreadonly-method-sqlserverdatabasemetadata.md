---
title: "isReadOnly Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.isReadOnly"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: d1569e03-b7bd-486a-af0b-d3f108f712dc
author: MightyPen
ms.author: genemi
manager: craigg
---
# isReadOnly Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database is in read-only mode.  
  
## Syntax  
  
```  
  
public boolean isReadOnly()  
```  
  
## Return Value  
 **true** if the database is in read-only mode. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isReadOnly method is specified by the isReadOnly method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
