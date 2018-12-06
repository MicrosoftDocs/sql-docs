---
title: "getSearchStringEscape Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getSearchStringEscape"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ea0f95d0-0238-4dc8-9f26-7ff9b65f30c3
author: MightyPen
ms.author: genemi
manager: craigg
---
# getSearchStringEscape Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the **String** that can be used to escape wildcard characters.  
  
## Syntax  
  
```  
  
public java.lang.String getSearchStringEscape()  
```  
  
## Return Value  
 A **String** that contains the escape wildcard character String.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSearchStringEscape method is specified by the getSearchStringEscape method in the java.sql.DatabaseMetaData interface.  
  
 This method is used only for metadata pattern searches. It returns "\\". A **String** search pattern can escape wildcards ("%" and "_") and provide them as literals by prepending a backslash. This translates "\\%" to "[%]" and "\\\_" to "[\_]".  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
