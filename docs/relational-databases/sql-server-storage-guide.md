---
title: "SQL Server I/O fundamentals"
description: Learn about how storage choice and caching affect SQL Server performance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/12/2024
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "I/O guide"
  - "I/O guidance"
  - "guide, I/O"
  - "guidance, I/O"
  - "disk cache"
  - "drive cache"
  - "storage cache"
  - "SQL Server I/O internals"
  - "SQL Server I/O architecture"
  - "SQL Server I/O guide"
  - "SQL Server I/O guidance"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# SQL Server I/O fundamentals

[!INCLUDE [appliesto-sqlserver-sqlmi-sqlvm](../includes/applies-to-version/appliesto-sqlserver-sqlmi-sqlvm.md)]

The primary purpose of a [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] database is to store and retrieve data, so intensive disk input/output (I/O) is a core characteristic of the [!INCLUDE [ssde-md](../includes/ssde-md.md)]. Because disk I/O operations can consume many resources and take a relatively long time to finish, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] focuses on making I/O highly efficient.

Storage subsystems for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] are provided in multiple form factors, including mechanical drives and solid state storage. This article provides details on how to use drive caching principles to improve [!INCLUDE [ssde-md](../includes/ssde-md.md)] I/O.

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] requires that systems support *guaranteed delivery to stable media* as outlined under the [SQL Server I/O Reliability Program Requirements](https://download.microsoft.com/download/f/1/e/f1ecc20c-85ee-4d73-baba-f87200e8dbc2/sql_server_io_reliability_program_review_requirements.pdf). For more information about the input and output requirements for the [!INCLUDE [ssdenoversion-md](../includes/ssdenoversion-md.md)], see [SQL Server Database Engine Disk Input/Output (I/O) requirements](/troubleshoot/sql/database-engine/database-file-operations/database-engine-input-output-requirements).

## Disk I/O

The buffer manager only performs reads and writes to the database. Other file and database operations such as open, close, extend, and shrink are performed by the database manager and file manager components.

Disk I/O operations by the buffer manager have the following characteristics:

- I/O is typically performed asynchronously, which allows the calling thread to continue processing while the I/O operation takes place in the background. Under some circumstances (for example, misaligned log I/O), synchronous I/Os can occur.

- All I/Os are issued in the calling threads unless the affinity I/O option is in use. The affinity I/O mask option binds [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs. In high-end [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] online transactional processing (OLTP) environments, this extension can enhance the performance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] threads issuing I/Os.

- Multiple page I/Os are accomplished with scatter-gather I/O, which allows data to be transferred into or out of noncontiguous areas of memory. This means that [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] can quickly fill or flush the buffer cache while avoiding multiple physical I/O requests.

### Long I/O requests

The buffer manager reports on any I/O request that is outstanding for at least 15 seconds. This helps the system administrator distinguish between [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] problems and I/O subsystem problems. Error message [MSSQLSERVER_833](errors-events/mssqlserver-833-database-engine-error.md) is reported and appears in the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] error log as follows:

```output
SQL Server has encountered ## occurrence(s) of I/O requests taking longer than 15 seconds to complete on file [##] in database [##] (#). The OS file handle is 0x00000. The offset of the latest long I/O is: 0x00000.
```

A long I/O can be either a read or a write; it isn't currently indicated in the message. Long-I/O messages are warnings, not errors. They don't indicate problems with [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] but with the underlying I/O system. The messages are reported to help the system administrator find the cause of poor [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] response times more quickly, and to distinguish problems that are outside the control of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. As such, they don't require any action, but the system administrator should investigate why the I/O request took so long, and whether the time is justifiable.

### Causes of long I/O requests

A long I/O message can indicate that an I/O is permanently blocked and will never complete (known as lost I/O), or merely that it just hasn't completed yet. It isn't possible to tell from the message which scenario is the case, although a lost I/O often leads to a latch timeout.

