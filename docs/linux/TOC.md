# [About SQL Server on Linux](sql-server-linux-overview.md)

# Overview
## [Release notes](sql-server-linux-release-notes.md)
## [What's new in this release?](sql-server-linux-whats-new.md)
## [New and updated articles](new-updated-linux.md)
## [Editions and supported features](sql-server-linux-editions-and-components-2017.md)

# Quickstarts
## [Install & Connect - Red Hat](quickstart-install-connect-red-hat.md)
## [Install & Connect - SUSE](quickstart-install-connect-suse.md)
## [Install & Connect - Ubuntu](quickstart-install-connect-ubuntu.md)
## [Run & Connect - Docker](quickstart-install-connect-docker.md)
## [Provision a SQL VM in Azure](/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=%2fsql%2flinux%2ftoc.json)
## [Run & Connect - Cloud](quickstart-install-connect-clouds.md)

# Tutorials
## [1_Migrate from Windows](sql-server-linux-migrate-restore-database.md)
## [2_Migrate from Oracle](../ssma/oracle/sql-server-linux-convert-from-oracle.md?toc=%2fsql%2flinux%2ftoc.json)
## [3_Migrate to Docker](tutorial-restore-backup-in-sql-server-container.md)
## [4_Create a job](sql-server-linux-run-sql-server-agent-job.md)
## [5_Setup AD Authentication](sql-server-linux-active-directory-authentication.md)
## [6_Create failover cluster instance](sql-server-linux-shared-disk-cluster-configure.md)
### [iSCSI](sql-server-linux-shared-disk-cluster-configure-iscsi.md)
### [NFS](sql-server-linux-shared-disk-cluster-configure-nfs.md)
### [SMB](sql-server-linux-shared-disk-cluster-configure-smb.md)
## [7_Deploy a Pacemaker cluster](sql-server-linux-deploy-pacemaker-cluster.md)
## [8_Create and configure availability groups](sql-server-linux-create-availability-group.md)

# Concepts
## Install
### [Install SQL Server](sql-server-linux-setup.md)
### [Install SQL Server tools](sql-server-linux-setup-tools.md)
### [Install SQL Server Agent](sql-server-linux-setup-sql-agent.md)
### [Install SQL Server Full-Text Search](sql-server-linux-setup-full-text-search.md)
### [Install SQL Server Integration Services](sql-server-linux-setup-ssis.md)
### [Register GA repository](sql-server-linux-change-repo.md)

## Configure
### [Configure with mssql-conf](sql-server-linux-configure-mssql-conf.md)
### [Environment variables](sql-server-linux-configure-environment-variables.md)
### [Configure Docker containers](sql-server-linux-configure-docker.md)
### [Customer Feedback](sql-server-linux-customer-feedback.md)

## [Develop](sql-server-linux-develop-overview.md)
### [Connectivity libraries](sql-server-linux-develop-connectivity-libraries.md)
### [Use Visual Studio Code](sql-server-linux-develop-use-vscode.md)
### [Use SSMS](sql-server-linux-develop-use-ssms.md)
### [Use SSDT](sql-server-linux-develop-use-ssdt.md)

## [Manage](sql-server-linux-management-overview.md)
### [Use SSMS to manage](sql-server-linux-manage-ssms.md)
### [Use PowerShell to manage](sql-server-linux-manage-powershell.md)
### [Use log shipping](sql-server-linux-use-log-shipping.md)
### [Use DB Mail and email alerts](sql-server-linux-db-mail-sql-agent.md)
### [Configure multiple subnets for availability](sql-server-linux-configure-multiple-subnet.md)

## [Migrate](sql-server-linux-migrate-overview.md)
### [Export and import a BACPAC from Windows](sql-server-linux-migrate-ssms.md)
### [Migrate with SQL Server Migration Assistant](sql-server-linux-migrate-ssma.md)
### [Bulk copy with bcp](sql-server-linux-migrate-bcp.md)

## [Extract, transform, load](sql-server-linux-migrate-ssis.md)
### [Limitations and known issues](sql-server-linux-ssis-known-issues.md)
### [Configure SSIS](sql-server-linux-configure-ssis.md)
### [Schedule SSIS packages](sql-server-linux-schedule-ssis-packages.md)

## [Configure business continuity](sql-server-linux-business-continuity-dr.md)
### [Availability basics](sql-server-linux-ha-basics.md)
### [Backup and restore](sql-server-linux-backup-and-restore-database.md)
#### [Virtual Device Interface - Linux](sql-server-linux-backup-vdi-specification.md)
### [Failover cluster instance](sql-server-linux-shared-disk-cluster-concepts.md)
#### [Red Hat Enterprise Linux (RHEL)]()
##### [Configure (HA add-on)](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)
##### [Operate (HA add-on)](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)
#### [SUSE Linux Enterprise Server (SLES)]()
##### [Configure (HA add-on)](sql-server-linux-shared-disk-cluster-sles-configure.md)
### [Availability groups](sql-server-linux-availability-group-overview.md)
#### [Create for high availability](sql-server-linux-availability-group-ha.md)
##### [Configure AG](sql-server-linux-availability-group-configure-ha.md)
##### [Configure on RHEL](sql-server-linux-availability-group-cluster-rhel.md)
##### [Configure on SLES](sql-server-linux-availability-group-cluster-sles.md)
##### [Configure on Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md)
##### [Operate](sql-server-linux-availability-group-failover-ha.md)
#### [Create for read-scale only]()
##### [Configure AG](sql-server-linux-availability-group-configure-rs.md)

## [Security](sql-server-linux-security-overview.md)
### [Get started with security features](sql-server-linux-security-get-started.md)
### [Encrypting connections](sql-server-linux-encrypted-connections.md)

## Performance
### [Best practices](sql-server-linux-performance-best-practices.md)
### [Get started with performance features](sql-server-linux-performance-get-started.md)

# Samples
## Unattended install
### [Red Hat Enterprise Linux (RHEL)](sample-unattended-install-redhat.md)
### [SUSE Linux Enterprise Server (SLES)](sample-unattended-install-suse.md)
### [Ubuntu](sample-unattended-install-ubuntu.md)

# Resources
## [FAQ](sql-server-linux-faq.md)
## [Troubleshoot](sql-server-linux-troubleshooting-guide.md)
## [SQL Server Documentation](../sql-server/sql-server-technical-documentation.md)
## Partners
### [Monitoring](../sql-server/partner-monitor-sql-server.md)
### [High availability and disaster recovery](../sql-server/partner-hadr-sql-server.md)
### [Management](../sql-server/partner-management-sql-server.md)
### [Development](../sql-server/partner-dev-sql-server.md)
## [DBA Stack Exchange](https://dba.stackexchange.com/questions/tagged/sql-server)
## [Stack Overflow](http://stackoverflow.com/questions/tagged/sql-server)
## [MSDN Forums](https://social.msdn.microsoft.com/Forums/en-US/home?category=sqlserver)
## [Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback)
## [Reddit](https://www.reddit.com/r/SQLServer)
