---
title: Apache Spark & Apache Hadoop configuration properties
titleSuffix: SQL Server Big Data Clusters
description: Reference article for configuration properties for Apache Spark & Apache Hadoop (HDFS).
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: rahul.ajmera
ms.date: 03/23/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# Apache Spark & Apache Hadoop (HDFS) configuration properties

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Big Data Clusters supports deployment time and post-deployment time configuration of Apache Spark and Hadoop components at the service and resource scopes. Big Data Clusters uses the same default configuration values as the respective open source project for most settings. The settings that we do change are listed below along with a description and their default value. Other than the gateway resource, there is no difference between settings that are configurable at the service scope and the resource scope.

You can find all possible configurations and the defaults for each at the associated Apache documentation site:
- Apache Spark: https://spark.apache.org/docs/latest/configuration.html
- Apache Hadoop:
  - HDFS HDFS-Site: https://hadoop.apache.org/docs/r2.7.1/hadoop-project-dist/hadoop-hdfs/hdfs-default.xml
  - HDFS Core-Site: https://hadoop.apache.org/docs/r2.8.0/hadoop-project-dist/hadoop-common/core-default.xml  
  - Yarn: https://hadoop.apache.org/docs/r3.1.1/hadoop-yarn/hadoop-yarn-site/ResourceModel.html
- Hive: https://cwiki.apache.org/confluence/display/Hive/Configuration+Properties#ConfigurationProperties-MetaStore
- Livy: https://github.com/cloudera/livy/blob/master/conf/livy.conf.template
- Apache Knox Gateway: https://knox.apache.org/books/knox-0-14-0/user-guide.html#Gateway+Details

The settings we do not support configuring are also listed below.

> [!NOTE]
> To include Spark in the Storage pool, set the boolean value `includeSpark` in the `bdc.json` configuration file at `spec.resources.storage-0.spec.settings.spark`. See [Configure Apache Spark and Apache Hadoop in Big Data Clusters](configure-spark-hdfs.md) for instructions.


##  Big Data Clusters-specific default Spark settings
The Spark settings below are those that have BDC-specific defaults but are user configurable. System-managed settings are not included.

