---
title: "SQL Server Native Client Configuration Properties (Flags Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: 59af121d-c8b9-4faa-91a1-b664f2c9b441
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Native Client Configuration Properties (Flags Tab)
  [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients on this machine, communicate with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] servers using the protocols provided in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client library file. This page provides configures the client computer to request an encrypted connection using Secure Sockets Layer (SSL). If an encrypted connection cannot be established, the connection will fail.  
  
 The login process is always encrypted. The options below apply only to encrypting data. For more information about how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] encrypts communication and for instructions on how to configure the client to trust the root authority of the server certificate, see "Encrypting Connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]" and "How to: Enable Encrypted Connections to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager)" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Options  
 **Force protocol encryption**  
 Request a connection using SSL.  
  
 **Trust Server Certificate**  
 When set to **No**, the client process attempts to validate the server certificate. The client and server must have each have a certificate issues from a public certification authority. If the certificate is not present on the client computer, or if the validation of the certificate fails, the connection is terminated.  
  
 When set to **Yes**, the client does not validate the server certificate, thereby enabling the use of a self-signed certificate.  
  
 **Trust Server Certificate** is only available if **Force protocol encryption** is set to **Yes**.  
  
  
