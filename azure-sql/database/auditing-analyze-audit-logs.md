---
title: Use Auditing to analyze audit logs and reports
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: Use Auditing to analyze logs in Log Analytics, Event Hubs, or through an Azure storage account.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma
ms.date: 04/26/2023
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
---
# Use Auditing to analyze audit logs and reports

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

This article provides an overview of analyzing audit logs using Auditing for [Azure SQL Database](sql-database-paas-overview.md) and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is). You can use Auditing to analyze audit logs stored in:

- Log Analytics
- Event Hubs
- Azure storage

## Analyze logs using Log Analytics

If you chose to write audit logs to Log Analytics:

1. Use the [Azure portal](https://portal.azure.com).
1. Go to the relevant database resource.
1. At the top of the database's **Auditing** page, select **View audit logs**.

   :::image type="content" source="./media/auditing-overview/auditing-view-audit-logs.png" alt-text="Screenshot of the Auditing menu in the Azure portal where you can select the View audit logs option.":::

You have two ways to view the logs:

- Selecting **Log Analytics** at the top of the **Audit records** page opens the logs view in the Log Analytics workspace, where you can customize the time range and the search query.

  :::image type="content" source="./media/auditing-overview/auditing-log-analytics.png" alt-text="Screenshot of selecting Log Analytics in the Audit records menu in the Azure portal.":::

- Selecting **View dashboard** at the top of the **Audit records** page opens a dashboard displaying audit logs information, where you can drill down into **Security Insights** or **Access to Sensitive Data**. This dashboard is designed to help you gain security insights for your data. You can also customize the time range and search query.

  :::image type="content" source="media/auditing-overview/auditing-view-dashboard.png" alt-text="Screenshot of selecting view dashboard in the Audit records menu in the Azure portal.":::

  :::image type="content" source="media/auditing-overview/auditing-dashboard.png" alt-text="Screenshot of the Auditing dashboard.":::

- Alternatively, you can also access the audit logs from the **Log Analytics** menu. Open your **Log Analytics** workspace and under the **General** section, and select **Logs**. You can start with a simple query, such as: *search "SQLSecurityAuditEvents"* to view the audit logs. From here, you can also use [Azure Monitor logs](/azure/azure-monitor/logs/log-query-overview) to run advanced searches on your audit log data. Azure Monitor logs gives you real-time operational insights using integrated search and custom dashboards to readily analyze millions of records across all your workloads and servers. For extra useful information about Azure Monitor logs search language and commands, see [Azure Monitor logs search reference](/azure/azure-monitor/logs/log-query-overview).

## Analyze logs using Event Hubs

If you chose to write audit logs to Event Hubs:

- To consume audit logs data from Event Hubs, you need to set up a stream to consume events and write them to a target. For more information, see [Azure Event Hubs Documentation](/azure/event-hubs/index).
- Audit logs in Event Hubs are captured in the body of [Apache Avro](https://avro.apache.org/) events and stored using JSON formatting with UTF-8 encoding. To read the audit logs, you can use [Avro Tools](/azure/event-hubs/event-hubs-capture-overview#use-avro-tools), [Microsoft Fabric event streams](/fabric/real-time-analytics/event-streams/overview), or similar tools that process this format.

## Analyze logs using logs in an Azure storage account

If you chose to write audit logs to an Azure storage account, there are several methods you can use to view the logs:

- Audit logs are aggregated in the account you chose during setup. You can explore audit logs by using a tool such as [Azure Storage Explorer](https://storageexplorer.com/). In Azure storage, auditing logs are saved as a collection of blob files within a container named **sqldbauditlogs**. For more information about the hierarchy of the storage folders, naming conventions, and log format, see the [SQL Database Audit Log Format](./audit-log-format.md).

  1. Use the [Azure portal](https://portal.azure.com).
  1. Open the relevant database resource.
  1. At the top of the database's **Auditing** page, select **View audit logs**.

     :::image type="content" source="./media/auditing-overview/auditing-view-audit-logs.png" alt-text="Screenshot showing how to view audit logs.":::

     The **Audit records** page opens, and you're able to view the logs.

  1. You can view specific dates by selecting **Filter** at the top of the **Audit records** page.
  1. You can switch between audit records that were created by the *server audit policy* and the *database audit policy* by toggling **Audit Source**.

     :::image type="content" source="./media/auditing-overview/8_auditing_get_started_blob_audit_records.png" alt-text="Screenshot that shows the options for viewing the audit records.":::

- Use the system function `sys.fn_get_audit_file` (T-SQL) to return the audit log data in tabular format. For more information on using this function, see [sys.fn_get_audit_file](/sql/relational-databases/system-functions/sys-fn-get-audit-file-transact-sql).

- Use **Merge Audit Files** in SQL Server Management Studio (starting with SSMS 17):

  1. From the SSMS menu, select **File** > **Open** > **Merge Audit Files**.

     :::image type="content" source="./media/auditing-overview/9_auditing_get_started_ssms_1.png" alt-text="Screenshot that shows the Merge Audit Files menu option.":::

  1. The **Add Audit Files** dialog box opens. Select one of the **Add** options to choose whether to merge audit files from a local disk or import them from Azure Storage. You're required to provide your Azure Storage details and account key.

  1. After all files to merge have been added, select **OK** to complete the merge operation.

  1. The merged file opens in SSMS, where you can view and analyze it, as well as export it to an XEL or CSV file, or to a table.

- Use Power BI. You can view and analyze audit log data in Power BI. For more information and to access a downloadable template, see [Analyze audit log data in Power BI](https://techcommunity.microsoft.com/t5/azure-database-support-blog/sql-azure-blob-auditing-basic-power-bi-dashboard/ba-p/368895).
- Download log files from your Azure Storage blob container via the portal or by using a tool such as [Azure Storage Explorer](https://storageexplorer.com/).
  - After you have downloaded a log file locally, double-click the file to open, view, and analyze the logs in SSMS.
  - You can also download multiple files simultaneously in Azure Storage Explorer. To do so, right-click a specific subfolder and select **Save as** to save in a local folder.

- More methods:

  - After downloading several files or a subfolder that contains log files, you can merge them locally as described in the SSMS Merge Audit Files instructions described previously.
  - View blob auditing logs programmatically: [Query Extended Events Files](https://sqlscope.wordpress.com/2014/11/15/reading-extended-event-files-using-client-side-tools-only/) by using PowerShell.

## See also

- [Auditing overview](auditing-overview.md)
- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
