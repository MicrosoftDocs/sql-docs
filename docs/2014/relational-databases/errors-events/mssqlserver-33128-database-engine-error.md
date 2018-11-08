---
title: "MSSQLSERVER_33128 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "33128 (Database Engine error)"
ms.assetid: 12c1096f-d120-439b-85f3-f794859503c9
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_33128
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|33128|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SEC_DEPRECATED_ALGO|  
|Message Text|Encryption failed. Key uses deprecated algorithm '%.*ls' which is no longer supported.|  
  
## Explanation  
 This message occurs when referencing the RC4 (or RC4_128) encryption algorithm. RC4 and RC4_128 are weak algorithms and are deprecated. Use a stronger algorithm such as one of the AES algorithms instead.  
  
 When the database compatibility level is 90 or 100, the operation succeeds, the deprecation event is raised, and the message appears only in the ring buffer.  
  
 When the database compatibility level is 110 or higher, decryption operations succeed, the deprecation event is raised, and the message appears only in the ring buffer. Encryption operations will fail, the deprecation event is raised, and the message is displayed for the user, and the message appears in the ring buffer.  
  
> [!NOTE]  
>  The ring buffer is an internal component which is not fully documented and is not intended to be used by customers. Messages from the ring buffer are useful when contacting [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Support. To view the ring buffer, query the sys.dm_os_ring_buffers dynamic management view.  
  
|State|Description|  
|-----------|-----------------|  
|1|A RC4 key is used in the built-in encryptbykey() function. Built-in function returns NULL. This message only appears in the ring buffer.|  
|2|A RC4 key is used in by the built-in decryptbykey() function. This message only appears in the ring buffer.|  
|3|A deprecated RC4 key is being encrypted by a symmetric key. Seen by users and in the ring buffer. Deprecated RC4 symmetric keys cannot be altered in compatibility level 110. Try to use non-RC4 keys for crypto operations. If necessary, set backward compatibility level to a 90 or 100.|  
|4|A non-RC4 key is being encrypted by a deprecated RC4 symmetric key. Seen by users and in the ring buffer. Modify the application to use non-RC4 symmetric keys or set backward compatibility level to 90 or 100.|  
|5|A deprecated RC4 key is being decrypted by a symmetric key. This message only appears in the ring buffer.|  
|6|A non-RC4 key is being decrypted by an RC4 symmetric key. This message only appears in the ring buffer.|  
|7|A RC4 symmetric key is being encrypted by the certificate. Seen by users and in the ring buffer.|  
|8|A RC4 symmetric key is being decrypted by the certificate. This message only appears in the ring buffer.|  
|9|A RC4 symmetric key is being encrypted by the EKM key.|  
|10|A RC4 symmetric key is being decrypted by the EKM key. This message only appears in the ring buffer.|  
  
## User Action  
 Use a stronger algorithm such as one of the AES algorithms instead. (Recommended)  
  
 Use ALTER DATABASE SET COMPATIBILITY_LEVEL to set the database to compatibility level 100. (Not recommended.)  
  
  
