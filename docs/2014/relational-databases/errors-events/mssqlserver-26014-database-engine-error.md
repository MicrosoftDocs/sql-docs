---
title: "MSSQLSERVER_26014 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "26014 (Database Engine error)"
ms.assetid: e2b0dfc7-0681-4e5d-8875-1d5f63534086
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_26014
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|26014|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SNI_SSL_USER_CERT_FAILURE|  
|Message Text|Unable to load user-specified certificate [Cert Hash(sha1) "%hs"]. The server will not accept a connection. You should verify that the certificate is correctly installed. See "Configuring Certificate for Use by SSL" in Books Online.|  
  
## Explanation  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] attempted to load the certificate named in the message but the operation failed. This problem must be resolved before [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use this certificate.  
  
 Possible causes of the error include:  
  
-   The certificate could have been moved or deleted.  
  
-   If the login used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has changed, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may not have permission to access the certificate.  
  
-   The certificate may have expired.  
  
## User Action  
 Make sure the certificate named in the message exists on the system, is accessible, and it is valid for use.  
  
  
