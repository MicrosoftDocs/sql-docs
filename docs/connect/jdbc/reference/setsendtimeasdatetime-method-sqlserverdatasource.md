---
title: "setSendTimeAsDatetime Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 705a0494-b5e2-43db-940a-1b8cec550cdb
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
