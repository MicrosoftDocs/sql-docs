---
title: "GetCurrentCertificate Method (SecurityCertificate Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "GetCurrentCertificate Method (SecurityCertificate Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "GetCurrentCertificate method"
ms.assetid: 987a2671-1801-45c4-93e6-29f883c58720
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# GetCurrentCertificate Method (SecurityCertificate Class)
  Gets the current security certificate.  
  
## Syntax  
  
```  
  
object  
.GetCurrentCertificate(  
SHA , SQLInstance  
)  
  
```  
  
## Parts  
 *object*  
 A [SecurityCertificate Class](securitycertificate-class.md) object that represents a security certificate.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string value (output parameter) that specifies the current security certificate SHA thumbprint after the method completes.|  
|*SQLInstance*|A string value that specifies the instance for which the certificate is required.|  
  
## Property Value/Return Value  
 A `uint32` value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
