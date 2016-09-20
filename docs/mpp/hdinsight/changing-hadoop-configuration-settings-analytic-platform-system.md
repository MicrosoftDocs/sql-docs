---
title: "Changing Hadoop Configuration Settings (Analytic Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 821d6e81-217a-4fb1-b3ea-f7ab8acac042
caps.latest.revision: 13
author: BarbKess
---
# Changing Hadoop Configuration Settings (Analytic Platform System)
Describes how to change supported Hadoop configuration settings. Use these scenarios to change one or more parameters, revert cluster configurations to factory settings or the last known good configuration, or to restore the Hadoop cluster configuration after a servicing action.  
  
## Contents  
  
-   [Before You Begin](#BeforeBegin)  
  
-   [Scenario 1: Change one or more parameters from the list of supported configurations](#scenario1)  
  
-   [Scenario 2: Revert cluster configurations to factory settings](#scenario2)  
  
-   [Scenario 3: Revert cluster configurations to the last known good configuration](#scenario3)  
  
-   [Scenario 4: Restore cluster configuration after servicing action](#scenario4)  
  
## <a name="BeforeBegin"></a>Before You Begin  
  
### Requirements  
We only support changing the configuration settings that are listed in [Supported Hadoop Configuration Changes &#40;Analytics Platform System&#41;](../../mpp/hdinsight/supported-hadoop-configuration-changes-analytics-platform-system.md).  
  
The scenarios require membership in the **HDInsight Cluster Admin** security group.  
  
### Recommendations  
The cluster should not run any workload or data loading during reconfiguration actions. Expect HDInsight to have a period of downtime before new configuration becomes effective. When changing more than one configuration value, it is better to do all changes in a single batch, since changing configuration requires the services to restart. Although it is possible to effectively apply new configurations by restarting only the subset of dependent services that are affected by the change, restarting all Hadoop services is recommended as a more reliable practice.  
  
Updates to HDInsight and servicing actions such as replacing virtual machines can override custom configurations, returning them to their original default values. When make changes, administrators should use [HDInsight Configuration Tool &#40;Analytics Platform System&#41;](../../mpp/hdinsight/hdinsight-configuration-tool-analytics-platform-system.md) to make sure that last valid version of the cluster configuration is always available. The same tool can be used to revert the cluster to custom configuration settings.  
  
## <a name="scenario1"></a>Scenario 1: Change one or more parameters from the list of supported configurations  
  
#### To change a parameter  
  
1.  Determine if particular configuration needs to be altered on the head node only or on the worker nodes as well.  
  
2.  Determine if particular configuration needs to be altered on the head node only or on the worker nodes as well.  
  
3.  Remotely connect to the head node virtual machine *(<HDInsight domain\>***-HHN01**) and edit the targeted configuration files.  
  
4.  If the configuration change is required to be applied across all of the worker nodes repeat step 2 for all data nodes (HDNs) in the cluster.  
  
5.  Restart all Hadoop services. Restarting can be done using the Configuration Manager (dwconfig.exe) or System Center Operations Manager (SCOM).  
  
    ###### To restart using Configuration Manager  
  
    1.  Open the Configuration Manager, dwconfig.exe, tool. See [Launch the Configuration Manager &#40;Analytics Platform System&#41;](../../mpp/management/launch-the-configuration-manager-analytics-platform-system.md).  
  
    2.  In the Configuration Manager, open the **HDInsight Topology** menu, select the **Service Status** page, and click **Stop Region**. Wait for the operation to complete, and then click **Start Region**.  
  
    ###### To restart using SCOM  
  
    1.  In SCOM, go to Microsoft HDInsight, Clusters Diagram, select your appliance cluster, and then click **Stop all HDInsight cluster services**. Wait for operation to complete, and then click **Start all HDInsight cluster services**.  
  
6.  After verification, use [HDInsight Configuration Tool &#40;Analytics Platform System&#41;](../../mpp/hdinsight/hdinsight-configuration-tool-analytics-platform-system.md) to make a backup so that cluster can always be restored to that the latest valid point.  
  
    If cluster doesn’t perform well with new configuration values, use [Scenario 2: Revert cluster configurations to factory settings](#scenario2) or [Scenario 3: Revert cluster configurations to the last known good configuration](#scenario3) to revert the cluster to either the factory settings or the last known good configuration.  
  
## <a name="scenario2"></a>Scenario 2: Revert cluster configurations to factory settings  
The cluster is deployed with the optimal configurations which are safely saved on a backup location, both as part of initial deployment as well as every hotfix action. The default backup location for the configuration files is **<OS drive\>\HadoopConfBackupRoot\Hadoop\\**. Configuration files saved this way can be used to revert cluster to the original delivery settings.  
  
#### To restore the configuration to factory settings  
  
1.  Remotely connect to head node virtual machine *(<HDInsight domain \>***-HHN01**) and replace the existing configuration files with the original factory default files which are stored in the folder **<OS drive\>\HadoopConfBackupRoot\Hadoop\\**.  
  
2.  If custom configuration included data nodes, the head node and the worker nodes need to be repaired. In this case repeat step 1 for all data nodes in the cluster.  
  
3.  Restart all Hadoop services using the same procedure described in Scenario 1, Step 5.  
  
## <a name="scenario3"></a>Scenario 3: Revert cluster configurations to the last known good configuration  
Upon every successful configuration change, customers are strongly advised to back up the latest state using [HDInsight Configuration Tool &#40;Analytics Platform System&#41;](../../mpp/hdinsight/hdinsight-configuration-tool-analytics-platform-system.md).  
  
#### To restore the cluster configuration  
  
1.  Remotely connect to head node virtual machine *(<HDInsight domain \>***-HHN01**) and run [HDInsight Configuration Tool &#40;Analytics Platform System&#41;](../../mpp/hdinsight/hdinsight-configuration-tool-analytics-platform-system.md) with the **–restore** switch.  
  
2.  After the script completes successfully, restart all Hadoop services using the same procedure described in Scenario 1, Step 5.  
  
## <a name="scenario4"></a>Scenario 4: Restore cluster configuration after servicing action  
Any servicing action that affects HDInsight will restore the cluster to the default configuration. If you have applied a custom configuration follow [Scenario 3: Revert cluster configurations to the last known good configuration](#scenario3) to revert the cluster to the last good known configuration from the backup you have previously made.  
  
Although it is not a common practice, a hotfix servicing option can contain new configuration parameters or new recommended values for existing ones. In that case restoring cluster to custom configuration needs to include manual steps of merging the new and previous configurations. Contact Microsoft Support for help with this scenario.  
  
## See Also  
[HDInsight Configuration Tool &#40;Analytics Platform System&#41;](../../mpp/hdinsight/hdinsight-configuration-tool-analytics-platform-system.md)  
[Supported Hadoop Configuration Changes &#40;Analytics Platform System&#41;](../../mpp/hdinsight/supported-hadoop-configuration-changes-analytics-platform-system.md)  
[Unsupported HDInsight Actions &#40;Analytics Platform System&#41;](../../mpp/hdinsight/unsupported-hdinsight-actions-analytics-platform-system.md)  
  
