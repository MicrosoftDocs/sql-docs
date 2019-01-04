---
title: "SetBoolValue Method (SqlServiceAdvancedProperty Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
api_name: 
  - "SetBoolValue Method (SqlServiceAdvancedProperty Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetBoolValue method"
ms.assetid: 5252b439-fce5-446a-8e57-99e3054bee69
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# SetBoolValue Method (SqlServiceAdvancedProperty Class)
  Sets the Boolean value of a property.  
  
## Syntax  
  
```  
  
object  
.SetBoolValue [= value]  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](../wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*BoolValue*|A Boolean value that specifies the value of the advanced property.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
 The property value type must be Boolean to set the property to a Boolean value.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
