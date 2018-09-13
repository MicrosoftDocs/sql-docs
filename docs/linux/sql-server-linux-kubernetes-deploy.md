---
title: SQL Server Always On availability group Kubernetes operator global requirements
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group operator global requirements
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 08/09/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# SQL Server Always On availability group Kubernetes operator parameters

An Always On availability group on Kubernetes requires an operator. The operator is described in a .yaml file.  See an example of the specification in [this tutorial](tutorial-sql-server-ag-kubernetes.md).

This article explains the operator global environment variables.

## Example

The following example describes a deployment for the `mssql-operator`.

[operator.yaml](http://sqlhelsinki.visualstudio.com/_git/pm-tools?path=%2Fkubernetes-ag-samples%2Fazure-kubernetes-service-sql-ag-example&version=GBmaster#path=%2Fkubernetes-ag-samples%2Fazure-kubernetes-service-sql-ag-example%2Foperator.yaml&version=GBmaster)

## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)