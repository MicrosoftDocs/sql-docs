---
title: "Security Overview (Replication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "authorization [SQL Server replication]"
  - "cryptography [SQL Server replication]"
  - "encryption [SQL Server replication]"
  - "security [SQL Server replication], about security"
  - "authentication [SQL Server replication]"
ms.assetid: 27828fe4-3b54-4c33-886e-08e8279e34b5
caps.latest.revision: 45
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Security Overview (Replication)
  Fundamentally, how to help secure your replication environment is a matter of understanding the authentication and authorization options, understanding appropriate uses of replication filtering features, and learning specific measures for how to help secure each piece of the replication environment. The replication environment includes the Distributor, Publisher, Subscribers, and the snapshot folder. This chapter addresses replication security, but replication security is built on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] security and Windows security. Therefore, you should understand this foundation and the specifics of replication security. For more information about security, see [Security Considerations for a SQL Server Installation](../../../sql-server/install/security-considerations-for-a-sql-server-installation.md). For more information about security considerations for Oracle publishing, see the section "Replication Security Model" in the topic [Design Considerations and Limitations for Oracle Publishers](../../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md).  
  
## In This Section  
 [Threat and Vulnerability Mitigation &#40;Replication&#41;](../../../relational-databases/replication/security/threat-and-vulnerability-mitigation-replication.md)  
 Discusses potential threats to a replication topology and describes ways to reduce those threats.  
  
 [Identity and Access Control &#40;Replication&#41;](../../../relational-databases/replication/security/identity-and-access-control-replication.md)  
 Describes how to use authentication, authorization, and filtering to help secure a replication topology.  
  
 [Secure Development &#40;Replication&#41;](../../../relational-databases/replication/security/secure-development-replication.md)  
 Describes replication security behavior, replication security best practices, and least permissions for replication.  
  
 [Secure Deployment &#40;Replication&#41;](../../../relational-databases/replication/security/secure-deployment-replication.md)  
 Describes how to better secure all components of a replication topology  
  
## See Also  
 [Security and Protection &#40;Replication&#41;](../../../relational-databases/replication/security/security-and-protection-replication.md)  
  
  