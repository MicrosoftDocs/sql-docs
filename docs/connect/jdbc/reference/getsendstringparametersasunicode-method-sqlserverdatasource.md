---
title: "getSendStringParametersAsUnicode Method (SQLServerDataSource)"
description: "getSendStringParametersAsUnicode Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.getSendStringParametersAsUnicode"
apitype: "Assembly"
---
# getSendStringParametersAsUnicode Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a **boolean** value that indicates if sending string parameters to the server in UNICODE format is enabled.  
  
## Syntax  
  
```  
  
public boolean getSendStringParametersAsUnicode()  
```  
  
## Return Value  
 **true** if string parameters are sent to the server in UNICODE format. Otherwise, **false**.  
  
## Remarks  
 If the sendStringParametersAsUnicode property is set to **true**, which is the default value, string parameters are sent to the server in UNICODE format. If sendStringParametersAsUnicode is set to **false**, string parameters are sent to the server in an ASCII/MBCS format, not in UNICODE. If sendStringParametersAsUnicode is not set, getSendStringParametersAsUnicode returns the default value of **true**.  
  
 For more information about the sendStringParametersAsUnicode connection property, see [Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md).  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
