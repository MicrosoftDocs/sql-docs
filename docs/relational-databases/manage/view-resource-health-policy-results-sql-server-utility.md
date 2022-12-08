---
title: "View Resource Health Policy Results (SQL Server Utility) | Microsoft Docs"
description: Learn how to use SQL Server Management Studio to view SQL Server Utility resource health policy results for instances of SQL Server and data-tier applications.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
ms.assetid: 80cb14fb-f4c6-4be2-ba17-eb4e4cddd35f
author: MikeRayMSFT
ms.author: mikeray
---

# View Resource Health Policy Results (SQL Server Utility)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the Utility dashboard in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility resource parameters for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier applications. For more information, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  

##  <a name="SSMSProcedure"></a>

### View SQL Server Utility resource health policy results.  

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS), select **View**, then select **Utility Explorer** to view the Utility Explorer navigation pane. To view the content pane, select **View**, then select **Utility Explorer Content**.  

2. In the navigation pane, select ![connect to utility](../../relational-databases/manage/media/connect-to-utility.gif "Connect_to_Utility")**Connect to Utility**. If you have not created a utility control point (UCP) or if you have not enrolled instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or data-tier applications into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  

3. Select the UCP node to view summary data for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and data-tier applications (right-click to refresh). Dashboard data is displayed in the content pane.  

4. Select the **Managed Instances** node to view list view data for managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (right-click to refresh). List view data is displayed in the content pane.  

5. Select the **Deployed Data-tier Applications** node to view list view data for data-tier applications (right-click to refresh). List view data is displayed in the content pane.  

## See Also

- [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)
- [Reduce Noise in CPU Utilization Policies &#40;SQL Server Utility&#41;](../../relational-databases/manage/reduce-noise-in-cpu-utilization-policies-sql-server-utility.md)
- [Deployed Data-tier Application Details &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240857(v=sql.130))
- [Managed Instance Details &#40;SQL Server Utility&#41;](./utility-explorer-f1-help.md)
- [Utility Administration &#40;SQL Server Utility&#41;](/previous-versions/sql/sql-server-2016/ee240832(v=sql.130))