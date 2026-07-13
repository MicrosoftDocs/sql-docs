---
title: Manage After Migration
titleSuffix: Azure SQL Database
description: Learn how to manage your single and pooled databases after migration to Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: roblescarlos, mathoma, dfurman, randolphwest
ms.date: 07/13/2026
ms.service: azure-sql-database
ms.subservice: migration
ms.topic: concept-article
ms.collection:
  - sql-migration-content
ms.custom:
  - sqldbrb=1
  - sfi-image-nochange
---
# New DBA in the cloud: Managing Azure SQL Database after migration

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Migrating from a self-managed environment to a PaaS like Azure SQL Database can be complex. This article highlights the key capabilities of Azure SQL Database for single and pooled databases, helping you keep applications available, performant, secure, and resilient.

Core characteristics of Azure SQL Database include:

- Database monitoring with the Azure portal
- Business continuity and disaster recovery (BCDR)
- Security and compliance
- Intelligent database monitoring and maintenance
- Data movement

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Monitor databases using the Azure portal

For Azure Monitor metrics and alerts, including recommended alert rules, see [Monitor Azure SQL Database with metrics and alerts](monitoring-metrics-alerts.md). For more information about service tiers, see [DTU-based purchasing model overview](service-tiers-dtu.md) and [vCore-based purchasing model](service-tiers-vcore.md).

You can configure alerts on the performance metrics. Select the **Add alert** button in the **Metric** window. Follow the wizard to configure your alert. You can alert if the metrics exceed a certain threshold or if the metric falls below a certain threshold.

For example, if you expect the workload on your database to grow, you can choose to configure an email alert whenever your database reaches 80% on any of the performance metrics. You can use this as an early warning to figure out when you might have to switch to the next highest compute size.

The performance metrics can also help you determine if you're able to downgrade to a lower compute size. However, be aware of workloads that spike or fluctuate before making the decision to move to a lower compute size.

## Business continuity and disaster recovery (BCDR)

Business continuity and disaster recovery abilities enable you to continue your business if a disaster occurs. The disaster could be a database level event (for example, someone mistakenly drops a crucial table) or a data-center level event (regional catastrophe, for example a tsunami).

### How do I create and manage backups on SQL Database?

