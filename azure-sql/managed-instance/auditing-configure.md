---
title: SQL Managed Instance auditing
description: Learn how to get started with Azure SQL Managed Instance auditing using T-SQL
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: vanto, randolphwest, mathoma
ms.date: 12/23/2022
ms.service: sql-managed-instance
ms.subservice: security
ms.topic: how-to
ms.custom: sqldbrb=1
f1_keywords:
  - "mi.azure.sqlaudit.general.f1"
---
# Get started with Azure SQL Managed Instance auditing

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) auditing tracks database events and writes them to an audit log in your Azure storage account. Auditing also:

- Helps you maintain regulatory compliance, understand database activity, and gain insight into discrepancies and anomalies that could indicate business concerns or suspected security violations.
- Enables and facilitates adherence to compliance standards, although it doesn't guarantee compliance. For more information, see the [Microsoft Azure Trust Center](https://www.microsoft.com/trust-center/compliance/compliance-overview) where you can find the most current list of SQL Managed Instance compliance certifications.

> [!IMPORTANT]  
> Auditing for Azure SQL Database, Azure Synapse and Azure SQL Managed Instance is optimized for availability and performance. During very high activity, or high network load, Azure SQL Database, Azure Synapse and Azure SQL Managed Instance allow operations to proceed and may not record some audited events.

## Set up auditing for your server to Azure Storage

The following section describes the configuration of auditing on your managed instance.

