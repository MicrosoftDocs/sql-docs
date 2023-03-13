---
title: "SetDefaults Method (ServerSettings)"
description: "SetDefaults Method (ServerSettings Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetDefaults method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetDefaults Method (ServerSettings Class)"
apitype: "MOFDef"
---
# SetDefaults Method (ServerSettings Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets all the default values for the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the option to overwrite existing data.  
  
## Syntax  
  
```  
  
object.SetDefaults(OverwriteAll)  
```  
  
## Parts  
 *object*  
 A [ServerSettings Class](../../../relational-databases/wmi-provider-configuration-classes/serversettings-class/serversettings-class.md) object that represents a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client instance.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*OverwriteAll*|A Boolean value that specifies whether to overwrite existing values on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: **true** to overwrite existing data, or **false** if existing data is not to be overwritten.|  
  
## Property Value/Return Value  
 A u**int32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
