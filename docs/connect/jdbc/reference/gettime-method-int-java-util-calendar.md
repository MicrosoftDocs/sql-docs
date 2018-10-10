---
title: "getTime Method (int, java.util.Calendar) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.getTime (int, java.util.Calendar)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 87b7fbaf-7149-494f-b3b2-16b468a8ebf1
author: MightyPen
ms.author: genemi
manager: craigg
---
# getTime Method (int, java.util.Calendar)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.sql.Time object in the Java programming language, given the parameter index, by using the given Calendar object.  
  
## Syntax  
  
```  
  
public java.sql.Time getTime(int index,  
                             java.util.Calendar cal)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter index.  
  
 *cal*  
  
 A Calendar object.  
  
## Return Value  
 A Time object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTime method is specified by the getTime method in the java.sql.CallableStatement interface.  
  
 See the chart titled "Getter Method Conversions" in [Understanding Data Type Conversions](../../../connect/jdbc/understanding-data-type-conversions.md) to see which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types can be retrieved with this method.  
  
## See Also  
 [getTime Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/gettime-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
