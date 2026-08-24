---
title: "setSendTimeAsDatetime Method (SQLServerDataSource)"
description: "setSendTimeAsDatetime Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setSendTimeAsDatetime Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  This method was added in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0.  
  
 Modifies the setting of the **sendTimeAsDatetime** connection property.  
  
## Syntax  
  
```  
  
public void setSendTimeAsDatetime(boolean sendTimeAsDateTime)  
```  
  
#### Parameters  
 *sendTimeAsDateTime*  
  
 A Boolean value. When true, causes java.sql.Time values to be sent to the server as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **datetime** types. When false, causes java.sql.Time values to be sent to the server as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **time** types.  
  
## Remarks  
 [SQLServerDataSource.getSendTimeAsDatetime](../../../connect/jdbc/reference/getsendtimeasdatetime-method-sqlserverdatasource.md) returns the setting of the **sendTimeAsDatetime** connection property.  
  
 For more information on the **sendTimeAsDatetime** connection property, see [Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md).  
  
 For more information, see [Configuring How java.sql.Time Values are Sent to the Server](../../../connect/jdbc/configuring-how-java-sql-time-values-are-sent-to-the-server.md).  
  
## Related content

- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
