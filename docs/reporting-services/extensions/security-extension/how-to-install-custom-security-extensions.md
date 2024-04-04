---
title: "How to install custom security extensions"
description: Learn about the new web portal that Reporting Services 2016 introduced. See how the resulting changes affect the implementation of custom security extensions.
author: maggiesMSFT
ms.author: maggies
ms.reviewer: randolphwest
ms.date: 12/29/2022
ms.service: reporting-services
ms.subservice: extensions
ms.topic: reference
ms.custom: updatefrequency5
---
# How to install custom security extensions

[!INCLUDE[ssrs-appliesto](../../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../../includes/ssrs-appliesto-pbirs.md)]

Reporting Services 2016 introduced a new web portal in order to host new OData APIs, and also host new report workloads such as mobile reports and KPIs. This new portal relies on newer technologies and is isolated from the familiar ReportingServicesService.exe by running in a separate process called Microsoft.ReportingServices.Portal.WebHost.exe. This process isn't an ASP.NET hosted application and as such breaks assumptions from existing custom security extensions. Moreover, the current interfaces for custom security extensions don't allow for any external context to be passed-in, leaving implementers with the only choice to inspect well-known global ASP.NET objects. Because the Microsoft.ReportingServices.Portal.WebHost.exe process isn't an ASP.NET hosted application, some changes to the interface were required.

## What changed?

A new interface was introduced that can be implemented via IRSRequestContext, which provides the more common properties used by extensions to make decisions related to authentication.

In previous versions, Report Manager was the front-end and could be configured with its own custom sign-in page. In Reporting Services 2016, only one page hosted by reportserver is supported and should authenticate to both applications.

## Implementation

In previous versions, extensions could rely on a common assumption that ASP.NET objects would be readily available. Since the new portal doesn't run in ASP.NET, the extension might hit issues with objects being NULL.

The most generic example is accessing ``HttpContext.Current`` to read request information such as headers and cookies. In order to allow extensions to make the same decisions, we introduced a new method in the extension that provides request information and is called when authenticating from the portal.

Custom extensions have to implement the <xref:Microsoft.ReportingServices.Interfaces.IAuthenticationExtension2> interface in order to use this new interface. The extensions need to implement both versions of the <xref:Microsoft.ReportingServices.Interfaces.IAuthenticationExtension.GetUserInfo%2A> method, as one is called by the reportserver context and other used in Microsoft.ReportingServices.Portal.WebHost.exe process. The following sample shows one of the simple implementations for the portal where the identity resolved by the reportserver is the one used.

```csharp
public void GetUserInfo(IRSRequestContext requestContext, out IIdentity userIdentity, out IntPtr userId)
{
    userIdentity = null;
    if (requestContext.User != null)
    {
        userIdentity = requestContext.User;
    }

    // initialize a pointer to the current user ID to zero
    userId = IntPtr.Zero;
}
```

## Deployment and configuration

The basic configurations needed for custom security extension are the same as previous releases. Changes are needed for web.config and rsreportserver.config: For more information, see [Configure Custom or Forms authentication on the Report Server](../../../reporting-services/security/configure-custom-or-forms-authentication-on-the-report-server.md).

There's no longer a separate web.config for the Report Manager, the portal inherits the same settings as the reportserver endpoint.

## Machine keys

For the case of Forms authentication, which requires the decryption of the Authentication cookie, both processes need to be configured with the same machine key and decryption algorithm. This step is familiar to users who had previously setup Reporting Services to work on scale-out environments, but now is a requirement even for deployments on a single machine.

You should use a validation key specific for your deployment, there are several tools to generate the keys like Internet Information Services Manager (IIS). Other tools can be found on the internet.

**[!INCLUDE[ssrs-appliesto](../../../includes/ssrs-appliesto.md)]** [!INCLUDE[ssrs-appliesto-2017-and-later](../../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../../includes/ssrs-appliesto-pbirs.md)]

**\ReportServer\RSReportServer.config**

Open the RSReportServer.config file, and in the `<Configuration>` section paste the `<machineKey>` element that you generated. By default, the RSReportServer.config file is located in ``\Program Files\Microsoft SQL Server Reporting Services\SSRS\ReportServer\RSReportServer.config`` for Reporting Services and ``\Program Files\Microsoft Power BI Report Server\PBIRS\ReportServer\RSReportServer.config`` for Power BI Report Server.

```xml
<MachineKey ValidationKey="[YOUR KEY]" DecryptionKey=="[YOUR KEY]" Validation="AES" Decryption="AES" />
```

**[!INCLUDE[ssrs-appliesto](../../../includes/ssrs-appliesto.md)]** [!INCLUDE[ssrs-appliesto-2016](../../../includes/ssrs-appliesto-2016.md)]

**\ReportServer\web.config**

Open the web.config file for ReportServer, and in the `<system.web>` section paste the `<machineKey>` element that you generated. By default, the Web.config file is located in \Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\ReportServer\web.config.

```xml
<machineKey validationKey="[YOUR KEY]" decryptionKey=="[YOUR KEY]" validation="AES" decryption="AES" />
```

**\RSWebApp\Microsoft.ReportingServices.Portal.exe.config**

Open the Microsoft.ReportingServices.Portal.WebHost.exe.config file for RSWebApp, and in the `<configuration>` section paste the `<system.web>` and `<machineKey>` element that you generated. By default, the Microsoft.ReportingServices.Portal.WebHost.exe.config file is located in \Program Files\Microsoft SQL Server\MSRS13.MSSQLSERVER\Reporting Services\RSWebApp\Microsoft.ReportingServices.Portal.WebHost.exe.config.

```xml
<system.web>
    <machineKey validationKey=="[YOUR KEY]" decryptionKey=="[YOUR KEY]" validation="AES" decryption="AES" />
</system.web>
```

## Configure passthrough cookies

The new portal and the reportserver communicate using internal soap APIs for some of its operations (similar to the previous version of the Report Manager). When more cookies are required to be passed from the portal to the reportserver, the PassThroughCookies property is available to use. For more information, see [Configure the web portal to pass custom authentication cookies](../../../reporting-services/security/configure-the-web-portal-to-pass-custom-authentication-cookies.md).

```xml
<UI>
   <CustomAuthenticationUI>
      <PassThroughCookies>
         <PassThroughCookie>sqlAuthCookie</PassThroughCookie>
      </PassThroughCookies>
   </CustomAuthenticationUI>
</UI>
```

## Related content

- [Configure Custom or Forms authentication on the Report Server](../../../reporting-services/security/configure-custom-or-forms-authentication-on-the-report-server.md)
- [Configure the web portal to pass custom authentication cookies](../../security/configure-the-web-portal-to-pass-custom-authentication-cookies.md)

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
