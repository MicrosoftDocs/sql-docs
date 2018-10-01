---
title: "supportsANSI92EntryLevelSQL Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsANSI92EntryLevelSQL"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: a3fffc08-7254-4af7-bbae-8ff591fbd5ec
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsANSI92EntryLevelSQL Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the ANSI92 entry level SQL grammar.  
  
## Syntax  
  
```  
  
public boolean supportsANSI92EntryLevelSQL()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsANSI92EntryLevelSQL method is specified by the supportsANSI92EntryLevelSQL method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
