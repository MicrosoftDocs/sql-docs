---
title: Use Auditing to Analyze Audit Logs and Reports
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: Use Auditing to analyze logs in Log Analytics, Event Hubs, or through an Azure storage account.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma, vanto
ms.date: 03/03/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
---
# Use Auditing to analyze audit logs and reports

[!INCLUDE [appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

This article provides an overview of analyzing audit logs using Auditing for [Azure SQL Database](sql-database-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is). You can use Auditing to analyze audit logs stored in:

- Log Analytics
- Event Hubs
- Azure storage

## Analyze logs using Log Analytics

If you chose to write audit logs to Log Analytics:

1. In the [Azure portal](https://portal.azure.com), search for **SQL databases** and select your database, or search for **SQL servers** and select your server.
1. On the resource menu under **Security**, select **Auditing**.
1. At the top of the **Auditing** page, select **View audit logs**.

   :::image type="content" source="media/auditing-analyze-audit-logs/view-audit-logs.png" alt-text="Screenshot of the Auditing menu in the Azure portal where you can select the View audit logs option." lightbox="media/auditing-analyze-audit-logs/view-audit-logs.png":::

> [!NOTE]
> The **View audit logs** button appears on both server-level and database-level **Auditing** pages. When you select it from the **database** resource, you see audit records specific to that database. When you select it from the **server** resource, you see audit records for all databases on that server. Make sure you navigate to the correct resource level based on the scope of audit logs you need to review.

You have two ways to view the logs from the **Audit records** page:

- Select **Log Analytics** at the top of the page to open the logs view in the Log Analytics workspace, where you can customize the time range and the search query.
- Select **View dashboard** at the top of the page to open a dashboard displaying audit logs information, where you can drill down into **Security Insights** or **Access to Sensitive Data**. This dashboard helps you gain security insights for your data. You can also customize the time range and search query.

> [!TIP]
> The **View dashboard** option is available only when you access audit records from a **database-level** Auditing page that has database-level auditing enabled. If you configured server-level auditing only, you can still query the audit data directly in your Log Analytics workspace using the steps in the following section.

### Query audit logs directly in Log Analytics

You can also access audit logs directly from your Log Analytics workspace without navigating through the Auditing page. This approach is useful when you have server-level auditing only, or when you want to run custom queries across multiple databases.

1. In the Azure portal, open your **Log Analytics** workspace.
1. Under the **General** section, select **Logs**.
1. Start with a simple query, such as `search "SQLSecurityAuditEvents"` to view the audit logs.

From here, you can use [Azure Monitor logs](/azure/azure-monitor/logs/log-query-overview) to run advanced searches on your audit log data. Azure Monitor logs give you real-time operational insights using integrated search and custom dashboards to readily analyze millions of records across all your workloads and servers. For more information about Azure Monitor logs search language and commands, see [Azure Monitor logs search reference](/azure/azure-monitor/logs/log-query-overview).

## Analyze logs using Event Hubs

If you chose to write audit logs to Event Hubs:

- To consume audit logs data from Event Hubs, you need to set up a stream to consume events and write them to a target. For more information, see [Azure Event Hubs Documentation](/azure/event-hubs/index).
- Audit logs in Event Hubs are captured in the body of [Apache Avro](https://avro.apache.org/) events and stored using JSON formatting with UTF-8 encoding. To read the audit logs, you can use [Avro Tools](/azure/event-hubs/event-hubs-capture-overview#use-avro-tools), [Microsoft Fabric event streams](/fabric/real-time-analytics/event-streams/overview), or similar tools that process this format.

## Analyze logs using logs in an Azure storage account

If you chose to write audit logs to an Azure storage account, there are several methods you can use to view the logs:

- Audit logs are aggregated in the account you chose during setup. You can explore audit logs by using a tool such as [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer). In Azure storage, auditing logs are saved as a collection of blob files within a container named **sqldbauditlogs**. For more information about the hierarchy of the storage folders, naming conventions, and log format, see the [SQL Database audit log format](audit-log-format.md).

  1. In the [Azure portal](https://portal.azure.com), search for **SQL databases** and select your database, or search for **SQL servers** and select your server.
  1. On the resource menu under **Security**, select **Auditing**.
  1. At the top of the **Auditing** page, select **View audit logs**. The **Audit records** page opens, and you can view the logs.
  1. You can view specific dates by selecting **Filter** at the top of the **Audit records** page.
  1. You can switch between audit records that were created by the *server audit policy* and the *database audit policy* by toggling **Audit Source**.
- Use the system function `sys.fn_get_audit_file` (T-SQL) to return the audit log data in tabular format. For more information on using this function, see [sys.fn_get_audit_file](/sql/relational-databases/system-functions/sys-fn-get-audit-file-transact-sql).

- Use **Merge Audit Files** in SQL Server Management Studio (starting with SSMS 17):

  1. From the SSMS menu, select **File** > **Open** > **Merge Audit Files**.

     :::image type="content" source="media/auditing-analyze-audit-logs/merge-audit-files.png" alt-text="Screenshot that shows the Merge Audit Files menu option.":::

  1. The **Add Audit Files** dialog box opens. Select one of the **Add** options to choose whether to merge audit files from a local disk or import them from Azure Storage. You're required to provide your Azure Storage details and account key.

  1. After all files to merge have been added, select **OK** to complete the merge operation.

  1. The merged file opens in SSMS, where you can view and analyze it, as well as export it to an XEL or CSV file, or to a table.

- Use Power BI. You can view and analyze audit log data in Power BI. For more information, see [Using Azure Log Analytics in Power BI](/power-bi/transform-model/log-analytics/desktop-log-analytics-overview).
- Download log files from your Azure Storage blob container via the portal or by using a tool such as [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer).
  - After you have downloaded a log file locally, double-click the file to open, view, and analyze the logs in SSMS.
  - You can also download multiple files simultaneously in Azure Storage Explorer. To do so, right-click a specific subfolder and select **Save as** to save in a local folder.

- More methods:

  - After downloading several files or a subfolder that contains log files, you can merge them locally as described in the SSMS Merge Audit Files instructions described previously.
  - View blob auditing logs programmatically: [Query Extended Events Files](https://sqlscope.wordpress.com/2014/11/15/reading-extended-event-files-using-client-side-tools-only/) by using PowerShell.

## Related content

- [Auditing for Azure SQL Database and Azure Synapse Analytics](auditing-overview.md)
- [Get started with Azure SQL Managed Instance auditing](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
