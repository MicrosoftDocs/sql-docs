---
title: "Credentials (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "principals [SQL Server], credentials"
  - "schemas [SQL Server], credentials"
  - "permissions [SQL Server], credentials"
  - "groups [SQL Server], credentials"
  - "ALTER ANY CREDENTIAL permission"
  - "security [SQL Server], credentials"
  - "authentication [SQL Server], credentials"
  - "users [SQL Server], credentials"
  - "credentials [SQL Server], about credentials"
  - "credentials [SQL Server]"
ms.assetid: c8df6022-e0b4-46b8-9670-3f86938d3177
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Credentials (Database Engine)
  A credential is a record that contains the authentication information (credentials) required to connect to a resource outside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This information is used internally by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Most credentials contain a Windows user name and password.  
  
 The information stored in a credential enables a user who has connected to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by way of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication to access resources outside the server instance. When the external resource is Windows, the user is authenticated as the Windows user specified in the credential. A single credential can be mapped to multiple [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins. However, a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login can be mapped to only one credential.  
  
 System credentials are created automatically and are associated with specific endpoints. Names for system credentials start with two hash signs (##).  
  
 For more information about credentials, see the [sys.credentials](/sql/relational-databases/system-catalog-views/sys-credentials-transact-sql) catalog view.  
  
## Related Content  
 [Create a Credential](../authentication-access/create-a-credential.md) [CREATE CREDENTIAL &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-credential-transact-sql)  
  
 [Securing SQL Server](../securing-sql-server.md)  
  
  
