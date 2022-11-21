---
title: "getReference Method (SQLServerDataSource)"
description: "getReference Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getReference"
apitype: "Assembly"
---
# getReference Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a reference to this [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object.  
  
## Syntax  
  
```  
  
public javax.naming.Reference getReference()  
```  
  
## Return Value  
 A Reference object.  
  
## Remarks  
 This getReference method is specified by the getReference method in the javax.naming.Referenceable interface.  
  
 Prior to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, if SQLServerDataSource.setTrustStorePassword was called on a SQLServerDataSource object, the password would be present in the object returned by SQLServerDataSource.getReference, allowing the object to be used to make additional connections. In JDBC Driver 3.0, you will need to set the password on the object returned by SQLServerDataSource.getReference before you make connections with the object.  
  
 Also, if you set SQLServerDataSource.setTrustStorePassword before binding the data source properties, you must call SQLServerDataSource.setTrustStorePassword before getting the connection. For example,  
  
```  
ctx = new InitialContext(System.getProperties());  
  
SQLServerDataSource ds1 = (SQLServerDataSource) ctx.lookup(jndiName);  
  
ds1.setTrustStorePassword("XXXXX");  
Connection con = ds1.getConnection("user", "XXXXXX");  
  
ctx.rebind(jndiName, ds1);  
SQLServerDataSource ds2 = (SQLServerDataSource) ctx.lookup(jndiName);  
ds2.setTrustStorePassword("XXXXX");   // reset the truststore password  
con = ds2.getConnection("user", "XXXXXX");   // provide userid and password again  
```  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
