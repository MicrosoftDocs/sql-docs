---
description: "Identify the Source of Packages with Digital Signatures"
title: "Identify the Source of Packages with Digital Signatures | Microsoft Docs"
ms.custom: security
ms.date: "08/24/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.digitalsigning.f1"
helpviewer_keywords: 
  - "signing packages [Integration Services]"
  - "certificates [Integration Services]"
  - "packages [Integration Services], security"
  - "security [Integration Services], certificates"
  - "signing policies [Integration Services]"
ms.assetid: a433fbef-1853-4740-9d5e-8a32bc4ffbb2
author: chugugrace
ms.author: chugu
---
# Identify the Source of Packages with Digital Signatures

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package can be signed with a digital certificate to identify its source. After a package has been signed with a digital certificate, you can have [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] check the digital signature before loading the package. To have [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] check the signature, you set an option in either [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or in the **dtexec** utility (dtexec.exe), or set an optional registry value.  
  
## Sign a package with a digital certificate  
 Before you can sign a package with a digital certificate, you first have to obtain or create the certificate. After you have the certificate, you can then use this certificate to sign the package. For more information about how to obtain a certificate and sign a package with that certificate, see [Sign a Package by Using a Digital Certificate](#cert).  
  
## Set an option to check the package signature  
 Both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and the **dtexec** utility have an option that configures [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to check the digital signature of a signed package. Whether you use [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or the **dtexec** utility depends on whether you want to check all packages or just specific ones:  
  
-   To check the digital signature of all packages before loading the packages at design time, set the **Check digital signature when loading a package** option in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. This option is a global setting for all packages in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].
  
-   To check the digital signature of an individual package, specify the **/VerifyS[igned]** option when you use the **dtexec** utility to run the package. For more information, see [dtexec Utility](../../integration-services/packages/dtexec-utility.md).  
  
## Set a Registry value to check package signature  
[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] also supports an optional registry value, **BlockedSignatureStates**, that you can use to manage an organization's policy for loading signed and unsigned packages. The registry value can prevent packages from loading if the packages are unsigned, or have invalid or untrusted signatures. For more information about how to set this registry value, see [Implement a Signing Policy by Setting a Registry Value](#registry).  
  
> [!NOTE]
> The optional **BlockedSignatureStates** registry value can specify a setting that is more restrictive than the digital signature option set in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or at the **dtexec** command line. In this situation, the more restrictive registry setting overrides the other settings.  

## <a name="registry"></a> Implement a Signing Policy by Setting a Registry Value
You can use an optional registry value to manage an organization's policy for loading signed or unsigned packages. If you use this registry value, you must create this registry value on each computer on which [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages will run and on which you want to enforce the policy. After the registry value has been set, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] will check or verify the signatures before loading packages.  
  
The procedure in this article describes how to add the optional **BlockedSignatureStates** DWORD value to the HKLM\SOFTWARE\Microsoft\Microsoft SQL Server\150\SSIS\Setup\DTSPath registry key. 
 
 > [!NOTE]
 > A registry location under 150 represents SQL Server 2019, under 140 represents SQL Server 2017, under 130 represents SQL Server 2016, under 120 represents SQL Server 2014, and under 110 represents SQL Server 2012.
 
The data value in **BlockedSignatureStates** determines whether a package should be blocked if it has an untrusted signature, has an invalid signature, or is unsigned.

For the status of signatures used to sign packages, the **BlockedSignatureStates** registry value uses the following definitions:  
  
-   A *valid signature* is one that can be read successfully.  
  
-   An *invalid signature* is one for which the decrypted checksum (the one-way hash of the package code encrypted by a private key) does not match the decrypted checksum that is calculated as part of the process of loading [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages.  
  
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
>  The recommended setting for **BlockedSignatureStates** is 3. This setting provides the greatest protection against unsigned packages or signatures that are either not valid or untrusted. However, the recommended setting may not be appropriate in all circumstances. For more information about signing digital assets, see the topic, "[Introduction to Code Signing](/previous-versions/windows/internet-explorer/ie-developer/platform-apis/ms537361(v=vs.85))," in the MSDN Library.  
  
### To implement a signing policy for packages  
  
1.  On the **Start** menu, click **Run**.  
  
2.  In the Run dialog box, type **Regedit**, and then click **OK**.  
  
3.  Locate the registry key, HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\100\SSIS.  
  
4.  Right-click **MSDTS**, point to **New**, and then click **DWORD Value**.  
  
5.  Update the name of the new value to **BlockedSignatureStates**.  
  
6.  Right-click **BlockedSignatureStates** and click **Modify**.  
  
7.  In the **Edit DWORD Value** dialog box, type the value 0, 1, 2, or 3.  
  
8.  Click **OK**.  
  
9. On the **File** menu, click **Exit**.    

## <a name="cert"></a> Sign a Package by Using a Digital Certificate
  This topic describes how to sign an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package with a digital certificate. You can use a digital signature, together with other settings, to prevent a package that is not valid from loading and running.  
  
 Before you can sign an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package, you must do the following tasks:  
  
-   Create or obtain a private key to associate with the certificate, and store this private key on the local computer.  
  
-   Obtain a certificate for the purpose of code signing from a trusted certification authority. You can use one of the following methods to obtain or create a certificate:  
  
    -   Obtain a certificate from a public, commercial certification authority that issues certificates.  
  
    -   Obtain a certificate from a certificate server, that enables an organization to internally issue certificates. You have to add the root certificate that is used to sign the certificate to the **Trusted Root Certification Authorities** store. To add the root certificate, you can use the Certificates snap-in for the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Management Console (MMC). For more information, see the topic, "[Certificate Services](/windows/win32/seccrypto/certificate-services)," in the MSDN library.  
  
    -   Create your own certificate for testing purposes only. The Certificate Creation Tool (Makecert.exe) generates X.509 certificates for testing purposes. For more information, see the topic, "[Certificate Creation Tool (Makecert.exe)](/previous-versions/dotnet/netframework-2.0/bfsktky3(v=vs.80))," in the MSDN Library.  
  
     For more information about certificates, see the online Help for the Certificates snap-in. For more information about how to sign digital assets, see the topic, "[Signing and Checking Code with Authenticode](/previous-versions/windows/internet-explorer/ie-developer/platform-apis/ms537364(v=vs.85))," in the MSDN Library.  
  
-   Make sure that the certificate has been enabled for code signing. To determine whether a certificate is enabled for code signing, review the properties of the certificate in the Certificates snap-in.  
  
-   Store the certificate in the Personal store.  
  
 After you have completed the previous tasks, you can use the following procedure to sign a package.  
  
### To sign a package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package to be signed.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, on the **SSIS** menu, click **Digital Signing**.  
  
4.  In the **Digital Signing** dialog box, click **Sign**.  
  
5.  In the **Select a Certificate** dialog box, select a certificate.  
  
6.  (Optional) Click **View Certificat**e to view certificate information.  
  
7.  Click **OK** to close the **Select a Certificate** dialog box.  
  
8.  Click **OK** to close the **Digital Signing** dialog box.  
  
9. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
     Although the package has been signed, you must now configure [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to check or verify the digital signature before loading the package.  

## <a name="signing_dialog"></a> Digital Signing Dialog Box UI Reference
  Use the **Digital Signing** dialog box to sign a package with a digital signature or to remove the signature. The **Digital Signing** dialog box is available from the **Digital Signing** option on the **SSIS** menu in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)].  
  
 For more information, see [Sign a Package by Using a Digital Certificate](#cert).  
  
### Options  
 **Sign**  
 Click to open the **Select Certificate** dialog box, and select the certificate to use.  
  
 **Remove**  
 Click to remove the digital signature.  

## See also  
 [Integration Services \(SSIS\) Packages](../../integration-services/integration-services-ssis-packages.md)   
 [Security Overview \(Integration Services\)](../../integration-services/security/security-overview-integration-services.md)  
  
