---
title: "Implement a Signing Policy by Setting a Registry Value | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "signing policies [Integration Services]"
ms.assetid: 64f6966f-2292-401f-acb1-2ccb5aee484a
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Implement a Signing Policy by Setting a Registry Value
  You can use an optional registry value to manage an organization's policy for loading signed or unsigned packages. If you use this registry value, you must create this registry value on each computer on which [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages will run and on which you want to enforce the policy. After the registry value has been set, [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] will check or verify the signatures before loading packages.  
  
 This procedure in this topic describes how to add the optional `BlockedSignatureStates` DWORD value to the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\SSIS registry key. The data value in `BlockedSignatureStates` determines whether a package should be blocked if it has an untrusted signature, has an invalid signature, or is unsigned. With regard to the status of signatures used to sign packages, the `BlockedSignatureStates` registry value uses the following definitions:  
  
-   A *valid signature* is one that can be read successfully.  
  
-   An *invalid signature* is one for which the decrypted checksum (the one-way hash of the package code encrypted by a private key) does not match the decrypted checksum that is calculated as part of the process of loading [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages.  
  
-   A *trusted signature* is one that is created by using a digital certificate signed by a Trusted Root Certification Authority. This setting does not require the signer to be found in the user's list of Trusted Publishers.  
  
-   An *untrusted signature* is one that cannot be verified as issued by a Trusted Root Certification Authority, or a signature that is not current.  
  
 The following table lists the valid values of the DWORD data and their associated policy.  
  
|Value|Description|  
|-----------|-----------------|  
|0|No administrative restriction.|  
|1|Block invalid signatures.<br /><br /> This setting does not block unsigned packages.|  
|2|Block invalid and untrusted signatures.<br /><br /> This setting does not block unsigned packages, but blocks self-generated signatures.|  
|3|Block invalid and untrusted signatures and unsigned packages<br /><br /> This setting also blocks self-generated signatures.|  
  
> [!NOTE]  
>  The recommended setting for `BlockedSignatureStates` is 3. This setting provides the greatest protection against unsigned packages or signatures that are either not valid or untrusted. However, the recommended setting may not be appropriate in all circumstances. For more information about signing digital assets, see the topic, "[Introduction to Code Signing](https://go.microsoft.com/fwlink/?LinkId=51414)," in the MSDN Library.  
  
### To implement a signing policy for packages  
  
1.  On the **Start** menu, click **Run**.  
  
2.  In the Run dialog box, type `Regedit`, and then click **OK**.  
  
3.  Locate the registry key, HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\SSIS.  
  
4.  Right-click **MSDTS**, point to **New**, and then click **DWORD Value**.  
  
5.  Update the name of the new value to `BlockedSignatureStates`.  
  
6.  Right-click `BlockedSignatureStates` and click **Modify**.  
  
7.  In the **Edit DWORD Value** dialog box, type the value 0, 1, 2, or 3.  
  
8.  Click **OK**.  
  
9. On the **File** menu, click **Exit**.  
  
## See Also  
 [Security Overview &#40;Integration Services&#41;](security/security-overview-integration-services.md)   
 [Identify the Source of Packages with Digital Signatures](security/identify-the-source-of-packages-with-digital-signatures.md)  
  
  