Long I/Os often indicate a [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] workload that is too intense for the disk subsystem. An inadequate disk subsystem might be indicated when:

- Multiple long I/O messages appear in the error log during a heavy [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] workload.
- Performance Monitor counters show long disk latencies, long disk queues, or no disk idle time.

Long I/Os can also be caused by a component in the I/O path (for example, a driver, controller, or firmware) continually postponing servicing an old I/O request, in favor of servicing newer requests. This can occur in interconnected environments, such as iSCSI and fiber channel networks (either due to a misconfiguration or path failure). This can be difficult to corroborate with the Performance Monitor tool because most I/Os are being serviced promptly. Long I/O requests can be aggravated by workloads that perform large amounts of sequential I/O, such as backup and restore, table scans, sorting, creating indexes, bulk loads, and zeroing out files.

Isolated long I/Os that don't appear related to any of the previous conditions can be caused by a hardware or driver problem. The system event log might contain a related event that helps to diagnose the problem.

### I/O performance issues caused by inefficient queries or filter drivers

Slow I/O can be caused by queries that aren't written efficiently, or not tuned properly with indexes and statistics. Another common factor in I/O latency is the presence of antivirus or other security programs that scan database files. This scanning software might extend to the network layer, which adds network latency, in turn indirectly affecting database latency. Although the scenario described about [15-second I/O](#long-io-requests) is more common with hardware components, a smaller I/O latency is more frequently observed with unoptimized queries or misconfigured antivirus programs.

For detailed information on how to address these issues, see [Troubleshoot slow SQL Server performance caused by I/O issues](/troubleshoot/sql/database-engine/performance/troubleshoot-sql-io-performance).

For information on how to configure antivirus protection on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see [Configure antivirus software to work with SQL Server](/troubleshoot/sql/database-engine/security/antivirus-and-sql-server).

## Write caching in storage controllers

I/O transfers that are performed without the use of a cache can be significantly longer in mechanical drives, because of hard drive spin rates, the mechanical time that is needed to move the drive heads, and other limiting factors. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installations are targeted at systems that provide caching controllers. These controllers disable the on-disk caches and provide stable media caches to satisfy [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] I/O requirements. They avoid performance issues related to storage seek and write times by using the various optimizations of the caching controller.

> [!NOTE]  
> Some storage vendors use persistent memory (PMEM) as storage rather than a cache, which can improve overall performance. For more information, see [Configure persistent memory (PMEM) for SQL Server on Windows](../database-engine/configure-windows/configure-persistent-memory.md) and [Configure persistent memory (PMEM) for SQL Server on Linux](../linux/sql-server-linux-configure-pmem.md).

Use of a write caching (also called write-back caching) storage controller can improve [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] performance. Write caching controllers and storage subsystems are safe for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], if they're designed for use in a data critical transactional database management system (DBMS) environment. These design features must preserve cached data if a system failure occurs. Using an external uninterruptible power supply (UPS) to achieve this is generally not sufficient, because failure modes that are unrelated to power can occur.

Caching controllers and storage subsystems can be safe for use by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Most new purpose-built server platforms that incorporate these are safe. However, you should check with your hardware vendor to be sure that the storage subsystem was tested and approved for use in a data critical transactional relational database management system (RDBMS) environment.

## Write-ahead logging

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data modification statements generate logical page writes. This stream of writes can be pictured as going two places: the log and the database itself. For performance reasons, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] defers writes to the database via its own cache buffer system. Writes to the log are only momentarily deferred until `COMMIT` time. They aren't cached in the same way as data writes. Because log writes for a given page always precede the page's data writes, the log is sometimes referred to as a *write-ahead log* (WAL).

### Write-ahead logging (WAL) protocol

The term *protocol* is an excellent way to describe WAL. The WAL used by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is known as ARIES (Algorithm for Recovery and Isolation Exploiting Semantics). For more information, see [Manage accelerated database recovery](accelerated-database-recovery-management.md).

