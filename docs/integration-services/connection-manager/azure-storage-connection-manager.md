---
title: "Azure Storage connection manager | Microsoft Docs"
description: The Azure Storage connection manager enables an SSIS package to connect to an Azure Storage account.
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
author: chugugrace
ms.author: chugu
---
# Azure Storage connection manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

The Azure Storage connection manager enables a SQL Server Integration Services (SSIS) package to connect to an Azure Storage account. The connection manager is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md). 
  
In the **Add SSIS Connection Manager** dialog box, select **AzureStorage** > **Add**.  
  
The following properties are available.

- **Service:** Specifies the storage service to connect to.
- **Account name:** Specifies the storage account name.
- **Authentication:** Specifies the authentication method to use. AccessKey, ServicePrincipal, and SharedAccessSignature authentication are supported.
    - **AccessKey:** For this authentication method, specify the **Account key**.
    - **ServicePrincipal:** For this authentication method, specify the **Application ID**, **Application key**, and **Tenant ID** of the service principal.
      For **Test Connection** to work, the service principal should be assigned at least the **Storage Blob Data Reader** role to the storage account.
      For more information, see [Grant access to Azure blob and queue data with RBAC in the Azure portal](https://docs.microsoft.com/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal).
    - **SharedAccessSignature:** For this authentication method, specify at least the **Token** of the shared access signature.
      To test connection, specify additionally the resource scope to test against. It may be **Service**, **Container**, or **Blob**.
      For **Container** and **Blob**, specify container name and blob path, respectively.
      For more information, see [Azure Storage shared access signature overview](https://docs.microsoft.com/azure/storage/common/storage-sas-overview).
- **Environment:** Specifies the cloud environment hosting the storage account.

## Managed identities for Azure resources authentication
When running SSIS packages on [Azure-SSIS integration runtime in Azure Data Factory](https://docs.microsoft.com/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime), you can use the [managed identity](https://docs.microsoft.com/azure/data-factory/connector-azure-sql-database#managed-identity) associated with your data factory for Azure storage authentication. The designated factory can access and copy data from or to your storage account by using this identity.

Refer to [Authenticate access to Azure Storage using Azure Active Directory](https://docs.microsoft.com/azure/storage/common/storage-auth-aad) for Azure Storage authentication in general. To use managed identity authentication for Azure Storage:

1. [Find the data factory managed identity from the Azure portal](https://docs.microsoft.com/azure/data-factory/data-factory-service-identity). Go to your data factory's **Properties**. Copy the **Managed Identity Application ID** (not **Managed Identity Object ID**).

1. Grant the managed identity proper permission in your storage account. For more details about roles, see [Manage access rights to Azure Storage data with RBAC](https://docs.microsoft.com/azure/storage/common/storage-auth-aad-rbac-portal).

    - **As source**, in Access control (IAM), grant at least the **Storage Blob Data Reader** role.
    - **As destination**, in Access control (IAM), grant at least the **Storage Blob Data Contributor** role.

Then configure managed identity authentication for the Azure Storage connection manager. Here are the options to do this:

- **Configure at design time.** In SSIS Designer, double-click the Azure Storage connection manager to open **Azure Storage Connection Manager Editor**. Select **Use managed identity to authenticate on Azure**.
    > [!NOTE]
    >  Currently, this option doesn't take effect (indicating that managed identity authentication doesn't work) when you run SSIS package in SSIS Designer or [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server.
    
- **Configure at runtime.** When you run the package via [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/integration-services/ssis-quickstart-run-ssms) or [Azure Data Factory Execute SSIS Package activity](https://docs.microsoft.com/azure/data-factory/how-to-invoke-ssis-package-ssis-activity), find the Azure Storage connection manager. Update its property `ConnectUsingManagedIdentity` to `True`.
    > [!NOTE]
    >  In Azure-SSIS integration runtime, all other authentication methods (for example, access key and service principal) preconfigured on the Azure Storage connection manager are overridden when managed identity authentication is used for storage operations.

> [!NOTE]
>  To configure managed identity authentication on existing packages, the preferred way is to rebuild your SSIS project with the [latest SSIS Designer](https://docs.microsoft.com/sql/ssdt/download-sql-server-data-tools-ssdt) at least once. Redeploy that SSIS project to your Azure-SSIS integration runtime, so that the new connection manager property `ConnectUsingManagedIdentity` is automatically added to all Azure Storage connection managers in your SSIS project. The alternative way is to directly use a property override with property path **\Package.Connections[{the name of your connection manager}].Properties[ConnectUsingManagedIdentity]** at runtime.

## Secure network traffic to your storage account
Azure Data Factory is now a [trusted Microsoft service](https://docs.microsoft.com/azure/storage/common/storage-network-security#trusted-microsoft-services) to Azure storage. When you use managed identity authentication, it is possible to 
secure your storage account by [limiting access to selected networks](https://docs.microsoft.com/azure/storage/common/storage-network-security#change-the-default-network-access-rule) while still allowing your data factory to access your storage account. Please refer to [Managing exceptions](https://docs.microsoft.com/azure/storage/common/storage-network-security#managing-exceptions) for instructions.

## See also  
 [Integration Services &#40;SSIS&#41; Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)
