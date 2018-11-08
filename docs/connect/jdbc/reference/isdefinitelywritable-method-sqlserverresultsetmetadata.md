---
title: "isDefinitelyWritable Method (SQLServerResultSetMetaData) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSetMetaData.isDefinitelyWritable"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 7650e89a-dc8e-43ca-8eb2-f962f1a4b4ae
author: MightyPen
ms.author: genemi
manager: craigg
---
# isDefinitelyWritable Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether a write on the designated column will definitely succeed.  
  
## Syntax  
  
```  
  
public boolean isDefinitelyWritable(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 **true** if the column write will definitely succeed. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isDefinitelyWritable method is specified by the isDefinitelyWritable method in the java.sql.ResultSetMetaData interface.  
  
> [!NOTE]  
>  When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method will always return false.  
  
## See Also  
 [SQLServerResultSetMetaData Methods](../../../connect/jdbc/reference/sqlserverresultsetmetadata-methods.md)   
 [SQLServerResultSetMetaData Members](../../../connect/jdbc/reference/sqlserverresultsetmetadata-members.md)   
 [SQLServerResultSetMetaData Class](../../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md)  
  
  
