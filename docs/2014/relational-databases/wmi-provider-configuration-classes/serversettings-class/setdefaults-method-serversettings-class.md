---
title: "SetDefaults Method (ServerSettings Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "SetDefaults Method (ServerSettings Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SetDefaults method"
ms.assetid: 76e4cfab-4b15-4da4-bb2f-8aac6f927f79
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SetDefaults Method (ServerSettings Class)
  Sets all the default values for the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with the option to overwrite existing data.  
  
## Syntax  
  
```  
  
object  
.SetDefaults(  
OverwriteAll  
)  
  
```  
  
## Parts  
 *object*  
 A [ServerSettings Class](serversettings-class.md) object that represents a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] client instance.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*OverwriteAll*|A Boolean value that specifies whether to overwrite existing values on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]: `true` to overwrite existing data, or `false` if existing data is not to be overwritten.|  
  
## Property Value/Return Value  
 A u`int32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
