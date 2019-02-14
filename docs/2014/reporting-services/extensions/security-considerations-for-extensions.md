---
title: "Security Considerations for Extensions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "security [Reporting Services], extensions"
  - "extensions [Reporting Services], security"
  - "permissions [Reporting Services], extensions"
ms.assetid: 58cbdfeb-1105-4a7d-a3b8-b897ff95f367
author: markingmyname
ms.author: maghan
manager: kfile
---
# Security Considerations for Extensions
  Every application that targets the common language runtime (CLR) must interact with the CLR security system. When such an application runs, it is automatically evaluated and given a set of permissions by the CLR. Depending on the permissions that the application receives, it either continues running or generates a security exception. The local security settings and policies in the security policy configuration files for a particular report server define the code permissions that an assembly receives.  
  
 Before requesting permissions, you need to be aware of the resources and protected operations your extension code is planning to use, and you also need to know which permissions protect those resources and operations. In addition, you need to keep track of any resources accessed by any class library methods that are called by the extension components. For more information, see "Requesting Permissions" in the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Developer's Guide.  
  
 Extensions deployed to a report server must run as fully trusted, meaning that your extension needs to be part of a code group that is granted the **FullTrust** permission set. This also means that your extension may have access to certain server resources and operations available through the CLR depending on the user that is being authenticated for a particular report. For more information about code groups and extensions, see [Code Access Security in Reporting Services](secure-development/code-access-security-in-reporting-services.md).  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] enforces [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] security for all of its extensions.  
  
 The following conditions apply to the deployment of data processing, delivery, rendering, and security extensions in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]:  
  
-   Only the local administrator has permission to deploy an extension.  
  
-   Only users with the appropriate read/write permissions can change the configuration files for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] component that is being extended.  
  
-   Only privileged users have permission to edit the security policy files and enable code access security for an extension.  
  
 For more information about code access security in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Secure Development &#40;Reporting Services&#41;](secure-development/secure-development-reporting-services.md).  
  
 For more information about [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] security, see ".NET Framework Security" in your [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Developer's Guide.  
  
## Initialization of Extension Assemblies  
 When extensions are first loaded into memory by the report server, they use the service account credentials, because some extension assemblies require specific permissions to access system resources, to read configuration files, and to load other, dependent assemblies. After an assembly has been loaded and initialized, however, all subsequent calls to extension assemblies use the credentials of the user account that is currently logged on.  
  
## See Also  
 [Reporting Services Extensions](reporting-services-extensions.md)   
 [Reporting Services Extension Library](reporting-services-extension-library.md)  
  
  
