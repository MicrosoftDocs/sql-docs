---
title: "setIntegratedSecurity Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.setIntegratedSecurity"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 4c968ee4-b041-424a-bf69-cc2c4a4f51c6
author: MightyPen
ms.author: genemi
manager: craigg
---
# setIntegratedSecurity Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets a **Boolean** value that indicates if the integratedSecurity property is enabled.  
  
## Syntax  
  
```  
  
public void setIntegratedSecurity(boolean enable)  
```  
  
#### Parameters  
 *enable*  
  
 **true** if integratedSecurity is enabled. Otherwise, **false**.  
  
## Remarks  
 Set to "**true**" to indicate that Windows credentials will be used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to authenticate the user of the application. If "**true**", the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will search the local computer credential cache for credentials that have already been provided at the computer or network logon. If "**false**", the username and password must be supplied.  
  
> [!NOTE]  
>  This property is only supported on [!INCLUDE[msCoName](../../../includes/msconame_md.md)] Windows operating systems.  
  
 For more information about using integrated authentication, see [Building the Connection URL](../../../connect/jdbc/building-the-connection-url.md).  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
