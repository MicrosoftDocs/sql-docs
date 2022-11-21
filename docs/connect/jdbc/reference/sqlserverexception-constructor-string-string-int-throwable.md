---
title: "SQLServerException Constructor (java.lang.String, java.lang.String, int, java.lang.Throwable)"
description: "SQLServerException Constructor (java.lang.String, java.lang.String, int, java.lang.Throwable)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apitype: "Assembly"
---
# SQLServerException Constructor (java.lang.String, java.lang.String, int, java.lang.Throwable)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Initializes a new instance of the [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md) class when given a **string** object, a **string** object, an **int**, and a **throwable** object.

## Syntax  
  
```  
public SQLServerException(java.lang.String errText,
            SQLState errState,
            int errNum,
            java.lang.Throwable cause)
			
```  
  
#### Parameters  
 *errText*  
  
 A string containing the error text.
  
 *errState*  
  
 A string containing the state of the error.
 
 *errNum*  
  
 An int that contains the error code for the exception.
 
 *cause*  
  
 A throwable object that contains the cause of the exception.
  
## See Also  
 [SQLServerException Constructors](../../../connect/jdbc/reference/sqlserverexception-constructors.md)   
 [SQLServerException Members](../../../connect/jdbc/reference/sqlserverexception-members.md)   
 [SQLServerException Class](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
  
