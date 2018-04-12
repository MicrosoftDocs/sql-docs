---
title: "getTrustManagerConstructorArg Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: "sql"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "jdbc"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerDataSource.getTrustManagerConstructorArg"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
caps.latest.revision: 1
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
ms.workload: "Inactive"
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
 If the TrustManagerClass property is not set, the [getTrustManagerConstructorArg](../../../connect/jdbc/reference/gettrustmanagerconstructorarg-method-sqlserverdatasource.md) method returns null.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
