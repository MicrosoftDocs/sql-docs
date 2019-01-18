---
title: "Server Properties (Security Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.serverproperties.security.f1"
ms.assetid: b8a131c7-e7bd-4203-bf26-234f1ebfe622
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Server Properties - Security Page
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this page to view or modify your server security options.  
  
## Server Authentication  
 **Windows Authentication mode**  
 Uses Windows Authentication to validate attempted connections. If the **sa** password is blank when the security mode is being changed, the user is prompted to enter an **sa** password.  
  
> [!IMPORTANT]  
>  Windows Authentication is much more secure than SQL Server Authentication. When possible, you should use Windows Authentication.  
  
 **SQL Server and Windows Authentication mode**  
 Uses mixed mode authentication to verify attempted connections, for backward compatibility with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the **sa** password is blank when the security mode is being changed, the user is prompted to enter an **sa** password.  
  
> [!NOTE]  
>  Changing the security configuration requires a restart of the service. When changing the Server Authentication to SQL Server and Windows Authentication mode the SA account is not automatically enabled. To use the SA account, execute [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) with the ENABLE option.  
  
## Login Auditing  
 **None**  
 Turns off login auditing.  
  
 **Failed logins only**  
 Audits unsuccessful logins only.  
  
 **Successful logins only**  
 Audits successful logins only.  
  
 **Both failed and successful logins**  
 Audits all login attempts.  
  
> [!NOTE]  
>  Changing the audit level requires restarting the service.  
  
## Server Proxy Account  
 **Enable server proxy account**  
 Enables an account for use by **xp_cmdshell**. Proxy accounts allow for the impersonation of logins, server roles, and database roles when an operating system command is being executed.  
  
> [!CAUTION]  
>  The login used by the server proxy account should have the least privileges required to perform the intended work. Excessive privileges for the proxy account could be used by a malicious user to compromise your system security.  
  
 **Proxy account**  
 Specify the proxy account used.  
  
 **Password**  
 Specify the password for the proxy account.  
  
## Options  
 **Enable C2 audit tracing**  
 Audits all attempts to access statements and objects and records them to a file in the \MSSQL\Data directory for default instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or the \MSSQL$*instancename*\Data directory for named instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [c2 audit mode Server Configuration Option](../../database-engine/configure-windows/c2-audit-mode-server-configuration-option.md).  
  
 **Cross database ownership chaining**  
 Select to allow the database to be the source or target of a cross-database ownership chain. For more information, see [cross db ownership chaining Server Configuration Option](../../database-engine/configure-windows/cross-db-ownership-chaining-server-configuration-option.md).  
  
## See Also  
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)  
  
  
