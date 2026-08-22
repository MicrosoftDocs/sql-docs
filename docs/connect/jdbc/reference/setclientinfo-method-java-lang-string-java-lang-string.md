---
title: "setClientInfo Method (java.lang.String, java.lang.String)"
description: "setClientInfo Method (java.lang.String, java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setClientInfo Method (java.lang.String, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the value of the specified client information property.  
  
## Syntax  
  
```  
  
public void setClientInfo (java.lang.String name,  
                           java.lang.String value)  
```  
  
#### Parameters  
 *name*  
  
 A String that contains the name of the client information property to set.  
  
 *value*  
  
 A String that contains the value to set the client information property to.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setClientInfo method is specified by the setClientInfo method in the java.sql.Connection interface.  
  
 The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] does not support any client information properties. In the 2.0 JDBC Driver, this method generates a warning for a property. Applications should use [getWarnings](../../../connect/jdbc/reference/getwarnings-method-sqlserverconnection.md) method of the [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) class to retrieve a warning.  
  
## Related content

- [setClientInfo Method (SQLServerConnection)](setclientinfo-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
