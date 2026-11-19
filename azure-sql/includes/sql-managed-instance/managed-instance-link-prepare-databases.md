---
author: danimir
ms.author: danil
ms.date: 11/18/2026
ms.service: azure-sql-managed-instance
ms.topic: include
ai-usage: ai-assisted
---

Before you configure the link, complete the database preparation steps for your initial primary. When SQL Server is your initial primary, databases replicate from SQL Server to Azure SQL Managed Instance. When SQL Managed Instance is your initial primary, databases replicate from SQL Managed Instance to SQL Server. Follow the instructions for your replication direction in the following sections.

### When SQL Server is your initial primary

- Configure each database to use the [full recovery model](../../managed-instance/managed-instance-link-preparation.md#configure-databases-to-use-full-recovery-model).
- Create at least one [full backup](../../managed-instance/managed-instance-link-preparation.md#create-full-database-backup) of each database.
- If the database uses TDE, [migrate the TDE protector certificate](../../managed-instance/tde-certificate-migrate.md) to the managed instance before creating the link.
- For databases that use In-Memory OLTP, choose the Business Critical service tier, or [remove unsupported In-Memory OLTP objects](../../managed-instance/managed-instance-link-preparation.md#prepare-databases-that-use-in-memory-oltp) before migrating to General Purpose.
- If you use accelerated database recovery (ADR), ensure that the [persistent version store (PVS) is set to the `PRIMARY`](../../managed-instance/managed-instance-link-preparation.md#prepare-databases-that-use-accelerated-database-recovery-adr) filegroup for replicated databases.
- If Service Broker is disabled on the database you're replicating and you plan to use Service Broker after migration to SQL Managed Instance, [enable Service Broker on the source database](../../managed-instance/managed-instance-link-preparation.md#prepare-databases-with-service-broker-disabled) before migrating.

### When SQL Managed Instance is your initial primary

- If the database uses TDE, use a customer-managed key and [configure the destination SQL Server instance to access the same key](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault). Databases on Azure SQL Managed Instance encrypted with service-managed TDE keys can't be replicated from SQL Managed Instance to SQL Server.

For requirements and instructions, see [Prepare databases](../../managed-instance/managed-instance-link-preparation.md#prepare-databases) in the environment preparation article.
