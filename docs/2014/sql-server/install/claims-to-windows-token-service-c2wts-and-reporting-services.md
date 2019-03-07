---
title: "Claims to Windows Token Service (C2WTS) and Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/25/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "c2wts.exe.config"
  - "SharePoint mode"
  - "C2WTS"
  - "WSS_WPG"
ms.assetid: 4d380509-deed-4b4b-a9c1-a9134cc40641
author: markingmyname
ms.author: maghan
manager: craigg
---
# Claims to Windows Token Service (C2WTS) and Reporting Services
  The SharePoint Claims to Windows Token Service (c2WTS) is required with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode if you want to use windows authentication for Data Sources that are outside the SharePoint farm. This is true even if the user accesses the data sources with Windows Authentication because the communication between the web front-end (WFE) and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] shared service will always be Claims authentication.  
  
 c2WTS is needed even if your data source(s) are on the same computer as the shared service. However in this scenario, constrained delegation is not needed.  
  
 The tokens created by c2WTS will only work with constrained delegation (constrains to specific services) and the configuration option "using any authentication protocol". As noted earlier, if your data sources are on the same computer as the shared service, then constrained delegation is not needed.  
  
 If your environment will use Kerberos constrained delegation, then the SharePoint Server service and external data sources need to reside in the same Windows domain. Any service that relies on the Claims to Windows token service (c2WTS) must use Kerberos **constrained** delegation to allow c2WTS to use Kerberos protocol transition to translate claims into Windows credentials. These requirements are true for all SharePoint Shared Services. For more information, see [Overview of Kerberos authentication for Microsoft SharePoint 2010 Products  (https://technet.microsoft.com/library/gg502594.aspx)](https://technet.microsoft.com/library/gg502594.aspx).  
  
 The procedure is summarized in this topic.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2013 &#124; SharePoint 2010|  
  
## Prerequisites  
  
> [!NOTE]  
>  Note: Some of the configuration steps may change, or may not work in certain farm topologies. For instance, a single server install does not support the Windows Identity Foundation c2WTS services so claims to windows token delegation scenarios are not possible with this farm configuration.  
  
### Basic steps needed to configure c2WTS  
  
1.  Configure the c2WTS service account. Add the service account to the local Administrators group on each application server running c2WTS. In addition, verify that the account has the following local security policy rights:  
  
    -   Act as part of the operating system  
  
    -   Impersonate a client after authentication  
  
    -   Log on as a service  
  
     The account you use for c2WTS also needs to be configured for Constrained Delegation with Protocol Transitioning and needs permissions to delegate to the Services it is required to communicate with (i.e. SQL Server Engine, SQL Server Analysis Services).To configure delegation you can use the Active Directory Users and Computer snap-in.  
  
    1.  Right-click each service account and open the properties dialog. In the dialog click the **Delegation** tab.  
  
        > [!NOTE]  
        >  Note: the delegation tab is only visible if the object has an SPN assigned to it. c2WTS does not require an SPN on the c2WTS Account, however, without an SPN, the **Delegation** tab will not be visible. An alternative way to configure constrained delegation is to use a utility such as **ADSIEdit**.  
  
    2.  Key configuration options on the delegation tab are the following:  
  
        -   Select "Trust this user for delegation to specified services only"  
  
        -   Select "Use any authentication protocol"  
  
         For more information, see the "configure Kerberos constrained delegation for computers and service accounts" section of the following white paper, [Configuring Kerberos authentication for SharePoint 2010 and SQL Server 2008 R2 products](http://blogs.technet.com/b/tothesharepoint/archive/2010/07/22/whitepaper-configuring-kerberos-authentication-for-sharepoint-2010-and-sql-server-2008-r2-products.aspx)  
  
2.  Configure c2WTS 'AllowedCallers'  
  
     c2WTS requires the 'callers' identities explicitly listed in the configuration file, **c2wtshost.exe.config**. c2WTS does not accept requests from all authenticated users in the system unless it is configured to do so. In this case the 'caller' is the WSS_WPG Windows group. The c2wtshost.exe.confi file is saved in the following location:  
  
     **\Program Files\Windows Identity Foundation\v3.5\c2wtshost.exe.config**  
  
     The following is an example of the configuration file:  
  
    ```  
    <configuration>  
      <windowsTokenService>  
        <!--  
            By default no callers are allowed to use the Windows Identity Foundation Claims To NT Token Service.  
            Add the identities you wish to allow below.  
          -->  
        <allowedCallers>  
          <clear/>  
          <add value="WSS_WPG" />  
        </allowedCallers>  
      </windowsTokenService>  
    </configuration>  
    ```  
  
3.  Start the operating system c2WTS service:  
  
    1.  Configure the service to use the service account you configured in the previous step.  
  
    2.  Change the Startup type to "**Automatic**" and start the service.  
  
4.  Start the SharePoint 'Claims to Windows Token Service': Start the Claims to Windows Token Service through SharePoint Central Administration on the **Manage Services on Server** page. The service should be started on the server that will be performing the action. For example if you have a server that is a WFE and another server that is an Application Server that has the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] shared service running, you only need to start c2WTS on the Application Server. c2WTS is not needed on the WFE.  
  
## See Also  
 [Claims to Windows Token Service (c2WTS) Overview (https://msdn.microsoft.com/library/ee517278.aspx)](https://msdn.microsoft.com/library/ee517278.aspx)   
 [Overview of Kerberos authentication for Microsoft SharePoint 2010 Products (https://technet.microsoft.com/library/gg502594.aspx)](https://technet.microsoft.com/library/gg502594.aspx)  
  
  