It's a specific and defined set of implementation steps necessary to ensure that data is stored and exchanged properly and can be recovered to a known state in the event of a failure. Just as a network contains a defined protocol to exchange data in a consistent and protected manner, so too does the WAL describe the protocol to protect data. All versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] open the log and data files using the Win32 `CreateFile` function. The `dwFlagsAndAttributes` member includes the `FILE_FLAG_WRITE_THROUGH` option when opened by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### FILE_FLAG_WRITE_THROUGH

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] creates its database files using the `FILE_FLAG_WRITE_THROUGH` flag. This option instructs the system to write through any intermediate cache and go directly to storage. The system can still cache write operations, but can't lazily flush them. For more information, see [CreateFileA](/windows/win32/api/fileapi/nf-fileapi-createfilea).

The `FILE_FLAG_WRITE_THROUGH` option ensures that when a write operation returns successful completion the data is correctly stored in stable storage. This aligns with the Write Ahead Logging (WAL) protocol specification to ensure the data. Many storage devices (NVMe, PCIe, SATA, ATA, SCSI, and IDE-based) contain onboard caches of 512 KB, 1 MB, and larger. Storage caches usually rely on a capacitor and not a battery-backed solution. These caching mechanisms can't guarantee writes across a power cycle or similar failure point. They only guarantee the completion of the sector write operations. As the storage devices continue to grow in size, the caches become larger, and they can expose larger amounts of data during a failure.

For more information on FUA support by Linux distribution and its effect on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see [SQL Server On Linux: Forced Unit Access (FUA) Internals](https://techcommunity.microsoft.com/t5/sql-server-blog/sql-server-on-linux-forced-unit-access-fua-internals/ba-p/3199102).

## Transactional integrity and SQL Server recovery

Transactional integrity is one of the fundamental concepts of a relational database system. Transactions are considered to be atomic units of work that are either totally applied or totally rolled back. The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] write-ahead transaction log is a vital component in implementing transactional integrity.

Any relational database system must also deal with a concept closely related to transactional integrity, which is recovery from unplanned system failure. Several non-ideal, real-world effects can cause this failure. On many database management systems, system failure can result in a lengthy human-directed manual recovery process.

In contrast, the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] recovery mechanism is automatic and operates without human intervention. For example, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] could be supporting a mission-critical production application, and experience a system failure due to a momentary power fluctuation. Upon restoration of power, the server hardware would restart, networking software would load and initialize, and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] would restart. As [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] initializes, it automatically runs its recovery process based on data in the transaction log. This entire process occurs without human intervention. Whenever the client workstations restarted, users would find all of their data present, up to the last transaction they entered.

Transactional integrity and automatic recovery in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] constitute a powerful time-and-labor saving capability. If a write caching controller isn't properly designed for use in a data critical transactional DBMS environment, it can compromise the ability of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to recover, hence corrupting the database. This can occur if the controller intercepts [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] transaction log writes and buffers them in a hardware cache on the controller board, but doesn't preserve these written pages during a system failure.

## Write caching and storage device controllers

Most storage device caching controllers perform write caching. The write caching function can't always be disabled.

Even if the server uses a UPS, this doesn't guarantee the security of the cached writes. Many types of system failures can occur that a UPS doesn't address. For example, a memory parity error, an operating system (OS) trap, or a hardware glitch that causes a system reset can produce an uncontrolled system interruption. A memory failure in the hardware write cache can also result in the loss of vital log information.

Another possible problem related to a write-caching controller can occur at system shutdown. It isn't uncommon to *cycle* the OS or restart the system during configuration changes. Even if a careful operator follows the OS recommendation to wait until all storage activity ceases before restarting the system, cached writes can still be present in the controller. When the `Ctrl`+`Alt`+`Del` key combination is pressed, or a hardware reset button is pressed, cached writes can be discarded, potentially damaging the database.

