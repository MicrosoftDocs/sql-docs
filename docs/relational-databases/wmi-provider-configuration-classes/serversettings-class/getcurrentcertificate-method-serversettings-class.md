---
title: "GetCurrentCertificate Method (ServerSettings Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "GetCurrentCertificate Method (ServerSettings Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "GetCurrentCertificate method"
ms.assetid: 450e33c6-91d4-420f-ab7c-1905111f5658
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# GetCurrentCertificate Method (ServerSettings Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the current security certificate.  
  
## Syntax  
  
```  
  
object.GetCurrentCertificate(SHA)  
```  
  
## Parts  
 *object*  
 A **ServerSettings** object that represents the server settings on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string object value (output parameter) that specifies the current security certificate after the method completes.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
