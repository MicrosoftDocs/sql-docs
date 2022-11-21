---
title: SQL Server 2019 Big Data Clusters platform release notes
titleSuffix: SQL Server 2019 Big Data Clusters
description: This article describes the latest updates and known issues for SQL Server Big Data Clusters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 09/28/2022
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# SQL Server 2019 Big Data Clusters platform release notes

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

> [!IMPORTANT]
> The Microsoft SQL Server 2019 Big Data Clusters add-on will be retired. Support for SQL Server 2019 Big Data Clusters will end on January 14, 2025. All existing users of SQL Server 2019 with Software Assurance will be fully supported on the platform and the software will continue to be maintained through SQL Server cumulative updates until that time. For more information, see [the announcement blog post](https://aka.ms/sqlserver_bigdataclusters) and [Big data options on the Microsoft SQL Server platform](../big-data-cluster/big-data-options.md). 

The following release notes apply to [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)]. This article is broken into sections for each release describing the CU changes. The article also lists [known issues](#known-issues) for the most recent releases of [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)].

## Tested configurations

[!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] is a fully containerized solution orchestrated by Kubernetes. Starting with [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] CU12, each release of SQL Server Big Data Clusters is tested against a fixed configuration of components. The configuration is evaluated with each release and adjustments are made to stay in-line with the ecosystem as Kubernetes continues to evolve.

   > [!IMPORTANT]
   > Kubernetes is a fast paced ecosystem. It is key to keep your platform updated in order to be secure, and to be on a tested configuration for SQL Server Big Data Clusters.

   > [!WARNING]
   > On [!INCLUDE[sssql19-md](../includes/sssql19-md.md)] Cumulative Update 15, __the upgrade order is critical__. Upgrade your big data cluster to CU15 __before__ upgrading the Kubernetes cluster to version 1.21. If the Kubernetes cluster is upgraded to version 1.21 before BDC is upgraded to CU14 or CU15 then the cluster will end up in error state and the BDC upgrade will not succeed. In this case, reverting back to Kubernetes version 1.20 will fix the problem.

The following table contains the tested configuration matrix for each release of SQL Server 2019 Big Data Clusters:

| Release | Container OS | Kubernetes API | Runtime | Data Storage | Log Storage |
| ----------- | ------------ | ------- | ------- | ------------ | ----------- |
| CU18 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |
| CU17 | Ubuntu 20.04 LTS | 1.23.1 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |
| CU16 GDR | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |
| CU16 | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |
| CU15 | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |
| CU14 | Ubuntu 20.04 LTS | 1.21 | containerd 1.4.6<br/>CRI-O 1.20.4 | Block only | Block only |
| CU13 | Ubuntu 20.04 LTS | 1.20 | containerd 1.4.6<br/>CRI-O 1.20.0 | Block only | Block only |
| CU12 | Ubuntu 20.04 LTS | 1.20 | containerd 1.4.3<br/>docker 20.10.2<br/>CRI-O 1.20.0 | Block only | Block only |

Restrictions:

* SQL Server 2019 Big Data Clusters is supported as a *workload*. Microsoft provides support for the software components on the containers installed and configured by SQL Server 2019 Big Data Clusters only. Kubernetes itself, and other containers that may influence SQL Server 2019 Big Data Clusters behavior, are not supported by the support team. For Kubernetes support, contact your certified Kubernetes distribution provider.
* SQL Server 2019 Big Data Clusters requires block storage for all persisted volumes. Management operation on top of the persisted volumes created and used by a big data cluster is a capability that depends on the storage provider including, for example, operations to expand persistent volumes (PVs). Reference your specific CSI storage provider documentation or the [partner reference architecture and white papers](partner-big-data-cluster.md).
* The open-source components included by SQL Server 2019 Big Data Clusters are fixed for that particular release and must not be updated or modified.
* Container images are provided "as-is". Composability features of Kubernetes aren't supported. Changing the set of container images in a SQL Server 2019 Big Data Cluster release, or to customize the containers, is not supported.

Reference architecture and white papers for [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)] can be found on the following pages:

