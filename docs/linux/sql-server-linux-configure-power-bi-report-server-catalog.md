---
title: Configure Power BI Report Server catalog databases for SQL Server on Linux
description: Learn how to configure SQL Server on Linux to host the Power BI Report Server catalog database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/05/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Configure Power BI Report Server catalog databases for SQL Server on Linux

[!INCLUDE [sql-linux-pbirs-2019](../includes/applies-to-version/sql-linux-pbirs-2019.md)]

This article explains how to install and configure the Power BI Report Server (PBIRS) catalog database for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

## Prerequisites

In this article, the examples use the domain `CORPNET.CONTOSO.COM`, and the following configuration.

### Configure machines

| Machine | Operating system | Details |
| --- | --- | --- |
| Windows domain controller | Windows Server 2019 or Windows Server 2022 | |
| Report development and deployment (`WIN19`) | Windows Server 2019, running Visual Studio 2019 | - Report development and deployment<br /><br />- File share services to serve as a repository for demand driven or scheduled report output |
| SQL Server Reporting Services (`WIN22`) | Windows Server 2022, running a supported version of Power BI Report Server (PBIRS) <sup>1</sup> | |
| Developer machine | Windows 11 client, running SQL Server Management Studio (SSMS) | |
| SQL Server 2019 (`rhel8test`) | Red Hat Enterprise Linux (RHEL) 8.x Server, running [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] with the latest CU | |

### Configure accounts

