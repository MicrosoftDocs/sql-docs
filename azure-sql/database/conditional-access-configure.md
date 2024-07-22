---
title: Conditional Access
titleSuffix: Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics
description: Learn how to configure Conditional Access for Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics.
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 02/13/2024
ms.service: sql-db-mi
ms.subservice: security
ms.topic: how-to
ms.custom: sqldbrb=1
tag: azure-synapse
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Conditional access

[!INCLUDE[appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

This article explains how to configure a [conditional access policy](/entra/identity/conditional-access/overview) for [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) in a tenant. 

Microsoft Entra Conditional Access enables organizations to create policy decisions at the tenant level for access to resources. For example, this article uses a conditional access policy to _require_ multifactor authentication for user access to any Azure SQL or Azure Synapse Analytics resources in a tenant.


## Prerequisites

- Full conditional access requires, at minimum, a [P1 Microsoft Entra ID tenant](/entra/fundamentals/get-started-premium) in the same subscription as your database or instance. Only limited multifactor authentication is available with a free Microsoft Entra ID tenant - conditional access is not supported. 
- You must configure Azure SQL Database, Azure SQL Managed Instance, or a dedicated SQL pool in Azure Synapse to support Microsoft Entra authentication. For specific steps, see [Configure and manage Microsoft Entra authentication](authentication-aad-configure.md).  

> [!NOTE]
> Conditional access policies only apply to users, and not to programmatic connections, such as service principals and managed identities. 

## Configure conditional access

You can configure a conditional access policy by using the Azure portal. 

> [!NOTE]
> When configuring conditional access for Azure SQL Managed Instance, or Azure Synapse Analytics, the cloud app target resource is *Azure SQL Database*, which is the Microsoft application automatically created in any Microsoft Entra tenant with Azure SQL or Azure Synapse Analytics resources.  

To configure your conditional access policy, follow these steps: 

1. Sign into the [Azure portal](https://portal.azure.com), search for `Enterprise Applications` and choose **Enterprise Applications**: 

   :::image type="content" source="media/conditional-access-configure/enterprise-applications.png" alt-text="Screenshot of searching for enterprise applications in the Azure portal.":::

1. Select **All applications** under **Manage** on the **Enterprise applications** page, update the existing filter to `Application type == Microsoft Applications` and then search for Azure SQL Database - even if you're configuring a policy for Azure SQL Managed Instance or Azure Synapse Analytics: 

   :::image type="content" source="media/conditional-access-configure/azure-sql-database-microsoft-application.png" alt-text="Screenshot of Azure SQL Database as a Microsoft Application in the Azure portal.":::

   The first party application Azure SQL Database is registered in a tenant when the first Azure SQL or Azure Synapse Analytics instance is created. If you don't see Azure SQL Database, check that one of those resources is deployed in your tenant.

1. Select the **Azure SQL Database** application to navigate to the **Overview** page for your Azure SQL Database Enterprise Application.
1. Select **Conditional Access** under **Security** to open the **Conditional Access** page. Select **+ New policy** to open the **New Conditional Access policy** page: 

   :::image type="content" source="media/conditional-access-configure/azure-sql-database-new-policies.png" alt-text="Screenshot of the Conditional Access page for Azure SQL Database in the Azure portal. ":::

1. On the **New Conditional Access policy** page, select **1 app included** under **Target resources** to validate that Azure SQL Database is the included application. 

   :::image type="content" source="media/conditional-access-configure/target-resources.png" alt-text="Screenshot of Azure SQL Database designated as the target resource in the Azure portal. ":::

1. Under **Assignments**, select **Specific users included**, and then check **Select users and groups** under **Include**. Select **0 users and groups selected** to open the **Select users and groups page** and search for the user or group you want to add.  Check the box next to the group or user you want to choose, and then use **Select** to apply your settings and close the window. 

   :::image type="content" source="./media/conditional-access-configure/add-user-assignments.png" alt-text="Screenshot showing how to select users and groups on the new Conditional Access policy page." lightbox="./media/conditional-access-configure/add-user-assignments.png":::  

1. Under **Access controls**, select **0 controls selected** to open the **Grant** page. Check **Grant access** and then choose the policy you want to apply, such as **Require multifactor authentication**.  Use **Select** to apply your access settings. 

   :::image type="content" source="./media/conditional-access-configure/grant-access.png" alt-text="Screenshot of the Grant page with the policy selected.":::  

1. Select **Create** to save and apply your policy to your resource. 

Once you're done, you can view your new conditional access policy on the **Conditional Access** page for your Azure SQL Database Enterprise Application. 




## Related content

For a tutorial, see [Secure Azure SQL Database](secure-database-tutorial.md).
