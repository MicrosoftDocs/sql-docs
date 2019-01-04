---
title: "SetStringValue Method (SqlServiceAdvancedProperty Class ) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetStringValue Method (SqlServiceAdvancedProperty Class )"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetStringValue method"
ms.assetid: a02d05f6-1072-4709-9ecc-e23e51c8c898
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetStringValue Method (SqlServiceAdvancedProperty Class )
  Sets the string value of a property.  
  
## Syntax  
  
```  
  
object  
.SetStringValue(  
StrValue  
)  
  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*StrValue*|A string value that specifies the value of the advanced property.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
 The property value type must be `string` to be able to set the property to a string value.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
