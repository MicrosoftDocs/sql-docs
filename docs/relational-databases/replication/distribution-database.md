---
title: "Distribution Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.configuredistributionwizard.distributiondatabase.f1"
ms.assetid: 5b42a083-7a11-41d8-9e3f-320c7c907237
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Distribution Database
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The distribution database stores metadata and history data for all types of replication, and transactions for transactional replication.  
  
 In many cases, a single distribution database is sufficient. However, if multiple Publishers use a single Distributor, consider creating a distribution database for each Publisher. Doing so ensures that the data flowing through each distribution database is distinct. You can specify one distribution database for the Distributor using the Configure Distribution Wizard. If necessary, specify additional distribution databases in the **Distributor Properties** dialog box.  
  
## Options  
 **Distribution database name**  
 Enter a name for the distribution database. The default name for the distribution database is 'distribution'. If you specify a name, the name can be a maximum of 128 characters, must be unique within an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and must conform to the rules for identifiers. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 **Folder for the distribution database file** and **Folder for the distribution database log file**  
 Enter the path for the distribution database and log files. Paths must refer to disks that are local to the Distributor and begin with a local drive letter and colon (for example, C:). Mapped drive letters and network paths are not valid.  
  
> [!NOTE]  
>  You can decrease the time it takes to write transactions and improve the performance of replication by placing the distribution database log on a separate disk drive from the distribution database.  
  
## See Also  
 [Configure Distribution](../../relational-databases/replication/configure-distribution.md)   
 [Configure Publishing and Distribution](../../relational-databases/replication/configure-publishing-and-distribution.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)  
  
  
