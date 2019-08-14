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
      For **Test Connection** to work, the service principal should be assigned at least **Storage Blob Data Reader** role to the storage account.
      Refer to [this](https://docs.microsoft.com/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal) page for details.
- **Environment:** Specifies the cloud environment hosting the storage account.

## Managed Identities for Azure Resources Authentication
When running SSIS packages on [Azure-SSIS integration runtime in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime), you can use the [managed identity](https://docs.microsoft.com/azure/data-factory/connector-azure-sql-database#managed-identity) that is associated with your data factory for Azure storage authentication. The designated factory can access and copy data from or to your storage account by using this identity.

Refer to [Authenticate access to Azure Storage using Azure Active Directory](https://docs.microsoft.com/azure/storage/common/storage-auth-aad) for Azure Storage authentication in general. To use managed identity authentication for Azure storage, follow these steps to configure your storage account:

1. **[Find the data factory managed identity from the Azure portal](https://docs.microsoft.com/azure/data-factory/data-factory-service-identity)**. Go to your data factory's **Properties**. Copy the **Managed Identity Application ID** (NOT **Managed Identity Object ID**).

1. **Grant the managed identity proper permission in your storage account**. Refer to [Manage access rights to Azure Storage data with RBAC](https://docs.microsoft.com/azure/storage/common/storage-auth-aad-rbac-portal) with more details on the roles.

    - **As source**, in Access control (IAM), grant at least **Storage Blob Data Reader** role.
    - **As destination**, in Access control (IAM), grant at least **Storage Blob Data Contributor** role.

Then **configure managed identity authentication** for the Azure storage connection manager. There are two options to do this.

1. Configure at design time. In SSIS Designer, double-click the Azure storage connection manager to open **Azure Storage Connection Manager Editor** and check **Use managed identity to authenticate on Azure**.
    > [!NOTE]
    >  Currently this option DOES NOT take effect (indicating that managed identity authentication does not work) when you run SSIS package in SSIS Designer or [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server.
    
1. Configure at run time. When you execute the package via [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/integration-services/ssis-quickstart-run-ssms) or [Azure Data Factory Execute SSIS Package activity](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity), find the Azure storage connection manager and update its property **ConnectUsingManagedIdentity** to **True**.
    > [!NOTE]
    >  In Azure-SSIS integration runtime, all other authentication methods (e.g., access key, service principal) preconfigured on the Azure storage connection manager will be **overridden** when managed identity authentication is used for storage operations.

> [!NOTE]
>  To configure managed identity authentication on existing packages, the preferred way is to rebuild your SSIS project with the [latest SSIS Designer](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt) at least once and redeploy that SSIS project to your Azure-SSIS integration runtime so that the new connection manager property **ConnectUsingManagedIdentity** will automatically be added to all Azure storage connection managers in your SSIS project. The alternative way is to directly use property override with property path **\Package.Connections[{the name of your connection manager}].Properties[ConnectUsingManagedIdentity]** at run time.

## See Also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)
