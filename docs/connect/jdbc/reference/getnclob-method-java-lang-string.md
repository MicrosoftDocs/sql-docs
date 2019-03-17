---
title: "getNClob Method (java.lang.String) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: be01ce56-8f13-437b-8de6-246cda5f7830
author: MightyPen
ms.author: genemi
manager: craigg
---
# getNClob Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of a JDBC **NCLOB** parameter as a NClob object in the Java programming language.  
  
## Syntax  
  
```  
  
public java.sql.NClob getNClob(java.lang.String parameterName)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 ANClobobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNClob method is specified by the getNClob method in the java.sql.CallableStatement interface.  
  
 This method only supports retrieving **NCHAR**, **NVARCHAR**, **NTEXT**, and **XML** parameters. Calling these methods on other data type parameters will cause an exception.  
  
## See Also  
 [getNClob Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getnclob-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)  
  
  
