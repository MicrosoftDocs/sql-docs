---
title: "Features Supported by the Editions of SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: database-engine
ms.topic: conceptual
ms.assetid: 5da61ff5-12b9-48e6-b3c8-0dacca1751c4
author: mightypen
ms.author: genemi
manager: craigg
---
# Features Supported by the Editions of SQL Server 2014


  This topic provides details of features supported by the different editions of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. 

 > **NOTE:** [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is available in an Evaluation edition for a 180-day trial period. For more information, see the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Trial Software Web Site](https://go.microsoft.com/fwlink/?LinkId=190955).  
> 
> **NOTE:** For features supported by Evaluation and Developer editions see the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise feature set.  
  
 To navigate to the table for a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] technology, click on its link:  
  
 [Cross-Box Scale Limits](#CrossBoxScale)  
  
 [High Availability](#High_availability)  
  
 [Scalability and Performance](#Scalability)  
  
 [Security](#Enterprise_security)  
  
 [Replication](#Replication)  
  
 [Management Tools](#Mgmt_Tools)  
  
 [RDBMS Manageability](#RDBMS_mgmt)  
  
 [Development Tools](#Dev_tools)  
  
 [Programmability](#Programmability)  
  
 [Integration Services](#SSIS)  
  
 [Integration Services-Advanced Adapters](#SSIS_AA)  
  
 [Integration Services-Advanced Transforms](#SSIS_AT)  
  
 [Master Data Services](#MDS)  
  
 [Data Warehouse](#Data_warehouse)  
  
 [Analysis Services](#SSAS)  
  
 [BI Semantic Model (Multidimensional)](#BISemModel_multi)  
  
 [BI Semantic Model (Tabular)](#BISemModel_tabular)  
  
 [PowerPivot for SharePoint](#PowerPivot)  
  
 [Data Mining](#DataMining)  
  
 [Reporting Services](#Reporting)  
  
 [Business Intelligence Clients](#BIClients)  
  
 [Spatial and Location Services](#Spatial)  
  
 [Additional Database Services](#Add_DBServices)  
  
 [Other Components](#Other_Components)  
  
##  <a name="CrossBoxScale"></a> Cross-Box Scale Limits  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Maximum Compute Capacity Used by a Single Instance ([!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine)<sup>1</sup>|Operating System maximum|Limited to lesser of 4 Sockets or 16 cores|Limited to lesser of 4 Sockets or 16 cores|Limited to lesser of 4 Sockets or 16 cores|Limited to lesser of 1 Socket or 4 cores|Limited to lesser of 1 Socket or 4 cores|Limited to lesser of 1 Socket or 4 cores|  
|Maximum Compute Capacity Used by a Single Instance (Analysis Services, Reporting Services) <sup>1</sup>|Operating system maximum|Operating system maximum|Limited to lesser of 4 Sockets or 16 cores|Limited to lesser of 4 Sockets or 16 cores|Limited to lesser of 1 Socket or 4 cores|Limited to lesser of 1 Socket or 4 cores|Limited to lesser of 1 Socket or 4 cores|  
|Maximum memory utilized (per instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine)|Operating system maximum|128 GB|128 GB|64 GB|1 GB|1 GB|1 GB|  
|Maximum memory utilized (per instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)])|Operating system maximum|Operating system maximum|64 GB|N/A|N/A|N/A|N/A|  
|Maximum memory utilized (per instance of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)])|Operating system maximum|Operating system maximum|64 GB|64 GB|4 GB|N/A|N/A|  
|Maximum relational Database size|524 PB|524 PB|524 PB|524 PB|10 GB|10 GB|10 GB|  
  
 <sup>1</sup> Enterprise Edition with Server + Client Access License (CAL) based licensing (not available for new agreements) is limited to a maximum of 20 cores per [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. There are no limits under the Core-based Server Licensing model. For more information, see [Compute Capacity Limits by Edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).  
  
##  <a name="High_availability"></a> High Availability  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Server Core support<sup>1</sup>|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Log Shipping|Yes|Yes|Yes|Yes||||  
|Database mirroring|Yes|Yes (Safety Full Only)|Yes (Safety Full Only)|Witness only|Witness only|Witness only|Witness only|  
|Backup compression|Yes|Yes|Yes|||||  
|Database snapshot|Yes|||||||  
|Alwayson Failover Cluster Instances|Yes  (Node support: Operating system maximum|Yes (Node support: 2)|Yes (Node support: 2)|||||  
|AlwaysOn Availability Groups|Yes (up to 8 secondary replicas, including 2 synchronous secondary replicas)|||||||  
|Connection Director|Yes|||||||  
|Online page and file restore|Yes|||||||  
|Online indexing|Yes|||||||  
|Online schema change|Yes|||||||  
|Fast recovery|Yes|||||||  
|Mirrored backups|Yes|||||||  
|Hot Add Memory and CPU<sup>2</sup>|Yes|||||||  
|Database Recovery Advisor|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Encrypted Backup|Yes|Yes|Yes|||||  
|Smart Backup|Yes|Yes|Yes|No||||  
  
 <sup>1</sup>For more information on installing [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] on Server Core, see [Install SQL Server 2014 on Server Core](../database-engine/install-windows/install-sql-server-on-server-core.md).  
  
 <sup>2</sup>This feature is only available for 64-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
##  <a name="Scalability"></a> Scalability and Performance  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Multi-instance support|50|50|50|50|50|50|50|  
|Table and index partitioning|Yes|||||||  
|Data compression|Yes|||||||  
|Resource Governor|Yes|||||||  
|Partition Table Parallelism|Yes|||||||  
|Multiple Filestream containers|Yes|||||||  
|NUMA Aware Large Page Memory and  Buffer Array Allocation|Yes|||||||  
|Buffer Pool Extension <sup>1</sup>|Yes|Yes|Yes|||||  
|IO Resource Governance|Yes|||||||  
|In-Memory OLTP <sup>1</sup>|Yes|||||||  
|Delayed durability|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
  
 <sup>1</sup> This feature is only available for 64-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
##  <a name="Enterprise_security"></a> Security  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Basic Auditing|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Fine Grained Auditing|Yes|||||||  
|Transparent database encryption|Yes|||||||  
|Extensible Key Management|Yes|||||||  
|User-Defined Roles|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Contained Databases|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Encryption for Backups|Yes|Yes|Yes|||||  
  
##  <a name="Replication"></a> Replication  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] change tracking|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Merge replication|Yes|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|  
|Transactional replication|Yes|Yes|Yes|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|  
|Snapshot replication|Yes|Yes|Yes|Yes (Subscriber only|Yes (Subscriber only)|Yes (Subscriber only)|Yes (Subscriber only)|  
|Heterogeneous subscribers|Yes|Yes|Yes|||||  
|Oracle publishing|Yes|||||||  
|Peer to Peer transactional replication|Yes|||||||  
  
##  <a name="Mgmt_Tools"></a> Management Tools  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|SQL Management Objects (SMO)|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|SQL Configuration Manager|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|SQL CMD (Command Prompt tool)|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Studio|Yes|Yes|Yes|Yes|Yes|Yes||  
|Distributed Replay - Admin Tool|Yes|Yes|Yes|Yes|Yes|Yes||  
|Distributed Replay - Client|Yes|No|Yes|Yes||||  
|Distributed Replay - Controller|Yes (Enterprise supports up to 16 clients,  Developer supports only 1 client)|No|Yes (1 client support only)|Yes (1 client support only)||||  
|SQL Profiler|Yes|Yes|Yes|No<sup>2</sup>|No<sup>2</sup>|No<sup>2</sup>|No<sup>2</sup>|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent|Yes|Yes|Yes|Yes||||  
|Microsoft System Center Operations Manager Management Pack|Yes|Yes|Yes|Yes||||  
|Database Tuning Advisor (DTA)|Yes|Yes|Yes<sup>3</sup>|Yes<sup>3</sup>||||  
|Deploy a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database to a Windows Azure VM Wizard|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Files in Windows Azure|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
  
 <sup>2</sup> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Web, [!INCLUDE[ssExpress](../includes/ssexpress-md.md)], [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] with Tools, and [!INCLUDE[ssExpress](../includes/ssexpress-md.md)] with Advanced Services can be profiled using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise editions.  
  
 <sup>3</sup> Tuning enabled only on Standard edition features.  
  
##  <a name="RDBMS_mgmt"></a> RDBMS Manageability  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|User Instances|||||Yes|Yes|Yes|  
|LocalDB|||||Yes|Yes||  
|Dedicated admin connection|Yes|Yes|Yes|Yes|Yes (under trace flag)|Yes (under trace flag)|Yes (under trace flag)|  
|PowerShell scripting support|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|SysPrep support<sup>1</sup>|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Support for Data-tier application component operations - extract, deploy, upgrade, delete|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Policy automation (check on schedule and change)|Yes|Yes|Yes|Yes||||  
|Performance data collector|Yes|Yes|Yes|Yes||||  
|Able to enroll  as a managed instance in a multi-instance management|Yes|Yes|Yes|Yes||||  
|Standard performance reports|Yes|Yes|Yes|Yes||||  
|Plan guides and plan freezing for plan guides|Yes|Yes|Yes|Yes||||  
|Direct query of indexed views (using NOEXPAND hint)|Yes|Yes|Yes|Yes||||  
|Automatic indexed view maintenance|Yes|Yes|Yes|Yes||||  
|Distributed partitioned views|Yes|Partial. Distributed Partitioned Views are not updatable|Partial. Distributed Partitioned Views are not updatable|Partial. Distributed Partitioned Views are not updatable|Partial. Distributed Partitioned Views are not updatable|Partial. Distributed Partitioned Views are not updatable|Partial. Distributed Partitioned Views are not updatable|  
|Parallel indexed operations|Yes|||||||  
|Automatic use of indexed view by query optimizer|Yes|||||||  
|Parallel consistency check|Yes|||||||  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Utility control point|Yes|||||||  
|Contained Databases|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Buffer Pool Extension<sup>2</sup>|Yes|Yes|Yes|||||  
  
 <sup>1</sup> For more information, see [Considerations for Installing SQL Server Using SysPrep](../database-engine/install-windows/considerations-for-installing-sql-server-using-sysprep.md).  
  
 <sup>2</sup> This feature is only available for 64-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
##  <a name="Dev_tools"></a> Development Tools  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|[!INCLUDE[msCoName](../includes/msconame-md.md)] Visual Studio Integration|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Intellisense ([!INCLUDE[tsql](../includes/tsql-md.md)] and MDX)|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|[!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]|Yes|Yes|Yes|Yes|Yes|||  
|SQL query edit and design tools<sup>1</sup>|Yes|Yes|Yes|||||  
|Version control support<sup>1</sup>|Yes|Yes|Yes|||||  
|MDX edit, debug, and design tools<sup>1</sup>|Yes|Yes|Yes|||||  
  
 <sup>1</sup> This feature is not available for 64-bit version of Standard edition.  
  
##  <a name="Programmability"></a> Programmability  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Common Language Runtime (CLR) Integration|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Native XML support|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|XML indexing|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|MERGE & UPSERT Capabilities|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|FILESTREAM support|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|FileTable|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Date and Time datatypes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Internationalization support|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Full-text and semantic search|Yes|Yes|Yes|Yes|Yes|||  
|Specification of language in query|Yes|Yes|Yes|Yes|Yes|||  
|Service Broker (messaging)|Yes|Yes|Yes|No (Client only)|No (Client only)|No (Client only)|No (Client only)|  
|[!INCLUDE[tsql](../includes/tsql-md.md)] endpoints|Yes|Yes|Yes|Yes||||  
  
##  <a name="SSIS"></a> Integration Services  
  
|Feature|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|-------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Import and Export Wizard|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Built-in data source connectors|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|SSIS designer and runtime|Yes|Yes|Yes|||||  
|Basic Transforms|Yes|Yes|Yes|||||  
|Basic data profiling tools|Yes|Yes|Yes|||||  
|Change Data Capture Service for Oracle by Attunity|Yes|||||||  
|Change Data Capture Designer for Oracle by Attunity|Yes|||||||  
  
###  <a name="SSIS_AA"></a> Integration Services - Advanced Adapters  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|High performance Oracle destination|Yes|||||||  
|High performance Teradata destination|Yes|||||||  
|SAP BW source and destination|Yes|||||||  
|Data mining model training destination adapter|Yes|||||||  
|Dimension processing destination adapter|Yes|||||||  
|Partition processing destination adapter|Yes|||||||  
|Change Data Capture components by Attunity|Yes|||||||  
|Connector for Open Database Connectivity (ODBC) by Attunity|Yes|||||||  
  
###  <a name="SSIS_AT"></a> Integration Services - Advanced Transforms  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Persistent (high performance) lookups|Yes|||||||  
|Data mining query transformation|Yes|||||||  
|Fuzzy grouping and lookup transformations|Yes|||||||  
|Term extractions and lookup transformations|Yes|||||||  
  
##  <a name="MDS"></a> Master Data Services  
  
> [!NOTE]  
>  -   [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] is available on the 64-bit editions of Business Intelligence and Enterprise only.  
  
|Feature|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|-------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database|Yes|Yes||||||  
|[!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application|Yes|Yes||||||  
  
##  <a name="Data_warehouse"></a> Data Warehouse  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Create cubes without a database|Yes|Yes|Yes|||||  
|Auto-generate staging and data warehouse schema|Yes|Yes|Yes|||||  
|Change data capture|Yes|||||||  
|Star join query optimizations|Yes|||||||  
|Scalable read-only Analysis Services configuration|Yes|||||||  
|Parallel query processing on partitioned tables and indices|Yes|||||||  
|xVelocity memory optimized columnstore indexes|Yes|||||||  
|Global Batch Aggregation|Yes|||||||  
  
##  <a name="SSAS"></a> Analysis Services  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Scalable Shared Databases (Attach/Detach, Read only databases)|Yes|Yes||||||  
|Backup/Restore, Attach/Detach databases|Yes|Yes|Yes|||||  
|Synchronize databases|Yes|Yes||||||  
|High Availability|Yes|Yes|Yes|||||  
|Programmability (AMO, ADOMD.Net, OLEDB, XML/A, ASSL)|Yes|Yes|Yes|||||  
  
###  <a name="BISemModel_multi"></a> BI Semantic Model (Multidimensional)  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Semi-additive Measures|Yes|Yes|No<sup>1</sup>|||||  
|Hierarchies|Yes|Yes|Yes|||||  
|KPIs|Yes|Yes|Yes|||||  
|Perspectives|Yes|Yes||||||  
|Actions|Yes|Yes|Yes|||||  
|Account intelligence|Yes|Yes|Yes|||||  
|Time Intelligence|Yes|Yes|Yes|||||  
|Custom rollups|Yes|Yes|Yes|||||  
|Writeback cube|Yes|Yes|Yes|||||  
|Writeback dimensions|Yes|Yes||||||  
|Writeback cells|Yes|Yes|Yes|||||  
|Drillthrough|Yes|Yes|Yes|||||  
|Advanced hierarchy types (Parent-Child, Ragged Hiearchies)|Yes|Yes|Yes|||||  
|Advanced dimensions (Reference dimensions, many-to-many dimensions|Yes|Yes|Yes|||||  
|Linked measures and dimensions|Yes|Yes||||||  
|Translations|Yes|Yes|Yes|||||  
|Aggregations|Yes|Yes|Yes|||||  
|Multiple Partitions|Yes|Yes|Yes, up to 3|||||  
|Proactive Caching|Yes|Yes||||||  
|Custom Assemblies (stored procs)|Yes|Yes|Yes|||||  
|MDX queries and scripts|Yes|Yes|Yes|||||  
|DAX queries|Yes|Yes||||||  
|Role-based security model|Yes|Yes|Yes|||||  
|Dimension and Cell-level Security|Yes|Yes|Yes|||||  
|Scalable string storage|Yes|Yes|Yes|||||  
|MOLAP, ROLAP, HOLAP storage modes|Yes|Yes|Yes|||||  
|Binary and compressed XML transport|Yes|Yes|Yes|||||  
|Push-mode processing|Yes|Yes||||||  
|Direct Writeback|Yes|Yes||||||  
|Measure Expressions|Yes|Yes||||||  
  
 <sup>1</sup>The LastChild semi-additive measure is supported in standard edition, but other semi-additive measures, such as None, FirstChild, FirstNonEmpty, LastNonEmpty, AverageOfChildren, and ByAccount, are not. Additive measures, such as Sum, Count, Min, Max, and non-additive measures (DistinctCount) are supported on all editions.  
  
###  <a name="BISemModel_tabular"></a> BI Semantic Model (Tabular)  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Hierarchies|Yes|Yes||||||  
|KPIs|Yes|Yes||||||  
|Perspectives|Yes|Yes||||||  
|Translations|Yes|Yes||||||  
|DAX calculations, DAX queries, MDX queries|Yes|Yes||||||  
|Row-level Security|Yes|Yes||||||  
|Partitions|Yes|Yes||||||  
|In-Memory and DirectQuery storage modes (Tabular only)|Yes|Yes||||||  
  
###  <a name="PowerPivot"></a> PowerPivot for SharePoint  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|SharePoint farm integration based on shared service architecture|Yes|Yes||||||  
|Usage reporting|Yes|Yes||||||  
|Health monitoring rules|Yes|Yes||||||  
|PowerPivot Gallery|Yes|Yes||||||  
|PowerPivot Data Refresh|Yes|Yes||||||  
|PowerPivot Data Feeds|Yes|Yes||||||  
  
###  <a name="DataMining"></a> Data Mining  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Standard Algorithms|Yes|Yes|Yes|||||  
|Data Mining Tools (Wizards, Editors Query Builders)|Yes|Yes|Yes|||||  
|Cross Validation|Yes|Yes||||||  
|Models on Filtered Subsets of Mining Structure Data|Yes|Yes||||||  
|Time Series: Custom Blending Between ARTXP and ARIMA Methods|Yes|Yes||||||  
|Time Series: Prediction with New Data|Yes|Yes||||||  
|Unlimited Concurrent DM Queries|Yes|Yes||||||  
|Advanced Configuration & Tuning Options for Data Mining Algorithms|Yes|Yes||||||  
|Support for plug-in algorithms|Yes|Yes||||||  
|Parallel Model Processing|Yes|Yes||||||  
|Time Series: Cross-Series Prediction|Yes|Yes||||||  
|Unlimited attributes for Association Rules|Yes|Yes||||||  
|Sequence Prediction|Yes|Yes||||||  
|Multiple Prediction Targets for Naïve Bayes, Neural Network and Logistic Regression|Yes|Yes||||||  
  
##  <a name="Reporting"></a> Reporting Services  
  
###  <a name="Reporting_features"></a> Reporting Services Features  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Supported catalog DB [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|Standard or higher|Standard or higher|Standard or higher|Web|Express|||  
|Supported data source [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|All   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|Web|Express|||  
|Report server|Yes|Yes|Yes|Yes|Yes|||  
|Report Designer|Yes|Yes|Yes|Yes|Yes|||  
|Report Manager|Yes|Yes|Yes|Yes|Yes|||  
|Role based security|Yes|Yes|Yes|Yes|Yes|||  
|Word Export and Rich Text Support|Yes|Yes|Yes|Yes|Yes|||  
|Enhanced gauges and charting|Yes|Yes|Yes|Yes|Yes|||  
|Export to Excel, PDF, and Images|Yes|Yes|Yes|Yes|Yes|||  
|Custom authentication|Yes|Yes|Yes|Yes|Yes|||  
|Report as data feeds|Yes|Yes|Yes|Yes|Yes|||  
|Model support|Yes|Yes|Yes|Yes||||  
|Create custom roles for role-based security|Yes|Yes|Yes|||||  
|Model Item security|Yes|Yes|Yes|||||  
|Infinite click through|Yes|Yes|Yes|||||  
|Shared component library|Yes|Yes|Yes|||||  
|Email and file share subscriptions and scheduling|Yes|Yes|Yes|||||  
|Report history, execution snapshots and caching|Yes|Yes|Yes|||||  
|SharePoint Integration|Yes|Yes|Yes|||||  
|Remote and non-SQL data source support<sup>1</sup>|Yes|Yes|Yes|||||  
|Data source, delivery and rendering, RDCE extensibility|Yes|Yes|Yes|||||  
|Data driven report subscription|Yes|Yes||||||  
|Scale out deployment (Web farms)|Yes|Yes||||||  
|Alerting<sup>2</sup>|Yes|Yes||||||  
|[!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] <sup>2</sup>|Yes|Yes||||||  
  
 <sup>1</sup>For more information on the supported datasources in [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)], see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../reporting-services/create-deploy-and-manage-mobile-and-paginated-reports.md).  
  
 <sup>2</sup>Requires [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode. For more information, see [Reporting Services SharePoint Mode Installation &#40;SharePoint 2010 and SharePoint 2013&#41;](../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md).  
  
### Report Server Database Server Edition Requirements  
 When creating a report server database, not all editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can be used to host the database. The following table shows you which editions of the [!INCLUDE[ssDE](../includes/ssde-md.md)] you can use for specific editions of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
|For this edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Reporting Services|Use this edition of the Database Engine instance to host the database|  
|----------------------------------------------------------------------|---------------------------------------------------------------------------|  
|Enterprise|Standard, Business Intelligence Enterprise,  editions (local or remote)|  
|Business Intelligence|Standard, Business Intelligence Enterprise,  editions (local or remote)|  
|Standard|Standard, Enterprise editions (local or remote)|  
|Web|Web edition (local only)|  
|Express with Advanced Services|Express with Advanced Services (local only).|  
|Evaluation|Evaluation|  
  
##  <a name="BIClients"></a> Business Intelligence Clients  
 The following software client applications are available on the Microsoft Downloads center and are provided to assist you with creating business intelligence documents that run on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. When you host these documents in a server environment, use an edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that is supported for that document type. The following table identifies which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition contains the server features required to host the documents created in these client applications.  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|[!INCLUDE[ssRBnoversion](../includes/ssrbnoversion.md)]|Yes|Yes|Yes|||||  
|Data Mining Addins for Excel and Visio 2010|Yes|Yes|Yes|||||  
|[!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] 2010|Yes|Yes||||||  
|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)]|Yes|Yes||||||  
  
> [!NOTE]
>  1.  [!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] is an Excel addin and does not depend on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. However [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] is required for sharing and collaborating with [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] workbooks in SharePoint and this capability is available as part of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise and Business Intelligence editions.  
> 2.  The above table identifies the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions that are required to enable these client tools; however these features can access data hosted on any edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
##  <a name="Spatial"></a> Spatial and Location Services  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Spatial indexes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Planar and Geodetic datatypes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Advanced spatial libraries|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Import/export of industry-standard spatial data formats|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
  
##  <a name="Add_DBServices"></a> Additional Database Services  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Migration Assistant|Yes|Yes|Yes|Yes|Yes|Yes|Yes|  
|Database mail|Yes|Yes|Yes|Yes||||  
  
##  <a name="Other_Components"></a> Other Components  
  
|Feature Name|Enterprise|Business Intelligence|Standard|Web|Express with Advanced Services|Express with Tools|Express|  
|------------------|----------------|---------------------------|--------------|---------|------------------------------------|------------------------|-------------|  
|Data Quality Services|Yes|Yes||||||  
|StreamInsight|StreamInsight Premium Edition|StreamInsight Standard Edition|StreamInsight Standard Edition|StreamInsight Standard Edition||||  
|StreamInsight HA|StreamInsight Premium Edition|||||||  
  
## See Also  
 [Product Specifications for SQL Server 2014](../../2014/getting-started/sql-server-2014-product-specifications.md)   
 [Installation for SQL Server 2014](../database-engine/install-windows/installation-for-sql-server.md)   
 [Quick-Start Installation of SQL Server 2014](../../2014/getting-started/quick-start-installation-of-sql-server-2014.md)  
  
  
