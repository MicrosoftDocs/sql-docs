---
title: "setTrustManagerConstructorArg Method (SQLServerDataSource)"
description: "setTrustManagerConstructorArg Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setTrustManagerConstructorArg"
apitype: "Assembly"
---
# setTrustManagerConstructorArg Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the String value of the TrustManagerConstructorArg connection property.
  
## Syntax  
  
```  
  
public void setTrustManagerConstructorArg(java.lang.String trustManagerClass)  
```  
  
#### Parameters  
 *trustManagerClass*  
  
 A **String** that contains the fully qualified class name of a custom javax.net.ssl.TrustManager.
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
