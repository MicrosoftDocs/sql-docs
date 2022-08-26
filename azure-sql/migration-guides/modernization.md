---
title: Migration and modernization FAQ
description: Frequently Asked Questions about migrating from SQL Server to Azure SQL, and modernizing the data platform
ms.service: sql-database
ms.topic: faq
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dmarinkovic
ms.date: 08/26/2022
---
# Migrating SQL Server workloads (FAQ)

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqlserver-sqldb-sqlmi-asvm.md)]

#### \*\****[View the YAML version here](./modernization-faq.yml)***\*\*

## Modernize applications and SQL

### Azure SQL

#### What are the benefits of moving Apps and SQL Server workloads to Azure?

A [migration to Azure](https://azure.microsoft.com/overview/cloud-migration-benefits-challenges/#overview) brings optimized costs, flexibility and scalability, enhanced security, compliance, improved business continuity, and simplified management and monitoring.

#### What is Azure SQL?

[Azure SQL](../azure-sql-iaas-vs-paas-what-is-overview.md) is a family of services that use the SQL Server database engine in the Azure Cloud. The following services belong to Azure SQL: [Azure SQL Database](../database/sql-database-paas-overview.md) (SQL DB), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md) (SQL MI) and [SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/).

#### What is the difference between migration and modernization to Azure SQL?