| Account name | Details |
| --- | --- |
| `CORPNET\cluadmin` | Global user account. Local Administrator account on all Windows servers except for the domain controller. |
| `CORPNET\pbirsservice` | PBIRS service account |
| `CORPNET\linuxservice` | [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service account (created just for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux environment) |
| `CORPNET\reportuser` | Global user account used to simulate a normal user of PBIRS |

Separate servers and separate accounts are used in this example scenario to ensure that Kerberos delegation is functioning properly (that is, double-hop scenarios are being handled).

## SQL Server on Linux configuration

Before proceeding with the configuration (or reconfiguration) of PBIRS to use [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux as the backend to host the Report Server catalog databases, ensure that the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux instance has been joined to the domain.

You can install and configure **adutil**, and join the domain, following the instructions in [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](sql-server-linux-ad-auth-adutil-tutorial.md).

> [!NOTE]
> For information about specific packages on RHEL 8, see [Connecting RHEL systems directly to AD using SSSD](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/8/html-single/integrating_rhel_systems_directly_with_windows_active_directory/index#discovering-and-joining-an-ad-domain-using-sssd_connecting-directly-to-ad).

## SQL Server service principal names (SPNs)

Prior to installing and configuring PBIRS, you must configure the required SPNs on the `CORPNET` domain. A user with Domain Admin privileges can be used in this case, but any user with enough permissions to create SPNs is sufficient. After the SPN creation, the accounts need to be configured to use Kerberos constrained delegation.

Here are the minimum required SPNs for this scenario:

- Using an Administrative command prompt, create the SPN for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux service account. This instance is using the default port of 1433:

   ```console
   setspn -S MSSQLSvc/rhel8test:1433 CORPNET\linuxservice
   setspn -S MSSQLSvc/rhel8test.CORPNET.CONTOSO.COM:1433 CORPNET\linuxservice
   ```

- The next two SPNs are for the Power BI Report Server service account.

   ```console
   setspn -S HTTP/WIN22.CORPNET.CONTOSO.COM CORPNET\pbirsservice
   setspn -S HTTP/WIN22 CORPNET\pbirsservice
   ```

To handle the Kerberos requirements for forwarding Kerberos tickets, when operating within a constrained delegation implementation, we configure delegation using Microsoft's extension to the MIT Kerberos standard, as specified in [RFC 4120](https://www.rfc-editor.org/rfc/rfc4120.txt), and use the [Service for User to Proxy](/openspecs/windows_protocols/ms-sfu/bde93b0e-f3c9-4ddf-9f44-e1453be7af5a) (S4U2proxy). This mechanism allows the PBIRS service and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to obtain service tickets to other specified services on behalf of a user.

For example, when the `reportuser` authenticates with the PBIRS server's web interface to view a report, the report executes and has to access data from a data source like a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] table. The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service must obtain the `reportuser` Kerberos service ticket, which was granted during the authentication process to the PBIRS server. The S4U2proxy extension provides the necessary protocol transition in order to pass the required credentials without having to forward the user's TGT (ticket granting ticket) or the user's session key.

In order to achieve this, the PBIRS service account (`pbirsservice` in this example) and the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service account (`linuxservice` in this example) need to be granted the **Trusted To Authenticate for Delegation** right in the domain. There are multiple ways to grant this right (that is, ADSI Edit, Computer and Users UI, etc.). We use an elevated PowerShell command in this example:

- Get the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service account and set it to allow delegation. This step enables not only Kerberos delegation, but the S4U2proxy (for protocol transition) delegation on the account. The final two cmdlets apply the delegation authority to specific resources in the domain, the SPNs for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance.

  ```powershell
  Get-ADUser -Identity linuxservice | Set-ADAccountControl -TrustedToAuthForDelegation $True
  Set-ADUser -Identity linuxservice -Add @{'msDS-AllowedToDelegateTo'=@('MSSQLSvc/rhel8test.CORPNET.CONTOSO.COM:1433')}
  Set-ADUser -Identity linuxservice -Add @{'msDS-AllowedToDelegateTo'=@('MSSQLSvc/rhel8test:1433')}
  ```

- Get the Power BI Report Server service account and set it to allow delegation. This step enables not only Kerberos delegation, but the S4U2proxy (for protocol transition) delegation on the account. The final two cmdlets apply the delegation authority to specific resources in the domain, the SPNs for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and PBIRS server.

  ```powershell
  Get-ADUser -Identity pbirsservice | Set-ADAccountControl -TrustedToAuthForDelegation $True
  Set-ADUser -Identity pbirsservice -Add @{'msDS-AllowedToDelegateTo'=@('MSSQLSvc/rhel8test.CORPNET.CONTOSO.COM:1433')}
  Set-ADUser -Identity pbirsservice -Add @{'msDS-AllowedToDelegateTo'=@('MSSQLSvc/rhel8test:1433')}
  Set-ADUser -Identity pbirsservice -Add @{'msDS-AllowedToDelegateTo'=@('HTTP/Win22.CORPNET.CONTOSO.COM')}
  Set-ADUser -Identity pbirsservice -Add @{'msDS-AllowedToDelegateTo'=@('HTTP/Win22')}
  ```

## Power BI Report Server (PBIRS)

PBIRS should be installed in *configuration only* mode.

Immediately after installing PBIRS, you must configure it to support Kerberos authentication. PBIRS by default only supports NTLM authentication. During the installation process, you need to update one of the PBIRS configuration files before completing the PBIRS configuration process, either in the UI, or via the command line. If you use an existing PBIRS installation, you still need to perform the edits, and the PBIRS service must be restarted to take effect. The configuration file is the `rsreportserver.config`. It is in path where PBIRS was installed. For example, on a default installation of PBIRS, the file is in the following location:

`C:\Program Files\Microsoft SQL Server Reporting Services\SSRS\ReportServer`

This XML file can be edited in any text editor. Remember to make a copy of the file before editing. Once you have opened the file, search for the `AuthenticationTypes` tag within the XML document, and add the `RSWindowsNegotiate` and `RSWindowsKerberos` attributes ahead of the `RSWindowsNTLM` attribute. For example:

```xml
<Authentication>
<AuthenticationTypes>
    <RSWindowsNegotiate/>
    <RSWindowsKerberos/>
    <RSWindowsNTLM/>
</AuthenticationTypes>
```

This step is required, because [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux only supports SQL and Kerberos authentication.

> [!NOTE]  
> We only need to include the `RSWindowsKerberos` attribute, but using `RSWindowsNegotiate` is helpful if standardizing PBIRS configuration files across a fleet of servers that support a mixture of Windows and Linux SQL Server instances is desired.

## PBIRS UI configuration

Once the PBIRS service has been restarted after the configuration file edits have been completed, you can proceed with the remaining PBIRS configuration options such as setting the domain based service account and connecting to the remote [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux instance.

The PBIRS service account should appear within the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance with appropriate permissions. You can check the permissions in SQL Server Management Studio (SSMS). In Object Explorer, navigate to **Security > Logins**, right-click on the `CORPNET\pbirsservice` account, and select **Properties**. The permissions are visible on the **User Mapping** page.

Finally, we can add the `reportuser` as a login on the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for testing purposes. In this case, we took the easy button and added the user to the **db_datareader** role within two user databases: `AdventureWorks` and `AdventureWorksDW`.

## After reports have been deployed

If you need to set up report subscriptions after reports are deployed, it is a good practice to configure embedded credentials in the PBIRS data sources. All credential options work properly, except for the use of embedded credentials configured with the **impersonate the user viewing the report** option. This step fails when using Windows credentials, because of a limitation within the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux implementation that makes impersonation more difficult.

## Related content

- [Active Directory authentication for SQL Server on Linux](sql-server-linux-active-directory-auth-overview.md)
- [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md)
- [Reporting Services Tools](../reporting-services/tools/reporting-services-tools.md)
