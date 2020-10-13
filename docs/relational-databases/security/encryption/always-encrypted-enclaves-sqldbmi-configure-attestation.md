---
title: "Configure Azure Attestation for your Azure SQL database server"
ms.custom: ""
ms.date: 11/18/2020
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

[Microsoft Azure Attestation](https://docs.microsoft.com/azure/attestation/overview) (preview) is a solution for attesting Trusted Execution Environments (TEEs), including Intel SGX enclaves. 

To use Azure Attestation for attesting Intel SGX enclaves in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], you need to:

1. Create an [attestation provider](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-provider) and configure it with the recommended attestation policy. 

2. Grant your Azure SQL database server access to your attestation provider.

## Create and configure an attestation provider

An [attestation provider](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-provider) is a resource in Azure Attestation that evaluates [attestation requests](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-request) against [attestation policies](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-request) and issues [attestation tokens](https://docs.microsoft.com/azure/attestation/basic-concepts#attestation-token). 

Attestation policies are specified using the [claim rule grammer](https://docs.microsoft.com/azure/attestation/claim-rule-grammar).

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

For instructions for how create an attestation provider and configure with an attestation policy using:

- Azure Portal - see ...
- Azure Powershell - see [Quickstart: Set up Azure Attestation with Azure PowerShell](https://docs.microsoft.com/azure/attestation/quickstart-powershell)
- ARM templates - see [Quickstart: Create an Azure Attestation provider with an ARM template](https://docs.microsoft.com/azure/attestation/quickstart-template?tabs=CLI)

## Next Steps
 [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)
