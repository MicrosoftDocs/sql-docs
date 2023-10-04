---
author: bobtabor-msft
ms.author: rotabor
ms.date: 06/01/2023
ms.service: sql-database
ms.topic: include
ms.custom: generated
---

Passwordless connections use Azure Active Directory (Azure AD) authentication to connect to Azure services, including Azure SQL Database. With Azure AD authentication, you can manage identities in a central location to simplify permission management. Learn more about configuring Azure AD authentication for your Azure SQL Database:

- [Azure AD authentication overview](/azure/azure-sql/database/authentication-aad-overview)
- [Configure Azure AD auth](/azure/azure-sql/database/authentication-aad-configure)

For this migration guide, ensure you have an Azure AD admin assigned to your Azure SQL Database.

1) Navigate to the **Azure Active Directory** page of your logical server.

1) Select **Set admin**.

1) In the **Azure Active Directory** flyout menu, search for the user you want to assign as admin.

1) Select the user and choose **Select**.

    :::image type="content" source="../../database/media/passwordless-connections/migration-enable-active-directory-small.png" lightbox="../../database/media/passwordless-connections/migration-enable-active-directory.png" alt-text="A screenshot showing how to enable active directory admin.":::
