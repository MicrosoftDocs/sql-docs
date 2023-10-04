---
title: "SetServiceAccount Method (SqlService)"
description: "SetServiceAccount Method (SqlService Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetServiceAccount method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetServiceAccount Method (SqlService Class)"
apitype: "MOFDef"
---
# SetServiceAccount Method (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Attempts to change the user name and password that the service instance runs under.  
  
## Syntax  
  
```  
  
object.SetServiceAccount(ServiceStartName , ServiceStartPassword)  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
#### Parameters  
 *ServiceStartName*  
 A string value that specifies the account name that the service runs under. Depending on the service type, the account name might be in the form of DomainName\Username. When it runs, the service process will be logged using one of two forms:  
  
-   If the account belongs to the built-in domain, \Username can be specified.  
  
-   If NULL is specified, the service will be logged on as the **LocalSystem** account.  
  
 For kernel or system-level drivers, *StartName* contains the driver object name, either \FileSystem\Rdr or \Driver\Xns, which the I/O system uses to load the device driver. If NULL is specified, the driver runs with a default object name created by the I/O system based on the service name, for example, DWDOM\Admin.  
  
 *ServiceStartPassword*  
 A string value that specifies the password for the account name in the *StartName* parameter. Specify NULL if you are not changing the password. Specify an empty string if the service has no password.  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified or 1 if the request is not supported. Any other number indicates an error.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
