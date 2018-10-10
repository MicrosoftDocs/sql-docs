---
title: "getType Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getType"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ffbc4a02-e851-431c-bc1a-7ab381d982bb
author: MightyPen
ms.author: genemi
manager: craigg
---
# getType Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the cursor type of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Syntax  
  
```  
  
public int getType()  
```  
  
## Return Value  
 An **int** that indicates the current cursor type, which can be one of the following values:  
  
 ResultSet.TYPE_FORWARD_ONLY  
  
 ResultSet.TYPE_SCROLL_INSENSITIVE  
  
 ResultSet.TYPE_SCROLL_SENSITIVE  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getType method is specified by the getType method in the java.sql.ResultSet interface.  
  
 This method can be used to determine the actual cursor type. If the application selected TYPE_FORWARD_ONLY or used a default cursor type, TYPE_FORWARD_ONLY will be returned.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
