---
title: Article Issues
description: Article Issues in SQL Server replication lists the conditions detected for articles and actions to take. Review each issue before you generate your first snapshot.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "sql13.rep.newpubwizard.articleissues.f1"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Article issues
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Article Issues** page lists conditions that SQL Server found for articles and any changes required as a result of these conditions. The following table lists possible issues and the actions required to ensure replication and existing applications function properly.  
  
|Article issue|Details|Action required|  
|-------------------|-------------|---------------------|  
|Uniqueidentifier columns will be added to tables.|Replication requires a column of data type **uniqueidentifier** for all articles in a merge publication or a transactional publication that allows updating subscriptions.|Replication automatically adds a column of data type **uniqueidentifier** to published tables that don't have one when SQL Server generates the first snapshot. Ensure that INSERT and UPDATE statements that reference these tables use column lists. Also ensure that there's sufficient space on disk for the additional column.|  
|IDENTITY columns require the NOT FOR REPLICATION option.|Replication requires that all IDENTITY columns use the NOT FOR REPLICATION option. If a published IDENTITY column doesn't use this option, INSERT commands might not replicate properly.|Applies to publications created on Publishers running [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] and earlier. Specify the NOT FOR REPLICATION property for all IDENTITY columns.|  
|IDENTITY property not transferred to Subscribers.|This publication doesn't allow updates at Subscribers. When IDENTITY columns are transferred to the Subscriber, the IDENTITY property isn't transferred. For example, a column defined as INT IDENTITY at the Publisher is defined as INT at the Subscriber.|Applies to publications created on Publishers running [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] and earlier. No action is necessary.|  
|Tables referenced by views are required.|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that all tables referenced by views and indexed views that are published be available at the Subscriber. If you don't publish the referenced tables as articles in this publication, you must create them at the Subscriber manually.|Use the **Back** button to go to the **Articles** page. Add any required objects.|  
|Objects referenced by stored procedures are required.|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires that all objects referenced by published stored procedures, such as tables and user-defined functions, be available at the Subscriber. If you don't publish the referenced objects as articles in this publication, you must create them at the Subscriber manually.|Use the **Back** button to go to the **Articles** page. Add any required objects.|  
  
## Related content

- [Publish Data and Database Objects](publish/publish-data-and-database-objects.md)
- [Create a publication](publish/create-a-publication.md)
