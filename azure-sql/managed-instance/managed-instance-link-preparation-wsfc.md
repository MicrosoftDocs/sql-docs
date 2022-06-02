---
title: Prepare environment for Managed Instance link with WSFC
titleSuffix: Azure SQL Managed Instance
description: Learn how to prepare your environment with WSFC for using a Managed Instance link to replicate and fail over your database to SQL Managed Instance. 
services: sql-database
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.custom: 
ms.devlang: 
ms.topic: guide
author: danimir
ms.author: danil
ms.reviewer: mathoma, danil
ms.date: 06/02/2022
---

# Prepare your environment for a link with WSFC - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to enable Always On with Windows Server Failover Cluster (WSFC) on your SQL Server as an extra step to [prepare your environment](managed-instance-link-preparation.md) for Managed Instance link.

Configuration of a single-node (local) Windows Server Failover Cluster (WSFC) is the minimum requirement for SQL Server 2016 only. No multiple node WSFC configuration is required for the link, and it's optional. This step is not required for SQL Servers 2019-2022, and it's optional.

## Install WSFC module on Windows Server

Run the following PowerShell command as Administrator on Windows Server hosting the SQL Server to install Windows Server Failover Cluster module.

```powershell
-- Run as Administrator in PowerShell on Windows Server OS hosting the SQL Server
Install-WindowsFeature -Name Failover-Clustering –IncludeManagementTools
```

Alternatively, you can also use Server Manager to install WSFC module using the graphical user interface.

## Configure single-node cluster using Failover Cluster Manager

On the Windows Server hosting the SQL Server, configure a single-node (local) cluster using the graphical user interface. Follow these steps:

1. Find out your Windows Server name by executing `hostname` command from the command prompt.
1. Record the output of this command (sample output marked in the image below), or keep this window open as you'll use this name in one of the next steps.

   :::image type="content" source="./media/managed-instance-link-preparation-wsfc/mi-link-server-hostname.png" alt-text="Screenshot of finding out Windows Server hostname through the command prompt.":::

1. Open Failover Cluster Manager by pressing Windows key + R on the keyboard, type `%windir%\system32\Cluadmin.msc`, and click OK.

- Alternatively, Failover Cluster Manager can be accessed by opening Server Manager, selecting Tools in the upper right corner, and then selecting Failover Cluster Manager. 

1. In Windows Cluster manager, click on Create Cluster option.

   :::image type="content" source="./media/managed-instance-link-preparation-wsfc/mi-link-cluster-create-01.png" alt-text="Screenshot of accessing the create cluster option.":::

1. On the Before You Begin screen, click Next.
1. On the Select Server screen, enter your Windows Server name (type, or copy-paste the output from the earlier executed `hostname` command),  click Add, and then Next.

   :::image type="content" source="./media/managed-instance-link-preparation-wsfc/mi-link-cluster-create-02.png" alt-text="Screenshot of entering Windows Server hostname when creating WSFC.":::

1. On the Validation Warning screen, leave Yes on, click Next.
1. On the Before You Being screen, click Next.
1. On the Testing Options screen, leave Run all tests on, and click Next.
1. On the Confirmation screen, click Next.
1. On the Summary screen, wait for the validation to complete, and then click Finish.

1. On the Access Point for Administering the Cluster screen, type your cluster name, for example `WSFCluser`, and then click Next.

   :::image type="content" source="./media/managed-instance-link-preparation-wsfc/mi-link-cluster-create-03.png" alt-text="Screenshot of entering the cluster name.":::

1. On the Confirmation screen, click Next.
1. On the Creating New Cluster screen, wait for the creation to be completed, and then click Finish.

With the above steps, you've created a single-node (local) Windows Server Failover Cluster.

## Verification

To verify that single-node WSFC cluster has been created, follow these steps:

1. In the Failover Cluster Manager, click on the cluster name on the left hand side, and expand it by clicking on the `>` arrow.
1. Click on Nodes.

   :::image type="content" source="./media/managed-instance-link-preparation-wsfc/mi-link-cluster-validate.png" alt-text="Screenshot of validating the WSFC creation.":::

You should be able to see the local machine single-node added to this cluster and with the Status being `Up`. This confirms the WSFC configuration has been completed successfully. You can now close the Failover Cluster Manager tool.

Next, verify that Always On option can be enabled on SQL Server by following these steps:

1. Open SQL Server Configuration Manager
1. Double-click on SQL Server
1. Click on AlwaysOn High Availability tab

   :::image type="content" source="./media/managed-instance-link-preparation-wsfc/mi-link-alwayson-validate.png" alt-text="Screenshot of validating that Always On option is enabled in SQL Server.":::

You should be able to see the name of the WSFC you've created, and you should be able to check-on the Enable AlwaysOn Availability Groups should option. This confirms the configuration has been completed successfully.

## Grant Permissions in SQL Server to work with WSFC

Next, grant permission in SQL Server to `NT Authority \ System` system account to create Availability Groups using WSFC. Execute the following T-SQL script on your SQL Server:

1. Log in to your SQL Server, using a client such is SSMS
2. Execute the following T-SQL script

```sql
-- Run on SQL Server
-- Grant permissions to NT Authority \ System to create AG on this SQL Server
GRANT ALTER ANY AVAILABILITY GROUP TO [NT AUTHORITY\SYSTEM]
GO
GRANT CONNECT SQL TO [NT AUTHORITY\SYSTEM]
GO
GRANT VIEW SERVER STATE TO [NT AUTHORITY\SYSTEM]
GO
```

## Next steps

- Continue environment preparation for the link by returning to the prepare your environment guide and [enablling Always On](managed-instance-link-preparation.md#enable-availability-groups) on your SQL Server.
- To learn more about configuring multiple-node WSFC (not mandatory for the link, and optional), see [Create a failover cluster](/windows-server/failover-clustering/create-failover-cluster) guide for Windows Server.