| Setting Name                                                                                 | Description                                                                                                                                                           | Type   | Default Value                                                                                                                              |
| ------------------------------------------------------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------ | ------------------------------------------------------------------------------------------------------------------------------------------ |
| capacity-scheduler.yarn.scheduler.capacity.maximum-applications                      | Maximum number of applications in the system which can be concurrently active both running and pending.                                                               | int    | 10000                                                                                                                                      |
| capacity-scheduler.yarn.scheduler.capacity.resource-calculator                       | The ResourceCalculator implementation to be used to compare Resources in the scheduler.                                                                               | string | org.apache.hadoop.yarn.util.resource.DominantResourceCalculator                                                                            |
| capacity-scheduler.yarn.scheduler.capacity.root.queues                               | The capacity scheduler with predefined queue called root.                                                                                                              | string | default                                                                                                                                    |
| capacity-scheduler.yarn.scheduler.capacity.root.default.capacity                     | Queue capacity in percentage (%) as absolute resource queue minimum capacity for root queue.                                                                          | int    | 100                                                                                                                                        |
| spark-defaults-conf.spark.driver.cores                                               | Number of cores to use for the driver process, only in cluster mode.                                                                                                  | int    | 1                                                                                                                                          |
| spark-defaults-conf.spark.driver.memoryOverhead                                      | The amount of off-heap memory to be allocated per driver in cluster mode.                                                                                             | int    | 384                                                                                                                                        |
| spark-defaults-conf.spark.executor.instances                                         | The number of executors for static allocation.                                                                                                                        | int    | 1                                                                                                                                          |
| spark-defaults-conf.spark.executor.cores                                             | The number of cores to use on each executor.                                                                                                                          | int    | 1                                                                                                                                          |
| spark-defaults-conf.spark.driver.memory                                              | Amount of memory to use for the driver process.                                                                                                                       | string | 1g                                                                                                                                         |
| spark-defaults-conf.spark.executor.memory                                            | Amount of memory to use per executor process.                                                                                                                         | string | 1g                                                                                                                                         |
| spark-defaults-conf.spark.executor.memoryOverhead                                    | The amount of off-heap memory to be allocated per executor.                                                                                                           | int    | 384                                                                                                                                        |
| yarn-site.yarn.nodemanager.resource.memory-mb                                        | Amount of physical memory, in MB, that can be allocated for containers.                                                                                               | int    | 8192                                                                                                                                       |
| yarn-site.yarn.scheduler.maximum-allocation-mb                                       | The maximum allocation for every container request at the resource manager.                                                                                           | int    | 8192                                                                                                                                       |
| yarn-site.yarn.nodemanager.resource.cpu-vcores                                       | Number of CPU cores that can be allocated for containers.                                                                                                             | int    | 32                                                                                                                                         |
| yarn-site.yarn.scheduler.maximum-allocation-vcores                                   | The maximum allocation for every container request at the resource manager, in terms of virtual CPU cores.                                                            | int    | 8                                                                                                                                          |
| yarn-site.yarn.nodemanager.linux-container-executor.secure-mode.pool-user-count      | The number of pool users for the linux container executor in secure mode.                                                                                             | int    | 6                                                                                                                                          |
| yarn-site.yarn.scheduler.capacity.maximum-am-resource-percent                        | Maximum percent of resources in the cluster that can be used to run application masters.                                                                              | float  | 0.1                                                                                                                                        |
| yarn-site.yarn.nodemanager.container-executor.class                                  | Container executors for a specific operating system(s).                                                                                                               | string | org.apache.hadoop.yarn.server.nodemanager.LinuxContainerExecutor                                                                           |
| capacity-scheduler.yarn.scheduler.capacity.root.default.user-limit-factor            | The multiple of the queue capacity which can be configured to allow a single user to acquire more resources.                                                          | int    | 1                                                                                                                                          |
| capacity-scheduler.yarn.scheduler.capacity.root.default.maximum-capacity             | Maximum queue capacity in percentage (%) as a float OR as absolute resource queue maximum capacity. Setting this value to -1 sets maximum capacity to 100%.           | int    | 100                                                                                                                                        |
| capacity-scheduler.yarn.scheduler.capacity.root.default.state                        | State of queue can be one of Running or Stopped.                                                                                                                      | string | RUNNING                                                                                                                                    |
| capacity-scheduler.yarn.scheduler.capacity.root.default.maximum-application-lifetime | Maximum lifetime of an application which is submitted to a queue in seconds. Any value less than or equal to zero will be considered as disabled.                     | int    | -1                                                                                                                                         |
| capacity-scheduler.yarn.scheduler.capacity.root.default.default-application-lifetime | Default lifetime of an application which is submitted to a queue in seconds. Any value less than or equal to zero will be considered as disabled.                     | int    | -1                                                                                                                                         |
| capacity-scheduler.yarn.scheduler.capacity.node-locality-delay                       | Number of missed scheduling opportunities after which the CapacityScheduler attempts to schedule rack-local containers.                                               | int    | 40                                                                                                                                         |
| capacity-scheduler.yarn.scheduler.capacity.rack-locality-additional-delay            | Number of additional missed scheduling opportunities over the node-locality-delay ones, after which the CapacityScheduler attempts to schedule off-switch containers. | int    | -1                                                                                                                                         |
| hadoop-env.HADOOP_HEAPSIZE_MAX                                                       | Default maximum heap size of all Hadoop JVM processes.                                                                                                                 | int    | 2048                                                                                                                                       |
| yarn-env.YARN_RESOURCEMANAGER_HEAPSIZE                                               | Heap size of Yarn ResourceManager.                                                                                                                                     | int    | 2048                                                                                                                                       |
| yarn-env.YARN_NODEMANAGER_HEAPSIZE                                                   | Heap size of Yarn NodeManager.                                                                                                                                         | int    | 2048                                                                                                                                       |
| mapred-env.HADOOP_JOB_HISTORYSERVER_HEAPSIZE                                         | Heap size of Hadoop Job HistoryServer.                                                                                                                                 | int    | 2048                                                                                                                                       |
| hive-env.HADOOP_HEAPSIZE                                                             | Heap size of Hadoop for Hive.                                                                                                                                          | int    | 2048                                                                                                                                       |
| livy-conf.livy.server.session.timeout-check                                          | Check for Livy server session timeout.                                                                                                                                | bool   | true                                                                                                                                       |
| livy-conf.livy.server.session.timeout-check.skip-busy                                | Skip-busy for Check for Livy server session timeout.                                                                                                                  | bool   | true                                                                                                                                       |
| livy-conf.livy.server.session.timeout                                                | Timeout for livy server session in (ms/s/m \| min/h/d/y).                                                                                                              | string | 2h                                                                                                                                         |
| livy-conf.livy.server.yarn.poll-interval                                             | Polling interval for yarn in Livy server in (ms/s/m \| min/h/d/y).                                                                                                     | string | 500ms                                                                                                                                      |
| livy-conf.livy.rsc.jars                                                              | Livy RSC jars.                                                                                                                                                        | string | local:/opt/livy/rsc-jars/livy-api.jar,local:/opt/livy/rsc-jars/livy-rsc.jar,local:/opt/livy/rsc-jars/netty-all.jar                         |
| livy-conf.livy.repl.jars                                                             | Livy repl jars.                                                                                                                                                       | string | local:/opt/livy/repl_2.11-jars/livy-core.jar,local:/opt/livy/repl_2.11-jars/livy-repl.jar,local:/opt/livy/repl_2.11-jars/commons-codec.jar |
| livy-conf.livy.rsc.sparkr.package                                                    | Livy RSC SparkR package.                                                                                                                                              | string | hdfs:///system/livy/sparkr.zip                                                                                                             |
| livy-env.LIVY_SERVER_JAVA_OPTS                                                       | Livy Server Java Options.                                                                                                                                             | string | -Xmx2g                                                                                                                                     |
| spark-defaults-conf.spark.r.backendConnectionTimeout                                 | Connection timeout set by R process on its connection to RBackend in seconds.                                                                                         | int    | 86400                                                                                                                                      |
| spark-defaults-conf.spark.pyspark.python                                             | Python option for spark.                                                                                                                                              | string | /opt/bin/python3                                                                                                                           |
| spark-defaults-conf.spark.yarn.jars                                                  | Yarn jars.                                                                                                                                                            | string | local:/opt/spark/jars/*                                                                                                                    |
| spark-history-server-conf.spark.history.fs.cleaner.maxAge                            | Maximum age of job history files before they are deleted by the filesystem history cleaner in (ms/s/m \| min/h/d/y).                                                   | string | 7d                                                                                                                                         |
| spark-history-server-conf.spark.history.fs.cleaner.interval                          | Interval of cleaner for spark history in (ms/s/m \| min/h/d/y).                                                                                                        | string | 12h                                                                                                                                        |
| hadoop-env.HADOOP_CLASSPATH                                                          | Sets the additional Hadoop classpath.                                                                                                                                 | string |                                                                                                                                            |
| spark-env.SPARK_DAEMON_MEMORY                                                        | Spark Daemon Memory.                                                                                                                                                  | string | 2g                                                                                                                                         |
| yarn-site.yarn.log-aggregation.retain-seconds                                        | When log aggregation in enabled, this property determines the number of seconds to retain logs.                                                                       | int    | 604800                                                                                                                                     |
| yarn-site.yarn.nodemanager.log-aggregation.compression-type                          | Compression type for log aggregation for Yarn NodeManager.                                                                                                            | string | gz                                                                                                                                         |
| yarn-site.yarn.nodemanager.log-aggregation.roll-monitoring-interval-seconds          | Interval seconds for Roll Monitoring in NodeManager Log Aggregation.                                                                                                  | int    | 3600                                                                                                                                       |
| yarn-site.yarn.scheduler.minimum-allocation-mb                                       | The minimum allocation for every container request at the Resource Manager, in MBs.                                                                                   | int    | 512                                                                                                                                        |
| yarn-site.yarn.scheduler.minimum-allocation-vcores                                   | The minimum allocation for every container request at the Resource Manager in terms of virtual CPU cores.                                                             | int    | 1                                                                                                                                          |
| yarn-site.yarn.nm.liveness-monitor.expiry-interval-ms                                | How long to wait until a node manager is considered dead.                                                                                                             | int    | 180000                                                                                                                                     |
| yarn-site.yarn.resourcemanager.zk-timeout-ms                                         | 'ZooKeeper' session timeout in milliseconds.                                                                                                                          | int    | 40000                                                                                                                                      |
| capacity-scheduler.yarn.scheduler.capacity.root.default.acl_application_max_priority | The ACL of who can submit applications with configured priority. For e.g, [user={name} group={name} max_priority={priority} default_priority={priority}].             | string | *                                                                                                                                          |
| includeSpark                                                                         | Boolean to configure whether or not Spark jobs can run in the Storage pool.                                                                                           | bool   | true                                                                                                                                       |
| enableSparkOnK8s                                                                     | Boolean to configure whether or not enable Spark on K8s, which adding containers for K8s in Spark head.                                                               | bool   | false                                                                                                                                      |
| sparkVersion                                                                         | The version of Spark                                                                                                                                                  | string | 2.4                                                                                                                                        |
| spark-env.PYSPARK_ARCHIVES_PATH                                                      | Path to pyspark archive jars used in spark jobs.                                                                                                                      | string | local:/opt/spark/python/lib/pyspark.zip,local:/opt/spark/python/lib/py4j-0.10.7-src.zip                                                    |

The following sections list the unsupported configurations.

##  Big Data Clusters-specific default HDFS settings
The HDFS settings below are those that have BDC-specific defaults but are user configurable. System-managed settings are not included.

| Setting Name                                                                       | Description                                                                                         | Type   | Default Value                                                                    |
| -------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------- | ------ | -------------------------------------------------------------------------------------- |
| hdfs-site.dfs.replication                                                  | Default Block Replication.                                                                          | int    | 2                                                                                      |
| hdfs-site.dfs.namenode.provided.enabled                                    | Enables the name node to handle provided storages.                                                   | bool   | true                                                                                   |
| hdfs.site.dfs.namenode.mount.acls.enabled |  Set to true to inherit ACLs (Access Control Lists) from remote stores during mount.| bool | false |
| hdfs-site.dfs.datanode.provided.enabled                                    | Enables the data node to handle provided storages.                                                   | bool   | true                                                                                   |
| hdfs-site.dfs.datanode.provided.volume.lazy.load                           | Enable lazy load in data node for provided storages.                                                 | bool   | true                                                                                   |
| hdfs-site.dfs.provided.aliasmap.inmemory.enabled                           | Enable in-memory alias map for provided storages.                                                     | bool   | true                                                                                   |
| hdfs-site.dfs.provided.aliasmap.class                                      | The class that is used to specify the input format of the blocks on provided storages.              | string | org.apache.hadoop.hdfs.server.common.blockaliasmap.impl.InMemoryLevelDBAliasMapClient  |
| hdfs-site.dfs.namenode.provided.aliasmap.class                             | The class that is used to specify the input format of the blocks on provided storages for namenode. | string | org.apache.hadoop.hdfs.server.common.blockaliasmap.impl.NamenodeInMemoryAliasMapClient |
| hdfs-site.dfs.provided.aliasmap.load.retries                               | Number of retries on the datanode to load the provided aliasmap.                                    | int    | 0                                                                                      |
| hdfs-site.dfs.provided.aliasmap.inmemory.batch-size                        | The batch size when iterating over the database backing the aliasmap.                               | int    | 500                                                                                    |
| hdfs-site.dfs.datanode.provided.volume.readthrough                         | Enable readthrough for provided storages in datanode.                                               | bool   | true                                                                                   |
| hdfs-site.dfs.provided.cache.capacity.mount                                | Enable cache capacity mount for provided storages.                                                  | bool   | true                                                                                   |
| hdfs-site.dfs.provided.overreplication.factor                              | Overreplication factor for provided storages.  Number of cache-blocks on BDC created per remote HDFS block.                                                     | float  | 1                                                                                      |
| hdfs-site.dfs.provided.cache.capacity.fraction                             | Cache capacity fraction for provided storage. The fraction of the total capacity in the cluster that can be used to cache data from Provided stores.                                                      | float  | 0.01                                                                                   |
| hdfs-site.dfs.provided.cache.capacity.bytes | Cluster capacity to use as cache space for Provided blocks, in bytes. | int | -1 | 
| hdfs-site.dfs.ls.limit                                                     | Limit the number of files printed by ls.                                                            | int    | 500                                                                                    |
| hdfs-env.HDFS_NAMENODE_OPTS                                                | HDFS Namenode options.                                                                              | string | -Dhadoop.security.logger=INFO,RFAS -Xmx2g                                              |
| hdfs-env.HDFS_DATANODE_OPTS                                                | HDFS Datanode Options.                                                                              | string | -Dhadoop.security.logger=ERROR,RFAS -Xmx2g                                             |
| hdfs-env.HDFS_ZKFC_OPTS                                                    | HDFS ZKFC Options.                                                                                  | string | -Xmx1g                                                                                 |
| hdfs-env.HDFS_JOURNALNODE_OPTS                                             | HDFS JournalNode Options.                                                                           | string | -Xmx2g                                                                                 |
| hdfs-env.HDFS_AUDIT_LOGGER                                                 | HDFS Audit Logger Options.                                                                          | string | INFO,RFAAUDIT                                                                          |
| core-site.hadoop.security.group.mapping.ldap.search.group.hierarchy.levels | Hierarchy levels for core site Hadoop LDAP Search group.                                            | int    | 10                                                                                     |
| core-site.fs.permissions.umask-mode                                        | Permission umask mode.                                                                              | string | 077                                                                                    |
| core-site.hadoop.security.kms.client.failover.max.retries                  | Max retries for client failover.                                                                    | int    | 20                                                                                     |
| zoo-cfg.tickTime                                       | Tick Time for 'ZooKeeper' config.                         | int    | 2000                |
| zoo-cfg.initLimit                                      | Init Time for 'ZooKeeper' config.                         | int    | 10                  |
| zoo-cfg.syncLimit                                      | Sync Time for 'ZooKeeper' config.                         | int    | 5                   |
| zoo-cfg.maxClientCnxns                                 | Max Client connections for 'ZooKeeper' config.            | int    | 60                  |
| zoo-cfg.minSessionTimeout                              | Minimum Session Timeout for 'ZooKeeper' config.           | int    | 4000                |
| zoo-cfg.maxSessionTimeout                              | Maximum Session Timeout for 'ZooKeeper' config.           | int    | 40000               |
| zoo-cfg.autopurge.snapRetainCount                      | Snap Retain count for Autopurge 'ZooKeeper' config.       | int    | 3                   |
| zoo-cfg.autopurge.purgeInterval                        | Purge interval for Autopurge 'ZooKeeper' config.          | int    | 0                   |
| zookeeper-java-env.JVMFLAGS                            | JVM Flags for Java environment in 'ZooKeeper'.            | string | -Xmx1G -Xms1G       |
| zookeeper-log4j-properties.zookeeper.console.threshold | Threshold for log4j console in 'ZooKeeper'.               | string | INFO                |
| zoo-cfg.zookeeper.request.timeout                      | Controls the 'ZooKeeper' request timeout in milliseconds. | int    | 40000               |
| kms-site.hadoop.security.kms.encrypted.key.cache.size | Cache size for encrypted key in hadoop kms. | int  | 500 |
    

##  Big Data Clusters-specific default Gateway settings
The Gateway settings below are those that have BDC-specific defaults but are user configurable. System-managed settings are not included. Gateway settings can only be configured at the **resource** scope.

| Setting Name                                          | Description                                            | Type   | Default Value |
| --------------------------------------------- | ------------------------------------------------------ | ------ | ------------------- |
| gateway-site.gateway.httpclient.socketTimeout | Socket Timeout for HTTP client in gateway in (ms/s/m). | string | 90s                 |
| gateway-site.sun.security.krb5.debug          | Debug for Kerberos Security.                           | bool   | true                |
| knox-env.KNOX_GATEWAY_MEM_OPTS                | Knox Gateway Memory options.                           | string | -Xmx2g              |

## Unsupported Spark configurations

The following `spark` configurations are unsupported and cannot be changed in the context of the Big Data Cluster.

| Category  | Sub-Category               | File                       | Unsupported Configurations                                              |
|-----------|----------------------------|----------------------------|-------------------------------------------------------------------------|
|           | yarn-site                  | yarn-site.xml              | yarn.log-aggregation-enable                                             |
|           |                            |                            | yarn.log.server.url                                                     |
|           |                            |                            | yarn.nodemanager.pmem-check-enabled                                     |
|           |                            |                            | yarn.nodemanager.vmem-check-enabled                                     |
|           |                            |                            | yarn.nodemanager.aux-services                                           |
|           |                            |                            | yarn.resourcemanager.address                                            |
|           |                            |                            | yarn.nodemanager.address                                                |
|           |                            |                            | yarn.client.failover-no-ha-proxy-provider                               |
|           |                            |                            | yarn.client.failover-proxy-provider                                     |
|           |                            |                            | yarn.http.policy                                                        |
|           |                            |                            | yarn.nodemanager.linux-container-executor.secure-mode.use-pool-user     |
|           |                            |                            | yarn.nodemanager.linux-container-executor.secure-mode.pool-user-prefix  |
|           |                            |                            | yarn.nodemanager.linux-container-executor.nonsecure-mode.local-user     |
|           |                            |                            | yarn.acl.enable                                                         |
|           |                            |                            | yarn.admin.acl                                                          |
|           |                            |                            | yarn.resourcemanager.hostname                                           |
|           |                            |                            | yarn.resourcemanager.principal                                          |
|           |                            |                            | yarn.resourcemanager.keytab                                             |
|           |                            |                            | yarn.resourcemanager.webapp.spnego-keytab-file                          |
|           |                            |                            | yarn.resourcemanager.webapp.spnego-principal                            |
|           |                            |                            | yarn.nodemanager.principal                                              |
|           |                            |                            | yarn.nodemanager.keytab                                                 |
|           |                            |                            | yarn.nodemanager.webapp.spnego-keytab-file                              |
|           |                            |                            | yarn.nodemanager.webapp.spnego-principal                                |
|           |                            |                            | yarn.resourcemanager.ha.enabled                                         |
|           |                            |                            | yarn.resourcemanager.cluster-id                                         |
|           |                            |                            | yarn.resourcemanager.zk-address                                         |
|           |                            |                            | yarn.resourcemanager.ha.rm-ids                                          |
|           |                            |                            | yarn.resourcemanager.hostname.*                                         |
|           | capacity-scheduler         | capacity-scheduler.xml     | yarn.scheduler.capacity.root.acl_submit_applications                    |
|           |                            |                            | yarn.scheduler.capacity.root.acl_administer_queue                       |
|           |                            |                            | yarn.scheduler.capacity.root.default.acl_application_max_priority       |
|           | yarn-env                   | yarn-env.sh                |                                                                         |
|           | spark-defaults-conf        | spark-defaults.conf        | spark.yarn.archive                                                      |
|           |                            |                            | spark.yarn.historyServer.address                                        |
|           |                            |                            | spark.eventLog.enabled                                                  |
|           |                            |                            | spark.eventLog.dir                                                      |
|           |                            |                            | spark.sql.warehouse.dir                                                 |
|           |                            |                            | spark.sql.hive.metastore.version                                        |
|           |                            |                            | spark.sql.hive.metastore.jars                                           |
|           |                            |                            | spark.extraListeners                                                    |
|           |                            |                            | spark.metrics.conf                                                      |
|           |                            |                            | spark.ssl.enabled                                                       |
|           |                            |                            | spark.authenticate                                                      |
|           |                            |                            | spark.network.crypto.enabled                                            |
|           |                            |                            | spark.ssl.keyStore                                                      |
|           |                            |                            | spark.ssl.keyStorePassword  
|           |                            |                            | spark.ui.enabled                                                        |
|           | spark-env                  | spark-env.sh               | SPARK_NO_DAEMONIZE                                                      |
|           |                            |                            | SPARK_DIST_CLASSPATH                                                    |
|           | spark-history-server-conf  | spark-history-server.conf  | spark.history.fs.logDirectory                                           |
|           |                            |                            | spark.ui.proxyBase                                                      |
|           |                            |                            | spark.history.fs.cleaner.enabled                                        |
|           |                            |                            | spark.ssl.enabled                                                       |
|           |                            |                            | spark.authenticate                                                      |
|           |                            |                            | spark.network.crypto.enabled                                            |
|           |                            |                            | spark.ssl.keyStore                                                      |
|           |                            |                            | spark.ssl.keyStorePassword                                              |
|           |                            |                            | spark.history.kerberos.enabled                                          |
|           |                            |                            | spark.history.kerberos.principal                                        |
|           |                            |                            | spark.history.kerberos.keytab                                           |
|           |                            |                            | spark.ui.filters                                                        |
|           |                            |                            | spark.acls.enable                                                       |
|           |                            |                            | spark.history.ui.acls.enable                                            |
|           |                            |                            | spark.history.ui.admin.acls                                             |
|           |                            |                            | spark.history.ui.admin.acls.groups                                      |
|           | livy-conf                  | livy.conf                  | livy.keystore                                                           |
|           |                            |                            | livy.keystore.password                                                  |
|           |                            |                            | livy.spark.master                                                       |
|           |                            |                            | livy.spark.deploy-mode                                                  |
|           |                            |                            | livy.rsc.jars                                                           |
|           |                            |                            | livy.repl.jars                                                          |
|           |                            |                            | livy.rsc.pyspark.archives                                               |
|           |                            |                            | livy.rsc.sparkr.package                                                 |
|           |                            |                            | livy.repl.enable-hive-context                                           |
|           |                            |                            | livy.superusers                                                         |
|           |                            |                            | livy.server.auth.type                                                   |
|           |                            |                            | livy.server.launch.kerberos.keytab                                      |
|           |                            |                            | livy.server.launch.kerberos.principal                                   |
|           |                            |                            | livy.server.auth.kerberos.principal                                     |
|           |                            |                            | livy.server.auth.kerberos.keytab                                        |
|           |                            |                            | livy.impersonation.enabled                                              |
|           |                            |                            | livy.server.access-control.enabled                                      |
|           |                            |                            | livy.server.access-control.*                                            |
|           | livy-env                   | livy-env.sh                |                                                                         |
|           | hive-site                  | hive-site.xml              | javax.jdo.option.ConnectionURL                                          |
|           |                            |                            | javax.jdo.option.ConnectionDriverName                                   |
|           |                            |                            | javax.jdo.option.ConnectionUserName                                     |
|           |                            |                            | javax.jdo.option.ConnectionPassword                                     |
|           |                            |                            | hive.metastore.uris                                                     |
|           |                            |                            | hive.metastore.pre.event.listeners                                      |
|           |                            |                            | hive.security.authorization.enabled                                     |
|           |                            |                            | hive.security.metastore.authenticator.manager                           |
|           |                            |                            | hive.security.metastore.authorization.manager                           |
|           |                            |                            | hive.metastore.use.SSL                                                  |
|           |                            |                            | hive.metastore.keystore.path                                            |
|           |                            |                            | hive.metastore.keystore.password                                        |
|           |                            |                            | hive.metastore.truststore.path                                          |
|           |                            |                            | hive.metastore.truststore.password                                      |
|           |                            |                            | hive.metastore.kerberos.keytab.file                                     |
|           |                            |                            | hive.metastore.kerberos.principal                                       |
|           |                            |                            | hive.metastore.sasl.enabled                                             |
|           |                            |                            | hive.metastore.execute.setugi                                           |
|           |                            |                            | hive.cluster.delegation.token.store.class                               |
|           | hive-env                   | hive-env.sh                | |

## Unsupported HDFS configurations

The following `hdfs` configurations are unsupported and cannot be changed in the context of the Big Data Cluster.

| Category  | Sub-Category               | File                       | Unsupported Configurations                                              |
|-----------|----------------------------|----------------------------|-------------------------------------------------------------------------|
|          | core-site                   | core-site.xml                 | fs.defaultFS                                          |
|          |                             |                               | ha.zookeeper.quorum                                   |
|          |                             |                               | hadoop.tmp.dir                                        |
|          |                             |                               | hadoop.rpc.protection                                 |
|          |                             |                               | hadoop.security.auth_to_local                         |
|          |                             |                               | hadoop.security.authentication                        |
|          |                             |                               | hadoop.security.authorization                         |
|          |                             |                               | hadoop.http.authentication.simple.anonymous.allowed   |
|          |                             |                               | hadoop.http.authentication.type                       |
|          |                             |                               | hadoop.http.authentication.kerberos.principal         |
|          |                             |                               | hadoop.http.authentication.kerberos.keytab            |
|          |                             |                               | hadoop.http.filter.initializers                       |
|          |                             |                               | hadoop.security.group.mapping.*                       |                               
|          |                             |                               | hadoop.security.key.provider.path                     |                               
|          | mapred-env                  | mapred-env.sh                 |                                                       |
|          | hdfs-site                   | hdfs-site.xml                 | dfs.namenode.name.dir                                 |
|          |                             |                               | dfs.datanode.data.dir                                 |
|          |                             |                               | dfs.namenode.acls.enabled                             |
|          |                             |                               | dfs.namenode.datanode.registration.ip-hostname-check  |
|          |                             |                               | dfs.client.retry.policy.enabled                       |
|          |                             |                               | dfs.permissions.enabled                               |
|          |                             |                               | dfs.nameservices                                      |
|          |                             |                               | dfs.ha.namenodes.nmnode-0                             |
|          |                             |                               | dfs.namenode.rpc-address.nmnode-0.*                   |
|          |                             |                               | dfs.namenode.shared.edits.dir                         |
|          |                             |                               | dfs.ha.automatic-failover.enabled                     |
|          |                             |                               | dfs.ha.fencing.methods                                |
|          |                             |                               | dfs.journalnode.edits.dir                             |
|          |                             |                               | dfs.client.failover.proxy.provider.nmnode-0           |
|          |                             |                               | dfs.namenode.http-address                             |
|          |                             |                               | dfs.namenode.httpS-address                            |
|          |                             |                               | dfs.http.policy                                       |
|          |                             |                               | dfs.encrypt.data.transfer                             |
|          |                             |                               | dfs.block.access.token.enable                         |
|          |                             |                               | dfs.data.transfer.protection                          |
|          |                             |                               | dfs.encrypt.data.transfer.cipher.suites               |
|          |                             |                               | dfs.https.port                                        |
|          |                             |                               | dfs.namenode.keytab.file                              |
|          |                             |                               | dfs.namenode.kerberos.principal                       |
|          |                             |                               | dfs.namenode.kerberos.internal.spnego.principal       |
|          |                             |                               | dfs.datanode.data.dir.perm                            |
|          |                             |                               | dfs.datanode.address                                  |
|          |                             |                               | dfs.datanode.http.address                             |
|          |                             |                               | dfs.datanode.ipc.address                              |
|          |                             |                               | dfs.datanode.https.address                            |
|          |                             |                               | dfs.datanode.keytab.file                              |
|          |                             |                               | dfs.datanode.kerberos.principal                       |
|          |                             |                               | dfs.journalnode.keytab.file                           |
|          |                             |                               | dfs.journalnode.kerberos.principal                    |
|          |                             |                               | dfs.journalnode.kerberos.internal.spnego.principal    |
|          |                             |                               | dfs.web.authentication.kerberos.keytab                |
|          |                             |                               | dfs.web.authentication.kerberos.principal             |
|          |                             |                               | dfs.webhdfs.enabled                                   |
|          |                             |                               | dfs.permissions.superusergroup                        |
|          | hdfs-env                    | hdfs-env.sh                   | HADOOP_HEAPSIZE_MAX                                   |
|          | zoo-cfg                     | zoo.cfg                       | secureClientPort                                      |
|          |                             |                               | clientPort                                            |
|          |                             |                               | dataDir                                               |
|          |                             |                               | dataLogDir                                            |
|          |                             |                               | 4lw.commands.whitelist                                |
|          | zookeeper-java-env          | java.env                      | ZK_LOG_DIR                                            |
|          |                             |                               | SERVER_JVMFLAGS                                       |
|          | zookeeper-log4j-properties  | log4j.properties (zookeeper)  | log4j.rootLogger                                      |
|          |                             |                               | log4j.appender.CONSOLE.*                              |

> [!NOTE]
> This article contains the term *whitelist*, a term Microsoft considers insensitive in this context. The term appears in this article because it currently appears in the software. When the term is removed from the software, we will remove it from the article.

## Unsupported `gateway` configurations

The following `gateway` configurations for are unsupported and cannot be changed in the context of the Big Data Cluster.

| Category  | Sub-Category               | File                       | Unsupported Configurations                                              |
|-----------|----------------------------|----------------------------|-------------------------------------------------------------------------|
|          | gateway-site                | gateway-site.xml              | gateway.port                                          |
|          |                             |                               | gateway.path                                          |
|          |                             |                               | gateway.gateway.conf.dir                              |
|          |                             |                               | gateway.hadoop.kerberos.secured                       |
|          |                             |                               | java.security.krb5.conf                               |
|          |                             |                               | java.security.auth.login.config                       |
|          |                             |                               | gateway.websocket.feature.enabled                     |
|          |                             |                               | gateway.scope.cookies.feature.enabled                 |
|          |                             |                               | ssl.exclude.protocols                                 |
|          |                             |                               | ssl.include.ciphers                                   |

## Next steps

[Configure SQL Server Big Data Clusters](configure-bdc-overview.md)
