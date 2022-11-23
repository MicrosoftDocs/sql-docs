---
description: "GetCurrentCertificate Method (SInstance Class)"
title: "GetCurrentCertificate Method (SInstance)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "GetCurrentCertificate Method (SInstance Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "GetCurrentCertificate method"
ms.assetid: 9d2b72df-cb21-414a-abef-917f13d4de62
author: markingmyname
ms.author: maghan
---
# GetCurrentCertificate Method (SInstance Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the current security certificate.  
  
## Syntax  
  
```  
  
object.GetCurrentCertificate(SHA)  
```  
  
## Parts  
 *object*  
 An [SInstance Class](../../../relational-databases/wmi-provider-configuration-classes/sinstance-class/sinstance-class.md) object that represents the server settings on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string object value (output parameter) that specifies the current security certificate after the method completes.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
