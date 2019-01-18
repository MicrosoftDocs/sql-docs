---
title: "Security Extensions Overview | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "security [Reporting Services], extensions"
ms.assetid: 24ccd795-6506-457c-93ac-6a9dd6bb9a46
author: markingmyname
ms.author: maghan
manager: craigg
---
# Security Extensions Overview
  A [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] security extension enables the authentication and authorization of users or groups; that is, it enables different users to log on to a report server and, based on their identities, perform different tasks or operations. By default, [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses a Windows-based authentication extension, which uses Windows account protocols to verify the identities of users who claim to have accounts on the system. [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] uses a role-based security system to authorize users. The [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] role-based security model is similar to the role-based security models of other technologies.  
  
 Because security extensions are based on an open and extensible API, you can create new authentication and authorization extensions in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)]. The following is an example of a typical security extension implementation that uses Forms-based authentication and authorization:  
  
 ![Reporting Services security extension process](../../media/rosettasecurityextensionflow.gif "Reporting Services security extension process")  
  
 As shown in the illustration, authentication and authorization occur as follows:  
  
1.  A user tries to access Report Manager by using a URL and is redirected to a form that collects user credentials for the client application.  
  
2.  The user submits credentials to the form.  
  
3.  The user credentials are submitted to the Reporting Services Web service through the <xref:ReportService2010.ReportingService2010.LogonUser%2A> method.  
  
4.  The Web service calls the customer-supplied security extension and verifies that the user name and password exist in the custom security authority.  
  
5.  After authentication, the Web service creates an authentication ticket (known as a "cookie"), manages the ticket, and verifies the user's role for the Home page of Report Manager.  
  
6.  The Web service returns the cookie to the browser and displays the appropriate user interface in Report Manager.  
  
7.  After the user is authenticated, the browser makes requests to Report Manager while transmitting the cookie in the HTTP header. These requests are in response to user actions within the Report Manager application.  
  
8.  The cookie is transmitted in the HTTP header to the Web service along with the requested user operation.  
  
9. The cookie is validated, and if it is valid, the report server returns the security descriptor and other information relating to the requested operation from the report server database.  
  
10. If the cookie is valid, the report server makes a call to the security extension to check if the user is authorized to perform the specific operation.  
  
11. If the user is authorized, the report server performs the requested operation and returns control to the caller.  
  
12. After the user is authenticated, URL access to the report server uses the same cookie. The cookie is transmitted in the HTTP header.  
  
13. The user continues to request operations on the report server until the session has ended.  
  
## When to Implement a Security Extension  
 We recommend that you use Windows Authentication if at all possible. However, custom authentication and authorization for [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] may be appropriate in the following two cases:  
  
-   You have an Internet or extranet application that cannot use Windows accounts.  
  
-   You have custom-defined users and roles and need to provide a matching authorization scheme in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].  
  
## See Also  
 [Implementing a Security Extension](../security-extension/implementing-a-security-extension.md)   
 [Configure Report Manager to Pass Custom Authentication Cookies](../../security/configure-the-web-portal-to-pass-custom-authentication-cookies.md)  
  
  
