---
title: "SetNumValue Method (SqlServiceAdvancedProperty)"
description: "SetNumValue Method (SqlServiceAdvancedProperty Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "SetNumValue method"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SetNumValue Method (SqlServiceAdvancedProperty Class)"
apitype: "MOFDef"
---
# SetNumValue Method (SqlServiceAdvancedProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets the numeric value of a property.  
  
## Syntax  
  
```  
  
object.SetNumValue(NumValue)  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*NumValue*|A **uint32** value that specifies the value of the advanced property.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
 The property value type must be numeric to be able to set the property to a numeric value.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
