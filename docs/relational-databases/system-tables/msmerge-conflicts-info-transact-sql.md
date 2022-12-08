---
title: "MSmerge_conflicts_info (Transact-SQL)"
description: MSmerge_conflicts_info (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_conflicts_info_TSQL"
  - "MSmerge_conflicts_info"
helpviewer_keywords:
  - "MSmerge_conflicts_info system table"
dev_langs:
  - "TSQL"
ms.assetid: 6b76ae96-737a-4000-a6b6-fcc8772c2af4
---
# MSmerge_conflicts_info (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_conflicts_info** table tracks conflicts that occur when synchronizing a subscription to a merge publication. The losing row data for conflicts is stored in the [MSmerge_conflict_publication_article](../../relational-databases/system-tables/msmerge-conflict-publication-article-transact-sql.md) table for the article where the conflict occurred. This table is stored at the Publisher in the publication database and at the Subscriber in the subscription database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**tablenick**|**int**|The nickname of the published table.|  
|**rowguid**|**uniqueidentifier**|The identifier for the conflict row.|  
|**origin_datasource**|**nvarchar(255)**|The name of the database where the conflicting change originated.|  
|**conflict_type**|**int**|The type of conflict that occurred, which can be one of the following:<br /><br /> **1** = Update Conflict: The conflict is detected at the row level.<br /><br /> **2** = Column Update Conflict: The conflict detected at the column level.<br /><br /> **3** = Update Delete Wins Conflict: The delete wins the conflict.<br /><br /> **4** = Update Wins Delete Conflict: The deleted rowguid that loses the conflict is recorded in this table.<br /><br /> **5** = Upload Insert Failed: The insert from Subscriber could not be applied at the Publisher.<br /><br /> **6** = Download Insert Failed: The insert from Publisher could not be applied at the Subscriber.<br /><br /> **7** = Upload Delete Failed: The delete at Subscriber could not be uploaded to the Publisher.<br /><br /> **8** = Download Delete Failed: The delete at Publisher could not be downloaded to the Subscriber.<br /><br /> **9** = Upload Update Failed: The update at Subscriber could not be applied at the Publisher.<br /><br /> **10** = Download Update Failed: The update at Publisher could not be applied to the Subscriber.<br /><br /> **11** = Resolution<br /><br /> **12** = Logical Record Update Wins Delete: The deleted logical record that loses the conflict is recorded in this table.<br /><br /> **13** = Logical Record Conflict Insert Update: Insert to a logical record conflicts with an update.<br /><br /> **14** = Logical Record Delete Wins Update Conflict: The updated logical record that loses the conflict is recorded in this table.|  
|**reason_code**|**int**|The error code that can be context-sensitive. In the case of update-update and update-delete conflicts, the value used for this column is the same as the **conflict_type**. However, for failed change conflicts, the reason code is the error that prevented the Merge Agent from applying the change. For example, if the Merge Agent cannot apply an insert at the Subscriber because of a primary key violation, it logs a **conflict_type** of 6 ("download insert failed") and a **reason_code** of 2627, which is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal error message for a primary key violation: "Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.\*ls'."|  
|**reason_text**|**nvarchar(720)**|The error description that can be context-sensitive.|  
|**pubid**|**uniqueidentifier**|The identifier for the publication.|  
|**MSrepl_create_time**|**datetime**|The time that the conflict occurred.|  
|**origin_datasource_id**|**uniqueidentifier**|The identifier of the database where the conflicting change originated.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
