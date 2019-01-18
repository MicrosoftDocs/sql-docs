---
title: Use a DNS forwarder in Analytics Platform System | Microsoft Docs"
description: Use a DNS forwarder to resolve non-appliance DNS names in Analytics Platform System.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Use a DNS Forwarder to Resolve Non-Appliance DNS Names in Analytics Platform System
A DNS forwarder can be configured on the Active Directory Domain Services nodes (**_appliance\_domain_-AD01** and **_appliance\_domain_-AD02**) of your Analytics Platform System appliance to allow scripts and software applications to access external servers.  
  
## <a name="ResolveDNS"></a>Using a DNS Forwarder  
The Analytics Platform System appliance is configured to prevent resolving DNS names of servers that are not in the appliance. Some processes, such as Windows Software Update Services (WSUS), will need to access servers outside of the appliance. To support this usage scenario the Analytics Platform System DNS can be configured to support an external name forwarder that will allow Analytics Platform System hosts and Virtual Machines (VMs) to use external DNS servers to resolve names outside of the appliance. Custom configuration of DNS suffixes is not supported, which means you must use fully qualified domain names to resolve a non-appliance server name.  
  
**To create a DNS forwarder with the DNS GUI**  
  
1.  Log on to the **_appliance\_domain_-AD01** node.  
  
2.  Open the DNS Manager (**dnsmgmt.msc**).  
  
3.  Right-click the name of the server and then click **Properties**.  
  
4.  On the **Advanced** tab, unselect the **Disable recursion (also disables forwarders)** option, and then click **Apply**.)  
  
5.  Click the **Forwarders** tab and then click **Edit**.  
  
6.  Enter the IP address for the external DNS server that will provide the name resolution. The VMs and servers (hosts) in the appliance will connect to external servers by using fully qualified domain names.  
  
7.  Repeat steps 1 - 6 on the **_appliance\_domain_-AD02** node  
  
**To create a DNS forwarder by using Windows PowerShell**  
  
1.  Log on to the **_appliance\_domain_-AD01**node.  
  
2.  Run the following Windows PowerShell script from the **_appliance\_domain_-AD01** node. Before running the Windows PowerShell script, replace IP addresses with the IP addresses of your non-appliance DNS servers.  
  
    ```  
    $DNS=Get-WmiObject -class "MicrosoftDNS_Server"  -Namespace "root\microsoftdns"  
    $DNS.Forwarders = ("xxx.xxx.xxx.xxx", "xxx.xxx.xxx.xxx")  
    $DNS.put()  
    ```  
  
3.  Execute the same command on the **_appliance\_domain_-AD02** node.  
  
## Configuring DNS resolution for WSUS  
SQL Server PDW 2012 provides integrated servicing and patching functionality. SQL Server PDW uses Microsoft Update and other Microsoft servicing technologies. To enable updates the appliance must be able to either connect to a corporate WSUS repository or to the Microsoft public WSUS repository.  
  
For customers that choose to configure the appliance to look for updates on the Microsoft public WSUS repository, the following instructions set the proper configuration details on the appliance.  
  
> [!NOTE]  
> The customer network administrator must provide the IP address for a corporate DNS server that can resolve names at **Microsoft.com**.  
  
1.  Using remote desktop, log on to VMM VM (<fabric domain>-VMM) using the fabric domain administrator account.  
  
2.  Open the Control Panel, click **Network and Internet**, and then click **Network and Sharing Center**.  
  
3.  In the connection list, click **VMSEthernet**, and then click **Properties**.  
  
4.  Select **Internet Protocol Version 4 (TCP/IPv4)**, and then click **Properties**.  
  
5.  In the **Alternate DNS server** box, add the IP address provided by the customer network administrator.  
  
<!-- MISSING LINKS ## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
  
