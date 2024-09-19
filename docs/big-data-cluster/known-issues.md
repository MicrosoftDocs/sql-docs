---
title: SQL Server 2019 Big Data Clusters platform known issues
titleSuffix: SQL Server 2019 Big Data Clusters
description: This article includes known issues for SQL Server Big Data Clusters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei, randolphwest
ms.date: 12/04/2023
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: troubleshooting-known-issue
---
# SQL Server 2019 Big Data Clusters platform known issues

[!INCLUDE [SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE [big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

## Known issues

### HDFS copy of large files using azdata with random failures

- **Affected releases**: CU14

- **Issue and customer effect**: This was a bug causing random failures on `azdata bdc cp` commands between HDFS paths. The bug affects larger file copies more often.

- **Solution**: Update to Cumulative Update 15 (CU15).

### Log4j security

- **Affected releases**: None

- **Issue and customer effect**: After a thorough assessment of the SQL Server 2019 Big Data Clusters codebase, no risk associated with the log4j vulnerability reported in December was identified. CU15 includes an updated version of log4j (2.17) for the ElasticSearch instance in the control plane to ensure that image scan alerts aren't triggered by static code analysis of Big Data Cluster containers.

- **Solution**: Always keep your big data cluster updated to the latest release.

### Cluster upgrade from a CU8 and previous release to a post-CU9 release is not supported

- **Affected releases**: Releases CU8 and previous

- **Issue and customer effect**: When directly upgrading a cluster on CU8 release or previous to any release above CU9, upgrade fails from Monitoring Phase.

- **Solution**: Upgrade to CU9 first. Then upgrade from CU9 to the latest release.

### Kubernetes platforms with Kubernetes API version 1.21+

- **Affected releases**: All releases

- **Issue and customer effect**: Kubernetes API 1.21 or superior isn't a tested configuration of SQL Server Big Data Clusters as of CU12.

### MicrosoftML packages on SQL Server Machine Learning Services

- **Affected releases**: CU10, CU11, CU12, and CU13

- **Issue and customer effect**: Some MicrosoftML R/Python packages on SQL Server Machine Learning Services aren't working. It affects all SQL Server master instances.

### Failed to connect to remote instance of SQL Server 2016 or older

- **Affected releases**: CU10
- **Issue and customer effect**: When using PolyBase in [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] CU10 to connect to an existing SQL Server instance that is using a certificate for channel encryption that was created using the SHA1 algorithm, you might observe the following error:

> `Msg 105082, Level 16, State 1, Line 1`
> `105082;Generic ODBC error: [Microsoft][ODBC Driver 17 for SQL Server]SSL Provider: An existing connection was forcibly closed by the remote host.`
> `Additional error <2>: ErrorMsg: [Microsoft][ODBC Driver 17 for SQL Server]Client unable to establish connection, SqlState: 08001, NativeError: 10054 Additional error <3>: ErrorMsg: [Microsoft][ODBC Driver 17 for SQL Server]`
> `Invalid connection string attribute, SqlState: 01S00, NativeError: 0 .`

- **Solution**: Due to the heightened security requirements of Ubuntu 20.04 over the previous base image version, the remote connection isn't allowed for a certificate using the SHA1 algorithm. The default self-signed certificate of SQL Server releases 2005-2016 used the SHA1 algorithm. For more information on this change, see [changes made to self-signed certificates in SQL Server 2017](https://techcommunity.microsoft.com/t5/sql-server-support/changes-to-hashing-algorithm-for-self-signed-certificate-in-sql/ba-p/319026). In the remote SQL Server instance, use a certificate that is created with an algorithm that uses at least 112 bits of security (for example, SHA256). For production environments, it's recommended to obtain a trusted certificate from a Certificate Authority. For testing purposes, self-signed certificate can also be used. To create a self-signed certificate, see the [PowerShell Cmdlet New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate) or [certreq command](/windows-server/administration/windows-commands/certreq_1). For instructions to install a new certificate it on the remote SQL Server instance, see [Enable encrypted connections to the Database Engine](../database-engine/configure-windows/configure-sql-server-encryption.md)

### Partial loss of logs collected in ElasticSearch upon rollback

- **Affected releases**: Affects existing clusters, when a failed upgrade to CU9 results in a rollback or user issues a downgrade to an older release.

- **Issue and customer effect**: The software version used for Elastic Search was upgraded with CU9 and the new version isn't backward compatible with previous logs format/metadata. If ElasticSearch component upgrades successfully, but a later rollback is triggered, the logs collected between the ElasticSearch upgrade and the rollback is permanently lost. If you issue a downgrade to older version of [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] (not recommended), logs stored in Elasticsearch will be lost. If you upgrade back to CU9, the data is restored.

- **Workaround**: If needed, you can troubleshoot using logs collected using `azdata bdc debug copy-logs` command.

### Missing pods and container metrics

- **Affected releases**: Existing and new clusters upon upgrade to CU9

- **Issue and customer effect**: As a result of upgrading the version of Telegraf used for monitoring components in CU9, when upgrading the cluster to CU9 release, you'll notice that pods and container metrics aren't being collected. This is because an additional resource is required in the definition of the cluster role used for Telegraf as a result of the software upgrade. If the user deploying the cluster or performing the upgrade doesn't have sufficient permissions, deployment/upgrade proceeds with a warning and succeeds, but the pod & node metrics won't be collected.

- **Workaround**: You can ask an administrator to create or update the role and the corresponding service account (either before or after the deployment/upgrade), and the big data cluster will use them. [This article](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) describes how to create the required artifacts.

### Issuing azdata bdc copy-logs does not result in logs being copied

- **Affected releases**: [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version *20.0.0*

- **Issue and customer effect**: Implementation of *copy-logs* command is assuming `kubectl` client tool version 1.15 or higher is installed on the client machine from which the command is issued. If `kubectl` version 1.14 is used, the `azdata bdc debug copy-logs` command completes with no failures, but logs aren't copied. When run with `--debug` flag, you can see this error in the output: *source '.' is invalid*.

- **Workaround**: Install `kubectl` version 1.15 or higher tool on the same client machine and reissue the `azdata bdc copy-logs` command. See instructions [here](deploy-big-data-tools.md) how to install `kubectl`.

### MSDTC capabilities cannot be enabled for SQL Server master instance

- **Affected releases**: All big data cluster deployment configurations, irrespective of the release.

- **Issue and customer effect**: With SQL Server deployed within the big data cluster as SQL Server master instance, the MSDTC feature can't be enabled. There's no workaround to this issue.

### HA SQL Server Database Encryption key encryptor rotation

- **Affected releases**: All version up to CU8. Resolved for CU9.

- **Issue and customer effect**: With SQL Server deployed with HA, the certificate rotation for the encrypted database fails. When the following command is executed on the master pool, an error message appears:

  ```sql
  ALTER DATABASE ENCRYPTION KEY
  ENCRYPTION BY SERVER
  CERTIFICATE <NewCertificateName>;
  ```

  There is no effect, the command fails, and the target database encryption is preserved using the previous certificate.

### Enable HDFS Encryption Zones support on CU8

- **Affected releases**: This scenario surfaces when upgrading specifically to CU8 release from CU6 or previous. This won't happen on new deployments of CU8+ or when upgrading directly to CU9. CU10 or superior releases aren't affected.

- **Issue and customer effect**: HDFS Encryption Zones support isn't enabled by default in this scenario and needs to be configured using the steps provided in the [configuration guide](encryption-at-rest-concepts-and-configuration.md).

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

- **Issue and customer effect**: During big data cluster deployment, the workflow generates a set of [service accounts](active-directory-objects.md). Depending on the password expiration policy set in the Domain Controller, passwords for these accounts can expire (default is 42 days). At this time, there is no mechanism to rotate credentials for all accounts in the big data cluster, so the cluster becomes inoperable once the expiration period is met.

- **Workaround**: Update the expiration policy for service accounts in a big data cluster to "Password never expires" in the Domain Controller. For a complete list of these accounts, see [Auto generated Active Directory objects](active-directory-objects.md). This action can be done before or after the expiration time. In the latter case, Active Directory reactivates the expired passwords.

### Credentials for accessing services through gateway endpoint

- **Affected releases**: New clusters deployed starting with CU5.

- **Issue and customer effect**: For new big data clusters deployed using SQL Server Big Data Clusters CU5, gateway username isn't `root`. If the application used to connect to gateway endpoint is using the wrong credentials, you'll see an authentication error. This change is a result of running applications within the big data cluster as non-root user (a new default behavior starting with SQL Server Big Data Clusters CU5 release, when you deploy a new big data cluster using CU5, the username for the gateway endpoint is based on the value passed through `AZDATA_USERNAME` environment variable. It is the same username used for the controller and SQL Server endpoints. This only affects new deployments. Existing big data clusters deployed with any of the previous releases continue to use `root`. There is no effect to credentials when the cluster is deployed to use Active Directory authentication.

- **Workaround**: Azure Data Studio handles the credentials change transparently for the connection made to gateway to enable HDFS browsing experience in the Object Explorer. You must install [latest Azure Data Studio release](/azure-data-studio/download-azure-data-studio) that includes the necessary changes that address this use case.
For other scenarios where  you must provide credentials for accessing service through the gateway (for example, logging in with [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], accessing web dashboards for Spark), you must ensure the correct credentials are used. If you're targeting an existing cluster deployed before CU5 you'll continue using the `root` username to connect to gateway, even after upgrading the cluster to CU5. If you deploy a new cluster using CU5 build, sign in by providing the username corresponding to `AZDATA_USERNAME` environment variable.

### Pods and nodes metrics not being collected

- **Affected releases**: New and existing clusters that are using CU5 images

- **Issue and customer effect**: As a result of a security fix related to the API that `telegraf` was using to collect metrics pod and host node metrics, customers might notice that the metrics aren't being collected. This is possible in both new and existing deployments of [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] (after upgrade to CU5). As a result of the fix, Telegraf now requires a service account with cluster-wide role permissions. The deployment attempts to create the necessary service account and cluster role, but if the user deploying the cluster or performing the upgrade doesn't have sufficient permissions, deployment/upgrade proceeds with a warning and succeeds, but the pod and node metrics won't be collected.

- **Workaround**: You can ask an administrator to create the role and service account (either before or after the deployment/upgrade), and the big data cluster will use them. [This article](kubernetes-rbac.md#cluster-role-required-for-pods-and-nodes-metrics-collection) describes how to create the required artifacts.

### azdata bdc copy-logs command failure

- **Affected releases**: [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] version *20.0.0*

- **Issue and customer effect**: Implementation of *copy-logs* command is assuming `kubectl` client tool is installed on the client machine from which the command is issued. If you're issuing the command against a big data cluster installed on OpenShift, from a client where only `oc` tool is installed, you'll get an error: `An error occurred while collecting the logs: [WinError 2] The system cannot find the file specified`.

- **Workaround**: Install `kubectl` tool on the same client machine and reissue the `azdata bdc copy-logs` command. See instructions [here](deploy-big-data-tools.md) how to install `kubectl`.

### Deployment with private repository

- **Affected releases**: GDR1, CU1, CU2. Resolved for CU3.

- **Issue and customer effect**: Upgrade from private repository has specific requirements

- **Workaround**: If you use a private repository to pre-pull the images for deploying or upgrading the big data cluster, ensure that the current build images, and the target build images, are in the private repository. This enables successful rollback, if necessary. Also, if you changed the credentials of the  private repository since the original deployment, update the corresponding secret in Kubernetes before you upgrade. [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] doesn't support updating the credentials through `AZDATA_PASSWORD` and `AZDATA_USERNAME` environment variables. Update the secret using [`kubectl edit secrets`](https://kubernetes.io/docs/concepts/configuration/secret/#editing-a-secret).

Upgrading using different repositories for current and target builds isn't supported.

### Upgrade might fail due to timeout

- **Affected releases**: GDR1, CU1, CU2. Resolved for CU 3.

- **Issue and customer effect**: An upgrade might fail due to timeout.

  The following code shows what the failure might look like:

  ```output
  > azdata.EXE bdc upgrade --name <mssql-cluster>
  Upgrading cluster to version 15.0.4003

  NOTE: Cluster upgrade can take a significant amount of time depending on
  configuration, network speed, and the number of nodes in the cluster.

  Upgrading Control Plane.
  Control plane upgrade failed. Failed to upgrade controller.
  ```

  This error is more likely to occur when you upgrade [!INCLUDE [ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)] in Azure Kubernetes Service (AKS).

- **Workaround**: Increase the timeout for the upgrade.

  To increase the timeouts for an upgrade, edit the upgrade config map. To edit the upgrade config map:

  1. Run the following command:

     ```bash
     kubectl edit configmap controller-upgrade-configmap
     ```

  1. Edit the following fields:

     `controllerUpgradeTimeoutInMinutes` Designates the number of minutes to wait for the controller or controller db to finish upgrading. Default is 5. Update to at least 20.

     `totalUpgradeTimeoutInMinutes`: Designates the combines amount of time for both the controller and controller db to finish upgrading (`controller` + `controllerdb` upgrade). Default is 10. Update to at least  40.

     `componentUpgradeTimeoutInMinutes`: Designates the amount of time that each subsequent phase of the upgrade has to complete. Default is 30. Update to 45.

  1. Save and exit.

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

- **Issue and customer effect**: You can't use the primary endpoint exposed for connecting to availability group databases (listener) to create memory-optimized tables.

- **Workaround**: To create memory-optimized tables when SQL Server master instance is an availability group configuration, [connect to the SQL Server instance](deployment-high-availability.md#instance-connect), expose an endpoint, connect to the SQL Server database, and create the memory optimized tables in the session created with the new connection.

### Insert to external tables Active Directory authentication mode

- **Issue and customer effect**: When SQL Server master instance is in Active Directory authentication mode, a query that selects only from external tables, where at least one of the external tables is in a storage pool, and inserts into another external table, the query returns:

> `Msg 7320, Level 16, State 102, Line 1`
> `Cannot execute the query "Remote Query" against OLE DB provider "SQLNCLI11" for linked server "SQLNCLI11". Only domain logins can be used to query Kerberized storage pool.`

- **Workaround**: Modify the query in one of the following ways. Either join the storage pool table to a local table, or insert into the local table first, then read from the local table to insert into the data pool.

### Transparent Data Encryption capabilities cannot be used with databases that are part of the availability group in the SQL Server master instance

- **Issue and customer effect**: In an HA configuration, databases that have encryption enabled can't be used after a failover since the master key used for encryption is different on each replica.

- **Workaround**: There's no workaround for this issue. We recommend to not enable encryption in this configuration until a fix is in place.

## Related content

For more information about [!INCLUDE[ssbigdataclusters-ver15](../includes/ssbigdataclusters-ver15.md)], see [Introducing [!INCLUDE[big-data-clusters-nover](../includes/ssbigdataclusters-ss-nover.md)]](big-data-cluster-overview.md).
