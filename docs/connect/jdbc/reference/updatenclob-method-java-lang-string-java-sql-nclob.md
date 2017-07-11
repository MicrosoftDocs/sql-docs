---
title: "updateNClob Method (java.lang.String, java.sql.NClob) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a025d124-3634-49fa-8bb5-e9b98f2d5de3
caps.latest.revision: 18
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# updateNClob Method (java.lang.String, java.sql.NClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an **NClob** value.  
  
## Syntax  
  
```  
  
public void updateNClob(java.lang.String columnLabel,  
                        java.sql.NClob x)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that indicates the column label.  
  
 *x*  
  
 An NClob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateNClob method is specified by the updateNClob method in the java.sql.ResultSet interface.  
  
 This method is supported only on **nvarchar(max)**, **ntext**, and **xml** columns. Using this method on any other data types will cause an exception to be thrown.  
  
## See Also  
 [updateNClob Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatenclob-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  