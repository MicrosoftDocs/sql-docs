# Troubleshoot `pyspark` notebook

This article demonstrates how to troubleshoot a `pyspark` notebook that fails with error message.

## Background

Azure data studio communicates with the `livy` endpoint which in turn issues `spark-submit` commands. The `spark-submit` command will have a parameter to use YARN as the spark cluster manager.

## Troubleshooting Steps

1. Review the stack and error messages in the `pyspark`.

   Get the application ID from the first cell in the notebook that you can use to investigate the `livy`, YARN, and spark logs\UI with. This is the YARN application ID that the `SparkContext` uses. 

   :::image type="content" source="../big-data-cluster/media/troubleshoot-pyspark-notebook/1-failed-cell.png" alt-text="Failed cell":::

1. Get the logs.

   Use azdata copy-logs to investigate

   ```bash
   azdata login --auth basic --username <username> --endpoint https://<ip_address>:30080
   azdata bdc debug copy-logs -n <namespace> -d <folder_to_copy_logs>
   ```

   Output example

   ```
   <user>@<server>:~$ azdata bdc debug copy-logs -n <namespace> -d copy_logs
   Collecting the logs for cluster '<namespace>'.
   Collecting logs for containers...
   Creating an archive from logs-tmp/<namespace>.
   Log files are archived in /home/<user>/copy_logs/debuglogs-<namespace>-YYYYMMDD-HHMMSS.tar.gz.
   Creating an archive from logs-tmp/dumps.
   Log files are archived in /home/<user>/copy_logs/debuglogs-<namespace>-YYYYMMDD-HHMMSS-dumps.tar.gz.
   Collecting the logs for cluster 'kube-system'.
   Collecting logs for containers...
   Creating an archive from logs-tmp/kube-system.
   Log files are archived in /home/<user>/copy_logs/debuglogs-kube-system-YYYYMMDD-HHMMSS.tar.gz.
   Creating an archive from logs-tmp/dumps.
   Log files are archived in /home/<user>/copy_logs/debuglogs-kube-system-YYYYMMDD-HHMMSS-dumps.tar.gz.
   ```

1. Review the livy logs. The livy logs are at `<namespace>\sparkhead-0\hadoop-livy-sparkhistory\supervisor\log`.

   - Search for the YARN application ID from the pyspark notebook first cell.
   - Search for `ERR` status.
   
   Example of livy log that has a `YARN ACCEPTED` state. Livy has submitted the yarn application.

   ```
   HH:MM:SS INFO utils.LineBufferedStream: YYY-MM-DD HH:MM:SS INFO impl.YarnClientImpl: Submitted application application_1580771254352_0001
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream: YYY-MM-DD HH:MM:SS INFO yarn.Client: Application report for application_1580771254352_0001 (state: ACCEPTED)
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream: YYY-MM-DD HH:MM:SS INFO yarn.Client: 
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      client token: N/A
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      diagnostics: N/A
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      ApplicationMaster host: N/A
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      ApplicationMaster RPC port: -1
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      queue: default
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      start time: ############
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      final status: UNDEFINED
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      tracking URL: https://sparkhead-1.fnbm.corp:8090/proxy/application_1580771254352_0001/
   20/02/10 HH:MM:SS INFO utils.LineBufferedStream:      user: <account>
   ```

1. Review the YARN UI 

   Get YARN endpoint from Azure Data Studio big data cluster. or run `azdata bdc endpoint list –o table`.

   For example:

   ```bash
   azdata bdc endpoint list -o table
   ```

   Returns

   ```
   Description                                             Endpoint                                                          Name                        Protocol
   ------------------------------------------------------  ----------------------------------------------------------------  --------------------------  ----------
   Gateway to access HDFS files, Spark                     https://knox.<namespace-value>.local:30443                               gateway                     https
   Spark Jobs Management and Monitoring Dashboard          https://knox.<namespace-value>.local:30443/gateway/default/sparkhistory  spark-history               https
   Spark Diagnostics and Monitoring Dashboard              https://knox.<namespace-value>.local:30443/gateway/default/yarn          yarn-ui                     https
   Application Proxy                                       https://proxy.<namespace-value>.local:30778                              app-proxy                   https
   Management Proxy                                        https://bdcmon.<namespace-value>.local:30777                             mgmtproxy                   https
   Log Search Dashboard                                    https://bdcmon.<namespace-value>.local:30777/kibana                      logsui                      https
   Metrics Dashboard                                       https://bdcmon.<namespace-value>.local:30777/grafana                     metricsui                   https
   Cluster Management Service                              https://bdcctl.<namespace-value>.local:30080                             controller                  https
   SQL Server Master Instance Front-End                    sqlmaster.<namespace-value>.local,31433                                  sql-server-master           tds
   SQL Server Master Readable Secondary Replicas           sqlsecondary.<namespace-value>.local,31436                               sql-server-master-readonly  tds
   HDFS File System Proxy                                  https://knox.<namespace-value>.local:30443/gateway/default/webhdfs/v1    webhdfs                     https
   Proxy for running Spark statements, jobs, applications  https://knox.<namespace-value>.local:30443/gateway/default/livy/v1       livy                        https
   ```

1. Check the application ID and individual application_master and container logs.

1. Review the YARN application logs

   Get application log for app 1. You can `kubectl` to the `sparkhead-0` pod, and run this command:

   ```bash
   yarn logs -applicationId application_1580771254352_0001
   ```

   `Workitem` has been submitted to add this to the azdata copy_logs command for the future.
   

1. Search for errors or stacks.

   An example of permission error against hdfs. In the java stack look for the `Caused by:`

   ```
   YYYY-MM-DD HH:MM:SS,MMM ERROR spark.SparkContext: Error initializing SparkContext.
   org.apache.hadoop.security.AccessControlException: Permission denied: user=<account>, access=WRITE, inode="/system/spark-events":sph:<bdc-admin>:drwxr-xr-x
        at org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.check(FSPermissionChecker.java:399)
        at org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.checkPermission(FSPermissionChecker.java:255)
        at org.apache.hadoop.hdfs.server.namenode.FSPermissionChecker.checkPermission(FSPermissionChecker.java:193)
        at org.apache.hadoop.hdfs.server.namenode.FSDirectory.checkPermission(FSDirectory.java:1852)
        at org.apache.hadoop.hdfs.server.namenode.FSDirectory.checkPermission(FSDirectory.java:1836)
        at org.apache.hadoop.hdfs.server.namenode.FSDirectory.checkAncestorAccess(FSDirectory.java:1795)
        at org.apache.hadoop.hdfs.server.namenode.FSDirWriteFileOp.resolvePathForStartFile(FSDirWriteFileOp.java:324)
        at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.startFileInt(FSNamesystem.java:2504)
        at org.apache.hadoop.hdfs.server.namenode.FSNamesystem.startFileChecked(FSNamesystem.java:2448)
   
   Caused by: org.apache.hadoop.ipc.RemoteException(org.apache.hadoop.security.AccessControlException): Permission denied: user=<account>, access=WRITE, inode="/system/spark-events":sph:<bdc-admin>:drwxr-xr-x
   ```

1. Review the SPARK UI
   Drill down into the stages tasks looking for errors. 

## Next steps

[Troubleshoot SQL Server Big Data Cluster Active Directory integration](troubleshoot-active-directory.md)
