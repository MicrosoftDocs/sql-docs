---
title: "getSearchStringEscape Method (SQLServerDatabaseMetaData)"
description: "getSearchStringEscape Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getSearchStringEscape"
apitype: "Assembly"
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
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
