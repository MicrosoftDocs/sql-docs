---
title: "Claims to Windows Token Service (c2WTS) and Reporting Services | Microsoft Docs"
ms.custom: "The Claims to Windows Token Service (C2WTS) is used by SharePoint and needs to be configured for Kerberos constrained delegation to work with SQL Server Reporting Services properly."
ms.date: "09/15/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Claims to Windows Token Service (C2WTS) and Reporting Services

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

The SharePoint Claims to Windows Token Service (C2WTS) is required if you want to view native mode reports within the [SQL Server Reporting Services Report Viewer web part](../report-server-sharepoint/deploy-report-viewer-web-part.md).

C2WTS is also required with SQL Server Reporting Services SharePoint mode if you want to use Windows authentication for data sources that are outside the SharePoint farm. C2WTS is needed even if your data source(s) are on the same computer as the shared service. However in this scenario, constrained delegation is not needed.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

## Report Viewer web part configuration

The Report Viewer web part can be used to embed SQL Server Reporting Services native mode reports within your SharePoint site. This web part is available for SharePoint 2013 and SharePoint 2016. Both SharePoint 2013 and SharePoint 2016 make use of claims authentication. SQL Server Reporting Services (native mode) uses Windows authentication by default. As a result, C2WTS needs to be configured properly for reports to render correctly.

## SharePoint mode integaration

**This section only applies to SQL Server 2016 Reporting Services and earlier.**

The SharePoint Claims to Windows Token Service (C2WTS) is required with SQL Server Reporting Services SharePoint mode if you want to use Windows Authentication for data sources that are outside the SharePoint farm. This is true even if the user accesses the data sources with Windows Authentication because the communication between the web front-end (WFE) and the Reporting Services shared service will always be Claims Authentication.

## Steps needed to configure c2WTS

The tokens created by C2WTS will only work with constrained delegation (constrains to specific services) and the configuration option "using any authentication protocol". As noted earlier, if your data sources are on the same computer as the shared service, then constrained delegation is not needed.

If your environment will use Kerberos constrained delegation, then the SharePoint Server service and external data sources need to reside in the same Windows domain. Any service that relies on the Claims to Windows token service (c2WTS) must use Kerberos **constrained** delegation to allow c2WTS to use Kerberos protocol transition to translate claims into Windows credentials. These requirements are true for all SharePoint Shared Services. For more information, see [Plan for Kerberos authentication in SharePoint 2013](http://technet.microsoft.com/library/ee806870.aspx).  

1. Configure the C2WTS service account. Add the service account to the local Administrators group on each server that C2WTS will be used.

    For the **Report Viewer web part**, this will be the Web Front End (WFE) servers. For **SharePoint integrated mode**, this will be the application servers where the Reporting Services service is running.

2. Configure delegation for the C2WTS service account.

    The account needs Constrained Delegation with Protocol Transitioning and permissions to delegate to the services it is required to communicate with (i.e. SQL Server Database Engine, SQL Server Analysis Services). To configure delegation you can use the Active Directory Users and Computer snap-in and will need to be a domain administrator.

    > [!IMPORTANT]
    > Whatever settings you configure for the C2WTS service account, on the delegation tab, needs to match the main service account being used. For the **Report Viewer web part**, this will be the service account for the SharePoint web application. For **SharePoint integrated mode**, this will be the Reporting Services service account.
    >
    > For example, if you allow the C2WTS service account to delegate to a SQL Service, you need to do the same on the Reporting Services service account for SharePoint integrated mode.

    * Right-click each service account and open the properties dialog. In the dialog click the **Delegation** tab.

        The delegation tab is only visible if the object has a Service Prinicpal Name (SPN) assigned to it. C2WTS does not require an SPN on the C2WTS Account, however, without an SPN, the **Delegation** tab will not be visible. An alternative way to configure constrained delegation is to use a utility such as **ADSIEdit**.

    * Key configuration options on the delegation tab are the following:

        * Select **Trust this user for delegation to specified services only**
        * Select **Use any authentication protocol**

    * Select **Add** to add a service to delegate to.

    * Select **Users or Computers...*** and enter the account that hosts the service. For example, if a SQL Server is running under an account named *sqlservice*, enter `sqlservice`. 

    * Select the service listing. This will show the SPNs that are available on that account. If you don't see the service listed on that account, it may be missing or placed on a different account. you can use the SetSPN utility to adjust SPNs.

    * Select OK to get out of the dialogs.

3. Configure C2WTS *AllowedCallers*.

    C2WTS requires the ‘callers’ identities explicitly listed in the configuration file, **C2WTShost.exe.config**. C2WTS does not accept requests from all authenticated users in the system unless it is configured to do so. In this case the 'caller' is the WSS_WPG Windows group. The C2WTShost.exe.confi file is saved in the following location:

    Changing the service account within SharePoint Central Admin, for the C2WTS service, will add that account to the WSS_WPG group.

    **\Program Files\Windows Identity Foundation\v3.5\c2WTShost.exe.config**

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

4. Start the Claims to Windows Token Service through SharePoint Central Administration on the **Manage Services on Server** page. The service should be started on the server that will be performing the action. For example if you have a server that is a WFE and another server that is an Application Server that has the SQL Server Reporting Services shared service running, you only need to start C2WTS on the Application Server. C2WTS is only required on a WFE server if you are running the Report Viewer web part.

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)