---
title: "getFetchDirection Method (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.getFetchDirection"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ceb4ae68-decc-46d3-83f1-0bbd23aaf58c
author: MightyPen
ms.author: genemi
manager: craigg
---
# getFetchDirection Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the direction for fetching rows from database tables that is the default for result sets that are generated from this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
> [!NOTE]  
>  This method is not currently implemented by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. Therefore, it will always return FETCH_UNKNOWN.  
  
## Syntax  
  
```  
  
public final int getFetchDirection()  
```  
  
## Return Value  
 An **int** that indicates the fetch direction that is specified by the [setFetchDirection](../../../connect/jdbc/reference/setfetchdirection-method-sqlserverstatement.md) method.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFetchDirection method is specified by the getFetchDirection method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
