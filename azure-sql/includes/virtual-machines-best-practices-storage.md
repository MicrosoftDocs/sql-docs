---
author: MashaMSFT
ms.author: mathoma
ms.date: 03/29/2023
ms.service: virtual-machines
ms.topic: include
---
- Monitor the application and [determine storage bandwidth and latency requirements](/azure/virtual-machines/premium-storage-performance#counters-to-measure-application-performance-requirements) for SQL Server data, log, and `tempdb` files before choosing the disk type.
- To optimize storage performance, plan for highest uncached IOPS available and use data caching as a performance feature for data reads while avoiding [virtual machine and disks capping](/azure/virtual-machines/premium-storage-performance#throttling).
- Place data, log, and `tempdb` files on separate drives.
  - For the data drive, use [premium P30 and P40 or smaller disks](/azure/virtual-machines/disks-types#premium-ssds) to ensure the availability of cache support. When using the [Ebdsv5 VM series](/azure/virtual-machines/ebdsv5-ebsv5-series), use [Premium SSD v2](/azure/virtual-machines/disks-types#premium-ssd-v2) which provides better price-performance for workloads that require high IOPS and I/O throughput.
  - For the log drive plan for capacity and test performance versus cost while evaluating either [Premium SSD v2](/azure/virtual-machines/disks-types#premium-ssd-v2) or Premium SSD [P30 - P80 disks](/azure/virtual-machines/disks-types#premium-ssds)
    - If submillisecond storage latency is required, use either [Premium SSD v2](/azure/virtual-machines/disks-types#premium-ssd-v2) or [Azure ultra disks](/azure/virtual-machines/disks-types#ultra-disks) for the transaction log.
    - For M-series virtual machine deployments consider [write accelerator](/azure/virtual-machines/how-to-enable-write-accelerator) over using Azure ultra disks.
  - Place [tempdb](/sql/relational-databases/databases/tempdb-database) on the temporary disk (the temporary disk is ephemeral, and defaults to `D:\`) for most SQL Server workloads that aren’t part of a failover cluster instance (FCI) after choosing the optimal VM size.
    - If the capacity of the local drive isn't enough for `tempdb`, consider sizing up the VM. See [Data file caching policies](../virtual-machines/windows/performance-guidelines-best-practices-storage.md#data-file-caching-policies) for more information.
  - For FCI place `tempdb` on the shared storage.
    - If the FCI workload is heavily dependent on `tempdb` disk performance, then as an advanced configuration place `tempdb` on the local ephemeral SSD (default `D:\`) drive, which isn't part of FCI storage. This configuration needs custom monitoring and action to ensure the local ephemeral SSD (default `D:\`) drive is available all the time as any failures of this drive won't trigger action from FCI.
- Stripe multiple Azure data disks using [Storage Spaces](/windows-server/storage/storage-spaces/overview) to increase I/O bandwidth up to the target virtual machine's IOPS and throughput limits.
- Set [host caching](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits) to read-only for data file disks.
- Set [host caching](/azure/virtual-machines/disks-performance#virtual-machine-uncached-vs-cached-limits) to none for log file disks.
  - Don't enable read/write caching on disks that contain SQL Server data or log files.
  - Always stop the SQL Server service before changing the cache settings of your disk.
- For development and test workloads, and long-term backup archival consider using standard storage. It isn't recommended to use Standard HDD/SSD for production workloads.
- [Credit-based Disk Bursting](/azure/virtual-machines/disk-bursting#credit-based-bursting) (P1-P20) should only be considered for smaller dev/test workloads and departmental systems.
- To optimize storage performance, plan for highest uncached IOPS available, and use data caching as a performance feature for data reads while avoiding [virtual machine and disks capping/throttling](/azure/virtual-machines/premium-storage-performance#throttling).
- Format your data disk to use 64-KB allocation unit size for all data files placed on a drive other than the temporary `D:\` drive (which has a default of 4 KB). SQL Server VMs deployed through Azure Marketplace come with data disks formatted with allocation unit size and interleave for the storage pool set to 64 KB.
- Configure the storage account in the same region as the SQL Server VM.
- Disable Azure geo-redundant storage (geo-replication) and use LRS (local redundant storage) on the storage account.
- Enable the [SQL Best Practices Assessment](../virtual-machines/windows/sql-assessment-for-sql-vm.md) to identify possible performance issues and evaluate that your SQL Server VM is configured to follow best practices.
- Review and monitor disk and VM limits using [Storage IO utilization metrics](/azure/virtual-machines/disks-metrics#storage-io-utilization-metrics).
- [Exclude SQL Server files](/troubleshoot/sql/database-engine/security/antivirus-and-sql-server) from antivirus software scanning. This includes data files, log files, and backup files.