It's possible to design a hardware write cache, which takes into account all possible causes of discarding dirty cache data, which would thus be safe for use by a database server. Some of these design features would include intercepting the RST bus signal to avoid uncontrolled reset of the caching controller, on-board battery backup, and mirrored or error checking and correcting (ECC) memory. Check with your hardware vendor to ensure that the write cache includes these and any other features necessary to avoid data loss.

## Use storage caches with SQL Server

A database system is first and foremost responsible for the accurate storage and retrieval of data, even in the event of unexpected system failures.

The system must guarantee the atomicity and durability of transactions, while accounting for current execution, multiple transactions, and various failure points. This is often referred to as the ACID (Atomicity, Consistency, Isolation, and Durability) properties.

This section addresses the implications of storage caches. It's recommended that you read the following articles in the Microsoft Knowledge Base for further clarification on caching and alternate failure mode discussions:

- [86903 SQL Server and caching disk controllers](https://support.microsoft.com/help/86903)
- [Description of logging and data storage algorithms that extend data reliability in SQL Server](/troubleshoot/sql/database-engine/database-file-operations/logging-data-storage-algorithms)

The following documents are also recommended:

- [SQL Server 2000 I/O Basics](/previous-versions/cc966500(v=technet.10))
- [SQL Server I/O Basics Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10))

These two documents apply to all currently supported versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### Battery-backed caching solutions

Enhanced caching controller systems disable on-disk cache and provide a functional battery-backed caching solution. These caches can maintain the data in the cache for several days and even allow the caching card to be placed in a second computer. When power is properly restored, the unwritten data is completely flushed before any further data access is allowed. Many of them allow percentage of read versus write cache to be established for optimal performance. Some contain large memory storage areas. Some hardware vendors provide high-end battery-backed drive caching systems with multiple gigabytes of cache. These can significantly improve database performance. Using battery-backed caching solutions provides durability and consistency of data that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] expects.

### Storage subsystem implementations

There are many types of subsystem implementations. RAID (redundant array of independent disks) and SAN (storage area network) are two examples of these types of subsystem implementations. These systems are typically built with SCSI-based drives. There are several reasons for this. The following section generically describes high level storage considerations.

#### SCSI, SAS, and NVMe

SCSI, SAS, and NVMe storage devices:

- Are typically manufactured for heavy duty use.
- Are typically targeted at multiuser, server-based implementations.
- Typically have better meantime to failure rates than other implementations.
- Contain sophisticated heuristics to help predict imminent failures.

#### Non-SCSI

Other drive implementations, such as IDE, ATA, and SATA:

- Are typically manufactured for light and medium duty use.
- Are typically targeted at single user-based applications.

Non-SCSI, desktop-based controllers require more main processor (CPU) bandwidth, and are frequently limited by a single active command. For example, when a non-SCSI drive is adjusting a bad block, the drive requires that the host commands wait. The ATA bus presents another example: the ATA bus supports two devices, but only a single command can be active. This leaves one drive idle while the other drive services the pending command. RAID systems built on desktop technologies can all experience these symptoms and be greatly affected by the slowest responder. Unless these systems use advanced designs, their performance isn't as efficient as the performance of SCSI-based systems.

## Storage considerations

There are situations in which a desktop-based drive or array is an appropriate low cost solution. For example, if you set up a read-only database for reporting, you shouldn't encounter many of the performance factors of an OLTP database when drive caching is disabled.

Storage device sizes continue to increase. Low cost, high capacity drives can be appealing. But when you configure the drive for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and your business response time needs, you should carefully consider the following issues:

- Access path design
- The requirement to disable the on-disk cache

### Mechanical hard drives

Mechanical drives use spinning magnetic platters for storing data. They are available in several capacities and form factors, such as IDE, SATA, SCSI, and Serial Attached SCSI (SAS). Some SATA drives include failure prediction constructs. SCSI drives are designed for heavier duty cycles and decreased failure rates.

