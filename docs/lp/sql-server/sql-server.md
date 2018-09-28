#SQL Server Overview

#Overview
###[What's new](https://docs.microsoft.com/sql/sql-server/what-s-new-in-sql-server-2017)
whats new in the latest version of sql server
###[SQL Server On-Premises](sql-server.md) 
sql server on premises overview 
###[SQL On Linux](https://docs.microsoft.com/sql/linux/sql-server-linux-overview) 
sql server on linux overview
###[SQL on Azure VM](https://docs.microsoft.com/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview)
SQL Server on azure vm overivew

testing some stuff

## Migrate to SQL Server
###[Installation](https://docs.microsoft.com/sql/database-engine/install-windows/installation-for-sql-server)
###[Upgrade](https://docs.microsoft.com/sql/database-engine/install-windows/upgrade-sql-server)
###[Configuration](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/server-configuration-options-sql-server)

## Load & move data
### [Replication](replication.md)
####[Transactional](https://docs.microsoft.com/en-us/sql/relational-databases/replication/transactional/transactional-replication)
moves data between continuously connected servers
####[Merge](https://docs.microsoft.com/en-us/sql/relational-databases/replication/merge/merge-replication) 
moves data between intermittently connected servers
####[Change data capture](https://docs.microsoft.com/sql/relational-databases/track-changes/about-change-data-capture-sql-server)
captures when data is changed
####[Change tracking](https://docs.microsoft.com/sql/relational-databases/track-changes/about-change-tracking-sql-server)
tracks when data is changed
###[Integration Services](https://docs.microsoft.com/sql/integration-services/sql-server-integration-services)
moves data using etl and stuff

## [Business continuity](business-continuity.md)
High availability is the concept of delivering data to your customers, even in the event of a disaster, This includes instance-level [failover clustering](https://docs.microsoft.com/sql/sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server)
, database-level [AlwaysOn availability groups](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server), [database mirroring](https://docs.microsoft.com/sql/database-engine/database-mirroring/database-mirroring-sql-server). 

Disaster recovery is when  you can recover your database after a disaster. FOr example, there is  [Log Shipping](https://docs.microsoft.com/sql/database-engine/log-shipping/about-log-shipping-sql-server) and [backup and restore](https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/back-up-and-restore-of-sql-server-database)

## [Security](security.md)
Security is all abotu keeping your data secure. You can do so by encrypting your data in flight using [ssl](https://docs.microsoft.com/en-us/sql/connect/jdbc/using-ssl-encryption?view=sql-server-2017) or at rest with [tde](https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/transparent-data-encryption?view=sql-server-2017). The newer versions of sql have a nifty feature called [https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/always-encrypted-database-engine?view=sql-server-2017]] 
Security is all abotu keeping your data secure. You can do so by encrypting your data in flight using [ssl](https://docs.microsoft.com/en-us/sql/connect/jdbc/using-ssl-encryption?view=sql-server-2017) or at rest with [tde](https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/transparent-data-encryption?view=sql-server-2017). The newer versions of sql have a nifty feature called [always encrypted]() 

## [Monitor and Tune](monitor-and-tune.md)
Performance is an important consideration when managing your SQL Server. There are things to consider such as [statistics](https://docs.microsoft.com/en-us/sql/relational-databases/statistics/statistics), and [indexes](https://docs.microsoft.com/en-us/sql/relational-databases/indexes/clustered-and-nonclustered-indexes-described). You can maintain both by creating [maintenance plans](https://docs.microsoft.com/en-us/sql/relational-databases/maintenance-plans/maintenance-plans)

[SQL Profiler](https://docs.microsoft.com/sql/tools/sql-server-profiler/sql-server-profiler)
[Extended Events](https://docs.microsoft.com/sql/relational-databases/extended-events/extended-events)
[Statistics](https://docs.microsoft.com/en-us/sql/relational-databases/statistics/statistics)
[Indexes](https://docs.microsoft.com/en-us/sql/relational-databases/indexes/clustered-and-nonclustered-indexes-described)
[Maintenance plans](https://docs.microsoft.com/en-us/sql/relational-databases/maintenance-plans/maintenance-plans)
[In-memory OLTP tables](https://docs.microsoft.com/en-us/sql/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization)



##[Tutorials](https://docs.microsoft.com/sql/sql-server/tutorials-for-sql-server-2016)
###[Database Engine](https://docs.microsoft.com/sql/relational-databases/database-engine-tutorials)
###[Analysis Services](https://docs.microsoft.com/sql/analysis-services/analysis-services-tutorials-ssas)
###[Enterprise Info Management](https://msdn.microsoft.com/library/8745dc80-193d-4de0-9f17-ba648ab1e81c)
###[Integration Services](https://docs.microsoft.com/sql/integration-services/integration-services-tutorials)
###[Replication](https://docs.microsoft.com/sql/relational-databases/replication/replication-tutorials)
###[Reporting Services](https://docs.microsoft.com/sql/reporting-services/reporting-services-tutorials-ssrs)
###[SQL Machine Learning](https://docs.microsoft.com/sql/advanced-analytics/tutorials/machine-learning-services-tutorials)


##Samples
###[AdventureWorks](https://docs.microsoft.com/sql/samples/sql-samples-where-are)
###[World Wide Importers](https://docs.microsoft.com/sql/samples/wide-world-importers-what-is)
###[Code sampes on Github](https://github.com/Microsoft/sql-server-samples/tree/master/samples)





##Reference
###[Transact-SQL (T-SQL)](https://review.docs.microsoft.com/sql/t-sql/language-reference)
###[DMVs](https://docs.microsoft.com/sql/relational-databases/system-dynamic-management-views/system-dynamic-management-views)
###[Powershell](https://docs.microsoft.com/sql/powershell/sql-server-powershell)
###[xQuery](https://docs.microsoft.com/sql/xquery/xquery-language-reference-sql-server)
###[System stored procedures](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/system-stored-procedures-transact-sql)
###[System catalogs](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql)

##Resource
### [Troubleshooters](troubleshooters.md)
#### [Replication](https://docs.microsoft.com/sql/troubleshooters/replication/troubleshoot-tran-repl-errors)
#### Performance
#### Replication
#### High availability
#### Connectivity