**Migration to Azure SQL** involves moving applications, infrastructure, and data from one location (for example, a company's on-premises datacenter) to Azure infrastructure. For SQL Server customers, this means migrating their workloads while minimizing impact to operations, that is, to reduce IT costs, enhance security & resilience, and achieve on-demand scale.

**Modernization to Azure SQL** involves updating existing applications for newer computing approaches and application frameworks and use of cloud-native technologies. This can be achieved by using PaaS services such as Azure SQL Database and Azure SQL Managed Instance, which provides additional benefits of app innovation, agility, developer velocity, and cost optimization.

#### How do I decide if I should move my SQL Server to a VM, SQL MI or SQL DB?

SQL Server on VM is suitable to lift-and-shift your SQL workloads quickly, maintaining 100% SQL Server compatibility and OS access. SQL MI is the right PaaS target to modernize your existing SQL Server applications at scale providing almost all SQL Server features (including instance-level features) while reducing the costs of server and database management. SQL DB is the most appropriate choice when building native cloud applications, as it offers high elasticity and flexibility of choosing between architectural and compute tiers, such as Serverless tier for increased elasticity and Hyperscale tier for a highly scalable storage and compute resources. The service comparison provides more details. A range of migration tools helps making the optimal choice by providing an assessment of target service compatibility and costs.

#### How can I reduce costs by moving to Azure SQL?

Moving to Azure brings savings in resource, maintenance, and real estate costs, in addition to the ability to optimize workloads so that they cost less to run. Azure SQL MI and SQL DB bring all the advantages of PaaS services, providing automated performance tuning, backups, software patching and high-availability; all of which entails enormous effort and cost when performing manually. For example, SQL MI and SQL DB (single database and elastic pool) come with built-in HA. Also, Business Critical (SQL MI) and Premium (SQL DB) tires provide read-only replicas at no additional cost, while SQL DB Hyperscale tier allows HA and named secondary replicas for read scale out at no license cost. Additionally, Software Assurance customers can use their on-premises SQL Server license on Azure by applying Azure Hybrid Benefit (AHB). Software Assurance also lets you implement free passive HA and DR secondaries using SQL VM. In addition, every Azure SQL service provides you the option to reserve instances in advance (1-3 years) and obtain significant additional savings.

#### What is the best licensing path to save costs when moving existing SQL Server workloads to Azure?

Unique to Azure, Azure Hybrid Benefit (AHB) is a licensing benefit that allows you bringing your existing Windows Server and SQL Server licenses with Software Assurance (SA) to Azure. Combined with reservations savings and extended security updates, AHB can bring you up to 85% savings compared to pay-as-you-go pricing in Azure SQL. In addition, make sure to check different Dev/Test pricing plans.

### Applications and SQL modernization scenarios

#### Scenario 1: Data Center move to the Cloud: What is the modernization path for Apps and SQL Server databases?

Updating an organization's existing apps to a cloud first model can be achieved by using fully managed application and data services Azure App Service, Azure Spring Apps, Azure SQL Database, Azure SQL MI and other PaaS services. Application and Data Modernization in Azure is achieved through several stages, with the most common scenario examples described within Cloud Adoption Framework.

#### Scenario 2: Reducing SQL Server costs: How can I reduce the cost for my existing SQL Server fleet?

Moving to Azure SQL VMs, SQL MI or SQL DB brings savings in resource, maintenance, and real estate costs. Using your SQL Server on-premises licenses in Azure via Azure Hybrid Benefit, using Azure Reservations for SQL VM, SQL MI and SQL DB vCores, and using constrained vCPU capable Virtual Machines, will give you a wide variety of options to build a cost-effective solution. For implementing BCDR solutions in Azure SQL, you benefit from built-in HA replicas of SQL MI and SQL DB, or free passive HA and DR secondaries using SQL VM. Also, Business Critical (SQL MI) and Premium (SQL DB) tiers provide read-only replicas at no additional cost, while SQL DB Hyperscale tier allows HA and named secondary replicas for read scale out at no license cost. In addition, make sure to check different Dev/Test pricing plans. If you are interested to understand how you can save up to 64% by moving to Azure SQL please check ESG report on The Economic Value of Migrating on-premises SQL Server Instances to Microsoft Azure SQL Solutions.

#### Scenario 3: Optimize Apps portfolio: How can I at the same time modernize both my Apps portfolio and SQL Server instances?

Application and Data Modernization in Azure is achieved through several stages, with the most common scenario examples described within Cloud Adoption Framework.

#### Scenario 4: SQL Server End of Support: Which options do I have to move to Azure SQL?

Once your SQL Server has reached the end of support stage, you have several modernization options towards Azure SQL. One of the options is to migrate your workload to an Azure SQL MI, which provides high feature parity with the on-premises SQL Server product. Alternatively, with some additional effort, you can move the workload to Azure SQL DB. These services run on SQL Server evergreen features, effectively granting you "the end of End of Support". Backward compatibility is provided via compatibility levels and database compatibility level settings. Tools like  Azure SQL Migration extension in Azure Data Studio or Data Migration Assistant will help you to identify possible incompatibilities. Whenever a Platform-as-a-Service (PaaS) solution doesn't fit your workload, Azure SQL Virtual Machines provide the possibility to do an as-is migration. By moving to Azure SQL VM you will also receive free extended security patches, which can provide significant savings (for example, up to 69% for SQL Server 2012).

#### Scenario 5: Meeting regulatory compliance: How does Azure SQL help meet regulatory compliance requirements?

Azure Policy has built-in policies, which help organizations meet regulatory compliance. Ad-hoc and customized policies can also be created: please check Azure Policy Regulatory Compliance controls for Azure SQL Database & SQL Managed Instance. For an overview of compliance offerings, you can consult Azure compliance documentation.

### Getting started, the holistic approach

#### How to prepare a migration business case?

Microsoft Cloud Adoption Framework for Azure is a great starting point to help you create and implement the business and technology strategy necessary for your move to Azure.

#### Where can I find migration guides for Azure SQL?

The following guides help you discover, assess, and migrate from SQL Server to SQL VM, SQL MI and SQL DB.

#### Do I have to modernize Apps and SQL at the same time, what are my options?

No. Feel free to take an iterative approach to modernizing each workload and component.

#### Can I modernize SQL Server to SQL MI and just Lift & Shift my App to a VM?

Yes. You can connect your App towards SQL MI through different scenarios (including when hosting it on a Virtual Machine), as described in this article.

## Business and technical evaluation

### Total cost of ownership, licensing and benefits

#### How can I estimate Total Cost of Ownership (TCO) savings when moving to Azure SQL?

Moving to Azure SQL brings significant TCO savings by improving operational efficiency and business agility, as well as eliminating the need for on-premises hardware and software. According to ESG report on The Economic Value of Migrating on-premises SQL Server Instances to Microsoft Azure SQL Solutions, you can save up to 47% when migrating from on-premises to Azure SQL Virtual Machines (IaaS), and up to 64% when migrating to Azure SQL Managed Instance or Azure SQL Database (PaaS).

#### What is the licensing model for SQL MI?

SQL MI licensing follows vCore-based licensing model, where you pay for compute, storage, and backup storage resources. You can choose between several service tiers (General Purpose, Business Critical) and hardware generations. SQL MI pricing page provides a full overview of possible SKUs and prices.

#### What is the licensing model for SQL DB?

SQL DB provides a choice between vCore-based purchasing model and Database transaction unit purchasing model. You can explore Pricing - Azure SQL Database Single Database | Microsoft Azure and learn about pricing options.

#### What subscription types are supported in SQL MI?

Check Supported subscription types.

#### Can I use my on-premises SQL Server license when moving to Azure SQL?

If you own Software Assurance for core-based or qualifying subscription licenses for SQL Server Standard Edition or SQL Server Enterprise Edition, you can use your existing SQL Server license when moving to SQL MI, SQL DB or Azure VM by applying Azure Hybrid Benefit (AHB). You can also simultaneously use these licenses both in on-premises and Azure environments (dual use rights) for up to 180 days.

#### How can I move from SQL VM to SQL MI?

You can follow the same migration guide as for the on-premises SQL Server.

#### I am using SQL Server subscription license. Can I use it to move to Azure SQL?

Yes, qualifying subscription licenses can be used to pay Azure SQL services at a reduced (base) rate by applying Azure Hybrid Benefit (AHB).

#### I am using SQL Server CAL licenses. How can I move to Azure SQL?

SQL Server CAL licenses with appropriate license mobility rights can be used on Azure SQL VMs, and on Azure SQL Dedicated Host.

#### What is Azure Hybrid Benefit (AHB)?

Unique to Azure, Azure Hybrid Benefit (AHB) is a licensing benefit that allows you bringing your existing Windows Server and SQL Server licenses with Software Assurance (SA) to Azure. Combined with reservations savings and extended security updates, AHB can bring you up to 85% savings compared to pay-as-you-go pricing in Azure SQL.

#### How do I translate my SQL Server on-premises license to vCore license in SQL MI?

For every 1 core of SQL Server Enterprise Edition, you get 4 vCore-s of SQL MI General Purpose tier or 1 vCore of SQL MI Business Critical tier; Similarly, 1 core of SQL Server Standard Edition translates to 1 vCore of SQL MI General Purpose tier. You can use Azure Hybrid Benefit Savings Calculator to calculate the exact savings for your SQL Server estate.

#### Is Software Assurance (SA) required for Azure SQL?

Software Assurance is a licensing program that can be applied to on-premises SQL Server licenses, allowing license mobility, AHB and other benefits. SA is required if AHB is to be invoked for using existing SQL Server licenses (with SA) when moving to Azure SQL. Without SA + AHB, customers are charged with PAYG pricing. Alternatively, the outsourcing software management terms applicable to SQL server licenses acquired prior to October 1, 2019 permit you to allocate your existing licenses to Azure Dedicated Host just as you would license a server in your own data center: see Pricing - Dedicated Host Virtual Machines | Microsoft Azure.

#### Do I have to pay for High Availability (HA) in SQL MI?

SQL MI is built on top of inherent high availability architecture. This way, there is no extra charge for HA.

#### Do I have to pay for HA and DR replicas for Azure SQL?

If you have Software Assurance, on Azure SQL VM you can implement high availability (HA) as well as disaster recovery (DR) plans with SQL Server without incurring additional licensing costs for the passive disaster recovery instance. Consult SQL VM documentation for more details.

#### Do I have to pay for Disaster Recovery (DR) in SQL MI?

Yes. These are separate costs. Among other services, you incur the cost of another SQL MI (GP or BC tier, depending on requirements), storage, and networking.

#### Can I centrally manage Azure Hybrid Benefit for SQL Server across the entire Azure subscription?

Yes. You can centrally manage your Azure Hybrid Benefit for SQL Server across the scope of an entire Azure subscription or overall billing account. This feature is currently in preview.

#### If I move some of SQL Servers, my workloads to SQL MI and leave some workloads on-premises: can I manage all my SQL licenses in one place?

You can centrally manage your licenses that are covered by Azure Hybrid Benefit for SQL Server across the scope of an entire Azure subscription or overall billing account. This data can be combined with an overview maintained by your licensing partner/procurement department or obtaining licensing information by creating your own custom licensing overview. Your licenses must be used either on-premises or in the cloud, but you'll have 180 days of concurrent use rights while migrating servers.

#### How can I minimize downtime during the online migration?

Link feature for Azure SQL Managed Instance offers the best possible minimum downtime online migrations solution, meeting the needs of the most critical tier-1 applications. You can consult a full tool of migration tools and technologies choose the optimal for your use scenario.

### Risk free migration with a hybrid strategy

#### Can I keep running on-prem, while modernizing my applications in Azure?

With SQL Server 2016-2022, you can use the Link feature for Azure SQL Managed Instance to create a hybrid connection between SQL Server and Azure SQL Managed Instance. Data is replicated near real-time from SQL Server to Azure, and can be used to modernize your workloads in Azure. You can use the replicated data in Azure for read scale-out and for offloading analytics.

#### For how long can I keep the hybrid solution using Link feature for Azure SQL Managed Instance running?

You can keep running the hybrid link for as long as needed: weeks, months, years at a time, there are no restrictions on this.

#### Can I apply a hybrid approach and use Link feature for Azure SQL Managed Instance in order to validate my migration strategy, before migrating to Azure?

Yes, you can use your replicated data in Azure to test and validate your migration strategy (performance, workloads and applications) prior to migrating to Azure.

#### Can I reverse migrate out of Azure SQL and go back to SQL Server if required?

With SQL Server 2022, we offer the best possible solution to seamlessly move data back offline and online1 from Azure SQL Managed Instance to SQL Server, completely de-risking the migrations strategy to Azure.

### Workloads and architecture

#### How do I determine which SQL Server workloads should be moved to SQL MI?

There are a variety of tools available to help you assess your workload for compatibility with Azure SQL MI. You can use the Azure SQL Migration extension in Azure Data Studio or Data Migration Assistant. Both tools provide help to detect issues that can affect the Azure SQL MI migration and provide guidance on how to resolve them. After verifying compatibility, you can run the SKU recommendation tool to analyze performance data and recommend a minimal Azure SQL MI SKU. Make sure to visit Azure Migrate which is a centralized hub to assess and migrate on-premises servers, infrastructure, applications, and data to Azure.

#### How do I determine the appropriate SQL MI target for a particular SQL Server on-premises workload: SQL MI General Purpose or Business Critical Tier?

SQL MI tier choice is guided by availability, performance (for example, throughput, OIPS, latency) and feature (for example, in-memory OLTP, …) requirements. General Purpose tier is suitable for most generic workloads, as it already provides HA architecture and a fully managed database engine with a storage latency between 5 ms and 10 ms. Business Critical tier is designed for applications that require low-latency (1-2 ms) responses from the storage layer, fast recovery, strict availability requirements, and the ability to off-load analytics workloads.

#### How can I move a highly automated SQL Server to SQL MI?

Infrastructure deployment automation of Azure SQL can be applied through PowerShell and CLI. Useful examples can be found on the Azure PowerShell samples for Azure SQL Database and Azure SQL Managed Instance page. By using Azure DevOps Continuous Integration (CI) and Deployment (CD) pipelines, it is possible to fully embed this within your Infrastructure-as-Code practices. Building your database models and scripts can also be integrated through Database Projects with Visual Studio Code or Visual Studio. The use of Azure DevOps CI/CD pipelines will enable deployment of your Database Projects to an Azure SQL destination of your choice.

#### Can I move only a specific workload out of an on-premises cluster and what will be the impact to licensing & cost?

It's possible to only migrate the databases related to one workload towards an Azure SQL MI. Creating and operating an Azure SQL MI will require SQL Server licenses. Azure Hybrid Benefit will provide you with the ability to reuse your licenses. Reach out to your licensing partner to review what possibilities can be used with license mobility and restructuring your current licenses.

#### I maintain a highly consolidated SQL Server with multiple applications running against it: can I move it to SQL MI?

SQL MI is closer to Server as a Service rather than Database as a service. You can import multiple databases to a single instance with shared security. SQL MI also supports cross database queries.

#### How to migrate SQL Server Business Intelligence workloads (including Reporting Services and Analysis Services) which are not compatible with SQL MI?

Least effort migration path will be to move as-is and host the Business Intelligence components on an Azure VM. Do note that the Reporting Services database can be hosted on Azure SQL MI and Azure Data Factory provides the capability to lift and shift SSIS solutions to the cloud. When building a modern solution is part of the migration effort, Azure is providing a wide variety of services to build an Enterprise data warehouse solution.

#### I am using an App from an ISV that doesn't support SQL MI / Azure. What are my options to move my App to Azure and SQL Server to Azure SQL?

Migrating your environment as-is to an Azure Virtual Machine will be the safest option when the ISV or vendor isn't providing any options. However, we encourage ISVs and vendors that are working closely with Microsoft to review the options with Azure SQL Managed Instance. Azure SQL Managed Instance provides backward compatibility options through database compatibility level, guidance for T-SQL differences and has implemented major features to Azure SQL Managed instance.

#### How do I keep compatibility of my current SQL Server database version in SQL MI?

Database compatibility level can be set in MI, as described in Azure SQL Blog.

### Security

#### How does Azure SQL help in enhancing the database security posture?

The security strategy follows the layered defense-in-depth approach: Network security + Access management + Threat protection + Information Protection. Check SQL DB and SQL MI security capabilities. Azure-wide, Microsoft Defender for Cloud provides a solution for Cloud Security Posture Management (SCPM) and Cloud Workload Protection (CWP).

### Business continuity

#### How can I adapt on-premises Business Continuity and Disaster Recover (BCDR) concepts into Azure SQL MI concepts?

Most Azure SQL BCDR concepts have an equivalent in on-premises SQL Server implementations. For example, the inherent High Availability of SQL MI General Purpose tier can be seen as a cloud equivalent for SQL Server FCI. Similarly, SQL MI Business Critical tier can be seen as a cloud equivalent for an Always On Availability Group with synchronous commit to a minimum number of replicas. As a Disaster Recovery concept, an Auto-failover Group on SQL MI is comparable to an Asynchronous Always On Availability Group with asynchronous commit. SQL DB and SQL MI HA are backed by SLAs. You can check find more on SQL DB and SQL MI Business Continuity in the official documentation.

#### How are backups handled in Azure SQL PaaS services?

You can check documentation for automated backups in SQL MI and SQL DB to learn about RPO, RTO, retention, scheduling and other backup capabilities and features.

#### How is High Availability (HA) achieved in SQL MI and SQL DB?

SQL MI and DB are built on top of inherent high availability (HA) architecture. This includes support for auto-failover groups and various other features. One can choose between two HA architecture variants (standard, premium).

#### How does Disaster Recovery work in SQL MI and SQL DB?

SQL DB and SQL MI documentation, and SQL MI FAQ page provide information on DR options.

### Performance and scale

#### How do I obtain better performance by moving on-prem SQL Server to SQL MI, SQL DB or SQL VM?

Moving from on-premises will provide you with performance benefits due to the latest SQL Server database engine features, cloud scaling flexibility and the newest generation of underlying hardware. It is important to ensure a proper sizing based on your requirements for CPU, memory and storage (IOPS, latency, transaction log throughput and size). SQL MI and SQL DB also provide a choice between different hardware configurations and service tiers that provide additional means to reach target performance. Applications can also take advantage read scale-out abilities including with named replicas and geo replicas and techniques such as database sharding.

## Migration and modernization process

#### We decided we want to modernize SQL Server workloads to Azure SQL, what is the next step?

A good practice is to form a dedicated Migration team, which will formulate and execute the migration plan. If your company has an assigned Microsoft or Microsoft Partner account team, they can provide guidance regarding Migration team required skill set and overall process.

#### Where can I find migration guides to Azure SQL?

The following guides help you discover, assess, and migrate from SQL Server to SQL VM, SQL MI and SQL DB. You can consult Azure Database Migration Guides that also contains guides for migrating to another database targets.

#### Which migration tools can I use?

You can use Azure SQL migration extension for Azure Data Studio for SQL Server assessment and migration, or choose among other migration tools.

#### How can I minimize downtime during the online migration?

Link feature for Azure SQL Managed Instance offers the best possible minimum downtime online migrations solution, meeting the needs of the most critical tier-1 applications.

#### How can I optimize the costs once I migrate to Azure SQL?

Cost optimization guidelines from the Microsoft Azure Well-Architected Framework (WAF) provide methodology to optimize costs for every Azure SQL service. The following blog provides WAF cost optimization highlights in case of SQL MI.

## See also

- [Frequently asked questions for SQL Server on Azure VMs](../virtual-machines/windows/frequently-asked-questions-faq.yml)
- [Azure SQL Managed Instance frequently asked questions (FAQ)](../managed-instance/frequently-asked-questions-faq.yml)
- [Azure SQL Database Hyperscale FAQ](../database/service-tier-hyperscale-frequently-asked-questions-faq.yml)
- [Azure Hybrid Benefit FAQ](https://azure.microsoft.com/pricing/hybrid-benefit/faq/)