* [SQL Server 2019](https://www.microsoft.com/sql-server/sql-server-2019)
* [SQL Server 2019 Big Data Clusters partners](partner-big-data-cluster.md)

## Release history

The following table lists the release history for [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)]. For more information, see [SQL Server 2019 Big Data Clusters cumulative updates history](release-notes-cumulative-updates-history.md).

| Release <sup>1</sup> | [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] version | [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version <sup>2</sup> | Release date |
|--|--|--|--|
| [CU18](release-notes-cumulative-update-18.md) |  15.0.4261.1  | 20.3.12   | September 28 2022 |
| [CU17](release-notes-cumulative-update-17.md) |  15.0.4249.2  | 20.3.12   | August 11 2022 |
| CU16 GDR [KB 5014356](https://support.microsoft.com/help/5014356) |  15.0.4236.7  | 20.3.12   | June 14 2022 |
| [CU16](release-notes-cumulative-update-16.md) |  15.0.4223.1  | 20.3.11   | May 2 2022 |
| [CU15](release-notes-cumulative-update-15.md) |  15.0.4198.2  | 20.3.10   | Jan 27 2022 |
| [CU14](release-notes-cumulative-update-14.md) |  15.0.4188.2  | 20.3.9    | Nov 22 2021 |
| [CU13](release-notes-cumulative-update-13.md) |  15.0.4178.15 | 20.3.8    | Sept 9 2021 |
| [CU12](release-notes-cumulative-update-12.md) |  15.0.4153.13 | 20.3.7    | Aug 4 2021 |
| [CU11](release-notes-cumulative-updates-history.md#cu11) |  15.0.4138.2 | 20.3.5    | June 10 2021 |
| [CU10](release-notes-cumulative-updates-history.md#cu10) |  15.0.4123.1 | 20.3.2    | April 6 2021 |
| [CU9](release-notes-cumulative-updates-history.md#cu9) |  15.0.4102.2 | 20.3.0    | Feb 11 2021 |
| [CU8-GDR](release-notes-cumulative-updates-history.md#cu8-gdr) | 15.0.4083.2  | 20.2.6    | Jan 12 2021 |
| [CU8](release-notes-cumulative-updates-history.md#cu8)     | 15.0.4073.23 | 20.2.2    | Oct 19 2020 |
| [CU6](release-notes-cumulative-updates-history.md#cu6)     | 15.0.4053.23 | 20.0.1    | Aug 4 2020 |
| [CU5](release-notes-cumulative-updates-history.md#cu5)     | 15.0.4043.16 | 20.0.0    | June 22 2020 |
| [CU4](release-notes-cumulative-updates-history.md#cu4)     | 15.0.4033.1  | 15.0.4033 | March 31 2020 |
| [CU3](release-notes-cumulative-updates-history.md#cu3)     | 15.0.4023.6  | 15.0.4023 | March 12 2020 |
| [CU2](release-notes-cumulative-updates-history.md#cu2)     | 15.0.4013.40 | 15.0.4013 | Feb 13 2020 |
| [CU1](release-notes-cumulative-updates-history.md#cu1)     | 15.0.4003.23 | 15.0.4003 | Jan 7 2020 |
| [GDR1](release-notes-cumulative-updates-history.md#rtm)    | 15.0.2070.34 | 15.0.2070 | Nov 4 2019 |

<sup>1</sup> CU7 is not available for [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)].

<sup>2</sup> [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version reflects the version of the tool at the time of the CU release. `azdata` can also release independently of the server release, therefore you might get newer versions when you install the latest packages. Newer versions are compatible with previously released CUs.

## How to install updates

To install updates, see [How to upgrade [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](deployment-upgrade.md).

## Known issues

### HDFS copy of large files using azdata with random failures

- **Affected releases**: CU14

- **Issue and customer effect**: This was a bug causing random failures on `azdata bdc cp` commands between HDFS paths. The bug affects larger file copies more often.

- **Solution**: Update to Cumulative Update 15 (CU15).

### Log4j security

- **Affected releases**: None

- **Issue and customer effect**: After a thorough assessment of the SQL Server 2019 Big Data Clusters codebase, no risk associated with the log4j vulnerability reported in December was identified. CU15 includes an updated version of log4j (2.17) for the ElasticSearch instance in the control plane to ensure that image scan alerts are not triggered by static code analysis of Big Data Cluster containers.

- **Solution**: Always keep your big data cluster updated to the latest release.

### Cluster upgrade from a CU8 and previous release to a post-CU9 release is not supported

- **Affected releases**: Releases CU8 and previous

- **Issue and customer effect**: When directly upgrading a cluster on CU8 release or previous to any release above CU9, upgrade fails from Monitoring Phase.

- **Solution**: Upgrade to CU9 first. Then upgrade from CU9 to the latest release.

### Kubernetes platforms with Kubernetes API version 1.21+

- **Affected releases**: All releases

- **Issue and customer effect**: Kubernetes API 1.21 or superior is not a tested configuration of SQL Server Big Data Clusters as of CU12.

### MicrosoftML packages on SQL Server Machine Learning Services

- **Affected releases**: CU10, CU11, CU12, and CU13

- **Issue and customer effect**: Some MicrosoftML R/Python packages on SQL Server Machine Learning Services aren't working. It affects all SQL Server master instances.

### Failed to connect to remote instance of SQL Server 2016 or older

- **Affected releases**: CU10
- **Issue and customer effect**: When using PolyBase in [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] CU10 to connect to an existing SQL Server instance that is using a certificate for channel encryption that was created using the SHA1 algorithm, you may observe the following error:     

> `Msg 105082, Level 16, State 1, Line 1`
> `105082;Generic ODBC error: [Microsoft][ODBC Driver 17 for SQL Server]SSL Provider: An existing connection was forcibly closed by the remote host.`
> `Additional error <2>: ErrorMsg: [Microsoft][ODBC Driver 17 for SQL Server]Client unable to establish connection, SqlState: 08001, NativeError: 10054 Additional error <3>: ErrorMsg: [Microsoft][ODBC Driver 17 for SQL Server]`
> `Invalid connection string attribute, SqlState: 01S00, NativeError: 0 .`

- **Solution**: Due to the heightened security requirements of Ubuntu 20.04 over the previous base image version, the remote connection isn't allowed for a certificate using the SHA1 algorithm. The default self-signed certificate of SQL Server releases 2005-2016 used the SHA1 algorithm. For more information on this change, see [changes made to self-signed certificates in SQL Server 2017](https://techcommunity.microsoft.com/t5/sql-server-support/changes-to-hashing-algorithm-for-self-signed-certificate-in-sql/ba-p/319026). In the remote SQL Server instance, use a certificate that is created with an algorithm that uses at least 112 bits of security (for example, SHA256). For production environments, it's recommended to obtain a trusted certificate from a Certificate Authority. For testing purposes, self-signed certificate can also be used. To create a self-signed certificate, see the [PowerShell Cmdlet New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate) or [certreq command](/windows-server/administration/windows-commands/certreq_1). For instructions to install a new certificate it on the remote SQL Server instance, see [Enable encrypted connections to the Database Engine](../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md)

### Partial loss of logs collected in ElasticSearch upon rollback

- **Affected releases**: Affects existing clusters, when a failed upgrade to CU9 results in a rollback or user issues a downgrade to an older release.

- **Issue and customer effect**: The software version used for Elastic Search was upgraded with CU9 and the new version is not backwards compatible with previous logs format/metadata. If ElasticSearch component upgrades successfully, but a later rollback is triggered, the logs collected between the ElasticSearch upgrade and the rollback will be permanently lost. If you issue a downgrade to older version of [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] (not recommended), logs stored in Elasticsearch will be lost. If you upgrade back to CU9, the data will be restored.

- **Workaround**: If needed, you can troubleshoot using logs collected using `azdata bdc debug copy-logs` command.

### Missing pods and container metrics

- **Affected releases**: Existing and new clusters upon upgrade to CU9

- **Issue and customer effect**: As a result of upgrading the version of Telegraf used for monitoring components in CU9, when upgrading the cluster to CU9 release, you'll notice that pods and container metrics are not being collected. This is because an additional resource is required in the definition of the cluster role used for Telegraf as a result of the software upgrade. If the user deploying the cluster or performing the upgrade doesn't have sufficient permissions, deployment/upgrade proceeds with a warning and succeeds, but the pod & node metrics won't be collected.

- **Workaround**: You can ask an administrator to create or update the role and the corresponding service account (either before or after the deployment/upgrade), and the big data cluster will use them. [This article](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) describes how to create the required artifacts.

### Issuing `azdata bdc copy-logs` does not result in logs being copied

- **Affected releases**: [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version *20.0.0*

- **Issue and customer effect**: Implementation of *copy-logs* command is assuming `kubectl` client tool version 1.15 or higher is installed on the client machine from which the command is issued. If `kubectl` version 1.14 is used, the `azdata bdc debug copy-logs` command will complete with no failures, but logs are not copied. When run with `--debug` flag, you can see this error in the output: *source '.' is invalid*.

- **Workaround**: Install `kubectl` version 1.15 or higher tool on the same client machine and reissue the `azdata bdc copy-logs` command. See instructions [here](deploy-big-data-tools.md) how to install `kubectl`.

### MSDTC capabilities cannot be enabled for SQL Server master instance 

- **Affected releases**: All big data cluster deployment configurations, irrespective of the release.

- **Issue and customer effect**: With SQL Server deployed within the big data cluster as SQL Server master instance, the MSDTC feature cannot be enabled. There's no workaround to this issue.

### HA SQL Server Database Encryption key encryptor rotation

- **Affected releases**: All version up to CU8. Resolved for CU9.

- **Issue and customer effect**: With SQL Server deployed with HA, the certificate rotation for the encrypted database fails. When the following command is executed on the master pool, an error message will appear:
    ```sql
    ALTER DATABASE ENCRYPTION KEY
    ENCRYPTION BY SERVER
    CERTIFICATE <NewCertificateName>
    ```
    There is no effect, the command fails, and the target database encryption is preserved using the previous certificate.

### Enabling HDFS Encryption Zones support on CU8

- **Affected releases**: This scenario surfaces when upgrading specifically to CU8 release from CU6 or previous. This won't happen on new deployments of CU8+ or when upgrading directly to CU9. CU10 or superior releases are not affected.

- **Issue and customer effect**: HDFS Encryption Zones support is not enabled by default in this scenario and needs to be configured using the steps provided in the [configuration guide](encryption-at-rest-concepts-and-configuration.md).

### Empty Livy jobs before you apply cumulative updates

- **Affected releases**: All version up to CU6. Resolved for CU8.

- **Issue and customer effect**: During an upgrade, `sparkhead` returns 404 error.

- **Workaround**: Before upgrading the big data cluster, ensure that there are no active Livy sessions or batch jobs. Follow the instructions under [Upgrade from supported release](deployment-upgrade.md#upgrade-from-supported-release) to avoid this. 

   If Livy returns a 404 error during the upgrade process, restart the Livy server on both `sparkhead` nodes. For example:

   ```console
   kubectl -n <clustername> exec -it sparkhead-0/sparkhead-1 -c hadoop-livy-sparkhistory -- exec supervisorctl restart livy
   ```

### Password expiration for big data cluster-generated service accounts

- **Affected releases**: All big data cluster deployments with Active Directory integration, irrespective of the release

- **Issue and customer effect**: During big data cluster deployment, the workflow generates a set of [service accounts](active-directory-objects.md). Depending on the password expiration policy set in the Domain Controller, passwords for these accounts can expire (default is 42 days). At this time, there is no mechanism to rotate credentials for all accounts in the big data cluster, so the cluster will become inoperable once the expiration period is met.

- **Workaround**: Update the expiration policy for service accounts in a big data cluster to "Password never expires" in the Domain Controller. For a complete list of these accounts, see [Auto generated Active Directory objects](active-directory-objects.md). This action can be done before or after the expiration time. In the latter case, Active Directory will reactivate the expired passwords.

### Credentials for accessing services through gateway endpoint

- **Affected releases**: New clusters deployed starting with CU5.

- **Issue and customer effect**: For new big data clusters deployed using SQL Server Big Data Clusters CU5, gateway username is not **root**. If the application used to connect to gateway endpoint is using the wrong credentials, you'll see an authentication error. This change is a result of running applications within the big data cluster as non-root user (a new default behavior starting with SQL Server Big Data Clusters CU5 release, when you deploy a new big data cluster using CU5, the username for the gateway endpoint is based on the value passed through **AZDATA_USERNAME** environment variable. It is the same username used for the controller and SQL Server endpoints. This is only affecting new deployments, existing big data clusters deployed with any of the previous releases will continue to use **root**. There is no effect to credentials when the cluster is deployed to use Active Directory authentication. 

- **Workaround**: Azure Data Studio will handle the credentials change transparently for the connection made to gateway to enable HDFS browsing experience in the ObjectExplorer. You must install [latest Azure Data Studio release](../azure-data-studio/download-azure-data-studio.md) that includes the necessary changes that address this use case.
For other scenarios where  you must provide credentials for accessing service through the gateway (for example, logging in with [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], accessing web dashboards for Spark), you must ensure the correct credentials are used. If you're targeting an existing cluster deployed before CU5 you'll continue using **root** username to connect to gateway, even after upgrading the cluster to CU5. If you deploy a new cluster using CU5 build, sign in by providing the username corresponding to **AZDATA_USERNAME** environment variable.

### Pods and nodes metrics not being collected

- **Affected releases**: New and existing clusters that are using CU5 images

- **Issue and customer effect**: As a result of a security fix related to the API that `telegraf` was using to collect metrics pod and host node metrics, customers may have noticed that the metrics are not being collected. This is possible in both new and existing deployments of [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] (after upgrade to CU5). As a result of the fix, Telegraf now requires a service account with cluster-wide role permissions. The deployment attempts to create the necessary service account and cluster role, but if the user deploying the cluster or performing the upgrade does not have sufficient permissions, deployment/upgrade proceeds with a warning and succeeds, but the pod & node metrics will not be collected.

- **Workaround**: You can ask an administrator to create the role and service account (either before or after the deployment/upgrade), and the big data cluster will use them. [This article](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) describes how to create the required artifacts.

### `azdata bdc copy-logs` command failure

- **Affected releases**: [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version *20.0.0*

- **Issue and customer effect**: Implementation of *copy-logs* command is assuming `kubectl` client tool is installed on the client machine from which the command is issued. If you're issuing the command against a big data cluster installed on OpenShift, from a client where only `oc` tool is installed, you'll get an error: `An error occurred while collecting the logs: [WinError 2] The system cannot find the file specified`.

- **Workaround**: Install `kubectl` tool on the same client machine and reissue the `azdata bdc copy-logs` command. See instructions [here](deploy-big-data-tools.md) how to install `kubectl`.

### Deployment with private repository

- **Affected releases**: GDR1, CU1, CU2. Resolved for CU3.

- **Issue and customer effect**: Upgrade from private repository has specific requirements

- **Workaround**: If you use a private repository to pre-pull the images for deploying or upgrading the big data cluster, ensure that the current build images as well as the target build images are in the private repository. This enables successful rollback, if necessary. Also, if you changed the credentials of the  private repository since the original deployment, update the corresponding secret in Kubernetes before you upgrade. [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] does not support updating the credentials through `AZDATA_PASSWORD` and `AZDATA_USERNAME` environment variables. Update the secret using [`kubectl edit secrets`](https://kubernetes.io/docs/concepts/configuration/secret/#editing-a-secret). 

Upgrading using different repositories for current and target builds is not supported.

### Upgrade may fail due to timeout

- **Affected releases**: GDR1, CU1, CU2. Resolved for CU 3.

- **Issue and customer effect**: An upgrade may fail due to timeout.

   The following code shows what the failure might look like:

   ```
   >azdata.EXE bdc upgrade --name <mssql-cluster>
   Upgrading cluster to version 15.0.4003

   NOTE: Cluster upgrade can take a significant amount of time depending on
   configuration, network speed, and the number of nodes in the cluster.

   Upgrading Control Plane.
   Control plane upgrade failed. Failed to upgrade controller.
   ```

   This error is more likely to occur when you upgrade [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] in Azure Kubernetes Service (AKS).

- **Workaround**: Increase the timeout for the upgrade. 

   To increase the timeouts for an upgrade, edit the upgrade config map. To edit the upgrade config map:

   1. Run the following command:

      ```bash
      kubectl edit configmap controller-upgrade-configmap
      ```

   2. Edit the following fields:

       **`controllerUpgradeTimeoutInMinutes`** Designates the number of minutes to wait for the controller or controller db to finish upgrading. Default is 5. Update to at least 20.

       **`totalUpgradeTimeoutInMinutes`**: Designates the combines amount of time for both the controller and controller db to finish upgrading (`controller` + `controllerdb` upgrade). Default is 10. Update to at least  40.

       **`componentUpgradeTimeoutInMinutes`**: Designates the amount of time that each subsequent phase of the upgrade has to complete. Default is 30. Update to 45.

   3. Save and exit.

   The following python script is another way to set the timeout:

   ```python
   from kubernetes import client, config
   import json

   def set_upgrade_timeouts(namespace, controller_timeout=20, controller_total_timeout=40, component_timeout=45):
       """ Set the timeouts for upgrades

       The timeout settings are as follows

       controllerUpgradeTimeoutInMinutes: sets the max amount of time for the controller
           or controllerdb to finish upgrading

       totalUpgradeTimeoutInMinutes: sets the max amount of time to wait for both the
           controller and controllerdb to complete their upgrade

       componentUpgradeTimeoutInMinutes: sets the max amount of time allowed for
           subsequent phases of the upgrade to complete
       """
       config.load_kube_config()

       upgrade_config_map = client.CoreV1Api().read_namespaced_config_map("controller-upgrade-configmap", namespace)

       upgrade_config = json.loads(upgrade_config_map.data["controller-upgrade"])

       upgrade_config["controllerUpgradeTimeoutInMinutes"] = controller_timeout

       upgrade_config["totalUpgradeTimeoutInMinutes"] = controller_total_timeout

       upgrade_config["componentUpgradeTimeoutInMinutes"] = component_timeout

       upgrade_config_map.data["controller-upgrade"] = json.dumps(upgrade_config)

       client.CoreV1Api().patch_namespaced_config_map("controller-upgrade-configmap", namespace, upgrade_config_map)
   ```

### Livy job submission from Azure Data Studio (ADS) or curl fail with 500 error

- **Issue and customer effect**: In an HA configuration, Spark shared resources `sparkhead` are configured with multiple replicas. In this case, you might experience failures with Livy job submission from Azure Data Studio (ADS) or `curl`. To verify, `curl` to any `sparkhead` pod results in refused connection. For example, `curl https://sparkhead-0:8998/` or `curl https://sparkhead-1:8998` returns 500 error.

   This happens in the following scenarios:

   - Zookeeper pods, or processes for each zookeeper instance, are restarted a few times.
   - When networking connectivity is unreliable between `sparkhead` pod and Zookeeper pods.

- **Workaround**: Restarting both Livy servers.

   ```bash
   kubectl -n <clustername> exec sparkhead-0 -c hadoop-livy-sparkhistory supervisorctl restart livy
   ```

   ```bash
   kubectl -n <clustername> exec sparkhead-1 -c hadoop-livy-sparkhistory supervisorctl restart livy
   ```

### Create memory-optimized table when master instance in an availability group

- **Issue and customer effect**: You cannot use the primary endpoint exposed for connecting to availability group databases (listener) to create memory-optimized tables.

- **Workaround**: To create memory-optimized tables when SQL Server master instance is an availability group configuration, [connect to the SQL Server instance](deployment-high-availability.md#instance-connect), expose an endpoint, connect to the SQL Server database, and create the memory optimized tables in the session created with the new connection.

### Insert to external tables Active Directory authentication mode

- **Issue and customer effect**: When SQL Server master instance is in Active Directory authentication mode, a query that selects only from external tables, where at least one of the external tables is in a storage pool, and inserts into another external table, the query returns:

> `Msg 7320, Level 16, State 102, Line 1`
> `Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "SQLNCLI11". Only domain logins can be used to query Kerberized storage pool.`
   

- **Workaround**: Modify the query in one of the following ways. Either join the storage pool table to a local table, or insert into the local table first, then read from the local table to insert into the data pool.

### Transparent Data Encryption capabilities cannot be used with databases that are part of the availability group in the SQL Server master instance

- **Issue and customer effect**: In an HA configuration, databases that have encryption enabled can't be used after a failover since the master key used for encryption is different on each replica. 

- **Workaround**: There's no workaround for this issue. We recommend to not enable encryption in this configuration until a fix is in place.


## Next steps

For more information about [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)], see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
