---
title: "SetValue Method (ServerSettingsGeneralFlag Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetValue Method (ServerSettingsGeneralFlag Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetValue method"
ms.assetid: a889feac-c0e0-4635-b506-843863d86967
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetValue Method (ServerSettingsGeneralFlag Class)
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
 A [ServerSettingsGeneralFlag Class](serversettingsgeneralflag-class.md) object that represents a general flag for the server settings.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*Value*|A Boolean value that specifies the value of the flag.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
