---
title: "Plan for secure enclaves in Azure SQL Database"
description: Plan the deployment of Always Encrypted with secure enclaves in Azure SQL Database.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, mathoma
ms.date: 09/26/2023
ms.service: sql-database
ms.subservice: security
ms.custom: ignite-2023
ms.topic: conceptual
---

# Plan for secure enclaves in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

In Azure SQL Database, Always Encrypted with secure enclaves can use either Intel Software Guard Extensions (Intel SGX) enclaves or Virtualization-based Security (VBS) enclaves.

## Intel SGX enclaves

[Intel SGX](https://www.intel.com/content/www/us/en/architecture-and-technology/software-guard-extensions.html) is a hardware-based trusted execution environment technology. It's available in databases and elastic pools that use the [vCore purchasing model](service-tiers-sql-database-vcore.md) and the [DC-series](service-tiers-sql-database-vcore.md?#dc-series) hardware configuration. To make an Intel SGX enclave available for your database or elastic pool, you need to either select the DC-series hardware configuration when you create the database or elastic pool, or you can update your existing database or elastic pool to use the DC-series hardware.

> [!NOTE]
> Intel SGX is not available in hardware other than DC-series. For example, Intel SGX is not available in [standard-series (Gen5)](service-tiers-sql-database-vcore.md#standard-series-gen5) hardware configuration, and it is not available for databases using the [DTU model](service-tiers-dtu.md).

Intel SGX enclaves combined with attestation provided by [Microsoft Azure Attestation](/sql/relational-databases/security/encryption/always-encrypted-enclaves#secure-enclave-attestation) offer a stronger protection against attacks from actors with the OS-level admin access, compared to VBS enclaves. However, before you configure the DC-series hardware for your database, make sure you're aware of its performance properties and limitations:

- Unlike other hardware configurations of the vCore purchasing model, DC-series uses physical processor cores, not logical cores. The [resource limits](service-tiers-vcore.md#resource-limits) of DC-series databases differ from the resource limits of the standard-series (Gen 5) hardware configuration.
- The maximum number of processor cores you can set for a DC-series database is 40.
- DC-series doesn't work with [serverless](serverless-tier-overview.md).

Also, check the current regional availability of DC-series and make sure it's available in your preferred regions. For details, see [DC-series](service-tiers-sql-database-vcore.md#dc-series).

SGX enclaves are recommended for workloads that require the strongest data confidentiality protection and can adhere to the current limitations of DC-series.

## VBS enclaves

[VBS enclaves](https://www.microsoft.com/security/blog/2018/06/05/virtualization-based-security-vbs-memory-enclaves-data-protection-through-isolation/) (also known as Virtual Secure Mode, or VSM enclaves) is a software-based technology that relies on Windows hypervisor and doesn't require any special hardware. Therefore, VBS enclaves are available in all Azure SQL Database offerings, including Azure SQL Elastic Pools, providing you with the flexibility to use Always Encrypted with secure enclaves with a compute size, service tier, purchasing model, hardware configuration and region that best meets your workload requirements.

> [!NOTE]
> VBS enclaves are available in all Azure SQL Database regions **except**: Jio India Central.

VBS enclaves are the recommended solution for customers who seek protection for data in use from high-privileged users in the customer's organization, including Database Administrators (DBAs). Without having the cryptographic keys protecting the data, a DBA won't be able to access the data in plaintext.

VBS enclaves can also help prevent some OS-level threats, such as exfiltrating sensitive data from memory dumps within a VM hosting your database. The plaintext data processed in an enclave doesn't show up in memory dumps, providing the code inside the enclave and its properties haven't been maliciously altered. However, VBS enclaves in Azure SQL Database can't address more sophisticated attacks, such as replacing the enclave binary with malicious code, due to the current lack of enclave attestation. Also, regardless of attestation, VBS enclaves don't provide any protection from attacks using privileged system accounts originating from the host. It's important to note that Microsoft has implemented multiple layers of security controls to detect and prevent such attacks in the Azure cloud, including just-in-time access, multifactor authentication, and security monitoring. Nevertheless, customers who require strong security isolation might prefer Intel SGX enclaves with the DC-series hardware configuration over VBS enclaves.

## Plan for enclave attestation in Azure SQL Database

Configuring attestation using [Microsoft Azure Attestation](/azure/attestation/overview) is required when using Intel SGX enclaves in DC-series databases.

> [!IMPORTANT]
> Attestation is currently not supported for VBS enclaves. The remainder of this section applies only to Intel SGX enclaves in DC-series databases.

To use Microsoft Azure Attestation for attesting Intel SGX enclaves in Azure SQL Database, you need to create an [attestation provider](/azure/attestation/basic-concepts#attestation-provider) and configure it with the Microsoft-provided attestation policy. See [Configure attestation for Always Encrypted using Azure Attestation](always-encrypted-enclaves-configure-attestation.md)

### Roles and responsibilities when configuring Intel SGX enclaves and attestation

Configuring your environment to support Intel SGX enclaves and attestation for Always Encrypted in Azure SQL Database involves setting different components: an attestation provider, a database, and applications that trigger enclave attestation. Configuring components of each type is performed by users assuming one of the below distinct roles:

- Attestation administrator - creates an attestation provider in Microsoft Azure Attestation, authors the attestation policy, grants Azure SQL logical server access to the attestation provider, and shares the attestation URL that points to the policy to application administrators.
- Database administrator (DBA) - enables SGX enclaves in databases by selecting the DC-series hardware, and provides the attestation administrator with the identity of the Azure SQL logical server that needs to access the attestation provider.
- Application administrator - configures applications with the attestation URL obtained from the attestation administrator.

In production environments (handling real sensitive data), it's important your organization adheres to role separation when configuring attestation, where each distinct role is assumed by different people. In particular, if the goal of deploying Always Encrypted in your organization is to reduce the attack surface area by ensuring database administrators can't access sensitive data, database administrators shouldn't control attestation policies.

## Next steps

- [Enable Always Encrypted with secure enclaves for your Azure SQL Database](always-encrypted-enclaves-enable.md)

## See also

- [Getting started using Always Encrypted with secure enclaves](always-encrypted-enclaves-getting-started.md)
