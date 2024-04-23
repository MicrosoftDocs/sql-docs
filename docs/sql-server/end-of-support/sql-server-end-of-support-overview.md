---
title: "End of support options"
description: Learn about the different options available for SQL Server products that reach end of support, including SQL Server 2012.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/26/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---
# SQL Server end of support options

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article explains your options for addressing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] products that reach end of support.

## Understand the SQL Server lifecycle

Each version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is backed by a minimum of 10 years support, which includes five years in mainstream support, and five years in extended support:

- **Mainstream support** includes functional, performance, scalability, and security updates.
- **Extended support** includes only security updates.

**End of support** (also sometimes known as end of life) indicates that a product has reached the end of its lifecycle, and servicing and support is no longer available for the product. For more information about the Microsoft Lifecycle, see [Microsoft Lifecycle Policy](/lifecycle).

## Options

Once your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of support stage, you can choose to:

- [Migrate](/azure/azure-sql/migration-guides/) your workload to [Azure SQL](/azure/sql-database/sql-database-paas-vs-sql-server-iaas).
- Migrate your workload to an Azure Virtual Machine as-is for [free Extended Security Updates](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support).
- Upgrade to a current version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- Purchase an [Extended Security Updates subscription](https://www.microsoft.com/windows-server/extended-security-updates).

For more information, guidance, and tools to plan and automate your upgrade or migration, see:

- [SQL Server 2012 end of support](/lifecycle/products/microsoft-sql-server-2012)
- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Extended Security Updates: Frequently asked questions](extended-security-updates-frequently-asked-questions.md)

This table provides migration options for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance that is out of extended support, running on-premises.

| Migration option | Description |
| --- | --- |
| **Modernize to Azure** | |
| Move to PaaS with Azure SQL Managed Instance | **Lift-and-shift** to SQL Managed Instance for fully managed services that always run on evergreen features. In other words, the end of End of Support. |
| Move to IaaS with SQL Server on Azure VM | **Upgrade** to latest SQL Server on Azure VMs, or **move "as-is"** with SQL Server 2012, and get 3 years of Extended Security Updates for free. |
| Move to Azure VMware Solution | **Move "as-is"** with SQL Server out of extended support, and get 3 years of Extended Security Updates for free. |
| **Modernize on-premises** | |
| Upgrade on-premises | **Upgrade** to the latest versions of SQL Server and Windows Server. |
| **Stay on-premises** | |
| Purchase extended security updates | **Keep** server + application as-is for up to three years. |

This article describes the benefits and considerations for each approach, with more resources to help guide your decision-making process.

## Upgrade SQL Server

Once your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of support, you can choose to upgrade to a newer and supported version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option gives you environmental consistency, allows you to use the latest feature set, and adopts the new version's support lifecycle.

### Benefits

- **Latest technology**: New [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] versions introduce innovations that include performance, scalability, and high-availability features, and improved security.

- **Control**: You have the most control over features and scalability, because you manage both hardware and software.

- **Familiar environment**: If you're upgrading from an older version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this environment is the most similar.

- **Wide applicability**: Applicable for database applications of any kind, including OLTP systems and data warehousing.

- **Low risk for database applications**: When the database compatibility is at the same level as the legacy system, existing database applications are protected from functional and performance changes that can have detrimental effects. An application only needs to be fully recertified when it requires features available under a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

### Considerations

- **Cost**: This approach requires the biggest up-front investment and the most ongoing management. You have to buy, maintain, and manage your own hardware and software.
- **Downtime**: There could be downtime depending on your upgrade strategy. There's also an inherent risk of running into issues during an in-place upgrade process.
- **Complexity**: If you're on an unsupported version of Windows Server, you also need to upgrade the OS as the newer versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might not be supported on those Windows versions. There's added risk during the OS upgrade process, so doing a side-by-side migration might be the more prudent, yet more costly, approach. In-place OS upgrades aren't supported on failover cluster instances for Windows Server 2008 or [!INCLUDE [winserver2008r2-md](../../includes/winserver2008r2-md.md)].

  > [!NOTE]  
  > Cluster OS rolling upgrades are available starting with Windows Server 2016.

### Resources

