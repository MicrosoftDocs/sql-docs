---
title: "getTrustManagerConstructorArg Method (SQLServerDataSource)"
description: "getTrustManagerConstructorArg Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getTrustManagerConstructorArg"
apitype: "Assembly"
---
# getTrustManagerConstructorArg Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the String value of the TrustManagerConstructorArg connection property.
  
## Syntax  
  
```  
  
public java.lang.String getTrustManagerConstructorArg()  
```  
  
## Return Value  
 A **String** that contains the value of the TrustManagerConstructorArg connection property, or null if no value is set.  
  
## Remarks  
 If the TrustManagerClass property is not set, the [getTrustManagerConstructorArg](#gettrustmanagerconstructorarg-method-sqlserverdatasource) method returns null.  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
