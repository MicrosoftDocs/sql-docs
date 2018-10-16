---
title: "Does Data Definition Statement Force Transaction Commit. | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDatabaseMetaData.dataDefinitionCausesTransactionCommit"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: bf04fa73-b9f1-4403-b6a0-e53d0d27c671
author: MightyPen
ms.author: genemi
manager: craigg
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
  
  
