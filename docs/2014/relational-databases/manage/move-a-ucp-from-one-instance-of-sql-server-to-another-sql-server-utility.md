---
title: "Move a UCP from One Instance of SQL Server to Another (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: b402fd9e-0bea-4c38-a371-6ed7fea12e96
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Move a UCP from One Instance of SQL Server to Another (SQL Server Utility)
  This topic describes how to move a utility control point (UCP) from one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### Move a UCP from one instance of SQL Server to another.  
  
1.  Create a new UCP on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that will be the new host instance of the UCP. For more information, see [Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](create-a-sql-server-utility-control-point-sql-server-utility.md).  
  
2.  If non-default policy settings have been set for any instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, make note of the policy thresholds so that you can re-establish them on the new UCP. Default policies are applied when instances are added to the new UCP. If default policies are in effect, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility list view displays **Global** in the **Policy Type** column.  
  
3.  Remove all managed instances from the old UCP. For more information, see [Remove an Instance of SQL Server from the SQL Server Utility](remove-an-instance-of-sql-server-from-the-sql-server-utility.md).  
  
4.  Back up the utility management data warehouse (UMDW) from the old UCP. The filename is Sysutility_mdw_\<GUID>_DATA, and the database default location is \<System drive>:\Program Files\Microsoft SQL Server\MSSQL10_50.<UCP_Name>\MSSQL\Data\\, where \<System drive> is normally the C:\ drive. For more information, see [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md).  
  
5.  Restore the backup of the UMDW to the new UCP. For more information, see [Copy Databases with Backup and Restore](../databases/copy-databases-with-backup-and-restore.md).  
  
6.  Enroll instances into the new UCP to make them managed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information, see [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](enroll-an-instance-of-sql-server-sql-server-utility.md).  
  
7.  Implement custom policy definitions for the managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], as necessary.  
  
8.  Wait approximately 1 hour for data collection and aggregation operations to complete.  
  
9. To refresh data, right-click the **Managed Instances** node in **Utility Explorer**, then select **Refresh**. List view data are displayed in the **Utility Explorer** content pane. For more information, see [View Resource Health Policy Results &#40;SQL Server Utility&#41;](view-resource-health-policy-results-sql-server-utility.md).  
  
## See Also  
 [SQL Server Utility Features and Tasks](sql-server-utility-features-and-tasks.md)   
 [Enroll an Instance of SQL Server &#40;SQL Server Utility&#41;](enroll-an-instance-of-sql-server-sql-server-utility.md)  
  
  
