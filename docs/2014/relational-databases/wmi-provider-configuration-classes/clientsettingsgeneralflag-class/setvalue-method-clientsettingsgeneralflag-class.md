---
title: "SetValue Method (ClientSettingsGeneralFlag Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetValue Method (ClientSettingsGeneralFlag Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetValue method"
ms.assetid: 34443689-a0e0-4668-a811-17532c6fd271
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetValue Method (ClientSettingsGeneralFlag Class)
  Sets all the values of the referenced flag.  
  
## Syntax  
  
```  
  
object  
.SetValue(  
Value  
)  
  
```  
  
## Parts  
 *object*  
 A [ClientSettingsGeneralFlag Class](clientsettingsgeneralflag-class.md) object that represents a general flag for the server settings.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*Value*|A Boolean value that specifies the value of the flag.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)  
  
  
