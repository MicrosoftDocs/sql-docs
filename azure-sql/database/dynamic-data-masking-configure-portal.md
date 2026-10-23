---
title: "Dynamic Data Masking"
description: How to get started with Azure SQL Database dynamic data masking in the Azure portal.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: matripathy, vanto
ms.date: 06/13/2025
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db "
---
# Get started with SQL Database dynamic data masking with the Azure portal

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article shows you how to implement [dynamic data masking](dynamic-data-masking-overview.md) with the Azure portal. You can also implement dynamic data masking using [Azure SQL Database PowerShell cmdlets](/powershell/module/az.sql/) or the [REST API](/rest/api/sql/).

> [!NOTE]
> This feature cannot be set using the Azure portal for Azure SQL Managed Instance (use PowerShell or REST API). For more information, see [Dynamic Data Masking](/azure/azure-sql/database/dynamic-data-masking-overview?view=azuresql-mi&preserve-view=true).

## Enable dynamic data masking

1. Launch the [Azure portal](https://portal.azure.com).
1. Go to your database resource in the Azure portal. 
1. Under the **Security** section, select **Dynamic Data Masking**. 
1. In the **Dynamic Data Masking** configuration page, you might see some database columns that the recommendations engine has flagged for masking. In order to accept the recommendations, select **Add Mask** for one or more columns, and a mask is created based on the default type for this column. You can change the masking function by selecting on the masking rule and editing the masking field format to a different format of your choice. Select **Save** to save your settings. In the following screenshot, you can see recommended dynamic data masks for the sample `AdventureWorksLT` database.

    :::image type="content" source="media/dynamic-data-masking-configure-portal/recommendations.png" alt-text="Screenshot that shows the Dynamic Data Masking configuration page, based on the AdventureWorksLT sample database." lightbox="media/dynamic-data-masking-configure-portal/recommendations.png":::

1. To add a mask for any column in your database, at the top of the **Dynamic Data Masking** configuration page, select **Add Mask** to open the **Add Masking Rule** configuration page.

    :::image type="content" source="media/dynamic-data-masking-configure-portal/add-mask.png" alt-text="Screenshot that shows the Add Masking Rule configuration page." lightbox="media/dynamic-data-masking-configure-portal/add-mask.png":::

1. Select the **Schema**, **Table** and **Column** to define the designated field for masking.
1. **Select how to mask** from the list of sensitive data masking categories.

    :::image type="content" source="media/dynamic-data-masking-configure-portal/mask-field-format.png" alt-text="Screenshot that shows the sensitive data masking categories under the Select how to mask section." lightbox="media/dynamic-data-masking-configure-portal/mask-field-format.png":::

1. Select **Add** in the data masking rule page to update the set of masking rules in the dynamic data masking policy.
1. Type the SQL authenticated users or authenticated identities from Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) that should be excluded from masking, and have access to the unmasked sensitive data. This should be a semicolon-separated list of users. Users with administrator privileges always have access to the original unmasked data.

    :::image type="content" source="media/dynamic-data-masking-configure-portal/excluded-users.png" alt-text="Screenshot from the Azure portal of the SQL users excluded from masking (administrators are always excluded) list box.":::

    > [!TIP]
    > To make it so the application layer can display sensitive data for application privileged users, add the SQL user or Microsoft Entra identity the application uses to query the database. It is highly recommended that this list contain a minimal number of privileged users to minimize exposure of the sensitive data.

1. Select **Save** in the data masking configuration page to save the new or updated masking policy.

## Related content

- [Dynamic data masking](dynamic-data-masking-overview.md)
- [Azure SQL Database PowerShell cmdlets](/powershell/module/az.sql/)
- [REST API](/rest/api/sql/)
