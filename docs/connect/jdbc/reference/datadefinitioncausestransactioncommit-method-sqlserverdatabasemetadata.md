---
title: "Does Data Definition Statement Force Transaction Commit."
description: "dataDefinitionCausesTransactionCommit Method (SQLServerDatabaseMetaData)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.dataDefinitionCausesTransactionCommit"
apitype: "Assembly"
---
# dataDefinitionCausesTransactionCommit Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves whether a data definition statement within a transaction forces the transaction to commit.  
  
## Syntax  
  
```  
  
public boolean dataDefinitionCausesTransactionCommit()  
```  
  
## Return Value  
 **true** if the DDL statement forces a commit. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This dataDefinitionCausesTransactionCommit method is specified by the dataDefinitionCausesTransactionCommit method in the java.sql.DatabaseMetaData interface.  
  
## See Also  
 [SQLServerDatabaseMetaData Methods](../../../connect/jdbc/reference/sqlserverdatabasemetadata-methods.md)   
 [SQLServerDatabaseMetaData Members](../../../connect/jdbc/reference/sqlserverdatabasemetadata-members.md)   
 [SQLServerDatabaseMetaData Class](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md)  
  
  