Drive caching should be disabled in order to use the drive with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. By default, the drive cache is enabled. In Windows Server, use the **Disk Properties** > **Hardware** tab to access the **Properties** > **Policy** tab to control the drive cache setting.

> [!NOTE]  
> Some drives don't honor this setting. These drives require a specific manufacturer utility to disable cache.

IDE and ATA-based systems can postpone host commands when they perform activities such as bad block adjustment. This could lead to periods of stalled I/O activity.

SAS benefits include advanced queuing up to 256 levels, and head of queue and out of order queuing. The SAS backplane is designed in a way that enables the use of both SAS and SATA drives within the same system.

Your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation depends on the controller's ability to disable the on-disk cache and to provide a stable I/O cache. Writing data out of order to various drives isn't a hindrance to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] as long as the controller provides the correct stable media caching capabilities. The complexity of the controller design increases with advanced data security techniques such as mirroring.

### Solid state storage

Solid-state storage has advantages over mechanical (spinning) hard drives, but is susceptible to many of the same failure patterns as spinning media. Solid-state storage is connected to your server using various interfaces, including NVM Express (NVMe), PCI Express (PCIe), and SATA. Treat the solid-state media as you would spinning media, and make sure that the appropriate safeguards are in place for power failure, such as a battery-backed caching controller.

Common issues caused by a power fault include:

- **Bit corruption**: Records exhibit random bit errors.
- **Flying writes**: Well-formed records end up in the wrong place.
- **Shorn writes**: Operations are partially done at a level below the expected sector size.
- **Metadata corruption**: Metadata in FTL is corrupted.
- **Unresponsive device**: Device doesn't work at all, or mostly doesn't work.
- **Unserializability**: Final state of storage doesn't result from a serializable operation order.

#### 512e

Most solid-state storage reports 512-byte sector sizes but use 4-KB pages inside the 1-MB erasure blocks. Using 512-byte aligned sectors for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] log device can generate more read/modify/write (RMW) activities, which can contribute to slower performance and drive wear.

**Recommendation**: Make sure the caching controller is aware of the correct page size of the storage device, and can align physical writes with the solid-state storage infrastructure properly.

#### 0xFFFFFFFF

A newly formatted drive usually holds all zeros. An erased block of a solid-state device is all `1`s, making a raw read of an erased block all `0xFF`s. However, it's unusual for a user to read an erased block during normal I/O operations.

#### Pattern stamping

A technique we used in the past is to write a known pattern to the entire drive. Then as we execute database activity against that same drive, we can detect incorrect behavior (stale read / lost write / read of incorrect offset / etc.) when the pattern unexpectedly appears.

This technique doesn't work well on solid-state storage. The erasure and RMW activities for writes destroys the pattern. The solid-state storage garbage collection (GC) activity, wear leveling, proportional/set-aside list blocks and other optimizations tend to cause writes to acquire different physical locations, unlike spinning media's sector reuse.

#### Firmware

The firmware used in solid-state storage tends to be complex when compared to spinning media counterparts. Many drives use multiple processing cores to handle incoming requests and garbage collection activities. Make sure you keep your solid-state device firmware up to date to avoid known problems.

#### Read data damage and wear leveling

A common garbage collection (GC) approach for solid-state storage helps prevent repeated, read data damage. When reading the same cell repeatedly, it's possible the electron activity can leak and cause damage to neighboring cells. Solid-state storage protects the data with various levels of error correction code (ECC) and other mechanisms.

One such mechanism relates to wear leveling. Solid-state storage keeps track of the read and write activity on the storage device. The garbage collection can determine hot spots or locations wearing faster than other locations. For example, the GC determines that a block has been in a read-only state for a period of time and needs to move. This movement is generally to a block with more wear, so the original block can be used for writes. This helps balance the wear on the drive, but moves read-only data to a location that has more wear and mathematically increases the failure chances, even if slightly.

