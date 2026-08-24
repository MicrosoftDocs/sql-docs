---
title: "getUDTs Method (SQLServerDatabaseMetaData)"
description: "getUDTs Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getUDTs"
apitype: "Assembly"
---
# getUDTs Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves a description of the user-defined types that are defined in a particular schema.  
  
> [!NOTE]  
>  This method is not currently supported with [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. When used, this method will always return an empty result set.  
  
## Syntax  
  
```  
  
public java.sql.ResultSet getUDTs(java.lang.String catalog,  
                                  java.lang.String schemaPattern,  
                                  java.lang.String typeNamePattern,  
                                  int[] types)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name.  
  
 *schemaPattern*  
  
 A **String** that contains the schema name pattern.  
  
 *typeNamePattern*  
  
 A **String** that contains the type name pattern.  
  
 *types*  
  
 An array of ints that contain the data types to include. Null indicates that all types should be included.  
  
## Return Value  
 A [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getUDTs method is specified by the getUDTs method in the java.sql.DatabaseMetaData interface.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
