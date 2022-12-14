---
title: "Create a Credential | Microsoft Docs"
description: Learn how to create a credential in SQL Server by using SQL Server Management Studio or Transact-SQL. Find out how to work within the limitations and restrictions.
ms.custom: ""
ms.date: "08/25/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "credentials [SQL Server], creating"
  - "authentication [SQL Server], credentials"
  - "logins [SQL Server], credentials"
ms.assetid: c1e77e91-2a69-40d9-b8b3-97cffc710586
author: VanMSFT
ms.author: vanto
---
# Create a Credential
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to create a credential in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 Credentials provide a way to allow [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication users to have an identity outside of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. This is primarily used to execute code in Assemblies with EXTERNAL_ACCESS permission set. Credentials can also be used when a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication user needs access to a domain resource, such as a file location to store a backup.  
  
 A credential can be mapped to one SQL Server login, and a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login can only be mapped to one credential at a time. After a credential is created, use the **Login Properties (General Page)** to map a login to a credential.  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If there is no login mapped credential for the provider, the credential mapped to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account is used.  
  
-   A login can have multiple credentials mapped to it as long as they are used with distinctive providers. There must be only one mapped credential per provider per login. The same credential can be mapped to other logins.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER ANY CREDENTIAL permission to create or modify a credential and ALTER ANY LOGIN permission to map a login to a credential.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a credential  
  
1.  In Object Explorer, expand  the **Security** folder.  
  
2.  Right-click the **Credentials** folder and select **New Credential...**.  
  
3.  In the **New Credential** dialog box, in the **Credential Name** box, type a name for the credential.  
  
4.  In the **Identity** box, type the name of the account used for outgoing connections (when leaving the context of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]). Typically, this will be a Windows user account, but the identity can be an account of another type.  
  
     Alternately, click the ellipsis **(...)** to open the **Select User or Group** dialog box.  
  
5.  In the **Password** and **Confirm password** boxes, type the password of the account specified in the **Identity** box. If **Identity** is a Windows user account, this is the Windows password. The **Password** can be blank, if no password is required.  
  
6.  Select **Use Encryption Provider** to set the credential to be verified by an Extensible Key Management (EKM) Provider. For more information, see [Extensible Key Management &#40;EKM&#41;](../../../relational-databases/security/encryption/extensible-key-management-ekm.md)  
  
7.  Select **OK**.
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a credential  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- Creates the credential called "AlterEgo.".   
    -- The credential contains the Windows user "Mary5" and a password.  
    CREATE CREDENTIAL AlterEgo WITH IDENTITY = 'Mary5',   
        SECRET = '<EnterStrongPasswordHere>';  
    GO  
    ```  
  
 For more information, see [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../../t-sql/statements/create-credential-transact-sql.md).  
  
  
