---
author: bobtabor-msft
ms.author: rotabor
ms.date: 06/01/2023
ms.service: azure-sql-database
ms.topic: include
ms.custom: generated
---

Passwordless connections use Microsoft Entra authentication to connect to Azure services, including Azure SQL Database. Microsoft Entra authentication, you can manage identities in a central location to simplify permission management. Learn more about configuring Microsoft Entra authentication for your Azure SQL Database:

- [Microsoft Entra authentication overview](/azure/azure-sql/database/authentication-aad-overview)
- [Configure Microsoft Entra auth](/azure/azure-sql/database/authentication-aad-configure)

For this migration guide, ensure you have a Microsoft Entra admin assigned to your Azure SQL Database.

1) Navigate to the **Microsoft Entra** page of your logical server.

1) Select **Set admin** to open the **Microsoft Entra ID** flyout menu.

1) In the **Microsoft Entra ID** flyout menu, search for the user you want to assign as admin.

1) Select the user and choose **Select**.

    :::image type="content" source="../../database/media/passwordless-connections/migration-enable-active-directory-small.png" lightbox="../../database/media/passwordless-connections/migration-enable-active-directory.png" alt-text="A screenshot showing how to enable Microsoft Entra admin.":::
