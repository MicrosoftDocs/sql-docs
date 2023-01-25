---
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2022
ms.service: sql
ms.subservice: linux
ms.topic: include
---
The following sections describe known issues with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] on Linux.

### General

- The length of the hostname where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed needs to be 15 characters or less.

  - **Resolution**: Change the name in `/etc/hostname` to something 15 characters long or less.

- Manually setting the system time backwards in time will cause [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to stop updating the internal system time within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

  - **Resolution**: Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- Only single instance installations are supported.

  - **Resolution**: If you want to have more than one instance on a given host, consider using VMs or Docker containers.

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager can't connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux.

- The default language of the **sa** login is English.

  - **Resolution**: Change the language of the **sa** login with the `ALTER LOGIN` statement.

- OLEDB provider logs the following warning: `Failed to verify the Authenticode signature of 'C:\binn\msoledbsql.dll'. Signature verification of SQL Server DLLs will be skipped. Genuine copies of SQL Server are signed. Failure to verify the Authenticode signature might indicate that this is not an authentic release of SQL Server. Install a genuine copy of SQL Server or contact customer support.`

  - **Resolution**: No action is required. The OLEDB provider is signed using SHA256. SQL Server Database engine doesn't validate the signed .dll correctly.

### Databases

- The `master` database can't be moved with the **mssql-conf** utility. Other system databases can be moved with **mssql-conf**.

- When restoring a database that was backed up on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, you must use the `WITH MOVE` clause in the Transact-SQL statement.

- Certain algorithms (cipher suites) for Transport Layer Security (TLS) don't work properly with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. This results in connection failures when attempting to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and problems establishing connections between replicas in high availability groups.

  - **Resolution**: Modify the `mssql.conf` configuration script for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux to disable problematic cipher suites, by doing the following:

    1. Add the following to `/var/opt/mssql/mssql.conf`.

       ```ini
       [network]
       tlsciphers=AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA:!ECDHE-RSA-AES128-GCM-SHA256:!ECDHE-RSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES256-GCM-SHA384:!ECDHE-ECDSA-AES128-GCM-SHA256:!ECDHE-ECDSA-AES256-SHA384:!ECDHE-ECDSA-AES128-SHA256:!ECDHE-ECDSA-AES256-SHA:!ECDHE-ECDSA-AES128-SHA:!ECDHE-RSA-AES256-SHA384:!ECDHE-RSA-AES128-SHA256:!ECDHE-RSA-AES256-SHA:!ECDHE-RSA-AES128-SHA:!DHE-RSA-AES256-GCM-SHA384:!DHE-RSA-AES128-GCM-SHA256:!DHE-RSA-AES256-SHA:!DHE-RSA-AES128-SHA:!DHE-DSS-AES256-SHA256:!DHE-DSS-AES128-SHA256:!DHE-DSS-AES256-SHA:!DHE-DSS-AES128-SHA:!DHE-DSS-DES-CBC3-SHA:!NULL-SHA256:!NULL-SHA
       ```

       > [!NOTE]  
       > In the preceding code, `!` negates the expression. This tells OpenSSL to not use the following cipher suite.

    1. Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the following command.

       ```bash
       sudo systemctl restart mssql-server
       ```

- [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] databases on Windows that use In-memory OLTP can't be restored to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux. To restore a [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] database that uses In-memory OLTP, first upgrade the databases to a newer version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows, before moving them to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux, via backup/restore or detach/attach.

- User permission `ADMINISTER BULK OPERATIONS` is not supported on Linux at this time.

- TDE-compressed backups that are made using [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 16 and later cannot be restored to previous CU versions of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. For more information, see [FIX: Error 3241 occurs during executing RESTORE LOG or RESTORE DATABASE](https://support.microsoft.com/help/5014298).

  Transparent Data Encryption (TDE)-compressed backups that are made using previous CU versions of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] can still be restored by using [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 16 and later versions.

### <a id="networking"></a> Network

Features that involve outbound TCP connections from the `sqlservr` process, such as linked servers, PolyBase, or availability groups, might not work if both the following conditions are met:

1. The target server is specified as a hostname and not an IP address.

1. The source instance has IPv6 disabled in the kernel. To verify if your system has IPv6 enabled in the kernel, all the following tests must pass:

   - `cat /proc/cmdline` will print the boot cmdline of the current kernel. The output must not contain `ipv6.disable=1`.
   - The `/proc/sys/net/ipv6/` directory must exist.
   - A C program that calls `socket(AF_INET6, SOCK_STREAM, IPPROTO_IP)` should succeed - the syscall must return an `fd != -1` and not fail with `EAFNOSUPPORT`.

The exact error depends on the feature. For linked servers, this manifests as a login timeout error. For availability groups, the `ALTER AVAILABILITY GROUP JOIN` DDL on the secondary will fail after 5 minutes with a `download configuration timeout` error.

To work around this issue, do one of the following:

1. Use IPs instead of host names to specify the target of the TCP connection.

1. Enable IPv6 in the kernel by removing `ipv6.disable=1` from the boot command line. The way to do this depends on the Linux distribution and the bootloader, such as **grub**. If you want IPv6 to be disabled, you can still disable it by setting `net.ipv6.conf.all.disable_ipv6 = 1` in the `sysctl` configuration (for example, `/etc/sysctl.conf`). This will still prevent the system's network adapter from getting an IPv6 address, but allow the `sqlservr` features to work.

#### Network File System (NFS)

If you use Network File System (NFS) remote shares in production, note the following support requirements:

- Use NFS version 4.2 or higher. Older versions of NFS don't support required features, such as `fallocate` and sparse file creation, common to modern file systems.

- Locate only the `/var/opt/mssql` directories on the NFS mount. Other files, such as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] system binaries, aren't supported.

- Ensure that NFS clients use the `nolock` option when mounting the remote share.

### Localization

- If your locale isn't English (`en_us`) during setup, you must use UTF-8 encoding in your bash session/terminal. If you use ASCII encoding, you might see an error similar to the following:

  ```output
  UnicodeEncodeError: 'ascii' codec can't encode character u'\xf1' in position 8: ordinal not in range(128)
  ```

  If you can't use UTF-8 encoding, run setup using the `MSSQL_LCID` environment variable to specify your language choice.

  ```bash
  sudo MSSQL_LCID=<LcidValue> /opt/mssql/bin/mssql-conf setup
  ```

- When running `mssql-conf setup`, and performing a non-English installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], incorrect extended characters are displayed after the localized text, "Configuring SQL Server...". Or, for non-Latin based installations, the sentence might be missing completely. The missing sentence should display the following localized string:

  `The licensing PID was successfully processed. The new edition is [<Name> edition]`.

  This string is output for information purposes only, and the next [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Cumulative Update will address this for all languages. This doesn't affect the successful installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in any way.

### Full-Text Search

Not all filters are available with this release, including filters for Microsoft Office documents. For a list of supported filters, see [Install SQL Server Full-Text Search on Linux](../sql-server-linux-setup-full-text-search.md#filters).

### <a id="ssis"></a> SQL Server Integration Services (SSIS)

The **mssql-server-is** package isn't supported on SUSE in this release. It's currently supported on Ubuntu and on Red Hat Enterprise Linux (RHEL).

[!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] packages can use ODBC connections on Linux. This functionality has been tested with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the MySQL ODBC drivers, but is also expected to work with any Unicode ODBC driver that observes the ODBC specification. At design time, you can provide either a DSN or a connection string to connect to the ODBC data; you can also use Windows authentication. For more info, see the [blog post announcing ODBC support on Linux](https://blogs.msdn.microsoft.com/ssis/2017/06/16/odbc-is-supported-in-ssis-on-linux-ssis-helsinki-ctp2-1-refresh/).

The following features aren't supported in this release when you run SSIS packages on Linux:

- [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Catalog database
- Scheduled package execution by SQL Agent
- Windows Authentication
- Third-party components
- Change Data Capture (CDC)
- [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Scale Out
- Azure Feature Pack for SSIS
- Hadoop and HDFS support
- Microsoft Connector for SAP BW

For a list of built-in SSIS components that aren't currently supported, or that are supported with limitations, see [Limitations and known issues for SSIS on Linux](../sql-server-linux-ssis-known-issues.md#components).

For more info about SSIS on Linux, see the following articles:

- [Blog post announcing SSIS support for Linux](https://blogs.msdn.microsoft.com/ssis/2017/05/17/ssis-helsinki-is-available-in-sql-server-vnext-ctp2-1/).
- [Install SQL Server Integration Services (SSIS) on Linux](../sql-server-linux-setup-ssis.md)
- [Extract, transform, and load data on Linux with SSIS](../sql-server-linux-migrate-ssis.md)

### SQL Server Management Studio (SSMS)

The following limitations apply to [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] on Windows connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Linux.

- Maintenance plans aren't supported.

- Management Data Warehouse (MDW) and the data collector in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] aren't supported.

- [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] UI components that have Windows Authentication or Windows event log options don't work with Linux. You can still use these features with other options, such as SQL logins.

- Number of log files to retain can't be modified.
