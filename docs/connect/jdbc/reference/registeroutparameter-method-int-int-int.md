---
title: "registerOutParameter Method (int, int, int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.registerOutParameter (int, int, int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: d902d4e0-881f-4182-814c-0ede9a8da7fd
author: MightyPen
ms.author: genemi
manager: craigg
---
# registerOutParameter Method (int, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Registers the OUT parameter in the specified ordinal position to the given JDBC type and scale.  
  
## Syntax  
  
```  
  
public void registerOutParameter(int index,  
                                 int sqlType,  
                                 int scale)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the ordinal position of the parameter.  
  
 *sqlType*  
  
 A JDBC type code as defined in java.sql.Types.  
  
 *scale*  
  
 An **int** that indicates the number of digits to be placed to the right of the decimal point.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This registerOutParameter method is specified by the registerOutParameter method in the java.sql.CallableStatement interface.  
  
 Beginning with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, when *sqlType* is of type java.sql.Types.TIME, the behavior of this method is modified by the **sendTimeAsDatetime** connection property ([Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md)) and [SQLServerDataSource.setSendTimeAsDatetime](../../../connect/jdbc/reference/setsendtimeasdatetime-method-sqlserverdatasource.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## See Also  
 [registerOutParameter Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
