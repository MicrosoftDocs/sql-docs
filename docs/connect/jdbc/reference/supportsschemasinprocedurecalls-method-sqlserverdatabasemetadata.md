---
title: "supportsSchemasInProcedureCalls Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsSchemasInProcedureCalls"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 8955457a-b176-4674-9366-39a1942164a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsSchemasInProcedureCalls Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a schema name can be used in a procedure call statement.  
  
## Syntax  
  
```  
  
public boolean supportsSchemasInProcedureCalls()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsSchemasInProcedureCalls method is specified by the supportsSchemasInProcedureCalls method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
