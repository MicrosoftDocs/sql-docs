---
title: "SQLServerException Constructor (java.lang.String, SQLState, DriverError, java.lang.Throwable)"
description: "SQLServerException Constructor (java.lang.String, SQLState, DriverError, java.lang.Throwable)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apitype: "Assembly"
---
# SQLServerException Constructor (java.lang.String, SQLState, DriverError, java.lang.Throwable)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Initializes a new instance of the [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md) class when given a **string** object, a **sqlstate** object, a **drivererror** object, and a **throwable** object.

## Syntax  
  
```  
public SQLServerException(java.lang.String errText,
            SQLState sqlState,
            DriverError driverError,
            java.lang.Throwable cause)
			
```  
  
#### Parameters  
 *errText*  
  
 A string that holds the error text.
  
 *sqlState*  
  
 An enum object that holds the SQL state.
 
 *driverError*  
  
 An enum object that holds the driver error.
 
 *cause*  
  
 A throwable object that holds the cause of the exception.
  
## See Also  
 [SQLServerException Constructors](../../../connect/jdbc/reference/sqlserverexception-constructors.md)   
 [SQLServerException Members](../../../connect/jdbc/reference/sqlserverexception-members.md)   
 [SQLServerException Class](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
  
