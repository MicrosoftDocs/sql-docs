---
title: "getObject Method (java.lang.String, java.util.Map) (SQLServerResultSet)"
description: "getObject Method (java.lang.String, java.util.Map) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getObject (java.lang.String, java.util.Map)"
apitype: "Assembly"
---
# getObject Method (java.lang.String, java.util.Map) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gets the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as an object in the Java programming language, using the given Map object.  
  
> [!NOTE]  
>  This method is not currently supported by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. Using this method will always return the default mapping.  
  
## Syntax  
  
```  
  
public java.lang.Object getObject(java.lang.String colName,  
                                  java.util.Map map)  
```  
  
#### Parameters  
 *colName*  
  
 A **String** that contains the column name.  
  
 *map*  
  
 A Map object.  
  
## Return Value  
 An **Object** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getObject method is specified by the getObject method in the java.sql.ResultSet interface.  
  
 This method will return the value of the given column as a Java object. The type of the Java object will be the default Java object type corresponding to the SQL type of the column, following the mapping for built-in types that is specified in the JDBC specification. If the value is an SQL NULL, the driver returns a Java null.  
  
 This method can also be used to read database-specific abstract data types. In the JDBC 2.0 API, the behavior of the getObject method is extended to materialize data of SQL user-defined types. When a column contains a structured or distinct value, the behavior of this method is as if it were a call to `getObject(columnIndex, this.getStatement().getConnection().getTypeMap())`.  
  
 Beginning in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0:  
  
-   A value of type date will be returned as a java.sql.Date object.  
  
-   A value of type time will be returned as a java.sql.Time object.  
  
-   A value of type datetime2 will be returned as a java.sql.Timestamp object.  
  
-   A value of type datetimeoffset will be returned as a microsoft.sql.DateTimeOffset object.  
  
## See Also  
 [getObject Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getobject-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
