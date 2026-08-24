---
title: "getClientInfo Method (java.lang.String)"
description: "getClientInfo Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getClientInfo Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of a specified client information property.  
  
## Syntax  
  
```  
  
public java.lang.String getClientInfo (java.lang.String name)  
```  
  
#### Parameters  
 *name*  
  
 A String that contains the name of the client information property to retrieve.  
  
## Return Value  
 A String that contains the value of the client information property.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getClientInfo method is specified by the getClientInfo method in the java.sql.Connection interface.  
  
 The [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] does not support any client info properties. As a result, this method returns **null**.  
  
 Similarly, applications can use the [getClientInfoProperties](../../../connect/jdbc/reference/getclientinfoproperties-method-sqlserverdatabasemetadata.md) method of the [SQLServerDatabaseMetaData](../../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md) class to retrieve a list of the client information properties that the driver supports. The [getClientInfoProperties](../../../connect/jdbc/reference/getclientinfoproperties-method-sqlserverdatabasemetadata.md) method returns an empty result set.  
  
## Related content

- [getClientInfo Method (SQLServerConnection)](getclientinfo-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
