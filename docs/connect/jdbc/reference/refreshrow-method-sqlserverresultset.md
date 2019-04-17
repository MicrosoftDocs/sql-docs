---
title: "refreshRow Method (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.refreshRow"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 048fe245-157f-4fd8-be75-ce54b83e02b3
author: MightyPen
ms.author: genemi
manager: craigg
---
# refreshRow Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Refreshes the current row with its most recent value in the database.  
  
## Syntax  
  
```  
  
public void refreshRow()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This refreshRow method is specified by the refreshRow method in the java.sql.ResultSet interface.  
  
 This method cannot be called when the cursor is on the insert row.  
  
 This method provides a way for an application to explicitly tell the JDBC driver to refetch rows from the database. An application might need to call this method when the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] is caching or prefetching to fetch the latest value of a row from the database. The JDBC driver might actually refresh multiple rows at the same time if the fetch size is greater than one.  
  
 All values are refetched subject to the transaction isolation level and cursor sensitivity. If this method is called after calling an updater method, but before calling the [updateRow](../../../connect/jdbc/reference/updaterow-method-sqlserverresultset.md) method, the updates made to the row are lost. Calling this method frequently will probably slow performance.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
