---
title: "MSpeer_topologyresponse (Transact-SQL)"
description: MSpeer_topologyresponse (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSpeer_topologyresponse"
  - "MSpeer_topologyresponse_TSQL"
helpviewer_keywords:
  - "MSpeer_topologyresponse"
dev_langs:
  - "TSQL"
ms.assetid: 1bc5c0c6-c432-405c-89fd-e953d173a247
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# MSpeer_topologyresponse (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Used in peer-to-peer replication to store the response of each node to a topology status request. This table is stored in the publication database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|request_id|**int**|Identifies a topology status request entry in the [MSpeer_topologyrequest](../../relational-databases/system-tables/mspeer-topologyrequest-transact-sql.md) table.|  
|peer|**sysname**|Name of the server instance that generated the response.|  
|peer_version|**int**|Identifies the version number of the Publisher.|  
|peer_db|**sysname**|Subscription database at the peer that generated the response.|  
|originator_id|**int**|Identifies each node in the topology for the purposes of conflict detection. For more information, see [Conflict Detection in Peer-to-Peer Replication](../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md).|  
|peer_conflict_retention|**int**|Time period, in days, that metadata is stored in conflict tables.|  
|received_date|**datetime**|Time at which the topology request was received.|  
|connection_info|**xml**|Information about the node that responded to the request.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
