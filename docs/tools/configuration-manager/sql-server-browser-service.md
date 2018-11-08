---
title: "SQL Server Browser Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.browseservers.local.f1"
  - "sql13.swb.browseservers.network.f1"
helpviewer_keywords: 
  - "services [SQL Server], security"
  - "SQL Browser service (See SQL Server Browser Service)"
  - "Browser Service"
  - "SQL Server Browser service"
ms.assetid: 3cc00d3a-487c-4cd9-a155-655f02485fa0
author: "stevestein"
ms.author: "sstein"
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# SQL Server Browser Service
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]Browser program runs as a Windows service. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens for incoming requests for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources and provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the computer. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser contributes to the following actions:  
  
-   Browsing a list of available servers  
  
-   Connecting to the correct server instance  
  
-   Connecting to dedicated administrator connection (DAC) endpoints  
  
 For each instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssAS](../../includes/ssas-md.md)], the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service (sqlbrowser) provides the instance name and the version number. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser is installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser can be configured during setup or by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. By default, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service starts automatically:  
  
-   When upgrading an installation.  
  
-   When installing on a cluster.  
  
-   When installing a named instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] including all instances of SQL Server Express.  
  
-   When installing a named instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## Background  
 Prior to [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], only one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could be installed on a computer. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listened for incoming requests on port 1433, assigned to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by the official Internet Assigned Numbers Authority (IANA). Only one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use a port, so when [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] introduced support for multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Resolution Protocol (SSRP) was developed to listen on UDP port 1434. This listener service responded to client requests with the names of the installed instances, and the ports or named pipes used by the instance. To resolve limitations of the SSRP system, [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] introduced the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service as a replacement for SSRP.  
  
## How SQL Server Browser Works  
 When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts, if the TCP/IP protocol is enabled for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the server is assigned a TCP/IP port. If the named pipes protocol is enabled, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens on a specific named pipe. This port, or "pipe," is used by that specific instance to exchange data with client applications. During installation, TCP port 1433 and pipe `\sql\query` are assigned to the default instance, but those can be changed later by the server administrator using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. Because only one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use a port or pipe, different port numbers and pipe names are assigned for named instances, including [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)]. By default, when enabled, both named instances and [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] are configured to use dynamic ports, that is, an available port is assigned when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts. If you want, a specific port can be assigned to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When connecting, clients can specify a specific port; but if the port is dynamically assigned, the port number can change anytime [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is restarted, so the correct port number is unknown to the client.  
  
 Upon startup, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser starts and claims UDP port 1434. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser reads the registry, identifies all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the computer, and notes the ports and named pipes that they use. When a server has two or more network cards, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser returns the first enabled port it encounters for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser support ipv6 and ipv4.  
  
 When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients request [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources, the client network library sends a UDP message to the server using port 1434. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser responds with the TCP/IP port or named pipe of the requested instance. The network library on the client application then completes the connection by sending a request to the server using the port or named pipe of the desired instance.  
  
 For information about starting and stopping the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Using SQL Server Browser  
 If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is not running, you are still able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if you provide the correct port number or named pipe. For instance, you can connect to the default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with TCP/IP if it is running on port 1433.  
  
 However, if the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is not running, the following connections do not work:  
  
-   Any component that tries to connect to a named instance without fully specifying all the parameters (such as the TCP/IP port or named pipe).  
  
-   Any component that generates or passes server\instance information that could later be used by other components to reconnect.  
  
-   Connecting to a named instance without providing the port number or pipe.  
  
-   DAC to a named instance or the default instance if not using TCP/IP port 1433.  
  
-   The OLAP redirector service.  
  
-   Enumerating servers in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], Enterprise Manager, or Query Analyzer.  
  
 If you are using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a client-server scenario (for example, when your application is accessing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] across a network), if you stop or disable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service, you must assign a specific port number to each instance and write your client application code to always use that port number. This approach has the following problems:  
  
-   You must update and maintain client application code to ensure it is connecting to the proper port.  
  
-   The port you choose for each instance may be used by another service or application on the server, causing the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to be unavailable.  
  
## Clustering  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser is not a clustered resource and does not support failover from one cluster node to the other. Therefore, in the case of a cluster, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser should be installed and turned on for each node of the cluster. On clusters, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens on IP_ANY.  
  
> [!NOTE]  
>  When listening on IP_ANY, when you enable listening on specific IPs, the user must configure the same TCP port on each IP, because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser returns the first IP/port pair that it encounters.  
  
## Installing, Uninstalling, and Running from the Command Line  
 By default, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser program is installed at C:\Program Files (x86)\Microsoft SQL Server\90\Shared\sqlbrowser.exe.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is uninstalled when the last instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is removed.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser can be started from the command prompt for troubleshooting, by using the **-c** switch:  
  
```  
<drive>\<path>\sqlbrowser.exe -c  
```  
  
## Security  
  
### Account Privileges  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser listens on a UDP port and accepts unauthenticated requests by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Resolution Protocol (SSRP). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser should be run in the security context of a low privileged user to minimize exposure to a malicious attack. The logon account can be changed by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. The minimum user rights for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser are the following:  
  
-   Deny access to this computer from the network  
  
-   Deny logon locally  
  
-   Deny Log on as a batch job  
  
-   Deny Log On Through Terminal Services  
  
-   Log on as a service  
  
-   Read and write the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] registry keys related to network communication (ports and pipes)  
  
### Default Account  
 Setup configures [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser to use the account selected for services during setup. Other possible accounts include the following:  
  
-   Any **domain\local** account  
  
-   The **local service** account  
  
-   The **local system** account (not recommended as has unnecessary privileges)  
  
### Hiding SQL Server  
 Hidden instances are instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that support only shared memory connections. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], set the `HideInstance` flag to indicate that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser should not respond with information about this server instance.  
  
### Using a Firewall  
 To communicate with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service on a server behind a firewall, open UDP port 1434, in addition to the TCP port used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (e.g., 1433). For information about working with a firewall, see "How to: Configure a Firewall for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Access" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Network Protocols and Network Libraries](../../sql-server/install/network-protocols-and-network-libraries.md)  
  
  
