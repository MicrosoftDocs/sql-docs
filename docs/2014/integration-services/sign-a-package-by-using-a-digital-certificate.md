---
title: "Sign a Package by Using a Digital Certificate | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "digital signatures [Integration Services]"
  - "signing packages [Integration Services]"
  - "signatures [Integration Services]"
ms.assetid: 182b115e-0fe2-4717-8dff-183f9eb6e397
author: janinezhang
ms.author: janinez
manager: craigg
---
# Sign a Package by Using a Digital Certificate
  This topic describes how to sign an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package with a digital certificate. You can use a digital signature, together with other settings, to prevent a package that is not valid from loading and running.  
  
 Before you can sign an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package, you must do the following tasks:  
  
-   Create or obtain a private key to associate with the certificate, and store this private key on the local computer.  
  
-   Obtain a certificate for the purpose of code signing from a trusted certification authority. You can use one of the following methods to obtain or create a certificate:  
  
    -   Obtain a certificate from a public, commercial certification authority that issues certificates.  
  
    -   Obtain a certificate from a certificate server, that enables an organization to internally issue certificates. You have to add the root certificate that is used to sign the certificate to the **Trusted Root Certification Authorities** store. To add the root certificate, you can use the Certificates snap-in for the [!INCLUDE[msCoName](../includes/msconame-md.md)] Management Console (MMC). For more information, see the topic, "[Certificate Services](https://go.microsoft.com/fwlink/?LinkId=100755)," in the MSDN library.  
  
    -   Create your own certificate for testing purposes only. The Certificate Creation Tool (Makecert.exe) generates X.509 certificates for testing purposes. For more information, see the topic, "[Certificate Creation Tool (Makecert.exe)](https://go.microsoft.com/fwlink/?LinkId=100756)," in the MSDN Library.  
  
     For more information about certificates, see the online Help for the Certificates snap-in. For more information about how to sign digital assets, see the topic, "[Signing and Checking Code with Authenticode](https://go.microsoft.com/fwlink/?LinkId=78100)," in the MSDN Library.  
  
-   Make sure that the certificate has been enabled for code signing. To determine whether a certificate is enabled for code signing, review the properties of the certificate in the Certificates snap-in.  
  
-   Store the certificate in the Personal store.  
  
 After you have completed the previous tasks, you can use the following procedure to sign a package.  
  
### To sign a package  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project that contains the package to be signed.  
  
2.  In Solution Explorer, double-click the package to open it.  
  
3.  In [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer, on the **SSIS** menu, click **Digital Signing**.  
  
4.  In the **Digital Signing** dialog box, click **Sign**.  
  
5.  In the **Select a Certificate** dialog box, select a certificate.  
  
6.  (Optional) Click **View Certificat**e to view certificate information.  
  
7.  Click **OK** to close the **Select a Certificate** dialog box.  
  
8.  Click **OK** to close the **Digital Signing** dialog box.  
  
9. To save the updated package, click **Save Selected Items** on the **File** menu.  
  
     Although the package has been signed, you must now configure [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] to check or verify the digital signature before loading the package. For more information, see [Identify the Source of Packages with Digital Signatures](security/identify-the-source-of-packages-with-digital-signatures.md).  
  
## See Also  
 [Security Overview &#40;Integration Services&#41;](security/security-overview-integration-services.md)  
  
  
