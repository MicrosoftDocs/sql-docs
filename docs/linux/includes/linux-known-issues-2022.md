---
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/27/2023
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
The following sections describe known issues with [!INCLUDE [sssql22](../../includes/sssql22-md.md)] on Linux.

- [General](#general)
- [Databases](#databases)
- [Network](#network)
- [Localization](#localization)
- [Full-Text Search](#full-text-search)
- [SQL Server Integration Services (SSIS)](#ssis)
- [SQL Server Management Studio (SSMS)](#sql-server-management-studio-ssms)
- [High availability and disaster recovery](#high-availability-and-disaster-recovery)
- [Availability group continuously switches primary role](#availability-group-continuously-switches-primary-role)
- [Machine Learning Services](#machine-learning-services)

### General

- The length of the hostname where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed needs to be 15 characters or less.

  - **Resolution**: Change the name in `/etc/hostname` to something 15 characters long or less.

- Manually setting the system time backward in time causes [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to stop updating the internal system time within [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

  - **Resolution**: Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Only single instance installations are supported.

  - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager can't connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux.

- The default language of the **sa** login is English.

  - **Resolution**: Change the language of the **sa** login with the `ALTER LOGIN` statement.

- OLEDB provider logs the following warning: `Failed to verify the Authenticode signature of 'C:\binn\msoledbsql.dll'. Signature verification of SQL Server DLLs will be skipped. Genuine copies of SQL Server are signed. Failure to verify the Authenticode signature might indicate that this is not an authentic release of SQL Server. Install a genuine copy of SQL Server or contact customer support.`

  - **Resolution**: No action is required. The OLEDB provider is signed using SHA256. SQL Server Database engine doesn't validate the signed .dll correctly.

- The Reset password command using **mssql-conf** throws the following error: `'Unable to set the system administrator password. Please consult the ERRORLOG in /path for more information.'`

  - **Resolution**: The error message is a false negative. The password reset was *successful*, and you can continue using the new password. This issue only applies to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] container images, and doesn't occur in previous versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

### Databases

- The `master` database can't be moved with the **mssql-conf** utility. Other system databases can be moved with **mssql-conf**.

- When restoring a database that was backed up on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, you must use the `WITH MOVE` clause in the Transact-SQL statement.

- Certain algorithms (cipher suites) for Transport Layer Security (TLS) don't work properly with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. This results in connection failures when attempting to connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and problems establishing connections between replicas in high availability groups.

  - **Resolution**: Modify the `mssql.conf` configuration script for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux to disable problematic cipher suites, by following these steps:

    1. Add the following to `/var/opt/mssql/mssql.conf`.

       ```ini
       [network]
       tlsciphers=AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA:!ECDHE-RSA-AES128-GCM-SHA256:!ECDHE-RSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES128-GCM-SHA256:!ECDHE-ECDSA-AES256-SHA384:!ECDHE-ECDSA-AES128-SHA256:!ECDHE-ECDSA-AES256-SHA:!ECDHE-ECDSA-AES128-SHA:!ECDHE-RSA-AES256-SHA384:!ECDHE-RSA-AES128-SHA256:!ECDHE-RSA-AES256-SHA:!ECDHE-RSA-AES128-SHA:!DHE-RSA-AES256-GCM-SHA384:!DHE-RSA-AES128-GCM-SHA256:!DHE-RSA-AES256-SHA:!DHE-RSA-AES128-SHA:!DHE-DSS-AES256-SHA256:!DHE-DSS-AES128-SHA256:!DHE-DSS-AES256-SHA:!DHE-DSS-AES128-SHA:!DHE-DSS-DES-CBC3-SHA:!NULL-SHA256:!NULL-SHA
       ```

       > [!NOTE]  
       > In the preceding code, `!` negates the expression. This tells OpenSSL to not use the following cipher suite.

    1. Restart [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with the following command.

       ```bash
       sudo systemctl restart mssql-server
       ```

- [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] databases on Windows that use In-memory OLTP can't be restored to  [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. To restore a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] database that uses In-memory OLTP, first upgrade the databases to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, before moving them to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, via backup/restore or detach/attach.

- User permission `ADMINISTER BULK OPERATIONS` isn't supported on Linux at this time.

### Network

Features that involve outbound TCP connections from the `sqlservr` process, such as linked servers, PolyBase, or availability groups, might not work if both the following conditions are met:

1. The target server is specified as a hostname and not an IP address.

1. The source instance has IPv6 disabled in the kernel. To verify if your system has IPv6 enabled in the kernel, all the following tests must pass:

   - `cat /proc/cmdline` prints the boot cmdline of the current kernel. The output must not contain `ipv6.disable=1`.
   - The `/proc/sys/net/ipv6/` directory must exist.
   - A C program that calls `socket(AF_INET6, SOCK_STREAM, IPPROTO_IP)` should succeed - the syscall must return an `fd != -1` and not fail with `EAFNOSUPPORT`.

The exact error depends on the feature. For linked servers, this manifests as a login timeout error. For availability groups, the `ALTER AVAILABILITY GROUP JOIN` DDL on the secondary will fail after 5 minutes with a `download configuration timeout` error.

To work around this issue, do one of the following steps:

- Use IPs instead of host names to specify the target of the TCP connection.

- Enable IPv6 in the kernel by removing `ipv6.disable=1` from the boot command line. The method depends on the Linux distribution and the bootloader, such as **grub**. If you want IPv6 to be disabled, you can still disable it by setting `net.ipv6.conf.all.disable_ipv6 = 1` in the `sysctl` configuration (for example, `/etc/sysctl.conf`). This still prevents the system's network adapter from getting an IPv6 address, but allow the `sqlservr` features to work.

#### TLS 1.3 not supported

Although TLS 1.3 is supported on [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] for Windows, you must use TLS 1.2 on Linux.

#### Network File System (NFS)

If you use Network File System (NFS) remote shares in production, note the following support requirements:

- Use NFS version 4.2 or higher. Older versions of NFS don't support required features, such as `fallocate` and sparse file creation, common to modern file systems.

- Locate only the `/var/opt/mssql` directories on the NFS mount. Other files, such as the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system binaries, aren't supported.

- Ensure that NFS clients use the `nolock` option when mounting the remote share.

### Localization

- If your locale isn't English (`en_us`) during setup, you must use UTF-8 encoding in your bash session/terminal. If you use ASCII encoding, you might see an error similar to the following output:

  ```output
  UnicodeEncodeError: 'ascii' codec can't encode character u'\xf1' in position 8: ordinal not in range(128)
  ```

  If you can't use UTF-8 encoding, run setup using the `MSSQL_LCID` environment variable to specify your language choice.

  ```bash
  sudo MSSQL_LCID=<LcidValue> /opt/mssql/bin/mssql-conf setup
  ```

- When you run `mssql-conf setup` while performing a non-English installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], incorrect extended characters are displayed after the localized text, "Configuring SQL Server...". Or, for non-Latin based installations, the sentence might be missing completely. The missing sentence should display the following localized string:

  `The licensing PID was successfully processed. The new edition is [<Name> edition]`.

  This string is output for information purposes only, and the next [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Cumulative Update will address this issue for all languages. This doesn't affect the successful installation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in any way.

### Full-Text Search

Not all filters are available with this release, including filters for Microsoft Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](../sql-server-linux-setup-full-text-search.md#filters).

### <a id="ssis"></a> SQL Server Integration Services (SSIS)

The **mssql-server-is** package isn't supported on SUSE in this release. It's currently supported on Ubuntu and on Red Hat Enterprise Linux (RHEL).

[!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] packages can use ODBC connections on Linux. We have tested this functionality with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the MySQL ODBC drivers, but is also expected to work with any Unicode ODBC driver that observes the ODBC specification. At design time, you can provide either a DSN or a connection string to connect to the ODBC data; you can also use Windows authentication. For more info, see the [blog post announcing ODBC support on Linux](https://blogs.msdn.microsoft.com/ssis/2017/06/16/odbc-is-supported-in-ssis-on-linux-ssis-helsinki-ctp2-1-refresh/).

The following features aren't supported in this release when you run SSIS packages on Linux:

- [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] Catalog database
- Scheduled package execution by SQL Agent
- Windows Authentication
- Third-party components
- Change Data Capture (CDC)
- [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out
- Azure Feature Pack for SSIS
- Hadoop and HDFS support
- Microsoft Connector for SAP BW

For a list of built-in SSIS components that aren't currently supported, or that are supported with limitations, see [Limitations and known issues for SSIS on Linux](../sql-server-linux-ssis-known-issues.md#components).

For more info about SSIS on Linux, see the following articles:

- [Blog post announcing SSIS support for Linux](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/).
- [Install SQL Server Integration Services (SSIS) on Linux](../sql-server-linux-setup-ssis.md)
- [Extract, transform, and load data on Linux with SSIS](../sql-server-linux-migrate-ssis.md)

### SQL Server Management Studio (SSMS)

The following limitations apply to [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] on Windows connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Linux.

- Maintenance plans aren't supported.

- Management Data Warehouse (MDW) and the data collector in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] aren't supported.

- [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] UI components that have Windows Authentication or Windows event log options don't work with Linux. You can still use these features with other options, such as SQL logins.

- Number of log files to retain can't be modified.

### High availability and disaster recovery

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] packages for RHEL 9 and Ubuntu 22.04, when you enable the HA/DR stack with Pacemaker, you can experience issues with automatic and manual failover. These issues are currently limited to the Pacemaker HA stack. Other HA stacks, including HPE Serviceguard and DH2i DxEnterprise, don't have these issues.

### Availability group continuously switches primary role

When working with availability groups (AGs) in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on RHEL 8, Ubuntu 20.04, and later versions, you can encounter a situation where the primary role in the AG switches from one node to another continuously. Currently, you can work around the issue with these steps:

1. Update the `ag_cluster` resource property `failure-timeout` to `0s`:

   ```bash
   pcs resource update ag_cluster meta failure-timeout=0s
   ```

   For more information, see [Configure a Pacemaker cluster for SQL Server availability groups](../sql-server-linux-availability-group-cluster-pacemaker.md).

1. Reset the fail count on the Pacemaker cluster:

   ```bash
   crm_failcount -r ag_resource_name -delete
   ```

### Machine Learning Services

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] packages for RHEL 9 and Ubuntu 22.04, there are some prerequisites to take into account with `cgroup-v1`, before installing Machine Learning Services.

#### [RHEL 9](#tab/rhel9)

1. As a prerequisite, `cgroup-v1` needs to be enabled as per [Using cgroupfs to manually manage cgroups Red Hat Enterprise Linux 9](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/9/html/managing_monitoring_and_updating_the_kernel/assembly_using-cgroupfs-to-manually-manage-cgroups_managing-monitoring-and-updating-the-kernel#proc_mounting-cgroups-v1_assembly_using-cgroupfs-to-manually-manage-cgroups) from Red Hat.

1. Then follow instructions to [install SQL Machine Learning Services](../sql-server-linux-setup-machine-learning-sql-2022.md#install-runtimes-and-packages) as documented.

1. Disable network namespace isolation.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set extensibility outboundnetworkaccess 1
   ```

1. Restart `mssql-launchpadd` service for these changes to take effect.

   ```bash
   sudo systemctl restart mssql-launchpadd
   ```

#### [Ubuntu 22.04](#tab/ubuntu22)

For Ubuntu 22.04, you should reach out to Canonical directly for the exact steps. Based on the available information, here's a summary of the steps that are needed. We don't recommend these steps on a production workload.

1. Open the **grub** bootloader configuration file located at `/etc/default/grub`.

   ```bash
   sudo vi /etc/default/grub
   ```

1. Find the line starting with `GRUB_CMDLINE_LINUX_DEFAULT=` and append `systemd.unified_cgroup_hierarchy=0` to the string value of this parameter. The result should be something like the following output:

   ```makefile
   GRUB_CMDLINE_LINUX_DEFAULT="quiet splash systemd.unified_cgroup_hierarchy=0"
   ```

   Save the file and exit the editor.

1. Update **grub** and reboot the system.

   ```bash
   sudo update-grub
   sudo reboot
   ```

1. Once the system finishes the reboot, verify that `cgroup-v1` is set up correctly, by checking the `cgroup` mount points.

   ```bash
   mount | grep cgroup
   ```

   The output should look as follows, you should see `cgroup` without a number suffix in the output:

   ```output
   tmpfs on /sys/fs/cgroup type tmpfs (ro,nosuid,nodev,noexec,size=4096k,nr_inodes=1024,mode=755,inode64)
   cgroup2 on /sys/fs/cgroup/unified type cgroup2 (rw,nosuid,nodev,noexec,relatime,nsdelegate)
   cgroup on /sys/fs/cgroup/systemd type cgroup (rw,nosuid,nodev,noexec,relatime,xattr,name=systemd)
   cgroup on /sys/fs/cgroup/net_cls,net_prio type cgroup (rw,nosuid,nodev,noexec,relatime,net_cls,net_prio)
   cgroup on /sys/fs/cgroup/cpu,cpuacct type cgroup (rw,nosuid,nodev,noexec,relatime,cpu,cpuacct)
   cgroup on /sys/fs/cgroup/cpuset type cgroup (rw,nosuid,nodev,noexec,relatime,cpuset)
   cgroup on /sys/fs/cgroup/pids type cgroup (rw,nosuid,nodev,noexec,relatime,pids)
   cgroup on /sys/fs/cgroup/hugetlb type cgroup (rw,nosuid,nodev,noexec,relatime,hugetlb)
   cgroup on /sys/fs/cgroup/blkio type cgroup (rw,nosuid,nodev,noexec,relatime,blkio)
   cgroup on /sys/fs/cgroup/rdma type cgroup (rw,nosuid,nodev,noexec,relatime,rdma)
   cgroup on /sys/fs/cgroup/devices type cgroup (rw,nosuid,nodev,noexec,relatime,devices)
   cgroup on /sys/fs/cgroup/perf_event type cgroup (rw,nosuid,nodev,noexec,relatime,perf_event)
   cgroup on /sys/fs/cgroup/freezer type cgroup (rw,nosuid,nodev,noexec,relatime,freezer)
   cgroup on /sys/fs/cgroup/memory type cgroup (rw,nosuid,nodev,noexec,relatime,memory)
   cgroup on /sys/fs/cgroup/misc type cgroup (rw,nosuid,nodev,noexec,relatime,misc)
   ```

   Once you have enabled `cgroup-v1` for Ubuntu 22.04, follow the steps in [Install SQL Server 2022 Machine Learning Services (Python and R) on Linux](../sql-server-linux-setup-machine-learning-sql-2022.md#install-runtimes-and-packages), to install and enable SQL Machine Learning Service for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] packages on Ubuntu 22.04.

---
