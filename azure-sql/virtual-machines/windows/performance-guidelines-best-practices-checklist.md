---
title: "Checklist: Best practices & guidelines"
description: Provides a quick checklist to review your best practices and guidelines to optimize the performance of your SQL Server on Azure Virtual Machine (VM).
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 03/22/2022
ms.service: virtual-machines-sql
ms.subservice: performance
ms.topic: conceptual
ms.custom: contperf-fy21q3, ignite-2022
tags: azure-service-management
---
# Checklist: Best practices for SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article provides a quick checklist as a series of best practices and guidelines to optimize performance of your SQL Server on Azure Virtual Machines (VMs). 

For comprehensive details, see the other articles in this series: [VM size](performance-guidelines-best-practices-vm-size.md), [Storage](performance-guidelines-best-practices-storage.md), [Security](security-considerations-best-practices.md), [HADR configuration](hadr-cluster-best-practices.md), [Collect baseline](performance-guidelines-best-practices-collect-baseline.md). 

Enable [SQL Assessment for SQL Server on Azure VMs](sql-assessment-for-sql-vm.md) and your SQL Server will be evaluated against known best practices with results  on the [SQL VM management page](manage-sql-vm-portal.md) of the Azure portal.

For videos about the latest features to optimize SQL Server VM performance and automate management, review the following Data Exposed videos:

- [Caching and Storage Capping (Ep. 1)](/shows/data-exposed/azure-sql-vm-caching-and-storage-capping-ep-1-data-exposed)
- [Automate Management with the SQL Server IaaS Agent extension (Ep. 2)](/shows/data-exposed/azure-sql-vm-automate-management-with-the-sql-server-iaas-agent-extension-ep-2)
- [Use Azure Monitor Metrics to Track VM Cache Health (Ep. 3)](/shows/data-exposed/azure-sql-vm-use-azure-monitor-metrics-to-track-vm-cache-health-ep-3)
- [Get the best price-performance for your SQL Server workloads on Azure VM](/shows/data-exposed/azure-sql-vm-get-the-best-price-performance-for-your-sql-server-workloads-on-azure-vm)
- [Using PerfInsights to Evaluate Resource Health and Troubleshoot (Ep. 5)](/shows/data-exposed/azure-sql-vm-using-perfinsights-to-evaluate-resource-health-and-troubleshoot-ep-5)
- [Best Price-Performance with Ebdsv5 Series (Ep.6)](/shows/data-exposed/azure-sql-vm-best-price-performance-with-ebdsv5-series)
- [Optimally Configure SQL Server on Azure Virtual Machines with SQL Assessment (Ep. 7)](/shows/data-exposed/optimally-configure-sql-server-on-azure-virtual-machines-with-sql-assessment)
- [New and Improved SQL Server on Azure VM deployment and management experience (Ep.8)](/shows/data-exposed/new-and-improved-sql-on-azure-vm-deployment-and-management-experience)

## Overview

While running SQL Server on Azure Virtual Machines, continue using the same database performance tuning options that are applicable to SQL Server in on-premises server environments. However, the performance of a relational database in a public cloud depends on many factors, such as the size of a virtual machine, and the configuration of the data disks.

There's typically a trade-off between optimizing for costs and optimizing for performance. This performance best practices series is focused on getting the *best* performance for SQL Server on Azure Virtual Machines. If your workload is less demanding, you might not require every recommended optimization. Consider your performance needs, costs, and workload patterns as you evaluate these recommendations.

## VM Size

The following is a quick checklist of VM size best practices for running your SQL Server on Azure VM:

