---
title: "Create a Disaster Recovery Plan (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 20f91c3c-ce2a-4070-b446-de2d58260c0c
caps.latest.revision: 10
author: BarbKess
---
# Create a Disaster Recovery Plan (SQL Server PDW)
A disaster recovery plan is a crucial part of managing a data warehouse. These best practices describe important aspects of a disaster recovery plan.  
  
## <a name="CreatePlan"></a>Disaster Recovery Checklist  
The following checklist describes best practices to consider when implementing a disaster recovery plan.  
  
|Best Practice|Description|  
|-----------------|---------------|  
|Schedule regular backups|Depending on your business requirements, backups should be scheduled regularly as part of your disaster recovery plan.|  
|Use differential backups for frequent backups|Differential backups can be scheduled more frequently than full backups. For example, you could schedule a weekly full backup and a daily differential backup. If you retain all of the differentials after the last full backup, you can restore a database to any day after the last full backup.<br /><br />Hence, if you create a full backup on Saturday, and the data gets corrupted on Thursday, you can restore the database by using the full backup from Saturday and the differential from Wednesday.|  
|Back up all user databases and the appliance metadata|To restore an appliance, you will need the appliance metadata and all of the user databases.|  
|Review the backup logs|Review the backup logs regularly to make sure the backups complete successfully.|  
|Restore data regularly|Restore data regularly to verify that the backup and restore process is working end-to-end. To test the restore of an appliance database, you can restore the database to a test appliance or to a database with a different name on the production appliance. There must be enough storage space available to perform the test restore.<br /><br />[Copy a SQL Server PDW Database to Another Appliance &#40;SQL Server PDW&#41;](../sqlpdw/copy-a-sql-server-pdw-database-to-another-appliance-sql-server-pdw.md)|  
|Use different logins and personnel for backup and restore|For security of the data, the logins and the people who are authorized to perform backups should not also be authorized to perform restores. This helps to keep the backup operator from unintentionally restoring a database to a production system.|  
|Train multiple people how to do backup and restores|When a backup or restore is needed, the person who performs these operations on a regular basis might be unavailable. To help ensure that a qualified person is available, train more than one person how to perform the backup and restore tasks.|  
|Store backup files in multiple locations|The disaster recovery plan should include storing backup files in more than one location. If disk space allows, leave backup files on the Backup server for fast restores, and copy them to another server in case of data loss on the Backup server. Disaster recovery plans should also include offsite storage depending upon business requirements.|  
|Store backup devices and backup media in a secure location|Secure the access to the backup devices and media to minimize risk of the data getting stolen or corrupted.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Backup and Restore &#40;SQL Server PDW&#41;](../sqlpdw/backup-and-restore-sql-server-pdw.md)  
[BACKUP DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/backup-database-sql-server-pdw.md)  
[RESTORE DATABASE &#40;SQL Server PDW&#41;](../sqlpdw/restore-database-sql-server-pdw.md)  
  
