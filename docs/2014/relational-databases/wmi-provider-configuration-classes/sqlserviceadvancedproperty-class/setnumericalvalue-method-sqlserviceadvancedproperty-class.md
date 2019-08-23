---
title: "SetNumericalValue Method (SqlServiceAdvancedProperty Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetNumericalValue Method (SqlServiceAdvancedProperty Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetNumericalValue method"
ms.assetid: 950ed1e8-0538-4db4-807c-a2c36f43cf6b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetNumericalValue Method (SqlServiceAdvancedProperty Class)
  Sets the numeric value of a property.  
  
## Syntax  
  
```  
  
object  
.SetNumericalValue(  
NumValue  
)  
  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*NumValue*|A `uint32` value that specifies the value of the advanced property.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
 The property value type must be numeric to be able to set the property to a numeric value.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
