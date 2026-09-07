---
title: "End of Support Options"
description: Learn about the different options available for SQL Server products that reach end of support, including SQL Server 2014.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mathoma, wiassaf
ms.date: 08/31/2026
ms.service: sql
ms.subservice: install
ms.topic: concept-article
monikerRange: ">=sql-server-2017"
---
# SQL Server end of support options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article explains your options for addressing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] products that reach end of support.

## Understand the SQL Server lifecycle

Each version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] comes with a minimum of 10 years of support, which includes five years of mainstream support and five years of extended support:

- **Mainstream support** includes functional, performance, scalability, and security updates.
- **Extended support** includes only security updates.

**End of support** (also sometimes known as end of life) means that a product reached the end of its support lifecycle. Microsoft no longer provides servicing and support for the product. For more information about the Microsoft Lifecycle, see [Microsoft Lifecycle Policy](/lifecycle).

## End of support options

When your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance reaches the end of support, choose from the following options:

- Upgrade to a current version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- [Migrate](/azure/azure-sql/migration-guides/) your SQL Server workload to [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview), [Azure SQL Database](/azure/azure-sql/database/sql-database-paas-overview), or [Azure Virtual Machine](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview). When you migrate to an Azure VM, you can choose to:
   - Upgrade your SQL Server instance to a supported version during the migration.
   - Migrate your SQL Server instance as-is. Subscribe for extended security updates (ESUs) for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. Or, with [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], migrate to an Azure VM for free ESUs until the end of the ESU period.
- Subscribe to [Extended Security Updates (ESUs)](https://www.microsoft.com/windows-server/extended-security-updates) and stay on your current environment until you're able to upgrade or migrate to a supported version or platform.

For more information, guidance, and tools to plan and automate your upgrade or migration, see:

- [SQL Server 2016 end of support](/lifecycle/products/sql-server-2016)
- [SQL Server 2014 end of support](/lifecycle/products/sql-server-2014)
- [What are ESUs for SQL Server?](sql-server-extended-security-updates.md)
- [Frequently asked SQL Server ESU questions](extended-security-updates-frequently-asked-questions.yml)
- [ESUs enabled by Azure Arc](../azure-arc/extended-security-updates.md)

The following table summarizes options for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance that is out of extended support: 

| End of support options | Description |
| --- | --- |
|   | **Modernize** |
| Upgrade on-premises | **Upgrade** your existing environment to the latest versions of SQL Server and Windows Server. |
| Move to PaaS with Azure SQL Managed Instance | **Lift and shift** to SQL Managed Instance for fully managed services that never reach end of support.|
| Upgrade to IaaS with SQL Server on Azure VMs | **Lift and shift** your workload by moving it to the latest version of SQL Server on an Azure virtual machine. |
| Move to PaaS with Azure SQL Database | **Modernize** your workload by moving it to the Hyperscale service tier of Azure SQL Database, a fully managed database service that never reaches end of support. |
|  |**Migrate to Azure as-is**|
|Move to IaaS with SQL Server on Azure VM | **Move "as-is"**, and subscribe to ESUs for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. Or for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], get ESUs for free until the end of the ESU period. |
|Move to IaaS with Azure VMware Solution | **Move "as-is"**, and subscribe to ESUs for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. Or for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], get ESUs for free until the end of the ESU period.  |
| |**Stay on existing version**|
| Subscribe to ESUs |**Keep** server and application as-is for up to three years by subscribing to ESUs |

This article describes the benefits and considerations for each approach, with more resources to help guide your decision-making process.

