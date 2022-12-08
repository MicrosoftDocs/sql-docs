---
title: "MSpeer_conflictdetectionconfigresponse (T-SQL)"
description: Describes the MSPeer_conflictdetectionconfigureresponse stored procedure used in Peer-to-Peer replication to store each node's response to a topology wide configuration requestion.
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
ms.custom: seo-lt-2019
f1_keywords:
  - "MSpeer_conflictdetectionconfigresponse"
  - "MSpeer_conflictdetectionconfigresponse_TSQL"
helpviewer_keywords:
  - "MSpeer_conflictdetectionconfigureresponse"
dev_langs:
  - "TSQL"
ms.assetid: 2685fb66-731d-40f7-af4b-596b9222c5d4
---
# MSpeer_conflictdetectionconfigresponse (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Used in peer-to-peer replication to store each node's response to a topology wide configuration request. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|request_id|**int**|Identifies a conflict configuration request entry in the [MSpeer_conflictdetectionconfigrequest](../../relational-databases/system-tables/mspeer-conflictdetectionconfigrequest-transact-sql.md) table.|  
|peer_node|**sysname**|Name of the server instance that generated the response.|  
|peer_db|**sysname**|Subscription database at the peer that generated the response.|  
|peer_version|**sysname**|Identifies the version number of the Publisher.|  
|peer_db_version|**sysname**|Identifies the version number of the peer database.|  
|is_peer|**bit**|Indicates whether a node is a read-only Subscriber. A value of **0** indicated a read-only Subscriber.|  
|conflict_detection_enabled|**bit**|Indicates whether conflict detection is enabled for the topology.|  
|originator_id|**varbinary(16)**|Identifies each node in the topology for the purposes of conflict detection. For more information, see [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).|  
|peer_conflict_retention|**int**|Time period, in days, that metadata is stored in conflict tables.|  
|peer_subscriptions|**XML**|Information about the node that responded to the request.|  
|progress_phase|**nvarchar(32)**|Identifies the current phase of processing, by using one of the following values:<br /><br /> Started<br /><br /> Peer version collected<br /><br /> Status collected|  
|modified_date|**datetime**|Date and time that a phase was completed.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
