---
title: SQL Server Always On availability group Kubernetes specification
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group specification
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 7/16/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
---

# SQL Server Always On availability group Kubernetes specification

To configure an Always On availability group on Kubernetes, create a specification. The specification is a .yaml file.  See an example of the specification in [this tutorial](tutorial-sql-server-ag-kubernetes.md).

This article explains the parameters in the specification.

## Parameters

* `sqlServerContainer` 
  * Required
  * **Description**: SQL Container spec. Should at least contain image path. In addition, it can have any env variable available for SQL Server container.
  * **Example**

   ```yaml
     sqlServerContainer:
     image: private-repo.microsoft.com/mssql-private-preview/mssql-server
   ```

* `saPassword`
  * Required
  * **Description**: The `sa` password for the sql server. Sets the initial password for the SQL Server instance. Changing this password through this field is not supported. Rotate the password instead.
  * **Example**

   ```yaml
     saPassword:
      secretKeyRef:
        name: sql-secrets
        key: sapassword
   ```

* `masterKeyPassword` 
  * Required
  * **Description**:  The server master key password for sql server. Used and for db mirroring certificates, logins and passwords. It is regeneratable. You can change it post-deployment. DB mirroring logins are generated automatically as well.
  * **Example**

   ```yaml
   masterKeyPassword:
     secretKeyRef:
       name: sql-secrets
       key: masterkeypassword
   ```

* `agentsContainerImage`
  * Required
  * **Description**: The Agent container image name.
  * **Example**

   ```yaml
    agentsContainerImage: private-repo.microsoft.com/mssql-private-preview/mssql-server-k8s-agents
   ```

* `acceptEula`
  * Required
  * **Description**: Boolean acceptance of the SQL Server EULA.
  * **Example**

   ```yaml
    acceptEula: true
   ```

* `instanceRootVolume`
  * Required if `instanceRootVolumeClaimTemplate` is not present.
  * **Description**: Volume source for the sql server container. For more information, see [Kubernetes storage volumes](https://kubernetes.io/docs/concepts/storage/volumes/).
  * **Example**

   ```yaml
     instanceRootVolume:
      persistentVolumeClaim:
       claimName: <claim name> 
   ```

* `instanceRootVolumeClaimTemplate` 
  * Required if `instanceRootVolume` is not present.
  * **Description**: Persistent Volume claim template for instance root [Kubernetes storage persistent volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/).

* `pid`
  * Optional
  * **Description**: The SQL Server edition or product key. Defaults to Developer edition. See [Environment variables](sql-server-linux-configure-environment-variables.md).

* `monitorPolicy`
  * Optional 
  * **Description**: Integer health level used with `sp_server_diagnostics` to trigger failovers. Minimum: 0. Maximum: 5. Same semantics as monitor policy levels in Pacemaker/WSFC. 2 does not exist in Linux.

* `sqlServerPod`
  * Optional
  * **Description**: Pod spec with overrides for the SQL Server StatefulSet pod. Used when you use a private image. For more information, see [Kubernetes pods](http://kubernetes.io/docs/concepts/workloads/pods/pod/).

* `initSQLPod`
  * Optional
  * **Description**: Pod spec with overrides for the init-sql job pod. Used when you use a private image.

* `connectionTimeoutSec`
  * Optional
  * **Description**: Connection timeout in seconds for the AG agents to connect to SQL Server. Minimum: 1 second. Default: 30 seconds.

* `queryCommandTimeoutSec`
  * Optional
  * **Description**: The generic SQL query command timeout in seconds for waiting for data from a query. Minimum: 1. Default value: 10.
  
* `joinCommandTimeoutSec`
  * Optional
  * **Description**: The sql query command timeout in seconds for waiting for data from a longer `ALTER AVAILABILITY GROUP <group_name> JOIN;`. Minimum: 1. Default value: 60. Recommended minimum is 60.

* `transientDBHealthTimeoutSec`
  * Optional
  * **Description**: Timeout in seconds to wait for transient database states to come online after becoming the primary before triggering a failover. Minimum: 1. Default: 180. Used when `db_failover` is `ON`. Kubernetes Availability Group is always created with this setting `ON`, it can be updated to `OFF`.`

* `sqlPostInitScript`
  * Optional
  * **Description**: Post initialization sql script to run. This is a custom SQL script that runs after every update of the [Kubernetes custom resource](http://kubernetes.io/docs/concepts/extend-kubernetes/api-extension/custom-resources/). For example, resource create, resource change property, or resource upgrade.

* `availabilityGroups` 
  * Optional
  * **Description**: List of availability groups the server is a part of and the server replica mode.
  * **Example**

   ```yaml
     availabilityGroups:
      name: <availabilityGroupName>
       availabilityMode: <synchronousCommit | asynchronousCommit | configurationOnly> 
   ``` 

   ## Example
   
   [!INCLUDE[kubernetes-ag-sql-statefulset-yaml](../includes/kubernetes-ag-sql-statefulset-yaml.md)]