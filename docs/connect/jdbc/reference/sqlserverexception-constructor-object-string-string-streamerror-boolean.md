---
title: "SQLServerException Constructor (java.lang.Object, java.lang.String, java.lang.String, StreamError, boolean)"
description: "SQLServerException Constructor (java.lang.Object, java.lang.String, java.lang.String, StreamError, boolean)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apitype: "Assembly"
---
# SQLServerException Constructor (java.lang.Object, java.lang.String, java.lang.String, StreamError, boolean)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Initializes a new instance of the [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md) class when given an **object**, a **string** object, a **string** object, a **StreamError** object, and a **boolean**.

## Syntax  
  
```  

public SQLServerException(java.lang.Object obj,
            java.lang.String errText,
            java.lang.String errState,
            StreamError streamError,
            boolean bStack)

			
```  
  
#### Parameters  
 *obj*  
  
 The IO buffer that generated the exception.

 *errText*  
  
 A string containing the error text.
  
 *sqlState*  
  
 An enum object that contains the SQL state.
 
 *streamError*  
  
 A StreamError object that contains details about the error.
 
 *bStack*  
  
 A boolean that indicates if the stack trace should be generated.
  
## Related content

- [SQLServerException Constructors](sqlserverexception-constructors.md)
- [SQLServerException Members](sqlserverexception-members.md)
- [SQLServerException Class](sqlserverexception-class.md)
