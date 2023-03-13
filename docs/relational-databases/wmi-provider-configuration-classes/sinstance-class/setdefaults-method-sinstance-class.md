---
title: "SetDefaults Method (SInstance)"
description: "SetDefaults Method (SInstance Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetDefaults method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetDefaults Method (SInstance Class)"
apitype: "MOFDef"
---
# SetDefaults Method (SInstance Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets all the default values for the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the option to overwrite existing data.  
  
## Syntax  
  
```  
  
object.SetDefaults(OverwriteAll)  
```  
  
## Parts  
 *object*  
 An [SInstance Class](../../../relational-databases/wmi-provider-configuration-classes/sinstance-class/sinstance-class.md) object that represents a server instance.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*OverwriteAll*|A Boolean value that specifies whether to overwrite existing value on the instance of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client: **true** if existing data is overwritten, or **false** if existing data is not overwritten.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
