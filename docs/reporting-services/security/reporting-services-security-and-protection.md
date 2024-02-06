---
title: "Reporting Services security and protection"
description: "Reporting Services security and protection"
author: maggiesMSFT
ms.author: maggies
ms.date: 08/26/2016
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "security [Reporting Services]"
  - "Reporting Services, security"
---
# Reporting Services security and protection
  You can use information in this section to learn about the security features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. This section also explains the authorization models and authentication providers supported in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
## Extended protection for authentication  
 Beginning with [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)], support for Extended Protection for Authentication is available. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] feature supports the use of channel binding and service binding to enhance protection of authentication. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features need to be used with an operating system that supports Extended Protection. For more information, see [Extended protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md).  
  
## Authentication and authorization  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides different authentication types for users and client applications to authenticate with the report server. Using the right authentication type for your report server enables your organization to achieve the appropriate level of security required by your organization. For more information, see [Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md).  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] also employs roles and permissions to control user access to content in the report server catalog. The roles and permissions define who can access what and how they can access it. For more information, see [Roles and permissions &#40;Reporting Services&#41;](../../reporting-services/security/roles-and-permissions-reporting-services.md).  
  
## Related content  
  
|Task Descriptions|Links|  
|-----------------------|-----------|  
|Configure the Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), to secure client connections to the report server.|[Configure TLS connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md)|  
  
  