- [Installation media](https://www.microsoft.com/evalcenter/evaluate-sql-server-2019)
- [Upgrade SQL Server Using the Installation Wizard (Setup)](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)

What's new in:

- [SQL Server 2022](../what-s-new-in-sql-server-2022.md)
- [SQL Server 2019](../what-s-new-in-sql-server-2019.md)
- [SQL Server 2017](../what-s-new-in-sql-server-2017.md)
- [SQL Server 2016](../what-s-new-in-sql-server-2016.md)

Hardware requirements:

- [SQL Server 2022](../install/hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [SQL Server 2019](../install/hardware-and-software-requirements-for-installing-sql-server-2019.md)
- [SQL Server 2017 and earlier versions](../install/hardware-and-software-requirements-for-installing-sql-server.md)

Supported version and edition upgrades:

- [SQL Server 2022](../../database-engine/install-windows/supported-version-and-edition-upgrades-2022.md)
- [SQL Server 2019](../../database-engine/install-windows/supported-version-and-edition-upgrades-2019.md)
- [SQL Server 2017](../../database-engine/install-windows/supported-version-and-edition-upgrades-2017.md)
- [SQL Server 2016](../../database-engine/install-windows/supported-version-and-edition-upgrades.md?view=sql-server-2016&preserve-view=true)

Tools:

- [Database Experimentation Assistant](../../dea/database-experimentation-assistant-overview.md) can help evaluate the target version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for a specific workload.
- [Data Migration Assistant](../../dma/dma-overview.md) can help detect compatibility issues that can affect database functionality in your new version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- [Query Tuning Assistant](../../relational-databases/performance/upgrade-dbcompat-using-qta.md) can help to tune workloads that can experience adverse effects when upgrading the database compatibility.

For more information about new features in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], see [SQL Server 2022 comparison](https://www.microsoft.com/sql-server/sql-server-2022-comparison).

## Azure SQL Managed Instance

If you'd like to take advantage of offloading maintenance and cost, but find the feature set of an [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] single database too limiting, you can move to [SQL Managed Instance](/azure/sql-database/sql-database-managed-instance). A managed instance closely resembles an on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], without having to worry about such things as hardware failure, or patching. SQL Managed Instance is a collection of system and user databases with a shared set of resources that is lift-and-shift ready, and can be used for most migrations to the cloud. This option is best for new applications or existing on-premises applications that want to use the latest stable [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] features and that are migrated to the cloud with minimal changes.

### Benefits

- **Cost**: You can save costs by offloading software and hardware maintenance.
- **Lift and shift**: You can lift and shift your entire [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on-premises instance to a managed instance, including all databases with minimal to no database change.
- **Features**: Closely matches the features of an on-premises instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], such as cross-database queries, transactional replication publishing and distribution, SQL job scheduling, and CLR support.
- **Scalability**: Within a managed instance, all databases share resources, and it's possible to scale up and down at any time without downtime.
- **Automation**: Patching and backups happening automatically, saving you valuable maintenance time.
- **Availability**: The cost of the service includes both storage and high availability, with 99.99% availability guaranteed.
- **Intelligent Insights**: Gain insight about the performance of your databases with built-in intelligence analytics.
- **Versionless**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is versionless, meaning you're always on the latest version, and never have to worry about upgrading, or downtime. Plus, you're always on the latest and greatest, with our latest stable features being released to the cloud first.
- **Low risk for database applications**: When the database compatibility is at the same level as the on-premises databases, existing database applications are protected from functional and performance changes that can have detrimental effects. An application only needs to be fully recertified when it requires features available in a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

### Considerations

- **Cost**: The managed instance option can be more costly than the single database option.
- **Transact-SQL differences**: There are some [!INCLUDE [tsql](../../includes/tsql-md.md)] (T-SQL) differences between a single database and an on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Deployment**: Deploying a managed instance can take more time than a single database.
- **Feature limitation**: Although a managed instance shares most features with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], there are still some features that are unsupported.
- **Size limitation**: The combined storage size for all databases within a managed instance is limited to 8 TB, as opposed to 524 PB for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on-premises.
- **Networking**: The networking requirements for a managed instance add an extra layer of complexity to your infrastructure, and requires either an Azure ExpressRoute or VPN Gateway.
- **Maintenance time**: You have no guarantee for the exact maintenance time, though it's nearly transparent.

### Resources

- [SQL Managed Instance overview](/azure/sql-database/sql-database-managed-instance)
- [Choosing an Azure SQL option](/azure/sql-database/sql-database-paas-vs-sql-server-iaas)
- [SQL Database feature comparison](/azure/sql-database/sql-database-features)
- [Migrate SQL Server to Azure SQL Managed Instance](/azure/sql-database/sql-database-managed-instance-migrate)
- [Broader migration process](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)

Tools:

- [Data Migration Assistant](../../dma/dma-overview.md)
- [Database Migration Service](/azure/dms/dms-overview)

## Extend support

If you're not ready to upgrade, and you're not ready to move to the cloud, you have the ability to purchase an Extended Security Updates subscription to receive **Critical** security updates for up to three years past the end of the support date.

### Benefits

- **Application support**: This option is the best option if your application requires recertification on a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option is common for applications that don't use [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).
- **Consistent infrastructure**: You don't have to change your infrastructure in any way.
- **Technical support**: If you have Software Assurance, or another support plan, you can continue receiving technical support from [!INCLUDE [msCoName](../../includes/msconame-md.md)] on your end-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product. This option is the only way to get support for [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)].
- **Time**: This option is available for three years, giving you extra time to certify your applications.

