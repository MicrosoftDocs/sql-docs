---
title: "Requirements and Considerations for Analysis Services Deployment | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Requirements and Considerations for Analysis Services Deployment
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  The performance and availability of a solution depends on many factors, including the capabilities of the underlying hardware, the topology of your server deployment, the characteristics of your solution (for example, having partitions distributed across multiple servers or using ROLAP storage that requires direct access to the relational engine), service level agreements, and the complexity of your data model.  
  
## Memory and Processor Requirements  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] needs more memory and processor resources in the following cases:  
  
-   When processing large or complex cubes. These require more memory and processor resources than small or simple cubes.  
  
-   When the number of cubes within a single database increases.  
  
-   When the number of databases within a single instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] increases.  
  
-   When the number of instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on a single computer increases.  
  
-   When the number of users who are accessing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] resources simultaneously increases.  
  
 The amount of memory and processor resources that are available to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] varies depending on the edition of SQL Server, operating system, hardware capability, and whether you are using virtual or physical processors. For more information, follow these links:  
  
 [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)  
  
 [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md)  
  
 [Features Supported by the Editions of SQL Server 2016](../../analysis-services/analysis-services-features-supported-by-the-editions-of-sql-server-2016.md)  
  
 [Maximum Capacity Specifications &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/olap-physical/maximum-capacity-specifications-analysis-services.md)  
  
## Disk Space Requirements  
 Different aspects of your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] installation and the tasks related to object processing require different amounts of disk space. The following list describes these requirements.  
  
 Cubes  
 Cubes that have large fact tables require more disk space than cubes that have small fact tables. Similarly, although to a lesser extent, cubes that have many large dimensions require more disk space than cubes that have fewer dimension members. Generally, you can expect that an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database will require approximately 20 percent of the amount of space required for the same data stored in the underlying relational database.  
  
 Aggregations  
 Aggregations require additional space proportional to aggregations added-the more aggregations there are, the more space is required. If you avoid creating unneeded aggregations, the additional disk space that is needed for aggregations typically should not exceed approximately 10 percent of the size of the data that is stored in the underlying relational database.  
  
 Data Mining  
 By default, mining structures cache to disk the dataset with which they are trained. To remove this cached data from the disk, you can use the **Process Clear Structure** processing option on the mining structure object. For more information, see [Processing Requirements and Considerations &#40;Data Mining&#41;](../../analysis-services/data-mining/processing-requirements-and-considerations-data-mining.md).  
  
 Object Processing  
 During processing, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] stores copies of the objects it is processing in the processing transaction on disk until the processing is finished. When the processing is finished, the processed copies of the objects replace the original objects. Therefore, you must provide sufficient additional disk space for a second copy of each object to be processed. For example, if you plan to process a whole cube in a single transaction, you need sufficient hard disk space to store a second copy of the whole cube.  
  
##  <a name="BKMK_Availability"></a> Availability Considerations  
 In an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] environment, a cube or mining model may be unavailable for querying because of a hardware or software failure. A cube also may be unavailable because it needs to be processed.  
  
### Providing Availability in the Event of Hardware or Software Failures  
 Hardware or software may fail for various reasons. However, maintaining availability of your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] installation is not only about troubleshooting the source of those failures, but also about providing alternative resources that enable the user to continue using the system if a failure occurs. Clustering and load balancing servers are typically used to provide the alternative resources that are necessary to maintain availability when hardware or software failures occur.  
  
 To provide availability in the event of a hardware or software failure, consider deploying [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] into a failover cluster. In a failover cluster, if the primary node fails for any reason or if it must be rebooted, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Clustering fails over to a secondary node. After the failover, which occurs very quickly, when users run query they are accessing the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that is running on the secondary node. For more information about failover clusters, see [Windows Server Technologies:  Failover Clusters](http://technet.microsoft.com/library/cc732488\(v=WS.10\).aspx).  
  
 Another solution for availability issues is to deploy your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project onto two or more production servers. You can then use the Network Load Balancing (NLB) feature of Windows servers to combine the production servers into a single cluster. In an NLB cluster, if a server in the cluster is unavailable due to hardware or software issues, the NLB service directs user queries to those servers that are still available.  
  
### Providing Availability While Processing Structural Changes  
 Certain changes to a cube can cause the cube to be unavailable until it is processed. For example, if you make structural changes to a dimension in a cube, even if you reprocess the dimension, each cube that uses the modified dimension must also be processed. Until you process those cubes, users cannot query them, nor can they query any mining models that are based on a cube that has the modified dimension.  
  
 To provide availability while you process structural changes that may affect one or more cubes in an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, consider incorporating a staging server and using the Synchronize Database Wizard. This feature lets you update data and metadata on a staging server, and then to perform an online synchronization of the production server and the staging server. For more information, see [Synchronize Analysis Services Databases](../../analysis-services/multidimensional-models/synchronize-analysis-services-databases.md).  
  
 To transparently process incremental updates to source data, enable proactive caching. Proactive caching updates cubes with new source data without requiring manual processing and without affecting the availability of cubes. For more information, see [Proactive Caching &#40;Partitions&#41;](../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).  
  
##  <a name="BKMK_Scalability"></a> Scalability Considerations  
 Multiple instances of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on the same computer may cause performance issues. To solve these issues, one option may be to increase the processor, memory, and disk resources on the server. However, you may also need to scale the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] across multiple computers.  
  
### Scaling Analysis Services Across Multiple Computers  
 There are several ways to scale an installation of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] across multiple computers. These options are described in the following list.  
  
-   If there are multiple instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on a single computer, you can move one or more instances to another computer.  
  
-   If there are multiple [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases on a single computer, you can move one or more of the databases onto its own instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on a separate computer.  
  
-   If one or more relational databases provide data to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database, you can move these databases to a separate computer. Before you move the databases, consider the network speed and bandwidth that exist between the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database and its underlying databases. If the network is slow or congested, moving the underlying databases to a separate computer will degrade processing performance.  
  
-   If processing affects query performance, but you can't process during times of reduced query load, consider moving your processing tasks to a staging server and then performing an online synchronization of the production server and the staging server. For more information, see [Synchronize Analysis Services Databases](../../analysis-services/multidimensional-models/synchronize-analysis-services-databases.md). You can also distribute processing across multiple instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] by using remote partitions. Processing remote partitions uses the processor and memory resources on the remote server, instead of the resources on the local computer. For information on remote partitions management, see [Create and Manage a Remote Partition &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/create-and-manage-a-remote-partition-analysis-services.md).  
  
-   If query performance is poor but you cannot increase the processor and memory resources on the local server, consider deploying an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project onto two or more production servers. Then you can use Network Load Balancing (NLB) to combine the servers into a single cluster. In an NLB cluster, queries are automatically distributed across all the servers in the NLB cluster.  
  
  
