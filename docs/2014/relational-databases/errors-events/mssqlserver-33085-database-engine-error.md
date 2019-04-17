---
title: "MSSQLSERVER_33085 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "33085 (Database Engine error)"
ms.assetid: c27b8d1d-668a-4ba8-8b61-25a5ebbc5485
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_33085
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|33085|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_CRYPTOPROVE_METHOD_CANNOT_FOUND|  
|Message Text|One or more methods cannot be found in cryptographic provider library '%.*ls'.|  
  
## Explanation  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was unable to use the cryptographic provider listed in the error message. The cryptographic provider did not support a required method. The state of the error indicates which method was not found.  
  
|State|Description|  
|-----------|-----------------|  
|1|SqlCryptInitializeProvider|  
|2|SqlCryptFreeProvider|  
|3|SqlCryptOpenSession|  
|4|SqlCryptCloseSession|  
|5|SqlCryptGetProviderInfo|  
|6|SqlCryptGetNextAlgorithmId|  
|7|SqlCryptGetAlgorithmInfo|  
|8|SqlCryptCreateKey|  
|9|SqlCryptDropKey|  
|10|SqlCryptGetNextKeyId|  
|11|SqlCryptGetKeyInfoByKeyId|  
|12|SqlCryptGetKeyInfoByThumb|  
|13|SqlCryptGetKeyInfoByName|  
|14|SqlCryptExportKey|  
|15|SqlCryptImportKey|  
|16|SqlCryptEncrypt|  
|17|SqlCryptDecrypt|  
  
## User Action  
 Contact the cryptographic provider for more information.  
  
  