### Considerations

- **Limited availability**: This option is only available to customers with Software Assurance or subscription licenses.
- **Cost**: This option can prove costly, as Extended Security Updates are approximately 75% of the on-premises license cost annually.
- **Limited time-frame**: This option is only available for three years, so you still need to upgrade or migrate at the end of the three-year period if you want to ensure your security and compliance.
- **No bug fixes**: If you encounter a non-security bug with the product, [!INCLUDE [msCoName](../../includes/msconame-md.md)] won't release a fix for it.
- **Limited support**: Extended Security Updates don't include new features, functional improvements, or customer-requested fixes. Security fixes are limited to fixes rated as Critical by the [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/update-guide).

### Resources

- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Detailed ESU frequently asked questions](https://www.microsoft.com/windows-server/extended-security-updates)
- [Extend support for SQL Server with Azure](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support)
- [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default)

## SQL Server on Azure VMs

Another option is to migrate your workload to an [Azure Virtual Machine running SQL Server](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview). You can migrate your system as-is and keep your end-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], or you can upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This option is best for migrations and applications requiring OS-level access. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] virtual machines are lift-and-shift ready for existing applications that require fast migration to the cloud with minimal or no changes.

### Benefits

- **Free Extended Security Updates**: If you choose to keep your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] as-is, using [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)], you can get free Extended Security Updates for three years past the end of support date, even without having Software Assurance.

- **Cost-saving**: You save the cost of hardware and server software, only paying for hourly usage.
- **Lift-and-shift**: You can lift-and-shift your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and application infrastructure into the cloud with minimal or no changes.
- **Hosted environment**: You get the benefits of a hosted environment, such as offloading hardware, and software maintenance.
- **Automation**: If you're on [!INCLUDE [winserver2008r2-md](../../includes/winserver2008r2-md.md)] and later versions, you get the benefit of automated patching, and automated backups.
- **OS Control**: You have control over the operating system environment, but with the familiar feature set of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Rapid deployment**: You can quickly deploy from a library of virtual machine images.
- **License mobility**: You can bring your license, allowing you to decrease operating cost.
- **High availability**: You benefit from the built-in virtual machine availability by the Azure infrastructure with up to 99.99% availability, and take advantage of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] high availability options such as failover cluster instances and Always On availability groups.
- **Low risk for database applications**: When the database compatibility is at the same level as the legacy databases, existing database applications are protected from functional and performance changes that can have detrimental effects. An application only needs to be fully recertified when it requires features available under a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

### Considerations

