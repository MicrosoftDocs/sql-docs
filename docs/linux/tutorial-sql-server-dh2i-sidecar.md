---
title: Deploy availability groups with DH2i DxEnterprise sidecar on Kubernetes
description: Set up an availability group in SQL Server on Kubernetes using DH2i DxEnterprise in a sidecar configuration.
author: aravindmahadevan-ms
ms.author: armaha
ms.reviewer: amitkh, randolphwest
ms.date: 04/06/2023
ms.service: sql
ms.subservice: linux
ms.topic: tutorial
ms.custom: intro-deployment
---
# Deploy availability groups with DH2i DxEnterprise in a sidecar configuration

This tutorial explains how to configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] availability groups on a Kubernetes cluster with DH2i DxEnterprise. Using the steps mentioned in this article, learn how to deploy a StatefulSet and use the DH2i DxEnterprise solution to create and configure an availability group (AG). This tutorial consists of the following steps:

> [!div class="checklist"]
> - Create a headless service configuration
> - Create a StatefulSet configuration
> - Create a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] AG, then configure a primary pod and add it to the AG
> - Join two other pods to cluster and add them to the AG
> - Create a database in the AG, and test failover

## Prerequisites

This tutorial shows an example of an AG with three replicas. You need:

- An Azure Kubernetes Service (AKS) or Kubernetes cluster.
- A valid DxEnterprise license with AG features and tunnels enabled. For more information, see the [developer edition](https://dh2i.com/trial/) for nonproduction usage, or [DxEnterprise software](https://dh2i.com/dxenterprise-high-availability/) for production workloads.

## Create the headless service

1. In a Kubernetes cluster, headless services allow your pods to connect to one another using hostnames.

   To create the headless service, Create a YAML file called `headless_services.yml`, with the following sample content:

   ```yaml
   #Headless services for local connections/resolution
   apiVersion: v1
   kind: Service
   metadata:
   name: dxemssql-0
   spec:
   clusterIP: None
   selector:
       statefulset.kubernetes.io/pod-name: dxemssql-0
   ports:
   - name: dxl
       protocol: TCP
       port: 7979
   - name: dxc-tcp
       protocol: TCP
       port: 7980
   - name: dxc-udp
       protocol: UDP
       port: 7981
   - name: sql
       protocol: TCP
       port: 1433
   - name: listener
       protocol: TCP
       port: 14033
   ---
   apiVersion: v1
   kind: Service
   metadata:
   name: dxemssql-1
   spec:
   clusterIP: None
   selector:
       statefulset.kubernetes.io/pod-name: dxemssql-1
   ports:
   - name: dxl
       protocol: TCP
       port: 7979
   - name: dxc-tcp
       protocol: TCP
       port: 7980
   - name: dxc-udp
       protocol: UDP
       port: 7981
   - name: sql
       protocol: TCP
       port: 1433
   - name: listener
       protocol: TCP
       port: 14033
   ---
   apiVersion: v1
   kind: Service
   metadata:
   name: dxemssql-2
   spec:
   clusterIP: None
   selector:
       statefulset.kubernetes.io/pod-name: dxemssql-2
   ports:
   - name: dxl
       protocol: TCP
       port: 7979
   - name: dxc-tcp
       protocol: TCP
       port: 7980
   - name: dxc-udp
       protocol: UDP
       port: 7981
   - name: sql
       protocol: TCP
       port: 1433
   - name: listener
       protocol: TCP
       port: 14033
   ```

1. Run the following command to apply the configuration:

   ```bash
   kubectl apply -f headless_services.yml
   ```

## Create the StatefulSet

1. Create a StatefulSet YAML file with following sample content, and name it `dxemmsql.yml`.

   This StatefulSet configuration creates three DxEMSSQL replicas that utilize persistent volume claims to store their data. Each pod in this StatefulSet comprises two containers: a [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container and a DxEnterprise container. These containers are started separately from one another in a "sidecar" configuration, but DxEnterprise manages the AG replica in the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] container.

   ```yaml
   #DxEnterprise + MSSQL StatefulSet
   apiVersion: apps/v1
   kind: StatefulSet
   metadata:
   name: dxemssql
   spec:
   serviceName: "dxemssql"
   replicas: 3
   selector:
       matchLabels:
       app: dxemssql
   template:
       metadata:
       labels:
           app: dxemssql
       spec:
       securityContext:
           fsGroup: 10001
       containers:
       - name: sql
           image: mcr.microsoft.com/mssql/server:2022-latest
           env:
           - name: ACCEPT_EULA
           value: "Y"
           - name: MSSQL_ENABLE_HADR
           value: "1"
           - name: MSSQL_SA_PASSWORD
           valueFrom:
               secretKeyRef:
               name: mssql
               key: MSSQL_SA_PASSWORD
           volumeMounts:
           - name: mssql
           mountPath: "/var/opt/mssql"
       - name: dxe
           image: dh2i/dxe
           env:
           - name: MSSQL_SA_PASSWORD
           valueFrom:
               secretKeyRef:
               name: mssql
               key: MSSQL_SA_PASSWORD
           volumeMounts:
           - name: dxe
           mountPath: "/etc/dh2i"
   volumeClaimTemplates:
   - metadata:
       name: dxe
       spec:
       accessModes:
       - ReadWriteOnce
       resources:
           requests:
           storage: 1Gi
   - metadata:
       name: mssql
       spec:
       accessModes:
       - ReadWriteOnce
       resources:
           requests:
           storage: 1Gi
   ```

1. Create a credential for the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance:

   ```bash
   kubectl create secret generic mssql --from-literal=MSSQL_SA_PASSWORD="<password>"
   ```

1. Apply the StatefulSet configuration:

   ```bash
   kubectl apply -f dxemssql.yaml
   ```

1. Verify the status of the pods, and proceed to the next step when the pod's status becomes `running`:

   ```bash
   kubectl get pods
   kubectl describe pods
   ```

## Create the availability group

In this section, you create the AG, configure the primary pod, and add it to the AG.

1. First, you must activate your DxEnterprise license using the following command. You need a valid activation code for this step:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli activate-server AAAA-BBBB-CCCC-DDDD --accept-eula
   ```

1. Add a virtual host to the cluster:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli cluster-add-vhost vhost1 "" dxemssql-0
   ```

1. Encrypt the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] sysadmin password for DxEnterprise. The encrypted password is used to create the AG in the later steps:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli encrypt-text p@ssw0rd
   ```

1. Add an AG to the virtual host. The [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] sysadmin password must be encrypted using the output from the previous step, and used in the following example:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli add-ags vhost1 ags1 "dxemssql-0|mssqlserver|sa|entergeneratedpasswordinpreviousstephere=|5022|synchronous_commit|0"
   ```

