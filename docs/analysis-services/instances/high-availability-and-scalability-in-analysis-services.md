---
title: "High availability and Scalability in Analysis Services | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# High availability and Scalability in Analysis Services
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This article describes the most commonly used techniques for making Analysis Services databases highly available and scalable. While each objective could be addressed separately, in reality they often go hand in hand: a scalable deployment for large query or processing workloads typically comes with expectations of high availability.  
  
 The reverse case is not always true, however. High availability, without scale, can be the sole objective when stringent service level agreements exist for mission-critical, but moderate, query workloads.  
  
 Techniques for making Analysis Services highly available and scalable tend to be the same for all server modes (Multidimensional, Tabular, and SharePoint integrated mode). Unless specifically noted otherwise, you should assume the information in this article applies to all modes.  
  
## Key Points  
 Because the techniques for availability and scale differ from those of the relational database engine, a short summary of key points is an effective introduction to techniques used with Analysis Services:  
  
-   Analysis Services utilizes the high availability and scalability mechanisms built into the Windows server platform: network load balancing (NLB), Window Server Failover Clustering (WSFC), or both.  
  
    > [!NOTE]  
    >  The Always On feature of the relational database engine does not extend to Analysis Services.  You cannot configure an Analysis Services instance to run in an Always On availability group.  
    >   
    >  Although Analysis Services does not run in Always On Availability Groups, it can both retrieve and process data from Always On relational databases. For instructions on how to configure a highly available relational database so that it can be used by Analysis Services, see [Analysis Services with Always On Availability Groups](../../database-engine/availability-groups/windows/analysis-services-with-always-on-availability-groups.md).  
  
-   High availability, as a sole objective, can be achieved via server redundancy in a failover cluster Replacement nodes are assumed to have identical hardware and software configuration as the active node.  By itself, WSFC gives you high availability, but without scale.  
  
-   Scalability, with or without availability, is achieved via  NLB over read-only databases.  Scalability is usually a concern when query volumes are large or subject to sudden spikes.  
  
     Load balancing, coupled with multiple read-only databases, give you both scale and high availability because all nodes are active, and when a server goes down, requests are automatically redistributed among the remaining nodes. When you need both scalability and availability, an NLB cluster is the right choice.  
  
 For processing, objectives of high availability and scalability are less of a concern because you control the timing and scope of operations. Processing can be both partial and incremental across portions of a model, although at some point, you will need to process a model in full on a single server to ensure data consistency across all indexes and aggregations. A robust scalable architecture relies on hardware that can accommodate full processing at whatever cadence is required. For large solutions, this work is structured as an independent operation, with its own hardware resources.  
  
##  <a name="bkmk_serverconfig"></a> Single vs. multi-server configurations  
 In a regular single-server deployment, processing and query workloads run concurrently,  assuming system resources are sufficient for both activities. Analysis Services keeps existing data structures intact for query support while an updated version is being processed in the background. Having sufficient memory and disk space to handle temporary data structures is a hardware requirement that exists for all server modes, although each mode places different demands on system resources and comes with different levels of NUMA-awareness.  
  
 **Single servers and scalability**  
  
 A single high-end, multi-core server might provide sufficient scale on its own. On high end system with a large number of cores, RAM, and disk space, you can potentially scale-up within a single system.  
  
 For Multidimensional databases, you can adjust server configuration properties to create affinity between processes and  processors. See [Thread Pool Properties](../../analysis-services/server-properties/thread-pool-properties.md) for more information.  
  
 **Multi-server deployments**  
  
 Sometimes operational requirements dictate the use of multiple servers. For example, failover clusters are multi-server by definition, with each node running on identical hardware and software configurations.  
  
 Similarly, a serious requirement for high availability of query workloads typically calls for multiple servers. In this scenario, the  recommended configuration for Analysis Services is to use a mix of read-only and read-write databases, running on separate instances of Analysis Services, on dedicated hardware. Read-only databases handle query requests. Read-write databases are used for processing. An expanded description of this commonly used technique is provided in the next section.  
  
 **Virtual machines and high availability**  
  
 Another strategy for meeting a high availability requirement could include the use of virtual machines. If availability can be satisfied by standing up a replacement server within hours rather than minutes, you might be able to use virtual machines that can be started on demand, and loaded with updated databases retrieved from a central location.  
  
