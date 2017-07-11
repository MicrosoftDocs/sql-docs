---
title: "SQLServerClob Constructor (SQLServerConnection, java.lang.String) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerConnection.SQLServerClob (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7058f4f7-ef3e-4d62-90d1-79299708b1eb
caps.latest.revision: 16
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# SQLServerClob Constructor (SQLServerConnection, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Initializes a new instance of the [SQLServerClob](../../../connect/jdbc/reference/sqlserverclob-class.md) class when given a [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object and a string of data.  
  
> [!NOTE]  
>  This method has been deprecated in JDBC Driver version 2.0. Instead, use the [createClob](../../../connect/jdbc/reference/createclob-method-sqlserverconnection.md) method of the [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) class.  
  
## Syntax  
  
```  
  
public SQLServerClob(SQLServerConnection connection,  
                     java.lang.String data)  
```  
  
#### Parameters  
 *connection*  
  
 A SQLServerConnection object.  
  
 *data*  
  
 The CLOB data.  
  
## See Also  
 [SQLServerClob Constructors](../../../connect/jdbc/reference/sqlserverclob-constructors.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  