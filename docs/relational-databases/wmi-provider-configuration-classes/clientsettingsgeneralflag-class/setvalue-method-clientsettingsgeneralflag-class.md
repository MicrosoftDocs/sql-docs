---
title: "SetValue Method (ClientSettingsGeneralFlag)"
description: "SetValue Method (ClientSettingsGeneralFlag Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetValue method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetValue Method (ClientSettingsGeneralFlag Class)"
apitype: "MOFDef"
---
# SetValue Method (ClientSettingsGeneralFlag Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets all the values of the referenced flag.  
  
## Syntax  
  
```  
  
object.SetValue(Value)  
```  
  
## Parts  
 *object*  
 A [ClientSettingsGeneralFlag Class](../../../relational-databases/wmi-provider-configuration-classes/clientsettingsgeneralflag-class/clientsettingsgeneralflag-class.md) object that represents a general flag for the server settings.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*Value*|A Boolean value that specifies the value of the flag.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  
