---
title: "SetValue Method (ClientSettingsGeneralFlag Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "SetValue Method (ClientSettingsGeneralFlag Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetValue method"
ms.assetid: 34443689-a0e0-4668-a811-17532c6fd271
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SetValue Method (ClientSettingsGeneralFlag Class)
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
 [Configure Client Protocols](http://technet.microsoft.com/library/ms181035.aspx)  
  
  