---
title: "SQLServerException Constructor (java.lang.Object, java.lang.String, java.lang.String, StreamError, boolean) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid:
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [SQLServerException Constructors](../../../connect/jdbc/reference/sqlserverexception-constructors.md)   
 [SQLServerException Members](../../../connect/jdbc/reference/sqlserverexception-members.md)   
 [SQLServerException Class](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
  