1. Go to the [Azure portal](https://portal.azure.com).
1. Create an Azure Storage **container** where audit logs are stored.

   1. Navigate to the Azure storage account where you would like to store your audit logs.

      - Use a storage account in the same region as the managed instance to avoid cross-region reads/writes.
      - If your storage account is behind a Virtual Network or a Firewall, see [Grant access from a virtual network](/azure/storage/common/storage-network-security#grant-access-from-a-virtual-network).
      - If you change retention period from 0 (unlimited retention) to any other value, retention will only apply to logs written after retention value was changed (logs written during the period when retention was set to *unlimited* are preserved, even after retention is enabled).

   1. In the storage account, go to **Overview** and select **Blobs**.

      :::image type="content" source="media/auditing-configure/1_blobs_widget.png" alt-text="Screenshot showing the Azure Blobs widget.":::

   1. In the top menu, select **+ Container** to create a new container.

      :::image type="content" source="media/auditing-configure/2_create_container_button.png" alt-text="Screenshot showing the Create blob container icon.":::

   1. Provide a container **Name**, set **Public access level** to **Private**, and then select **OK**.

      :::image type="content" source="media/auditing-configure/3_create_container_config.png" alt-text="Screenshot showing the Create blob container configuration.":::

    > [!IMPORTANT]  
    > Customers wishing to configure an immutable log store for their server- or database-level audit events should follow the [instructions provided by Azure Storage](/azure/storage/blobs/immutable-time-based-retention-policy-overview#allow-protected-append-blobs-writes). (Please ensure you have selected **Allow additional appends** when you configure the immutable blob storage.)

1. After you create the container for the audit logs, there are two ways to configure it as the target for the audit logs: [using T-SQL](#blobtsql) or [using the SQL Server Management Studio (SSMS) UI](#blobssms):

   - <a id="blobtsql"></a>**Configure blob storage for audit logs using T-SQL:**

     1. In the containers list, select the newly created container and then select **Container properties**.

        :::image type="content" source="media/auditing-configure/4_container_properties_button.png" alt-text="Screenshot showing the Blob container properties button.":::

     1. Copy the container URL by selecting the copy icon and save the URL (for example, in Notepad) for future use. The container URL format should be `https://<StorageName>.blob.core.windows.net/<ContainerName>`

        :::image type="content" source="media/auditing-configure/5_container_copy_name.png" alt-text="Screenshot showing the Blob container copy URL.":::

     1. Generate an Azure Storage **SAS token** to grant managed instance auditing access rights to the storage account:

        - Navigate to the Azure storage account where you created the container in the previous step.

        - Select **Shared access signature** in the **Storage Settings** menu.

          :::image type="content" source="media/auditing-configure/6_storage_settings_menu.png" alt-text="Shared access signature icon in storage settings menu.":::

        - Configure the SAS as follows:

          - **Allowed services**: Blob

          - **Start date**: to avoid time zone-related issues, use yesterday's date

          - **End date**: choose the date on which this SAS token expires

            > [!NOTE]  
            > Renew the token upon expiry to avoid audit failures.

          - Select **Generate SAS**.

            :::image type="content" source="media/auditing-configure/7_sas_configure.png" alt-text="Screenshot showing the SAS configuration.":::

        - The SAS token appears at the bottom. Copy the token by selecting on the copy icon, and save it (for example, in Notepad) for future use.

          :::image type="content" source="media/auditing-configure/8_sas_copy.png" alt-text="Screenshot showing how to copy SAS token.":::

          > [!IMPORTANT]  
          > Remove the question mark (`?`) character from the beginning of the token.

     1. Connect to your managed instance via SQL Server Management Studio or any other supported tool.

     1. Execute the following T-SQL statement to **create a new credential** using the container URL and SAS token that you created in the previous steps:

        ```SQL
        CREATE CREDENTIAL [<container_url>]
        WITH IDENTITY='SHARED ACCESS SIGNATURE',
        SECRET = '<SAS KEY>'
        GO
        ```

     1. Execute the following T-SQL statement to create a new server audit (choose your own audit name, and use the container URL that you created in the previous steps). If not specified, the `RETENTION_DAYS` default is 0 (unlimited retention):

        ```SQL
        CREATE SERVER AUDIT [<your_audit_name>]
        TO URL (PATH ='<container_url>', RETENTION_DAYS = <integer>);
        GO
        ```

     1. Continue by [creating a server audit specification or database audit specification](#createspec).

   - <a id="blobssms"></a>**Configure blob storage for audit logs, using SQL Server Management Studio 18 and later versions:**

     1. Connect to the managed instance using the SQL Server Management Studio UI.

     1. Expand the root note of Object Explorer.

     1. Expand the **Security** node, right-click on the **Audits** node, and select **New Audit**:

        :::image type="content" source="media/auditing-configure/10_mi_SSMS_new_audit.png" alt-text="Screenshot showing how to Expand security and audit node.":::

     1. Make sure **URL** is selected in **Audit destination** and select **Browse**:

        :::image type="content" source="media/auditing-configure/11_mi_SSMS_audit_browse.png" alt-text="Screenshot showing how to Browse Azure Storage.":::

     1. (Optional) Sign in to your Azure account:

        :::image type="content" source="media/auditing-configure/12_mi_SSMS_sign_in_to_azure.png" alt-text="Screenshot showing how to Sign in to Azure.":::

     1. Select a subscription, storage account, and blob container from the dropdowns, or create your own container by selecting on **Create**. Once you have finished, select **OK**:

        :::image type="content" source="media/auditing-configure/13_mi_SSMS_select_subscription_account_container.png" alt-text="Select Azure subscription, storage account, and blob container.":::

     1. Select **OK** in the **Create Audit** dialog.

        > [!NOTE]  
        > When using SQL Server Management Studio UI to create audit, a credential to the container with SAS key will be automatically created.

     1. <a id="createspec"></a>After you configure the blob container as target for the audit logs, create and enable a server audit specification or database audit specification as you would for SQL Server:

   - [Create server audit specification T-SQL guide](/sql/t-sql/statements/create-server-audit-specification-transact-sql)
   - [Create database audit specification T-SQL guide](/sql/t-sql/statements/create-database-audit-specification-transact-sql)

1. Enable the server audit that you created in step 3:

    ```SQL
    ALTER SERVER AUDIT [<your_audit_name>]
    WITH (STATE = ON);
    GO
    ```

For additional information:

- [Auditing differences between Azure SQL Managed Instance and a database in SQL Server](#audit-differences-between-databases-in-azure-sql-managed-instance-and-databases-in-sql-server)
- [CREATE SERVER AUDIT](/sql/t-sql/statements/create-server-audit-transact-sql)
- [ALTER SERVER AUDIT](/sql/t-sql/statements/alter-server-audit-transact-sql)

## Auditing of Microsoft Support operations

Auditing of Microsoft Support operations for SQL Managed Instance allows you to audit Microsoft support engineers' operations when they need to access your server during a support request. The use of this capability, along with your auditing, enables more transparency into your workforce and allows for anomaly detection, trend visualization, and data loss prevention.

To enable auditing of Microsoft Support operations, navigate to **Create Audit** under **Security** > **Audit** in your SQL Manage Instance, and select **Microsoft support operations**.

:::image type="content" source="media/auditing-configure/support-operations.png" alt-text="Screenshot showing the Create audit icon.":::

> [!NOTE]  
> You must create a separate server audit for auditing Microsoft operations. If you enable this check box for an existing audit then it will overwrite the audit and only log support operations.

## Set up auditing for your server to Event Hubs or Azure Monitor logs

Audit logs from a managed instance can be  sent to Azure Event Hubs or Azure Monitor logs. This section describes how to configure this:

1. Navigate in the [Azure portal](https://portal.azure.com/) to the managed instance.

1. Select **Diagnostic settings**.

1. Select **Turn on diagnostics**. If diagnostics is already enabled, **+Add diagnostic setting** will show instead.

1. Select **SQLSecurityAuditEvents** in the list of logs.

1. If you are configuring Microsoft support operations, select **DevOps operations Audit Logs** in the list of logs.

1. Select a destination for the audit events: Event Hubs, Azure Monitor logs, or  both. Configure for each target the required parameters (for example, Log Analytics workspace).

1. Select **Save**.

   :::image type="content" source="media/auditing-configure/9-mi-configure-diagnostics.png" alt-text="Screenshot showing how to configure diagnostic settings.":::

1. Connect to the managed instance using **SQL Server Management Studio (SSMS)** or any other supported client.

1. Execute the following T-SQL statement to create a server audit:

   ```SQL
   CREATE SERVER AUDIT [<your_audit_name>] TO EXTERNAL_MONITOR;
   GO
   ```

1. Create and enable a server audit specification or database audit specification as you would for SQL Server:

   - [Create Server audit specification T-SQL guide](/sql/t-sql/statements/create-server-audit-specification-transact-sql)
   - [Create Database audit specification T-SQL guide](/sql/t-sql/statements/create-database-audit-specification-transact-sql)

1. Enable the server audit created in step 8:

   ```SQL
   ALTER SERVER AUDIT [<your_audit_name>]
   WITH (STATE = ON);
   GO
   ```

## Set up audit using T-SQL

```SQL
-- Create audit without OPERATOR_AUDIT - Will audit standard SQL Audit events
USE [master];
GO

CREATE SERVER AUDIT testingauditnodevops TO EXTERNAL_MONITOR;
GO

CREATE SERVER AUDIT SPECIFICATION testingaudit_Specification_nodevops
FOR SERVER AUDIT testingauditnodevops ADD (SUCCESSFUL_LOGIN_GROUP),
    ADD (BATCH_COMPLETED_GROUP),
    ADD (FAILED_LOGIN_GROUP)
WITH (STATE = ON);
GO

ALTER SERVER AUDIT testingauditnodevops
    WITH (STATE = ON);
GO

-- Create separate audit without OPERATOR_AUDIT ON - Will audit Microsoft Support Operations
USE [master]

CREATE SERVER AUDIT testingauditdevops TO EXTERNAL_MONITOR
    WITH (OPERATOR_AUDIT = ON);
GO

CREATE SERVER AUDIT SPECIFICATION testingaudit_Specification_devops
FOR SERVER AUDIT testingauditdevops ADD (SUCCESSFUL_LOGIN_GROUP),
    ADD (BATCH_COMPLETED_GROUP),
    ADD (FAILED_LOGIN_GROUP)
WITH (STATE = ON);
GO

ALTER SERVER AUDIT testingauditdevops
    WITH (STATE = ON);
GO
```

## Consume audit logs

### Consume logs stored in Azure Storage

There are several methods you can use to view blob auditing logs.

- Use the system function `sys.fn_get_audit_file` (T-SQL) to return the audit log data in tabular format. For more information on using this function, see the [sys.fn_get_audit_file documentation](/sql/relational-databases/system-functions/sys-fn-get-audit-file-transact-sql).

- You can explore audit logs by using a tool such as [Azure Storage Explorer](https://azure.microsoft.com/features/storage-explorer/). In Azure Storage, auditing logs are saved as a collection of blob files within a container that was defined to store the audit logs. For more information about the hierarchy of the storage folder, naming conventions, and log format, see the [Blob Audit Log Format Reference](../database/audit-log-format.md).

- For a full list of audit log consumption methods, refer to [Get started with Azure SQL Database auditing](../database/auditing-overview.md).

### Consume logs stored in Event Hubs

To consume audit logs data from Event Hubs, you will need to set up a stream to consume events and write them to a target. For more information, see the Azure Event Hubs documentation.

### Consume and analyze logs stored in Azure Monitor logs

If audit logs are written to Azure Monitor logs, they are available in the Log Analytics workspace, where you can run advanced searches on the audit data. As a starting point, navigate to the Log Analytics workspace. Under the **General** section, select **Logs** and enter a basic query, such as: `search "SQLSecurityAuditEvents"` to view the audit logs.

Azure Monitor logs gives you real-time operational insights using integrated search and custom dashboards to readily analyze millions of records across all your workloads and servers. For more information about Azure Monitor logs search language and commands, see [Azure Monitor logs search reference](/azure/azure-monitor/logs/log-query-overview).

[!INCLUDE [azure-monitor-log-analytics-rebrand](../includes/azure-monitor-log-analytics-rebrand.md)]

## Audit differences between databases in Azure SQL Managed Instance and databases in SQL Server

The key differences between auditing in databases in Azure SQL Managed Instance and databases in SQL Server are:

- With Azure SQL Managed Instance, auditing works at the server level and stores `.xel` log files in Azure Blob storage.
- In SQL Server, audit works at the server level, but stores events in the file system and Windows event logs.

XEvent auditing in managed instances supports Azure Blob storage targets. File and Windows logs are **not supported**.

The key differences in the `CREATE AUDIT` syntax for auditing to Azure Blob storage are:

- A new syntax `TO URL` is provided and enables you to specify the URL of the Azure Blob storage container where the `.xel` files are placed.
- A new syntax `TO EXTERNAL MONITOR` is provided to enable Event Hubs and Azure Monitor log targets.
- The syntax `TO FILE` is **not supported** because Azure SQL Managed Instance can't access Windows file shares.
- Shutdown option is **not supported**.
- `queue_delay` of 0 is **not supported**.

## Next steps

- For a full list of audit log consumption methods, refer to [Get started with Azure SQL Database auditing](../database/auditing-overview.md).
