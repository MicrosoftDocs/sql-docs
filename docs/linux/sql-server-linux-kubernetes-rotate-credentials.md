---
title: SQL Server Always On availability group Kubernetes rotate credentials environment variables
description: This article explains the environment variables to rotate credentials for a SQL Server Kubernetes Always On availability group on Kubernetes.
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

# SQL Server Always On availability group Kubernetes environment variables to rotate credentials

Rotate credentials on a SQL Server Always On availability group in Kubernetes to update the SQL Server instance `sa` password, set a new master key password, or rotate the database mirroring endpoint certificates. 

## Rotate credentials environment variables

* `MSSQL_K8S_SA_PASSWORD`
  * Required
  * **Description**: The current `sa` password of the SQL Server instance.

* `MSSQL_K8S_STATEFULSET_NAME `
  * Required
  * **Description**: The name of the SQL Server StatefulSet, the same as the custom resource name .

* `MSSQL_K8S_NAMESPACE`
  * Optional
  * **Description**: Kubernetes namespace of the availability group.

* `MSSQL_ROTATE_CERT` 
  * Optional
  * **Description**: Boolean to rotate the database mirroring endpoint certificate.

* `MSSQL_K8S_MASTER_KEY_PASSWORD`
  * Optional
  * **Description**: The master key password.

* `MSSQL_K8S_NEW_SA_PASSWORD`
  * Optional
  * **Description**: The new `sa` password the SQL Server instance. 

