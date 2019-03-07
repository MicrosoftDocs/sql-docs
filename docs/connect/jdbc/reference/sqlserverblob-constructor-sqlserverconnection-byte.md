---
title: "SQLServerBlob Constructor (SQLServerConnection, byte) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerConnection, byte[].SQLServerBlob"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 9fe573e3-30db-4828-abab-e9346493e931
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLServerBlob Constructor (SQLServerConnection, byte)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Initializes a new instance of the [SQLServerBlob](../../../connect/jdbc/reference/sqlserverblob-class.md) class when given a [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object and a **byte** array.  
  
> [!NOTE]  
>  This method has been deprecated in JDBC Driver version 2.0. Instead, use the [createBlob](../../../connect/jdbc/reference/createblob-method-sqlserverconnection.md) method of the [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) class.  
  
## Syntax  
  
```  
  
public SQLServerBlob(SQLServerConnection connection,  
                     byte[] data)  
```  
  
#### Parameters  
 *connection*  
  
 A SQLServerConnection object.  
  
 *data*  
  
 A **byte** array.  
  
## See Also  
 [SQLServerBlob Constructors](../../../connect/jdbc/reference/sqlserverblob-constructors.md)   
 [SQLServerBlob Members](../../../connect/jdbc/reference/sqlserverblob-members.md)   
 [SQLServerBlob Class](../../../connect/jdbc/reference/sqlserverblob-class.md)  
  
  
