---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/15/2025
ms.service: sql
ms.topic: include
ms.custom:
---
> [!NOTE]  
> Secure defaults pertain to the underlying OLEDB provider 19, which enhances security. The option to override the default is less secure than configuring your instance to use a trusted certificate. After overriding the default, you have the option to configure SQL Server to use a certificate, and then use the [sp_changedistributor_property](../relational-databases/system-stored-procedures/sp-changedistributor-property-transact-sql.md) stored procedure to set the `trust_distributor_certificate=no` property back to the secure default.
