---
title: "Quick-Start Installation of SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "data-quality-services"
  - "database-engine"
  - "integration-services"
  - "master-data-services"
  - "replication"
  - "reporting-services-native"
  - "reporting-services-sharepoint"
ms.topic: conceptual
helpviewer_keywords: 
  - "quick start installation [SQL Server]"
  - "installation [SQL Server]"
  - "installing SQL Server, quick start installations"
ms.assetid: 672afac9-364d-4946-ad5d-8a2d89cf8d81
author: mightypen
ms.author: genemi
manager: craigg
---
# Quick-Start Installation of SQL Server 2014
    
## Introduction  
 The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard is based on Windows Installer. It provides a single feature tree for installation of the following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components:  
  
-   [!INCLUDE[ssDE](../includes/ssde-md.md)]  
  
-   [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]  
  
-   [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]  
  
-   [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)]  
  
-   [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)]  
  
-   Management tools  
  
-   Connectivity components  
  
 You can install each component individually or select a combination of the components listed above. To make the best choice among the editions and components available in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], see [Editions and Components of SQL Server 2014](../sql-server/editions-and-components-of-sql-server-2016.md).  
  
 [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is available in 32-bit and 64-bit editions. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Setup supports the following installation options:  
  
-   **Installation Wizard**  
  
     See [Install SQL Server 2014 from the Installation Wizard &#40;Setup&#41;](../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) for procedural information about installing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using the Installation Wizard.  
  
-   **Command Prompt**  
  
     See [Install SQL Server 2014 from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md) for sample syntax and installation parameters for running unattended Setup.  
  
-   **Configuration File**  
  
     See [Install SQL Server 2014 Using a Configuration File](../database-engine/install-windows/install-sql-server-using-a-configuration-file.md) for sample syntax and installation parameters for running Setup through a configuration file.  
  
-   **SysPrep**  
  
     See [Install SQL Server 2014 Using SysPrep](../database-engine/install-windows/install-sql-server-using-sysprep.md) for procedural information about installing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using SysPrep.  
  
-   **Server Core Installation**  
  
     See [Install SQL Server 2014 on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md) for procedural information about installing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Windows Server Core.  
  
-   **[!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] BI Feature Installation**  
  
     See [Install SQL Server 2014 BI Features](../sql-server/install/install-sql-server-business-intelligence-features.md) for information about installing the features that are part of the Microsoft BI platform, that include [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)], [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], and several client applications used for creating or working with analytical data.  
  
-   **Failover Cluster Installation**  
  
     See [SQL Server Failover Cluster Installation](../sql-server/failover-clusters/install/sql-server-failover-cluster-installation.md) for procedural information about installing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] failover cluster.  
  
 By default, sample databases and sample code are not installed as part of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Setup. To install sample databases and sample code for non-Express editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see the [CodePlex Web site](https://go.microsoft.com/fwlink/?LinkId=87843). To read about support for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] sample databases and sample code for [!INCLUDE[ssExpress](../includes/ssexpress-md.md)], see [Databases and Samples Overview](https://go.microsoft.com/fwlink/?LinkId=110391).  
  
## [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation  
 Regardless of whether you use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation Wizard or the command prompt to install [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], setup involves one or more of the following steps:  
  
-   Review installation requirements, system configuration checks, and security considerations for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] installation.  For more information, see [Planning a SQL Server Installation](quick-start-installation-of-sql-server-2014.md#BKMK_BeforeYouInstall).  
  
-   Run [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Setup to upgrade an existing version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. For more information, see [Upgrading to SQL Server 2014](#BKMK_Upgrading).  
  
-   Run [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Setup to install a new instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. For more information, see [Installing SQL Server 2014](#BKMK_Install).  
  
-   After you are finished with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] installation, the next major step is configuration of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and its components. Use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] utilities to configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [Configuring SQL Server 2014](#BKMK_Configure).  
  
 You can find detailed explanations of these tasks in the next section.  
  
## Related Tasks  
  
###  <a name="BKMK_BeforeYouInstall"></a> Planning a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Installation  
 Before you install [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], you must review the hardware and software requirements, network and Internet considerations, and security considerations to install and run [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information, see [Planning a SQL Server Installation](../../2014/sql-server/install/planning-a-sql-server-installation.md) and also the following topics:  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Review the hardware and software requirements, operating system support, network and Internet considerations, and hard disk space requirements.|[Installation Prerequisites](../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)|  
|Review the security considerations for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] installation.|[Security Considerations](../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md)|  
|Review the details of the features supported by the different editions of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].|[Features and Editions](features-supported-by-the-editions-of-sql-server-2014.md)|  
|Determine the best choice among the editions and components available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].|[Editions and Components of SQL Server 2014](../sql-server/editions-and-components-of-sql-server-2016.md)|  
|Review the hardware configuration, and learn to prepare for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] failover cluster installation.|[Before Installing Failover Clustering](../sql-server/failover-clusters/install/before-installing-failover-clustering.md)|  
  
###  <a name="BKMK_Upgrading"></a> Upgrading to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]  
 You can upgrade existing instances of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)], or [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. For more information, see [Upgrade to SQL Server 2014](../database-engine/install-windows/upgrade-sql-server.md). Before running [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Setup to upgrade to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], review the following topics about the upgrade process:  
  
|Description|Topic|  
|-----------------|-----------|  
|Documents supported upgrade paths to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].|[Supported Upgrades](../database-engine/install-windows/supported-version-and-edition-upgrades.md)|  
|Describes Upgrade Advisor, a tool that analyzes instances of [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] and [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] to identify known upgrade issues.|[Use Upgrade Advisor to Prepare for Upgrades](../../2014/sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md)|  
|Describes the Distributed Replay Utility, a tool that can use multiple computers to replay trace data, simulating a mission-critical workload. By performing a replay on a test server before and after a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] upgrade, you can measure performance differences and look for any incompatibilities your application may have with the upgrade.|[Use the Distributed Replay Utility to Prepare for Upgrades](../../2014/sql-server/install/use-the-distributed-replay-utility-to-prepare-for-upgrades.md)|  
|Lists the significant changes that might affect your applications after you upgrade to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].|[Backward Compatibility](backward-compatibility.md)|  
|The procedural topic to upgrade a stand-alone instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].|[Upgrade to SQL Server 2014 Using the Installation Wizard &#40;Setup&#41;](../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)|  
|The procedural topic to upgrade an edition of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] to another edition. For information about supported edition upgrade paths, see [Supported Version and Edition Upgrades](../database-engine/install-windows/supported-version-and-edition-upgrades.md).|[Upgrade to a Different Edition of SQL Server 2014 &#40;Setup&#41;](../database-engine/install-windows/upgrade-to-a-different-edition-of-sql-server-setup.md)|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] supports upgrade of the [!INCLUDE[ssDE](../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] from [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] to [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] failover clusters separately on all failover cluster nodes. Review this topic for more information.|[Upgrade a SQL Server Failover Cluster](../sql-server/failover-clusters/windows/upgrade-a-sql-server-failover-cluster-instance.md)|  
  
###  <a name="BKMK_Install"></a> Installing [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]  
 Review the following topics for information about various installation scenarios for [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
|Description|Topic|  
|-----------------|-----------|  
|Provides links to the topics for installing various components of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] and procedural topics for installing [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].|[Install SQL Server 2014](../database-engine/install-windows/install-sql-server.md)|  
|Review this topic to install [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] on Windows Server Core.|[Install SQL Server 2014 on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md)|  
|Review this topic to add individual features to an existing instance of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].|[Add Features to an Instance of SQL Server 2014 &#40;Setup&#41;](../database-engine/install-windows/add-features-to-an-instance-of-sql-server-setup.md)|  
|Review this topic to create a new [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] failover cluster instance.|[Create a New SQL Server Failover Cluster &#40;Setup&#41;](../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)|  
|Use this topic to manage nodes in an existing [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] failover cluster instance.|[Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md)|  
|Use this topic to install [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] client tools on a failover cluster.|[Install Client Tools on a SQL Server Failover Cluster](../sql-server/failover-clusters/install/install-client-tools-on-a-sql-server-failover-cluster.md)|  
|Review the use of the SQL Discovery report to verify the version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] features installed on the computer.|[Validate a SQL Server Installation](../database-engine/install-windows/validate-a-sql-server-installation.md)|  
|Provides links to procedural topics for installing [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] from the installation wizard, from the command prompt, by using configuration files, and by using SysPrep.|[Installation How-to Topics](../../2014/sql-server/install/installation-how-to-topics.md)|  
  
## Related Content  
 This section provides information about configuring and uninstalling [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
###  <a name="BKMK_Configure"></a> Configuring [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]  
 After you have installed [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], you can further configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using graphical and command-prompt utilities. See the following topics to configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] for the first time:  
  
|Description|Topic|  
|-----------------|-----------|  
|Use the information in this topic to determine whether you need to unblock ports in a firewall to allow access to [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] or PowerPivot for SharePoint. You can follow the steps provided in this topic to configure both port and firewall settings.|[Configure the Windows Firewall to Allow Analysis Services Access](../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md)|  
|This topic provides an overview of firewall configuration and summarizes information of interest to a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] administrator.|[Configure the Windows Firewall to Allow SQL Server Access](../../2014/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)|  
|This topic describes how to configure [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] and Windows Firewall with Advanced Security to provide for network connections to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in a multi-homed environment.|[Configure a Multi-Homed Computer for SQL Server Access](../../2014/sql-server/install/configure-a-multi-homed-computer-for-sql-server-access.md)|  
  
###  <a name="BKMK_Uninstalling"></a> Uninstalling [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]  
 The following topics describe how to manually uninstall a stand-alone instance and a failover-clustered instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
|Description|Topic|  
|-----------------|-----------|  
|This topic describes how to manually uninstall a stand-alone instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].|[Uninstall SQL Server 2014](../sql-server/install/uninstall-sql-server.md)|  
|This topic describes how to uninstall a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] failover-clustered instance.|[Remove a SQL Server Failover Cluster Instance &#40;Setup&#41;](../sql-server/failover-clusters/install/remove-a-sql-server-failover-cluster-instance-setup.md)|  
|This topic provides information about manually removing [!INCLUDE[ssDQSnoversion](../includes/ssdqsnoversion-md.md)] (DQS) objects after uninstalling [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or just DQS server.|[Remove Data Quality Server Objects](../../2014/sql-server/install/remove-data-quality-server-objects.md)|  
  
## See Also  
 [Product Specifications for SQL Server 2014](sql-server-2014-product-specifications.md)   
 [Get Started with Product Documentation for SQL Server](../2014-toc/books-online-for-sql-server-2014.md) 
 [Backward Compatibility](backward-compatibility.md)  
  
  
