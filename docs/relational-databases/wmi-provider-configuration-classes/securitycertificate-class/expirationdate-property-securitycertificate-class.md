---
title: "ExpirationDate Property (SecurityCertificate Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "ExpirationDate Property (SecurityCertificate Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ExpirationDate property"
ms.assetid: b7fbb9e9-85c1-475b-8e49-7c82fb3740aa
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# ExpirationDate Property (SecurityCertificate Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the date when the security certificate ceases to be in effect.  
  
## Syntax  
  
```  
  
object.ExpirationDate [= value]  
```  
  
## Parts  
 *object*  
 A [SecurityCertificate Class](../../../relational-databases/wmi-provider-configuration-classes/securitycertificate-class/securitycertificate-class.md) object that represents a security certificate.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the expiration date for the security certificate.  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
