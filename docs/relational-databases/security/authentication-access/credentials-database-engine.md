---
title: "Credentials (Database Engine) | Microsoft Docs"
description: Learn about credentials in SQL Server. Get acquainted with the authentication information required to connect to a resource outside SQL Server.
ms.custom: ""
ms.date: "06/27/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
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
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Credentials (Database Engine)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  A credential is a record that contains the authentication information (credentials) required to connect to a resource outside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This information is used internally by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Most credentials contain a Windows user name and password.  
  
 The information stored in a credential enables a user who has connected to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by way of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication to access resources outside the server instance. When the external resource is Windows, the user is authenticated as the Windows user specified in the credential. A single credential can only be mapped to a single [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login. And a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login can be mapped to only one credential.  
  
 For credentials that are stored in the master database and can be used throughout the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-credential-transact-sql.md). For credentials used by a specific database, and portable with that database, see [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-database-scoped-credential-transact-sql.md).  
  
 System credentials are created automatically and are associated with specific endpoints. Names for system credentials start with two hash signs (##).  
  
 For more information about credentials, see the [sys.credentials](../../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md) and [sys.database_scoped_credentials](../../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) catalog views.  
  
## Related Content  
 [Create a Credential](../../../relational-databases/security/authentication-access/create-a-credential.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-credential-transact-sql.md)   
 [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-database-scoped-credential-transact-sql.md)  
 [Securing SQL Server](../../../relational-databases/security/securing-sql-server.md)  
  
  
