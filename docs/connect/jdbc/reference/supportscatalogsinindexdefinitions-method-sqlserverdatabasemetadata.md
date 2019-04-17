---
title: "supportsCatalogsInIndexDefinitions Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsCatalogsInIndexDefinitions"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: a19423a0-e7b6-4f5c-94be-80ddf3fa4717
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsCatalogsInIndexDefinitions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a catalog name can be used in an index definition statement.  
  
## Syntax  
  
```  
  
public boolean supportsCatalogsInIndexDefinitions()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsCatalogsInIndexDefinitions method is specified by the supportsCatalogsInIndexDefinitions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
