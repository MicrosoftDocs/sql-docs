---
title: "Symmetric Keys on User Databases | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 3333ab5b-2518-4753-a0a8-57df5e5af74f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Symmetric Keys on User Databases
  This rule checks whether keys that have a length of less than 128 bytes do not use the RC2 or RC4 encryption algorithm.  
  
## Best Practices Recommendations  
 Use AES 128 bit or larger to create symmetric keys for data encryption. If AES is not supported by your operating system, use 3DES.  
  
## For More Information  
 [Choose an Encryption Algorithm](../security/encryption/choose-an-encryption-algorithm.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