1. Set a cluster passkey and enable hostname lookup in DxEnterprise's global settings:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli cluster-set-secret-ex clusp@ssw0rd
   kubectl exec -c dxe dxemssql-0 -- dxcli set-globalsetting membername.lookup true
   ```

## Join two pods to cluster and add them to the availability group

1. To join the pods to DxEnterprise cluster, you need to activate the DxEnterprise license and run the command to join second pod and add it to existing AG using the `dxcli add ags-node` command. You need a valid activation code for this step:

   ```bash
   kubectl exec -c dxe dxemssql-1 -- dxcli activate-server AAAA-BBBB-CCCC-DDDD --accept-eula
   kubectl exec -c dxe dxemssql-1 -- dxcli join-cluster-ex dxemssql-0 p@ssw0rd
   ```

1. Use the encrypted password output you get on the previous step on the following command:

   ```bash
   kubectl exec -c dxe dxemssql-1 -- dxcli add-ags-node vhost1 ags1 "dxemssql-1|mssqlserver|sa|entergeneratedpasswordinpreviousstephere=|5022|synchronous_commit|0"
   ```

1. Activate the DxEnterprise license for the third pod:

   ```bash
   kubectl exec -c dxe dxemssql-2 -- dxcli activate-server AAAA-BBBB-CCCC-DDDD --accept-eula
   ```

1. Join the third pod to the DxEnterprise cluster, and add the pod to the existing AG:

   ```bash
   kubectl exec -c dxe dxemssql-2 -- dxcli join-cluster-ex dxemssql-0 p@ssw0rd
   kubectl exec -c dxe dxemssql-2 -- dxcli add-ags-node vhost1 ags1 "dxemssql-2|mssqlserver|sa|6pnFaDrRS+W/F+dkRuPKAA==|5022|synchronous_commit|0"
   ```

1. Create database in the AG:

   ```bash
   kubectl exec -c sql dxemssql-0 -- /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P p@ssw0rd -Q "create database db1"
   ```

1. Add the database to the AG and verify that it was added successfully:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli add-ags-databases vhost1 ags1 db1
   kubectl exec -c dxe dxemssql-0 -- dxcli get-ags-detail vhost1 ags1 | kubectl exec -c dxe dxemssql-0 -- dxcli format-xml
   ```

## Test failover

1. To verify the state of the AG before failover, check the status of the database of each AG member, and find out which pod is the primary:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli get-ags-detail vhost1 ags1 | kubectl exec -c dxe dxemssql-0 -- dxcli format-xml
   ```

1. Delete the primary pod:

   ```bash
   kubectl delete pod <PRIMARY_POD>
   ```

1. Now check the status of AG. The database should be active and synchronized on the other pods. After few minutes, the deleted pod automatically rejoins the cluster and synchronizes with the AG:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli get-ags-detail
   ```

1. To set the rejoined pod role back to primary, run the following command:

   ```bash
   kubectl exec -c dxe dxemssql-0 -- dxcli vhost-start-node vhost1 dxemssql-0
   ```

## Next steps

- [Deploy SQL Server Linux containers on Kubernetes with StatefulSets](sql-server-linux-kubernetes-best-practices-statefulsets.md)
- [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](sql-server-linux-containers-ad-auth-adutil-tutorial.md)
- [Deploy availability group with DH2i for SQL Server containers on AKS](tutorial-sql-server-containers-kubernetes-dh2i.md)
