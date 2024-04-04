---
title: "Azure Storage connection manager"
description: The Azure Storage connection manager enables an SSIS package to connect to an Azure Storage account.
author: chugugrace
ms.author: chugu
ms.date: "10/13/2023"
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords:
  - "sql13.dts.designer.afpstorageconn.f1"
  - "sql14.dts.designer.afpstorageconn.f1"
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
      For more information, see [Grant access to Azure blob and queue data with RBAC in the Azure portal](/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal).
    - **SharedAccessSignature:** For this authentication method, specify at least the **Token** of the shared access signature.
      To test connection, specify additionally the resource scope to test against. It may be **Service**, **Container**, or **Blob**.
      For **Container** and **Blob**, specify container name and blob path, respectively.
      For more information, see [Azure Storage shared access signature overview](/azure/storage/common/storage-sas-overview).
- **Environment:** Specifies the cloud environment hosting the storage account.

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Managed identities for Azure resources authentication
When running SSIS packages on [Azure-SSIS integration runtime (IR) in Azure Data Factory (ADF)](/azure/data-factory/concepts-integration-runtime#azure-ssis-integration-runtime), you can use Microsoft Entra authentication with [the managed identity of your ADF](/azure/data-factory/connector-azure-blob-storage#managed-identity) to access Azure Storage. Your Azure-SSIS IR can access and copy data from or to your storage account by using this managed identity.

> [!NOTE]
> When you authenticate with a user-assigned managed identity, the SSIS integration runtime needs to be enabled for authentication with the same identity. For more information, see [Enable Microsoft Entra authentication for Azure-SSIS integration runtime](/azure/data-factory/enable-aad-authentication-azure-ssis-ir).

Refer to the [Authenticate access to Azure Storage using Microsoft Entra ID](/azure/storage/common/storage-auth-aad) article for Azure Storage authentication in general. To use Microsoft Entra authentication with the managed identity of your ADF to access Azure Storage, follow these steps:
 
1. [Find the managed identity for your ADF from Azure portal](/azure/data-factory/data-factory-service-identity). Go to your data factory's **Properties**. Copy the **Managed Identity Application ID** (not the **Managed Identity Object ID**).

1. Grant the managed identity for your ADF the required permissions to access Azure Storage. For more details about roles, see the [Manage access rights to Azure Storage data with RBAC](/azure/storage/common/storage-auth-aad-rbac-portal) article.

   - **As source**, in Access control (IAM), grant at least the **Storage Blob Data Reader** role.
   - **As destination**, in Access control (IAM), grant at least the **Storage Blob Data Contributor** role.

Finally, you can configure Microsoft Entra authentication with the managed identity of your ADF on the Azure Storage connection manager. Here are the options to do this:

- **Configure at design time.** In SSIS Designer, double-click on your Azure Storage connection manager to open the **Azure Storage Connection Manager Editor**. Select the **Use managed identity to authenticate on Azure** option.

  > [!NOTE]
  > The connection manager property `ConnectUsingManagedIdentity` doesn't take effect when you run your package in SSIS Designer or on SQL Server, indicating that Microsoft Entra authentication with the managed identity of your ADF doesn't work.

- **Configure at run time.** When you run your package via [SQL Server Management Studio (SSMS)](../ssis-quickstart-run-ssms.md) or [Execute SSIS Package activity in ADF pipeline](/azure/data-factory/how-to-invoke-ssis-package-ssis-activity), find the Azure Storage connection manager and update its property `ConnectUsingManagedIdentity` to `True`.

  > [!NOTE]
  > On Azure-SSIS IR, all other authentication methods (for example, integrated security and password) preconfigured on your Azure Storage connection manager are overridden when using Microsoft Entra authentication with the managed identity of your ADF.

To configure Microsoft Entra authentication with the managed identity of your ADF on your existing packages, the preferred way is to rebuild your SSIS project with the [latest SSIS Designer](../../ssdt/download-sql-server-data-tools-ssdt.md) at least once. Redeploy your SSIS project to run on Azure-SSIS IR, so that the new connection manager property `ConnectUsingManagedIdentity` is automatically added to all Azure Storage connection managers in your project. The alternative way is to directly use property overrides with the property path *\Package.Connections[{the name of your connection manager}].Properties[ConnectUsingManagedIdentity]* assigned to `True` at run time.

## Secure network traffic to your storage account
When you use Microsoft Entra authentication with the managed identity of your ADF, it's possible to join your Azure-SSIS integration runtime to a virtual network, then secure your storage account by [limiting access to selected networks](/azure/storage/common/storage-network-security#change-the-default-network-access-rule) or private endpoint. Please refer to the [Managing exceptions](/azure/storage/common/storage-network-security#managing-exceptions) article for instructions.

## See also
- [Integration Services (SSIS) Connections](../../integration-services/connection-manager/integration-services-ssis-connections.md)
