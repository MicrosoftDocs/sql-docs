---
title: "getMaxProcedureNameLength Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getMaxProcedureNameLength"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e1c05eb3-8465-46fd-99bc-5e8effcafee5
author: MightyPen
ms.author: genemi
manager: craigg
---
# getMaxProcedureNameLength Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of characters that this database allows in a procedure name.  
  
## Syntax  
  
```  
  
public int getMaxProcedureNameLength()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of characters allowed.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxProcedureNameLength method is specified by the getMaxProcedureNameLength method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