## Scalability using read-only and read-write databases  
 Network load balancing is recommended for high or escalating query and processing workloads. Analysis Services databases in a NLB solution are defined as read-only databases to ensure consistency across queries.  
  
 Although the guidance in [Scale-out querying for Analysis Services using read-only databases](https://technet.microsoft.com/library/ff795582\(v=sql.100\).aspx) (published in 2008) is dated, it's still generally valid. While server operating systems and computer hardware have evolved, and references to specific platforms and CPU limits are obsolete, the basic technique of using read-only and read-write databases for large query volumes is unchanged.  
  
 The approach can be summarized as follows:  
  
-   Use dedicated hardware and instances of Analysis Services to process the database. Set the database to read-only after processing is finished. See [Switch an Analysis Services database between ReadOnly and ReadWrite modes](../../analysis-services/multidimensional-models/switch-an-analysis-services-database-between-readonly-and-readwrite-modes.md) for instructions.  
  
-   Use multiple, identical query servers to run copies of the same  read-only Analysis Services database. Servers are deployed in an NLB cluster,  accessed via one virtual server name that serves as a single point of entry to the cluster.  
  
-   Use robocopy to copy an entire data directory from the processing server to each query server and attach the same database in read-only mode to all query servers. You can also use SAN snapshots, synchronize, or any other tool or method you use for moving production databases.  
  
## Resource demands for Tabular and Multidimensional workloads  
 The following table is a high-level summary of how Analysis Services uses system resources for queries and processing, separated out by server mode and storage. This summary might help you understand what to emphasize in a multi-server deployment that handles a distributed workload.  
  
|||  
|-|-|  
|**Server and storage mode**|**Impact on system resource**|  
|Tabular in-memory (default) where queries are executed as table scans of in-memory data structures.|Emphasize RAM and CPUs with fast clock speeds.|  
|Tabular in DirectQuery mode, where queries are offloaded to backend relational database servers and processing is limited to constructing metadata in the model.|Focus on relational database performance, lowering network latency, and maximizing throughput. Faster CPUs can also improve performance of the Analysis Services query processor.|  
|Multidimensional models using MOLAP storage|Choose a balanced configuration that accommodates disk IO for loading data quickly and sufficient RAM for cached data.|  
|Multidimensional models using ROLAP storage.|Maximize disk IO and minimize network latency.|  
  
## High availability and redundancy through WSFC  
 Analysis Services can be installed into an existing Windows Server Failover Cluster (WSFC) to achieve high availability that restores service within the shortest time possible.  
  
 Failover clusters provide full access (read and writeback) to the database, but only one node at a time. Secondary databases run on additional nodes in the cluster, as replacement servers if the first node goes down.  
  
 The primary advantage of failover clustering is fast recovery from a service failure. This advantage comes with certain limitations. For one, if failover is never needed, dedicated resources in the cluster are idle. Second, in the event of a failover, all connections are lost, with the corresponding loss of uncommitted work. Most client applications should be able to handle this situation; often, hitting the refresh button in the application will bring the results back. 
 
 When considering a WSFC, keep the following points in mind:

- Active/Active is not currently supported. Active/Passive (failover) is the only supported WSFC configuration for Analysis Services.
- When clustering Analysis Services, make sure that any nodes participating in the cluster run on identical or highly similar hardware, and that the operational context of each node is the same in terms of operating system version and service packs, Analysis Services version and service packs (or cumulative updates), and server mode.
- Avoid repurposing a Passive node as another workload's Active node. Any short-term gains in computer utilization will be lost in the event of an actual failover situation if the node is unable to handle both workloads.
 
 In-depth instructions and background information for deploying Analysis Services in a failover cluster are provided in this whitepaper: [How to Cluster SQL Server Analysis Services](https://msdn.microsoft.com/library/dn736073.aspx). Although written for SQL Server 2012, this guidance still applies to newer versions of Analysis Services.  
  
## See Also  
 [Synchronize Analysis Services Databases](../../analysis-services/multidimensional-models/synchronize-analysis-services-databases.md)   
 [Forcing NUMA affinity for Analysis Services Tabular Databases](https://blogs.msdn.microsoft.com/sqlcat/2013/11/05/forcing-numa-node-affinity-for-analysis-services-tabular-databases/)   
 [An Analysis Services Case Study: Using Tabular Models in a Large-scale Commercial Solution](https://msdn.microsoft.com/library/dn751533.aspx)  
  
  
