---
title: "HDInsight Configuration Tool (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f8c3c449-96bf-4521-a339-070864d3ef89
caps.latest.revision: 8
author: BarbKess
---
# HDInsight Configuration Tool (Analytics Platform System)
Describes the HDInsightConfigurationUtility PowerShell script that performs backup and restore operations for the HDInsight configuration files used by HDInsight services running in the HDInsight region. Use this script perform configuration backups after every configuration change that you would like to save as a stable restore point, and to restore a configuration backup to an HDInsight region.  
  
## Syntax  
  
```powershell  
HDInsightConfigurationUtility.ps1   
    { –backup | -restore }  
    -UserName  
    -Password  
    [-force]  
```  
  
## Arguments  
-backup  
Stores a backup of the supported configuration files from all cluster nodes (HHN01 and all HDNs).  
  
-restore  
Replace the current configuration files with the latest backup.  
  
-*UserName*  
User name for the administrator account that will run the script.  
  
-*Password*  
Password for the administrator account that will run the script.  
  
-force  
Run without showing the confirmation prompt.  
  
This script is designed always to ask for confirmation. This behavior can be overridden by using –force switch, but this pattern of usage is not recommended.  
  
## Permissions and Requirements  
Requires an account that is a member of the **HDInsight Cluster Admins** security group.  
  
Requires a remote desktop connection to the **<HDInsight Domain>-HHN01** head node in the HDInsight region. For more information, see [Connecting to HDInsight Using Remote Desktop &#40;Analytics Platform System&#41;](../../mpp/hdinsight/connecting-to-hdinsight-using-remote-desktop-analytics-platform-system.md).  
  
## Error Handling  
If the script encounters an error, and your appliance is in a healthy state, try re-running the script.  
  
To further troubleshoot a script error, view the log file called **HadoopConfBackup.log**, which is located in the same directory as the script.  
  
## General Remarks  
The PowerShell script **HDInsightConfigurationUtility.ps1** is located on the head node (<HDInsight domain name>-HHN01), in folder **<OS drive>\HadoopConfBackupRoot**.  
  
The **HDInsightConfigurationUtility** script backups up the configuration files listed for each of these Hadoop services.  
  
-   All services - core-site.xml  
  
-   HDFS Service – hdfs-site.xml  
  
-   MapReduce Service – mapred-site.xml  
  
-   Hive Service – hive-site.xml  
  
-   HCatalog, WebHCat Services – webhcat-site.xml, proto-hive-site.xml  
  
-   Oozie Service – core-site.xml, oozie-site.xml, hive.xml  
  
-   Sqoop Service – sqoop-site.xml (including files on worker nodes)  
  
-   Yarn Service – yarn-site.xml  
  
## Limitations and Restrictions  
Only one backup is supported. Each new backup will replace the previous backup.  
  
Run this script only if HDInsight is in a healthy state and in full capacity (all nodes are running).  
  
## Recommendations  
We recommend performing a configuration backup after every configuration change that you would like to save as a stable restore point. We strongly recommend that you perform checkpoint.  
  
## Examples  
  
### Create a backup of the HDInsight supported configuration files  
To create a backup of the supported configuration files, open PowerShell and change to the C:\HadoopConfBackupRoot directory.  Then run the following command, using an administrator user name.  
  
```  
PS C:\HadoopConfBackupRoot> .\HDInsightConfigurationUtility.ps1 –backup -<User Name> -Password <password>  
```  
  
The script will prompt you to confirm the action. Enter Y to continue.  
  
As the script runs, it will report progress. When it finishes successfully it outputs this line: `SUCCESS: <timestamp>: Backup committed in the database`  
  
### Restore a backup of the HDInsight supported configuration files  
To restore a backup of the supported configuration files, log in to the head node (<HDInsight Domain>-HHN01), open PowerShell and change to the <OS-Drive>:\HadoopConfBackupRoot directory. Run the following command, using an administrator user name.  
  
```  
PS C:\HadoopConfBackupRoot> .\HDInsightConfigurationUtility.ps1 –restore -<User Name> -Password <password>  
```  
  
The script will prompt you to confirm the action. Enter Y to continue.  
  
As the script runs, it will report progress. When it finishes successfully the last line of output is: `SUCCESS: <timestamp>: NodeBackup script executed on machine @{Name=<HDInsight domain>-HDN01; NodeType=Data}`  
  
