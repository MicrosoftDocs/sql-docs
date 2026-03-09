---
title: Configure Advanced Threat Protection
description: Advanced Threat Protection detects anomalous database activities indicating potential security threats to the database in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 06/13/2025
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db"
ms.custom:
  - sqldbrb=1
  - sfi-image-nochange
---

# Configure Advanced Threat Protection for Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

[SQL Advanced Threat Protection](threat-detection-overview.md) for Azure SQL Database detects anomalous activities indicating unusual and potentially harmful attempts to access or exploit databases. Advanced Threat Protection can identify **Potential SQL injection**, **Access from unusual location or data center**, **Access from unfamiliar principal or potentially harmful application**, and **Brute force SQL credentials** - see more details in [Advanced Threat Protection alerts](threat-detection-overview.md#alerts).

You can receive notifications about the detected threats via [email notifications](threat-detection-overview.md#explore-detection-of-a-suspicious-event) or [Azure portal](threat-detection-overview.md#explore-alerts-in-the-azure-portal)

[SQL Advanced Threat Protection](threat-detection-overview.md) is part of the [Microsoft Defender for SQL](azure-defender-for-sql.md) offering, which is a unified package for advanced SQL security capabilities. Advanced Threat Protection can be accessed and managed via the central Microsoft Defender for SQL portal.

## Set up Advanced Threat Protection in the Azure portal

1. Sign into the [Azure portal](https://portal.azure.com).
1. Navigate to the configuration page of the [Azure SQL Database logical server](logical-servers.md) you want to protect. Under **Security** settings, select **Microsoft Defender for Cloud**.
1. On the **Microsoft Defender for Cloud** configuration page:

   1. If Microsoft Defender for SQL hasn't yet been enabled, select **Enable Microsoft Defender for SQL**.

   1. Select **Configure**.

       :::image type="content" source="media/threat-detection-configure/enable-microsoft-defender-sql.png" alt-text="Enable Microsoft Defender for SQL." lightbox="media/threat-detection-configure/enable-microsoft-defender-sql.png":::

   1. Under **ADVANCED THREAT PROTECTION SETTINGS**, select **Add your contact details to the subscription's email settings in Defender for Cloud**.

       :::image type="content" source="media/threat-detection-configure/advanced-threat-protection-add-contact-details.png" alt-text="Select link to proceed to advanced threat protection settings.":::

   1. Provide the list of emails to receive notifications upon detection of anomalous database activities in the **Additional email addresses (separated by commas)** text box.
   1. Optionally customize the severity of alerts that will trigger notifications to be sent under **Notification types**.
   1. Select **Save**.

       :::image type="content" source="media/threat-detection-configure/advanced-threat-protection-configure-emails.png" alt-text="Enter emails for Advanced Threat Protection notifications." lightbox="media/threat-detection-configure/advanced-threat-protection-configure-emails.png":::

## Set up Advanced Threat Protection using PowerShell

For a script example, see [Configure auditing and Advanced Threat Protection using PowerShell](/powershell/module/az.sql/update-azsqlserveradvancedthreatprotectionsetting?preserve-view=true).

## Related content

- [SQL Advanced Threat Protection](threat-detection-overview.md)
- [Microsoft Defender for SQL](azure-defender-for-sql.md)
- [Auditing for Azure SQL Database and Azure Synapse Analytics](auditing-overview.md)
- [Microsoft Defender for Cloud](/azure/security-center/security-center-introduction)
- [SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/)
