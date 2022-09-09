---
title: Migrating SQL Server Workloads FAQ
description: Frequently Asked Questions about migrating from SQL Server to Azure SQL, and modernizing the data platform
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dmarinkovic, randolphwest
ms.date: 09/08/2022
ms.service: sql-database
ms.topic: faq
---
# Migrate SQL Server workloads (FAQ)

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqlserver-sqldb-sqlmi-asvm.md)]

Migrating on-premises SQL Server workloads and associated applications to the cloud usually brings a wide range of questions which go beyond mere product feature information.

This article provides a holistic view and helps understand how to fully unlock the value when migrating to Azure SQL. The **[Modernize applications and SQL](#modernize-applications-and-sql)** section covers questions about Azure SQL in general as well as common application and SQL modernization scenarios. The **[Business and technical evaluation](#business-and-technical-evaluation)** section covers cost saving, licensing, minimizing migration risk, business continuity, security, workloads and architecture, performance and similar business and technical evaluation questions. The last section covers the actual **[Migration and modernization process](#migration-and-modernization-process)**, including guidance on migration tools.

## Modernize applications and SQL

### Azure SQL

#### What are the benefits of moving applications and SQL Server workloads to Azure?

A [migration to Azure](https://azure.microsoft.com/overview/cloud-migration-benefits-challenges/#overview) brings optimized costs, flexibility and scalability, enhanced security, compliance, improved business continuity, and simplified management and monitoring.

#### What is Azure SQL?

[Azure SQL](../azure-sql-iaas-vs-paas-what-is-overview.md) is a family of services that use the SQL Server database engine in the Azure Cloud. The following services belong to Azure SQL: [Azure SQL Database](../database/sql-database-paas-overview.md) (SQL DB), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md) (SQL MI) and [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/).

#### What is the difference between migration and modernization to Azure SQL?

**Migration to Azure SQL** involves moving applications, infrastructure, and data from one location (for example, a company's on-premises datacenter) to Azure infrastructure. For SQL Server customers, this means migrating your workloads while minimizing impact to operations. You can reduce IT costs, enhance security and resilience, and achieve on-demand scale.

**Modernization to Azure SQL** involves updating existing applications for newer computing approaches and application frameworks and use of cloud-native technologies. This can be achieved by using PaaS services such as Azure SQL Database and Azure SQL Managed Instance, which provides extra benefits of app innovation, agility, developer velocity, and cost optimization.

#### What does IaaS and PaaS mean?

**[Infrastructure as a service](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-iaas/#overview)** (IaaS) is a type of cloud computing service that offers essential compute, storage, and networking resources on demand.

**[Platform as a service](https://azure.microsoft.com/resources/cloud-computing-dictionary/what-is-paas/)** (PaaS) is a complete development and deployment environment in the cloud, with resources that enable you to deliver everything from simple cloud-based apps to sophisticated, cloud-enabled enterprise applications.

PaaS provides additional advantages over IaaS, such as shorter development cycles, extra development capabilities without adding staff, affordable access to sophisticated tools, to mention a few. Azure SQL provides both PaaS (SQL MI, SQL DB) and IaaS (SQL VM) services.

#### How do I decide if I should move my SQL Server to a Virtual Machine, SQL Managed Instance or SQL Database?

- **SQL Managed Instance** is the right PaaS target to modernize your existing SQL Server applications at scale providing almost all SQL Server features (including instance-level features) while reducing the costs of server and database management.

- **SQL Database** is the most appropriate choice when building native cloud applications, as it offers high elasticity and flexibility of choosing between architectural and compute tiers, such as Serverless tier for increased elasticity and [Hyperscale tier](/azure/azure-sql/database/service-tier-hyperscale) for a highly scalable storage and compute resources.

- If you need full control and customization, including OS access, you can opt for **SQL Server on Azure VM**. The [service comparison](/azure/azure-sql/azure-sql-iaas-vs-paas-what-is-overview#service-comparison) provides more details. A range of [migration tools](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview#migration-tools) helps making the optimal choice by providing an assessment of target service compatibility and costs.

#### How can I reduce costs by moving to Azure SQL?

Moving to Azure brings savings in resource, maintenance, and real estate costs, in addition to the ability to optimize workloads so that they cost less to run. Azure SQL MI and SQL DB bring all the advantages of PaaS services, providing automated performance tuning, backups, software patching and high-availability, all of which entails enormous effort and cost when performing manually.

For example, SQL MI and SQL DB (single database and elastic pool) come with built-in HA. Also, Business Critical (SQL MI) and Premium (SQL DB) tiers provide read-only replicas at no additional cost, while SQL DB Hyperscale tier allows HA and named [secondary replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas) for [read scale-out](/azure/azure-sql/database/read-scale-out) at no license cost. Additionally, [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default) customers can use their on-premises SQL Server license on Azure by applying [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/) (AHB). Software Assurance also lets you implement [free passive HA and DR secondaries using SQL VM](/azure/azure-sql/virtual-machines/windows/business-continuity-high-availability-disaster-recovery-hadr-overview#free-dr-replica-in-azure).

In addition, every Azure SQL service provides you the option to reserve instances in advance (1-3 years) and obtain significant additional savings. [Dev/Test pricing plans](https://azure.microsoft.com/pricing/dev-test/#devtest) provide a way to further reduce development costs. Finally, check the following article on how you can [Optimize your Azure SQL Managed Instance cost with Microsoft Azure Well-Architected Framework](https://techcommunity.microsoft.com/t5/azure-sql-blog/optimize-your-azure-sql-managed-instance-cost-with-microsoft/ba-p/2235216).

#### What is the best licensing path to save costs when moving existing SQL Server workloads to Azure?

Unique to Azure, Azure Hybrid Benefit (AHB) is a licensing benefit that allows you bringing your existing Windows Server and SQL Server licenses with Software Assurance (SA) to Azure. Combined with reservations savings and extended security updates, AHB can bring you up to 85% savings compared to pay-as-you-go pricing in Azure SQL. In addition, make sure to check different [Dev/Test pricing plans](https://azure.microsoft.com/pricing/dev-test/#devtest).

### Applications and SQL modernization scenarios

#### Scenario 1: Data center move to the cloud: what is the modernization path for applications and SQL Server databases?

Updating an organization's existing apps to a cloud first model can be achieved by using fully managed application and data services including [Azure App Service](https://azure.microsoft.com/services/app-service/), [Azure Spring Apps](https://azure.microsoft.com/services/spring-cloud/), [Azure SQL Database](/azure/azure-sql/database/service-tier-hyperscale), [Azure SQL MI](/azure/azure-sql/managed-instance/) and other PaaS services. [Azure Kubernetes Services](https://azure.microsoft.com/services/kubernetes-service/) (AKS) provides a managed container-based approach within Azure. [Application and Data Modernization](https://azure.microsoft.com/solutions/application-and-database-modernization/#overview) in Azure is achieved through several [stages](https://azure.microsoft.com/migration/migration-journey/#how-to-migrate), with the most common scenario [examples](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-overview) described within the [Cloud Adoption Framework](/azure/cloud-adoption-framework/).

#### Scenario 2: Reducing SQL Server costs: How can I reduce the cost for my existing SQL Server fleet?

Moving to Azure SQL VMs, SQL MI or SQL DB brings savings in resource, maintenance, and real estate costs. Using your SQL Server on-premises licenses in Azure via [Azure Hybrid Benefit](https://azure.microsoft.com/pricing/hybrid-benefit/#why-azure-hybrid-benefit), using [Azure Reservations](/azure/cost-management-billing/reservations/save-compute-costs-reservations) for SQL VM, SQL MI and SQL DB vCores, and using [constrained vCPU capable Virtual Machines](/azure/virtual-machines/constrained-vcpu) will give you a wide variety of options to build a cost-effective solution.

For implementing BCDR solutions in Azure SQL, you benefit from built-in HA replicas of SQL MI and SQL DB or [free passive HA and DR secondaries using SQL VM](/azure/azure-sql/virtual-machines/windows/business-continuity-high-availability-disaster-recovery-hadr-overview#free-dr-replica-in-azure). Also, Business Critical (SQL MI) and Premium (SQL DB) tiers provide read-only replicas at no additional cost, while SQL DB Hyperscale tier allows HA and named [secondary replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas) for [read scale-out](/azure/azure-sql/database/read-scale-out) at no license cost. In addition, make sure to check different [Dev/Test pricing plans](https://azure.microsoft.com/pricing/dev-test/#devtest).

If you're interested to understand how you can save up to 64% by moving to Azure SQL please check ESG report on [The Economic Value of Migrating On-Premises SQL Server Instances to Microsoft Azure SQL Solutions](https://azure.microsoft.com/resources/the-economic-value-of-migrating-on-premises-sql-server-instances-to-microsoft-azure-sql-solutions/). Finally, check the following article on how you can [Optimize your Azure SQL Managed Instance cost with Microsoft Azure Well-Architected Framework](https://techcommunity.microsoft.com/t5/azure-sql-blog/optimize-your-azure-sql-managed-instance-cost-with-microsoft/ba-p/2235216).

#### Scenario 3: Optimize application portfolio: How can I at the same time modernize both my application portfolio and SQL Server instances?

[Application and Data Modernization](https://azure.microsoft.com/solutions/application-and-database-modernization/#overview) in Azure is achieved through several stages, with the most common scenario [examples](/azure/cloud-adoption-framework/migrate/azure-best-practices/contoso-migration-overview) described within the [Cloud Adoption Framework](/azure/cloud-adoption-framework/).

#### Scenario 4: SQL Server end of support: Which options do I have to move to Azure SQL?

Once your SQL Server has reached the end of support stage, you have several [modernization options](/sql/sql-server/end-of-support/sql-server-end-of-life-overview) towards Azure SQL. One of the options is to migrate your workload to an Azure SQL Managed Instance, which provides high feature parity with the on-premises SQL Server product. Alternatively, with some additional effort, you can move the workload to Azure SQL DB. These services run on SQL Server evergreen features, effectively granting you *"the end of End of Support"*.

Backward compatibility is provided via [compatibility levels](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server#compatibility-levels) and [database compatibility level](/sql/relational-databases/databases/view-or-change-the-compatibility-level-of-a-database) settings. Tools like [Azure SQL Migration extension](/sql/azure-data-studio/extensions/azure-sql-migration-extension) in Azure Data Studio or [Data Migration Assistant](/sql/dma/dma-overview) will help you identify possible incompatibilities.

Whenever a Platform-as-a-Service (PaaS) solution doesn't fit your workload, Azure SQL Virtual Machines provide the possibility to do an as-is migration. By moving to Azure SQL VM, you'll also receive [free extended security patches](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support) which can provide significant savings (for example, [up to 69%](https://cloudblogs.microsoft.com/sqlserver/2022/03/24/move-end-of-support-sql-server-2012-to-azure-virtual-machines-and-save/) for SQL Server 2012).

#### Scenario 5: Meeting regulatory compliance: How does Azure SQL help meet regulatory compliance requirements?

[Azure Policy](/azure/governance/policy/overview) has built-in policies that help organizations meet regulatory compliance. Ad-hoc and customized policies can also be created. For more information, see [Azure Policy Regulatory Compliance controls for Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/security-controls-policy). For an overview of compliance offerings, you can consult [Azure compliance documentation](/azure/compliance/).

### Getting started, the holistic approach

#### How to prepare a migration business case?

The Microsoft [Cloud Adoption Framework for Azure](https://azure.microsoft.com/cloud-adoption-framework/#overview) is a great starting point to help you create and implement the business and technology strategy necessary for your move to Azure.

#### Where can I find migration guides for Azure SQL?

The following guides help you discover, assess, and migrate from SQL Server to [SQL VM](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide), [SQL MI](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview) and [SQL DB](/azure/azure-sql/migration-guides/database/sql-server-to-sql-database-overview).

#### Do I have to modernize applications and SQL at the same time? What are my options?

No. Feel free to take an iterative approach to modernizing each workload and component.

#### Can I modernize SQL Server to SQL MI and just lift and shift my application to a VM?

Yes. You can [Connect your application to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/connect-application-instance) through different scenarios, including when hosting it on a VM.

## Business and technical evaluation

### Total cost of ownership, licensing and benefits

#### How can I estimate Total Cost of Ownership (TCO) savings when moving to Azure SQL?

Moving to Azure SQL brings significant TCO savings by improving operational efficiency and business agility, as well as eliminating the need for on-premises hardware and software. According to ESG report on [The Economic Value of Migrating On-Premises SQL Server Instances to Microsoft Azure SQL Solutions](https://azure.microsoft.com/resources/the-economic-value-of-migrating-on-premises-sql-server-instances-to-microsoft-azure-sql-solutions/), you can save up to 47% when migrating from on-premises to Azure SQL Virtual Machines (IaaS), and up to 64% when migrating to Azure SQL Managed Instance or Azure SQL Database (PaaS).

#### What is the licensing model for SQL MI?

[SQL MI licensing](/azure/azure-sql/managed-instance/service-tiers-managed-instance-vcore) follows vCore-based licensing model, where you pay for compute, storage, and backup storage resources. You can choose between several service tiers (General Purpose, Business Critical) and hardware generations. The [SQL MI pricing page](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/) provides a full overview of possible SKUs and prices.

#### What is the licensing model for SQL DB?

SQL DB provides a choice between the [vCore-based purchasing model](/azure/azure-sql/database/service-tiers-sql-database-vcore) and [Database transaction unit purchasing model](/azure/azure-sql/database/service-tiers-dtu). You can explore [Pricing - Azure SQL Database Single Database](https://azure.microsoft.com/pricing/details/azure-sql-database/single/) and learn about pricing options.

#### What subscription types are supported in SQL MI?

Check [Supported subscription types](/azure/azure-sql/managed-instance/resource-limits#supported-subscription-types) for SQL MI.

#### Can I use my on-premises SQL Server license when moving to Azure SQL?

If you own Software Assurance for core-based or qualifying subscription licenses for SQL Server Standard Edition or SQL Server Enterprise Edition, you can use your existing SQL Server license when moving to SQL MI, SQL DB or Azure VM by applying Azure Hybrid Benefit (AHB). You can also simultaneously use these licenses both in on-premises and Azure environments (dual use rights) for up to 180 days.

#### How do I move from SQL VM to SQL MI?

You can follow the same [migration guide](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview) as for the on-premises SQL Server.

#### I'm using SQL Server subscription license. Can I use it to move to Azure SQL?

Yes, qualifying subscription licenses can be used to pay Azure SQL services at a reduced (base) rate by applying Azure Hybrid Benefit (AHB).

#### I'm using SQL Server CAL licenses. How can I move to Azure SQL?

SQL Server CAL licenses with appropriate license mobility rights can be used on Azure SQL VMs, and on Azure SQL Dedicated Host.

#### What is Azure Hybrid Benefit (AHB)?

Unique to Azure, Azure Hybrid Benefit (AHB) is a licensing benefit that allows you bringing your existing Windows Server and SQL Server licenses with Software Assurance (SA) to Azure. AHB can bring you up to 85% savings compared to pay-as-you-go pricing in Azure SQL, when combined with reservations savings and extended security updates.

#### How do I translate my SQL Server on-premises license to vCore license in SQL MI, SQL DB, and SQL VM?

For every one (1) core of SQL Server Enterprise Edition, you get four (4) vCores of SQL MI General Purpose tier or one (1) vCore of SQL MI Business Critical tier. Similarly, one (1) core of SQL Server Standard Edition translates to one (1) vCore of SQL MI General Purpose tier, while four (4) vCores of SQL Server Standard Edition translate to one (1) vCore of SQL MI Business Critical.

The [Azure Hybrid Benefit August 2020 update](https://www.microsoft.com/licensing/news/expanded-ahb-rights-for-microsoft-sql-server) provides an overview of possible core-to-vCore conversions for SQL MI, SQL DB and SQL VM. Applicable AHB rights are also described in the [Product Terms](https://www.microsoft.com/licensing/terms/welcome/welcomepage). You can also use the [Azure Hybrid Benefit Savings Calculator](https://azure.microsoft.com/pricing/hybrid-benefit/#calculator) to calculate the exact savings for your SQL Server estate.

#### Is Software Assurance (SA) required for using SQL Server license on Azure SQL?

[Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-by-benefits) is a licensing program that can be applied to on-premises SQL Server licenses, allowing license mobility, AHB, and other benefits. SA is required if AHB is to be invoked for using existing SQL Server licenses (with SA) when moving to Azure SQL. Without SA + AHB, customers are charged with PAYG pricing.

Alternatively, the outsourcing software management terms applicable to SQL server licenses acquired prior to October 1, 2019 permit you to allocate your existing licenses to Azure Dedicated Host just as you would license a server in your own data center: see [Pricing - Dedicated Host Virtual Machines](https://azure.microsoft.com/pricing/details/virtual-machines/dedicated-host/).

#### Do I have to pay for high availability (HA) in SQL MI and SQL DB?

Both General Purpose and Business Critical tiers of SQL MI and SQL DB are built on top of inherent [high availability architecture](/azure/azure-sql/database/high-availability-sla). This way, there's no extra charge for HA. For SQL DB Hyperscale tier HA replica is charged.

#### Do I have to pay for HA and DR replicas for Azure SQL?

If you have Software Assurance, on Azure SQL VM you can implement high availability (HA) and disaster recovery (DR) plans with SQL Server without incurring additional licensing costs for the passive disaster recovery instance. See the [SQL VM documentation](/azure/azure-sql/virtual-machines/windows/business-continuity-high-availability-disaster-recovery-hadr-overview#free-dr-replica-in-azure) for more details.

#### Do I have to pay for disaster recovery (DR) in SQL MI and SQL DB?

Yes. These are separate costs.

#### Can I centrally manage Azure Hybrid Benefit for SQL Server across the entire Azure subscription?

Yes. You can [centrally manage your Azure Hybrid Benefit](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope) for SQL Server across the scope of an entire Azure subscription or overall billing account. This feature is currently in preview.

#### If I move some of SQL Servers, my workloads to SQL MI and leave some workloads on-premises, can I manage all my SQL licenses in one place?

You can [centrally manage your licenses](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope) that are covered by Azure Hybrid Benefit for SQL Server across the scope of an entire Azure subscription or overall billing account. This data can be combined with an overview maintained by your licensing partner/procurement department or obtaining licensing information by creating your own [custom licensing overview](https://techcommunity.microsoft.com/t5/azure-sql-blog/getting-insights-into-the-utilization-of-sql-server-licenses-on/ba-p/2051320). Your licenses must be used either on-premises or in the cloud, but you'll have 180 days of concurrent use rights while migrating servers.

#### How can I minimize downtime during the online migration?

The [Link feature for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview) offers the best possible minimum downtime online migrations solution, meeting the needs of the most critical tier-1 applications. You can consult a full range of [migration tools](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview#migration-tools) and technologies choose the optimal for your use scenario.

### Risk free migration with a hybrid strategy

#### Can I keep running on-premises, while modernizing my applications in Azure?

With SQL Server 2016, 2017, 2019, and 2022, you can use the [Link feature for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview) to create a hybrid connection between SQL Server and Azure SQL Managed Instance. Data is replicated near real-time from SQL Server to Azure, and can be used to modernize your workloads in Azure. You can use the replicated data in Azure for read scale-out and for offloading analytics.

#### For how long can I keep the hybrid solution using Link feature for Azure SQL Managed Instance running?

You can keep running the hybrid link for as long as needed: weeks, months, years at a time, there are no restrictions on this.

#### Can I apply a hybrid approach and use Link feature for Azure SQL Managed Instance in order to validate my migration strategy, before migrating to Azure?

Yes, you can use your replicated data in Azure to test and validate your migration strategy (performance, workloads and applications) prior to migrating to Azure.

#### Can I reverse migrate out of Azure SQL and go back to SQL Server if necessary?

With SQL Server 2022, we offer the best possible solution to seamlessly move data back with native backup and restore from SQL Managed Instance to SQL Server, completely de-risking the migrations strategy to Azure.

### Workloads and architecture

#### How do I determine which SQL Server workloads should be moved to SQL MI?

When migrating SQL Server workloads to Azure SQL MI is normally the first option, as most databases are "as-is" ready to migrate to SQL MI. There are several tools available to help you assess your workload for compatibility with Azure SQL MI.

You can use the [Azure SQL Migration extension](/sql/azure-data-studio/extensions/azure-sql-migration-extension) in Azure Data Studio or [Data Migration Assistant](/sql/dma/dma-overview). Both tools provide help to detect issues that can affect the Azure SQL MI migration and provide guidance on how to resolve them. After verifying compatibility, you can run the [SKU recommendation tool](/sql/dma/dma-sku-recommend-sql-db) to analyze performance data and recommend a minimal Azure SQL MI SKU. Make sure to visit [Azure Migrate](/azure/migrate/) which is a centralized hub to assess and migrate on-premises servers, infrastructure, applications, and data to Azure.

#### How do I determine the appropriate SQL MI target for a particular SQL Server on-premises workload: SQL MI General Purpose or Business Critical Tier?

SQL MI tier choice is guided by availability, performance (for example, throughput, OIPS, latency) and feature (for example, in-memory OLTP) requirements. The [General Purpose tier](/azure/azure-sql/database/service-tier-general-purpose) is suitable for most generic workloads, as it already provides HA architecture and a fully managed database engine with a storage latency between 5 ms and 10 ms. The [Business Critical tier](/azure/azure-sql/database/service-tier-business-critical/) is designed for applications that require low-latency (1-2 ms) responses from the storage layer, fast recovery, strict availability requirements, and the ability to off-load analytics workloads.

#### How can I move a highly automated SQL Server to SQL MI?

**Infrastructure deployment automation** of Azure SQL can be done with [PowerShell](/powershell/module/az.sql) and [CLI](/cli/azure/sql/db). Useful examples can be found in the [Azure PowerShell samples for Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/powershell-script-content-guide) article. You can use [Azure DevOps Continuous Integration (CI) and Deployment (CD) Pipelines](/azure/devops/pipelines/get-started/what-is-azure-pipelines) to fully embed automation within your Infrastructure-as-Code practices.

**Building your database models and scripts** can also be integrated through Database Projects with [Visual Studio Code](/sql/tools/visual-studio-code/sql-server-develop-use-vscode) or [Visual Studio](/visualstudio/data-tools/creating-and-managing-databases-and-data-tier-applications-in-visual-studio). The use of Azure DevOps CI/CD pipelines will [enable deployment of your Database Projects](https://devblogs.microsoft.com/azure-sql/devops-for-azure-sql/) to an Azure SQL destination of your choice. Finally, service automation via third party tools is also possible. For more information, see [Azure SQL Managed Instance – Terraform command](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-managed-instance-terraform-command-available/ba-p/2749141).

#### Can I move only a specific workload out of an on-premises cluster and what will be the impact to licensing and cost?

It's possible to only migrate the databases related to one workload towards an Azure SQL MI. Creating and operating an Azure SQL MI will require SQL Server licenses. [Azure Hybrid Benefit](/azure/azure-sql/azure-hybrid-benefit) will provide you with the ability to reuse your licenses. Reach out to your licensing partner to review what possibilities can be used with [license mobility](/licensing/licensing-programs/software-assurance-license-mobility) and restructuring your current licenses.

#### I maintain a highly consolidated SQL Server with multiple applications running against it. Can I move it to SQL MI?

Similarly as with on-premises SQL Server, you can consolidate and run multiple databases on a single SQL MI instance, at the same time benefiting from inherent high-availability architecture as well as shared security and management. SQL MI also supports cross database queries.

#### How do I migrate SQL Server Business Intelligence workloads (including Reporting Services and Analysis Services) that aren't compatible with SQL MI?

Least effort migration path will be to move as-is and host the Business Intelligence components on an Azure VM. The `Reporting Services` database can be [hosted on Azure SQL MI](https://techcommunity.microsoft.com/t5/azure-sql-blog/modernizing-ssrs-and-a-step-by-step-guide-to-bringing/ba-p/2483050) and Azure Data Factory provides the capability to [lift and shift SSIS solutions](/sql/integration-services/lift-shift/ssis-azure-lift-shift-ssis-packages-overview) to the cloud. When building a modern solution is part of the migration effort, Azure is providing a wide variety of services to build an [Enterprise data warehouse](/azure/architecture/solution-ideas/articles/enterprise-data-warehouse) solution.

#### I'm using an application from an ISV that doesn't support SQL MI / Azure. What are my options to move my App to Azure and SQL Server to Azure SQL?

Migrating your environment as-is to an Azure Virtual Machine will be the safest option when the ISV or vendor isn't providing any options. However, we [encourage](https://azure.microsoft.com/partners/isv/#get-started) ISVs and vendors that are working closely with Microsoft to review the options with Azure SQL Managed Instance. Azure SQL Managed Instance provides backward compatibility options through [database compatibility level](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level), guidance for [Transact-SQL differences](/azure/azure-sql/managed-instance/transact-sql-tsql-differences-sql-server) and has implemented [major features](/azure/azure-sql/database/features-comparison) to Azure SQL Managed instance.

#### How do I keep the compatibility of my current SQL Server database version in SQL MI?

Database compatibility level can be set in MI, as described on the [Azure SQL Blog](https://techcommunity.microsoft.com/t5/azure-sql-blog/sql-managed-instance-closing-the-gap-on-sql-server-application/ba-p/3214460).

### Security

#### How does Azure SQL help in enhancing the database security posture?

The security strategy follows the layered defense-in-depth approach: Network security + Access management + Threat protection + Information Protection. You can read more about [SQL DB and SQL MI security capabilities](/azure/azure-sql/database/security-overview). Azure-wide, [Microsoft Defender for Cloud](/azure/defender-for-cloud/defender-for-cloud-introduction) provides a solution for Cloud Security Posture Management (SCPM) and Cloud Workload Protection (CWP).

### Business continuity

#### How can I adapt on-premises business continuity and disaster recovery (BCDR) concepts into Azure SQL MI concepts?

Most Azure SQL BCDR concepts have an equivalent in on-premises SQL Server implementations. For example, the inherent [high availability](/azure/azure-sql/database/high-availability-sla) of SQL MI [General Purpose tier](https://techcommunity.microsoft.com/t5/azure-sql-blog/high-availability-in-azure-sql-mi-general-purpose-service-tier/ba-p/3298977) can be seen as a cloud equivalent for SQL Server FCI. Similarly, SQL MI [Business Critical tier](https://techcommunity.microsoft.com/t5/azure-sql-blog/high-availability-in-azure-sql-mi-business-critical-service-tier/ba-p/3521212) can be seen as a cloud equivalent for an Always On Availability Group with synchronous commit to a minimum number of replicas. As a Disaster Recovery concept, an [Auto-failover Group](/azure/azure-sql/managed-instance/auto-failover-group-sql-mi) on SQL MI is comparable to an Asynchronous Always On Availability Group with asynchronous commit. SQL DB and SQL MI HA are backed by [Service-Level Agreements](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance/). You can find more on SQL DB and SQL MI Business Continuity in the [official documentation](/azure/azure-sql/database/business-continuity-high-availability-disaster-recover-hadr-overview).

#### How are backups handled in Azure SQL PaaS services?

You can check documentation for automated backups in [SQL MI](/azure/azure-sql/managed-instance/automated-backups-overview) and [SQL DB](/azure/azure-sql/database/automated-backups-overview) to learn about RPO, RTO, retention, scheduling and other backup capabilities and features.

#### How is high availability (HA) achieved in SQL MI and SQL DB?

SQL MI and DB are built on top of inherent [high availability](/azure/azure-sql/database/high-availability-sla) (HA) architecture. This includes support for auto-failover groups and various other features. You can choose between two HA architecture models: [Standard availability model in General Purpose service tier](https://techcommunity.microsoft.com/t5/azure-sql-blog/high-availability-in-azure-sql-mi-general-purpose-service-tier/ba-p/3298977), or [Premium availability model in Business Critical service tier](https://techcommunity.microsoft.com/t5/azure-sql-blog/high-availability-in-azure-sql-mi-business-critical-service-tier/ba-p/3521212).

#### How does disaster recovery work in SQL MI and SQL DB?

See the [SQL DB and SQL MI](/azure/azure-sql/database/business-continuity-high-availability-disaster-recover-hadr-overview) documentation. The [SQL MI Frequently Asked Questions](/azure/azure-sql/managed-instance/frequently-asked-questions-faq#business-continuity) provide information on DR options.

### Performance and scale

#### How do I obtain better performance by moving on-premises SQL Server to SQL MI, SQL DB or SQL VM?

Moving from on-premises will provide you with performance benefits due to the latest SQL Server database engine features, cloud scaling flexibility and the newest generation of underlying hardware. [Find out why your SQL Server data belongs on Azure](https://azure.microsoft.com/blog/find-out-why-your-sql-server-data-belongs-on-azure/). You can also read a recently published [study by Principled Technologies](https://facts.pt/GZUp6xk) benchmarking SQL MI and SQL Server on Amazon Web Services (AWS) RDS. It's important to ensure a [proper sizing based on your requirements](/sql/dma/dma-sku-recommend-sql-db) for CPU, memory and [storage](/azure/azure-sql/managed-instance/resource-limits#data-and-log-storage) (IOPS, latency, transaction log throughput and size). SQL MI and SQL DB also provide a choice between different [hardware configurations](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview#vcore-based-purchasing-model) and [service tiers](/azure/azure-sql/managed-instance/resource-limits#service-tier-characteristics) that provide additional means to reach target performance. Applications can also take advantage of [read scale-out](/azure/azure-sql/database/read-scale-out) abilities including with [named replicas](/azure/azure-sql/database/service-tier-hyperscale-replicas) and [geo-replicas](/azure/azure-sql/database/active-geo-replication-overview), and techniques such as [database sharding](/azure/azure-sql/database/elastic-scale-introduction).

#### How can I compare SQL Managed Instance performance to SQL Server performance?

See the [Performance](/azure/azure-sql/managed-instance/frequently-asked-questions-faq#performance-) section of the [SQL MI FAQ](/azure/azure-sql/managed-instance/frequently-asked-questions-faq) for guidance on performance comparison and tuning.

## Migration and modernization process

#### I want to modernize SQL Server workloads to Azure SQL. What is the next step?

A great place to start is joining the [Azure Migration and Modernization Program](https://azure.microsoft.com/migration/migration-modernization-program/#overview). When you start a migration project, a good practice is to form a dedicated Migration team to formulate and execute the migration plan. If your company has an assigned Microsoft or Microsoft Partner account team, they can provide guidance regarding Migration team required skill set and overall process.

#### Where can I find migration guides to Azure SQL?

The following guides help you discover, assess, and migrate from SQL Server to [SQL VM](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide), [SQL MI](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview) and [SQL DB](/azure/azure-sql/migration-guides/database/sql-server-to-sql-database-overview). You can consult [Azure Database Migration Guides](/data-migration/) that also contains guides for migrating to another database targets.

#### Which migration tools can I use?

You can use the [Azure SQL migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) for SQL Server assessment and migration, or choose among other [migration tools](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-managed-instance-overview#migration-tools).

#### How do I minimize downtime during the online migration?

The [Link feature for Azure SQL Managed Instance](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview) offers the best possible minimum downtime online migrations solution, meeting the needs of the most critical tier-1 applications.

#### How can I optimize the costs once I migrate to Azure SQL?

[Cost Optimization](/azure/architecture/framework/cost/) guidelines of Microsoft Azure Well-Architected Framework (WAF) provide methodology to optimize costs for every Azure SQL service. You can also find out more about [WAF cost optimization highlights](https://techcommunity.microsoft.com/t5/azure-sql-blog/optimize-your-azure-sql-managed-instance-cost-with-microsoft/ba-p/2235216) for SQL MI.

## See also

- [Frequently asked questions for SQL Server on Azure VMs](../virtual-machines/windows/frequently-asked-questions-faq.yml)
- [Azure SQL Managed Instance frequently asked questions (FAQ)](../managed-instance/frequently-asked-questions-faq.yml)
- [Azure SQL Database Hyperscale FAQ](../database/service-tier-hyperscale-frequently-asked-questions-faq.yml)
- [Azure Hybrid Benefit FAQ](https://azure.microsoft.com/pricing/hybrid-benefit/faq/)
