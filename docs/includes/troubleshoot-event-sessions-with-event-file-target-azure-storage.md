---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 10/02/2025
ms.service: sql
ms.subservice: xevents
ms.topic: include
---
The following list contains errors that you might encounter when starting an extended event session that uses Azure Storage, with the possible explanations for the error.

- **The operating system returned error 5: 'Access is denied.'**
  - If using managed identity authentication:
    - The managed identity used by the database engine doesn't have the required RBAC role assignment. For more information, see [Grant access using managed identity](#grant-access-using-managed-identity).
    - The storage account [firewall](/azure/storage/common/storage-network-security) is enabled and an exception to allow trusted Azure services to access the storage account is also enabled, but a `Microsoft.Sql/servers` resource instance for the logical server hasn't been added to the list of resource instances that are granted access. For more information, see [Grant access from Azure resource instances](/azure/storage/common/storage-network-security#grant-access-from-azure-resource-instances).
    - If using a [network security perimeter](/azure/private-link/network-security-perimeter-concepts) with enforced mode, the database and the storage account aren't in the same perimeter.
  - If using SAS token authentication:
    - The storage account [firewall](/azure/storage/common/storage-network-security) is enabled. This is not supported for event sessions that use SAS token authentication.
    - The SAS token doesn't have sufficient permissions, or has expired. For more information, see [Grant access using a SAS token](#grant-access-using-a-sas-token).
    - If using a [network security perimeter](/azure/private-link/network-security-perimeter-concepts) with enforced mode, access rules to allow unrestricted outbound communication from the database and unrestricted inbound communication to the storage account aren't in place.
- **The operating system returned error 86: 'The specified network password is not correct.'**
  - There is no database-scoped credential (for Azure SQL Database) or server-scoped credential (for Azure SQL Managed Instance or SQL Server) with the name matching the blob container URL. For more information, see the examples to [Grant access using managed identity](#grant-access-using-managed-identity) or [Grant access using a SAS token](#grant-access-using-a-sas-token).
  - The credential name ends with a slash (`/`). The credential name should end with the container name not including the trailing slash.
- **The operating system returned error 3: 'The system cannot find the path specified.'**
  - The container specified in the blob container URL doesn't exist.
- **The operating system returned error 13: 'The data is invalid.'**
  - There is an [immutability policy](/azure/storage/blobs/immutable-policy-configure-container-scope) on the blob container. Immutable storage isn't supported for event sessions.
  - The storage account has the [hierarchical namespace](/azure/storage/blobs/data-lake-storage-namespace) enabled. Storage accounts with hierarchical namespace enabled aren't supported for event sessions.
