---
title: "supportsCatalogsInProcedureCalls Method | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsCatalogsInProcedureCalls"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 5ec3571a-c7c6-4b94-a9ea-ac08adc7f978
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsCatalogsInProcedureCalls Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a catalog name can be used in a procedure call statement.  
  
## Syntax  
  
```  
  
public boolean supportsCatalogsInProcedureCalls()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsCatalogsInProcedureCalls method is specified by the supportsCatalogsInProcedureCalls method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
