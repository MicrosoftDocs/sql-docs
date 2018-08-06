---
title: "Authentication methodologies supported by Analysis Services | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Authentication methodologies supported by Analysis Services
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Connections from a client application to an Analysis Services instance require Windows authentication (integrated). You can provide a Windows user identity using any of the following methods:  
  
-   NTLM  
  
-   Kerberos (see [Configure Analysis Services for Kerberos constrained delegation](../../analysis-services/instances/configure-analysis-services-for-kerberos-constrained-delegation.md))  
  
-   EffectiveUserName on the connection string  
  
-   Basic or Anonymous (requires configuration for HTTP access)  
  
-   Stored credentials  
  
 Notice that Claims authentication is not supported. You cannot use a Windows Claims token to access Analysis Services. The Analysis Services client libraries only work with Windows security principles. If your BI solution includes claims identities, you will need Windows identity shadow accounts for each user, or use stored credentials to access Analysis Services data.  
  
 For more information about BI and Analysis Services authentication flows, see [Microsoft BI Authentication and Identity Delegation](http://go.microsoft.com/fwlink/?LinkID=286576).  
  
##  <a name="bkmk_auth"></a> Understanding your authentication alternatives  
 Connecting to an Analysis Services database requires a Windows user or group identity and associated permissions. The identity might be a general purpose login used by anyone who needs to view a report, but a more likely scenario includes the identity of individual users.  
  
 Often, a tabular or multidimensional model will have different levels of data access, by object or within the data itself, based on who is making the request. To meet this requirement, you can use NTLM, Kerberos, EffectiveUserName, or Basic authentication. All of these techniques offer an approach for passing in different user identities with each connection. However, most of these choices are subject to a single hop limitation. Only Kerberos with delegation allows the original user identity to flow across multiple computer connections to a backend data store on a remote server.  
  
 **NTLM**  
  
 For connections that specify `SSPI=Negotiate`, NTLM is the backup authentication subsystem used when a Kerberos domain controller is not available. Under NTLM, any user or client application can access a server resource as long as the request is a direct connection from a client to the server, the person requesting the connection has permission to the resource, and the client and server computers are in the same domain.  
  
 In multi-tier solutions, the single hop restriction of NLTM can be a major constraint. The user identity making the request can be impersonated on exactly one remote server, but travels no further. If the current operation requires services running on multiple computers, you will need to configure Kerberos constrained delegation to reuse the security token on backend servers. Alternatively, you can use stored credentials or Basic authentication to pass in new identity information over a single hop connection.  
  
 **Kerberos Authentication and Kerberos Constrained Delegation**  
  
 Kerberos authentication is the basis of Windows integrated security in Active Directory domains. As with NTLM, impersonation under Kerberos is limited to a single hop unless you enable delegation.  
  
 To support multi-hop connections, Kerberos provides both constrained and unconstrained delegation, but for most scenarios, constrained delegation is considered a security best practice. Constrained delegation allows a service to pass the security token of the user identity to a designated down-level service on a remote computer. For multi-tier applications, delegating a user identity from a middle tier application server to a backend database such as Analysis Services is a common requirement. For example, a tabular or multidimensional model that returns different data based on user identity would require identity delegation from a middle tier service to avoid having the user re-enter credentials, or getting security credentials some other way.  
  
 Constrained delegation requires additional configuration in Active Directory, where services on both the sending and receiving end of the request are explicitly authorized for delegation. Although there are configuration costs up front, once the service is configured, password updates are managed independently in Active Directory. You do not need to update stored account information in applications, as you would if using the stored credentials option described further on.  
  
 For more information about configuring Analysis Services for constrained delegation, see [Configure Analysis Services for Kerberos constrained delegation](../../analysis-services/instances/configure-analysis-services-for-kerberos-constrained-delegation.md).  
  
> [!NOTE]  
>  Windows Server 2012 supports constrained delegation across domains. In contrast, configuring Kerberos constrained delegation in domains at lower functional levels, such as Windows Server 2008 or 2008 R2, require both client and server computers to be members of the same domain.  
  
 **EffectiveUserName**  
  
 EffectiveUserName is a connection string property used for passing identity information to Analysis Services. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint uses it to record user activity in the usage logs. Excel Services and PerformancePoint Services can use it to retrieve data used by workbooks or dashboards in SharePoint. It can also be used in custom applications or scripts that perform operations on an Analysis Services instance.  
  
 For more information about using EffectiveUserName in SharePoint, see [Use Analysis Services EffectiveUserName in SharePoint Server 2010](http://go.microsoft.com/fwlink/?LinkId=311905).  
  
 **Basic Authentication and Anonymous User**  
  
 Basic authentication provides yet a fourth alternative for connecting to a backend server as a specific user. Using Basic authentication, the Windows user name and password are passed on the connection string, introducing additional wire encryption requirements to ensure sensitive information is protected while in transit. An important advantage to using Basic authentication is that the authentication request can cross domain boundaries.  
  
 For Anonymous authentication, you can set the anonymous user identity to a specific Windows user account (IUSR_GUEST by default) or an application pool identity. The anonymous user account will be used on the Analysis Services connection, and must have data access permissions on the Analysis Services instance. When you use this approach, only the user identity associated with the Anonymous account is used on the connection. If your application requires additional identity management, you will need to choose one of the other approaches, or supplement with an identity management solution that you provide.  
  
 Basic and Anonymous are available only when you configure Analysis Services for HTTP access, using IIS and the msmdpump.dll to establish the connection. For more information, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md).  
  
 **Stored Credentials**  
  
 Most middle tier application services include functionality for storing a user name and password subsequently used to retrieve data from a down-level data store, such as Analysis Services or the SQL Server relational engine. As such, stored credentials provide a fifth alternative for retrieving data. Limitations with this approach include maintenance overhead associated with keeping user names and passwords up to date, and the use of a single identity on the connection. If your solution requires the identity of the original caller, then stored credentials would not be a viable alternative.  
  
 For more information about stored credentials, see [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](../../reporting-services/report-data/create-modify-and-delete-shared-data-sources-ssrs.md) and [Use Excel Services with Secure Store Service in SharePoint Server 2013](http://go.microsoft.com/fwlink/?LinkID=309869).  
  
## See Also  
 [Using Impersonation with Transport Security](http://go.microsoft.com/fwlink/?LinkId=311727)   
 [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md)   
 [Configure Analysis Services for Kerberos constrained delegation](../../analysis-services/instances/configure-analysis-services-for-kerberos-constrained-delegation.md)   
 [SPN registration for an Analysis Services instance](../../analysis-services/instances/spn-registration-for-an-analysis-services-instance.md)   
 [Connect to Analysis Services](../../analysis-services/instances/connect-to-analysis-services.md)  
  
  
