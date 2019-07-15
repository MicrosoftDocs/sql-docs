---
title: "Azure Storage Connection Manager | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afpstorageconn.f1"
  - "sql14.dts.designer.afpstorageconn.f1"
ms.assetid: 68bd1d04-d20f-4357-a34e-7c9c76457062
author: janinezhang
ms.author: janinez
---
# Azure Storage Connection Manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]

  The **Azure Storage connection manager** enables an SSIS package to connect to an Azure Storage account.
   
 The **Azure Storage connection manager** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md). 
  
In the **Add SSIS Connection Manager** dialog box, select **AzureStorage**, and click **Add**.  
  
Following properties are available.

- **Service:** Specifies the storage service to connect to.
- **Account name:** Specifies the storage account name.
- **Authentication:** Specifies the authentication method to use. **AccessKey** and **ServicePrincipal** authentication are supported.
    - **AccessKey:** For this authentication method, specify the **Account key**.
    - **ServicePrincipal:** For this authentication method, specify the **Application ID**, **Application key**, **Tenant ID** of the service principal.
      The service principal should be assigned **Storage Blob Data Contributor** role to the storage account.
      Refer to [this](https://docs.microsoft.com/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal) page for details.
- **Environment:** Specifies the cloud environment hosting the storage account.
