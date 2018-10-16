---
title: "setTrustManagerConstructorArg Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.setTrustManagerConstructorArg"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
