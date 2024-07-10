---
title: "Register a Service Principal Name (SPN) for a report server"
description: Learn how to create an SPN for the Report Server service if it runs as a domain user, if your network uses Kerberos for authentication.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/19/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Register a Service Principal Name (SPN) for a report server

If you're deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in a network that uses the Kerberos protocol for mutual authentication, you must create a Service Principal Name (SPN) for the Report Server service. You must create the name if you configure it to run as a domain user account.

## About SPNs

An SPN is a unique identifier for a service on a network that uses Kerberos authentication. It consists of a service class, a host name, and sometimes a port. HTTP SPNs don't require a port. On a network that uses Kerberos authentication, an SPN for the server must be registered under either a built-in computer account (such as NetworkService or LocalSystem) or user account. SPNs are registered for built-in accounts automatically. However, when you run a service under a domain user account, you must manually register the SPN for the account you want to use.

To create an SPN, you can use the **SetSPN** command line utility. For more information, see:

- [`SetSPN`](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/cc731241(v=ws.11))

You must be a domain administrator to run the utility on the domain controller.

## Syntax

When you manipulate SPNs with the `SetSPN`, the SPN must be entered in the correct format. The format of an HTTP SPN is `http/host`. The command syntax for using `SetSPN` utility to create an SPN for the report server resembles the following example:

```
Setspn -s http/<computer-name>.<domain-name> <domain-user-account>
```

`SetSPN` is available with Windows Server. The `-s` argument adds an SPN after validating no duplicate exists.

> [!NOTE]
> `-s` is available in Windows Server starting with Windows Server 2008.

`HTTP` is the service class. The Report Server Web service runs in `HTTP.SYS`. A by-product of creating an SPN for `HTTP` is that all Web applications on the same computer that run in `HTTP.SYS` (including applications hosted in IIS) are granted tickets based on the domain user account. If those services run under a different account, the authentication requests fail. To avoid this problem, be sure to configure all HTTP applications to run under the same account, or consider creating host headers for each application and then creating separate SPNs for each host header. When you configure host headers, DNS changes are required regardless of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration.

The values that you specify for `<computername>` and `<domainname>` identify the unique network address of the computer that hosts the report server. This value can be a local host name or a fully qualified domain name (FQDN). If you only have one domain, you can omit `<domainname>` from your command line. `<domain-user-account>` is the user account under which the Report Server service runs and for which the SPN must be registered.

## Register an SPN for a report server service running as a domain user

1. Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and configure the Report Server service to run as a domain user account. Users aren't able to connect to the report server until you complete the following steps.

1. Sign in to the domain controller as domain administrator.

1. Open a command prompt.

1. Copy the following command, replacing placeholder values with actual values that are valid for your network:

    ```
    Setspn -s http/<computer-name>.<domain-name> <domain-user-account>
    ```

    For example: `Setspn -s http/MyReportServer.MyDomain.com MyDomainUser`

1. Run the command.

1. Open the `RsReportServer.config` file and locate the `<AuthenticationTypes>` section.

1. Add `<RSWindowsNegotiate>` as the first entry in this section to enable Kerberos.

## Related content

- [Configure the Report Server Service Account (Report Server Configuration Manager)](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)
- [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)
