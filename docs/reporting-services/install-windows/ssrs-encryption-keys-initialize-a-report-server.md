---
title: "Initialize a Report Server (SSRS Configuration Manager) | Microsoft Docs"
ms.date: 05/31/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"

ms.topic: conceptual
helpviewer_keywords: 
  - "report servers [Reporting Services], initializing"
  - "initialization process [Reporting Services]"
  - "checking report server initializations"
  - "scale-out deployments [Reporting Services]"
  - "initializing report servers [Reporting Services]"
  - "verifying report server initializations"
ms.assetid: 861d4ec4-1085-412c-9a82-68869a77bd55
author: markingmyname
ms.author: maghan
---
# SSRS Encryption Keys - Initialize a Report Server
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], an initialized server is one that can encrypt and decrypt data in a report server database. Initialization is a requirement for report server operation. Initialization occurs when the Report Server service is started for the first time. It also occurs when you join the report server to the existing deployment, or when you manually recreate the keys as part of the recovery process. For more information about how and why encryption keys are used, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md) and [Store Encrypted Report Server Data &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md).  
  
 Encryption keys are based partly on the profile information of the Report Server service. If you change the user identity used to run the Report Server service, you must update the keys accordingly. If you are using the Reporting Services Configuration tool to change the identity, this step is handled for you automatically.  
  
 If initialization fails for some reason, the report server returns an **RSReportServerNotActivated** error in response to user and service requests. In this case, you may need to troubleshoot the system or server configuration. For more information, see [SSRS: Troubleshoot Issues and Errors with Reporting Services](https://social.technet.microsoft.com/wiki/contents/articles/1633.aspx) (https://social.technet.microsoft.com/wiki/contents/articles/1633.aspx) in Technet Wiki.  
  
## Overview of the Initialization Process  
 The initialization process creates and stores a symmetric key used for encryption. The symmetric key is created by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Cryptographic Services and subsequently used by the Report Server service to encrypt and decrypt data. The symmetric key is itself encrypted with an asymmetric key.  
  
 The following steps describe the initialization process:  
  
1.  At initial start up, the Report Server service reads the RSReportServer.config file to get the installation identifier and database connection information.  
  
2.  The Report Server service requests a public key from Cryptographic Services. Windows creates a private and public key and sends the public key to the Report Server service.  
  
3.  The Report Server service connects to the report server database and stores the installation identifier and public key values.  
  
4.  The Report Server service calls into Cryptographic Services again, this time to request a symmetric key. Windows creates the symmetric key.  
  
5.  The Report Server service connects to the report server database again, and adds the symmetric key to the public key and installation identifier values that were stored in step 3. Before storing it, the Report Server service uses its public key to encrypt the symmetric key. Once the symmetric key is stored, the report server is considered initialized and available to use.  
  
## Initializing a Report Server for Scale-out Deployment  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a scale-out deployment model that shares a single report server database among multiple report server instances. To join a scale-out deployment, a report server must create and store its copy of the symmetric key in the shared database. Although a single symmetric key is used by servers that use the database, each report server has its copy of the key. Each copy varies in that it is uniquely encrypted using the public key it owns.  
  
 The first set of steps for initializing a report server for scale-out deployment are identical to the first three steps that describe initialization for a single server and database combination.  
  
 The initialization process for a scale out deployment differs in how the report server gets the symmetric key. When the first server is initialized, it gets the symmetric key from Windows. When the second server is initialized during configuration for scale-out deployment, it gets the symmetric key from the Report Server service that is already initialized. The first report server instance uses the public key of the second instance to create an encrypted copy of the symmetric key for the second report server instance. The symmetric key is never exposed as plain text at any point in this process.  
  
## How to Initialize a Report Server  
  
-   To initialize a report server, use the Reporting Services Configuration tool. Initialization occurs automatically when you create and configure the report server database. For more information, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
-   To initialize a report server for scale-out deployment, you can use the Initialization page in the Reporting Services Configuration tool or the **RSKeymgmt** utility. To follow step-by-step instructions, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  
  
> [!NOTE]  
>  **RSKeymgmt** is a console application that you run from a command line on a computer that hosts a report server instance that is already part of a scale-out deployment. When you run the utility, you specify arguments to select a remote report server instance that you want to initialize.  
  
 A report server will be initialized only if there is a match between the installation identifier and the public key. If the match succeeds, a symmetric key is created that permits reversible encryption. If the match fails, the report server is disabled, in which case you may be required to apply a backup key or delete the encrypted data if a backup key is unavailable or not valid. For more information about encryption keys used by a report server, see [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md).  
  
> [!NOTE]  
>  You can also use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Windows Management Instrumentation (WMI) provider to initialize a report server programmatically. For more information, see [Access the Reporting Services WMI Provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## How to Confirm a Report Server Initialization  
 To confirm report server initialization, ping the Report Server Web service by typing **https://\<servername>/reportserver** in the command window. If the **RSReportServerNotActivated** error occurs, the initialization failed.  
  
## See Also
[Configure and Manage Encryption Keys (SSRS Configuration Manager)](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)
  
  