- **Manageability**: You still have to manage both [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and operating system software.
- **Networking**: You have to configure the virtual machine to integrate with your networking and Active Directory infrastructure, which is an added layer of complexity.
- **Shared storage FCI**: Azure virtual machines only support failover cluster instances using Storage Spaces Direct or Premium File Shares, and don't support a failover cluster instance using shared storage. As such, Azure virtual machines only support failover cluster instances when using Windows Server 2012 or greater.
- **Scalability downtime**: You have downtime while changing the CPU and storage resources.
- **Size limitation**: Although the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance can support as many databases as needed, the cumulative total of all databases for a single instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is 256 TB, as opposed to 524 PB for an on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

### Resources

- [SQL Server VM overview](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview)
- [Choosing an Azure SQL option](/azure/sql-database/sql-database-paas-vs-sql-server-iaas)
- [Migrate SQL Server to an Azure VM](/azure/azure-sql/virtual-machines/windows/migrate-to-vm-from-sql-server)
- [Extend support for SQL Server with Azure](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support)
- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Detailed ESU frequently asked questions](https://www.microsoft.com/windows-server/extended-security-updates)
- [Automated Patching for SQL Server on Azure virtual machines](/azure/azure-sql/virtual-machines/windows/automated-patching)
- [Automated Backup for Azure virtual machines (Resource Manager)](/azure/azure-sql/virtual-machines/windows/automated-backup)
- [Business continuity and HADR for SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/business-continuity-high-availability-disaster-recovery-hadr-overview)
- [SQL virtual machine frequently asked questions](/azure/azure-sql/virtual-machines/windows/frequently-asked-questions-faq)

## Azure VMware Solution

You can enable ESUs with Azure VMware Solution. For benefits, considerations, and resources, review [ESUs for SQL Server and Windows Server in Azure VMware Solution VMs](/azure/azure-vmware/extended-security-updates-windows-sql-server).

## Azure SQL Database

If you want to offload maintenance, reduce costs, and eliminate the need to upgrade in the future, you can move your workload to [Azure SQL Database single database](/azure/sql-database/sql-database-single-database). This option is best for modern cloud applications that want to use the latest stable [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] features and have time constraints in development and marketing.

### Benefits

- **Cost**: Single database can be cost-effective, since hardware, software, and maintenance costs are offloaded, and you can pay for usage by the second or the hour.
- **Flexibility**: Single database is well suited for cloud-designed applications when developer productivity and fast time-to-market solutions are critical, or that have require external access.
- **Common features**: The most commonly used [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] features are available, but not as many as for Azure SQL Managed Instance.
- **Rapid deployment**: You can quickly deploy a single database.
- **Scalability**: You can quickly and easily scale up and down as needed for your business, providing more cost-saving benefits.
- **Availability**: The cost of the service includes both storage and high availability, with 99.995% availability guaranteed.
- **Automation**: Patching and backups happening automatically, saving you valuable maintenance time.
- **Intelligent Insights**: Gain insight about the performance of your database with built-in intelligence analytics.
- **Versionless**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is versionless, meaning you're always on the latest version, and never have to worry about upgrading, or downtime. Plus, you're always on the latest and greatest, with our latest stable features being released to the cloud first.
- **Low risk for database applications**: When the database compatibility is at the same level as the on-premises database, existing applications are protected from functional and performance changes that can have detrimental effects. An application only needs to be fully recertified when it requires features available under a newer database compatibility setting. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

### Considerations

- **Limited migration options**: You can only migrate a single database at a time, rather than an entire instance.
- **Feature limitation**: Although the most commonly used Azure SQL Database features are available, the feature set for a single database isn't as complete as for Azure SQL Managed Instance, or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Transact-SQL differences**: There are some [!INCLUDE [tsql](../../includes/tsql-md.md)] (T-SQL) differences between a single database and an on-premises [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Size limitations**: A single database has a maximum database size of 100 TB, compared to 524 PB for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].
- **Maintenance time**: You have no guarantee for the exact maintenance time, though it's nearly transparent.

### Resources

- [Azure SQL Database overview](/azure/sql-database/sql-database-technical-overview)
- [Choosing an Azure SQL option](/azure/sql-database/sql-database-paas-vs-sql-server-iaas)
- [SQL Database feature comparison](/azure/sql-database/sql-database-features)
- [Migrate SQL Server to a single database](/azure/sql-database/sql-database-single-database-migrate)
- [Broader migration process](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)
- [Single database T-SQL differences](/azure/sql-database/sql-database-transact-sql-information)
- [vCore](/azure/sql-database/sql-database-vcore-resource-limits-single-databases) and [DTU](/azure/sql-database/sql-database-dtu-resource-limits-single-databases) resource limits
- [Intelligent Insights](/azure/sql-database/sql-database-intelligent-insights)

Tools:

- [Data Migration Assistant](../../dma/dma-overview.md)
- [Database Migration Service](/azure/dms/dms-overview)

## Lifecycle dates

The following table provides an approximation of lifecycle dates for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] products. For greater details and accuracy, see the [Microsoft Lifecycle Policy](/lifecycle/products/?terms=sql%20server) page.

| **Version** | **Release year** | **Mainstream Support end year** | **Extended Support end year** |
| :--- | :--- | :--- | :--- |
| [SQL Server 2022](/lifecycle/products/?terms=sql%20server%202022) | 2022 | 2028 | 2033 |
| [SQL Server 2019](/lifecycle/products/?terms=SQL%20Server%202019) | 2019 | 2025 | 2030 |
| [SQL Server 2017](/lifecycle/products/?terms=SQL%20Server%202017) | 2017 | 2022 | 2027 |
| [SQL Server 2016](/lifecycle/products/?terms=SQL%20Server%202016) | 2016 | 2021 | 2026 |
| [SQL Server 2014](/lifecycle/products/?terms=SQL%20Server%202014) | 2014 | 2019 | 2024 |
| [SQL Server 2012](/lifecycle/products/?terms=SQL%20Server%202012) | 2012 | 2017 | 2022 |
| [SQL Server 2008 R2](/lifecycle/products/??terms=SQL%20Server%202008%20R2) | 2010 | 2012 | 2019 |
| [SQL Server 2008](/lifecycle/products/?terms=SQL%20Server%202008) | 2008 | 2012 | 2019 |
| [SQL Server 2005](/lifecycle/products/?terms=SQL%20Server%202005) | 2006 | 2011 | 2016 |
| [SQL Server 2000](/lifecycle/products/?terms=SQL%20Server%202000) | 2000 | 2005 | 2013 |

> [!IMPORTANT]  
> If any discrepancy exists between this table, and the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Lifecycle page, then the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Lifecycle supersedes this table, as this table is meant to be used as an approximate reference.

## Related content

- [SQL Server 2022](https://www.microsoft.com/sql-server/sql-server-2022)
- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Extend support for SQL Server with Azure](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support)
- [SQL Server on Azure VM overview](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview)
- [Azure SQL Database overview](/azure/sql-database/sql-database-technical-overview)
