---
title: Remote table copies
description: Describes how to configure Parallel Data Warehouse to use the remote table copy feature to copy tables to SMP SQL Server databases on non-appliance servers.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Configure Parallel Data Warehouse for remote table copies
Describes how to configure SQL Server PDW to use the remote table copy feature to copy tables to SMP SQL Server databases on non-appliance servers.  
  
This article describes one of the configuration steps for configuring remote table copy. For a list of all the configuration steps, see [Remote Table Copy](remote-table-copy.md).  
  
## Before you begin
In order to configure SQL Server PDW to use remote table copy, you must:  
  
-   Have an Analytics Platform System administrator account with the ability to log directly into the ***appliance_domain*-AD01** and ***appliance_domain*-AD02** nodes.  
  
-   Know the host name or IP name of the destination server.  
  
## <a id="HowToPDW"></a> Configure SQL Server PDW for Remote Table Copy: Update Host Names in DNS

The **CREATE REMOTE TABLE** statement, used for remote table copies, specifies the destination server by using either the IP address or the IP name of the SMP Windows system. To use the IP name, you need to add entries for successful name resolution to the DNS server.  
  
The following steps outline how to update the DNS server.  
  
1. Sign in the active AD node (normally ***appliance_domain*-AD01**).  
  
1. Open the DNS Manager. This is located under **Administrative Tools** in the **Start** menu.  
  
1. Use the DNS Manager to add the IP name.  
  
## Related content

- [Use a DNS Forwarder to Resolve Non-Appliance DNS Names in Analytics Platform System](use-a-dns-forwarder-to-resolve-non-appliance-dns-names.md)
- [PDW certificate provisioning - Analytics Platform System (PDW)](pdw-certificate-provisioning.md)
- [Configure domain trusts in Analytics Platform System](configure-domain-trusts.md)
- [What's new in Analytics Platform System, a scale-out MPP data warehouse](whats-new-analytics-platform-system.md)
