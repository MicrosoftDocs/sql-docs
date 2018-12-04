---
title: "locatorsUpdateCopy Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.locatorsUpdateCopy"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f6ec8c1d-7ff8-4bc5-8bd3-0199a9294a6e
author: MightyPen
ms.author: genemi
manager: craigg
---
# locatorsUpdateCopy Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether updates made to a LOB are made on a copy or directly to the LOB.  
  
## Syntax  
  
```  
  
public boolean locatorsUpdateCopy()  
```  
  
## Return Value  
 **true** if updates are made to a copy. **false** if updates are made directly.  
  
## Exceptions  
 java.sql.SQLException  
  
## Remarks  
 This locatorsUpdateCopy method is specified by the locatorsUpdateCopy method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
