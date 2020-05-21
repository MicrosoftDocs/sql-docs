---
title: Security concepts
titleSuffix: SQL Server big data clusters
description: This article describes security concepts for SQL Server Big Data Clusters. This content includes describing the cluster endpoints and cluster authentication.
author: nelgson 
ms.author: negust
ms.reviewer: mikeray
ms.date: 10/23/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Security concepts for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article will cover the key security-related concepts in the big data cluster

[!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] provide coherent and consistent authorization and authentication. A big data cluster can be integrated with Active Directory (AD) through a fully automated deployment that sets up AD integration against an existing domain. Once a big data cluster is configured with AD integration, you can leverage existing identities and user groups for unified access across all endpoints. In addition, once you have created external tables in SQL Server, you can control access to data sources by granting access to external tables to AD users and groups, thus centralizing the data access policies to a single location.

In this 14-minute video you will get an overview of big data cluster security:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Overview-Big-Data-Cluster-Security/player?WT.mc_id=dataexposed-c9-niner]


## Authentication

The external cluster endpoints support AD authentication. Use your AD identity to authenticate to the big data cluster.

### Cluster endpoints

There are five entry points to the big data cluster

* Master Instance - TDS endpoint for accessing SQL Server Master Instance in the cluster, using database tools and applications like SSMS or Azure Data Studio. When using HDFS or SQL Server commands from azdata, the tool will connect to the other endpoints, depending on the operation.

* Gateway to access HDFS files, Spark (Knox) - HTTPS endpoint for accessing services like webHDFS and Spark.

* Cluster Management Service (Controller) endpoint - Big data cluster  management service that exposes REST APIs for managing the cluster. Azdata tool requires connecting to this endpoint.

* Management Proxy - For access to the Logs search dashboard and Metrics dashboard.

* Application proxy - Endpoint for managing applications deployed inside the big data cluster.

![Cluster endpoints](media/concept-security/cluster_endpoints.png)

Currently, there is no option of opening up additional ports for accessing the cluster from the outside.

## Authorization

Throughout the cluster, integrated security between different components allows the original user’s identity to be passed through when issuing queries from Spark and SQL Server, all the way to HDFS. As mentioned above, the various external cluster endpoints support AD authentication.

There are two levels of authorization checks in the cluster for managing data access. Authorization in the context of big data is done in SQL Server, using the traditional SQL Server permissions on objects and in HDFS with control lists (ACLs), which associate user identities with specific permissions.

A secure big data cluster implies consistent and coherent support for authentication and authorization scenarios, across both SQL Server and HDFS/Spark. Authentication is the process of verifying the identity of a user or service and ensuring they are who they are claiming to be. Authorization refers to granting or denying of access to specific resources based on the requesting user's identity. This step is performed after a user is identified through authentication.

Authorization in Big Data context is usually performed through access control lists (ACLs), which associate user identities with specific permissions. HDFS supports authorization by limiting access to service APIs, HDFS files, and job execution.

## Encryption and other security mechanisms

Encryption of communication between clients to the external endpoints, as well as between components inside the cluster are secured with TLS/SSL, using certificates.

All SQL Server to SQL Server communication, such as the SQL master instance communicating with a data pool, is secured using SQL logins.

> [!IMPORTANT]
>  Big data clusters uses etcd to store credentials. As a best practice, you must ensure your Kubernetes cluster is configured to use etcd encryption at rest. By default, secrets stored in etcd are unencrypted. Kubernetes documentation provides details on this administrative task: https://kubernetes.io/docs/tasks/administer-cluster/kms-provider/ and 
https://kubernetes.io/docs/tasks/administer-cluster/encrypt-data/.


## Basic administrator login

You can choose to deploy the cluster in either AD mode, or using only basic administrator login. Only using basic administrator login is not a production supported security mode, and intended for evaluation of the product.

Even if you choose Active directory mode, basic logins will be created for the cluster administrator. This feature provides alternative access, in case AD connectivity is down.

Upon deployment, this basic login will be given administrator permissions in the cluster. The login user will be system administrator in SQL Server Master Instance and an administrator in the cluster controller.
Hadoop components do not support mixed mode authentication, which means that a basic administrator login can't be used to authenticate to Gateway (Knox).

The login credentials you need to define during deployment include.

Cluster admin username:

 + `AZDATA_USERNAME=<username>`

