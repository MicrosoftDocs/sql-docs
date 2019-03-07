---
title: "getIdentifierQuoteString Method (SQLServerDatabaseMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.getIdentifierQuoteString"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6dea35a0-56a8-412c-8cd3-6539527ff597
author: MightyPen
ms.author: genemi
manager: craigg
---
# getIdentifierQuoteString Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the **String** that is used to quote SQL identifiers.  
  
## Syntax  
  
```  
  
public java.lang.String getIdentifierQuoteString()  
```  
  
## Return Value  
 A **String** that contains the quote identifiers.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getIdentifierQuoteString method is specified by the getIdentifierQuoteString method in the java.sql.DatabaseMetaData interface.  
  
 When using the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] JDBC Driver with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method returns **double** quotation marks ("").  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
