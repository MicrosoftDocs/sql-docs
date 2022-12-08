---
title: "Choose an Authentication Mode | Microsoft Docs"
description: Choose between Windows Authentication mode and mixed mode authentication for the SQL Server Database Engine at setup time.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.ins.instwizard.authenticationmode.f1"
  - "sql13.swb.passwordexpired.f1"
helpviewer_keywords: 
  - "sa account"
  - "authentication modes"
  - "trusted connection"
  - "SQL Server Installation Wizard, Authentication Mode page"
  - "choose authentication mode"
  - "authentication [SQL Server], choosing a mode"
  - "Windows authentication [SQL Server]"
  - "mixed mode authentication"
  - "mixed authentication mode"
  - "SQL authentication mode"
  - "Password Expired dialog box"
ms.assetid: ff7a6a48-3d38-4209-aa0f-7d6c0a8c64ef
author: VanMSFT
ms.author: vanto
---
# Choose an Authentication Mode
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  During setup, you must select an authentication mode for the [!INCLUDE[ssDE](../../includes/ssde-md.md)]. There are two possible modes: Windows Authentication mode and mixed mode. Windows Authentication mode enables Windows Authentication and disables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Mixed mode enables both Windows Authentication and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Windows Authentication is always available and cannot be disabled.  
  
## Configuring the Authentication Mode  
 If you select Mixed Mode Authentication during setup, you must provide and then confirm a strong password for the built-in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system administrator account named sa. The sa account connects by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
 If you select Windows Authentication during setup, Setup creates the sa account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication but it is disabled. If you later change to Mixed Mode Authentication and you want to use the sa account, you must enable the account. Any Windows or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account can be configured as a system administrator. Because the sa account is well known and often targeted by malicious users, do not enable the sa account unless your application requires it. Never set a blank or weak password for the sa account. To change from Windows Authentication mode to Mixed Mode Authentication and use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, see [Change Server Authentication Mode](../../database-engine/configure-windows/change-server-authentication-mode.md).  
  
## Connecting Through Windows Authentication  
 When a user connects through a Windows user account, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] validates the account name and password using the Windows principal token in the operating system. This means that the user identity is confirmed by Windows. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not ask for the password, and does not perform the identity validation. Windows Authentication is the default authentication mode, and is much more secure than [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Windows Authentication uses Kerberos security protocol, provides password policy enforcement with regard to complexity validation for strong passwords, provides support for account lockout, and supports password expiration. A connection made using Windows Authentication is sometimes called a trusted connection, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trusts the credentials provided by Windows.  
  
 By using Windows Authentication, Windows groups can be created at the domain level, and a login can be created on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the entire group. Managing access from at the domain level can simplify account administration.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]  
  
## Connecting Through SQL Server Authentication  
 When using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, logins are created in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that aren't based on Windows user accounts. Both the user name and the password are created by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Users connecting using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication must provide their credentials (login and password) every time that they connect. When using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, you must set strong passwords for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] accounts. For strong password guidelines, see [Strong Passwords](../../relational-databases/security/strong-passwords.md).  
  
 Three optional password policies are available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
-   User must change password at next login  
  
     Requires the user to change the password the next time that the user connects. The ability to change the password is provided by [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Third-party software developers should provide this feature if this option is used.  
  
-   Enforce password expiration  
  
     The maximum password age policy of the computer is enforced for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
-   Enforce password policy  
  
     The Windows password policies of the computer are enforced for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. This includes password length and complexity. This functionality depends on the `NetValidatePasswordPolicy` API, which is only available in [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)] and later versions.  
  
#### To determine the password policies of the local computer  
  
1.  On the **Start** menu, select **Run**.  
  
2.  In the **Run** dialog box, type **secpol.msc**, and then select **OK**.  
  
3.  In the **Local Security Settings** application, expand **Security Settings**, expand **Account Policies**, and then select **Password Policy**.  

     The password policies are described in the results pane.  
  
### Disadvantages of SQL Server Authentication  
  
-   If a user is a Windows domain user who has a login and password for Windows, they must still provide another ([!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]) login and password to connect. Keeping track of multiple names and passwords is difficult for many users. Having to provide [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credentials every time that one connects to the database can be annoying.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication cannot use Kerberos security protocol.  
  
-   Windows offers additional password policies that aren't available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
-   The encrypted [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login password, must be passed over the network at the time of the connection. Some applications that connect automatically will store the password at the client. These are additional attack points.  
  
### Advantages of SQL Server Authentication  
  
-   Allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support older applications and applications provided by third parties that require [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.  
  
-   Allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support environments with mixed operating systems, where all users aren't authenticated by a Windows domain.  
  
-   Allows users to connect from unknown or untrusted domains. For instance, an application where established customers connect with assigned [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins to receive the status of their orders.  
  
-   Allows [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to support Web-based applications where users create their own identities.  
  
-   Allows software developers to distribute their applications by using a complex permission hierarchy based on known, preset [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
    > [!NOTE]  
    >  Using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication does not limit the permissions of local administrators on the computer where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
## See Also  
 [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
  
