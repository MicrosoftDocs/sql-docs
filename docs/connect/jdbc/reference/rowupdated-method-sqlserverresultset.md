---
title: "rowUpdated Method (SQLServerResultSet) | Microsoft Docs"
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
  - "SQLServerResultSet.rowUpdated"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 29303550-294e-4d43-b892-312b42e21271
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# rowUpdated Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether the current row has been updated.  
  
## Syntax  
  
```  
  
public boolean rowUpdated()  
```  
  
## Return Value  
 **true** if both the row has been visibly updated by the owner or another user, and updates are detected. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rowUpdated method is specified by the rowUpdated method in the java.sql.ResultSet interface.  
  
 The value that is returned depends on whether or not the result set can detect updates.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] does not detect updated rows for any cursor type.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  