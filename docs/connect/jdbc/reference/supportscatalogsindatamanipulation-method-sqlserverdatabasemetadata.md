---
title: "supportsCatalogsInDataManipulation Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsCatalogsInDataManipulation"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ee1af47a-4c04-4391-83a5-54ced80218c8
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsCatalogsInDataManipulation Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a catalog name can be used in a data manipulation statement.  
  
## Syntax  
  
```  
  
public boolean supportsCatalogsInDataManipulation()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsCatalogInDataManipulation method is specified by the supportsCatalogInDataManipulation method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
