---
title: "getCharacterStream Method () | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerClob.getCharacterStream"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 70a5a8c8-791a-43f9-8a0e-1c390f30857c
author: MightyPen
ms.author: genemi
manager: craigg
---
# getCharacterStream Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the **CLOB** data as a Reader object or as a stream of characters.  
  
## Syntax  
  
```  
  
public java.io.Reader getCharacterStream()  
```  
  
## Return Value  
 A Reader object that contains the **CLOB** data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCharacterStream method is specified by the getCharacterStream method in the java.sql.Clob interface.  
  
## See Also  
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)   
 [SQLServerClob Class](../../../connect/jdbc/reference/sqlserverclob-class.md)  
  
  