Azure SQL Database automatically backs up databases for you. The platform takes a full backup every week, differential backup every few hours, and a log backup every five minutes to ensure the disaster recovery is efficient and data loss is minimal. The first full backup happens as soon as you create a database. You can access these backups for a certain period called the *retention period*, which varies according to the service tier you choose. You can restore to any point in time within this retention period by using [Point in Time Recovery (PITR)](recovery-using-backups.md#point-in-time-restore).

In addition, the [Long-term retention backups](long-term-retention-overview.md) feature allows you to keep your backup files for up to 10 years and restore data from these backups at any point within that period. Azure keeps database backups in geo-replicated storage to provide resilience from regional catastrophe. You can also restore these backups in any Azure region at any point in time within the retention period. For more information, see [Business continuity in Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md).

### How do I ensure business continuity in the event of a datacenter-level disaster or regional catastrophe?

Store your database backups in geo-replicated storage to ensure that, during a regional disaster, you can restore the backup to another Azure region. This feature is called geo-restore. For more information and timing of geo-restores, see [Geo-restore for Azure SQL Database](recovery-using-backups.md#geo-restore).

For mission-critical databases, Azure SQL Database offers [active geo-replication](active-geo-replication-overview.md), which creates a geo-replicated secondary copy of your original database in another region. For example, if your database is initially hosted in Azure West US region and you want regional disaster resilience, create an active geo replica of the database in West US to East US. When calamity strikes on West US, you can fail over to the East US region.

In addition to active geo-replication, failover groups help you manage replication and failover of a group of databases. You can create a failover group that contains multiple databases in the same or different regions. You can then initiate a failover of all databases in the failover group to the secondary region. For more information, see [Failover groups overview & best practices (Azure SQL Database)](failover-group-sql-db.md).

To achieve resiliency for datacenter or availability zone failures, ensure zone redundancy is enabled for the database or elastic pool.

Actively monitor your application for a disaster and initiate a failover to the secondary. You can create up to four such active geo-replicas in different Azure regions. It gets even better. You can also access these secondary active geo-replicas for read-only access, which helps to reduce latency for a geo-distributed application scenario.

### What does disaster recovery look like with SQL Database?

You can configure your disaster recovery strategy in just a few steps in Azure SQL Database when you use [active geo-replication](active-geo-replication-overview.md) or [failover groups](failover-group-sql-db.md). You still have to monitor the application and its database for any regional disaster and fail over to the secondary region to restore business continuity.

For more information, see [Azure SQL Database Disaster Recovery 101](https://azure.microsoft.com/blog/azure-sql-databases-disaster-recovery-101/).

## Security and compliance

Azure SQL Database provides security at both the database level and the platform level. You can control and provide optimal security for your application by using the following features:

- Identity and [authentication](logins-create-manage.md) (SQL authentication and authentication with Microsoft Entra ID).
- Monitoring activity ([Auditing](auditing-overview.md) and [threat detection](threat-detection-configure.md)).
- Protecting actual data ([Transparent data encryption [TDE]](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql) and [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine)).
- Controlling access to sensitive and privileged data ([Row-level security](/sql/relational-databases/security/row-level-security) and [Dynamic data masking](/sql/relational-databases/security/dynamic-data-masking)).

[Microsoft Defender for Cloud](https://azure.microsoft.com/services/security-center/) offers centralized security management across workloads running in Azure, on-premises, and in other clouds. You can view whether essential SQL Database protection such as [Auditing](auditing-overview.md) and [Transparent data encryption [TDE]](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql) are configured on all resources, and create policies based on your own requirements.

<a id="what-user-authentication-methods-are-offered-in-sql-database"></a>

### What user authentication methods does SQL Database offer?

SQL Database offers two authentication methods:

- [Microsoft Entra authentication](authentication-aad-overview.md)
- [SQL authentication](/sql/relational-databases/security/choose-an-authentication-mode#connecting-through-sql-server-authentication)

Windows authentication isn't supported. Microsoft Entra ID is a centralized identity and access management service. Microsoft Entra ID provides single sign-on (SSO) access to the personnel in your organization. This means that the credentials are shared across Azure services for easier authentication.

Microsoft Entra ID supports [multifactor authentication](authentication-mfa-ssms-overview.md), and can easily be [integrated with Microsoft Entra Connect Sync](/entra/identity/hybrid/connect/how-to-connect-install-express). This integration also allows Azure SQL Database to offer multifactor authentication and guest user accounts within a Microsoft Entra domain. If you already use Active Directory on-premises, you can federate it with Microsoft Entra ID to extend your directory to Azure.

SQL authentication supports only username and password to authenticate users to any database on a given server.

| If you ... | ... use |
| --- | --- |
| Used AD on SQL Server on-premises | [Federate AD with Microsoft Entra ID](/entra/identity/hybrid/whatis-hybrid-identity), and use Microsoft Entra authentication. Federation allows you to use single sign-on. |
| Need to enforce multifactor authentication | Require multifactor authentication as a policy through [Conditional access](conditional-access-configure.md), and use [Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md). |
| Are signed in to Windows using your Microsoft Entra credentials from a federated domain | Use [Microsoft Entra authentication](authentication-aad-configure.md). |
| Are signed in to Windows using credentials from a domain not federated with Azure | Use [Microsoft Entra integrated authentication](authentication-aad-configure.md). |
| Have middle-tier services that need to connect to SQL Database | Use [Microsoft Entra integrated authentication](authentication-aad-configure.md). |
| Have a technical requirement to use SQL authentication | Use [SQL authentication](security-overview.md) |

### How do I limit or control connectivity access to my database?

To organize connectivity for your application, use the following techniques:

- Firewall rules
- Virtual network service endpoints
- Reserved IPs

#### Firewall

By default, the logical SQL server disallows all connections to databases, except (optionally) connections coming in from other Azure services. By using a firewall rule, you can open access to your server only to entities (for example, a developer machine) that you approve of by allowing that computer's IP address through the firewall. You can also specify a range of IPs that you want to allow access to the server. For example, you can add developer machine IP addresses in your organization at once by specifying a range in the Firewall settings page.

You can create firewall rules at the server level or at the database level. You can create server level IP firewall rules by using the Azure portal or by using SSMS. For more information about how to set a server-level and database-level firewall rule, see [Create IP firewall rules in SQL Database](secure-database-tutorial.md#create-firewall-rules).

#### Service endpoints

By default, your database is configured to **Allow Azure services and resources to access this server**, which means any virtual machine in Azure might attempt to connect to your database. These attempts still have to be authenticated. If you don't want your database to be accessible by any Azure IPs, you can disable **Allow Azure services and resources to access this server**. Additionally, you can configure [Virtual network service endpoints](vnet-service-endpoint-rule-overview.md).

Service endpoints allow you to expose your critical Azure resources only to your own private virtual network in Azure. This option eliminates public access to your resources. The traffic between your virtual network to Azure stays on the Azure backbone network. Without service endpoints, you get forced-tunneling packet routing. Your virtual network forces the internet traffic to your organization and the Azure Service traffic to go over the same route. By using service endpoints, packets flow straight from your virtual network to the service on Azure backbone network.

#### Reserved IPs

Another option is to provision [reserved IPs](/previous-versions/azure/virtual-network/virtual-networks-reserved-public-ip) for your VMs, and add those specific VM IP addresses in the server firewall settings. By assigning reserved IPs, you don't need to update the firewall rules with changing IP addresses.

### What port do I connect to SQL Database on?

Azure SQL Database communicates over port 1433. To connect from within a corporate network, you must add an outbound rule in the firewall settings of your organization. As a guideline, avoid exposing port 1433 outside the Azure boundary.

### How can I monitor and regulate activity on my server and database in SQL Database?

#### SQL Database Auditing

[Azure SQL Database Auditing](auditing-overview.md) records database events and writes them into an audit log file in your Azure Storage Account. Auditing is especially useful if you want to gain insight into potential security and policy violations, maintain regulatory compliance, and more. It provides preconfigured reports and a dashboard to give you an overview of events occurring on your database. You can define and configure the categories of events that need auditing.

You can apply these auditing policies at the database level or at the server level. For more information, see [Enable SQL Database Auditing](secure-database-tutorial.md#enable-security-features).

#### Threat detection

By using [threat detection](threat-detection-configure.md), you can act on security or policy violations discovered by auditing. You don't need to be a security expert to address potential threats or violations in your system. Threat detection also has some built-in capabilities like SQL injection detection, which is a common way of attacking a database application. Threat detection runs multiple sets of algorithms that detect potential vulnerabilities and SQL injection attacks, and anomalous database access patterns (such as access from an unusual location or by an unfamiliar principal).

Security officers or other designated administrators receive an email notification if a threat is detected on the database. Each notification provides details of the suspicious activity and recommendations on how to further investigate and mitigate the threat. To learn how to turn on Threat detection, see [Enable threat detection](secure-database-tutorial.md#enable-security-features).

### How do I protect my data in general on SQL Database?

Encryption provides a strong mechanism to protect and secure your sensitive data from intruders. Your encrypted data is of no use to the intruder without the decryption key. Thus, it adds an extra layer of protection on top of the existing layers of security built in SQL Database. There are two aspects to protecting your data in SQL Database:

- Your data at-rest in the data and log files
- Your data in-flight

In SQL Database, by default, your data at rest in the data and log files on the storage subsystem is completely and always encrypted via [Transparent data encryption [TDE]](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql). Your backups are also encrypted. With TDE, there are no changes required on your application side that is accessing this data. The encryption and decryption happen transparently; hence the name.

For protecting your sensitive data in-flight and at rest, SQL Database provides a feature called [Always Encrypted](/sql/relational-databases/security/encryption/always-encrypted-database-engine). Always Encrypted is a form of client-side encryption that encrypts sensitive columns in your database (so they're in ciphertext to database administrators and unauthorized users). The server receives the encrypted data to begin with.

The key for Always Encrypted is also stored on the client side, so only authorized clients can decrypt the sensitive columns. The server and data administrators can't see the sensitive data since the encryption keys are stored on the client. Always Encrypted encrypts sensitive columns in the table end to end, from unauthorized clients to the physical disk.

Always Encrypted supports equality comparisons, so DBAs can continue to query encrypted columns as part of their SQL commands. Always Encrypted can be used with a variety of key store options, such as [Azure Key Vault](always-encrypted-azure-key-vault-configure.md), Windows certificate store, and local hardware security modules.

| Characteristics | Always Encrypted | Transparent data encryption |
| --- | --- | --- |
| **Encryption span** | End-to-end | At-rest data |
| **Server can access sensitive data** | No | Yes, since encryption is for the data at rest |
| **Allowed T-SQL operations** | Equality comparison | All T-SQL surface area is available |
| **App changes required to use the feature** | Minimal | Minimal |
| **Encryption granularity** | Column level | Database level |

### How can I limit access to sensitive data in my database?

Every application has sensitive data in the database that you need to protect from being visible to everyone. Certain personnel within the organization need to view this data, but others shouldn't. In such cases, you need to either mask your sensitive data or not expose it at all. SQL Database offers two approaches to prevent unauthorized users from viewing sensitive data:

- [Dynamic data masking](dynamic-data-masking-overview.md) is a data masking feature that you can use to limit sensitive data exposure by masking it to non-privileged users. You define a masking rule that creates a masking pattern. For example, you can only show the last four digits of a national ID number `XXX-XX-0000` and mask the rest with the `X` character. With dynamic data masking, you identify which users are excluded from the masking rule and can see unmasked data. The masking happens on-the-fly and various masking functions are available for different data categories.

- [Row-level security](/sql/relational-databases/security/row-level-security) enables you to control access at the row level. This feature hides certain rows in a database table based on the user executing the query (group membership or execution context). The access restriction is done on the database tier instead of in an application tier, which simplifies your app logic. You start by creating a predicate that filters out rows that aren't exposed. Then, you create the security policy that defines who has access to these rows. Finally, the end user runs their query and, depending on the user's privilege, they either view those restricted rows or can't see them at all.

### How do I manage encryption keys in the cloud?

Both Always Encrypted (client-side encryption) and transparent data encryption (encryption at rest) offer [customer-managed key](transparent-data-encryption-byok-overview.md) options. Rotate encryption keys regularly. Choose a rotation frequency that aligns with your internal organization regulations and compliance requirements.

#### Transparent Data Encryption (TDE)

TDE uses a two-key hierarchy. Each user database's data is encrypted by a symmetric AES-256 database-unique database encryption key (DEK), which is encrypted by a server-unique asymmetric RSA 2048 master key. The master key can be managed either:

- Automatically by Azure SQL Database
- Or by you using [Azure Key Vault](always-encrypted-azure-key-vault-configure.md) as the key store

By default, Azure SQL Database manages the TDE master key. If your organization wants control over the master key, use [Azure Key Vault](always-encrypted-azure-key-vault-configure.md) as the key store. By using Azure Key Vault, your organization assumes control over key provisioning, rotation, and permission controls. [Rotation or switching the type of a TDE master key](/sql/relational-databases/security/encryption/transparent-data-encryption-byok-azure-sql-key-rotation) is fast, as it only re-encrypts the DEK. For organizations with separation of roles between security and data management, a security admin can provision the key material for the TDE master key in Azure Key Vault and provide an Azure Key Vault key identifier to the database administrator to use for encryption at rest on a server. The Key Vault is designed such that Microsoft doesn't see or extract any encryption keys. You also get a centralized management of keys for your organization.

#### Always Encrypted

Always Encrypted also uses a [two-key hierarchy](/sql/relational-databases/security/encryption/overview-of-key-management-for-always-encrypted). A column of sensitive data is encrypted by an AES 256-column encryption key (CEK), which is encrypted by a column master key (CMK). The client drivers provided for Always Encrypted have no limitations on the length of CMKs. The encrypted value of the CEK is stored on the database, and the CMK is stored in a trusted key store, such as Windows Certificate Store, Azure Key Vault, or a hardware security module.

- Rotate both the [CEK and CMK](/sql/relational-databases/security/encryption/rotate-always-encrypted-keys-using-powershell).

- CEK rotation is a size of data operation and can be time-intensive depending on the size of the tables containing the encrypted columns. Plan CEK rotations accordingly.

- CMK rotation doesn't interfere with database performance, and can be done with separated roles.

The following diagram shows the key store options for the column master keys in Always Encrypted:

:::image type="content" source="media/manage-data-after-migrating-to-database/always-encrypted.png" alt-text="Diagram of Always encrypted CMK store providers." lightbox="media/manage-data-after-migrating-to-database/always-encrypted.png":::

### How can I optimize and secure the traffic between my organization and SQL Database?

The network traffic between your organization and SQL Database generally routes over the public network. However, you can optimize this path and make it more secure by using [Azure ExpressRoute](/azure/expressroute/expressroute-introduction). ExpressRoute extends your corporate network into the Azure platform over a private connection, bypassing the public Internet. You also get higher security, reliability, and routing optimization that translates to lower network latencies and faster speeds than you would normally experience going over the public internet. If you're planning to transfer a significant chunk of data between your organization and Azure, using ExpressRoute can yield cost benefits. You can choose from three different connectivity models for the connection from your organization to Azure:

- [Cloud Exchange Colocation](/azure/expressroute/expressroute-connectivity-models#CloudExchange)
- [Any-to-any](/azure/expressroute/expressroute-connectivity-models#IPVPN)
- [Point-to-Point](/azure/expressroute/expressroute-connectivity-models#Ethernet)

ExpressRoute also allows you to burst up to 2x the bandwidth limit you purchase for no extra charge. You can also configure cross region connectivity by using ExpressRoute. For a list of ExpressRoute connectivity providers, see [ExpressRoute Partners and Peering Locations](/azure/expressroute/expressroute-locations). The following articles describe ExpressRoute in more detail:

- [Prerequisites](/azure/expressroute/expressroute-prerequisites)
- [Workflows](/azure/expressroute/expressroute-workflows)

### Is SQL Database compliant with any regulatory requirements, and how does that help with my own organization's compliance?

Azure SQL Database is compliant with a range of regulatory demands. To view the latest set of compliances that SQL Database meets, visit the [Microsoft Trust Center](https://www.microsoft.com/trust-center/product-overview) and review the compliances that are important to your organization to see if SQL Database is included under the compliant Azure services. Although SQL Database is certified as a compliant service, it aids in the compliance of your organization's service but doesn't automatically guarantee it.

## Intelligent database monitoring and maintenance after migration

After you migrate your database to SQL Database, monitor your database (for example, check how the resource utilization is or DBCC checks) and perform regular maintenance (for example, rebuild or reorganize indexes, statistics, and more). SQL Database uses the historical trends and recorded metrics and statistics to proactively help you monitor and maintain your database, so that your application runs optimally always. In some cases, Azure SQL Database can automatically perform maintenance tasks depending on your configuration setup. There are three facets to monitoring your database in SQL Database:

- Performance monitoring and optimization
- Security optimization
- Cost optimization

### Performance monitoring and optimization

By using Query Performance Insights, you can get tailored recommendations for your database workload so that your applications can keep running at an optimal level. You can also set it up so that these recommendations get applied automatically and you don't have to bother performing maintenance tasks. By using SQL Database Advisor, you can automatically implement index recommendations based on your workload. This feature is called Auto-Tuning. The recommendations evolve as your application workload changes to provide you with the most relevant suggestions. You also get the option to manually review these recommendations and apply them at your discretion.

### Security optimization

SQL Database provides actionable security recommendations to help you secure your data. It also offers threat detection for identifying and investigating suspicious database activities that can pose a potential threat to the database. [Vulnerability assessment](/azure/defender-for-cloud/sql-azure-vulnerability-assessment-overview) is a database scanning and reporting service that you can use to monitor the security state of your databases at scale and identify security risks and drift from a security baseline that you define. After every scan, it provides a customized list of actionable steps and remediation scripts, along with an assessment report that can help you meet compliance requirements.

By using Microsoft Defender for Cloud, you can identify security recommendations across the board and quickly apply them.

### Cost optimization

The Azure SQL platform analyzes the utilization history across the databases in a server to evaluate and recommend cost-optimization options. This analysis usually takes a few weeks of activity to analyze and build up actionable recommendations.

You might receive banner notifications in your Azure SQL server of cost recommendations. For more information, see [Elastic pools help you manage and scale multiple databases in Azure SQL Database](elastic-pool-overview.md) and [Plan and manage costs for Azure SQL Database](cost-management.md).

### How do I monitor the performance and resource utilization in SQL Database?

You can monitor performance and resource utilization in SQL Database by using the following methods:

#### Database watcher

Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Dashboards in the Azure portal provide a single-pane-of-glass view of your Azure SQL estate and a detailed view of each monitored resource. Data is collected into a central data store in your Azure subscription. You can query, analyze, export, visualize collected data, and integrate it with downstream systems.

For more information about database watcher, see the following articles:

- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Quickstart: Create a watcher to monitor Azure SQL (preview)](../database-watcher-quickstart.md)
- [Create and configure a watcher (preview)](../database-watcher-manage.md)
- [Database watcher data collection and datasets (preview)](../database-watcher-data.md)
- [Analyze database watcher monitoring data (preview)](../database-watcher-analyze.md)
- [Database watcher FAQ](../database-watcher-faq.yml)

#### Azure portal

The Azure portal shows a database's utilization when you select the database and select the chart in the **Overview** pane. You can modify the chart to show multiple metrics, including CPU percentage, DTU percentage, Data IO percentage, Sessions percentage, and Database size percentage.

:::image type="content" source="media/manage-data-after-migrating-to-database/chart.png" alt-text="Screenshot from the Azure portal of a Monitoring chart of database DTU." lightbox="media/manage-data-after-migrating-to-database/chart.png":::

From this chart, you can also configure alerts by resource. These alerts allow you to respond to resource conditions with an email, write to an HTTPS/HTTP endpoint, or perform an action. For more information, see [Create alerts for Azure SQL Database using the Azure portal](alerts-create.md).

#### Dynamic management views

You can query the [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database) dynamic management view to return resource consumption statistics history from the last hour and the [sys.resource_stats](/sql/relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database) system catalog view to return history for the last 14 days.

#### Query performance insight

[Query performance insight](query-performance-insight-use.md) allows you to see a history of the top resource-consuming queries and long-running queries for a specific database. You can quickly identify `TOP` queries by resource utilization, duration, and frequency of execution. You can track queries and detect regression. This feature requires [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) to be enabled and active for the database.

:::image type="content" source="media/manage-data-after-migrating-to-database/query-performance-insight.png" alt-text="Screenshot from the Azure portal of a Query performance insight." lightbox="media/manage-data-after-migrating-to-database/query-performance-insight.png":::

### I notice performance issues: How does my SQL Database troubleshooting methodology differ from SQL Server?

Most troubleshooting techniques you use for diagnosing query and database performance problems are the same as on-premises SQL Server. Azure SQL Database uses the same [SQL Database Engine](/sql/database-engine/sql-database-engine). However, features in Azure help you troubleshoot and diagnose performance problems even more easily. It can also perform some of these corrective actions on your behalf and, in some cases, proactively fix them automatically.

Your approach to troubleshooting performance problems can significantly benefit from using intelligent features such as [Query Performance Insight](query-performance-insight-use.md) (QPI) and [Database Advisor](database-advisor-implement-performance-recommendations.md). The difference in methodology is that you no longer need to do the manual work of grinding out the essential details that might help you troubleshoot the problem at hand. The platform does the hard work for you. One example of that work is QPI. By using QPI, you can drill all the way down to the query level and look at the historical trends to figure out when exactly the query regressed. The Database Advisor gives you recommendations on things that might help you improve your overall performance in general, such as missing indexes, dropping indexes, parameterizing your queries, and more.

With performance troubleshooting, it's important to identify whether it's just the application or the database affecting your application performance. Often the performance problem lies in the application layer. It could be the architecture or the data access pattern. For example, consider you have a chatty application that's sensitive to network latency. In this case, your application suffers because there are many short requests going back and forth ("chatty") between the application and the server. On a congested network, these roundtrips add up fast. To improve the performance in this case, you can use [Batch Queries](performance-guidance.md#batch-queries), which help to reduce roundtrip latency and improve your application's performance.

Additionally, if you notice a degradation in the overall performance of your database, you can monitor the [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database) and [sys.resource_stats](/sql/relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database) dynamic management views to understand CPU, IO, and memory consumption. Your performance might be affected if your database is starved of resources. You might need to change the compute size and/or service tier based on the growing and shrinking workload demands.

For a comprehensive set of recommendations for tuning performance problems, see [Tune your database](performance-guidance.md#tune-your-database).

<a id="how-do-i-ensure-i-am-using-the-appropriate-service-tier-and-compute-size"></a>

### How do I ensure I'm using the appropriate service tier and compute size?

SQL Database offers two different purchasing models: the older DTU model and the more adaptable vCore purchasing model. For more information, see [Compare vCore and DTU-based purchasing models of Azure SQL Database](purchasing-models.md#purchasing-models).

You can monitor your query and database resource consumption in either purchasing model. For more information, see [Monitor and performance tuning](monitor-tune-overview.md). If you find that your databases are consistently running at high utilization, consider scaling up to a higher compute size. Similarly, if you don't use the resources as much during peak hours, consider scaling down from the current compute size. You can consider using [Azure Automation](automation-manage.md) to scale your SQL databases on a schedule.

If you have a SaaS app pattern or a database consolidation scenario, consider using an elastic pool for cost optimization. An elastic pool is a great way to achieve database consolidation and cost optimization. For more information about managing multiple databases by using an elastic pool, see [Manage pools and databases](elastic-pool-manage.md#azure-portal).

### How often do I need to run database integrity checks for my database?

SQL Database can handle certain classes of data corruption automatically and without any data loss. The service uses these built-in techniques when the need arises. If there are problems, SQL Database proactively addresses them. As an extra layer of protection, you might choose to test backup restoration and run integrity checks. For more information, see [Data integrity in Azure SQL Database](data-integrity.md).

[Automatic page repair](/sql/sql-server/failover-clusters/automatic-page-repair-availability-groups-database-mirroring) is used for fixing pages that are corrupt or have data integrity problems. The `CHECKSUM` setting always verifies the integrity of the database pages. For more information, see [Data Integrity in SQL Database](data-integrity.md).

## Data movement after migration

<a id="how-do-i-export-and-import-data-as-bacpac-files-from-sql-database-using-the-azure-portal"></a>

### How do I export and import data as BACPAC files from SQL Database by using the Azure portal?

- **Export**: You can export your database in Azure SQL Database as a BACPAC file from the Azure portal:

  :::image type="content" source="media/manage-data-after-migrating-to-database/database-export.png" alt-text="Screenshot from the Azure portal of the Export database button on an Azure SQL database." lightbox="media/manage-data-after-migrating-to-database/database-export.png":::

- **Import**: You can also import data as a BACPAC file into your database in Azure SQL Database by using the Azure portal:

  :::image type="content" source="media/manage-data-after-migrating-to-database/database-import.png" alt-text="Screenshot from the Azure portal of the Import database button on an Azure SQL server." lightbox="media/manage-data-after-migrating-to-database/database-import.png":::

### How do I synchronize data between SQL Database and SQL Server?

For more information on data sync alternatives, see [Migrate to alternative solutions](sql-data-sync-retirement-migration.md).

## Related content

- [Monitor and performance tuning in Azure SQL Database and Azure SQL Managed Instance](monitor-tune-overview.md)