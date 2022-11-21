---
title: Connect to a SQL Server Utility
description: Learn how to connect to a SQL Server Utility so that you can manage SQL Server resource health. You can connect through SQL Server Management Studio (SSMS).
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
ms.assetid: b9b90b8d-241f-4b74-ac14-de7b10ea1821
author: MikeRayMSFT
ms.author: mikeray
---

# Connect to a SQL Server Utility

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Before you can connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility, you must create a utility control point (UCP). For more information, see [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md).  

To view and manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource health, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS) to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility.  

To connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility through SSMS:  

1. Launch SSMS.

2. In SSMS, connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

3. Select **View** and then **Utility Explorer**.

4. In the Utility Explorer navigation pane, select :::image type="icon" source="media/connect-to-utility.gif" border="false"::: **Connect to Utility**.

5. In the **Connect to Server** dialog box, specify the UCP instance name, then select **Connect**.

6. View the Utility Explorer Navigation Pane to see a tree view of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resources in the UCP.

Creating a new UCP also connects you to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Utility. For more information, see [Create a SQL Server Utility Control Point &#40;SQL Server Utility&#41;](../../relational-databases/manage/create-a-sql-server-utility-control-point-sql-server-utility.md).

## See Also

- [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)
- [View Resource Health Policy Results &#40;SQL Server Utility&#41;](../../relational-databases/manage/view-resource-health-policy-results-sql-server-utility.md)