---
title: Configure Parallel Data Warehouse for remote table copies | Microsoft Docs
description: Describes how to configure Parallel Data Warehouse to use the remote table copy feature to copy tables to SMP SQL Server databases on non-appliance servers.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Configure Parallel Data Warehouse for remote table copies
Describes how to configure SQL Server PDW to use the remote table copy feature to copy tables to SMP SQL Server databases on non-appliance servers.  
  
This topic describes one of the configuration steps for configuring remote table copy. For a list of all the configuration steps, see [Remote Table Copy](remote-table-copy.md).  
  
## Before You Begin  
In order to configure SQL Server PDW to use remote table copy, you must:  
  
-   Have a Analytics Platform System administrator account with the ability to log directly into the <strong>*appliance_domain*-AD01</strong> and <strong>*appliance_domain*-AD02</strong> nodes.  
  
-   Know the host name or IP name of the destination server.  
  
## <a name="HowToPDW"></a>Configure SQL Server PDW for Remote Table Copy: Update Host Names in DNS  
The **CREATE REMOTE TABLE** statement, used for remote table copies, specifies the destination server by using either the IP address or the IP name of the SMP Windows system. To use the IP name, you need to add entries for successful name resolution to the DNS server.  
  
The following steps outline how to update the DNS server.  
  
1.  Log on to the active AD node (normally <strong>*appliance_domain*-AD01</strong>).  
  
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
  
