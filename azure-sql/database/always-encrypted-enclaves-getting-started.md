---
title: "Getting Started Using Always Encrypted with Secure Enclaves"
description: A collection of tutorials on how to create a basic environment for Always Encrypted with secure enclaves.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, mathoma
ms.date: 09/02/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: tutorial
---
# Getting started using Always Encrypted with secure enclaves

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

These tutorials teach you how to get started with [Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/always-encrypted-enclaves). They'll show you:

> [!div class="checklist"]
>
> - How to create a basic environment for testing and evaluating Always Encrypted with secure enclaves.
> - How to encrypt data in-place and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS).

## Always Encrypted with secure enclaves in SQL Server

- [Tutorial: Getting started using Always Encrypted with secure enclaves in SQL Server](/sql/relational-databases/security/tutorial-getting-started-with-always-encrypted-enclaves)
- [Tutorial: Getting started using Always Encrypted with secure enclaves in SQL Server with attestation using HGS](/sql/relational-databases/security/tutorial-getting-started-with-always-encrypted-enclaves-hgs)

## Always Encrypted with secure enclaves in Azure SQL Database

> [!IMPORTANT]
> Always Encrypted with Intel Software Guard Extensions (Intel SGX) enclaves reaches the end of support on October 31, 2027. Migrate affected databases before this date. After October 31, 2027, Azure automatically moves any database that remains on the DC-series compute tier to a supported standard-series (non-DC) compute tier and enables virtualization-based security (VBS) enclaves. For migration options, see [Always Encrypted with Intel SGX enclaves migration guide](/sql/relational-databases/security/encryption/always-encrypted-enclaves-migration).

- [Tutorial: Getting started using Always Encrypted with Intel SGX enclaves in Azure SQL Database](always-encrypted-enclaves-getting-started-sgx.md)
- [Tutorial: Getting started using Always Encrypted with VBS enclaves in Azure SQL Database](always-encrypted-enclaves-getting-started-vbs.md)

<a id="next-steps"></a>

## Tutorials

- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](/sql/connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps)
- [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](/sql/relational-databases/security/tutorial-always-encrypted-enclaves-develop-net-framework-apps)
- [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](/sql/relational-databases/security/tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption)

## Related content

- [Configure and use Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/configure-always-encrypted-enclaves)