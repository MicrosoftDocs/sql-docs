---
title: "rowDeleted Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.rowDeleted"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 9c6db315-e614-4604-b020-41af6a214cc1
author: MightyPen
ms.author: genemi
manager: craigg
---
# rowDeleted Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a row has been deleted.  
  
## Syntax  
  
```  
  
public boolean rowDeleted()  
```  
  
## Return Value  
 **true** if a row was deleted and deletions are detected. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This rowDeleted method is specified by the rowDeleted method in the java.sql.ResultSet interface.  
  
 A deleted row might leave a visible hole in a result set. This method can be used to detect holes in a result set. The value that is returned depends on whether this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object can detect deletions.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] detects deleted rows for all updatable cursor types, though the detection is transient for forward and dynamic cursors.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
