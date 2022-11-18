---
title: Deploy on OpenShift
titleSuffix: SQL Server Big Data Cluster
description: Learn how to upgrade SQL Server Big Data Clusters on OpenShift.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 07/29/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on OpenShift on-premises and Azure Red Hat OpenShift

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article explains how to deploy a SQL Server Big Data Cluster on OpenShift environments, on-premises or on Azure Red Hat OpenShift (ARO).

> [!TIP]
> For a quick way to bootstrap a sample environment using ARO and then BDC deployed on this platform, you can use the Python script available [here](quickstart-big-data-cluster-deploy-aro.md).


You can deploy big data clusters to on-premises OpenShift or on Azure Red Hat OpenShift (ARO). Validate the OpenShifts CRI-O version against tested configurations on the [SQL Server Big Data Clusters release notes](release-notes-big-data-cluster.md). While the deployment workflow is similar to deploying in other Kubernetes based platforms ([kubeadm](deploy-with-kubeadm.md) and [AKS](deploy-on-aks.md)), there are some differences. The difference is mainly in relation to running applications as non-root user and the security context used for the namespace BDC is deployed in.

For deploying the OpenShift cluster on-premises see the [Red Hat OpenShift documentation](https://docs.openshift.com/container-platform/4.3/release_notes/ocp-4-3-release-notes.html#ocp-4-3-installation-and-upgrade). For ARO deployments see the [Azure Red Hat OpenShift](/azure/openshift/intro-openshift).

This article outlines deployment steps that are specific to the OpenShift platform, points out options you have for accessing the target environment and the namespace you are using to deploy the big data cluster.

## Pre-requisites

> [!IMPORTANT]
> Below pre-requisites must be performed by a OpenShift cluster admin (cluster-admin cluster role) that has sufficient permissions to create these cluster level objects. For more information on cluster roles in OpenShift see [Using RBAC to define and apply permissions](https://docs.openshift.com/container-platform/4.4/authentication/using-rbac.html).

1. Ensure the `pidsLimit` setting on the OpenShift is updated to accommodate SQL Server workloads. The default value in OpenShift is too low for production like workloads. Start with at least `4096`, but the optimal value depends the `max worker threads` setting in SQL Server and the number of CPU processors on the OpenShift host node. 
    - To find out how to update `pidsLimit` for your OpenShift cluster use [these instructions]( https://github.com/openshift/machine-config-operator/blob/master/docs/ContainerRuntimeConfigDesign.md). Note that OpenShift versions before `4.3.5` had a defect causing the updated value to not take effect. Make sure you upgrade OpenShift to the latest version. 
    - To help you compute the optimal value depending on your environment and planned SQL Server workloads, you can use the estimation and examples below:

    |Number of processors|Default max worker threads|Default workers per processor|Minimum pidsLimit value|
    |--------------------|--------------------------|-----------------------------|-----------------------|
    |          64        |           512            |             16              | 512 + (64 *16) = 1536 |
    |         128        |           512            |             32              | 512 + (128*32) = 4608 |

    > [!NOTE]
    > Other processes (e.g. backups, CLR, Fulltext, SQLAgent) also add some overhead, so add a buffer to the estimated value.

1. Download the custom security context constraint (SCC) [`bdc-scc.yaml`](#bdc-sccyaml-file):

    ```console
    curl https://raw.githubusercontent.com/microsoft/sql-server-samples/master/samples/features/sql-big-data-cluster/deployment/openshift/bdc-scc.yaml -o bdc-scc.yaml
    ```

1. Apply the SCC to the cluster.

    ```console
    oc apply -f bdc-scc.yaml
    ```

    > [!NOTE]
    > The custom SCC for BDC is based on the built-in `nonroot` SCC in OpenShift, with additional permissions. To learn more about security context constraints in OpenShift see [Managing Security Context Constraints](https://docs.openshift.com/container-platform/4.3/authentication/managing-security-context-constraints.html). For a detailed information on what additional permissions are required for big data clusters on top of the `nonroot` SCC, download the whitepaper [here](https://aka.ms/sql-bdc-openshift-security).

3. Create a namespace/project:

   ```console
   oc new-project <namespaceName>
   ```

4. Bind the custom SCC with the service accounts in the namespace where BDC is deployed:

   ```console
   oc create clusterrole bdc-role --verb=use --resource=scc --resource-name=bdc-scc -n <namespaceName>
   oc create rolebinding bdc-rbac --clusterrole=bdc-role --group=system:serviceaccounts:<namespaceName>
   ```

5. Assign appropriate permission to the user deploying BDC. Do one of the following. 

   - If the user deploying BDC has cluster-admin role, proceed to [deploy big data cluster](#deploy-big-data-cluster).

   - If the user deploying BDC is a namespace admin, assign the user cluster-admin local role for the namespace created. This is the preferred option for the user deploying and managing the big data cluster to have namespace level admin permissions.

   ```console
   oc create rolebinding bdc-user-rbac --clusterrole=cluster-admin --user=<userName> -n <namespaceName>
   ```

   The user deploying big data cluster must then log in to the OpenShift console:

   ```console
   oc login -u <deployingUser> -p <password>
   ```

## Deploy big data cluster

1. Install latest [azdata](../azdata/install/deploy-install-azdata.md).

1. Clone one of the built-in configuration files for OpenShift, depending on your target environment (OpenShift on premises or ARO) and deployment scenario. See the *OpenShift specific settings in the deployment configuration files* section below for settings that are specific to OpenShift in the built-in configuration files. For more details on available configuration files see [deployment guidance](deployment-guidance.md).

   List all the available built-in configuration files.

   ```console
   azdata bdc config list
   ```

   To clone one of the built-in configuration files, run below command (optionally, you can replace the profile based on your targeted platform/scenario):

   ```console
   azdata bdc config init --source openshift-dev-test --target custom-openshift
   ```

   For a deployment on ARO, start with one of the `aro-` profiles, that includes default values for `serviceType` and `storageClass` appropriate for this environment. For example:

   ```console
   azdata bdc config init --source aro-dev-test --target custom-openshift
   ```

1. Customize the configuration files control.json and bdc.json. Here are some additional resources that guide you through the customizations for various use cases:

   - [Storage](concept-data-persistence.md)
   - [AD related settings](active-directory-deploy.md)
   - [Other customizations](deployment-custom-configuration.md)

   > [!NOTE]
   > Integrating with Azure Active Directory for BDC is not supported, hence you can not use this authentication method when deploying on ARO.

1. Set [environment variables](deployment-guidance.md#env)

1. Deploy big data cluster

   ```console
   azdata bdc create --config custom-openshift --accept-eula yes
   ```

1. Upon successful deployment, you can log in and list the external cluster endpoints:

   ```console
      azdata login -n mssql-cluster
      azdata bdc endpoint list
   ```

## OpenShift specific settings in the deployment configuration files

SQL Server 2019 CU5 introduces two feature switches to control the collection of pod and node metrics. These parameters  are set to `false`  by default in the built-in profiles for OpenShift since the monitoring containers require [privileged security context](https://www.openshift.com/blog/managing-sccs-in-openshift), which will relax some of the security constraints for the namespace BDC is deployed on.

```json
    "security": {
      "allowNodeMetricsCollection": false,
      "allowPodMetricsCollection": false
}
```

The name of the default storage class in ARO is managed-premium (as opposed to AKS where the default storage class is called default). You would find this in the `control.json` corresponding to `aro-dev-test` and `aro-dev-test-ha`:

```json
    },
    "storage": {
      "data": {
        "className": "managed-premium",
        "accessMode": "ReadWriteOnce",
        "size": "15Gi"
      },
      "logs": {
        "className": "managed-premium",
        "accessMode": "ReadWriteOnce",
        "size": "10Gi"
      }
```

## `bdc-scc.yaml` file

The SCC file for this deployment is:

:::code language="yaml" source="../../sql-server-samples/samples/features/sql-big-data-cluster/deployment/openshift/bdc-scc.yaml":::

## Next steps

[Tutorial: Load sample data into a SQL Server big data cluster](tutorial-load-sample-data.md)
