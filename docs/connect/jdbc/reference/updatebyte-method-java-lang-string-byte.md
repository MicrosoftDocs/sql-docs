---
title: "updateByte Method (java.lang.String, byte) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.updateByte (java.lang.String, byte)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 5416aed2-a5b6-4e3b-9750-90db8cda8cec
author: MightyPen
ms.author: genemi
manager: craigg
---
# updateByte Method (java.lang.String, byte)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **byte** value given the column name.  
  
## Syntax  
  
```  
  
public void updateByte(java.lang.String columnName,  
                       byte x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A **byte** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateByte method is specified by the updateByte method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateByte Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatebyte-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
