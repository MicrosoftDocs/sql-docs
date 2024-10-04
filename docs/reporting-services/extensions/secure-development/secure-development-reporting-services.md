---
title: "Secure development (Reporting Services)"
description: Learn about the code access security system that Reporting Services uses, which runs code in tightly constrained, administrator-defined security contexts.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "development security [Reporting Services]"
  - "security [Reporting Services], development"
  - "security [Reporting Services], strategies"
---
# Secure development (Reporting Services)
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] provides a robust security system that can run code in tightly constrained, administrator-defined security contexts. [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] security system, known as code access security (or evidence-based security). Under code access security, a user might be trusted to access a resource, but if the code the user executes isn't trusted, access to the resource is denied.  
  
 Security based on code, as opposed to specific users, permits security to be expressed for custom assemblies or data, delivery, rendering, and security extensions that you develop for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. Any number of [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] users might execute your extension code, all of whom are unknown at development time. The custom assemblies or extensions that you develop require specific security policies in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. These security policies are represented as types in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)]. For a more information about code access security, see "Code Access Security" in the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] documentation.  
  
## In this section  
 [Code access security in Reporting Services](../../../reporting-services/extensions/secure-development/code-access-security-in-reporting-services.md)  
 Introduces code access security and policy configuration for custom assemblies and extensions in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
 [Understand security policies](../../../reporting-services/extensions/secure-development/understanding-security-policies.md)  
 Describes the various assembly types in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and how code access security affects code permissions.  
  
 [Use Reporting Services security policy files](../../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md)  
 Describes the different [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] components and the corresponding policy configuration files.  
  
  
