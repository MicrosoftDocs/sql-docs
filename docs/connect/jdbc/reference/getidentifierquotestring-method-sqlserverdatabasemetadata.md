---
title: "getIdentifierQuoteString Method (SQLServerDatabaseMetaData)"
description: "getIdentifierQuoteString Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getIdentifierQuoteString"
apitype: "Assembly"
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
  
 When using the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] JDBC Driver with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method returns **double** quotation marks ("").  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
