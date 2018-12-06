---
title: "usesLocalFiles Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.usesLocalFiles"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 69afb3a9-ed56-4191-88b8-bc46c03b817b
author: MightyPen
ms.author: genemi
manager: craigg
---
# usesLocalFiles Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database stores tables in a local file.  
  
## Syntax  
  
```  
  
public boolean usesLocalFiles()  
```  
  
## Return Value  
 **true** if the database uses local files. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This usesLocalFiles method is specified by the usesLocalFiles method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