- The new [Ebdsv5-series](/azure/virtual-machines/ebdsv5-ebsv5-series#ebdsv5-series) provides the highest I/O throughput-to-vCore ratio in Azure along with a memory-to-vCore ratio of 8. This series offers the best price-performance for SQL Server workloads on Azure VMs. Consider this series first for most SQL Server workloads.
- Use VM sizes with 4 or more vCPUs like the [E4ds_v5](/azure/virtual-machines/edv5-edsv5-series#edsv5-series) or higher.
- Use [memory optimized](/azure/virtual-machines/sizes-memory) virtual machine sizes for the best performance of SQL Server workloads. 
- The [Edsv5](/azure/virtual-machines/edv5-edsv5-series#edsv5-series) series, the [M-](/azure/virtual-machines/m-series), and the [Mv2-](/azure/virtual-machines/mv2-series) series offer the optimal memory-to-vCore ratio required for OLTP workloads. 
- The M series VMs offer the highest memory-to-vCore ratio in Azure. Consider these VMs for mission critical and data warehouse workloads.
- Leverage Azure Marketplace images to deploy your SQL Server Virtual Machines as the SQL Server settings and storage options are configured for optimal performance. 
- Collect the target workload's performance characteristics and use them to determine the appropriate VM size for your business.
- Use the [Data Migration Assistant](https://www.microsoft.com/download/details.aspx?id=53595) [SKU recommendation](/sql/dma/dma-sku-recommend-sql-db) tool to find the right VM size for your existing SQL Server workload.

To learn more, see the comprehensive [VM size best practices](performance-guidelines-best-practices-vm-size.md). 

## Storage

The following is a quick checklist of storage configuration best practices for running your SQL Server on Azure VM: 

- Monitor the application and [determine storage bandwidth and latency requirements](/azure/virtual-machines/premium-storage-performance#counters-to-measure-application-performance-requirements) for SQL Server data, log, and tempdb files before choosing the disk type. 
- To optimize storage performance, plan for highest uncached IOPS available and use data caching as a performance feature for data reads while avoiding [virtual machine and disks capping/throttling](/azure/virtual-machines/premium-storage-performance#throttling).
- Place data, log, and tempdb files on separate drives.
    - For the data drive, use [premium P30 and P40 or smaller disks](/azure/virtual-machines/disks-types#premium-ssds) to ensure the availability of cache support
    - For the log drive plan for capacity and test performance versus cost while evaluating the [premium P30 - P80 disks](/azure/virtual-machines/disks-types#premium-ssds).
      - If submillisecond storage latency is required, use [Azure ultra disks](/azure/virtual-machines/disks-types#ultra-disks) for the transaction log. 
      - For M-series virtual machine deployments consider [Write Accelerator](/azure/virtual-machines/how-to-enable-write-accelerator) over using Azure ultra disks.
    - Place [tempdb](/sql/relational-databases/databases/tempdb-database) on the local ephemeral SSD (default `D:\`) drive for most SQL Server workloads that aren't part of Failover Cluster Instance (FCI) after choosing the optimal VM size. 
      - If the capacity of the local drive isn't enough for tempdb, consider sizing up the VM. See [Data file caching policies](performance-guidelines-best-practices-storage.md#data-file-caching-policies) for more information.
    - For FCI place tempdb on the shared storage. 
      - If the FCI workload is heavily dependent on tempdb disk performance, then as an advanced configuration place tempdb on the local ephemeral SSD (default `D:\`) drive, which isn't part of FCI storage. This configuration will need custom monitoring and action to ensure the local ephemeral SSD (default `D:\`) drive is available all the time as any failures of this drive won't trigger action from FCI.       
- Stripe multiple Azure data disks using [Storage Spaces](/windows-server/storage/storage-spaces/overview) to increase I/O bandwidth up to the target virtual machine's IOPS and throughput limits.
- Set [host caching](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits) to read-only for data file disks.
- Set [host caching](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits) to none for log file disks.
    - Don't enable read/write caching on disks that contain SQL Server data or log files. 
    - Always stop the SQL Server service before changing the cache settings of your disk.
- For development and test workloads consider using standard storage. It isn't recommended to use Standard HDD/SDD for production workloads.
- [Credit-based Disk Bursting](/azure/virtual-machines/disk-bursting#credit-based-bursting) (P1-P20) should only be considered for smaller dev/test workloads and departmental systems.
- Provision the storage account in the same region as the SQL Server VM. 
- Disable Azure geo-redundant storage (geo-replication) and use LRS (local redundant storage) on the storage account.
- Format your data disk to use 64-KB allocation unit size for all data files placed on a drive other than the temporary `D:\` drive (which has a default of 4 KB). SQL Server VMs deployed through Azure Marketplace come with data disks formatted with allocation unit size and interleave for the storage pool set to 64 KB. 


To learn more, see the comprehensive [Storage best practices](performance-guidelines-best-practices-storage.md). 

## SQL Server features

The following is a quick checklist of best practices for SQL Server configuration settings when running your SQL Server instances in an Azure virtual machine in production: 

- Enable [database page compression](/sql/relational-databases/data-compression/data-compression) where appropriate.
- Enable [backup compression](/sql/relational-databases/backup-restore/backup-compression-sql-server).
- Enable [instant file initialization](/sql/relational-databases/databases/database-instant-file-initialization) for data files.
- Limit [autogrowth](/troubleshoot/sql/admin/considerations-autogrow-autoshrink#considerations-for-autogrow) of the database.
- Disable [autoshrink](/troubleshoot/sql/admin/considerations-autogrow-autoshrink#considerations-for-auto_shrink) of the database.
- Disable autoclose of the database.
- Move all databases to data disks, including [system databases](/sql/relational-databases/databases/move-system-databases).
- Move SQL Server error log and trace file directories to data disks.
- Configure default backup and database file locations.
- Set max [SQL Server memory limit](/sql/database-engine/configure-windows/server-memory-server-configuration-options#use-) to leave enough memory for the Operating System. ([Leverage Memory\Available Bytes](/sql/relational-databases/performance-monitor/monitor-memory-usage) to monitor the operating system memory health).
- Enable [lock pages in memory](/sql/database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows).
- Enable [optimize for adhoc workloads](/sql/database-engine/configure-windows/optimize-for-ad-hoc-workloads-server-configuration-option) for OLTP heavy environments.
- Evaluate and apply the [latest cumulative updates](/sql/database-engine/install-windows/latest-updates-for-microsoft-sql-server) for the installed versions of SQL Server.
- Enable [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) on all production SQL Server databases [following best practices](/sql/relational-databases/performance/best-practice-with-the-query-store).
- Enable [automatic tuning](/sql/relational-databases/automatic-tuning/automatic-tuning) on mission critical application databases.
- Ensure that all [tempdb best practices](/sql/relational-databases/databases/tempdb-database#optimizing-tempdb-performance-in-sql-server) are followed.
- [Use the recommended number of files](/troubleshoot/sql/performance/recommendations-reduce-allocation-contention#resolution), using multiple tempdb data files starting with one file per core, up to eight files.
- Schedule SQL Server Agent jobs to run [DBCC CHECKDB](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql#a-checking-both-the-current-and-another-database), [index reorganize](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes#reorganize-an-index), [index rebuild](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes#rebuild-an-index), and [update statistics](/sql/t-sql/statements/update-statistics-transact-sql#examples) jobs.
- Monitor and manage the health and size of the SQL Server [transaction log file](/sql/relational-databases/logs/manage-the-size-of-the-transaction-log-file#Recommendations).
- Take advantage of any new [SQL Server features](/sql/sql-server/what-s-new-in-sql-server-ver15) available for the version being used.
- Be aware of the differences in [supported features](/sql/sql-server/editions-and-components-of-sql-server-version-15) between the editions you're considering deploying.

## Azure features

The following is a quick checklist of best practices for Azure-specific guidance when running your SQL Server on Azure VM:

- Register with [the SQL IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md) to unlock a number of [feature benefits](sql-server-iaas-agent-extension-automate-management.md#feature-benefits).
- Leverage the best [backup and restore strategy](backup-restore.md#decision-matrix) for your SQL Server workload.
- Ensure [Accelerated Networking is enabled](/azure/virtual-network/create-vm-accelerated-networking-cli#portal-creation) on the virtual machine.
- Leverage [Microsoft Defender for Cloud](/azure/security-center/index) to improve the overall security posture of your virtual machine deployment.
- Leverage [Microsoft Defender for Cloud](/azure/security-center/azure-defender), integrated with [Microsoft Defender for Cloud](https://azure.microsoft.com/services/security-center/), for specific [SQL Server VM coverage](/azure/security-center/defender-for-sql-introduction) including vulnerability assessments, and just-in-time access, which reduces the attack service while allowing legitimate users to access virtual machines when necessary. To learn more, see [vulnerability assessments](/azure/security-center/defender-for-sql-on-machines-vulnerability-assessment), [enable vulnerability assessments for SQL Server VMs](/azure/security-center/defender-for-sql-on-machines-vulnerability-assessment) and [just-in-time access](/azure/security-center/just-in-time-explained). 
- Leverage [Azure Advisor](/azure/advisor/advisor-overview) to address [performance](/azure/advisor/advisor-performance-recommendations), [cost](/azure/advisor/advisor-cost-recommendations), [reliability](/azure/advisor/advisor-high-availability-recommendations), [operational excellence](/azure/advisor/advisor-operational-excellence-recommendations), and [security recommendations](/azure/advisor/advisor-security-recommendations).
- Leverage [Azure Monitor](/azure/azure-monitor/vm/monitor-virtual-machine) to collect, analyze, and act on telemetry data from your SQL Server environment. This includes identifying infrastructure issues with [VM insights](/azure/azure-monitor/vm/vminsights-overview) and monitoring data with [Log Analytics](/azure/azure-monitor/logs/log-query-overview) for deeper diagnostics.
- Enable [Autoshutdown](/azure/automation/automation-solution-vm-management) for development and test environments. 
- Implement a high availability and disaster recovery (HADR) solution that meets  your business continuity SLAs, see the [HADR options](business-continuity-high-availability-disaster-recovery-hadr-overview.md#deployment-architectures) options available for SQL Server on Azure VMs. 
- Use the Azure portal (support + troubleshooting) to evaluate [resource health](/azure/service-health/resource-health-overview) and history; submit new support requests when needed.

## HADR configuration

High availability and disaster recovery (HADR) features, such as the [Always On availability group](availability-group-overview.md) and the [failover cluster instance](failover-cluster-instance-overview.md) rely on underlying [Windows Server Failover Cluster](hadr-windows-server-failover-cluster-overview.md) technology. Review the best practices for modifying your HADR settings to better support the cloud environment. 

For your Windows cluster, consider these best practices: 

* Deploy your SQL Server VMs to multiple subnets whenever possible to avoid the dependency on an Azure Load Balancer or a distributed network name (DNN) to route traffic to your HADR solution. 
* Change the cluster to less aggressive parameters to avoid unexpected outages from transient network failures or Azure platform maintenance. To learn more, see [heartbeat and threshold settings](hadr-cluster-best-practices.md#heartbeat-and-threshold). For Windows Server 2012 and later, use the following recommended values: 
   - **SameSubnetDelay**:  1 second
   - **SameSubnetThreshold**: 40 heartbeats
   - **CrossSubnetDelay**: 1 second
   - **CrossSubnetThreshold**:  40 heartbeats
* Place your VMs in an availability set or different availability zones.  To learn more, see [VM availability settings](hadr-cluster-best-practices.md#vm-availability-settings). 
* Use a single NIC per cluster node and a single subnet. 
* Configure cluster [quorum voting](hadr-cluster-best-practices.md#quorum-voting) to use 3 or more odd number of votes. Don't assign votes to DR regions. 
* Carefully monitor [resource limits](hadr-cluster-best-practices.md#resource-limits) to avoid unexpected restarts or failovers due to resource constraints.
   - Ensure your OS, drivers, and SQL Server are at the latest builds. 
   - Optimize performance for SQL Server on Azure VMs. Review the other sections in this article to learn more. 
   - Reduce or spread out workload to avoid resource limits. 
   - Move to a VM or disk that his higher limits to avoid constraints. 

For your SQL Server availability group or failover cluster instance, consider these best practices: 

* If you're experiencing frequent unexpected failures, follow the performance best practices outlined in the rest of this article. 
* If optimizing SQL Server VM performance doesn't resolve your unexpected failovers, consider [relaxing the monitoring](hadr-cluster-best-practices.md#relaxed-monitoring) for the availability group or failover cluster instance. However, doing so may not address the underlying source of the issue and could mask symptoms by reducing the likelihood of failure. You may still need to investigate and address the underlying root cause. For Windows Server 2012 or higher, use the following recommended values: 
   - **Lease timeout**: Use this equation to calculate the maximum lease time-out value:   
    `Lease timeout < (2 * SameSubnetThreshold * SameSubnetDelay)`.    
    Start with 40 seconds. If you're using the relaxed `SameSubnetThreshold` and `SameSubnetDelay` values recommended previously, don't exceed 80 seconds for the lease timeout value. 
   - **Max failures in a specified period**: You can set this value to 6.
   - **Healthcheck timeout**: You can set this value to 60000 initially, adjust as necessary. 
* When using the virtual network name (VNN) and Azure Load Balancer to connect to your HADR solution, specify `MultiSubnetFailover = true` in the connection string, even if your cluster only spans one subnet. 
   - If the client doesn't support `MultiSubnetFailover = True` you may need to set `RegisterAllProvidersIP = 0` and `HostRecordTTL = 300` to cache client credentials for shorter durations. However, doing so may cause additional queries to the DNS server. 
- To connect to your HADR solution using the distributed network name (DNN), consider the following:
   - You must use a client driver that supports `MultiSubnetFailover = True`, and this parameter must be in the connection string. 
   - Use a unique DNN port in the connection string when connecting to the DNN listener for an availability group. 
- Use a database mirroring connection string for a basic availability group to bypass the need for a load balancer or DNN. 
- Validate the sector size of your VHDs before deploying your high availability solution to avoid having misaligned I/Os. See [KB3009974](https://support.microsoft.com/topic/kb3009974-fix-slow-synchronization-when-disks-have-different-sector-sizes-for-primary-and-secondary-replica-log-files-in-sql-server-ag-and-logshipping-environments-ed181bf3-ce80-b6d0-f268-34135711043c) to learn more. 
- If the SQL Server database engine, Always On availability group listener, or failover cluster instance health probe are configured to use a port between 49,152 and 65,536 (the [default dynamic port range for TCP/IP](/windows/client-management/troubleshoot-tcpip-port-exhaust#default-dynamic-port-range-for-tcpip)), add an exclusion for each port. Doing so will prevent other systems from being dynamically assigned the same port. The following example creates an exclusion for port 59999:   
`netsh int ipv4 add excludedportrange tcp startport=59999 numberofports=1 store=persistent`

To learn more, see the comprehensive [HADR best practices](hadr-cluster-best-practices.md). 

## Security

The checklist in this section covers the [security best practices](security-considerations-best-practices.md) for SQL Server on Azure VMs. 

SQL Server features and capabilities provide a method of security at the data level and is how you achieve [defense-in-depth](https://azure.microsoft.com/resources/videos/defense-in-depth-security-in-azure/) at the infrastructure level for cloud-based and hybrid solutions. In addition, with Azure security measures, it is possible to encrypt your sensitive data, protect virtual machines from viruses and malware, secure network traffic, identify and detect threats, meet compliance requirements, and provides a single method for administration and reporting for any security need in the hybrid cloud.

- Use [Azure Security Center](/azure/defender-for-cloud/defender-for-cloud-introduction) to evaluate and take action to improve the security posture of your data environment. Capabilities such as [Azure Advanced Threat Protection (ATP)](../../database/threat-detection-overview.md) can be leveraged across your hybrid workloads to improve security evaluation and give the ability to react to risks. Registering your SQL Server VM with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) surfaces Azure Security Center assessments within the [SQL virtual machine resource](manage-sql-vm-portal.md) of the Azure portal. 
- Leverage [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-introduction) to discover and mitigate potential database vulnerabilities, as well as detect anomalous activities that could indicate a threat to your SQL Server instance and database layer.
- [Vulnerability Assessment](../../database/sql-vulnerability-assessment.md) is a part of [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-introduction) that can discover and help remediate potential risks to your SQL Server environment. It provides visibility into your security state, and includes actionable steps to resolve security issues.
- Use [Azure confidential VMs](security-considerations-best-practices.md#confidential-vms) to reinforce protection of your data in-use, and data-at-rest against host operator access. Azure confidential VMs allow you to confidently store your sensitive data in the cloud and meet strict compliance requirements. 
- If you're on SQL Server 2022, consider using [Azure Active Directory authentication](security-considerations-best-practices.md#azure-ad-authentication-preview) to connect to your instance of SQL Server. 
- [Azure Advisor](/azure/advisor/advisor-security-recommendations) analyzes your resource configuration and usage telemetry and then recommends solutions that can help you improve the cost effectiveness, performance, high availability, and security of your Azure resources.. Leverage Azure Advisor at the virtual machine, resource group, or subscription level to help identify and apply best practices to optimize your Azure deployments. 
- Use [Azure Disk Encryption](/azure/virtual-machines/windows/disk-encryption-windows) when your compliance and security needs require you to encrypt the data end-to-end using your encryption keys, including encryption of the ephemeral (locally attached temporary) disk.
- [Managed Disks are encrypted](/azure/virtual-machines/disk-encryption) at rest by default using Azure Storage Service Encryption, where the encryption keys are Microsoft-managed keys stored in Azure.
- For a comparison of the managed disk encryption options review the [managed disk encryption comparison chart](/azure/virtual-machines/disk-encryption-overview#comparison)
- Management ports should be closed on your virtual machines - Open remote management ports expose your VM to a high level of risk from internet-based attacks. These attacks attempt to brute force credentials to gain admin access to the machine.
- Turn on [Just-in-time (JIT) access](/azure/defender-for-cloud/just-in-time-access-usage) for Azure virtual machines
- Use [Azure Bastion](/azure/bastion/bastion-overview) over Remote Desktop Protocol (RDP).
- Lock down ports and only allow the necessary application traffic using [Azure Firewall](/azure/firewall/features) which is a managed Firewall as a Service (FaaS) that grants/ denies server access based on the originating IP address.
- Use [Network Security Groups (NSGs)](/azure/virtual-network/network-security-groups-overview) to filter network traffic to, and from, Azure resources on Azure Virtual Networks
- Leverage [Application Security Groups](/azure/virtual-network/application-security-groups) to group servers together with similar port filtering requirements, with similar functions, such as web servers and database servers.
- For web and application servers leverage [Azure Distributed Denial of Service (DDoS) protection](/azure/ddos-protection/ddos-protection-overview). DDoS attacks are designed to overwhelm and exhaust network resources, making apps slow or unresponsive. It is common for DDos attacks to target user interfaces. Azure DDoS protection sanitizes unwanted network traffic, before it impacts service availability
- Leverage VM extensions to help address anti-malware, desired state, threat detection, prevention, and remediation to address threats at the operating system, machine, and network levels:
   - [Guest Configuration extension](/azure/virtual-machines/extensions/guest-configuration) performs audit and configuration operations inside virtual machines.
   - [Network Watcher Agent virtual machine extension for Windows and Linux](/azure/virtual-machines/extensions/network-watcher-windows) monitors network performance, diagnostic, and analytics service that allows monitoring of Azure networks. 
   - [Microsoft Antimalware Extension for Windows](/azure/virtual-machines/extensions/iaas-antimalware-windows) to help identify and remove viruses, spyware, and other malicious software, with configurable alerts.
   - [Evaluate third party extensions](/azure/virtual-machines/extensions/overview) such as Symantec Endpoint Protection for Windows VM (/azure/virtual-machines/extensions/symantec)
- Leverage [Azure Policy](/azure/governance/policy/overview) to create business rules that can be applied to your environment. Azure Policies evaluate Azure resources by comparing the properties of those resources against rules defined in JSON format.
- Azure Blueprints enables cloud architects and central information technology groups to define a repeatable set of Azure resources that implements and adheres to an organization's standards, patterns, and requirements. Azure Blueprints are [different than Azure Policies](/azure/governance/blueprints/overview#how-its-different-from-azure-policy).





## Next steps

To learn more, see the other articles in this best practices series:

- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Storage](performance-guidelines-best-practices-storage.md)
- [Security](security-considerations-best-practices.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)

Consider enabling [SQL Assessment for SQL Server on Azure VMs](sql-assessment-for-sql-vm.md).

Review other SQL Server Virtual Machine articles at [SQL Server on Azure Virtual Machines Overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently Asked Questions](frequently-asked-questions-faq.yml).
