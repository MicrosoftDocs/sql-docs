---
title: Kubernetes RBAC
titleSuffix: SQL Server Big Data Clusters
description: This article describes how SQL Server Big Data Clusters uses RBAC with Kubernetes.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 02/11/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Kubernetes RBAC model & impact on users and service accounts managing SQL Server 2019 Big Data Clusters

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article describes the permissions requirements for users managing big data clusters and the semantics around default service account and Kubernetes access from within the big data cluster.

> [!NOTE]
> For additional resources on Kubernetes RBAC model see [Using RBAC Authorization - Kubernetes](https://kubernetes.io/docs/reference/access-authn-authz/rbac/) and [Using RBAC to define and apply permissions - OpenShift](https://docs.openshift.com/container-platform/4.4/authentication/using-rbac.html).

## Role required for deployment

SQL Server 2019 Big Data Clusters uses service accounts (such as `sa-mssql-controller` or `master`) to orchestrate the provisioning of the cluster pods, services, high availability, monitoring, etc. When BDC deployment starts (for example, `azdata bdc create`), [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] does the following following:

1. Checks if provided namespace exists.
2. If it does not exist, it creates one and applies the `MSSQL_CLUSTER` label.
3. Creates the `sa-mssql-controller` service account.
4. Creates a `<namespaced>-admin` role with full permissions on the namespace or project but not cluster level permissions.
5. Creates a role assignment of that service account to the role.

Once these steps complete, the control plane pods are provisioned and the service account deploys the rest of big data cluster.  

Consequentially, the deploying user must have permissions to:

- List the namespaces in the cluster (1).
- Patch the new or existing namespace with the label (2).
- Create the service account `sa-mssql-controller`, the `<namespaced>-admin` role and the role binding (3-5).

The default `admin` role does not have these permissions, so the user deploying the big data cluster must have at least namespace level admin permissions.

## Cluster role required for pods and nodes metrics collection

Starting with SQL Server 2019 CU5, Telegraf requires a service account with cluster wide role permissions to collect the pod and node metrics. During the deployment (or upgrade for existing deployments), we attempt to create the necessary service account and cluster role, but if the user deploying the cluster or performing the upgrade does not have sufficient permissions, deployment or upgrade will still proceed with a warning and succeed. In this case, the pod & node metrics will not be collected. The user deploying the cluster must ask a cluster administrator to create the role and service account (either before or after the deployment or upgrade). After they are created, BDC uses them. 

Here are the steps to show how to create the required artifacts:

1. Create a *metrics-role.yaml* file with below content. Make sure to replace the *\<clusterName\>* placeholders  with the name of your big data cluster.

   ```yaml
   apiVersion: rbac.authorization.k8s.io/v1
   kind: ClusterRole
   metadata:
     name: <clusterName>:cr-mssql-metricsdc-reader
   rules:
   - apiGroups:
     - '*'
     resources:
     - pods
     - nodes/stats
     - nodes/proxy
     verbs:
     - get
   ---
   apiVersion: rbac.authorization.k8s.io/v1
   kind: ClusterRoleBinding
   metadata:
     name: <clusterName>:crb-mssql-metricsdc-reader
   roleRef:
     apiGroup: rbac.authorization.k8s.io
     kind: ClusterRole
     name: <clusterName>:cr-mssql-metricsdc-reader
   subjects:
   - kind: ServiceAccount
     name: sa-mssql-metricsdc-reader
     namespace: <clusterName>
   ```

2. Create the cluster role and the cluster role binding:

   ```bash
   kubectl create -f metrics-role.yaml
   ```

The service account, cluster role and the cluster role binding can be created either before or post BDC deployment. Kubernetes automatically updates the permission for the Telegraf service account. If these are created as a pod deployment, you will see a few minutes' delay in the pod and node metrics being collected.

> [!NOTE]
> SQL Server 2019 CU5 introduces two feature switches to control the collection of pod and node metrics. By default these parameters are set to true in all environment targets, except OpenShift where the default is overridden. 

You can customize these settings in the security section in the `control.json` deployment configuration file:

```json
  "security": {
    …
    "allowNodeMetricsCollection": false,
    "allowPodMetricsCollection": false
  }
```

If these settings are set to `false`, BDC deployment workflow will not attempt to create the service account, cluster role, and the binding for Telegraf.

## Default service account usage from within a BDC pod

For a tighter security model, SQL Server 2019 CU5 disabled mounting by default credentials for the default Kubernetes service account within the BDC pods. This applies to both new and upgraded deployments in CU5 or later versions.
The credential token inside the pods can be used to access the Kubernetes API server, and the level of permissions depends on the Kubernetes authorization policy settings. If you have specific use cases that require reverting to the previous CU5 behavior, in CU6 we are introducing a new feature switch so you can turn on the auto-mount at deployment time only. You can do so by using the control.json configuration deployment file and setting *automountServiceAccountToken* to *true*. Run this command to update this setting in your *control.json* custom configuration file using [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]: 

``` bash
azdata bdc config replace -c custom-bdc/control.json -j "$.security.automountServiceAccountToken=true"
```
