---
title: "supportsSchemasInPrivilegeDefinitions Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsSchemasInPrivilegeDefinitions"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 2957af1d-62d6-4375-b214-bbba9aafcc2d
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsSchemasInPrivilegeDefinitions Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a schema name can be used in a privilege definition statement.  
  
## Syntax  
  
```  
  
public boolean supportsSchemasInPrivilegeDefinitions()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsSchemasInPrivilegeDefinitions method is specified by the supportsSchemasInPrivilegeDefinitions method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