Cluster admin password:  
 + `AZDATA_PASSWORD=<password>`

> [!NOTE]
> Note that in non-AD mode, the username "root" has to be used in combination with the above password, for authenticating to the Gateway (Knox) for access to HDFS/Spark.

## Kubernetes RBAC model & impact on users managing BDC

This following section describes the permissions required for the users managing big data clusters.

> [!NOTE]
> For additional resources on Kubernetes RBAC model see [Using RBAC Authorization - Kubernetes](https://kubernetes.io/docs/reference/access-authn-authz/rbac/) and [Using RBAC to define and apply permissions - OpenShift](https://docs.openshift.com/container-platform/4.4/authentication/using-rbac.html).

### Role required for deployment

BDC control plane uses service account, `sa-mssql-controller` to orchestrate the provisioning of the cluster pods, services, high availability, monitoring, etc. When BDC deployment starts (for example, `azdata bdc create`), `azdata` does the following following:

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

### Cluster role required for pods and nodes metrics collection

Starting with SQL Server 2019 CU5, Telegraf requires a service account with cluster wide role permissions to collect the pod and node metrics. During the deployment (or upgrade for existing deployments), we attempt to create the necessary service account and cluster role, but if the user deploying the cluster or performing the upgrade does not have sufficient permissions, deployment or upgrade will still proceed with a warning and succeed. In this case, the pod & node metrics will not be collected. The user deploying the cluster must ask a cluster administrator to create the role and service account (either before or after the deployment or upgrade). After they are created, BDC uses them. 

Here is a script that shows how to create the required artifacts:
​
```console
export CLUSTER_NAME=mssql-cluster
kubectl create -f - <<EOF
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRole
metadata:
  name: ${CLUSTER_NAME}:cr-mssql-metricsdc-reader
rules:
- apiGroups:
  - '*'
  resources:
  - pods
  - nodes/stats
  verbs:
  - get
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRoleBinding
metadata:
  name: ${CLUSTER_NAME}:crb-mssql-metricsdc-reader
roleRef:
  apiGroup: rbac.authorization.k8s.io
  kind: ClusterRole
  name: ${CLUSTER_NAME}:cr-mssql-metricsdc-reader
subjects:
- kind: ServiceAccount
  name: sa-mssql-metricsdc-reader
  namespace: ${CLUSTER_NAME}
EOF
```

The service account, cluster role and the cluster role binding can be created either before or post BDC deployment. Kubernetes automatically updates the permission for the Telegraf service account. If these are created as a pod deployment, you will see a few minutes delay in the pod and node metrics being collected.

```console
export CLUSTER_NAME=mssql-cluster
kubectl create -f - <<EOF
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRole
metadata:
  name: ${CLUSTER_NAME}:cr-mssql-metricsdc-reader
rules:
- apiGroups:
  - '*'
  resources:
  - pods
  - nodes/stats
  verbs:
  - get
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRoleBinding
metadata:
  name: ${CLUSTER_NAME}:crb-mssql-metricsdc-reader
roleRef:
  apiGroup: rbac.authorization.k8s.io
  kind: ClusterRole
  name: ${CLUSTER_NAME}:cr-mssql-metricsdc-reader
subjects:
- kind: ServiceAccount
  name: sa-mssql-metricsdc-reader
  namespace: ${CLUSTER_NAME}
EOF
```

​> [!NOTE]
> The service account, cluster role and the cluster role binding can be created either before or post BDC deployment. Kubernetes will automatically update the permission for the Telegraf service account. If these are created pod deployment, you will see a few minutes delay in the pod and node metrics being collected.

> [!NOTE]
> SQL Server 2019 CU5 introduces two feature switches to control the collection of pod and node metrics. By default these parameters are set to true in all environment targets, except OpenShift where the default is overridden. 

You can customize the these settings in  the security section in the `control.json` deployment configuration file:

```json
  "security": {
    …
    "allowNodeMetricsCollection": false,
    "allowPodMetricsCollection": false
  }
```

If these settings are set to `false`, there is not going to be an attempt to create the service account, cluster role, and the binding for Telegraf.

## Next steps

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following resources:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
- [Workshop: Microsoft [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] Architecture](https://github.com/Microsoft/sqlworkshops/tree/master/sqlserver2019bigdataclusters)
