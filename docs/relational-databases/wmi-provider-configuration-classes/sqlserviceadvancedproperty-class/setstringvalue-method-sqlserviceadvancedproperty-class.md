---
description: "SetStringValue Method (SqlServiceAdvancedProperty Class )"
title: "SetStringValue Method (SqlServiceAdvancedProperty Class )"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SetStringValue Method (SqlServiceAdvancedProperty Class )"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetStringValue method"
ms.assetid: a02d05f6-1072-4709-9ecc-e23e51c8c898
author: markingmyname
ms.author: maghan
---
# SetStringValue Method (SqlServiceAdvancedProperty Class )
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Sets the string value of a property.  
  
## Syntax  
  
```  
  
object.SetStringValue(StrValue)  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*StrValue*|A string value that specifies the value of the advanced property.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
 The property value type must be **string** to be able to set the property to a string value.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
