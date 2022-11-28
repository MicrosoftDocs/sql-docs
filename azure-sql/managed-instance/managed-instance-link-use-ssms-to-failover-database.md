---
title: Fail over a database by using the link in SSMS
titleSuffix: Azure SQL Managed Instance
description: Learn how to use the link feature in SQL Server Management Studio (SSMS) to fail over a database from SQL Server to Azure SQL Managed Instance.
author: sasapopo
ms.author: sasapopo
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
---

# Fail over a database by using the link in SSMS - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to fail over a database from SQL Server to Azure SQL Managed Instance by using [the link feature](managed-instance-link-feature-overview.md) in SQL Server Management Studio (SSMS). 

Failing over your database from SQL Server 2019 or earlier to SQL Managed Instance breaks the link between the two databases. It stops replication and leaves both databases in an independent state, ready for individual read/write workloads. Failing over from SQL Server 2022 does not break the link, and fail back to SQL Server 2022 is supported - this is currently in preview. 

> [!NOTE]
> - Some functionality of the link is generally available, while some is currently in preview. Review the [requirements](managed-instance-link-feature-overview.md#requirements) to learn more. 
> -  You can also use a [T-SQL and PowerShell](managed-instance-link-use-scripts-to-failover-database.md) to failover a database with the link. 

## Prerequisites 

To fail over your databases to SQL Managed Instance, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#requirements) with required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- [SQL Server Management Studio v19.0 or later](/sql/ssms/download-sql-server-management-studio-ssms).
- [An environment that's prepared for replication](managed-instance-link-preparation.md).
- [Setup of the link feature and replication of your database to your managed instance in Azure](managed-instance-link-use-ssms-to-replicate-database.md). 

## Fail over a database

In the following steps, you use the **Failover database to Managed Instance** wizard in SSMS to fail over your database from SQL Server to SQL Managed Instance. The wizard takes you through failing over your database, breaking the link between the two instances in the process if you're on SQL Server 2019 or earlier. 

> [!CAUTION]
> If you're performing a planned manual failover, stop the workload on the source SQL Server database to allow the SQL Managed Instance replicated database to completely catch up and failover without data loss. If you're performing a forced failover, you might lose data. 

1. Open SSMS and connect to your SQL Server instance. 
1. In Object Explorer, right-click your database, hover over **Azure SQL Managed Instance link**, and select **Failover database** to open the **Failover database to Managed Instance** wizard. 

   :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-ssms-database-context-failover-database.png" alt-text="Screenshot that shows a database's context menu option for failover.":::

1. On the **Introduction** page of the **Failover database to Managed Instance** wizard, select **Next**.

   :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-introduction.png" alt-text="Screenshot that shows the Introduction page.":::


3. On the **Log in to Azure** page, select **Sign-in** to provide your credentials and sign in to your Azure account. Select the subscription that's hosting SQL Managed Instance from the dropdown list, and then select **Next**. 

    :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-login-to-azure.png" alt-text="Screenshot that shows the page for signing in to Azure.":::

4. On the **Failover Type** page, choose the type of failover you're performing. Select the box to confirm that you've stopped the workload for a planned failover, or you understand that you might lose data if using a forced failover. Select **Next**. 

   :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-failover-type.png" alt-text="Screenshot that shows the Failover Type page.":::

1. On the **Clean-up (optional)** page, choose to drop the availability group if you created it solely for the purpose of migrating your database to Azure and you no longer need it. If you want to keep the availability group, leave the boxes cleared. Select **Next**. 


   :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-cleanup-optional.png" alt-text="Screenshot that shows the page for the option of deleting an availability group.":::

1. On the **Summary** page, review the actions that will be performed for your failover. Optionally, select **Script** to create a script that you can run at a later time. When you're ready to proceed with the failover, select **Finish**. 

   :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-summary.png" alt-text="Screenshot that shows the Summary page.":::

7. The **Executing actions** page displays the progress of each action.  

    :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-executing-actions.png" alt-text="Screenshot that shows the page for executing actions.":::

8. After all steps finish, the **Results** page shows check marks next to the successfully completed actions. You can now close the window. 

    :::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-results.png" alt-text="Screenshot that shows the Results page with completed status.":::

On successful execution of the failover process, the link is dropped and no longer exists. The source SQL Server database and the target SQL Managed Instance database can both execute a read/write workload. They're completely independent. Repoint your application connection string to managed instance to complete the migration process.

> [!IMPORTANT]
> On successful failover, manually repoint your application(s) connection string to managed instance FQDN to continue running in Azure, and to complete the migration process.

## View the failed-over database 

You can validate that the link has been dropped by reviewing the database on SQL Server. 

:::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-ssms-sql-server-database.png" alt-text="Screenshot that shows a database on SQL Server in S S M S.":::

Then, review the database on SQL Managed Instance. 

:::image type="content" source="./media/managed-instance-link-use-ssms-to-failover-database/link-failover-ssms-managed-instance-database.png" alt-text="Screenshot that shows a database on SQL Managed Instance in S S M S.":::

## Next steps

To learn more, see [Link feature for Azure SQL Managed Instance](managed-instance-link-feature-overview.md). 
