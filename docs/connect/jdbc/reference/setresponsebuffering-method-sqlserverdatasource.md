---
title: "setResponseBuffering Method (SQLServerDataSource)"
description: "setResponseBuffering Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "SQLServerDataSource.setResponseBuffering(String responseBufferingValue)"
apiname: "SQLServerDataSource.setResponseBuffering(String responseBufferingValue)"
apitype: "Assembly"
---
# setResponseBuffering Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the response buffering mode for connections created by using this [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) object.  
  
## Syntax  
  
```  
  
public void setResponseBuffering(java.lang.String value)  
```  
  
#### Parameters  
 *value*  
  
 A **String** that contains the buffering and streaming mode. The valid mode can be one of the following case-insensitive Strings: **full** or **adaptive**.  
  
## Remarks  
 The **full** value specifies reading the entire result from the server at run time.  
  
 The **adaptive** value specifies buffering the minimum possible data when necessary. The **adaptive** value is the default buffering mode.  
  
 For more information about using the response buffering mode, see [Using Adaptive Buffering](../../../connect/jdbc/using-adaptive-buffering.md).  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
