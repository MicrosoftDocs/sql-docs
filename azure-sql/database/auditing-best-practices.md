---
title: Auditing best practices for production environments
titleSuffix: Azure SQL Database & Azure Synapse Analytics
description: This article goes over best practices when using Auditing in production environments for Azure SQL Database and Azure Synapse Analytics.
author: sravanisaluru
ms.author: srsaluru
ms.reviewer: mathoma
ms.date: 04/26/2023
ms.service: azure-sql-database
ms.subservice: security
ms.topic: conceptual
---
# Auditing best practices for production environments

[!INCLUDE[appliesto-sqldb-asa](../includes/appliesto-sqldb-asa.md)]

Here are some recommendations for using Azure SQL Auditing in production environments.

## Auditing for geo-replicated databases

With geo-replicated databases, the secondary database has an identical auditing policy to the primary database when enabling auditing on the primary database. It's also possible to set up auditing on the secondary database by enabling auditing on the **secondary server**, independently from the primary database.

- Server-level (**recommended**): Turn on auditing on both the **primary server** and the **secondary server** - the primary and secondary databases are audited independently based on their respective server-level policy.
- Database-level: Database-level auditing for secondary databases can only be configured from the primary database auditing settings.
  - Auditing must be enabled on the *primary database itself*, not the server.
  - After auditing is enabled on the primary database, it will also become enabled on the secondary database.

    > [!IMPORTANT]  
    > With database-level auditing, the storage settings for the secondary database will be identical to those of the primary database, causing cross-regional traffic. We recommend that you enable only server-level auditing, and leave the database-level auditing disabled for all databases.

## Storage key regeneration

In production, you're likely to refresh your storage keys periodically. When writing audit logs to Azure storage, you need to resave your auditing policy when refreshing your keys. The process is as follows:

1. Open **Advanced properties** under **Storage**. In the **Storage Access Key** section, select **Secondary**. Then select **Save** at the top of the auditing configuration page.

   :::image type="content" source="./media/auditing-overview/5_auditing_get_started_storage_key_regeneration.png" alt-text="Screenshot that shows the process for selecting a secondary storage access key.":::

1. Go to the Azure **Storage account** that holds the key, and navigate to **Access keys**. Regenerate the primary access key.

   :::image type="content" source="./media/auditing-overview/6_auditing_get_started_regenerate_key.png" alt-text="Screenshot of the Access keys menu of the Azure Storage account.":::

1. Go back to the auditing configuration page, switch the storage access key from secondary to primary, and then select **OK**. Then select **Save** at the top of the auditing configuration page.
1. Go back to the storage configuration page and regenerate the secondary access key (in preparation for the next key's refresh cycle).

## Storage account encrypted with Azure Key Vault 

When you configure auditing with a storage account as the target, which is encrypted using a key vault behind a firewall, you must set up an **access policy** for the key vault. Navigate to the Azure Key Vault access policy, add a new policy with the necessary key permissions, enable the **unwrap key** option, and select the appropriate principal (such as the storage account) to grant access.

## Related content

- [Auditing overview](auditing-overview.md)
- Data Exposed episode [What's New in Azure SQL Auditing](/Shows/Data-Exposed/Whats-New-in-Azure-SQL-Auditing)
- [Auditing for SQL Managed Instance](../managed-instance/auditing-configure.md)
- [Auditing for SQL Server](/sql/relational-databases/security/auditing/sql-server-audit-database-engine)
