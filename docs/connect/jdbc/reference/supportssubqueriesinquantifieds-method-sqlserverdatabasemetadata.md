---
title: "supportsSubqueriesInQuantifieds Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsSubqueriesInQuantifieds"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6749e14c-0f8a-4f1f-8583-dd5cc79b24fe
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsSubqueriesInQuantifieds Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports subqueries in quantified expressions.  
  
## Syntax  
  
```  
  
public boolean supportsSubqueriesInQuantifieds()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsSubqueriesInQuantifieds method is specified by the supportsSubqueriesInQuantifieds method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
