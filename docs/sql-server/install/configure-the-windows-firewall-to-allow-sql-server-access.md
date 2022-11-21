---
title: "Configure the Windows Firewall to allow SQL Server access"
description: Learn how to configure the Windows firewall to allow access to an instance of the SQL Server through the firewall.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/17/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: contperf-fy21q3
helpviewer_keywords:
  - "Windows Firewall ports"
  - "WMI firewall ports"
  - "Windows Firewall [Database Engine]"
  - "firewall systems, configuring"
  - "advfirewall"
  - "firewall systems"
  - "rules firewall"
  - "firewall systems, overview and port list"
  - "1433 TCP port"
  - "portopening using netsh"
  - "ports [SQL Server], TCP"
  - "netsh to open firewall ports"
---
# Configure the Windows Firewall to allow SQL Server access

[!INCLUDE [SQL Server-Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Firewall systems help prevent unauthorized access to computer resources. If a firewall is turned on but not correctly configured, attempts to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might be blocked.

To access an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through a firewall, you must configure the firewall on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The firewall is a component of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows. You can also install a firewall from another vendor. This article discusses how to configure the Windows Firewall, but the basic principles apply to other firewall programs.

> [!NOTE]  
> This article provides an overview of firewall configuration and summarizes information of interest to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] administrator. For more information about the firewall and for authoritative firewall information, see the firewall documentation, such as [Windows Firewall security deployment guide](/windows/security/threat-protection/windows-firewall/windows-firewall-with-advanced-security-deployment-guide).

Users familiar with managing the  **Windows Firewall**, and know which firewall settings they want to configure can move directly to the more advanced articles:

- [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)
- [Configure the Windows Firewall to Allow Analysis Services Access](/analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access)
- [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)

## Basic firewall information

Firewalls work by inspecting incoming packets, and comparing them against the following set of rules:

- The packet meets the standards dictated by the rules, then the firewall passes the packet to the TCP/IP protocol for more processing.
- The packet doesn't meet the standards specified by the rules.
  - The firewall then discards the packet.- If logging is enabled, an entry is created in the firewall logging file.

The list of allowed traffic is populated in one of the following ways:

- **Automatically**: When a computer with a firewall enabled starts communication, the firewall creates an entry in the list so that the response is allowed. The response is considered solicited traffic, and there's nothing that needs to be configured.

- **Manually**: An administrator configures exceptions to the firewall. It allows either access to specified programs or ports on your computer. In this case, the computer accepts unsolicited incoming traffic when acting as a server, a listener, or a peer. The configuration  must be completed to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

Choosing a firewall strategy is more complex than just deciding if a given port should be open or closed. When designing a firewall strategy for your enterprise, make sure you consider all the rules and configuration options available to you. This article doesn't review all the possible firewall options. We recommend you review the following documents:

- [Windows Firewall Deployment Guide](/windows/security/threat-protection/windows-firewall/windows-firewall-with-advanced-security-deployment-guide)  
- [Windows Firewall Design Guide](/windows/security/threat-protection/windows-firewall/windows-firewall-with-advanced-security-design-guide)  
- [Introduction to Server and Domain Isolation](/windows/security/threat-protection/windows-firewall/domain-isolation-policy-design)

## Default firewall settings

The first step in planning your firewall configuration is to determine the current status of the firewall for your operating system. If the operating system was upgraded from a previous version, the earlier firewall settings may have been preserved. The Group Policy or Administrator can change the firewall settings in the domain.

> [!NOTE]  
> Turning on the firewall will affect other programs that access this computer, such as file and print sharing, and remote desktop connections. Administrators should consider all applications that are running on the computer before adjusting the firewall settings.

## Programs to configure the firewall

Configure the Windows Firewall settings with either **Microsoft Management Console** or **netsh**.

- **Microsoft Management Console (MMC)**

  The Windows Firewall with Advanced Security MMC snap-in lets you configure more advanced firewall settings. This snap-in presents most of the firewall options in an easy-to-use manner, and presents all firewall profiles. For more information, see [Using the Windows Firewall with Advanced Security Snap-in](#use-the-windows-firewall-with-advanced-security-snap-in) later in this article.

- **netsh**

  The **netsh.exe** is an Administrator tool to configure and monitor Windows-based computers at a command prompt or using a batch file. By using the **netsh** tool, you can direct the context commands you enter to the appropriate helper, and the helper does the command. A helper is a Dynamic Link Library (.dll) file that extends the functionality. The helper provides: configuration, monitoring, and support for one or more services, utilities, or protocols for the **netsh** tool.

  All operating systems that support [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] have a firewall helper. [!INCLUDE[winserver2008](../../includes/winserver2008-md.md)] also has an advanced firewall helper called **advfirewall**. Many of the configuration options described can be configured by using **netsh**. For example, run the following script at a command prompt to open TCP port 1433:

  ```console
  netsh firewall set portopening protocol = TCP port = 1433 name = SQLPort mode = ENABLE scope = SUBNET profile = CURRENT
  ```

  A similar example using the Windows Firewall for Advanced Security helper:

  ```console
  netsh advfirewall firewall add rule name = SQLPort dir = in protocol = tcp action = allow localport = 1433 remoteip = localsubnet profile = DOMAIN
  ```

  For more information about **netsh**, see the following links:

  - [Netsh Command Syntax, Contexts, and Formatting](/windows-server/networking/technologies/netsh/netsh-contexts)
  - [How to use the "netsh advfirewall firewall" context instead of the "netsh firewall" context to control Windows Firewall behavior in Windows Server 2008 and in Windows Vista](https://support.microsoft.com/kb/947709)

- **PowerShell**

  See the following example to open TCP port 1433 and UDP port 1434 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] default instance, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service:

  ```powershell
  New-NetFirewallRule -DisplayName "SQLServer default instance" -Direction Inbound -LocalPort 1433 -Protocol TCP -Action Allow
  New-NetFirewallRule -DisplayName "SQLServer Browser service" -Direction Inbound -LocalPort 1434 -Protocol UDP -Action Allow
  ```

  For more examples, see [New-NetFirewallRule](/powershell/module/netsecurity/new-netfirewallrule).

- **For Linux**

  On Linux, you also need to open the ports associated with the services you need access to. Different distributions of Linux and different firewalls have their own procedures. For two examples, see [SQL Server on Red Hat](../../linux/quickstart-install-connect-red-hat.md), and [SQL Server on SUSE](../../linux/quickstart-install-connect-suse.md).

## Ports used by SQL Server

The following tables can help you identify the ports being used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

### Ports used by the Database Engine

By default, the typical ports used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and associated database engine services are: TCP **1433**, **4022**, **135**, **1434**, UDP **1434**. The table below explains these ports in greater detail. A named instance uses [Dynamic ports](#dynamic-ports).

The following table lists the ports that are frequently used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

|Scenario|Port|Comments|
| --- | --- | --- |
|Default instance running over TCP|TCP port 1433|The most common port allowed through the firewall. It applies to routine connections to the default installation of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], or a named instance that is the only instance running on the computer. (Named instances have special considerations. See [Dynamic ports](#dynamic-ports) later in this article.)|
|Named instances with default port|The TCP port is a dynamic port determined at the time the [!INCLUDE[ssDE](../../includes/ssde-md.md)] starts.|See the discussion below in the section [Dynamic ports](#dynamic-ports). UDP port 1434 might be required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service when you're using named instances.|
| Named instances with fixed port |The port number configured by the administrator.|See the discussion below in the section [Dynamic ports](#dynamic-ports).|
|Dedicated Admin Connection|TCP port 1434 for the default instance. Other ports are used for named instances. Check the error log for the port number.|By default, remote connections to the Dedicated Administrator Connection (DAC) aren't enabled. To enable remote DAC, use the Surface Area Configuration facet. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md).|
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service|UDP port 1434|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] browser service listens for incoming connections to a named instance.<br /><br />The service provides the client the TCP port number that corresponds to that named instance. Normally the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is started whenever named instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are used. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service isn't required if the client is configured to connect to the specific port of the named instance.|  
|Instance with HTTP endpoint.|Can be specified when an HTTP endpoint is created. The default is TCP port 80 for CLEAR_PORT traffic and 443 for SSL_PORT traffic.|Used for an HTTP connection through a URL.|
|Default instance with HTTPS endpoint |TCP port 443|Used for an HTTPS connection through a URL. HTTPS is an HTTP connection that uses Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL).|
|[!INCLUDE[ssSB](../../includes/sssb-md.md)]|TCP port 4022. To verify the port used, execute the following query:<br /><br />`SELECT name, protocol_desc, port, state_desc`<br /><br />`FROM sys.tcp_endpoints`<br /><br />`WHERE type_desc = 'SERVICE_BROKER'`|There's no default port for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssSB](../../includes/sssb-md.md)], Books Online examples use the conventional configuration.|
|Database Mirroring|Administrator chosen port. To determine the port, execute the following query:<br /><br />`SELECT name, protocol_desc, port, state_desc FROM sys.tcp_endpoints`<br /><br />`WHERE type_desc = 'DATABASE_MIRRORING'`|There's no default port for database mirroring however Books Online examples use TCP port 5022 or 7022. It's important to avoid interrupting an in-use mirroring endpoint, especially in high-safety mode with automatic failover. Your firewall configuration must avoid breaking quorum. For more information, see [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md).|
|Replication|Replication connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use the typical regular [!INCLUDE[ssDE](../../includes/ssde-md.md)] ports (TCP port 1433 is the default instance)<br /><br />Web synchronization and FTP/UNC access for replication snapshot require more ports to be opened on the firewall. To transfer initial data and schema from one location to another, replication can use FTP (TCP port 21), or sync over HTTP (TCP port 80) or File Sharing. File sharing uses UDP port 137 and 138, and TCP port 139 if used along with NetBIOS. File Sharing uses TCP port 445.|For sync over HTTP, replication uses the IIS endpoint (configurable; port 80 default), but the IIS process connects to the backend [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through the standard ports (1433 for the default instance.<br /><br />During Web synchronization using FTP, the FTP transfer is between IIS and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] publisher, not between subscriber and IIS.|
|[!INCLUDE[tsql](../../includes/tsql-md.md)] debugger|TCP port 135<br /><br />See [Special Considerations for Port 135](#special-considerations-for-port-135)<br /><br />The [IPsec](#ipsec) exception might also be required.|If using [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], on the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] host computer, you must also add **Devenv.exe** to the Exceptions list and open TCP port 135.<br /><br />If using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], on the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] host computer, you must also add **ssms.exe** to the Exceptions list and open TCP port 135. For more information, see [Configure firewall rules before running the TSQL Debugger](../../ssms/scripting/configure-firewall-rules-before-running-the-tsql-debugger.md).|

For step-by-step instructions to configure the Windows Firewall for the [!INCLUDE[ssDE](../../includes/ssde-md.md)], see [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).

#### Dynamic ports

By default, named instances (including [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]) use dynamic ports.  means each time [!INCLUDE[ssDE](../../includes/ssde-md.md)] starts, it identifies an available port and uses that port number. If the named instance is the only instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] installed, it will probably use TCP port 1433. If other instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] are installed, it will probably use a different TCP port. Because the port selected might change every time that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is started, it's difficult to configure the firewall to enable access to the correct port number. If a firewall is used, we recommend reconfiguring the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to use the same port number every time. A fixed port or a static port is recommended. For more information, see [Configure a Server to Listen on a Specific TCP Port (SQL Server Configuration Manager)](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).

An alternative to configuring a named instance to listen on a fixed port is to create an exception in the firewall for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] program such as **sqlservr.exe** (for the [!INCLUDE[ssDE](../../includes/ssde-md.md)]). The port number won't appear in the **Local Port** column of the **Inbound Rules** page when you're using the Windows Firewall with Advanced Security MMC snap-in. It can be difficult to audit which ports are open. Another consideration is that a service pack or cumulative update can change the path to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable file and invalidate the firewall rule.

To add an exception for SQL Server using Windows Firewall with Advanced Security, see [Use the Windows Firewall with Advanced Security snap-in](#use-the-windows-firewall-with-advanced-security-snap-in) later in this article.

### Ports used by Analysis Services

By default, the typical ports used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Analysis Services and associated services are: TCP **2382**, **2383**, **80**, **443**. The table below explains these ports in greater detail.

The following table lists the ports that are frequently used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].

|Feature|Port|Comments|
|-------------|----------|--------------|
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|TCP port 2383 for the default instance|The standard port for the default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service|TCP port 2382 only needed for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] named instance|Client connection requests for a named instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that don't specify a port number are directed to port 2382, the port on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser then redirects the request to the port that the named instance uses.|
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configured for use through IIS/HTTP<br /><br />(The PivotTable® Service uses HTTP or HTTPS)|TCP port 80|Used for an HTTP connection through a URL.|
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] configured for use through IIS/HTTPS<br /><br />(The PivotTable® Service uses HTTP or HTTPS)|TCP port 443|Used for an HTTPS connection through a URL. HTTPS is an HTTP connection that uses TLS.|

If users access [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] through IIS and the Internet, you must open the port on which IIS is listening. Next, specify port in the client connection string. In this case, no ports have to be open for direct access to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The default port 2389, and port 2382, should be restricted together with all other ports that aren't required.

For step-by-step instructions to configure the Windows Firewall for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [Configure the Windows Firewall to Allow Analysis Services Access](/analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access).

### Ports used By Reporting Services

By default, the typical ports used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Reporting Services and associated services are: TCP **80**, **443**. The table below explains these ports in greater detail.

The following table lists the ports that are frequently used by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].

