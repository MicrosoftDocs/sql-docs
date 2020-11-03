---
title: "Configure Azure Attestation for your Azure SQL database server"
description: "Configure Azure Attestation for Always Encrypted with secure enclaves in Azure SQL Database."
ms.custom: ""
ms.date: 12/01/2020
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: "vanto"
ms.technology: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Configure Azure Attestation for your Azure SQL database server

[!INCLUDE [asdb](../../../includes/applies-to-version/asdb.md)]

[Microsoft Azure Attestation](https://docs.microsoft.com/azure/attestation/overview) is a solution for attesting Trusted Execution Environments (TEEs), including Intel SGX enclaves. 

To use Azure Attestation for attesting Intel SGX enclaves used for [Always Encrypted with secure enclaves](always-encrypted-enclaves.md) in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], you need to:

1. Create an [attestation provider](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-provider) and configure it with the recommended attestation policy.

2. Grant your Azure SQL database server access to your attestation provider.

## Requirements

The Azure SQL database server and the attestation provider must belong to the same Azure Active Directory tenant. Cross-tenant interactions are not supported. 

The Azure SQL database server must have an Azure AD identity assigned to it. The administrator of the attestation provider will use the identity to grant the server access to the attestation provider. For instructions on how to create a server with an identity or assign an identity to an existing server using:

- Powershell - see [Assign an Azure AD identity to your server using PowerShell](https://docs.microsoft.com/azure/azure-sql/database/transparent-data-encryption-byok-configure?tabs=azure-powershell#assign-an-azure-active-directory-azure-ad-identity-to-your-server).
- Azure CLI - see [Assign an Azure AD identity to your server using Azure CLI](https://docs.microsoft.com/azure/azure-sql/database/transparent-data-encryption-byok-configure?tabs=azure-cli#assign-an-azure-ad-identity-to-your-server).

## Create and configure an attestation provider

An [attestation provider](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-provider) is a resource in Azure Attestation that evaluates [attestation requests](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-request) against [attestation policies](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-request) and issues [attestation tokens](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-token). 

Attestation policies are specified using the [claim rule grammar](https://docs.microsoft.com/azure/attestation/claim-rule-grammar).

Microsoft recommends the following policy for attesting Intel SGX enclaves used for Always Encrypted in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)]:

```sql
version= 1.0;
authorizationrules 
{
       [ type=="$is-debuggable", value==false ]
        && [ type=="$product-id", value==4639 ]
        && [ type=="$svn", value>= 0 ]
        && [ type=="$sgx-mrsigner", value=="e31c9e505f37a58de09335075fc8591254313eb20bb1a27e5443cc450b6e33e5"] 
    => permit();
};
```

The above policy verifies:
- The enclave inside the [!INCLUDE[ssde-md](../../../includes/ssde-md.md)] does not support debugging (which would reduce the level of protection the enclave provides).
- The product id of the library inside the enclave is the product id assigned to Always Encrypted with secure enclaves (4639).
- The version id (svn) of the library is greater than 0.
- The library in the enclave has been signed using the Microsoft signing key (the value of the sgx-mrsigner claim is the hash of the signing key).

For instructions for how to create an attestation provider and configure with an attestation policy using:

- Azure Portal - see ...
- Azure Powershell - see [Quickstart: Set up Azure Attestation with Azure PowerShell](https://docs.microsoft.com/azure/attestation/quickstart-powershell)
- ARM templates - see [Quickstart: Create an Azure Attestation provider with an ARM template](https://docs.microsoft.com/azure/attestation/quickstart-template?tabs=CLI)

## Determine the attestation URL for your attestation policy

As an administrator of the attestation policy, you need to determine the attestation URL referencing the policy and share it with administrators or users of database client applications in your organization. Client application admins or users will need to configure their apps with the attestation URL, so that they can run statements that use secure enclaves.

### Using PowerShell

```powershell
$attestationProvider = Get-AzAttestation -Name $attestationProviderName -ResourceGroupName $attestationResourceGroupName 
$attestationUrl = $attestationProvider.AttestUri + “/attest/SgxEnclave?api-version=2018-09-01-preview”
Write-Host "Your attestation URL is: " $attestationUrl 
```

### Using Azure Portal

1. In the Overview pane for you attestation provider copy the value of the Attest URI property to clipboard. An Attest URI should look like this: `https://MyAttestationProvider.us.attest.azure.net`.

2. Paste the Attest URI property values in a text editor. Append the following to the Attest URI: /attest/SgxEnclave?api-version=2018-09-01-preview. 

The resulting attestation URL should look like this: `https://MyAttestationProvider.us.attest.azure.net/attest/SgxEnclave?api-version=2018-09-01-preview`

## Grant your Azure SQL database server access to your attestation provider

During the attestation workflow, the Azure SQL database server, containing your database, calls the attestation provider to submit an attestation request. For the Azure SQL database server to be able to submit attestation requests, the server must have a permission for the Microsoft.Attestation/attestationProviders/attestation/read action on the attestation provider. The recommended way to grant the permission is for the administrator of the attestation provider to assign the Azure AD identity of the server to the Attestation Reader role for the attestation provider or its containing resource group.

### Using Azure Portal

To assign the identity of an Azure SQL database to the Attestation Reader role for an attestation provider, follow the general instructions in [Add or remove Azure role assignments using the Azure portal](https://docs.microsoft.com/azure/role-based-access-control/role-assignments-portal). When you are in the **Add role assignment** pane:

- In the **Role** drop-down Select the Attestation Reader role .
- In the **Select** field, enter the name of your Azure SQL database server, to search for it.

See the below screenshot for an example.

![attestation reader role assignment](./media/always-encrypted-enclaves/sqldbmi-attestation-provider-role-assigment.png)

> [!NOTE]
> For a server to show up in the **Add role assignment** pane, the server must have an Azure AD identity assigned - see [Requirements](#requirements).

### Using PowerShell

1. Find your Azure SQL database server.

```powershell
$serverResourceGroupName = "<server resource group name>"
$serverName = "<server name>" 
$server = Get-AzSqlServer -ServerName $serverName -ResourceGroupName
```

2. Assign the server to the Attestation Reader role for the resource group containing your attestation provider.

```powershell
$attestationResourceGroupName = "<attestation provider resource group name>"
New-AzRoleAssignment -ObjectId $server.Identity.PrincipalId -RoleDefinitionName "Attestation Reader" -ResourceGroupName $attestationResourceGroupName
```

For more information see [Add or remove Azure role assignments using Azure PowerShell](https://docs.microsoft.com/azure/role-based-access-control/role-assignments-powershell#add-a-role-assignment).

## Next Steps
 [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)

## See also

- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](../tutorial-always-encrypted-enclaves-getting-started-sqldb.md)