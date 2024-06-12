---
title: Known issues for SQL Server on Linux
description: This article contains the known issues for SQL Server running on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, vanto
ms.date: 01/03/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# SQL Server on Linux: Known issues

The following sections describe known issues with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

## General

The following table lists the most common issues with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

| Issue | Resolution |
| --- | --- |
| The length of the hostname where [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] is installed needs to be 15 characters or less. | Change the name in `/etc/hostname` to a value 15 characters long or less. |
| Manually setting the system time backward in time causes [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to stop updating the internal system time within the [!INCLUDE [ssde-md](../includes/ssde-md.md)]. | Restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| Only single instance installations are supported. | If you want to have more than one instance on a given host, consider using [virtual machines](/azure/azure-sql/virtual-machines/linux/sql-server-on-linux-vm-what-is-iaas-overview) or [Linux containers](sql-server-linux-docker-container-deployment.md). |
| [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Configuration Manager can't connect to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux. | None. |
| The default language of the **sa** login is English. | Change the language of the **sa** login with the `ALTER LOGIN` statement. |
| The OLE DB provider logs the following warning:<br /><br />`Failed to verify the Authenticode signature of 'C:\binn\msoledbsql.dll'. Signature verification of SQL Server DLLs will be skipped. Genuine copies of SQL Server are signed. Failure to verify the Authenticode signature might indicate that this isn't an authentic release of SQL Server. Install a genuine copy of SQL Server or contact customer support.` | No action is required. The OLE DB provider is signed using SHA256. The [!INCLUDE [ssdenoversion-md](../includes/ssdenoversion-md.md)] doesn't validate the signed .dll correctly. |
| The Reset password command using **mssql-conf** throws the following error:<br /><br />`Unable to set the system administrator password. Please consult the ERRORLOG in /path for more information.` | The error message is a false negative. The password reset was successful, and you can continue using the new password.<br /><br />**Applies to:** [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] container images only. |

## Databases

- The `master` database can't be moved with the **mssql-conf** utility. Other system databases can be moved with **mssql-conf**.

- When you restore a database that was backed up on [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Windows, you must use the `WITH MOVE` clause in the Transact-SQL statement. For more information, see [Migrate a SQL Server database from Windows to Linux using backup and restore](sql-server-linux-migrate-restore-database.md).

- Certain algorithms (cipher suites) for Transport Layer Security (TLS) don't work properly with [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux. This results in connection failures when attempting to connect to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], and problems establishing connections between replicas in high availability groups.

  To resolve this issue, modify the `mssql.conf` configuration script for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux to disable problematic cipher suites, by following these steps:

  1. Add the following section to `/var/opt/mssql/mssql.conf`. The exclamation symbol (`!`) negates the expression. This tells OpenSSL not to use the cipher suite that follows.

     ```ini
     [network]
     tlsciphers=AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA:!ECDHE-RSA-AES128-GCM-SHA256:!ECDHE-RSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES128-GCM-SHA256:!ECDHE-ECDSA-AES256-SHA384:!ECDHE-ECDSA-AES128-SHA256:!ECDHE-ECDSA-AES256-SHA:!ECDHE-ECDSA-AES128-SHA:!ECDHE-RSA-AES256-SHA384:!ECDHE-RSA-AES128-SHA256:!ECDHE-RSA-AES256-SHA:!ECDHE-RSA-AES128-SHA:!DHE-RSA-AES256-GCM-SHA384:!DHE-RSA-AES128-GCM-SHA256:!DHE-RSA-AES256-SHA:!DHE-RSA-AES128-SHA:!DHE-DSS-AES256-SHA256:!DHE-DSS-AES128-SHA256:!DHE-DSS-AES256-SHA:!DHE-DSS-AES128-SHA:!DHE-DSS-DES-CBC3-SHA:!NULL-SHA256:!NULL-SHA
     ```

  1. Restart [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] with the following command.

     ```bash
     sudo systemctl restart mssql-server
     ```

- [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] databases on Windows that use [!INCLUDE [inmemory-md](../includes/inmemory-md.md)] can't be restored to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux. If your [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] database uses [!INCLUDE [inmemory-md](../includes/inmemory-md.md)], first upgrade the databases to a newer version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Windows. Then you can move it to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux, with backup/restore, or detach/attach.

- User permission `ADMINISTER BULK OPERATIONS` isn't supported on Linux at this time.

- TDE-compressed backups that are made using [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 16 and later versions can't be restored to previous CU versions of [!INCLUDE [sssql19-md](../includes/sssql19-md.md)]. For more information, see [FIX: Error 3241 occurs during executing RESTORE LOG or RESTORE DATABASE](https://support.microsoft.com/help/5014298).

  Transparent Data Encryption (TDE)-compressed backups that are made using previous CU versions of [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] can still be restored by using [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 16 and later versions.

- When you install [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Ubuntu 22.04, you might see the following error message: `Failed to start Microsoft SQL Server Database Engine`. If you review the error log, you see an incorrect path for the system databases.

  To work around this issue, start the instance in single-user mode, and use `ALTER DATABASE ... MODIFY FILE` to move the configured location of the system databases to the default location `/var/opt/mssql/data`. After making this change, restart the service.

## Network

Features that involve outbound TCP connections from the `sqlservr` process, such as linked servers, PolyBase, or availability groups, might not work if both the following conditions are met:

- The target server is specified as a hostname and not an IP address.

- The source instance has IPv6 disabled in the kernel. To verify if your system has IPv6 enabled in the kernel, all the following tests must pass:

   - `cat /proc/cmdline` prints the boot cmdline of the current kernel. The output must not contain `ipv6.disable=1`.
   - The `/proc/sys/net/ipv6/` directory must exist.
   - A C program that calls `socket(AF_INET6, SOCK_STREAM, IPPROTO_IP)` should succeed - the syscall must return an `fd != -1` and not fail with `EAFNOSUPPORT`.

The exact error depends on the feature. For linked servers, you see a login timeout error. For availability groups, the `ALTER AVAILABILITY GROUP JOIN` DDL on the secondary will fail after five minutes with a `download configuration timeout` error.

To work around this issue, do one of the following options:

- Use IPs instead of host names to specify the target of the TCP connection.

- Enable IPv6 in the kernel by removing `ipv6.disable=1` from the boot command line. The method depends on the Linux distribution and the bootloader, such as **grub**. If you want IPv6 to be disabled, you can still disable it by setting `net.ipv6.conf.all.disable_ipv6 = 1` in the `sysctl` configuration (for example, `/etc/sysctl.conf`). Although this setting prevents the system's network adapter from getting an IPv6 address, it allows the `sqlservr` features to work.

### TLS 1.3 not supported

**Applies to:** [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] only.

Although TLS 1.3 is supported on [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] for Windows, you must use TLS 1.2 on Linux.

### Network File System (NFS)

If you use Network File System (NFS) remote shares in production, note the following support requirements:

- Use NFS version 4.2 or later versions. Older versions of NFS don't support required features, such as `fallocate` and sparse file creation, common to modern file systems.

- Locate only the `/var/opt/mssql` directories on the NFS mount. Other files, such as the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] system binaries, aren't supported.

- Ensure that NFS clients use the `nolock` option when mounting the remote share.

## Localization

- If your locale isn't English (`en_us`) during setup, you must use UTF-8 encoding in your bash session/terminal. If you use ASCII encoding, you might see an error similar to the following output:

  > UnicodeEncodeError: 'ascii' codec can't encode character u'\xf1' in position 8: ordinal not in range(128)

  If you can't use UTF-8 encoding, run setup using the `MSSQL_LCID` environment variable to specify your language choice.

  ```bash
  sudo MSSQL_LCID=<LcidValue> /opt/mssql/bin/mssql-conf setup
  ```

- When you run `mssql-conf setup`, and perform a non-English installation of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], incorrect extended characters can be displayed after the localized text, "Configuring SQL Server...". Or, for non-Latin based installations, the sentence might be missing completely. The missing sentence should display the following localized string:

  > The licensing PID was successfully processed. The new edition is [\<Name> edition].

  This string is output for information purposes only, doesn't affect the successful installation of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in any way.

## Full-Text Search

Not all filters are available with this release, including filters for Microsoft Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md#filters).

## SQL Server Integration Services (SSIS)

The `mssql-server-is` package isn't supported on SUSE Linux Enterprise Server (SLES). The package is supported on Ubuntu, and Red Hat Enterprise Linux (RHEL).

[!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] packages can use ODBC connections on Linux. This functionality was tested with the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and the MySQL ODBC drivers, but is also expected to work with any Unicode ODBC driver that observes the ODBC specification. At design time, you can provide either a DSN or a connection string to connect to the ODBC data; you can also use Windows authentication. For more info, see the [blog post announcing ODBC support on Linux](https://techcommunity.microsoft.com/t5/sql-server-integration-services/bg-p/SSIS).

The following features aren't supported in this release when you run SSIS packages on Linux:

- [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] Catalog database
- Scheduled package execution by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent
- Windows Authentication
- Third-party components
- Change data capture (CDC)
- [!INCLUDE [ssISnoversion](../includes/ssisnoversion-md.md)] Scale Out
- Azure Feature Pack for SSIS
- Hadoop and HDFS support
- Microsoft Connector for SAP BW

For a list of built-in SSIS components that aren't currently supported, or that are supported with limitations, see [Limitations and known issues for SSIS on Linux](sql-server-linux-ssis-known-issues.md#components).

For more info about SSIS on Linux, see the following articles:

- [Blog post announcing SSIS support for Linux](https://techcommunity.microsoft.com/t5/sql-server-integration-services/bg-p/SSIS).
- [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)
- [Extract, transform, and load data on Linux with SSIS](sql-server-linux-migrate-ssis.md)

## SQL Server Management Studio (SSMS)

The following limitations apply to [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)] on Windows connected to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux.

- Maintenance plans aren't supported.

- Management Data Warehouse (MDW) and the data collector in [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)] aren't supported.

- [!INCLUDE [ssManStudioFull](../includes/ssmanstudiofull-md.md)] UI components that have Windows Authentication or Windows event log options don't work with Linux. You can still use these features with other options, such as [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] logins.

- The number of log files to retain can't be modified.

## High availability and disaster recovery

**Applies to:** [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] only.

When you run [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on RHEL 9 as a confined application with SELinux enabled, Pacemaker clustering might not work as expected. You must install [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] as an unconfined application with SELinux turned on, to make use of Pacemaker clustering capabilities.


## Machine Learning Services

**Applies to:** [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] only.

For [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] packages for RHEL 9 and Ubuntu 22.04, there are some prerequisites to take into account with `cgroup-v1`, before installing Machine Learning Services.

#### [RHEL 9](#tab/rhel9)

1. As a prerequisite, `cgroup-v1` needs to be enabled as per [Using cgroupfs to manually manage cgroups Red Hat Enterprise Linux 9](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/9/html/managing_monitoring_and_updating_the_kernel/assembly_using-cgroupfs-to-manually-manage-cgroups_managing-monitoring-and-updating-the-kernel#proc_mounting-cgroups-v1_assembly_using-cgroupfs-to-manually-manage-cgroups) from Red Hat.

1. Then follow instructions to [install SQL Machine Learning Services](sql-server-linux-setup-machine-learning-sql-2022.md#install-runtimes-and-packages) as documented.

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

   Once you have enabled `cgroup-v1` for Ubuntu 22.04, follow the steps in [Install SQL Server 2022 Machine Learning Services (Python and R) on Linux](sql-server-linux-setup-machine-learning-sql-2022.md#install-runtimes-and-packages), to install and enable SQL Machine Learning Service for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] packages on Ubuntu 22.04.

---

## Related content

- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md)
- [Editions and supported features of [!INCLUDE[sssql22](../includes/sssql22-md.md)] on Linux](sql-server-linux-editions-and-components-2022.md)
- [Troubleshoot [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux](sql-server-linux-troubleshooting-guide.md)
