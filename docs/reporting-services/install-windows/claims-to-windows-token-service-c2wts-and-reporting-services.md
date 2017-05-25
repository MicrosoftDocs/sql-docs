---
title: "Claims to Windows Token Service (c2WTS) and Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "c2wts.exe.config"
  - "SharePoint mode"
  - "C2WTS"
  - "WSS_WPG"
ms.assetid: 4d380509-deed-4b4b-a9c1-a9134cc40641
caps.latest.revision: 17
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Claims to Windows Token Service (c2WTS) and Reporting Services

[!INCLUDE[ssrs-appliesto-sql2016-xpreview](../includes/ssrs-appliesto-sql2016-xpreview.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

  The SharePoint Claims to Windows Token Service (c2WTS) is required with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode if you want to use Windows Authentication for data sources that are outside the SharePoint farm. This is true even if the user accesses the data sources with Windows Authentication because the communication between the web front-end (WFE) and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] shared service will always be Claims Authentication.  
  
 c2WTS is needed even if your data source(s) are on the same computer as the shared service. However in this scenario, constrained delegation is not needed.  
  
 The tokens created by c2WTS will only work with constrained delegation (constrains to specific services) and the configuration option "using any authentication protocol". As noted earlier, if your data sources are on the same computer as the shared service, then constrained delegation is not needed.  
  
 If your environment will use Kerberos constrained delegation, then the SharePoint Server service and external data sources need to reside in the same Windows domain. Any service that relies on the Claims to Windows token service (c2WTS) must use Kerberos **constrained** delegation to allow c2WTS to use Kerberos protocol transition to translate claims into Windows credentials. These requirements are true for all SharePoint Shared Services. For more information, see [Plan for Kerberos authentication in SharePoint 2013](http://technet.microsoft.com/library/ee806870.aspx).  
  
 The procedure is summarized in this topic.

## Prerequisites

> [!NOTE]
>  Note: Some of the configuration steps may change, or may not work in certain farm topologies. For instance, a single server install does not support the Windows Identity Foundation c2WTS services so claims to windows token delegation scenarios are not possible with this farm configuration.

> [!IMPORTANT]
> If you are using Power View to work against Power Pivot workbooks, you will need to do additional configuration for [Office Online Server overview](https://technet.microsoft.com/library/jj219437\(v=office.16\).aspx). For more information, see the following white papers. 
>
> - [Deploying SQL Server 2016 PowerPivot and Power View in SharePoint 2016](../../analysis-services/instances/install-windows/deploying-sql-server-2016-powerpivot-and-power-view-in-sharepoint-2016.md)
> 
> - [Deploying SQL Server 2016 PowerPivot and Power View in a Multi-Tier SharePoint 2016 Farm](../../analysis-services/instances/install-windows/deploy-powerpivot-and-power-view-multi-tier-sharepoint-2016-farm.md)
  
### Basic steps needed to configure c2WTS  
  
1.  Configure the c2WTS service account. Add the service account to the local Administrators group on each application server running c2WTS. In addition, verify that the account has the following local security policy rights:  
  
    -   Act as part of the operating system  
  
    -   Impersonate a client after authentication  
  
    -   Log on as a service  
  
2.  Configure delegation for the c2WTS service account. The account needs Constrained Delegation with Protocol Transitioning and permissions to delegate to the services it is required to communicate with (i.e. SQL Server Engine, SQL Server Analysis Services). To configure delegation you can use the Active Directory Users and Computer snap-in.  

    > [!IMPORTANT]
    > Whatever settings you configure for the C2WTS service account, on the delegation tab, needs to match the Reporting Services Service account. For example, if you allow the C2WTS service account to delegate to a SQL Service, you need to do the same on the Reporting Services service account.
  
    1.  Right-click each service account and open the properties dialog. In the dialog click the **Delegation** tab.  
  
        > [!NOTE]  
        >  Note: the delegation tab is only visible if the object has a Service Prinicpal Name (SPN) assigned to it. c2WTS does not require an SPN on the c2WTS Account, however, without an SPN, the **Delegation** tab will not be visible. An alternative way to configure constrained delegation is to use a utility such as **ADSIEdit**.  
  
    2.  Key configuration options on the delegation tab are the following:  
  
        -   Select “Trust this user for delegation to specified services only”  
  
        -   Select “Use any authentication protocol”  

    3. Select **Add** to add a service to delegate to.
    
    4. Select **Users or Computers...*** and enter the account that hosts the service. For example, if a SQL Server is running under an account named *sqlservice*, enter `sqlservice`.
    
    5. Select the service listing. This will show the SPNs that are available on that account. If you don't see the service listed on that account, it may be missing or placed on a different account. you can use the SetSPN utility to adjust SPNs.
    
    6. Select OK to get out of the dialogs.
  
3.  Configure c2WTS ‘AllowedCallers’  
  
     c2WTS requires the ‘callers’ identities explicitly listed in the configuration file, **c2WTShost.exe.config**. c2WTS does not accept requests from all authenticated users in the system unless it is configured to do so. In this case the ‘caller’ is the WSS_WPG Windows group. The c2WTShost.exe.confi file is saved in the following location:  
     
     > [!NOTE]
     > Changing the service account within SharePoint Central Admin, for the C2WTS service, will add that account to the WSS_WPG group.
  
     **\Program Files\Windows Identity Foundation\v3.5\c2WTShost.exe.config**  
  
     The following is an example of the configuration file:  
  
    ```  
    <configuration>  
      <windowsTokenService>  
        \<!--  
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
4.  Start the SharePoint ‘Claims to Windows Token Service’: Start the Claims to Windows Token Service through SharePoint Central Administration on the **Manage Services on Server** page. The service should be started on the server that will be performing the action. For example if you have a server that is a WFE and another server that is an Application Server that has the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] shared service running, you only need to start c2WTS on the Application Server. c2WTS is not needed on the WFE.

## Next steps

[Claims to Windows Token Service (c2WTS) Overview](http://msdn.microsoft.com/library/ee517278.aspx)   
[Plan for Kerberos authentication in SharePoint 2013](http://technet.microsoft.com/library/ee806870.aspx)  

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)