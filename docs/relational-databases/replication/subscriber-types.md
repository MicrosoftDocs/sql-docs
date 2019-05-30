---
title: "Subscriber Types | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.newpubwizard.subscribertypes.f1"
ms.assetid: a70656cb-21c9-4489-be77-ccd396747e3b
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Subscriber Types
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Merge replication allows you to specify the types of Subscribers that a publication must support. Selecting Subscriber types sets the *publication compatibility level*, which determines which features can be used by a publication.  
  
 After a publication snapshot is created, the publication compatibility level can be increased (made more restrictive) on the **General** page of the **Publication Properties** dialog box; the compatibility level cannot be decreased.  
  
## Options  
 Select each Subscriber type that this publication must support.  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]  
 The publication can use all features.  
  
 [!INCLUDE[ssEW](../../includes/ssew-md.md)]  
 The publication requires snapshot files to be in character format (this is handled automatically by the Snapshot Agent). [!INCLUDE[ssEW](../../includes/ssew-md.md)] also has a number of restrictions not related to compatibility level.  
  
 If this option is selected, the Web synchronization option is enabled for the publication. For more information about Web synchronization, see [Web Synchronization for Merge Replication](../../relational-databases/replication/web-synchronization-for-merge-replication.md).  
  
## See Also  
 [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md)   
 [Create a Publication](../../relational-databases/replication/publish/create-a-publication.md)   
 [View and Modify Distributor and Publisher Properties](../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
  
  
