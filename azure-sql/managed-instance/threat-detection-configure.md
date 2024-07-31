---
title: Configure Advanced Threat Protection
titleSuffix: Azure SQL Managed Instance
description: Advanced Threat Protection detects anomalous database activities indicating potential security threats to the database in Azure SQL Managed Instance.
author: rmatchoro
ms.author: ronmat
ms.reviewer: vanto, mathoma
ms.date: 12/01/2020
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: how-to
ms.custom: sqldbrb=1
---
# Configure Advanced Threat Protection in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[Advanced Threat Protection](../database/threat-detection-overview.md) for an [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) detects anomalous activities indicating unusual and potentially harmful attempts to access or exploit databases. Advanced Threat Protection can identify **Potential SQL injection**, **Access from unusual location or data center**, **Access from unfamiliar principal or potentially harmful application**, and **Brute force SQL credentials** - see more details in [Advanced Threat Protection alerts](../database/threat-detection-overview.md#alerts).

You can receive notifications about the detected threats via [email notifications](../database/threat-detection-overview.md#explore-detection-of-a-suspicious-event) or [Azure portal](../database/threat-detection-overview.md#explore-alerts-in-the-azure-portal)

[Advanced Threat Protection](../database/threat-detection-overview.md) is part of the [Microsoft Defender for SQL](../database/azure-defender-for-sql.md)  offering, which is a unified package for advanced SQL security capabilities. Advanced Threat Protection can be accessed and managed via the central Microsoft Defender for SQL portal.

## Azure portal

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Navigate to the configuration page of the instance of SQL Managed Instance you want to protect. Under **Security**, select **Microsoft Defender for Cloud**.
1. For **Enablement Status**, select **Configure** to open the **Server settings** page.

   :::image type="content" source="media/threat-detection-configure/defender-cloud-configure.png" alt-text="Screenshot shows the Microsoft Defender for Cloud page for a SQL managed instance with the option to configure." lightbox="media/threat-detection-configure/defender-cloud-configure.png":::

1. In the **Server settings** configuration page, for **Microsoft Defender for SQL**, select **ON**.

   :::image type="content" source="../database/media/azure-defender-for-sql/set-up-advanced-threat-protection-mi.png" alt-text="Screenshot shows the Server settings page where you can set up advanced threat protection.":::

1. Choose **Select Storage account** and then select a storage account where Microsoft Defender for Cloud saves anomalous threat audit records.
1. Under **Advanced Threat Protection Settings**, select **Add your contact details to the subscriptions email settings in Defender for Cloud**.
1. For **Email recipients**, select users by role or add individual email addresses.

   :::image type="content" source="media/threat-detection-configure/defender-cloud-email-notification.png" alt-text="Screenshot shows the Email notifications page where you can specify email recipients and notification types.":::

1. Select the **Notification types** sent by Microsoft Defender for Cloud. Learn more about [Advanced Threat Protection alerts](../database/threat-detection-overview.md).
1. Select **Save**.

## Next steps

- Learn more about [Advanced Threat Protection](../database/threat-detection-overview.md).
- Learn about managed instances, see [What is an Azure SQL Managed Instance](sql-managed-instance-paas-overview.md).
- Learn more about [Advanced Threat Protection for Azure SQL Database](../database/threat-detection-configure.md).
- Learn more about [SQL Managed Instance auditing](./auditing-configure.md).
- Learn more about [Microsoft Defender for Cloud](/azure/security-center/security-center-introduction).
