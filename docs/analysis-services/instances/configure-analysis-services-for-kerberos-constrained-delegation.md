---
title: "Configure Analysis Services for Kerberos constrained delegation | Microsoft Docs"
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
# Configure Analysis Services for Kerberos constrained delegation
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  When configuring Analysis Services for Kerberos authentication, you are most likely interested in achieving one or both of the following outcomes: having Analysis Services impersonate a user identity when querying data; or having Analysis Services delegate a user identity to a down-level service. Each scenario calls for slightly different configuration requirements. Both scenarios require verification to ensure configuration was done properly.  
  
> [!TIP]  
>  **[!INCLUDE[msCoName](../../includes/msconame-md.md)] Kerberos Configuration Manager for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** is a diagnostic tool that helps troubleshoot Kerberos related connectivity issues with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Microsoft Kerberos Configuration Manager for SQL Server](http://www.microsoft.com/download/details.aspx?id=39046).  
  
 This topic contains the following sections:  
  
-   [Allow Analysis Services to impersonate a user identity](#bkmk_impersonate)  
  
-   [Configure Analysis Services for trusted delegation](#bkmk_delegate)  
  
-   [Test for impersonated or delegated identity](#bkmk_test)  
  
> [!NOTE]  
>  Delegation is not required if the connection to Analysis Services is a single hop, or your solution uses stored credentials provided by SharePoint Secure Store Service or Reporting Services. If all connections are direct connections from Excel to an Analysis Services database, or based on stored credentials, you can use Kerberos (or NTLM) without having to configure constrained delegation.  
>   
>  Kerberos constrained delegation is required if the user identity has to flow over multiple computer connections (known as "double-hop"). When Analysis Services data access is contingent upon user identity, and the connection request is from a delegating service, use the checklist in the next section to ensure that Analysis Services is able to impersonate the original caller. For more information about Analysis Services authentication flows, see [Microsoft BI Authentication and Identity Delegation](http://go.microsoft.com/fwlink/?LinkID=286576).  
>   
>  As a security best practice, Microsoft always recommends constrained delegation over unconstrained delegation. Unconstrained delegation is a major security risk because it allows the service identity to impersonate another user on *any* downstream computer, service, or application (as opposed to just those services explicitly defined via constrained delegation).  
  
##  <a name="bkmk_impersonate"></a> Allow Analysis Services to impersonate a user identity  
 To allow up-level services such as Reporting Services, IIS, or SharePoint to impersonate a user identity on Analysis Services, you must configure Kerberos constrained delegation for those services. In this scenario, Analysis Services impersonates the current user using the identity provided by the delegating service, returning results based on role membership of that user identity.  
  
|Task|Description|  
|----------|-----------------|  
|Step 1: Verify that accounts are suitable for delegation|Ensure that the accounts used to run the services have the correct properties in Active Directory. Service accounts in Active Directory must not be marked as sensitive accounts, or specifically excluded from delegation scenarios. For more information, see [Understanding User Accounts](http://go.microsoft.com/fwlink/?LinkId=235818).<br /><br /> Note: Generally, all accounts and servers must belong to the same Active Directory domain or to trusted domains in the same forest. However, because Windows Server 2012 supports delegation across domain boundaries, you can configure Kerberos constrained delegation across a domain boundary if the domain functional level is Windows Server 2012. Another alternative is to configure Analysis Services for HTTP access and use IIS authentication methods on the client connection. For more information, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md).|  
|Step 2: Register the SPN|Before setting up constrained delegation, you must register a Service Principle Name (SPN) for the Analysis Services instance. You will need the Analysis Services SPN when configuring Kerberos constrained delegation for middle tier services. See [SPN registration for an Analysis Services instance](../../analysis-services/instances/spn-registration-for-an-analysis-services-instance.md) for instructions.<br /><br /> A Service Principle Name (SPN) specifies the unique identity of a service in a domain configured for Kerberos authentication. Client connections using integrated security often request an SPN as part of SSPI authentication. The request is forwarded to an Active Directory Domain Controller (DC), with the KDC granting a ticket if the SPN presented by the client has a matching SPN registration in Active Directory.|  
|Step 3: Configure constrained delegation|After validating the accounts you want to use and registering SPNs for those accounts, your next step is to configure up-level services, such as IIS, Reporting Services, or SharePoint web services for constrained delegation, specifying the Analysis Services SPN as the specific service for which delegation is allowed.<br /><br /> Services that run in SharePoint, such as Excel Services or Reporting Services in SharePoint mode, often host workbooks and reports that consume Analysis Services multidimensional or tabular data. Configuring constrained delegation for these services is a common configuration task, and necessary for supporting data refresh from Excel Services. The following links provide instructions for SharePoint services, as well as other services likely to present a downstream data connection request for Analysis Services data:<br /><br /> [Identity delegation for Excel Services (SharePoint Server 2010)](http://go.microsoft.com/fwlink/?LinkId=299826) or [How to configure Excel Services in SharePoint Server 2010 for Kerberos authentication](http://support.microsoft.com/kb/2466519)<br /><br /> [Identity delegation for PerformancePoint Services (SharePoint Server 2010)](http://go.microsoft.com/fwlink/?LinkId=299827)<br /><br /> [Identity delegation for SQL Server Reporting Services (SharePoint Server 2010)](http://go.microsoft.com/fwlink/?LinkId=299828)<br /><br /> For IIS 7.0 see [Configure Windows Authentication (IIS 7.0)](http://technet.microsoft.com/library/cc754628\(v=ws.10\).aspx) or [How to configure SQL Server 2008 Analysis Services and SQL Server 2005 Analysis Services to use Kerberos authentication](http://support.microsoft.com/kb/917409).|  
|Step 4: Test connections|When testing, connect from remote computers, under different identities, and query Analysis Services using the same applications as business users. You can use SQL Server Profiler to monitor the connection. You should see the user identity on the request. For more information, see [Test for impersonated or delegated identity](#bkmk_test) in this section.|  
  
##  <a name="bkmk_delegate"></a> Configure Analysis Services for trusted delegation  
 Configuring Analysis Services for Kerberos constrained delegation allows the service to impersonate a client identity on a down-level service, such as the relational database engine, so that data can be queried as if the client was connected directly.  
  
 Delegation scenarios for Analysis Services are limited to tabular models configured for **DirectQuery** mode. This is the only scenario in which Analysis Services can pass delegated credentials to another service. In all other scenarios, such as SharePoint scenarios mentioned in the previous section, Analysis Services is on the receiving end of the delegation chain. For more information about DirectQuery, see [DirectQuery Mode](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md).  
  
> [!NOTE]  
>  A common misconception is that ROLAP storage, processing operations, or access to remote partitions somehow introduce requirements for constrained delegation. This is not the case. All of these operations are executed directly by the service account (also referred to as the processing account), on its own behalf. Delegation is not required for these operations in Analysis Services, given that permissions for such operations are granted directly to the service account (for example, granting db_datareader permissions on the relational database so that the service can process data). For more information about server operations and permissions, see [Configure Service Accounts &#40;Analysis Services&#41;](../../analysis-services/instances/configure-service-accounts-analysis-services.md).  
  
 This section explains how to set up Analysis Services for trusted delegation. After you complete this task, Analysis Services will be able to pass delegated credentials to SQL Server, in support of DirectQuery mode used in Tabular solutions.  
  
 Before you start:  
  
-   Verify that Analysis Services is started.  
  
-   Verify the SPN registered for Analysis Services is valid. For instructions, see [SPN registration for an Analysis Services instance](../../analysis-services/instances/spn-registration-for-an-analysis-services-instance.md)  
  
 When both prerequisites are satisfied, continue with the following steps. Note that you must be a domain administrator to set up constrained delegation.  
  
1.  In Active Directory Users and Computers, find the service account under which Analysis Services runs. Right-click the service account and choose **Properties**.  
  
     For illustrative purposes, the following screenshots use OlapSvc and SQLSvc to represent Analysis Services and SQL Server, respectively.  
  
     OlapSvc is the account that will be configured for constrained delegation to SQLSvc. When you complete this task, OlapSvc will have permission to pass delegated credentials on a service ticket to SQLSvc, impersonating the original caller when requesting data.  
  
2.  On the Delegation tab, select **Trust this user for delegation to specified services only**, followed by **Use Kerberos only**. Click **Add** to specify which service Analysis Services is permitted to delegate credentials.  
  
     Delegation tab appears only when the user account (OlapSvc) is assigned to a service (Analysis Services), and the service has an SPN registered for it. SPN registration requires that the service is running.  
  
     ![SSAS_Kerberos_1_AccountProperties](../../analysis-services/instances/media/ssas-kerberos-1-accountproperties.gif "SSAS_Kerberos_1_AccountProperties")  
  
3.  On the Add Services page, click **Users or Computers**.  
  
     ![SSAS_Kerberos_2_](../../analysis-services/instances/media/ssas-kerberos-2.gif "SSAS_Kerberos_2_")  
  
4.  On the Select Users or Computer page, enter the account used to run the SQL Server instance providing data to Analysis Services tabular model databases. Click **OK** to accept the service account.  
  
     If you cannot select the account you want, verify that SQL Server is running and has an SPN registered for that account. For more information about SPNs for the database engine, see [Register a Service Principal Name for Kerberos Connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).  
  
     ![SSAS_Kerberos_3_SelectUsers](../../analysis-services/instances/media/ssas-kerberos-3-selectusers.gif "SSAS_Kerberos_3_SelectUsers")  
  
5.  The SQL Server instance should now appear in Add Services. Any service also using that account will also appear in the list. Choose the SQL Server instance you want to use. Click **OK** to accept the instance.  
  
     ![SSAS_Kerberos_4_](../../analysis-services/instances/media/ssas-kerberos-4.gif "SSAS_Kerberos_4_")  
  
6.  The properties page of the Analysis Services service account should now look similar to the following screenshot. Click **OK** to save your changes.  
  
     ![SSAS_Kerberos_5_Finished](../../analysis-services/instances/media/ssas-kerberos-5-finished.gif "SSAS_Kerberos_5_Finished")  
  
7.  Test for successful delegation by connecting from a remote client computer, under a different identity, and query the tabular model. You should see the user identity on the request in SQL Server Profiler.  
  
##  <a name="bkmk_test"></a> Test for impersonated or delegated identity  
 Use SQL Server Profiler to monitor the identity of the user who is querying data.  
  
1.  Start **SQL Server Profiler** on the Analysis Services instance and then start a new trace.  
  
2.  In Events Selection, verify that **Audit Login** and **Audit Logout** are checked in the Security Audit section.  
  
3.  Connect to Analysis Services via an application service (such as SharePoint or Reporting Services) from a remote client computer. The Audit Login event will show the identity of the user connecting to Analysis Services.  
  
 Thorough testing will require the use of network monitoring tools that can capture Kerberos requests and responses on the network. The Network Monitor utility (netmon.exe), filtered for Kerberos, can be used for this task. For more information about using Netmon 3.4 and other tools for testing Kerberos authentication, see [Configuring Kerberos authentication: Core configuration (SharePoint Server 2010)](http://technet.microsoft.com/library/gg502602\(v=office.14\).aspx).  
  
 Additionally, see [The Most Confusing Dialog Box in Active Directory](http://windowsitpro.com/windows/most-confusing-dialog-box-active-directory) for a thorough description of each option in the Delegation tab of the Active Directory object's property dialog box. This article also explains how to use LDP to test and interpret test results.  
  
## See Also  
 [Microsoft BI Authentication and Identity Delegation](http://go.microsoft.com/fwlink/?LinkID=286576)   
 [Mutual Authentication Using Kerberos](http://go.microsoft.com/fwlink/?LinkId=299283)   
 [Connect to Analysis Services](../../analysis-services/instances/connect-to-analysis-services.md)   
 [SPN registration for an Analysis Services instance](../../analysis-services/instances/spn-registration-for-an-analysis-services-instance.md)   
 [Connection String Properties &#40;Analysis Services&#41;](../../analysis-services/instances/connection-string-properties-analysis-services.md)  
  
  
