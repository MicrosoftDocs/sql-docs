---
title: "Configure SQL Server PDW for Remote Table Copies (SQL Server PDW)"
ms.custom: na
ms.date: 01/13/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 496b4214-5891-404c-8237-c2a1e09db6d5
caps.latest.revision: 11
author: BarbKess
---
# Configure SQL Server PDW for Remote Table Copies
Describes how to configure SQL Server PDW to use the remote table copy feature to copy tables to SMP SQL Server databases on non-appliance servers.  
  
This topic describes one of the configuration steps for configuring remote table copy. For a list of all the configuration steps, see [Remote Table Copy](remote-table-copy.md).  
  
## Before You Begin  
In order to configure SQL Server PDW to use remote table copy, you must:  
  
-   Have a Analytics Platform System administrator account with the ability to log directly into the ***appliance_domain*-AD01** and ***appliance_domain*-AD02** nodes.  
  
-   Know the host name or IP name of the destination server.  
  
## <a name="HowToPDW"></a>Configure SQL Server PDW for Remote Table Copy: Update Host Names in DNS  
The **CREATE REMOTE TABLE** statement, used for remote table copies, specifies the destination server by using either the IP address or the IP name of the SMP Windows system. To use the IP name, you need to add entries for successful name resolution to the DNS server.  
  
The following steps outline how to update the DNS server.  
  
1.  Log on to the active AD node (normally ***appliance_domain*-AD01**).  
  
2.  Open the DNS Manager. This is located under **Administrative Tools** in the **Start** menu.  
  
3.  Use the DNS Manager to add the IP name.  
  
## See Also  
<!-- MISSING LINKS 
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
[Use a DNS Forwarder to Resolve Non-Appliance DNS Names](use-a-dns-forwarder-to-resolve-non-appliance-dns-names.md)  
<!-- MISSING LINKS 
[Security - Configure Domain Trusts &#40;SQL Server PDW&#41;](../sqlpdw/security-configure-domain-trusts-sql-server-pdw.md)  
-->
  
