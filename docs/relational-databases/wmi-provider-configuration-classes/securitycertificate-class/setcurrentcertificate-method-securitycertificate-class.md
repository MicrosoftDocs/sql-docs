---
title: "SetCurrentCertificate Method (SecurityCertificate Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "SetCurrentCertificate Method (SecurityCertificate Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetCurrentCertificate method"
ms.assetid: 04b1a76a-932d-4824-8506-e346afe7554e
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# SetCurrentCertificate Method (SecurityCertificate Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Sets the current security certificate.  
  
## Syntax  
  
```  
  
object.SetCurrentCertificate(SHA , SQLInstance)  
```  
  
## Parts  
 *object*  
 A [SecurityCertificate Class](../../../relational-databases/wmi-provider-configuration-classes/securitycertificate-class/securitycertificate-class.md) object that represents a security certificate.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*SHA*|A string value that specifies the secure hash algorithm (SHA) thumbprint for the required security certificate.|  
|*SQLInstance*|A string value that specifies the instance for which the certificate is required.|  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