|Feature|Port|Comments|
|-------------|----------|--------------|
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Web Services|TCP port 80|Used for an HTTP connection to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] through a URL. We recommend that you don't use the preconfigured rule **World Wide Web Services (HTTP)**. For more information, see the [Interaction with Other Firewall Rules](#interaction-with-other-firewall-rules) section below.|
|[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configured for use through HTTPS|TCP port 443|Used for an HTTPS connection through a URL. HTTPS is an HTTP connection that uses TLS. We recommend that you don't use the preconfigured rule **Secure World Wide Web Services (HTTPS)**. For more information, see the [Interaction with Other Firewall Rules](#interaction-with-other-firewall-rules) section below.|

When [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] connects to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] or [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you must also open the appropriate ports for those services. For step-by-step instructions to configure the Windows Firewall for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).

### Ports used by Integration Services

The following table lists the ports that are used by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service.

|Feature|Port|Comments|
|-------------|----------|--------------|
|[!INCLUDE[msCoName](../../includes/msconame-md.md)] remote procedure calls (MS RPC)<br /><br />Used by the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] runtime.|TCP port 135<br /><br />See [Special Considerations for Port 135](#special-considerations-for-port-135)|The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service uses DCOM on port 135. The Service Control Manager uses port 135 to do tasks such as starting and stopping the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service and transmitting control requests to the running service. The port number can't be changed.<br /><br />This port is only required to be open if you're connecting to a remote instance of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service from [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or a custom application.|

For step-by-step instructions to configure the Windows Firewall for [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Integration Services Service &#40;SSIS Service&#41;](/previous-versions/sql/sql-server-2012/ms137861(v=sql.110)).

### Other ports and services

The following table lists ports and services that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might depend on.

|Scenario|Port|Comments|
|--------------|----------|--------------|
|Windows Management Instrumentation<br /><br />For more information about **Windows Management Instrumentation (WMI)**, see [WMI Provider for Configuration Management Concepts](../../relational-databases/wmi-provider-configuration/wmi-provider-for-configuration-management.md)|WMI runs as part of a shared service host with ports assigned through DCOM. WMI might be using TCP port 135.<br /><br />See [Special Considerations for Port 135](#special-considerations-for-port-135)|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager uses WMI to list and manage services. We recommend that you use the preconfigured rule group **Windows Management Instrumentation (WMI)**. For more information, see the [Interaction with Other Firewall Rules](#interaction-with-other-firewall-rules) section below.|
|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC)|TCP port 135<br /><br />See [Special Considerations for Port 135](#special-considerations-for-port-135)|If your application uses distributed transactions, you might have to configure the firewall to allow [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) traffic to flow between separate MS DTC instances, and between the MS DTC and resource managers such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. We recommend that you use the preconfigured **Distributed Transaction Coordinator** rule group.<br /><br />When a single shared MS DTC is configured for the entire cluster in a separate resource group, you should add sqlservr.exe as an exception to the firewall.|
|The browse button in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] uses UDP to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service. For more information, see [SQL Server Browser Service &#40;Database Engine and SSAS&#41;](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md).|UDP port 1434|UDP is a connectionless protocol.<br /><br />The firewall has a setting ([UnicastResponsesToMulticastBroadcastDisabled Property of the INetFwProfile Interface](/windows/win32/api/netfw/nf-netfw-inetfwprofile-get_unicastresponsestomulticastbroadcastdisabled)) which controls the behavior of the firewall and unicast responses to a broadcast (or multicast) UDP request.  It has two behaviors:<br /><br />If the setting is TRUE, no unicast responses to a broadcast are permitted at all. Enumerating services will fail.<br /><br />If the setting is FALSE (default), unicast responses are permitted for 3 seconds. The length of time isn't configurable. In a congested or high-latency network, or for heavily loaded servers, tries to enumerate instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might return a partial list, which might mislead users.|
|<a id="ipsec"></a> IPsec traffic|UDP port 500 and UDP port 4500|If the domain policy requires network communications to be done through IPsec, you must also add UDP port 4500 and UDP port 500 to the exception list. IPsec is an option using the **New Inbound Rule Wizard** in the Windows Firewall snap-in. For more information, see [Using the Windows Firewall with Advanced Security Snap-in](#use-the-windows-firewall-with-advanced-security-snap-in) below.|
|Using Windows Authentication with Trusted Domains|Firewalls must be configured to allow authentication requests.|For more information, see [How to configure a firewall for domains and trusts](https://support.microsoft.com/kb/179442/).|
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Clustering|Clustering requires extra ports that aren't directly related to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|For more information, see [Enable a network for cluster use](/previous-versions/windows/it-pro/windows-server-2003/cc728293(v=ws.10)).|
|URL namespaces reserved in the HTTP Server API (HTTP.SYS)|Probably TCP port 80, but can be configured to other ports. For general information, see [Configuring HTTP and HTTPS](/dotnet/framework/wcf/feature-details/configuring-http-and-https).|For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] specific information about reserving an HTTP.SYS endpoint using HttpCfg.exe, see [About URL Reservations and Registration  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/about-url-reservations-and-registration-ssrs-configuration-manager.md).|

## Special considerations for port 135

When you use RPC with TCP/IP or with UDP/IP as the transport, inbound ports are dynamically assigned to system services as required. TCP/IP and UDP/IP ports that are larger than port 1024 are used. The ports are referred to as "random RPC ports." In these cases, RPC clients rely on the RPC endpoint mapper to tell them which dynamic ports were assigned to the server. For some RPC-based services, you can configure a specific port instead of letting RPC assign one dynamically. You can also restrict the range of ports that RPC dynamically assigns to a small range, independent of the service. Because port 135 is used for many services, it's frequently attacked by malicious users. When opening port 135, consider restricting the scope of the firewall rule.

For more information about port 135, see the following references:

- [Service overview and network port requirements for the Windows Server system](https://support.microsoft.com/kb/832017)
- [Remote procedure call (RPC)](/previous-versions/ms950395(v=msdn.10))
- [How to configure RPC dynamic port allocation to work with firewalls](https://support.microsoft.com/kb/154596/)

## Interaction with other firewall rules

The Windows Firewall uses rules and rule groups to establish its configuration. Each rule or rule group is associated with a particular program or service, and that program or service might modify or delete that rule without your knowledge. For example, the rule groups **World Wide Web Services (HTTP)** and **World Wide Web Services (HTTPS)** are associated with IIS. Enabling those rules will open ports 80 and 443, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that depend on ports 80 and 443 will function if those rules are enabled. However, administrators configuring IIS might modify or disable those rules. If you're using port 80 or port 443 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should create your own rule or rule group that maintains your preferred port configuration independently of the other IIS rules.

The [Windows Firewall with Advanced Security MMC snap-in](#use-the-windows-firewall-with-advanced-security-snap-in) allows any traffic that matches any applicable allow rule. So if there are two rules that both apply to port 80 (with different parameters). Traffic that matches either rule will be permitted. So if one rule allows traffic over port 80 from local subnet and one rule allows traffic from any address, the net effect is that all traffic to port 80 is independent of the source. To effectively manage access to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], administrators should periodically review all firewall rules enabled on the server.

## Overview of firewall profiles

Firewall profiles  are used by the operating systems to identify and remember each of the networks by: connectivity, connections, and category.

There are three network location types in Windows Firewall with Advanced Security:

- **Domain**: Windows can authenticate access to the domain controller for the domain to which the computer is joined.
- **Public**: Other than domain networks, all networks are initially categorized as public. Networks that represent direct connections to the Internet or are in public locations, such as airports and coffee shops should be left public.
- **Private**: A network identified by a user or application as private. Only trusted networks should be identified as private networks. Users will likely want to identify home or small business networks as private.

The administrator can create a profile for each network location type, with each profile containing different firewall policies. Only one profile is applied at any time. Profile order is applied as follows:

1. The domain profile is applied if all interfaces are authenticated to the domain controller where the computer is a member.
1. If all interfaces are either authenticated to the domain controller or are connected to networks that are classified as private network locations, the private profile is applied.
1. Otherwise, the public profile is applied.

Use the Windows Firewall with Advanced Security MMC snap-in to view and configure all firewall profiles. The **Windows Firewall** item in Control Panel only configures the current profile.

## Additional firewall settings using the Windows Firewall item in Control Panel

The added firewall can restrict the opening of the port to incoming connections from specific computers or  local subnet. Limit the scope of the port opening to reduce how much your computer is exposed to malicious users.

> [!NOTE]  
> Using the **Windows Firewall** item in Control Panel only configures the current firewall profile.

### Change the scope of a firewall exception using the Windows Firewall item in Control Panel

1. In the **Windows Firewall** item in Control Panel, select a program or port on the **Exceptions** tab, and then select **Properties** or **Edit**.

1. In the **Edit a Program** or **Edit a Port** dialog box, select **Change Scope**.

1. Choose one of the following options:

   - **Any computer (including computers on the Internet)**: Not recommended. Any computer that can address your computer to connect to the specified program or port. This setting might be necessary to allow information to be presented to anonymous users on the internet, but increases your exposure to malicious users. Enabling this setting an allow Network Address Translation (NAT) traversal, such as the Allow edge traversal option will increase exposure.

   - **My network (subnet) only**: A more secure setting than **Any computer**. Only computers on the local subnet of your network can connect to the program or port.

   - **Custom list**: Only computers that have the IP addresses listed can connect. A secure setting can be more secure than **My network (subnet) only**, however, client computers using DHCP can occasionally change their IP address; will disable the ability to connect. Another computer, which you had not intended to authorize, might accept the listed IP address and connect to it. The **Custom list** is appropriate for listing other servers that are configured to use a fixed IP address.

     IP addresses can be spoofed by an intruder. Restricting firewall rules are only as strong as your network infrastructure.

## Use the Windows Firewall with Advanced Security snap-in

Advanced firewall settings can be configured by using the Windows Firewall with Advanced Security MMC snap-in. The snap-in includes a rule wizard and settings that aren't available in the **Windows Firewall** item in Control Panel. These settings include:

- Encryption settings
- Services restrictions
- Restricting connections for computers by name
- Restricting connections to specific users or profiles
- Edge traversal allowing traffic to bypass Network Address Translation (NAT) routers
- Configuring outbound rules
- Configuring security rules
- Requiring IPsec for incoming connections

### Create a new firewall rule using the New Rule wizard

1. On the Start menu, select **Run**, type `WF.msc`, and then select **OK**.
1. In the **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then select **New Rule**.
1. Complete the **New Inbound Rule Wizard** using the settings that you want.

### Add a program exception for the SQL Server executable

1. From the start menu, type *wf.msc*. Press Enter or select the search result wf.msc to open **Windows Defender Firewall with Advanced Security**.
1. In the left pane, select **Inbound rules**.
1. In the right pane, under **Actions**, select **New rule...**. **New Inbound Rule Wizard** opens.
1. On **Rule type**, select **Program**. Select **Next**.
1. On **Program**, select **This program path**. Select **Browse** to locate your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The program is called `sqlservr.exe`. It's normally located at:

   `C:\Program Files\Microsoft SQL Server\MSSQL<VersionNumber>.<InstanceName>\MSSQL\Binn\sqlservr.exe`

   Select **Next**.

1. On **Action**, select **Allow the connection**. Select **Next**.
1. On **Profile**, include all three profiles. Select **Next**.
1. On **Name**, type a name for the rule. Select **Finish**.

For more information about endpoints, see:

- [Configure the Database Engine to Listen on Multiple TCP Ports](../../database-engine/configure-windows/configure-the-database-engine-to-listen-on-multiple-tcp-ports.md)
- [Endpoints Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/endpoints-catalog-views-transact-sql.md)

## Troubleshoot firewall settings

The following tools and techniques can be useful in troubleshooting firewall issues:

- The effective port status is the union of all rules related to the port. It can be helpful to review all the rules that cite the port number, when trying to block access to a port. Review the rules with the Windows Firewall with Advanced Security MMC snap-in and sort the inbound and outbound rules by port number.

- Review the ports that are active on the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The review process includes [verifying which TCP/IP ports are listening](#list-which-tcpip-ports-are-listening) and also verifying the status of the ports.

- The **PortQry** utility can be used to report the status of TCP/IP ports as listening, not listening, or filtered.
(The utility may not receive response from the port if it has a filtered status.)
The **PortQry** utility is available for download from the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=17148).

### List which TCP/IP ports are listening

To verify which ports are listening, display active TCP connections and IP statistics use the **netstat** command-line utility.

1. Open the Command Prompt window.

1. At the command prompt, type `netstat -n -a`.

   The `-n` switch instructs **netstat** to numerically display the address and port number of active TCP connections. The `-a` switch instructs **netstat** to display the TCP and UDP ports on which the computer is listening.

## See also

- [Service overview and network port requirements for the Windows Server system](https://support.microsoft.com/kb/832017)
- [How to: Configure Firewall Settings (Azure SQL Database)](/azure/azure-sql/database/firewall-configure)
