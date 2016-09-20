---
title: "PDW Domain Security (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d709eb25-2382-47b2-bc1d-dfa104c08cdf
caps.latest.revision: 15
author: BarbKess
---
# PDW Domain Security (SQL Server PDW)
This topic summarizes the domain structure. Use this list to determine which component to connect to.  
  
There are four layers where components can be configured.  
  
-   **Hardware** – Physical adjustments to the CPU's and disks.  
  
-   **Appliance Domain/Region** – The domain for all physical and virtual servers in the appliance. The Active Directory Domain Services, Windows Failover Clustering and Virtual Machine Manager are hosted in the fabric organizational unit (OU) of the appliance domain.  
  
-   **PDW Region** – The Control and Compute VMs that belong to the **PDWRegion** organizational unit of the appliance domain.  
  
-   **HDInsight Region** – The HDI Management, Secure, Head, and Data VMs that belong to the **HDIRegion** organizational unit of the appliance domain. For more information about the HDInsight region, see [Understanding the Security Model of the HDInsight Region &#40;Analytics Platform System&#41;](../../mpp/hdinsight/understanding-the-security-model-of-the-hdinsight-region-analytics-platform-system.md).  
  
> [!NOTE]  
> Domain names are provided during setup and are specified as up to 6 alphanumeric characters, starting with a letter. A frequent naming system creates an appliance domain starting with F, a PDW workload region starting with P, and an HDInsight region starting with H. This format is presumed throughout the help file topics but is not required.  
  
## Do the following activities using the Configuration Manager on the Control node  
Use **Remote Desktop** to connect to the Control node ( ***PDW_region*-CTL01**) virtual machine, and then open **C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\dwconfig.exe**.  
  
-   Change the passwords for the appliance domain administrator and the PDW **sa** login. You can also use the **ALTER LOGIN** statement to change the **sa** password.  
  
    > [!WARNING]  
    > Analytics Platform System does not support the dollar sign character ($) in the domain administrator or local administrator passwords. A password containing a dollar sign will validate and be usable but can block upgrade and maintenance activities.  
  
-   Set the time zone.  
  
-   Set externally facing network setting for nodes.  
  
-   Import and remove security certificates.  
  
-   Configure firewall settings.  
  
-   Set **Instant File Initialization**.  
  
-   Restore the **master** database.  
  
For more information about the Configuration Manager tool, see [Appliance Configuration &#40;Analytics Platform System&#41;](../../mpp/management/appliance-configuration-analytics-platform-system.md).  
  
## Do the following activities using the Active Directory Users and Computers program (dsa.msc)  
Use **Remote Desktop** to the Active Directory virtual machine (**<*appliance_domain*>-AD01** or ***appliance_domain*-AD02**).  
  
-   Create additional domain administrators in the appliance domain.  
  
## Do the following activities using the Admin Console  
Using Internet Explorer, connect to the IP address of the Control node cluster (**https://###.###.###.###/**) specifying the login and password of a SQL Server PDW login. Logins that are not members of the sysadmin fixed server role can connect but may not be able to perform all activities.  
  
-   Various tasks related to the SQL Server PDW health and performance.  
  
## Additional Domain and Security Topics  
[Security - Windows Authentication and SQL Server Authentication &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/security-windows-authentication-and-sql-server-authentication-sql-server-pdw.md)  
  
[Security - Configure Domain Trusts &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/security-configure-domain-trusts-sql-server-pdw.md)  
  
[PDW Permissions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/pdw-permissions-sql-server-pdw.md)  
  
[Understanding the Security Model of the HDInsight Region &#40;Analytics Platform System&#41;](../../mpp/hdinsight/understanding-the-security-model-of-the-hdinsight-region-analytics-platform-system.md)  
  
