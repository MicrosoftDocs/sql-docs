---
title: "Plan for secure enclaves in Azure SQL Database"
description: Plan the deployment of Always Encrypted with secure enclaves in Azure SQL Database.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto
ms.date: 02/01/2023
ms.service: sql-database
ms.subservice: security
ms.topic: conceptual
---

# Plan for secure enclaves in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

In [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], Always Encrypted with secure enclaves can use either [Intel Software Guard Extensions (Intel SGX) enclaves](https://www.intel.com/content/www/us/en/architecture-and-technology/software-guard-extensions.html) or [Virtualization-based Security (VBS) enclaves](https://www.microsoft.com/security/blog/2018/06/05/virtualization-based-security-vbs-memory-enclaves-data-protection-through-isolation/).

## Intel SGX enclaves

Intel SGX is a hardware-based trusted execution environment technology. It is available in databases that use the [vCore purchasing model](service-tiers-sql-database-vcore.md) and the [DC-series](service-tiers-sql-database-vcore.md?#dc-series) hardware configuration. To make an Intel SGX enclave available for your database, you need to either select the DC-series hardware configuration when you create the database, or you can update your existing database to use the DC-series hardware.

> [!NOTE]
> Intel SGX is not available in hardware other than DC-series. For example, Intel SGX is not available in [standard-series (Gen5)](service-tiers-sql-database-vcore.md#standard-series-gen5) hardware configuration, and it is not available for databases using the [DTU model](service-tiers-dtu.md).

Combined with attestation provided by [Microsoft Azure Attestation](/sql/relational-databases/security/encryption/always-encrypted-enclaves#secure-enclave-attestation), Intel SGX enclaves offer a stronger protection against attacks from actors with the OS-level admin access, compared to VBS enclaves. However, before you configure the DC-series hardware for your database, make sure you are aware of its performance properties and limitations:

- Unlike other hardware configurations of the v-core purchasing model, DC-series uses physical processor cores, not logical cores. The [resource limits](service-tiers-vcore.md#resource-limits) of DC-series databases differ from the resource limits of the standard-series (Gen 5) hardware configuration.
- The maximum number of processor cores you can set for a DC-series database is 8.
- DC-series doesn't work with [serverless](serverless-tier-overview.md).

Also, check the current regional availability of DC-series and make sure it is available in your preferred regions. For details, see [DC-series](service-tiers-sql-database-vcore.md#dc-series).

Considering the above, SGX enclaves are recommended for workloads that require the strongest data confidentiality protection and can adhere to the current limitations of DC-series.

## VBS enclaves

VBS enclaves (also known as Virtual Secure Mode, or VSM enclaves) is a software-based technology that relies on Windows hypervisor and doesn't require any special hardware. Therefore, VBS enclaves are available in all [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] offerings, providing you with the flexibility to use Always Encrypted with secure enclaves with a compute size, service tier, purchasing model, hardware configuration and region that best meets your workload requirements. 

VBS enclaves are the recommended solution for customers who seek protection for data in use from high-privileged users in the customer’s organization, including DBAs. Without having the cryptographic keys protecting the data, a DBA will not be able to access the data in plaintext.

VBS enclaves can also help prevent some OS-level threats, such as exfiltrating sensitive data from memory dumps – the plaintext data processed in an enclave does not show up in memory dumps, providing the code inside the enclave and its properties have not been maliciously altered. However, VBS enclaves in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] cannot address more sophisticated attacks, such as replacing the enclave binary with malicious code, due to the current lack of enclave attestation. It is important to note that Microsoft has implemented multiple layers of security controls to detect and prevent such attacks in the Azure cloud, including just-in-time access, multi-factor authentication, and security monitoring. Nevertheless, customers who require strong security isolation may prefer Intel SGX enclaves with the DC-series hardware configuration over VBS enclaves.

> [!IMPORTANT]
> VBS enclaves in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] are currently in preview. The [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/) include additional legal terms that apply to Azure features that are in beta, in preview, or otherwise not yet released into general availability.

> [!NOTE]
> VBS enclaves are currently available in all [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)] regions **except**: Australia Central, Australia Central 2, Jio India Central, Jio India West, Korea Central, Korea South, UAE Central.

## Plan for enclave attestation in Azure SQL Database

Configuring attestation using [Microsoft Azure Attestation](/azure/attestation/overview) is required when using Intel SGX enclaves in DC-series databases.

> [!NOTE]
> Attestation is currently not supported for VBS enclaves. The remainder of this section applies only to Intel SGX enclaves in DC-series databases.

To use Microsoft Azure Attestation for attesting Intel SGX enclaves in [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], you need to create an [attestation provider](/azure/attestation/basic-concepts#attestation-provider) and configure it with the Microsoft-provided attestation policy. See [Configure attestation for Always Encrypted using Azure Attestation](always-encrypted-enclaves-configure-attestation.md)

### Roles and responsibilities when configuring Intel SGX enclaves and attestation

Configuring your environment to support Intel SGX enclaves and attestation for Always Encrypted in Azure SQL Database involves setting up components of different types: Microsoft Azure Attestation, Azure SQL Database, and applications that trigger enclave attestation. Configuring components of each type is performed by users assuming one of the below distinct roles:

- Attestation administrator - creates an attestation provider in Microsoft Azure Attestation, authors the attestation policy, grants Azure SQL logical server access to the attestation provider, and shares the attestation URL that points to the policy to application administrators.
- Azure SQL Database administrator - enables SGX enclaves in databases by selecting the DC-series hardware, and provides the attestation administrator with the identity of the Azure SQL logical server that needs to access the attestation provider.
- Application administrator - configures applications with the attestation URL obtained from the attestation administrator.

In production environments (handling real sensitive data), it is important your organization adheres to role separation when configuring attestation, where each distinct role is assumed by different people. In particular, if the goal of deploying Always Encrypted in your organization is to reduce the attack surface area by ensuring Azure SQL Database administrators cannot access sensitive data, Azure SQL Database administrators should not control attestation policies.

## Next steps

- [Enable Intel SGX for your Azure SQL database](always-encrypted-enclaves-enable-sgx.md)

## See also

- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](always-encrypted-enclaves-getting-started.md)
