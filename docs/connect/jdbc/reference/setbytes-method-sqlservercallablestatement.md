---
title: "setBytes Method (SQLServerCallableStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.setBytes"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f264f1a6-ee35-4eaf-81d8-ecf99f03b35d
author: MightyPen
ms.author: genemi
manager: craigg
---
# setBytes Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given array of **byte** values.  
  
## Syntax  
  
```  
  
public void setBytes(java.lang.String sCol,  
                     byte[] b)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
 *b*  
  
 An array of **byte** values.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 In a previous version of the driver, you could use SQLServerCallableStatement.setBytes to convert values between byte arrays and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type **date**, **time**, **datetime2**, or **datetimeoffset**. Now, using this method with those data types will cause an exception indicating that the conversion is not supported.  
  
 This setBytes method is specified by the setBytes method in the java.sql.CallableStatement interface.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
