---
title: "supportsConvert Method () | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.supportsConvert ()"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 45c83c4f-649a-4cd6-9d44-d38524758bb8
author: MightyPen
ms.author: genemi
manager: craigg
---
# supportsConvert Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether this database supports the CONVERT function between SQL types.  
  
## Syntax  
  
```  
  
public boolean supportsConvert()  
```  
  
## Return Value  
 **true** if supported. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This supportsConvert method is specified by the supportsConvert method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [supportsConvert Method &#40;SQLServerDatabaseMetaData&#41;](../../../connect/jdbc/reference/supportsconvert-method-sqlserverdatabasemetadata.md)   
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
