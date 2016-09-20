---
title: "Supported Hadoop Configuration Changes (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 507ffdd7-e047-4bf5-862e-6bf8b41c2933
caps.latest.revision: 22
author: BarbKess
---
# Supported Hadoop Configuration Changes (Analytics Platform System)
Microsoft only supports changing the following specified (whitelisted) configuration settings. All setting not listed below, should not be changed. Changing items that are not listed, might affect your support contract. For more information about how to change settings, see [Changing Hadoop Configuration Settings &#40;Analytic Platform System&#41;](../../mpp/hdinsight/changing-hadoop-configuration-settings-analytic-platform-system.md).  
  
> [!NOTE]  
> HDP 2.0 introduces changes with respect to Hadoop configuration. Some configuration parameters from HDP 1.3 are deprecated in HDP 2.0 and replaced with new parameters while others are not supported (i.e. do not affect system behavior). If you upgraded from previous version of APS and if you had customized configurations you will need to migrate your settings to HDP 2.0 format. For assistance, contact Microsoft Support for help with this scenario.  
  
These configuration files are located in `%HADOOP_HOME%/etc/Hadoop`.  
  
## Core-site.xml  
  
|Configuration property|Comment|  
|--------------------------|-----------|  
|fs.trash.interval|Number of minutes after which the checkpoint gets deleted. If zero, the trash feature is disabled.|  
|(For AU1 only) fs.checkpoint.period|The number of seconds between two periodic checkpoints.|  
|fs.checkpoint.size|The size of the current edit log (in bytes) that triggers a periodic checkpoint even if the fs.checkpoint.period hasn't expired.|  
|polybase.recursive.traversal|When reading data from external tables, set <polybase.recursive.traversal> to 'false' to read data from only the root folder of the external file location.|  
|hadoop.http.authentication.simple.anonymous.allowed|Indicates if anonymous requests are allowed when using 'simple' authentication (i.e. requests without user.name specified). This configuration applies to all REST APIs.|  
  
## Log4J.properties  
  
|Configuration property|Comment|  
|--------------------------|-----------|  
|hadoop.root.logger|Controls the logging level for hadoop services. It can be changed from WARN to INFO for troubleshooting purposes.|  
  
## Hdfs-site.xml  
  
|Configuration property|Comment|  
|--------------------------|-----------|  
|dfs.client.read.shortcircuit.skip.checksum|If this configuration parameter is set, short-circuit local reads will skip checksums. This is normally not recommended, but it may be useful for special setups.|  
|(For AU1) io.bytes.per.checksum<br /><br />(For AU2) dfs.bytes-per-checksum|The number of bytes per checksum. Must not be larger than dfs.stream-buffer-size.|  
|dfs.stream-buffer-size|The size of buffer to stream files. The size of this buffer should probably be a multiple of hardware page size (4096 on Intel x86), and it determines how much data is buffered during read and write operations.|  
|dfs.webhdfs.enabled|Enables WebHDFS (REST API).|  
|dfs.replication|This determines how many copies of each block to store for durability.|  
|dfs.block.size|The size of HDFS blocks. When operating on data stored in HDFS, the split size is generally the size of an HDFS block. Larger numbers provide less task granularity, but also put less strain on the cluster NameNode|  
|dfs.datanode.handler.count|The number of server threads for the datanode.|  
|dfs.namenode.handler.count|The number of server threads for the namenode.|  
|(For AU1) dfs.balance.bandwidthPerSec<br /><br />(For AU2) dfs.datanode.balance.bandwidthPerSec|Specifies the maximum amount of bandwidth that each datanode can utilize for the balancing purpose in term of the number of bytes per second.|  
|(For AU2 only) dfs.namenode.checkpoint.period|The number of seconds between two periodic checkpoints.|  
|dfs.web.authentication.simple.anonymous.allowed|Indicates if anonymous requests are allowed when using 'simple' authentication (i.e. requests without user.name specified). This configuration applies to WebHDFS REST API.<br /><br />Note. When set to 'true', new files in HDFS, created with anonymous requests, default to user.name as the file owner. This could prevent a later audit from determining the original file creator.|  
  
## Mapred-site.xml  
  
|Configuration property|Comment|  
|--------------------------|-----------|  
|(For AU1) mapred.map.child.java.opts<br /><br />(For AU2) mapreduce.map.java.opts|This property stores Java options for map tasks.|  
|(For AU1) mapred.reduce.child.java.opts<br /><br />(For AU2) mapreduce.reduce.java.opts|This property stores Java options for to reduce tasks.|  
|(For AU1) mapred.tasktracker.map.tasks.maximum<br /><br />(For AU2) mapreduce.tasktracker.map.tasks.maximum|The maximum number of map task slots to run simultaneously.|  
|(For AU1) mapred.tasktracker.reduce.tasks.maximum<br /><br />(For AU2) mapreduce.tasktracker.reduce.tasks.maximum|The maximum number of reduce task slots to run simultaneously.|  
|(For AU1) io.sort.mb<br /><br />(For AU2) mapreduce.task.io.sort.mb|This value sets the size, in megabytes, of the memory buffer that holds map outputs before writing the final map outputs. Lower values for this property increases the chance of spills.|  
|(For AU1 only) io.sort.record.percent<br /><br />(For AU2)|The percentage of the memory buffer specified by the io.sort.mb property that is dedicated to tracking record boundaries.|  
|(For AU1) io.sort.spill.percent<br /><br />(For AU2) mapreduce.map.sort.spill.percent|This property's value sets the soft limit for either the buffer or record collection buffers.|  
|(For AU1) mapred.tasktracker.expiry.interval<br /><br />(For AU2) mapreduce.jobtracker.expire.trackers.interval|This property's value specifies a time interval in milliseconds. After this interval expires without any heartbeats sent, a TaskTracker is marked lost.|  
|(For AU1) mapred.map.tasks.speculative.execution<br /><br />(For AU2) mapreduce.map.speculative|If true, then multiple instances of some map tasks may be executed in parallel.|  
|(For AU1) mapred.reduce.tasks.speculative.execution<br /><br />(For AU2) mapreduce.reduce.speculative|If true, then multiple instances of some reduce tasks may be executed in parallel.|  
|(For AU1) mapred.map.tasks<br /><br />(For AU2) mapreduce.job.maps|The default number of map tasks per job. Ignored when the value of the mapred.job.tracker property is local.|  
|(For AU1) mapred.reduce.tasks<br /><br />(For AU2) mapreduce.job.reduces|The default number of reduce tasks per job. Ignored when the value of the mapred.job.tracker property is local.|  
|(For AU1 only) tasktracker.http.threads<br /><br />(For AU2) mapreduce.tasktracker.http.threads|The number of worker threads that for the HTTP server. This is used for map output fetching.|  
|(For AU1) mapred.reduce.parallel.copies<br /><br />(For AU2) mapreduce.reduce.shuffle.parallelcopies|The default number of parallel transfers run by reduce during the copy (shuffle) phase.|  
|(For AU1) mapred.reduce.slowstart.completed.maps<br /><br />(For AU2) mapreduce.job.reduce.slowstart.completedmaps|Fraction of the number of maps in the job which should be complete before reduces are scheduled for the job.|  
|(For AU1) mapred.min.split.size<br /><br />(For AU2) mapreduce.input.fileinputformat.split.minsize|The minimum size chunk that map input should be split into. File formats with minimum split sizes take priority over this setting.|  
|(For AU1) mapred.job.tracker.handler.count<br /><br />(For AU2) mapreduce.jobtracker.handler.count|This property's value sets the number of server threads for the JobTracker.|  
  
## Oozie-site.xml  
  
|Configuration property|Comment|  
|--------------------------|-----------|  
|oozie.service.coord.normal.default.timeout|Default timeout for a coordinator action input check (in minutes) for normal job.|  
|oozie.command.default.lock.timeout|Default timeout (in milliseconds) for commands for acquiring an exclusive lock on an entity.|  
|oozie.notification.url.connection.timeout|Defines the timeout, in milliseconds, for Oozie HTTP notification callbacks. Oozie does HTTP notifications for workflow jobs which set the 'oozie.wf.action.notification.url', 'oozie.wf.worklfow.notification.url' and/or 'oozie.coord.action.notification.url' properties in their job properties.|  
|use.system.libpath.for.mapreduce.and.pig.jobs|If set to true, submissions of MapReduce and Pig jobs will include automatically the system library path, thus not requiring users to specify where the Pig JAR files are. Instead, the ones from the system library path are used.|  
|oozie.service.UUIDService.generator|random : generated UUIDs will be random strings.<br /><br />counter: generated UUIDs generated will be a counter postfixed with the system startup time.|  
|oozie.action.hadoop.delete.hdfs.tmp.dir|If set to true, it will delete temporary directory at the end of execution of map reduce action.|  
|oozie.action.pig.delete.hdfs.tmp.dir|If set to true, it will delete temporary directory at the end of execution of pig action.|  
|oozie.service.LiteWorkflowStoreService.user.retry.max|Automatic retry max count for workflow action is 3 in default.|  
|oozie.service.LiteWorkflowStoreService.user.retry.inteval|Automatic retry interval for workflow action is in minutes and the default value is 10 minutes.|  
|oozie.service.LiteWorkflowStoreService.user.retry.error.code|Automatic retry interval for workflow action is handled for these specified error code: FS009, FS008 is file exists error when using chmod in fs action. JA018 is output directory exists error in workflow map-reduce action. JA019 is error while executing distcp action. JA017 is job not exists error in action executor. JA008 is FileNotFoundException in action executor. JA009 is IOException in action executor.|  
  
## yarn-site.xml  
The HDInsight region uses the default settings. Changing these settings is not supported.  
  
## See Also  
[Changing Hadoop Configuration Settings &#40;Analytic Platform System&#41;](../../mpp/hdinsight/changing-hadoop-configuration-settings-analytic-platform-system.md)  
[Unsupported HDInsight Actions &#40;Analytics Platform System&#41;](../../mpp/hdinsight/unsupported-hdinsight-actions-analytics-platform-system.md)  
  
