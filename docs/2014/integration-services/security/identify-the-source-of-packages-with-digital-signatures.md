---
title: "Identify the Source of Packages with Digital Signatures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "signing packages [Integration Services]"
  - "certificates [Integration Services]"
  - "packages [Integration Services], security"
  - "security [Integration Services], certificates"
  - "signing policies [Integration Services]"
ms.assetid: a433fbef-1853-4740-9d5e-8a32bc4ffbb2
author: janinezhang
ms.author: janinez
manager: craigg
---
# Identify the Source of Packages with Digital Signatures
  An [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package can be signed with a digital certificate to identify its source. After a package has been signed with a digital certificate, you can have [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] check the digital signature before loading the package. To have [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] check the signature, you set an option in either [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or in the **dtexec** utility (dtexec.exe), or set an optional registry value.  
  
## Signing a Package with a Digital Certificate  
 Before you can sign a package with a digital certificate, you first have to obtain or create the certificate. After you have the certificate, you can then use this certificate to sign the package. For more information about how to obtain a certificate and sign a package with that certificate, see [Sign a Package by Using a Digital Certificate](../sign-a-package-by-using-a-digital-certificate.md).  
  
## Setting an Option to Check the Package Signature  
 Both [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] and the **dtexec** utility have an option that configures [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] to check the digital signature of a signed package. Whether you use [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or the **dtexec** utility depends on whether you want to check all packages or just specific ones:  
  
-   To check the digital signature of all packages before loading the packages at design time, set the **Check digital signature when loading a package** option in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. This option is a global setting for all packages in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. For more information, see [General Page](../general-page-of-integration-services-designers-options.md).  
  
-   To check the digital signature of an individual package, specify the `/VerifyS[igned]` option when you use the **dtexec** utility to run the package. For more information, see [dtexec Utility](../packages/dtexec-utility.md).  
  
## Setting a Registry Value to Check the Package Signature  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] also supports an optional registry value, **BlockedSignatureStates**, that you can use to manage an organization's policy for loading signed and unsigned packages. The registry value can prevent packages from loading if the packages are unsigned, or have invalid or untrusted signatures. For more information about how to set this registry value, see [Implement a Signing Policy by Setting a Registry Value](../implement-a-signing-policy-by-setting-a-registry-value.md).  
  
> [!NOTE]  
>  The optional **BlockedSignatureStates** registry value can specify a setting that is more restrictive than the digital signature option set in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] or at the **dtexec** command line. In this situation, the more restrictive registry setting overrides the other settings.  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Packages](../integration-services-ssis-packages.md)   
 [Security Overview &#40;Integration Services&#41;](security-overview-integration-services.md)  
  
  
