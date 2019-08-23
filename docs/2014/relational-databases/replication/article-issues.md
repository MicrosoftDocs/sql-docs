---
title: "Article Issues | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newpubwizard.articleissues.f1"
ms.assetid: bde57da2-dd47-412f-9df7-9224968b2448
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Article Issues
  The **Article Issues** page lists conditions that have been found for articles and any changes required as a result of these conditions. The following table lists possible issues and the actions required to ensure replication and existing applications function properly.  
  
|Article issue|Details|Action required|  
|-------------------|-------------|---------------------|  
|Uniqueidentifier columns will be added to tables.|Replication requires a column of data type **uniqueidentifier** for all articles in a merge publication or a transactional publication that allows updating subscriptions.|Replication automatically adds a column of data type **uniqueidentifier** to published tables that do not have one when the first snapshot is generated. You must ensure that INSERT and UPDATE statements that reference these tables use column lists. Also ensure that there is sufficient space on disk for the additional column.|  
|IDENTITY columns require the NOT FOR REPLICATION option.|Replication requires that all IDENTITY columns use the NOT FOR REPLICATION option. If a published IDENTITY column does not use this option, INSERT commands might not replicate properly.|Applies to publications created on Publishers running [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] and earlier. You must specify the NOT FOR REPLICATION property for all IDENTITY columns.|  
|IDENTITY property not transferred to Subscribers.|This publication does not allow updates at Subscribers. When IDENTITY columns are transferred to the Subscriber, the IDENTITY property is not be transferred. For example, a column defined as INT IDENTITY at the Publisher is defined as INT at the Subscriber.|Applies to publications created on Publishers running [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] and earlier. No action is necessary.|  
|Tables referenced by views are required.|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that all tables referenced by views and indexed views that are published be available at the Subscriber. If the referenced tables are not published as articles in this publication, they must be created at the Subscriber manually.|Use the **Back** button to navigate to the **Articles** page. Add any required objects.|  
|Objects referenced by stored procedures are required.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that all objects referenced by published stored procedures, such as tables and user-defined functions, be available at the Subscriber. If the referenced objects are not published as articles in this publication, they must be created at the Subscriber manually.|Use the **Back** button to navigate to the **Articles** page. Add any required objects.|  
  
## See Also  
 [Publish Data and Database Objects](publish/publish-data-and-database-objects.md)   
 [Create a Publication](publish/create-a-publication.md)  
  
  