> [!TIP]
> Use the **Azure SQL Decision Tree** in the [Azure SQL hub](https://aka.ms/azuresqlhub) to help guide your decision.

## Upgrade SQL Server

When your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance reaches the end of support, consider upgrading to a newer supported version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option provides environmental consistency, lets you use the latest features, and aligns you with the new version's support lifecycle.

### Benefits

- **Latest technology**: New [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions bring innovations in performance, scalability, high availability, and security.

- **Control**: You have the most control over features and scalability, because you manage both hardware and software.

- **Familiar environment**: If you're upgrading from an older version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this environment feels the most familiar.

- **Wide applicability**: Suitable for all kinds of database applications, including OLTP systems and data warehousing.

- **Low risk for database applications**: When the database compatibility matches the legacy system, existing database applications stay protected from functional and performance changes. An application only needs full recertification when it requires features available under a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

- **Comprehensive technical support**: You can rely on technical support for a wide range of topics to troubleshoot your operational issues and to mitigate business risks.

### Considerations

- **Cost**: This approach requires up-front investment and involves higher ongoing management cost compared to other options. You must buy, maintain, and manage your own hardware and software.
- **Downtime**: Depending on your upgrade strategy, downtime might occur. There's also an inherent risk of running into problems during an in-place upgrade process.
- **Complexity**: If you're on an unsupported version of Windows Server, you also need to upgrade the OS. Newer versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might not support those Windows versions. There's added risk during the OS upgrade process, so doing a side-by-side migration might be the more prudent, yet more costly, approach. In-place OS upgrades aren't supported on failover cluster instances for Windows Server 2008 or [!INCLUDE [winserver2008r2-md](../../includes/winserver2008r2-md.md)].

  > [!NOTE]  
  > Cluster OS rolling upgrades are available starting with Windows Server 2016.

### Resources

- [Installation media](https://www.microsoft.com/evalcenter/evaluate-sql-server-2025)
- [Upgrade SQL Server Using the Installation Wizard (Setup)](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)

The following table provides links to resources for upgrading from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to a supported version:

| SQL Server version | What's new | Hardware requirements | Supported upgrades |
| --- | --- | --- | --- |
| SQL Server 2025 | [What's new](../what-s-new-in-sql-server-2025.md) | [Requirements](../install/hardware-and-software-requirements-for-installing-sql-server-2025.md) | [Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades-2025.md) |
| SQL Server 2022 | [What's new](../what-s-new-in-sql-server-2022.md) | [Requirements](../install/hardware-and-software-requirements-for-installing-sql-server-2022.md) | [Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades-2022.md) |
| SQL Server 2019 | [What's new](../what-s-new-in-sql-server-2019.md) | [Requirements](../install/hardware-and-software-requirements-for-installing-sql-server-2019.md) | [Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades-2019.md) |
| SQL Server 2017 | [What's new](../what-s-new-in-sql-server-2017.md) | [Requirements](../install/hardware-and-software-requirements-for-installing-sql-server-2017.md) | [Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades-2017.md) |

Tools:

- The [SQL Server migration component in SQL Server Management Studio](/ssms/migrate-sql-server-component) can help detect compatibility problems that affect database functionality in your new version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- [Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md) can help tune workloads that might experience adverse effects when upgrading the database compatibility.

## Lift and shift to Azure SQL Managed Instance

If you want to offload maintenance and reduce costs, consider moving to [Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance). A SQL managed instance closely resembles an on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, without the need to worry about hardware failure or patching. SQL Managed Instance is a collection of system and user databases with a shared set of resources that is lift-and-shift ready and can be used for most migrations to the cloud. This option is best for new applications or existing on-premises applications that want to use the latest stable [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] features and that are migrated to the cloud with minimal changes.

### Benefits

- **Cost**: You save costs by offloading software and hardware maintenance.
- **Lift and shift**: You can lift and shift your entire [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on-premises instance to a SQL managed instance, including all databases with minimal to no database change.
- **Features**: It closely matches the features of an on-premises instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], such as cross-database queries, transactional replication publishing and distribution, SQL job scheduling, and CLR support.
- **Scalability**: Within a SQL managed instance, all databases share resources, and you can scale up and down at any time without downtime.
- **Automation**: Patching and backups happen automatically, saving you valuable maintenance time.
- **Availability**: The cost of the service includes both storage and high availability, with 99.99% availability guaranteed.
- **Intelligent Insights**: Gain insight about the performance of your databases with built-in intelligence analytics.
- **Versionless**: Azure SQL Managed Instance with the [Always-up-to-date update policy](/azure/azure-sql/managed-instance/update-policy#always-up-to-date-update-policy) is versionless, meaning you're always on the latest version, and never have to worry about upgrading or downtime. Plus, you're always on the latest and greatest, with the latest stable features being released to the cloud first.
- **Cross-environment compatibility**: If you configure your SQL managed instance with a corresponding [update policy](/azure/azure-sql/managed-instance/update-policy) you can take advantage of bidirectional failover and cross-environment disaster recovery with on-premises [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] or [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instances.
- **Low risk for database applications**: When the database compatibility is at the same level as the on-premises databases, existing database applications are protected from functional and performance changes that can have detrimental effects. An application only needs to be fully recertified when it requires features available in a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

### Considerations

- **Cost**: SQL managed instance can be more costly than SQL Server on Azure VMs.
- **Transact-SQL differences**: While the feature set of Azure SQL Managed Instance is comprehensive, there are some [compatibility](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server) differences that can affect your existing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] operations. 
- **Deployment**: Deploying a SQL managed instance can take more time than deploying an Azure virtual machine.
- **Feature limitation**: Although a SQL managed instance shares most features with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], some features are unsupported.
- **Size limitation**: The combined storage size for all databases within a SQL managed instance is limited to 32 TB, as opposed to 524 PB for on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Networking**: The networking requirements for a SQL managed instance add an extra layer of complexity to your infrastructure and require either an Azure ExpressRoute or VPN Gateway.
- **Maintenance time**: You have no guarantee for the exact maintenance time, though it's nearly transparent.

### Resources

- [SQL Managed Instance overview](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview)
- [Choosing an Azure SQL option](/azure/azure-sql/azure-sql-iaas-vs-paas-what-is-overview)
- [Migrate SQL Server to Azure SQL Managed Instance](/data-migration/sql-server/managed-instance/guide)
- [Broader migration process](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)

Tools:

- [SQL Server migration in Azure Arc](../azure-arc/migrate-to-azure-sql-managed-instance.md)
- [SQL Server migration component in SQL Server Management Studio](/ssms/migrate-sql-server-component)
- [Azure Database Migration Service (DMS)](/azure/dms/dms-overview)

## Lift and shift to SQL Server on Azure VMs

Another option is to migrate your workload to an [Azure Virtual Machine running SQL Server](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview). You can migrate your system as-is and keep your end-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or you can upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option is best for migrations and applications requiring OS-level access. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] virtual machines are lift-and-shift ready for existing applications that require fast migration to the cloud with minimal or no changes.

### Benefits

- **Cost-saving**: You save the cost of hardware and server software, only paying for hourly usage.
- **Lift-and-shift**: You can lift and shift your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and application infrastructure into the cloud with minimal or no changes.
- **Hosted environment**: You get the benefits of a hosted environment, such as offloading hardware and software maintenance.
- **Automation**: If you're on [!INCLUDE [winserver2008r2-md](../../includes/winserver2008r2-md.md)] and later versions, you get the benefit of automated patching and automated backups.
- **OS Control**: You have control over the operating system environment, but with the familiar feature set of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Rapid deployment**: You can quickly deploy from a library of virtual machine images.
- **License mobility**: You can bring your license, allowing you to decrease operating cost.
- **High availability**: You benefit from the built-in virtual machine availability by the Azure infrastructure with up to 99.99% availability, and take advantage of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] high availability options such as failover cluster instances and Always On availability groups.
- **Low risk for database applications**: When the database compatibility is at the same level as the legacy databases, existing database applications are protected from functional and performance changes that can have detrimental effects. An application only needs to be fully recertified when it requires features available under a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).
- **Free [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] Extended Security Updates**: If you choose to keep your [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] as-is, you can get free ESUs for three years past the end of support date by migrating to SQL Server on Azure VMs, even without having Software Assurance. For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] ESU, you can subscribe to ESUs.

### Considerations

- **Manageability**: You still need to manage both [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and operating system software.
- **Networking**: You need to configure the virtual machine to integrate with your networking and Active Directory infrastructure, which adds complexity.
- **Shared storage FCI**: Azure virtual machines only support failover cluster instances that use Storage Spaces Direct or Premium File Shares. They don't support a failover cluster instance that uses shared storage. As such, Azure virtual machines only support failover cluster instances when using Windows Server 2012 and later versions.
- **Scalability downtime**: You have downtime while changing the CPU and storage resources.
- **Size limitation**: Although the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance can support as many databases as needed, the cumulative total of all databases for a single instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is 256 TB, as opposed to 524 PB for an on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### Resources

- [SQL Server on Azure VM overview](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview)
- [Choosing an Azure SQL option](/azure/sql-database/sql-database-paas-vs-sql-server-iaas)
- [Migrate SQL Server to an Azure VM](/azure/azure-sql/virtual-machines/windows/migrate-to-vm-from-sql-server)
- [Extend support for SQL Server with Azure](/azure/azure-sql/virtual-machines/windows/extended-security-updates-sql-vm)
- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Detailed ESU frequently asked questions](https://www.microsoft.com/windows-server/extended-security-updates)
- [Automated Patching for SQL Server on Azure virtual machines](/azure/azure-sql/virtual-machines/windows/automated-patching)
- [Automated Backup for Azure virtual machines (Resource Manager)](/azure/azure-sql/virtual-machines/windows/automated-backup)
- [Business continuity and HADR for SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/business-continuity-high-availability-disaster-recovery-hadr-overview)
- [SQL virtual machine frequently asked questions](/azure/azure-sql/virtual-machines/windows/frequently-asked-questions-faq)

## Modernize with Azure SQL Database Hyperscale

Consider migrating your workload to the [Hyperscale service tier](/azure/azure-sql/database/service-tier-hyperscale) of Azure SQL Database. Hyperscale is a managed platform as a service (PaaS) database that separates compute from storage. The service tier supports up to 128 TB per database and up to 128 vCores (up to 192 vCores currently in preview). This architecture supports scaling compute without moving data.

### Benefits

- **Licensing cost**: Hyperscale has no SQL software license fee.
- **Compute scaling**: Scale provisioned compute up or down in single-digit minutes, regardless of database size. Serverless compute scales automatically based on workload demand, typically in less than a second.
- **Storage scaling**: Storage grows automatically up to 128 TB per database or 100 TB per database in an elastic pool. You don't need to preallocate or manually resize storage.
- **Fast backup and restore**: Hyperscale uses storage snapshots for backups, which minimizes I/O utilization. File snapshot-based restores and database copies can finish in minutes, regardless of database size.
- **Read scale-out**: Offload read-only workloads such as reporting or analytics to as many as 30 read-only named replicas. Configure compute independently for each named replica. Built-in high-availability replicas and optional geo-replicas are also available.
- **Workload support**: Use Hyperscale for online transaction processing (OLTP), hybrid transactional and analytical processing (HTAP), reporting, and analytical workloads.
- **Fully managed PaaS**: Azure manages patching, backups, and platform maintenance.

### Considerations

- **Purchasing model**: Hyperscale uses the vCore-based purchasing model. It supports provisioned and serverless compute for single databases, and provisioned compute for elastic pools.
- **Feature differences**: Azure SQL Database uses a database-level service model and differs from SQL Server in some Transact-SQL features and instance-level capabilities. Review [T-SQL differences between SQL Server and Azure SQL Database](/azure/azure-sql/database/transact-sql-tsql-differences-sql-server) and [Azure SQL Database and Azure SQL Managed Instance feature differences](/azure/azure-sql/database/features-comparison) before migration.
- **Application changes**: Applications that use cross-database queries, linked servers, SQL Server Agent jobs, or other instance-level objects might require changes. Azure SQL Database provides alternatives for some features, such as elastic queries and [elastic jobs](/azure/azure-sql/database/elastic-jobs-overview).
- **Networking**: Azure SQL Database provides a public endpoint by default. Configure [Azure Private Link](/azure/azure-sql/database/private-endpoint-overview) or [virtual network service endpoints](/azure/azure-sql/database/vnet-service-endpoint-rule-overview) if your deployment requires private or restricted connectivity.
- **Serverless compute**: Hyperscale serverless doesn't support auto-pause. Compare its idle compute costs with General Purpose serverless when evaluating intermittent workloads.

### Resources

- [Azure SQL Database overview](/azure/azure-sql/database/sql-database-paas-overview)
- [Hyperscale service tier](/azure/azure-sql/database/service-tier-hyperscale)
- [Azure SQL Database Hyperscale FAQ](/azure/azure-sql/database/service-tier-hyperscale-frequently-asked-questions-faq)
- [Hyperscale architecture](/azure/azure-sql/database/hyperscale-architecture)
- [Migration overview: SQL Server to Azure SQL Database](/data-migration/sql-server/database/overview)
- [Compare Azure SQL deployment options](/azure/azure-sql/azure-sql-iaas-vs-paas-what-is-overview)
- [Reverse migrate from Hyperscale](/azure/azure-sql/database/reverse-migrate-from-hyperscale)
- [Configure and manage Hyperscale named replicas](/azure/azure-sql/database/hyperscale-named-replica-configure)

### Migration tools

- [Azure Migrate](/azure/migrate/tutorial-assess-sql) discovers and assesses SQL Server instances and recommends Azure SQL deployment options and sizing.
- [Azure Database Migration Service](/azure/dms/dms-overview) supports offline migrations to Azure SQL Database.
- [SQL Server migration component in SQL Server Management Studio](/ssms/migrate/migrate-sql-server-azure-sql) assesses SQL Server instances and supports migration to Azure SQL.
- Use SQL Server Management Studio or SqlPackage 18.4 and later to import or export BACPAC files for Hyperscale databases up to 150 GB.

## Extend support for existing environments with ESUs

[!INCLUDE [2016-esu](../../includes/2016-esu.md)]

If you're not ready to upgrade, and you're not ready to move to the cloud, you can subscribe to Extended Security Updates (ESU) to receive **Critical** security updates for up to three years past the end of the support date.

### Benefits

- **Application support**: Choose this option if your application needs recertification on a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option is common for applications that don't use [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).
- **Consistent infrastructure**: You don't need to change your infrastructure in any way.
- **Time**: This option is available for three years, giving you extra time to certify your applications.

### Considerations

- **Limited availability**: Only customers with Software Assurance or subscription licenses can use this option.
- **Cost**: This option can be costly, as ESUs are approximately 75% of the on-premises license cost annually.
- **Limited time frame**: You can use this option for only three years. You need to upgrade or migrate at the end of the three-year period to ensure your security and compliance.
- **No bug fixes**: If you encounter a non-security bug with the product, [!INCLUDE [msCoName](../../includes/msconame-md.md)] won't release a fix for it.
- **Limited support**: ESUs don't include new features, functional improvements, or customer-requested fixes. Security fixes are limited to fixes rated as Critical by the [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/update-guide). Technical support is limited to issues directly related to the released updates. 

### Resources

- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Detailed ESU frequently asked questions](https://www.microsoft.com/windows-server/extended-security-updates)
- [Extend support for SQL Server with Azure](/azure/azure-sql/virtual-machines/windows/extended-security-updates-sql-vm)
- [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default)

## Migrate to Azure VMware Solution

By using Azure VMware Solution, you can run your VMware environment in Azure by using familiar tooling and a quick migration path.

When you run [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] on Azure VMware in this environment, you get the free ESU benefits extended to running in Azure. For information about how to configure ESUs in Azure VMware Solution, see [ESUs for SQL Server and Windows Server in Azure VMware Solution VMs](/azure/azure-vmware/extended-security-updates-windows-sql-server). For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] ESU, you can subscribe to ESUs.


## Lifecycle dates

The following table provides an approximation of lifecycle dates for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] products. For more details and accuracy, see the [Microsoft Lifecycle Policy](/lifecycle/products/?terms=sql%20server) page.

| **Version** | **Release year** | **Mainstream Support end year** | **Extended Support end year** |
| :--- | :--- | :--- | :--- |
| [SQL Server 2025](/lifecycle/products/sql-server-2025) | 2025 | 2031 | 2036 |
| [SQL Server 2022](/lifecycle/products/sql-server-2022) | 2022 | 2028 | 2033 |
| [SQL Server 2019](/lifecycle/products/sql-server-2019) | 2019 | 2025 | 2030 |
| [SQL Server 2017](/lifecycle/products/sql-server-2017) | 2017 | 2022 | 2027 |
| [SQL Server 2016](/lifecycle/products/sql-server-2016) | 2016 | 2021 | 2026 |
| [SQL Server 2014](/lifecycle/products/sql-server-2014) | 2014 | 2019 | 2024 |


> [!IMPORTANT]  
> If any discrepancy exists between this table and the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Lifecycle page, the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Lifecycle page supersedes this table. This table is intended as an approximate reference.

## Related content

- [SQL Server 2025](https://www.microsoft.com/sql-server/sql-server-downloads)
- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Extended security updates (ESUs) for SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/extended-security-updates-sql-vm)
- [SQL Server on Azure VM overview](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview)
- [What is Azure SQL Managed Instance?](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview)