Another side-effect of wear leveling can occur with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Suppose you execute [DBCC CHECKDB](../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md), and it reports an error. If you run it a second time, there's a small chance that `DBCC CHECKDB` reports additional or a different pattern of errors, because the solid-state storage GC activity might make changes between the `DBCC CHECKDB` executions.

#### OS error 665 and defragmentation

Spinning media needs to keep blocks near one another to reduce the drive's head movement and increase performance. Solid-state storage doesn't have the physical head, which eliminates *seek time*. Many solid-state devices are designed to allow parallel operations on different blocks in parallel. This means that defragmentation of solid-state media is unnecessary. Serial activities are the best I/O patterns to maximize I/O throughput on solid-state storage devices.

> [!NOTE]  
> Solid-state storage benefits from the *trim* feature, an operating system (OS) level command that erases blocks that are considered no longer in use. In Windows, use the **Optimize Drives** tool to set a weekly schedule for optimizing drives.

**Recommendations:**

- Use an appropriate, battery-backed controller designed to optimize write activities. This can improve performance, reduce drive wear and physical fragmentation levels.

- Consider using the [ReFS](/windows-server/storage/refs/refs-overview) file system to avoid the NTFS attribute limitations.

- Make sure the file growth sizes are appropriately sized.

For more information on troubleshooting OS error 665 as it relates to fragmentation, see [OS errors 665 and 1450 are reported for SQL Server files](/troubleshoot/sql/database-engine/database-file-operations/1450-and-665-errors-running-dbcc-checkdb).

#### Compression

As long as the drive maintains the intent of stable media, compression can elongate the drive life and might positively affect performance. However, some firmware might already compress data invisibly. Remember to test new storage scenarios before deploying it to your production environment.

#### Summary

- Maintain proper backup and disaster recovery procedures and processes.
- Keep your firmware up to date.
- Listen closely to your hardware manufactures guidance.

## Cache considerations and SQLIOSim

To fully secure your data, you should ensure that all data caching is properly handled. In many situations, this means you must disable the write caching of the drive.

> [!NOTE]  
> Ensure that any alternate caching mechanism can properly handle multiple types of failure.

Microsoft performed testing on several SCSI and IDE drives using the **SQLIOSim** utility. This utility simulates heavy asynchronous read/write activity to a simulated data device and log device. For more information and details about **SQLIOSim**, see [Use the SQLIOSim utility to simulate SQL Server activity on a disk subsystem](/troubleshoot/sql/tools/sqliosim-utility-simulate-activity-disk-subsystem).

Many PC manufacturers order the drives with the write cache disabled. However, testing shows that this might not always be the case, so you should always test it completely. If you have any questions about the caching status of your storage device, contact the manufacturer and obtain the appropriate utility or jumper settings to disable write caching operations.

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] requires systems to support *guaranteed delivery to stable media*, as outlined under the [SQL Server I/O Reliability Program Requirements](https://download.microsoft.com/download/f/1/e/f1ecc20c-85ee-4d73-baba-f87200e8dbc2/sql_server_io_reliability_program_review_requirements.pdf). For more information about the input and output requirements for the [!INCLUDE [ssdenoversion-md](../includes/ssdenoversion-md.md)], see [SQL Server Database Engine Disk Input/Output (I/O) requirements](/troubleshoot/sql/database-engine/database-file-operations/database-engine-input-output-requirements).

## Related content

- [Pages and extents architecture guide](pages-and-extents-architecture-guide.md)
- [Memory management architecture guide](memory-management-architecture-guide.md)
- [SQL Server On Linux: Forced Unit Access (FUA) Internals](https://techcommunity.microsoft.com/t5/sql-server-blog/sql-server-on-linux-forced-unit-access-fua-internals/ba-p/3199102)
- [SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10))
