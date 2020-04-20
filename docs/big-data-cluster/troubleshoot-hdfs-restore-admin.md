---
title: Restore HDFS admin rights
titleSuffix: SQL Server Big Data Cluster
description: TRestore HDFS admin rights.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mikeray
ms.date: 04/21/2020
ms.topic: how-to
ms.prod: sql
ms.technology: big-data-cluster
---

# Restore HDFS admin rights

HDFS access control lists (ACLs) modifications may have affected the `/system` and `/tmp` folders in HDFS. The most likely cause of ACL modification is a user manually manipulating the folder ACLs.

## Symptom

A spark job is submitted through ADS and it fails with SparkContext initialization error and AccessControlException.

```
583 ERROR spark.SparkContext: Error initializing SparkContext.
org.apache.hadoop.security.AccessControlException: Permission denied: user=<UserAccount>, access=WRITE, inode="/system/spark-events":sph:BDCAdmin:drwxr-xr-x
```

Yarn UI shows application ID in KILLED status.

When you try to write to the folder as the domain user, it also fails. You can test with the following example:

```bash
kinit <UserAccount>
hdfs dfs -touch /system/spark-events/test
hdfs dfs -rm /system/spark-events/test
```

## Cause

The HDFS ACLs were modified for BDC user domain security group. Possible modifications included ACLs for /system and /tmp folders. Modifications of these folders are not supported.

Verify the effect in the Livy logs:

```
INFO utils.LineBufferedStream: <date time> ,858 INFO yarn.Client: Application report for application_1580771254352_0041 (state: ACCEPTED)
...
WARN rsc.RSCClient: Client RPC channel closed unexpectedly.
INFO interactive.InteractiveSession: Failed to ping RSC driver for session <ID>. Killing application
```

The YARN UI shows applications in KILLED status for the application ID.

To get the root cause of RPC connection close, check the YARN application log for app corresponding to the application. In the preceding example, it refers to `application_1580771254352_0041`. Use `kubectl` to connect to the sparkhead-0 pod, and run this command:

```bash
yarn logs -applicationId application_1580771254352_0041
```

## Resolution

Do not modify permissions in /system folder and /tmp/logs folder.

Restore the permissions with the following script: Use `kinit` with admin:

```bash
hdfs dfs -chmod 733 /system/spark-events
hdfs dfs -setfacl --set default:user:sph:rwx,default:other::--- /system/spark-events
hdfs dfs -setfacl --set default:user:app-setup:r-x,default:other::--- /system/appdeploy
hadoop fs -chmod 733 /tmp/logs
hdfs dfs -setfacl --set default:user:yarn:rwx,default:other::--- /tmp/logs
```
